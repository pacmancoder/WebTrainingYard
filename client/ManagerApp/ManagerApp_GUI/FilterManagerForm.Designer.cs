namespace ManagerApp_GUI
{
    partial class FilterManagerForm
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
            this.filtersList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.casesList = new System.Windows.Forms.ListBox();
            this.filterNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.newFilterButton = new System.Windows.Forms.Button();
            this.deleteFilterButton = new System.Windows.Forms.Button();
            this.saveFilterButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.filterCaseNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.newCaseButton = new System.Windows.Forms.Button();
            this.deleteCaseButton = new System.Windows.Forms.Button();
            this.saveCaseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // filtersList
            // 
            this.filtersList.FormattingEnabled = true;
            this.filtersList.Location = new System.Drawing.Point(12, 30);
            this.filtersList.Name = "filtersList";
            this.filtersList.Size = new System.Drawing.Size(120, 524);
            this.filtersList.TabIndex = 0;
            this.filtersList.SelectedIndexChanged += new System.EventHandler(this.filtersList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filters:";
            // 
            // casesList
            // 
            this.casesList.FormattingEnabled = true;
            this.casesList.Location = new System.Drawing.Point(152, 102);
            this.casesList.Name = "casesList";
            this.casesList.Size = new System.Drawing.Size(145, 381);
            this.casesList.TabIndex = 2;
            this.casesList.SelectedIndexChanged += new System.EventHandler(this.casesList_SelectedIndexChanged);
            // 
            // filterNameTextBox
            // 
            this.filterNameTextBox.Location = new System.Drawing.Point(152, 30);
            this.filterNameTextBox.Name = "filterNameTextBox";
            this.filterNameTextBox.Size = new System.Drawing.Size(145, 20);
            this.filterNameTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Filter name:";
            // 
            // newFilterButton
            // 
            this.newFilterButton.Location = new System.Drawing.Point(260, 56);
            this.newFilterButton.Name = "newFilterButton";
            this.newFilterButton.Size = new System.Drawing.Size(37, 23);
            this.newFilterButton.TabIndex = 5;
            this.newFilterButton.Text = "New";
            this.newFilterButton.UseVisualStyleBackColor = true;
            this.newFilterButton.Click += new System.EventHandler(this.newFilterButton_Click);
            // 
            // deleteFilterButton
            // 
            this.deleteFilterButton.Location = new System.Drawing.Point(205, 56);
            this.deleteFilterButton.Name = "deleteFilterButton";
            this.deleteFilterButton.Size = new System.Drawing.Size(49, 23);
            this.deleteFilterButton.TabIndex = 6;
            this.deleteFilterButton.Text = "Delete";
            this.deleteFilterButton.UseVisualStyleBackColor = true;
            this.deleteFilterButton.Click += new System.EventHandler(this.deleteFilterButton_Click);
            // 
            // saveFilterButton
            // 
            this.saveFilterButton.Location = new System.Drawing.Point(152, 56);
            this.saveFilterButton.Name = "saveFilterButton";
            this.saveFilterButton.Size = new System.Drawing.Size(47, 23);
            this.saveFilterButton.TabIndex = 7;
            this.saveFilterButton.Text = "Save";
            this.saveFilterButton.UseVisualStyleBackColor = true;
            this.saveFilterButton.Click += new System.EventHandler(this.saveFilterButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(152, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Cases:";
            // 
            // filterCaseNameTextBox
            // 
            this.filterCaseNameTextBox.Location = new System.Drawing.Point(152, 503);
            this.filterCaseNameTextBox.Name = "filterCaseNameTextBox";
            this.filterCaseNameTextBox.Size = new System.Drawing.Size(145, 20);
            this.filterCaseNameTextBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(152, 486);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Filter case text:";
            // 
            // newCaseButton
            // 
            this.newCaseButton.Location = new System.Drawing.Point(260, 529);
            this.newCaseButton.Name = "newCaseButton";
            this.newCaseButton.Size = new System.Drawing.Size(37, 23);
            this.newCaseButton.TabIndex = 5;
            this.newCaseButton.Text = "New";
            this.newCaseButton.UseVisualStyleBackColor = true;
            this.newCaseButton.Click += new System.EventHandler(this.newCaseButton_Click);
            // 
            // deleteCaseButton
            // 
            this.deleteCaseButton.Location = new System.Drawing.Point(205, 529);
            this.deleteCaseButton.Name = "deleteCaseButton";
            this.deleteCaseButton.Size = new System.Drawing.Size(49, 23);
            this.deleteCaseButton.TabIndex = 6;
            this.deleteCaseButton.Text = "Delete";
            this.deleteCaseButton.UseVisualStyleBackColor = true;
            this.deleteCaseButton.Click += new System.EventHandler(this.deleteCaseButton_Click);
            // 
            // saveCaseButton
            // 
            this.saveCaseButton.Location = new System.Drawing.Point(152, 529);
            this.saveCaseButton.Name = "saveCaseButton";
            this.saveCaseButton.Size = new System.Drawing.Size(47, 23);
            this.saveCaseButton.TabIndex = 7;
            this.saveCaseButton.Text = "Save";
            this.saveCaseButton.UseVisualStyleBackColor = true;
            this.saveCaseButton.Click += new System.EventHandler(this.saveCaseButton_Click);
            // 
            // FilterManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 566);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.saveCaseButton);
            this.Controls.Add(this.saveFilterButton);
            this.Controls.Add(this.deleteCaseButton);
            this.Controls.Add(this.newCaseButton);
            this.Controls.Add(this.deleteFilterButton);
            this.Controls.Add(this.newFilterButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.filterCaseNameTextBox);
            this.Controls.Add(this.filterNameTextBox);
            this.Controls.Add(this.casesList);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.filtersList);
            this.Name = "FilterManagerForm";
            this.Text = "FilterManagerForm";
            this.Load += new System.EventHandler(this.FilterManagerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox filtersList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox casesList;
        private System.Windows.Forms.TextBox filterNameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button newFilterButton;
        private System.Windows.Forms.Button deleteFilterButton;
        private System.Windows.Forms.Button saveFilterButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox filterCaseNameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button newCaseButton;
        private System.Windows.Forms.Button deleteCaseButton;
        private System.Windows.Forms.Button saveCaseButton;
    }
}