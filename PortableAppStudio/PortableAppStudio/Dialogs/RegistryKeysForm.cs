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
    public partial class RegistryKeysForm : INIValueEditForm
    {
        public RegistryKeysForm()
        {
            InitializeComponent();
            this.Load += RegistryKeysForm_Load;

            bttnOk.Click += BttnOk_Click;

            helpTextBox.Rtf = PathManager.Init.GetHelpData("RegistryKeys");
        }

        private void BttnOk_Click(object sender, EventArgs e)
        {
            FullValue = string.Format("{0}={1}", fileNameTextBox.Text, regKeyTextBox.Text);
            DialogResult = DialogResult.OK;

            this.Close();
        }

        private void RegistryKeysForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Registry Keys - {0}", this.Title);
            if (!string.IsNullOrWhiteSpace(FullValue))
            {
                int startIndex = FullValue.IndexOf("=");
                if (startIndex != -1)
                {
                    fileNameTextBox.Text = FullValue.Substring(0, startIndex);
                    regKeyTextBox.Text = FullValue.Substring(startIndex + 1);
                }
                else
                {
                    fileNameTextBox.Text = FullValue;
                    regKeyTextBox.Text = "<New Reg Key>";
                }
            }
            else
            {
                regKeyTextBox.Text = "<New Reg Key>";
                fileNameTextBox.Text = "<Reg File Name from Data Folder>";
            }
        }
    }
}
