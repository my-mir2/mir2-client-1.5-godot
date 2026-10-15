with open(r'D:\vbcoding\mir2\mir2-my\radare2\MIR_final_v3_p - Copy.exe', 'rb') as f:
    data = f.read()

# PE Base 通常是 0x400000
# 找 '宋体' 的文件偏移
idx = data.find('宋体'.encode('gbk'))
print(f"'宋体' file offset: 0x{idx:X}")

# 假定 .rdata 或 .data 节，计算 VA
# 搜索引用此偏移（或 VA）的代码
# 读取 PE 节头
import struct
pe_offset = struct.unpack('<I', data[0x3c:0x40])[0]
num_sections = struct.unpack('<H', data[pe_offset+6:pe_offset+8])[0]
opt_hdr_size = struct.unpack('<H', data[pe_offset+20:pe_offset+22])[0]
image_base = struct.unpack('<I', data[pe_offset+28+24:pe_offset+28+28])[0] # 32-bit PE

sec_table_offset = pe_offset + 24 + opt_hdr_size
sections = []
for i in range(num_sections):
    sec = data[sec_table_offset + i*40 : sec_table_offset + (i+1)*40]
    name = sec[:8].decode('latin1').strip('\x00')
    vsize, vaddr, rsize, raddr = struct.unpack('<IIII', sec[8:24])
    sections.append((name, vaddr, vsize, raddr, rsize))

va = 0
for name, vaddr, vsize, raddr, rsize in sections:
    if raddr <= idx < raddr + rsize:
        va = image_base + vaddr + (idx - raddr)
        print(f"In section {name}: VA = 0x{va:X}")
        break

if va != 0:
    # 搜索 0x{va:X} 的小端字节
    target_bytes = struct.pack('<I', va)
    pos = 0
    refs = []
    while True:
        ref_idx = data.find(target_bytes, pos)
        if ref_idx == -1: break
        # 计算引用的 VA
        ref_va = 0
        for name, vaddr, vsize, raddr, rsize in sections:
            if raddr <= ref_idx < raddr + rsize:
                ref_va = image_base + vaddr + (ref_idx - raddr)
                break
        refs.append((ref_idx, ref_va))
        pos = ref_idx + 4
    print(f"References to VA 0x{va:X}: {len(refs)}")
    for r_raw, r_va in refs:
        print(f"  Ref at file 0x{r_raw:X}, VA 0x{r_va:X}")
