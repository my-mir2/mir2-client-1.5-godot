#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
欧洲官方 1.4 服务端 Paradox .DB (Monster/Magic/StdItems) -> PostgreSQL SQL 转换脚本。
用法:
    python convert_official_db_to_sql.py            # 生成 SQL 文件并打印各表结构与前 3 行样本
    python convert_official_db_to_sql.py --schema   # 仅打印结构 (不写文件)
输出: data/official_14/official_14_db.sql
"""
import os, re, struct, sys

SRC = r'D:\vbcoding\mir2\欧洲官方-14Server\korea\mud2\DB'
OUT_DIR = os.path.join(os.path.dirname(os.path.abspath(__file__)), '..', 'data', 'official_14')
TABLES = {'Monster.DB': 'official_monster', 'Magic.DB': 'official_magic', 'StdItems.DB': 'official_stditems'}
ENCODINGS = ('cp949', 'gbk', 'latin1')


def decode_text(raw):
    raw = raw.split(b'\x00', 1)[0]
    for enc in ENCODINGS:
        try:
            return raw.decode(enc)
        except UnicodeDecodeError:
            continue
    return raw.decode('latin1', 'replace')


def decode_int(b):
    n = len(b)
    if all(x == 0 for x in b):
        return None  # Paradox NULL
    v = int.from_bytes(b, 'big') ^ (1 << (8 * n - 1))
    if v >= 1 << (8 * n - 1):
        v -= 1 << (8 * n)
    return v


def decode_number(b):
    if all(x == 0 for x in b):
        return None
    bb = bytearray(b)
    if bb[0] & 0x80:
        bb[0] &= 0x7F
    else:
        bb = bytearray(x ^ 0xFF for x in bb)
    return struct.unpack('>d', bytes(bb))[0]


def parse_paradox(path):
    d = open(path, 'rb').read()
    rec_size, hdr_size, ftype, blk_k = struct.unpack_from('<HHBB', d, 0)
    num_rec = struct.unpack_from('<I', d, 6)[0]
    num_fields = struct.unpack_from('<H', d, 0x21)[0]
    off = 0x78
    fields = []
    for i in range(num_fields):
        t, sz = d[off + 2 * i], d[off + 2 * i + 1]
        fields.append([None, t, sz])
    off += 2 * num_fields + 4 + 4 * num_fields
    # 表名 (跳过) 与字段名: 在剩余头部里按 \0 切分，取最后 num_fields 个可打印串
    chunk = d[off:hdr_size]
    strs = [s for s in re.split(rb'\x00+', chunk) if s and all(32 <= c < 127 for c in s)]
    names = [s.decode('ascii') for s in strs[-(num_fields + 1):-1]]
    for f, n in zip(fields, names):
        f[0] = n
    blk = blk_k * 1024
    per_blk = (blk - 6) // rec_size
    rows = []
    pos = hdr_size
    while pos + blk <= len(d) and len(rows) < num_rec:
        add = struct.unpack_from('<h', d, pos + 4)[0]
        cnt = add // rec_size + 1 if add >= 0 else 0
        for r in range(min(cnt, per_blk)):
            ro = pos + 6 + r * rec_size
            row, p = [], ro
            for name, t, sz in fields:
                raw = d[p:p + sz]
                p += sz
                if t == 0x01:
                    row.append(decode_text(raw))
                elif t in (0x03, 0x04, 0x16):
                    row.append(decode_int(raw))
                elif t in (0x06, 0x05):
                    row.append(decode_number(raw))
                elif t == 0x09:
                    row.append(None if raw[0] == 0 else bool(raw[0] & 0x7F))
                else:
                    row.append(raw.hex())
            rows.append(row)
        pos += blk
    return fields, rows[:num_rec], num_rec


def sql_type(t, sz):
    return {0x01: f'VARCHAR({max(sz,1)})', 0x03: 'INTEGER', 0x04: 'BIGINT', 0x16: 'BIGINT',
            0x06: 'DOUBLE PRECISION', 0x05: 'DOUBLE PRECISION', 0x09: 'BOOLEAN'}.get(t, 'TEXT')


def q(v):
    if v is None:
        return 'NULL'
    if isinstance(v, bool):
        return 'TRUE' if v else 'FALSE'
    if isinstance(v, (int, float)):
        return repr(v)
    return "'" + str(v).replace("'", "''") + "'"


CN_CSV = os.path.join(OUT_DIR, 'cn_names.csv')
ITEM_ALIAS = r'D:\vbcoding\mir2\mir2-my\mir2-rust\assets\config\item_name_alias.csv'
NAME_FIELD = {'Monster.DB': 'name', 'Magic.DB': 'magname', 'StdItems.DB': 'name'}
KIND = {'Monster.DB': 'monster', 'Magic.DB': 'magic', 'StdItems.DB': 'item'}


def load_cn():
    """汉化来源优先级: cn_names.csv (curated) > item_name_alias.csv (alias_csv)。其余为 pending(留空，绝不编造)。"""
    import csv
    cn = {}
    if os.path.exists(ITEM_ALIAS):
        with open(ITEM_ALIAS, encoding='utf-8-sig', newline='') as f:
            for r in csv.DictReader(f):
                if r['en'].strip() and r['cn'].strip():
                    cn[('item', r['en'].strip().lower())] = (r['cn'].strip(), 'alias_csv')
    with open(CN_CSV, encoding='utf-8-sig', newline='') as f:
        for r in csv.DictReader(f):
            cn[(r['table'].strip(), r['en'].strip().lower())] = (r['cn'].strip(), r['source'].strip())
            cn[('exact', r['table'].strip(), r['en'].strip())] = (r['cn'].strip(), r['source'].strip())
    return cn


def cn_for(cn, kind, en):
    key = en.strip().lower()
    if ('exact', kind, en.strip()) in cn:
        return cn[('exact', kind, en.strip())]
    if (kind, key) in cn:
        return cn[(kind, key)]
    if kind == 'monster':  # Oma0 / Oma10 / Zombie30 等数字变体沿用基础名的译名
        b = re.sub(r'\d+$', '', key)
        if (kind, b) in cn:
            return cn[(kind, b)]
    return ('', 'pending')


def gen_monster_cs(fields, rows, names_cn):
    """生成 src/Mir2.Shared/Data/OfficialMonsterStats.g.cs (全量官方怪物属性，含汉化名，仅作数据库不可用时的兜底)。"""
    idx = {n.lower(): i for i, (n, _, _) in enumerate(fields)}
    g = lambda r, k: int(r[idx[k]] or 0)
    esc = lambda s: str(s).replace('\\', '\\\\').replace('"', '\\"')
    lines = [
        '// <auto-generated> 由 tools/convert_official_db_to_sql.py 从 mud2/DB/Monster.DB 生成，请勿手改 </auto-generated>',
        'using System;', 'using System.Collections.Generic;', '',
        'namespace Mir2.Shared.Data;', '',
        '/// <summary>官方 Monster.DB 全量怪物战斗属性 (Speed=敏捷, Hit=准确, 速度类字段单位均为毫秒; NameCn 为汉化名，空串表示待汉化)。</summary>',
        'public sealed record OfficialMonsterStats(string Name, int Race, int RaceImg, int Appr, int Level, int Undead, int CoolEye, int Exp,',
        '    int Hp, int Mp, int Ac, int Mac, int Dc, int DcMax, int Mc, int Sc, int Speed, int Hit, int WalkSpeedMs, int WalkStep, int WalkWait, int AttackSpeedMs, string NameCn = "");', '',
        'public static partial class OfficialMonsterStatsTable', '{',
        '    public static readonly IReadOnlyDictionary<string, OfficialMonsterStats> ByName = new Dictionary<string, OfficialMonsterStats>(StringComparer.OrdinalIgnoreCase)', '    {']
    for r, cnv in zip(rows, names_cn):
        nm = esc(r[idx['name']])
        v = [g(r, k) for k in ('race', 'raceimg', 'appr', 'lvl', 'undead', 'coolEye'.lower(), 'exp', 'hp', 'mp', 'ac', 'mac', 'dc', 'dcmax', 'mc', 'sc', 'speed', 'hit', 'walk_spd', 'walkstep', 'walkwait', 'attack_spd')]
        lines.append(f'        ["{nm}"] = new OfficialMonsterStats("{nm}", ' + ', '.join(map(str, v)) + f', "{esc(cnv)}"),')
    lines += ['    };', '}', '']
    p = os.path.normpath(os.path.join(os.path.dirname(os.path.abspath(__file__)), '..', 'src', 'Mir2.Shared', 'Data', 'OfficialMonsterStats.g.cs'))
    open(p, 'w', encoding='utf-8').write('\n'.join(lines))
    print('已写出', p, f'({len(rows)} 条)')


def main():
    schema_only = '--schema' in sys.argv
    cn = load_cn()
    out = ['-- 欧洲官方 1.4 服务端数据 + 汉化 (由 tools/convert_official_db_to_sql.py 自动生成，勿手改)',
           '-- name_cn: 汉化名; cn_source: curated(整理译名,待审阅) / alias_csv(来自 item_name_alias.csv) / pending(待汉化,留空)', 'BEGIN;']
    for fn, tbl in TABLES.items():
        fields, rows, num_rec = parse_paradox(os.path.join(SRC, fn))
        cols = [re.sub(r'\W', '_', n).lower() for n, _, _ in fields]
        ni = cols.index(NAME_FIELD[fn])
        translated = [cn_for(cn, KIND[fn], str(r[ni])) for r in rows]
        if fn == 'Monster.DB' and not schema_only:
            gen_monster_cs(fields, rows, [t[0] for t in translated])
        done = sum(1 for t in translated if t[0])
        print(f'== {fn} -> {tbl}: 记录数={len(rows)}(声明{num_rec}) 字段数={len(fields)} 已汉化={done} 待汉化={len(rows) - done}')
        out.append(f'DROP TABLE IF EXISTS {tbl};')
        out.append(f'CREATE TABLE {tbl} (' + ', '.join(f'"{c}" {sql_type(t, s)}' for c, (_, t, s) in zip(cols, fields))
                   + ', "name_cn" VARCHAR(64) NOT NULL DEFAULT \'\', "cn_source" VARCHAR(16) NOT NULL DEFAULT \'pending\');')
        allcols = ', '.join(f'"{c}"' for c in cols) + ', "name_cn", "cn_source"'
        for r, (cnv, src) in zip(rows, translated):
            out.append(f'INSERT INTO {tbl} ({allcols}) VALUES (' + ', '.join(q(v) for v in r) + f', {q(cnv)}, {q(src)});')
    out.append('COMMIT;')
    if not schema_only:
        os.makedirs(OUT_DIR, exist_ok=True)
        p = os.path.join(OUT_DIR, 'official_14_db.sql')
        open(p, 'w', encoding='utf-8').write('\n'.join(out) + '\n')
        print('已写出', os.path.abspath(p))


if __name__ == '__main__':
    main()
