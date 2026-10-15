from PIL import Image
import os

items = [
    ('Mon1_0000.png', 'Mon1 @ 0'),
    ('Mon1_0360.png', 'Mon1 @ 360'),
    ('Mon1_0720.png', 'Mon1 @ 720'),
    ('Mon2_0000.png', 'Mon2 @ 0'),
    ('Mon4_2160.png', 'Mon4 @ 2160'),
    ('Mon4_2520.png', 'Mon4 @ 2520'),
    ('Mon4_2880.png', 'Mon4 @ 2880'),
    ('Mon5_1800.png', 'Mon5 @ 1800'),
    ('Mon5_2160.png', 'Mon5 @ 2160'),
    ('Mon5_2520.png', 'Mon5 @ 2520'),
]

canvas = Image.new('RGBA', (len(items) * 90, 140), (20, 20, 20, 255))
for i, (fn, desc) in enumerate(items):
    m = fn.split('_')[0]
    p = os.path.join('resources', 'wil', 'png', m, fn)
    if os.path.exists(p):
        img = Image.open(p)
        print(f"{desc}: size={img.size}")
        x = i * 90 + (90 - img.width) // 2
        y = (140 - img.height) // 2
        canvas.paste(img, (x, y), img if img.mode == 'RGBA' else None)

canvas.save(r'scratch\mon_catalog_overview.png')
print('Saved scratch/mon_catalog_overview.png')
