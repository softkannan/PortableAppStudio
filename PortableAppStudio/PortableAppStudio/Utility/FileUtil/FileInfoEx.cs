using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Security.AccessControl;
using System.Security.Principal;

namespace PortableAppStudio.Utility.FileUtil
{
    public class FileInfoEx
    {
        internal const int ERROR_PATH_NOT_FOUND = 0x3;

        private readonly string _path;

        public string FullName { get => _path; }

        public string HumanPath { get => ToHumanFrientlyPath(_path); }
        public string Name { get; private set; }

        public FileInfoEx(string path)
        {
            _path = path;
            Name = Path.GetFileName(_path);
        }

        public bool Exists()
        {
            return Exists(_path);
        }

        public static bool Exists(string path)
        {
            var attr = NativeMethods.GetFileAttributesW(ToWin32LongPath(path));
            return  (attr != NativeMethods.INVALID_FILE_ATTRIBUTES && !((attr & NativeMethods.FILE_ATTRIBUTE_DIRECTORY) == NativeMethods.FILE_ATTRIBUTE_DIRECTORY));
        }

        public int Touch(out string errorMessage)
        {
            return Touch(_path,out errorMessage);
        }

        public static int Touch(string path, out string errorMessage)
        {
            errorMessage = null;
            string localPath = ToWin32LongPath(path);

            if (Exists(path))
            {
                SetAttributes(localPath, FileAttributes.Normal);
                bool ok = NativeMethods.DeleteFileW(localPath);
                if (!ok)
                {
                    int code = Marshal.GetLastWin32Error();
                    errorMessage = string.Format("Unable to delete \"{0}\"", GetErrorMessage(code));
                    return code;
                }
            }

            using (SafeFileHandle hfile = NativeMethods.CreateFileW(localPath, (int)NativeMethods.FILE_GENERIC_WRITE, NativeMethods.FILE_SHARE_NONE, IntPtr.Zero, NativeMethods.CREATE_ALWAYS, 0, IntPtr.Zero))
            {
                if (hfile.IsInvalid)
                {
                    int code = Marshal.GetLastWin32Error();
                    errorMessage = string.Format("Unable to create new file \"{0}\"", GetErrorMessage(code));
                    return code;
                }
            }
            errorMessage = string.Format("\"{0}\" File Touched.", path);
            return 0;
        }

        public void Copy(string destFileName, bool overwrite = false)
        {
            Copy(_path, destFileName, overwrite);
        }

        public static void Copy(string sourceFileName, string destFileName, bool overwrite = false)
        {
            var ok = NativeMethods.CopyFileW(ToWin32LongPath(sourceFileName), ToWin32LongPath(destFileName), !overwrite);
            if (!ok) ThrowWin32Exception();
        }

        public void Move(string destFileName)
        {
            Move(_path, destFileName);
        }

        public static void Move(string sourceFileName, string destFileName)
        {
            var ok = NativeMethods.MoveFileW(ToWin32LongPath(sourceFileName), ToWin32LongPath(destFileName));
            if (!ok) ThrowWin32Exception();
        }

        public int Delete(out string errorMessage)
        {
            return Delete(_path,out errorMessage);
        }
        public static int Delete(string path, out string errorMessage)
        {
            errorMessage = null;
            string localPath = ToWin32LongPath(path);
            SetAttributes(localPath, FileAttributes.Normal);
            bool ok = NativeMethods.DeleteFileW(localPath);
            if (!ok)
            {
                int code = Marshal.GetLastWin32Error();
                if (ERROR_PATH_NOT_FOUND != code)
                {
                    if (Exists(localPath))
                    {
                        if (!NativeMethods.MoveFileExW(localPath, null, NativeMethods.MoveFileFlags.DelayUntilReboot))
                        {
                            code = Marshal.GetLastWin32Error();
                            errorMessage = string.Format("Failed to schedule \"{0}\" file deletion on reboot error: {1}", path, GetErrorMessage(code));
                            return code;
                        }
                        else
                        {
                            errorMessage = string.Format("Scheduled file deletion on reboot : {0}", localPath);
                            return 0;
                        }
                    }
                }
                errorMessage = GetErrorMessage(code);
                return code;
            }
            errorMessage = string.Format("\"{0}\" File Deleted.", path);
            return 0;
        }

        public int Delete()
        {
            return Delete(_path);
        }

