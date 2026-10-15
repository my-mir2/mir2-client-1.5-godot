import struct

path = r'D:\vbcoding\mir2\欧洲官方-14Server\korea\mud2\DB\StdItems.DB'
with open(path, 'rb') as f:
    data = f.read()

def parse_num(b):
    if len(b) == 1:
        return b[0] ^ 0x80
    elif len(b) == 2:
        return struct.unpack('>H', b)[0] ^ 0x8000
    elif len(b) == 4:
        return struct.unpack('>I', b)[0] ^ 0x80000000

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
        idx = struct.unpack('>H', chunk[0:2])[0]
        name_raw = chunk[2:16]
        if b'\x00' in name_raw:
            name = name_raw[:name_raw.find(b'\x00')].decode('latin1', errors='ignore').strip()
        else:
            name = name_raw.decode('latin1', errors='ignore').strip()
        if not name:
            continue
        
        stdmode = parse_num(chunk[16:17])
        shape = parse_num(chunk[17:18])
        weight = parse_num(chunk[18:19])
        anicount = parse_num(chunk[19:20])
        source = parse_num(chunk[20:21])
        reserved = parse_num(chunk[21:22])
        looks = parse_num(chunk[22:26])
        duramax = parse_num(chunk[26:30])
        ac = parse_num(chunk[30:31])
        ac2 = parse_num(chunk[31:32])
        price = parse_num(chunk[62:66])
        
        items.append((idx, name, stdmode, shape, looks, duramax, price))
    blk += block_size

print(f"Total items extracted: {len(items)}")
print(f"{'Idx':<4} {'Name':<20} {'StdMode':<8} {'Shape':<6} {'Looks':<8} {'Price':<8}")
print('-'*60)
for it in items:
    name_lower = it[1].lower()
    if any(k in name_lower for k in ['drug', 'potion', 'meat', 'sword', 'dress', 'armour']):
        print(f"{it[0]:<4} {it[1]:<20} {it[2]:<8} {it[3]:<6} {it[4]:<8} {it[6]:<8}")
