import struct

path = r'D:\vbcoding\mir2\欧洲官方-14Server\korea\mud2\DB\Monster.DB'
with open(path, 'rb') as f:
    data = f.read()

# Monster.DB: 块 0x800, 名称 at blk+6, 槽 56 (0x38), 名称最大 14
block_size = 0x800
name_at = 0x06
rec_size = 56
name_max = 14

blk = 0x800
monsters = []
while blk + name_at < len(data):
    n = 0
    while True:
        slot = blk + name_at + n * rec_size
        if slot + rec_size > blk + block_size or slot + rec_size > len(data):
            break
        name_raw = data[slot:slot+name_max]
        if b'\x00' in name_raw:
            name = name_raw[:name_raw.find(b'\x00')].decode('latin1', errors='ignore').strip()
        else:
            name = name_raw.decode('latin1', errors='ignore').strip()
        if not name:
            break
        
        # 看看 Appr 字段 (在名称后面)
        after = data[slot+name_max : slot+rec_size]
        # after 中有 Race, RaceImg, Appr, Lvl, Undead, CoolEye, Exp, HP, MP, AC, MAC, DC, DCMax, MC, SC, Speed, Hit, Walk_Spd, WalkStep, WalkWait, Attack_Spd
        # 打印名称和前 10 个字段的十六进制
        monsters.append((name, after))
        n += 1
    blk += block_size

print(f"Total monsters in Monster.DB: {len(monsters)}")
for name, after in monsters:
    if any(k in name.lower() for k in ['snow', 'cat', 'orc', 'hook', 'scare', 'guard', 'deer', 'hen', 'zombie']):
        hex_str = ' '.join(f'{b:02x}' for b in after[:12])
        print(f"{name:<18}: {hex_str}")
