import struct, os

def export_all():
    p_items = r'D:\vbcoding\mir2\mir2-my\reference\欧洲官方-14Server\korea\mud2\DB\StdItems.DB'
    with open(p_items, 'rb') as f:
        data = f.read()
    rec_size = 70
    block_size = 2048
    first_block = struct.unpack_from('<H', data, 14)[0]
    cur_block = first_block
    items = []
    visited = set()
    while cur_block != 0 and cur_block not in visited:
        visited.add(cur_block)
        b_offset = cur_block * block_size
        if b_offset + 6 > len(data): break
        next_b, prev_b, data_len = struct.unpack_from('<HHH', data, b_offset)
        num_recs = data_len // rec_size
        for r in range(num_recs):
            ro = b_offset + 6 + r * rec_size
            chunk = data[ro : ro + rec_size]
            idx = struct.unpack_from('>I', chunk, 0)[0] ^ 0x80000000
            name = chunk[4:18].split(b'\x00')[0].decode('latin1', errors='ignore').strip()
            std_mode = struct.unpack_from('>H', chunk, 18)[0] ^ 0x8000
            shape = struct.unpack_from('>H', chunk, 20)[0] ^ 0x8000
            weight = struct.unpack_from('>H', chunk, 22)[0] ^ 0x8000
            looks = struct.unpack_from('>I', chunk, 30)[0] ^ 0x80000000
            dura = struct.unpack_from('>I', chunk, 34)[0] ^ 0x80000000
            price = struct.unpack_from('>I', chunk, 52)[0] ^ 0x80000000
            items.append((idx, name, std_mode, shape, weight, looks, dura, price))
        cur_block = next_b

    p_mon = r'D:\vbcoding\mir2\mir2-my\reference\欧洲官方-14Server\korea\mud2\DB\Monster.DB'
    with open(p_mon, 'rb') as f:
        mdata = f.read()
    m_rec_size = 56
    m_first_block = struct.unpack_from('<H', mdata, 14)[0]
    cur_block = m_first_block
    monsters = []
    visited = set()
    while cur_block != 0 and cur_block not in visited:
        visited.add(cur_block)
        b_offset = cur_block * block_size
        if b_offset + 6 > len(mdata): break
        next_b, prev_b, data_len = struct.unpack_from('<HHH', mdata, b_offset)
        num_recs = data_len // m_rec_size
        for r in range(num_recs):
            ro = b_offset + 6 + r * m_rec_size
            chunk = mdata[ro : ro + m_rec_size]
            name = chunk[0:14].split(b'\x00')[0].decode('latin1', errors='ignore').strip()
            race = struct.unpack_from('>H', chunk, 14)[0] ^ 0x8000
            race_img = struct.unpack_from('>H', chunk, 16)[0] ^ 0x8000
            appr = struct.unpack_from('>H', chunk, 18)[0] ^ 0x8000
            lvl = struct.unpack_from('>H', chunk, 20)[0] ^ 0x8000
            hp = struct.unpack_from('>H', chunk, 26)[0] ^ 0x8000 if len(chunk) >= 28 else 100
            if name:
                monsters.append((name, race, race_img, appr, lvl, hp))
        cur_block = next_b

    os.makedirs('src/Mir2.Shared/Data', exist_ok=True)

    # 1. 写入 OfficialStdItemsRegistry.cs
    with open('src/Mir2.Shared/Data/OfficialStdItemsRegistry.cs', 'w', encoding='utf-8') as f:
        f.write('// ============================================================================\n')
        f.write('// 盛大官方 1.4/1.5 原版 StdItems.DB 377 条全量物理真值静态大字典 (零猜测、纯数据驱动)\n')
        f.write('// 依据欧洲官方/盛大官方 mud2/DB/StdItems.DB 二进制真值导出\n')
        f.write('// ============================================================================\n')
        f.write('using System;\nusing System.Collections.Generic;\n\nnamespace Mir2.Shared.Data;\n\n')
        f.write('public record StdItemEntry(int Idx, string Name, int StdMode, int Shape, int Weight, int Looks, int DuraMax, int Price);\n\n')
        f.write('public static class OfficialStdItemsRegistry\n{\n')
        f.write('    public static readonly Dictionary<int, StdItemEntry> ItemsByIdx = new();\n')
        f.write('    public static readonly Dictionary<string, StdItemEntry> ItemsByName = new(StringComparer.OrdinalIgnoreCase);\n\n')
        f.write('    static OfficialStdItemsRegistry()\n    {\n')
        for it in items:
            s_name = it[1].replace('"', '')
            f.write(f'        Register(new StdItemEntry({it[0]}, "{s_name}", {it[2]}, {it[3]}, {it[4]}, {it[5]}, {it[6]}, {it[7]}));\n')
        f.write('    }\n\n')
        f.write('    private static void Register(StdItemEntry e)\n    {\n')
        f.write('        ItemsByIdx[e.Idx] = e;\n')
        f.write('        if (!ItemsByName.ContainsKey(e.Name))\n')
        f.write('            ItemsByName[e.Name] = e;\n')
        f.write('    }\n\n')
        f.write('    public static int ResolveLooks(string itemName, int defaultLooks = 1)\n    {\n')
        f.write('        if (string.IsNullOrEmpty(itemName)) return defaultLooks;\n')
        f.write('        if (ItemsByName.TryGetValue(itemName, out var entry)) return entry.Looks;\n')
        f.write('        return defaultLooks;\n')
        f.write('    }\n')
        f.write('}\n')

    # 2. 写入 OfficialMonsterDBRegistry.cs
    with open('src/Mir2.Shared/Data/OfficialMonsterDBRegistry.cs', 'w', encoding='utf-8') as f:
        f.write('// ============================================================================\n')
        f.write('// 盛大官方 1.4/1.5 原版 Monster.DB 185 种全量怪物生态物理真值静态大字典\n')
        f.write('// 依据欧洲官方/盛大官方 mud2/DB/Monster.DB 二进制真值导出\n')
        f.write('// ============================================================================\n')
        f.write('using System;\nusing System.Collections.Generic;\n\nnamespace Mir2.Shared.Data;\n\n')
        f.write('public record MonsterDBEntry(string Name, int Race, int RaceImg, int Appr, int Level, int HP);\n\n')
        f.write('public static class OfficialMonsterDBRegistry\n{\n')
        f.write('    public static readonly Dictionary<string, MonsterDBEntry> MonstersByName = new(StringComparer.OrdinalIgnoreCase);\n\n')
        f.write('    static OfficialMonsterDBRegistry()\n    {\n')
        for m in monsters:
            s_name = m[0].replace('"', '')
            f.write(f'        Register(new MonsterDBEntry("{s_name}", {m[1]}, {m[2]}, {m[3]}, {m[4]}, {m[5]}));\n')
        f.write('    }\n\n')
        f.write('    private static void Register(MonsterDBEntry m)\n    {\n')
        f.write('        if (!MonstersByName.ContainsKey(m.Name))\n')
        f.write('            MonstersByName[m.Name] = m;\n')
        f.write('    }\n\n')
        f.write('    public static int ResolveAppr(string monsterName, int defaultAppr = 26)\n    {\n')
        f.write('        if (string.IsNullOrEmpty(monsterName)) return defaultAppr;\n')
        f.write('        if (MonstersByName.TryGetValue(monsterName, out var entry)) return entry.Appr;\n')
        f.write('        return defaultAppr;\n')
        f.write('    }\n')
        f.write('}\n')

    print(f'Exported {len(items)} items and {len(monsters)} monsters successfully!')

if __name__ == '__main__':
    export_all()
