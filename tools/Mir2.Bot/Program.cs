using System.Diagnostics;
using Mir2.Bot;
using Npgsql;

// 无头玩家机器人: 真实 TCP -> 登录 -> 建角色 -> 进图 -> 走/练级/换图 -> 打怪 -> 捡东西 -> 死了怎么办。
// 一切结论只来自服务端下发的包 (换图用的传送门坐标例外: 它从数据库读，相当于玩家看地图/攻略)。
//   dotnet run --project tools/Mir2.Bot -- [--host 127.0.0.1] [--port 7000] [--seconds 900] [--trace]
//       [--root <仓库根>] [--pg <连接串>]
//       [--target-level N]    先在出生地练到 N 级 (配合服务端 MIR2_EXP_RATE 加速)
//       [--goto <官方地图号>]  练完后走传送门到该地图 (如 1 / 2 / 3 / 11 / D001)
//       [--kills N]            最后一阶段在当前地图打 N 只怪 (默认 8)
//       [--hunt <怪物名子串>]  最后一阶段只打名字含该子串的怪；[--attack-guards] 允许打守卫
var arg = args.ToList();
string Opt(string k, string d) { int i = arg.IndexOf(k); return i >= 0 && i + 1 < arg.Count ? arg[i + 1] : d; }
bool Flag(string k) => arg.Contains(k);

var cfg = new BotConfig
{
    Host = Opt("--host", "127.0.0.1"), Port = int.Parse(Opt("--port", "7000")),
    Kills = int.Parse(Opt("--kills", "8")), Seconds = int.Parse(Opt("--seconds", "900")),
    Root = Opt("--root", Directory.GetCurrentDirectory()),
    Pg = Opt("--pg", "Host=127.0.0.1;Port=5432;Database=mir2_world;Username=postgres;Password=postgres"),
    Hunt = arg.Contains("--hunt") ? Opt("--hunt", "") : null,
    AttackGuards = Flag("--attack-guards"), Trace = Flag("--trace"), JsonPath = arg.Contains("--json") ? Opt("--json", "") : null, Gm = arg.Contains("--gm") ? Opt("--gm", "") : null,
    TargetLevel = int.Parse(Opt("--target-level", "0")),
    GotoMap = arg.Contains("--goto") ? Opt("--goto", "") : null,
    BuyPotion = arg.Contains("--buy-potion") ? Opt("--buy-potion", "") : null,
    Spell = arg.Contains("--spell") ? Opt("--spell", "") : null,
    TestEquip = Flag("--test-equip"),
    TaoistCombat = Flag("--taoist-combat"),
    PartyRole = arg.Contains("--party-role") ? Opt("--party-role", "") : null,
    CharName = arg.Contains("--char-name") ? Opt("--char-name", "") : null,
    InviteName = arg.Contains("--invite-name") ? Opt("--invite-name", "") : null,
    TestKeybind = Flag("--test-keybind"),
    TestAdjustBonus = Flag("--test-adjust-bonus"),
    TestDropGold = Flag("--test-drop-gold"),
    TestButch = Flag("--test-butch"),
    TestSell = Flag("--test-sell"),
    TestRepair = Flag("--test-repair"),
    TestSwordSwitch = Flag("--test-sword-switch"),
    NoRest = Flag("--no-rest"),
};
return await new Bot(cfg).RunAsync();

namespace Mir2.Bot
{
    public sealed class BotConfig
    {
        public string Host = "", Root = "", Pg = ""; public int Port, Kills, Seconds, TargetLevel;
        public string? Hunt, GotoMap, JsonPath, Gm, BuyPotion, Spell, PartyRole, CharName, InviteName; public bool AttackGuards, Trace, TestEquip, TaoistCombat, TestKeybind, TestAdjustBonus, TestDropGold, TestButch, TestSell, TestRepair, TestSwordSwitch, NoRest;
    }

    public sealed class FightLog
    {
        public string Map = "", Name = ""; public int Uid, Swings, Spells, Misses, DamageTaken; public long StartMs, EndMs; public bool Killed;
        public List<int> Hits = new(); public List<string> Drops = new(); public string Note = "";
    }

    public sealed record Gate(string From, int Fx, int Fy, string To, int Tx, int Ty);

    public sealed class Bot
    {
        private readonly BotConfig _cfg;
        private readonly Conn _c = new(); private readonly World _w = new(); private MapGrid? _map;
        private readonly Stopwatch _sw = Stopwatch.StartNew();
        private readonly List<FightLog> _fights = new(); private readonly List<string> _problems = new(); private readonly List<string> _facts = new();
        private readonly List<(int Level, double AtSec, int Kills, long MaxExp)> _levelLog = new();
        private readonly List<string> _deathLog = new(); private readonly List<string> _travelLog = new();
        private readonly List<(string Map, int Ents, int Monsters, int ExpectX, int ExpectY, int ActualX, int ActualY, string Expect)> _arrivals = new();
        private readonly List<string> _gmLog = new();
        private readonly HashSet<string> _avoid = new();
        public string CharacterName { get; private set; } = "";
        private double _restSec; private bool _bagFull; private int _bagFullSkips; private int _totalKills; private int _lvSeen;

        public Bot(BotConfig cfg) { _cfg = cfg; }

        private void Log(string s) => Console.WriteLine($"[{_sw.Elapsed.TotalSeconds,6:0.0}s] {s}");
        private void Problem(string s) { _problems.Add(s); Log("!! " + s); }
        private TimeSpan Deadline => TimeSpan.FromSeconds(_cfg.Seconds);

        private async Task<bool> Until(Func<bool> cond, int ms)
        {
            var t = Stopwatch.StartNew();
            while (t.ElapsedMilliseconds < ms) { _w.Apply(_c); if (cond()) return true; await Task.Delay(15); }
            _w.Apply(_c); return cond();
        }
        private async Task Pump(int ms) { await Until(() => false, ms); }

        // ============================================================ 主流程
        public async Task<int> RunAsync()
        {
            Log($"连接 {_cfg.Host}:{_cfg.Port} ...");
            try { await _c.ConnectAsync(_cfg.Host, _cfg.Port); } catch (Exception e) { Log("连接失败: " + e.Message); return 2; }
            if (!await EnterGame()) { DumpPackets(); return 3; }

            _levelLog.Add((_w.Level, _sw.Elapsed.TotalSeconds, 0, _w.MaxExp));
            _lvSeen = _w.LevelUps.Count;

            if (!string.IsNullOrWhiteSpace(_cfg.Gm))
            {
                Log("=== 阶段 G: GM 造场景 ===");
                foreach (var gc in _cfg.Gm.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)) await GmSay(gc);
                await Pump(1500); LoadMapGrid();
                _levelLog.Add((_w.Level, _sw.Elapsed.TotalSeconds, 0, _w.MaxExp)); _lvSeen = _w.LevelUps.Count;
            }

            if (_cfg.TargetLevel > 0)
            {
                Log($"=== 阶段 A: 在 {_w.MapName} 练级到 {_cfg.TargetLevel} 级 ===");
                await Hunt(() => _w.Level >= _cfg.TargetLevel, null);
            }
            if (_cfg.GotoMap != null && !_w.SelfDead && !_c.Closed)
            {
                Log($"=== 阶段 B: 走传送门前往地图 {_cfg.GotoMap} (当前 {_w.MapId}, Lv{_w.Level}) ===");
                await TravelTo(_cfg.GotoMap);
            }
            if (!string.IsNullOrWhiteSpace(_cfg.PartyRole))
            {
                await PartyFlow(_cfg.PartyRole);
            }

            if ((_cfg.TargetLevel == 0 || _cfg.GotoMap != null) && string.IsNullOrWhiteSpace(_cfg.PartyRole) && !_w.SelfDead && !_c.Closed && _sw.Elapsed < Deadline)
            {
                int target = _totalKills + _cfg.Kills;
                Log($"=== 阶段 C: 在 {_w.MapId}:{_w.MapName} 打 {_cfg.Kills} 只{(_cfg.Hunt != null ? $"「{_cfg.Hunt}」" : "怪")} ===");
                await Hunt(() => _totalKills >= target, _cfg.Hunt);
            }

