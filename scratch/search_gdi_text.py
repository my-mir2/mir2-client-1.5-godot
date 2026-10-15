with open(r'D:\vbcoding\mir2\mir2-my\radare2\MIR_final_v3_p - Copy.exe', 'rb') as f:
    data = f.read()

import struct

# 搜索导入的 TextOutA 或 ExtTextOutA
funcs = [b'TextOutA', b'ExtTextOutA', b'DrawTextA', b'CreateFontIndirectA', b'CreateFontA']
for fn in funcs:
    pos = 0
    while True:
        idx = data.find(fn, pos)
        if idx == -1: break
        print(f"Found {fn.decode()} at file offset 0x{idx:X}")
        pos = idx + len(fn)
