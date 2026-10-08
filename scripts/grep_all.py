import os, re
root = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi"
def walk(exts):
    for dp, dn, fns in os.walk(root):
        if ".git" in dp or ".godot" in dp:
            continue
        for f in fns:
            if any(f.endswith(e) for e in exts):
                yield os.path.join(dp, f)
print("== CM/SM opcodes ==")
for p in walk([".cs"]):
    try:
        t = open(p, encoding="utf-8", errors="ignore").read()
    except Exception:
        continue
    for m in re.finditer(r"(CM_\w+|SM_\w+)\s*=\s*(\d+)", t):
        print(m.group(1), m.group(2), os.path.basename(p))
        break
print("== Deal mentions ==")
n = 0
for p in walk([".cs"]):
    try:
        t = open(p, encoding="utf-8", errors="ignore").read()
    except Exception:
        continue
    if re.search(r"Deal|Trade", t):
        print(os.path.relpath(p, root))
        n += 1
        if n > 30:
            break
print("== Prguse files ==")
for p in walk([".png"]):
    b = os.path.basename(p)
    if "Prguse_039" in b or "Prguse_038" in b or "Prguse_0064" in b or "Prguse_0028" in b:
        print(os.path.relpath(p, root))
print("== Guild mentions ==")
for p in walk([".cs"]):
    try:
        t = open(p, encoding="utf-8", errors="ignore").read()
    except Exception:
        continue
    if "Guild" in os.path.basename(p):
        print(os.path.relpath(p, root))
