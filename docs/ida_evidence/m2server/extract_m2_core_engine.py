# -*- coding: utf-8 -*-
"""
IDA Python script to extract TUserEngine, TPlayObject and Combat Logic
"""
import os
import json
import idautils
import ida_funcs
import ida_name
import ida_hexrays
import ida_auto
import idc

def main():
    output_dir = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\docs\ida_evidence\m2server"
    os.makedirs(output_dir, exist_ok=True)

    print("[M2-Core] Scanning functions in range 0x4A0000 - 0x4F0000...")
    target_funcs = []
    
    for ea in idautils.Functions(0x4A0000, 0x4F0000):
        name = ida_name.get_name(ea)
        f_end = idc.get_func_attr(ea, idc.FUNCATTR_END)
        size = f_end - ea
        target_funcs.append((ea, name, size))

    print(f"[M2-Core] Found {len(target_funcs)} functions in core range.")

    # 尝试反编译核心战斗与实体控制函数
    decompiled = {}
    key_symbols = [
        "attack", "regen", "damage", "human", "monster", "walk", "run", 
        "magic", "spell", "operate", "timer", "process", "die", "death"
    ]

    # 另外查找对关键字符串引用的函数
    # 0x4ae174: RegenMonsters
    # 0x4b0a60: MakeNewHuman
    # 0x4c27a0: TCreature._Attack
    special_eas = [0x4ae174, 0x4b0a60, 0x4c27a0]
    for sea in special_eas:
        for xref in idautils.XrefsTo(sea):
            fn = ida_funcs.get_func(xref.frm)
            if fn:
                target_funcs.append((fn.start_ea, ida_name.get_name(fn.start_ea), fn.end_ea - fn.start_ea))

    # 去重
    unique_funcs = {}
    for ea, name, size in target_funcs:
        unique_funcs[ea] = (name, size)

    # 针对性反编译
    count = 0
    with open(os.path.join(output_dir, "m2_combat_engine_decompiled.c"), "w", encoding="utf-8") as f:
        for ea, (name, size) in unique_funcs.items():
            lower = name.lower()
            # 筛选重要函数
            is_priority = any(k in lower for k in key_symbols) or size > 300
            if not is_priority:
                continue

            try:
                cfunc = ida_hexrays.decompile(ea)
                if cfunc:
                    f.write(f"// ==================== [0x{ea:X}] {name} (size: {size}) ====================\n")
                    f.write(str(cfunc) + "\n\n")
                    count += 1
            except Exception as e:
                pass

    print(f"[M2-Core] Decompiled {count} core engine functions successfully!")
    idc.qexit(0)

if __name__ == "__main__":
    main()
