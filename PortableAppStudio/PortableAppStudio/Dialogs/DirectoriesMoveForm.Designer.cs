namespace PortableAppStudio.Dialogs
{
    partial class DirectoriesMoveForm
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
            this.bttnCancel = new System.Windows.Forms.Button();
            this.targetDirTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bttnOk = new System.Windows.Forms.Button();
            this.srcDirTextBox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.helpTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // bttnCancel
            // 
            this.bttnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bttnCancel.Location = new System.Drawing.Point(622, 370);
            this.bttnCancel.Name = "bttnCancel";
            this.bttnCancel.Size = new System.Drawing.Size(75, 23);
            this.bttnCancel.TabIndex = 3;
            this.bttnCancel.Text = "Cancel";
            this.bttnCancel.UseVisualStyleBackColor = true;
            // 
            // targetDirTextBox
            // 
            this.targetDirTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.targetDirTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.targetDirTextBox.Location = new System.Drawing.Point(135, 41);
            this.targetDirTextBox.Name = "targetDirTextBox";
            this.targetDirTextBox.Size = new System.Drawing.Size(562, 20);
            this.targetDirTextBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 16;
            this.label2.Text = "Target Folder Path:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Folder Name In [Data] :";
            // 
            // bttnOk
            // 
            this.bttnOk.Location = new System.Drawing.Point(541, 370);
            this.bttnOk.Name = "bttnOk";
            this.bttnOk.Size = new System.Drawing.Size(75, 23);
            this.bttnOk.TabIndex = 2;
            this.bttnOk.Text = "Ok";
            this.bttnOk.UseVisualStyleBackColor = true;
            // 
            // srcDirTextBox
            // 
            this.srcDirTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.srcDirTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.srcDirTextBox.Location = new System.Drawing.Point(135, 12);
            this.srcDirTextBox.Name = "srcDirTextBox";
            this.srcDirTextBox.Size = new System.Drawing.Size(562, 20);
            this.srcDirTextBox.TabIndex = 0;
            // 
            // helpTextBox
            // 
            this.helpTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.helpTextBox.Location = new System.Drawing.Point(15, 76);
            this.helpTextBox.Name = "helpTextBox";
            this.helpTextBox.ReadOnly = true;
            this.helpTextBox.Size = new System.Drawing.Size(682, 288);
            this.helpTextBox.TabIndex = 31;
            this.helpTextBox.TabStop = false;
            this.helpTextBox.Text = "";
            // 
            // DirectoriesMoveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.bttnCancel;
            this.ClientSize = new System.Drawing.Size(709, 415);
            this.Controls.Add(this.helpTextBox);
            this.Controls.Add(this.bttnCancel);
            this.Controls.Add(this.targetDirTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bttnOk);
            this.Controls.Add(this.srcDirTextBox);
            this.Name = "DirectoriesMoveForm";
            this.Text = "DirectoriesMove";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bttnCancel;
        private System.Windows.Forms.TextBox targetDirTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bttnOk;
        private System.Windows.Forms.TextBox srcDirTextBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.RichTextBox helpTextBox;
    }
}