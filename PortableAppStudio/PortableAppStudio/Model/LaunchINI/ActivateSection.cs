using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.LaunchINI
{
    public class ActivateSection : SectionINI
    {
        [Browsable(true)]
        [Category("[Activate]")]
        [DefaultValue(false)]
        public bool? Registry { get; set; }

        [Browsable(true)]
        [Category("[Activate]")]
        [TypeConverter(typeof(JavaStringConverter))]
        [DefaultValue("none")]
        public string Java { get; set; }

        [Browsable(true)]
        [Category("[Activate]")]
        [DefaultValue(false)]
        public bool? XML { get; set; }
    }
}
