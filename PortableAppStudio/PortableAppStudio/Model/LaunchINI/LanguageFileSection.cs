using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.LaunchINI
{
    public class LanguageFileSection : SectionINI
    {

        [Browsable(true)]
        [Category("[LanguageFile]")]
        [TypeConverter(typeof(FileWriteNTypeConverter))]
        public string Type { get; set; }

        [Browsable(true)]
        [Category("[LanguageFile]")]
        [TypeConverter(typeof(EnvVariableListStringConverter))]
        public string File { get; set; }

        [Browsable(true)]
        [Category("[LanguageFile]")]
        public string Entry { get; set; }


        [Browsable(true)]
        [Category("[LanguageFile]")]
        public string Section { get; set; }


        [Browsable(true)]
        [Category("[LanguageFile]")]
        public string Key { get; set; }


        [Browsable(true)]
        [Category("[LanguageFile]")]
        public string Attribute { get; set; }


        [Browsable(true)]
        [Category("[LanguageFile]")]
        public string XPath { get; set; }

        [Browsable(true)]
        [Category("[LanguageFile]")]
        public bool? CaseSensitive { get; set; }


        [Browsable(true)]
        [Category("[LanguageFile]")]
        public string TrimRight { get; set; }
    }
}
