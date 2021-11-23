using PortableAppStudio.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Dialogs
{
    public partial class FileWriteNForm : INIValueEditForm
    {
        public FileWriteNForm()
        {
            InitializeComponent();

            typeComboBox.Items.AddRange(new string[]{"ConfigWrite","INI","Replace","XML attribute","XML text"});
            caseSensitiveComboBox.Items.AddRange(new string[] { "", "true", "false" });
            encodingComboBox.Items.AddRange(new string[] { "", "auto", "ANSI", "UTF-16LE" });

            typeComboBox.KeyDown += EditComboBox_KeyDown;
            caseSensitiveComboBox.KeyDown += EditComboBox_KeyDown;
            encodingComboBox.KeyDown += EditComboBox_KeyDown;
            fileComboBox.KeyDown += EditComboBox_KeyDown;
            entryComboBox.KeyDown += EditComboBox_KeyDown;
            sectionComboBox.KeyDown += EditComboBox_KeyDown;
            keyComboBox.KeyDown += EditComboBox_KeyDown;
            valueComboBox.KeyDown += EditComboBox_KeyDown;
            findComboBox.KeyDown += EditComboBox_KeyDown;
            replaceComboBox.KeyDown += EditComboBox_KeyDown;
            attributeComboBox.KeyDown += EditComboBox_KeyDown;
            xpathComboBox.KeyDown += EditComboBox_KeyDown;

            typeComboBox.Tag = "Type";
            caseSensitiveComboBox.Tag = "CaseSensitive";
            encodingComboBox.Tag = "Encoding";
            fileComboBox.Tag = "File";
            entryComboBox.Tag = "Entry";
            sectionComboBox.Tag = "Section";
            keyComboBox.Tag = "Key";
            valueComboBox.Tag = "Value";
            findComboBox.Tag = "Find";
            replaceComboBox.Tag = "Replace";
            attributeComboBox.Tag = "Attribute";
            xpathComboBox.Tag = "XPath";

            this.Load += FileWriteNForm_Load;
            bttnOk.Click += BttnOk_Click;

            fileComboBox.AddList(Model.OtherFileListStringConverter.OtherList);
            fileComboBox.AutoCompleteCustomSource = Model.InteliSense.Inst.EnvVaraibles;

            valueComboBox.AddList(Model.InteliSense.Inst.EnvVaraibles);
            valueComboBox.AutoCompleteCustomSource = Model.InteliSense.Inst.EnvVaraibles;
            findComboBox.AddList(Model.InteliSense.Inst.EnvVaraibles);
            findComboBox.AutoCompleteCustomSource = Model.InteliSense.Inst.EnvVaraibles;
            replaceComboBox.AddList(Model.InteliSense.Inst.EnvVaraibles);
            replaceComboBox.AutoCompleteCustomSource = Model.InteliSense.Inst.EnvVaraibles;

            helpTextBox.Rtf = PathManager.Init.GetHelpData("FileWriteN");
        }

        private void FileWriteNForm_Load(object sender, EventArgs e)
        {
            UpdateUI(SelectedSection);
        }

        public Model.LaunchINI.FileWriteNSection SelectedSection { get; set; }

        private void BttnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            UpdateObject(SelectedSection);
            this.Close();
        }

        private void UpdateUI(Model.LaunchINI.FileWriteNSection obj)
        {
            if(obj == null)
            {
                return;
            }

            typeComboBox.Text = obj.Type;
            caseSensitiveComboBox.Text = obj.CaseSensitive == null ? "" : obj.CaseSensitive.ToString().ToLower();
            encodingComboBox.Text = obj.Encoding;
            fileComboBox.Text = obj.File;
            entryComboBox.Text = obj.Entry;
            sectionComboBox.Text = obj.Section;
            keyComboBox.Text = obj.Key;
            valueComboBox.Text = obj.Value;
            findComboBox.Text = obj.Find;
            replaceComboBox.Text = obj.Replace;
            attributeComboBox.Text = obj.Attribute;
            xpathComboBox.Text = obj.XPath;
        }

        private void UpdateObject(Model.LaunchINI.FileWriteNSection obj)
        {
            if (obj == null)
            {
                return;
            }

            obj.Type = typeComboBox.Text;
            var tempVal = caseSensitiveComboBox.Text;
            if (string.IsNullOrWhiteSpace(tempVal))
            {
                obj.CaseSensitive = null;
            }
            else
            {
                if(bool.TryParse(tempVal,out bool result))
                {
                    obj.CaseSensitive = result;
                }
                else
                {
                    obj.CaseSensitive = null;
                }
            }
            obj.Encoding = encodingComboBox.Text;
            obj.File = fileComboBox.Text;
            obj.Entry = entryComboBox.Text;
            obj.Section = sectionComboBox.Text;
            obj.Key = keyComboBox.Text;
            obj.Value = valueComboBox.Text;
            obj.Find = findComboBox.Text;
            obj.Replace = replaceComboBox.Text;
            obj.Attribute = attributeComboBox.Text;
            obj.XPath = xpathComboBox.Text;
        }

        private void ShowHelp(RichTextBox textBox, string helpName, string selectedProprty)
        {
            string helpNameLocal = helpName.RemoveStartEnd('[', ']');
            var tempObj = PathManager.Init.GetHelpData(helpNameLocal);
            if (string.IsNullOrWhiteSpace(tempObj))
            {
                textBox.Text = "";
            }
            else
            {
                textBox.Rtf = tempObj;
                if (!string.IsNullOrWhiteSpace(selectedProprty))
                {
                    NavigateHelp(textBox, selectedProprty);
                }
            }
        }

        private void NavigateHelp(RichTextBox textBox, string helpName)
        {
            var selectedText = textBox.Text;
            if (!string.IsNullOrEmpty(selectedText))
            {
                Regex helpMatch = new Regex(string.Format("^{0}\\s*$", helpName), RegexOptions.Multiline | RegexOptions.CultureInvariant);
                var match = helpMatch.Match(selectedText);
                if (match != null && match.Success)
                {
                    int matchIndex = match.Index;
                    textBox.Select(matchIndex, 0);
                    textBox.ScrollToCaret();
                    //containerGroupBox.Select();
                }
                else
                {
                    textBox.Select(1, 0);
                    textBox.ScrollToCaret();
                    //containerGroupBox.Select();
                }
            }
        }

        private void EditComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F1 && sender is ComboBox senderCombo)
            {
                ShowHelp(helpTextBox, "[FileWriteN]", senderCombo.Tag as string);
                e.Handled = true;
            }
        }
    }
}
