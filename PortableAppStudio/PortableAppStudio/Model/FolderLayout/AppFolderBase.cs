using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Model.FolderLayout
{
    public class AppFolderBase
    {
        public string RootFolder { get; protected set; }

        public TreeNode RootNode { get; protected set; }

        public string PortableAppFolder { get; protected set; }


        protected TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            var directoryNode = new TreeNode(directoryInfo.Name);
            foreach (var directory in directoryInfo.GetDirectories())
            {
                var nodeItem = CreateDirectoryNode(directory);
                var folderInfo = new FolderInfo(directory.FullName);
                folderInfo.CanDelete = true;
                nodeItem.Tag = folderInfo;
                directoryNode.Nodes.Add(nodeItem);
            }
            foreach (var file in directoryInfo.GetFiles())
            {
                var nodeItem = new TreeNode(file.Name);
                var fileInfo = new FileInfo(file.FullName);
                fileInfo.CanDelete = true;
                nodeItem.Tag = fileInfo;
                directoryNode.Nodes.Add(nodeItem);
            }
            return directoryNode;
        }

        public TreeNode BuildTreeUI()
        {
            this.RootNode = new TreeNode();

            if ((!string.IsNullOrWhiteSpace(this.RootFolder)) && Directory.Exists(this.RootFolder))
            {
                var rootDirectoryInfo = new DirectoryInfo(this.RootFolder);
                this.RootNode.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
            }

            return this.RootNode;
        }

        protected void SaveFileNode(TreeNode node)
        {
            if (node.Tag == null)
            {
                string folderName = string.Format("{0}\\{1}", this.PortableAppFolder, node.FullPath);
                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(folderName);
                }
            }
            else if (node.Tag is Model.FileInfo)
            {
                Model.FileInfo fileInfo = node.Tag as Model.FileInfo;
                string fileName = string.Format("{0}\\{1}", this.PortableAppFolder, node.GetFullPath());
                if (fileName.ToLower() != fileInfo.AbsolutePath.ToLower())
                {
                    if (File.Exists(fileInfo.AbsolutePath))
                    {
                        File.Copy(fileInfo.AbsolutePath, fileName, true);
                    }
                }
            }
            //else if(node.Tag is Model.FolderInfo)
            //{
            //    Model.FolderInfo folderInfo = node.Tag as Model.FolderInfo;
            //    if (!Directory.Exists(folderInfo.FolderName))
            //    {
            //        Directory.CreateDirectory(folderInfo.FolderName);
            //    }
            //}

            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode childNode in node.Nodes)
                {
                    SaveFileNode(childNode);
                }
            }
        }

    }
}
