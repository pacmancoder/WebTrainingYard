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

        private CategoryMnager _model;
        Dictionary<int, int> _categoryIdByListIndex = new Dictionary<int, int>();

        Dictionary<int, int> _filterIdByListIndex = new Dictionary<int, int>();
        Dictionary<int, int> _filterListIndexById = new Dictionary<int, int>();

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
            categoryList.Items.Add($"[#{id}] {name}");
        }

        private void CategoryManagerForm_Load(object sender, EventArgs e)
        {
            _model = new CategoryMnager(this);
            _model.UpdateCategoryList();
            _model.UpdateFiltersList();
        }

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

        private void categoryList_Click(object sender, EventArgs e)
        {
            int category;
            if (!_categoryIdByListIndex.TryGetValue(
                categoryList.SelectedIndex,
                out category))
            {
                return;
            }
            _model.SelectCategory(category);
        }

        public void BlockEditControls()
        {
            mainGroupBox.Enabled = false;
        }

        public void UnblockEditControls()
        {
            mainGroupBox.Enabled = true;
        }

        public void SetCategoryId(string id)
        {
            categoryIdLabel.Text = id.ToString();
        }

        public void SetCategoryName(string name)
        {
            categoryNameTextBox.Text = name;
        }

        public void AddFilterToList(int id, string name)
        {
            _filterIdByListIndex[filtersList.Items.Count] = id;
            _filterListIndexById[id] = filtersList.Items.Count;

            filtersList.Items.Add(name);
            filtersList.SetItemChecked(filtersList.Items.Count - 1, false);
        }

        public void SetFilterCheckbox(int id, bool state)
        {
            filtersList.SetItemChecked(_filterListIndexById[id], state);
        }

        private void filtersList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (!mainGroupBox.Enabled)
            {
                return;
            }
            int filter;
            if (!_filterIdByListIndex.TryGetValue(
                e.Index,
                out filter))
            {
                return;
            }
            _model.AddFilterAction(filter, e.NewValue == CheckState.Checked);
        }

        private void AlterButton_Click(object sender, EventArgs e)
        {
            _model.AlterCategory();
        }

        private void categoryNameTextBox_TextChanged(object sender, EventArgs e)
        {
            _model.ChangeCategoryName(categoryNameTextBox.Text);
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            _model.CreateCategoryInRoot();
        }

        private void CreateChildButton_Click(object sender, EventArgs e)
        {
            _model.CreateCategoryAsChild();
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            _model.DeleteCategory();
        }

        public void ShowError(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void editFiltersButton_Click(object sender, EventArgs e)
        {
            Form filtersForm = new FilterManagerForm();
            filtersForm.ShowDialog();
            _model.UpdateFiltersList();
        }

        public void ClearFiltersList()
        {
            filtersList.Items.Clear();
            _filterIdByListIndex.Clear();
            _filterListIndexById.Clear();
        }
    }
}
