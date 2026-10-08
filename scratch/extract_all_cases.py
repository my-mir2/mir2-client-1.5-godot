import re

with open('scratch/ida_processmsg.c', 'r', encoding='utf-8', errors='ignore') as f:
    text = f.read()

cases = set()

# 匹配 case
for m in re.finditer(r'case\s+([^:]+):', text):
    raw = m.group(1).strip()
    val = None
    if raw.startswith("'") and raw.endswith("'"):
        content = raw[1:-1]
        if content.startswith('\\x'):
            val = int(content[2:], 16)
        elif len(content) == 1:
            val = ord(content)
    elif raw.startswith('0x') or raw.startswith('0X'):
        val = int(raw.rstrip('uUlL'), 16)
    elif raw.isdigit() or (raw.startswith('-') and raw[1:].isdigit()):
        val = int(raw)
    if val is not None and val >= 0:
        cases.add((val, raw))

# 匹配 v67 == ...
for m in re.finditer(r'v67\s*==\s*([0-9a-zA-Z]+)', text):
    raw = m.group(1).strip()
    val = None
    if raw.startswith('0x') or raw.startswith('0X'):
        val = int(raw.rstrip('uUlL'), 16)
    elif raw.isdigit():
        val = int(raw)
    if val is not None and val >= 0:
        cases.add((val, f'v67=={raw}'))

print(f"Total unique opcodes found: {len(set(c[0] for c in cases))}")
for val in sorted(set(c[0] for c in cases)):
    matches = [c[1] for c in cases if c[0] == val]
    print(f"Opcode {val:4d} (0x{val:04X}): {', '.join(matches)}")
