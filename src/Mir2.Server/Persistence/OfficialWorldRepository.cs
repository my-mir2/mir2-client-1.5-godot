using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Npgsql;

namespace Mir2.Server.Persistence;

/// <summary>
/// 官方世界数据仓库：服务端启动时一次性从 PostgreSQL 读取地图碰撞、传送门、刷怪、掉落与 NPC，
/// 数据库 (由 tools/import_official_envir_to_pg.py 导入) 是运行时唯一真值，不读取任何散落文件。
/// </summary>
public sealed class OfficialWorldRepository(NpgsqlDataSource dataSource)
{
    public sealed record SpawnRow(int RowId, int MapNum, int X, int Y, string MonsterName, int Scope, int Count,
        string OfficialKey, string NameCn, int Hp, int IntervalMin);

    public sealed record StartPoint(int MapNum, int X, int Y);

    public sealed class LoadReport
    {
        public int MapsLoaded, MapsSkippedBadSize, GatesLoaded, SpawnRows, SpawnRowsSkippedUnknownMonster, SpawnMonsterTotal;
        public int DropMonsters, DropRules, DropItemsUnmatched, NpcsLoaded, NpcsWithGoods;
        public List<string> SkippedMaps = new();
        public List<string> UnknownMonsters = new();
        public List<string> UnmatchedDropItems = new();
    }

    public sealed record WorldData(
        List<GameMap> Maps, List<MapPassage> Passages, List<SpawnRow> Spawns,
        Dictionary<string, List<MonsterDropEngine.MonItemRule>> Drops, List<NpcEntity> Npcs, List<StartPoint> StartPoints,
        LoadReport Report);

    public bool HasWorldData()
    {
        using var cmd = dataSource.CreateCommand("SELECT to_regclass('official_map') IS NOT NULL AND (SELECT COUNT(*) FROM official_map) > 0");
        return cmd.ExecuteScalar() is bool b && b;
    }

    public WorldData Load()
    {
        var rep = new LoadReport();
        var maps = LoadMaps(rep);
        var loadedNums = maps.Select(m => m.MapId).ToHashSet();
        var passages = LoadPassages(loadedNums, rep);
        var spawns = LoadSpawns(loadedNums, rep);
        var drops = LoadDrops(rep);
        var npcs = LoadNpcs(loadedNums, rep);
        var starts = LoadStartPoints(loadedNums);
        return new WorldData(maps, passages, spawns, drops, npcs, starts, rep);
    }

