# -*- coding: utf-8 -*-
"""
IDA Python script to extract ground-truth metadata from M2Server.exe.i64
"""
import sys
import os
import json
import idautils
import ida_funcs
import ida_name
import ida_bytes
import ida_segment
import ida_auto
import idc

def main():
    print("[M2-Truth] Waiting for auto analysis...")
    ida_auto.auto_wait()

    output_dir = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\docs\ida_evidence\m2server"
    os.makedirs(output_dir, exist_ok=True)

    # 1. 提取所有函数列表
    print("[M2-Truth] Extracting functions...")
    funcs = []
    interesting_funcs = []
    
    keywords = [
        "playobject", "userengine", "processuser", "attack", "magic", 
        "damage", "walk", "run", "hit", "spell", "levelup", "item", 
        "timer", "socket", "packet", "decode", "encode", "monster", 
        "guild", "castle", "mir2", "operat", "human"
    ]

    for ea in idautils.Functions():
        flags = idc.get_func_flags(ea)
        name = ida_name.get_name(ea)
        demangled = ida_name.get_ea_name(ea)
        f_end = idc.get_func_attr(ea, idc.FUNCATTR_END)
        size = f_end - ea
        
        info = {
            "ea": hex(ea),
            "name": name,
            "size": size
        }
        funcs.append(info)

        lower_name = name.lower()
        if any(kw in lower_name for kw in keywords):
            interesting_funcs.append(info)

    with open(os.path.join(output_dir, "m2_all_funcs_summary.json"), "w", encoding="utf-8") as f:
        json.dump({
            "total_functions": len(funcs),
            "interesting_functions_count": len(interesting_funcs),
            "interesting_functions": interesting_funcs
        }, f, ensure_ascii=False, indent=2)

    # 2. 提取字符串表
    print("[M2-Truth] Extracting strings...")
    strings = []
    for s in idautils.Strings():
        try:
            str_val = str(s)
            if len(str_val) >= 3:
                strings.append(f"{hex(s.ea)}: {str_val}")
        except:
            pass

    with open(os.path.join(output_dir, "m2_strings.txt"), "w", encoding="utf-8") as f:
        f.write("\n".join(strings))

    # 3. 尝试反编译核心关键函数
    print("[M2-Truth] Checking Hex-Rays decompiler...")
    decompile_enabled = False
    try:
        import ida_hexrays
        if ida_hexrays.init_hexrays_plugin():
            decompile_enabled = True
    except:
        decompile_enabled = False

    decompiled_results = {}
    if decompile_enabled:
        print("[M2-Truth] Decompiling top interesting functions...")
        for fn in interesting_funcs[:50]:
            ea = int(fn["ea"], 16)
            try:
                cfunc = ida_hexrays.decompile(ea)
                if cfunc:
                    decompiled_results[fn["name"]] = str(cfunc)
            except Exception as e:
                decompiled_results[fn["name"]] = f"/* Decompile failed: {e} */"
    else:
        print("[M2-Truth] Hex-Rays not available or not loaded, skipping decompilation.")

    if decompiled_results:
        with open(os.path.join(output_dir, "m2_decompiled_core.c"), "w", encoding="utf-8") as f:
            for fname, code in decompiled_results.items():
                f.write(f"// ==================== {fname} ====================\n")
                f.write(code + "\n\n")

    print("[M2-Truth] Extraction completed successfully!")
    # 保存并退出 IDA 批处理
    idc.qexit(0)

if __name__ == "__main__":
    main()
