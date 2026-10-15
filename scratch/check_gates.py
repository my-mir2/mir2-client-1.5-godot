import psycopg2

conn = psycopg2.connect('host=127.0.0.1 port=5432 user=postgres password=postgres dbname=mir2_world')
cur = conn.cursor()
cur.execute('select count(*) from official_map_gate')
print('Total gates:', cur.fetchone()[0])
cur.execute("select from_map, from_x, from_y, to_map, to_x, to_y from official_map_gate where from_map in ('0100', '0101', '0102', '0103') or to_map in ('0100', '0101', '0102', '0103')")
rows = cur.fetchall()
print(f'Interior gates count: {len(rows)}')
for r in rows:
    print(r)
