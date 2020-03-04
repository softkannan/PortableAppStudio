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
    public partial class FilesMoveForm : INIValueEditForm
    {
        public FilesMoveForm()
        {
            InitializeComponent();
            this.Load += FilesMoveForm_Load;

            bttnOk.Click += BttnOk_Click;

            helpTextBox.Rtf = PathManager.Init.GetHelpData("FilesMove");

            fileNameTextBox.AutoCompleteCustomSource = Model.InteliSense.Inst.EnvVaraibles;
            fileNameTextBox.AutoCompleteCustomSource = Model.InteliSense.Inst.EnvVaraibles;
        }

        private void BttnOk_Click(object sender, EventArgs e)
        {
            FullValue = string.Format("{0}={1}", fileNameTextBox.Text, filePathTextBox.Text);
            DialogResult = DialogResult.OK;
            
            this.Close();
        }

        private void FilesMoveForm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("Files Move - {0}", this.Title);
            if (!string.IsNullOrWhiteSpace(FullValue))
            {
                int startIndex = FullValue.IndexOf("=");
                if(startIndex != -1)
                {
                    filePathTextBox.Text = FullValue.Substring(startIndex + 1);
                    fileNameTextBox.Text = FullValue.Substring(0, startIndex);
                }
                else
                {
                    fileNameTextBox.Text = FullValue;
                    filePathTextBox.Text = "<Target File Path>";
                }
            }
            else
            {
                filePathTextBox.Text = "<Target File Path>";
                fileNameTextBox.Text = "<File Name from Data Folder";
            }
        }
    }
}
