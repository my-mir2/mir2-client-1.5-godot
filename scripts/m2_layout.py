import os
root = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi"
print("== top ==")
print(sorted(os.listdir(root)))
for d in ["src", "server", "rust", "m2server", "bridge", "gateway"]:
    p = os.path.join(root, d)
    if os.path.exists(p):
        print(f"== {d} ==")
        print(sorted(os.listdir(p))[:40])
print("== find cargo/rs ==")
n = 0
for dp, dn, fns in os.walk(root):
    if ".git" in dp or "target" in dp or ".godot" in dp:
        continue
    for f in fns:
        if f == "Cargo.toml" or f.endswith(".rs"):
            print(os.path.join(dp, f).replace(root, ""))
            n += 1
            if n > 40:
                break
    if n > 40:
        break
print("== find M2Server exe ==")
import glob
for pat in [r"D:\vbcoding\*\*.exe", r"D:\vbcoding\讨论\**\*.exe"]:
    for g in glob.glob(pat, recursive=True)[:20]:
        if "M2" in g.upper() or "SERVER" in g.upper() or "MIR" in g.upper():
            print(g)
