namespace ManagerApp_GUI
{
    partial class MainForm
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
            this.ItemManagerButton = new System.Windows.Forms.Button();
            this.OrderManagerButton = new System.Windows.Forms.Button();
            this.CategoryManagerButton = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ItemManagerButton
            // 
            this.ItemManagerButton.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ItemManagerButton.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.ItemManagerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ItemManagerButton.Location = new System.Drawing.Point(118, 114);
            this.ItemManagerButton.Name = "ItemManagerButton";
            this.ItemManagerButton.Size = new System.Drawing.Size(144, 96);
            this.ItemManagerButton.TabIndex = 0;
            this.ItemManagerButton.TabStop = false;
            this.ItemManagerButton.Text = "Item manager";
            this.ItemManagerButton.UseVisualStyleBackColor = false;
            this.ItemManagerButton.Click += new System.EventHandler(this.ItemManagerButton_Click);
            // 
            // OrderManagerButton
            // 
            this.OrderManagerButton.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.OrderManagerButton.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.OrderManagerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OrderManagerButton.Location = new System.Drawing.Point(118, 216);
            this.OrderManagerButton.Name = "OrderManagerButton";
            this.OrderManagerButton.Size = new System.Drawing.Size(144, 96);
            this.OrderManagerButton.TabIndex = 1;
            this.OrderManagerButton.TabStop = false;
            this.OrderManagerButton.Text = "Customer Orders";
            this.OrderManagerButton.UseVisualStyleBackColor = false;
            this.OrderManagerButton.Click += new System.EventHandler(this.OrderManagerButton_Click);
            // 
            // CategoryManagerButton
            // 
            this.CategoryManagerButton.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.CategoryManagerButton.FlatAppearance.BorderColor = System.Drawing.Color.LightGray;
            this.CategoryManagerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CategoryManagerButton.Location = new System.Drawing.Point(118, 12);
            this.CategoryManagerButton.Name = "CategoryManagerButton";
            this.CategoryManagerButton.Size = new System.Drawing.Size(144, 96);
            this.CategoryManagerButton.TabIndex = 2;
            this.CategoryManagerButton.TabStop = false;
            this.CategoryManagerButton.Text = "Category Manager";
            this.CategoryManagerButton.UseVisualStyleBackColor = false;
            this.CategoryManagerButton.Click += new System.EventHandler(this.CategoryManagerButton_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = global::ManagerApp_GUI.Properties.Resources.User;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox3.InitialImage = null;
            this.pictureBox3.Location = new System.Drawing.Point(16, 216);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(96, 96);
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::ManagerApp_GUI.Properties.Resources.Camera;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(16, 114);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(96, 96);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::ManagerApp_GUI.Properties.Resources.Folder;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(16, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(96, 96);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(271, 324);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.CategoryManagerButton);
            this.Controls.Add(this.OrderManagerButton);
            this.Controls.Add(this.ItemManagerButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "PCBuilder Manager";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ItemManagerButton;
        private System.Windows.Forms.Button OrderManagerButton;
        private System.Windows.Forms.Button CategoryManagerButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}

