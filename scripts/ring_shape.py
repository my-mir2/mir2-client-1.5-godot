import re
s = open("src/Game/SpecialRings.cs", encoding="utf-8", errors="ignore").read()
print("== flame/magic_shield shape sites ==")
for m in re.finditer(r".{0,80}(flame_ring|magic_shield|FlameRing|MagicShield|HasFlameRing|HasMagicShield).{0,40}", s):
    ss = m.group(0).replace("\n", " ")
    if len(ss) < 220 and ("Shape" in ss or "==" in ss or "=>" in ss or "=" in ss):
        print(" ", ss)
print("== shape switch fn ==")
for m in re.finditer(r"(public static \w+ \w+\([^)]*\)\s*\{?)", s):
    pass
idx = [m.start() for m in re.finditer(r"Shape", s)][:0]
i = s.find("HasFlameRing")
print(s[max(0, i-800):i+800] if i > 0 else "NO HasFlameRing")