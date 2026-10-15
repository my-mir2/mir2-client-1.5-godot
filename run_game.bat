@echo off
chcp 65001 >nul
title 盛大热血传奇 1.50 (虎卫传说) - 一键联调启动器
echo 正在以管理员/用户权限唤起 PowerShell 启动脚本...
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0scripts\launch_game.ps1"
pause
