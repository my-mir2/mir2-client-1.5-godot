import ctypes
from ctypes import wintypes
import os
# Use PowerShell CIM instead of wmic
import subprocess
r = subprocess.run(
    ["powershell", "-NoProfile", "-Command",
     "Get-CimInstance Win32_Process -Filter \"Name='ida.exe'\" | Select-Object ProcessId,ExecutablePath | Format-List"],
    capture_output=True, text=True, timeout=30)
print(r.stdout[:3000])
print(r.stderr[:1000])
