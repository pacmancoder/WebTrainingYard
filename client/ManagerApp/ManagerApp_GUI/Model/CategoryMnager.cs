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
            _rootCategoryId = 0;
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

        public void ChangeCurrentRootCategory(int id)
        {
            // check for leaf category
            string query =
                $"SELECT COUNT(*) FROM Category WHERE parent_category = {id}";
            var dbCommand = new OdbcCommand(query, _dbConnection);
            var reader = dbCommand.ExecuteReader();
            reader.Read();

            if (reader.GetInt16(0) == 0)
            {
                return;
            }
            // change root
            _rootCategoryId = id;
            UpdateCategoryList();
        }

        public void MoveToRoot()
        {
            string query =
                $"SELECT parent_category FROM Category WHERE id = {_rootCategoryId}";
            var dbCommand = new OdbcCommand(query, _dbConnection);
            var reader = dbCommand.ExecuteReader();
            if (reader.Read())
            {
                ChangeCurrentRootCategory(reader.GetInt16(0));
            }            
        }

        private OdbcConnection _dbConnection;
        private ICategoryManagerView _view;
        private int _rootCategoryId;
    }
}
