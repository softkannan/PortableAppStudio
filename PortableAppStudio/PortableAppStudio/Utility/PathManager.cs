using PortableAppStudio.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace PortableAppStudio.Utility
{
    public class PathManager
    {
        private static PathManager _inst = null;

        public static PathManager Init
        {
            get
            {
                if(_inst == null)
                {
                    _inst = new PathManager();
                }

                return _inst;
            }
        }

        public string GetLastPath(string lastKnownPath)
        {
            if (string.IsNullOrWhiteSpace(lastKnownPath))
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            }
            return lastKnownPath;
        }

        public Dictionary<string, Model.EnvironmentInfo> SystemEnvironments { get; private set; }
        public Dictionary<string, Model.EnvironmentInfo> AppEnvironments { get; private set; }
        public Dictionary<string,string> ToolTipMap { get; private set; }
        
        public Dictionary<string, string> EnvironmentVarToDirNameMap { get; private set; }

        public List<Model.FilePathEx> IgnoreFiles { get; private set; }
        public List<Model.FilePath> IgnoreFolders { get; private set; }
        public List<string> IgnoreRegKeys { get; private set; }

        public string ResourcePath { get; private set; }

        public PathManager()
        {
            SystemEnvironments = new Dictionary<string, Model.EnvironmentInfo>(StringComparer.InvariantCultureIgnoreCase);
            AppEnvironments = new Dictionary<string, Model.EnvironmentInfo>(StringComparer.InvariantCultureIgnoreCase);
            ToolTipMap = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            EnvironmentVarToDirNameMap = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            IgnoreFiles = new List<Model.FilePathEx>();
            IgnoreFolders = new List<Model.FilePath>();
            IgnoreRegKeys = new List<string>();

            ResourcePath = string.Format(@"{0}\Resource", Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

            SetEnvironments();
        }

        public bool IsGoodFile(string filePath)
        {
            bool retVal = true;

            for(int index=0; index < IgnoreFiles.Count; index++)
            {
                if(IgnoreFiles[index].ShortPath.IsMatch(filePath))
                {
                    retVal = false;
                    break;
                }
                else if (IgnoreFiles[index].LongPath.IsMatch(filePath))
                {
                    retVal = false;
                    break;
                }
            }

            if(retVal)
            {
                for (int index = 0; index < IgnoreFiles.Count; index++)
                {
                    if (IgnoreFolders[index].ShortPath.StartsWith(filePath))
                    {
                        retVal = false;
                        break;
                    }
                    else if (IgnoreFolders[index].LongPath.StartsWith(filePath))
                    {
                        retVal = false;
                        break;
                    }
                }
            }

            return retVal;
        }

        public const string APPENVIRONMENTS_FILE = "AppEnvironments.txt";
        public const string IGNOREFILES_FILE = "IgnoreFiles.txt";
        public const string IGNOREFOLDERS_FILE = "IgnoreFolders.txt";
        public const string IGNOREREGISTRYKEYS_FILE = "IgnoreRegistryKeys.txt";
        public const string SYSTEMENVIRONMENTS_x64_FILE = "SystemEnvironments_x64.txt";
        public const string SYSTEMENVIRONMENTS_x86_FILE = "SystemEnvironments_x86.txt";
        public const string APPINFO_FILE = @"appinfo.ini";
        public const string LAUNCH_FILE = @"launch.ini";
        public const string INSTALLER_FILE = @"installer.ini";
        public const string APPCOMPACTOR_FILE = "appcompactor.ini";
        public const string REGHIVE_FILE = "RegHive.dat";
        public const string ENVIRONMENTVARTODIRNAMEMAP_FILE = "EnvironmentVarToDirNameMap.txt";


        public string GetResourcePath(string resourceFilename)
        {
            return string.Format(@"{0}\\{1}", ResourcePath, resourceFilename);
        }

        public string GetExpandablePath(string filePath)
        {
            string retVal = filePath;

            foreach(KeyValuePair<string,Model.EnvironmentInfo> item in SystemEnvironments)
            {
                if(filePath.IndexOf(item.Value.ShortPath,StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    retVal = string.Format("{0}{1}", item.Key, filePath.Substring(item.Value.ShortPath.Length));
                    break;
                }
                else if (filePath.IndexOf(item.Value.LongPath, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    retVal = string.Format("{0}{1}", item.Key, filePath.Substring(item.Value.LongPath.Length));
                    break;
                }
            }

            return retVal;
        }

        public string GetHelpData(string fileName)
        {
            string helpFileName = string.Format("{0}\\Help\\{1}.rtf", ResourcePath, fileName);
            if(File.Exists(helpFileName))
            {
                return File.ReadAllText(helpFileName);
            }
            return "";
        }

        private void SetEnvironments()
        {
            {
                List<string> rawLines;
                if(Environment.Is64BitProcess)
                {
                    rawLines = FileUtility.Inst.GetFileLines(GetResourcePath(SYSTEMENVIRONMENTS_x64_FILE));
                }
                else
                {
                    rawLines = FileUtility.Inst.GetFileLines(GetResourcePath(SYSTEMENVIRONMENTS_x86_FILE));
                }

                foreach(var item in rawLines)
                {
                    int tipStartPos = item.IndexOf(',');
                    var envVariable = item.Substring(0, tipStartPos);
                    var nextSection = item.Substring(tipStartPos + 1);
                    tipStartPos = nextSection.IndexOf(',');
                    var envValue = nextSection.Substring(0, tipStartPos);
                    nextSection = nextSection.Substring(tipStartPos + 1);
                    tipStartPos = nextSection.IndexOf(',');
                    var displayName = nextSection.Substring(0, tipStartPos);
                    var envTips = nextSection.Substring(tipStartPos + 1);

                    ToolTipMap.Add(envVariable, envTips);

                    var filePath = new Model.EnvironmentInfo();
                    filePath.VarName = envVariable;
                    filePath.Tips = envTips;
                    filePath.DisplayName = displayName;
                    filePath.RelativePath = envValue;
                    filePath.ShortPath = envValue.GetAbsolutePath(true);
                    filePath.LongPath = envValue.GetAbsolutePath(false);

                    SystemEnvironments.Add(envVariable, filePath);
                }

            }

            {
                var rawLines = FileUtility.Inst.GetFileLines(GetResourcePath(APPENVIRONMENTS_FILE));

                foreach (var item in rawLines)
                {
                    int tipStartPos = item.IndexOf(',');

                    var envVariable = item.Substring(0, tipStartPos);
                    var envTips = item.Substring(tipStartPos);

                    ToolTipMap.Add(envVariable, envTips);

                    var filePath = new Model.EnvironmentInfo();
                    filePath.RelativePath = envVariable;
                    filePath.ShortPath = envVariable.GetAbsolutePath(true);
                    filePath.LongPath = envVariable.GetAbsolutePath(false);

                    AppEnvironments.Add(envVariable, filePath);
                }
            }

            {
                var rawLines = FileUtility.Inst.GetFileLines(GetResourcePath(IGNOREFOLDERS_FILE));

                foreach (var item in rawLines)
                {
                    var expandedValue = Environment.ExpandEnvironmentVariables(item);

                    var filePath = new Model.FilePath();
                    filePath.RelativePath = item;
                    filePath.ShortPath = expandedValue;
                    filePath.LongPath = string.Format(@"\\?\{0}", expandedValue);

                    IgnoreFolders.Add(filePath);
                }
            }

            {
                var rawLines = FileUtility.Inst.GetFileLines(GetResourcePath(IGNOREFILES_FILE));

                foreach (var item in rawLines)
                {
                    var expandedValue = Environment.ExpandEnvironmentVariables(item);

                    var filePath = new Model.FilePathEx();
                    filePath.RelativePath = item;
                    filePath.ShortPath = new Regex(expandedValue.Replace("\\", "\\\\"), 
                        RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled); 
                    var tempVal = string.Format(@"\\?\{0}", expandedValue);
                    filePath.LongPath = new Regex(tempVal.Replace("\\", "\\\\"), 
                        RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

                    IgnoreFiles.Add(filePath);
                }
            }

            IgnoreRegKeys = FileUtility.Inst.GetFileLines(GetResourcePath(IGNOREREGISTRYKEYS_FILE));
        }
    }
}
