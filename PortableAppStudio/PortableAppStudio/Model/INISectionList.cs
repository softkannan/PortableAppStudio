using PortableAppStudio.INI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Model
{
    public class INISectionList<T> : List<T> , IINIList
        where T : class,ISectionINI,IINIKeyValuePair, new()
    {
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
            var existingItem = this.FirstOrDefault((item) => item.SectionName == newItem.SectionName);
            if (existingItem != null)
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
            return topNode;
        }

        public virtual void LoadSection(string sectionNamePrefix, INIFile file)
        {
            if (string.IsNullOrWhiteSpace(sectionNamePrefix))
            {
                return;
            }

            foreach (var sectionName in file.ReadSections())
            {
                if (sectionName.IndexOf(sectionNamePrefix, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    var fileWriteSection = new T();
                    fileWriteSection.LoadSection(sectionName, file);
                    Add(fileWriteSection);
                }
            }
        }
        
        public virtual void SaveSection(string sectionNamePrefix, INIFile file)
        {
            if (string.IsNullOrWhiteSpace(sectionNamePrefix))
            {
                return;
            }

            foreach (var sectionName in file.ReadSections())
            {
                if (sectionName.IndexOf(sectionNamePrefix, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    file.DeleteSection(sectionName);
                }
            }

            int startCount = 1;
            foreach (var fileWriteSection in this)
            {
                var sectionName = string.Format("{0}{1}", sectionNamePrefix, startCount);
                startCount++;
                fileWriteSection.SaveSection(sectionName, file);
            }
        }


        public virtual string Validate()
        {
            StringBuilder retVal = new StringBuilder("");
            foreach (ISectionINI item in this)
            {
                string errMsg = item.Validate();
                if (!string.IsNullOrWhiteSpace(errMsg))
                {
                    retVal.Append(string.Format("Error : {0}", errMsg));
                }
            }
            return retVal.ToString();
        }

       
    }
}
