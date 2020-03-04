// Decompiled with JetBrains decompiler
// Type: PafLauncherEditor.AppEnvironments
// Assembly: PAFLauncherEditor, Version=5.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5FA988B0-28E9-47EB-9AB7-80FDA0B74460
// Assembly location: G:\csharp\PortableAppGen\Reference\%DEFAULT FOLDER%\PAFLauncherEditor.exe

using System;
using System.Collections.Generic;
using System.Linq;

namespace PafLauncherEditor
{
  public class AppEnvironments 
  {
    private static readonly AppEnvironments instance = new AppEnvironments();
    private Dictionary<string, string> _environments;

    public static AppEnvironments Instance
    {
      get
      {
        return AppEnvironments.instance;
      }
    }

    private AppEnvironments()
    {
      this.Reset();
    }

    public Dictionary<string, string> Environments
    {
      get
      {
        return this._environments;
      }
      private set
      {
        if (this._environments == value)
          return;
        this._environments = value;
      }
    }

    public void Add(string key, string value, bool update = false)
    {
      string envKey = string.Format("%{0}%", (object) key.Trim('%'));
      if (!update && this.Environments.All<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (k => !string.Equals(k.Key, envKey, StringComparison.OrdinalIgnoreCase))))
      {
        this._environments.Add(envKey, value);
      }
      else
      {
        KeyValuePair<string, string> keyValuePair = this.Environments.FirstOrDefault<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (k => string.Equals(k.Key, envKey, StringComparison.OrdinalIgnoreCase)));
        if (keyValuePair.Equals((object) null))
          return;
        this._environments.Remove(keyValuePair.Key);
        this._environments.Add(envKey, value);
      }
    }

    public void Remove(string key)
    {
      this._environments.Remove(string.Format("%{0}%", (object) key.Trim('%')));
    }

    public void Reset()
    {
      this._environments = new Dictionary<string, string>()
      {
        {
          "%COMPUTERNAME%",
          "XP= {computer name}\nVista/7/8= {computer name}"
        },
        {
          "%COMMONPROGRAMFILES%",
          "XP= C:\\Program Files\\Common Files\nVista/7/8= C:\\Program Files\\Common Files"
        },
        {
          "%COMMONPROGRAMFILES(x86)%",
          "XP= C:\\Program Files (x86)\\Common Files  ''(only in 64-bit version)''\nVista/7/8= C:\\Program Files (x86)\\Common Files  ''(only in 64-bit version)''"
        },
        {
          "%COMSPEC%",
          "XP= C:\\Windows\\System32\\cmd.exe\nVista/7/8= C:\\Windows\\System32\\cmd.exe"
        },
        {
          "%HOMEDRIVE%",
          "XP= C:\nVista/7/8= C:"
        },
        {
          "%SystemDrive%",
          "XP= C:\nVista/7/8= C:"
        },
        {
          "%HOMEPATH%",
          "XP= \\Documents and SettingsPath\\{username}\nVista/7/8= \\Users\\{username}"
        },
        {
          "%APPDATA%",
          "XP= C:\\Documents and SettingsPath\\{username}\\Application Data\nVista/7/8= C:\\Users\\{username}\\AppData\\Roaming"
        },
        {
          "%LOCALAPPDATA%",
          "XP= C:\\Documents and SettingsPath\\{username}\\Local SettingsPath\\Application Data\nVista/7/8= C:\\Users\\{username}\\AppData\\Local"
        },
        {
          "%LOGONSERVER%",
          "XP= \\{domain_logon_server}\nVista/7/8= \\{domain_logon_server}"
        },
        {
          "%PATH%",
          "XP= C:\\Windows\\system32;C:\\Windows;C:\\Windows\\System32\\Wbem;{plus program paths}\nVista/7/8= C:\\Windows\\system32;C:\\Windows;C:\\Windows\\System32\\Wbem;{plus program paths}"
        },
        {
          "%PATHEXT%",
          "XP= .COM;.EXE;.BAT;.CMD;.VBS;.VBE;.JS;.WSF;.WSH\nVista/7/8= .com;.exe;.bat;.cmd;.vbs;.vbe;.js;.jse;.wsf;.wsh;.msc"
        },
        {
          "%PROGRAMDATA%",
          "XP= \nVista/7/8= %SystemDrive%\\ProgramData"
        },
        {
          "%PROGRAMFILES%",
          "XP= %SystemDrive%\\Program Files\nVista/7/8=  %SystemDrive%\\Program Files"
        },
        {
          "%PROGRAMFILES(X86)%",
          "XP= %SystemDrive%\\Program Files (x86)  ''(only in 64-bit version)''\nVista/7/8= %SystemDrive%\\Program Files (x86)  ''(only in 64-bit version)''"
        },
        {
          "%PROMPT%",
          "XP= Code for current command prompt format. Code is usually $P$G\nVista/7/8= Code for current command prompt format. Code is usually $P$G"
        },
        {
          "%PSModulePath%",
          "XP= \nVista/7/8= %SystemRoot%\\system32\\WindowsPowerShell\\v1.0\\Modules\\"
        },
        {
          "%PUBLIC%",
          "XP= \nVista/7/8= %SystemDrive%\\Users\\Public"
        },
        {
          "\\$Recycle.Bin",
          "XP= \nVista/7/8= C:\\$Recycle.Bin"
        },
        {
          "%SystemRoot%",
          "XP= C:\\Windows, formerly C:\\WINNT\nVista/7/8= %SystemDrive%\\Windows"
        },
        {
          "%TEMP%",
          "XP= %SystemDrive%\\Documents and SettingsPath\\{username}\\Local SettingsPath\\Temp\nVista/7/8= %SystemDrive%\\Users\\{username}\\AppData\\Local\\Temp"
        },
        {
          "%TMP%",
          "XP= %SystemDrive%\\Documents and SettingsPath\\{username}\\Local SettingsPath\\Temp\nVista/7/8= %SystemDrive%\\Users\\{username}\\AppData\\Local\\Temp"
        },
        {
          "%USERDOMAIN%",
          "XP= {user domain}\nVista/7/8= {user domain}"
        },
        {
          "%DOCUMENTS%",
          "XP= C:\\Documents and SettingsPath\\{username}\\My Documents\nVista/7/8= C:\\Users\\{username}\\Documents"
        },
        {
          "%USERNAME%",
          "XP= {username}\nVista/7/8= {username}"
        },
        {
          "%USERPROFILE%",
          "XP= C:\\Documents and SettingsPath\\{username}\nVista/7/8= C:\\Users\\{username}"
        },
        {
          "%ALLUSERSPROFILE%",
          "XP= C:\\Documents and SettingsPath\\All Users\nVista/7/8= C:\\ProgramData"
        },
        {
          "%ALLUSERSAPPDATA%",
          "XP= C:\\Documents and SettingsPath\\All Users\\Application Data\nVista/7/8= C:\\ProgramData"
        },
        {
          "%windir%",
          "XP= %SystemDrive%\\WINDOWS\nVista/7/8= %SystemDrive%\\WINDOWS"
        },
        {
          "%PAL:Drive%",
          "X:"
        },
        {
          "%PAL:LastDrive%",
          "W:"
        },
        {
          "%PAL:DriveLetter%",
          "X"
        },
        {
          "%PAL:LastDriveLetter%",
          "W"
        },
        {
          "%PAL:AppDir%",
          "X:\\PortableApps\\AppNamePortable\\App or\n%TEMP%\\AppNamePortableLive\\App in Live mode except when[LiveMode]:CopyApp is set to false"
        },
        {
          "%PAL:DataDir%",
          "X:\\PortableApps\\AppNamePortable\\Data or\n%TEMP%\\AppNamePortableLive\\Data in Live mode"
        },
        {
          "%JAVA_HOME%",
          "X:\\PortableApps\\CommonFiles\\Java or\nC:\\Program Files\\Java when not run from platform directory"
        },
        {
          "%PortableApps.comDocuments%",
          "X:\\Documents"
        },
        {
          "%PortableApps.comPictures%",
          "X:\\Documents\\Pictures"
        },
        {
          "%PortableApps.comMusic%",
          "X:\\Documents\\Music"
        },
        {
          "%PortableApps.comVideos%",
          "X:\\Documents\\Videos"
        },
        {
          "%PAL:PortableAppsDir%",
          "X:\\PortableApps"
        },
        {
          "%PAL:PortableAppsBaseDir%",
          "where PAL:PortableAppsDir and PortableApps.comDocuments are"
        },
        {
          "%PAL:LastPortableAppsBaseDir%",
          "The value of PAL:PortableAppsBaseDir from the previous run"
        },
        {
          "%:DoubleBackslash%",
          "X:\\\\PortableApps\\\\AppNamePortable\\\\App"
        },
        {
          "%:ForwardSlash%",
          "X:/PortableApps/AppNamePortable/App"
        },
        {
          "%:java.util.prefs%",
          "/X:///Portable/Apps///App/Name/Portable///App"
        },
        {
          "%PAL:PackagePartialDir%",
          "The path, minus the drive letter and colon, from which the portable app is running. eg:\n\\PortableApps\\AppNamePortable"
        },
        {
          "%PAL:LastPackagePartialDir%",
          "The path, minus the drive letter and colon, from which the portable app ran last. eg:\n\\Apps\\AppNamePortable"
        },
        {
          "%PortableApps.comLanguageCode%",
          "e.g. “en”, “pt”, “pt-br”"
        },
        {
          "%PortableApps.comLocaleCode2%",
          "e.g. “en”, “pt”, “pt”"
        },
        {
          "%PortableApps.comLocaleCode3%",
          "e.g. “eng”, “por”, “por”"
        },
        {
          "%PortableApps.comLocaleglibc%",
          "e.g. “en_US”, “pt”, “pt_BR”"
        },
        {
          "%PortableApps.comLocaleID%",
          "e.g. “1033”, “2070”, “1046”"
        },
        {
          "%PortableApps.comLocaleName%",
          " e.g. “LANG_ENGLISH”, “LANG_PORTUGUESE”, “LANG_PORTUGUESEBR”"
        },
        {
          "%PortableApps.comLocaleWinName%",
          "e.g. “ENGLISH”, “PORTUGUESE”, “PORTUGUESEBR”"
        },
        {
          "%PAL:LanguageCustom%",
          "a custom variable constructed in the [Language] and [LanguageStrings] sections."
        }
      };
    }

    private Dictionary<string, string> Default()
    {
      return new Dictionary<string, string>()
      {
        {
          "%COMPUTERNAME%",
          "XP= {computer name}\nVista/7/8= {computer name}"
        },
        {
          "%COMMONPROGRAMFILES%",
          "XP= C:\\Program Files\\Common Files\nVista/7/8= C:\\Program Files\\Common Files"
        },
        {
          "%COMMONPROGRAMFILES(x86)%",
          "XP= C:\\Program Files (x86)\\Common Files  ''(only in 64-bit version)''\nVista/7/8= C:\\Program Files (x86)\\Common Files  ''(only in 64-bit version)''"
        },
        {
          "%COMSPEC%",
          "XP= C:\\Windows\\System32\\cmd.exe\nVista/7/8= C:\\Windows\\System32\\cmd.exe"
        },
        {
          "%HOMEDRIVE%",
          "XP= C:\nVista/7/8= C:"
        },
        {
          "%SystemDrive%",
          "XP= C:\nVista/7/8= C:"
        },
        {
          "%HOMEPATH%",
          "XP= \\Documents and SettingsPath\\{username}\nVista/7/8= \\Users\\{username}"
        },
        {
          "%APPDATA%",
          "XP= C:\\Documents and SettingsPath\\{username}\\Application Data\nVista/7/8= C:\\Users\\{username}\\AppData\\Roaming"
        },
        {
          "%LOCALAPPDATA%",
          "XP= C:\\Documents and SettingsPath\\{username}\\Local SettingsPath\\Application Data\nVista/7/8= C:\\Users\\{username}\\AppData\\Local"
        },
        {
          "%LOGONSERVER%",
          "XP= \\{domain_logon_server}\nVista/7/8= \\{domain_logon_server}"
        },
        {
          "%PATH%",
          "XP= C:\\Windows\\system32;C:\\Windows;C:\\Windows\\System32\\Wbem;{plus program paths}\nVista/7/8= C:\\Windows\\system32;C:\\Windows;C:\\Windows\\System32\\Wbem;{plus program paths}"
        },
        {
          "%PATHEXT%",
          "XP= .COM;.EXE;.BAT;.CMD;.VBS;.VBE;.JS;.WSF;.WSH\nVista/7/8= .com;.exe;.bat;.cmd;.vbs;.vbe;.js;.jse;.wsf;.wsh;.msc"
        },
        {
          "%PROGRAMDATA%",
          "XP= \nVista/7/8= %SystemDrive%\\ProgramData"
        },
        {
          "%PROGRAMFILES%",
          "XP= %SystemDrive%\\Program Files\nVista/7/8=  %SystemDrive%\\Program Files"
        },
        {
          "%PROGRAMFILES(X86)%",
          "XP= %SystemDrive%\\Program Files (x86)  ''(only in 64-bit version)''\nVista/7/8= %SystemDrive%\\Program Files (x86)  ''(only in 64-bit version)''"
        },
        {
          "%PROMPT%",
          "XP= Code for current command prompt format. Code is usually $P$G\nVista/7/8= Code for current command prompt format. Code is usually $P$G"
        },
        {
          "%PSModulePath%",
          "XP= \nVista/7/8= %SystemRoot%\\system32\\WindowsPowerShell\\v1.0\\Modules\\"
        },
        {
          "%PUBLIC%",
          "XP= \nVista/7/8= %SystemDrive%\\Users\\Public"
        },
        {
          "\\$Recycle.Bin",
          "XP= \nVista/7/8= C:\\$Recycle.Bin"
        },
        {
          "%SystemRoot%",
          "XP= C:\\Windows, formerly C:\\WINNT\nVista/7/8= %SystemDrive%\\Windows"
        },
        {
          "%TEMP%",
          "XP= %SystemDrive%\\Documents and SettingsPath\\{username}\\Local SettingsPath\\Temp\nVista/7/8= %SystemDrive%\\Users\\{username}\\AppData\\Local\\Temp"
        },
        {
          "%TMP%",
          "XP= %SystemDrive%\\Documents and SettingsPath\\{username}\\Local SettingsPath\\Temp\nVista/7/8= %SystemDrive%\\Users\\{username}\\AppData\\Local\\Temp"
        },
        {
          "%USERDOMAIN%",
          "XP= {user domain}\nVista/7/8= {user domain}"
        },
        {
          "%DOCUMENTS%",
          "XP= C:\\Documents and SettingsPath\\{username}\\My Documents\nVista/7/8= C:\\Users\\{username}\\Documents"
        },
        {
          "%USERNAME%",
          "XP= {username}\nVista/7/8= {username}"
        },
        {
          "%USERPROFILE%",
          "XP= C:\\Documents and SettingsPath\\{username}\nVista/7/8= C:\\Users\\{username}"
        },
        {
          "%ALLUSERSPROFILE%",
          "XP= C:\\Documents and SettingsPath\\All Users\nVista/7/8= C:\\ProgramData"
        },
        {
          "%ALLUSERSAPPDATA%",
          "XP= C:\\Documents and SettingsPath\\All Users\\Application Data\nVista/7/8= C:\\ProgramData"
        },
        {
          "%windir%",
          "XP= %SystemDrive%\\WINDOWS\nVista/7/8= %SystemDrive%\\WINDOWS"
        },
        {
          "%PAL:Drive%",
          "X:"
        },
        {
          "%PAL:LastDrive%",
          "W:"
        },
        {
          "%PAL:DriveLetter%",
          "X"
        },
        {
          "%PAL:LastDriveLetter%",
          "W"
        },
        {
          "%PAL:AppDir%",
          "X:\\PortableApps\\AppNamePortable\\App or\n%TEMP%\\AppNamePortableLive\\App in Live mode except when[LiveMode]:CopyApp is set to false"
        },
        {
          "%PAL:DataDir%",
          "X:\\PortableApps\\AppNamePortable\\Data or\n%TEMP%\\AppNamePortableLive\\Data in Live mode"
        },
        {
          "%JAVA_HOME%",
          "X:\\PortableApps\\CommonFiles\\Java or\nC:\\Program Files\\Java when not run from platform directory"
        },
        {
          "%PortableApps.comDocuments%",
          "X:\\Documents"
        },
        {
          "%PortableApps.comPictures%",
          "X:\\Documents\\Pictures"
        },
        {
          "%PortableApps.comMusic%",
          "X:\\Documents\\Music"
        },
        {
          "%PortableApps.comVideos%",
          "X:\\Documents\\Videos"
        },
        {
          "%PAL:PortableAppsDir%",
          "X:\\PortableApps"
        },
        {
          "%PAL:PortableAppsBaseDir%",
          "where PAL:PortableAppsDir and PortableApps.comDocuments are"
        },
        {
          "%PAL:LastPortableAppsBaseDir%",
          "The value of PAL:PortableAppsBaseDir from the previous run"
        },
        {
          "%:DoubleBackslash%",
          "X:\\\\PortableApps\\\\AppNamePortable\\\\App"
        },
        {
          "%:ForwardSlash%",
          "X:/PortableApps/AppNamePortable/App"
        },
        {
          "%:java.util.prefs%",
          "/X:///Portable/Apps///App/Name/Portable///App"
        },
        {
          "%PAL:PackagePartialDir%",
          "The path, minus the drive letter and colon, from which the portable app is running. eg:\n\\PortableApps\\AppNamePortable"
        },
        {
          "%PAL:LastPackagePartialDir%",
          "The path, minus the drive letter and colon, from which the portable app ran last. eg:\n\\Apps\\AppNamePortable"
        },
        {
          "%PortableApps.comLanguageCode%",
          "e.g. “en”, “pt”, “pt-br”"
        },
        {
          "%PortableApps.comLocaleCode2%",
          "e.g. “en”, “pt”, “pt”"
        },
        {
          "%PortableApps.comLocaleCode3%",
          "e.g. “eng”, “por”, “por”"
        },
        {
          "%PortableApps.comLocaleglibc%",
          "e.g. “en_US”, “pt”, “pt_BR”"
        },
        {
          "%PortableApps.comLocaleID%",
          "e.g. “1033”, “2070”, “1046”"
        },
        {
          "%PortableApps.comLocaleName%",
          " e.g. “LANG_ENGLISH”, “LANG_PORTUGUESE”, “LANG_PORTUGUESEBR”"
        },
        {
          "%PortableApps.comLocaleWinName%",
          "e.g. “ENGLISH”, “PORTUGUESE”, “PORTUGUESEBR”"
        },
        {
          "%PAL:LanguageCustom%",
          "a custom variable constructed in the [Language] and [LanguageStrings] sections."
        }
      };
    }
  }
}
