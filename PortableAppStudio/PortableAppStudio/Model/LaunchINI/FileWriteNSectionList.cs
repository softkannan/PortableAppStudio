using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Model.LaunchINI
{
    public class FileWriteNSectionList : INISectionList<FileWriteNSection>
    {
        public virtual TreeNode BuildTreeUI(string nodeName, TreeNode rootNode, PropertyDescriptor propDescrip)
        {
            TreeNode topNode = new TreeNode(string.Format("[{0}]", nodeName));
            topNode.Tag = this;

            if (propDescrip != null)
            {
                var descripAttrib = propDescrip.Attributes.OfType<DescriptionAttribute>().FirstOrDefault();
                if (descripAttrib != null)
                {
                    topNode.ToolTipText = descripAttrib.Description;
                }
            }

            foreach (var item in this)
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

        public virtual void SaveNode(TreeNode node)
        {
            //if (node != null)
            //{
            //    foreach (TreeNode item in node.Nodes)
            //    {
            //        var newItem = item.Tag  as FileWriteNSection;
            //        newItem.FullValue = item.Text;
            //        Merge(newItem);
            //    }
            //}
        }
    }
}
