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
    }
}
