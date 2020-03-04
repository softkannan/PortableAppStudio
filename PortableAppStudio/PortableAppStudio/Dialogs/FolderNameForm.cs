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
    public partial class FolderNameForm : INIValueEditForm
    {
        public FolderNameForm()
        {
            InitializeComponent();

            this.Load += FileEditForm_Load;
        }

        private void FileEditForm_Load(object sender, EventArgs e)
        {
            this.mainTextBox.Text = FullValue;
            this.Text = string.Format("Folder - {0}", this.Title);
        }

        private void bttnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.FullValue = mainTextBox.Text;
            this.Close();
        }
    }
}
