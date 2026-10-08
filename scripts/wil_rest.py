import re, os
from PIL import Image
refs = {}
for f in ["src/UI/ShopPanelView.cs", "src/UI/StoragePanelView.cs", "src/UI/NpcDialogView.cs",
          "src/UI/ItemTooltipView.cs", "src/UI/RepairPanelView.cs"]:
    if os.path.exists(f):
        c = open(f, encoding="utf-8", errors="ignore").read()
        refs[f] = sorted(set(re.findall(r"Prguse_(\d{4})", c)))
for f, ns in refs.items():
    print(f, ns)
    for n in ns:
        p = os.path.join("resources", "wil", "png", "Prguse", f"Prguse_{n}.png")
        try:
            im = Image.open(p)
            print("   ", n, im.size)
        except Exception:
            print("   ", n, "MISS")
