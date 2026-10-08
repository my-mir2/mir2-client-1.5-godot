import re, os
subs = set()
for dp, dn, fns in os.walk("src"):
    for f in fns:
        if f.endswith(".cs") and ("Skill" in f or "Magic" in f or "Spell" in f):
            c = open(os.path.join(dp, f), encoding="utf-8", errors="ignore").read()
            subs |= set(re.findall(r"(?:sub_|0x)(4[0-9A-Fa-f]{5})", c))
print("SUBS:", sorted(subs)[:30])
c = open("src/UI/SkillPanelView.cs", encoding="utf-8", errors="ignore").read()
print("== SkillPanel refs ==")
for m in sorted(set(re.findall(r"(Prguse_\d+|Magic_\d+|sub_[0-9A-Fa-f]+|0x4[0-9A-Fa-f]{5})", c)))[:40]:
    print(" ", m)
t = open("src/Net/ProtocolConsts.cs", encoding="utf-8").read()
print("== magic CM/SM ==")
for m in re.finditer(r"public const short ((?:CM|SM)_(?:SPELL|MAGIC|SKILL|ADD\S*MAGIC|DEL\S*MAGIC|UPDATE\S*MAGIC|MAGIC_\w+|CM_SPELL|CM_MAGIC)\w*)\s*=\s*(\d+).*", t):
    print(" ", m.group(1), m.group(2))
