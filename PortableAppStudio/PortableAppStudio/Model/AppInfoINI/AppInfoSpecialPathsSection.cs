using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.AppInfoINI
{
    public class AppInfoSpecialPathsSection : SectionINI
    {
        [Browsable(true)]
        [Category("[SpecialPaths]")]
        [TypeConverter(typeof(AppPathListStringConverter))]
        public string Plugins { get; set; }

        public AppInfoSpecialPathsSection()
        {
            Plugins = "NONE";
        }
    }
}
