namespace PortableAppStudio.Dialogs
{
    partial class NewPortableAppForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewPortableAppForm));
            this.appNameTextBox = new System.Windows.Forms.TextBox();
            this.bttnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.baseFolderTextBox = new System.Windows.Forms.TextBox();
            this.bttnCancel = new System.Windows.Forms.Button();
            this.bttnBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // appNameTextBox
            // 
            this.appNameTextBox.Location = new System.Drawing.Point(176, 16);
            this.appNameTextBox.Name = "appNameTextBox";
            this.appNameTextBox.Size = new System.Drawing.Size(301, 20);
            this.appNameTextBox.TabIndex = 0;
            // 
            // bttnOk
            // 
            this.bttnOk.Location = new System.Drawing.Point(402, 85);
            this.bttnOk.Name = "bttnOk";
            this.bttnOk.Size = new System.Drawing.Size(75, 23);
            this.bttnOk.TabIndex = 2;
            this.bttnOk.Text = "Ok";
            this.bttnOk.UseVisualStyleBackColor = true;
            this.bttnOk.Click += new System.EventHandler(this.bttnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Portable Application Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(163, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Portable Application Base Folder:";
            // 
            // baseFolderTextBox
            // 
            this.baseFolderTextBox.Location = new System.Drawing.Point(176, 48);
            this.baseFolderTextBox.Name = "baseFolderTextBox";
            this.baseFolderTextBox.Size = new System.Drawing.Size(301, 20);
            this.baseFolderTextBox.TabIndex = 1;
            // 
            // bttnCancel
            // 
            this.bttnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bttnCancel.Location = new System.Drawing.Point(483, 85);
            this.bttnCancel.Name = "bttnCancel";
            this.bttnCancel.Size = new System.Drawing.Size(75, 23);
            this.bttnCancel.TabIndex = 3;
            this.bttnCancel.Text = "Cancel";
            this.bttnCancel.UseVisualStyleBackColor = true;
            // 
            // bttnBrowse
            // 
            this.bttnBrowse.Location = new System.Drawing.Point(483, 45);
            this.bttnBrowse.Name = "bttnBrowse";
            this.bttnBrowse.Size = new System.Drawing.Size(75, 23);
            this.bttnBrowse.TabIndex = 4;
            this.bttnBrowse.Text = "Browse";
            this.bttnBrowse.UseVisualStyleBackColor = true;
            this.bttnBrowse.Click += new System.EventHandler(this.bttnBrowse_Click);
            // 
            // NewPortableAppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bttnCancel;
            this.ClientSize = new System.Drawing.Size(570, 122);
            this.Controls.Add(this.bttnBrowse);
            this.Controls.Add(this.bttnCancel);
            this.Controls.Add(this.baseFolderTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bttnOk);
            this.Controls.Add(this.appNameTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewPortableAppForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create New Portable App";
            this.Load += new System.EventHandler(this.NewPortableApp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox appNameTextBox;
        private System.Windows.Forms.Button bttnOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox baseFolderTextBox;
        private System.Windows.Forms.Button bttnCancel;
        private System.Windows.Forms.Button bttnBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}