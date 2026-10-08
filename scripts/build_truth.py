import json, os, re
root = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi"
os.makedirs(os.path.join(root, "docs", "ida_evidence"), exist_ok=True)
sw = json.load(open(os.path.join(root, "scratch", "ida_switch.json"), encoding="utf-8"))
dlgs = json.load(open(os.path.join(root, "scratch", "ida_dialogs.json"), encoding="utf-8"))
cov = json.load(open(os.path.join(root, "scratch", "coverage_gap.json"), encoding="utf-8"))
t = open(os.path.join(root, "src", "Net", "ProtocolConsts.cs"), encoding="utf-8").read()
sm = re.findall(r"public const short (SM_\w+)\s*=\s*(\d+)", t)
cm = re.findall(r"public const short (CM_\w+)\s*=\s*(\d+)", t)
smmap = {int(n): [] for _, n in sm}
for n, v in sm:
    smmap[int(v)].append(n)
cases = set(sw["cases"])
lines = ["# SM 真值表（IDA ProcessServerMessage 0x467920~0x46AF50，反编译 case 枚举 140 个）",
         f"伪代码长度 {sw['pseudo_len']}，证据 scratch/ida_processmsg.c",
         "",
         "| SM编号 | 真值表 | 代码定义 | 结论 |",
         "|---|---|---|---|"]
for name, num in sorted(sm, key=lambda x: int(x[1])):
    v = int(num)
    mark = "在真值表" if v in cases else "不在真值表（存疑/变体/错号）"
    lines.append(f"| {v} {name} | {'YES' if v in cases else 'NO'} | 定义 | {mark} |")
open(os.path.join(root, "docs", "ida_evidence", "SM_truth_table.md"), "w", encoding="utf-8").write("\n".join(lines))
dlines = ["# 对话框真名表（IDA 函数名枚举 165 个，证据 scratch/ida_dialogs.json）", "",
          "| IDA地址 | 官方函数名 | 我方View | 对齐 |",
          "|---|---|---|---|"]
views = "\n".join(cov["views"])
for d in dlgs:
    dlines.append(f"| {d['ea']} | {d['name']} | ? | 待建档 |")
open(os.path.join(root, "docs", "ida_evidence", "dialog_truth_table.md"), "w", encoding="utf-8").write("\n".join(dlines))
# sub ledger: verify each claimed sub is func / offset / nonfunc via quick map from dumps + known
known = {}
for f in ["ida_trade_dump.json", "ida_guild_dump.json"]:
    p = os.path.join(root, "scratch", f)
    if os.path.exists(p):
        for x in json.load(open(p, encoding="utf-8")):
            known[x["ea"].lower()] = x
led = ["# sub 点名册（118 个声明 vs 已实证）", ""]
for s in cov["subs"]:
    ea = "0x" + s.lower()
    if ea in known:
        k = known[ea]
        if k.get("is_func"):
            led.append(f"- [x] {ea} 真函数 {k.get('name')} {k.get('range')}")
        else:
            led.append(f"- [x] {ea} 非函数（字段/偏移），禁用函数口径")
    else:
        led.append(f"- [ ] {ea} 未实证（待新开idat点名）")
open(os.path.join(root, "docs", "ida_evidence", "sub_ledger.md"), "w", encoding="utf-8").write("\n".join(led))
print("WROTE 3 docs")
# print dup groups
from collections import Counter
print("CM_DUP", [k for k, v in Counter([n for _, n in cm]).items() if v > 1])
print("SM_DUP", [k for k, v in Counter([n for _, n in sm]).items() if v > 1])
