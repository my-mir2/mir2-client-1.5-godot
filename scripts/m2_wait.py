import subprocess, os, sys, time, json

def wait_result(path, log, timeout_sec, poll=3):
    start = time.time()
    while time.time() - start < timeout_sec:
        if os.path.exists(path):
            try:
                d = json.load(open(path, encoding="utf-8"))
                return d
            except Exception:
                time.sleep(poll)
                continue
        time.sleep(poll)
    # tail log
    if os.path.exists(log):
        lines = open(log, encoding="utf-8", errors="ignore").read().splitlines()
        print("LOG_TAIL:\n" + "\n".join(lines[-20:]))
    return None

d = wait_result("scratch/ida_m2_25.json", "scratch/m2_idat2.log", 1800)
if d is None:
    print("NO_RESULT_YET")
    sys.exit(1)
nf = sum(1 for x in d if x["is_func"])
print(f"funcs={nf}/{len(d)}")
names = [(x["ea"], x.get("name")) for x in d if x["is_func"]]
for a, n in names[:30]:
    print(" ", a, n)