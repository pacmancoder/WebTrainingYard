using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

using ManagerApp_GUI.View;

namespace ManagerApp_GUI.Model
{
    class OrderManager
    {
        IOrderManagerView _view;
        OdbcConnection _dbConnection;
        int _selectedOrder = -1;

        public OrderManager(IOrderManagerView view)
        {
            _view = view;
            _dbConnection = new OdbcConnection("Dsn=MySQL32");
            _dbConnection.Open();
        }

        public void SearchOrders(string query, bool onlyPending)
        {
            _view.ClearOrderList();
            OdbcCommand command;
            if (query != "") {
                command = new OdbcCommand($"SELECT id, created_at FROM `Order` WHERE id LIKE \"{query}\" ORDER BY created_at ASC", _dbConnection);
            } else
            {
                command = new OdbcCommand($"SELECT id, created_at FROM `Order` ORDER BY created_at ASC", _dbConnection);
            }
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                _view.AddOrderToList(reader.GetInt32(0), reader.GetString(1));
            }
        }

        public void SelectOrder(int id)
        {
            _selectedOrder = id;
            _view.SetOrderName(id);
            var orderCommand = new OdbcCommand(
                $"SELECT first_name, phone, warehouse, updated_at, created_at, status " +
                $"FROM ManagerOrderView WHERE order_id = {id}", _dbConnection);
            var orderReader = orderCommand.ExecuteReader();
            if (orderReader.Read())
            {
                _view.SetOrderDescription(
                    $"Customer: {orderReader.GetString(0)} \r\n" +
                    $"Phone: {orderReader.GetString(1)} \r\n" +
                    $"Warehouse: {orderReader.GetString(2)} \r\n" +
                    $"Updated at: {orderReader.GetString(3)} \r\n" +
                    $"Created at: {orderReader.GetString(4)}"
                );
                _view.SetOrderStatus(orderReader.GetString(5));
            }
        }

        public void ChangeStatus(string status)
        {
            if (_selectedOrder < 0) return;
            var command = new OdbcCommand($"UPDATE `Order` SET status = \"{status}\" WHERE id = {_selectedOrder}",
                _dbConnection);
            command.ExecuteNonQuery();
        }
    }
}
