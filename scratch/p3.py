f=r'tools/Mir2.Bot/Program.cs'
t=open(f,encoding='utf-8-sig').read()
a='deaths = _w.Deaths.Count(d => d.Uid == _w.SelfUid), revives = _w.Revives.Count(r => r.Uid == _w.SelfUid),'
assert a in t
t=t.replace(a,'deaths = _deathLog.Count, deathPackets = _w.Deaths.Count(d => d.Uid == _w.SelfUid), revives = _deathLog.Count(d => d.Contains("复活于")),')
open(f,'w',encoding='utf-8',newline='').write(t)
