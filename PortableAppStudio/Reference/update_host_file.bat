####################

@echo off

SET NEWLINE=^& echo.

FIND /C /I "servername" %WINDIR%\system32\drivers\etc\hosts

IF %ERRORLEVEL% NEQ 0 ECHO %NEWLINE%^ 0.0.0.0 servername>>%WINDIR%\System32\drivers\etc\hosts

####################

@echo off

SET NEWLINE=^& echo.

FIND /C /I "ns1.intranet.de" %WINDIR%\system32\drivers\etc\hosts
IF %ERRORLEVEL% NEQ 0 ECHO %NEWLINE%^62.116.159.4 ns1.intranet.de>>%WINDIR%\System32\drivers\etc\hosts

FIND /C /I "ns2.intranet.de" %WINDIR%\system32\drivers\etc\hosts
IF %ERRORLEVEL% NEQ 0 ECHO %NEWLINE%^217.160.113.37 ns2.intranet.de>>%WINDIR%\System32\drivers\etc\hosts

FIND /C /I "ns3.intranet.de" %WINDIR%\system32\drivers\etc\hosts
IF %ERRORLEVEL% NEQ 0 ECHO %NEWLINE%^89.146.248.4 ns3.intranet.de>>%WINDIR%\System32\drivers\etc\hosts

FIND /C /I "ns4.intranet.de" %WINDIR%\system32\drivers\etc\hosts
IF %ERRORLEVEL% NEQ 0 ECHO %NEWLINE%^74.208.254.4 ns4.intranet.de>>%WINDIR%\System32\drivers\etc\hosts