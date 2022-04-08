using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

namespace PortableAppStudio.Utility.FileUtil
{
    public class FileAttribEx
    {
        public FileAttributes FileAttributes { get; private set; }
        public FILETIME CreationTime { get; private set; }
        public FILETIME LastAccessTime { get; private set; }
        public FILETIME LastWriteTime { get; private set; }
        public ulong Length { get; private set; }

        private const Int64 INVALID_FILE_ATTRIBUTES = -1;

        public bool Sucess { get; private set; }

        public FileAttribEx(string filePath)
        {
            Sucess = false;
            NativeMethods.WIN32_FILE_ATTRIBUTE_DATA fileData;
            var longPath = FileInfoEx.ToWin32LongPath(filePath);
            var ok = NativeMethods.GetFileAttributesExW(longPath,NativeMethods.GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard, out fileData);
            if(ok != INVALID_FILE_ATTRIBUTES)
            {
                FileAttributes = fileData.dwFileAttributes;
                CreationTime = fileData.ftCreationTime;
                LastAccessTime = fileData.ftLastAccessTime;
                LastWriteTime = fileData.ftLastWriteTime;
                NativeMethods.LARGE_INTEGER fileSize;
                fileSize.QuadPart = 0;
                fileSize.LowPart = fileData.nFileSizeLow;
                fileSize.HighPart = fileData.nFileSizeHigh;
                Length = fileSize.QuadPart;
                Sucess = true;
            }
        }
    }
}