            if (!string.IsNullOrWhiteSpace(_cfg.BuyPotion))
            {
                Log($"=== 阶段 P: 前往药店购买并使用 {_cfg.BuyPotion} ===");
                await BuyAndDrinkPotionFlow(_cfg.BuyPotion);
            }

            if (_cfg.TestEquip)
            {
                await EquipWearAndDropFlow();
            }

            if (_cfg.TaoistCombat)
            {
                await TaoistSummonAndCastFlow();
            }

            if (_cfg.TestKeybind)
            {
                await KeybindFlow();
            }

            if (_cfg.TestAdjustBonus)
            {
                await AdjustBonusFlow();
            }

            if (_cfg.TestDropGold)
            {
                await DropGoldFlow();
            }

            if (_cfg.TestButch)
            {
                await ButchFlow();
            }

            if (_cfg.TestSell)
            {
                await SellItemFlow();
            }

            if (_cfg.TestRepair)
            {
                await RepairItemFlow();
            }

            if (_cfg.TestSwordSwitch)
            {
                await SwordSwitchFlow();
            }

            if (_w.SelfDead) await HandleDeath();
            if (_c.Closed) Problem("连接被服务端断开");
            Report();
            if (_cfg.Trace) DumpPackets();
            _c.Dispose();
            bool ok = _problems.Count == 0 && (_totalKills > 0 || !string.IsNullOrWhiteSpace(_cfg.BuyPotion) || !string.IsNullOrWhiteSpace(_cfg.Gm) || _cfg.TestEquip || _cfg.TaoistCombat || !string.IsNullOrWhiteSpace(_cfg.PartyRole) || _cfg.TestKeybind || _cfg.TestAdjustBonus || _cfg.TestDropGold || _cfg.TestButch || _cfg.TestSell || _cfg.TestRepair || _cfg.TestSwordSwitch);
            return ok ? 0 : 1;
        }

        private async Task<bool> EnterGame()
        {
            string name = !string.IsNullOrWhiteSpace(_cfg.CharName) ? _cfg.CharName : "机器人" + Random.Shared.Next(1000, 9999);
            string acct = "bot" + (name.GetHashCode() & 0x7FFFFFFF);
            string pass = "botpass1";
            _c.Send(2001, "0/0/0/0/0/");
            if (!await Until(() => _w.PacketCounts.ContainsKey(500), 3000)) { Problem("版本握手无应答 (SM_PASSOK 500)"); return false; }
            _c.Send(2, $"{acct}/{pass}/");
            if (!await Until(() => _w.PacketCounts.ContainsKey(530), 3000)) { Problem("登录无应答 (SM_LOGINSUCCESS 530)"); return false; }
            Log($"登录成功 账号={acct}");
            _c.Send(119); await Until(() => _w.PacketCounts.ContainsKey(523), 2000);
            CharacterName = name;
            int job = _cfg.TaoistCombat ? 2 : 0;
            _c.Send(101, $"{name}\t{job}\t0"); await Pump(800); // 0=战士, 1=法师, 2=道士, 男
            _c.Send(103, name);
            if (!await Until(() => _w.PacketCounts.ContainsKey(521) && _w.SelfUid != 0, 5000)) { Problem("选角后未收到进图包 (521) 与主角绑定 (50)"); return false; }
            Log($"进入游戏 角色={name} uid={_w.SelfUid} 地图={_w.MapId}:{_w.MapName} 出生=({_w.X},{_w.Y})");
            await Pump(2500);
            LoadMapGrid();
            var by = _w.Ents.Values.GroupBy(e => (e.Kind, e.Name)).OrderByDescending(g => g.Count()).Take(10);
            Log($"视野内 {_w.Ents.Count} 个实体: " + string.Join(", ", by.Select(g => $"{g.Key.Kind}:{g.Key.Name}x{g.Count()}")));
            Log($"我的状态 Lv{_w.Level} HP {_w.Hp}/{_w.MaxHp} MP {_w.Mp}/{_w.MaxMp} 金币 {_w.Gold}");
            return true;
        }

        private void LoadMapGrid()
        {
            _map = MapGrid.Load(Path.Combine(_cfg.Root, "resources", "maps", "Map", _w.MapId + ".map"));
            if (_map == null) Problem($"找不到地图文件 {_w.MapId}.map，无法寻路");
        }

        // ============================================================ 狩猎
        private async Task Hunt(Func<bool> done, string? hunt)
        {
            var skipped = new HashSet<int>();
            while (!done() && _sw.Elapsed < Deadline && !_c.Closed && _map != null)
            {
                if (_w.SelfDead) { await HandleDeath(); if (_w.SelfDead) break; LoadMapGrid(); continue; }
                await RestIfHurt();
                var t = PickTarget(hunt, skipped);
                if (t == null)
                {
                    // 视野里没怪：按数据库刷怪点向最近的怪区走（每个刷怪点只去一次）
                    if (_seeks < 12 && await SeekSpawn(hunt)) { _seeks++; continue; }
                    Problem($"视野内没有可打的怪物，也找不到可前往的刷怪点 (地图 {_w.MapId} 位置 {_w.X},{_w.Y})"); break;
                }
                var f = await Fight(t);
                _fights.Add(f);
                if (f.Killed) _totalKills++; else skipped.Add(t.Uid);
                if (_w.MaxHp > 0 && f.DamageTaken * 100 > _w.MaxHp * 60 && (hunt == null || !f.Name.Contains(hunt)) && _avoid.Add(f.Name))
                    Log($"这种怪太危险，之后绕开: {f.Name} (一场受到 {f.DamageTaken} 点伤害 / 最大血量 {_w.MaxHp})");
                while (_lvSeen < _w.LevelUps.Count)
                {
                    var lu = _w.LevelUps[_lvSeen++];
                    _levelLog.Add((lu.Level, _sw.Elapsed.TotalSeconds, _totalKills, _w.MaxExp));
                    Log($"★ 升级到 {lu.Level} 级  (累计击杀 {_totalKills} 只, HP {_w.Hp}/{_w.MaxHp})");
                }
            }
        }

        private int _seeks;
        private readonly HashSet<(int, int)> _seekVisited = new();

        /// <summary>从 official_mongen 取当前地图的刷怪点，走向最近且未去过的一个；返回是否真的走了过去。</summary>
        private async Task<bool> SeekSpawn(string? hunt)
        {
            List<(int x, int y, string name)> pts = new();
            try
            {
                using var ds = NpgsqlDataSource.Create(_cfg.Pg);
                using var cmd = ds.CreateCommand("SELECT x,y,monster_name FROM official_mongen WHERE area=@a AND monster_name NOT ILIKE '%Guard%' AND monster_name NOT ILIKE '%Archer%'");
                cmd.Parameters.AddWithValue("a", _w.MapId);
                using var r = cmd.ExecuteReader();
                while (r.Read()) pts.Add((r.GetInt32(0), r.GetInt32(1), r.GetString(2)));
            }
            catch (Exception e) { Problem("读取刷怪点失败: " + e.Message); return false; }
            foreach (var p in pts.Where(p => !_seekVisited.Contains((p.x, p.y)) && !_avoid.Contains(p.name)
                                            && (string.IsNullOrEmpty(hunt) || p.name.Contains(hunt, StringComparison.OrdinalIgnoreCase)))
                                 .OrderBy(p => MapGrid.Cheb(_w.X, _w.Y, p.x, p.y)).Take(5))
            {
                _seekVisited.Add((p.x, p.y));
                Log($"视野无怪，前往刷怪点 {p.name} ({p.x},{p.y})，距离 {MapGrid.Cheb(_w.X, _w.Y, p.x, p.y)}");
                if (await WalkTo(p.x, p.y)) { await Pump(1500); return true; }
            }
            return false;
        }
        private async Task RestIfHurt()
        {
            if (_cfg.NoRest || _w.MaxHp <= 0 || _w.Hp * 2 >= _w.MaxHp) return;
            var t0 = _sw.Elapsed.TotalSeconds; int hp0 = _w.Hp;
            Log($"血量过低 {_w.Hp}/{_w.MaxHp}，原地休息");
            while (_w.Hp * 100 < _w.MaxHp * 85 && _sw.Elapsed.TotalSeconds - t0 < 90 && !_w.SelfDead && !_c.Closed) await Pump(500);
            _restSec += _sw.Elapsed.TotalSeconds - t0;
            Log($"休息 {(_sw.Elapsed.TotalSeconds - t0):0}s: HP {hp0} -> {_w.Hp}/{_w.MaxHp}");
        }

