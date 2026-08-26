param(
    [string]$Configuration = "Release"
)

$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
$project = Join-Path $root "src\BAIOS.App\BAIOS.App.csproj"
$dist = Join-Path $root "dist"
$homeDir = Join-Path $dist "BAIOS"
$tech = Join-Path $dist "BAIOS.Technician"

if (Test-Path $dist) {
    Remove-Item -Recurse -Force $dist
}

New-Item -ItemType Directory -Force -Path $homeDir | Out-Null

Write-Host "Publicando self-contained win-x64..."
dotnet publish $project -p:PublishProfile=win-x64 -c $Configuration -o $homeDir
if ($LASTEXITCODE -ne 0) {
    throw "dotnet publish falló."
}

Copy-Item -Recurse -Force $homeDir $tech
$techConfig = Join-Path $tech "config.json"
if (Test-Path $techConfig) {
    $json = Get-Content -Raw $techConfig
    $json = $json -replace '"modeDefault"\s*:\s*"Home"', '"modeDefault": "Technician"'
    Set-Content -Path $techConfig -Value $json -Encoding utf8
}

$homeZip = Join-Path $dist "BAIOS-win-x64.zip"
$techZip = Join-Path $dist "BAIOS.Technician-win-x64.zip"
if (Test-Path $homeZip) { Remove-Item $homeZip }
if (Test-Path $techZip) { Remove-Item $techZip }
Compress-Archive -Path (Join-Path $homeDir "*") -DestinationPath $homeZip
Compress-Archive -Path (Join-Path $tech "*") -DestinationPath $techZip

Write-Host "Listo:"
Write-Host "  $homeDir"
Write-Host "  $tech"
Write-Host "  $homeZip"
Write-Host "  $techZip"
Write-Host "Copia BAIOS.Technician a un USB para el perfil técnico."