        public static int Delete(string path)
        {
            int retCode = 0;
            string localPath = ToWin32LongPath(path);
            SetAttributes(localPath, FileAttributes.Normal);
            bool ok = NativeMethods.DeleteFileW(localPath);
            if (!ok)
            {
                int code = Marshal.GetLastWin32Error();
                if (ERROR_PATH_NOT_FOUND != code)
                {
                    if (Exists(localPath))
                    {
                        if (!NativeMethods.MoveFileExW(localPath, null, NativeMethods.MoveFileFlags.DelayUntilReboot))
                        {
                            code = Marshal.GetLastWin32Error();
                            ErrorLog.Inst.LogError("Failed to schedule file deletion on reboot : {0}", localPath);
                            ThrowWin32Exception(code);
                        }
                        else
                        {
                            ErrorLog.Inst.LogError("Scheduled file deletion on reboot : {0}", localPath);
                            retCode = 0;
                        }
                    }
                    else
                    {
                        ThrowWin32Exception(code);
                    }
                }
                else
                {
                    return code;
                }
            }

            return retCode;
        }

        #region File attributes

        public void SetCreationTime(DateTime creationTime)
        {
            SetCreationTime(_path, creationTime);
        }

        public static void SetCreationTime(string path, DateTime creationTime)
        {
            long cTime = 0;
            long aTime = 0;
            long wTime = 0;

            using (var handle = GetFileHandleWithWrite(path))
            {
                NativeMethods.GetFileTime(handle, ref cTime, ref aTime, ref wTime);
                var fileTime = creationTime.ToFileTimeUtc();
                if (!NativeMethods.SetFileTime(handle, ref fileTime, ref aTime, ref wTime))
                {
                    throw new Win32Exception();
                }
            }
        }

        public void SetLastAccessTime(DateTime lastAccessTime)
        {
            SetLastAccessTime(_path, lastAccessTime);
        }

        public static void SetLastAccessTime(string path, DateTime lastAccessTime)
        {
            long cTime = 0;
            long aTime = 0;
            long wTime = 0;

            using (var handle = GetFileHandleWithWrite(path))
            {
                NativeMethods.GetFileTime(handle, ref cTime, ref aTime, ref wTime);

                var fileTime = lastAccessTime.ToFileTimeUtc();
                if (!NativeMethods.SetFileTime(handle, ref cTime, ref fileTime, ref wTime))
                {
                    throw new Win32Exception();
                }
            }
        }

        public void SetLastWriteTime(DateTime lastWriteTime)
        {
            SetLastWriteTime(_path, lastWriteTime);
        }

        public static void SetLastWriteTime(string path, DateTime lastWriteTime)
        {
            long cTime = 0;
            long aTime = 0;
            long wTime = 0;

            using (var handle = GetFileHandleWithWrite(path))
            {
                NativeMethods.GetFileTime(handle, ref cTime, ref aTime, ref wTime);

                var fileTime = lastWriteTime.ToFileTimeUtc();
                if (!NativeMethods.SetFileTime(handle, ref cTime, ref aTime, ref fileTime))
                {
                    throw new Win32Exception();
                }
            }
        }

        public DateTime GetLastWriteTime()
        {
            return  GetLastWriteTime(_path);
        }

        public static DateTime GetLastWriteTime(string path)
        {
            long cTime = 0;
            long aTime = 0;
            long wTime = 0;

            using (var handle = GetFileHandleWithWrite(path))
            {
                NativeMethods.GetFileTime(handle, ref cTime, ref aTime, ref wTime);

                return DateTime.FromFileTimeUtc(wTime);
            }
        }

        public string GetLastWriteTimeStr()
        {
            return GetLastWriteTimeStr(_path);
        }

        public static string GetLastWriteTimeStr(string path)
        {
            long cTime = 0;
            long aTime = 0;
            long wTime = 0;

            using (var handle = GetFileHandleForTime(path))
            {
                if (handle != null && handle.IsInvalid == false)
                {
                    NativeMethods.GetFileTime(handle, ref cTime, ref aTime, ref wTime);

                    return DateTime.FromFileTimeUtc(wTime).ToString();
                }
            }

            return "";
        }

        public void SetAttributes(FileAttributes attributes)
        {
            SetAttributes(_path, attributes);
        }

        public static void SetAttributes(string path, FileAttributes attributes)
        {
            var longFilename = ToWin32LongPath(path);
            NativeMethods.SetFileAttributesW(longFilename, (int)attributes);
        }
        #endregion

        #region File Read/Write Methods

        public void AppendAllText(string contents)
        {
            AppendAllText(_path, contents, Encoding.Default);
        }

        public static void AppendAllText(string path, string contents)
        {
            AppendAllText(path, contents, Encoding.Default);
        }

