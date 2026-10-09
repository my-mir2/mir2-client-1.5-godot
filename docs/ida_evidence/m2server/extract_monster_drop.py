# -*- coding: utf-8 -*-
"""
IDA Python script to extract ProcessMonsters, ScatterBagItems, DropUseItems and MonGen
"""
import os
import idautils
import ida_funcs
import ida_name
import ida_hexrays
import ida_auto
import idc

def main():
    output_dir = r"D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\docs\ida_evidence\m2server"
    os.makedirs(output_dir, exist_ok=True)

    print("[M2-MonsterDrop] Locating target functions by string xrefs...")
    
    # 关键字符串物理地址
    target_string_eas = [
        0x4b1b70, # [UsrEngn] ProcessMonsters
        0x4bb614, # [Exception] TCreature.ScatterBagItems
        0x4bba54, # [Exception] TCreature.DropUseItems
        0x4ae174, # [TUserEngine] RegenMonsters exception
        0x4b0a60, # [TUserEngine] MakeNewHuman exception
        0x4c6220, # Unable to pick up item
        0x485ddc, # MonItems\
        0x486c04  # MonGen.txt
    ]

    target_funcs = {}
    for sea in target_string_eas:
        for xref in idautils.XrefsTo(sea):
            fn = ida_funcs.get_func(xref.frm)
            if fn:
                target_funcs[fn.start_ea] = (ida_name.get_name(fn.start_ea), fn.end_ea - fn.start_ea)

    print(f"[M2-MonsterDrop] Found {len(target_funcs)} functions referencing key strings.")

    # 尝试反编译
    out_file = os.path.join(output_dir, "m2_monster_drop_decompiled.c")
    with open(out_file, "w", encoding="utf-8") as f:
        for ea, (name, size) in target_funcs.items():
            try:
                cfunc = ida_hexrays.decompile(ea)
                if cfunc:
                    f.write(f"// ==================== [0x{ea:X}] {name} (size: {size}) ====================\n")
                    f.write(str(cfunc) + "\n\n")
                    print(f"Decompiled 0x{ea:X} {name}")
            except Exception as e:
                print(f"Decompile failed for 0x{ea:X}: {e}")

    print("[M2-MonsterDrop] Extraction finished!")
    idc.qexit(0)

if __name__ == "__main__":
    main()
