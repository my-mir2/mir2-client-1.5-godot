with open(r'D:\vbcoding\mir2\mir2-my\radare2\MIR_final_v3_p - Copy.exe', 'rb') as f:
    data = f.read()

import struct

target_bytes = struct.pack('<I', 0x4A2CE8)
pos = 0
refs = []
while True:
    idx = data.find(target_bytes, pos)
    if idx == -1: break
    refs.append(idx)
    pos = idx + 4

print(f"References to g_FontArr (0x4A2CE8): {len(refs)}")
for r in refs:
    # 转换为 VA
    va = 0x400000 + 0x1000 + (r - 0x1000) # CODE 节
    print(f"  Ref at file 0x{r:X} (approx VA 0x{va:X})")
