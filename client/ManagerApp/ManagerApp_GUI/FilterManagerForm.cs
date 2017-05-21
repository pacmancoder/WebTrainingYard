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
    public partial class FilterManagerForm : Form, IFilterManagerView
    {

        private FilterManager _model;
        
        Dictionary<int, int> _filterIdByIndex = new Dictionary<int, int>();
        Dictionary<int, int> _filterCaseIdByIndex = new Dictionary<int, int>();

        public FilterManagerForm()
        {
            InitializeComponent();
        }

        public void AddFilterCaseToList(int id, string name)
        {
            _filterCaseIdByIndex.Add(casesList.Items.Count, id);
            casesList.Items.Add($"[#{id}] {name}");
        }

        public void AddFilterToList(int id, string name)
        {
            _filterIdByIndex.Add(filtersList.Items.Count, id);
            filtersList.Items.Add($"[#{id}] {name}");
        }

        public void ClearFilterCaseList()
        {
            casesList.Items.Clear();
            _filterCaseIdByIndex.Clear();
        }

        public void ClearFiltersList()
        {
            filtersList.Items.Clear();
            _filterIdByIndex.Clear();
        }

        public void SetCurrentFilterCaseName(string name)
        {
            filterCaseNameTextBox.Text = name;
        }

        public void SetCurrentFilterName(string name)
        {
            filterNameTextBox.Text = name;
        }

        public void SetFilterCaseNameControlActive(bool state)
        {
            filterCaseNameTextBox.Enabled = state;
        }

        public void SetFilterNameControlActive(bool state)
        {
            filterNameTextBox.Enabled = state;
        }

        public void ShowError(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FilterManagerForm_Load(object sender, EventArgs e)
        {
            _model = new FilterManager(this);
            _model.UpdateFiltersList();
        }

        private void filtersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int filter;
            if (!_filterIdByIndex.TryGetValue(
                filtersList.SelectedIndex,
                out filter))
            {
                return;
            }
            _model.SelectFilter(filter);
        }

        private void casesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int filterCase;
            if (!_filterCaseIdByIndex.TryGetValue(
                casesList.SelectedIndex,
                out filterCase))
            {
                return;
            }
            _model.selectFilterCase(filterCase);
        }

        private void saveFilterButton_Click(object sender, EventArgs e)
        {
            _model.saveFilterName(filterNameTextBox.Text);
        }

        private void deleteFilterButton_Click(object sender, EventArgs e)
        {
            _model.deleteFilter();
        }

        private void newFilterButton_Click(object sender, EventArgs e)
        {
            _model.addFilter(filterNameTextBox.Text);
        }

        private void saveCaseButton_Click(object sender, EventArgs e)
        {
            _model.saveFilterCase(filterCaseNameTextBox.Text);
        }

        private void deleteCaseButton_Click(object sender, EventArgs e)
        {
            _model.deleteFilterCase();
        }

        private void newCaseButton_Click(object sender, EventArgs e)
        {
            _model.addFilterCase(filterCaseNameTextBox.Text);
        }
    }
}
