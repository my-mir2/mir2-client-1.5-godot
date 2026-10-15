using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Rules;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 怪物刷新规则配置模型
/// 对齐 IDA 0x4ADF04 (TUserEngine.RegenMonsters)
/// </summary>
public sealed class MonsterSpawnRule
{
    public int RuleId { get; init; }
    public string MonsterName { get; init; } = string.Empty;
    public int MapId { get; init; }
    public int CenterX { get; init; }
    public int CenterY { get; init; }
    public int Radius { get; init; }
    public int MaxCount { get; init; }
    public int ActiveCount { get; set; }
    public int BaseHp { get; init; } = 100;
    public int Level { get; init; } = 0;
    /// <summary>官方 Monster.DB 英文键 (如 Zombie3)。非空时属性/外观按该键精确取库，避免同中文名的数字变体串数据。</summary>
    public string OfficialKey { get; init; } = string.Empty;
    /// <summary>Mongen 的“时间(分)”：怪物被杀后经过该时长才补刷；0 表示立即补刷 (单元测试默认)。</summary>
    public long IntervalMs { get; init; } = 0;
    /// <summary>首次满编是否完成 (首次开服一次性刷满，之后才按间隔补刷)。</summary>
    public bool InitialFilled { get; set; }
    /// <summary>出现缺额后允许补刷的时刻 (0 表示当前无缺额计时)。</summary>
    public long NextRefillMs { get; set; }
}

/// <summary>
/// 盛大官方 1.4/1.5 怪物仇恨权值大账本 (MonsterThreatTable)
/// 严格 1:1 对齐官方 M2Server 物理反编译代码 (0x4B966C / TargetHate):
/// - 伤害权值累积换仇
/// - 道士召唤兽 (神兽/骷髅) 嘲讽高权值优先吸引怪物转火
/// - 1.2 倍换仇门槛 (新目标必须超过当前第一仇恨 120% 才能换火，防止抽搐)
/// - 目标隐身、死亡、距离脱出视野则脱仇
/// </summary>
public sealed class MonsterThreatTable
{
    private readonly Dictionary<int, int> _hateMap = new();
    public int PrimaryTargetId { get; private set; }
    public int MaxHate { get; private set; }

    public void AddHate(int sessionId, int hateAmount, bool isTaunt = false)
    {
        if (sessionId <= 0 || hateAmount <= 0) return;
        // 神兽与骷髅嘲讽加成: 伤害 * 2 + 50 额外嘲讽权值
        int bonus = isTaunt ? (hateAmount * 2 + 50) : hateAmount;
        if (_hateMap.TryGetValue(sessionId, out int current))
        {
            _hateMap[sessionId] = current + bonus;
        }
        else
        {
            _hateMap[sessionId] = bonus;
        }

        int newHate = _hateMap[sessionId];
        if (PrimaryTargetId == 0 || PrimaryTargetId == sessionId)
        {
            PrimaryTargetId = sessionId;
            MaxHate = newHate;
        }
        else if (isTaunt || newHate > (int)(MaxHate * 1.2f))
        {
            PrimaryTargetId = sessionId;
            MaxHate = newHate;
        }
    }

    public void RemoveTarget(int sessionId)
    {
        _hateMap.Remove(sessionId);
        if (PrimaryTargetId == sessionId)
        {
            PrimaryTargetId = 0;
            MaxHate = 0;
            foreach (var kvp in _hateMap)
            {
                if (kvp.Value > MaxHate)
                {
                    MaxHate = kvp.Value;
                    PrimaryTargetId = kvp.Key;
                }
            }
        }
    }

    public void Clear()
    {
        _hateMap.Clear();
        PrimaryTargetId = 0;
        MaxHate = 0;
    }

    public bool HasTarget => PrimaryTargetId != 0;
    public int GetHate(int sessionId) => _hateMap.TryGetValue(sessionId, out int h) ? h : 0;
}

/// <summary>
/// 服务端怪物运行时实体
/// </summary>
public sealed class MonsterInstance
{
    public int Uid { get; init; }
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; } = 1;
    public int MapId { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public byte Direction { get; set; }
    public int Appr { get; set; } = 0;
    /// <summary>官方 Monster.DB 英文键，掉落表 (MonItems) 按它检索。</summary>
    public string OfficialKey { get; set; } = string.Empty;
    /// <summary>官方 Monster.DB 种族号 (Race)，0 表示无官方数据。</summary>
    public int Race { get; set; }
    /// <summary>城镇守卫类 (大刀卫士 11 / 弓箭卫士 20 / 弓箭守卫 112)：不是野怪，不得主动攻击善良玩家，只处决红名。</summary>
    public bool IsTownGuard => Race is 11 or 20 or 80 or 81 or 112 || Name.Contains("守卫") || Name.Contains("卫士");
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public int Exp { get; set; } = 0;
    public int MasterSessionId { get; set; } = 0; // 所属主人 SessionId (道士神兽/法师宝宝)
    public bool IsMad { get; set; } = false;      // 祈祷大叛变发狂标志
    public bool IsRevoltWarning { get; set; } = false; // 盛大官方 1.5 随从叛变前 60 秒预警标志
    public long TamedTickMs { get; set; } = 0;    // 诱惑之光驯服时间戳 (用于超 6 小时叛变检测)
    public int SpawnRuleId { get; init; }
    public bool IsDead => Hp <= 0;
    public bool IsButchered { get; set; } // 盛大官方 1.5 尸体割肉标记 (已被割空则不可再次割肉)
    public long CorpseExpireTimeMs { get; set; } // 尸体腐烂时间戳 (官方默认 20 秒)

    // 官方怪物 AI 属性 (IDA 0x4B966C)
    public int ViewRange { get; set; } = 5; // 官方默认视野 5 格 (0x4B966C @ +484)
    public int AttackDamageMin { get; set; } = 6;
    public int AttackDamageMax { get; set; } = 14;
    public int Defense { get; set; } = 0; // 盛大官方 1.5 怪物基础物防
    public int Mac { get; set; } = 0;     // 盛大官方 1.5 怪物基础魔防

    // 官方 Monster.DB 战斗属性 (IDA: 命中判定 RandInt(目标敏捷) >= 攻击者准确 即 Miss)
    public int Agility { get; set; } = 0;           // DB.SPEED 敏捷，0 表示无官方数据
    public int Accuracy { get; set; } = 0;          // DB.HIT 准确，0 表示无官方数据
    public int AttackIntervalMs { get; set; } = 1000; // DB.ATTACK_SPD
    public int WalkIntervalMs { get; set; } = 0;    // DB.WALK_SPD，0 表示回退旧的步长表
    public bool HasOfficialStats { get; private set; }

    /// <summary>按官方 Monster.DB 数据覆盖等级、血量、攻防、敏捷、准确与速度类字段。</summary>
    public void ApplyOfficialStats(Mir2.Shared.Data.OfficialMonsterStats s)
    {
        OfficialKey = s.Name;
        Race = s.Race;
        Appr = s.Appr; // 外观以 Monster.DB 为唯一真值
        Level = s.Level;
        MaxHp = s.Hp;
        Hp = s.Hp;
        Exp = s.Exp;
        AttackDamageMin = s.Dc;
        AttackDamageMax = Math.Max(s.Dc, s.DcMax);
        Defense = s.Ac;
        Mac = s.Mac;
        Agility = s.Speed;
        Accuracy = s.Hit;
        AttackIntervalMs = s.AttackSpeedMs > 0 ? s.AttackSpeedMs : 1000;
        WalkIntervalMs = s.WalkSpeedMs;
        HasOfficialStats = true;
    }
    public long NextActionTimeMs { get; set; }
    public int TargetSessionId { get; set; }

    // 盛大官方怪物特异 AI 状态属性
    public MonsterThreatTable ThreatTable { get; } = new();
    public bool CanMove { get; set; } = true; // 角蝇、赤月恶魔等固定不可移动
    public bool IsStoneForm { get; set; } = false; // 祖玛雕像/祖玛卫士初始石化状态
    public bool SeeInvisibility { get; set; } = false; // 赤月恶魔、各大教主破隐看穿隐身术
    public long NextSpecialSkillTimeMs { get; set; } = 0; // 特殊技能冷却计时器
    public int SpawnedSubMonsterCount { get; set; } = 0; // 角蝇当前繁殖存活的小怪数
    public int ParentMonsterUid { get; set; } = 0; // 孵化出来的子怪记录母体 UID

    // 官方 1.5 随从模式与指令 (Ctrl+A / @Rest)
    public bool IsResting { get; set; } = false;

    // 官方 1.5 困魔咒金光牢笼禁锢 (受外力打击立即破除)
    public bool IsTrappedByHolyCurse { get; set; }
    public long TrappedExpireMs { get; set; }

    // 官方 1.5 道士心灵启示透视显血
    public bool IsRevealed { get; set; }
    public long RevealedExpireMs { get; set; }
    public int RevealedBySessionId { get; set; }

    // 官方 1.5 怪物中毒状态
    public bool HasGreenPoison { get; set; }
    public bool HasRedPoison { get; set; }
    public long GreenPoisonExpireMs { get; set; }
    public long RedPoisonExpireMs { get; set; }
    public long LastPoisonTickMs { get; set; }
    public int PoisonSc { get; set; }
    public int PoisonerSessionId { get; set; } = 0;

    public void ApplyPoison(TaoistCombatEngine.PoisonType type, int durationSec, int sc, long nowMs, int poisonerSessionId = 0)
    {
        if (poisonerSessionId > 0) PoisonerSessionId = poisonerSessionId;
        if (type == TaoistCombatEngine.PoisonType.Green)
        {
            HasGreenPoison = true;
            GreenPoisonExpireMs = nowMs + durationSec * 1000L;
            LastPoisonTickMs = nowMs;
            PoisonSc = sc;
        }
        else if (type == TaoistCombatEngine.PoisonType.Red)
        {
            HasRedPoison = true;
            RedPoisonExpireMs = nowMs + durationSec * 1000L;
        }
    }
}

