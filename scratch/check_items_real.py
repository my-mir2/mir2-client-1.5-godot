from PIL import Image
import os

folder = r'resources\wil\png\Items'
check_indices = [1, 16, 28, 29, 394, 395, 396, 397, 398, 399, 400, 3, 4, 9, 10, 11, 12, 14, 15]

# 生成拼接图
cell_w, cell_h = 60, 60
cols = 5
rows = (len(check_indices) + cols - 1) // cols
canvas = Image.new('RGBA', (cols * cell_w, rows * cell_h), (40, 40, 40, 255))

for i, idx in enumerate(check_indices):
    fname = f'Items_{idx:04d}.png'
    p = os.path.join(folder, fname)
    if os.path.exists(p):
        img = Image.open(p)
        print(f'{fname}: size={img.size}')
        r = i // cols
        c = i % cols
        x = c * cell_w + (cell_w - img.width) // 2
        y = r * cell_h + (cell_h - img.height) // 2
        canvas.paste(img, (x, y), img if img.mode == 'RGBA' else None)
    else:
        print(f'{fname}: NOT FOUND')

canvas.save(r'scratch\items_truth_compare.png')
print('Saved scratch/items_truth_compare.png')
