using System;
using System.Collections.Generic;
using Mir2.Shared.Models;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 传奇三大 BOSS 特殊战斗机制与狂暴 AI 引擎 (BossEncounterEngine)
/// 严格 1:1 对齐官方 M2Server 怪物底层逻辑 (AUDIT.md 细节 191 ~ 210):
/// 1. 触龙神 (EvilCentipede):
///    - 破土钻地潜伏状态机
///    - 100% 绝对魔法免疫 (Magic Immunity): 任何魔法伤害强制归 0，仅受纯物理攻击伤害
///    - 全屏大范围绿毒喷吐与大范围群体石化
/// 2. 赤月恶魔 (RedMoonDemon):
///    - 无法移动的肉山固定实体
///    - 全屏无死角地刺突刺 (受角色物理防御 AC 与魔法防御 MAC 双重减免)
/// 3. 祖玛教主 (ZumaLeader):
///    - 初始处于石化雕像休眠状态
///    - 玩家踏入 5 格警戒区触发破石而出
///    - 残血狂暴 (HP < 20%): 敲响战鼓召唤 4 尊祖玛卫士 (极品大锤) 助战
/// 4. 双头金刚 (DualHeadKong):
///    - 死后绝对不爆任何地表掉落物，必须通过 Alt 挖肉方可挖取赤月终极首饰【法神套装】(法神头盔、法神项链、法神手镯、法神戒指)
/// </summary>
public sealed class BossEncounterEngine
{
    /// <summary>
    /// 触龙神受击魔法免疫校验 (法术攻击打在触龙神身上强制为 0 伤害)
    /// </summary>
    public static int ResolveMagicDamageOnBoss(MonsterInstance monster, int rawMagicDamage)
    {
        if (monster == null) return 0;
        if (monster.Name.Contains("触龙神"))
        {
            // 触龙神百分百绝对魔免
            return 0;
        }
        return rawMagicDamage;
    }

    /// <summary>
    /// 赤月恶魔全屏地刺穿刺伤害结算 (受 AC 与 MAC 双重抵抗)
    /// </summary>
    public static int CalculateRedMoonDemonGroundSpikeDamage(int baseDamage, PlayerState player)
    {
        if (player == null) return baseDamage;
        // 官方 1.5 公式: 基础伤害减去 50% 防御与 50% 魔防
        int defenseReduction = (player.Ac + player.Mac) / 2;
        return Math.Max(5, baseDamage - defenseReduction);
    }

    /// <summary>
    /// 祖玛教主濒死狂暴召唤卫士判定 (HP < 20% 仅触发一次召唤 4 只大锤卫士)
    /// </summary>
    public static (bool shouldSummon, List<string> summonedMonsters) CheckZumaLeaderRageSummon(MonsterInstance leader)
    {
        var list = new List<string>();
        if (leader == null || !leader.Name.Contains("祖玛教主"))
        {
            return (false, list);
        }

        double hpPercent = (double)leader.Hp / Math.Max(1, leader.MaxHp);
        if (hpPercent <= 0.20 && !leader.IsMad)
        {
            leader.IsMad = true; // 标记已进入狂暴召唤阶段，防止重复召唤
            list.Add("祖玛卫士(极品大锤)");
            list.Add("祖玛卫士(极品大锤)");
            list.Add("祖玛雕像");
            list.Add("祖玛弓箭手");
            return (true, list);
        }

        return (false, list);
    }