/// <summary>
/// 盛大官方 1.4/1.5 怪物刷新与时间片切片保护引擎 (MonsterRegenEngine)
/// 严格 1:1 对齐官方 M2Server 物理反编译代码:
/// - 刷新心跳与时间片熔断: TUserEngine.RegenMonsters @ 0x4ADF04 (MAX_TIME_SLICE = 15ms)
/// - 区域随机分布坐标公式: Center - Radius + RandInt(2 * Radius + 1)
/// </summary>
public sealed class MonsterRegenEngine
{
    // 官方 15 毫秒硬时间片保护 (防止成千上万只怪瞬发导致单帧 CPU 挂起)
    public const int MaxRegenTimeSliceMs = 15;

    private readonly MapManager _mapManager;
    private readonly List<MonsterSpawnRule> _rules = new();
    private readonly ConcurrentDictionary<int, MonsterInstance> _monsters = new();
    private readonly ConcurrentDictionary<int, MonsterInstance> _corpses = new();
    private int _nextMonsterUid = 100_000;

    public IReadOnlyList<MonsterSpawnRule> Rules => _rules;
    public int TotalActiveMonsters => _monsters.Count;
    public int TotalActiveCorpses => _corpses.Count;
    public IEnumerable<MonsterInstance> AllActiveMonsters => _monsters.Values;
    public IEnumerable<MonsterInstance> ActiveMonsters => _monsters.Values;
    public IEnumerable<MonsterInstance> AllMonsters => _monsters.Values;
    public SafeZoneAndGuardEngine SafeZoneEngine { get; set; } = new();

    public void AddMonster(MonsterInstance monster)
    {
        _monsters[monster.Uid] = monster;
    }

    public static int ResolveMonsterLevel(string monsterName, int baseHp, int ruleLevel = 0)
    {
        if (ruleLevel > 0) return ruleLevel;
        if (string.IsNullOrWhiteSpace(monsterName)) return 1;

        if (monsterName.Contains("赤月恶魔") || monsterName.Contains("Demon")) return 60;
        if (monsterName.Contains("祖玛教主")) return 50;
        if (monsterName.Contains("沃玛教主")) return 45;
        if (monsterName.Contains("触龙神")) return 45;
        if (monsterName.Contains("祖玛卫士")) return 40;
        if (monsterName.Contains("祖玛雕像")) return 37;
        if (monsterName.Contains("白野猪")) return 35;
        if (monsterName.Contains("神兽")) return 35;
        if (monsterName.Contains("邪恶钳虫")) return 32;
        if (monsterName.Contains("虎卫")) return 30;
        if (monsterName.Contains("黑野猪")) return 30;
        if (monsterName.Contains("红野猪")) return 28;
        if (monsterName.Contains("钳虫")) return 26;
        if (monsterName.Contains("巨型多角虫")) return 25;
        if (monsterName.Contains("黑色恶蛆")) return 24;
        if (monsterName.Contains("变异骷髅")) return 19;
        if (monsterName.Contains("尸王") || monsterName.Contains("僵尸")) return 18;
        if (monsterName.Contains("骷髅")) return 15;
        if (monsterName.Contains("蝎子")) return 12;
        if (monsterName.Contains("森林雪人") || monsterName.Contains("食人花") || monsterName.Contains("毒蜘蛛")) return 10;
        if (monsterName.Contains("半兽人")) return 9;
        if (monsterName.Contains("钉耙猫")) return 6;
        if (monsterName.Contains("多钩猫")) return 5;
        if (monsterName.Contains("蛤蟆")) return 4;
        if (monsterName.Contains("稻草人")) return 3;
        if (monsterName.Contains("鹿")) return 2;
        if (monsterName.Contains("鸡")) return 1;

        if (baseHp >= 1000) return 35;
        if (baseHp >= 500) return 25;
        if (baseHp >= 200) return 18;
        if (baseHp >= 100) return 10;
        return Math.Max(1, baseHp / 10);
    }

    public event Action<MonsterInstance>? MonsterSpawned;

