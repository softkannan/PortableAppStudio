using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.AppInfoINI
{
    public partial class AppInfoINI
    {
        public AppInfoINI()
        {
            Create();
        }
        public AppInfoFormatSection Format { get; set; }
        public AppInfoDetailsSection Details { get; set; }
        public AppInfoLicenseSection License { get; set; }
        public AppInfoVersionSection Version { get; set; }
        public AppInfoSpecialPathsSection SpecialPaths { get; set; }
        public AppInfoDependenciesSection Dependencies { get; set; }
        public AppInfoControlSection Control { get; set; }
    }
}