        public void AppendAllText(string contents, Encoding encoding)
        {
            AppendAllText(_path, contents, encoding);
        }

        public static void AppendAllText(string path, string contents, Encoding encoding)
        {
            var fileHandle = CreateFileForAppend(ToWin32LongPath(path));
            using (var fs = new System.IO.FileStream(fileHandle, System.IO.FileAccess.Write))
            {
                var bytes = encoding.GetBytes(contents);
                fs.Position = fs.Length;
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        public void WriteAllText(string contents)
        {
            WriteAllText(_path, contents, Encoding.Default);
        }

        public static void WriteAllText(string path, string contents)
        {
            WriteAllText(path, contents, Encoding.Default);
        }

        public void WriteAllText(string contents, Encoding encoding)
        {
            WriteAllText(_path, contents, encoding);
        }

        public static void WriteAllText(string path, string contents, Encoding encoding)
        {
            var fileHandle = CreateFileForWrite(ToWin32LongPath(path));
            using (var fs = new System.IO.FileStream(fileHandle, System.IO.FileAccess.Write))
            {
                var bytes = encoding.GetBytes(contents);
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        public void WriteAllBytes(byte[] bytes)
        {
            WriteAllBytes(_path, bytes);
        }

        public static void WriteAllBytes(string path, byte[] bytes)
        {
            var fileHandle = CreateFileForWrite(ToWin32LongPath(path));
            using (var fs = new System.IO.FileStream(fileHandle, System.IO.FileAccess.Write))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        public string ReadAllText()
        {
            return ReadAllText(_path, Encoding.Default);
        }

        public static string ReadAllText(string path)
        {
            return ReadAllText(path, Encoding.Default);
        }

        public string ReadAllText(Encoding encoding)
        {
            return ReadAllText(_path, encoding);
        }

        public static string ReadAllText(string path, Encoding encoding)
        {
            var fileHandle = GetFileHandle(ToWin32LongPath(path));
            using (var fs = new System.IO.FileStream(fileHandle, System.IO.FileAccess.Read))
            {
                var data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                return encoding.GetString(data);
            }
        }

        public string[] ReadAllLines()
        {
            return ReadAllLines(_path, Encoding.Default);
        }

        public static string[] ReadAllLines(string path)
        {
            return ReadAllLines(path, Encoding.Default);
        }

        public string[] ReadAllLines(Encoding encoding)
        {
            return ReadAllLines(_path, encoding);
        }

        public static string[] ReadAllLines(string path, Encoding encoding)
        {
            var fileHandle = GetFileHandle(path);
            using (var fs = new System.IO.FileStream(fileHandle, System.IO.FileAccess.Read))
            {
                var data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                var str = encoding.GetString(data);
                if (str.Contains("\r")) return str.Split(new[] { "\r\n" }, StringSplitOptions.None);
                return str.Split('\n');
            }
        }

        public byte[] ReadAllBytes()
        {
            return ReadAllBytes(_path);
        }

        public static byte[] ReadAllBytes(string path)
        {
            var fileHandle = GetFileHandle(ToWin32LongPath(path));
            using (var fs = new System.IO.FileStream(fileHandle, System.IO.FileAccess.Read))
            {
                var data = new byte[fs.Length];
                fs.Read(data, 0, data.Length);
                return data;
            }
        }

        #endregion

        #region Create / Open Methods

        private static SafeFileHandle CreateFileForWrite(string filename)
        {
            filename = ToWin32LongPath(filename);
            SafeFileHandle hfile = NativeMethods.CreateFileW(filename, (int)NativeMethods.FILE_GENERIC_WRITE, NativeMethods.FILE_SHARE_NONE, IntPtr.Zero, NativeMethods.CREATE_ALWAYS, 0, IntPtr.Zero);
            if (hfile.IsInvalid) ThrowWin32Exception();
            return hfile;
        }

        private static SafeFileHandle CreateFileForAppend(string filename)
        {
            filename = ToWin32LongPath(filename);
            SafeFileHandle hfile = NativeMethods.CreateFileW(filename, (int)NativeMethods.FILE_GENERIC_WRITE, NativeMethods.FILE_SHARE_NONE, IntPtr.Zero, NativeMethods.CREATE_NEW, 0, IntPtr.Zero);
            if (hfile.IsInvalid)
            {
                hfile = NativeMethods.CreateFileW(filename, (int)NativeMethods.FILE_GENERIC_WRITE, NativeMethods.FILE_SHARE_NONE, IntPtr.Zero, NativeMethods.OPEN_EXISTING, 0, IntPtr.Zero);
                if (hfile.IsInvalid) ThrowWin32Exception();
            }
            return hfile;
        }
        internal static SafeFileHandle GetFileHandleWithWrite(string filename)
        {
            var longFilename = ToWin32LongPath(filename);
            SafeFileHandle hfile = NativeMethods.CreateFileW(longFilename, (int)(NativeMethods.FILE_GENERIC_READ | NativeMethods.FILE_GENERIC_WRITE | NativeMethods.FILE_WRITE_ATTRIBUTES), NativeMethods.FILE_SHARE_NONE, IntPtr.Zero, NativeMethods.OPEN_EXISTING, 0, IntPtr.Zero);
            if (hfile.IsInvalid) ThrowWin32Exception();
            return hfile;
        }

        public System.IO.FileStream GetFileStream(FileAccess access = FileAccess.Read)
        {
            return GetFileStream(_path, access);
        }

        public static System.IO.FileStream GetFileStream(string filename, FileAccess access = FileAccess.Read)
        {
            var longFilename = ToWin32LongPath(filename);
            SafeFileHandle hfile;
            if (access == FileAccess.Write || access == FileAccess.ReadWrite)
            {
                hfile = NativeMethods.CreateFileW(longFilename, (int)(NativeMethods.FILE_GENERIC_READ | NativeMethods.FILE_GENERIC_WRITE | NativeMethods.FILE_WRITE_ATTRIBUTES), NativeMethods.FILE_SHARE_NONE, IntPtr.Zero, NativeMethods.CREATE_ALWAYS, 0, IntPtr.Zero);
            }
            else
            {
                hfile = NativeMethods.CreateFileW(longFilename, (int)NativeMethods.FILE_GENERIC_READ, NativeMethods.FILE_SHARE_READ, IntPtr.Zero, NativeMethods.OPEN_EXISTING, 0, IntPtr.Zero);
            }

            if (hfile.IsInvalid) ThrowWin32Exception();
            return new System.IO.FileStream(hfile, access);
        }

        internal static SafeFileHandle GetFileHandle(string filename)
        {
            var longFilename = ToWin32LongPath(filename);
            SafeFileHandle hfile = NativeMethods.CreateFileW(longFilename, (int)NativeMethods.FILE_GENERIC_READ, NativeMethods.FILE_SHARE_READ, IntPtr.Zero, NativeMethods.OPEN_EXISTING, 0, IntPtr.Zero);
            if (hfile.IsInvalid) ThrowWin32Exception();
            return hfile;
        }

        internal static SafeFileHandle GetFileHandleForTime(string filename)
        {
            var longFilename = ToWin32LongPath(filename);
            SafeFileHandle hfile = NativeMethods.CreateFileW(longFilename, (int)NativeMethods.FILE_GENERIC_READ, NativeMethods.FILE_SHARE_READ, IntPtr.Zero, NativeMethods.OPEN_EXISTING, 0, IntPtr.Zero);
            //if (hfile.IsInvalid) ThrowWin32Exception();
            return hfile;
        }

        #endregion

        #region Helper methods

        [DebuggerStepThrough]
        public static void ThrowWin32Exception(int code = 0)
        {
            int localCode = code;
            if (localCode == 0)
            {
                localCode = Marshal.GetLastWin32Error();
            }
            if (localCode != 0)
            {
                throw new System.ComponentModel.Win32Exception(code);
            }
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

        public static string ToWin32LongPath(string path)
        {
            if (path.StartsWith(@"\\?\", StringComparison.Ordinal)) return path;

            if (path.StartsWith("\\", StringComparison.Ordinal))
            {
                path = @"\\?\UNC\" + path.Substring(2);
            }
            else if (path.Contains(":"))
            {
                path = @"\\?\" + path;
            }
            else
            {
                var currdir = Environment.CurrentDirectory;
                path = Combine(currdir, path);
                while (path.Contains("\\.\\")) path = path.Replace("\\.\\", "\\");
                path = @"\\?\" + path;
            }
            return path.TrimEnd('.'); ;
        }

        private static string ToHumanFrientlyPath(string path)
        {
            if (path.StartsWith(@"\\?\UNC\", StringComparison.Ordinal)) return @"\\" + path.Substring(8);
            if (path.StartsWith(@"\\?\", StringComparison.Ordinal)) return path.Substring(4);
            return path;
        }

        private static string Combine(string path1, string path2)
        {
            return path1.TrimEnd('\\') + "\\" + path2.TrimStart('\\').TrimEnd('.'); ;
        }

        #endregion
        
    }
}
