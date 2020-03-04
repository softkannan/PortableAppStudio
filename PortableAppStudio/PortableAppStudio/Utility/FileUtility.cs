using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace PortableAppStudio.Utility
{
    public class FileUtility
    {
        private static FileUtility _inst = null;

        public static FileUtility Inst
        {
            get
            {
                if(_inst == null)
                {
                    _inst = new FileUtility();
                }
                return _inst;
            }
        }

        [Flags]
        public enum EFileAccess : uint
        {
            GenericRead = 0x80000000,
            GenericWrite = 0x40000000,
            GenericExecute = 0x20000000,
            GenericAll = 0x10000000,
        }

        [Flags]
        public enum EFileShare : uint
        {
            None = 0x00000000,
            Read = 0x00000001,
            Write = 0x00000002,
            Delete = 0x00000004,
        }

        public enum ECreationDisposition : uint
        {
            New = 1,
            CreateAlways = 2,
            OpenExisting = 3,
            OpenAlways = 4,
            TruncateExisting = 5,
        }

        [Flags]
        public enum EFileAttributes : uint
        {
            Readonly = 0x00000001,
            Hidden = 0x00000002,
            System = 0x00000004,
            Directory = 0x00000010,
            Archive = 0x00000020,
            Device = 0x00000040,
            Normal = 0x00000080,
            Temporary = 0x00000100,
            SparseFile = 0x00000200,
            ReparsePoint = 0x00000400,
            Compressed = 0x00000800,
            Offline = 0x00001000,
            NotContentIndexed = 0x00002000,
            Encrypted = 0x00004000,
            Write_Through = 0x80000000,
            Overlapped = 0x40000000,
            NoBuffering = 0x20000000,
            RandomAccess = 0x10000000,
            SequentialScan = 0x08000000,
            DeleteOnClose = 0x04000000,
            BackupSemantics = 0x02000000,
            PosixSemantics = 0x01000000,
            OpenReparsePoint = 0x00200000,
            OpenNoRecall = 0x00100000,
            FirstPipeInstance = 0x00080000
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern SafeFileHandle CreateFile(string lpFileName, EFileAccess dwDesiredAccess, EFileShare dwShareMode, IntPtr lpSecurityAttributes, ECreationDisposition dwCreationDisposition, EFileAttributes dwFlagsAndAttributes, IntPtr hTemplateFile);

        public FileStream CreateNew(string fileName)
        {
            // Create a file with generic write access
            SafeFileHandle fileHandle = CreateFile(fileName, EFileAccess.GenericWrite, EFileShare.None, IntPtr.Zero, 
                ECreationDisposition.CreateAlways, 0, IntPtr.Zero);

            // Check for errors
            int lastWin32Error = Marshal.GetLastWin32Error();
            if (fileHandle.IsInvalid)
            {
                throw new System.ComponentModel.Win32Exception(lastWin32Error);
            }

            // Pass the file handle to FileStream. FileStream will close the
            // handle
            return new FileStream(fileHandle, FileAccess.Write);
        }

        public void UpdateAll(string folderName)
        {
            string[] files = System.IO.Directory.GetFiles(folderName, "*.*", SearchOption.AllDirectories);
            foreach (var filePath in files)
            {
                string decodedPath = HttpUtility.UrlDecode(filePath);
                if (decodedPath.ToLower() != filePath.ToLower())
                {
                    string directoryName = Path.GetDirectoryName(decodedPath);
                    if(!Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }
                    File.Move(filePath, decodedPath);
                }
            }

            RemoveEmptyDir(folderName);
        }

        public void RemoveEmptyDir(string folderName)
        {
            foreach (var directory in Directory.GetDirectories(folderName))
            {
                RemoveEmptyDir(directory);
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                {
                    Directory.Delete(directory, false);
                }
            }
        }

        public void CopyAllFix(string sourcePath, string destinationPath)
        {
            string[] directories = System.IO.Directory.GetDirectories(sourcePath, "*.*", SearchOption.AllDirectories);
            foreach (var srcDirPath in directories)
            {
                string tempDestPath = Uri.UnescapeDataString(srcDirPath.Replace(sourcePath, destinationPath));
                Directory.CreateDirectory(tempDestPath);
            }
            string[] files = System.IO.Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);
            foreach(var srcFilePath in files)
            {
                string tempDestPath = Uri.UnescapeDataString(srcFilePath.Replace(sourcePath, destinationPath));
                File.Copy(srcFilePath, tempDestPath, true);
            }
        }

        public void CopyAll(string sourcePath, string destinationPath)
        {

            string[] directories = System.IO.Directory.GetDirectories(sourcePath, "*.*", SearchOption.AllDirectories);

            Parallel.ForEach(directories, dirPath =>
            {
                Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));
            });

            string[] files = System.IO.Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);

            Parallel.ForEach(files, newPath =>
            {
                File.Copy(newPath, newPath.Replace(sourcePath, destinationPath),true);
            });
        }

        /// <summary>
        /// Determines a text file's encoding by analyzing its byte order mark (BOM).
        /// Defaults to ASCII when detection of the text file's endianness fails.
        /// </summary>
        /// <param name="filename">The text file to analyze.</param>
        /// <returns>The detected encoding.</returns>
        public Encoding GetEncoding(string filename)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
            //Just hack to determine if the text file don't declare BOM, this works for english language only
            if (bom[1] == 0 && bom[3] == 0) return Encoding.Unicode;
            return Encoding.ASCII;
        }

        public List<string> GetFileLines(string fileName)
        {
            List<string> retVal = new List<string>();

            if (string.IsNullOrWhiteSpace(fileName) || (!File.Exists(fileName)))
            {
                return retVal;
            }

            List<string> rawLines = new List<string>();
            var encoding = GetEncoding(fileName);
            using (TextReader reader = new StreamReader(fileName, encoding))
            {
                string item;
                while ((item = reader.ReadLine()) != null)
                {
                    retVal.Add(item);
                }
            }

            return retVal;
        }
    }
}
