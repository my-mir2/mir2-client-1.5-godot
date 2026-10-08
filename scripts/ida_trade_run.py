import shutil, subprocess, os, sys
src = r"D:\vbcoding\AI\x8育儿\unpack_mir_dat\out\MIR_final_v3_p - Copy.exe.i64"
# fix path: actual uses AI育儿
src = r"D:\vbcoding\讨论\AI育儿\unpack_mir_dat\out\MIR_final_v3_p - Copy.exe.i64"
dst = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\scratch\trade_copy.i64"
print("SRC_EXISTS", os.path.exists(src))
if os.path.exists(src) and not os.path.exists(dst):
    shutil.copyfile(src, dst)
    print("COPIED")
else:
    print("DST_EXISTS", os.path.exists(dst))
idat = r"D:\soft\soft-runner\Ida\IDA Professional 9.4\idat.exe"
script = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\scripts\ida_trade_dump.py"
cmd = [idat, "-A", "-S" + script, dst]
print("LAUNCH", cmd)
r = subprocess.run(cmd, capture_output=True, text=True, timeout=280)
print("RC", r.returncode)
print(r.stdout[-3000:])
print(r.stderr[-2000:])
