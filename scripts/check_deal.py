import re, os
from PIL import Image
root = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi"
print("== PNG sizes ==")
for n in ["Prguse_0389.png","Prguse_0390.png","Prguse_0391.png","Prguse_0064.png","Prguse_0028.png","Prguse_0180.png","Prguse_0204.png","Prguse_0181.png"]:
    p = os.path.join(root, "resources","wil","png","Prguse", n)
    try:
        im = Image.open(p)
        print(n, im.size)
    except Exception as e:
        print(n, "MISS", e)
print("== tests expect DEAL numbers ==")
for fn in os.listdir(os.path.join(root,"tests","CSharp")):
    if not fn.endswith(".cs"): continue
    t = open(os.path.join(root,"tests","CSharp",fn), encoding="utf-8", errors="ignore").read()
    for m in re.finditer(r"(SM_DEAL\w*|CM_DEAL\w*|CM_TRADE\w*)\D{0,12}(\d{2,4})", t):
        print(fn, m.group(1), m.group(2))
print("== ProtocolConsts DEAL block ==")
t = open(os.path.join(root,"src","Net","ProtocolConsts.cs"), encoding="utf-8").read().splitlines()
for i,l in enumerate(t):
    if re.search(r"DEAL", l):
        print(f"{i+1}: {l}")
