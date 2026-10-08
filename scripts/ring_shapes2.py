import re
s = open("src/Game/SpecialRings.cs", encoding="utf-8", errors="ignore").read()
print("== Shape == checks ==")
seen = set()
for m in re.finditer(r"Shape == (\d+)", s):
    seen.add(m.group(1))
print(sorted(seen, key=int))
print("== overload/teleport/invisible fns ==")
for m in re.finditer(r"public static \w+ (\w+)\(", s):
    print(" ", m.group(1))