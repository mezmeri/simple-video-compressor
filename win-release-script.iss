; --- INNO SETUP SCRIPT FOR SIMPLE VIDEO COMPRESSOR ---

#define MyAppName "Simple Video Compressor"
#define MyAppVersion "1.1"
#define MyAppPublisher "mezmeri"
#define MyAppExeName "SimpleVideoCompressor.exe"
#define MyAppAssocName MyAppName + " File"
#define MyAppAssocExt ".svc"
#define MyAppAssocKey StringChange(MyAppAssocName, " ", "") + MyAppAssocExt

; Definerer den primære publish-mappe som en variabel for renere kode
#define MyPublishDir "C:\Users\Mads\Documents\Programmering\skole\SimpleEncoder\bin\Release\net8.0-windows\win-x64\publish\win-x64"

[Setup]
AppId={{A6D6B3A5-E744-4B18-8041-6C50B721F6E8}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
UninstallDisplayIcon={app}\{#MyAppExeName}

; Sikrer, at der kun installeres på 64-bit systemer
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible

ChangesAssociations=yes
DisableProgramGroupPage=yes

; Rettelse: Kræver administratorrettigheder for korrekt installation i C:\Program Files
PrivilegesRequired=admin

OutputDir=C:\Users\Mads\Desktop
OutputBaseFilename=svc-installer
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; Kopierer hovedfilen (.exe)
Source: "{#MyPublishDir}\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion

; Kopierer alle tilhørende filer og undermapper (herunder .dll-filer, konfigurationsfiler og ffmpeg.exe hvis placeret i publish)
Source: "{#MyPublishDir}\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs; Excludes: "{#MyAppExeName}"

[Registry]
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocExt}\OpenWithProgids"; ValueType: string; ValueName: "{#MyAppAssocKey}"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}"; ValueType: string; ValueName: ""; ValueData: "{#MyAppAssocName}"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\{#MyAppExeName},0"
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent