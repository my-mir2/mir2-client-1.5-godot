import shutil, subprocess, os
src = r"D:\vbcoding\讨论\AI育儿\unpack_mir_dat\out\MIR_final_v3_p - Copy.exe.i64"
dst = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\scratch\all_copy.i64"
if not os.path.exists(dst):
    shutil.copyfile(src, dst)
    print("COPIED")
idat = r"D:\soft\soft-runner\Ida\IDA Professional 9.4\idat.exe"
script = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\scripts\ida_all_dump.py"
r = subprocess.run([idat, "-A", "-S" + script, dst], capture_output=True, text=True, timeout=280)
print("RC", r.returncode)
print(r.stdout[-1200:])
print(r.stderr[-600:])
