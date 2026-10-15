import os
from PIL import Image

# 扫描 Mon1, Mon3, Mon4, Mon5 中是否有白色高大的雪人
# 雪人体型较大 (宽 > 60, 高 > 70)
# 让我们在 Mon3 的前 200 帧里看看
for m in [1, 2, 3, 4, 5]:
    folder = rf'resources\wil\png\Mon{m}'
    if os.path.exists(folder):
        count = len(os.listdir(folder))
        print(f'Mon{m}: {count} files')
