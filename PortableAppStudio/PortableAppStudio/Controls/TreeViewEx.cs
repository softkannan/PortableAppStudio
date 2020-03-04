using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Controls
{
    public class TreeViewEx : TreeView
    {
        protected List<TreeNode> _selectedNodes;
        protected TreeNode m_lastNode, m_firstNode;
        public Dictionary<string, TreeNode> KeyNodes { get; private set; }

        public TreeViewEx()
        {
            _selectedNodes = new List<TreeNode>();
            KeyNodes = new Dictionary<string, TreeNode>();
        }

        public void Reset()
        {
            Nodes.Clear();
            KeyNodes.Clear();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // TODO: Add custom paint code here

            // Calling the base class OnPaint
            base.OnPaint(pe);
        }

        public void ClearSelection()
        {
            lock (_selectedNodes)
            {
                removePaintFromNodes();
                _selectedNodes.Clear();
                paintSelectedNodes();
            }
        }


        public List<TreeNode> MultiSelectedNodes
        {
            get
            {
                List<TreeNode> retVal = new List<TreeNode>(_selectedNodes);
                return retVal;
            }
        }

        public bool IsMultiSelected
        {
            get
            {
                return _selectedNodes.Count > 1;
            }
        }

        public int MultiSelectionCount
        {
            get
            {
                return _selectedNodes.Count;
            }
        }



        // Triggers
        //
        // (overriden method, and base class called to ensure events are triggered)


        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            lock (_selectedNodes)
            {
                base.OnBeforeSelect(e);

                bool bControl = (ModifierKeys == Keys.Control);
                bool bShift = (ModifierKeys == Keys.Shift);

                // selecting twice the node while pressing CTRL ?
                if (bControl && _selectedNodes.Contains(e.Node))
                {
                    // unselect it (let framework know we don't want selection this time)
                    e.Cancel = true;

                    // update nodes
                    removePaintFromNodes();
                    _selectedNodes.Remove(e.Node);
                    paintSelectedNodes();
                    return;
                }

                m_lastNode = e.Node;
                if (!bShift) m_firstNode = e.Node; // store begin of shift sequence
            }
        }


        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            lock (_selectedNodes)
            {
                base.OnAfterSelect(e);

                bool bControl = (ModifierKeys == Keys.Control);
                bool bShift = (ModifierKeys == Keys.Shift);

                if (bControl)
                {
                    if (!_selectedNodes.Contains(e.Node)) // new node ?
                    {
                        _selectedNodes.Add(e.Node);
                    }
                    else  // not new, remove it from the collection
                    {
                        removePaintFromNodes();
                        _selectedNodes.Remove(e.Node);
                    }
                    paintSelectedNodes();
                }
                else
                {
                    // SHIFT is pressed
                    if (bShift)
                    {
                        Queue<TreeNode> myQueue = new Queue<TreeNode>();

                        TreeNode uppernode = m_firstNode;
                        TreeNode bottomnode = e.Node;
                        // case 1 : begin and end nodes are parent
                        bool bParent = isParent(m_firstNode, e.Node); // is m_firstNode parent (direct or not) of e.Node
                        if (!bParent)
                        {
                            bParent = isParent(bottomnode, uppernode);
                            if (bParent) // swap nodes
                            {
                                TreeNode t = uppernode;
                                uppernode = bottomnode;
                                bottomnode = t;
                            }
                        }
                        if (bParent)
                        {
                            TreeNode n = bottomnode;
                            while (n != uppernode.Parent)
                            {
                                if (!_selectedNodes.Contains(n)) // new node ?
                                    myQueue.Enqueue(n);

                                n = n.Parent;
                            }
                        }
                        // case 2 : nor the begin nor the end node are descendant one another
                        else
                        {
                            if ((uppernode.Parent == null && bottomnode.Parent == null) || (uppernode.Parent != null && uppernode.Parent.Nodes.Contains(bottomnode))) // are they siblings ?
                            {
                                int nIndexUpper = uppernode.Index;
                                int nIndexBottom = bottomnode.Index;
                                if (nIndexBottom < nIndexUpper) // reversed?
                                {
                                    TreeNode t = uppernode;
                                    uppernode = bottomnode;
                                    bottomnode = t;
                                    nIndexUpper = uppernode.Index;
                                    nIndexBottom = bottomnode.Index;
                                }

                                TreeNode n = uppernode;
                                while (nIndexUpper <= nIndexBottom)
                                {
                                    if (!_selectedNodes.Contains(n)) // new node ?
                                        myQueue.Enqueue(n);

                                    n = n.NextNode;

                                    nIndexUpper++;
                                } // end while

                            }
                            else
                            {
                                if (!_selectedNodes.Contains(uppernode)) myQueue.Enqueue(uppernode);
                                if (!_selectedNodes.Contains(bottomnode)) myQueue.Enqueue(bottomnode);
                            }
                        }

                        _selectedNodes.AddRange(myQueue);

                        paintSelectedNodes();
                        m_firstNode = e.Node; // let us chain several SHIFTs if we like it
                    } // end if m_bShift
                    else
                    {
                        // in the case of a simple click, just add this item
                        if (_selectedNodes != null && _selectedNodes.Count > 0)
                        {
                            removePaintFromNodes();
                            _selectedNodes.Clear();
                        }
                        _selectedNodes.Add(e.Node);
                    }
                }
            }
        }



        // Helpers
        //
        //


        protected bool isParent(TreeNode parentNode, TreeNode childNode)
        {
            if (parentNode == childNode)
                return true;

            TreeNode n = childNode;
            bool bFound = false;
            while (!bFound && n != null)
            {
                n = n.Parent;
                bFound = (n == parentNode);
            }
            return bFound;
        }

        protected void paintSelectedNodes()
        {
            foreach (TreeNode n in _selectedNodes)
            {
                n.BackColor = SystemColors.Highlight;
                n.ForeColor = SystemColors.HighlightText;
            }
        }

        protected void removePaintFromNodes()
        {
            if (_selectedNodes.Count == 0) return;

            try
            {
                TreeNode n0 = (TreeNode)_selectedNodes[0];
                Color back = n0.TreeView.BackColor;
                Color fore = n0.TreeView.ForeColor;

                foreach (TreeNode n in _selectedNodes)
                {
                    n.BackColor = back;
                    n.ForeColor = fore;
                }
            }
            catch(Exception)
            { }
        }
    }
}
