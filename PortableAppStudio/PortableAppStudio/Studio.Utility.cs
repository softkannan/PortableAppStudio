using PortableAppStudio.Controls;
using PortableAppStudio.Dialogs;
using PortableAppStudio.Model.AppInfoINI;
using PortableAppStudio.Model.FolderLayout;
using PortableAppStudio.Model.LaunchINI;
using PortableAppStudio.Parser;
using PortableAppStudio.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio
{
    partial class Studio
    {
        private List<TreeNode> GetDropData(DragEventArgs e)
        {
            return (List<TreeNode>)e?.Data?.GetData(TreeNode_Drop_DataType);
        }

        private TreeViewEx GetDropSourceTree(DragEventArgs e)
        {
            var tempDropData = GetDropData(e);
            return tempDropData?.FirstOrDefault()?.TreeView as TreeViewEx;
        }

        
        private void ImportSourceFiles(ParserBase importParser, string fileOrFolderName)
        {
            ErrorLog.Inst.WriteStatus("Importing Source Files...");
            var progressBar = ProgressDialog.Run(this, "Importing Source Files and Registry Entries ...",
                    "Importing Source Files and Registry Entries From \"{0}\"", fileOrFolderName);
            try
            {
                importParser.Parse(string.Format(@"\\?\{0}",fileOrFolderName));

                this.SuspendLayout();

                importParser.PopulateSourceTreeView(sourceFilesTree, sourceRegTree);

                this.ResumeLayout();

            }
            catch (Exception ex)
            {
                ErrorLog.Inst.ShowError("Source Files Import Error : {0}", ex.Message);
            }
            finally
            {
                progressBar.ShutDown();
            }
            ErrorLog.Inst.WriteStatus("Source Files and Registry Entries Import Completed");
        }

        // Updates all child tree nodes recursively.
        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }
        private void GenerateDebugOptions()
        {
            string debugFile = PortableApp.Inst.App.Launch.DebugFile;
            if (File.Exists(debugFile))
            {
                try { File.Delete(debugFile); } catch (Exception) { }
            }
            StringBuilder debugOutput = new StringBuilder();
            if (DEBUG_ALL_Check.Checked)
            {
                debugOutput.AppendLine("!define DEBUG_ALL");
                debugOutput.AppendLine();
            }
            if (DEBUG_GLOBAL_Check.Checked)
            {
                debugOutput.AppendLine("!define DEBUG_GLOBAL");
                debugOutput.AppendLine();
            }
            if (DEBUG_SEGWRAP_Check.Checked)
            {
                debugOutput.AppendLine("!define DEBUG_SEGWRAP");
                debugOutput.AppendLine();
            }
            string selectedItem = DEBUG_OUTPUT_Combo.SelectedItem as string;
            if (selectedItem == "file")
            {
                debugOutput.AppendLine("!define DEBUG_OUTPUT file");
                debugOutput.AppendLine();
            }
            else if (selectedItem == "messagebox")
            {
                debugOutput.AppendLine("!define DEBUG_OUTPUT messagebox");
                debugOutput.AppendLine();
            }

            foreach (ListViewItem item in DEBUG_SEGMENT_List.SelectedItems)
            {
                if (item.Checked)
                {
                    debugOutput.AppendLine(string.Format("!define {0}", item.Text));
                }
            }

            if (debugOutput.Length > 0)
            {
                using (var writer = new StreamWriter(debugFile,false))
                {
                    writer.Write(debugOutput.ToString());
                }
                //using (var writer = FileUtility.Inst.CreateNew(debugFile))
                //{
                //    var textfileData = Encoding.Unicode.GetBytes(debugOutput.ToString());
                //    writer.Write(textfileData,0, textfileData.Length);
                //}
            }
        }

        private void SaveInternal()
        {
            appFilesTree.SuspendLayout();
            appRegTree.SuspendLayout();

            ErrorLog.Inst.WriteStatus("Saving Portable App...");
            var progressBar = ProgressDialog.Run(this, "Saving Portable App ...", "Saving Portable App to \"{0}\"", PortableApp.Inst.RootFolder);

            try
            {
                if (PortableApp.Inst.IsAlreadyOpen)
                {
                    PortableApp.Inst.App.IsGenerateRegFile = generateRegFilesCheck.Checked;
                    PortableApp.Inst.App.IsGenerateManifest = generateManifestChk.Checked;
                    PortableApp.Inst.Save();
                    GenerateDebugOptions();
                    Thread.Sleep(1000);
                }

                PortableApp.Inst.Reload();
            }
            catch (Exception ex)
            {
                ErrorLog.Inst.ShowError("App Save Error : {0}", ex.Message);
            }
            finally
            {
                progressBar.ShutDown();

                appFilesTree.ResumeLayout();
                appRegTree.ResumeLayout();
            }

            ErrorLog.Inst.WriteStatus("Save Completed");
        }

        private void ClosePortableApp()
        {
            if (PortableApp.Inst.IsValueModified)
            {
                if(ErrorLog.Inst.ShowYesNo("Are you want to save the portable app ?"))
                {
                    SaveInternal();
                }
            }

            PortableApp.Inst.Close();
        }
        private void LoadPortableApp(string portableAppFolder, bool existing)
        {
            ErrorLog.Inst.WriteStatus("Opening Portable App...");
            var progressBar = ProgressDialog.Run(this, "Opening Portable App ...", "Opening Portable App from \"{0}\"", portableAppFolder);
            try
            {
                ClosePortableApp();
                PortableApp.Inst.LoadFolder(string.Format(@"\\?\{0}", portableAppFolder), existing);
                UpdateDynamicIntelisense();
            }
            catch(Exception ex)
            {
                ErrorLog.Inst.ShowError("App Open Error : {0}", ex.Message);
            }
            finally
            {
                progressBar.ShutDown();
            }
            ErrorLog.Inst.WriteStatus("Ready");
        }

        public INIValueEditForm GetEditForm(string formName)
        {
            switch (formName)
            {
                case LaunchINI.FilesMove_Tag:
                    return new FilesMoveForm();
                case LaunchINI.DirectoriesCleanupForce_Tag:
                    return new DirectoriesCleanupForceForm();
                case LaunchINI.DirectoriesCleanupIfEmpty_Tag:
                    return new DirectoriesCleanupIfEmptyForm();
                case LaunchINI.DirectoriesMove_Tag:
                    return new DirectoriesMoveForm();
                case LaunchINI.RegistryCleanupForce_Tag:
                    return new RegistryCleanupForceForm();
                case LaunchINI.RegistryCleanupIfEmpty_Tag:
                    return new RegistryCleanupIfEmptyForm();
                case LaunchINI.RegistryKeys_Tag:
                    return new RegistryKeysForm();
                case LaunchINI.RegistryValueBackupDelete_Tag:
                    return new RegistryValueBackupDeleteForm();
                case LaunchINI.RegistryValueWrite_Tag:
                    return new RegistryValueWriteForm();
                case LaunchINI.RegistrationFreeCOM_Tag:
                    return new RegistrationFreeCOMForm();
                case LaunchINI.QtKeysCleanup_Tag:
                    return new QtKeysCleanupForm();
                case LaunchINI.Environment_Tag:
                    return new EnvironmentForm();
                case LaunchINI.DirectoriesLink_Tag:
                    return new DirectoriesLinkForm();
                default:
                    return new FolderNameForm();
            }
        }

        private List<string> GetComDLLFiles(TreeViewEx sourceRegTree)
        {
            List<string> dllNames = new List<string>();
            Model.COM.ComRegInfo.Inst.Clear();
            Model.COM.ComRegInfo.Inst.ParseComInfo(sourceRegTree);
            dllNames = Model.COM.ComRegInfo.Inst.ListOfComDLL;
            return dllNames.Distinct().ToList();
        }
    }
}
