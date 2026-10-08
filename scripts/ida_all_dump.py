import ida_funcs, json
targets = json.load(open("D:/vbcoding/mir2/mir2-client1.5-delphi/mir2-client1.5-delphi/scratch/remaining_subs.json"))
res = []
for s in targets:
    try:
        ea = int(s, 16)
    except Exception:
        res.append({"sub": s, "error": "badhex"})
        continue
    fn = ida_funcs.get_func(ea)
    e = {"sub": s, "ea": hex(ea), "is_func": fn is not None}
    if fn:
        e["name"] = ida_funcs.get_func_name(ea)
        e["range"] = [hex(fn.start_ea), hex(fn.end_ea)]
        e["is_start"] = (fn.start_ea == ea)
    res.append(e)
with open("D:/vbcoding/mir2/mir2-client1.5-delphi/mir2-client1.5-delphi/scratch/ida_all_remaining.json", "w", encoding="utf-8") as f:
    json.dump(res, f, ensure_ascii=False, indent=2)
print("ALL_DONE " + str(len(res)))
import ida_pro
ida_pro.qexit(0)
