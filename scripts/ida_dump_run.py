import ida_funcs, idautils, ida_hexrays, ida_lines, json, sys

def dump_one(ea):
    try:
        fn = ida_funcs.get_func(ea)
        if not fn:
            return {"ea": hex(ea), "error": "not-a-func"}
        name = ida_funcs.get_func_name(ea)
        out = {"ea": hex(ea), "name": name, "start": hex(fn.start_ea), "end": hex(fn.end_ea)}
        try:
            ida_hexrays.init_hexrays_plugin()
            cfunc = ida_hexrays.decompile(ea)
            out["pseudo"] = str(cfunc)[:12000] if cfunc else None
        except Exception as e:
            out["pseudo_error"] = str(e)
        # disasm first 60 insns
        lines = []
        cur = fn.start_ea
        for _ in range(60):
            lines.append(f"{cur:#x}: {ida_lines.generate_disasm_line(cur, 0)}")
            nxt = idautils.idc.next_head(cur)
            if nxt == idautils.BADADDR or nxt >= fn.end_ea:
                break
            cur = nxt
        out["disasm"] = lines
        return out
    except Exception as e:
        return {"ea": hex(ea), "error": str(e)}

addrs = []
for a in sys.argv[1:] if len(sys.argv) > 1 else []:
    try:
        addrs.append(int(a, 0))
    except Exception:
        pass
if not addrs:
    addrs = [0x4C7720, 0x4C1EF4, 0x4D1E54]
res = [dump_one(a) for a in addrs]
with open(r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\scratch\ida_dump.json", "w", encoding="utf-8") as f:
    json.dump(res, f, ensure_ascii=False, indent=2)
print("IDA_DUMP_DONE", len(res))
ida_pro.qexit(0)
