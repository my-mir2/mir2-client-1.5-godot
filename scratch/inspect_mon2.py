import os
from PIL import Image

folder = r'resources\wil\png\Mon2'
files = [f for f in os.listdir(folder) if f.endswith('.png')]
print(f'Total pngs in Mon2: {len(files)}')

# 检查空帧（1x1 或 4x1 像素）和有效立绘
valid_frames = []
for f in sorted(files):
    img = Image.open(os.path.join(folder, f))
    if img.width > 10 and img.height > 10:
        idx = int(f.split('_')[1].split('.')[0])
        valid_frames.append((idx, img.size))

print(f'Valid frames: {len(valid_frames)}')
# 打印有效帧的连续区间
ranges = []
start = None
prev = None
for idx, size in valid_frames:
    if start is None:
        start = idx
        prev = idx
    elif idx == prev + 1:
        prev = idx
    else:
        ranges.append((start, prev))
        start = idx
        prev = idx
if start is not None:
    ranges.append((start, prev))

print('Valid frame ranges in Mon2:')
for r in ranges:
    print(f'  {r[0]}..{r[1]} (count: {r[1]-r[0]+1})')