    public static int ResolveMonsterAppr(string monsterName)
    {
        if (string.IsNullOrWhiteSpace(monsterName)) return 26; // 默认钉耙猫

        // 优先全量检索盛大官方 Monster.DB 185 种怪物物理真值
        int dbAppr = Mir2.Shared.Data.OfficialMonsterDBRegistry.ResolveAppr(monsterName, -1);
        if (dbAppr >= 0) return dbAppr;

        if (monsterName.Contains("大刀") || monsterName.Contains("护卫") || monsterName.Contains("守卫") && !monsterName.Contains("弓箭")) return 2; // Mon1 (2*280 = 560 大刀卫士)
        if (monsterName.Contains("弓箭")) return 0; // Mon1 (0*280 = 0 弓箭守卫)
        if (monsterName.Contains("多钩猫")) return 25; // Mon3 (5*360 = 1800)
        if (monsterName.Contains("钉耙猫")) return 26; // Mon3 (6*360 = 2160)
        if (monsterName.Contains("稻草人")) return 27; // Mon3 (7*360 = 2520)
        if (monsterName.Contains("蛤蟆")) return 28; // Mon3 (8*360 = 2880)
        if (monsterName.Contains("毒蜘蛛")) return 28; // Mon3 (8*360 = 2880)
        if (monsterName.Contains("洞蛆")) return 24; // Mon3 (4*360 = 1440)
        if (monsterName.Contains("半兽人")) return 100; // Mon11 (0*360 = 0, Oma 经典木棒豹皮半兽人)
        if (monsterName.Contains("半兽战士")) return 101; // Mon11 (1*360 = 360, OmaFighter 铁斧半兽战士)
        if (monsterName.Contains("半兽勇士") || monsterName.Contains("半兽战将")) return 102; // Mon11 (2*360 = 720, OmaWarrior 铠甲石斧半兽勇士)
        if (monsterName.Contains("森林雪人") || monsterName.Contains("雪人")) return 10; // Mon2
        if (monsterName.Contains("红蛇")) return 36; // Mon4 (6*360 = 2160)
        if (monsterName.Contains("骷髅")) return 20; // Mon3 (0*360 = 0)
        if (monsterName.Contains("虎蛇")) return 38; // Mon4 (8*360 = 2880)
        if (monsterName.Contains("楔蛾")) return 39; // Mon4 (9*360 = 3240)
        if (monsterName.Contains("蜈蚣")) return 45; // Mon5 (5*360 = 1800)
        if (monsterName.Contains("黑色恶蛆")) return 46; // Mon5 (6*360 = 2160)
        if (monsterName.Contains("红野猪")) return 110; // Mon12 (0*360 = 0)
        if (monsterName.Contains("黑野猪")) return 111; // Mon12 (1*360 = 360)
        if (monsterName.Contains("白野猪")) return 112; // Mon12 (2*360 = 720)
        if (monsterName.Contains("鸡")) return 3; // Mon1
        if (monsterName.Contains("鹿")) return 4; // Mon1
        if (monsterName.Contains("神兽")) return 170; // Mon18 (0*350 = 0)
        return 26; // 默认钉耙猫
    }


    /// <summary>
    /// 动态生成怪物实体并加入世界 (用于祖玛教主狂暴召唤等)
    /// </summary>
    public MonsterInstance SpawnMonsterDirect(int mapId, int x, int y, string monsterName, int baseHp = 500, int minDmg = 20, int maxDmg = 40)
    {
        int uid = 70000 + Random.Shared.Next(20000);
        var monster = new MonsterInstance
        {
            Uid = uid,
            MapId = mapId,
            X = x,
            Y = y,
            Name = monsterName,
            Level = ResolveMonsterLevel(monsterName, baseHp),
            Hp = baseHp,
            MaxHp = baseHp,
            AttackDamageMin = minDmg,
            AttackDamageMax = maxDmg,
            CanMove = true
        };
        _monsters[uid] = monster;
        return monster;
    }

    /// <summary>GM/测试用：在指定位置按官方属性表立即刷出一只怪（无刷怪规则，击杀后不会重生）。名称未能对应官方属性时返回 null。</summary>
    public MonsterInstance? SpawnOfficialNow(int mapId, int x, int y, string monsterName, Random rng)
    {
        var stats = Mir2.Shared.Data.OfficialMonsterNameMap.Resolve(monsterName);
        if (stats == null) return null;
        int uid = System.Threading.Interlocked.Increment(ref _nextMonsterUid);
        var monster = new MonsterInstance
        {
            Uid = uid, Name = monsterName, MapId = mapId, X = x, Y = y,
            Level = ResolveMonsterLevel(monsterName, 100, 0), Appr = ResolveMonsterAppr(monsterName),
            Direction = (byte)rng.Next(8), Hp = 100, MaxHp = 100, CanMove = true
        };
        monster.ApplyOfficialStats(stats);
        _monsters[uid] = monster;
        return monster;
    }
    public MonsterRegenEngine(MapManager mapManager)
    {
        _mapManager = mapManager;
    }

    /// <summary>
    /// 注册刷怪区域规则
    /// </summary>
    public void AddRule(MonsterSpawnRule rule)
    {
        lock (_rules)
        {
            _rules.Add(rule);
        }
    }

    /// <summary>清空全部刷怪规则 (生产启动用数据库规则整体替换时调用)。</summary>
    public void ClearRules()
    {
        lock (_rules) { _rules.Clear(); }
    }

    public int RuleCount { get { lock (_rules) { return _rules.Count; } } }

    public MonsterSpawnRule? FindRule(int ruleId)
    {
        lock (_rules)
        {
            for (int i = 0; i < _rules.Count; i++)
                if (_rules[i].RuleId == ruleId) return _rules[i];
            return null;
        }
    }

    /// <summary>
    /// 地图激活集合提供者。为 null 时所有规则始终刷怪 (单元测试默认行为)；
    /// 非 null 时仅为返回集合内的地图刷怪 —— 官方全图共约 3 万只怪，生产环境只模拟有玩家 (或预热) 的地图。
    /// </summary>
    public Func<IEnumerable<int>>? ActiveMapProvider { get; set; }

    private readonly ConcurrentDictionary<int, byte> _forcedMaps = new();

    /// <summary>玩家换图进入某地图时立即激活并同步刷出该图怪物，保证随后下发的视野快照里已有怪物（否则要等下个心跳，快照为空）。</summary>
    public void PopulateMapNow(int mapId)
    {
        if (ActiveMapProvider == null) return;
        _forcedMaps[mapId] = 1;
        ProcessRegenTick(OfficialClientRulesEngine.DelphiRandom.Shared, enforceTimeSlice: false);
    }

