using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace PortableAppStudio.Utility.FileUtil
{
    public class DirectoryInfoEx
    {
        private const string LONG_PATH_PREFIX = @"\\?\";
        private const int ERROR_ACCESS_DENIED = 0x5;
        private const int ERROR_BAD_PATHNAME = 0xA1;

        static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        private readonly string _folderName = string.Empty;

        public string FullName { get => _folderName; }
        public string HumanFullName { get => ToHumanFrientlyPath(_folderName); }
        public string Name { get; private set; }

        public DirectoryInfoEx(string folderName)
        {
            _folderName = folderName;
            Name = Path.GetFileName(folderName);
        }

        public int CreateDirectory(out string errorMessage)
        {
            return CreateDirectory(_folderName, out errorMessage);
        }

        public static int CreateDirectory(string path, out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                errorMessage = string.Format("\"{0}\" {1}", path, GetErrorMessage(ERROR_BAD_PATHNAME));
                return ERROR_BAD_PATHNAME;
            }
            var paths = ToList(ToWin32LongPath(path));
            foreach (var item in paths)
            {
                if (!LongExists(item))
                {
                    var ok = NativeMethods.CreateDirectoryW(item, IntPtr.Zero);
                    if (!ok)
                    {
                        int code = Marshal.GetLastWin32Error();
                        errorMessage = string.Format("{0}", GetErrorMessage(code));
                        return code;
                    }
                }
            }
            errorMessage = "";
            return 0;
        }

        public void Delete(bool recursive = false)
        {
            Delete(_folderName, recursive);
        }

        public static void Delete(string path)
        {
            Delete(path, false);
        }

        public static void Delete(string path, bool recursive)
        {
            if (!recursive)
            {
                bool ok = NativeMethods.RemoveDirectoryW(ToWin32LongPath(path));
                if (!ok) ThrowWin32Exception();
            }
            else
            {
                DeleteDirectories(new List<DirectoryInfoEx> { new DirectoryInfoEx(ToWin32LongPath(path)) });
            }
        }


        private static void DeleteDirectories(List<DirectoryInfoEx> directories)
        {
            foreach (var directory in directories)
            {
                var files = DirectoryInfoEx.GetFilesInternal(directory.FullName, null, System.IO.SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    FileInfoEx.Delete(file.FullName);
                }
                List<DirectoryInfoEx> dirs = new List<DirectoryInfoEx>();
                DirectoryInfoEx.InternalGetDirectories(directory.FullName, null, System.IO.SearchOption.TopDirectoryOnly, ref dirs);
                DeleteDirectories(dirs);
                bool ok = NativeMethods.RemoveDirectoryW(ToWin32LongPath(directory.FullName));
                if (!ok) ThrowWin32Exception();
            }
        }

        public bool Exists()
        {
            return Exists(_folderName);
        }

        public static bool Exists(string path)
        {
            var localPath = ToWin32LongPath(path);
            return LongExists(localPath);
        }

        private static bool LongExists(string path)
        {
            var attr = NativeMethods.GetFileAttributesW(path);
            return (attr != NativeMethods.INVALID_FILE_ATTRIBUTES && ((attr & NativeMethods.FILE_ATTRIBUTE_DIRECTORY) == NativeMethods.FILE_ATTRIBUTE_DIRECTORY));
        }


        public List<DirectoryInfoEx> GetDirectories(string searchPattern = "*", System.IO.SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var dirs = new List<DirectoryInfoEx>();
            InternalGetDirectories(_folderName, searchPattern, searchOption, ref dirs);
            return dirs;
        }

        public static List<DirectoryInfoEx> GetDirectories(string folderName, string searchPattern = "*", System.IO.SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            var dirs = new List<DirectoryInfoEx>();
            InternalGetDirectories(folderName, searchPattern, searchOption, ref dirs);
            return dirs;
        }

        private static void InternalGetDirectories(string path, string searchPattern, System.IO.SearchOption searchOption, ref List<DirectoryInfoEx> dirs)
        {
            NativeMethods.WIN32_FIND_DATA findData;
            string longPath = ToWin32LongPath(path);
            string dirPattern = System.IO.Path.Combine(longPath, searchPattern);
            //IntPtr findHandle = NativeMethods.FindFirstFile(dirPattern, out findData);
            IntPtr findHandle = NativeMethods.FindFirstFileExW(dirPattern, NativeMethods.FINDEX_INFO_LEVELS.FindExInfoBasic, out findData, NativeMethods.FINDEX_SEARCH_OPS.FindExSearchNameMatch, IntPtr.Zero,
                 NativeMethods.FIND_FIRST_EX_LARGE_FETCH);
            if (findHandle != INVALID_HANDLE_VALUE)
            {
                do
                {
                    if ((findData.dwFileAttributes & System.IO.FileAttributes.Directory) == System.IO.FileAttributes.Directory)
                    {
                        if (string.Compare(findData.cFileName, ".", StringComparison.Ordinal) != 0 &&
                            string.Compare(findData.cFileName, "..", StringComparison.Ordinal) != 0)
                        {
                            string subdirectory = System.IO.Path.Combine(path, findData.cFileName);
                            dirs.Add(new DirectoryInfoEx(subdirectory));
                            if (searchOption == SearchOption.AllDirectories)
                            {
                                InternalGetDirectories(subdirectory, searchPattern, searchOption, ref dirs);
                            }
                        }
                    }
                } while (NativeMethods.FindNextFileW(findHandle, out findData));
                NativeMethods.FindClose(findHandle);
            }
            else
            {
                int code = Marshal.GetLastWin32Error();
                string errorMessage = string.Format("\"{0}\"", GetErrorMessage(code));
                //ThrowWin32Exception();
            }
        }

        public List<FileInfoEx> GetFiles(string searchPattern = "*", System.IO.SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return GetFilesInternal(ToWin32LongPath(_folderName), searchPattern, searchOption);
        }

        public static List<FileInfoEx> GetFiles(string folderName, string searchPattern = "*", System.IO.SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return GetFilesInternal(ToWin32LongPath(folderName), searchPattern, searchOption);
        }


        private static List<FileInfoEx> GetFilesInternal(string path, string searchPattern, System.IO.SearchOption searchOption)
        {
            searchPattern = searchPattern ?? "*";

            var files = new List<FileInfoEx>();
            var dirs = new List<DirectoryInfoEx> { new DirectoryInfoEx(path) };

            if (searchOption == SearchOption.AllDirectories)
            {
                //Add all the subpaths
                dirs.AddRange(GetDirectories(path, searchPattern, SearchOption.AllDirectories));
            }

            foreach (var dir in dirs)
            {
                NativeMethods.WIN32_FIND_DATA findData;
                var filePattern = System.IO.Path.Combine(dir.FullName, searchPattern);
                IntPtr findHandle = NativeMethods.FindFirstFileExW(filePattern, NativeMethods.FINDEX_INFO_LEVELS.FindExInfoBasic, out findData, NativeMethods.FINDEX_SEARCH_OPS.FindExSearchNameMatch, IntPtr.Zero,
                NativeMethods.FIND_FIRST_EX_LARGE_FETCH);
                try
                {
                    if (findHandle != INVALID_HANDLE_VALUE)
                    {
                        do
                        {
                            if ((findData.dwFileAttributes & System.IO.FileAttributes.Directory) != System.IO.FileAttributes.Directory)
                            {
                                string filename = System.IO.Path.Combine(dir.FullName, findData.cFileName);
                                files.Add(new FileInfoEx(filename));
                            }
                        } while (NativeMethods.FindNextFileW(findHandle, out findData));
                        NativeMethods.FindClose(findHandle);
                    }
                }
                catch (Exception)
                {
                    NativeMethods.FindClose(findHandle);
                    throw;
                }
            }

            return files;
        }

        #region Helper methods



        [DebuggerStepThrough]
        public static void ThrowWin32Exception()
        {
            int code = Marshal.GetLastWin32Error();
            if (code != 0)
            {
                throw new System.ComponentModel.Win32Exception(code);
            }
        }

        public static string ToWin32LongPath(string path)
        {
            if (path.StartsWith(@"\\?\", StringComparison.Ordinal)) return path;

            var newpath = path;
            if (newpath.StartsWith("\\", StringComparison.Ordinal))
            {
                newpath = @"\\?\UNC\" + newpath.Substring(2);
            }
            else if (newpath.Contains(":"))
            {
                newpath = @"\\?\" + newpath;
            }
            else
            {
                var currdir = Environment.CurrentDirectory;
                newpath = CombinePath(currdir, newpath);
                while (newpath.Contains("\\.\\")) newpath = newpath.Replace("\\.\\", "\\");
                newpath = @"\\?\" + newpath;
            }
            return newpath.TrimEnd('.').TrimEnd();
        }

        private static string ToHumanFrientlyPath(string path)
        {
            if (path.StartsWith(@"\\?\UNC\", StringComparison.Ordinal)) return @"\\" + path.Substring(8);
            if (path.StartsWith(@"\\?\", StringComparison.Ordinal)) return path.Substring(4);
            return path;
        }

        /// <summary>
        /// Breaks the folder path to list of folder names
        /// </summary>
        /// <returns>list of sub folder names</returns>
        public List<string> ToList()
        {
            return ToList(_folderName);
        }

        /// <summary>
        /// Breaks the folder path to list of folder names
        /// </summary>
        /// <param name="path">folder path</param>
        /// <returns>list of subfolder names</returns>
        private static List<string> ToList(string path)
        {
            var list = new List<string>();
            if (string.IsNullOrWhiteSpace(path))
            {
                return list;
            }

            bool unc = false;
            var prefix = @"\\?\";
            if (path.StartsWith(prefix + @"UNC\", StringComparison.Ordinal))
            {
                prefix += @"UNC\";
                unc = true;
            }
            var split = path.Split('\\');
            int uncIndex = unc ? 6 : 4;

            var txt = "";

            for (int index = 0; index < uncIndex; index++)
            {
                if (index > 0) txt += "\\";
                txt += split[index];
            }
            for (; uncIndex < split.Length; uncIndex++)
            {
                txt = CombinePath(txt, split[uncIndex]);
                list.Add(txt);
            }

            return list;
        }

        public static string GetDirectoryName(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                return "";
            }

            int index = filePath.LastIndexOf('\\');
            if (index > 0 && index < filePath.Length)
            {
                return filePath.Substring(0, index);
            }
            else
            {
                return "";
            }
        }

        private static string CombinePath(string path1, string path2)
        {
            return path1.TrimEnd('\\') + "\\" + path2.TrimStart('\\').TrimEnd('.');
        }

        private static string GetErrorMessage(int code = 0)
        {
            int localCode = code;
            if (localCode == 0)
            {
                localCode = Marshal.GetLastWin32Error();
            }
            if (localCode != 0)
            {
                var win32Exception = new System.ComponentModel.Win32Exception(code);
                return win32Exception.Message;
            }
            return "";
        }

        #endregion
    }
}
