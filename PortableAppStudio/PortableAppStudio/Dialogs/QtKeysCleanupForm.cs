using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Dialogs
{
    public partial class QtKeysCleanupForm : INIValueEditForm
    {
        public QtKeysCleanupForm()
        {
            InitializeComponent();
            this.Load += QtKeysCleanupForm_Load;
            bttnOk.Click += BttnOk_Click;

            helpTextBox.Rtf = PathManager.Init.GetHelpData("QtKeysCleanup");
        }
        private void BttnOk_Click(object sender, EventArgs e)
        {
            FullValue = string.Format("{0}={1}", seqTextBox.Text, regKeyTextBox.Text);
            DialogResult = DialogResult.OK;
            this.Close();
        }
        private void QtKeysCleanupForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("QtKeysCleanup - {0}", this.Title);
            seqTextBox.Enabled = false;
            if (!string.IsNullOrWhiteSpace(FullValue))
            {
                int startIndex = FullValue.IndexOf("=");
                if (startIndex != -1)
                {
                    regKeyTextBox.Text = FullValue.Substring(startIndex + 1);
                    seqTextBox.Text = FullValue.Substring(0, startIndex);
                }
                else
                {
                    seqTextBox.Text = FullValue;
                    regKeyTextBox.Text = "<New Reg Key>";
                }
            }
            else
            {
                regKeyTextBox.Text = "<New Reg Key>";
                seqTextBox.Enabled = false;
                seqTextBox.Text = Index.ToString();
            }
        }
    }
}

