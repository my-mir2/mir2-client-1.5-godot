import capstone

with open(r'D:\vbcoding\mir2\mir2-my\radare2\MIR_final_v3_p - Copy.exe', 'rb') as f:
    data = f.read()

# 0x6E09D -> VA 0x46EC9D
# 往回找最近的函数头 (55 8b ec / push ebp; mov ebp, esp, 或者 push ebp; mov ebp, esp; add esp, ...)
file_target = 0x6E09D
start = file_target
while start > file_target - 200:
    if data[start:start+3] == b'\x55\x8b\xec':
        break
    start -= 1

va_start = 0x400C00 + start
print(f"Func start at file 0x{start:X}, VA 0x{va_start:X}")

code = data[start : file_target + 100]
cs = capstone.Cs(capstone.CS_ARCH_X86, capstone.CS_MODE_32)
for insn in cs.disasm(code, va_start):
    mark = "  <-- REF HERE" if insn.address == 0x46EC9D or "4a2ce8" in insn.op_str.lower() else ""
    print(f"0x{insn.address:06X}: {insn.mnemonic:<8} {insn.op_str}{mark}")
