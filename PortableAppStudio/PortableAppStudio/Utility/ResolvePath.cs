using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Utility
{
    public static class ResolvePath
    {
        private static string _homeDir = null;
        public static string ResolveFullPath(this string pThis)
        {
            if (string.IsNullOrEmpty(pThis))
            {
                return "";
            }

            try
            {

                string expandedValue = Environment.ExpandEnvironmentVariables(pThis);

                if (expandedValue.IndexOf("..\\") == -1 &&
                    expandedValue.IndexOf(@".\\") == -1 &&
                    expandedValue.IndexOf("../") == -1 &&
                    expandedValue.IndexOf(@"./") == -1 &&
                    expandedValue.IndexOf(@".") != 0 &&
                    expandedValue.IndexOf(@"..") != 0)
                {
                    if (Directory.Exists(expandedValue))
                    {
                        return expandedValue;
                    }
                    if (File.Exists(expandedValue))
                    {
                        return expandedValue;
                    }
                }

                try
                {
                    if(_homeDir == null)
                    {
                        _homeDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    }

                    string relativePath = Path.GetFullPath(Path.Combine(_homeDir, expandedValue));
                    if (Directory.Exists(relativePath))
                    {
                        return relativePath;
                    }
                    if (File.Exists(relativePath))
                    {
                        return relativePath;
                    }
                }
                catch (Exception)
                { }

                return expandedValue;
            }
            catch (Exception)
            {
            }
            return pThis;
        }

    }
}