    private List<GameMap> LoadMaps(LoadReport rep)
    {
        var list = new List<GameMap>();
        using var cmd = dataSource.CreateCommand(
            "SELECT m.map_id, m.map_num, m.width, m.height, m.cells, COALESCE(i.map_name, m.map_id), COALESCE(i.flags, '') " +
            "FROM official_map m LEFT JOIN official_map_info i ON i.map_id = m.map_id ORDER BY m.map_num");
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            string id = r.GetString(0);
            int num = r.GetInt32(1), w = r.GetInt32(2), h = r.GetInt32(3);
            byte[] cells = r.GetFieldValue<byte[]>(4);
            string name = r.GetString(5);
            var map = new GameMap(num, name, w, h) { OfficialId = id };
            map.ApplyOfficialFlags(r.GetString(6));
            if (!map.LoadFromCells(cells, w, h))
            {
                // 文件长度与头部宽高不吻合 (官方 T118/kt0000 等老格式)，如实跳过而不是挂一张全可通行的假地图
                rep.MapsSkippedBadSize++;
                rep.SkippedMaps.Add($"{id}({w}x{h}, cells={cells.Length})");
                continue;
            }
            list.Add(map);
            rep.MapsLoaded++;
        }
        return list;
    }

    private List<MapPassage> LoadPassages(HashSet<int> loaded, LoadReport rep)
    {
        var list = new List<MapPassage>();
        using var cmd = dataSource.CreateCommand(
            "SELECT a.map_num, g.from_x, g.from_y, b.map_num, g.to_x, g.to_y, g.from_map, g.to_map " +
            "FROM official_map_gate g JOIN official_map a ON a.map_id = g.from_map JOIN official_map b ON b.map_id = g.to_map");
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            int from = r.GetInt32(0), to = r.GetInt32(3);
            if (!loaded.Contains(from) || !loaded.Contains(to)) continue;
            list.Add(new MapPassage
            {
                FromMapId = from, FromX = r.GetInt32(1), FromY = r.GetInt32(2),
                ToMapId = to, ToX = r.GetInt32(4), ToY = r.GetInt32(5),
                Description = $"官方 MapInfo {r.GetString(6)}->{r.GetString(7)}"
            });
            rep.GatesLoaded++;
        }
        return list;
    }

    private List<SpawnRow> LoadSpawns(HashSet<int> loaded, LoadReport rep)
    {
        var list = new List<SpawnRow>();
        var unknown = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        using var cmd = dataSource.CreateCommand(
            "SELECT g.id, m.map_num, g.x, g.y, g.monster_name, g.scope, g.mon_count, o.name, o.name_cn, o.hp, g.interval_min " +
            "FROM official_mongen g JOIN official_map m ON m.map_id = g.area " +
            "LEFT JOIN official_monster o ON lower(o.name) = lower(g.monster_name) ORDER BY g.id");
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            int num = r.GetInt32(1);
            if (!loaded.Contains(num)) continue;
            rep.SpawnRows++;
            if (r.IsDBNull(7))
            {
                // 官方 Mongen 里的拼写错误 (如 Keartoid / CannnibalPlant)，原版同样刷不出，如实记录
                rep.SpawnRowsSkippedUnknownMonster++;
                unknown.Add(r.GetString(4));
                continue;
            }
            int cnt = r.GetInt32(6);
            rep.SpawnMonsterTotal += cnt;
            list.Add(new SpawnRow(r.GetInt32(0), num, r.GetInt32(2), r.GetInt32(3), r.GetString(4), r.GetInt32(5), cnt,
                r.GetString(7), r.IsDBNull(8) ? "" : r.GetString(8), r.IsDBNull(9) ? 100 : r.GetInt32(9), r.GetInt32(10)));
        }
        rep.UnknownMonsters.AddRange(unknown);
        return list;
    }

    private Dictionary<string, List<MonsterDropEngine.MonItemRule>> LoadDrops(LoadReport rep)
    {
        var dict = new Dictionary<string, List<MonsterDropEngine.MonItemRule>>(StringComparer.OrdinalIgnoreCase);
        var unmatched = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        // 每只怪取一份掉落表：优先根目录 default (M2 实际读取)，其次 english，再次 chinese
        using var cmd = dataSource.CreateCommand(
            "WITH pick AS (SELECT monster_key, MIN(CASE variant WHEN 'default' THEN 1 WHEN 'english' THEN 2 ELSE 3 END) AS rk " +
            "FROM official_mon_drop GROUP BY monster_key) " +
            "SELECT d.monster_key, d.prob_num, d.prob_den, d.item_name, d.item_count, s.name_cn " +
            "FROM official_mon_drop d JOIN pick p ON p.monster_key = d.monster_key " +
            "AND p.rk = CASE d.variant WHEN 'default' THEN 1 WHEN 'english' THEN 2 ELSE 3 END " +
            "LEFT JOIN official_stditems s ON lower(s.name) = lower(d.item_name) ORDER BY d.monster_key, d.seq");
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            string key = r.GetString(0);
            string raw = r.GetString(3);
            bool gold = raw.Equals("gold", StringComparison.OrdinalIgnoreCase) || raw == "金币";
            string name;
            if (gold) name = "金币";
            else if (!r.IsDBNull(5)) name = r.GetString(5);
            else { name = raw; unmatched.Add(raw); rep.DropItemsUnmatched++; }
            int count = r.IsDBNull(4) ? 1 : r.GetInt32(4);
            if (!dict.TryGetValue(key, out var rules)) dict[key] = rules = new();
            rules.Add(new MonsterDropEngine.MonItemRule
            {
                Numerator = Math.Max(1, r.GetInt32(1)),
                Denominator = Math.Max(1, r.GetInt32(2)),
                ItemName = name,
                Count = gold ? Math.Max(1, count) : 1
            });
            rep.DropRules++;
        }
        rep.DropMonsters = dict.Count;
        rep.UnmatchedDropItems.AddRange(unmatched);
        return dict;
    }

    private List<StartPoint> LoadStartPoints(HashSet<int> loaded)
    {
        var list = new List<StartPoint>();
        using var cmd = dataSource.CreateCommand(
            "SELECT m.map_num, s.x, s.y FROM official_start_point s JOIN official_map m ON m.map_id = s.map_id ORDER BY s.id");
        using var r = cmd.ExecuteReader();
        while (r.Read())
            if (loaded.Contains(r.GetInt32(0))) list.Add(new StartPoint(r.GetInt32(0), r.GetInt32(1), r.GetInt32(2)));
        return list;
    }

    private List<NpcEntity> LoadNpcs(HashSet<int> loaded, LoadReport rep)
    {
        var items = new Dictionary<string, (string Cn, int Price, int Weight, int Dura)>(StringComparer.OrdinalIgnoreCase);
        using (var ic = dataSource.CreateCommand("SELECT name, name_cn, price, weight, duramax FROM official_stditems"))
        using (var ir = ic.ExecuteReader())
        {
            while (ir.Read())
                items[ir.GetString(0)] = (ir.GetString(1), ir.IsDBNull(2) ? 0 : (int)ir.GetInt64(2),
                    ir.IsDBNull(3) ? 1 : ir.GetInt32(3), ir.IsDBNull(4) ? 1000 : (int)ir.GetInt64(4));
        }

        var list = new List<NpcEntity>();
        using var cmd = dataSource.CreateCommand(
            "SELECT p.id, m.map_num, p.x, p.y, p.npc_name, p.appr, s.content " +
            "FROM official_npc_placement p JOIN official_map m ON m.map_id = p.map_id " +
            "LEFT JOIN official_script s ON s.category = 'market_def' AND lower(s.script_name) = lower(p.script_name || '-' || p.map_id) " +
            "ORDER BY p.id");
        using var r = cmd.ExecuteReader();
        while (r.Read())
        {
            int num = r.GetInt32(1);
            if (!loaded.Contains(num)) continue;
            string rawName = r.GetString(4);
            string cnName = rawName switch
            {
                "Jim" or "Danielle" or "Daisy" or "Vincent" or "Peter" => "肉店老板",
                "Smith" or "Blacksmith" => "铁匠",
                "Steven" or "BookStore" => "书店老板",
                "Assistant" or "Instructor" or "Trainer" => "新手指导员",
                _ => rawName
            };
            var npc = new NpcEntity
            {
                Uid = 100000 + r.GetInt32(0), Name = cnName, MapId = num,
                X = r.GetInt32(2), Y = r.GetInt32(3), Appr = r.IsDBNull(5) ? 0 : r.GetInt32(5)
            };
            if (!r.IsDBNull(6)) ApplyScript(npc, r.GetString(6), items);
            if (npc.Goods.Count > 0) rep.NpcsWithGoods++;
            list.Add(npc);
            rep.NpcsLoaded++;
        }
        return list;
    }

    /// <summary>解析官方 market_def 脚本：[@页面] 取 #SAY 对话 (遇 #ELSEACT goto 则跟随目标页)，[goods] 取在售货品。</summary>
    internal static void ApplyScript(NpcEntity npc, string content,
        Dictionary<string, (string Cn, int Price, int Weight, int Dura)> items)
    {
        var sections = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
        List<string>? cur = null;
        foreach (var raw in content.Replace("\r", "").Split('\n'))
        {
            var line = raw.TrimEnd();
            var m = Regex.Match(line.Trim(), @"^\[(.+)\]$");
            if (m.Success) { cur = new List<string>(); sections[m.Groups[1].Value.Trim()] = cur; continue; }
            cur?.Add(line);
        }

        foreach (var name in sections.Keys.Where(k => k.StartsWith('@')))
        {
            string text = BuildPageText(sections, name, 0);
            if (text.Length > 0) npc.ScriptPages[name] = text;
        }

        if (sections.TryGetValue("goods", out var goods))
        {
            int gid = 1;
            foreach (var line in goods)
            {
                var t = line.Trim();
                if (t.Length == 0 || t.StartsWith(';')) continue;
                string itemName = t.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries)[0];
                if (!items.TryGetValue(itemName, out var it)) continue; // 货品名不在 StdItems 的行不上架
                npc.Goods.Add(new MerchantGoods { GoodsId = gid++, Name = it.Cn, Price = it.Price, Weight = it.Weight, MaxDura = it.Dura });
            }
        }
    }

    private static string BuildPageText(Dictionary<string, List<string>> sections, string page, int depth)
    {
        if (depth > 4 || !sections.TryGetValue(page, out var lines)) return "";
        // 带 #IF/#ELSEACT goto 的页面：条件分支通常是拒绝文案，主流程在 goto 目标页
        for (int i = 0; i < lines.Count; i++)
        {
            if (lines[i].Trim().Equals("#ELSEACT", StringComparison.OrdinalIgnoreCase) && i + 1 < lines.Count)
            {
                var g = Regex.Match(lines[i + 1].Trim(), @"^goto\s+(@\S+)", RegexOptions.IgnoreCase);
                if (g.Success) return BuildPageText(sections, g.Groups[1].Value, depth + 1);
            }
        }
        var sb = new System.Text.StringBuilder();
        bool inSay = lines.All(l => !l.Trim().Equals("#SAY", StringComparison.OrdinalIgnoreCase)); // 无 #SAY 标记则整页即对话
        foreach (var l in lines)
        {
            var t = l.Trim();
            if (t.Equals("#SAY", StringComparison.OrdinalIgnoreCase)) { inSay = true; continue; }
            if (t.StartsWith('#')) { if (sb.Length > 0 && !t.Equals("#SAY", StringComparison.OrdinalIgnoreCase)) break; inSay = false; continue; }
            if (!inSay || t.StartsWith(';') || t == "<>") continue;
            sb.Append(t);
        }
        return sb.ToString();
    }
}
