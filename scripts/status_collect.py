import re, os
subs = set()
for f in ["src/UI/StatusPanelView.cs", "src/UI/ItemTooltipView.cs", "src/UI/SlotButton.cs"]:
    p = f
    if os.path.exists(p):
        c = open(p, encoding="utf-8", errors="ignore").read()
        subs |= set(re.findall(r"(?:sub_|0x)(4[0-9A-Fa-f]{5})", c))
print("SUBS:", sorted(subs)[:30])
c = open("src/UI/StatusPanelView.cs", encoding="utf-8", errors="ignore").read()
print("== Status refs ==")
for m in sorted(set(re.findall(r"(StateItem_\d+|Prguse_\d+|sub_[0-9A-Fa-f]+|0x4[0-9A-Fa-f]{5})", c)))[:50]:
    print(" ", m)
