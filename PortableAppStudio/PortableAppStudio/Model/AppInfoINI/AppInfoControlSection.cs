using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PortableAppStudio.Model.AppInfoINI
{
    
    public class AppInfoControlSection : SectionINI
    {
        [Browsable(true)]
        [Category("[Control]")]
        public int? Icons { get; set; }

        [Browsable(true)]
        [Category("[Control]")]
        public string Start { get; set; }

        [Browsable(true)]
        [Category("[Control]")]
        [TypeConverter(typeof(ExeFileListStringConverter))]
        public string ExtractIcon { get; set; }
        public AppInfoControlSection()
        {
            Icons = 1;
            Start = "AppNamePortable.exe";
            ExtractIcon = @"";
        }
    }
}
