using PortableAppStudio.INI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Model
{
    public class SectionINI : ISectionINI
    {
        public SectionINI()
        {
            this.SectionName = "";
        }

        public SectionINI(string sectionName)
        {
            this.SectionName = sectionName;
        }

        [Browsable(false)]
        public string SectionName { get; set; }

        [Browsable(false)]
        public bool IsRemoved { get; set; }

        public virtual TreeNode BuildTreeUI(string nodeName, TreeNode rootNode)
        {
            TreeNode topNode = new TreeNode(string.Format("[{0}]", nodeName));
            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(this))
            {
                if (item.IsBrowsable)
                {
                    if (item.PropertyType.IsValueType || item.PropertyType == typeof(string))
                    {
                        string iniValue = "";
                        var rawData = item.GetValue(this);
                        if (rawData != null)
                        {
                            iniValue = rawData.ToString();
                        }
                        topNode.Nodes.Add(string.Format("{0}={1}", item.Name, iniValue));
                    }
                    else if (item.PropertyType.GetInterface("IList") != null)
                    {
                        var topNodeN = topNode.Nodes.Add(string.Format("{0}", item.Name));
                        var rawData = item.GetValue(this);
                        if (rawData != null)
                        {
                            IList listData = rawData as IList;
                            if(listData != null)
                            {
                                foreach(var listItem in listData)
                                {
                                    topNodeN.Nodes.Add(listItem.ToString());
                                }
                            }
                        }
                    }
                }
            }

            rootNode.Nodes.Add(topNode);
            return topNode;
        }

        public virtual void LoadSection(INIFile file)
        {
            if(string.IsNullOrWhiteSpace(SectionName))
            {
                return;
            }

            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(this))
            {
                if (item.IsBrowsable)
                {
                    ReadValue(SectionName, file, item);
                }
            }
        }
        public virtual void LoadSection(string section, INIFile file)
        {
            if (string.IsNullOrWhiteSpace(SectionName))
            {
                SectionName = section;
            }

            LoadSection(file);
        }

        public virtual string Validate()
        {
            return "";
        }

        protected void ReadValue(string section, INIFile file, PropertyDescriptor item)
        {
            var iniData = file.ReadValue(section, item.Name);
            if (!string.IsNullOrWhiteSpace(iniData))
            {
                if (item.PropertyType == typeof(string))
                {
                    item.SetValue(this, iniData);
                }

                Type propertyType = item.PropertyType;

                if (item.PropertyType.GenericTypeArguments.Length > 0)
                {
                    propertyType = item.PropertyType.GenericTypeArguments.FirstOrDefault();
                }

                if (propertyType == typeof(int))
                {
                    int finalData = 0;
                    if (int.TryParse(iniData, out finalData))
                    {
                        item.SetValue(this, finalData);
                    }
                }
                else if (propertyType == typeof(bool))
                {
                    bool finalData = false;
                    if (bool.TryParse(iniData, out finalData))
                    {
                        item.SetValue(this, finalData);
                    }
                }
                else if (propertyType == typeof(double))
                {
                    double finalData = 0;
                    if (double.TryParse(iniData, out finalData))
                    {
                        item.SetValue(this, finalData);
                    }
                }
                else if (propertyType == typeof(long))
                {
                    long finalData = 0;
                    if (long.TryParse(iniData, out finalData))
                    {
                        item.SetValue(this, finalData);
                    }
                }
                else if (propertyType == typeof(float))
                {
                    float finalData = 0;
                    if (float.TryParse(iniData, out finalData))
                    {
                        item.SetValue(this, finalData);
                    }
                }
            }
        }

        public virtual void SaveSection(INIFile file)
        {
            if(string.IsNullOrWhiteSpace(SectionName))
            {
                return;
            }

            foreach (PropertyDescriptor item in TypeDescriptor.GetProperties(this))
            {
                if (item.IsBrowsable)
                {
                    WriteValue(SectionName, file, item);
                }
            }
        }

        public virtual void SaveSection(string section, INIFile file)
        {
            SectionName = section;
            SaveSection(file);
        }

        protected void WriteValue(string section, INIFile file, PropertyDescriptor item)
        {
            object iniData = item.GetValue(this);
            if (iniData == null)
            {
                file.DeleteKey(section, item.Name);
            }
            else
            {
                string tempVal = iniData.ToString();
                if (string.IsNullOrWhiteSpace(tempVal))
                {
                    file.DeleteKey(section, item.Name);
                }
                else
                {
                    file.WriteValue(section, item.Name, iniData.ToString());
                }
            }
        }
    }
}
