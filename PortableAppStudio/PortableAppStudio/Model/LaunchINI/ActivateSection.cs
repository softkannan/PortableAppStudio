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
        [TypeConverter(typeof(GhostscriptStringConverter))]
        [DefaultValue("none")]
        public string Ghostscript { get; set; }

        [Browsable(true)]
        [Category("[Activate]")]
        [DefaultValue(false)]
        [TypeConverter(typeof(BooleanStringConverter))]
        public string Registry { get; set; }

        [Browsable(true)]
        [Category("[Activate]")]
        [TypeConverter(typeof(JavaStringConverter))]
        [DefaultValue("none")]
        public string Java { get; set; }

        [Browsable(true)]
        [Category("[Activate]")]
        [DefaultValue(false)]
        [TypeConverter(typeof(BooleanStringConverter))]
        public string XML { get; set; }
    }
}
