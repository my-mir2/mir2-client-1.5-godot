import struct

path = r'D:\vbcoding\mir2\欧洲官方-14Server\korea\mud2\DB\StdItems.DB'
with open(path, 'rb') as f:
    data = f.read()

names = ['(HP)DrugSmall', '(MP)DrugSmall', '(HP)DrugMedium', '(MP)DrugMedium', '(HP)DrugLarge', '(MP)DrugLarge', 'SunPotion', 'Meat', 'WoodenSword', 'BaseDress(M)']
for name in names:
    pos = data.find(name.encode('latin1') + b'\x00')
    if pos != -1:
        chunk = data[pos-2:pos-2+70]
        # 解析前 30 个字节
        hex_bytes = ' '.join(f'{b:02x}' for b in chunk)
        print(f'{name:<16}: pos={pos-2}')
        print(f'  {hex_bytes}')
