import re

with open("scratch/ida_processmsg.c", "r", encoding="utf-8") as f:
    ida_text = f.read()

ida_cases = set()
for m in re.finditer(r"case\s+([0-9xXa-fA-F]+|'[^']+'):", ida_text):
    c = m.group(1)
    if c.startswith("'"):
        val = ord(c[1])
    elif c.lower().startswith("0x"):
        val = int(c, 16)
    else:
        val = int(c)
    ida_cases.add(val)

for m in re.finditer(r"v67\s*==\s*([0-9xXa-fA-F]+)", ida_text):
    c = m.group(1)
    val = int(c, 16) if c.lower().startswith("0x") else int(c)
    ida_cases.add(val)

with open("src/Net/ServerMessageRouter.cs", "r", encoding="utf-8") as f:
    router_text = f.read()

with open("src/Net/ProtocolConsts.cs", "r", encoding="utf-8") as f:
    consts_text = f.read()

const_vals = {}
for m in re.finditer(r"public\s+const\s+(?:int|short)\s+(\w+)\s*=\s*([0-9a-fA-Fx]+);", consts_text):
    val_str = m.group(2)
    val = int(val_str, 16) if val_str.lower().startswith("0x") else int(val_str)
    const_vals[m.group(1)] = val
for m in re.finditer(r"(?:public|private)\s+const\s+(?:int|short)\s+(\w+)\s*=\s*([0-9a-fA-Fx]+);", router_text):
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
    else:
        print("Unknown constant in router:", c)

missing = sorted(list(ida_cases - router_cases))
print("Total IDA cases found:", len(ida_cases))
print("Total Router cases found:", len(router_cases))
print("Missing cases count:", len(missing))
print("Missing cases list:", missing)

print("\n--- DETAILED MISSING CASES ---")
for case_num in missing:
    patterns = [f"case {case_num}:", f"case 0x{case_num:x}:", f"case 0x{case_num:X}:"]
    if 32 <= case_num <= 126:
        patterns.append(f"case '{chr(case_num)}':")
    patterns.append(f"v67 == {case_num}")
    
    for p in patterns:
        idx = ida_text.find(p)
        if idx != -1:
            snippet = ida_text[max(0, idx-20):min(len(ida_text), idx+150)].replace("\n", " ")
            print(f"[{case_num}] {snippet[:120]}")
            break
