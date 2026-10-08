import re

with open('scratch/ida_processmsg.c', 'r', encoding='utf-8', errors='ignore') as f:
    lines = f.readlines()

blocks = [186, 314, 632, 833, 904, 945, 1007, 1127, 1180, 1202, 1262, 1432, 1540, 1678, len(lines)]

def parse_val(v):
    v = v.strip()
    if v.startswith("'") and v.endswith("'"):
        c = v[1:-1]
        if c.startswith('\\'):
            c = c[1:]
        return ord(c) if c else 0
    if v.endswith('u'):
        v = v[:-1]
    if v.startswith('0x') or v.startswith('0X'):
        return int(v, 16)
    try:
        return int(v)
    except:
        return v

for idx in range(len(blocks)-1):
    start_l = blocks[idx]
    end_l = blocks[idx+1]
    block_lines = lines[start_l-1:end_l-1]
    print(f'=== Block at Line {start_l} ===')
    for l_idx, line in enumerate(block_lines):
        st = line.strip()
        if st.startswith('case ') and st.endswith(':'):
            c_val = st[5:-1].strip()
            num = parse_val(c_val)
            # Preview first code line after case
            next_code = ""
            for k in range(l_idx+1, min(l_idx+6, len(block_lines))):
                kl = block_lines[k].strip()
                if kl and not kl.startswith('case ') and not kl.startswith('{') and not kl.startswith('//'):
                    next_code = kl
                    break
            print(f'  L{start_l + l_idx}: {c_val:10} -> {str(num):6} | {next_code[:60]}')
