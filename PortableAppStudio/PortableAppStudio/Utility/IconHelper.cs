using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Utility
{
    public class IconHelper
    {
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct RESICONDIRENTRY
        {
            public byte bWidth;
            public byte bHeight;
            public byte bColorCount;
            public byte bReserved;
            public ushort wPlanes;
            public ushort wBitCount;
            public uint dwBytesInRes;
            public ushort wId;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct RESICONDIR
        {
            public ushort wReserved;
            public ushort wType;
            public ushort wCount;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct FILEICONDIRENTRY
        {
            public byte bWidth;
            public byte bHeight;
            public byte bColorCount;
            public byte bReserved;
            public ushort wPlanes;
            public ushort wBitCount;
            public uint dwBytesInRes;
            public uint dwImageOffset;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct FILEICONDIR
        {
            public UInt16 idReserved;
            public UInt16 idType;
            public UInt16 idCount;
        }
        public enum IconOutputFormat
        {
            None = 0,
            Vista = 1,
            WinXP = 2,
            WinXPUnpopular = 4,
            Win95 = 8,
            Win95Unpopular = 16,
            Win31 = 32,
            Win31Unpopular = 64,
            Win30 = 128,
            Win30Unpopular = 256,
            FromWinXP = WinXP | Vista,
            FromWin95 = Win95 | FromWinXP,
            FromWin31 = Win31 | FromWin95,
            FromWin30 = Win30 | FromWin31,
            All = FromWin31 | Win31Unpopular | Win95Unpopular | WinXPUnpopular
        }
        public enum LoadLibraryFlags
        {
            DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
            LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
            LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008,
            LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010
        }
        public enum ResType : uint
        {
            CURSOR          = 1,
            BITMAP          = 2,
            ICON            = 3,
            MENU            = 4,
            DIALOG          = 5,
            STRING          = 6,
            FONTDIR         = 7,
            FONT            = 8,
            ACCELERATOR     = 9,
            RCDATA          = 10,
            MESSAGETABLE    = 11,
            GROUP_CURSOR    = 12,
            GROUP_ICON      = 14,
            VERSION         = 16,
            DLGINCLUDE      = 17,
            PLUGPLAY        = 19,
            VXD             = 20,
            ANICURSOR       = 21,
            ANIICON         = 22,
            HTML            = 23,
            MANIFEST        = 24
        }

        [UnmanagedFunctionPointer(CallingConvention.Winapi, SetLastError = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public delegate Int32 EnumResTypeProc(IntPtr hModule, IntPtr lpszType, IntPtr lParam);
        [UnmanagedFunctionPointer(CallingConvention.Winapi, SetLastError = true, CharSet = CharSet.Unicode)]
        [SuppressUnmanagedCodeSecurity]
        public delegate bool EnumResNameProc(IntPtr hModule, IntPtr pType, IntPtr pName, IntPtr param);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern uint SizeofResource(IntPtr hModule, IntPtr hResource);
        [DllImport("kernel32.dll",CharSet = CharSet.Unicode)]
        public static extern int FreeLibrary(IntPtr hModule);
        [DllImport("kernel32.dll",CharSet = CharSet.Unicode)]
        public static extern IntPtr LockResource(IntPtr hGlobalResource);
        [DllImport("kernel32.dll",CharSet = CharSet.Unicode)]
        public static extern IntPtr LoadResource(IntPtr hModule, IntPtr hResource);
        [DllImport("Kernel32.dll", SetLastError = true,CharSet = CharSet.Unicode)]
        public static extern bool EnumResourceNames(IntPtr hModule, IntPtr pType, EnumResNameProc lpEnumFunc, IntPtr lParam);
        [DllImport("kernel32.dll")]
        static extern bool EnumResourceNames(IntPtr hModule, int dwID, EnumResNameProc lpEnumFunc, IntPtr lParam);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool EnumResourceTypes(IntPtr hModule, EnumResTypeProc callback, IntPtr lParam);
        [DllImport("kernel32.dll",CharSet = CharSet.Unicode)]
        public static extern IntPtr FindResource(IntPtr hModule, string resourceID, IntPtr type);
        [DllImport("kernel32.dll",CharSet = CharSet.Unicode)]
        public static extern IntPtr FindResource(IntPtr hModule, Int32 resourceID, IntPtr type);
        [DllImport("kernel32.dll",CharSet = CharSet.Unicode)]
        public static extern IntPtr FindResource(IntPtr hModule, IntPtr resourceID, IntPtr type);
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindResource(IntPtr hModule, int lpName, int lpType);
        [DllImport("kernel32.dll",CharSet = CharSet.Unicode)]
        public static extern IntPtr FindResource(IntPtr hModule, IntPtr resourceID, string resourceName);
        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string libraryName);
        [DllImport("kernel32.dll",CharSet = CharSet.Unicode)]
        public static extern IntPtr LoadLibraryEx(string path, IntPtr hFile, LoadLibraryFlags flags);
        [DllImport("user32.dll")]
        public static extern int LookupIconIdFromDirectoryEx(byte[] presbits, bool fIcon, int cxDesired, int cyDesired, uint Flags);
        [DllImport("user32.dll")]
        public static extern int LookupIconIdFromDirectory(byte[] presbits, bool fIcon);
        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconFromResourceEx(byte[] pbIconBits, uint cbIconBits, bool fIcon, uint dwVersion, int cxDesired, int cyDesired, uint uFlags);
        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconFromResource(byte[] pbIconBits, uint cbIconBits, bool fIcon, uint dwVersion);

        private static bool IS_INTRESOURCE(IntPtr value)
        {
            if (((uint)value) > ushort.MaxValue)
                return false;
            return true;
        }

        private static bool IS_INTRESOURCE(string value)
        {
            int iResult;
            return int.TryParse(value, out iResult);
        }

        private static int MAKEINTRESOURCE(int resource)
        {
            return 0x0000FFFF & resource;
        }

        public const int NewFile = 2;
        public const int OpenFolder = 3;
        public const int Search = 8;
        public const int Run = 100;

        private static List<string> mIconsIDs;

        private static bool EnumResource(IntPtr hModule, IntPtr pType, IntPtr pName, IntPtr param)
        {
            if (IS_INTRESOURCE(pName))
            {
                mIconsIDs.Add(pName.ToString());
            }
            else
            {
                mIconsIDs.Add(Marshal.PtrToStringUni(pName));
            }
            return true;
        }

        //private static bool EnumResource(IntPtr hModule, IntPtr lpszType, IntPtr lpszName, IntPtr lParam)
        //{
        //    long idorname = (long)lpszName;
        //    if (idorname >> 16 == 0)
        //    {
        //        groupIconId = idorname;
        //    }
        //    else
        //    {
        //        groupIconName = Marshal.PtrToStringUni(lpszName);
        //    }

        //    return false;
        //}

        public static void SaveAppIconGroup(string exeFile, string targetICOFile)
        {
            if (!File.Exists(exeFile))
            {
                return;
            }

            IntPtr hModule = IntPtr.Zero;
            try
            {
                hModule = LoadLibraryEx(exeFile, IntPtr.Zero, LoadLibraryFlags.LOAD_LIBRARY_AS_DATAFILE);
                if (hModule == IntPtr.Zero)
                    throw new Win32Exception();

                //FileName = GetFileName(hModule);

                // Enumerate the icon resource and build .ico files in memory.

                EnumResNameProc callback = (h, t, name, l) =>
                {
                    // Refer the following URL for the data structures used here:
                    // http://msdn.microsoft.com/en-us/library/ms997538.aspx

                    // RT_GROUP_ICON resource consists of a GRPICONDIR and GRPICONDIRENTRY's.

                    var grpIconResDir = GetDataFromResource(hModule, (IntPtr) ResType.GROUP_ICON, name);

                    // Calculate the size of an entire .icon file.

                    int iconsCount = BitConverter.ToUInt16(grpIconResDir, 4);  // GRPICONDIR.idCount
                    //int totalResSize = Marshal.SizeOf(typeof(RESICONDIR)) + Marshal.SizeOf(typeof(RESICONDIRENTRY)) * iconsCount;
                    ////int len = 6 + 16 * count;                   // sizeof(ICONDIR) + sizeof(ICONDIRENTRY) * count
                    //for (int i = 0; i < iconsCount; ++i)
                    //{
                    //    totalResSize += BitConverter.ToInt32(grpIconDir, 6 + 14 * i + 8);   // GRPICONDIRENTRY.dwBytesInRes
                    //}

                    using (var dst = new BinaryWriter(new FileStream(targetICOFile,FileMode.Create)))
                    {
                        // Copy GRPICONDIR to ICONDIR. Both are identical hence just copy bytes
                        dst.Write(grpIconResDir, 0, 6);

                        //int picOffset = 6 + 16 * iconsCount; // sizeof(ICONDIR) + sizeof(ICONDIRENTRY) * count
                        // sizeof(ICONDIR) + sizeof(ICONDIRENTRY) * count
                        int picFileOffset = Marshal.SizeOf(typeof(FILEICONDIR)) + Marshal.SizeOf(typeof(FILEICONDIRENTRY)) * iconsCount; 

                        for (int iconIndex = 0; iconIndex < iconsCount; ++iconIndex)
                        {
                            // Load the picture.

                            ushort iconID = BitConverter.ToUInt16(grpIconResDir, 6 + 14 * iconIndex + 12);    // GRPICONDIRENTRY.nID
                            var picData = GetDataFromResource(hModule, (IntPtr)ResType.ICON, (IntPtr)iconID);

                            // Copy GRPICONDIRENTRY to ICONDIRENTRY.
                            int iconFileDirEntryOffset = Marshal.SizeOf(typeof(FILEICONDIR)) + Marshal.SizeOf(typeof(FILEICONDIRENTRY)) * iconIndex;
                            //dst.Seek(6 + 16 * iconIndex, SeekOrigin.Begin);
                            dst.Seek(iconFileDirEntryOffset, SeekOrigin.Begin);

                            int iconResDirEntryOffset = Marshal.SizeOf(typeof(RESICONDIR)) + Marshal.SizeOf(typeof(RESICONDIRENTRY)) * iconIndex;
                            //dst.Write(grpIconDir, 6 + 14 * iconIndex, 8);  // First 8bytes are identical.
                            // First 8bytes are identical between RESICONDIRENTRY & FILEICONDIRENTRY
                            dst.Write(grpIconResDir, iconResDirEntryOffset, 8);  //FILEICONDIRENTRY first 8 bytes
                            dst.Write(picData.Length);          // FILEICONDIRENTRY.dwBytesInRes
                            dst.Write(picFileOffset);           // FILEICONDIRENTRY.dwImageOffset

                            // Copy a picture.

                            dst.Seek(picFileOffset, SeekOrigin.Begin);
                            dst.Write(picData, 0, picData.Length);

                            picFileOffset += picData.Length;
                        }
                    }
                    //stops at first enumuration, we interested only to extract first icon
                    return false;
                };

                EnumResourceNames(hModule, (IntPtr)ResType.GROUP_ICON, callback, IntPtr.Zero);
            }
            finally
            {
                if (hModule != IntPtr.Zero)
                {
                    FreeLibrary(hModule);
                }
            }
        }

        private static byte[] GetDataFromResource(IntPtr hModule, IntPtr type, IntPtr name)
        {
            // Load the binary data from the specified resource.

            IntPtr hResInfo = FindResource(hModule, name, type);
            if (hResInfo == IntPtr.Zero)
                throw new Win32Exception();

            IntPtr hResData = LoadResource(hModule, hResInfo);
            if (hResData == IntPtr.Zero)
                throw new Win32Exception();

            IntPtr pResData = LockResource(hResData);
            if (pResData == IntPtr.Zero)
                throw new Win32Exception();

            uint size = SizeofResource(hModule, hResInfo);
            if (size == 0)
                throw new Win32Exception();

            byte[] buf = new byte[size];
            Marshal.Copy(pResData, buf, 0, buf.Length);

            return buf;
        }

        public static void SaveAppIconGroup1(string file,string targetFile)
        {
            IntPtr hExe = LoadLibrary(Environment.ExpandEnvironmentVariables(file));
            if (hExe != IntPtr.Zero)
            {


                List<string> groupIDs;
                lock (typeof(IconHelper))
                {
                    mIconsIDs = new List<string>();
                    bool bResult = EnumResourceNames(hExe, (IntPtr)ResType.GROUP_ICON, EnumResource, IntPtr.Zero);
                    if (bResult == false)
                    {
                        // No Resources in this file
                    }
                    groupIDs = new List<string>(mIconsIDs);
                }

                for (int index = 0; index < groupIDs.Count; index++)
                {
                    string id = groupIDs[index];

                    IntPtr hResource = IntPtr.Zero;

                    if (IS_INTRESOURCE(id))
                        hResource = FindResource(hExe, int.Parse(id), (IntPtr)ResType.GROUP_ICON);
                    else
                        hResource = FindResource(hExe, id, (IntPtr)ResType.GROUP_ICON);

                    if (hResource == IntPtr.Zero)
                        return;

                    IntPtr hGroupIconMem = LoadResource(hExe, hResource);
                    IntPtr lpGroupResourcePtr = LockResource(hGroupIconMem);
                    uint sz = SizeofResource(hExe, hResource);
                    byte[] lpGroupResourceBytes = new byte[sz];
                    Marshal.Copy(lpGroupResourcePtr, lpGroupResourceBytes, 0, lpGroupResourceBytes.Length);
                    RESICONDIR pDirectory = Marshal.PtrToStructure<RESICONDIR>(lpGroupResourcePtr);

                    IntPtr startPtr = lpGroupResourcePtr + (sizeof(ushort) * 3);

                    //TODO Get ID
                    //int nID = LookupIconIdFromDirectory(lpGroupResourceBytes, true);
                    List<System.Drawing.Icon> multiIcons = new List<System.Drawing.Icon>();
                    for (int nID = 0; nID < pDirectory.wCount; nID++)
                    {
                        int size = Marshal.SizeOf(typeof(RESICONDIRENTRY));
                        IntPtr currPtr = startPtr + (nID * size);
                        RESICONDIRENTRY pEntry = Marshal.PtrToStructure<RESICONDIRENTRY>(currPtr);
                        IntPtr hIconResource = FindResource(hExe, pEntry.wId, (IntPtr)ResType.ICON);
                        IntPtr hIconMem = LoadResource(hExe, hIconResource);

                        IntPtr lpIconResourcePtr = LockResource(hIconMem);
                        var iconResSize = SizeofResource(hExe, hIconResource);
                        byte[] lpIconResourceBytes = new byte[iconResSize];
                        Marshal.Copy(lpIconResourcePtr, lpIconResourceBytes, 0, lpIconResourceBytes.Length);

                        IntPtr hIcon = CreateIconFromResourceEx(lpIconResourceBytes, (uint)lpIconResourceBytes.Length, true, 0x00030000,pEntry.bWidth,pEntry.bHeight,0);

                        multiIcons.Add(System.Drawing.Icon.FromHandle(hIcon));
                    }
                }
            }
        }

        public static System.Drawing.Icon GetAppIconGroup(string file)
        {
            IntPtr hExe = LoadLibrary(Environment.ExpandEnvironmentVariables(file));
            if (hExe != IntPtr.Zero)
            {

                List<string> iconsIDs;
                lock (typeof(IconHelper))
                {
                    mIconsIDs = new List<string>();
                    bool bResult = EnumResourceNames(hExe, (IntPtr)ResType.GROUP_ICON, EnumResource, IntPtr.Zero);
                    if (bResult == false)
                    {
                        // No Resources in this file
                    }
                    iconsIDs = new List<string>(mIconsIDs);
                }

                if (iconsIDs.Count > 0)
                {
                    string id = iconsIDs.First();
                    IntPtr hResource = IntPtr.Zero;

                    if (IS_INTRESOURCE(id))
                        hResource = FindResource(hExe, int.Parse(id), (IntPtr)ResType.GROUP_ICON);
                    else
                        hResource = FindResource(hExe, id, (IntPtr)ResType.GROUP_ICON);

                    if (hResource == IntPtr.Zero)
                        return null;

                    IntPtr hMem = LoadResource(hExe, hResource);
                    IntPtr lpResourcePtr = LockResource(hMem);

                    uint sz = SizeofResource(hExe, hResource);
                    byte[] lpResource = new byte[sz];
                    Marshal.Copy(lpResourcePtr, lpResource, 0, (int)sz);

                    int nID = LookupIconIdFromDirectory(lpResource, true);

                    hResource = FindResource(hExe, nID, (int)ResType.ICON);

                    hMem = LoadResource(hExe, hResource);

                    lpResourcePtr = LockResource(hMem);
                    sz = SizeofResource(hExe, hResource);
                    lpResource = new byte[sz];
                    Marshal.Copy(lpResourcePtr, lpResource, 0, (int)sz);

                    IntPtr hIcon = CreateIconFromResource(lpResource, sz, true, 0x00030000);

                    System.Drawing.Icon testIco = System.Drawing.Icon.FromHandle(hIcon);

                    return testIco;
                }


            }

            return null;
        }

        /// <summary>
        /// %SystemRoot%\system32\DDORes.dll
        /// %SystemRoot%\system32\imageres.dll
        /// %SystemRoot%\system32\shell32.dll
        /// </summary>
        /// <param name="file"></param>
        /// <param name="groupId"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static System.Drawing.Icon GetIconFromGroup(string file, int groupId, int size)
        {
            IntPtr hExe = LoadLibrary(Environment.ExpandEnvironmentVariables(file));
            if (hExe != IntPtr.Zero)
            {
                IntPtr hResource = FindResource(hExe, groupId, (int)ResType.GROUP_ICON);
                IntPtr hMem = LoadResource(hExe, hResource);
                IntPtr lpResourcePtr = LockResource(hMem);

                uint sz = SizeofResource(hExe, hResource);
                byte[] lpResource = new byte[sz];
                Marshal.Copy(lpResourcePtr, lpResource, 0, (int)sz);

                int nID = LookupIconIdFromDirectoryEx(lpResource, true, size, size, 0x0000);

                hResource = FindResource(hExe, nID, (int)ResType.GROUP_ICON);

                hMem = LoadResource(hExe, hResource);

                lpResourcePtr = LockResource(hMem);
                sz = SizeofResource(hExe, hResource);
                lpResource = new byte[sz];
                Marshal.Copy(lpResourcePtr, lpResource, 0, (int)sz);

                IntPtr hIcon = CreateIconFromResourceEx(lpResource, sz, true, 0x00030000, size, size, 0);

                System.Drawing.Icon testIco = System.Drawing.Icon.FromHandle(hIcon);

                return testIco;
            }

            return null;
        }
    }
}
