﻿using System;
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
        [TypeConverter(typeof(BooleanStringConverter))]
        public string Shareable { get; set; }

        [Browsable(true)]
        [Category("[License]")]
        [TypeConverter(typeof(BooleanStringConverter))]
        public string OpenSource { get; set; }

        [Browsable(true)]
        [Category("[License]")]
        [TypeConverter(typeof(BooleanStringConverter))]
        public string Freeware { get; set; }

        [Browsable(true)]
        [Category("[License]")]
        [TypeConverter(typeof(BooleanStringConverter))]
        public string CommercialUse { get; set; }

        [Browsable(true)]
        [Category("[License]")]
        public string EULAVersion { get; set; }
        public AppInfoLicenseSection()
        {
            Shareable = "true";
            OpenSource = "true";
            Freeware = "true";
            CommercialUse = "true";
            EULAVersion = "1";
        }
    }
}
