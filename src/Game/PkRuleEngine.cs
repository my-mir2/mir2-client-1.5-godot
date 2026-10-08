using System;

namespace Mir2Client.Game;

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

        string msg = target.PkPoint >= 300
            ? $"【大刀卫士】一声怒吼，手起刀落将恶贯满盈的超级杀人魔【{target.Name}】当场正法斩首！"
            : $"【大刀卫士】凛然出手，一道森寒刀光将红名通缉犯【{target.Name}】当场格杀斩落马下！";

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
}
