﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.LaunchINI
{
    public class LiveModeSection : SectionINI
    {
        [Browsable(true)]
        [Category("[LiveMode]")]
        [TypeConverter(typeof(BooleanStringConverter))]
        public string CopyApp { get; set; }
    }
}
