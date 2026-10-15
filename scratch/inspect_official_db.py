import os, struct, sys
base = r'D:\vbcoding\mir2\欧洲官方-14Server\korea\mud2\DB'
for n in ('Monster.DB', 'Magic.DB', 'StdItems.DB'):
    p = os.path.join(base, n)
    d = open(p, 'rb').read()
    print(f'== {n} size={len(d)}')
    print('head 64:', d[:64].hex(' '))
    # 常见头: TDBHeader 124 字节 (Title string[39]+... ) 之后为记录数组
    print('u32 @0..:', struct.unpack('<8I', d[:32]))
# 在 Monster.DB 里找怪物名，推断记录大小
d = open(os.path.join(base, 'Monster.DB'), 'rb').read()
names = [b'Hen', b'Deer', b'Scarecrow', b'Oma', b'ZumaTaurus']
for nm in names:
    i = d.find(bytes([len(nm)]) + nm)
    print(nm, i)
idx = [m for m in range(len(d)) if d[m:m+4] == bytes([3]) + b'Hen'][:3]
print('Hen positions', idx)
if len(idx) >= 2:
    print('record size guess', idx[1] - idx[0])
