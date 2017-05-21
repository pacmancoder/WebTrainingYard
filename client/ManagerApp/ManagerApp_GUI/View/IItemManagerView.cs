using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerApp_GUI.View
{
    interface IItemManagerView
    {
        void ClearIitemList();
        void AddFoundItem(int id, string name);

        void ClearMediaList();
        void AddMedia(int id, int priority);

        void ClearWarehouseList();
        void AddWarehouse(int id, string name, int count);

        void ClearCategoryList();
        void AddCategory(int id, string name);

        void SetItemName(string name);
        void SetItemPrice(int price);
        void SetItemCategory(int id);
        void SetItemDescription(string description);

        void ClearImage();
        void SetImage(string url);

        void ClearProperties();
        void AddProperty(int id, string name, IList<Tuple<int, string>> variants, int selectionId, string text);

        void ShowError(string msg);

        void SetEditControlsEnabled(bool state);

        void SimulateSearch(string query = null);
    }
}
