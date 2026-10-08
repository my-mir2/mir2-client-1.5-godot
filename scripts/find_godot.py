import os
cands = []
for base in [r"D:\soft", r"D:\soft\soft-runner", r"C:\Godot"]:
    if os.path.exists(base):
        for dp, dn, fns in os.walk(base):
            for f in fns:
                if f.lower().startswith("godot") and f.lower().endswith(".exe"):
                    cands.append(os.path.join(dp, f))
            if len(cands) > 5:
                break
    if cands:
        break
print(cands)
# PATH lookup
import shutil
print(shutil.which("godot"))
