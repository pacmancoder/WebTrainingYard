using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerApp_GUI.View
{
    interface ICategoryManagerView
    {
        void ClearCategoryList();
        void AddCategoryToList(int id, string name);

        void BlockEditControls();
        void UnblockEditControls();

        void SetCategoryId(string id);
        void SetCategoryName(string name);

        void AddFilterToList(int id, string name);
        void SetFilterCheckbox(int id, bool state);
        void ClearFiltersList();

        void ShowError(string msg);
    }
}
