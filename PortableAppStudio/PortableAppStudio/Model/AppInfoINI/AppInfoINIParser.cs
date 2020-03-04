using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using PortableAppStudio.INI;
using System.Windows.Forms;
using System.Collections;

namespace PortableAppStudio.Model.AppInfoINI
{
    public partial class AppInfoINI
    {
        private INIFile _iniFile = null;

        public string AppInfoFolder { get; set; }

        public void Load(string appInfoFolder,string portableAppFolder)
        {
            AppInfoFolder = appInfoFolder;
            bool isNewApp = false;
            var fileName = Directory.GetFiles(AppInfoFolder, "appinfo*.ini").FirstOrDefault();
            if(string.IsNullOrWhiteSpace(fileName))
            {
                fileName = string.Format("{0}\\appinfo.ini", appInfoFolder);
            }

            if(!File.Exists(fileName))
            {
                File.Copy(PathManager.Init.GetResourcePath(PathManager.APPINFO_FILE), fileName);
                isNewApp = true;
            }

            if (File.Exists(fileName))
            {
                LoadInternal(fileName);
            }

            if (isNewApp)
            {
                this.Details.Name = Path.GetFileName(portableAppFolder);
                this.Details.AppId = this.Details.Name.Replace(" ","");
                this.Details.Publisher = string.Format("{0} Team", this.Details.Name);
                this.Details.Homepage = "";
                this.Details.Category = "Utilities";
                this.Details.Language = "Multilingual";
                this.Details.Description = string.Format("{0} Application", this.Details.Name);

                this.Control.Start = string.Format("{0}.exe", this.Details.Name);
            }
        }

        private void LoadInternal(string fileName)
        {
            _iniFile = new INIFile(fileName);

            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(this))
            {
                if(item.IsBrowsable)
                {
                    switch(item.Name)
                    {
                        case "Format":
                            Format.LoadSection("Format", _iniFile);
                            break;
                        case "Details":
                            Details.LoadSection("Details", _iniFile);
                            break;
                        case "License":
                            License.LoadSection("License", _iniFile);
                            break;
                        case "Version":
                            Version.LoadSection("Version", _iniFile);
                            break;
                        case "SpecialPaths":
                            SpecialPaths.LoadSection("SpecialPaths", _iniFile);
                            break;
                        case "Dependencies":
                            Dependencies.LoadSection("Dependencies", _iniFile);
                            break;
                        case "Control":
                            Control.LoadSection("Control", _iniFile);
                            break;
                    }

                }
            }
        }

        public void GetSectionToEdit(string name, out object retObjValue, out Model.IINIList retListValue)
        {
            retObjValue = null;
            retListValue = null;

            switch (name)
            {
                case "[Details]":
                    {
                        retObjValue = Details;
                    }
                    break;
                case "[Version]":
                    {
                        retObjValue = Version;
                    }
                    break;
                case "[SpecialPaths]":
                    {
                        retObjValue = SpecialPaths;
                    }
                    break;
                case "[Dependencies]":
                    {
                        retObjValue = Dependencies;
                    }
                    break;
                case "[Control]":
                    {
                        retObjValue = Control;
                    }
                    break;
                case "[License]":
                    {
                        retObjValue = License;
                    }
                    break;
                case "[Format]":
                    {
                        retObjValue = Format;
                    }
                    break;
            }
        }

        public TreeNode BuildTreeUI()
        {
            TreeNode rootNode = new TreeNode();

            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(this))
            {
                if (item.IsBrowsable)
                {
                    switch (item.Name)
                    {
                        case "Format":
                            {
                                var topNode = Format.BuildTreeUI("Format", rootNode);
                                topNode.Expand();
                            }
                            break;
                        case "Details":
                            {
                                var topNode = Details.BuildTreeUI("Details", rootNode);
                                topNode.Expand();
                            }
                            break;
                        case "License":
                            {
                                var topNode = License.BuildTreeUI("License", rootNode);
                                topNode.Expand();
                            }
                            break;
                        case "Version":
                            {
                                var topNode = Version.BuildTreeUI("Version", rootNode);
                                topNode.Expand();
                            }
                            break;
                        case "SpecialPaths":
                            {
                                var topNode = SpecialPaths.BuildTreeUI("SpecialPaths", rootNode);
                                topNode.Expand();
                            }
                            break;
                        case "Dependencies":
                            {
                                var topNode = Dependencies.BuildTreeUI("Dependencies", rootNode);
                                topNode.Expand();
                            }
                            break;
                        case "Control":
                            {
                                var topNode = Control.BuildTreeUI("Control", rootNode);
                                topNode.Expand();
                            }
                            break;
                    }

                }
            }

            return rootNode;
        }

        public void Save()
        {
            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(this))
            {
                if (item.IsBrowsable)
                {
                    switch (item.Name)
                    {
                        case "Format":
                            Format.SaveSection("Format", _iniFile);
                            break;
                        case "Details":
                            Details.SaveSection("Details", _iniFile);
                            break;
                        case "License":
                            License.SaveSection("License", _iniFile);
                            break;
                        case "Version":
                            Version.SaveSection("Version", _iniFile);
                            break;
                        case "SpecialPaths":
                            SpecialPaths.SaveSection("SpecialPaths", _iniFile);
                            break;
                        case "Dependencies":
                            Dependencies.SaveSection("Dependencies", _iniFile);
                            break;
                        case "Control":
                            Control.SaveSection("Control", _iniFile);
                            break;
                    }

                }
            }
        }

        public void Create()
        {
            Format = new AppInfoFormatSection();
            Details = new AppInfoDetailsSection();
            License = new AppInfoLicenseSection();
            Version = new AppInfoVersionSection();
            SpecialPaths = new AppInfoSpecialPathsSection();
            Dependencies = new AppInfoDependenciesSection();
            Control = new AppInfoControlSection();
        }

        public void CreateDefault()
        {

        }
    }
}