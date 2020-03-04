using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.LaunchINI
{
    [DisplayName("[FileWriteN]")]
    [Description("For writing data to files. The values which must be set depend on the Type specified below.")]
    public class FileWriteNSection : SectionINI,IINIKeyValuePair
    {
        [Browsable(true)]
        [Category("[FileWriteN]")]
        [Description("Mandatory. Environment variable substitions apply.")]
        [TypeConverter(typeof(OtherFileListStringConverter))]
        public string File
        {
            get
            {
                return IniValue;
            }
            set
            {
                IniValue = value;
            }
        }

        [Browsable(true)]
        [Category("[FileWriteN]")]
        [Description("Values: ConfigWrite, INI, Replace, XML attribute, XML text Mandatory.")]
        [TypeConverter(typeof(FileWriteNTypeConverter))]
        public string Type { get; set; }

        [Browsable(true)]
        [Category("[FileWriteN]")]
        [Description("Mandatory for Type=ConfigWrite. The Value will be written to a line starting with this value, or if it is not found, at the end of the file. ")]
        public string Entry { get; set; }

        [Browsable(true)]
        [Category("[FileWriteN]")]
        [Description("Mandatory for Type=INI. The INI section to write the value to.")]
        public string Section { get; set; }

        [Browsable(true)]
        [Category("[FileWriteN]")]
        [Description("Mandatory for Type=INI. Mandatory for Type=INI.")]
        public string Key { get; set; }

        [Browsable(true)]
        [Category("[FileWriteN]")]
        [Description("Mandatory for Type=ConfigWrite, INI, XML attribute, XML text. Environment variable substitions apply.\nThe value which will be written to the file. If dealing with Type=ConfigWrite, you should remember with things like XML files that you will normally need to close the tag, for example %PAL:DataDir%\\settings</config>. In such cases you can also try using the inbuilt XML support.")]
        [TypeConverter(typeof(EnvVariableListStringConverter))]
        public string Value { get; set; }

        [Browsable(true)]
        [Category("[FileWriteN]")]
        [Description("Mandatory for Type=Replace. Environment variable substitions apply. The string to search for.")]
        [TypeConverter(typeof(EnvVariableListStringConverter))]
        public string Find { get; set; }

        [Browsable(true)]
        [Category("[FileWriteN]")]
        [Description("Mandatory for Type=Replace.\nEnvironment variable substitions apply.\nThe string to replace the search string with.If, after environment variable replacement, this is the same as the Find string, the replacement will be skipped(e.g. if you use it to update drive letters and it’s on the same letter).")]
        [TypeConverter(typeof(EnvVariableListStringConverter))]
        public string Replace { get; set; }

        [Browsable(true)]
        [Category("[FileWriteN]")]
        [Description("Mandatory for Type=XML attribute Environment variable substitions apply. The attribute which will be set inside the element identified by the given XPath.See Dealing with XML data for more details.")]
        [TypeConverter(typeof(EnvVariableListStringConverter))]
        public string Attribute { get; set; }

        [Browsable(true)]
        [Category("[FileWriteN]")]
        [Description("Mandatory for Type=XML attribute, XML text.\nSpecify the XPath to find the place to write to.It is a good idea to make sure that you have a solid understanding of how XPaths work and how to use them before writing one.\nFor information about what this should look like, see Dealing with XML data.")]
        public string XPath { get; set; }

        [Browsable(true)]
        [Category("[FileWriteN]")]
        [Description("Values: true / false, Default: false, Applies for Type = ConfigWrite, Replace.,Optional.")]
        public bool? CaseSensitive { get; set; }

        [Browsable(true)]
        [Category("[FileWriteN]")]
        [Description("Values: auto / ANSI / UTF-16LE, Default: auto, Applies to Type = Replace., Optional.")]
        [TypeConverter(typeof(EncodingConverter))]
        public string Encoding { get; set; }

        [Browsable(false)]
        public string KeyDisplayName { get { return "SectionName"; } }
        [Browsable(false)]
        public string ValueDisplayName { get { return "FileName"; } }
        [Browsable(false)]
        public string IniKey
        {
            get
            {
                return SectionName;
            }
            set
            {
                SectionName = value;
            }
        }
        [Browsable(false)]
        public string IniValue { get; set; }
        [Browsable(false)]
        public string FullValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public FileWriteNSection():base()
        {
            this.IniValue = "<New File>";
        }
        public FileWriteNSection( string sectionName) : base(sectionName)
        {
            this.IniValue = "<New File>";
        }

        public void UpdateIndex(int index)
        {
            IniKey = string.Format("FileWrite{0}", index);
        }

        //public override string ToString()
        //{
        //    if(string.IsNullOrWhiteSpace(File))
        //    {
        //        return "{Enter File Name}";
        //    }
        //    return File;
        //}
    }
}
