from PIL import Image, ImageDraw, ImageFont
import os

folder = r'resources\wil\png\Items'
cols = 10
rows = 6
cell_w, cell_h = 70, 70
canvas = Image.new('RGBA', (cols * cell_w, rows * cell_h), (30, 30, 30, 255))
draw = ImageDraw.Draw(canvas)

for idx in range(60):
    fname = f'Items_{idx:04d}.png'
    p = os.path.join(folder, fname)
    r = idx // cols
    c = idx % cols
    x = c * cell_w
    y = r * cell_h
    draw.rectangle([x, y, x + cell_w - 1, y + cell_h - 1], outline=(60, 60, 60, 255))
    draw.text((x + 2, y + 2), str(idx), fill=(200, 200, 200, 255))
    if os.path.exists(p):
        img = Image.open(p)
        ix = x + (cell_w - img.width) // 2
        iy = y + 16 + (cell_h - 16 - img.height) // 2
        canvas.paste(img, (ix, iy), img if img.mode == 'RGBA' else None)

canvas.save(r'scratch\items_0_to_59.png')
print('Saved scratch/items_0_to_59.png')
