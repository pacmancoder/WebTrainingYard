namespace ManagerApp_GUI
{
    partial class CategoryManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.categoryList = new System.Windows.Forms.ListBox();
            this.upButton = new System.Windows.Forms.Button();
            this.filtersList = new System.Windows.Forms.CheckedListBox();
            this.mainGroupBox = new System.Windows.Forms.GroupBox();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.CreateButton = new System.Windows.Forms.Button();
            this.CreateChildButton = new System.Windows.Forms.Button();
            this.AlterButton = new System.Windows.Forms.Button();
            this.categoryNameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.categoryIdLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.editFiltersButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.mainGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // categoryList
            // 
            this.categoryList.FormattingEnabled = true;
            this.categoryList.Location = new System.Drawing.Point(12, 65);
            this.categoryList.Name = "categoryList";
            this.categoryList.Size = new System.Drawing.Size(120, 420);
            this.categoryList.TabIndex = 0;
            this.categoryList.Click += new System.EventHandler(this.categoryList_Click);
            this.categoryList.DoubleClick += new System.EventHandler(this.categoryList_DoubleClick);
            // 
            // upButton
            // 
            this.upButton.Image = global::ManagerApp_GUI.Properties.Resources.ArrowUpSmall;
            this.upButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.upButton.Location = new System.Drawing.Point(13, 12);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(119, 39);
            this.upButton.TabIndex = 1;
            this.upButton.Text = "   Go Up";
            this.upButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // filtersList
            // 
            this.filtersList.FormattingEnabled = true;
            this.filtersList.Location = new System.Drawing.Point(21, 96);
            this.filtersList.Name = "filtersList";
            this.filtersList.Size = new System.Drawing.Size(170, 319);
            this.filtersList.TabIndex = 2;
            this.filtersList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.filtersList_ItemCheck);
            // 
            // mainGroupBox
            // 
            this.mainGroupBox.Controls.Add(this.DeleteButton);
            this.mainGroupBox.Controls.Add(this.CreateButton);
            this.mainGroupBox.Controls.Add(this.CreateChildButton);
            this.mainGroupBox.Controls.Add(this.AlterButton);
            this.mainGroupBox.Controls.Add(this.categoryNameTextBox);
            this.mainGroupBox.Controls.Add(this.label3);
            this.mainGroupBox.Controls.Add(this.categoryIdLabel);
            this.mainGroupBox.Controls.Add(this.label2);
            this.mainGroupBox.Controls.Add(this.editFiltersButton);
            this.mainGroupBox.Controls.Add(this.label1);
            this.mainGroupBox.Controls.Add(this.filtersList);
            this.mainGroupBox.Enabled = false;
            this.mainGroupBox.Location = new System.Drawing.Point(138, 12);
            this.mainGroupBox.Name = "mainGroupBox";
            this.mainGroupBox.Size = new System.Drawing.Size(325, 473);
            this.mainGroupBox.TabIndex = 3;
            this.mainGroupBox.TabStop = false;
            this.mainGroupBox.Text = "Category settiongs";
            // 
            // DeleteButton
            // 
            this.DeleteButton.Location = new System.Drawing.Point(197, 304);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(104, 63);
            this.DeleteButton.TabIndex = 9;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // CreateButton
            // 
            this.CreateButton.Location = new System.Drawing.Point(197, 166);
            this.CreateButton.Name = "CreateButton";
            this.CreateButton.Size = new System.Drawing.Size(104, 63);
            this.CreateButton.TabIndex = 9;
            this.CreateButton.Text = "Create";
            this.CreateButton.UseVisualStyleBackColor = true;
            this.CreateButton.Click += new System.EventHandler(this.CreateButton_Click);
            // 
            // CreateChildButton
            // 
            this.CreateChildButton.Location = new System.Drawing.Point(197, 235);
            this.CreateChildButton.Name = "CreateChildButton";
            this.CreateChildButton.Size = new System.Drawing.Size(104, 63);
            this.CreateChildButton.TabIndex = 9;
            this.CreateChildButton.Text = "Create child";
            this.CreateChildButton.UseVisualStyleBackColor = true;
            this.CreateChildButton.Click += new System.EventHandler(this.CreateChildButton_Click);
            // 
            // AlterButton
            // 
            this.AlterButton.Location = new System.Drawing.Point(197, 97);
            this.AlterButton.Name = "AlterButton";
            this.AlterButton.Size = new System.Drawing.Size(104, 63);
            this.AlterButton.TabIndex = 9;
            this.AlterButton.Text = "Alter";
            this.AlterButton.UseVisualStyleBackColor = true;
            this.AlterButton.Click += new System.EventHandler(this.AlterButton_Click);
            // 
            // categoryNameTextBox
            // 
            this.categoryNameTextBox.Location = new System.Drawing.Point(61, 53);
            this.categoryNameTextBox.Name = "categoryNameTextBox";
            this.categoryNameTextBox.Size = new System.Drawing.Size(240, 20);
            this.categoryNameTextBox.TabIndex = 8;
            this.categoryNameTextBox.TextChanged += new System.EventHandler(this.categoryNameTextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Name:";
            // 
            // categoryIdLabel
            // 
            this.categoryIdLabel.AutoSize = true;
            this.categoryIdLabel.Location = new System.Drawing.Point(108, 19);
            this.categoryIdLabel.Name = "categoryIdLabel";
            this.categoryIdLabel.Size = new System.Drawing.Size(10, 13);
            this.categoryIdLabel.TabIndex = 6;
            this.categoryIdLabel.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Category ID:";
            // 
            // editFiltersButton
            // 
            this.editFiltersButton.Location = new System.Drawing.Point(21, 421);
            this.editFiltersButton.Name = "editFiltersButton";
            this.editFiltersButton.Size = new System.Drawing.Size(170, 38);
            this.editFiltersButton.TabIndex = 4;
            this.editFiltersButton.Text = "Edit filters...";
            this.editFiltersButton.UseVisualStyleBackColor = true;
            this.editFiltersButton.Click += new System.EventHandler(this.editFiltersButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Filters:";
            // 
            // CategoryManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 510);
            this.Controls.Add(this.mainGroupBox);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.categoryList);
            this.Name = "CategoryManagerForm";
            this.Text = "CategoryManagerForm";
            this.Load += new System.EventHandler(this.CategoryManagerForm_Load);
            this.mainGroupBox.ResumeLayout(false);
            this.mainGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox categoryList;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.CheckedListBox filtersList;
        private System.Windows.Forms.GroupBox mainGroupBox;
        private System.Windows.Forms.Button editFiltersButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button CreateButton;
        private System.Windows.Forms.Button CreateChildButton;
        private System.Windows.Forms.Button AlterButton;
        private System.Windows.Forms.TextBox categoryNameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label categoryIdLabel;
    }
}