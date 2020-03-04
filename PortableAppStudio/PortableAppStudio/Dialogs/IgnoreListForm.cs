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
    public partial class IgnoreListForm : Form
    {
        private string regshotExcludeSectionName;
        public IgnoreListForm()
        {
            InitializeComponent();

            noneRadio.Checked = true;
            outputTypeCombo.SelectedIndex = 0;
        }

        public void InitalizeExcludeFolders()
        {
            mainList.Columns.Add("Folders");
            mainList.Columns.Add("Path");
            this.Text = "Ignore Folders";
            outputTypeCombo.Enabled = true;

            regshotExcludeSectionName = "Folders.Exclude";

            var rawLines = FileUtility.Inst.GetFileLines(PathManager.Init.GetResourcePath(PathManager.IGNOREFOLDERS_FILE));

            foreach(var item in rawLines)
            {
                ListViewItem tempItem = new ListViewItem(item);
                tempItem.SubItems.Add(Environment.ExpandEnvironmentVariables(item));
                mainList.Items.Add(tempItem);
            }

            mainList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        public void InitalizeExcludeFiles()
        {
            mainList.Columns.Add("Files");
            mainList.Columns.Add("Path");

            this.Text = "Ignore Files";
            regshotExcludeSectionName = "Files.Exclude";
            outputTypeCombo.Enabled = false;

            var rawLines = FileUtility.Inst.GetFileLines(PathManager.Init.GetResourcePath(PathManager.IGNOREFILES_FILE));

            foreach (var item in rawLines)
            {
                ListViewItem tempItem = new ListViewItem(item);
                tempItem.SubItems.Add(Environment.ExpandEnvironmentVariables(item));
                mainList.Items.Add(tempItem);
            }

            mainList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        }

        public void InitalizeExcludeRegKeys()
        {
            mainList.Columns.Add("Registry Keys");

            this.Text = "Ignore Registry Keys";
            regshotExcludeSectionName = "Registry.Exclude";
            outputTypeCombo.Enabled = false;

            var rawLines = FileUtility.Inst.GetFileLines(PathManager.Init.GetResourcePath(PathManager.IGNOREREGISTRYKEYS_FILE));

            foreach (var item in rawLines)
            {
                ListViewItem tempItem = new ListViewItem(item);
                mainList.Items.Add(tempItem);
            }

            mainList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        }

        private void bttnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mainList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control)
            {
                mainList.MultiSelect = true;
                foreach (ListViewItem item in mainList.Items)
                {
                    item.Selected = true;
                }
            }
            else if(e.KeyCode == Keys.C && e.Control)
            {
                String text = "";
                foreach (ListViewItem item in mainList.SelectedItems)
                {
                    text += string.Format("{0}\r\n", item.SubItems[0].Text);
                }
                Clipboard.SetText(text);
            }
        }

        private string FormatText(string textValue,string sectionName,int counterValue)
        {
            string retVal = string.Empty;
            if (noneRadio.Checked)
            {
                retVal += string.Format("{0}\r\n", textValue);
            }
            else if (thinAppRadio.Checked)
            {
                retVal += string.Format("{0:d4}={1}\r\n", counterValue, textValue);
            }
            else if (xregshotRadio.Checked)
            {
                retVal += string.Format("{0}|{1}=1\r\n", sectionName, textValue);
            }
            else if (regshotRadio.Checked)
            {
                retVal += string.Format("{0}=1\r\n", textValue);
            }
            return retVal;
        }

        private void bttnCopy_Click(object sender, EventArgs e)
        {
            String text = "";
            int startCount = (int)startCountText.Value;
            foreach (ListViewItem item in mainList.Items)
            {
                string tempText = outputTypeCombo.SelectedIndex == 0 ? Environment.ExpandEnvironmentVariables(item.SubItems[0].Text) : PathManager.Init.GetExpandablePath(item.SubItems[1].Text);
                text += FormatText(tempText, regshotExcludeSectionName,startCount);
                startCount++;
            }
            try
            {
                Clipboard.SetText(text);
            }
            catch(Exception ex)
            {
                ErrorLog.Inst.ShowError("Please disable any cliboard monitoring apps and retry : {0}", ex.Message);
            }
        }
    }
}
