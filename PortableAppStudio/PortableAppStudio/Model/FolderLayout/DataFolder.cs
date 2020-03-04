using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Model.FolderLayout
{
    public class DataFolder : AppFolderBase
    {
        private string _dataFolder = string.Empty;
        private string _defaultDataFolder = string.Empty;

        public void LoadFolder(string folderName, string portableAppFolder)
        {
            this.RootFolder = folderName;
            this.PortableAppFolder = portableAppFolder;
            _defaultDataFolder = string.Format("{0}\\App\\DefaultData", this.PortableAppFolder);

            if(!Directory.Exists(_defaultDataFolder))
            {
                ErrorLog.Inst.ShowInfo("Unable to Find Default Data folder {0}", _defaultDataFolder);
            }
        }

        public void SaveTree(TreeView srcFileTree, TreeView srcRegTree, TreeView destFileTree, TreeView destRegTree)
        {
            foreach (TreeNode item in destFileTree.Nodes)
            {
                foreach (TreeNode appNode in item.Nodes)
                {
                    if (item.Text == "Data")
                    {
                        SaveFileNode(appNode);
                    }
                }
            }

            FileUtility.Inst.CopyAll(_defaultDataFolder, this.RootFolder);
        }
    }
}
