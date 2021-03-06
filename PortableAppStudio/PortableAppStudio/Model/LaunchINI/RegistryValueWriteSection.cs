﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.LaunchINI
{
    public class RegistryValueWriteSection : KeyValueSection
    {
        [Browsable(true)]
        [Category("[RegistryValueWrite]")]
        public string RegKey
        {
            get
            {
                return this.IniKey;
            }
            set
            {
                this.IniKey = value;
            }
        }
        [Browsable(true)]
        [Category("[RegistryValueWrite]")]
        public string RegValue
        {
            get
            {
                return this.IniValue;
            }
            set
            {
                this.IniValue = value;
            }
        }

        [Browsable(false)]
        public override string KeyDisplayName { get { return "RegKey"; } }
        [Browsable(false)]
        public override string ValueDisplayName { get { return "RegValue"; } }
    }
}
