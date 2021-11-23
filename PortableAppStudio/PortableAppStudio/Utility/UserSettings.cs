using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PortableAppStudio.Utility
{
    public class UserSettings
    {
        private static UserSettings _inst = null;

        public static void LoadSettings()
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            var exeFolder = Path.GetDirectoryName(exePath);

            string filePath = string.Format("{0}\\{1}", exeFolder, "UserSettings.xml");
            if (File.Exists(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(UserSettings));
                // A FileStream is needed to read the XML document.
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    _inst = (UserSettings)serializer.Deserialize(fs);
                }
            }
            if (_inst == null)
            {
                _inst = new UserSettings();
            }
        }

        public static UserSettings Inst { get => _inst; }
        public UserSettings()
        {
            Environment.SetEnvironmentVariable("PSTUDIO_DIR", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            IsPortableAppRunning = false;
        }

        private string _launchGeneratorPath = null;
        public string LaunchGeneratorPath
        {
            get => _launchGeneratorPath; 
            set => _launchGeneratorPath = value.ResolveFullPath();
        }

        private string _notepadPath = null;
        public string NotepadPath
        {
            get => _notepadPath;
            set => _notepadPath = value.ResolveFullPath();
        }

        private string _diffToolPath = null;
        public string DiffToolPath
        {
            get => _diffToolPath;
            set => _diffToolPath = value.ResolveFullPath();
        }

        private string _nSISEditorPath = null;

        public string NSISEditorPath
        {
            get => _nSISEditorPath;
            set => _nSISEditorPath = value.ResolveFullPath();
        }

        private string _regJumpPath = null;

        public string RegJumpPath
        {
            get => _regJumpPath;
            set => _regJumpPath = value.ResolveFullPath();
        }

        private string _registryChangesView = null;

        public string RegistryChangesView 
        {
            get => _registryChangesView;
            set => _registryChangesView = value.ResolveFullPath();
        }

        private string _regFromApp = null;

        public string RegFromApp
        {
            get => _regFromApp;
            set => _regFromApp = value.ResolveFullPath();
        }


        private string _procExp = null;

        public string ProcExp 
        {
            get => _procExp;
            set => _procExp = value.ResolveFullPath();
        }

        private string _procMon = null;

        public string ProcMon
        {

            get => _procMon;
            set => _procMon = value.ResolveFullPath();
        }

        private string _resourceHacker = null;

        public string ResourceHacker 
        {
            get => _resourceHacker;
            set => _resourceHacker = value.ResolveFullPath();
        }

        private string _whatChanged = null;

        public string WhatChanged
        {
            get => _whatChanged;
            set => _whatChanged = value.ResolveFullPath();
        }

        private string _tcpview = null;

        public string Tcpview 
        {
            get => _tcpview;
            set => _tcpview = value.ResolveFullPath();
        }

        private string _fileActivityWatch = null;

        public string FileActivityWatch
        {
            get => _fileActivityWatch;
            set => _fileActivityWatch = value.ResolveFullPath();
        }

        private string _smartSniff = null;

        public string SmartSniff 
        {
            get => _smartSniff;
            set => _smartSniff = value.ResolveFullPath();
        }

        private string _exeInfo = null;

        public string ExeInfo
        {
            get => _exeInfo;
            set => _exeInfo = value.ResolveFullPath();
        }

        private string _iconsExtract = null;
        public string IconsExtract
        {
            get => _iconsExtract;
            set => _iconsExtract = value.ResolveFullPath();
        }

        private string _regEdit = null;

        public string RegEdit 
        {
            get => _regEdit;
            set => _regEdit = value.ResolveFullPath();
        }

        private string _fileManager = null;

        public string FileManager 
        {
            get => _fileManager;
            set => _fileManager = value.ResolveFullPath();
        }

        [XmlIgnore]
        public string PortableAppPath { get; set; }

        [XmlIgnore]
        public bool IsPortableAppRunning { get; set; }
    }
}
