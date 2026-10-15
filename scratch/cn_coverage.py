import csv, io, os, re, subprocess, sys
CFG = r'D:\vbcoding\mir2\mir2-my\mir2-rust\assets\config'
sys.path.insert(0, os.path.dirname(os.path.abspath(__file__)))

def read_csv(name):
    with open(os.path.join(CFG, name), encoding='utf-8-sig', newline='') as f:
        return list(csv.DictReader(f))

alias = {r['en'].strip().lower(): r['cn'].strip() for r in read_csv('item_name_alias.csv')}
print('item_name_alias 条数:', len(alias))

# 读取已导入数据库的官方英文名
def q(sql):
    env = dict(os.environ, PGPASSWORD='postgres', PGCLIENTENCODING='UTF8')
    out = subprocess.run([r'D:\tools\pgsql_bin\pgsql\bin\psql.exe', '-h', '127.0.0.1', '-U', 'postgres', '-d', 'mir2_world', '-tAc', sql],
                         capture_output=True, text=True, encoding='utf-8', env=env).stdout
    return [l for l in out.splitlines() if l.strip()]

mons = q('select name from official_monster order by name')
magics = q('select magname from official_magic order by magid')
items = q('select name from official_stditems order by idx')
print('怪物', len(mons), '技能', len(magics), '物品', len(items))
mon_hit = [m for m in mons if m.lower() in alias]
item_hit = [i for i in items if i.lower() in alias]
print('别名表命中: 怪物', len(mon_hit), '物品', len(item_hit))
# 怪物去掉末尾数字变体后的基础名
base = lambda s: re.sub(r'\d+$', '', s)
print('怪物基础名(去数字后缀)种数:', len({base(m) for m in mons}))
print('MONSTERS:', ' '.join(sorted({base(m) for m in mons})))
print('MAGICS:', ' '.join(magics))
um = [i for i in items if i.lower() not in alias]
print('UNMAPPED_ITEMS(%d):' % len(um), ' | '.join(um))
