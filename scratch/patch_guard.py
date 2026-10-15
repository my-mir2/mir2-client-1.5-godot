with open('src/Mir2.Server/Game/MonsterRegenEngine.cs', 'rb') as f:
    raw = f.read()

text = raw.decode('utf-8')
crlf = '\r\n' in text
nl = '\r\n' if crlf else '\n'

p1 = '    public bool IsTownGuard => Race is 11 or 20 or 112;'
r1 = '    public bool IsTownGuard => Race is 11 or 20 or 80 or 81 or 112 || Name.Contains("守卫") || Name.Contains("卫士");'

p2 = nl.join([
    '                            else',
    '                            {',
    '                                targetPlayer = candidate;',
    '                            }'
])

r2 = nl.join([
    '                            else if (monster.IsTownGuard && !candidate.State.IsRedName)',
    '                            {',
    '                                monster.ThreatTable.RemoveTarget(primaryId);',
    '                            }',
    '                            else',
    '                            {',
    '                                targetPlayer = candidate;',
    '                            }'
])

if p1 not in text:
    print('p1 not found')
if p2 not in text:
    print('p2 not found')

text = text.replace(p1, r1, 1)
text = text.replace(p2, r2, 1)

with open('src/Mir2.Server/Game/MonsterRegenEngine.cs', 'wb') as f:
    f.write(text.encode('utf-8'))
print('DONE!')
