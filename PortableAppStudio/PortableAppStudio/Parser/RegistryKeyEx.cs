using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Parser
{
    public class RegistryKeyEx
    {
        RegistryKey regKey;

        public RegistryKeyEx(RegistryKey key)
        {
            regKey = key;
        }

        
        internal Object GetValue(String name)
        {
            Object data = null;
            RegistryValueKind type = 0;
            int datasize = 0;

            int ret = RegistryNativeMethods.RegQueryValueEx(regKey.Handle, name,null, ref type, IntPtr.Zero, ref datasize);

            if (ret != 0)
            {
                return null;
            }

            if (datasize < 0)
            {
                return null;
            }


            switch (type)
            {
                case RegistryValueKind.None:
                case RegistryValueKind.Binary:
                    {
                        byte[] blob = new byte[datasize];
                        ret = RegistryNativeMethods.RegQueryValueEx(regKey.Handle, name, null, ref type, blob, ref datasize);
                        data = blob;
                    }
                    break;
                case RegistryValueKind.QWord:
                    {    // also REG_QWORD_LITTLE_ENDIAN
                        if (datasize > 8)
                        {
                            // prevent an AV in the edge case that datasize is larger than sizeof(long)
                            goto case RegistryValueKind.Binary;
                        }
                        long blob = 0;
                        // Here, datasize must be 8 when calling this
                        ret = RegistryNativeMethods.RegQueryValueEx(regKey.Handle, name, null, ref type, ref blob, ref datasize);

                        data = blob;
                    }
                    break;
                case RegistryValueKind.DWord:
                    {    // also REG_DWORD_LITTLE_ENDIAN
                        if (datasize > 4)
                        {
                            // prevent an AV in the edge case that datasize is larger than sizeof(int)
                            goto case RegistryValueKind.QWord;
                        }
                        int blob = 0;
                        // Here, datasize must be four when calling this
                        ret = RegistryNativeMethods.RegQueryValueEx(regKey.Handle, name, null, ref type, ref blob, ref datasize);

                        data = blob;
                    }
                    break;

                case RegistryValueKind.String:
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

                        ret = RegistryNativeMethods.RegQueryValueEx(regKey.Handle, name, null, ref type, blob, ref datasize);
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

                case RegistryValueKind.ExpandString:
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

                        ret = RegistryNativeMethods.RegQueryValueEx(regKey.Handle, name, null, ref type, blob, ref datasize);

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
                case RegistryValueKind.MultiString:
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

                        ret = RegistryNativeMethods.RegQueryValueEx(regKey.Handle, name, null, ref type, blob, ref datasize);

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
                                        strings.Add(String.Empty);
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
                    break;
            }

            return data;
        }

    }
}
