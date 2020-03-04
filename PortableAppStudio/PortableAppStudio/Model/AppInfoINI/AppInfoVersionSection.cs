using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.AppInfoINI
{
    public class AppInfoVersionSection : SectionINI
    {
        [Browsable(true)]
        [Description("PackageVersion is the version of the package itself. This must be in 1.2.3.4 format with no other characters and must be incremented with each public release.")]
        [Category("[Version]")]
        public string PackageVersion { get; set; }

        [Browsable(true)]
        [Description("DisplayVersion is the user-friendly version that is generally used to describe the version. So, a released app may have a DisplayVersion of 2.4 Revision 2 but a PackageVersion of 2.4.0.2.")]
        [Category("[Version]")]
        public string DisplayVersion { get; set; }
        public AppInfoVersionSection()
        {
            PackageVersion = "1.2.0.1";
            DisplayVersion = "1.2 Release 1";
        }
    }
}
