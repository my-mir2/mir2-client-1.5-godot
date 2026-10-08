import ida_funcs, json
TARGETS = [0x484468, 0x48419C, 0x48999D, 0x488BB7, 0x491EA0, 0x4BDD20, 0x4C03B0]
res = []
for ea in TARGETS:
    fn = ida_funcs.get_func(ea)
    e = {"ea": hex(ea), "is_func": fn is not None}
    if fn:
        e["name"] = ida_funcs.get_func_name(ea)
        e["range"] = [hex(fn.start_ea), hex(fn.end_ea)]
    res.append(e)
with open("D:/vbcoding/mir2/mir2-client1.5-delphi/mir2-client1.5-delphi/scratch/ida_skill_dump.json", "w", encoding="utf-8") as f:
    json.dump(res, f, ensure_ascii=False, indent=2)
print("IDA_SKILL_DUMP_DONE")
import ida_pro
ida_pro.qexit(0)
