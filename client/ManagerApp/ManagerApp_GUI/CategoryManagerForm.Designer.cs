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
            this.SuspendLayout();
            // 
            // categoryList
            // 
            this.categoryList.FormattingEnabled = true;
            this.categoryList.Location = new System.Drawing.Point(12, 65);
            this.categoryList.Name = "categoryList";
            this.categoryList.Size = new System.Drawing.Size(120, 433);
            this.categoryList.TabIndex = 0;
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
            // CategoryManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 510);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.categoryList);
            this.Name = "CategoryManagerForm";
            this.Text = "CategoryManagerForm";
            this.Load += new System.EventHandler(this.CategoryManagerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox categoryList;
        private System.Windows.Forms.Button upButton;
    }
}