import ida_funcs, json
TARGETS = [0x483E8C, 0x484391, 0x4892C2, 0x48A000, 0x48C3C8, 0x48E458]
res = []
for ea in TARGETS:
    fn = ida_funcs.get_func(ea)
    e = {"ea": hex(ea), "is_func": fn is not None}
    if fn:
        e["name"] = ida_funcs.get_func_name(ea)
        e["range"] = [hex(fn.start_ea), hex(fn.end_ea)]
    res.append(e)
with open("D:/vbcoding/mir2/mir2-client1.5-delphi/mir2-client1.5-delphi/scratch/ida_status_dump.json", "w", encoding="utf-8") as f:
    json.dump(res, f, ensure_ascii=False, indent=2)
print("IDA_STATUS_DUMP_DONE")
import ida_pro
ida_pro.qexit(0)