        private static bool IsGuard(string n) => n.Contains("守卫") || n.Contains("卫士") || n.Contains("Guard") || n.Contains("Archer");

        private Ent? PickTarget(string? hunt, HashSet<int> skipped)
        {
            Ent? best = null; int bd = int.MaxValue;
            foreach (var e in _w.Ents.Values)
            {
                if (e.Kind != Kind.Monster || e.Dead || skipped.Contains(e.Uid) || _avoid.Contains(e.Name)) continue;
                if (!_cfg.AttackGuards && IsGuard(e.Name)) continue;
                if (hunt != null && !e.Name.Contains(hunt)) continue;
                int d = MapGrid.Cheb(_w.X, _w.Y, e.X, e.Y);
                if (d < bd) { bd = d; best = e; }
            }
            return best;
        }

        private HashSet<(int, int)> Occupied(int exceptUid) =>
            _w.Ents.Values.Where(e => e.Uid != exceptUid && !(e.Kind == Kind.Monster && e.Dead)).Select(e => (e.X, e.Y)).ToHashSet();

        private async Task<bool> StepTo(int nx, int ny)
        {
            int dir = MapGrid.DirOf(nx - _w.X, ny - _w.Y);
            int rb = _w.Rollbacks; string mapBefore = _w.MapId;
            _c.Send(3011, $"{nx}/{ny}/{dir}/");
            await Pump(680); // 官方步频约 600ms
            if (_w.MapId != mapBefore) return true;      // 踩中传送门换了图: 服务端随后的位置同步不是拉回
            if (_w.Rollbacks != rb) { Log($"  被服务端拉回: 想走 ({nx},{ny})，服务端说我在 ({_w.X},{_w.Y})"); return false; }
            _w.X = nx; _w.Y = ny; return true;
        }

        private bool _runDisabled;
        /// <summary>跑步: 一次前进 2 格 (CM_RUN)。长途赶路用，被服务端拉回一次就停用跑步。</summary>
        private async Task<bool> RunTo(int x1, int y1, int x2, int y2)
        {
            int dir = MapGrid.DirOf(x2 - x1, y2 - y1);
            int rb = _w.Rollbacks; string mapBefore = _w.MapId;
            _c.Send(3013, $"{x2}/{y2}/{dir}/");
            await Pump(680);
            if (_w.MapId != mapBefore) return true;
            if (_w.Rollbacks != rb) { _runDisabled = true; Log($"  跑步被服务端拉回到 ({_w.X},{_w.Y})，之后只走不跑"); return false; }
            _w.X = x2; _w.Y = y2; return true;
        }

        private async Task<FightLog> Fight(Ent target)
        {
            var f = new FightLog { Map = _w.MapId, Name = target.Name, Uid = target.Uid, StartMs = _c.NowMs };
            Log($"目标: {target.Name}#{target.Uid} @({target.X},{target.Y})，我在({_w.X},{_w.Y}) 距离 {MapGrid.Cheb(_w.X, _w.Y, target.X, target.Y)}");
            int struckFrom = _w.Struck.Count, deathsFrom = _w.Deaths.Count, fails = 0, walkSteps = 0;
            int spellMid = 0;
            if (!string.IsNullOrEmpty(_cfg.Spell))
            {
                var kv = _w.Magics.FirstOrDefault(m => m.Value.Name.Contains(_cfg.Spell, StringComparison.OrdinalIgnoreCase) || m.Key.ToString() == _cfg.Spell);
                if (kv.Key > 0) spellMid = kv.Key;
            }
            var giveUp = _c.NowMs + 60_000;

            while (_c.NowMs < giveUp && !_w.SelfDead && !_c.Closed)
            {
                _w.Apply(_c);
                if (!_w.Ents.TryGetValue(target.Uid, out var cur) || cur.Dead) break;
                int dist = MapGrid.Cheb(_w.X, _w.Y, cur.X, cur.Y);

                if (spellMid > 0 && dist <= 6 && _w.Mp >= 5)
                {
                    // 施法远程打击: targetUid/magicId/targetX/targetY (CM_SPELL 3017)
                    f.Spells++; int sBefore = _w.Struck.Count;
                    _c.Send(3017, $"{cur.Uid}/{spellMid}/{cur.X}/{cur.Y}/");
                    await Pump(800);
                    var dm = _w.Struck.Skip(sBefore).Where(s => s.Uid == target.Uid).Select(s => s.Dmg).ToList();
                    if (dm.Count == 0) f.Misses++; else f.Hits.AddRange(dm);
                    if (f.Spells + f.Swings >= 80) { f.Note = "施法/挥刀 80 次仍未击杀"; break; }
                    continue;
                }

                if (dist <= 1)
                {
                    int dir = MapGrid.DirOf(cur.X - _w.X, cur.Y - _w.Y);
                    f.Swings++; int sBefore = _w.Struck.Count;
                    _c.Send(3014, $"{cur.X}/{cur.Y}/{dir}/");
                    await Pump(1000);
                    var dm = _w.Struck.Skip(sBefore).Where(s => s.Uid == target.Uid).Select(s => s.Dmg).ToList();
                    if (dm.Count == 0) f.Misses++; else f.Hits.AddRange(dm);
                    if (f.Spells + f.Swings >= 80) { f.Note = "挥刀 80 次仍未击杀"; break; }
                    continue;
                }
                var path = _map!.Path(_w.X, _w.Y, cur.X, cur.Y, spellMid > 0 ? 5 : 1, Occupied(target.Uid));
                if (path == null || path.Count == 0) { f.Note = "找不到通往目标的路"; break; }
                if (!await StepTo(path[0].x, path[0].y)) { if (++fails > 6) { f.Note = "多次被服务端拉回"; break; } }
                else { fails = 0; walkSteps++; }
                if (walkSteps > 150) { f.Note = "走了 150 步仍未接近"; break; }
            }
            await Pump(400);
            f.Killed = _w.Deaths.Skip(deathsFrom).Any(d => d.Uid == target.Uid);
            f.EndMs = _c.NowMs;
            f.DamageTaken = _w.Struck.Skip(struckFrom).Where(s => s.Uid == _w.SelfUid).Sum(s => s.Dmg);
            if (f.Killed)
            {
                var tgt = _w.Ents.TryGetValue(target.Uid, out var te) ? te : target;
                long diedAt = _w.Deaths.Last(d => d.Uid == target.Uid).At;
                await Pump(1200);
                var drops = _w.Ground.Values.Where(g => g.AtMs >= diedAt - 200 && MapGrid.Cheb(g.X, g.Y, tgt.X, tgt.Y) <= 3).ToList();
                foreach (var g in drops) f.Drops.Add($"{g.Name}@({g.X},{g.Y})");
                Log($"击杀 {target.Name}: 挥刀 {f.Swings} 次(落空 {f.Misses}) 用时 {(f.EndMs - f.StartMs) / 1000.0:0.0}s 伤害 [{string.Join(",", f.Hits)}] 受到伤害 {f.DamageTaken} 掉落 [{string.Join(", ", f.Drops)}]");
                foreach (var g in drops)
                {
                    if (_w.SelfDead || _w.Hp <= 0 || _bagFull) break;
                    await PickUp(g);
                }
            }
            else Log($"未能击杀 {target.Name}: {(f.Note == "" ? (_w.SelfDead ? "我死了" : "目标消失") : f.Note)} (挥刀 {f.Swings}, 命中 {f.Hits.Count}, 受到伤害 {f.DamageTaken})");
            return f;
        }

