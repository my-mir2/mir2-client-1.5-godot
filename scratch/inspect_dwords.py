with open(r'D:\vbcoding\mir2\mir2-my\radare2\MIR_final_v3_p - Copy.exe', 'rb') as f:
    data = f.read()

import struct

# 0xA20E8 处读 10 个 DWORD
for i in range(-5, 15):
    pos = 0xA20E8 + i * 4
    val = struct.unpack('<I', data[pos:pos+4])[0]
    print(f"0x{0x4A2CE8 + i*4:X} (file 0x{pos:X}): 0x{val:X}")
