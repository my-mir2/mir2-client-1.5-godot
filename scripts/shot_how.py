import os, glob
print("== godot exe ==")
for g in glob.glob(r"D:\*\godot*.exe", recursive=False)[:5]:
    print(g)
for p in [r"D:\soft", r"C:\Program Files"]:
    if os.path.exists(p):
        for x in os.listdir(p)[:30]:
            if "godot" in x.lower():
                print(p, x)
print("== how autotest launched (docs) ==")
import re
for f in ["AGENTS.md", "GEMINI.md"]:
    if os.path.exists(f):
        c = open(f, encoding="utf-8", errors="ignore").read()
        for m in re.finditer(r".{0,60}(autotest|AutoEndToEnd|headless|godot\.exe.{0,40}|--autotest).{0,60}", c):
            s = m.group(0).strip().replace("\n", " ")
            if len(s) < 260:
                print(f, "::", s)
            break
