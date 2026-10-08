import os
d = r"D:\vbcoding\mir2\欧洲官方-14Server"
try:
    print(sorted(os.listdir(d))[:40])
except Exception as e:
    print(e)
print("== AGENTS server mentions ==")
t = open("AGENTS.md", encoding="utf-8", errors="ignore").read()
import re
for m in re.finditer(r".{0,80}([Rr]ust|bridge|M2Server|服务端).{0,80}", t):
    s = m.group(0).strip().replace("\n", " ")
    if len(s) < 220:
        print(" ", s)
