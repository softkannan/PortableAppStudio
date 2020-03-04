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
            ParseInternal(fileName);
        }

        private void ParseInternal(string folderName)
        {
            string rootFolder = Path.GetDirectoryName(folderName);
            string rootFolderSub = PathManager.Init.GetExpandablePath(rootFolder);
            foreach (var item in Directory.GetDirectories(folderName))
            {
                AddFolder(item, rootFolder, rootFolderSub);
            }
        }
    }
}
