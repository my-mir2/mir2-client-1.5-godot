import os, glob
print("== Cargo anywhere in vbcoding ==")
for g in glob.glob(r"D:\vbcoding\*\Cargo.toml")[:10]:
    print(g)
for g in glob.glob(r"D:\vbcoding\*\*\Cargo.toml")[:10]:
    print(g)
print("== bridge.rs ==")
for dp, dn, fns in os.walk(r"D:\vbcoding\mir2"):
    for f in fns:
        if f == "bridge.rs" or (f.endswith(".rs") and "bridge" in f.lower()):
            print(os.path.join(dp, f))
print("== mir2 dirs ==")
print(sorted(os.listdir(r"D:\vbcoding\mir2"))[:20])
print("== M2Server sizes ==")
for p in [r"D:\vbcoding\讨论\AI育儿\GameOfMir\MirServer\M2Server.exe",
          r"D:\vbcoding\讨论\AI育儿\GameOfMir-DELPHI10\MirServer\M2Server.exe"]:
    try:
        print(p, os.path.getsize(p))
    except Exception as e:
        print(p, e)
