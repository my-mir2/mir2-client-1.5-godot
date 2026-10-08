import ida_funcs, idaapi, json
idaapi.auto_wait()
TARGETS = [0x4B2C38, 0x4BA43C, 0x4BB63C, 0x4BB6C8, 0x4BD4A0, 0x4BDD20, 0x4BDEB8,
           0x4BE3FC, 0x4BF0A0, 0x4BF180, 0x4BF7DC, 0x4C01B8, 0x4C03B0, 0x4C1EF4,
           0x4C2854, 0x4C3130, 0x4C50E9, 0x4C5114, 0x4C5238, 0x4C6238, 0x4C6E4C,
           0x4C7720, 0x4C93A0, 0x4C9B80, 0x4CBCEC]
res = []
for ea in TARGETS:
    fn = ida_funcs.get_func(ea)
    e = {"ea": hex(ea), "is_func": fn is not None}
    if fn:
        e["name"] = ida_funcs.get_func_name(ea)
        e["range"] = [hex(fn.start_ea), hex(fn.end_ea)]
        e["is_start"] = (fn.start_ea == ea)
    res.append(e)
with open("D:/vbcoding/mir2/mir2-client1.5-delphi/mir2-client1.5-delphi/scratch/ida_m2_25.json", "w", encoding="utf-8") as f:
    json.dump(res, f, ensure_ascii=False, indent=2)
print("M2_25_DONE")
import ida_pro
ida_pro.qexit(0)
