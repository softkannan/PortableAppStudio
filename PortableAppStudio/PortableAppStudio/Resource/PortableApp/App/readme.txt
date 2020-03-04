This directory contains files used by the portable app.
They should generally not be accessed directly by users.

User data or settings should not be stored within the App
directory or its sub-directories.  Any data stored here
will likely be deleted on upgrades.

App: contains all the binary and other files that make up the application itself, 
usually within a directory called AppName. The other directory called AppInfo (discussed in section 2) 
contains the configuration details for the PortableApps.com Platform as well as the icons used within the menu. 
It may also contain the launcher.ini configuration file used for the PortableApps.com Launche