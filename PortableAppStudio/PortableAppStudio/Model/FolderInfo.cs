using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model
{
    public class FolderInfo
    {
        public FolderInfo(string folderName)
        {
            this.FolderName = folderName;
            CanDelete = false;
        }
        public bool CanDelete { get; set; }
        public string FolderName { get; private set; }
    }
}