        private async Task PickUp(Ground g)
        {
            if (_w.SelfDead || _w.Hp <= 0) return;
            if (!_w.Ground.ContainsKey(g.Uid)) return;
            if (_bagFull) { _bagFullSkips++; return; }
            var path = _map!.Path(_w.X, _w.Y, g.X, g.Y, 0, new());
            if (path == null) { Problem($"够不到掉落物 {g.Name}@({g.X},{g.Y})"); return; }
            foreach (var (x, y) in path)
            {
                if (_w.SelfDead || _w.Hp <= 0) return;
                if (!await StepTo(x, y)) break;
            }
            if (_w.SelfDead || _w.Hp <= 0) return;
            int bag = _w.BagAdds.Count, gold = _w.Gold, gone = _w.GroundGone.Count, msgs = _w.SysMsgs.Count;
            _c.Send(1001); await Pump(900);
            bool got = _w.BagAdds.Count > bag || _w.Gold != gold || _w.GroundGone.Count > gone || !_w.Ground.ContainsKey(g.Uid);
            var full = _w.SysMsgs.Skip(msgs).FirstOrDefault(m => (m.Text != null && (m.Text.Contains("背包") || m.Text.Contains("负重"))));
            if (!got && full.Text != null)
            {
                _bagFull = true;
                Log($"拾取 {g.Name}: 服务端提示「{full.Text}」—— 容量或负重已达上限 (已入包 {_w.BagAdds.Count} 件)，之后不再拾取");
                return;
            }
            var protectedMsg = _w.SysMsgs.Skip(msgs).FirstOrDefault(m => m.Text != null && m.Text.Contains("保护"));
            if (!got && protectedMsg.Text != null)
            {
                Log($"拾取 {g.Name}: 服务端提示「{protectedMsg.Text}」—— 他人掉落保护期内");
                return;
            }
            Log($"拾取 {g.Name}: {(got ? "成功(含被他人捡走)" : "无反馈")}  背包新增={_w.BagAdds.Skip(bag).Select(b => b.Name).FirstOrDefault() ?? "-"} 金币={_w.Gold}");
            if (!got && !_w.SelfDead) Problem($"拾取 {g.Name} 没有任何服务端确认");
        }

        // ============================================================ 死亡
        private async Task HandleDeath()
        {
            long now = _c.NowMs;
            var killers = _w.Struck.Where(s => s.Uid == _w.SelfUid && now - s.At < 6000).Select(s => s.Who).Where(x => x.Length > 0).GroupBy(x => x).Select(g => $"{g.Key}x{g.Count()}");
            string who = string.Join(",", killers); if (who == "") who = "未知";
            string rec = $"死于 地图 {_w.MapId}({_w.X},{_w.Y}) Lv{_w.Level} 攻击者[{who}]";
            Log("☠ " + rec);
            int before = _w.PacketCounts.Values.Sum();
            await Pump(2500);
            Log("死亡后 2.5s 内服务端是否自动复活: " + (_w.SelfDead ? "否，仍是死亡状态" : "是"));
            if (_w.SelfDead)
            {
                Log("发送 CM_REVIVE(1018)，等同点击死亡确认框");
                _c.Send(1018); await Until(() => !_w.SelfDead, 6000);
            }
            rec += _w.SelfDead ? " → 发送复活请求后仍未复活 (角色卡死)" : $" → 复活于 {_w.MapId}({_w.X},{_w.Y}) HP {_w.Hp}/{_w.MaxHp}";
            if (_w.SelfDead) Problem("角色死亡后无法复活");
            _deathLog.Add(rec); Log(rec);
            if (!_w.SelfDead) { await Pump(1500); LoadMapGrid(); }
        }