    /// <summary>
    /// 执行全服刷怪心跳处理 (包含官方 15ms 时间片硬熔断保护)
    /// 返回本次刷出的怪物数量及是否因耗时超限而触发熔断
    /// </summary>
    public (int spawnedCount, bool sliceTimeout) ProcessRegenTick(OfficialClientRulesEngine.DelphiRandom rng, bool enforceTimeSlice = true)
    {
        long startTick = Environment.TickCount64;
        int totalSpawned = 0;
        bool sliceTimeout = false;

        HashSet<int>? activeMaps = ActiveMapProvider != null ? new HashSet<int>(System.Linq.Enumerable.Concat(ActiveMapProvider(), _forcedMaps.Keys)) : null;
        lock (_rules)
        {
            for (int r = 0; r < _rules.Count; r++)
            {
                var rule = _rules[r];
                if (activeMaps != null && !activeMaps.Contains(rule.MapId)) continue;
                var map = _mapManager.GetMap(rule.MapId);
                if (map == null) continue;

                int needSpawn = rule.MaxCount - rule.ActiveCount;
                if (needSpawn <= 0) { rule.NextRefillMs = 0; continue; }
                if (rule.InitialFilled && rule.IntervalMs > 0)
                {
                    long nowRefill = Environment.TickCount64;
                    if (rule.NextRefillMs == 0) rule.NextRefillMs = nowRefill + rule.IntervalMs; // 出现缺额，开始计时
                    if (nowRefill < rule.NextRefillMs) continue;
                }
                rule.NextRefillMs = 0;
                int attempts = 0;
                int maxAttempts = needSpawn * 30 + 30; // 落点不可走时的重试上限，超限则留待下次心跳，杜绝死循环
                while (needSpawn > 0 && attempts++ < maxAttempts)
                {
                    // 官方坐标随机公式 (0x4ADF04 @ L460~L466):
                    // targetX = CenterX - Radius + RandInt(2 * Radius + 1)
                    // targetY = CenterY - Radius + RandInt(2 * Radius + 1)
                    int span = 2 * rule.Radius + 1;
                    int targetX = rule.CenterX - rule.Radius + rng.Next(span);
                    int targetY = rule.CenterY - rule.Radius + rng.Next(span);

                    // 配合地图物理阻挡检测 (水面/墙壁/边界不可刷怪)
                    if (map.IsWalkable(targetX, targetY))
                    {
                        int uid = System.Threading.Interlocked.Increment(ref _nextMonsterUid);
                        var monster = new MonsterInstance
                        {
                            Uid = uid,
                            Name = rule.MonsterName,
                            Level = ResolveMonsterLevel(rule.MonsterName, rule.BaseHp, rule.Level),
                            Appr = ResolveMonsterAppr(rule.MonsterName),
                            MapId = rule.MapId,
                            X = targetX,
                            Y = targetY,
                            Direction = (byte)rng.Next(8),
                            Hp = rule.BaseHp,
                            MaxHp = rule.BaseHp,
                            SpawnRuleId = rule.RuleId,
                            // 官方怪物特异初始化规则 (祖玛全系含教主初始石化)
                            IsStoneForm = rule.MonsterName.Contains("祖玛"),
                            CanMove = !rule.MonsterName.Contains("角蝇") && !rule.MonsterName.Contains("赤月恶魔") && !rule.MonsterName.Contains("Demon"),
                            SeeInvisibility = rule.MonsterName.Contains("赤月恶魔") || rule.MonsterName.Contains("教主") || rule.MonsterName.Contains("Demon")
                                           || rule.MonsterName.Contains("蛾") || rule.MonsterName.Contains("月魔") || rule.MonsterName.Contains("角蝇")
                                           || rule.MonsterName.Contains("蝙蝠") || rule.MonsterName.Contains("触龙神") || rule.MonsterName.Contains("魔王") || rule.MonsterName.Contains("黑锷")
                        };

                        Mir2.Shared.Data.OfficialMonsterStats? officialStats = null;
                        if (!string.IsNullOrEmpty(rule.OfficialKey))
                            Mir2.Shared.Data.OfficialMonsterStatsTable.Current.TryGetValue(rule.OfficialKey, out officialStats);
                        officialStats ??= Mir2.Shared.Data.OfficialMonsterNameMap.Resolve(rule.MonsterName);
                        if (officialStats != null) monster.ApplyOfficialStats(officialStats);

                        _monsters[uid] = monster;
                        rule.ActiveCount++;
                        needSpawn--;
                        totalSpawned++;
                        MonsterSpawned?.Invoke(monster);
                    }

                    // 官方时间片硬熔断检测 (0x4ADF04 @ L470: GetTickCount() - TickCount > *off_4E9FD4)
                    if (enforceTimeSlice && Environment.TickCount64 - startTick > MaxRegenTimeSliceMs)
                    {
                        sliceTimeout = true;
                        return (totalSpawned, sliceTimeout);
                    }
                }
                if (needSpawn <= 0) rule.InitialFilled = true;
            }
        }

        return (totalSpawned, sliceTimeout);
    }

    /// <summary>
    /// 处理怪物死亡下线并释放刷新配额，转入尸体列表保留 20 秒 (供挖肉与倒地表现)
    /// </summary>
    public MonsterInstance? OnMonsterDied(int monsterUid)
    {
        if (_monsters.TryRemove(monsterUid, out var monster))
        {
            lock (_rules)
            {
                var rule = _rules.Find(r => r.RuleId == monster.SpawnRuleId);
                if (rule != null && rule.ActiveCount > 0)
                {
                    rule.ActiveCount--;
                }
            }

            // 回收母体子怪计数 (角蝇小蝙蝠死亡回收)
            if (monster.ParentMonsterUid != 0 && _monsters.TryGetValue(monster.ParentMonsterUid, out var parent))
            {
                parent.SpawnedSubMonsterCount = Math.Max(0, parent.SpawnedSubMonsterCount - 1);
            }

            // 官方默认保留 20 秒尸体 (CorpseRemainSec)
            monster.CorpseExpireTimeMs = Environment.TickCount64 + 20_000;
            _corpses[monsterUid] = monster;
            return monster;
        }
        return null;
    }

    /// <summary>
    /// 盛大官方 1.5 彻底注销并清除怪物或随从实例 (如随从消散/断线回收，不留尸体，直接抹除)
    /// </summary>
    public bool RemoveMonster(int monsterUid)
    {
        bool removed = _monsters.TryRemove(monsterUid, out var m);
        _corpses.TryRemove(monsterUid, out _);
        if (removed && m != null)
        {
            lock (_rules)
            {
                var rule = _rules.Find(r => r.RuleId == m.SpawnRuleId);
                if (rule != null && rule.ActiveCount > 0)
                {
                    rule.ActiveCount--;
                }
            }
        }
        return removed;
    }

    public MonsterInstance? GetMonster(int monsterUid)
    {
        _monsters.TryGetValue(monsterUid, out var m);
        return m;
    }

    public MonsterInstance? FindMonsterByUid(int monsterUid)
    {
        if (_monsters.TryGetValue(monsterUid, out var m)) return m;
        if (_corpses.TryGetValue(monsterUid, out var c)) return c;
        return null;
    }

    public void AddCorpse(MonsterInstance corpse)
    {
        corpse.CorpseExpireTimeMs = Environment.TickCount64 + 150_000;
        _corpses[corpse.Uid] = corpse;
    }

    public MonsterInstance? FindCorpse(int monsterUid)
    {
        if (_corpses.TryGetValue(monsterUid, out var c)) return c;
        if (_monsters.TryGetValue(monsterUid, out var m) && m.IsDead) return m;
        return null;
    }

    /// <summary>
    /// 检索指定地图与坐标的有效尸体 (未完全腐烂)
    /// </summary>
    public MonsterInstance? FindCorpseAt(int mapId, int x, int y)
    {
        long now = Environment.TickCount64;
        foreach (var kvp in _corpses)
        {
            var c = kvp.Value;
            if (c.MapId == mapId && c.X == x && c.Y == y && now <= c.CorpseExpireTimeMs)
            {
                return c;
            }
        }
        return null;
    }

    /// <summary>
    /// 清理已超时的腐烂尸体
    /// </summary>
    public void CleanExpiredCorpses()
    {
        long now = Environment.TickCount64;
        foreach (var kvp in _corpses)
        {
            if (now > kvp.Value.CorpseExpireTimeMs)
            {
                _corpses.TryRemove(kvp.Key, out _);
            }
        }
    }

