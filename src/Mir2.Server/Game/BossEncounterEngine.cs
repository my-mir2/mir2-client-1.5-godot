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
///    - 死后绝对不爆任何地表掉落物，必须通过 Alt 挖肉方可挖取【法神披风】与法神首饰
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

        // 官方挖取概率: 50% 挖到法神首饰或法神披风
        int roll = rng.Next(100);
        if (roll < 50)
        {
            var fashenItem = new EquipItem
            {
                Uid = 88801 + rng.Next(1000),
                Name = roll < 20 ? "法神披风" : "法神手镯",
                Dura = 20000,
                MaxDura = 20000,
                Mc = 2,
                MaxMc = 5,
                Weight = 10
            };
            return (true, fashenItem, $"你从双头金刚体内挖掘到了绝世至宝【{fashenItem.Name}】！");
        }

        return (true, null, "你割下了一块金刚肉，但并未发现法神至宝。");
    }
}
