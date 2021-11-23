using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Parser
{
    public class AppVParser : RegHiveParser
    {

        public AppVParser() : base()
        {
            RelativePathMap = new Dictionary<string, string> {
            {"AppData","%APPDATA%" },
            {"Common AppData","%PROGRAMDATA%"},
            {"Local AppData","%LOCALAPPDATA%"},
            {"LocalAppDataLow","%LOCALLOWAPPDATA%"},
            {"Profile","%USERPROFILE%"},
            {"System","%WINDIR%\\System32"},
            {"Fonts","%WINDIR%\\Fonts"},
            {"Windows","%WINDIR%"},
            {"AppVPackageDrive" ,"%SystemDrive%"},
            {"AppVSystem32Spool" ,"%WINDIR%\\System32\\Spool"},
            {"Common Documents","%PUBLIC%\\Documents"},
            {"UserProfiles","%USERPROFILE%" },
            {"AppVAllUsersDir","%ALLUSERSPROFILE%" },
            {"Public","%PUBLIC%" },
            };
            if (Environment.Is64BitProcess)
            {
                RelativePathMap.Add("ProgramFilesCommonX64", "%CommonProgramW6432%");
                RelativePathMap.Add("ProgramFilesCommonX86","%COMMONPROGRAMFILES(x86)%");
                RelativePathMap.Add("ProgramFilesX64", "%ProgramW6432%");
                RelativePathMap.Add("ProgramFilesX86","%PROGRAMFILES(X86)%");
                RelativePathMap.Add("SystemX86", "%WINDIR%\\SysWOW64");
            }
            else
            {
                RelativePathMap.Add("ProgramFilesCommon", "%COMMONPROGRAMFILES%");
                RelativePathMap.Add("ProgramFiles", "%PROGRAMFILES%");
            }
        }

        public override void Parse(string fileName)
        {
            this.ParserType = RegSourceType.AppV;
            SearchReplaceList = Model.InteliSense.Inst.UpdateSearchReplaceList("AppVIntellisense.txt");
            Model.InteliSense.Inst.UpdateEnvironmentList();
            ParseInternal(fileName);
        }

        private void ParseInternal(string folderName)
        {
            string regBinFile = string.Format("{0}\\Registry.dat", folderName);

            string fileRootFolder = string.Format("{0}\\Root\\VFS", folderName);

            ParseAppVBinaryRegValues(regBinFile);

            ParseFiles(fileRootFolder);
        }

        
    }
}
