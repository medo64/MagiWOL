#define AppName        GetStringFileInfo('..\Binaries\MagiWol.exe', 'ProductName')
#define AppVersion     GetStringFileInfo('..\Binaries\MagiWol.exe', 'ProductVersion')
#define AppFileVersion GetStringFileInfo('..\Binaries\MagiWol.exe', 'FileVersion')
#define AppCompany     GetStringFileInfo('..\Binaries\MagiWol.exe', 'CompanyName')
#define AppCopyright   GetStringFileInfo('..\Binaries\MagiWol.exe', 'LegalCopyright')
#define AppBase        LowerCase(StringChange(AppName, ' ', ''))
#define AppSetupFile   AppBase + StringChange(AppVersion, '.', '')

[Setup]
AppName={#AppName}
AppVersion={#AppVersion}
AppVerName={#AppName} {#AppVersion}
AppPublisher={#AppCompany}
AppPublisherURL=http://jmedved.com/{#AppBase}/
AppCopyright={#AppCopyright}
VersionInfoProductVersion={#AppVersion}
VersionInfoProductTextVersion={#AppVersion}
VersionInfoVersion={#AppFileVersion}
DefaultDirName={pf}\{#AppCompany}\{#AppName}
OutputBaseFilename={#AppSetupFile}
OutputDir=..\Releases
SourceDir=..\Binaries
AppId=JosipMedved_MagiWOL
CloseApplications="yes"
RestartApplications="no"
UninstallDisplayIcon={app}\MagiWol.exe
AlwaysShowComponentsList=no
ArchitecturesInstallIn64BitMode=x64
DisableProgramGroupPage=yes
MergeDuplicateFiles=yes
MinVersion=0,5.1
PrivilegesRequired=admin
ShowLanguageDialog=no
SolidCompression=yes
ChangesAssociations=yes
DisableWelcomePage=yes

[Messages]
SetupAppTitle=Setup {#AppName} {#AppVersion}
SetupWindowTitle=Setup {#AppName} {#AppVersion}
BeveledLabel=jmedved.com

[Files]
Source: "MagiWol.exe";                    DestDir: "{app}";                      Flags: ignoreversion;
Source: "wol.exe";                        DestDir: "{app}";                      Flags: ignoreversion;
Source: "ReadMe.txt";                     DestDir: "{app}";  Attribs: readonly;  Flags: overwritereadonly uninsremovereadonly;
Source: "Resources/MagiWolDocument.ico";  DestDir: "{app}";  Attribs: readonly;  Flags: overwritereadonly uninsremovereadonly;

[Icons]
Name: "{userstartmenu}\MagiWOL"; Filename: "{app}\MagiWol.exe"

[Registry]
Root: HKLM;  Subkey: "Software\Josip Medved\MagiWol";   ValueType: none;    ValueName: "Installed";                                              Flags: deletevalue uninsdeletevalue
Root: HKCU;  Subkey: "Software\Josip Medved\MagiWol";   ValueType: dword;   ValueName: "Installed";  ValueData: "1";                             Flags: uninsdeletekey
Root: HKCU;  Subkey: "Software\Josip Medved";                                                                                                    Flags: uninsdeletekeyifempty
Root: HKCR;  Subkey: ".magiwol";                        ValueType: string;  ValueName: "";           ValueData: "MagiWolFile";                   Flags: uninsdeletevalue
Root: HKCR;  Subkey: "MagiWolFile";                     ValueType: string;  ValueName: "";           ValueData: "MagiWOL File";                  Flags: uninsdeletekey
Root: HKCR;  Subkey: "MagiWolFile\DefaultIcon";         ValueType: string;  ValueName: "";           ValueData: """{app}\MagiWolDocument.ico""";
Root: HKCR;  Subkey: "MagiWolFile\shell\open\command";  ValueType: string;  ValueName: "";           ValueData: """{app}\MagiWol.exe"" ""%1""";

[Run]
Filename: "{app}\MagiWol.exe";  Description: "Launch application now";  Flags: postinstall nowait skipifsilent runasoriginaluser unchecked
Filename: "{app}\ReadMe.txt";   Description: "View read me";            Flags: postinstall runasoriginaluser shellexec nowait skipifsilent unchecked
