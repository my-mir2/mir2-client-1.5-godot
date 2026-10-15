import capstone

with open(r'D:\vbcoding\mir2\mir2-my\radare2\MIR_final_v3_p - Copy.exe', 'rb') as f:
    data = f.read()

# 0x6E09D
# 往回找 55 8B EC (push ebp; mov ebp, esp)
start_offset = 0x6E09D
while start_offset > 0x6E000:
    if data[start_offset:start_offset+3] == b'\x55\x8b\xec':
        break
    start_offset -= 1

print(f"Function start at file 0x{start_offset:X}, VA 0x{0x400000 + start_offset:X}")
func_code = data[start_offset:start_offset + 300]
cs = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)
for insn in cs.disasm(func_code, 0x400000 + start_offset):
    print(f"0x{insn.address:06X}: {insn.mnemonic:<8} {insn.op_str}")
