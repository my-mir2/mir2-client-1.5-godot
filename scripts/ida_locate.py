import subprocess
r = subprocess.run(
    ["wmic", "process", "where", 'name="ida.exe"', "get", "ProcessId,ExecutablePath", "/format:list"],
    capture_output=True, text=True)
print(r.stdout[:3000])
print(r.stderr[:1000])
