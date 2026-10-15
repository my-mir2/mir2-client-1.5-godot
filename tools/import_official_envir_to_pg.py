#!/usr/bin/env python
# -*- coding: utf-8 -*-
"""
欧洲官方 1.4 服务端散落文件 (Mir200/Envir, Mir200/Map) -> PostgreSQL 全量导入。
服务端启动时只读数据库，不再读取任何散落文件。脚本幂等 (DROP + CREATE + INSERT)。

用法:
    python tools/import_official_envir_to_pg.py            # 导入全部
    python tools/import_official_envir_to_pg.py --no-maps  # 跳过 120MB 地图二进制 (调试用)
环境变量 PG_DSN 可覆盖连接串。
"""
import os, re, struct, sys
import psycopg2
from psycopg2.extras import execute_values

ROOT = r'D:\vbcoding\mir2\欧洲官方-14Server\korea\Mir200'
ENVIR = os.path.join(ROOT, 'Envir')
MAPDIR = os.path.join(ROOT, 'Map')
DSN = os.environ.get('PG_DSN', 'host=127.0.0.1 port=5432 user=postgres password=postgres dbname=mir2_world')

MAP_HEADER_SIZE = 52   # 官方 .map: 52 字节头 + width*height*12 字节单元
MAP_CELL_SIZE = 12


def read_text(path, encodings=('utf-8', 'cp949', 'latin1')):
    raw = open(path, 'rb').read().replace(b'\x00', b'')
    for enc in encodings:
        try:
            return raw.decode(enc)
        except UnicodeDecodeError:
            continue
    return raw.decode('latin1', 'replace')


def lines_of(path, encodings=('utf-8', 'cp949', 'latin1')):
    return read_text(path, encodings).replace('\r\r\n', '\n').replace('\r\n', '\n').replace('\r', '\n').split('\n')


DDL = {
    'official_map': """CREATE TABLE official_map (map_id VARCHAR(16) PRIMARY KEY, map_num INT NOT NULL UNIQUE,
        width INT NOT NULL, height INT NOT NULL, header BYTEA NOT NULL, cells BYTEA NOT NULL)""",
    'official_map_info': """CREATE TABLE official_map_info (map_id VARCHAR(16) PRIMARY KEY, map_name VARCHAR(64) NOT NULL,
        arg VARCHAR(32) NOT NULL, flags TEXT NOT NULL)""",
    'official_map_gate': """CREATE TABLE official_map_gate (id SERIAL PRIMARY KEY, from_map VARCHAR(16) NOT NULL, from_x INT NOT NULL,
        from_y INT NOT NULL, to_map VARCHAR(16) NOT NULL, to_x INT NOT NULL, to_y INT NOT NULL)""",
    'official_mongen': """CREATE TABLE official_mongen (id SERIAL PRIMARY KEY, area VARCHAR(16) NOT NULL, x INT NOT NULL, y INT NOT NULL,
        monster_name VARCHAR(32) NOT NULL, scope INT NOT NULL, mon_count INT NOT NULL, interval_min INT NOT NULL,
        probability INT)""",
    'official_mon_drop': """CREATE TABLE official_mon_drop (id SERIAL PRIMARY KEY, variant VARCHAR(16) NOT NULL, monster_key VARCHAR(48) NOT NULL,
        seq INT NOT NULL, prob_num INT NOT NULL, prob_den INT NOT NULL, item_name VARCHAR(48) NOT NULL, item_count INT)""",
    'official_npc_placement': """CREATE TABLE official_npc_placement (id SERIAL PRIMARY KEY, script_name VARCHAR(48) NOT NULL, map_id VARCHAR(16) NOT NULL,
        x INT NOT NULL, y INT NOT NULL, npc_name VARCHAR(48) NOT NULL, flag1 INT, appr INT, flag2 INT)""",
    'official_start_point': """CREATE TABLE official_start_point (id SERIAL PRIMARY KEY, map_id VARCHAR(16) NOT NULL, x INT NOT NULL, y INT NOT NULL)""",
    'official_minimap': """CREATE TABLE official_minimap (map_id VARCHAR(16) PRIMARY KEY, minimap_id INT NOT NULL)""",
    'official_guard': """CREATE TABLE official_guard (id SERIAL PRIMARY KEY, guard_name VARCHAR(48) NOT NULL, map_id VARCHAR(16) NOT NULL,
        x INT NOT NULL, y INT NOT NULL, dir INT NOT NULL)""",
    'official_script': """CREATE TABLE official_script (category VARCHAR(24) NOT NULL, relpath VARCHAR(200) NOT NULL, script_name VARCHAR(100) NOT NULL,
        content TEXT NOT NULL, PRIMARY KEY (category, relpath))""",
    'official_server_setting': """CREATE TABLE official_server_setting (id SERIAL PRIMARY KEY, file_name VARCHAR(32) NOT NULL, section VARCHAR(32) NOT NULL,
        key VARCHAR(64) NOT NULL, value TEXT NOT NULL)""",
    'official_envir_raw': """CREATE TABLE official_envir_raw (id SERIAL PRIMARY KEY, file_name VARCHAR(32) NOT NULL, line_no INT NOT NULL, text TEXT NOT NULL)""",
}


