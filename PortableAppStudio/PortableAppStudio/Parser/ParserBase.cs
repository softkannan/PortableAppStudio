using Microsoft.Win32;
using PortableAppStudio.Controls;
using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Parser
{
    public class ParserBase
    {
        protected const string HKLM_LONG = "HKEY_LOCAL_MACHINE";
        protected const string HKLM_SHORT = "HKLM";
        protected const string HKCU_LONG = "HKEY_CURRENT_USER";
        protected const string HKCU_SHORT = "HKCU";
        protected const string HKU_SHORT = "HKU";
        protected const string HKU_LONG = "HKEY_USER";

        protected const string REG_DWORD = "dword:";
        protected const string REG_BINARY = "hex:";
        protected const string REG_QWORD = "hex(b):";
        protected const string REG_MULTI_SZ = "hex(7):";
        protected const string REG_EXPAND_SZ = "hex(2):";
        protected const string REG_CUSTOM = "hex(";
        protected const string REG_SZ = "";

        protected const string REGEDIT5 = "Windows Registry Editor Version 5.00";
        protected const string REGEDIT4 = "REGEDIT4";

        public string ParserType { get; protected set; }
        public SortedDictionary<string,string> SearchReplaceList { get; protected set; }

        protected StringExtensionMethods.CodingType codingType = StringExtensionMethods.CodingType.Unicode;

        public ParserBase()
        {
            this.ParserType = RegSourceType.RegFile;

            SearchReplaceList = new SortedDictionary<string, string>(new DescendingComparer<string>());

            foreach (var item in PathManager.Init.SystemEnvironments)
            {
                if (!SearchReplaceList.ContainsKey(item.Value.ShortPath))
                {
                    SearchReplaceList.Add(item.Value.ShortPath, item.Value.RelativePath);
                }
            }
        }

        private List<Model.RegInfo> _regValues = new List<Model.RegInfo>();
        private List<Model.FileInfo> _fileValues = new List<Model.FileInfo>();

        protected Dictionary<string, string>  RelativePathMap {get;set;}

        protected void ParseFiles(string folderName)
        {
            foreach (var item in Directory.GetDirectories(folderName))
            {
                string folderSubStitution = "";
                string currentDirName = Path.GetFileName(item);
                if (RelativePathMap != null)
                {
                    foreach (var dicItem in RelativePathMap)
                    {
                        if (string.Compare(currentDirName, dicItem.Key, true) == 0)
                        {
                            folderSubStitution = dicItem.Value;
                            break;
                        }
                    }
                }
                if (string.IsNullOrEmpty(folderSubStitution))
                {
                    folderSubStitution = currentDirName;
                }
                AddFolder(item, item, folderSubStitution);
            }
        }

        protected void AddFolder(string folderName, string rootFolder, string rootFolderSubStitution)
        {
            int isEmptyFolder = 0;
            foreach (var item in Directory.GetFiles(folderName, "*.*", SearchOption.TopDirectoryOnly))
            {
                string relativePath = string.Format("{0}\\{1}", rootFolderSubStitution, item.Substring(rootFolder.Length + 1));
                AddFile(item, relativePath);
                isEmptyFolder = 1;
            }

            foreach (var item in Directory.GetDirectories(folderName))
            {
                isEmptyFolder = 2;
                AddFolder(item, rootFolder, rootFolderSubStitution);
            }

            if (isEmptyFolder == 0)
            {
                AddFile(folderName, rootFolderSubStitution);
            }
        }

        protected void AddFile(string filePath, string relativePath = null)
        {
            if(relativePath == null && PathManager.Init.IsGoodFile(filePath))
            {
                _fileValues.Add(new Model.FileInfo(filePath));
            }
            else if(relativePath != null)
            {
                _fileValues.Add(new Model.FileInfo(filePath, relativePath));
            }
        }

        protected void AddFile(string[] filePath, string baseFolder = null)
        {
            for (int index = 0; index < filePath.Length; index++)
            {
                if (baseFolder == null && PathManager.Init.IsGoodFile(filePath[index]))
                {
                    _fileValues.Add(new Model.FileInfo(filePath[index]));
                }
                else if(baseFolder != null)
                {
                    string relativePath = filePath[index].Substring(baseFolder.Length + 1);
                    _fileValues.Add(new Model.FileInfo(filePath[index], relativePath));
                }
            }
        }

        protected void AddRegValue(string key, string valueName, string regKind, string regValue)
        {
            if(string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(valueName) || string.IsNullOrWhiteSpace(regKind))
            {
                Debugger.Break();
            }
            _regValues.Add(new Model.RegInfo(SearchReplaceList,ParserType, key, valueName, regKind, regValue));
        }
        
        public virtual void Parse(string fileName)
        {
            throw new NotImplementedException("Parse");
        }
        
        public void PopulateSourceTreeView(TreeViewEx fileView, TreeViewEx regView)
        {
            if (_fileValues != null)
            {
                PopulateFileTreeView(fileView, _fileValues);

                if (fileView.Nodes.Count == 0)
                {
                    fileView.Nodes.Add("Empty");
                }
            }

            if (_regValues != null)
            {

                PopulateRegTreeView(regView, _regValues);

                if (regView.Nodes.Count == 0)
                {
                    regView.Nodes.Add("Empty");
                }
            }
        }
        private void PopulateRegTreeView(TreeViewEx regView, List<Model.RegInfo> listOfRegistryValues, char pathSeparator = '\\')
        {
            string subPathAgg;

            foreach (Model.RegInfo regItem in listOfRegistryValues)
            {
                TreeNode lastNode = null;
                subPathAgg = string.Empty;
                var tempKeyNodes = regItem.Key.Split(pathSeparator);
                for (int index = 0; index < tempKeyNodes.Length; index++)
                {
                    subPathAgg += tempKeyNodes[index] + pathSeparator;
                    TreeNode foundNode;
                    var isNodeFound = regView.KeyNodes.TryGetValue(subPathAgg, out foundNode);
                    if (isNodeFound == false)
                    {
                        if (lastNode == null)
                        {
                            lastNode = regView.Nodes.Add(subPathAgg, tempKeyNodes[index]);
                            regView.KeyNodes.Add(subPathAgg, lastNode);
                        }
                        else
                        {
                            lastNode = lastNode.Nodes.Add(subPathAgg, tempKeyNodes[index]);
                            regView.KeyNodes.Add(subPathAgg, lastNode);
                        }
                    }
                    else
                    {
                        lastNode = foundNode;
                    }
                }

                if(lastNode != null && subPathAgg != string.Empty)
                {
                    var tempNode = lastNode.Nodes.Add(subPathAgg, string.Format("{0}={1}:{2}", regItem.ValueName, regItem.Kind, regItem.Value));
                    tempNode.Tag = regItem;
                }
            }
        }
        private void PopulateFileTreeView(TreeViewEx fileView, List<Model.FileInfo> listOfFiles, char pathSeparator = '\\')
        {
            string subPathAgg;
            foreach (Model.FileInfo fileInfo in listOfFiles)
            {
                TreeNode lastNode = null;
                subPathAgg = string.Empty;
                var subPath = fileInfo.RelativePath.Split(pathSeparator);

                for (int index = 0; index < subPath.Length; index++)
                {
                    subPathAgg += subPath[index] + pathSeparator;
                    TreeNode foundNode;
                    var isNodeFound = fileView.KeyNodes.TryGetValue(subPathAgg, out foundNode);
                    if (isNodeFound == false)
                    {
                        if (lastNode == null)
                        {
                            lastNode = fileView.Nodes.Add(subPathAgg, subPath[index]);
                            fileView.KeyNodes.Add(subPathAgg, lastNode);
                            if (index == subPath.Length - 1)
                            {
                                lastNode.Tag = fileInfo;
                            }
                        }
                        else
                        {
                            lastNode = lastNode.Nodes.Add(subPathAgg, subPath[index]);
                            fileView.KeyNodes.Add(subPathAgg, lastNode);
                            if (index == subPath.Length - 1)
                            {
                                lastNode.Tag = fileInfo;
                            }
                        }
                    }
                    else
                    {
                        lastNode = foundNode;
                    }
                }
            }
        }
        
    }
}
