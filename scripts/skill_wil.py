import re, os
from PIL import Image
c = open("src/UI/SkillPanelView.cs", encoding="utf-8", errors="ignore").read()
refs = sorted(set(re.findall(r"Prguse_(\d{4})", c)))
print("REFS:", refs)
for n in refs:
    p = os.path.join("resources", "wil", "png", "Prguse", f"Prguse_{n}.png")
    try:
        print(f"Prguse_{n}", Image.open(p).size)
    except Exception:
        print(f"Prguse_{n} MISS")
print("== consts/positions ==")
for m in re.finditer(r".{0,60}(PanelWidth|PanelHeight|Position\s*=\s*new Vector2\([^)]+\)|Size\s*=\s*new Vector2\([^)]+\)).{0,40}", c):
    s = m.group(0).strip().replace("\n", " ")
    if len(s) < 200:
        print(" ", s)
