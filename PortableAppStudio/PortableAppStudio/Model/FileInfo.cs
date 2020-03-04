using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model
{
    public class FileInfo
    {
        public FileInfo(string absolutePath,string relativePath = null)
        {
            this.AbsolutePath = absolutePath;
            if(relativePath == null)
            {
                this.RelativePath = PathManager.Init.GetExpandablePath(this.AbsolutePath);
            }
            else
            {
                this.RelativePath = relativePath;
            }

            CanDelete = false;
        }

        public bool CanDelete { get; set; }

        public string RelativePath { get; private set; }
        public string AbsolutePath { get; private set; }
    }
}
