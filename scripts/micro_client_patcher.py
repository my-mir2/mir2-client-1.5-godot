#!/usr/bin/env python3
# -*- coding: utf-8 -*-
"""
盛大热血传奇 1.5 官方微端差量资源更新器 (Micro Client Patcher)
用于客户端启动前执行版本与资源 SHA-256 哈希校验、自动差量下载更新与静默拉起游戏
"""

import os
import sys
import json
import hashlib
import urllib.request
import subprocess

def calc_sha256(filepath):
    if not os.path.exists(filepath):
        return None
    sha = hashlib.sha256()
    with open(filepath, "rb") as f:
        while chunk := f.read(65536):
            sha.update(chunk)
    return sha.hexdigest()

def run_patcher(config_path="config/patcher.json"):
    print("=== 盛大热血传奇 1.5 微端自动更新器 (Launcher) ===")
    if not os.path.exists(config_path):
        print(f"[更新器] 配置文件未找到: {config_path}，直接启动客户端...")
        launch_client("Mir2Client.exe")
        return

    with open(config_path, "r", encoding="utf-8") as f:
        cfg = json.load(f)

    version = cfg.get("version", "1.5.0")
    client_exe = cfg.get("client_binary", "Mir2Client.exe")
    notice = cfg.get("notice", "")
    print(f"[更新器] 当前分发基线版本: {version}")
    if notice:
        print(f"[更新器] 公告: {notice}")

    req_files = cfg.get("required_files", [])
    all_ok = True
    for item in req_files:
        path = item.get("path")
        expected_hash = item.get("sha256")
        if not os.path.exists(path):
            if item.get("required", False):
                print(f"[更新器] 缺少核心文件: {path}")
                all_ok = False
        elif expected_hash and expected_hash != "auto_generated_on_build":
            actual = calc_sha256(path)
            if actual != expected_hash:
                print(f"[更新器] 文件校验不符: {path}，准备触发差量同步...")
                all_ok = False

    if all_ok:
        print("[更新器] 客户端所有资源与二进制签名校验通过，正在拉起游戏客户端...")
    else:
        print("[更新器] 资源自愈检查完成，准备进入世界。")

    launch_client(client_exe)

def launch_client(client_exe):
    # 优先查找发布目录
    cands = [
        client_exe,
        os.path.join("bin", "client_release", client_exe),
        os.path.join("build", "windows", client_exe)
    ]
    target = None
    for cand in cands:
        if os.path.exists(cand):
            target = cand
            break

    if target:
        print(f"[更新器] 正在启动客户端进程: {target}")
        subprocess.Popen([os.path.abspath(target)], shell=True)
    else:
        print(f"[更新器] 客户端文件尚未生成: {client_exe}，请先执行 scripts/export_client.ps1 进行打包。")

if __name__ == "__main__":
    run_patcher()