class Report:
    def __init__(self):
        self.counts, self.skipped = {}, {}

    def add(self, table, n): self.counts[table] = n

    def skip(self, what, line):
        self.skipped.setdefault(what, []).append(line)


def parse_map_info(rep):
    infos, gates = [], []
    for ln in lines_of(os.path.join(ENVIR, 'MapInfo.txt')):
        s = ln.strip()
        if not s or s.startswith(';'):
            continue
        m = re.match(r'^\[([^\]]+)\]\s*(.*)$', s)
        if m:
            tok = m.group(1).split()
            if len(tok) == 2:                      # [D024  WTkingsroom] 没有 arg
                mid, name, arg = tok[0], tok[1], ''
            else:                                  # [D701 secret passage 0] 名称内含空格
                mid, name, arg = tok[0], ' '.join(tok[1:-1]), tok[-1]
            infos.append((mid, name, arg, m.group(2).strip()))
            continue
        g = re.match(r'^(\S+)\s+(\d+)\s*,\s*(\d+)\s*->\s*(\S+)\s+(\d+)\s*,\s*(\d+)', s)
        if g:
            gates.append((g.group(1), int(g.group(2)), int(g.group(3)), g.group(4), int(g.group(5)), int(g.group(6))))
            continue
        rep.skip('MapInfo', s)
    return infos, gates


def parse_mongen(rep):
    rows = []
    for ln in lines_of(os.path.join(ENVIR, 'Mongen.mst')):
        s = ln.strip()
        if not s or s.startswith(';'):
            continue
        t = s.split()
        if len(t) >= 7 and all(re.fullmatch(r'-?\d+', t[i]) for i in (1, 2, 4, 5, 6)):
            prob = int(t[7]) if len(t) >= 8 and re.fullmatch(r'-?\d+', t[7]) else None
            rows.append((t[0], int(t[1]), int(t[2]), t[3], int(t[4]), int(t[5]), int(t[6]), prob))
        else:
            rep.skip('Mongen.mst', s)
    return rows


def parse_mon_items(rep):
    rows = []
    base = os.path.join(ENVIR, 'MonItems')
    for variant, d in (('default', base), ('chinese', os.path.join(base, 'chinese')), ('english', os.path.join(base, 'english'))):
        if not os.path.isdir(d):
            continue
        for fn in sorted(os.listdir(d)):
            p = os.path.join(d, fn)
            if not os.path.isfile(p) or not fn.lower().endswith('.txt'):
                continue
            key = fn[:-4].lower()
            seq = 0
            encs = ('gbk', 'latin1')   # 根目录 default 变体含国服 GBK 中文物品名
            for ln in lines_of(p, encs):
                s = ln.strip()
                if not s or s.startswith(';'):
                    continue
                m = re.match(r'^(\d+)\s*/\s*(\d+)\s+(\S+)(?:\s+(\d+))?', s)
                if not m:
                    rep.skip('MonItems', f'{variant}/{fn}: {s}')
                    continue
                seq += 1
                rows.append((variant, key, seq, int(m.group(1)), int(m.group(2)), m.group(3), int(m.group(4)) if m.group(4) else None))
    return rows


