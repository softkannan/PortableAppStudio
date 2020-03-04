using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableAppStudio.Model.LaunchINI
{
    public partial class LaunchINI
    {
        public const string RegistryKeys_Tag = "[RegistryKeys]";
        public const string RegistryValueWrite_Tag = "[RegistryValueWrite]";
        public const string RegistryCleanupIfEmpty_Tag = "[RegistryCleanupIfEmpty]";
        public const string RegistryCleanupForce_Tag = "[RegistryCleanupForce]";
        public const string RegistryValueBackupDelete_Tag = "[RegistryValueBackupDelete]";
        public const string FilesMove_Tag = "[FilesMove]";
        public const string DirectoriesMove_Tag = "[DirectoriesMove]";
        public const string DirectoriesCleanupIfEmpty_Tag = "[DirectoriesCleanupIfEmpty]";
        public const string DirectoriesCleanupForce_Tag = "[DirectoriesCleanupForce]";
        public const string FileWriteN_Tag = "[FileWriteN]";
        public const string RegistrationFreeCOM_Tag = "[RegistrationFreeCOM]";
        public const string QtKeysCleanup_Tag = "[QtKeysCleanup]";
        public const string Environment_Tag = "[Environment]";
        public const string DirectoriesLink_Tag = "[DirectoriesLink]";

        public LaunchINI()
        {
            Create();
        }
        public LaunchSection Launch { get; set; }

        public ActivateSection Activate { get; set; }

        public LiveModeSection LiveMode { get; set; }

        public  INIValueList<EnvironmentSection> Environment { get; set; }

        public INIValueList<RegistryKeysSection> RegistryKeys { get; set; }

        public INIValueList<RegistryValueWriteSection> RegistryValueWrite { get; set; }

        public INIValueList<RegistryCleanupIfEmptySection> RegistryCleanupIfEmpty { get; set; }

        public INIValueList<RegistryCleanupForceSection> RegistryCleanupForce { get; set; }

        public INIValueList<RegistryValueBackupDeleteSection> RegistryValueBackupDelete { get; set; }

        public INIValueList<QtKeysCleanupSection> QtKeysCleanup { get; set; }

        public INISectionList<FileWriteNSection> FileWriteN { get; set; }

        public INIValueList<FilesMoveSection> FilesMove { get; set; }

        public INIValueList<DirectoriesMoveSection> DirectoriesMove { get; set; }

        public INIValueList<DirectoriesCleanupIfEmptySection> DirectoriesCleanupIfEmpty { get; set; }

        public INIValueList<DirectoriesCleanupForceSection> DirectoriesCleanupForce { get; set; }

        public LanguageSection Language { get;set;}

        public INIValueList<LanguageStringsSection> LanguageStrings { get; set; }

        public LanguageFileSection LanguageFile { get; set; }

        public INIValueList<RegistrationFreeCOMSection> RegistrationFreeCOM { get; set; }

        public INIValueList<DirectoriesLinkSection> DirectoriesLink { get; set; }

    }
}
