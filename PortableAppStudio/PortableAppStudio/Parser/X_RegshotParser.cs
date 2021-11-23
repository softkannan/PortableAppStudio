using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortableAppStudio.Parser
{
    public class X_RegshotParser : RegFileParser
    {
        WebBrowser _wb;
        string _rawFilesText;
        string _rawRegKeysText;
        ApplicationContext _appContext = new ApplicationContext();
        ManualResetEvent _jobCompleted = new ManualResetEvent(false);

        public override void Parse(string fileName)
        {
            this.ParserType = RegSourceType.X_RegShot;
            ParseInternal(fileName);
        }

        private void ParseFiles()
        {
            var rawFilesList = new List<string>(_rawFilesText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries));

            var fileHeader = new Regex(@"(?<Action>[a-zA-Z]+)\s+files\s+\((?<NoOfRecord>\d+)\)", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);

            int lastSegmentLength = 0;
            int segmentIndex = 0;
            bool addFile = false;
            foreach (var item in rawFilesList)
            {
                var tempVal = item.Trim();
                if (string.IsNullOrWhiteSpace(tempVal))
                {
                    continue;
                }

                if (segmentIndex < lastSegmentLength)
                {
                    if (addFile)
                    {
                        AddFile(string.Format(@"\\?\{0}",tempVal));
                    }
                    segmentIndex++;
                }
                else
                {
                    addFile = false;
                    segmentIndex = 0;
                    lastSegmentLength = 0;
                    var match = fileHeader.Match(tempVal);
                    if (match != null && match.Success)
                    {
                        switch (match.Groups["Action"].Value)
                        {
                            case "Deleted":
                                lastSegmentLength = int.Parse(match.Groups["NoOfRecord"].Value);
                                break;
                            case "New":
                                lastSegmentLength = int.Parse(match.Groups["NoOfRecord"].Value);
                                addFile = true;
                                break;
                            case "Changed":
                                lastSegmentLength = int.Parse(match.Groups["NoOfRecord"].Value);
                                break;
                        }
                    }
                }
            }
        }


        private void ParseInternal(string folderName)
        {
            var regReDoFiles = Directory.GetFiles(folderName, "*.Redo.reg");
            if (regReDoFiles != null && regReDoFiles.Length > 0)
            {
                ParseTextRegFile(regReDoFiles.FirstOrDefault());
            }
            var htmlFiles = Directory.GetFiles(folderName, "*.html");
            if (htmlFiles != null && htmlFiles.Length > 0)
            {
                LoadData(htmlFiles.FirstOrDefault());
                //ParseRegKeys();
                ParseFiles();
            }
        }

        private void LoadData(string fileName)
        {
            if(string.IsNullOrWhiteSpace(fileName) || (!File.Exists(fileName)))
            {
                return;
            }

            var thread = Utility.TaskUtility.StartSTAThread(() =>
            {
                _wb = new WebBrowser();
                _wb.Navigate(fileName);
                _wb.DocumentCompleted += Wb_DocumentCompleted;
                Application.Run(_appContext);
            });
            _jobCompleted.WaitOne();
        }

        private void Wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var doc = _wb.Document;
            var fileElem = doc.GetElementById("file");
            _rawFilesText = fileElem.InnerText;
            var regElem = doc.GetElementById("hive");
            _rawRegKeysText = regElem.InnerText; 
            _jobCompleted.Set();
            _appContext.ExitThread();
        }
    }
}
