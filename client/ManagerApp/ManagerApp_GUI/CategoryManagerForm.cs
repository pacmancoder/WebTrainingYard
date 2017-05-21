using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ManagerApp_GUI.View;
using ManagerApp_GUI.Model;

namespace ManagerApp_GUI
{
    public partial class CategoryManagerForm : Form, ICategoryManagerView
    {
        public CategoryManagerForm()
        {
            InitializeComponent();
        }

        public void ClearCategoryList()
        {
            categoryList.Items.Clear();
            _categoryIdByListIndex.Clear();
        }

        public void AddCategoryToList(int id, string name)
        {
            _categoryIdByListIndex[categoryList.Items.Count] = id;
            categoryList.Items.Add(name);
        }

        private void CategoryManagerForm_Load(object sender, EventArgs e)
        {
            _model = new CategoryMnager(this);
            _model.UpdateCategoryList();
        }

        private CategoryMnager _model;
        Dictionary<int, int> _categoryIdByListIndex = new Dictionary<int, int>();

        private void categoryList_DoubleClick(object sender, EventArgs e)
        {
            int category;
            if (!_categoryIdByListIndex.TryGetValue(
                categoryList.SelectedIndex, 
                out category))
            {
                return;
            }
            _model.ChangeCurrentRootCategory(category);
        }

        private void upButton_Click(object sender, EventArgs e)
        {
            _model.MoveToRoot();
        }
    }
}
