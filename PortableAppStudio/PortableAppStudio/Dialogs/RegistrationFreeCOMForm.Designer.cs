﻿namespace PortableAppStudio.Dialogs
{
    partial class RegistrationFreeCOMForm
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
            this.helpTextBox = new System.Windows.Forms.RichTextBox();
            this.bttnCancel = new System.Windows.Forms.Button();
            this.comFileName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bttnOk = new System.Windows.Forms.Button();
            this.seqTextBox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // helpTextBox
            // 
            this.helpTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.helpTextBox.Location = new System.Drawing.Point(12, 87);
            this.helpTextBox.Name = "helpTextBox";
            this.helpTextBox.ReadOnly = true;
            this.helpTextBox.Size = new System.Drawing.Size(583, 296);
            this.helpTextBox.TabIndex = 67;
            this.helpTextBox.TabStop = false;
            this.helpTextBox.Text = "";
            // 
            // bttnCancel
            // 
            this.bttnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.bttnCancel.Location = new System.Drawing.Point(523, 400);
            this.bttnCancel.Name = "bttnCancel";
            this.bttnCancel.Size = new System.Drawing.Size(75, 23);
            this.bttnCancel.TabIndex = 64;
            this.bttnCancel.Text = "Cancel";
            this.bttnCancel.UseVisualStyleBackColor = true;
            // 
            // comFileName
            // 
            this.comFileName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comFileName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comFileName.Location = new System.Drawing.Point(61, 49);
            this.comFileName.Name = "comFileName";
            this.comFileName.Size = new System.Drawing.Size(537, 20);
            this.comFileName.TabIndex = 62;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 66;
            this.label2.Text = "Key:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 65;
            this.label1.Text = "Seq #:";
            // 
            // bttnOk
            // 
            this.bttnOk.Location = new System.Drawing.Point(442, 400);
            this.bttnOk.Name = "bttnOk";
            this.bttnOk.Size = new System.Drawing.Size(75, 23);
            this.bttnOk.TabIndex = 63;
            this.bttnOk.Text = "Ok";
            this.bttnOk.UseVisualStyleBackColor = true;
            // 
            // seqTextBox
            // 
            this.seqTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.seqTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.seqTextBox.Location = new System.Drawing.Point(61, 15);
            this.seqTextBox.Name = "seqTextBox";
            this.seqTextBox.Size = new System.Drawing.Size(537, 20);
            this.seqTextBox.TabIndex = 61;
            // 
            // RegistrationFreeCOMForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 435);
            this.Controls.Add(this.helpTextBox);
            this.Controls.Add(this.bttnCancel);
            this.Controls.Add(this.comFileName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bttnOk);
            this.Controls.Add(this.seqTextBox);
            this.Name = "RegistrationFreeCOMForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RegistrationFreeCOMForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox helpTextBox;
        private System.Windows.Forms.Button bttnCancel;
        private System.Windows.Forms.TextBox comFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bttnOk;
        private System.Windows.Forms.TextBox seqTextBox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}