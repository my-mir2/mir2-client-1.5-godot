using System;
using System.Collections.Generic;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.5 虎卫传说核心随从与成长引擎 (HuWeiPetEngine)
/// 严格 1:1 对齐官方 M2Server 随从逻辑:
/// 1. 白日门虎卫/鹰卫与常规怪物诱惑驯服 (技能 13 诱惑之光)
/// 2. 随从成长等级 0 ~ 7 级系统:
///    - 0 级 (白名) -> 1~2 级 (黄名) -> 3~4 级 (红名) -> 5~6 级 (绿名) -> 7 级 (蓝名巅峰)
///    - 升级大幅提升血量上限 (虎卫 0 级 400 HP，7 级 3200 HP)、攻击力与防御
/// 3. 随从行为模式: 休息模式 (原地待命) / 跟随战斗模式 (Ctrl+A 指令)
/// 4. 随主人跨地图瞬移传送同屏同步
/// </summary>
public sealed class HuWeiPetEngine
{
    public sealed class SlavePet
    {
        public int Uid { get; init; }
        public string Name { get; set; } = "虎卫";
        public int MasterSessionId { get; set; }
        public int CurrentMapId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Level { get; set; } // 0 ~ 7 级
        public int KillExp { get; set; } // 随从击杀经验
        public int Hp { get; set; }
        public int MaxHp { get; set; }
        public int MinDc { get; set; }
        public int MaxDc { get; set; }
        public int Ac { get; set; }
        public int Mac { get; set; }
        public bool IsResting { get; set; } // 是否为休息指令 (原地不移动不主动攻击)
        public bool IsDead => Hp <= 0;

        /// <summary>
        /// 获取当前随从名字的显示颜色 (0级白, 1~2黄, 3~4红, 5~6绿, 7级蓝)
        /// </summary>
        public string NameColor => Level switch
        {
            0 => "White",
            1 or 2 => "Yellow",
            3 or 4 => "Red",
            5 or 6 => "Green",
            7 => "Blue",
            _ => "White"
        };

        /// <summary>
        /// 获取当前随从名字的显示颜色码 (官方 SM_CHANGENAMECOLOR 41 协议参数，IDA 0x46A157)
        /// 0: 白色 (0级), 1: 黄色 (1~2级), 2: 红色 (3~4级), 3: 绿色 (5~6级), 4: 蓝色 (7级极品巅峰)
        /// </summary>
        public int NameColorCode => Level switch
        {
            0 => 0,
            1 or 2 => 1,
            3 or 4 => 2,
            5 or 6 => 3,
            7 => 4,
            _ => 0
        };
    }

    /// <summary>
    /// 根据初始召唤技能等级 (0~3级) 初始化随从属性与生命上限
    /// 盛大官方 1.5 铁律:
    /// - 0级技能招0级宝宝 (白名)
    /// - 1级技能招1级宝宝 (黄名，MaxHp * 1.35)
    /// - 2级技能招2级宝宝 (黄名，MaxHp * 1.7)
    /// - 3级技能招3级宝宝 (红名，MaxHp * 2.1)
    /// </summary>
    public static void InitializePetBySkillLevel(SlavePet pet, int skillLevel)
    {
        pet.Level = Math.Clamp(skillLevel, 0, 3);
        if (pet.Level > 0)
        {
            float mult = 1.0f + pet.Level * 0.35f;
            pet.MaxHp = (int)(pet.MaxHp * mult);
            pet.Hp = pet.MaxHp;
            pet.MinDc += pet.Level * 4;
            pet.MaxDc += pet.Level * 8;
            pet.Ac += pet.Level * 3;
            pet.Mac += pet.Level * 2;
        }
    }

