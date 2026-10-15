with open(r'D:\vbcoding\mir2\mir2-my\radare2\MIR_final_v3_p - Copy.exe', 'rb') as f:
    data = f.read()

import struct
pe_offset = struct.unpack('<I', data[0x3c:0x40])[0]
num_sections = struct.unpack('<H', data[pe_offset+6:pe_offset+8])[0]
opt_hdr_size = struct.unpack('<H', data[pe_offset+20:pe_offset+22])[0]
sec_table_offset = pe_offset + 24 + opt_hdr_size

print(f"{'Name':<8} {'VAddr':<10} {'VSize':<10} {'RAddr':<10} {'RSize':<10}")
for i in range(num_sections):
    sec = data[sec_table_offset + i*40 : sec_table_offset + (i+1)*40]
    name = sec[:8].decode('latin1').strip('\x00')
    vsize, vaddr, rsize, raddr = struct.unpack('<IIII', sec[8:24])
    print(f"{name:<8} 0x{vaddr:08X} 0x{vsize:08X} 0x{raddr:08X} 0x{rsize:08X}")
