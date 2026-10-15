from PIL import Image
import os

folder = r'resources\wil\png\Mon3'
# Mon3 共有 10 个怪物: 0, 360, 720, 1080, 1440, 1800, 2160, 2520, 2880, 3240
canvas = Image.new('RGBA', (10 * 80, 120), (30, 30, 30, 255))
for i in range(10):
    base = i * 360
    fname = f'Mon3_{base:04d}.png'
    p = os.path.join(folder, fname)
    if os.path.exists(p):
        img = Image.open(p)
        print(f"Offset {base} ({fname}): size={img.size}")
        x = i * 80 + (80 - img.width) // 2
        y = (120 - img.height) // 2
        canvas.paste(img, (x, y), img if img.mode == 'RGBA' else None)
    else:
        print(f"Offset {base} NOT FOUND")

canvas.save(r'scratch\mon3_monsters.png')
print('Saved scratch/mon3_monsters.png')
