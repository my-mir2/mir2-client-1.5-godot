import ida_funcs, idautils, ida_hexrays, ida_lines, json, re
EA = 0x467920
ida_hexrays.init_hexrays_plugin()
cf = ida_hexrays.decompile(EA)
pseudo = str(cf) if cf else ""
open("D:/vbcoding/mir2/mir2-client1.5-delphi/mir2-client1.5-delphi/scratch/ida_processmsg.c",
     "w", encoding="utf-8").write(pseudo)
cases = sorted(set(int(x) for x in re.findall(r"\bcase\s+(\d+)", pseudo)))
cmps = []
fn = ida_funcs.get_func(EA)
cur = fn.start_ea
n = 0
while cur < fn.end_ea and n < 4000:
    try:
        l = ida_lines.generate_disasm_line(cur, 0)
    except Exception:
        break
    m = re.search(r"cmp\s+\w+,\s*([0-9A-Fa-f]+h?)", l)
    if m:
        cmps.append(l.strip() + " @ " + hex(cur))
    cur = idautils.idc.next_head(cur)
    n += 1
json.dump({"func": hex(EA), "range": [hex(fn.start_ea), hex(fn.end_ea)],
           "pseudo_len": len(pseudo), "case_count": len(cases), "cases": cases,
           "cmp_sample": cmps[:80]},
          open("D:/vbcoding/mir2/mir2-client1.5-delphi/mir2-client1.5-delphi/scratch/ida_switch.json",
               "w", encoding="utf-8"), indent=2)
print("SWITCH_DONE cases=" + str(len(cases)))
import ida_pro
ida_pro.qexit(0)
