using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.AppInfoINI
{
    public class AppInfoLicenseSection : SectionINI
    {
        [Browsable(true)]
        [Category("[License]")]
        public bool? Shareable { get; set; }

        [Browsable(true)]
        [Category("[License]")]
        public bool? OpenSource { get; set; }

        [Browsable(true)]
        [Category("[License]")]
        public bool? Freeware { get; set; }

        [Browsable(true)]
        [Category("[License]")]
        public bool? CommercialUse { get; set; }

        [Browsable(true)]
        [Category("[License]")]
        public int? EULAVersion { get; set; }
        public AppInfoLicenseSection()
        {
            Shareable = true;
            OpenSource = true;
            Freeware = true;
            CommercialUse = true;
            EULAVersion = 1;
        }
    }
}
