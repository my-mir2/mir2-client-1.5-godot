import subprocess
r = subprocess.run(["powershell","-NoProfile","-Command","Get-ChildItem 'D:/soft/soft-runner/Ida/IDA Professional 9.4' | Select-Object Name | Format-Table -HideTableHeaders | Out-String"],
    capture_output=True, text=True, timeout=30)
print(r.stdout[:3000])
