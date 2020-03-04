namespace PortableAppStudio.Dialogs
{
    partial class FolderNameForm
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
            this.mainTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bttnOk = new System.Windows.Forms.Button();
            this.bttnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mainTextBox
            // 
            this.mainTextBox.AutoCompleteCustomSource.AddRange(new string[] {
            "%AppData%\\Microsoft",
            "%APPDATA%\\PeaZip",
            "%APPDATA%\\Thinstall",
            "%LocalAppData%\\Comms",
            "%LocalAppData%\\ConnectedDevicesPlatform",
            "%LocalAppData%\\Microsoft",
            "%LocalAppData%\\Packages",
            "%LocalAppData%\\VirtualStore",
            "%ProgramData%\\Microsoft",
            "%ProgramData%\\USOShared",
            "%SystemDrive%\\$Recycle.bin",
            "%SystemDrive%\\Boot",
            "%SystemDrive%\\RECYCLER",
            "%SystemRoot%\\appcompat",
            "%SystemRoot%\\CSC",
            "%SystemRoot%\\ie7",
            "%SystemRoot%\\INF",
            "%SystemRoot%\\Installer",
            "%SystemRoot%\\Logs",
            "%SystemRoot%\\Microsoft.NET",
            "%SystemRoot%\\Performance",
            "%SystemRoot%\\Prefetch"});
            this.mainTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.mainTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.mainTextBox.Location = new System.Drawing.Point(15, 25);
            this.mainTextBox.Name = "mainTextBox";
            this.mainTextBox.Size = new System.Drawing.Size(461, 20);
            this.mainTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter Folder Name:";
            // 
            // bttnOk
            // 
            this.bttnOk.Location = new System.Drawing.Point(322, 51);
            this.bttnOk.Name = "bttnOk";
            this.bttnOk.Size = new System.Drawing.Size(75, 23);
            this.bttnOk.TabIndex = 1;
            this.bttnOk.Text = "Ok";
            this.bttnOk.UseVisualStyleBackColor = true;
            this.bttnOk.Click += new System.EventHandler(this.bttnOk_Click);
            // 
            // bttnCancel
            // 
            this.bttnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bttnCancel.Location = new System.Drawing.Point(403, 51);
            this.bttnCancel.Name = "bttnCancel";
            this.bttnCancel.Size = new System.Drawing.Size(75, 23);
            this.bttnCancel.TabIndex = 2;
            this.bttnCancel.Text = "Cancel";
            this.bttnCancel.UseVisualStyleBackColor = true;
            // 
            // FolderNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bttnCancel;
            this.ClientSize = new System.Drawing.Size(490, 87);
            this.Controls.Add(this.bttnCancel);
            this.Controls.Add(this.bttnOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mainTextBox);
            this.Name = "FolderNameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Folder Name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox mainTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bttnOk;
        private System.Windows.Forms.Button bttnCancel;
    }
}