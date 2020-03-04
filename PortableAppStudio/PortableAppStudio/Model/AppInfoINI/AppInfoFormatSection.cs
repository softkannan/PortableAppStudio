using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.AppInfoINI
{
    public class AppInfoFormatSection : SectionINI
    {
        [Browsable(true)]                         
        [ReadOnly(true)]                          
        [Category("[Format]")]
        public string Type { get; set; }


        [Browsable(true)]                         
        [ReadOnly(true)]                         
        [Category("[Format]")]
        public string Version { get; set; }
        public AppInfoFormatSection()
        {
            Type = "PortableApps.comFormat";
            Version = "3.5";
        }
    }
}
