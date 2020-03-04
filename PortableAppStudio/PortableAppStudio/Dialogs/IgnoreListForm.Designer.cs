namespace PortableAppStudio.Dialogs
{
    partial class IgnoreListForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IgnoreListForm));
            this.bttnOk = new System.Windows.Forms.Button();
            this.ignoreListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToAbsoluteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToRelativeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainList = new System.Windows.Forms.ListView();
            this.bttnCopy = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.startCountText = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.thinAppRadio = new System.Windows.Forms.RadioButton();
            this.regshotRadio = new System.Windows.Forms.RadioButton();
            this.xregshotRadio = new System.Windows.Forms.RadioButton();
            this.noneRadio = new System.Windows.Forms.RadioButton();
            this.outputTypeCombo = new System.Windows.Forms.ComboBox();
            this.ignoreListContextMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startCountText)).BeginInit();
            this.SuspendLayout();
            // 
            // bttnOk
            // 
            this.bttnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bttnOk.Location = new System.Drawing.Point(979, 621);
            this.bttnOk.Name = "bttnOk";
            this.bttnOk.Size = new System.Drawing.Size(106, 32);
            this.bttnOk.TabIndex = 2;
            this.bttnOk.Text = "Ok";
            this.bttnOk.UseVisualStyleBackColor = true;
            this.bttnOk.Click += new System.EventHandler(this.bttnOk_Click);
            // 
            // ignoreListContextMenu
            // 
            this.ignoreListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copToolStripMenuItem,
            this.convertToAbsoluteToolStripMenuItem,
            this.convertToRelativeToolStripMenuItem});
            this.ignoreListContextMenu.Name = "ignoreListContextMenu";
            this.ignoreListContextMenu.Size = new System.Drawing.Size(177, 70);
            // 
            // copToolStripMenuItem
            // 
            this.copToolStripMenuItem.Name = "copToolStripMenuItem";
            this.copToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.copToolStripMenuItem.Text = "Copy";
            // 
            // convertToAbsoluteToolStripMenuItem
            // 
            this.convertToAbsoluteToolStripMenuItem.Name = "convertToAbsoluteToolStripMenuItem";
            this.convertToAbsoluteToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.convertToAbsoluteToolStripMenuItem.Text = "ConvertToAbsolute";
            // 
            // convertToRelativeToolStripMenuItem
            // 
            this.convertToRelativeToolStripMenuItem.Name = "convertToRelativeToolStripMenuItem";
            this.convertToRelativeToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.convertToRelativeToolStripMenuItem.Text = "ConvertToRelative";
            // 
            // mainList
            // 
            this.mainList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainList.ContextMenuStrip = this.ignoreListContextMenu;
            this.mainList.FullRowSelect = true;
            this.mainList.Location = new System.Drawing.Point(12, 12);
            this.mainList.Name = "mainList";
            this.mainList.Size = new System.Drawing.Size(1073, 594);
            this.mainList.TabIndex = 0;
            this.mainList.TabStop = false;
            this.mainList.UseCompatibleStateImageBehavior = false;
            this.mainList.View = System.Windows.Forms.View.Details;
            this.mainList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainList_KeyDown);
            // 
            // bttnCopy
            // 
            this.bttnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bttnCopy.Location = new System.Drawing.Point(873, 621);
            this.bttnCopy.Name = "bttnCopy";
            this.bttnCopy.Size = new System.Drawing.Size(100, 32);
            this.bttnCopy.TabIndex = 1;
            this.bttnCopy.Text = "Copy";
            this.bttnCopy.UseVisualStyleBackColor = true;
            this.bttnCopy.Click += new System.EventHandler(this.bttnCopy_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.startCountText);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.thinAppRadio);
            this.groupBox1.Controls.Add(this.regshotRadio);
            this.groupBox1.Controls.Add(this.xregshotRadio);
            this.groupBox1.Controls.Add(this.noneRadio);
            this.groupBox1.Location = new System.Drawing.Point(12, 612);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(675, 49);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // startCountText
            // 
            this.startCountText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.startCountText.Location = new System.Drawing.Point(563, 17);
            this.startCountText.Name = "startCountText";
            this.startCountText.Size = new System.Drawing.Size(106, 20);
            this.startCountText.TabIndex = 6;
            this.startCountText.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(494, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "StartCount :";
            // 
            // thinAppRadio
            // 
            this.thinAppRadio.AutoSize = true;
            this.thinAppRadio.Location = new System.Drawing.Point(212, 16);
            this.thinAppRadio.Name = "thinAppRadio";
            this.thinAppRadio.Size = new System.Drawing.Size(65, 17);
            this.thinAppRadio.TabIndex = 3;
            this.thinAppRadio.TabStop = true;
            this.thinAppRadio.Text = "ThinApp";
            this.thinAppRadio.UseVisualStyleBackColor = true;
            // 
            // regshotRadio
            // 
            this.regshotRadio.AutoSize = true;
            this.regshotRadio.Location = new System.Drawing.Point(141, 16);
            this.regshotRadio.Name = "regshotRadio";
            this.regshotRadio.Size = new System.Drawing.Size(65, 17);
            this.regshotRadio.TabIndex = 2;
            this.regshotRadio.TabStop = true;
            this.regshotRadio.Text = "Regshot";
            this.regshotRadio.UseVisualStyleBackColor = true;
            // 
            // xregshotRadio
            // 
            this.xregshotRadio.AutoSize = true;
            this.xregshotRadio.Location = new System.Drawing.Point(60, 16);
            this.xregshotRadio.Name = "xregshotRadio";
            this.xregshotRadio.Size = new System.Drawing.Size(75, 17);
            this.xregshotRadio.TabIndex = 1;
            this.xregshotRadio.TabStop = true;
            this.xregshotRadio.Text = "X-Regshot";
            this.xregshotRadio.UseVisualStyleBackColor = true;
            // 
            // noneRadio
            // 
            this.noneRadio.AutoSize = true;
            this.noneRadio.Location = new System.Drawing.Point(3, 16);
            this.noneRadio.Name = "noneRadio";
            this.noneRadio.Size = new System.Drawing.Size(51, 17);
            this.noneRadio.TabIndex = 0;
            this.noneRadio.TabStop = true;
            this.noneRadio.Text = "None";
            this.noneRadio.UseVisualStyleBackColor = true;
            // 
            // outputTypeCombo
            // 
            this.outputTypeCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.outputTypeCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outputTypeCombo.FormattingEnabled = true;
            this.outputTypeCombo.Items.AddRange(new object[] {
            "Absolute",
            "Relative"});
            this.outputTypeCombo.Location = new System.Drawing.Point(693, 629);
            this.outputTypeCombo.Name = "outputTypeCombo";
            this.outputTypeCombo.Size = new System.Drawing.Size(174, 21);
            this.outputTypeCombo.TabIndex = 7;
            // 
            // IgnoreListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1097, 673);
            this.Controls.Add(this.outputTypeCombo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.bttnCopy);
            this.Controls.Add(this.mainList);
            this.Controls.Add(this.bttnOk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "IgnoreListForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IgnoreListForm";
            this.ignoreListContextMenu.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startCountText)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button bttnOk;
        private System.Windows.Forms.ContextMenuStrip ignoreListContextMenu;
        private System.Windows.Forms.ToolStripMenuItem copToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToAbsoluteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToRelativeToolStripMenuItem;
        private System.Windows.Forms.ListView mainList;
        private System.Windows.Forms.Button bttnCopy;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton thinAppRadio;
        private System.Windows.Forms.RadioButton regshotRadio;
        private System.Windows.Forms.RadioButton xregshotRadio;
        private System.Windows.Forms.RadioButton noneRadio;
        private System.Windows.Forms.NumericUpDown startCountText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox outputTypeCombo;
    }
}