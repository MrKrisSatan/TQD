$ErrorActionPreference = 'Stop'
$editorPath = 'C:\Program Files\Unity\Hub\Editor\6000.6.6f1\Editor\Unity.exe'
if (!(Test-Path -LiteralPath $editorPath)) { throw 'Install Unity 6000.6.6f1 with Windows Build Support, or edit $editorPath.' }
$projectPath = $PSScriptRoot
$logPath = Join-Path $projectPath 'unity-build.log'
$arguments = '-batchmode -nographics -quit -projectPath "' + $projectPath + '" -executeMethod BuildGame.Build -logFile "' + $logPath + '"'
$buildProcess = Start-Process -FilePath $editorPath -ArgumentList $arguments -WindowStyle Hidden -Wait -PassThru
if ($buildProcess.ExitCode -ne 0) { throw "Unity build failed. Read $logPath" }
Write-Output (Join-Path $projectPath 'Build\Windows\The Quiet Dread.exe')
