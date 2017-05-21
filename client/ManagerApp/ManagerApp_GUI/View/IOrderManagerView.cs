using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerApp_GUI.View
{
    interface IOrderManagerView
    {
        void ClearOrderList();
        void AddOrderToList(int id, string date);

        void SetOrderName(int id);
        void SetOrderDescription(string description);
        void SetOrderStatus(string status);
    }
}
