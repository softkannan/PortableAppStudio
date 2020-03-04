using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PortableAppStudio.INI;

namespace PortableAppStudio.Model
{
    public class INIValueList<T>: List<T> , ISectionINI, IINIList
        where T : class, IINIKeyValuePair, new()
    {
        public string SectionName { get;set; }

        public void UpdateIndex()
        {
            int startIndex = 1;
            foreach (var item in this)
            {
                item.UpdateIndex(startIndex);
                startIndex++;
            }
        }

        public void Merge(T newItem)
        {
            var existingItem = this.FirstOrDefault((item) => item.IniKey == newItem.IniKey);
            if(existingItem != null)
            {
                this.Remove(existingItem);
                this.Add(newItem);
            }
            else
            {
                this.Add(newItem);
            }
        }

        public virtual TreeNode BuildTreeUI(string nodeName, TreeNode rootNode)
        {
            TreeNode topNode = new TreeNode(string.Format("[{0}]", nodeName));
            foreach(var item in this)
            {
                var tempNode = new TreeNode(item.FullValue);
                tempNode.Tag = item;
                if (tempNode != null)
                {
                    topNode.Nodes.Add(tempNode);
                }
            }
            rootNode.Nodes.Add(topNode);
            return topNode;
        }

        public virtual string Validate()
        {
            StringBuilder retVal = new StringBuilder("");
            foreach (var item in this)
            {
                string errMsg = item.Validate();
                if (!string.IsNullOrWhiteSpace(errMsg))
                {
                    retVal.Append(string.Format("Error : {0}", errMsg));
                }
            }
            return retVal.ToString();
        }
        public virtual void LoadSection(string section, INIFile file)
        {
            var listOfKeys = file.ReadKeys(section);
            if (listOfKeys != null)
            {
                foreach (var item in listOfKeys)
                {
                    var iniData = file.ReadValue(section, item, null);
                    if (!string.IsNullOrWhiteSpace(iniData))
                    {
                        T newData = new T();
                        newData.IniKey = item;
                        newData.IniValue = iniData;
                        Add(newData);
                    }
                }
            }
        }

        public virtual void SaveNode(TreeNode node)
        {
            if(node != null)
            {
                foreach(TreeNode item in node.Nodes)
                {
                    T newItem = new T();
                    newItem.FullValue = item.Text;
                    Merge(newItem);
                }
            }
        }

        public virtual void SaveSection(string section, INIFile file)
        {
            if (string.IsNullOrWhiteSpace(section))
            {
                return;
            }

            var listOfKeys = file.ReadKeys(section);
            if (listOfKeys != null)
            {
                foreach(var item in listOfKeys)
                {
                    file.DeleteKey(section, item);
                }
            }

            foreach (var item in this)
            {
                if (item.IsRemoved)
                {
                    continue;
                }
                else if (!string.IsNullOrWhiteSpace(item.IniValue))
                {
                    file.WriteValue(section, item.IniKey, item.IniValue);
                }
            }
        }
    }
}
