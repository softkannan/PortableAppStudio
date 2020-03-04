using PortableAppStudio.Controls;
using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Model.COM
{
    public class ComRegInfo
    {
        enum ParseStage
        {
            CLSID,
            Interface,
            TypeLib,
            ProgId
        }

        private static ComRegInfo _inst = new ComRegInfo();
        public static ComRegInfo Inst { get { return _inst; } }


        private Dictionary<string, ComFileInfo> _allRegComFiles;
        private Dictionary<string, ComClassRegInfo> _allRegComClasses;
        private Dictionary<string, ComClassRegInfo> _allRegFileNameEmptyClasses;
        private Dictionary<string, ComInterfaceProxyStubRegInfo> _allRegComInterfaceProxyStub;
        private Dictionary<string, ComInterfaceExternalProxyStubRegInfo> _allRegComInterfaceExternalProxyStub;

        private Dictionary<string, ComFileInfo> _allFileComFiles;

        private bool _overwriteManifestFiles = false;

        public List<string> ListOfComDLL { get; private set; }

        public ComRegInfo()
        {
            Clear();
        }

        public void Clear()
        {
            ListOfComDLL = new List<string>();

            _allRegComFiles = new Dictionary<string, ComFileInfo>(StringComparer.InvariantCultureIgnoreCase);
            _allFileComFiles = new Dictionary<string, ComFileInfo>(StringComparer.InvariantCultureIgnoreCase);
            _allRegComInterfaceProxyStub = new Dictionary<string, ComInterfaceProxyStubRegInfo>(StringComparer.InvariantCultureIgnoreCase);
            _allRegComInterfaceExternalProxyStub = new Dictionary<string, ComInterfaceExternalProxyStubRegInfo>(StringComparer.InvariantCultureIgnoreCase);
            _allRegComClasses = new Dictionary<string, ComClassRegInfo>(StringComparer.InvariantCultureIgnoreCase);
            _allRegFileNameEmptyClasses = new Dictionary<string, ComClassRegInfo>(StringComparer.InvariantCultureIgnoreCase);
        }

        public void ParseComInfo(TreeViewEx regTreeView)
        {
            Dictionary<string, ParseStage> listOfParseRegNodes = new Dictionary<string, ParseStage> {
                {@"SOFTWARE\Classes\CLSID",ParseStage.CLSID},
                {@"SOFTWARE\WOW6432Node\Classes\CLSID",ParseStage.CLSID},
                {@"SOFTWARE\Classes\WOW6432Node\CLSID" ,ParseStage.CLSID},
                {@"SOFTWARE\Classes\Interface",ParseStage.Interface},
                {@"SOFTWARE\WOW6432Node\Classes\Interface",ParseStage.Interface},
                {@"SOFTWARE\Classes\WOW6432Node\Interface" ,ParseStage.Interface},
                {@"SOFTWARE\Classes\TypeLib",ParseStage.TypeLib},
                {@"SOFTWARE\WOW6432Node\Classes\TypeLib",ParseStage.TypeLib},
                {@"SOFTWARE\Classes\WOW6432Node\TypeLib" ,ParseStage.TypeLib},
                {@"SOFTWARE\Classes",ParseStage.ProgId},
                {@"SOFTWARE\WOW6432Node\Classes",ParseStage.ProgId},
                {@"SOFTWARE\Classes\WOW6432Node" ,ParseStage.ProgId},
            };
            foreach (TreeNode item in regTreeView.Nodes)
            {
                foreach (var regNodePath in listOfParseRegNodes)
                {
                    switch (regNodePath.Value)
                    {
                        case ParseStage.CLSID:
                            {
                                var clsidNode = item.GetNode(regNodePath.Key);
                                if (clsidNode == null)
                                {
                                    continue;
                                }
                                foreach (TreeNode clsidItem in clsidNode.Nodes)
                                {
                                    ComClassRegInfo comClassRegInfo;
                                    string comFile;
                                    ParseComClass(clsidItem,out comClassRegInfo, out comFile);
                                    if (string.IsNullOrWhiteSpace(comFile))
                                    {
                                        if(!_allRegFileNameEmptyClasses.ContainsKey(comClassRegInfo.clsid))
                                        {
                                            _allRegFileNameEmptyClasses.Add(comClassRegInfo.clsid, comClassRegInfo);
                                        }
                                    }
                                    else
                                    {
                                        ListOfComDLL.Add(comFile);
                                        ComFileInfo fileInfo;
                                        if (_allRegComFiles.TryGetValue(comFile, out fileInfo))
                                        {
                                            if (!fileInfo.ComClassInfo.ContainsKey(comClassRegInfo.clsid))
                                            {
                                                fileInfo.ComClassInfo.Add(comClassRegInfo.clsid, comClassRegInfo);
                                            }
                                        }
                                        else
                                        {
                                            fileInfo = new ComFileInfo();
                                            fileInfo.ComClassInfo.Add(comClassRegInfo.clsid, comClassRegInfo);
                                            _allRegComFiles.Add(comFile, fileInfo);
                                        }

                                        if (!_allRegComClasses.ContainsKey(comClassRegInfo.clsid))
                                        {
                                            _allRegComClasses.Add(comClassRegInfo.clsid, comClassRegInfo);
                                        }
                                    }
                                }
                            }
                            break;
                        case ParseStage.Interface:
                            {
                                var interfaceNode = item.GetNode(regNodePath.Key);
                                if (interfaceNode == null)
                                {
                                    continue;
                                }
                                foreach (TreeNode interfaceNodeItem in interfaceNode.Nodes)
                                {
                                    ComInterfaceExternalProxyStubRegInfo externalProxyStub;
                                    ComInterfaceProxyStubRegInfo proxyStub;
                                    ParseInterface(interfaceNodeItem, out externalProxyStub, out proxyStub);
                                    if (!_allRegComInterfaceExternalProxyStub.ContainsKey(externalProxyStub.iid))
                                    {
                                        _allRegComInterfaceExternalProxyStub.Add(externalProxyStub.iid, externalProxyStub);
                                    }
                                    if (!_allRegComInterfaceProxyStub.ContainsKey(proxyStub.iid))
                                    {
                                        _allRegComInterfaceProxyStub.Add(proxyStub.iid, proxyStub);
                                    }
                                }
                            }
                            break;
                        case ParseStage.TypeLib:
                            {
                                var typeLibNode = item.GetNode(regNodePath.Key);
                                if (typeLibNode == null)
                                {
                                    continue;
                                }
                                foreach (TreeNode typeLibItem in typeLibNode.Nodes)
                                {
                                    Dictionary<string, ComTypeLibRegInfo> comClassRegInfoList;
                                    ParseTypeLib(typeLibItem, out comClassRegInfoList);
                                    foreach (var fileItem in comClassRegInfoList)
                                    {
                                        ComFileInfo fileInfo;
                                        if (_allRegComFiles.TryGetValue(fileItem.Key, out fileInfo))
                                        {
                                            if (!fileInfo.TypeLibInfo.ContainsKey(fileItem.Value.tlbid))
                                            {
                                                fileInfo.TypeLibInfo.Add(fileItem.Value.tlbid, fileItem.Value);
                                            }
                                        }
                                        //else
                                        //{
                                        //    fileInfo = new ComFileInfo();
                                        //    fileInfo.TypeLibInfo.Add(fileItem.Value.tlbid, fileItem.Value);
                                        //    _allComFiles.Add(fileItem.Key, fileInfo);
                                        //}
                                    }
                                }
                            }
                            break;
                        case ParseStage.ProgId:
                            {
                                var progIdNode = item.GetNode(regNodePath.Key);
                                if (progIdNode == null)
                                {
                                    continue;
                                }
                                foreach (TreeNode progIdItem in progIdNode.Nodes)
                                {
                                    string progid = progIdItem.Text;
                                    string clsid = progIdItem.GetNodeValue("CLSID");
                                    if ((!string.IsNullOrWhiteSpace(clsid)) && (!string.IsNullOrWhiteSpace(progid)))
                                    {
                                        ComClassRegInfo comclassInfo;
                                        if (_allRegComClasses.TryGetValue(clsid, out comclassInfo))
                                        {
                                            comclassInfo.AddProgId(progid);
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
            }

            foreach(var item in _allRegFileNameEmptyClasses)
            {
                foreach(var fileInfo in _allRegComFiles)
                {
                    if(fileInfo.Value.TypeLibInfo.ContainsKey(item.Value.tlbid))
                    {
                        if(!fileInfo.Value.ComClassInfo.ContainsKey(item.Value.clsid))
                        {
                            fileInfo.Value.ComClassInfo.Add(item.Value.clsid,item.Value);
                        }
                    }
                }
            }
        }

        public void UpdateTypeInfo(List<string> listOfComFile, List<string> allFiles)
        {
            foreach (var item in listOfComFile)
            {
                string filePath = allFiles.GetFilePath(item);
                if (File.Exists(filePath))
                {
                    string tlbFilePath = string.Format("{0}\\{1}.tlb", Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath));
                    ComFileInfo fileInfo;
                    string fileName = Path.GetFileName(filePath);
                    ITypeLib typeLib;
                    if(File.Exists(tlbFilePath))
                    {
                        typeLib = TypeLib.GetTypeLib(tlbFilePath);
                    }
                    else
                    {
                        typeLib = TypeLib.GetTypeLib(filePath);
                    }

                    if (typeLib == null)
                    {
                        if(_allRegComFiles.TryGetValue(fileName, out fileInfo))
                        {
                            fileInfo.name = fileName;
                            _allFileComFiles.Add(fileName, fileInfo);
                        }
                        continue;
                    }

                    fileInfo = new ComFileInfo();
                    fileInfo.name = fileName;
                    ComTypeLibRegInfo typeLibInfo = new ComTypeLibRegInfo();
                    _allFileComFiles.Add(fileInfo.name, fileInfo);

                    IntPtr typeLibAttribPtr;
                    typeLib.GetLibAttr(out typeLibAttribPtr);

                    if(typeLibAttribPtr != IntPtr.Zero)
                    {
                        var typeLibAttrib = Marshal.PtrToStructure<System.Runtime.InteropServices.ComTypes.TYPELIBATTR>(typeLibAttribPtr);

                        typeLibInfo.tlbid = typeLibAttrib.guid.ToString("B");
                        typeLibInfo.version = typeLibAttrib.wMajorVerNum.ToString();
                        typeLibInfo.flags = typeLibAttrib.wLibFlags.ToString();
                        typeLibInfo.helpdir = "";
                        string typelibname;
                        string description;
                        int dwHelpContext;
                        string helpStr;
                        typeLib.GetDocumentation(-1, out typelibname, out description, out dwHelpContext, out helpStr);
                        typeLibInfo.name = typelibname;
                        fileInfo.TypeLibInfo.Add(typeLibInfo.tlbid, typeLibInfo);
                    }

                    typeLib.ReleaseTLibAttr(typeLibAttribPtr);

                    int totalNoDefined = typeLib.GetTypeInfoCount();

                    for (int typeIndex = 0; typeIndex < totalNoDefined; typeIndex++)
                    {
                        ITypeInfo typeInfo;
                        typeLib.GetTypeInfo(typeIndex, out typeInfo);

                        IntPtr typeAttrPtr;
                        typeInfo.GetTypeAttr(out typeAttrPtr);

                        if(typeAttrPtr != IntPtr.Zero)
                        {
                            var typeAttr = Marshal.PtrToStructure<System.Runtime.InteropServices.ComTypes.TYPEATTR>(typeAttrPtr);
                            if(typeAttr.typekind == System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_COCLASS)
                            {
                                ComClassRegInfo coClassInfo;
                                string clsid = typeAttr.guid.ToString("B");
                                if (!_allRegComClasses.TryGetValue(clsid, out coClassInfo))
                                {
                                    coClassInfo = new ComClassRegInfo();
                                    coClassInfo.clsid = clsid;
                                }
                                string className;
                                string description;
                                int dwHelpContext;
                                string helpStr;
                                typeInfo.GetDocumentation(-1, out className, out description, out dwHelpContext, out helpStr);
                                coClassInfo.tlbid = typeLibInfo.tlbid;
                                coClassInfo.description = description;
                                coClassInfo.name = className;
                                if (string.IsNullOrWhiteSpace(coClassInfo.progid))
                                {
                                    coClassInfo.AddProgId(string.Format("{0}.{1}", typeLibInfo.name, className));
                                }
                                fileInfo.ComClassInfo.Add(clsid, coClassInfo);
                            }
                            else if(typeAttr.typekind == System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_INTERFACE ||
                                typeAttr.typekind == System.Runtime.InteropServices.ComTypes.TYPEKIND.TKIND_DISPATCH)
                            {
                                ComInterfaceExternalProxyStubRegInfo proxyStub;
                                string clsid = typeAttr.guid.ToString("B");
                                if (!_allRegComInterfaceExternalProxyStub.TryGetValue(clsid, out proxyStub))
                                {
                                    proxyStub = new ComInterfaceExternalProxyStubRegInfo();
                                    proxyStub.iid = clsid;
                                    if(typeAttr.wTypeFlags == System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FDUAL ||
                                        typeAttr.wTypeFlags == System.Runtime.InteropServices.ComTypes.TYPEFLAGS.TYPEFLAG_FOLEAUTOMATION)
                                    {
                                        //Just add std automation proxy
                                        proxyStub.proxyStubClsid32 = "{00020424-0000-0000-C000-000000000046}";
                                    }
                                    
                                }
                                string interfaceName;
                                string description;
                                int dwHelpContext;
                                string helpStr;
                                typeInfo.GetDocumentation(-1, out interfaceName,out description, out dwHelpContext, out helpStr);
                                proxyStub.tlbid = typeLibInfo.tlbid;
                                proxyStub.name = interfaceName;
                                fileInfo.InterfaceInfo.Add(clsid, proxyStub);
                            }

                        }

                        typeInfo.ReleaseTypeAttr(typeAttrPtr);
                    }
                }
            }
        }

        private void ParseTypeLib(TreeNode node, out Dictionary<string, ComTypeLibRegInfo> comClassRegInfoList)
        {
            comClassRegInfoList = new Dictionary<string, ComTypeLibRegInfo>(StringComparer.InvariantCultureIgnoreCase);
            foreach (TreeNode subItem in node.Nodes)
            {
                if (subItem.Tag == null)
                {
                    var comClassRegInfo = new ComTypeLibRegInfo();
                    comClassRegInfo.tlbid = node.Text;
                    comClassRegInfo.version = subItem.Text;
                    string typeLibFile = string.Empty;
                    foreach (TreeNode subSubItem in subItem.Nodes)
                    {
                        if (subSubItem.Text == "FLAGS")
                        {
                            comClassRegInfo.flags = subSubItem.GetNodeValue();
                        }
                        else if (subSubItem.Text == "HELPDIR")
                        {
                            comClassRegInfo.helpdir = subSubItem.GetNodeValue();
                        }
                        else
                        {
                            string tempFile = subSubItem.GetNodeValue("win32");
                            if (string.IsNullOrWhiteSpace(tempFile))
                            {
                                tempFile = subSubItem.GetNodeValue("win64");
                            }
                            int startPos = tempFile.LastIndexOf('\\');
                            if (startPos != -1)
                            {
                                typeLibFile = tempFile.Substring(startPos + 1);
                            }
                            else
                            {
                                typeLibFile = tempFile;
                            }
                        }
                    }

                    if (!comClassRegInfoList.ContainsKey(typeLibFile))
                    {
                        comClassRegInfoList.Add(typeLibFile, comClassRegInfo);
                    }
                }
            }
        }
        private void ParseInterface(TreeNode node, out ComInterfaceExternalProxyStubRegInfo externalProxyStub, out ComInterfaceProxyStubRegInfo proxyStub)
        {
            externalProxyStub = new ComInterfaceExternalProxyStubRegInfo();
            externalProxyStub.iid = node.Text;
            proxyStub = new ComInterfaceProxyStubRegInfo();
            proxyStub.iid = node.Text;
            externalProxyStub.name = node.GetNodeValue();
            proxyStub.name = externalProxyStub.name;
            externalProxyStub.proxyStubClsid32 = node.GetNodeValue("ProxyStubClsid32");
            proxyStub.proxyStubClsid32 = externalProxyStub.proxyStubClsid32;
            externalProxyStub.tlbid = node.GetNodeValue("TypeLib");
            proxyStub.tlbid = externalProxyStub.tlbid;
        }

        private void ParseComClass(TreeNode node, out ComClassRegInfo comClassRegInfo, out string comFile)
        {
            comClassRegInfo = new ComClassRegInfo();
            comClassRegInfo.clsid = node.Text;
            comFile = "";
            comClassRegInfo.description = node.GetNodeValue();
            var progId = node.GetNodeValue("ProgId");
            if(!string.IsNullOrWhiteSpace(progId))
            {
                comClassRegInfo.AddProgId(progId);
            }
            var inprocNode = node.GetNode("InprocServer32");
            if(inprocNode == null)
            {
                inprocNode = node.GetNode("InprocHandler32");
            }
            if (inprocNode != null)
            {
                foreach (TreeNode inprocItem in inprocNode.Nodes)
                {
                    Model.RegInfo regInfo = inprocItem.Tag as Model.RegInfo;
                    if (regInfo != null && regInfo.ValueName == "@")
                    {
                        string comFilePath = regInfo.Value.Replace(@"\\", @"\");
                        int startPos = comFilePath.LastIndexOf("\\");
                        if (startPos != -1)
                        {
                            comFilePath = comFilePath.Substring(startPos + 1);
                        }
                        comFile = comFilePath;
                    }
                    else if (regInfo != null && string.Compare(regInfo.ValueName, "ThreadingModel", true) == 0)
                    {
                        comClassRegInfo.threadingModel = regInfo.Value;
                    }
                }
            }
            else
            {
                comClassRegInfo.tlbid = node.GetNodeValue("TypeLib");
            }
        }

        public void GenerateServerManifest(string comServerPath)
        {
            string manifestFilePath = string.Format("{0}\\{1}.X.manifest", Path.GetDirectoryName(comServerPath), Path.GetFileNameWithoutExtension(comServerPath));
            string comfileName = Path.GetFileName(comServerPath);
            using (TextWriter writer = new StreamWriter(manifestFilePath,false,Encoding.UTF8))
            {
                writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                writer.WriteLine("<assembly xmlns=\"urn:schemas-microsoft-com:asm.v1\" manifestVersion=\"1.0\">");
                writer.WriteLine("<assemblyIdentity type=\"win32\" name=\"{0}.X\" version=\"1.0.0.0\"/>",Path.GetFileNameWithoutExtension(comServerPath));
                foreach (var fileInfo in _allFileComFiles)
                {
                    if (comfileName.EndsWith(fileInfo.Key,StringComparison.InvariantCultureIgnoreCase))
                    {
                        writer.WriteLine("\t<file name=\"{0}\">", fileInfo.Key);

                        foreach (var item in fileInfo.Value.ComClassInfo)
                        {
                            var outputStr = new StringBuilder();
                            outputStr.AppendFormat("\t\t<comClass clsid=\"{0}\"", item.Value.clsid);
                            if(!string.IsNullOrWhiteSpace(item.Value.tlbid))
                            {
                                outputStr.AppendFormat(" tlbid=\"{0}\"", item.Value.tlbid);
                            }
                            if (!string.IsNullOrWhiteSpace(item.Value.description))
                            {
                                outputStr.AppendFormat(" description=\"{0}\"", item.Value.description);
                            }
                            if (!string.IsNullOrWhiteSpace(item.Value.progid))
                            {
                                outputStr.AppendFormat(" progid=\"{0}\"", item.Value.progid);
                            }
                            if (!string.IsNullOrWhiteSpace(item.Value.threadingModel))
                            {
                                outputStr.AppendFormat(" threadingModel=\"{0}\"", item.Value.threadingModel);
                            }
                            outputStr.Append("></comClass>");
                            writer.WriteLine(outputStr.ToString());
                        }

                        foreach (var item in fileInfo.Value.TypeLibInfo)
                        {
                            var outputStr = new StringBuilder();
                            outputStr.AppendFormat("\t\t<typelib tlbid=\"{0}\"", item.Value.tlbid);
                            if (!string.IsNullOrWhiteSpace(item.Value.version))
                            {
                                outputStr.AppendFormat(" version=\"{0}\"", item.Value.version);
                            }
                            outputStr.AppendFormat(" helpdir=\"\"");
                            if (!string.IsNullOrWhiteSpace(item.Value.flags))
                            {
                                outputStr.AppendFormat(" flags=\"{0}\"", item.Value.flags);
                            }
                            outputStr.Append("></typelib>");
                            writer.WriteLine(outputStr.ToString());
                        }

                        writer.WriteLine("\t</file>");

                        foreach (var item in fileInfo.Value.InterfaceInfo)
                        {
                            var outputStr = new StringBuilder();
                            outputStr.AppendFormat("\t<comInterfaceExternalProxyStub iid=\"{0}\"", item.Value.iid);
                            if (!string.IsNullOrWhiteSpace(item.Value.name))
                            {
                                outputStr.AppendFormat(" name=\"{0}\"", item.Value.name);
                            }
                            if (!string.IsNullOrWhiteSpace(item.Value.proxyStubClsid32))
                            {
                                outputStr.AppendFormat(" proxyStubClsid32=\"{0}\"", item.Value.proxyStubClsid32);
                            }
                            if (!string.IsNullOrWhiteSpace(item.Value.tlbid))
                            {
                                outputStr.AppendFormat(" tlbid=\"{0}\"", item.Value.tlbid);
                            }
                            outputStr.Append("></comInterfaceExternalProxyStub>");
                            writer.WriteLine(outputStr.ToString());
                        }
                        break;
                    }
                }
                writer.WriteLine("</assembly>");
            }
        }

        public void GenerateClientManifest(string exeFilePath, List<string> listOfComfiles)
        {
            string manifestfile = string.Format("{0}.manifest", exeFilePath);
            using (TextWriter writer = new StreamWriter(manifestfile, false, Encoding.UTF8))
            {
                writer.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>");
                writer.WriteLine("<assembly xmlns=\"urn:schemas-microsoft-com:asm.v1\" manifestVersion=\"1.0\">");

                writer.WriteLine("\t<assemblyIdentity type=\"win32\" name=\"{0}\" version=\"1.0.0.0\" />", Path.GetFileNameWithoutExtension(exeFilePath));
                foreach (var item in listOfComfiles)
                {
                    writer.WriteLine("\t<dependency>");
                    writer.WriteLine("\t\t<dependentAssembly>");
                    writer.WriteLine("\t\t\t<assemblyIdentity type=\"win32\" name=\"{0}.X\" version=\"1.0.0.0\" />",Path.GetFileNameWithoutExtension(item));
                    writer.WriteLine("\t\t</dependentAssembly>");
                    writer.WriteLine("\t</dependency>");
                }
                writer.WriteLine("</assembly>");
            }
        }
    }
}
