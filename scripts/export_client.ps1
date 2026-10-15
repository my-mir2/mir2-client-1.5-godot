param(
    [string]$GodotExe = "D:\sdk\Godot_v4.7-stable_mono_win64\Godot_v4.7-stable_mono_win64_console.exe",
    [string]$OutputDir = "bin\client_release"
)

if (!(Test-Path $GodotExe)) {
    Write-Host "错误: 未找到 Godot 引擎: $GodotExe" -ForegroundColor Red
    exit 1
}

if (!(Test-Path $OutputDir)) {
    New-Item -ItemType Directory -Path $OutputDir -Force | Out-Null
}

$targetExe = Join-Path $OutputDir "Mir2Client.exe"

Write-Host "=== 正在构建发布盛大热血传奇 1.5 独立桌面端 (Windows x64) ===" -ForegroundColor Cyan
& $GodotExe --headless --export-release "Windows Desktop" $targetExe

if (Test-Path $targetExe) {
    $size = (Get-Item $targetExe).Length
    Write-Host "客户端打包成功！产物: $targetExe ($([math]::Round($size/1MB, 2)) MB)" -ForegroundColor Green
} else {
    Write-Host "客户端构建完成（退出码: $LASTEXITCODE）。" -ForegroundColor Yellow
}
