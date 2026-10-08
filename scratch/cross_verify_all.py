import re

# 1. Parse ProtocolConsts.cs
const_map = {}
with open('src/Net/ProtocolConsts.cs', 'r', encoding='utf-8') as f:
    for line in f:
        m = re.search(r'public\s+const\s+(?:short|int)\s+([A-Za-z0-9_]+)\s*=\s*([0-9\-]+);', line)
        if m:
            const_map[m.group(1)] = int(m.group(2))

print(f'Parsed {len(const_map)} protocol consts.')

with open('src/Net/ServerMessageRouter.cs', 'r', encoding='utf-8') as f:
    router_text = f.read()

for m in re.finditer(r'public\s+const\s+(?:short|int)\s+([A-Za-z0-9_]+)\s*=\s*([0-9\-]+);', router_text):
    const_map[m.group(1)] = int(m.group(2))

router_handled = set()
for m in re.finditer(r'case\s+([A-Za-z0-9_.]+):', router_text):
    val = m.group(1).replace('ProtocolConsts.', '')
    if val in const_map:
        router_handled.add(const_map[val])
    elif val.lstrip('-').isdigit():
        router_handled.add(int(val))
    else:
        print(f'Unresolved case in router: {val}')


print(f'Router handles {len(router_handled)} unique integer opcodes.')

# 3. Parse ida_processmsg.c cases
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
        return None

ida_cases = {}
with open('scratch/ida_processmsg.c', 'r', encoding='utf-8', errors='ignore') as f:
    lines = f.readlines()

blocks = [186, 314, 632, 833, 904, 945, 1007, 1127, 1180, 1202, 1262, 1432, 1540, 1678, len(lines)]
for idx in range(len(blocks)-1):
    start_l = blocks[idx]
    end_l = blocks[idx+1]
    block_lines = lines[start_l-1:end_l-1]
    # check if there are sub-switches
    sub_switch = False
    for l_idx, line in enumerate(block_lines):
        st = line.strip()
        if st.startswith('switch ') and 'v67' not in st:
            sub_switch = True
        if st.startswith('case ') and st.endswith(':'):
            c_val = st[5:-1].strip()
            num = parse_val(c_val)
            if num is not None and num > 0: # only positive top opcodes
                ida_cases[num] = (start_l + l_idx, c_val)

print(f'IDA has {len(ida_cases)} positive opcodes in v67 switch blocks.')

# Diff
missing_in_router = []
for op, (line_no, raw) in sorted(ida_cases.items()):
    if op not in router_handled:
        missing_in_router.append((op, line_no, raw))

print(f'Missing in Router: {len(missing_in_router)}')
for op, l_no, raw in missing_in_router:
    print(f'  Opcode {op} (raw: {raw}, line {l_no})')
