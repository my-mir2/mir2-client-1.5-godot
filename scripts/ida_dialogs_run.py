import subprocess
idat = r"D:\soft\soft-runner\Ida\IDA Professional 9.4\idat.exe"
dst = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\scratch\switch_copy.i64"
script = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\scripts\ida_dialogs_dump.py"
r = subprocess.run([idat, "-A", "-S" + script, dst], capture_output=True, text=True, timeout=280)
print("RC", r.returncode)
print(r.stdout[-2000:])
print(r.stderr[-1000:])