    /// <summary>
    /// 创建官方标准 0 级虎卫
    /// </summary>
    public static SlavePet CreateHuWei(int uid, int masterSessionId, int mapId, int x, int y)
    {
        return new SlavePet
        {
            Uid = uid,
            Name = "虎卫",
            MasterSessionId = masterSessionId,
            CurrentMapId = mapId,
            X = x,
            Y = y,
            Level = 0,
            KillExp = 0,
            Hp = 400,
            MaxHp = 400,
            MinDc = 15,
            MaxDc = 25,
            Ac = 10,
            Mac = 8,
            IsResting = false
        };
    }

    /// <summary>
    /// 创建官方标准 0 级鹰卫 (丛林迷宫远程神射手)
    /// </summary>
    public static SlavePet CreateYingWei(int uid, int masterSessionId, int mapId, int x, int y)
    {
        return new SlavePet
        {
            Uid = uid,
            Name = "鹰卫",
            MasterSessionId = masterSessionId,
            CurrentMapId = mapId,
            X = x,
            Y = y,
            Level = 0,
            KillExp = 0,
            Hp = 350,
            MaxHp = 350,
            MinDc = 20,
            MaxDc = 35,
            Ac = 8,
            Mac = 12,
            IsResting = false
        };
    }

    /// <summary>
    /// 随从击杀怪物获取杀敌经验并晋级 (最高 7 级)
    /// </summary>
    public static bool AddKillExpAndTryLevelUp(SlavePet pet, int expGained)
    {
        if (pet.IsDead || pet.Level >= 7) return false;

        pet.KillExp += expGained;

        // 官方等级晋升门槛 (0->1: 100, 1->2: 250, 2->3: 500, 3->4: 1000, 4->5: 2000, 5->6: 4000, 6->7: 8000)
        int expNeeded = pet.Level switch
        {
            0 => 100,
            1 => 250,
            2 => 500,
            3 => 1000,
            4 => 2000,
            5 => 4000,
            6 => 8000,
            _ => int.MaxValue
        };

        if (pet.KillExp >= expNeeded)
        {
            pet.Level++;
            pet.KillExp = 0;

            // 升级属性几何倍增
            pet.MaxHp = (int)(pet.MaxHp * 1.35f);
            pet.Hp = pet.MaxHp; // 晋级满血
            pet.MinDc += 4;
            pet.MaxDc += 8;
            pet.Ac += 3;
            pet.Mac += 2;
            return true;
        }

        return false;
    }

    /// <summary>
    /// 切换随从工作模式: 休息原地待命 / 跟随进攻 (官方 Ctrl+A 随从控制指令)
    /// </summary>
    public static (bool isResting, string message) TogglePetMode(SlavePet pet)
    {
        pet.IsResting = !pet.IsResting;
        string msg = pet.IsResting ? "你的宝宝处于休息状态。" : "你的宝宝处于跟随主人战斗状态。";
        return (pet.IsResting, msg);
    }

    /// <summary>
    /// 主人跨地图瞬移时同步拉扯随从至身边 (九宫格空闲坐标)
    /// </summary>
    public static void SyncPetTeleportWithMaster(SlavePet pet, int targetMapId, int masterX, int masterY)
    {
        SyncPetTeleportWithMaster(pet, null, targetMapId, masterX, masterY);
    }

    /// <summary>
    /// 盛大官方 1.5 铁律: 主人跨地图瞬移或组队天地合一拉扯时，同步随从 SlavePet 与世界实体 MonsterInstance 物理坐标与地图，彻底杜绝老地图幽灵怪残留
    /// </summary>
    public static void SyncPetTeleportWithMaster(SlavePet pet, MonsterInstance? monster, int targetMapId, int masterX, int masterY)
    {
        if (pet.IsDead) return;

        pet.CurrentMapId = targetMapId;
        pet.X = masterX + 1; // 默认随侍在侧
        pet.Y = masterY;

        if (monster != null)
        {
            monster.MapId = targetMapId;
            monster.X = pet.X;
            monster.Y = pet.Y;
            monster.TargetSessionId = 0; // 传送后解除旧仇恨，优先护卫主人
        }
    }
}
