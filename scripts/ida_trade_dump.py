import ida_funcs, idautils, ida_hexrays, ida_lines, ida_xref, json
TARGETS = [0x4857C7, 0x485905, 0x4DDC8C, 0x4DD79C, 0x4915EC]
res = []
for ea in TARGETS:
    fn = ida_funcs.get_func(ea)
    e = {"ea": hex(ea), "is_func": fn is not None}
    if fn:
        e["name"] = ida_funcs.get_func_name(ea)
        e["range"] = [hex(fn.start_ea), hex(fn.end_ea)]
        try:
            ida_hexrays.init_hexrays_plugin()
            cf = ida_hexrays.decompile(ea)
            e["pseudo"] = str(cf)[:8000] if cf else None
        except Exception as ex:
            e["pseudo_error"] = str(ex)
        lines = []
        cur = fn.start_ea
        for _ in range(40):
            try:
                lines.append(hex(cur) + ": " + ida_lines.generate_disasm_line(cur, 0))
            except Exception:
                break
            cur = idautils.idc.next_head(cur)
            if cur == 0xFFFFFFFF or cur >= fn.end_ea:
                break
        e["disasm"] = lines
        try:
            e["xrefs_to"] = [hex(x.frm) for x in idautils.XrefsTo(ea)][:20]
        except Exception as ex:
            e["xrefs_error"] = str(ex)
    res.append(e)
with open("D:/vbcoding/mir2/mir2-client1.5-delphi/mir2-client1.5-delphi/scratch/ida_trade_dump.json", "w", encoding="utf-8") as f:
    json.dump(res, f, ensure_ascii=False, indent=2)
print("IDA_TRADE_DUMP_DONE " + str(len(res)))
import ida_pro
ida_pro.qexit(0)