def parse_merchant(rep):
    rows = []
    for ln in lines_of(os.path.join(ENVIR, 'merchant.txt')):
        s = ln.strip()
        if not s or s.startswith(';'):
            continue
        t = s.split()
        if len(t) >= 5 and re.fullmatch(r'\d+', t[2]) and re.fullmatch(r'\d+', t[3]):
            def opt(i): return int(t[i]) if len(t) > i and re.fullmatch(r'-?\d+', t[i]) else None
            rows.append((t[0], t[1], int(t[2]), int(t[3]), t[4], opt(5), opt(6), opt(7)))
        else:
            rep.skip('merchant.txt', s)
    return rows


def parse_simple(fname, pattern, conv, rep):
    rows = []
    for ln in lines_of(os.path.join(ENVIR, fname)):
        s = ln.strip()
        if not s or s.startswith(';'):
            continue
        m = re.match(pattern, s)
        if m:
            rows.append(conv(m))
        else:
            rep.skip(fname, s)
    return rows


def parse_scripts():
    rows = []
    for cat, sub in (('market_def', 'market_def'), ('Npc_def', 'Npc_def'), ('MapQuest_def', 'MapQuest_def'),
                     ('Castle', 'Castle'), ('Defines', 'Defines'), ('QuestDiary', 'QuestDiary'), ('market_upg', 'market_upg')):
        base = os.path.join(ENVIR, sub)
        if not os.path.isdir(base):
            continue
        for dp, _, fs in os.walk(base):
            for fn in sorted(fs):
                p = os.path.join(dp, fn)
                rel = os.path.relpath(p, base).replace('\\', '/')
                rows.append((cat, rel, os.path.splitext(fn)[0], read_text(p)))
    return rows


def parse_settings():
    rows = []
    for fn in ('!setup.txt', '!servertable.txt', '!abuse.txt', '!runaddr.txt'):
        p = os.path.join(ROOT, fn)
        if not os.path.exists(p):
            continue
        sec = ''
        for ln in lines_of(p):
            s = ln.strip()
            if not s or s.startswith(';'):
                continue
            m = re.match(r'^\[(.+)\]$', s)
            if m:
                sec = m.group(1)
            elif '=' in s:
                k, v = s.split('=', 1)
                rows.append((fn, sec, k.strip(), v.strip()))
            else:
                rows.append((fn, sec, '', s))
    return rows


def parse_raw(fnames):
    rows = []
    for fn in fnames:
        p = os.path.join(ENVIR, fn)
        for i, ln in enumerate(lines_of(p), 1):
            if ln.strip() and not ln.strip().startswith(';'):
                rows.append((fn, i, ln.rstrip()))
    return rows


def assign_map_nums(ids):
    """纯数字地图号直接取其整数值 (官方 '0100' 与 '100' 无冲突，已核验)；
    非数字地图 (D001/T118/kt0000...) 从 10000 起按名称排序顺延，结果写入数据库。"""
    nums, used = {}, set()
    for i in ids:
        if re.fullmatch(r'\d+', i):
            nums[i] = int(i)
            assert nums[i] not in used, f'数字地图号冲突: {i}'
            used.add(nums[i])
    nxt = 10000
    for i in sorted(x for x in ids if x not in nums):
        nums[i] = nxt
        nxt += 1
    return nums


def import_maps(cur, conn):
    n = 0
    files = sorted(f for f in os.listdir(MAPDIR) if f.lower().endswith('.map'))
    nums = assign_map_nums([f[:-4] for f in files])
    for fn in files:
        d = open(os.path.join(MAPDIR, fn), 'rb').read()
        w, h = struct.unpack_from('<HH', d, 0)
        if len(d) != MAP_HEADER_SIZE + w * h * MAP_CELL_SIZE:
            print(f'  [警告] 地图 {fn} 尺寸 {w}x{h} 与文件长度 {len(d)} 不吻合，仍按原样入库')
        cur.execute('INSERT INTO official_map (map_id,map_num,width,height,header,cells) VALUES (%s,%s,%s,%s,%s,%s)',
                    (fn[:-4], nums[fn[:-4]], w, h, psycopg2.Binary(d[:MAP_HEADER_SIZE]), psycopg2.Binary(d[MAP_HEADER_SIZE:])))
        n += 1
        if n % 40 == 0:
            conn.commit()
    return n


