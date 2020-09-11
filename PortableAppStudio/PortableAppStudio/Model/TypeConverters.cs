using Microsoft.Win32;
using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model
{
    public class YesNoStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<String> list = new List<String>();
            list.Add("");
            list.Add("no");
            list.Add("yes");
            return new StandardValuesCollection(list);
        }
    }
    public class DotNetStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return false; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if (_list == null)
            {
                _list = new List<String>();
                _list.Add("");
                _list.Add("1.1");
                _list.Add("2.0");
                _list.Add("3.0");
                _list.Add("3.5");
                _list.Add("4.0");
                _list.Add("4.5");
                _list.Add("4.6");

                RegistryKey installed_versions = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP");
                string[] version_names = installed_versions.GetSubKeyNames();

                foreach (var item in version_names)
                {
                    _list.Add(item.Remove(0, 1));
                }
            }
            return new StandardValuesCollection(_list);
        }

        List<String> _list = null;
    }
    public class EnvVariableListStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return false; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(_list);
        }

        static EnvVariableListStringConverter()
        {
            _list.Add("");
            string filename = PathManager.Init.GetResourcePath("EnvIntellisense.txt");
            if (File.Exists(filename))
            {
                _list.AddRange(File.ReadLines(filename));
            }
        }

        private static List<string> _list = new List<string>();
    }

    public class OtherFileListStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return false; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(_list);
        }

        private static List<string> _list = new List<string>();

        public static List<string> OtherList { get { return _list; } }
    }
    public class ExeFileListStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(_list);
        }

        private static List<string> _list = new List<string>();

        public static List<string> ExeList { get { return _list; } }
    }

    public class DllOcxFileListStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(_list);
        }

        private static List<string> _list = new List<string>();

        public static List<string> DllOcxList { get { return _list; } }
    }

    public class AppPathListStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return false; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(_list);
        }

        private static List<string> _list = new List<string>();

        public static List<string> AppPath { get { return _list; } }
    }

    public class FirewallListStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return false; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(_list);
        }

        private static List<string> _list = new List<string>();

        public static List<string> FirewallList { get { return _list; } }

        public static List<string> GetRegistryWriteList()
        {
            List<string> retVal = new List<string>();

            //HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SharedAccess\Parameters\FirewallPolicy\FirewallRules
            string regKey = @"HKLM\SYSTEM\CurrentControlSet\Services\SharedAccess\Parameters\FirewallPolicy\FirewallRules";

            foreach(var item in _list)
            {
                if(!string.IsNullOrWhiteSpace(item))
                {
                    string newGuid = Guid.NewGuid().ToString().ToUpper();
                    string exePath = item.Trim();
                    string exeName = Path.GetFileName(item.Trim());
                    var regItem = string.Format("{0}\\{{{1}}}=REG_SZ:v2.28|Action=Block|Active=TRUE|Dir=Out|App={2}|Name=Blocked By PortableApps {3}|", regKey,newGuid, exePath, exeName);
                    retVal.Add(regItem);
                }
            }
            //@"HKLM\Software\AppName\Key\Value2=REG_SZ:%PAL:DataDir%"
            return retVal;
        }
    }

    public class COMComponentsListStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return false; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(_list);
        }

        private static List<string> _list = new List<string>();

        public static List<string> COMComponentsList { get { return _list; } }

        public static List<string> GetRegistryWriteList()
        {
            List<string> retVal = new List<string>();

            //HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SharedAccess\Parameters\FirewallPolicy\FirewallRules
            string regKey = @"HKLM\SYSTEM\CurrentControlSet\Services\SharedAccess\Parameters\FirewallPolicy\FirewallRules";

            foreach (var item in _list)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    string newGuid = Guid.NewGuid().ToString().ToUpper();
                    string exePath = item.Trim();
                    string exeName = Path.GetFileName(item.Trim());
                    var regItem = string.Format("{0}\\{{{1}}}=REG_SZ:v2.28|Action=Block|Active=TRUE|Dir=Out|App={2}|Name=Blocked By PortableApps {3}|", regKey, newGuid, exePath, exeName);
                    retVal.Add(regItem);
                }
            }
            //@"HKLM\Software\AppName\Key\Value2=REG_SZ:%PAL:DataDir%"
            return retVal;
        }
    }

    public class ExeFileNameListStringConverter : StringConverter
    {
        public override Boolean GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override Boolean GetStandardValuesExclusive(ITypeDescriptorContext context) { return false; }
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(_list);
        }

        private static List<string> _list = new List<string>();

        public static List<string> ExeFileNameList { get { return _list; } }
    }
}
