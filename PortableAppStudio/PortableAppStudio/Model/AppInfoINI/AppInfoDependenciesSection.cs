using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.AppInfoINI
{
    public class AppInfoDependenciesSection : SectionINI
    {
        [Browsable(true)]
        [Category("[Dependencies]")]
        [TypeConverter(typeof(YesNoStringConverter))]
        public string UsesJava { get; set; }

        [Browsable(true)]
        [Category("[Dependencies]")]
        [TypeConverter(typeof(DotNetStringConverter))]
        public string UsesDotNetVersion { get; set; }

        [Browsable(true)]
        [Category("[Dependencies]")]
        [TypeConverter(typeof(YesNoStringConverter))]
        public string UsesGhostscript { get; set; }
        public AppInfoDependenciesSection()
        {
            UsesJava = "";
            UsesDotNetVersion = "";
            UsesGhostscript = "";
        }
    }
}
