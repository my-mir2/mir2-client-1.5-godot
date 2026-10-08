import re
c = open("src/UI/StatusPanelView.cs", encoding="utf-8", errors="ignore").read()
for m in re.finditer(r".{0,70}(483[Ee]8[Cc]|484391|4892[Cc]2|48C3C8|48E458).{0,70}", c):
    print(m.group(0).strip().replace("\n", " "))
