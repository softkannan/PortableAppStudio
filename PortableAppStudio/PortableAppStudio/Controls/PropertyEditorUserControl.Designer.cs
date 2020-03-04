namespace PortableAppStudio.Controls
{
    partial class PropertyEditorUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.helpTextBox = new System.Windows.Forms.RichTextBox();
            this.containerGroupBox = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // helpTextBox
            // 
            this.helpTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.helpTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.helpTextBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.helpTextBox.Location = new System.Drawing.Point(4, 403);
            this.helpTextBox.Name = "helpTextBox";
            this.helpTextBox.ReadOnly = true;
            this.helpTextBox.ShortcutsEnabled = false;
            this.helpTextBox.Size = new System.Drawing.Size(574, 283);
            this.helpTextBox.TabIndex = 11;
            this.helpTextBox.Text = "";
            // 
            // containerGroupBox
            // 
            this.containerGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.containerGroupBox.Location = new System.Drawing.Point(4, 5);
            this.containerGroupBox.Name = "containerGroupBox";
            this.containerGroupBox.Size = new System.Drawing.Size(574, 392);
            this.containerGroupBox.TabIndex = 10;
            this.containerGroupBox.TabStop = false;
            // 
            // PropertyEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.helpTextBox);
            this.Controls.Add(this.containerGroupBox);
            this.Name = "PropertyEditor";
            this.Size = new System.Drawing.Size(583, 694);
            this.SizeChanged += new System.EventHandler(this.PropertyEditor_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox helpTextBox;
        private System.Windows.Forms.GroupBox containerGroupBox;
    }
}
