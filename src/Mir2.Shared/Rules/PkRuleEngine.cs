using System;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 PK 红黄名与城镇守卫规则引擎 (Delphi 权威 1:1 还原)
/// 逆向实证锚点:
/// - 恶性谋杀与 PKLevel 计算: IDA sub_4BF0A0 @ 0x4BF0A0..0x4BF120 (PKLevel = pk_point / 100)
/// - 大刀卫士与弓箭手仇恨锁定: IDA sub_4BF180 & 0x4BF144（已证client无函数） (PKLevel >= 2 触发极度仇恨)
/// - 守卫高速突进与 999 斩杀: IDA sub_4D7C20 @ 0x4D7C20..0x4D7DF0
/// - 红名村死亡强制重生地图流转: IDA 0x4BD1BC（已证client无函数） case 3 (红名村地图 "9", 坐标 33, 33)
/// </summary>
public static class PkRuleEngine
{
    public const int KillHumanAddPkPoint = 100; // 每恶意击杀 1 名白名增加 100 点 PK 值
    public const int RedNameThreshold = 200;    // >= 200 点 (PKLevel >= 2) 判定为红名
    public const int YellowNameThreshold = 100; // >= 100 点 (PKLevel 1) 判定为黄名
    public const int GuardExecutionDamage = 999;// 守卫对红名玩家的秒杀处决真实伤害

    public enum PkStatus
    {
        White = 0,   // 白名无辜守法市民 (PKPoint < 100)
        Yellow = 1,  // 黄名恶性警告状态 (100 <= PKPoint < 200)
        Red = 2,     // 红名恶贯满盈通缉犯 (PKPoint >= 200)
        DeepRed = 3  // 深红超级杀人狂魔 (PKPoint >= 300)
    }

    /// <summary>
    /// 获取官方 PK 等级 (Delphi 0x4BF0A0（已证client无函数）: pk_point / 100)
    /// </summary>
    public static int GetPkLevel(int pkPoint)
    {
        return Math.Max(0, pkPoint / 100);
    }

    /// <summary>
    /// 获取 PK 身份标识
    /// </summary>
    public static PkStatus GetPkStatus(int pkPoint)
    {
        if (pkPoint >= 300) return PkStatus.DeepRed;
        if (pkPoint >= RedNameThreshold) return PkStatus.Red;
        if (pkPoint >= YellowNameThreshold) return PkStatus.Yellow;
        return PkStatus.White;
    }

    /// <summary>
    /// 判定目标是否被大刀卫士或弓箭手锁定为敌对 (0x4BF180（已证client无函数）: PKLevel >= 2 或灰名攻击标志)
    /// </summary>
    public static bool IsHostileToGuard(int targetPkPoint, bool targetHasPkFlag = false)
    {
        return targetPkPoint >= RedNameThreshold || targetHasPkFlag;
    }

    /// <summary>
    /// 大刀卫士或弓箭手处决红名玩家 (0x4D7C20（已证client无函数）)
    /// 造成 999 点无视一切防御与魔法盾的真实伤害秒杀
    /// </summary>
    public static (int damage, bool isDead, string broadcastMsg) ExecuteGuardSlash(
        PlayerState target)
    {
        if (target == null)
        {
            return (0, false, string.Empty);
        }

        if (!IsHostileToGuard(target.PkPoint))
        {
            return (0, false, "守卫未发现目标有违纪行为。");
        }

        int damage = GuardExecutionDamage;
        target.Hp = 0; // 一刀斩杀当场暴毙

        string msg = "大刀卫士攻击红名";

        return (damage, true, msg);
    }

    /// <summary>
    /// 获取角色阵亡后的重生出生点 (0x4BD1BC（已证client无函数） case 3)
    /// 红名玩家强制遣送红名村 (Map 9, 33, 33)，非红名回主城安全区 (如盟重 3, 330, 330)
    /// </summary>
    public static (int mapId, int spawnX, int spawnY, string locationName) GetRespawnLocation(int pkPoint)
    {
        if (pkPoint >= RedNameThreshold)
        {
            // 红名村 (MapId 9, 坐标 33, 33)
            return (9, 33, 33, "盟重红名村");
        }
        // 普通安全区 (MapId 3 盟重省 330, 330)
        return (3, 330, 330, "盟重省土城安全区");
    }

    /// <summary>
    /// 自然洗红名时间递减（对齐 Rust decay：sub_4BF520 DecPkPoint，50 秒减 1 点）
    /// </summary>
    public static int DecayPkPoint(int currentPkPoint, int elapsedSec)
    {
        int decay = elapsedSec / 50;
        return Math.Max(0, currentPkPoint - decay);
    }

    /// <summary>
    /// 盛大官方 1.4/1.5 权威攻击模式 (Delphi M2Server btAttackMode 0~4)
    /// </summary>
    public enum OfficialAttackMode : byte
    {
        All = 0,      // 全体模式 (攻击所有人)
        Peace = 1,    // 和平模式 (禁止攻击任何玩家，只能打怪)
        Group = 2,    // 编组模式 (豁免同组队伍成员)
        Guild = 3,    // 行会模式 (豁免同行会及同盟行会成员，战争敌对行会除外)
        RedName = 4   // 善恶模式 (仅攻击红名与黄名/灰名玩家，禁止攻击守法白名)
    }

    /// <summary>
    /// 盛大官方 1.5 攻击模式权威判定 (IDA sub_4B00D4 / CanAttackPlayer)
    /// </summary>
    public static bool CanAttackTargetPlayer(
        byte attackMode,
        bool isAttackerInSafeZone,
        bool isTargetInSafeZone,
        int attackerSessionId,
        int targetSessionId,
        bool targetIsDead,
        bool targetIsRedOrYellow,
        bool isSameGroup,
        bool isSameGuild,
        bool isAlliedGuild,
        bool isWarGuild)
    {
        // 1. 安全区禁武: 攻击者处于安全区或目标处于安全区，绝对禁止任何攻击
        if (isAttackerInSafeZone || isTargetInSafeZone) return false;

        // 2. 禁止攻击自身，禁止鞭尸
        if (attackerSessionId == targetSessionId || targetIsDead) return false;

        // 3. 行会宣战战争豁免: 战争双方成员互为第一优先级战争目标，任何攻击模式均放行
        if (isWarGuild) return true;

        // 4. 按攻击模式逐级裁决
        return (OfficialAttackMode)attackMode switch
        {
            OfficialAttackMode.All => true,
            OfficialAttackMode.Peace => false, // 和平模式绝对禁止攻击任何玩家
            OfficialAttackMode.Group => !isSameGroup, // 组队模式豁免队友
            OfficialAttackMode.Guild => !isSameGuild && !isAlliedGuild, // 行会模式豁免同会与同盟
            OfficialAttackMode.RedName => targetIsRedOrYellow, // 善恶模式仅能攻击红名或黄名
            _ => true
        };
    }
}

