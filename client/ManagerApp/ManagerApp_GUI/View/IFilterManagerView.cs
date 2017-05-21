using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerApp_GUI.View
{
    interface IFilterManagerView
    {
        void ClearFiltersList();
        void AddFilterToList(int id, string name);

        void ClearFilterCaseList();
        void AddFilterCaseToList(int id, string name);

        void SetCurrentFilterName(string name);
        void SetCurrentFilterCaseName(string name);

        void ShowError(string msg);

        void SetFilterNameControlActive(bool state);
        void SetFilterCaseNameControlActive(bool state);
    }
}
