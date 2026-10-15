import struct

path = r'D:\vbcoding\mir2\欧洲官方-14Server\korea\mud2\DB\StdItems.DB'
with open(path, 'rb') as f:
    data = f.read()

# 检查 StdItems.DB 结构定义
# 从 bde_parser.rs:
# Idx: u16
# Name: 14 bytes (ASCII)
# 然后是:
# Stdmode(1), Shape(1), Weight(1), Anicount(1), Source(1), Reserved(1), Looks(2), DuraMax(2), ...
# 让我们严格验证 BaseDress(M) (Looks=60)
# BaseDress(M): Name 之后是:
# 80 0a (Stdmode=10)
# 80 01 (Shape=1)
# 80 05 (Weight=5)
# 80 00 (Anicount=0)
# 80 00 (Source=0)
# 80 00 (Reserved=0)
# 80 00 00 3c (Looks) -> 4字节还是2字节?
# 注意: 80 00 00 3c -> 如果是 2 字节: 80 00 可能是 NeedIdentify?
# 来看字段列表:
# 'Idx', 'Name', 'Stdmode', 'Shape', 'Weight', 'Anicount', 'Source', 'Reserved', 'Looks', 'DuraMax', ...
# 让我们逐个打印 BaseDress(M) 和 (HP)DrugSmall 的字节
for name in ['(HP)DrugSmall', '(MP)DrugSmall', '(HP)DrugMedium', '(MP)DrugMedium', '(HP)DrugLarge', '(MP)DrugLarge', 'SunPotion', 'Meat', 'BaseDress(M)', 'WoodenSword']:
    pos = data.find(name.encode() + b'\x00')
    if pos != -1:
        chunk = data[pos-2:pos-2+70]
        name_bytes = chunk[2:16]
        after = chunk[16:]
        # after[0]: Stdmode
        # after[1]: Shape
        # after[2]: Weight
        # after[3]: Anicount
        # after[4]: Source
        # after[5]: Reserved
        # after[6..10]: ?
        print(f'{name:<16}: Stdmode={after[0]^0x80} Shape={after[1]^0x80} Weight={after[2]^0x80} Ani={after[3]^0x80}')
        # 打印接下来的 20 个字节
        print('  after+4:', ' '.join(f'{b:02x}' for b in after[4:24]))
