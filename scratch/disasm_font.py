import capstone

with open(r'D:\vbcoding\mir2\mir2-my\radare2\MIR_final_v3_p - Copy.exe', 'rb') as f:
    data = f.read()

# file offset 0xA20E8 - 100
file_offset = 0xA20E8 - 60
code = data[file_offset : file_offset + 200]
start_va = 0x4A2CE8 - 60

cs = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)
for insn in cs.disasm(code, start_va):
    print(f"0x{insn.address:06X}: {insn.mnemonic:<8} {insn.op_str}")