    /// <summary>
    /// 根据地图与坐标检索处于该位置的存活怪物
    /// </summary>
    public MonsterInstance? FindMonsterAt(int mapId, int x, int y)
    {
        foreach (var kvp in _monsters)
        {
            var m = kvp.Value;
            if (!m.IsDead && m.MapId == mapId && m.X == x && m.Y == y)
            {
                return m;
            }
        }
        return null;
    }

    /// <summary>
    /// 判定目标格子是否被其他存活怪物实体占据 (防止怪物重叠)
    /// </summary>
    public bool IsTileOccupiedByOtherMonster(int mapId, int x, int y, int selfUid)
    {
        foreach (var kvp in _monsters)
        {
            var m = kvp.Value;
            if (!m.IsDead && m.Uid != selfUid && m.MapId == mapId && m.X == x && m.Y == y)
            {
                return true;
            }
        }
        return false;
    }

    public List<MonsterInstance> GetMonstersInArea(int mapId, int minX, int minY, int maxX, int maxY)
    {
        var list = new List<MonsterInstance>();
        foreach (var kvp in _monsters)
        {
            var m = kvp.Value;
            if (!m.IsDead && m.MapId == mapId && m.X >= minX && m.X <= maxX && m.Y >= minY && m.Y <= maxY)
            {
                list.Add(m);
            }
        }
        return list;
    }

    public readonly record struct MonsterAiActionResult(
        int MonsterUid,
        string MonsterName,
        int ActionType, // 0=追击移动, 1=近战攻击, 2=闲置巡逻, 3=远程喷毒, 4=全屏地刺, 5=孵化吐怪, 6=破石苏醒
        int X,
        int Y,
        byte Direction,
        int TargetSessionId,
        int DamageDealt,
        string Extra = "",
        int MapId = 0,
        bool ShieldBroken = false,
        bool Revived = false,
        bool Died = false
    );

    /// <summary>
    /// 执行全服怪物 AI 核心心跳 (IDA 0x4B1638 ProcessMonsters & 0x4B966C)
    /// 严格 1:1 闭环官方特异行为树:
    /// - 仇恨大账本锁定、换仇 (1.2倍门槛) 与隐身/距离脱仇
    /// - 祖玛雕像/祖玛卫士: 初始石化 (IsStoneForm), 玩家进入 3 格内或承伤时破石苏醒
    /// - 毒蜘蛛: 2~4 格远程喷吐绿毒 (清空血池并持续扣血)
    /// - 契蛾 / 蝙蝠 / 洞蛆: 15% 几率近战麻痹石化 (定身无法行动 3 秒)
    /// - 角蝇: 固定原地不可移动, 每 2.5 秒吐出小蝙蝠 (最多维持 10 只)
    /// - 赤月恶魔: 固定原地不可移动, 破隐全屏 15 格穿刺地刺群伤
    /// </summary>
    public (List<MonsterAiActionResult> actions, bool sliceTimeout) ProcessMonsterAiTick(
        Func<int, int, int, List<ClientSession>> getSessionsInNinePatch,
        OfficialClientRulesEngine.DelphiRandom rng)
    {
        long startTick = Environment.TickCount64;
        var actions = new List<MonsterAiActionResult>();
        bool sliceTimeout = false;

        foreach (var kvp in _monsters)
        {
            var monster = kvp.Value;
            if (monster.IsDead) continue;

            long now = Environment.TickCount64;
            if (now < monster.NextActionTimeMs) continue;

            var map = _mapManager.GetMap(monster.MapId);
            if (map == null) continue;

            var nearbyPlayers = getSessionsInNinePatch(monster.MapId, monster.X, monster.Y);

            // ==================== 1. 祖玛雕像/祖玛卫士 破石苏醒状态机 ====================
            if (monster.IsStoneForm)
            {
                bool wakeUp = monster.ThreatTable.HasTarget; // 受到外界攻击立即苏醒
                if (!wakeUp)
                {
                    for (int i = 0; i < nearbyPlayers.Count; i++)
                    {
                        var p = nearbyPlayers[i];
                        if (p.State == null || p.State.Hp <= 0 || p.State.IsInvisible) continue;
                        int dist = Math.Max(Math.Abs(p.X - monster.X), Math.Abs(p.Y - monster.Y));
                        if (dist <= 3) // 官方 3 格接近唤醒距离
                        {
                            wakeUp = true;
                            monster.ThreatTable.AddHate(p.SessionId, 1);
                            break;
                        }
                    }
                }

                if (wakeUp)
                {
                    monster.IsStoneForm = false;
                    monster.NextActionTimeMs = now + 800; // 苏醒硬直后摇
                    actions.Add(new MonsterAiActionResult(
                        monster.Uid, monster.Name, ActionType: 6,
                        monster.X, monster.Y, monster.Direction,
                        monster.ThreatTable.PrimaryTargetId, DamageDealt: 0,
                        Extra: "破石苏醒",
                        MapId: monster.MapId
                    ));
                }
                continue; // 石化状态本帧不执行后续行动
            }

            // ==================== 2. 赤月恶魔 全屏地刺面伤 ====================
            if (monster.Name.Contains("赤月恶魔") || monster.Name.Contains("Demon"))
            {
                monster.CanMove = false;
                monster.SeeInvisibility = true;
                if (now >= monster.NextSpecialSkillTimeMs)
                {
                    bool attacked = false;
                    for (int i = 0; i < nearbyPlayers.Count; i++)
                    {
                        var p = nearbyPlayers[i];
                        if (p.State == null || p.State.Hp <= 0) continue;
                        int dist = Math.Max(Math.Abs(p.X - monster.X), Math.Abs(p.Y - monster.Y));
                        if (dist <= 15) // 官方全屏 15 格地刺判定
                        {
                            if (SafeZoneEngine.IsInSafeZone(p.CurrentMapId, p.X, p.Y)) continue; // 安全区禁武防护
                            int rawSpike = rng.NextRange(monster.AttackDamageMin, monster.AttackDamageMax);
                            int spikeDmg = BossEncounterEngine.CalculateRedMoonDemonGroundSpikeDamage(rawSpike, p.State);
                            var (finalHp, finalMp, died, revived, absorbed, shieldBroken) = p.State.TakeCombatDamageFull(spikeDmg, now);
                            if (spikeDmg > 0 && !died)
                            {
                                ItemLifecycleEngine.DeductArmorDurabilityOnDamage(p.State, Random.Shared);
                            }
                            actions.Add(new MonsterAiActionResult(
                                monster.Uid, monster.Name, ActionType: 4,
                                monster.X, monster.Y, monster.Direction,
                                p.SessionId, DamageDealt: spikeDmg,
                                Extra: "全屏地刺",
                                MapId: monster.MapId,
                                ShieldBroken: shieldBroken,
                                Revived: revived,
                                Died: died
                            ));
                            attacked = true;
                        }
                    }
                    if (attacked)
                    {
                        monster.NextSpecialSkillTimeMs = now + 2000; // 地刺冷却 2.0 秒
                        monster.NextActionTimeMs = now + 1200;
                        continue;
                    }
                }
            }

            // ==================== 3. 角蝇 吐出小蝙蝠特异行为 ====================
            if ((monster.Name == "角蝇" || monster.Name == "HouseFly") && !monster.Name.Contains("蝙蝠"))
            {
                monster.CanMove = false;
                if (now >= monster.NextSpecialSkillTimeMs && monster.SpawnedSubMonsterCount < 10)
                {
                    // 只要视野内有活人，每 2.5 秒吐出一只小蝙蝠
                    bool hasActivePlayer = false;
                    for (int i = 0; i < nearbyPlayers.Count; i++)
                    {
                        var player = nearbyPlayers[i];
                        if (player?.State != null && player.State.Hp > 0)
                        {
                            hasActivePlayer = true;
                            break;
                        }
                    }

                    if (hasActivePlayer)
                    {
                        for (int d = 0; d < 8; d++)
                        {
                            var (offX, offY) = GetDirOffset((byte)d);
                            int batX = monster.X + offX;
                            int batY = monster.Y + offY;
                            if (map.IsWalkable(batX, batY) && !IsTileOccupiedByOtherMonster(monster.MapId, batX, batY, monster.Uid))
                            {
                                int batUid = System.Threading.Interlocked.Increment(ref _nextMonsterUid);
                                var bat = new MonsterInstance
                                {
                                    Uid = batUid,
                                    Name = "角蝇小蝙蝠",
                                    MapId = monster.MapId,
                                    X = batX,
                                    Y = batY,
                                    Direction = (byte)d,
                                    Hp = 15,
                                    MaxHp = 15,
                                    AttackDamageMin = 3,
                                    AttackDamageMax = 8,
                                    SpawnRuleId = 0,
                                    ParentMonsterUid = monster.Uid,
                                    NextActionTimeMs = now + 1000 // 刚孵化落地出生硬直 1 秒
                                };
                                _monsters[batUid] = bat;
                                monster.SpawnedSubMonsterCount++;
                                monster.NextSpecialSkillTimeMs = now + 2500;
                                monster.NextActionTimeMs = now + 1000;
                                actions.Add(new MonsterAiActionResult(
                                    monster.Uid, monster.Name, ActionType: 5,
                                    batX, batY, (byte)d,
                                    0, DamageDealt: 0,
                                    Extra: "吐出小蝙蝠",
                                    MapId: monster.MapId
                                ));
                                break;
                            }
                        }
                        continue;
                    }
                }
            }

            // ==================== 3.85 盛大官方 1.5 规则: 诱惑随从叛变前 60 秒野性骚动变色预警 ====================
            if (monster.MasterSessionId > 0 && !monster.IsMad && !monster.IsRevoltWarning && monster.TamedTickMs > 0
                && (now - monster.TamedTickMs >= 6 * 3600 * 1000L - 60 * 1000L)
                && !monster.Name.Contains("神兽") && !monster.Name.Contains("变异骷髅"))
            {
                monster.IsRevoltWarning = true;
                var master = nearbyPlayers.Find(p => p.SessionId == monster.MasterSessionId);
                if (master != null)
                {
                    master.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"【野性骚动】你的随从【{monster.Name}】眼神泛起凶光，似乎即将摆脱驯化（60秒内可能叛变）！");
                }
            }

            // ==================== 3.9 盛大官方 1.5 规则: 诱惑之光驯服随从存活超 6 小时发狂叛变 (神兽与变异骷髅严格豁免) ====================
            if (monster.MasterSessionId > 0 && !monster.IsMad && monster.TamedTickMs > 0 && (now - monster.TamedTickMs >= 6 * 3600 * 1000L)
                && !monster.Name.Contains("神兽") && !monster.Name.Contains("变异骷髅"))
            {
                monster.IsMad = true;
                int formerMasterId = monster.MasterSessionId;
                monster.MasterSessionId = 0;
                monster.AttackDamageMin = (int)(monster.AttackDamageMin * 1.5);
                monster.AttackDamageMax = (int)(monster.AttackDamageMax * 1.5);
                monster.ThreatTable.AddHate(formerMasterId, 100, isTaunt: true); // 优先反噬前主人

                // 生成随从发狂叛变行为记录，供同屏广播红名并跨地图清除前主人 Pets 列表
                actions.Add(new MonsterAiActionResult(
                    monster.Uid, monster.Name, ActionType: 7, // 7: 随从发狂叛变
                    monster.X, monster.Y, monster.Direction,
                    formerMasterId, DamageDealt: 0,
                    Extra: "随从叛变",
                    MapId: monster.MapId
                ));

                var formerMaster = nearbyPlayers.Find(p => p.SessionId == formerMasterId);
                if (formerMaster != null)
                {
                    formerMaster.Pets.RemoveAll(pet => pet.Uid == monster.Uid);
                    formerMaster.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"【随从叛变】你的随从【{monster.Name}】驯化时效已达上限(6小时)，失去理智陷入狂暴叛变！");
                }
            }

