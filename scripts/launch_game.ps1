# ============================================================================
# 盛大热血传奇 1.50 (虎卫传说) - 一键本地全栈环境拉起与联调启动脚本
# 自动检测并拉起：PostgreSQL 数据库 -> M2 高并发服务端 -> Godot 4 客户端图形窗口
# ============================================================================

$ErrorActionPreference = "Stop"

$repoRoot = Split-Path -Parent $PSScriptRoot
Set-Location $repoRoot

Write-Host "====================================================================" -ForegroundColor Cyan
Write-Host "  盛大热血传奇 1.50 官方原版 C# 单轨工程 - 一键全栈手动联调环境启动" -ForegroundColor Cyan
Write-Host "====================================================================" -ForegroundColor Cyan

# 1. 检查并拉起 PostgreSQL 数据库
$pgPort = 5432
$pgRunning = Get-NetTCPConnection -LocalPort $pgPort -State Listen -ErrorAction SilentlyContinue
if (!$pgRunning) {
    Write-Host "[1/3] 正在启动本地 PostgreSQL 数据库 (端口: 5432)..." -ForegroundColor Yellow
    $pgExe = "d:\tools\pgsql_bin\pgsql\bin\postgres.exe"
    $pgData = "d:\tools\pgsql_data"
    if (Test-Path $pgExe) {
        Start-Process -FilePath $pgExe -ArgumentList "-D `"$pgData`"" -WindowStyle Hidden
        Start-Sleep -Seconds 2
    } else {
        Write-Host "警告: 未检测到 $pgExe，请确认 PostgreSQL 是否已单独运行！" -ForegroundColor DarkYellow
    }
} else {
    Write-Host "[1/3] 本地 PostgreSQL 数据库已在运行 (端口: 5432)" -ForegroundColor Green
}

# 2. 检查并拉起 Mir2.Server 服务端
$m2Port = 7000
$m2Running = Get-NetTCPConnection -LocalPort $m2Port -State Listen -ErrorAction SilentlyContinue
if (!$m2Running) {
    Write-Host "[2/3] 正在启动 Mir2.Server 现代 M2 微内核服务端 (Native AOT 单文件)..." -ForegroundColor Yellow
    $serverExe = Join-Path $repoRoot "bin\server_release\Mir2.Server.exe"
    if (!(Test-Path $serverExe)) {
        Write-Host "未找到 AOT 编译产物，正在即时编译生成..." -ForegroundColor Cyan
        dotnet publish src/Mir2.Server/Mir2.Server.csproj -c Release -r win-x64 --self-contained -o bin/server_release
    }
    Start-Process -FilePath $serverExe -WorkingDirectory $repoRoot
    Start-Sleep -Seconds 2
} else {
    Write-Host "[2/3] Mir2.Server 服务端已在运行 (端口: 7000 / Web: 8080)" -ForegroundColor Green
}

# 3. 编译并启动 Godot 4 客户端图形窗口
Write-Host "[3/3] 正在同步编译最新客户端 C# 程序集 (Debug)..." -ForegroundColor Cyan
dotnet build Mir2Client.csproj -c Debug --nologo -v q

$godotExe = "D:\sdk\Godot_v4.7-stable_mono_win64\Godot_v4.7-stable_mono_win64.exe"
if (!(Test-Path $godotExe)) {
    Write-Host "错误: 未找到 Godot 引擎: $godotExe" -ForegroundColor Red
    exit 1
}

Write-Host "[3/3] 正在拉起盛大热血传奇 1.5 客户端窗口 (800x600)..." -ForegroundColor Green
Write-Host "提示: 客户端将自动连接本地服务端 127.0.0.1:7000" -ForegroundColor Cyan
Write-Host "Web 监控大盘: http://127.0.0.1:8080/api/stats" -ForegroundColor Cyan

Start-Process -FilePath $godotExe -ArgumentList "--path `"$repoRoot`"" -WorkingDirectory $repoRoot
