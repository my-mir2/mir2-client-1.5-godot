import ida_funcs, idautils, json
out = []
for ea in idautils.Functions():
    try:
        name = ida_funcs.get_func_name(ea)
    except Exception:
        continue
    if name and ("Dlg" in name or "Guild" in name or "Deal" in name or "Trade" in name
                 or "Frm" in name or "TClient" in name or "Process" in name):
        fn = ida_funcs.get_func(ea)
        out.append({"ea": hex(ea), "name": name,
                    "range": [hex(fn.start_ea), hex(fn.end_ea)]})
out.sort(key=lambda x: x["ea"])
json.dump(out, open("D:/vbcoding/mir2/mir2-client1.5-delphi/mir2-client1.5-delphi/scratch/ida_dialogs.json",
                    "w", encoding="utf-8"), ensure_ascii=False, indent=2)
print("DIALOG_DONE " + str(len(out)))
import ida_pro
ida_pro.qexit(0)
