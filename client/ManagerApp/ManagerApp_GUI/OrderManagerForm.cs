using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ManagerApp_GUI.Model;
using ManagerApp_GUI.View;

namespace ManagerApp_GUI
{
    public partial class OrderManagerForm : Form, IOrderManagerView
    {

        OrderManager _model;
        Dictionary<int, int> _orderIdByIndex = new Dictionary<int, int>();

        public OrderManagerForm()
        {
            InitializeComponent();
        }

        public void AddOrderToList(int id, string date)
        {

            _orderIdByIndex.Add(OrderList.Items.Count, id);
            OrderList.Items.Add($"[{date}] {id.ToString()}");
        }

        public void ClearOrderList()
        {
            _orderIdByIndex.Clear();
            OrderList.Items.Clear();
        }

        public void SetOrderDescription(string description)
        {
            InfoTextBox.Text = description;
        }

        public void SetOrderName(int id)
        {
            OrderNameLabel.Text = $"Order #{id}";
        }

        public void SetOrderStatus(string status)
        {
            blockControlEvents = true;
            switch (status)
            {
                case "PENDING":
                    StatusSelector.SelectedIndex = 0;
                    break;
                case "CANCELED":
                    StatusSelector.SelectedIndex = 1;
                    break;
                case "SHIPPING":
                    StatusSelector.SelectedIndex = 2;
                    break;
                case "DELIVERED":
                    StatusSelector.SelectedIndex = 3;
                    break;
                default:
                    StatusSelector.SelectedIndex = -1;
                    break;
            }
            blockControlEvents = false;
        }

        private void OrderManagerForm_Load(object sender, EventArgs e)
        {
            _model = new OrderManager(this);
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            _model.SearchOrders(SearchBox.Text, OnlyPendingCheck.Checked);
        }

        private void OrderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int order;
            if (!_orderIdByIndex.TryGetValue(OrderList.SelectedIndex, out order))
            {
                return;
            }
            _model.SelectOrder(order);
        }

        private void StatusSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blockControlEvents) return;
            switch (StatusSelector.SelectedIndex)
            {
                case 0:
                    _model.ChangeStatus("PENDING");
                    break;
                case 1:
                    _model.ChangeStatus("CANCELED");
                    break;
                case 2:
                    _model.ChangeStatus("SHIPPING");
                    break;
                case 3:
                    _model.ChangeStatus("DELIVERED");
                    break;
            }
        }

        bool blockControlEvents = false;
    }
}
