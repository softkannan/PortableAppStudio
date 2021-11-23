using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Parser
{
    public class FolderParser : ParserBase
    {
        public override void Parse(string fileName)
        {
            this.ParserType = RegSourceType.RegFile;
            ParseInternal(fileName);
        }

        private void ParseInternal(string fileOrFolderName)
        {
            if (Directory.Exists(fileOrFolderName))
            {
                int isEmptyFolder = 0;

                foreach (var item in Directory.GetDirectories(fileOrFolderName))
                {
                    AddFolderInternal(item);
                    isEmptyFolder = 1;
                }

                if (isEmptyFolder == 0)
                {
                    AddFolderInternal(fileOrFolderName);
                }
            }
            else if (File.Exists(fileOrFolderName))
            {
                string topRelPath = PathManager.Init.GetExpandablePath(fileOrFolderName);
                AddFile(fileOrFolderName, topRelPath);
            }
        }

        protected void AddFolderInternal(string folderName)
        {
            int isEmptyFolder = 0;
            string topRelPath = PathManager.Init.GetExpandablePath(folderName);
            foreach (var item in Directory.GetFiles(folderName, "*.*", SearchOption.TopDirectoryOnly))
            {
                var tempRelPath = item.Replace(folderName, topRelPath, StringComparison.OrdinalIgnoreCase);
                AddFile(item, tempRelPath);
                isEmptyFolder = 1;
            }

            foreach (var item in Directory.GetDirectories(folderName))
            {
                isEmptyFolder = 2;
                AddFolderInternal(item);
            }

            if (isEmptyFolder == 0)
            {
                AddFile(folderName, topRelPath);
            }
        }
    }
}
