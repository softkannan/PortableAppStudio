/*
    Copyright 1999-2003,2007 TiANWEi

    This file is part of Regshot.

    Regshot is free software; you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 2 of the License, or
    (at your option) any later version.

    Regshot is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Regshot; if not, write to the Free Software
    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/

#include "global.h"

LPSTR	str_DefaultLanguage	="English";
LPSTR	str_ItemTranslator	="Translator";
LPSTR	str_SectionCurrent	="CURRENT";
LPSTR	str_Original		="[Original]";

//This is the Pointer to Language Strings
u_char *	lan_key;
u_char *	lan_value;
u_char *	lan_dir;
u_char *	lan_file;
u_char *	lan_time;
u_char *	lan_keyadd;
u_char *	lan_keydel;
u_char *	lan_valadd;
u_char *	lan_valdel;
u_char *	lan_valmodi;
u_char *	lan_fileadd;
u_char *	lan_filedel;
u_char *	lan_filemodi;
u_char *	lan_diradd;
u_char *	lan_dirdel;
u_char *	lan_dirmodi;
u_char *	lan_total;
u_char *	lan_comments;
u_char *	lan_datetime;
u_char *	lan_computer;
u_char *	lan_username;
u_char *	lan_about;
u_char *	lan_error;
u_char *	lan_errorexecviewer;
u_char *	lan_errorcreatefile;
u_char *	lan_erroropenfile;
u_char *	lan_errormovefp;
u_char *	lan_menushot;
u_char *	lan_menushotsave;
u_char *	lan_menuload;
u_char *	lan_menuclearallshots;
u_char *	lan_menuclearshot1;
u_char *	lan_menuclearshot2;

//This is the dimension for MultiLanguage Default Strings[English]
unsigned char lan_default[][22]=
{
"Keys:",
"Values:",
"Dirs:",
"Files:",
"Time:",
"Keys added:",
"Keys deleted:",
"Values added:",
"Values deleted:",
"Values modified:",
"Files added:",
"Files deleted:",
"Files[attr]modified:",
"Folders added:",
"Folders deleted:",
"Folders[attr]changed:",
"Total changes:",
"Comments:",
"Datetime:",
"Computer:",
"Username:",
"About",
"Error",
"Error call ex-viewer",
"Error create file",
"Error open file",
"Error move fp",
"&1st shot",
"&2nd shot",
"c&Ompare",
"&Clear",
"&Quit",
"&About",
"&Monitor",
"Compare logs save as:",
"Output path:",
"Add comment into log:",
"Plain &TXT",
"&HTML document",
"&Scan dir1[;dir2;...]",
"&Shot",
"Shot and Sa&ve...",
"Loa&d...",
"&Clear All",
"Clear &1st shot",
"Clear &2nd shot"
};


//--------------------------------------------------
// Get language types 
//--------------------------------------------------
BOOL	GetLanguageType(HWND hDlg)
{
	DWORD	nReturn;
	BOOL	bRet;
	LPSTR	lp;
	LPSTR	lpSectionNames=MYALLOC0(SIZEOF_LANGUAGE_SECTIONNAMES_BUFFER);
	//LPSTR	lpCurrentLanguage=MYALLOC0(SIZEOF_SINGLE_LANGUAGENAME);

	
	nReturn=GetPrivateProfileSectionNames(lpSectionNames,SIZEOF_LANGUAGE_SECTIONNAMES_BUFFER,lpIni);
	if (nReturn>1)
	{
		bRet=TRUE;
		for(lp=lpSectionNames;*lp!=0;lp=lp+strlen(lp)+1)
		{
			if(strcmpi(lp,str_SectionCurrent)!=0)
			SendDlgItemMessage(hDlg,IDC_COMBOLANGUAGE,CB_ADDSTRING,(WPARAM)0,(LPARAM)lp);
		}
		GetPrivateProfileString(str_SectionCurrent,str_SectionCurrent,
							str_DefaultLanguage,lpCurrentLanguage,16,lpIni);

		nReturn=SendDlgItemMessage(hDlg,IDC_COMBOLANGUAGE,CB_FINDSTRINGEXACT,(WPARAM)0,(LPARAM)lpCurrentLanguage);
		if (nReturn!=CB_ERR)
		{
			bRet=TRUE;
			SendDlgItemMessage(hDlg,IDC_COMBOLANGUAGE,CB_SETCURSEL,(WPARAM)nReturn,(LPARAM)0);
		}
		else
			bRet=FALSE;

	}
	else
		bRet=FALSE;


	MYFREE(lpSectionNames);
	//MYFREE(lpCurrentLanguage);
	return bRet;
	
}
//--------------------------------------------------
// Routines that show multi language
//--------------------------------------------------

VOID	GetDefaultStrings(VOID)
{
	//_asm int 3
	lan_key				=lan_default[0];
	lan_value			=lan_default[1];
	lan_dir				=lan_default[2];
	lan_file			=lan_default[3];
	lan_time			=lan_default[4];
	lan_keyadd			=lan_default[5];
	lan_keydel			=lan_default[6];
	lan_valadd			=lan_default[7];
	lan_valdel			=lan_default[8];
	lan_valmodi			=lan_default[9];
	lan_fileadd			=lan_default[10];
	lan_filedel			=lan_default[11];
	lan_filemodi		=lan_default[12];
	lan_diradd			=lan_default[13];
	lan_dirdel			=lan_default[14];
	lan_dirmodi			=lan_default[15];
	lan_total			=lan_default[16];
	lan_comments		=lan_default[17];
	lan_datetime		=lan_default[18];
	lan_computer		=lan_default[19];
	lan_username		=lan_default[20];
	lan_about			=lan_default[21];
	lan_error			=lan_default[22];
	lan_errorexecviewer	=lan_default[23];
	lan_errorcreatefile	=lan_default[24];
	lan_erroropenfile	=lan_default[25];
	lan_errormovefp		=lan_default[26];
	lan_menushot		=lan_default[40];
	lan_menushotsave	=lan_default[41];
	lan_menuload		=lan_default[42];
	lan_menuclearallshots=lan_default[43];
	lan_menuclearshot1	=lan_default[44];
	lan_menuclearshot2	=lan_default[45];


}
//--------------------------------------------------
// Routines that show multi language
//--------------------------------------------------

VOID	PointToNewStrings(VOID)
{
	LPDWORD	lp=ldwTempStrings;
	lan_key				=(u_char *)(*lp);lp++;
	lan_value			=(u_char *)(*lp);lp++;
	lan_dir				=(u_char *)(*lp);lp++;
	lan_file			=(u_char *)(*lp);lp++;
	lan_time			=(u_char *)(*lp);lp++;
	lan_keyadd			=(u_char *)(*lp);lp++;
	lan_keydel			=(u_char *)(*lp);lp++;
	lan_valadd			=(u_char *)(*lp);lp++;
	lan_valdel			=(u_char *)(*lp);lp++;
	lan_valmodi			=(u_char *)(*lp);lp++;
	lan_fileadd			=(u_char *)(*lp);lp++;
	lan_filedel			=(u_char *)(*lp);lp++;
	lan_filemodi		=(u_char *)(*lp);lp++;
	lan_diradd			=(u_char *)(*lp);lp++;
	lan_dirdel			=(u_char *)(*lp);lp++;
	lan_dirmodi			=(u_char *)(*lp);lp++;
	lan_total			=(u_char *)(*lp);lp++;
	lan_comments		=(u_char *)(*lp);lp++;
	lan_datetime		=(u_char *)(*lp);lp++;
	lan_computer		=(u_char *)(*lp);lp++;
	lan_username		=(u_char *)(*lp);lp++;
	lan_about			=(u_char *)(*lp);lp++;
	lan_error			=(u_char *)(*lp);lp++;
	lan_errorexecviewer	=(u_char *)(*lp);lp++;
	lan_errorcreatefile	=(u_char *)(*lp);lp++;
	lan_erroropenfile	=(u_char *)(*lp);lp++;
	lan_errormovefp		=(u_char *)(*lp);lp+=14;
	lan_menushot		=(u_char *)(*lp);lp++;
	lan_menushotsave	=(u_char *)(*lp);lp++;
	lan_menuload		=(u_char *)(*lp);lp++;
	lan_menuclearallshots=(u_char *)(*lp);lp++;
	lan_menuclearshot1	=(u_char *)(*lp);lp++;
	lan_menuclearshot2	=(u_char *)(*lp);

}
				
//--------------------------------------------------
// Routines that show multi language
//--------------------------------------------------
BOOL	GetLanguageStrings(HWND hDlg)
{
	DWORD	nIndex,i;
	BOOL	bRet;
	LPSTR	lpReturn;
	LPDWORD lp;
	char lpIniKey[8];	//1.8.2	LPSTR	lpIniKey=MYALLOC0(8);


	nIndex=SendDlgItemMessage(hDlg,IDC_COMBOLANGUAGE,CB_GETCURSEL,(WPARAM)0,(LPARAM)0);
	if (nIndex!=CB_ERR)
	{
		
		SendDlgItemMessage(hDlg,IDC_COMBOLANGUAGE,CB_GETLBTEXT,(WPARAM)nIndex,(LPARAM)lpCurrentLanguage);
		WritePrivateProfileString(str_SectionCurrent,str_SectionCurrent,lpCurrentLanguage,lpIni);
		ZeroMemory(lpFreeStrings,SIZEOF_FREESTRINGS);
		GetPrivateProfileSection(lpCurrentLanguage,lpFreeStrings,SIZEOF_FREESTRINGS,lpIni);
		for(i=1,lp=ldwTempStrings;i<47;i++)
		{
			
			sprintf(lpIniKey,"%d%s",i,"="); 
			//pointer returned was pointed to char just after "="
			if((lpReturn=AtPos(lpFreeStrings,lpIniKey,SIZEOF_FREESTRINGS))!=NULL)
			{
				//_asm int 3;
				*(lp+i-1)=(DWORD)lpReturn;
			}
			else
				*(lp+i-1)=(DWORD)lan_default[i-1];
			
			if(i>=28&&i<41&&i!=34)
			{
				SetDlgItemText(hDlg,ID_BASE+3+i-28,(LPSTR)(*(lp+i-1)));
			}


		}
		
		lpReturn=AtPos(lpFreeStrings,str_ItemTranslator,SIZEOF_FREESTRINGS);
		lpCurrentTranslator=(lpReturn!=NULL)?(lpReturn+1):str_Original;
		PointToNewStrings();
		bRet=TRUE;
	}
	else
		bRet=FALSE;
	//MYFREE(lpCurrentLanguage);
	//MYFREE(lpIniKey);
	return bRet;
}
