[Setup]
AppName=MagiWOL
AppVerName=MagiWOL 2.20
DefaultDirName={pf}\Josip Medved\MagiWOL
OutputBaseFilename=magiwol220
OutputDir=..\Releases
SourceDir=..\Binaries
AppId=JosipMedved_MagiWOL
AppMutex=Global\JosipMedved_MagiWOL
AppPublisher=Josip Medved
AppPublisherURL=http://www.jmedved.com/?page=magiwol
UninstallDisplayIcon={app}\MagiWOL.exe
AlwaysShowComponentsList=no
ArchitecturesInstallIn64BitMode=x64
DisableProgramGroupPage=yes
MergeDuplicateFiles=yes
MinVersion=0,5.01
PrivilegesRequired=admin
ShowLanguageDialog=no
SolidCompression=yes
ChangesAssociations=yes

[Files]
Source: "MagiWOL.exe";  DestDir: "{app}";
Source: "MagiWOLDocument.ico";  DestDir: "{app}";
Source: "wol.exe"; DestDir: "{app}";
Source: "ReadMe.txt";   DestDir: "{app}"; Attribs: readonly; Flags: overwritereadonly uninsremovereadonly;

[Icons]
Name: "{userstartmenu}\MagiWOL"; Filename: "{app}\MagiWOL.exe"

[Registry]
Root: HKCU; Subkey: "Software\Josip Medved\MagiWOL"; Flags: uninsdeletekey
Root: HKCU; Subkey: "Software\Josip Medved"; Flags: uninsdeletekeyifempty
Root: HKCR; Subkey: ".magiwol"; ValueType: string; ValueName: ""; ValueData: "MagiWolFile"; Flags: uninsdeletevalue
Root: HKCR; Subkey: "MagiWolFile"; ValueType: string; ValueName: ""; ValueData: "MagiWOL File"; Flags: uninsdeletekey
Root: HKCR; Subkey: "MagiWolFile\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\MagiWOLDocument.ico"
Root: HKCR; Subkey: "MagiWolFile\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\MagiWOL.exe"" ""%1"""

[Run]
Filename: "{app}\ReadMe.txt"; Description: "View ReadMe.txt"; Flags: postinstall runasoriginaluser shellexec nowait skipifsilent unchecked
Filename: "{app}\MagiWOL.exe"; Description: "Launch application now"; Flags: postinstall nowait skipifsilent runasoriginaluser unchecked


