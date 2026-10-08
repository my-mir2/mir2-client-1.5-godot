import os, re
print("== autotest infra ==")
for dp, dn, fns in os.walk("src"):
    for f in fns:
        if "autotest" in f.lower() or "autodriver" in f.lower() or "endtoend" in f.lower():
            print(" ", os.path.join(dp, f).replace("\\", "/"))
print("== AutoEndToEndDriver trade/guild/skill ==")
p = "src/AutoEndToEndDriver.cs"
if os.path.exists(p):
    c = open(p, encoding="utf-8", errors="ignore").read().splitlines()
    print(f"  lines={len(c)}")
    for i, l in enumerate(c):
        if re.search(r"Trade|Guild|Skill|Screenshot|SavePng|Capture", l):
            print(f"  {i+1}: {l.strip()}"[:140])
print("== screenshots recent ==")
ss = sorted(os.listdir("screenshots"))[-10:]
for s in ss:
    print(" ", s)