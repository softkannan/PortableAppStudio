using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Model.FolderLayout
{   
    public class OtherFolder : AppFolderBase
    {
        private string _helpFolder = string.Empty;
        private string _imagesFolder = string.Empty;
        private string _sourceFolder = string.Empty;

        public void LoadFolder(string folderName)
        {
            this.RootFolder = folderName;
            _helpFolder = string.Format("{0}\\Help", folderName);
            _sourceFolder = string.Format("{0}\\Source", folderName);
            _imagesFolder = string.Format("{0}\\Images", _helpFolder);

            if (!Directory.Exists(_helpFolder))
            {
                Directory.CreateDirectory(_helpFolder);
            }
            if (!Directory.Exists(_sourceFolder))
            {
                Directory.CreateDirectory(_sourceFolder);
            }
            if (!Directory.Exists(_imagesFolder))
            {
                Directory.CreateDirectory(_imagesFolder);
            }
        }

        public void SaveTree(TreeView srcFileTree, TreeView srcRegTree, TreeView destFileTree, TreeView destRegTree)
        {

        }
    }
}
