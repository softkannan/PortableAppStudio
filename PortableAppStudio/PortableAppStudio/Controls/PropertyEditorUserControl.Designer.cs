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
            helpTextBox = new System.Windows.Forms.RichTextBox();
            containerGroupBox = new System.Windows.Forms.GroupBox();
            SuspendLayout();
            // 
            // helpTextBox
            // 
            helpTextBox.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            helpTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            helpTextBox.Location = new System.Drawing.Point(5, 525);
            helpTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            helpTextBox.Name = "helpTextBox";
            helpTextBox.ReadOnly = true;
            helpTextBox.ShortcutsEnabled = false;
            helpTextBox.Size = new System.Drawing.Size(765, 530);
            helpTextBox.TabIndex = 11;
            helpTextBox.Text = "";
            // 
            // containerGroupBox
            // 
            containerGroupBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            containerGroupBox.Location = new System.Drawing.Point(5, 8);
            containerGroupBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            containerGroupBox.Name = "containerGroupBox";
            containerGroupBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            containerGroupBox.Size = new System.Drawing.Size(765, 507);
            containerGroupBox.TabIndex = 10;
            containerGroupBox.TabStop = false;
            // 
            // PropertyEditorUserControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(helpTextBox);
            Controls.Add(containerGroupBox);
            Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            Name = "PropertyEditorUserControl";
            Size = new System.Drawing.Size(777, 1068);
            SizeChanged += PropertyEditor_SizeChanged;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.RichTextBox helpTextBox;
        private System.Windows.Forms.GroupBox containerGroupBox;
    }
}
