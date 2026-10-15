from PIL import Image
import os

# 扫描 Mon1 ~ Mon18
# 打印每个库里每个 360 帧 offset 的怪
found = []
for m in range(1, 19):
    folder = rf'resources\wil\png\Mon{m}'
    if not os.path.exists(folder): continue
    files = os.listdir(folder)
    # 统计最大的 index
    indices = [int(f.split('_')[1].split('.')[0]) for f in files if f.endswith('.png')]
    if not indices: continue
    max_idx = max(indices)
    for base in range(0, max_idx + 1, 360):
        fname = f'Mon{m}_{base:04d}.png'
        p = os.path.join(folder, fname)
        if os.path.exists(p):
            img = Image.open(p)
            found.append((m, base, img.size))

print(f"Total monster bases found: {len(found)}")
for m, b, sz in found:
    print(f"Mon{m} @ {b:4d}: size={sz}")
