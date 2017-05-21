namespace ManagerApp_GUI
{
    partial class OrderManagerForm
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
            this.OrderList = new System.Windows.Forms.ListBox();
            this.OnlyPendingCheck = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.OrderNameLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.InfoTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.StatusSelector = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // OrderList
            // 
            this.OrderList.FormattingEnabled = true;
            this.OrderList.Location = new System.Drawing.Point(12, 83);
            this.OrderList.Name = "OrderList";
            this.OrderList.Size = new System.Drawing.Size(284, 173);
            this.OrderList.TabIndex = 0;
            this.OrderList.SelectedIndexChanged += new System.EventHandler(this.OrderList_SelectedIndexChanged);
            // 
            // OnlyPendingCheck
            // 
            this.OnlyPendingCheck.AutoSize = true;
            this.OnlyPendingCheck.Checked = true;
            this.OnlyPendingCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OnlyPendingCheck.Location = new System.Drawing.Point(12, 56);
            this.OnlyPendingCheck.Name = "OnlyPendingCheck";
            this.OnlyPendingCheck.Size = new System.Drawing.Size(88, 17);
            this.OnlyPendingCheck.TabIndex = 1;
            this.OnlyPendingCheck.Text = "Only pending";
            this.OnlyPendingCheck.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search order by id:";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(258, 30);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(34, 23);
            this.SearchButton.TabIndex = 17;
            this.SearchButton.Text = "GO!";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // SearchBox
            // 
            this.SearchBox.Location = new System.Drawing.Point(12, 30);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(240, 20);
            this.SearchBox.TabIndex = 16;
            // 
            // OrderNameLabel
            // 
            this.OrderNameLabel.AutoSize = true;
            this.OrderNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OrderNameLabel.Location = new System.Drawing.Point(298, 27);
            this.OrderNameLabel.Name = "OrderNameLabel";
            this.OrderNameLabel.Size = new System.Drawing.Size(74, 24);
            this.OrderNameLabel.TabIndex = 18;
            this.OrderNameLabel.Text = "Order #";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(299, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Info:";
            // 
            // InfoTextBox
            // 
            this.InfoTextBox.Location = new System.Drawing.Point(299, 83);
            this.InfoTextBox.Multiline = true;
            this.InfoTextBox.Name = "InfoTextBox";
            this.InfoTextBox.ReadOnly = true;
            this.InfoTextBox.Size = new System.Drawing.Size(277, 127);
            this.InfoTextBox.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(299, 213);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Status:";
            // 
            // StatusSelector
            // 
            this.StatusSelector.FormattingEnabled = true;
            this.StatusSelector.Items.AddRange(new object[] {
            "Pending",
            "Canceled",
            "Shipping",
            "Delivered"});
            this.StatusSelector.Location = new System.Drawing.Point(302, 230);
            this.StatusSelector.Name = "StatusSelector";
            this.StatusSelector.Size = new System.Drawing.Size(274, 21);
            this.StatusSelector.TabIndex = 22;
            this.StatusSelector.SelectedIndexChanged += new System.EventHandler(this.StatusSelector_SelectedIndexChanged);
            // 
            // OrderManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 263);
            this.Controls.Add(this.StatusSelector);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.InfoTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.OrderNameLabel);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.SearchBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.OnlyPendingCheck);
            this.Controls.Add(this.OrderList);
            this.Name = "OrderManagerForm";
            this.Text = "OrderManagerForm";
            this.Load += new System.EventHandler(this.OrderManagerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox OrderList;
        private System.Windows.Forms.CheckBox OnlyPendingCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Label OrderNameLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox InfoTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox StatusSelector;
    }
}