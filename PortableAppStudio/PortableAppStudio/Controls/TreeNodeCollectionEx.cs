using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Controls
{
    public class TreeNodeCollectionEx
    {
        private TreeNodeEx _owner;
        public TreeNodeCollectionEx(TreeNodeEx owner)
        {
            _owner = owner;
        }

		public virtual TreeNodeEx Add(string text)
        {
            TreeNodeEx treeNode = new TreeNodeEx(text);
            _owner.Nodes.Add(treeNode);
            return treeNode;
        }

        public virtual TreeNodeEx Add(string key, string text)
        {
            TreeNodeEx treeNode = new TreeNodeEx(text);
            treeNode.Name = key;
            _owner.Nodes.Add(treeNode);
            return treeNode;
        }

        public virtual void Add(TreeNodeEx node)
        {
            _owner.Nodes.Add(node);
        }
    }
}
