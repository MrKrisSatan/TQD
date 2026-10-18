param()
$ErrorActionPreference='Stop'
$root=Split-Path $PSScriptRoot -Parent
$parent=Split-Path $root -Parent
$runtime=Join-Path $root 'Build/Windows'
Copy-Item -LiteralPath (Join-Path $root 'Documentation') -Destination $runtime -Recurse -Force
Set-Content -LiteralPath (Join-Path $runtime 'README.txt') -Value 'The Quiet Dread 0.12.0. Run The Quiet Dread.exe from this folder. Keep the data folder and DLLs beside it. Settings: separate music/effects volume and music toggle. Five bundled tracks play across menus, campaigns and battles. Main menu: Miniature Catalogue and Field Manual. Settings > Local campaign narrator enables optional Ollama prose; campaign Chronicle shows the journal. Narration is off by default. See Documentation/LOCAL-NARRATOR.md and TODO.md. Battle: Rally removes suppression; Log shows dice results. Read Documentation/RELEASE-0.12.md for features and limitations.'
$win=Join-Path $parent 'The-Quiet-Dread-Windows-v0.12.0.zip'
$src=Join-Path $parent 'The-Quiet-Dread-Unity-Source-v0.12.0.zip'
Compress-Archive -Path (Join-Path $runtime '*') -DestinationPath $win -Force
$files=@('Assets','Packages','ProjectSettings','Tools','Documentation','QA','README.md','Build.ps1','Launch.cmd','.gitignore') | ForEach-Object {Join-Path $root $_}
Compress-Archive -LiteralPath $files -DestinationPath $src -Force
Add-Type -AssemblyName System.IO.Compression.FileSystem
foreach($file in @($win,$src)){
 $zip=[IO.Compression.ZipFile]::OpenRead($file)
 try {$entries=$zip.Entries.FullName -replace '\\','/'; if($file -eq $win){$required=@('The Quiet Dread.exe','UnityPlayer.dll','The Quiet Dread_Data/Managed/Assembly-CSharp.dll','Documentation/RELEASE-0.12.md')}else{$required=@('Assets/Scripts/Miniatures.cs','Assets/Scripts/ArmourMesh.cs','Assets/Scripts/TacticalAI.cs','Assets/Scripts/LocalNarrator.cs','Assets/Scripts/NarrativeController.cs','Assets/Scripts/MusicController.cs','Assets/Resources/Music/menu.mp3','Documentation/MUSIC.md','Assets/Resources/CommanderRewards.json','Assets/Scripts/CampaignStorage.cs','Documentation/COMMANDER-REWARDS.md','Documentation/COMMANDER-LEGACY-PROMPT.md','Documentation/TRAIL-PROGRESSION.md','Assets/Scripts/TrailProgression.cs','Documentation/TODO.md','Documentation/LOCAL-NARRATOR.md','Assets/Resources/FieldManual.txt','Assets/Editor/CoherenceChecks.cs','ProjectSettings/ProjectVersion.txt')};foreach($entry in $required){if($entry -notin $entries){throw "Missing archive entry: $entry"}}}finally{$zip.Dispose()}
}
Get-FileHash -Algorithm SHA256 -LiteralPath $win,$src | Format-List | Out-File -LiteralPath (Join-Path $parent 'The-Quiet-Dread-v0.12.0-SHA256.txt')
Get-Item -LiteralPath $win,$src | Select-Object FullName,Length



