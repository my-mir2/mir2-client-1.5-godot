import capstone
import struct
import os

EXE_PATH = r'D:\vbcoding\mir2\mir2-my\radare2\MIR_final_v3_p - Copy.exe'
with open(EXE_PATH, 'rb') as f:
    data = f.read()

def va_to_off(va):
    if 0x401000 <= va < 0x4A2000:
        return 0x400 + (va - 0x401000)
    if 0x4A2000 <= va < 0x4A5000:
        return 0xA1400 + (va - 0x4A2000)
    return None

def get_delphi_str(va):
    off = va_to_off(va)
    if not off or off < 8 or off >= len(data):
        return None
    rc, slen = struct.unpack('<iI', data[off-8:off])
    if rc == -1 and 1 <= slen <= 240 and off + slen <= len(data):
        try:
            s = data[off:off+slen].decode('gbk', errors='replace')
            return s.replace('\r', '\\r').replace('\n', '\\n')
        except:
            pass
    slen2 = data[off]
    if 1 <= slen2 <= 80 and off + 1 + slen2 <= len(data):
        b = data[off+1:off+1+slen2]
        if all(32 <= c < 127 for c in b):
            return b.decode('ascii')
    return None

# Collect VMTs and DMTs
syms = {}
for off in range(va_to_off(0x445000), va_to_off(0x4A1F80), 4):
    va = 0x401000 + (off - 0x400)
    val = struct.unpack('<I', data[off:off+4])[0]
    if val == va + 0x4C:
        vmt = va + 0x4C
        voff = off + 0x4C
        cname_ptr = struct.unpack('<I', data[voff-0x2C:voff-0x28])[0]
        coff = va_to_off(cname_ptr)
        if not coff:
            continue
        slen = data[coff]
        cname = data[coff+1:coff+1+slen].decode('ascii', errors='ignore')
        syms[vmt] = f'VMT_{cname}'
        destroy_va = struct.unpack('<I', data[voff-0x04:voff])[0]
        if 0x445000 <= destroy_va < 0x4A2000:
            syms.setdefault(destroy_va, f'{cname}.Destroy')
        dmt_ptr = struct.unpack('<I', data[voff-0x30:voff-0x2C])[0]
        if dmt_ptr and va_to_off(dmt_ptr):
            doff = va_to_off(dmt_ptr)
            dcount = struct.unpack('<H', data[doff:doff+2])[0]
            if dcount < 100:
                idxs = [struct.unpack('<h', data[doff+2+i*2:doff+4+i*2])[0] for i in range(dcount)]
                addrs = [struct.unpack('<I', data[doff+2+dcount*2+i*4:doff+6+dcount*2+i*4])[0] for i in range(dcount)]
                for idx, addr in zip(idxs, addrs):
                    if 0x445000 <= addr < 0x4A2000:
                        syms.setdefault(addr, f'{cname}.DMT_{idx}')

cs = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)

def disasm_smart(start_va, end_va, out_file):
    with open(out_file, 'w', encoding='utf-8') as out:
        cur = start_va
        while cur < end_va:
            if cur in syms:
                out.write(f'\n; === {syms[cur]} @ 0x{cur:06X} ===\n')
            off = va_to_off(cur)
            # Check if cur is at a Delphi AnsiString literal header (FF FF FF FF <len>)
            if cur + 8 <= end_va:
                rc, slen = struct.unpack('<iI', data[off:off+8])
                if rc == -1 and 1 <= slen <= 500 and off + 8 + slen <= len(data):
                    s = data[off+8:off+8+slen].decode('gbk', errors='replace').replace('\r', '\\r').replace('\n', '\\n')
                    out.write(f'0x{cur+8:06X}: [STR len={slen}] \"{s}\"\n')
                    nxt = cur + 8 + slen + 1
                    # align to 4 bytes
                    nxt = (nxt + 3) & ~3
                    cur = nxt
                    continue
            # Disassemble chunk until invalid or string header
            chunk_end = min(end_va, cur + 4096)
            chunk = data[off:va_to_off(chunk_end)]
            insns = list(cs.disasm(chunk, cur))
            if not insns:
                cur += 1
                continue
            for insn in insns:
                if insn.address in syms and insn.address != cur:
                    out.write(f'\n; === {syms[insn.address]} @ 0x{insn.address:06X} ===\n')
                comment = ''
                for tok in insn.op_str.replace('[', ' ').replace(']', ' ').replace(',', ' ').replace('+', ' ').split():
                    if tok.startswith('0x'):
                        try:
                            v = int(tok, 16)
                            if v in syms:
                                comment += f' ; {syms[v]}'
                            st = get_delphi_str(v)
                            if st:
                                comment += f' ; \"{st}\"'
                            elif 0x4A2000 <= v < 0x4A5000:
                                doff = va_to_off(v)
                                target = struct.unpack('<I', data[doff:doff+4])[0]
                                comment += f' ; *0x{v:06X}->0x{target:06X}'
                        except:
                            pass
                out.write(f'0x{insn.address:06X}: {insn.mnemonic:<8s} {insn.op_str}{comment}\n')
                cur = insn.address + insn.size
                if insn.mnemonic in ('ret', 'retn'):
                    # skip alignment or check next bytes
                    break

units = [
    ('Wil',       0x452CF8, 0x456090),
    ('DWinCtl',   0x456090, 0x458EF4),
    ('SoundUtil', 0x45DE00, 0x45E474),
    ('ClMain',    0x45E474, 0x46EDB8),
    ('PlayScn',   0x46EDB8, 0x473DFC),
    ('AxeMon',    0x473DFC, 0x47733C),
    ('ClEvent',   0x47733C, 0x477A40),
    ('HerbActor', 0x477A40, 0x479884),
    ('DrawScrn',  0x479884, 0x47AB78),
    ('IntroScn',  0x47AB78, 0x47FEC8),
    ('FrmDlg',    0x47FEC8, 0x495B18),
    ('Actor',     0x495B18, 0x49C124),
    ('magiceff',  0x49C124, 0x49EA9C),
    ('MapUnit',   0x49EA9C, 0x4A2000),
]

for name, s, e in units:
    disasm_smart(s, e, f'scratch/disasm_{name}.asm')
    print(f'Smart disassembled {name}')