        // ============================================================ 换图
        private Dictionary<string, List<Gate>> LoadGates()
        {
            var d = new Dictionary<string, List<Gate>>();
            using var ds = NpgsqlDataSource.Create(_cfg.Pg);
            using var cmd = ds.CreateCommand("SELECT from_map,from_x,from_y,to_map,to_x,to_y FROM official_map_gate");
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var g = new Gate(r.GetString(0), r.GetInt32(1), r.GetInt32(2), r.GetString(3), r.GetInt32(4), r.GetInt32(5));
                if (!d.TryGetValue(g.From, out var l)) d[g.From] = l = new();
                l.Add(g);
            }
            return d;
        }

        private bool HasMapFile(string id) => File.Exists(Path.Combine(_cfg.Root, "resources", "maps", "Map", id + ".map"));

        /// <summary>按跳数 BFS 找换图路线，只经过本机有 .map 文件的地图。</summary>
        private List<string>? MapRoute(Dictionary<string, List<Gate>> gates, string from, string to)
        {
            var prev = new Dictionary<string, string> { [from] = "" }; var q = new Queue<string>(); q.Enqueue(from);
            while (q.Count > 0)
            {
                var m = q.Dequeue();
                if (m == to) { var path = new List<string> { m }; while (prev[path[^1]] != "") path.Add(prev[path[^1]]); path.Reverse(); return path; }
                if (!gates.TryGetValue(m, out var gl)) continue;
                foreach (var g in gl) if (!prev.ContainsKey(g.To) && HasMapFile(g.To)) { prev[g.To] = m; q.Enqueue(g.To); }
            }
            return null;
        }

        private async Task TravelTo(string target)
        {
            Dictionary<string, List<Gate>> gates;
            try { gates = LoadGates(); } catch (Exception e) { Problem("读取传送门数据失败: " + e.Message); return; }
            var route = MapRoute(gates, _w.MapId, target);
            if (route == null) { Problem($"从地图 {_w.MapId} 找不到通往 {target} 的传送门路线 (可能缺 .map 文件)"); return; }
            Log($"换图路线: {string.Join(" -> ", route)}");
            for (int i = 0; i + 1 < route.Count && !_w.SelfDead && !_c.Closed && _sw.Elapsed < Deadline; i++)
            {
                string from = route[i], next = route[i + 1];
                var cands = gates[from].Where(g => g.To == next).OrderBy(g => MapGrid.Cheb(_w.X, _w.Y, g.Fx, g.Fy)).ToList();
                bool crossed = false;
                foreach (var gate in cands.Take(3))
                {
                    if (_w.MapId != from) break;
                    var t0 = _sw.Elapsed.TotalSeconds;
                    Log($"前往传送门 {from}({gate.Fx},{gate.Fy}) -> {next}({gate.Tx},{gate.Ty})，我在({_w.X},{_w.Y}) 距离 {MapGrid.Cheb(_w.X, _w.Y, gate.Fx, gate.Fy)}");
                    if (!await WalkTo(gate.Fx, gate.Fy)) { _travelLog.Add($"{from}->{next}: 走不到传送门 ({gate.Fx},{gate.Fy})"); continue; }
                    string old = from;
                    await Until(() => _w.MapId != old, 3000);
                    if (_w.MapId != old)
                    {
                        crossed = true;
                        _travelLog.Add($"{from}->{_w.MapId}: 成功，用时 {_sw.Elapsed.TotalSeconds - t0:0}s，落点 ({_w.X},{_w.Y}) (数据库期望 ({gate.Tx},{gate.Ty}))");
                        Log(_travelLog[^1]);
                        if (_w.MapId != next) Problem($"传送后地图是 {_w.MapId}，预期 {next}");
                        await Pump(2000); await Until(() => _w.Ents.Count > 0, 12000); _arrivals.Add((_w.MapId, _w.Ents.Count, _w.Ents.Values.Count(e => e.Kind == Kind.Monster), gate.Tx, gate.Ty, _w.X, _w.Y, next)); Log($"到图后等待实体: {_w.Ents.Count} 个, 首个实体出现用时见 trace"); LoadMapGrid(); break;
                    }
                    _travelLog.Add($"{from}->{next}: 已站在传送门格子 ({gate.Fx},{gate.Fy}) 但服务端没有换图");
                    Problem(_travelLog[^1]);
                }
                if (!crossed) { Problem($"没能从 {from} 换到 {next}"); return; }
            }
            if (_w.MapId == target) Log($"已到达目的地图 {_w.MapId}:{_w.MapName}，我在({_w.X},{_w.Y}) 视野内 {_w.Ents.Count} 个实体");
        }

        private async Task<bool> WalkTo(int tx, int ty)
        {
            int fails = 0, steps = 0;
            while (!(_w.X == tx && _w.Y == ty) && _sw.Elapsed < Deadline && !_w.SelfDead && !_c.Closed)
            {
                var path = _map?.Path(_w.X, _w.Y, tx, ty, 0, Occupied(-1), 400000);
                if (path == null || path.Count == 0) { Log($"A* 找不到通往 ({tx},{ty}) 的路 (我在 {_w.X},{_w.Y})"); return false; }
                // 每次最多走 8 步后重新规划 (怪物会走动、位置会被拉回)
                for (int i = 0; i < path.Count && i < 8; i++)
                {
                    string mapNow = _w.MapId; bool ok; int adv = 1;
                    var (x, y) = path[i];
                    if (!_runDisabled && i + 1 < path.Count && path[i + 1] is var (x2, y2)
                        && (x2 - x, y2 - y) == (x - _w.X, y - _w.Y) && !(x2 == tx && y2 == ty && false))
                    { ok = await RunTo(_w.X, _w.Y, x2, y2); adv = 2; i++; }
                    else ok = await StepTo(x, y);
                    if (_w.MapId != mapNow || _w.SelfDead) return true; // 已换图或死亡
                    if (!ok) { if (++fails > 8) return false; break; }
                    fails = 0; steps += adv;
                    if (steps % 40 < adv) Log($"  赶路中 ({_w.X},{_w.Y}) → ({tx},{ty}) 剩余约 {MapGrid.Cheb(_w.X, _w.Y, tx, ty)} 格");
                }
            }
            return _w.X == tx && _w.Y == ty;
        }

        private async Task BuyAndDrinkPotionFlow(string potionName)
        {
            // 药店 NPC Sam 位置比奇省 (324, 291)
            int targetX = 324, targetY = 291;
            if (_w.MapId != "0")
            {
                Log($"当前地图 {_w.MapId} 不是比奇省(0)，先传送至比奇省 (324, 291)");
                await GmSay($"tp 0 {targetX} {targetY}");
                await Pump(1500);
                LoadMapGrid();
            }

            int dist = MapGrid.Cheb(_w.X, _w.Y, targetX, targetY);
            if (dist > 2)
            {
                Log($"前往药店 NPC Sam ({targetX},{targetY})，当前坐标 ({_w.X},{_w.Y}) 距离 {dist}");
                await WalkTo(targetX, targetY);
            }

            // 寻找药店 NPC
            Ent? npc = _w.Ents.Values.FirstOrDefault(e => e.Kind == Kind.Npc && (e.Name.Contains("Sam") || e.Name.Contains("药")));
            if (npc == null)
            {
                // 若视野尚未刷新出来，尝试寻找 (324, 291) 附近的 NPC
                npc = _w.Ents.Values.FirstOrDefault(e => e.Kind == Kind.Npc && MapGrid.Cheb(e.X, e.Y, targetX, targetY) <= 3);
            }

            if (npc == null)
            {
                Problem($"药店 (324, 291) 附近未找到 NPC Sam (视野内实体数={_w.Ents.Count})");
                return;
            }

            // 靠近 NPC (切比雪夫距离 <= 2)
            if (MapGrid.Cheb(_w.X, _w.Y, npc.X, npc.Y) > 2)
            {
                var p = _map?.Path(_w.X, _w.Y, npc.X, npc.Y, 2, Occupied(npc.Uid));
                if (p != null && p.Count > 0)
                {
                    foreach (var pt in p) await StepTo(pt.x, pt.y);
                }
            }

            Log($"与药店 NPC {npc.Name}#{npc.Uid} 对话，我与 NPC 距离 {MapGrid.Cheb(_w.X, _w.Y, npc.X, npc.Y)}");
            int dlgBefore = _w.NpcDialogs.Count;
            // 发送 CM_MERCHANTHOMEPAGE (1010)
            _c.Send(1010, npc.Uid.ToString());
            if (!await Until(() => _w.NpcDialogs.Count > dlgBefore, 3000))
            {
                Log("未收到 NPC 对话文本 (54 SM_NPCDIALOG)，继续尝试购买");
            }
            else
            {
                Log($"收到药店对话: {_w.NpcDialogs[^1].Dialog[..Math.Min(60, _w.NpcDialogs[^1].Dialog.Length)]}...");
            }

            int goldBefore = _w.Gold;
            int buysBefore = _w.BuySuccesses.Count;
            int bagBefore = _w.BagAdds.Count;

            // 发送 CM_USERBUYITEM (1014)，购买商品：金创药(小)
            Log($"向 NPC 发起购买请求: {potionName}");
            _c.Send(1014, $"{npc.Uid}/{potionName}");

            bool buyOk = await Until(() => _w.BuySuccesses.Count > buysBefore || _w.BagAdds.Count > bagBefore, 4000);
            if (!buyOk)
            {
                if (_w.BuyFails.Count > 0)
                    Problem($"购买失败: {_w.BuyFails[^1].Error}");
                else
                    Problem($"购买 {potionName} 超时无响应 (金币={_w.Gold})");
                return;
            }

            Log($"购买成功！金币变动: {goldBefore} -> {_w.Gold}，当前背包新增物品数: {_w.BagAdds.Count}");

            // 接下来测试吃药：如果当前满血，先通过 GM 扣减血量以便观察药水回血效果
            if (_w.Hp >= _w.MaxHp)
            {
                Log($"当前满血 ({_w.Hp}/{_w.MaxHp})，设置 HP 为 5 以便验证药水回复");
                await GmSay("hp 5");
                await Pump(500);
            }

            int hpBefore = _w.Hp;
            int eatsBefore = _w.EatSuccesses.Count;
            Log($"使用药水: {potionName}，使用前 HP={_w.Hp}/{_w.MaxHp}");
            _c.Send(1006, potionName);

            bool eatOk = await Until(() => _w.EatSuccesses.Count > eatsBefore || _w.Hp > hpBefore, 4000);
            if (!eatOk)
            {
                Problem($"使用药水 {potionName} 未收到 SM_EAT_OK (606) 且 HP 未回复");
                return;
            }

            // 等待药效 Tick 释放并观察血量回复
            var t0 = _sw.Elapsed.TotalSeconds;
            while (_w.Hp <= hpBefore && _sw.Elapsed.TotalSeconds - t0 < 4)
            {
                await Pump(300);
            }
            Log($"药水生效！HP 回复: {hpBefore} -> {_w.Hp}/{_w.MaxHp} (收到 SM_EAT_OK={_w.EatSuccesses.Count > eatsBefore})");
        }

        // ============================================================ 装备穿脱丢弃场景
        private async Task EquipWearAndDropFlow()
        {
            // 等待背包内有装备
            await Until(() => _w.BagAdds.Count > 0, 3000);
            Log($"=== 执行装备穿脱丢弃测试: 当前背包新增物品数 {_w.BagAdds.Count} ===");

            // 1. 穿戴铁剑 (武器槽 0，第 0 格)
            int takeOnBefore = _w.TakeOns.Count;
            Log("发送 CM_WEARITEM (1003): 穿戴第 0 格武器至武器槽 0");
            _c.Send(1003, "0/0/");
            if (!await Until(() => _w.TakeOns.Count > takeOnBefore, 4000))
            {
                Problem("穿戴铁剑未收到 SM_TAKEON_OK (615)");
                return;
            }
            Log($"穿戴铁剑成功！收到 SM_TAKEON_OK (槽位={_w.TakeOns[^1].EquipSlot}, 物品={_w.TakeOns[^1].Name})");

            // 2. 穿戴金手镯 (手镯槽 5，此时原第 1 格成为第 0 格)
            takeOnBefore = _w.TakeOns.Count;
            Log("发送 CM_WEARITEM (1003): 穿戴项链/手镯至槽位 5");
            _c.Send(1003, "0/5/");
            await Until(() => _w.TakeOns.Count > takeOnBefore, 4000);

            // 3. 卸下武器 (CM_TAKEOFFITEM 1004)
            int takeOffBefore = _w.TakeOffs.Count;
            Log("发送 CM_TAKEOFFITEM (1004): 脱下槽位 0 武器");
            _c.Send(1004, "0/");
            if (!await Until(() => _w.TakeOffs.Count > takeOffBefore, 4000))
            {
                Problem("脱下铁剑未收到 SM_TAKEOFF_OK (619)");
                return;
            }
            Log($"脱下铁剑成功！收到 SM_TAKEOFF_OK (槽位={_w.TakeOffs[^1].EquipSlot})");

            // 4. 丢弃物品 (CM_DROPITEM 1000)
            int delBefore = _w.DelItems.Count;
            int groundBefore = _w.Ground.Count;
            Log("发送 CM_DROPITEM (1000): 丢弃背包第 0 格物品到地面");
            _c.Send(1000, "0/");
            if (!await Until(() => _w.DelItems.Count > delBefore || _w.Ground.Count > groundBefore, 4000))
            {
                Problem("丢弃物品未收到 SM_DELITEM (202) 或 SM_ITEMSHOW (625)");
                return;
            }
            Log($"丢弃物品成功！地面物品数={_w.Ground.Count}, 删除物品={(_w.DelItems.Count > delBefore ? _w.DelItems[^1].Name : "已同步")}");
        }

        // ============================================================ 道士召唤与火符战斗场景
        private async Task TaoistSummonAndCastFlow()
        {
            Log("=== 执行道士技能与随从战斗测试 ===");
            // 1. 施法召唤骷髅 (MagicId 17, CM_SPELL 3017)
            int spellsBefore = _w.SpellsCast.Count;
            int entsBefore = _w.Ents.Count;
            Log("道士施放【召唤骷髅】(MagicId 17)");
            _c.Send(3017, $"0/17/{_w.X}/{_w.Y}/");
            await Pump(1500);

            // 检查随从是否生成 (实体中出现变异骷髅或宠物)
            bool petSpawned = await Until(() => _w.Ents.Values.Any(e => e.Name.Contains("骷髅")), 5000);
            if (petSpawned)
            {
                var pet = _w.Ents.Values.First(e => e.Name.Contains("骷髅"));
                Log($"变异骷髅成功召唤出场！Uid={pet.Uid}, Name={pet.Name}, 坐标=({pet.X},{pet.Y})");
            }
            else
            {
                Log("未直接检测到骷髅实体，检查施法包广播");
            }

            // 2. 接下来用灵魂火符 (MagicId 13) 击杀怪物
            _cfg.Spell = "灵魂火符";
            int targetKills = _totalKills + Math.Max(2, _cfg.Kills);
            await Hunt(() => _totalKills >= targetKills, _cfg.Hunt);
        }

        // ============================================================ 组队场景 (队长/队员协同)
        private async Task PartyFlow(string role)
        {
            Log($"=== 执行组队测试流程: 当前角色={CharacterName}, 角色身份={role} ===");
            // 1. 发送 CM_GROUPMODE (1019) 开启允许编组模式
            _c.Send(1019, "1/");
            await Pump(500);

            if (role.StartsWith("leader"))
            {
                // 队长: 先创建队伍 CM_CREATEGROUP (1020)
                int groupBefore = _w.GroupLists.Count;
                Log("队长发送 CM_CREATEGROUP (1020) 创建队伍");
                _c.Send(1020, "/");
                await Until(() => _w.GroupLists.Count > groupBefore, 3000);

                // 等待队员上线并就绪 (约 4 秒)
                Log("队长等待队员上线并就绪...");
                await Pump(4000);

                // 寻找同图的队友角色名 (最多等待 15 秒)
                string targetName = _cfg.InviteName ?? "";
                var waitTeammate = Stopwatch.StartNew();
                while (waitTeammate.ElapsedMilliseconds < 15000 && string.IsNullOrEmpty(targetName))
                {
                    await Pump(400);
                    var tm = _w.Ents.Values.FirstOrDefault(e => e.Kind == Kind.Player && e.Name.StartsWith("机器人") && e.Name != CharacterName);
                    if (tm != null) targetName = tm.Name;
                }

                // 队长邀请队员入队 CM_ADDGROUPMEMBER (1021)
                if (!string.IsNullOrEmpty(targetName))
                {
                    groupBefore = _w.GroupLists.Count;
                    Log($"队长邀请队员 {targetName} 加入队伍 (CM_ADDGROUPMEMBER 1021)");
                    // 可能队员刚传送完视野还未完全同步，重试发送 2 次邀请
                    for (int inv = 0; inv < 3 && _w.GroupLists.Count == groupBefore; inv++)
                    {
                        _c.Send(1021, targetName);
                        await Until(() => _w.GroupLists.Count > groupBefore, 3000);
                    }
                }
                else
                {
                    Problem("队长未发现队员机器人，未能发起组队邀请");
                }

                // 队长打怪，提供经验包供全队分享
                if (_cfg.Kills > 0)
                {
                    int target = _totalKills + _cfg.Kills;
                    await Hunt(() => _totalKills >= target, _cfg.Hunt);
                }
            }
            else
            {
                // 队员: 等待队长创建与邀请，观察 SM_GROUPMEMBERS (667)
                Log("队员等待队长建队与组队广播 (SM_GROUPMEMBERS 667)...");
                await Until(() => _w.GroupLists.Count > 0, 15000);
                if (_w.GroupLists.Count > 0)
                {
                    Log($"队员收到队伍广播！名单={_w.GroupLists[^1].Members}");
                }

                // 队员在旁观察并等待均摊经验包 (SM_EXPERIENCE 45)
                long expBefore = _w.Exp;
                await Until(() => _w.Exp > expBefore, 30000);
                if (_w.Exp > expBefore)
                {
                    Log($"队员成功获得同屏均摊经验！经验变化: {expBefore} -> {_w.Exp}");
                }
            }
        }

        // ============================================================ 快捷键绑定设置场景
        private async Task KeybindFlow()
        {
            Log("=== 执行技能快捷键设置测试 ===");
            // 等待学会雷电术 (MagicId 10)
            await Until(() => _w.Magics.ContainsKey(10), 3000);
            if (!_w.Magics.ContainsKey(10))
            {
                Problem("未找到雷电术技能，无法测试快捷键绑定");
                return;
            }

            int updatesBefore = _w.MagicUpdates.Count;
            // 发送 CM_MAGICKEYCHANGE (1008): 设置雷电术 (10) 快捷键为 F1 (keyNum = 0)
            Log("发送 CM_MAGICKEYCHANGE (1008): 设置雷电术为 F1 (key=0)");
            _c.Send(1008, "10/0");

            bool ok = await Until(() => _w.MagicUpdates.Count > updatesBefore || (_w.Magics.TryGetValue(10, out var m) && m.Key == 0), 4000);
            if (!ok)
            {
                Problem("设置技能快捷键未收到 SM_UPDATEUSERMAGIC (203)");
                return;
            }
            Log($"技能快捷键设置成功！当前雷电术快捷键 Key={_w.Magics[10].Key} (收到 SM_UPDATEUSERMAGIC={_w.MagicUpdates.Count > updatesBefore})");
        }

        // ============================================================ 自由属性点分配场景
        private async Task AdjustBonusFlow()
        {
            Log($"=== 执行属性加点测试: 当前剩余点数={_w.BonusPoint} ===");
            if (_w.BonusPoint <= 0)
            {
                Log("当前属性点为 0，通过 GM 补充 5 点属性点");
                await GmSay("bonus 5");
                await Pump(1000);
            }

            int bonusBefore = _w.BonusPoint;
            int hpBefore = _w.MaxHp;
            // 发送 CM_ADJUST_BONUS (1043): 分配 1 点到 HP (bonusType = 5)
            Log("发送 CM_ADJUST_BONUS (1043): 分配 1 点至生命值 (bonusType=5)");
            _c.Send(1043, "5/");

            bool ok = await Until(() => _w.BonusPoint < bonusBefore || _w.MaxHp > hpBefore, 4000);
            if (!ok)
            {
                Problem("分配属性点未收到 SM_ABILITY 更新 (剩余点数或 MaxHp 未变动)");
                return;
            }
            Log($"属性点分配成功！剩余点数: {bonusBefore} -> {_w.BonusPoint}，最大生命值: {hpBefore} -> {_w.MaxHp}");
        }

        // ============================================================ 丢弃金币到地面场景
        private async Task DropGoldFlow()
        {
            Log($"=== 执行丢弃金币测试: 当前金币={_w.Gold} ===");
            if (_w.Gold < 500)
            {
                Log("当前金币不足 500，通过 GM 补充 2000 金币");
                await GmSay("gold 2000");
                await Pump(1000);
            }

            int goldBefore = _w.Gold;
            int groundBefore = _w.Ground.Count;
            // 发送 CM_DROPGOLD (1016): 丢弃 200 金币到地面
            Log("发送 CM_DROPGOLD (1016): 丢弃 200 金币到当前位置地面");
            _c.Send(1016, "200/");

            bool ok = await Until(() => (_w.Gold < goldBefore && _w.Gold >= 0) || _w.Ground.Values.Any(g => g.Name.Contains("金币")), 4000);
            if (!ok)
            {
                Problem("丢弃金币未收到金币扣减或地面金币实体出现 (SM_GOLDCHANGED / SM_ITEMSHOW)");
                return;
            }
            Log($"丢弃金币成功！金币变动: {goldBefore} -> {_w.Gold}，地面金币数={_w.Ground.Values.Count(g => g.Name.Contains("金币"))}");
        }

        // ============================================================ 生物尸体割肉场景
        private async Task ButchFlow()
        {
            Log("=== 执行尸体割肉测试 (Alt+挖肉): 寻找并击杀鸡/鹿 ===");
            // 击杀 1 只鸡作为割肉目标
            int deadBefore = _w.Deaths.Count;
            int butchBefore = _w.ButchResults.Count;
            int bagBefore = _w.BagAdds.Count;

            // 击杀 1 只怪
            int targetKills = _totalKills + 1;
            await Hunt(() => _totalKills >= targetKills, "鸡");

            if (_totalKills < targetKills || _w.Deaths.Count == deadBefore)
            {
                Problem("未能成功击杀割肉目标生物");
                return;
            }

            var deadCorpse = _w.Deaths.Last();
            int corpseUid = deadCorpse.Uid;
            Log($"找到击杀的尸体 Uid={corpseUid}，开始向尸体靠拢并进行割肉 (CM_BUTCHITEM 1007)");

            // 靠近尸体
            if (_w.Ents.TryGetValue(corpseUid, out var entCorpse))
            {
                if (MapGrid.Cheb(_w.X, _w.Y, entCorpse.X, entCorpse.Y) > 1)
                {
                    await WalkTo(entCorpse.X, entCorpse.Y);
                }
            }

            // 发送 CM_BUTCHITEM (1007): corpseUid/x/y/
            _c.Send(1007, $"{corpseUid}/{_w.X}/{_w.Y}/");

            bool ok = await Until(() => _w.ButchResults.Count > butchBefore || _w.BagAdds.Count > bagBefore, 4000);
            if (!ok)
            {
                Problem("尸体割肉未收到 SM_BUTCH (665) 或肉品入包回执");
                return;
            }
            string meatInfo = _w.ButchResults.Count > butchBefore ? _w.ButchResults[^1].MeatDesc : "已入包";
            Log($"割肉成功！获得反馈: {meatInfo} (SM_BUTCH 665 触发成功)");
        }

        // ============================================================ 商店出售物品场景
        private async Task SellItemFlow()
        {
            Log("=== 执行商店出售物品测试 (CM_USERSELLITEM 1013) ===");
            // 确保背包里有物品可卖 (若无则发放一把木剑)
            if (_w.BagAdds.Count == 0)
            {
                await GmSay("item 木剑 1");
                await Pump(1000);
            }

            int goldBefore = _w.Gold;
            int sellBefore = _w.SellSuccesses.Count;
            // 发送 CM_USERSELLITEM (1013): 0/ (卖出背包第 0 格)
            Log("向 NPC 发送出售请求 (CM_USERSELLITEM 1013): 出售背包第 0 格物品");
            _c.Send(1013, "0/");

            bool ok = await Until(() => _w.SellSuccesses.Count > sellBefore || (_w.Gold > goldBefore && goldBefore >= 0), 4000);
            if (!ok)
            {
                Problem("出售物品未收到 SM_SELL_OK_DELPHI (650) 或金币增加回显");
                return;
            }
            Log($"出售物品成功！金币变动: {goldBefore} -> {_w.Gold} (收到 SM_SELL_OK={_w.SellSuccesses.Count > sellBefore})");
        }

        // ============================================================ 铁匠铺修理装备场景
        private async Task RepairItemFlow()
        {
            Log("=== 执行装备修理测试 (CM_USERREPAIRITEM 1023) ===");
            // 确保有金币和待修理的装备 (木剑，且穿戴在身上或在背包中)
            if (_w.Gold < 500)
            {
                await GmSay("gold 2000");
                await Pump(800);
            }
            if (_w.BagAdds.Count == 0)
            {
                await GmSay("item 铁剑 1");
                await Pump(800);
            }

            int repairBefore = _w.RepairSuccesses.Count;
            // 发送 CM_USERREPAIRITEM (1023): 0/ (普通修理)
            Log("向铁匠铺发送普通修理请求 (CM_USERREPAIRITEM 1023)");
            _c.Send(1023, "0/");

            bool ok = await Until(() => _w.RepairSuccesses.Count > repairBefore, 4000);
            if (!ok)
            {
                Problem("修理装备未收到 SM_REPAIROK_DELPHI (669)");
                return;
            }
            var rep = _w.RepairSuccesses[^1];
            Log($"修理装备成功！装备={rep.Name}, 持久={rep.Dura}/{rep.MaxDura}, 剩余金币={rep.GoldRemaining} (SM_REPAIROK 669 触发成功)");
        }

        // ============================================================ 战士剑术被动开关切换场景
        private async Task SwordSwitchFlow()
        {
            Log("=== 执行战士剑术开关切换测试 (3026 刺杀 / 3027 半月) ===");
            int sysBefore = _w.SysMsgs.Count;

            // 1. 发送 CM_THRUSTINGONOFF (3026) 打开刺杀剑术
            Log("发送 CM_THRUSTINGONOFF (3026): 切换刺杀剑术开关");
            _c.Send(3026, "1/");
            bool thrustOk = await Until(() => _w.SysMsgs.Skip(sysBefore).Any(m => m.Text != null && m.Text.Contains("刺杀")), 3000);
            if (!thrustOk)
            {
                Problem("切换刺杀剑术未收到系统回显");
                return;
            }
            Log($"刺杀剑术开关切换成功！收到回显: {_w.SysMsgs[^1].Text}");

            sysBefore = _w.SysMsgs.Count;
            // 2. 发送 CM_HALFMOONONOFF (3027) 打开半月弯刀
            Log("发送 CM_HALFMOONONOFF (3027): 切换半月弯刀开关");
            _c.Send(3027, "1/");
            bool moonOk = await Until(() => _w.SysMsgs.Skip(sysBefore).Any(m => m.Text != null && m.Text.Contains("半月")), 3000);
            if (!moonOk)
            {
                Problem("切换半月弯刀未收到系统回显");
                return;
            }
            Log($"半月弯刀开关切换成功！收到回显: {_w.SysMsgs[^1].Text}");
        }

        // ============================================================ 输出
        private void DumpPackets()
        {
            Log("---- 收到的包类型统计 (含前 3 个样例) ----");
            foreach (var kv in _w.PacketCounts.OrderBy(k => k.Key))
                Log($"  id={kv.Key,4} x{kv.Value,5}   例: {string.Join("  ", _w.Samples[kv.Key].Select(s => s.ToString()[..Math.Min(80, s.ToString().Length)]))}");
        }

        /// <summary>经聊天框发 GM 命令并记录服务端回显（"GM: ..." 系统消息）。</summary>
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
                dead = _w.SelfDead, finalMap = _w.MapId, x = _w.X, y = _w.Y, gold = _w.Gold, exp = _w.Exp, maxExp = _w.MaxExp, bonusPoint = _w.BonusPoint,
                problems = _problems, gm = _gmLog,
                levels = _levelLog.Select(l => new { l.Level, l.AtSec, l.Kills, l.MaxExp }),
                arrivals = _arrivals.Select(a => new { map = a.Map, ents = a.Ents, monsters = a.Monsters, expectX = a.ExpectX, expectY = a.ExpectY, x = a.ActualX, y = a.ActualY, expect = a.Expect }),
                deaths = _deathLog.Count, deathPackets = _w.Deaths.Count(d => d.Uid == _w.SelfUid), revives = _deathLog.Count(d => d.Contains("复活于")),
                struckBy = _w.Struck.GroupBy(s => s.Who).Select(g => new { who = g.Key, hits = g.Count(), dmg = g.Sum(s => s.Dmg) }),
                bag = _w.BagAdds.Select(b => b.Name),
                buys = _w.BuySuccesses.Select(b => new { uid = b.ItemUid, name = b.Name }),
                eats = _w.EatSuccesses.Count,
                npcDialogs = _w.NpcDialogs.Select(d => new { npcUid = d.NpcUid, dialog = d.Dialog }),
                magics = _w.Magics.Select(m => new { id = m.Key, name = m.Value.Name, level = m.Value.Level, key = m.Value.Key }),
                magicUpdates = _w.MagicUpdates.Select(u => new { id = u.MagicId, level = u.Level, key = u.Key }),
                spellsCast = _w.SpellsCast.Count,
                spellEffects = _w.SpellEffects.Count,
                takeOns = _w.TakeOns.Select(t => new { slot = t.EquipSlot, name = t.Name }),
                takeOffs = _w.TakeOffs.Select(t => new { slot = t.EquipSlot }),
                delItems = _w.DelItems.Select(d => new { uid = d.ItemUid, name = d.Name }),
                groundCount = _w.Ground.Count,
                groupLists = _w.GroupLists.Select(g => new { count = g.Count, members = g.Members }),
                butchResults = _w.ButchResults.Select(b => new { corpseUid = b.CorpseUid, desc = b.MeatDesc }),
                sells = _w.SellSuccesses.Select(s => new { uid = s.ItemUid, gold = s.GoldEarned, name = s.Name }),
                repairs = _w.RepairSuccesses.Select(r => new { gold = r.GoldRemaining, dura = r.Dura, maxDura = r.MaxDura, name = r.Name }),
                sysMsgs = _w.SysMsgs.Select(s => s.Text),
                characterName = CharacterName,
                fights = _fights.Select(f => new { f.Map, f.Name, f.Killed, f.Swings, f.Spells, f.Misses, f.DamageTaken, f.Hits, drops = f.Drops.Select(d => d[..d.IndexOf('@')]) }),
            };
            File.WriteAllText(_cfg.JsonPath, System.Text.Json.JsonSerializer.Serialize(o, new System.Text.Json.JsonSerializerOptions { WriteIndented = true, Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping }));
        }

        private void Report()
        {
            WriteJson();
            Console.WriteLine();
            Console.WriteLine("================ 玩家视角报告 ================");
            Console.WriteLine($"总击杀 {_totalKills}   总用时 {_sw.Elapsed.TotalSeconds:0}s (休息 {_restSec:0}s)   服务端拉回 {_w.Rollbacks} 次   最终 Lv{_w.Level} HP {_w.Hp}/{_w.MaxHp} 经验 {_w.Exp}/{_w.MaxExp} 金币 {_w.Gold}   当前地图 {_w.MapId}:{_w.MapName}({_w.X},{_w.Y})");
            if (_levelLog.Count > 1)
            {
                Console.WriteLine("升级曲线 (等级 | 达成时刻 | 累计击杀 | 经验槽):");
                foreach (var l in _levelLog) Console.WriteLine($"  Lv{l.Level,-3} {l.AtSec,7:0}s  击杀 {l.Kills,4}  经验槽 {l.MaxExp}");
            }
            if (_travelLog.Count > 0) { Console.WriteLine("换图记录:"); foreach (var t in _travelLog) Console.WriteLine("  " + t); }
            if (_deathLog.Count > 0) { Console.WriteLine($"死亡 {_deathLog.Count} 次:"); foreach (var d in _deathLog) Console.WriteLine("  " + d); }
            foreach (var g in _fights.GroupBy(f => (f.Map, f.Name)))
            {
                var ks = g.Where(f => f.Killed).ToList();
                string ttk = ks.Count == 0 ? "-" : $"{ks.Average(f => (f.EndMs - f.StartMs) / 1000.0):0.0}s";
                var hits = g.SelectMany(f => f.Hits).ToList();
                string dmg = hits.Count == 0 ? "-" : $"{hits.Min()}~{hits.Max()} (均 {hits.Average():0.0})";
                Console.WriteLine($"  [{g.Key.Map}] {g.Key.Name,-8} 遇到 {g.Count()} 只 击杀 {ks.Count}  平均击杀 {ttk}  单刀 {dmg}  落空 {g.Sum(f => f.Misses)}/{g.Sum(f => f.Swings)}  受到伤害 {g.Sum(f => f.DamageTaken)}");
                var drops = g.SelectMany(f => f.Drops).GroupBy(d => d[..d.IndexOf('@')]).Select(d => $"{d.Key}x{d.Count()}");
                Console.WriteLine($"        掉落: {(drops.Any() ? string.Join(", ", drops) : "无")}");
                foreach (var n in g.Where(f => !f.Killed).Select(f => f.Note).Where(n => n != "").Distinct()) Console.WriteLine($"        未击杀原因: {n}");
            }
            if (_avoid.Count > 0) Console.WriteLine("被判定为危险而绕开的怪: " + string.Join(", ", _avoid));
            if (_bagFull) Console.WriteLine($"背包在获得 {_w.BagAdds.Count} 件物品后装满，之后 {_bagFullSkips} 件掉落物被放弃。");
            Console.WriteLine($"系统消息 {_w.SysMsgs.Count} 条: " + string.Join(" | ", _w.SysMsgs.Select(m => m.Text).Distinct().Take(8)));
            Console.WriteLine(_problems.Count == 0 ? "未发现问题。" : "发现的问题:\n" + string.Join("\n", _problems.Select(p => "  - " + p)));
        }
    }
}


