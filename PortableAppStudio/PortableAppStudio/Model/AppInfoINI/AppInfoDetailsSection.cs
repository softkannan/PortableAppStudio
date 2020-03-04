using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.AppInfoINI
{
    public class AppInfoDetailsSection : SectionINI
    {
        [Browsable(true)]
        [Category("[Details]")]
        public string Name { get; set; }

        [Browsable(true)]
        [Category("[Details]")]
        public string AppId { get; set; }

        [Browsable(true)]
        [Category("[Details]")]
        public string Publisher { get; set; }

        [Browsable(true)]
        [Category("[Details]")]
        public string Homepage { get; set; }


        [Browsable(true)]
        [Category("[Details]")]
        [TypeConverter(typeof(CategoryStringConverter))]
        public string Category { get; set; }

        [Browsable(true)]
        [Category("[Details]")]
        public string Description { get; set; }

        [Browsable(true)]
        [Category("[Details]")]
        [TypeConverter(typeof(LanguageStringConverter))]
        public string Language { get; set; }

        [Browsable(true)]
        [Description("Trademarks (optional) is any trademark notifications that should appear. For example, HappyApp is a trademark of Acme, Inc.")]
        [Category("[Details]")]
        public string Trademarks { get; set; }

        [Browsable(true)]
        [Description("InstallType (optional) is if you would like the app listed as a specific install type within the menu. For some apps that are packaged by language (like Mozilla Firefox), the language may be specified on this line. In installers with optional components, this line is automatically updated by the installer based on the details within installer.ini (see below). The InstallType will be shown within the PortableApps.com Platform in the app’s details.")]
        [Category("[Details]")]
        public string InstallType { get; set; }

        public AppInfoDetailsSection()
        {
            Name = "AppName Portable";
            AppId = "AppNamePortable";
            Publisher = "App Developer & PortableApps.com";
            Homepage = "PortableApps.com / AppNamePortable";
            Category = "Utilities";
            Description = "AppName Portable is a tool that does something.";
            Language = "Multilingual";
            Trademarks = "";
            InstallType = "";
        }
    }
}
