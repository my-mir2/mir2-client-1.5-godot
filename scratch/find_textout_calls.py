with open(r'D:\vbcoding\mir2\mir2-my\radare2\MIR_final_v3_p - Copy.exe', 'rb') as f:
    data = f.read()

import struct

# .idata section: VAddr=0xFA000, RAddr=0xA4400
# 找 TextOutA (0xA57B9) 的指针在 IAT 中的位置
# 导入表的 IAT 往往在 0xA4400 附近
# 在 data[0xA4400:0xA7400] 搜索指向 0xFA000 + (0xA57B9 - 0xA4400 - 2) 的地址 (带 Hint)
hint_name_va = 0x400000 + 0xFA000 + (0xA57B9 - 0xA4400 - 2) # Hint is 2 bytes before name
print(f"Hint/Name VA = 0x{hint_name_va:X}")

iat_offset = data.find(struct.pack('<I', hint_name_va))
if iat_offset != -1:
    iat_va = 0x400000 + 0xFA000 + (iat_offset - 0xA4400)
    print(f"IAT entry for TextOutA at file 0x{iat_offset:X}, VA 0x{iat_va:X}")
    
    # 搜索调用此 IAT 的指令: call dword ptr [iat_va] (FF 15 xx xx xx xx)
    call_bytes = b'\xff\x15' + struct.pack('<I', iat_va)
    pos = 0
    calls = []
    while True:
        idx = data.find(call_bytes, pos)
        if idx == -1: break
        calls.append(idx)
        pos = idx + 6
    print(f"Found {len(calls)} calls to TextOutA:")
    for c in calls:
        va = 0x400C00 + c
        print(f"  Call at file 0x{c:X}, VA 0x{va:X}")
else:
    print("IAT entry not found directly, scanning all pointers in .idata")
