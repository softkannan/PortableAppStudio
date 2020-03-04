using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.LaunchINI
{
    public class LanguageSection : SectionINI
    {
        [Browsable(true)]
        [Category("[Language]")]
        [TypeConverter(typeof(EnvVariableListStringConverter))]
        public string Base { get; set; }


        [Browsable(true)]
        [Category("[Language]")]
        [TypeConverter(typeof(EnvVariableListStringConverter))]
        public string Default { get; set; }


        [Browsable(true)]
        [Category("[Language]")]
        [TypeConverter(typeof(EnvVariableListStringConverter))]
        public string CheckIfExists { get; set; }


        [Browsable(true)]
        [Category("[Language]")]
        [TypeConverter(typeof(EnvVariableListStringConverter))]
        public string DefaultIfNotExists { get; set; }
    }
}
