using PortableAppStudio.Controls;
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
    public class PortableApp
    {
        private static PortableApp _inst = null;
        public static PortableApp Inst
        {
            get
            {
                if (_inst == null)
                {
                    _inst = new PortableApp();
                }
                return _inst;
            }
        }

        public string RootFolder { get; private set; }
        private string _appFolder;
        private string _dataFolder;
        private string _otherFolder;

        public PortableApp()
        {
            
        }

        private void CreateNew()
        {
            _isAlreadyOpen = false;
            _isValueModified = true;
            _lastDestFileTreeNodeCount = 0;
            _lastDestRegTreeNodeCount = 0;

            Data = new DataFolder();
            App = new AppFolder();
            Other = new OtherFolder();
        }

        public void Close()
        {
            App?.Dispose();
            CreateNew();
            RefreshAppTrees(false);
        }

        private bool _isValueModified = false;
        public void OnValueChanged(object s, EventArgs e)
        {
            _isValueModified = true;
        }

        public bool IsValueModified
        {
            get
            {
                bool retVal = false;
                if(_lastDestFileTreeNodeCount != _destFileTree.GetNodeCount(true) ||
                    _lastDestRegTreeNodeCount != _destRegTree.GetNodeCount(true))
                {
                    retVal = true;
                }

                if(!retVal)
                {
                    retVal = _isValueModified;
                }

                return retVal;
            }
        }

        private bool _isAlreadyOpen = false;
        public bool IsAlreadyOpen { get => _isAlreadyOpen;  }

        public AppFolder App { get; set; }

        public OtherFolder Other { get; set; }

        public DataFolder Data { get; set; }

        public void LoadFolder(string folderName, bool openExistingApp)
        {
            if(IsAlreadyOpen)
            {
                return;
            }

            if(Directory.Exists(folderName))
            {
                CreateNew();
                _isValueModified = !openExistingApp;
                _isAlreadyOpen = true;
                RootFolder = folderName;
                LoadInternal(RootFolder);
                RefreshAppTrees(false);
            }
        }

        public void Reload()
        {
            CreateNew();
            _isValueModified = false;
            _isAlreadyOpen = true;
            LoadInternal(RootFolder);
            RefreshAppTrees(false);
        }

        private void LoadInternal(string folderName)
        {
            _appFolder = string.Format("{0}\\App", folderName);
            if (!Directory.Exists(_appFolder))
            {
                Directory.CreateDirectory(_appFolder);
                var srcPath = PathManager.Init.GetResourcePath("PortableApp\\App");
                FileUtility.Inst.CopyAll(srcPath, _appFolder);
            }
            _dataFolder = string.Format("{0}\\Data", folderName);
            if (!Directory.Exists(_dataFolder))
            {
                Directory.CreateDirectory(_dataFolder);
                var srcPath = PathManager.Init.GetResourcePath("PortableApp\\Data");
                FileUtility.Inst.CopyAll(srcPath, _dataFolder);
            }
            _otherFolder = string.Format("{0}\\Other", folderName);
            if (!Directory.Exists(_otherFolder))
            {
                Directory.CreateDirectory(_otherFolder);
                var srcPath = PathManager.Init.GetResourcePath("PortableApp\\Other");
                FileUtility.Inst.CopyAll(srcPath, _otherFolder);
            }

            App.LoadFolder(_appFolder, RootFolder);

            Data.LoadFolder(_dataFolder, RootFolder);

            Other.LoadFolder(_otherFolder);
        }

        private TreeViewEx _srcFileTree;
        private TreeViewEx _srcRegTree;
        private TreeViewEx _destFileTree;
        private int _lastDestFileTreeNodeCount;
        private TreeViewEx _destRegTree;
        private int _lastDestRegTreeNodeCount;
        private TreeViewEx _appInfoTree;
        private TreeViewEx _launchTree;

        public void Initailize(TreeViewEx srcFileTree, TreeViewEx srcRegTree, TreeViewEx destFileTree, TreeViewEx destRegTree, TreeViewEx appInfoTree, TreeViewEx launchTree)
        {
            _srcFileTree = srcFileTree;
            _srcRegTree = srcRegTree;
            _destFileTree = destFileTree;
            _destRegTree = destRegTree;
            _appInfoTree = appInfoTree;
            _launchTree = launchTree;
        }

        public void Save()
        {
            if (!IsAlreadyOpen)
            {
                return;
            }

            App.SaveTree(_srcFileTree, _srcRegTree, _destFileTree, _destRegTree, _appInfoTree, _launchTree);
            Data.SaveTree(_srcFileTree, _srcRegTree, _destFileTree, _destRegTree);
            Other.SaveTree(_srcFileTree, _srcRegTree, _destFileTree, _destRegTree);

            _isValueModified = false;
        }

        private void AddNode(TreeNode rootNode, TreeNode child)
        {
            if (child != null)
            {
                foreach (TreeNode item in child.Nodes)
                {
                    rootNode.Nodes.Add(item);
                }
            }
        }

        public void RefreshAppTrees(bool externalRefresh)
        {
            if (!IsAlreadyOpen)
            {
                return;
            }

            try
            {
                //build appp files tree
                var portableAppsFiles = BuildAppFilesTreeUI();
                _destFileTree.Nodes.Clear();
                foreach (TreeNode item in portableAppsFiles.Nodes)
                {
                    _destFileTree.Nodes.Add(item);
                }
                
                //build app reg tree
                var launchRegRoot = App.Launch.BuildRegTreeUI();
                _destRegTree.Nodes.Clear();
                foreach (TreeNode item in launchRegRoot.Nodes)
                {
                    _destRegTree.Nodes.Add(item);
                }
                _destRegTree.ExpandAll();

                //builds appinfo.ini tree
                var appInfoINIRoot = App.AppInfo.BuildTreeUI();
                _appInfoTree.Nodes.Clear();
                foreach (TreeNode item in appInfoINIRoot.Nodes)
                {
                    _appInfoTree.Nodes.Add(item);
                }
                _appInfoTree.ExpandAll();

                //build launch.ini tree
                var launchINIRoot = PortableApp.Inst.App.Launch.BuildLaunchTreeUI();
                _launchTree.Nodes.Clear();
                foreach (TreeNode item in launchINIRoot.Nodes)
                {
                    _launchTree.Nodes.Add(item);
                }

                if (!externalRefresh)
                {
                    _lastDestFileTreeNodeCount = _destFileTree.GetNodeCount(true);
                    _lastDestRegTreeNodeCount = _destRegTree.GetNodeCount(true);
                }
            }
            finally
            {
               
            }
        }

        public TreeNode BuildAppFilesTreeUI()
        {
            if(!IsAlreadyOpen)
            {
                return null;
            }

            TreeNode rootNode = new TreeNode();

            var appNode = App.BuildTreeUI();
            appNode.Expand();
            foreach (TreeNode item in appNode.Nodes)
            {
                item.Expand();
            }

            AddNode(rootNode, appNode);

            var dataNode = Data.BuildTreeUI();

            AddNode(rootNode, dataNode);

            var otherNode = Other.BuildTreeUI();

            AddNode(rootNode, otherNode);

            var launchINIFileNode = App.Launch.BuildFileTreeUI();

            AddNode(rootNode, launchINIFileNode);

            return rootNode;
        }
    }
}
