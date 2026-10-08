import subprocess, os, sys, time, json, glob

def run_sync():
    log = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\scratch\m2_idat3.log"
    exe = r"D:\soft\soft-runner\Ida\IDA Professional 9.4\idat.exe"
    scr = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\scripts\m2_verify25.py"
    tgt = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\scratch\M2_copy.exe"
    # -A batch; idat writes M2_copy.exe.i64 next to input
    cmd = [exe, "-A", "-S" + scr, tgt]
    print("RUN", " ".join(cmd), flush=True)
    try:
        r = subprocess.run(cmd, capture_output=True, text=True, timeout=3000)
        print("RC", r.returncode, flush=True)
        print("OUT", r.stdout[-1500:], flush=True)
        print("ERR", r.stderr[-800:], flush=True)
    except subprocess.TimeoutExpired as e:
        print("TIMEOUT", str(e)[:300], flush=True)
    print("RESULT_EXISTS", os.path.exists(r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\scratch\ida_m2_25.json"), flush=True)

run_sync()