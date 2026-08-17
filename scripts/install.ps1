param(
    [string]$Source = ""
)

$ErrorActionPreference = "Stop"
$root = Split-Path -Parent $PSScriptRoot
if ([string]::IsNullOrWhiteSpace($Source)) {
    $Source = Join-Path $root "dist\BAIOS"
}

if (-not (Test-Path (Join-Path $Source "BAIOS.exe"))) {
    throw "No hay BAIOS.exe en $Source. Ejecuta antes scripts\publish.ps1."
}

$dest = Join-Path $env:LOCALAPPDATA "BAIOS"
Write-Host "Instalando en $dest (sin administrador)..."
if (Test-Path $dest) {
    Remove-Item -Recurse -Force $dest
}

Copy-Item -Recurse -Force $Source $dest

$programs = Join-Path $env:APPDATA "Microsoft\Windows\Start Menu\Programs"
New-Item -ItemType Directory -Force -Path $programs | Out-Null
$shortcutPath = Join-Path $programs "BAIOS.lnk"
$shell = New-Object -ComObject WScript.Shell
$shortcut = $shell.CreateShortcut($shortcutPath)
$shortcut.TargetPath = Join-Path $dest "BAIOS.exe"
$shortcut.WorkingDirectory = $dest
$shortcut.Description = "BAIOS — Blinter All In One Security"
$icon = Join-Path $dest "BAIOS.exe"
$shortcut.IconLocation = "$icon,0"
$shortcut.Save()

Write-Host "Instalado. Acceso directo: $shortcutPath"
Write-Host "Arranque: $(Join-Path $dest 'BAIOS.exe')"
