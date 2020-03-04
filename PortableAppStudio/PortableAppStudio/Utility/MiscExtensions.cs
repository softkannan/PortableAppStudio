using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Utility
{
    public static class MiscExtensions
    {
        public static string GetFilePath(this List<string> allFiles, string fileName)
        {
            string retVal = "";
            foreach (var item in allFiles)
            {
                if (item.EndsWith(fileName,StringComparison.InvariantCultureIgnoreCase))
                {
                    retVal = item;
                    break;
                }
            }
            return retVal;
        }
    }
}
