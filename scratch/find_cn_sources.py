import os, re
roots = [r'D:\vbcoding\mir2', ]
# 带有典型中文怪物/物品/技能名的数据文件 (排除代码与二进制资源)
keys = ['祖玛教主', '火球术', '半兽人', '屠龙', '金创药']
skip_ext = {'.wil', '.wix', '.png', '.jpg', '.bmp', '.dll', '.exe', '.pdb', '.import', '.wav', '.map', '.mp3', '.ogg', '.pck', '.zip', '.7z', '.rar'}
skip_dir = {'.git', 'bin', 'obj', '.godot', 'node_modules', '__pycache__'}
hits = {}
for root in roots:
    for dp, dn, fn in os.walk(root):
        dn[:] = [d for d in dn if d not in skip_dir]
        for f in fn:
            ext = os.path.splitext(f)[1].lower()
            if ext in skip_ext or ext in ('.cs',):
                continue
            p = os.path.join(dp, f)
            try:
                if os.path.getsize(p) > 8_000_000:
                    continue
                b = open(p, 'rb').read()
            except Exception:
                continue
            for enc in ('utf-8', 'gbk'):
                try:
                    t = b.decode(enc)
                    break
                except Exception:
                    t = None
            if not t:
                continue
            c = sum(t.count(k) for k in keys)
            if c >= 3:
                hits[p] = c
for p, c in sorted(hits.items(), key=lambda x: -x[1])[:25]:
    print(c, p)
