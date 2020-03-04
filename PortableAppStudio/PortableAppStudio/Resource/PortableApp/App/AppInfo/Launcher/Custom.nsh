${SegmentFile}

${SegmentInit}
    ${If} $Bits = 64
        ;${SetEnvironmentVariablesPath} 
    ${Else}
        ;${SetEnvironmentVariablesPath} 
    ${EndIf}
	StrCpy $0 "%APPDATA%\..\LocalLow"
	${ParseLocations} $0
	System::Call 'kernel32::GetFullPathName(t r0, i ${NSIS_MAX_STRLEN}, t .r2, i 0) i .r3'
	System::Call 'Kernel32::SetEnvironmentVariable(t, t) i("LOCALLOWAPPDATA", t r2).r0'
!macroend

${SegmentPrePrimary}
	${ForEachINIPair} DirectoriesLink $0 $1
		
		${If} $0 == -
			MessageBox MB_ICONSTOP "DirectoriesLink key -, not supported use DirectoriesMove $0 (no file copy)."
		${ElseIf} $0 == settings
			MessageBox MB_ICONSTOP "DON'T YOU DARE DO THAT! (You can't [DirectoriesMove] settings)"
		${EndIf}

		StrCpy $0 $DataDirectory\$0
		${ParseLocations} $1
		System::Call 'kernel32::GetFullPathName(t r1, i ${NSIS_MAX_STRLEN}, t .r2, i 0) i .r3'
		${DebugMsg} "Link Path $2"
		${If} ${FileExists} $2
			; backup already left over files by someone
			${DebugMsg} "Backing up $2 to $2.BackupBy$AppID"
			Rename $2 $2.BackupBy$AppID
		${EndIf}

		${DebugMsg} "Linking $2 to $0"
		System::Call "kernel32::CreateSymbolicLink(t r2, t r0, i 3) i .s" 
	${NextINIPair}
!macroend

${SegmentPostPrimary}
	${ForEachINIPair} DirectoriesLink $0 $1

		${ParseLocations} $1
		System::Call 'kernel32::GetFullPathName(t r1, i ${NSIS_MAX_STRLEN}, t .r2, i 0) i .r3'
		${DebugMsg} "Removing Link $1"
		RMDir $2
		${DebugMsg} "Restoring from $2 to $2.BackupBy$AppID"
		Rename $2.BackupBy$AppID $2
	${NextINIPair}
!macroend
