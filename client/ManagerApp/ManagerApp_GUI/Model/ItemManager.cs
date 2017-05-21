using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

using System.Data.Odbc;
using ManagerApp_GUI.View;

namespace ManagerApp_GUI.Model
{
    class ItemManager
    {
        IItemManagerView _view;
        OdbcConnection _dbConnection;
        int _selectedItem = -1;

        public ItemManager(IItemManagerView view)
        {
            _view = view;
            _dbConnection = new OdbcConnection("Dsn=MySQL32");
            _dbConnection.Open();
        }

        public void Init()
        {
            _view.ClearCategoryList();
            var command = new OdbcCommand($"SELECT id, name FROM Category WHERE parent_category IS NOT NULL", _dbConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                _view.AddCategory(reader.GetInt32(0), reader.GetString(1));
            }
        }
        public void SearchItems(string query)
        {
            _view.ClearIitemList();
            var command = new OdbcCommand($"SELECT id, name FROM Item WHERE id = \"{query}\" OR name LIKE \"%{query}%\"", _dbConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                _view.AddFoundItem(reader.GetInt32(0), reader.GetString(1));
            }
        }

        private void UpdateWarehouseList(int id)
        {
            _view.ClearWarehouseList();

            var warehouseCommand = new OdbcCommand(
                "SELECT Warehouse.id, Warehouse.name, COALESCE(WarehouseHasItem.quantity, 0) AS quantity " + 
                "FROM Warehouse " + 
                $"LEFT JOIN(SELECT * FROM WarehouseHasItem WHERE Item_id = {id}) AS WarehouseHasItem ON Warehouse.id = WarehouseHasItem.Warehouse_id ", _dbConnection);
            var warehouseReader = warehouseCommand.ExecuteReader();
            while (warehouseReader.Read())
            {
                _view.AddWarehouse(warehouseReader.GetInt32(0), warehouseReader.GetString(1), warehouseReader.GetInt32(2));
            }
        }

        private void UpdateMediaList(int id)
        {
            _view.ClearImage();
            _view.ClearMediaList();
            bool firstRead = true;
            var mediaCommand = new OdbcCommand($"SELECT id, priority, url FROM Media WHERE Item_id = {id} ORDER BY priority", _dbConnection);
            var mediaReader = mediaCommand.ExecuteReader();
            while (mediaReader.Read())
            {
                _view.AddMedia(mediaReader.GetInt32(0), mediaReader.GetInt32(1));
                if (firstRead)
                {
                    _view.SetImage(mediaReader.GetString(2));
                    firstRead = false;
                }
            }
        }

        public void SelectItem(int id)
        {
            var command = new OdbcCommand($"SELECT name, category_id, price, description FROM Item WHERE id = {id}", _dbConnection);
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                _selectedItem = id;
                _view.SetItemName(reader.GetString(0));
                var category = reader.GetInt16(1);
                _view.SetItemCategory(category);
                _view.SetItemPrice(reader.GetInt32(2));
                _view.SetItemDescription(reader.GetString(3));

                UpdateWarehouseList(id);

                var propertiesCommand = new OdbcCommand(
                    "SELECT Specification.id, Specification.Name, ItemSpecification.text as text, COALESCE(ItemSpecification.specification_case_id, -1) AS caseId " +
                        "FROM Specification " +
                        "JOIN CategoryHasSpecification " +
                        "ON Specification.id = CategoryHasSpecification.specification_id " +
                        "LEFT JOIN( " +
                        "SELECT ItemSpecification.specification_id, " +
                        "ItemSpecification.specification_case_id, ItemSpecification.text " +
                        $"FROM ItemSpecification WHERE ItemSpecification.item_id = {id}) AS ItemSpecification " +
                        "ON ItemSpecification.specification_id = CategoryHasSpecification.specification_id " +
                        $"WHERE CategoryHasSpecification.category_id = {category} "
                        , _dbConnection);
                var propertiesReader = propertiesCommand.ExecuteReader();

                _view.ClearProperties();
                while (propertiesReader.Read())
                {
                    var variants = new List<Tuple<int, string>>();
                    var specificationId = propertiesReader.GetInt32(0);
                    var variantsCommand = new OdbcCommand($"SELECT id, description FROM SpecificationCase WHERE specification_id = {specificationId}", _dbConnection);
                    var variantsReader = variantsCommand.ExecuteReader();
                    while (variantsReader.Read())
                    {
                        variants.Add(new Tuple<int, string>(variantsReader.GetInt32(0), variantsReader.GetString(1)));
                    }
                    _view.AddProperty(propertiesReader.GetInt32(0), propertiesReader.GetString(1), variants, propertiesReader.GetInt32(3), propertiesReader.IsDBNull(2) ? null : propertiesReader.GetString(2));
                }

                UpdateMediaList(id);
                _view.SetEditControlsEnabled(true);
            }

        }

        public void EditProperty(int id, int variant, string text)
        {
            var v = (variant < 0) ? "NULL" : variant.ToString();
            var t = (text == null) ? "NULL" : text;
            var command = new OdbcCommand($"REPLACE INTO ItemSpecification(specification_id, item_id, specification_case_id, text) VALUES" +
                $"({id}, {_selectedItem}, {v}, \"{t}\")", _dbConnection);
            command.ExecuteNonQuery();
        }

