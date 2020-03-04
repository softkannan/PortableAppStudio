using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Controls
{
    public class TreeNodeEx : TreeNode
    {
        public TreeNodeCollectionEx NodesEx { get; private set; }
        public TreeNodeEx()
        {
            NodesEx = new TreeNodeCollectionEx(this);
        }
        public TreeNodeEx(string text) : base(text)
        {
            NodesEx = new TreeNodeCollectionEx(this);
        }
    }
}
