using PortableAppStudio.Controls;
using PortableAppStudio.Dialogs;
using PortableAppStudio.Model.AppInfoINI;
using PortableAppStudio.Model.FolderLayout;
using PortableAppStudio.Model.LaunchINI;
using PortableAppStudio.Parser;
using PortableAppStudio.Properties;
using PortableAppStudio.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio
{
    partial class MainStudio
    {
        private void AddDataFolder(string folderPath)
        {
            string tempVal = folderPath.Trim();
            TreeNode firstNode = appFilesTree.Nodes.FindNode("Data");
            if (firstNode != null)
            {
                firstNode.CreateFolderNodes(folderPath);
            }
        }

        private void AddEnvironmentVar(string varName, string varValue)
        {
            TreeNode firstNode = appFilesTree.Nodes.FindNode(LaunchINI.Environment_Tag);
            if (firstNode != null)
            {
                firstNode.Nodes.Add(string.Format("{0}={1}", varName.Trim(), varValue.Trim()));
            }
        }

        private void AddPrefixPATHEnvVar(string varValue)
        {
            TreeNode firstNode = appFilesTree.Nodes.FindNode(LaunchINI.PrefixPATHEnv_Tag);
            if (firstNode != null)
            {
                var nodeIdx = firstNode.Nodes.Count + 1;
                firstNode.Nodes.Add(string.Format("Path{0}={1}", nodeIdx, varValue));
            }
        }

        private void CrossPlatformTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PortableApp.Inst.IsAlreadyOpen)
            {

                AddDataFolder(@"Home\Desktop");
                AddDataFolder(@"Home\Documents");
                AddDataFolder(@"Home\AppData\Local");
                AddDataFolder(@"Home\AppData\Roaming");
                AddDataFolder(@"Home\Videos");
                AddDataFolder(@"Home\Pictures");

                AddDataFolder(@"AppData");
                AddDataFolder(@"LocalAppData");
                AddDataFolder(@"ProgramData");
                AddDataFolder(@"Public");

                //AddEnvironmentVar("APPDATA", @"%PAL:DataDir%\AppData");
                //AddEnvironmentVar("LOCALAPPDATA", @"%PAL:DataDir%\LocalAppData");
                AddEnvironmentVar("USERPROFILE", @"%PAL:DataDir%\Home");
                AddEnvironmentVar("HOME", @"%PAL:DataDir%\Home");
                AddEnvironmentVar("HOMEDRIVE", @"%PAL:Drive%");
                AddEnvironmentVar("HOMEPATH", @"%PAL:PackagePartialDir%\Data\Home");
                AddEnvironmentVar("HOME_PATH", @"%PAL:DataDir%\Home");
                //AddEnvironmentVar("PROGRAMDATA", @"%PAL:DataDir%\ProgramData");
                //AddEnvironmentVar("ALLUSERSPROFILE", @"%PAL:DataDir%\ProgramData");
                //AddEnvironmentVar("PUBLIC", @"%PAL:DataDir%\Public");
            }
            else
            {
                ErrorLog.Inst.ShowError("please open portable application folder, before applying template");
            }
        }

        private void OpensourceTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PortableApp.Inst.IsAlreadyOpen)
            {
                AddDataFolder(@"Home\Desktop");
                AddDataFolder(@"Home\Documents");
                AddDataFolder(@"Home\AppData\Local");
                AddDataFolder(@"Home\AppData\Roaming");
                AddDataFolder(@"Home\Videos");
                AddDataFolder(@"Home\Pictures");

                AddDataFolder(@"AppData");
                AddDataFolder(@"LocalAppData");
                AddDataFolder(@"ProgramData");
                AddDataFolder(@"Public");

                //AddEnvironmentVar("APPDATA", @"%PAL:DataDir%\AppData");
                //AddEnvironmentVar("LOCALAPPDATA", @"%PAL:DataDir%\LocalAppData");
                AddEnvironmentVar("USERPROFILE", @"%PAL:DataDir%\Home");
                AddEnvironmentVar("HOME", @"%PAL:DataDir%\Home");
                AddEnvironmentVar("HOMEDRIVE", @"%PAL:Drive%");
                AddEnvironmentVar("HOMEPATH", @"%PAL:PackagePartialDir%\Data\Home");
                AddEnvironmentVar("HOME_PATH", @"%PAL:DataDir%\Home");
                //AddEnvironmentVar("PROGRAMDATA", @"%PAL:DataDir%\ProgramData");
                //AddEnvironmentVar("ALLUSERSPROFILE", @"%PAL:DataDir%\ProgramData");
                //AddEnvironmentVar("PUBLIC", @"%PAL:DataDir%\Public");
            }
            else
            {
                ErrorLog.Inst.ShowError("please open portable application folder, before applying template");
            }
        }

        private void QTFrameworkTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PortableApp.Inst.IsAlreadyOpen)
            {
                AddDataFolder(@"Home\Desktop");
                AddDataFolder(@"Home\Documents");
                AddDataFolder(@"Home\AppData\Local");
                AddDataFolder(@"Home\AppData\Roaming");
                AddDataFolder(@"Home\Videos");
                AddDataFolder(@"Home\Pictures");

                AddDataFolder(@"AppData");
                AddDataFolder(@"LocalAppData");
                AddDataFolder(@"ProgramData");
                AddDataFolder(@"Public");

                //AddEnvironmentVar("APPDATA", @"%PAL:DataDir%\AppData");
                //AddEnvironmentVar("LOCALAPPDATA", @"%PAL:DataDir%\LocalAppData");
                AddEnvironmentVar("USERPROFILE", @"%PAL:DataDir%\Home");
                AddEnvironmentVar("HOME", @"%PAL:DataDir%\Home");
                AddEnvironmentVar("HOMEDRIVE", @"%PAL:Drive%");
                AddEnvironmentVar("HOMEPATH", @"%PAL:PackagePartialDir%\Data\Home");
                AddEnvironmentVar("HOME_PATH", @"%PAL:DataDir%\Home");
                //AddEnvironmentVar("PROGRAMDATA", @"%PAL:DataDir%\ProgramData");
                //AddEnvironmentVar("ALLUSERSPROFILE", @"%PAL:DataDir%\ProgramData");
                //AddEnvironmentVar("ALLUSERSAPPDATA", @"%PAL:DataDir%\ProgramData");
                //AddEnvironmentVar("PUBLIC", @"%PAL:DataDir%\Public");
            }
            else
            {
                ErrorLog.Inst.ShowError("please open portable application folder, before applying template");
            }
        }

        private void PythonApplicationTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PortableApp.Inst.IsAlreadyOpen)
            {
                AddDataFolder(@"Home\Desktop");
                AddDataFolder(@"Home\Documents");
                AddDataFolder(@"Home\AppData\Local");
                AddDataFolder(@"Home\AppData\Roaming");
                AddDataFolder(@"Home\Videos");
                AddDataFolder(@"Home\Pictures");

                AddDataFolder(@"AppData");
                AddDataFolder(@"LocalAppData");
                AddDataFolder(@"ProgramData");
                AddDataFolder(@"Public");

                //AddEnvironmentVar("APPDATA", @"%PAL:DataDir%\AppData");
                //AddEnvironmentVar("LOCALAPPDATA", @"%PAL:DataDir%\LocalAppData");
                AddEnvironmentVar("USERPROFILE", @"%PAL:DataDir%\Home");
                AddEnvironmentVar("HOME", @"%PAL:DataDir%\Home");
                AddEnvironmentVar("HOMEDRIVE", @"%PAL:Drive%");
                AddEnvironmentVar("HOMEPATH", @"%PAL:PackagePartialDir%\Data\Home");
                AddEnvironmentVar("HOME_PATH", @"%PAL:DataDir%\Home");
                //AddEnvironmentVar("PROGRAMDATA", @"%PAL:DataDir%\ProgramData");
                //AddEnvironmentVar("ALLUSERSPROFILE", @"%PAL:DataDir%\ProgramData");
                //AddEnvironmentVar("ALLUSERSAPPDATA", @"%PAL:DataDir%\ProgramData");
                //AddEnvironmentVar("PUBLIC", @"%PAL:DataDir%\Public");

                AddEnvironmentVar("PYTHONPATH", @"%PAL:AppDir%\Python\Lib;%PAL:AppDir%\Python\DLLs;%PAL:AppDir%\Python\Lib\lib-tk");
                AddPrefixPATHEnvVar(@"%PAL:AppDir%\Python");
                AddPrefixPATHEnvVar(@"%PAL:AppDir%\Python\Scripts");
            }
            else
            {
                ErrorLog.Inst.ShowError("please open portable application folder, before applying template");
            }
        }

        private void JAVAApplicationTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PortableApp.Inst.IsAlreadyOpen)
            {
                AddDataFolder(@"Home\Desktop");
                AddDataFolder(@"Home\Documents");
                AddDataFolder(@"Home\AppData\Local");
                AddDataFolder(@"Home\AppData\Roaming");
                AddDataFolder(@"Home\Videos");
                AddDataFolder(@"Home\Pictures");

                AddDataFolder(@"AppData");
                AddDataFolder(@"LocalAppData");
                AddDataFolder(@"ProgramData");
                AddDataFolder(@"Public");

                //AddEnvironmentVar("APPDATA", @"%PAL:DataDir%\AppData");
                //AddEnvironmentVar("LOCALAPPDATA", @"%PAL:DataDir%\LocalAppData");
                AddEnvironmentVar("USERPROFILE", @"%PAL:DataDir%\Home");
                AddEnvironmentVar("HOME", @"%PAL:DataDir%\Home");
                AddEnvironmentVar("HOMEDRIVE", @"%PAL:Drive%");
                AddEnvironmentVar("HOMEPATH", @"%PAL:PackagePartialDir%\Data\Home");
                AddEnvironmentVar("HOME_PATH", @"%PAL:DataDir%\Home");
                //AddEnvironmentVar("PROGRAMDATA", @"%PAL:DataDir%\ProgramData");
                //AddEnvironmentVar("ALLUSERSPROFILE", @"%PAL:DataDir%\ProgramData");
                //AddEnvironmentVar("ALLUSERSAPPDATA", @"%PAL:DataDir%\ProgramData");
                //AddEnvironmentVar("PUBLIC", @"%PAL:DataDir%\Public");

                //`$JAVA_TOOL_OPTIONS, $_JAVA_OPTIONS` set these values to manipulate the JAVA vm parameters `$_JAVA_OPTIONS = '-Duser.home=%PAL:DataDir%\Home' '-Duser.dir=%PAL:DataDir%\Home'`

                AddEnvironmentVar("JAVA_TOOL_OPTIONS", @"-Duser.home='%PAL:DataDir%\Home' -Duser.dir='%PAL:DataDir%\Home'");
                AddEnvironmentVar("_JAVA_OPTIONS", @"-Duser.home='%PAL:DataDir%\Home' -Duser.dir='%PAL:DataDir%\Home'");
                AddEnvironmentVar("JAVAHOME", @"%PAL:AppDir%\OpenJDK");
                AddEnvironmentVar("JAVA_HOME", @"%PAL:AppDir%\OpenJDK");
                AddPrefixPATHEnvVar(@"%PAL:AppDir%\OpenJDK\bin");
            }
            else
            {
                ErrorLog.Inst.ShowError("please open portable application folder, before applying template");
            }
        }
    }
}
