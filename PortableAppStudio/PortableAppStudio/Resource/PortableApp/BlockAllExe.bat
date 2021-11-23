@ setlocal enableextensions 
@ cd /d "%~dp0"

for /R .\App\ %%a in (*.exe) do (

netsh advfirewall firewall add rule name="Blocked with Batchfile %%a" dir=out program="%%a" action=block

)

for /R .\Data\ %%a in (*.exe) do (

netsh advfirewall firewall add rule name="Blocked with Batchfile %%a" dir=out program="%%a" action=block

)

for /R .\Other\ %%a in (*.exe) do (

netsh advfirewall firewall add rule name="Blocked with Batchfile %%a" dir=out program="%%a" action=block

)

REM netsh advfirewall firewall add rule name="Blocked with Batchfile %%a" dir=out program="%%a" action=block