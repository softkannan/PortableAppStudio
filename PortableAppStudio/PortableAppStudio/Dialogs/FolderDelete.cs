using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Dialogs
{
    public partial class FolderDelete : System.Windows.Forms.Form
    {
        public FolderDelete()
        {
            InitializeComponent();
        }

        private void FolderDelete_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                string[] tempData = e.Data.GetData("FileDrop") as string[];
                if(tempData != null && tempData.Length >0)
                {
                    string folderPath = tempData[0];

                    if(File.Exists(folderPath))
                    {
                        ErrorLog.Inst.ShowError("Please Drop Folder Only");
                    }

                    if(Directory.Exists(folderPath))
                    {
                        var allFiles = Directory.GetFiles(string.Format(@"\\?\{0}", folderPath), "*.*", SearchOption.AllDirectories);

                        foreach(var item in allFiles)
                        {
                            File.Delete(item);
                        }

                        FileUtility.Inst.RemoveEmptyDir(string.Format(@"\\?\{0}", folderPath));

                        Directory.Delete(string.Format(@"\\?\{0}", folderPath));
                    }

                }
            }
            catch(Exception ex)
            {
                ErrorLog.Inst.ShowError("Folder Delete Failed : {0}", ex.Message);
            }
        }

        private void FolderDelete_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
    }
}
