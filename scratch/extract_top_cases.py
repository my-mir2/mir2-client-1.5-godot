import re

with open("scratch/ida_processmsg.c", "r", encoding="utf-8") as f:
    lines = f.readlines()

# 找出所有顶层的 switch ( v67 ) 或 if ( v67 == XXX )
top_cases = []
current_switch_v67 = False
bracket_depth = 0
switch_depth = 0

for line_no, line in enumerate(lines, 1):
    stripped = line.strip()
    
    # 检查是否进入 switch ( v67 )
    if re.search(r"switch\s*\(\s*(?:\([a-zA-Z0-9_ ]+\))?v67\s*\)", stripped):
        current_switch_v67 = True
        switch_depth = bracket_depth
    
    # 检查 if ( v67 == XXX )
    m_if = re.search(r"v67\s*==\s*([0-9xXa-fA-F]+)", stripped)
    if m_if and not current_switch_v67:
        c_str = m_if.group(1)
        val = int(c_str, 16) if c_str.lower().startswith("0x") else int(c_str)
        top_cases.append((val, line_no, stripped))

    if "{" in stripped:
        bracket_depth += stripped.count("{")
    if "}" in stripped:
        bracket_depth -= stripped.count("}")
        if current_switch_v67 and bracket_depth <= switch_depth:
            current_switch_v67 = False

    # 如果在 switch(v67) 的第一层
    if current_switch_v67 and bracket_depth == switch_depth + 1:
        m_case = re.search(r"case\s+([0-9xXa-fA-F]+|'[^']+'):", stripped)
        if m_case:
            c = m_case.group(1)
            if c.startswith("'"):
                val = ord(c[1])
            elif c.lower().startswith("0x"):
                val = int(c, 16)
            else:
                val = int(c)
            top_cases.append((val, line_no, stripped))

print(f"Total top-level SM opcodes found: {len(top_cases)}")

# 提取客户端已路由的 case
with open("src/Net/ProtocolConsts.cs", "r", encoding="utf-8") as f:
    consts_text = f.read()
with open("src/Net/ServerMessageRouter.cs", "r", encoding="utf-8") as f:
    router_text = f.read()

const_vals = {}
for m in re.finditer(r"(?:public|private)\s+const\s+(?:int|short)\s+(\w+)\s*=\s*([0-9a-fA-Fx]+);", consts_text + router_text):
    val_str = m.group(2)
    val = int(val_str, 16) if val_str.lower().startswith("0x") else int(val_str)
    const_vals[m.group(1)] = val

router_cases = set()
for m in re.finditer(r"case\s+(?:ProtocolConsts\.)?(\w+):", router_text):
    c = m.group(1)
    if c.isdigit():
        router_cases.add(int(c))
    elif c.lower().startswith("0x"):
        router_cases.add(int(c, 16))
    elif c in const_vals:
        router_cases.add(const_vals[c])

missing_top = []
for val, line_no, text in top_cases:
    if val not in router_cases:
        missing_top.append((val, line_no, text))

print(f"Top-level opcodes missing in ServerMessageRouter: {len(missing_top)}")
for val, line_no, text in missing_top:
    print(f"  Line {line_no:4d}: Opcode {val:4d} (0x{val:03X}) -> {text}")
