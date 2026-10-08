import subprocess, os
log = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\scratch\m2_idat2.log"
exe = r"D:\soft\soft-runner\Ida\IDA Professional 9.4\idat.exe"
scr = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\scripts\m2_verify25.py"
tgt = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\scratch\M2_copy.exe"
# -c also runs auto-analysis but allows us to wait; remove -A.
with open(log, "w") as lf:
    p = subprocess.Popen([exe, "-c", "-S" + scr, tgt], stdout=lf, stderr=subprocess.STDOUT,
                         creationflags=getattr(subprocess, "DETACHED_PROCESS", 0))
print(f"LAUNCHED pid={p.pid}, log={log}")