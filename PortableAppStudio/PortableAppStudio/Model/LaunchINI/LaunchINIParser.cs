using PortableAppStudio.Controls;
using PortableAppStudio.INI;
using PortableAppStudio.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Model.LaunchINI
{
    partial class LaunchINI
    {
        public const string WaitForEXEN_Tag = "WaitForEXEN";
        public const string KillProcN_Tag = "KillProcN";
        private INIFile _iniFile = null;
        private string _debugFile = string.Empty;
        public string DebugFile
        {
            get { return _debugFile; }
        }
        public string LaunchFolder { get; private set; }



       

        public void Load(string launchFolder, string portableAppFolder)
        {
            LaunchFolder = launchFolder;
            bool isNewApp = false;
            var fileName = Directory.GetFiles(LaunchFolder, "*.ini").FirstOrDefault();
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = string.Format("{0}\\{1}.ini", launchFolder, Path.GetFileName(portableAppFolder).Replace(" ",""));
            }

            if (!File.Exists(fileName))
            {
                var sourcefile = PathManager.Init.GetResourcePath(PathManager.LAUNCH_FILE);
                File.Copy(sourcefile, fileName);
                isNewApp = true;
            }

            _debugFile = string.Format("{0}\\Debug.nsh", LaunchFolder);
            //if (File.Exists(DebugFile))
            //{
            //    try { File.Delete(DebugFile); } catch (Exception) { }
            //}

            if (File.Exists(fileName))
            {
                LoadInternal(fileName);
            }

            if(isNewApp)
            {
            }
        }

        private void LoadInternal(string fileName)
        {
            _iniFile = new INIFile(fileName);

            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(this))
            {
                if (item.IsBrowsable)
                {
                    switch (item.Name)
                    {
                        case "Launch":
                            Launch.LoadSection("Launch", _iniFile);
                            break;
                        case "Activate":
                            Activate.LoadSection("Activate", _iniFile);
                            break;
                        case "LiveMode":
                            LiveMode.LoadSection("LiveMode", _iniFile);
                            break;
                        case "Environment":
                            Environment.LoadSection("Environment", _iniFile);
                            break;
                        case "RegistryKeys":
                            RegistryKeys.LoadSection("RegistryKeys", _iniFile);
                            break;
                        case "RegistryValueWrite":
                            RegistryValueWrite.LoadSection("RegistryValueWrite", _iniFile);
                            break;
                        case "RegistryCleanupIfEmpty":
                            RegistryCleanupIfEmpty.LoadSection("RegistryCleanupIfEmpty", _iniFile);
                            break;
                        case "RegistryCleanupForce":
                            RegistryCleanupForce.LoadSection("RegistryCleanupForce", _iniFile);
                            break;
                        case "RegistryValueBackupDelete":
                            RegistryValueBackupDelete.LoadSection("RegistryValueBackupDelete", _iniFile);
                            break;
                        case "RegistrationFreeCOM":
                            RegistrationFreeCOM.LoadSection("RegistrationFreeCOM", _iniFile);
                            break;
                        case "QtKeysCleanup":
                            QtKeysCleanup.LoadSection("QtKeysCleanup", _iniFile);
                            break;
                        case "FileWriteN":
                            FileWriteN.LoadSection("FileWrite", _iniFile);
                            break;
                        case "FilesMove":
                            FilesMove.LoadSection("FilesMove", _iniFile);
                            break;
                        case "DirectoriesMove":
                            DirectoriesMove.LoadSection("DirectoriesMove", _iniFile);
                            break;
                        case "DirectoriesCleanupIfEmpty":
                            DirectoriesCleanupIfEmpty.LoadSection("DirectoriesCleanupIfEmpty", _iniFile);
                            break;
                        case "DirectoriesCleanupForce":
                            DirectoriesCleanupForce.LoadSection("DirectoriesCleanupForce", _iniFile);
                            break;
                        case "Language":
                            Language.LoadSection("Language", _iniFile);
                            break;
                        case "LanguageStrings":
                            LanguageStrings.LoadSection("LanguageStrings", _iniFile);
                            break;
                        case "LanguageFile":
                            LanguageFile.LoadSection("LanguageFile", _iniFile);
                            break;
                        case "DirectoriesLink":
                            DirectoriesLink.LoadSection("DirectoriesLink",_iniFile);
                            break;
                        case "PrefixPATHEnv":
                            PrefixPATHEnv.LoadSection("PrefixPATHEnv", _iniFile);
                            break;
                    }

                }
            }
        }

        public TreeNode BuildRegTreeUI()
        {
            TreeNode rootNode = new TreeNode();

            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(this))
            {
                if (item.IsBrowsable)
                {
                    switch (item.Name)
                    {
                        case "RegistryKeys":
                            {
                                var topNode = RegistryKeys.BuildTreeUI("RegistryKeys", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                        case "RegistryValueWrite":
                            {
                                var topNode = RegistryValueWrite.BuildTreeUI("RegistryValueWrite", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                        case "RegistryCleanupIfEmpty":
                            {
                                var topNode = RegistryCleanupIfEmpty.BuildTreeUI("RegistryCleanupIfEmpty", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                        case "RegistryCleanupForce":
                            {
                                var topNode = RegistryCleanupForce.BuildTreeUI("RegistryCleanupForce", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                        case "RegistryValueBackupDelete":
                            {
                                var topNode = RegistryValueBackupDelete.BuildTreeUI("RegistryValueBackupDelete", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                        case "FileWriteN":
                            {
                                var topNode = FileWriteN.BuildTreeUI("FileWriteN", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                        //case "RegistrationFreeCOM":
                        //    {
                        //        var topNode = RegistrationFreeCOM.BuildTreeUI("RegistrationFreeCOM", rootNode, item);
                        //        topNode.Expand();
                        //    }
                        //    break;
                        //case "Environment":
                        //    {
                        //        var topNode = Environment.BuildTreeUI("Environment", rootNode, item);
                        //        topNode.Expand();
                        //    }
                        //    break;
                        case "QtKeysCleanup":
                            {
                                var topNode = QtKeysCleanup.BuildTreeUI("QtKeysCleanup", rootNode, item);
                                topNode.Expand();
                            }
                            break;

                    }
                }
            }



            return rootNode;
        }

        public TreeNode BuildFileTreeUI()
        {
            TreeNode rootNode = new TreeNode();

            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(this))
            {
                if (item.IsBrowsable)
                {
                    switch (item.Name)
                    {
                        case "FilesMove":
                            {
                                var topNode = FilesMove.BuildTreeUI("FilesMove", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                        case "DirectoriesMove":
                            {
                                var topNode = DirectoriesMove.BuildTreeUI("DirectoriesMove", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                        case "DirectoriesCleanupIfEmpty":
                            {
                                var topNode = DirectoriesCleanupIfEmpty.BuildTreeUI("DirectoriesCleanupIfEmpty", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                        case "DirectoriesCleanupForce":
                            {
                                var topNode = DirectoriesCleanupForce.BuildTreeUI("DirectoriesCleanupForce", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                        case "DirectoriesLink":
                            {
                                var topNode = DirectoriesLink.BuildTreeUI("DirectoriesLink", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                        case "Environment":
                            {
                                var topNode = Environment.BuildTreeUI("Environment", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                        case "PrefixPATHEnv":
                            {
                                var topNode = PrefixPATHEnv.BuildTreeUI("PrefixPATHEnv", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                    }
                }
            }

            return rootNode;
        }

        public TreeNode BuildLaunchTreeUI()
        {
            TreeNode rootNode = new TreeNode();

            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(this))
            {
                if (item.IsBrowsable)
                {
                    switch (item.Name)
                    {
                        case "Launch":
                            {
                                var topNode = Launch.BuildTreeUI("Launch", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                        case "Activate":
                            {
                                var topNode = Activate.BuildTreeUI("Activate", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                        case "LiveMode":
                            {
                                var topNode = LiveMode.BuildTreeUI("LiveMode", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                        //case "Environment":
                        //    {
                        //        var topNode = Environment.BuildTreeUI("Environment", rootNode, item);
                        //        topNode.Expand();
                        //    }
                        //    break;
                        //case "QtKeysCleanup":
                        //    {
                        //        var topNode = QtKeysCleanup.BuildTreeUI("QtKeysCleanup", rootNode, item);
                        //        topNode.Expand();
                        //    }
                        //    break;
                        //case "FileWriteN":
                        //    {
                        //        TreeNode topNode = new TreeNode("FileWriteN");
                        //        foreach (var fileWriteSection in FileWriteN)
                        //        {
                        //            fileWriteSection.BuildTreeUI(fileWriteSection.SectionName, topNode, item);
                        //        }
                        //        rootNode.Nodes.Add(topNode);
                        //        topNode.Expand();
                        //    }
                        //    break;
                        case "Language":
                            {
                                var topNode = Language.BuildTreeUI("Language", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                        case "LanguageStrings":
                            {
                                var topNode = LanguageStrings.BuildTreeUI("LanguageStrings", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                        case "LanguageFile":
                            {
                                var topNode = LanguageFile.BuildTreeUI("LanguageFile", rootNode, item);
                                topNode.Expand();
                            }
                            break;
                    }
                }
            }

            return rootNode;
        }

        public void SaveNode(TreeNode node)
        {
            switch (node.Text.RemoveStartEnd('[',']'))
            {
                case "RegistryKeys":
                    RegistryKeys.SaveNode(node);
                    break;
                case "RegistryValueWrite":
                    RegistryValueWrite.SaveNode(node);
                    break;
                case "RegistryCleanupIfEmpty":
                    RegistryCleanupIfEmpty.SaveNode(node);
                    break;
                case "RegistryCleanupForce":
                    RegistryCleanupForce.SaveNode(node);
                    break;
                case "RegistryValueBackupDelete":
                    RegistryValueBackupDelete.SaveNode(node);
                    break;
                case "RegistrationFreeCOM":
                    RegistrationFreeCOM.SaveNode(node);
                    break;
                case "FilesMove":
                    FilesMove.SaveNode(node);
                    break;
                case "DirectoriesMove":
                    DirectoriesMove.SaveNode(node);
                    break;
                case "DirectoriesCleanupIfEmpty":
                    DirectoriesCleanupIfEmpty.SaveNode(node);
                    break;
                case "DirectoriesCleanupForce":
                    DirectoriesCleanupForce.SaveNode(node);
                    break;
                case "DirectoriesLink":
                    DirectoriesLink.SaveNode(node);
                    break;
                case "Environment":
                    Environment.SaveNode(node);
                    break;
                case "QtKeysCleanup":
                    QtKeysCleanup.SaveNode(node);
                    break;
                case "PrefixPATHEnv":
                    PrefixPATHEnv.SaveNode(node);
                    break;
                case "FileWriteN":
                    FileWriteN.SaveNode(node);
                    break;
            }
        }

        public void Save()
        {
            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(this))
            {
                if (item.IsBrowsable)
                {
                    switch (item.Name)
                    {
                        case "Launch":
                            Launch.SaveSection("Launch", _iniFile);
                            break;
                        case "Activate":
                            Activate.SaveSection("Activate", _iniFile);
                            break;
                        case "LiveMode":
                            LiveMode.SaveSection("LiveMode", _iniFile);
                            break;
                        case "Environment":
                            Environment.SaveSection("Environment", _iniFile);
                            break;
                        case "RegistryKeys":
                            RegistryKeys.SaveSection("RegistryKeys", _iniFile);
                            break;
                        case "RegistryValueWrite":
                            RegistryValueWrite.SaveSection("RegistryValueWrite", _iniFile);
                            break;
                        case "RegistryCleanupIfEmpty":
                            RegistryCleanupIfEmpty.SaveSection("RegistryCleanupIfEmpty", _iniFile);
                            break;
                        case "RegistryCleanupForce":
                            RegistryCleanupForce.SaveSection("RegistryCleanupForce", _iniFile);
                            break;
                        case "RegistryValueBackupDelete":
                            RegistryValueBackupDelete.SaveSection("RegistryValueBackupDelete", _iniFile);
                            break;
                        case "RegistrationFreeCOM":
                            RegistrationFreeCOM.SaveSection("RegistrationFreeCOM", _iniFile);
                            break;
                        case "QtKeysCleanup":
                            QtKeysCleanup.SaveSection("QtKeysCleanup", _iniFile);
                            break;
                        case "FileWriteN":
                            FileWriteN.SaveSection("FileWrite", _iniFile);
                            break;
                        case "FilesMove":
                            FilesMove.SaveSection("FilesMove", _iniFile);
                            break;
                        case "DirectoriesMove":
                            DirectoriesMove.SaveSection("DirectoriesMove", _iniFile);
                            break;
                        case "DirectoriesCleanupIfEmpty":
                            DirectoriesCleanupIfEmpty.SaveSection("DirectoriesCleanupIfEmpty", _iniFile);
                            break;
                        case "DirectoriesCleanupForce":
                            DirectoriesCleanupForce.SaveSection("DirectoriesCleanupForce", _iniFile);
                            break;
                        case "Language":
                            Language.SaveSection("Language", _iniFile);
                            break;
                        case "LanguageStrings":
                            LanguageStrings.SaveSection("LanguageStrings", _iniFile);
                            break;
                        case "LanguageFile":
                            LanguageFile.SaveSection("LanguageFile", _iniFile);
                            break;
                        case "DirectoriesLink":
                            DirectoriesLink.SaveSection("DirectoriesLink", _iniFile);
                            break;
                        case "PrefixPATHEnv":
                            PrefixPATHEnv.SaveSection("PrefixPATHEnv", _iniFile);
                            break;
                    }
                }
            }
        }

        public void GetSectionToEdit(string name, out object retObjValue,out Model.IINIList retListValue)
        {
            retObjValue = null;
            retListValue = null;

            switch (name)
            {
                case WaitForEXEN_Tag:
                    {
                        retListValue = Launch.WaitForEXEN;
                    }
                    break;
                case KillProcN_Tag:
                    {
                        retListValue = Launch.KillProcN;
                    }
                    break;
                case "[Launch]":
                    {
                        retObjValue = Launch;
                    }
                    break;
                case "[Activate]":
                    {
                        retObjValue = Activate;
                    }
                    break;
                case "[LiveMode]":
                    {
                        retObjValue = LiveMode;
                    }
                    break;
                case "[Environment]":
                    {
                        retListValue = Environment;
                    }
                    break;
                case "[QtKeysCleanup]":
                    {
                        retListValue = QtKeysCleanup;
                    }
                    break;
                case "[Language]":
                    {
                        retObjValue = Language;
                    }
                    break;
                case "[LanguageStrings]":
                    {
                        retListValue = LanguageStrings;
                    }
                    break;
                case "[LanguageFile]":
                    {
                        retObjValue = LanguageFile;
                    }
                    break;
                case "FileWriteN":
                    {
                        retListValue = FileWriteN;
                    }
                    break;
            }
        }

        public void Create()
        {
            Launch = new LaunchSection();
            Activate = new ActivateSection();
            LiveMode = new LiveModeSection();
            Environment = new INIValueList<EnvironmentSection>();
            RegistryKeys = new INIValueList<RegistryKeysSection>();
            RegistryValueWrite = new INIValueList<RegistryValueWriteSection>();
            RegistryCleanupIfEmpty = new INIValueList<RegistryCleanupIfEmptySection>();
            RegistryCleanupForce = new INIValueList<RegistryCleanupForceSection>();
            RegistryValueBackupDelete = new INIValueList<RegistryValueBackupDeleteSection>();
            RegistrationFreeCOM = new INIValueList<RegistrationFreeCOMSection>();

            QtKeysCleanup = new INIValueList<QtKeysCleanupSection>();

            FileWriteN = new FileWriteNSectionList();
            FilesMove = new INIValueList<FilesMoveSection>();

            DirectoriesMove = new INIValueList<DirectoriesMoveSection>();
            DirectoriesCleanupIfEmpty = new INIValueList<DirectoriesCleanupIfEmptySection>();

            DirectoriesCleanupForce = new INIValueList<DirectoriesCleanupForceSection>();

            Language = new LanguageSection();
            LanguageStrings = new INIValueList<LanguageStringsSection>();

            LanguageFile = new LanguageFileSection();
            DirectoriesLink = new INIValueList<DirectoriesLinkSection>();
            PrefixPATHEnv = new INIValueList<PrefixPATHEnvSection>();
        }
    }
}