        public void OpenMedia(int id)
        {
            var command = new OdbcCommand($"SELECT url FROM Media WHERE id = {id}", _dbConnection);
            var reader = command.ExecuteReader();
            if (reader.Read())
            {
                _view.SetImage(reader.GetString(0));
            }
        }

        private short GetWarehouseItems(int warehouse)
        {
            var command = new OdbcCommand(
                $"SELECT quantity FROM WarehouseHasItem WHERE Item_id = {_selectedItem} AND warehouse_id = {warehouse}",
                _dbConnection);
            var prevCountColumn = command.ExecuteScalar();
            if (prevCountColumn != null)
            {
                return (short)prevCountColumn;
            } else
            {
                return 0;
            }
        }
        public void AddToWarehouse(int warehouse)
        {
            short prevCount = GetWarehouseItems(warehouse);
            var command = new OdbcCommand(
                $"REPLACE INTO WarehouseHasItem(warehouse_id, Item_id, quantity) VALUES ({warehouse}, {_selectedItem}, {prevCount + 1})",
                _dbConnection);
            command.ExecuteNonQuery();

            UpdateWarehouseList(_selectedItem);
        }

        public void RemoveFromWarehouse(int warehouse)
        {
            short prevCount = GetWarehouseItems(warehouse);
            if (prevCount == 0) return;
            if (prevCount == 1)
            {
                var command = new OdbcCommand(
                    $"DELETE FROM WarehouseHasItem WHERE warehouse_id = {warehouse} AND Item_id = {_selectedItem}",
                    _dbConnection);
                command.ExecuteNonQuery();
            } else
            {
                var command = new OdbcCommand(
                    $"REPLACE INTO WarehouseHasItem(warehouse_id, Item_id, quantity) VALUES ({warehouse}, {_selectedItem}, {prevCount - 1})",
                    _dbConnection);
                command.ExecuteNonQuery();
            }
            UpdateWarehouseList(_selectedItem);
        }

        public void RemoveMedia(int media)
        {
            var command = new OdbcCommand($"DELETE FROM Media WHERE id = {media}", _dbConnection);
            command.ExecuteNonQuery();
            UpdateMediaList(_selectedItem);
        }

        int GetMaxMediaPriority(int id)
        {
            var command = new OdbcCommand($"SELECT COALESCE(MAX(priority), -1) FROM Media WHERE Item_id = {id}", _dbConnection);
            return (int)((long)command.ExecuteScalar());
        }

        public void UploadMedia(string url_hd, string url)
        {
            var priority = GetMaxMediaPriority(_selectedItem) + 1;
            var command = new OdbcCommand(
                $"INSERT INTO Media(Item_id, priority, url, url_hd) VALUES({_selectedItem}, {priority}, \"{url}\", \"{url_hd}\")", _dbConnection);
            command.ExecuteNonQuery();
            UpdateMediaList(_selectedItem);
        }

        int GetMediaPriority(int id)
        {
            var command = new OdbcCommand($"SELECT priority FROM Media WHERE id = {id}", _dbConnection);
            return (int)command.ExecuteScalar();
        }

        public void IncMediaPriority(int media)
        {
            var oldPriority = GetMediaPriority(media);
            var command = new OdbcCommand($"UPDATE Media SET priority = {oldPriority + 1} WHERE id = {media}", _dbConnection);
            command.ExecuteNonQuery();
            UpdateMediaList(_selectedItem);
        }

        public void DecMediaPriority(int media)
        {
            var oldPriority = GetMediaPriority(media);
            if (oldPriority == 0) return;
            var command = new OdbcCommand($"UPDATE Media SET priority = {oldPriority - 1} WHERE id = {media}", _dbConnection);
            command.ExecuteNonQuery();
            UpdateMediaList(_selectedItem);
        }

        public void SaveItem(string name, int price, int category, string description)
        {
            var command = new OdbcCommand($"UPDATE Item SET name = \"{name}\", price = {price}, category_id = {category}, description = \"{description}\" WHERE id = {_selectedItem}",
                _dbConnection);
            command.ExecuteNonQuery();
            SelectItem(_selectedItem);

            _view.SimulateSearch(_selectedItem.ToString());
        }

        public void NewItem(string name, int price, int category, string description)
        {
            var command = new OdbcCommand($"INSERT INTO Item(name, price, category_id, description) VALUES (\"{name}\", {price}, {category}, \"{description}\")",
                _dbConnection);
            command.ExecuteNonQuery();
            command = new OdbcCommand("SELECT LAST_INSERT_ID()", _dbConnection);
            var id = (int)((decimal)command.ExecuteScalar());

            _view.SetEditControlsEnabled(false);
            _view.SimulateSearch(id.ToString());
        }

        public void DeleteItem()
        {
            var command = new OdbcCommand($"DELETE FROM Item WHERE id = {_selectedItem}", _dbConnection);
            command.ExecuteNonQuery();
            _view.SetEditControlsEnabled(false);
            _view.SimulateSearch(null);
        }
    }
}
