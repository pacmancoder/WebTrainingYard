using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

using ManagerApp_GUI.View;

namespace ManagerApp_GUI.Model
{
    class FilterManager
    {
        IFilterManagerView _view;
        private OdbcConnection _dbConnection;
        private int _selectedFilter = -1;
        private int _selectedFilterCase = -1;

        public FilterManager(IFilterManagerView view)
        {
            _view = view;
            _dbConnection = new OdbcConnection("Dsn=MySQL32");
            _dbConnection.Open();
        }

        public void UpdateFiltersList()
        {
            _view.ClearFiltersList();
            _view.ClearFilterCaseList();
            var command = new OdbcCommand("SELECT id, name FROM Specification", _dbConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                _view.AddFilterToList(reader.GetInt32(0), reader.GetString(1));
            }
            reader.Close();
            _selectedFilterCase = -1;
        }

        private void UpdateFilterCasesList()
        {
            _view.ClearFilterCaseList();
            var command = new OdbcCommand($"SELECT id, description FROM SpecificationCase where specification_id = {_selectedFilter}", _dbConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                _view.AddFilterCaseToList(reader.GetInt32(0), reader.GetString(1));
            }
            reader.Close();
            _selectedFilterCase = -1;
        }

        public void SelectFilter(int id)
        {
            _selectedFilter = id;

            UpdateFilterCasesList();

            var command = new OdbcCommand($"SELECT name FROM Specification WHERE id = {id}", _dbConnection);
            var reader = command.ExecuteReader();
            reader.Read();
            _view.SetCurrentFilterName(reader.GetString(0));
        }

        public void selectFilterCase(int id)
        {
            _selectedFilterCase = id;
            _view.SetFilterCaseNameControlActive(true);

            var command = new OdbcCommand($"SELECT description FROM SpecificationCase WHERE id = {id}", _dbConnection);
            var reader = command.ExecuteReader();
            reader.Read();
            _view.SetCurrentFilterCaseName(reader.GetString(0));
        }

        public void saveFilterName(string name)
        {
            if (_selectedFilter < 0) return;
            var command = new OdbcCommand($"UPDATE Specification SET name = \"{name}\" WHERE id = {_selectedFilter}", _dbConnection);
            command.ExecuteNonQuery();

            UpdateFiltersList();
        }

        public void addFilter(string name)
        {
            var command = new OdbcCommand($"INSERT INTO Specification(name) VALUES(\"{name}\")", _dbConnection);
            command.ExecuteNonQuery();
            _selectedFilter = -1;

            UpdateFiltersList();
        }

        public void deleteFilter()
        {
            if (_selectedFilter < 0) return;
            try
            {
                var command = new OdbcCommand($"DELETE FROM Specification WHERE id = {_selectedFilter}", _dbConnection);
                command.ExecuteNonQuery();
                UpdateFiltersList();
            } catch
            {
                _view.ShowError("can't delete filter - some items are still assigned to this filter");
            }
            _selectedFilter = -1;
        }

        public void saveFilterCase(string name)
        {
            if (_selectedFilterCase < 0) return;
            var command = new OdbcCommand($"UPDATE SpecificationCase SET description = \"{name}\" WHERE id = {_selectedFilterCase}", _dbConnection);
            command.ExecuteNonQuery();

            UpdateFilterCasesList();
        }

        public void addFilterCase(string name)
        {
            if (_selectedFilter < 0) return;
            var command = new OdbcCommand($"INSERT INTO SpecificationCase(specification_id, description) VALUES({_selectedFilter}, \"{name}\")", _dbConnection);
            command.ExecuteNonQuery();
            _selectedFilterCase = -1;
            UpdateFilterCasesList();
        }

        public void deleteFilterCase()
        {
            if (_selectedFilterCase < 0) return;
            try
            {
                var command = new OdbcCommand($"DELETE FROM SpecificationCase WHERE id = {_selectedFilterCase}", _dbConnection);
                command.ExecuteNonQuery();
                UpdateFiltersList();
                _selectedFilterCase = -1;
                UpdateFilterCasesList();
            }
            catch
            {
                _view.ShowError("can't delete filter case - some items are still assigned to this filter case");
            }
        }

    }
}
