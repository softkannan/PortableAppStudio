using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PortableAppStudio.INI;

namespace PortableAppStudio.Model.LaunchINI
{
    public class LaunchSection : SectionINI
    {
        private const int MaxWaitExeCount = 200;

        [Browsable(true)]
        [TypeConverter(typeof(ExeFileListStringConverter))]
        [Category("[Launch]")]
        public string ProgramExecutable { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(ExeFileListStringConverter))]
        public string ProgramExecutable64 { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(RunAsAdminStringConverter))]
        public string RunAsAdmin { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(EnvVariableListStringConverter))]
        public string WorkingDirectory { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        public bool? WaitForProgram { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [Editor(typeof(WaitForExeListEditor), typeof(UITypeEditor))]
        public ExeProcessList<WaitForEXENValue> WaitForEXEN { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [Editor(typeof(KillProcListEditor), typeof(UITypeEditor))]
        public ExeProcessList<KillProcNValue> KillProcN { get; set; }

        [Browsable(true)]
        [TypeConverter(typeof(ExeFileListStringConverter))]
        [Category("[Launch]")]
        public string ProgramExecutableWhenParameters { get; set; }

        [Browsable(true)]
        [TypeConverter(typeof(ExeFileListStringConverter))]
        [Category("[Launch]")]
        public string ProgramExecutableWhenParameters64 { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(EnvVariableListStringConverter))]
        public string CommandLineArguments { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(OSSelectionStringConverter))]
        public string MinOS { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(OSSelectionStringConverter))]
        public string MaxOS { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(RunAsAdminStringConverter))]
        public string RunAsAdmin2000 { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(RunAsAdminStringConverter))]
        public string RunAsAdminXP { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(RunAsAdminStringConverter))]
        public string RunAsAdmin2003 { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(RunAsAdminStringConverter))]
        public string RunAsAdminVista { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(RunAsAdminStringConverter))]
        public string RunAsAdmin2008 { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(RunAsAdminStringConverter))]
        public string RunAsAdmin7 { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(RunAsAdminStringConverter))]
        public string RunAsAdmin2008R2 { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(BooleanStringConverter))]
        public string CleanTemp { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(BooleanStringConverter))]
        public string SinglePortableAppInstance { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(BooleanStringConverter))]
        public string SingleAppInstance { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        public string CloseEXE { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        public int? SplashTime { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(BooleanStringConverter))]
        public string LaunchAppAfterSplash { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(BooleanStringConverter))]
        public string WaitForOtherInstances { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(BooleanStringConverter))]
        public string HideCommandLineWindow { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(YeNoWarnConverter))]
        public string DirectoryMoveOK { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(BooleanStringConverter))]
        public string NoSpacesInPath { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(YeNoWarnConverter))]
        public string SupportsUNC { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(RefreshShellIconsStringConverter))]
        public string RefreshShellIcons { get; set; }

        public LaunchSection()
        {
            WaitForEXEN = new ExeProcessList<WaitForEXENValue>();
            KillProcN = new ExeProcessList<KillProcNValue>();
        }

        public void UpdateWaitForExeList(List<string> exeFileNames, bool forceUpdate = false, TreeNode topNode = null)
        {
            if (WaitForEXEN.Count == 0 || forceUpdate)
            {
                int startCount = WaitForEXEN.Count + 1;
                List<string> existingNames = WaitForEXEN.ConvertAll((item) => item.ExeName);
                foreach (var item in exeFileNames)
                {
                    if (string.IsNullOrWhiteSpace(item))
                    {
                        continue;
                    }
                    if (!existingNames.Exists((file) => string.Compare(file, item, true) == 0))
                    {
                        var tempVal = new WaitForEXENValue(startCount, item);
                        WaitForEXEN.Add(tempVal);
                        startCount++;
                    }
                }

                if (topNode != null)
                {
                    topNode.Nodes.Clear();
                    foreach (var listItem in WaitForEXEN)
                    {
                        topNode.Nodes.Add(listItem.ToString());
                    }
                }
            }
        }

        public void UpdateKillProcList(List<string> exeFileNames, bool forceUpdate = false, TreeNode topNode = null)
        {
            if (KillProcN.Count == 0 || forceUpdate)
            {
                int startCount = KillProcN.Count + 1;
                List<string> existingNames = KillProcN.ConvertAll((item) => item.ExeName);
                foreach (var item in exeFileNames)
                {
                    if (string.IsNullOrWhiteSpace(item))
                    {
                        continue;
                    }
                    if (!existingNames.Exists((file) => string.Compare(file, item, true) == 0))
                    {
                        var tempVal = new KillProcNValue(startCount, item);
                        KillProcN.Add(tempVal);
                        startCount++;
                    }
                }

                if (topNode != null)
                {
                    topNode.Nodes.Clear();
                    foreach (var listItem in KillProcN)
                    {
                        topNode.Nodes.Add(listItem.ToString());
                    }
                }
            }
        }

        public override void LoadSection(string section, INIFile file)
        {
            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(this))
            {
                if (item.IsBrowsable)
                {
                    if (item.Name == "WaitForEXEN")
                    {
                        for(int index=1;index < MaxWaitExeCount;index++)
                        {
                            var key = string.Format("WaitForEXE{0}", index);
                            var iniData = file.ReadValue(section, key, null);
                            if(string.IsNullOrWhiteSpace(iniData))
                            {
                                break;
                            }
                            WaitForEXEN.Add(new WaitForEXENValue(index,iniData));
                        }
                    }
                    else if (item.Name == "KillProcN")
                    {
                        for (int index = 1; index < MaxWaitExeCount; index++)
                        {
                            var key = string.Format("KillProc{0}", index);
                            var iniData = file.ReadValue(section, key, null);
                            if (string.IsNullOrWhiteSpace(iniData))
                            {
                                break;
                            }
                            KillProcN.Add(new KillProcNValue(index, iniData));
                        }
                    }
                    else
                    {
                        ReadValue(section, file, item);
                    }
                }
            }
        }

        public override void SaveSection(string section, INIFile file)
        {
            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(this))
            {
                if (item.IsBrowsable)
                {
                    if (item.Name == "WaitForEXEN")
                    {
                        for (int index = 1; index < MaxWaitExeCount; index++)
                        {
                            var key = string.Format("WaitForEXE{0}", index);
                            var iniData = file.ReadValue(section, key, null);
                            if (string.IsNullOrWhiteSpace(iniData))
                            {
                                break;
                            }
                            file.DeleteKey(section,key);
                        }

                        for (int index = 0; index < WaitForEXEN.Count; index++)
                        {
                            var key = string.Format("WaitForEXE{0}", index + 1);
                            file.WriteValue(section, key, WaitForEXEN[index].ExeName);
                        }
                    }
                    else if (item.Name == "KillProcN")
                    {
                        for (int index = 1; index < MaxWaitExeCount; index++)
                        {
                            var key = string.Format("KillProc{0}", index);
                            var iniData = file.ReadValue(section, key, null);
                            if (string.IsNullOrWhiteSpace(iniData))
                            {
                                break;
                            }
                            file.DeleteKey(section, key);
                        }

                        for (int index = 0; index < WaitForEXEN.Count; index++)
                        {
                            var key = string.Format("KillProc{0}", index + 1);
                            file.WriteValue(section, key, WaitForEXEN[index].ExeName);
                        }
                    }
                    else
                    {
                        WriteValue(section, file, item);
                    }
                }
            }
        }
    }
}
