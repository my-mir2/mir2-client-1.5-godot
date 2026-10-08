import ida_funcs, idaapi, ida_hexrays, ida_bytes, json
idaapi.auto_wait()
ida_hexrays.init_hexrays_plugin()
out = []
for ea in [0x4BD48C, 0x4BDCFC, 0x4CBCD0]:
    try:
        cf = ida_hexrays.decompile(ea)
        out.append({"ea": hex(ea), "pseudo": str(cf)[:4000] if cf else None})
    except Exception as ex:
        out.append({"ea": hex(ea), "error": str(ex)})
# raw bytes at ring addresses
for ea in [0x4C1EF4, 0x4C7720, 0x4C03B0, 0x4BE3FC]:
    try:
        b = ida_bytes.get_bytes(ea, 16)
        out.append({"ea": hex(ea), "bytes": b.hex() if b else None})
    except Exception as ex:
        out.append({"ea": hex(ea), "error": str(ex)})
with open("D:/vbcoding/mir2/mir2-client1.5-delphi/mir2-client1.5-delphi/scratch/ida_m2_deep.json", "w", encoding="utf-8") as f:
    json.dump(out, f, ensure_ascii=False, indent=2)
print("DEEP_DONE")
import ida_pro
ida_pro.qexit(0)
