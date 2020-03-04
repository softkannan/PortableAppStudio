using PortableAppStudio.Controls;
using PortableAppStudio.Dialogs;
using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Model.FolderLayout
{
    public class AppFolder : AppFolderBase, IDisposable
    {
        private string _appInfoFolder = string.Empty;
        private string _launcherFolder = string.Empty;
        private string _fileTypeIconsFolder = string.Empty;
        private string _defaultDataFolder = string.Empty;
        private string _defaultSettings = string.Empty;

        private string _appInfoFile = string.Empty;
        private string _installerFile = string.Empty;
        private string _launchFile = string.Empty;

        public bool IsGenerateRegFile { get; set; }

        public AppFolder()
        {
            Launch = new LaunchINI.LaunchINI();
            AppInfo = new AppInfoINI.AppInfoINI();
            Installer = new InstallerINI.InstallerINI();

            Files = new List<string>();
            IsGenerateManifest = false;
        }

        public LaunchINI.LaunchINI Launch { get; private set; }
        public AppInfoINI.AppInfoINI AppInfo { get; private set; }
        public InstallerINI.InstallerINI Installer { get; private set; }

        public List<string> Files { get; private set; }

        
        public void LoadFolder(string folderName, string portableAppFolder)
        {
            this.RootFolder = folderName;
            this.PortableAppFolder = portableAppFolder;

            _appInfoFolder = string.Format("{0}\\AppInfo", folderName);
            _defaultDataFolder = string.Format("{0}\\DefaultData", folderName);
            _defaultSettings = string.Format("{0}\\settings", _defaultDataFolder);

            _launcherFolder = string.Format("{0}\\Launcher", _appInfoFolder);
            _fileTypeIconsFolder = string.Format("{0}\\FileTypeIcons", _appInfoFile);

            if (!Directory.Exists(_defaultDataFolder))
            {
                Directory.CreateDirectory(_defaultDataFolder);
            }

            if (!Directory.Exists(_defaultSettings))
            {
                Directory.CreateDirectory(_defaultSettings);
            }

            if (!Directory.Exists(_appInfoFolder))
            {
                Directory.CreateDirectory(_appInfoFolder);
            }
            AppInfo.Load(_appInfoFolder, this.PortableAppFolder);

            if (!Directory.Exists(_launcherFolder))
            {
                Directory.CreateDirectory(_launcherFolder);
            }
            Launch.Load(_launcherFolder, this.PortableAppFolder);

            if (!Directory.Exists(_fileTypeIconsFolder))
            {
                Directory.CreateDirectory(_fileTypeIconsFolder);
            }
        }

        private void GenerateIcons()
        {

            if(!string.IsNullOrWhiteSpace(AppInfo.Control.ExtractIcon))
            {
                return;
            }

            string[] files = {
                "appicon.ico",
                "appicon_16.png",
                "appicon_32.png",
                "appicon_75.png",
                "appicon_128.png"
            };

            string exeFileName = string.Empty;

            foreach (var item in files)
            {
                string fileName = string.Format("{0}\\{1}", _appInfoFolder, item);
                if (!File.Exists(fileName))
                {
                    if (string.IsNullOrWhiteSpace(exeFileName))
                    {
                        if (!string.IsNullOrEmpty(Launch.Launch.ProgramExecutable))
                        {
                            exeFileName = string.Format("{0}\\App\\{1}", this.PortableAppFolder, Launch.Launch.ProgramExecutable);
                        }

                        if ((!File.Exists(exeFileName)) && !string.IsNullOrEmpty(Launch.Launch.ProgramExecutable64))
                        {
                            exeFileName = string.Format("{0}\\App\\{1}", this.PortableAppFolder, Launch.Launch.ProgramExecutable64);
                        }
                    }
                    string fileExt = Path.GetExtension(fileName);
                    switch(fileExt.ToLower())
                    {
                        case ".ico":
                            {
                                IconHelper.SaveAppIconGroup(exeFileName, fileName);
                                if(!File.Exists(fileName))
                                {
                                    AppInfo.Control.ExtractIcon = string.IsNullOrWhiteSpace(Launch.Launch.ProgramExecutable) ?
                                        Launch.Launch.ProgramExecutable64 : Launch.Launch.ProgramExecutable;
                                }
                            }
                            break;
                        case ".png":
                            //using (var bmp = iconImage.ToBitmap())
                            //{
                            //    using (FileStream fs = new FileStream(fileName, FileMode.Create))
                            //    {
                            //        bmp.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                            //    }
                            //}
                            break;
                    }
                }
            }
        }

        

        private void GenerateRegFile(TreeNode node)
        {
            foreach(TreeNode item in node.Nodes)
            {
                if (item.Tag is TreeNode)
                {
                    try
                    {
                        INIKeyValuePairBase tempVal = new INIKeyValuePairBase();
                        tempVal.Parse(item.Text);
                        string regFileName = string.Format("{0}\\{1}.reg", _defaultSettings, tempVal.IniKey);

                        string dirName = Path.GetDirectoryName(regFileName);
                        if (!Directory.Exists(dirName))
                        {
                            Directory.CreateDirectory(dirName);
                        }

                        if (File.Exists(regFileName))
                        {
                            try { File.Delete(regFileName); } catch (Exception) { }
                        }

                        using (var writer = new StreamWriter(regFileName, false, Encoding.Unicode))
                        {
                            Parser.RegFileExport parser = new Parser.RegFileExport();

                            parser.GenerateRegTextFile(item.Tag as TreeNode, writer);
                        }
                    }
                    catch(Exception ex)
                    {
                        ErrorLog.Inst.ShowError("Reg file generation error : {0}", ex.Message);
                    }
                }
            }
        }

        public void SaveTree(TreeViewEx srcFileTree, TreeViewEx srcRegTree, TreeViewEx destFileTree, TreeViewEx destRegTree, TreeViewEx appInfoTree, TreeViewEx launchTree)
        {
            
            foreach (TreeNode item in destFileTree.Nodes)
            {
                if(item.Text.StartsWith("["))
                {
                    Launch.SaveNode(item);
                }
                else if(item.Text == "App")
                {
                    foreach(TreeNode appNode in item.Nodes)
                    {
                        if(appNode.Text != "AppInfo")
                        {
                            SaveFileNode(appNode);
                        }
                    }
                }
            }
            List<string> listOfComFile = new List<string>();
            foreach(TreeNode item in destRegTree.Nodes)
            {
                Launch.SaveNode(item);

                if (item.Text == LaunchINI.LaunchINI.RegistryKeys_Tag && IsGenerateRegFile)
                {
                    GenerateRegFile(item);
                }
                else if(item.Text == LaunchINI.LaunchINI.RegistrationFreeCOM_Tag)
                {
                    foreach (TreeNode subItem in item.Nodes)
                    {
                        INIKeyValuePairBase tempVal = new INIKeyValuePairBase();
                        tempVal.FullValue = subItem.Text;
                        listOfComFile.Add(tempVal.IniValue);
                    }
                }

            }

            Launch.Save();
            AppInfo.Save();

            GenerateIcons();

            if(listOfComFile.Count > 0 && IsGenerateManifest)
            {
                List<string> tempAllFiles = new List<string>();
                tempAllFiles.AddRange(Directory.GetFiles(RootFolder, "*.dll", SearchOption.AllDirectories));
                tempAllFiles.AddRange(Directory.GetFiles(RootFolder, "*.ocx", SearchOption.AllDirectories));
                tempAllFiles.AddRange(Directory.GetFiles(RootFolder, "*.exe", SearchOption.AllDirectories));

                Model.COM.ComRegInfo.Inst.Clear();
                Model.COM.ComRegInfo.Inst.ParseComInfo(srcRegTree);
                Model.COM.ComRegInfo.Inst.UpdateTypeInfo(listOfComFile, tempAllFiles);
                GenerateManifestInternal(Model.ExeFileNameListStringConverter.ExeFileNameList.ToList(),
                    listOfComFile, tempAllFiles);
            }
        }

        public List<Model.FileInfo> GetComDllOcx(TreeNode topNode)
        {
            var retVal = new List<Model.FileInfo>();

            return retVal;
        }

        public bool IsGenerateManifest { get; set; }

        public void GenerateManifestInternal(List<string> exefileList, List<string> listOfComfiles,List<string> allFiles)
        {
            if (listOfComfiles.Count > 0)
            {
                List<string> verifiedCOMList = new List<string>();

                foreach (var fileName in listOfComfiles)
                {
                    if (!string.IsNullOrWhiteSpace(fileName))
                    {
                        string fullPath = allFiles.GetFilePath(fileName);
                        if (!string.IsNullOrWhiteSpace(fullPath) && File.Exists(fullPath))
                        {
                            verifiedCOMList.Add(fileName);
                        }
                    }
                }

                foreach (var fileName in exefileList)
                {
                    if (!string.IsNullOrWhiteSpace(fileName))
                    {
                        string fullPath = allFiles.GetFilePath(fileName);
                        if (string.IsNullOrWhiteSpace(fullPath))
                        {
                            Debugger.Break();
                        }
                        Model.COM.ComRegInfo.Inst.GenerateClientManifest(fullPath, verifiedCOMList);
                    }
                }

                foreach(var fileName in verifiedCOMList)
                {
                    if (!string.IsNullOrWhiteSpace(fileName))
                    {
                        string fullPath = allFiles.GetFilePath(fileName);
                        if(string.IsNullOrWhiteSpace(fullPath))
                        {
                            Debugger.Break();
                        }
                        Model.COM.ComRegInfo.Inst.GenerateServerManifest(fullPath);
                    }
                }
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    //_destFileContextMenu?.Items.Clear();
                    //_destFileContextMenu?.Dispose();
                    //_destRegContextMenu?.Items.Clear();
                    //_destRegContextMenu?.Dispose();
                    //_destRegFreeComContextMenu?.Items.Clear();
                    //_destRegFreeComContextMenu?.Dispose();
                    //_sourceContextMenu?.Items.Clear();
                    //_sourceContextMenu?.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~AppFolder() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
             GC.SuppressFinalize(this);
        }
        #endregion

    }
}
