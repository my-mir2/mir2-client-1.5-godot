import subprocess
exe = r"D:\soft\soft-runner\Ida\IDA Professional 9.4\idat.exe"
scr = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\scripts\m2_deep.py"
tgt = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\scratch\M2_copy.exe.i64"
r = subprocess.run([exe, "-A", "-S" + scr, tgt], capture_output=True, text=True, timeout=1200)
print("RC", r.returncode, flush=True)
print("OUT", r.stdout[-800:], flush=True)
