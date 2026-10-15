f=r'tools/Mir2.Bot/Program.cs'
t=open(f,encoding='utf-8-sig').read()
def R(a,b):
    global t
    assert a in t, a[:50]
    t=t.replace(a,b,1)
R('AttackGuards = Flag("--attack-guards"), Trace = Flag("--trace"),','AttackGuards = Flag("--attack-guards"), Trace = Flag("--trace"), JsonPath = arg.Contains("--json") ? Opt("--json", "") : null, Gm = arg.Contains("--gm") ? Opt("--gm", "") : null,')
R('public string? Hunt, GotoMap; public bool AttackGuards, Trace;','public string? Hunt, GotoMap, JsonPath, Gm; public bool AttackGuards, Trace;')
R('private readonly List<string> _deathLog = new(); private readonly List<string> _travelLog = new();','''private readonly List<string> _deathLog = new(); private readonly List<string> _travelLog = new();
        private readonly List<(string Map, int Ents, int Monsters, int ExpectX, int ExpectY, int ActualX, int ActualY, string Expect)> _arrivals = new();
        private readonly List<string> _gmLog = new();''')
R('await Until(() => _w.Ents.Count > 0, 12000);','await Until(() => _w.Ents.Count > 0, 12000); _arrivals.Add((_w.MapId, _w.Ents.Count, _w.Ents.Values.Count(e => e.Kind == Kind.Monster), gate.Tx, gate.Ty, _w.X, _w.Y, next));')
R('''        private void Report()
        {''','''        /// <summary>经聊天框发 GM 命令并记录服务端回显（"GM: ..." 系统消息）。</summary>
        private async Task GmSay(string cmd)
        {
            int before = _w.SysMsgs.Count;
            _c.Send(3030, "@gm " + cmd);
            await Until(() => _w.SysMsgs.Count > before, 3000);
            var reply = _w.SysMsgs.Skip(before).Select(m => m.Text).FirstOrDefault(x => x.StartsWith("GM:")) ?? "(无回显，GM 未启用或命令未被识别)";
            _gmLog.Add($"{cmd} => {reply}");
            Log($"GM> {cmd}  =>  {reply}");
            if (reply.StartsWith("(无回显") || reply.Contains("没有") || reply.Contains("失败") || reply.Contains("不存在") || reply.Contains("未知")) Problem($"GM 命令失败: {cmd} => {reply}");
            await Pump(400);
        }

        private void WriteJson()
        {
            if (string.IsNullOrEmpty(_cfg.JsonPath)) return;
            var o = new
            {
                kills = _totalKills, seconds = _sw.Elapsed.TotalSeconds, rollbacks = _w.Rollbacks, level = _w.Level, hp = _w.Hp, maxHp = _w.MaxHp,
                dead = _w.SelfDead, finalMap = _w.MapId, x = _w.X, y = _w.Y, gold = _w.Gold, exp = _w.Exp, maxExp = _w.MaxExp,
                problems = _problems, gm = _gmLog,
                levels = _levelLog.Select(l => new { l.Level, l.AtSec, l.Kills, l.MaxExp }),
                arrivals = _arrivals.Select(a => new { map = a.Map, ents = a.Ents, monsters = a.Monsters, expectX = a.ExpectX, expectY = a.ExpectY, x = a.ActualX, y = a.ActualY, expect = a.Expect }),
                deaths = _w.Deaths.Count(d => d.Uid == _w.SelfUid), revives = _w.Revives.Count(r => r.Uid == _w.SelfUid),
                struckBy = _w.Struck.GroupBy(s => s.Who).Select(g => new { who = g.Key, hits = g.Count(), dmg = g.Sum(s => s.Dmg) }),
                bag = _w.BagAdds.Select(b => b.Name),
                fights = _fights.Select(f => new { f.Map, f.Name, f.Killed, f.Swings, f.Misses, f.DamageTaken, f.Hits, drops = f.Drops.Select(d => d[..d.IndexOf('@')]) }),
            };
            File.WriteAllText(_cfg.JsonPath, System.Text.Json.JsonSerializer.Serialize(o, new System.Text.Json.JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping }));
        }

        private void Report()
        {
            WriteJson();''')
open(f,'w',encoding='utf-8',newline='').write(t)
