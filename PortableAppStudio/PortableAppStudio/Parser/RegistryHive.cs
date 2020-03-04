using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Parser
{
    internal static class RegistryNativeMethods
    {
        internal enum RegSAM
        {
            /// <summary>
            /// Combines the STANDARD_RIGHTS_REQUIRED, KEY_QUERY_VALUE, KEY_SET_VALUE, KEY_CREATE_SUB_KEY, KEY_ENUMERATE_SUB_KEYS, KEY_NOTIFY, and KEY_CREATE_LINK access rights.
            /// </summary>
            KEY_ALL_ACCESS = 0xF003F,
            /// <summary>
            /// Reserved for system use.
            /// </summary>
            KEY_CREATE_LINK = 0x0020,
            /// <summary>
            /// Required to create a subkey of a registry key.
            /// </summary>
            KEY_CREATE_SUB_KEY = 0x0004,
            /// <summary>
            /// Required to enumerate the subkeys of a registry key.
            /// </summary>
            KEY_ENUMERATE_SUB_KEYS = 0x0008,
            /// <summary>
            /// Equivalent to KEY_READ.
            /// </summary>
            KEY_EXECUTE = 0x20019,
            /// <summary>
            /// Required to request change notifications for a registry key or for subkeys of a registry key.
            /// </summary>
            KEY_NOTIFY = 0x0010,
            /// <summary>
            /// Required to query the values of a registry key.
            /// </summary>
            KEY_QUERY_VALUE = 0x0001,
            /// <summary>
            /// Combines the STANDARD_RIGHTS_READ, KEY_QUERY_VALUE, KEY_ENUMERATE_SUB_KEYS, and KEY_NOTIFY values.
            /// </summary>
            KEY_READ = 0x20019,
            /// <summary>
            /// Required to create, delete, or set a registry value.
            /// </summary>
            KEY_SET_VALUE = 0x0002,
            /// <summary>
            /// Indicates that an application on 64-bit Windows should operate on the 32-bit registry view.This flag is ignored by 32-bit Windows.For more information,
            /// see Accessing an Alternate Registry View. This flag must be combined using the OR operator with the other flags in this table that either query or 
            /// access registry values. Windows 2000:  This flag is not supported.
            /// </summary>
            KEY_WOW64_32KEY = 0x0200,
            /// <summary>
            /// Indicates that an application on 64-bit Windows should operate on the 64-bit registry view.This flag is ignored by 32-bit Windows.
            /// For more information, see Accessing an Alternate Registry View. This flag must be combined using the OR operator with the other flags 
            /// in this table that either query or access registry values. Windows 2000:  This flag is not supported.
            /// </summary>
            KEY_WOW64_64KEY = 0x0100,
            /// <summary>
            /// Combines the STANDARD_RIGHTS_WRITE, KEY_SET_VALUE, and KEY_CREATE_SUB_KEY access rights.
            /// </summary>
            KEY_WRITE = 0x20006,
        }

        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms724884(v=vs.85).aspx
        /// </summary>
        internal enum RFlags
        {
            /// <summary>
            /// Any
            /// </summary>
            Any = 65535,

            /// <summary>
            /// No defined value type.
            /// </summary>
            RegNone = 1,

            /// <summary>
            /// ???
            /// </summary>
            Noexpand = 268435456,

            /// <summary>
            /// Bytes
            /// </summary>
            RegBinary = 8,

            /// <summary>
            /// Int32
            /// </summary>
            Dword = 24,

            /// <summary>
            /// Int32
            /// </summary>
            RegDword = 16,

            /// <summary>
            /// Int64
            /// </summary>
            Qword = 72,

            /// <summary>
            /// Int64
            /// </summary>
            RegQword = 64,

            /// <summary>
            /// A null-terminated string.
            /// This will be either a Unicode or an ANSI string,
            /// depending on whether you use the Unicode or ANSI functions.
            /// </summary>
            RegSz = 2,

            /// <summary>
            /// A sequence of null-terminated strings, terminated by an empty string (\0).
            /// The following is an example:
            /// String1\0String2\0String3\0LastString\0\0
            /// The first \0 terminates the first string, the second to the last \0 terminates the last string, 
            /// and the final \0 terminates the sequence. Note that the final terminator must be factored into the length of the string.
            /// </summary>
            RegMultiSz = 32,

            /// <summary>
            /// A null-terminated string that contains unexpanded references to environment variables (for example, "%PATH%").
            /// It will be a Unicode or ANSI string depending on whether you use the Unicode or ANSI functions. 
            /// To expand the environment variable references, use the ExpandEnvironmentStrings function.
            /// </summary>
            RegExpandSz = 4,

            RrfZeroonfailure = 536870912
        }

        /// <summary>
        /// http://msdn.microsoft.com/en-us/library/windows/desktop/ms724884(v=vs.85).aspx
        /// </summary>
        internal enum RType
        {
            RegNone = 0,

            RegSz = 1,
            RegExpandSz = 2,
            RegMultiSz = 7,

            RegBinary = 3,
            RegDword = 4,
            RegQword = 11,

            RegQwordLittleEndian = 11,
            RegDwordLittleEndian = 4,
            RegDwordBigEndian = 5,

            RegLink = 6,
            RegResourceList = 8,
            RegFullResourceDescriptor = 9,
            RegResourceRequirementsList = 10,
        }

        internal enum RegFileFormat : int
        {
            /// <summary>
            /// The key or hive is saved in standard format. The standard format is the only format supported by Windows 2000.
            /// </summary>
            REG_STANDARD_FORMAT = 1,
            /// <summary>
            /// The key or hive is saved in the latest format. The latest format is supported starting with Windows XP. 
            /// After the key or hive is saved in this format, it cannot be loaded on an earlier system.
            /// </summary>
            REG_LATEST_FORMAT = 2,
            /// <summary>
            /// The hive is saved with no compression, for faster save operations. 
            /// The hKey parameter must specify the root of a hive under HKEY_LOCAL_MACHINE or HKEY_USERS. For example, HKLM\SOFTWARE is the root of a hive.
            /// </summary>
            REG_NO_COMPRESSION = 4
        }

        private const int REG_PROCESS_APPKEY = 0x00000001;

        public const int ANYSIZE_ARRAY = 1;

        [StructLayout(LayoutKind.Sequential)]
        public struct TOKEN_USER
        {
            public _SID_AND_ATTRIBUTES User;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct _SID_AND_ATTRIBUTES
        {
            public IntPtr Sid;
            public int Attributes;
        }
        public struct LUID
        {
            public UInt32 LowPart;
            public Int32 HighPart;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct LUID_AND_ATTRIBUTES
        {
            public LUID Luid;
            public UInt32 Attributes;
        }

        public struct TOKEN_PRIVILEGES
        {
            public int PrivilegeCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = ANYSIZE_ARRAY)]
            public LUID_AND_ATTRIBUTES[] Privileges;

        }

        public enum TOKEN_INFORMATION_CLASS
        {
            TokenUser = 1,
            TokenGroups,
            TokenPrivileges,
            TokenOwner,
            TokenPrimaryGroup,
            TokenDefaultDacl,
            TokenSource,
            TokenType,
            TokenImpersonationLevel,
            TokenStatistics,
            TokenRestrictedSids,
            TokenSessionId,
            TokenGroupsAndPrivileges,
            TokenSessionReference,
            TokenSandBoxInert,
            TokenAuditPolicy,
            TokenOrigin
        }

        public enum SecurityEntity
        {
            SE_CREATE_TOKEN_NAME,
            SE_ASSIGNPRIMARYTOKEN_NAME,
            SE_LOCK_MEMORY_NAME,
            SE_INCREASE_QUOTA_NAME,
            SE_UNSOLICITED_INPUT_NAME,
            SE_MACHINE_ACCOUNT_NAME,
            SE_TCB_NAME,
            SE_SECURITY_NAME,
            SE_TAKE_OWNERSHIP_NAME,
            SE_LOAD_DRIVER_NAME,
            SE_SYSTEM_PROFILE_NAME,
            SE_SYSTEMTIME_NAME,
            SE_PROF_SINGLE_PROCESS_NAME,
            SE_INC_BASE_PRIORITY_NAME,
            SE_CREATE_PAGEFILE_NAME,
            SE_CREATE_PERMANENT_NAME,
            SE_BACKUP_NAME,
            SE_RESTORE_NAME,
            SE_SHUTDOWN_NAME,
            SE_DEBUG_NAME,
            SE_AUDIT_NAME,
            SE_SYSTEM_ENVIRONMENT_NAME,
            SE_CHANGE_NOTIFY_NAME,
            SE_REMOTE_SHUTDOWN_NAME,
            SE_UNDOCK_NAME,
            SE_SYNC_AGENT_NAME,
            SE_ENABLE_DELEGATION_NAME,
            SE_MANAGE_VOLUME_NAME,
            SE_IMPERSONATE_NAME,
            SE_CREATE_GLOBAL_NAME,
            SE_CREATE_SYMBOLIC_LINK_NAME,
            SE_INC_WORKING_SET_NAME,
            SE_RELABEL_NAME,
            SE_TIME_ZONE_NAME,
            SE_TRUSTED_CREDMAN_ACCESS_NAME
        }

        private const int ERROR_NOT_ALL_ASSIGNED = 1300;
        private const int SE_PRIVILEGE_ENABLED = 0x00000002;
        private const string SE_TIME_ZONE_NAMETEXT = "SeTimeZonePrivilege"; //http://msdn.microsoft.com/en-us/library/bb530716(VS.85).aspx
        private static uint STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        private static uint STANDARD_RIGHTS_READ = 0x00020000;
        private static uint TOKEN_ASSIGN_PRIMARY = 0x0001;
        private static uint TOKEN_DUPLICATE = 0x0002;
        private static uint TOKEN_IMPERSONATE = 0x0004;
        private static uint TOKEN_QUERY = 0x0008;
        private static uint TOKEN_QUERY_SOURCE = 0x0010;
        private static uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
        private static uint TOKEN_ADJUST_GROUPS = 0x0040;
        private static uint TOKEN_ADJUST_DEFAULT = 0x0080;
        private static uint TOKEN_ADJUST_SESSIONID = 0x0100;
        private static uint TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);
        private static uint TOKEN_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | TOKEN_ASSIGN_PRIMARY |
            TOKEN_DUPLICATE | TOKEN_IMPERSONATE | TOKEN_QUERY | TOKEN_QUERY_SOURCE |
            TOKEN_ADJUST_PRIVILEGES | TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT |
            TOKEN_ADJUST_SESSIONID);


        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern bool GetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass, IntPtr TokenInformation, uint TokenInformationLength, out uint ReturnLength);
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, ref LUID lpLuid);
        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool AdjustTokenPrivileges([In] IntPtr TokenHandle, [In] bool DisableAllPrivileges, [In] [Optional] ref TOKEN_PRIVILEGES NewState, [In] UInt32 BufferLengthInBytes, [Out] [Optional] out TOKEN_PRIVILEGES PreviousState, [Out] [Optional] out UInt32 ReturnLengthInBytes);
        [DllImport("kernel32")]
        internal static extern IntPtr GetCurrentProcess();
        [DllImport("kernel32")]
        internal static extern bool CloseHandle(IntPtr handle);
        [DllImport("advapi32", CharSet = CharSet.Auto)]
        internal static extern bool ConvertSidToStringSid(IntPtr pSID, [In, Out, MarshalAs(UnmanagedType.LPTStr)] ref string pStringSid);


        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int RegLoadAppKey(String hiveFile, out int hKey, RegSAM samDesired, int options, int reserved);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern Int32 RegLoadKey(IntPtr hKey, String lpSubKey, String lpFile);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern uint RegSaveKey(IntPtr hKey, string lpFile, IntPtr lpSecurityAttributes);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        internal static extern int RegSaveKeyEx(SafeRegistryHandle hKey, string fileName, IntPtr lpSecurityAttributes, int flags);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern uint RegSaveKeyEx(IntPtr hKey, string lpFile, IntPtr lpSecurityAttributes, uint Flags);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern uint RegUnLoadKey(IntPtr hKey, string SubKey);


        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern int RegCloseKey(IntPtr hKey);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int RegOpenKeyEx(IntPtr hKey, string subKey, int ulOptions, int samDesired, out IntPtr hkResult);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern uint RegSetValueEx(IntPtr hKey, [MarshalAs(UnmanagedType.LPStr)] string lpValueName, int Reserved, RegistryValueKind dwType, IntPtr lpData, int cbData);

        //[DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        //internal static extern int RegQueryValueEx(IntPtr hKey, string lpValueName, int lpReserved, ref RegistryValueKind lpType, IntPtr lpData, ref int lpcbData);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, String lpValueName, int[] lpReserved, ref int lpType, [Out] byte[] lpData, ref int lpcbData);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref RegistryValueKind lpType, IntPtr lpData, ref int lpcbData);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref RegistryValueKind lpType, [Out] byte[] lpData, ref int lpcbData);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref RegistryValueKind lpType, ref int lpData, ref int lpcbData);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref RegistryValueKind lpType, ref long lpData, ref int lpcbData);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref RegistryValueKind lpType, [Out] char[] lpData, ref int lpcbData);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref RegistryValueKind lpType, [Out]StringBuilder lpData, ref int lpcbData);

        [DllImport("Advapi32.dll", EntryPoint = "RegGetValueW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern uint RegGetValue(IntPtr hkey, string lpSubKey, string lpValue, RFlags dwFlags, out RType pdwType, IntPtr pvData, ref uint pcbData);

        [DllImport("Advapi32.dll", EntryPoint = "RegGetValueW", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern uint RegGetValue(SafeRegistryHandle hkey, string lpSubKey, string lpValue, RFlags dwFlags, out RType pdwType, IntPtr pvData, ref uint pcbData);

        public static byte[] GetSIDByteArr(IntPtr processHandle)
        {
            int MAX_INTPTR_BYTE_ARR_SIZE = 512;
            IntPtr tokenHandle;
            byte[] sidBytes;

            // Get the Process Token
            if (!OpenProcessToken(processHandle, TOKEN_READ, out tokenHandle))
                throw new ApplicationException("Could not get process token.  Win32 Error Code: " + Marshal.GetLastWin32Error());

            uint tokenInfoLength = 0;
            bool result;
            result = GetTokenInformation(tokenHandle, TOKEN_INFORMATION_CLASS.TokenUser, IntPtr.Zero, tokenInfoLength, out tokenInfoLength);  // get the token info length
            IntPtr tokenInfo = Marshal.AllocHGlobal((int)tokenInfoLength);
            result = GetTokenInformation(tokenHandle, TOKEN_INFORMATION_CLASS.TokenUser, tokenInfo, tokenInfoLength, out tokenInfoLength);  // get the token info

            // Get the User SID
            if (result)
            {
                TOKEN_USER tokenUser = (TOKEN_USER)Marshal.PtrToStructure(tokenInfo, typeof(TOKEN_USER));
                sidBytes = new byte[MAX_INTPTR_BYTE_ARR_SIZE];  // Since I don't yet know how to be more precise w/ the size of the byte arr, it is being set to 512
                Marshal.Copy(tokenUser.User.Sid, sidBytes, 0, MAX_INTPTR_BYTE_ARR_SIZE);  // get a byte[] representation of the SID
            }
            else throw new ApplicationException("Could not get process token.  Win32 Error Code: " + Marshal.GetLastWin32Error());

            return sidBytes;
        }

        public static bool DumpUserInfo(IntPtr pToken, out IntPtr SID)
        {
            IntPtr procToken = IntPtr.Zero;
            bool ret = false;
            SID = IntPtr.Zero;
            try
            {
                if (OpenProcessToken(pToken, 0X00000008, out procToken))
                {
                    WindowsIdentity wi = new WindowsIdentity(procToken);
                    ret = ProcessTokenToSid(procToken, out SID);
                    CloseHandle(procToken);
                }
                return ret;
            }
            catch
            {
                return false;
            }
        }

        private static bool ProcessTokenToSid(IntPtr token, out IntPtr SID)
        {
            TOKEN_USER tokUser;
            const int bufLength = 256;
            IntPtr tu = Marshal.AllocHGlobal(bufLength);
            bool ret = false;
            SID = IntPtr.Zero;
            try
            {
                uint cb = bufLength;
                ret = GetTokenInformation(token, TOKEN_INFORMATION_CLASS.TokenUser, tu, cb, out cb);
                if (ret)
                {
                    tokUser = (TOKEN_USER)Marshal.PtrToStructure(tu, typeof(TOKEN_USER));
                    SID = tokUser.User.Sid;
                }
                return ret;
            }
            catch
            {
                return false;
            }
            finally
            {
                Marshal.FreeHGlobal(tu);
            }
        }

        public static string ExGetProcessInfoByPID(int PID, out string SID)
        {
            IntPtr _SID = IntPtr.Zero;
            SID = String.Empty;
            try
            {
                Process process = Process.GetProcessById(PID);
                if (DumpUserInfo(process.Handle, out _SID))
                {
                    ConvertSidToStringSid(_SID, ref SID);
                }
                return process.ProcessName;
            }
            catch
            {
                return "Unknown";
            }
        }

        /// <summary>
        /// Gets the security entity value.
        /// </summary>
        /// <param name="securityEntity">The security entity.</param>
        private static string GetSecurityEntityValue(SecurityEntity securityEntity)
        {
            switch (securityEntity)
            {
                case SecurityEntity.SE_ASSIGNPRIMARYTOKEN_NAME:
                    return "SeAssignPrimaryTokenPrivilege";
                case SecurityEntity.SE_AUDIT_NAME:
                    return "SeAuditPrivilege";
                case SecurityEntity.SE_BACKUP_NAME:
                    return "SeBackupPrivilege";
                case SecurityEntity.SE_CHANGE_NOTIFY_NAME:
                    return "SeChangeNotifyPrivilege";
                case SecurityEntity.SE_CREATE_GLOBAL_NAME:
                    return "SeCreateGlobalPrivilege";
                case SecurityEntity.SE_CREATE_PAGEFILE_NAME:
                    return "SeCreatePagefilePrivilege";
                case SecurityEntity.SE_CREATE_PERMANENT_NAME:
                    return "SeCreatePermanentPrivilege";
                case SecurityEntity.SE_CREATE_SYMBOLIC_LINK_NAME:
                    return "SeCreateSymbolicLinkPrivilege";
                case SecurityEntity.SE_CREATE_TOKEN_NAME:
                    return "SeCreateTokenPrivilege";
                case SecurityEntity.SE_DEBUG_NAME:
                    return "SeDebugPrivilege";
                case SecurityEntity.SE_ENABLE_DELEGATION_NAME:
                    return "SeEnableDelegationPrivilege";
                case SecurityEntity.SE_IMPERSONATE_NAME:
                    return "SeImpersonatePrivilege";
                case SecurityEntity.SE_INC_BASE_PRIORITY_NAME:
                    return "SeIncreaseBasePriorityPrivilege";
                case SecurityEntity.SE_INCREASE_QUOTA_NAME:
                    return "SeIncreaseQuotaPrivilege";
                case SecurityEntity.SE_INC_WORKING_SET_NAME:
                    return "SeIncreaseWorkingSetPrivilege";
                case SecurityEntity.SE_LOAD_DRIVER_NAME:
                    return "SeLoadDriverPrivilege";
                case SecurityEntity.SE_LOCK_MEMORY_NAME:
                    return "SeLockMemoryPrivilege";
                case SecurityEntity.SE_MACHINE_ACCOUNT_NAME:
                    return "SeMachineAccountPrivilege";
                case SecurityEntity.SE_MANAGE_VOLUME_NAME:
                    return "SeManageVolumePrivilege";
                case SecurityEntity.SE_PROF_SINGLE_PROCESS_NAME:
                    return "SeProfileSingleProcessPrivilege";
                case SecurityEntity.SE_RELABEL_NAME:
                    return "SeRelabelPrivilege";
                case SecurityEntity.SE_REMOTE_SHUTDOWN_NAME:
                    return "SeRemoteShutdownPrivilege";
                case SecurityEntity.SE_RESTORE_NAME:
                    return "SeRestorePrivilege";
                case SecurityEntity.SE_SECURITY_NAME:
                    return "SeSecurityPrivilege";
                case SecurityEntity.SE_SHUTDOWN_NAME:
                    return "SeShutdownPrivilege";
                case SecurityEntity.SE_SYNC_AGENT_NAME:
                    return "SeSyncAgentPrivilege";
                case SecurityEntity.SE_SYSTEM_ENVIRONMENT_NAME:
                    return "SeSystemEnvironmentPrivilege";
                case SecurityEntity.SE_SYSTEM_PROFILE_NAME:
                    return "SeSystemProfilePrivilege";
                case SecurityEntity.SE_SYSTEMTIME_NAME:
                    return "SeSystemtimePrivilege";
                case SecurityEntity.SE_TAKE_OWNERSHIP_NAME:
                    return "SeTakeOwnershipPrivilege";
                case SecurityEntity.SE_TCB_NAME:
                    return "SeTcbPrivilege";
                case SecurityEntity.SE_TIME_ZONE_NAME:
                    return "SeTimeZonePrivilege";
                case SecurityEntity.SE_TRUSTED_CREDMAN_ACCESS_NAME:
                    return "SeTrustedCredManAccessPrivilege";
                case SecurityEntity.SE_UNDOCK_NAME:
                    return "SeUndockPrivilege";
                default:
                    throw new ArgumentOutOfRangeException(typeof(SecurityEntity).Name);
            }
        }

        public static void EnablePrivilege(SecurityEntity securityEntity)
        {
            if (!Enum.IsDefined(typeof(SecurityEntity), securityEntity))
                throw new InvalidEnumArgumentException("securityEntity", (int)securityEntity, typeof(SecurityEntity));

            var securityEntityValue = GetSecurityEntityValue(securityEntity);
            try
            {
                var locallyUniqueIdentifier = new LUID();

                if (LookupPrivilegeValue(null, securityEntityValue, ref locallyUniqueIdentifier))
                {
                    var tkp = new TOKEN_PRIVILEGES();
                    tkp.PrivilegeCount = 1;
                    tkp.Privileges = new LUID_AND_ATTRIBUTES[1];
                    tkp.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;
                    tkp.Privileges[0].Luid = locallyUniqueIdentifier;

                    var tokenHandle = IntPtr.Zero;
                    try
                    {
                        var currentProcess = GetCurrentProcess();
                        if (OpenProcessToken(currentProcess, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, out tokenHandle))
                        {
                            TOKEN_PRIVILEGES prvState;
                            uint retLeninBytes;
                            if (AdjustTokenPrivileges(tokenHandle, false, ref tkp, 1024, out prvState, out retLeninBytes))
                            {
                                var lastError = Marshal.GetLastWin32Error();
                                if (lastError == ERROR_NOT_ALL_ASSIGNED)
                                {
                                    var win32Exception = new Win32Exception();
                                    throw new InvalidOperationException("AdjustTokenPrivileges failed.", win32Exception);
                                }
                            }
                            else
                            {
                                var win32Exception = new Win32Exception();
                                throw new InvalidOperationException("AdjustTokenPrivileges failed.", win32Exception);
                            }
                        }
                        else
                        {
                            var win32Exception = new Win32Exception();
                            var exceptionMessage = string.Format(CultureInfo.InvariantCulture, "OpenProcessToken failed. CurrentProcess: {0}", currentProcess.ToInt32());
                            throw new InvalidOperationException(exceptionMessage, win32Exception);
                        }
                    }
                    finally
                    {
                        if (tokenHandle != IntPtr.Zero)
                        {
                            CloseHandle(tokenHandle);
                        }
                    }
                }
                else
                {
                    var win32Exception = new Win32Exception();
                    var exceptionMessage = string.Format(CultureInfo.InvariantCulture, "LookupPrivilegeValue failed. SecurityEntityValue: {0}", securityEntityValue);
                    throw new InvalidOperationException(exceptionMessage, win32Exception);
                }
            }
            catch (Exception e)
            {
                var exceptionMessage = string.Format(CultureInfo.InvariantCulture, "GrandPrivilege failed. SecurityEntity: {0}", securityEntity);
                throw new InvalidOperationException(exceptionMessage, e);
            }
        }

        internal static RegistryKey RegLoadAppKey(String hiveFile, RegSAM accessRights = RegSAM.KEY_QUERY_VALUE)
        {
            int hKey;
            int rc = RegLoadAppKey(hiveFile, out hKey, accessRights, REG_PROCESS_APPKEY, 0);

            if (rc != 0)
            {
                string errorMessage = new Win32Exception(rc).Message;
                return null;
            }

            return RegistryKey.FromHandle(new Microsoft.Win32.SafeHandles.SafeRegistryHandle(new IntPtr(hKey), true));
        }

        public static bool SaveKeyEx(this RegistryKey hKey, string fileName)
        {
            int rc = RegSaveKeyEx(hKey.Handle, fileName, IntPtr.Zero, (int)RegFileFormat.REG_STANDARD_FORMAT);

            if (rc != 0)
            {
                string errorMessage = new Win32Exception(rc).Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Renames a subkey of the passed in registry key since 
        /// the Framework totally forgot to include such a handy feature.
        /// </summary>
        /// <param name="regKey">The RegistryKey that contains the subkey 
        /// you want to rename (must be writeable)</param>
        /// <param name="subKeyName">The name of the subkey that you want to rename
        /// </param>
        /// <param name="newSubKeyName">The new name of the RegistryKey</param>
        /// <returns>True if succeeds</returns>
        public static bool RenameSubKey(this RegistryKey parentKey, string subKeyName, string newSubKeyName)
        {
            parentKey.CopyKey(subKeyName, newSubKeyName);
            parentKey.DeleteSubKeyTree(subKeyName);
            return true;
        }

        /// <summary>
        /// Copy a registry key.  The parentKey must be writeable.
        /// </summary>
        /// <param name="parentKey"></param>
        /// <param name="keyNameToCopy"></param>
        /// <param name="newKeyName"></param>
        /// <returns></returns>
        public static bool CopyKey(this RegistryKey parentKey, string keyNameToCopy, string newKeyName)
        {
            //Create new key
            using (RegistryKey destinationKey = parentKey.CreateSubKey(newKeyName))
            //Open the sourceKey we are copying from
            using (RegistryKey sourceKey = parentKey.OpenSubKey(keyNameToCopy))
            {
                RecurseCopyKey(sourceKey, destinationKey);
            }

            return true;
        }

        public static bool CopyKey(this RegistryKey sourceKey, RegistryKey destinationKey)
        {
            //Create new key
            RecurseCopyKey(sourceKey, destinationKey);
            return true;
        }

        public static List<string> ToList(this RegistryKey sourceKey)
        {
            string key = "";
            List<string> retVal = new List<string>();
            //Create new key
            RecurseConvertToList(sourceKey, key, retVal);
            return retVal;
        }

        private static string RegistryValueKindToStr(RegistryValueKind kind)
        {
            string retVal = "";
            switch(kind)
            {
                case RegistryValueKind.DWord:
                    retVal = "REG_DWORD";
                    break;
                case RegistryValueKind.QWord:
                    retVal = "REG_QWORD";
                    break;
                case RegistryValueKind.String:
                    retVal = "REG_SZ";
                    break;
                case RegistryValueKind.ExpandString:
                    retVal = "REG_EXPAND_SZ";
                    break;
                case RegistryValueKind.Binary:
                    retVal = "REG_BINARY";
                    break;
                case RegistryValueKind.MultiString:
                    retVal = "REG_MULTI_SZ";
                    break;
                case RegistryValueKind.None:
                    retVal = "REG_NONE";
                    break;
            }
            return retVal;
        }

        private static string RegistryValToStr(RegistryValueKind kind, object objValue)
        {
            string retVal = "";
            switch (kind)
            {
                case RegistryValueKind.DWord:
                case RegistryValueKind.String:
                case RegistryValueKind.ExpandString:
                    {
                        retVal = objValue.ToString();
                    }
                    break;
                case RegistryValueKind.QWord:
                    {
                        StringBuilder lineStr = new StringBuilder("");
                        if (objValue is long)
                        {
                            long tempVal = (long)objValue;
                            foreach (var item in BitConverter.GetBytes(tempVal))
                            {
                                lineStr.AppendFormat("{0:x2}", item);
                                lineStr.Append(",");
                            }
                        }
                        retVal = lineStr.ToString().Trim(',');
                    }
                    break;
                case RegistryValueKind.Binary:
                    {
                        byte[] tempVal = objValue as byte[];
                        StringBuilder lineStr = new StringBuilder("");
                        if (tempVal != null)
                        {
                            foreach (var item in tempVal)
                            {
                                lineStr.AppendFormat("{0:x2}", item);
                                lineStr.Append(",");
                            }
                        }
                        retVal = lineStr.ToString().Trim(',');
                    }
                    break;
                case RegistryValueKind.MultiString:
                    {
                        string[] tempVal = objValue as string[];
                        StringBuilder lineStr = new StringBuilder(""); 
                        if(tempVal != null)
                        {
                            foreach(var item in tempVal)
                            {
                                lineStr.Append(item);
                                lineStr.Append("\\n");
                            }
                        }
                        retVal = lineStr.ToString();
                    }
                    break;
                case RegistryValueKind.None:
                    retVal = "REG_NONE";
                    break;
            }
            return retVal;
        }

        private static void RecurseConvertToList(RegistryKey sourceKey, string key, List<string> destList)
        {
            foreach (string valueName in sourceKey.GetValueNames())
            {
                object objValue = sourceKey.GetValue(valueName);
                if (objValue != null)
                {
                    RegistryValueKind valKind = sourceKey.GetValueKind(valueName);
                    string tempVal = string.Format("{0}\\{1}={2}:{3}", key, valueName, RegistryValueKindToStr(valKind), RegistryValToStr(valKind, objValue));
                    destList.Add(tempVal);
                }
            }
            foreach (string sourceSubKeyName in sourceKey.GetSubKeyNames())
            {
                using (RegistryKey sourceSubKey = sourceKey.OpenSubKey(sourceSubKeyName))
                {
                    string subKey;
                    if (string.IsNullOrWhiteSpace(key))
                    {
                        subKey = sourceSubKeyName;
                    }
                    else
                    {
                        subKey = string.Format("{0}\\{1}", key, sourceSubKeyName);
                    }
                    RecurseConvertToList(sourceSubKey, subKey, destList);
                }
            }
        }

        internal static object GetValueEx(this RegistryKey pThisKey, String name)
        {
            object data = null;
            RegistryValueKind type = 0;
            int datasize = 0;

            int ret = RegQueryValueEx(pThisKey.Handle, name, null, ref type, IntPtr.Zero, ref datasize);

            if (ret != 0)
            {
                return null;
            }

            if (datasize < 0)
            {
                return null;
            }


            switch ((int)type)
            {
                case 0x400002:
                    {
                        if (datasize % 2 == 1)
                        {
                            // handle the case where the registry contains an odd-byte length (corrupt data?)
                            try
                            {
                                datasize = checked(datasize + 1);
                            }
                            catch (OverflowException)
                            {
                                return null;
                            }
                        }
                        char[] blob = new char[datasize / 2];

                        ret = RegQueryValueEx(pThisKey.Handle, name, null, ref type, blob, ref datasize);

                        if (blob.Length > 0 && blob[blob.Length - 1] == (char)0)
                        {
                            data = new String(blob, 0, blob.Length - 1);
                        }
                        else
                        {
                            // in the very unlikely case the data is missing null termination, 
                            // pass in the whole char[] to prevent truncating a character
                            data = new String(blob);
                        }

                    }
                    break;
                case 0x400007:
                    {
                        if (datasize % 2 == 1)
                        {
                            // handle the case where the registry contains an odd-byte length (corrupt data?)
                            try
                            {
                                datasize = checked(datasize + 1);
                            }
                            catch (OverflowException)
                            {
                                return null;
                            }
                        }
                        char[] blob = new char[datasize / 2];

                        ret = RegQueryValueEx(pThisKey.Handle, name, null, ref type, blob, ref datasize);

                        // make sure the string is null terminated before processing the data
                        if (blob.Length > 0 && blob[blob.Length - 1] != (char)0)
                        {
                            try
                            {
                                char[] newBlob = new char[checked(blob.Length + 1)];
                                for (int i = 0; i < blob.Length; i++)
                                {
                                    newBlob[i] = blob[i];
                                }
                                newBlob[newBlob.Length - 1] = (char)0;
                                blob = newBlob;
                            }
                            catch (OverflowException)
                            {
                                return null;
                            }
                            blob[blob.Length - 1] = (char)0;
                        }


                        IList<String> strings = new List<String>();
                        int cur = 0;
                        int len = blob.Length;

                        while (ret == 0 && cur < len)
                        {
                            int nextNull = cur;
                            while (nextNull < len && blob[nextNull] != (char)0)
                            {
                                nextNull++;
                            }

                            if (nextNull < len)
                            {
                                if (nextNull - cur > 0)
                                {
                                    strings.Add(new String(blob, cur, nextNull - cur));
                                }
                                else
                                {
                                    // we found an empty string.  But if we're at the end of the data, 
                                    // it's just the extra null terminator. 
                                    if (nextNull != len - 1)
                                    {
                                        strings.Add(String.Empty);
                                    }
                                }
                            }
                            else
                            {
                                strings.Add(new String(blob, cur, len - cur));
                            }
                            cur = nextNull + 1;
                        }

                        data = new String[strings.Count];
                        strings.CopyTo((String[])data, 0);
                    }
                    break;
                default:
                    {
                        if (datasize % 2 == 1)
                        {
                            // handle the case where the registry contains an odd-byte length (corrupt data?)
                            try
                            {
                                datasize = checked(datasize + 1);
                            }
                            catch (OverflowException)
                            {
                                return null;
                            }
                        }
                        char[] blob = new char[datasize / 2];

                        ret = RegQueryValueEx(pThisKey.Handle, name, null, ref type, blob, ref datasize);
                        if (blob.Length > 0 && blob[blob.Length - 1] == (char)0)
                        {
                            var tempRetVal = new String(blob, 0, blob.Length - 1);
                            if(string.IsNullOrWhiteSpace(tempRetVal))
                            {
                                data = "";
                            }
                            else
                            {
                                data = tempRetVal.Trim('\0');
                            }
                        }
                        else
                        {
                            // in the very unlikely case the data is missing null termination, 
                            // pass in the whole char[] to prevent truncating a character
                            var tempRetVal = new String(blob);
                            if (string.IsNullOrWhiteSpace(tempRetVal))
                            {
                                data = "";
                            }
                            else
                            {
                                data = tempRetVal.Trim('\0');
                            }
                        }
                    }
                    break;
            }
            return data;
        }

        public static RegistryValueKind GetValueKindEx(this RegistryKey pThis, string name)
        {
            int num = 0;
            int num2 = 0;
            int num3 = RegQueryValueEx(pThis.Handle, name, (int[])null, ref num, (byte[])null, ref num2);
            if (num3 != 0)
            {
                return RegistryValueKind.Unknown;
            }
            if (num == 0)
            {
                return RegistryValueKind.None;
            }
            if (!Enum.IsDefined(typeof(RegistryValueKind), num))
            {
                return (RegistryValueKind)num;
            }
            return (RegistryValueKind)num;
        }

        private static void RecurseCopyKey(RegistryKey sourceKey, RegistryKey destinationKey)
        {
            if(sourceKey == null || destinationKey == null)
            {
                return;
            }
            //copy all the values
            foreach (string valueName in sourceKey.GetValueNames())
            {
                //if(valueName == "TestValue_REG_MULTI_SZ")
                //{
                //    Debugger.Break();
                //}
                RegistryValueKind valKind = sourceKey.GetValueKindEx(valueName);
                object objValue;
                if (Enum.IsDefined(typeof(RegistryValueKind), valKind))
                {
                    objValue = sourceKey.GetValue(valueName, null, RegistryValueOptions.DoNotExpandEnvironmentNames);
                }
                else
                {
                    switch ((int)valKind)
                    {
                        default:
                        case 0x40001:
                        case 0x40000:
                            valKind = RegistryValueKind.String;
                            break;
                        case 0x400007:
                            valKind = RegistryValueKind.MultiString;
                            break;
                        case 0x400002:
                            valKind = RegistryValueKind.ExpandString;
                            break;
                    }
                    objValue = sourceKey.GetValueEx(valueName);
                }
                if (objValue != null)
                {
                    destinationKey.SetValue(valueName, objValue, valKind);
                }
            }

            //For Each subKey 
            //Create a new subKey in destinationKey 
            //Call myself 
            foreach (string sourceSubKeyName in sourceKey.GetSubKeyNames())
            {
                //if (sourceSubKeyName == "InprocServer32")
                //{
                //    Debugger.Break();
                //}
                using (RegistryKey sourceSubKey = sourceKey.OpenSubKey(sourceSubKeyName))
                using (RegistryKey destSubKey = destinationKey.CreateSubKey(sourceSubKeyName))
                {
                    RecurseCopyKey(sourceSubKey, destSubKey);
                }
            }
        }
    }
}
