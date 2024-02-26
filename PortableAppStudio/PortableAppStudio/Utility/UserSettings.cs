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
            string toolsBase = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (toolsBase.EndsWith("net8.0-windows") || toolsBase.EndsWith("Debug"))
            {
                var tempPath = string.Format(@"{0}\..\..\..\", toolsBase);
                toolsBase = tempPath.ResolveFullPath().Trim('\\');
            }
            Environment.SetEnvironmentVariable("TOOLSBASE_DIR", toolsBase);
            Environment.SetEnvironmentVariable("PSTUDIO_DIR", exePath);
            IsPortableAppRunning = false;
        }

        public Tool LaunchGeneratorPath { get; set; }
        public Tool NotepadPath { get; set; }
        public Tool DiffToolPath { get; set; }
        public Tool NSISEditorPath { get; set; }
        public Tool RegJumpPath { get; set;}
        public Tool RegistryChangesView { get;set; }
        public Tool RegFromApp { get; set; }
        public Tool ProcExp { get; set; }
        public Tool ProcMon { get; set; }
        public Tool ResourceHacker { get; set; }
        public Tool WhatChanged { get; set; }
        public Tool Tcpview { get; set; }
        public Tool FileActivityWatch { get; set; }
        public Tool SmartSniff { get; set; }
        public Tool ExeInfo { get; set; }
        public Tool IconsExtract { get; set; }
        public Tool RegEdit { get; set; }
        public Tool FileManager { get; set; }
        public Tool RegJump { get; set; }

        [XmlIgnore]
        public string PortableAppPath { get; set; }

        [XmlIgnore]
        public bool IsPortableAppRunning { get; set; }
    }
}