            // ==================== 4. 仇恨列表第一目标检索与脱仇判定 ====================
            ClientSession? targetPlayer = null;
            if (monster.ThreatTable.HasTarget)
            {
                int primaryId = monster.ThreatTable.PrimaryTargetId;
                for (int i = 0; i < nearbyPlayers.Count; i++)
                {
                    if (nearbyPlayers[i].SessionId == primaryId)
                    {
                        var candidate = nearbyPlayers[i];
                        if (candidate.State != null && candidate.State.Hp > 0)
                        {
                            int dist = Math.Max(Math.Abs(candidate.X - monster.X), Math.Abs(candidate.Y - monster.Y));
                            // 距离过远脱仇 (ViewRange + 4)
                            if (dist > monster.ViewRange + 4)
                            {
                                monster.ThreatTable.RemoveTarget(primaryId);
                            }
                            // 隐身脱仇 (道士隐身术/隐身戒指生效且怪不破隐)
                            else if (candidate.State.IsInvisible && !monster.SeeInvisibility)
                            {
                                monster.ThreatTable.RemoveTarget(primaryId);
                            }
                            else if (monster.IsTownGuard && !candidate.State.IsRedName)
                            {
                                monster.ThreatTable.RemoveTarget(primaryId);
                            }
                            else
                            {
                                targetPlayer = candidate;
                            }
                        }
                        else
                        {
                            monster.ThreatTable.RemoveTarget(primaryId);
                        }
                        break;
                    }
                }
            }

            // ==================== 5. 视野自然索敌 (寻找最近未隐身玩家) ====================
            if (targetPlayer == null)
            {
                int closestDist = int.MaxValue;
                for (int i = 0; i < nearbyPlayers.Count; i++)
                {
                    var p = nearbyPlayers[i];
                    if (p.State == null || p.State.Hp <= 0) continue;
                    if (p.State.IsInvisible && !monster.SeeInvisibility) continue;
                    if (monster.MasterSessionId > 0 && !monster.IsMad && p.SessionId == monster.MasterSessionId) continue; // 忠诚随从绝不主动攻击主人
                    if (monster.IsTownGuard && !p.State.IsRedName) continue; // 守卫只针对红名，不打善良玩家

                    int chebyshevDist = Math.Max(Math.Abs(p.X - monster.X), Math.Abs(p.Y - monster.Y));
                    if (chebyshevDist <= monster.ViewRange && chebyshevDist < closestDist)
                    {
                        closestDist = chebyshevDist;
                        targetPlayer = p;
                    }
                }

                if (targetPlayer != null)
                {
                    monster.ThreatTable.AddHate(targetPlayer.SessionId, 1);
                }
            }

