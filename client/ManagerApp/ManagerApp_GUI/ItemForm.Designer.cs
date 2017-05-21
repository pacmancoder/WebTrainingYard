namespace ManagerApp_GUI
{
    partial class ItemForm
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
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FoundItemsPanel = new System.Windows.Forms.ListBox();
            this.ItemNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ItemPriceTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ItemDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.CategorySelector = new System.Windows.Forms.ComboBox();
            this.WarehouseList = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.AddToWarehouseButton = new System.Windows.Forms.Button();
            this.SaveItemButton = new System.Windows.Forms.Button();
            this.NewItemButton = new System.Windows.Forms.Button();
            this.RemoveFromWarehouseButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.imageList = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.UploadImageButton = new System.Windows.Forms.Button();
            this.RemoveImageButton = new System.Windows.Forms.Button();
            this.SearchButton = new System.Windows.Forms.Button();
            this.ItemImage = new System.Windows.Forms.PictureBox();
            this.ImagePriorityUpButton = new System.Windows.Forms.Button();
            this.ImagePriorityDownButton = new System.Windows.Forms.Button();
            this.ItemPropertyGrid = new System.Windows.Forms.DataGridView();
            this.Property = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Variant = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.DeleteItemButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ItemImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemPropertyGrid)).BeginInit();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchBox
            // 
            this.SearchBox.Location = new System.Drawing.Point(12, 25);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(110, 20);
            this.SearchBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Search by name or id:";
            // 
            // FoundItemsPanel
            // 
            this.FoundItemsPanel.FormattingEnabled = true;
            this.FoundItemsPanel.Location = new System.Drawing.Point(12, 52);
            this.FoundItemsPanel.Name = "FoundItemsPanel";
            this.FoundItemsPanel.Size = new System.Drawing.Size(150, 576);
            this.FoundItemsPanel.TabIndex = 2;
            this.FoundItemsPanel.SelectedIndexChanged += new System.EventHandler(this.ItemsList_SelectedIndexChanged);
            // 
            // ItemNameTextBox
            // 
            this.ItemNameTextBox.Location = new System.Drawing.Point(465, 356);
            this.ItemNameTextBox.Name = "ItemNameTextBox";
            this.ItemNameTextBox.Size = new System.Drawing.Size(256, 20);
            this.ItemNameTextBox.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(396, 363);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(396, 389);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Price:";
            // 
            // ItemPriceTextBox
            // 
            this.ItemPriceTextBox.Location = new System.Drawing.Point(465, 382);
            this.ItemPriceTextBox.Name = "ItemPriceTextBox";
            this.ItemPriceTextBox.Size = new System.Drawing.Size(256, 20);
            this.ItemPriceTextBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(396, 415);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Category:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(396, 441);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Description:";
            // 
            // ItemDescriptionTextBox
            // 
            this.ItemDescriptionTextBox.Location = new System.Drawing.Point(399, 459);
            this.ItemDescriptionTextBox.Multiline = true;
            this.ItemDescriptionTextBox.Name = "ItemDescriptionTextBox";
            this.ItemDescriptionTextBox.Size = new System.Drawing.Size(322, 107);
            this.ItemDescriptionTextBox.TabIndex = 5;
            // 
            // CategorySelector
            // 
            this.CategorySelector.FormattingEnabled = true;
            this.CategorySelector.Location = new System.Drawing.Point(466, 407);
            this.CategorySelector.Name = "CategorySelector";
            this.CategorySelector.Size = new System.Drawing.Size(255, 21);
            this.CategorySelector.TabIndex = 6;
            // 
            // WarehouseList
            // 
            this.WarehouseList.FormattingEnabled = true;
            this.WarehouseList.Location = new System.Drawing.Point(736, 40);
            this.WarehouseList.Name = "WarehouseList";
            this.WarehouseList.Size = new System.Drawing.Size(158, 485);
            this.WarehouseList.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(736, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Warehouses:";
            // 
            // AddToWarehouseButton
            // 
            this.AddToWarehouseButton.Location = new System.Drawing.Point(736, 533);
            this.AddToWarehouseButton.Name = "AddToWarehouseButton";
            this.AddToWarehouseButton.Size = new System.Drawing.Size(158, 33);
            this.AddToWarehouseButton.TabIndex = 9;
            this.AddToWarehouseButton.Text = "Add to selected warehouse";
            this.AddToWarehouseButton.UseVisualStyleBackColor = true;
            this.AddToWarehouseButton.Click += new System.EventHandler(this.AddToWarehouseButton_Click);
            // 
            // SaveItemButton
            // 
            this.SaveItemButton.Location = new System.Drawing.Point(396, 572);
            this.SaveItemButton.Name = "SaveItemButton";
            this.SaveItemButton.Size = new System.Drawing.Size(103, 37);
            this.SaveItemButton.TabIndex = 9;
            this.SaveItemButton.Text = "Save";
            this.SaveItemButton.UseVisualStyleBackColor = true;
            this.SaveItemButton.Click += new System.EventHandler(this.SaveItemButton_Click);
            // 
            // NewItemButton
            // 
            this.NewItemButton.Location = new System.Drawing.Point(616, 572);
            this.NewItemButton.Name = "NewItemButton";
            this.NewItemButton.Size = new System.Drawing.Size(105, 37);
            this.NewItemButton.TabIndex = 10;
            this.NewItemButton.Text = "New";
            this.NewItemButton.UseVisualStyleBackColor = true;
            this.NewItemButton.Click += new System.EventHandler(this.NewItemButton_Click);
            // 
            // RemoveFromWarehouseButton
            // 
            this.RemoveFromWarehouseButton.Location = new System.Drawing.Point(736, 572);
            this.RemoveFromWarehouseButton.Name = "RemoveFromWarehouseButton";
            this.RemoveFromWarehouseButton.Size = new System.Drawing.Size(158, 37);
            this.RemoveFromWarehouseButton.TabIndex = 11;
            this.RemoveFromWarehouseButton.Text = "Remove from selected warehouse";
            this.RemoveFromWarehouseButton.UseVisualStyleBackColor = true;
            this.RemoveFromWarehouseButton.Click += new System.EventHandler(this.RemoveFromWarehouseButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(400, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Image:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Specifications:";
            // 
            // imageList
            // 
            this.imageList.FormattingEnabled = true;
            this.imageList.Location = new System.Drawing.Point(615, 25);
            this.imageList.Name = "imageList";
            this.imageList.Size = new System.Drawing.Size(115, 290);
            this.imageList.TabIndex = 12;
            this.imageList.SelectedIndexChanged += new System.EventHandler(this.imageList_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(612, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "All Images:";
            // 
            // UploadImageButton
            // 
            this.UploadImageButton.Location = new System.Drawing.Point(396, 246);
            this.UploadImageButton.Name = "UploadImageButton";
            this.UploadImageButton.Size = new System.Drawing.Size(213, 46);
            this.UploadImageButton.TabIndex = 13;
            this.UploadImageButton.Text = "Upload image...";
            this.UploadImageButton.UseVisualStyleBackColor = true;
            this.UploadImageButton.Click += new System.EventHandler(this.UploadImageButton_Click);
            // 
            // RemoveImageButton
            // 
            this.RemoveImageButton.Location = new System.Drawing.Point(396, 298);
            this.RemoveImageButton.Name = "RemoveImageButton";
            this.RemoveImageButton.Size = new System.Drawing.Size(213, 43);
            this.RemoveImageButton.TabIndex = 14;
            this.RemoveImageButton.Text = "Remove current image";
            this.RemoveImageButton.UseVisualStyleBackColor = true;
            this.RemoveImageButton.Click += new System.EventHandler(this.RemoveImageButton_Click);
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(128, 24);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(34, 23);
            this.SearchButton.TabIndex = 15;
            this.SearchButton.Text = "GO!";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // ItemImage
            // 
            this.ItemImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ItemImage.Location = new System.Drawing.Point(396, 25);
            this.ItemImage.Name = "ItemImage";
            this.ItemImage.Size = new System.Drawing.Size(213, 215);
            this.ItemImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ItemImage.TabIndex = 4;
            this.ItemImage.TabStop = false;
            // 
            // ImagePriorityUpButton
            // 
            this.ImagePriorityUpButton.Location = new System.Drawing.Point(616, 319);
            this.ImagePriorityUpButton.Name = "ImagePriorityUpButton";
            this.ImagePriorityUpButton.Size = new System.Drawing.Size(54, 22);
            this.ImagePriorityUpButton.TabIndex = 13;
            this.ImagePriorityUpButton.Text = "Up";
            this.ImagePriorityUpButton.UseVisualStyleBackColor = true;
            this.ImagePriorityUpButton.Click += new System.EventHandler(this.ImagePriorityUpButton_Click);
            // 
            // ImagePriorityDownButton
            // 
            this.ImagePriorityDownButton.Location = new System.Drawing.Point(676, 319);
            this.ImagePriorityDownButton.Name = "ImagePriorityDownButton";
            this.ImagePriorityDownButton.Size = new System.Drawing.Size(54, 22);
            this.ImagePriorityDownButton.TabIndex = 13;
            this.ImagePriorityDownButton.Text = "Down";
            this.ImagePriorityDownButton.UseVisualStyleBackColor = true;
            this.ImagePriorityDownButton.Click += new System.EventHandler(this.ImagePriorityDownButton_Click);
            // 
            // ItemPropertyGrid
            // 
            this.ItemPropertyGrid.AllowUserToAddRows = false;
            this.ItemPropertyGrid.AllowUserToDeleteRows = false;
            this.ItemPropertyGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ItemPropertyGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ItemPropertyGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Property,
            this.Variant,
            this.Text});
            this.ItemPropertyGrid.Location = new System.Drawing.Point(14, 25);
            this.ItemPropertyGrid.Name = "ItemPropertyGrid";
            this.ItemPropertyGrid.Size = new System.Drawing.Size(376, 578);
            this.ItemPropertyGrid.TabIndex = 16;
            this.ItemPropertyGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.ItemPropertyGrid_CellValueChanged);
            // 
            // Property
            // 
            this.Property.HeaderText = "Property";
            this.Property.Name = "Property";
            this.Property.ReadOnly = true;
            // 
            // Variant
            // 
            this.Variant.HeaderText = "Variant";
            this.Variant.Name = "Variant";
            // 
            // Text
            // 
            this.Text.HeaderText = "Text";
            this.Text.Name = "Text";
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.ItemImage);
            this.MainPanel.Controls.Add(this.ItemPropertyGrid);
            this.MainPanel.Controls.Add(this.label7);
            this.MainPanel.Controls.Add(this.label9);
            this.MainPanel.Controls.Add(this.label8);
            this.MainPanel.Controls.Add(this.RemoveImageButton);
            this.MainPanel.Controls.Add(this.label2);
            this.MainPanel.Controls.Add(this.ImagePriorityDownButton);
            this.MainPanel.Controls.Add(this.label3);
            this.MainPanel.Controls.Add(this.ImagePriorityUpButton);
            this.MainPanel.Controls.Add(this.label4);
            this.MainPanel.Controls.Add(this.UploadImageButton);
            this.MainPanel.Controls.Add(this.label5);
            this.MainPanel.Controls.Add(this.imageList);
            this.MainPanel.Controls.Add(this.ItemNameTextBox);
            this.MainPanel.Controls.Add(this.RemoveFromWarehouseButton);
            this.MainPanel.Controls.Add(this.ItemPriceTextBox);
            this.MainPanel.Controls.Add(this.DeleteItemButton);
            this.MainPanel.Controls.Add(this.NewItemButton);
            this.MainPanel.Controls.Add(this.ItemDescriptionTextBox);
            this.MainPanel.Controls.Add(this.SaveItemButton);
            this.MainPanel.Controls.Add(this.CategorySelector);
            this.MainPanel.Controls.Add(this.AddToWarehouseButton);
            this.MainPanel.Controls.Add(this.WarehouseList);
            this.MainPanel.Controls.Add(this.label6);
            this.MainPanel.Enabled = false;
            this.MainPanel.Location = new System.Drawing.Point(168, 9);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(907, 619);
            this.MainPanel.TabIndex = 17;
            // 
            // DeleteItemButton
            // 
            this.DeleteItemButton.Location = new System.Drawing.Point(504, 572);
            this.DeleteItemButton.Name = "DeleteItemButton";
            this.DeleteItemButton.Size = new System.Drawing.Size(105, 37);
            this.DeleteItemButton.TabIndex = 10;
            this.DeleteItemButton.Text = "Delete";
            this.DeleteItemButton.UseVisualStyleBackColor = true;
            this.DeleteItemButton.Click += new System.EventHandler(this.DeleteItemButton_Click);
            // 
            // ItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 634);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.FoundItemsPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SearchBox);
            this.Name = "ItemForm";
            this.Load += new System.EventHandler(this.ItemForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ItemImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ItemPropertyGrid)).EndInit();
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox FoundItemsPanel;
        private System.Windows.Forms.PictureBox ItemImage;
        private System.Windows.Forms.TextBox ItemNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ItemPriceTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ItemDescriptionTextBox;
        private System.Windows.Forms.ComboBox CategorySelector;
        private System.Windows.Forms.ListBox WarehouseList;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button AddToWarehouseButton;
        private System.Windows.Forms.Button SaveItemButton;
        private System.Windows.Forms.Button NewItemButton;
        private System.Windows.Forms.Button RemoveFromWarehouseButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox imageList;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button UploadImageButton;
        private System.Windows.Forms.Button RemoveImageButton;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Button ImagePriorityUpButton;
        private System.Windows.Forms.Button ImagePriorityDownButton;
        private System.Windows.Forms.DataGridView ItemPropertyGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Property;
        private System.Windows.Forms.DataGridViewComboBoxColumn Variant;
        private System.Windows.Forms.DataGridViewTextBoxColumn Text;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button DeleteItemButton;
    }
}