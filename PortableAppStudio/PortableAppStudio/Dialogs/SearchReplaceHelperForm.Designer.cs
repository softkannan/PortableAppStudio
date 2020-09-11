namespace PortableAppStudio.Dialogs
{
    partial class SearchReplaceHelperForm
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
            this.txtSearchReplace = new System.Windows.Forms.TextBox();
            this.bttnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtSearchReplace
            // 
            this.txtSearchReplace.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchReplace.Location = new System.Drawing.Point(12, 12);
            this.txtSearchReplace.Multiline = true;
            this.txtSearchReplace.Name = "txtSearchReplace";
            this.txtSearchReplace.Size = new System.Drawing.Size(1160, 647);
            this.txtSearchReplace.TabIndex = 0;
            // 
            // bttnOk
            // 
            this.bttnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bttnOk.Location = new System.Drawing.Point(1025, 672);
            this.bttnOk.Name = "bttnOk";
            this.bttnOk.Size = new System.Drawing.Size(146, 39);
            this.bttnOk.TabIndex = 1;
            this.bttnOk.Text = "Ok";
            this.bttnOk.UseVisualStyleBackColor = true;
            this.bttnOk.Click += new System.EventHandler(this.bttnOk_Click);
            // 
            // SearchReplaceHelperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 727);
            this.Controls.Add(this.bttnOk);
            this.Controls.Add(this.txtSearchReplace);
            this.MinimizeBox = false;
            this.Name = "SearchReplaceHelperForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SearchReplaceHelperForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearchReplace;
        private System.Windows.Forms.Button bttnOk;
    }
}