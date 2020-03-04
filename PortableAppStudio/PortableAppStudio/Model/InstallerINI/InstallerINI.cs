using PortableAppStudio.INI;
using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.InstallerINI
{
    public class InstallerINI
    {
        //private INIFile _iniFile = null;

        public void Load(string appInfoFolder)
        {
            var fileName = Directory.GetFiles(appInfoFolder, "installer*.ini").FirstOrDefault();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = string.Format("{0}\\AppCompactor.ini", appInfoFolder);
            }

            if (!File.Exists(fileName))
            {
                File.Copy(PathManager.Init.GetResourcePath(PathManager.INSTALLER_FILE), fileName);
            }

            if (File.Exists(fileName))
            {
                LoadInternal(fileName);
            }
        }

        private void LoadInternal(string fileName)
        {

        }
    }
}
