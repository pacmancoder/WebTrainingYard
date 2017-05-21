using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagerApp_GUI
{
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();
        }

        private void CategoryManagerButton_Click(object sender, EventArgs e)
        {
            CategoryManagerForm childForm = new CategoryManagerForm();
            childForm.Show();
        }
    }
}
