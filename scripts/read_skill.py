import json, re
d = json.load(open("scratch/ida_skill_dump.json", encoding="utf-8"))
for x in d:
    print(x["ea"], x["is_func"], x.get("name"), x.get("range"))
t = open("src/Net/ProtocolConsts.cs", encoding="utf-8").read()
print("== 3017/3015/3030 dups ==")
for m in re.finditer(r"public const short (\w+)\s*=\s*(3017|3015|3030)\b.*", t):
    print(" ", m.group(1), m.group(2))
