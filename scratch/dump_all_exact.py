import struct

path = r'D:\vbcoding\mir2\欧洲官方-14Server\korea\mud2\DB\StdItems.DB'
with open(path, 'rb') as f:
    data = f.read()

block_size = 0x800
rec_size = 70
blk = 0x800

items = []
while blk + 10 < len(data):
    for n in range(29):
        slot = blk + 8 + n * rec_size
        if slot + rec_size > blk + block_size or slot + rec_size > len(data):
            break
        chunk = data[slot:slot+rec_size]
        name_raw = chunk[2:16]
        if b'\x00' in name_raw:
            name = name_raw[:name_raw.find(b'\x00')].decode('latin1', errors='ignore').strip()
        else:
            name = name_raw.decode('latin1', errors='ignore').strip()
        if not name:
            continue
        
        # looks 在 chunk[26:28] 或 [24:26]?
        # 让我们把 chunk[20:30] 的 16 进制打印出来
        looks_b = chunk[26:28]
        looks = struct.unpack('>H', looks_b)[0]
        items.append((name, looks, chunk[20:30].hex()))
    blk += block_size

print(f"Total: {len(items)}")
for name, looks, hx in items:
    if any(k in name.lower() for k in ['drug', 'potion', 'meat', 'sword', 'dress', 'armour', 'ring', 'necklace', 'bracelet']):
        print(f"{name:<20}: looks={looks:<4} (hex={hx})")
