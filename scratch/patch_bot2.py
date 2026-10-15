f=r'tools/Mir2.Bot/Program.cs'
t=open(f,encoding='utf-8-sig').read()
a='            _lvSeen = _w.LevelUps.Count;\n'
if a not in t: a=a.replace('\n','\r\n')
assert a in t
b=a+'''
            if (!string.IsNullOrWhiteSpace(_cfg.Gm))
            {
                Log("=== 阶段 G: GM 造场景 ===");
                foreach (var gc in _cfg.Gm.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)) await GmSay(gc);
                await Pump(1500); LoadMapGrid();
                _levelLog.Add((_w.Level, _sw.Elapsed.TotalSeconds, 0, _w.MaxExp)); _lvSeen = _w.LevelUps.Count;
            }
'''
t=t.replace(a,b,1)
open(f,'w',encoding='utf-8',newline='').write(t)