            bool IsOccupiedByMonsterOrPlayer(int checkX, int checkY)
            {
                if (IsTileOccupiedByOtherMonster(monster.MapId, checkX, checkY, monster.Uid))
                    return true;
                for (int i = 0; i < nearbyPlayers.Count; i++)
                {
                    var p = nearbyPlayers[i];
                    if (p?.State != null && p.State.Hp > 0 && p.X == checkX && p.Y == checkY)
                        return true;
                }
                return false;
            }

            // ==================== 6. 目标执行行动 (远程喷毒 / 近战撕咬 / 追击寻路) ====================
            if (targetPlayer != null)
            {
                monster.TargetSessionId = targetPlayer.SessionId;
                int dx = targetPlayer.X - monster.X;
                int dy = targetPlayer.Y - monster.Y;
                int dist = Math.Max(Math.Abs(dx), Math.Abs(dy));

                // 6.0 防重叠保护：若怪物与目标玩家意外重合在同一格 (dist == 0)，优先向相邻空地退开 1 格以便正常近战交锋
                if (dist == 0 && monster.CanMove)
                {
                    for (int d = 0; d < 8; d++)
                    {
                        var (offX, offY) = GetDirOffset((byte)d);
                        int stepX = monster.X + offX;
                        int stepY = monster.Y + offY;
                        if (map.IsWalkable(stepX, stepY) && !IsOccupiedByMonsterOrPlayer(stepX, stepY))
                        {
                            monster.X = stepX;
                            monster.Y = stepY;
                            monster.Direction = GetDirection(targetPlayer.X - stepX, targetPlayer.Y - stepY);
                            monster.NextActionTimeMs = now + (monster.WalkIntervalMs > 0 ? monster.WalkIntervalMs : GetMonsterStepIntervalMs(monster.Name));
                            actions.Add(new MonsterAiActionResult(
                                monster.Uid, monster.Name, ActionType: 0,
                                monster.X, monster.Y, monster.Direction,
                                targetPlayer.SessionId, DamageDealt: 0,
                                Extra: "",
                                MapId: monster.MapId
                            ));
                            break;
                        }
                    }
                    continue;
                }

                // 6.1 毒蜘蛛远程喷毒攻击 (距离 2~4 格且 CD 到达)
                if (monster.Name.Contains("毒蜘蛛") && dist >= 2 && dist <= 4 && now >= monster.NextSpecialSkillTimeMs)
                {
                    monster.Direction = GetDirection(dx, dy);

                    // 权威安全区与视线障碍双重仲裁 (杜绝穿墙喷毒与安全区伤人)
                    if (!SafeZoneEngine.IsInSafeZone(targetPlayer.CurrentMapId, targetPlayer.X, targetPlayer.Y) &&
                        map.CheckLineOfSight(monster.X, monster.Y, targetPlayer.X, targetPlayer.Y))
                    {
                        int rawPoison = rng.NextRange(monster.AttackDamageMin, monster.AttackDamageMax);
                        int poisonDmg = OfficialClientRulesEngine.CombatMath.CalculateMagicDamage(rawPoison, rawPoison, targetPlayer.State!.Mac, 0, rng);
                        var (finalHp, finalMp, died, revived, absorbed, shieldBroken) = targetPlayer.State.TakeCombatDamageFull(poisonDmg, now);
                        if (poisonDmg > 0 && !died)
                        {
                            ItemLifecycleEngine.DeductArmorDurabilityOnDamage(targetPlayer.State, Random.Shared);
                        }

                        // 附加官方原版绿毒 (清空血池，每 2.5 秒扣血)
                        targetPlayer.State.HasGreenPoison = true;
                        targetPlayer.State.GreenPoisonExpireMs = now + 10_000;
                        targetPlayer.State.GreenPoisonSc = 5;
                        targetPlayer.State.IncHealth = 0;
                        targetPlayer.State.StatusBits[0] = true;

                        monster.NextSpecialSkillTimeMs = now + 2000;
                        monster.NextActionTimeMs = now + 1200;
                        actions.Add(new MonsterAiActionResult(
                            monster.Uid, monster.Name, ActionType: 3,
                            monster.X, monster.Y, monster.Direction,
                            targetPlayer.SessionId, DamageDealt: poisonDmg,
                            Extra: "毒液喷射",
                            MapId: monster.MapId,
                            ShieldBroken: shieldBroken,
                            Revived: revived,
                            Died: died
                        ));
                    }
                    else
                    {
                        // 处于安全区或视线被石壁阻挡，无法喷射毒液，转入动作冷却
                        monster.NextSpecialSkillTimeMs = now + 1000;
                        monster.NextActionTimeMs = now + 800;
                    }
                }
                // 6.2 邻接物理近战攻击
                else if (dist == 1)
                {
                    monster.Direction = GetDirection(dx, dy);

                    // 安全区禁武防护
                    if (!SafeZoneEngine.IsInSafeZone(targetPlayer.CurrentMapId, targetPlayer.X, targetPlayer.Y))
                    {
                        int rawDmg = rng.NextRange(monster.AttackDamageMin, monster.AttackDamageMax);
                        // IDA sub_4C1EF4 @ 0x4C2180: RandInt(目标敏捷) >= 攻击者准确 判定 Miss (仅对已载入官方属性的怪物生效)
                        bool monsterMissed = monster.HasOfficialStats && monster.Accuracy > 0
                            && rng.Next(Math.Max(1, (int)(targetPlayer.State?.Speed ?? 15))) >= monster.Accuracy;
                        int dmg = monsterMissed ? 0 : OfficialClientRulesEngine.CombatMath.CalculatePhysicalDamage(rawDmg, rawDmg, targetPlayer.State!.Ac, 0, rng);
                        var (finalHp, finalMp, died, revived, absorbed, shieldBroken) = targetPlayer.State.TakeCombatDamageFull(dmg, now);
                        if (dmg > 0 && !died)
                        {
                            ItemLifecycleEngine.DeductArmorDurabilityOnDamage(targetPlayer.State, Random.Shared);
                        }

                        // 楔蛾 / 契蛾 / 月魔蜘蛛 / 洞蛆 / 蝙蝠 15% 几率近战麻痹石化
                        if ((monster.Name.Contains("楔蛾") || monster.Name.Contains("契蛾") || monster.Name.Contains("月魔") || monster.Name.Contains("洞蛆") || monster.Name.Contains("蝙蝠")) && rng.Next(100) < 15)
                        {
                            targetPlayer.State.ParalyzeExpireMs = now + 3000; // 麻痹 3 秒
                            targetPlayer.State.StatusBits[5] = true;
                        }

                        monster.NextActionTimeMs = now + (monster.HasOfficialStats ? monster.AttackIntervalMs : 1000);
                        actions.Add(new MonsterAiActionResult(
                            monster.Uid, monster.Name, ActionType: 1,
                            monster.X, monster.Y, monster.Direction,
                            targetPlayer.SessionId, DamageDealt: dmg,
                            Extra: "",
                            MapId: monster.MapId,
                            ShieldBroken: shieldBroken,
                            Revived: revived,
                            Died: died
                        ));
                    }
                    else
                    {
                        monster.NextActionTimeMs = now + 1000;
                    }
                }
                // 6.3 追击移动寻路
                else if (dist > 1 && monster.CanMove)
                {
                    // 盛大官方 1.5: 随从处于休息状态时原地待命，严禁离位追击
                    if (monster.MasterSessionId > 0 && !monster.IsMad && monster.IsResting)
                    {
                        monster.NextActionTimeMs = now + 1000;
                    }
                    else
                    {
                        var (nextX, nextY, nextDir, found) = ServerPathFinder.FindNextStep(
                            map,
                            monster.X,
                            monster.Y,
                            targetPlayer.X,
                            targetPlayer.Y,
                            (checkX, checkY) => IsTileOccupiedByOtherMonster(monster.MapId, checkX, checkY, monster.Uid),
                            maxDepth: monster.ViewRange + 2
                        );

                        if (found && (nextX != monster.X || nextY != monster.Y) && !IsOccupiedByMonsterOrPlayer(nextX, nextY))
                        {
                            monster.Direction = nextDir;
                            monster.X = nextX;
                            monster.Y = nextY;
                            monster.NextActionTimeMs = now + (monster.WalkIntervalMs > 0 ? monster.WalkIntervalMs : GetMonsterStepIntervalMs(monster.Name));
                            actions.Add(new MonsterAiActionResult(
                                monster.Uid, monster.Name, ActionType: 0,
                                monster.X, monster.Y, monster.Direction,
                                targetPlayer.SessionId, DamageDealt: 0,
                                Extra: "",
                                MapId: monster.MapId
                            ));
                        }
                    }
                }
            }
            else
            {
                monster.TargetSessionId = 0;

                // 盛大官方 1.5 随从闲置护卫逻辑:
                // 1) 处于休息状态: 原地戒备待命，绝不漫步游荡
                // 2) 处于跟随状态: 若距离主人超过 2 格，智能寻路靠近主人身侧 (1~2格)；已在身侧则保持护卫
                if (monster.MasterSessionId > 0 && !monster.IsMad)
                {
                    if (!monster.IsResting && monster.CanMove)
                    {
                        var master = nearbyPlayers.Find(p => p.SessionId == monster.MasterSessionId);
                        if (master != null)
                        {
                            int masterDist = Math.Max(Math.Abs(master.X - monster.X), Math.Abs(master.Y - monster.Y));
                            if (masterDist > 2)
                            {
                                var (nextX, nextY, nextDir, found) = ServerPathFinder.FindNextStep(
                                    map,
                                    monster.X,
                                    monster.Y,
                                    master.X,
                                    master.Y,
                                    (checkX, checkY) => IsTileOccupiedByOtherMonster(monster.MapId, checkX, checkY, monster.Uid),
                                    maxDepth: monster.ViewRange + 4
                                );

                                if (found && (nextX != monster.X || nextY != monster.Y) && !IsOccupiedByMonsterOrPlayer(nextX, nextY))
                                {
                                    monster.Direction = nextDir;
                                    monster.X = nextX;
                                    monster.Y = nextY;
                                    monster.NextActionTimeMs = now + 800;
                                    actions.Add(new MonsterAiActionResult(
                                        monster.Uid, monster.Name, ActionType: 0,
                                        monster.X, monster.Y, monster.Direction,
                                        TargetSessionId: 0, DamageDealt: 0,
                                        Extra: "跟随主人",
                                        MapId: monster.MapId
                                    ));
                                }
                            }
                        }
                    }
                    continue; // 随从处理完毕，直接跳过野生怪物漫步
                }

                // 7. 闲置漫步巡逻 (野生怪物 20% 几率随机朝 8 方向漫步一格)
                if (monster.CanMove && rng.Next(5) == 0)
                {
                    int randDir = rng.Next(8);
                    var (offX, offY) = GetDirOffset((byte)randDir);
                    int roamX = monster.X + offX;
                    int roamY = monster.Y + offY;

                    var rule = _rules.Find(r => r.RuleId == monster.SpawnRuleId);
                    bool withinRadius = rule == null || (Math.Abs(roamX - rule.CenterX) <= rule.Radius && Math.Abs(roamY - rule.CenterY) <= rule.Radius);

                    if (withinRadius && map.IsWalkable(roamX, roamY) && !IsOccupiedByMonsterOrPlayer(roamX, roamY))
                    {
                        monster.X = roamX;
                        monster.Y = roamY;
                        monster.Direction = (byte)randDir;
                        monster.NextActionTimeMs = now + 1200;
                        actions.Add(new MonsterAiActionResult(
                            monster.Uid, monster.Name, ActionType: 2,
                            monster.X, monster.Y, monster.Direction,
                            TargetSessionId: 0, DamageDealt: 0,
                            Extra: "",
                            MapId: monster.MapId
                        ));
                    }
                }
            }

