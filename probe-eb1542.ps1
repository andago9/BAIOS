$log = Join-Path $PSScriptRoot "debug-eb1542.log"
function W([string]$hyp, [string]$loc, [string]$msg, [hashtable]$data) {
  $payload = [pscustomobject]@{
    sessionId = "eb1542"
    timestamp = [DateTimeOffset]::UtcNow.ToUnixTimeMilliseconds()
    location = $loc
    message = $msg
    data = $data
    runId = "probe-pre"
    hypothesisId = $hyp
  }
  $json = $payload | ConvertTo-Json -Compress -Depth 6
  Add-Content -LiteralPath $log -Value $json -Encoding utf8
}

$tvPath = Join-Path $PSScriptRoot "src\BAIOS.App\ViewModels\ToolsViewModel.cs"
$dvPath = Join-Path $PSScriptRoot "src\BAIOS.App\ViewModels\DiagnosticsViewModel.cs"
$tmPath = Join-Path $PSScriptRoot "src\BAIOS.Tools\ToolsModule.cs"
$tv = Get-Content -LiteralPath $tvPath
$dv = Get-Content -LiteralPath $dvPath
$tm = Get-Content -LiteralPath $tmPath

W "A" "ToolsViewModel.cs:57" "Line 57 of ToolsViewModel" @{
  line57 = [string]$tv[56]
  containsCoreTools = [bool](($tv | Select-String -Pattern "CoreTools" -Quiet))
  lastWrite = (Get-Item -LiteralPath $tvPath).LastWriteTime.ToString("o")
}

W "A" "DiagnosticsViewModel.cs:80" "Line 80 of DiagnosticsViewModel" @{
  line80 = [string]$dv[79]
  containsAutorunsMember = [bool](($dv | Select-String -Pattern "ToolsModule\.Autoruns" -Quiet))
  lastWrite = (Get-Item -LiteralPath $dvPath).LastWriteTime.ToString("o")
}

W "A" "ToolsModule.cs" "ToolsModule public API in source" @{
  hasCoreTools = [bool](($tm | Select-String -Pattern "CoreTools" -Quiet))
  hasAutorunsField = [bool](($tm | Select-String -Pattern "readonly ToolCard Autoruns" -Quiet))
  hasLoadCatalog = [bool](($tm | Select-String -Pattern "LoadCatalog" -Quiet))
  hasRequire = [bool](($tm | Select-String -Pattern "static ToolCard Require" -Quiet))
  lastWrite = (Get-Item -LiteralPath $tmPath).LastWriteTime.ToString("o")
}

function Scan-Dll([string]$path, [string]$hyp) {
  $exists = Test-Path -LiteralPath $path
  $write = ""
  $hasCore = $false
  $hasAuto = $false
  $hasLoad = $false
  if ($exists) {
    $write = (Get-Item -LiteralPath $path).LastWriteTime.ToString("o")
    $text = [System.Text.Encoding]::ASCII.GetString([System.IO.File]::ReadAllBytes($path))
    $hasCore = $text.Contains("CoreTools")
    $hasAuto = $text.Contains("Autoruns")
    $hasLoad = $text.Contains("LoadCatalog")
  }
  W $hyp $path "Compiled ToolsModule string scan" @{
    exists = $exists
    lastWrite = $write
    hasCoreTools = $hasCore
    hasAutoruns = $hasAuto
    hasLoadCatalog = $hasLoad
  }
}

Scan-Dll (Join-Path $PSScriptRoot "src\BAIOS.Tools\bin\Debug\net8.0-windows\BAIOS.Tools.dll") "C"
Scan-Dll (Join-Path $PSScriptRoot "src\BAIOS.Tools\bin\Release\net8.0-windows\BAIOS.Tools.dll") "C"
Scan-Dll (Join-Path $PSScriptRoot "src\BAIOS.Tools\bin\Release\net8.0-windows\win-x64\BAIOS.Tools.dll") "C"

$obj = Join-Path $PSScriptRoot "src\BAIOS.App\obj"
$wpf = @(Get-ChildItem -LiteralPath $obj -Recurse -Filter "*k1b3ubty*" -ErrorAction SilentlyContinue)
W "B" "obj/wpftmp" "Publish wpftmp leftovers" @{ count = $wpf.Count }

Write-Output "ok"