    /// <summary>
    /// 双头金刚死后挖肉取法神机制 (死后不爆装备，必须割肉产出)
    /// </summary>
    public static (bool canButcher, EquipItem? rewardItem, string message) ButcherDualHeadKong(MonsterInstance corpse, Random rng)
    {
        if (corpse == null || !corpse.IsDead)
        {
            return (false, null, "生物尚未死亡。");
        }

        if (!corpse.Name.Contains("双头金刚"))
        {
            return (false, null, "非双头金刚尸体。");
        }

        if (corpse.IsButchered)
        {
            return (false, null, "双头金刚的精气已被挖空。");
        }

        corpse.IsButchered = true;

        // 盛大官方 1.5《虎卫传说》权威真值:
        // 双头金刚死后地表无任何物品掉落，必须通过 Alt + 鼠标左键挖肉产出【法神首饰】(法神头盔/法神项链/法神手镯/法神戒指)
        // 绝非法神披风 (法神披风为 1.76 六大重装之一，由堕落坟场暗之沃玛教主掉落)
        int roll = rng.Next(100);
        if (roll < 50)
        {
            int subRoll = rng.Next(4);
            EquipItem fashenItem = subRoll switch
            {
                0 => new EquipItem { Uid = 88801 + rng.Next(1000), Name = "法神头盔", Dura = 8000, MaxDura = 8000, Ac = 4, MaxAc = 4, Mac = 1, MaxMac = 2, Mc = 0, MaxMc = 1, Weight = 2 },
                1 => new EquipItem { Uid = 88801 + rng.Next(1000), Name = "法神项链", Dura = 7000, MaxDura = 7000, Mc = 1, MaxMc = 8, Weight = 1 },
                2 => new EquipItem { Uid = 88801 + rng.Next(1000), Name = "法神手镯", Dura = 7000, MaxDura = 7000, Ac = 0, MaxAc = 1, Mac = 0, MaxMac = 1, Mc = 0, MaxMc = 4, Weight = 2 },
                _ => new EquipItem { Uid = 88801 + rng.Next(1000), Name = "法神戒指", Dura = 7000, MaxDura = 7000, Mac = 0, MaxMac = 1, Mc = 1, MaxMc = 6, Weight = 1 }
            };
            return (true, fashenItem, $"割下了 {fashenItem.Name}。");
        }

        return (true, null, "割下了一块肉。");
    }

    /// <summary>
    /// 极品祖玛雕像濒死自爆权威裁决
    /// </summary>
    public static (bool willExplode, int explodeDamage) CheckZumaStatueSelfDestruct(int currentHp, int maxHp) =>
        Mir2.Shared.Rules.AdvancedCombatRuleEngine.BossEncounterRuleEngine.ZumaStatueSelfDestructEngine.CheckSelfDestruct(currentHp, maxHp);

    /// <summary>
    /// 沃玛卫士连斩双倍伤害权威裁决
    /// </summary>
    public static (bool isDualSlash, int finalDamage) CalculateWomaDualSlash(int baseDc, bool dualSlashProc) =>
        Mir2.Shared.Rules.AdvancedCombatRuleEngine.BossEncounterRuleEngine.WomaDualBladeEngine.CalculateSlashDamage(baseDc, dualSlashProc);

    /// <summary>
    /// 盛大官方 1.5 触龙神 3x3 动态占地阻挡注册与注销 (IDA sub_4B966C)
    /// </summary>
    public static void SetEvilCentipedeDynamicCollision(Maps.GameMap map, int centerX, int centerY, bool isEmerged)
    {
        if (map == null) return;
        for (int x = centerX - 1; x <= centerX + 1; x++)
        {
            for (int y = centerY - 1; y <= centerY + 1; y++)
            {
                map.SetDynamicObstacle(x, y, isEmerged);
            }
        }
    }

    /// <summary>
    /// 盛大官方 1.5 废矿区东部爬行僵尸钻出地洞限时入口 (IDA sub_48A1B0 / ZombieHoleEngine)
    /// 当矿区东部坐标 (140, 108) 钻地爬行僵尸破土时，地表生成持续 60 秒的限时洞口入口，踩中直接传送进入尸王殿 (Dungeon Q004)
    /// </summary>
    public sealed class ZombieHoleEntrance
    {
        public int X { get; init; } = 140;
        public int Y { get; init; } = 108;
        public string TargetMapId { get; init; } = "Q004"; // 盛大官方尸王殿地图代码
        public int TargetX { get; init; } = 11;
        public int TargetY { get; init; } = 15;
        public long ExpireTimeMs { get; set; }

        public bool IsActive(long nowMs) => nowMs < ExpireTimeMs;

        public bool TryEnterHole(int playerX, int playerY, long nowMs, out (string map, int x, int y) target)
        {
            target = (TargetMapId, TargetX, TargetY);
            if (!IsActive(nowMs)) return false;
            return playerX == X && playerY == Y;
        }
    }
}
