import os, re, subprocess
def q(sql):
    env = dict(os.environ, PGPASSWORD='postgres', PGCLIENTENCODING='UTF8')
    out = subprocess.run([r'D:\tools\pgsql_bin\pgsql\bin\psql.exe', '-h', '127.0.0.1', '-U', 'postgres', '-d', 'mir2_world', '-tAF', '|', '-c', sql],
                         capture_output=True, text=True, encoding='utf-8', env=env).stdout
    return [l for l in out.splitlines() if l.strip()]
mons = q("select name,lvl,hp,appr from official_monster where cn_source='pending' order by lvl,name")
seen = {}
for l in mons:
    n, lvl, hp, appr = l.split('|')
    b = re.sub(r'\d+$', '', n)
    seen.setdefault(b, (lvl, hp, appr))
print('PENDING_MONSTER_BASES(%d): ' % len(seen) + '; '.join(f'{b}[L{v[0]} HP{v[1]} A{v[2]}]' for b, v in seen.items()))
items = q("select name,stdmode,shape from official_stditems where cn_source='pending' order by idx")
print('PENDING_ITEMS(%d): ' % len(items) + '; '.join(f"{i.split('|')[0]}[m{i.split('|')[1]}]" for i in items))
