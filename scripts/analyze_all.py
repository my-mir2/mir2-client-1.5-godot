import json, re, os
from collections import Counter
d = json.load(open("scratch/ida_all_remaining.json", encoding="utf-8"))
cats = Counter()
for x in d:
    if x.get("error"):
        cats["badhex"] += 1
    elif not x["is_func"]:
        cats["nonfunc"] += 1
    elif x.get("is_start"):
        cats["true_start"] += 1
    else:
        cats["offset"] += 1
print(cats)
print("== NONFUNC ==")
for x in d:
    if not x.get("is_func") and not x.get("error"):
        print(" ", x["sub"])
print("== TRUE START w/ real names ==")
for x in d:
    if x.get("is_start") and x.get("name") and not x["name"].startswith("sub_"):
        print(" ", x["sub"], x["name"])
# update ledger: replace all remaining placeholders
p = "docs/ida_evidence/sub_ledger.md"
t = open(p, encoding="utf-8").read()
n = 0
for x in d:
    s = x["sub"].lower()
    key = f"- [ ] 0x{s} 未实证"
    if key not in t:
        continue
    if x.get("error"):
        rep = f"- [?] 0x{s} 地址非法"
    elif not x["is_func"]:
        rep = f"- [x] 0x{s} 非函数（is_func=False，声明待纠）"
    elif x.get("is_start"):
        nm = x.get("name", "")
        rep = f"- [x] 0x{s} 真函数起点 {nm}"
    else:
        r = x.get("range", ["?", "?"])
        rep = f"- [x] 0x{s} 非起点（归属 {x.get('name')} {r} 内偏移）"
    t = t.replace(key, rep)
    n += 1
open(p, "w", encoding="utf-8").write(t)
print(f"ledger replaced {n}")