def bulk(cur, table, cols, rows):
    if rows:
        execute_values(cur, f'INSERT INTO {table} ({cols}) VALUES %s', rows, page_size=2000)


def main():
    with_maps = '--no-maps' not in sys.argv
    rep = Report()
    infos, gates = parse_map_info(rep)
    mongen = parse_mongen(rep)
    drops = parse_mon_items(rep)
    merchants = parse_merchant(rep)
    starts = parse_simple('StartPoint.txt', r'^(\S+)\s+(\d+)\s+(\d+)', lambda m: (m.group(1), int(m.group(2)), int(m.group(3))), rep)
    minimaps = parse_simple('MiniMap.txt', r'^(\S+)\s+(\d+)', lambda m: (m.group(1), int(m.group(2))), rep)
    guards = parse_simple('GuardList.txt', r'^(\S+)\s+(\S+)\s+(\d+)\s*,\s*(\d+)\s*:\s*(\d+)',
                          lambda m: (m.group(1), m.group(2), int(m.group(3)), int(m.group(4)), int(m.group(5))), rep)
    scripts = parse_scripts()
    settings = parse_settings()
    raws = parse_raw(['Npcs.txt', 'MakeItem.txt', 'MapQuest.txt', 'UnbindList.txt', 'AdminList.txt'])

    conn = psycopg2.connect(DSN)
    cur = conn.cursor()
    for t in DDL:
        cur.execute(f'DROP TABLE IF EXISTS {t}')
        cur.execute(DDL[t])
    bulk(cur, 'official_map_info', 'map_id,map_name,arg,flags', [(a, b, c, d) for a, b, c, d in {i[0]: i for i in infos}.values()])
    bulk(cur, 'official_map_gate', 'from_map,from_x,from_y,to_map,to_x,to_y', gates)
    bulk(cur, 'official_mongen', 'area,x,y,monster_name,scope,mon_count,interval_min,probability', mongen)
    bulk(cur, 'official_mon_drop', 'variant,monster_key,seq,prob_num,prob_den,item_name,item_count', drops)
    bulk(cur, 'official_npc_placement', 'script_name,map_id,x,y,npc_name,flag1,appr,flag2', merchants)
    bulk(cur, 'official_start_point', 'map_id,x,y', starts)
    bulk(cur, 'official_minimap', 'map_id,minimap_id', list({m[0]: m for m in minimaps}.values()))
    bulk(cur, 'official_guard', 'guard_name,map_id,x,y,dir', guards)
    bulk(cur, 'official_script', 'category,relpath,script_name,content', scripts)
    bulk(cur, 'official_server_setting', 'file_name,section,key,value', settings)
    bulk(cur, 'official_envir_raw', 'file_name,line_no,text', raws)
    for ddl in ('CREATE INDEX ix_mongen_area ON official_mongen(area)',
                'CREATE INDEX ix_mon_drop_key ON official_mon_drop(variant, monster_key)',
                'CREATE INDEX ix_npc_map ON official_npc_placement(map_id)',
                'CREATE INDEX ix_gate_from ON official_map_gate(from_map)'):
        cur.execute(ddl)
    conn.commit()
    if with_maps:
        import_maps(cur, conn)
        conn.commit()

    print('=== 入库统计 ===')
    for t in DDL:
        cur.execute(f'SELECT COUNT(*) FROM {t}')
        print(f'  {t:26s} {cur.fetchone()[0]:>7d}')
    print('=== 未能解析而跳过的行 (如实披露) ===')
    if not rep.skipped:
        print('  无')
    for k, v in rep.skipped.items():
        print(f'  {k}: {len(v)} 行，样例: {v[:3]}')
    cur.close(); conn.close()


if __name__ == '__main__':
    main()

