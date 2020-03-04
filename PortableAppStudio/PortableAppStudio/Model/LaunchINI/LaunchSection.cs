using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public INIValueList<WaitForEXENValue> WaitForEXEN { get; set; }

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
        public bool? CleanTemp { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        public bool? SinglePortableAppInstance { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        public bool? SingleAppInstance { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        public string CloseEXE { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        public int? SplashTime { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        public bool? LaunchAppAfterSplash { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        public bool? WaitForOtherInstances { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        public bool? HideCommandLineWindow { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        [TypeConverter(typeof(YeNoWarnConverter))]
        public string DirectoryMoveOK { get; set; }

        [Browsable(true)]
        [Category("[Launch]")]
        public bool? NoSpacesInPath { get; set; }

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
            WaitForEXEN = new Model.INIValueList<WaitForEXENValue>();
        }

        public void UpdateWaitForExeN(List<string> exeFileNames)
        {
            int startCount = WaitForEXEN.Count + 1;
            List<string> existingNames = WaitForEXEN.ConvertAll((item) => item.ExeName);
            foreach (var item in exeFileNames)
            {
                if(string.IsNullOrWhiteSpace(item))
                {
                    continue;
                }
                if (!existingNames.Exists((file) => string.Compare(file, item, true) == 0))
                {
                    var tempVal = new WaitForEXENValue(startCount,item);
                    WaitForEXEN.Add(tempVal);
                    startCount++;
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
                    else
                    {
                        WriteValue(section, file, item);
                    }
                }
            }
        }
    }
}
