import os, struct
from PIL import Image, ImageDraw, ImageFont

# 1. 查找和解析 StdItems.DB
db_path = r'D:\vbcoding\mir2\mir2-my\reference\欧洲官方-14Server\korea\mud2\DB\StdItems.DB'
with open(db_path, 'rb') as f:
    data = f.read()

# Paradox .DB 格式:
# Header: 
# 0x00: RecordSize (u16)
# 0x02: HeaderBlocks (u16) or FileType
# 0x06: TotalRecords (u32)
rec_size = struct.unpack_from('<H', data, 0)[0]
header_size = struct.unpack_from('<H', data, 2)[0]
field_count = struct.unpack_from('<H', data, 33)[0] if len(data) > 34 else 0
print(f"Record Size: {rec_size}, Header Size: {header_size}, Field Count: {field_count}")

# 盛大/欧洲官方 1.4/1.5 StdItems 结构通常是:
# Name (14 bytes null-terminated/fixed)
# StdMode (1 byte)
# Shape (1 byte)
# Weight (1 byte)
# AniCount (1 byte)
# Source (1 byte)
# Reserved (1 byte)
# Looks (2 bytes, u16 LE)
# DuraMax (2 or 4 bytes)
# ...
# 让我们扫描数据块中的每个记录
items = []
# Paradox records start after header blocks (typically at 0x800 = 2048)
block_size = 2048
offset = 2048
while offset + rec_size <= len(data):
    chunk = data[offset : offset + rec_size]
    # Check if this block looks like a valid record
    # Name is at start or after a record header (Paradox record header is often 0 or small flags)
    # Let's inspect chunk
    name_bytes = chunk[:15]
    try:
        # find null terminator
        zero_pos = name_bytes.find(b'\x00')
        if zero_pos > 0:
            name = name_bytes[:zero_pos].decode('latin1', errors='ignore').strip()
        else:
            name = name_bytes.decode('latin1', errors='ignore').strip()
        if len(name) >= 3 and name[0].isalpha():
            # Looks is typically at offset 19 or 20 (u16)
            # Let's print out the raw bytes around name
            # StdMode, Shape, Weight, AniCount, Source, Reserved, Looks (u16)
            std_mode = chunk[14] if len(chunk) > 14 else 0
            shape = chunk[15] if len(chunk) > 15 else 0
            weight = chunk[16] if len(chunk) > 16 else 0
            ani_count = chunk[17] if len(chunk) > 17 else 0
            looks = struct.unpack_from('<H', chunk, 20)[0] if len(chunk) >= 22 else 0
            items.append((name, std_mode, shape, weight, looks, offset))
    except Exception as e:
        pass
    offset += rec_size

print(f"Parsed {len(items)} items from StdItems.DB:")
potions = [it for it in items if 'Potion' in it[0] or 'Drug' in it[0] or 'HP' in it[0] or 'MP' in it[0] or 'Sun' in it[0] or 'Water' in it[0] or 'Herb' in it[0] or it[1] == 0]
for it in potions[:30]:
    print(f"Name: {it[0]:<20} StdMode: {it[1]:<3} Shape: {it[2]:<3} Weight: {it[3]:<3} Looks: {it[4]:<5} Offset: {it[5]}")

# 2. 生成 Items_0000 到 Items_0060 的图集一览
items_dir = r'D:\vbcoding\mir2\mir2-client1.5-delphi\mir2-client1.5-delphi\resources\wil\png\Items'
cols = 10
rows = 6
cell_w, cell_h = 70, 70
canvas = Image.new('RGB', (cols * cell_w, rows * cell_h), (20, 20, 25))
draw = ImageDraw.Draw(canvas)

for i in range(60):
    fn = os.path.join(items_dir, f"Items_{i:04d}.png")
    col = i % cols
    row = i // cols
    x = col * cell_w
    y = row * cell_h
    # draw border
    draw.rectangle([x, y, x + cell_w - 1, y + cell_h - 1], outline=(50, 50, 60))
    draw.text((x + 2, y + 2), f"{i}", fill=(180, 180, 200))
    if os.path.exists(fn):
        try:
            im = Image.open(fn).convert('RGBA')
            # paste in center
            px = x + (cell_w - im.width) // 2
            py = y + 16 + (cell_h - 16 - im.height) // 2
            canvas.paste(im, (px, py), im)
        except Exception as e:
            pass

out_img = r'scratch/items_catalog_0_59.png'
os.makedirs('scratch', exist_ok=True)
canvas.save(out_img)
print(f"Saved catalog to {out_img}")
