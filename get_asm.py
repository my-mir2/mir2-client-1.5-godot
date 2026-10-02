import idc
start = 0x493d21
end = 0x493d2f + 5
curr = start
res = []
while curr < end:
    res.append(f"{hex(curr)}: {idc.generate_disasm_line(curr, 0)}")
    curr = idc.next_head(curr)
print("\n".join(res))
