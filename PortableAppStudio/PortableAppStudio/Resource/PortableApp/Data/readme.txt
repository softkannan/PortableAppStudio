Data: contains all the user data for the application including settings, configuration and other data that would usually be stored within APPDATA
for a locally installed application. The applications released by PortableApps.com typically contain the settings in a settings subdirectory, 
profiles for Mozilla apps in a profiles subdirectory. No application components (binary files, etc) should be contained within the Data directory. 
The launcher or application must be able to recreate the Data directory and all required files within it if it is missing.