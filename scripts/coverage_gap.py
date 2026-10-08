import os, re, json
root = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi"
# 1. UI views
views = sorted([f for f in os.listdir(os.path.join(root, "src", "UI")) if f.endswith(".cs")])
print(f"VIEWS={len(views)}")
for v in views:
    print(" ", v)
# 2. CM/SM defined
t = open(os.path.join(root, "src", "Net", "ProtocolConsts.cs"), encoding="utf-8").read()
cms = re.findall(r"public const short (CM_\w+)\s*=\s*(\d+)", t)
sms = re.findall(r"public const short (SM_\w+)\s*=\s*(\d+)", t)
print(f"CM_DEFINED={len(cms)} SM_DEFINED={len(sms)}")
# dup numbers
from collections import Counter
c = Counter([n for _, n in cms]); print("CM_DUP_NUMS", [k for k, v in c.items() if v > 1])
c2 = Counter([n for _, n in sms]); print("SM_DUP_NUMS", [k for k, v in c2.items() if v > 1])
# 3. router handled
r = open(os.path.join(root, "src", "Net", "ServerMessageRouter.cs"), encoding="utf-8").read()
handled = set(re.findall(r"case (SM_\w+)", r))
defined = set(n for n, _ in sms)
print(f"HANDLED={len(handled)} UNHANDLED={len(defined - handled)}")
for s in sorted(defined - handled)[:60]:
    print("  UNHANDLED", s)
# 4. PacketEncoder coverage
p = open(os.path.join(root, "src", "Net", "PacketEncoder.cs"), encoding="utf-8").read()
sent = set(re.findall(r"(CM_\w+)", p))
cdef = set(n for n, _ in cms)
print(f"UNSENT={len(cdef - sent)}")
for s in sorted(cdef - sent)[:60]:
    print("  UNSENT", s)
# 5. Prguse referenced vs on disk
ref = set()
for dp, dn, fns in os.walk(os.path.join(root, "src")):
    for f in fns:
        if f.endswith(".cs"):
            ref |= set(re.findall(r"Prguse_(\d{4})", open(os.path.join(dp, f), encoding="utf-8", errors="ignore").read()))
disk = set()
d = os.path.join(root, "resources", "wil", "png", "Prguse")
for f in os.listdir(d):
    m = re.match(r"Prguse_(\d{4})", f)
    if m:
        disk.add(m.group(1))
print(f"PRGUSE_REF={len(ref)} DISK={len(disk)} REF_MISSING_ON_DISK={sorted(ref - disk)[:20]}")
# 6. IDA-claimed sub_ refs without evidence file
subs = set()
for dp, dn, fns in os.walk(os.path.join(root, "src")):
    for f in fns:
        if f.endswith(".cs"):
            subs |= set(re.findall(r"sub_([0-9A-Fa-f]{4,6})", open(os.path.join(dp, f), encoding="utf-8", errors="ignore").read()))
print(f"SUB_CLAIMED={len(subs)}")
json.dump({"views": views, "unhandled": sorted(defined - handled), "unsent": sorted(cdef - sent),
           "prguse_missing": sorted(ref - disk), "subs": sorted(subs)},
          open(os.path.join(root, "scratch", "coverage_gap.json"), "w", encoding="utf-8"), ensure_ascii=False, indent=2)
print("WROTE scratch/coverage_gap.json")