            // 时间片熔断检测 (IDA 0x4B1638)
            if (Environment.TickCount64 - startTick > MaxRegenTimeSliceMs)
            {
                sliceTimeout = true;
                break;
            }
        }

        return (actions, sliceTimeout);
    }

    /// <summary>
    /// 盛大官方 1.5 8方向计算函数 (0=上, 1=右上, 2=右, 3=右下, 4=下, 5=左下, 6=左, 7=左上)
    /// </summary>
    public static byte GetDirection(int dx, int dy)
    {
        int sx = Math.Sign(dx);
        int sy = Math.Sign(dy);
        return (sx, sy) switch
        {
            (0, -1) => 0,
            (1, -1) => 1,
            (1, 0) => 2,
            (1, 1) => 3,
            (0, 1) => 4,
            (-1, 1) => 5,
            (-1, 0) => 6,
            (-1, -1) => 7,
            _ => 0
        };
    }

    public static (int dx, int dy) GetDirOffset(byte dir) => dir switch
    {
        0 => (0, -1),
        1 => (1, -1),
        2 => (1, 0),
        3 => (1, 1),
        4 => (0, 1),
        5 => (-1, 1),
        6 => (-1, 0),
        7 => (-1, -1),
        _ => (0, 0)
    };

    /// <summary>
    /// 盛大官方 1.5 怪物生态步进时钟 (IDA sub_4B966C):
    /// 恶蛆/楔蛾 400ms 极速冲刺，野猪/蜘蛛 800ms 敏捷奔跑，僵尸/骷髅 1200ms 蹒跚慢走
    /// </summary>
    public static int GetMonsterStepIntervalMs(string monsterName)
    {
        if (monsterName.Contains("恶蛆") || monsterName.Contains("狂蛆") || monsterName.Contains("楔蛾") || monsterName.Contains("月魔"))
        {
            return 400; // 极速突进
        }
        if (monsterName.Contains("野猪") || monsterName.Contains("蜘蛛") || monsterName.Contains("虎卫") || monsterName.Contains("神兽"))
        {
            return 800; // 正常奔跑
        }
        if (monsterName.Contains("僵尸") || monsterName.Contains("骷髅") || monsterName.Contains("稻草人") || monsterName.Contains("蛤蟆"))
        {
            return 1200; // 蹒跚缓步
        }
        return 800;
    }
}
