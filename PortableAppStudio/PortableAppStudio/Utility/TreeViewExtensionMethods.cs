﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Utility
{
    public static class TreeViewExtensionMethods
    {
        public static string ToDirectoryName(this TreeNode pThis)
        {
            string retVal = "";
            Model.EnvironmentInfo envInfo;
            string dirInfo;
            if(PathManager.Init.SystemEnvironments.TryGetValue(pThis.GetTopNodeName(),out envInfo))
            {
                dirInfo = envInfo.DisplayName;
            }
            else
            {
                dirInfo = pThis.GetTopNodeName().Trim('%').Replace(" ", "_").Replace("(", "_").Replace(")", "_");
            }
            retVal = string.Format("{0}_{1}", dirInfo,
                pThis.Text.Replace(" ", "_"));
            return retVal;
        }
        public static string ToDirectoryPair(this TreeNode pThis)
        {
            string retVal = "";
            Model.EnvironmentInfo envInfo;
            string dirInfo;
            if (PathManager.Init.SystemEnvironments.TryGetValue(pThis.GetTopNodeName(), out envInfo))
            {
                dirInfo = envInfo.DisplayName;
            }
            else
            {
                dirInfo = pThis.GetTopNodeName().Trim('%').Replace(" ", "_").Replace("(", "_").Replace(")", "_");
            }
            retVal = string.Format("{0}_{1}={2}", dirInfo ,
                pThis.Text.Replace(" ", "_"),
                pThis.GetFullPath());
            return retVal;
        }
        public static string ToFilePair(this TreeNode pThis)
        {
            string retVal = "";
            Model.EnvironmentInfo envInfo;
            string dirInfo;
            if (PathManager.Init.SystemEnvironments.TryGetValue(pThis.GetTopNodeName(), out envInfo))
            {
                dirInfo = envInfo.DisplayName;
            }
            else
            {
                dirInfo = pThis.GetTopNodeName().Trim('%').Replace(" ", "_").Replace("(", "_").Replace(")", "_");
            }
            retVal = string.Format("{0}_{1}\\{2}={3}",
                dirInfo, pThis.Parent.Text, pThis.Text, pThis.GetFullPath());
            return retVal;

            
        }
        public static bool IsDescendantOf(this TreeNode pThis,string parentNode)
        {
            if (string.Compare(pThis.Text,parentNode,true) == 0)
            {
                return true;
            }
            else if(pThis.Parent != null)
            {
                return IsDescendantOf(pThis.Parent, parentNode);
            }
            else
            {
                return false;
            }
        }

        public static TreeNode FindNode(this TreeNodeCollection pThis,string nodeText, bool recursiveSearch = false)
        {
            TreeNode retVal = null;
            foreach(TreeNode item in pThis)
            {
                if(string.Compare(item.Text,nodeText,true) == 0)
                {
                    retVal = item;
                    break;
                }
                if(recursiveSearch && item.Nodes.Count > 0)
                {
                    retVal = FindNode(item.Nodes, nodeText, recursiveSearch);
                }

                if(retVal != null)
                {
                    break;
                }
            }
            return retVal;
        }

        public static bool IsNode(this TreeNode pThis, string nodeText)
        {
            if (string.Compare(pThis.Text, nodeText, true) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsChildOf(this TreeNode pThis, string parentNode)
        {
            if (pThis.Parent != null && string.Compare(pThis.Parent.Text, parentNode, true) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static TreeNode GetTopNode(this TreeNode pThis)
        {
            TreeNode retVal = null;
            if (pThis == null)
            {
                return retVal;
            }
            if (pThis.Parent != null)
            {
                retVal = GetTopNode(pThis.Parent);
            }
            else
            {
                retVal = pThis;
            }
            return retVal;
        }

        public static string GetNodeValue(this TreeNode pThis,string nodePath="",string valueName = "@")
        {
            string retVal = "";
            var foundNode = string.IsNullOrEmpty(nodePath) ? pThis : pThis.GetNode(nodePath);
            if(foundNode != null)
            {
                foreach(TreeNode node in foundNode.Nodes)
                {
                    Model.RegInfo regInfo = node.Tag as Model.RegInfo;
                    if (regInfo != null && string.Compare(regInfo.ValueName,valueName,true) == 0 )
                    {
                        retVal = regInfo.Value;
                    }
                }
            }
            return retVal;
        }
        
        public static TreeNode GetNode(this TreeNode pThis, string nodePath)
        {
            if(string.IsNullOrWhiteSpace(nodePath))
            {
                return null;
            }
            int nodeEndPos = nodePath.IndexOf("\\");
            if(nodeEndPos == -1)
            {
                TreeNode childNode = null;
                foreach (TreeNode item in pThis.Nodes)
                {
                    if (string.Compare(item.Text, nodePath, true) == 0)
                    {
                        childNode = item;
                        break;
                    }
                }
                return childNode;
            }
            else
            {
                string childNodeName = nodePath.Substring(0, nodeEndPos);
                TreeNode childNode = null;
                foreach(TreeNode item in pThis.Nodes)
                {
                    if(string.Compare(item.Text,childNodeName,true) == 0)
                    {
                        childNode = item;
                        break;
                    }
                }
                string remainPath = nodePath.Substring(nodeEndPos + 1);
                return childNode?.GetNode(remainPath);
            }

        }


        public static string GetTopNodeName(this TreeNode pThis)
        {
            string retVal = "";
            if(pThis.Parent != null)
            {
                retVal = GetTopNodeName(pThis.Parent);
            }
            else
            {
                retVal = pThis.Text;
            }
            return retVal;
        }

        public static string GetFullPath(this TreeNode pThis, string upTo = null)
        {
            if(upTo == null)
            {
                return GetFullPathInternal(pThis);
            }
            else if(pThis.IsDescendantOf(upTo))
            {
                return GetFullPathInternal(pThis,upTo);
            }
            return "";
        }

        private static string GetFullPathInternal(TreeNode pThis, string upToNode = null)
        {
            string retVal = "";
            if (pThis.Parent != null)
            {
                if (pThis.Parent.Text != upToNode)
                {
                    retVal = string.Format("{0}\\{1}", GetFullPathInternal(pThis.Parent, upToNode), pThis.Text);
                }
                else
                {
                    return pThis.Text;
                }
            }
            else
            {
                retVal = pThis.Text;
            }
            return retVal;
        }
        public static string GetDirectory(this TreeNode pThis)
        {
            string retVal = "";
            if (pThis.Parent != null)
            {
                string nodeText = pThis.Tag is Model.FileInfo ? "" : pThis.Text;
                retVal = string.Format("{0}\\{1}", GetFullPath(pThis.Parent), nodeText);
            }
            else
            {
                retVal = pThis.Text;
            }

            return retVal;
        }
    }
}
