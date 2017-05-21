using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using ManagerApp_GUI.View;

namespace ManagerApp_GUI.Model
{
    class CategoryMnager
    {
        public CategoryMnager(ICategoryManagerView view)
        {
            _dbConnection = new OdbcConnection("Dsn=MySQL32");
            _dbConnection.Open();
            _view = view;
            _rootCategoryId = 1;
            _selectedCategoryId = 1;
            _selectedCategoryName = "";
            _filterActions = new Dictionary<int, bool>();
        }

        public void UpdateCategoryList()
        {
            _view.ClearCategoryList();

            string query = 
                $"SELECT id, name FROM Category WHERE parent_category = {_rootCategoryId}";

            var dbCommand = new OdbcCommand(query, _dbConnection);

            var reader = dbCommand.ExecuteReader();
            while (reader.Read())
            {
                _view.AddCategoryToList(reader.GetInt16(0), reader.GetString(1));
            }
        }

        public void UpdateFiltersList(int category = 1)
        {
            string query;
            if (category == 1)
            {
                _view.ClearFiltersList();
                _view.BlockEditControls();
                query =
                    $@"SELECT specification.id, specification.name FROM specification";
            } else
            {
                query =
                    $@"SELECT 
                    specification.id, 
                    CASE WHEN specification.id IN
                        (SELECT categoryhasspecification.specification_id
                    FROM categoryhasspecification
                    WHERE categoryhasspecification.category_id = {category})
                    THEN 1 ELSE 0 END AS `active` 
                FROM specification";
            }
            var dbCommand = new OdbcCommand(query, _dbConnection);
            var reader = dbCommand.ExecuteReader();
            while (reader.Read())
            {
                if (category == 1)
                {
                    _view.AddFilterToList(reader.GetInt32(0), reader.GetString(1));
                } else
                {
                    _view.SetFilterCheckbox(reader.GetInt32(0), reader.GetInt32(1) == 1);
                }                    
            }
        }

        public void ChangeCurrentRootCategory(int id)
        {
            // check for leaf category
            string query =
                $"SELECT COUNT(*) FROM Category WHERE parent_category = {id}";
            var dbCommand = new OdbcCommand(query, _dbConnection);
            var reader = dbCommand.ExecuteReader();
            reader.Read();

            if (reader.GetInt16(0) == 1)
            {
                return;
            }
            // change root        
            _rootCategoryId = id;
            UpdateCategoryList();
            _view.BlockEditControls();
            _view.SetCategoryId("-");
            _view.SetCategoryName("");
            _selectedCategoryId = 1;
            _filterActions.Clear();
            _selectedCategoryName = "";
        }

        public void MoveToRoot()
        {
            string query =
                $"SELECT parent_category FROM Category WHERE id = {_rootCategoryId}";
            var dbCommand = new OdbcCommand(query, _dbConnection);
            var reader = dbCommand.ExecuteReader();
            if (reader.Read())
            {
                if (!reader.IsDBNull(0)) {
                    ChangeCurrentRootCategory(reader.GetInt16(0)); 
                }
            }
            _view.BlockEditControls();
            _view.SetCategoryId("-");
            _view.SetCategoryName("");
            _selectedCategoryId = 1;
            _filterActions.Clear();
            _selectedCategoryName = "";
        }

        public void SelectCategory(int id)
        {
            _selectedCategoryId = id;
            _filterActions.Clear();

            _view.SetCategoryId(id.ToString());

            string nameQuery =
                $"SELECT Category.name FROM Category WHERE Category.id = {id}";
            var nameDbCommand = new OdbcCommand(nameQuery, _dbConnection);
            var nameReader = nameDbCommand.ExecuteReader();
            if (nameReader.Read())
            {
                _selectedCategoryName = nameReader.GetString(0);
                _view.SetCategoryName(nameReader.GetString(0));
            }

            UpdateFiltersList(id);

            _view.UnblockEditControls();
        }

        public void AddFilterAction(int filterId, bool active)
        {
            _filterActions[filterId] =  active;
        }

        public void ChangeCategoryName(string name)
        {
            _selectedCategoryName = name;
        }
        public void AlterCategory()
        {
            if (_selectedCategoryId == 1)
            {
                return;
            }

            string query = $"UPDATE Category SET name = '{_selectedCategoryName}' WHERE id = {_selectedCategoryId}";
            var dbCommand = new OdbcCommand(query, _dbConnection);
            dbCommand.ExecuteNonQuery();
            
            foreach(var pair in _filterActions)
            {
                if (pair.Value)
                {
                    var replaceQuery = $"REPLACE INTO CategoryHasSpecification VALUES({_selectedCategoryId},{pair.Key})";
                    dbCommand = new OdbcCommand(replaceQuery, _dbConnection);
                    dbCommand.ExecuteNonQuery();
                } else
                {
                    var deleteQuery = $@"
                        DELETE FROM CategoryHasSpecification WHERE 
                            category_id = '{_selectedCategoryId}' AND specification_id = '{pair.Key}'";
                    dbCommand = new OdbcCommand(deleteQuery, _dbConnection);
                    dbCommand.ExecuteNonQuery();
                }
            }
        }

        public void CreateCategoryInRoot()
        {
            if (_selectedCategoryName != "")
            {
                string query =
                    $"INSERT INTO Category(name, parent_category) VALUES('{_selectedCategoryName}', {_rootCategoryId})";
                var dbCommand = new OdbcCommand(query, _dbConnection);
                var reader = dbCommand.ExecuteNonQuery();
                _view.BlockEditControls();
                _view.SetCategoryId("-");
                _view.SetCategoryName("");
                _selectedCategoryId = 1;
                _filterActions.Clear();
                _selectedCategoryName = "";

                UpdateCategoryList();
            }
        }

        public void CreateCategoryAsChild()
        {
            if (_selectedCategoryName != "" && _selectedCategoryId != 1)
            {
                string query =
                    $"INSERT INTO Category(name, parent_category) VALUES('{_selectedCategoryName}', {_selectedCategoryId})";
                var dbCommand = new OdbcCommand(query, _dbConnection);
                var reader = dbCommand.ExecuteNonQuery();

                ChangeCurrentRootCategory(_selectedCategoryId);
                UpdateCategoryList();

                _view.BlockEditControls();
                _view.SetCategoryId("-");
                _view.SetCategoryName("");
                _selectedCategoryId = 1;
                _filterActions.Clear();
                _selectedCategoryName = "";
            }
        }

        public void DeleteCategory()
        {
            if (_selectedCategoryId != 1)
            {
                try
                {
                    string query =
                        $"DELETE FROM Category WHERE id = {_selectedCategoryId}";
                    var dbCommand = new OdbcCommand(query, _dbConnection);
                    var reader = dbCommand.ExecuteNonQuery();

                    UpdateCategoryList();

                    _view.BlockEditControls();
                    _view.SetCategoryId("-");
                    _view.SetCategoryName("");
                    _selectedCategoryId = 1;
                    _filterActions.Clear();
                    _selectedCategoryName = "";
                } catch {
                    _view.ShowError($"Can't delete category - please move all assigned items from {_selectedCategoryName} to another place");
                }
            }
        }

        private OdbcConnection _dbConnection;
        private ICategoryManagerView _view;

        private int _rootCategoryId;

        private int _selectedCategoryId;
        private string _selectedCategoryName;
        Dictionary<int, bool> _filterActions;

    }
}
