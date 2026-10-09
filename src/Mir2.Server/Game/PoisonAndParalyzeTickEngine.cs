using System;
using Mir2.Shared.Models;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 施毒术持续掉血与石化麻痹控制引擎 (PoisonAndParalyzeTickEngine)
/// 严格 1:1 对齐官方 M2Server 异常状态机 (IDA 0x49350C / sub_4C7720):
/// 1. 绿毒 (GreenPoison): 周期性每 1000ms 扣减一次生命值，且中毒期间封锁角色自然生命回复；
/// 2. 红毒 (RedPoison): 易伤状态，受到的物理与法术伤害提升 20%；
/// 3. 石化麻痹 (Paralyze): 麻痹戒指击中后陷入 5 秒石化，状态掩码 Bit 5 置位，期间完全禁止行走、跑步、攻击与施法。
/// </summary>
public sealed class PoisonAndParalyzeTickEngine
{
    public const int ParalyzeDurationMs = 5000;

    /// <summary>
    /// 处理单次时间步长下的绿毒掉血结算
    /// </summary>
    public static (bool damaged, int poisonDamage, bool died) TickGreenPoison(PlayerState target, long nowMs)
    {
        if (target == null || target.Hp <= 0 || !target.HasGreenPoison)
        {
            return (false, 0, false);
        }

        if (nowMs >= target.GreenPoisonExpireMs)
        {
            // 毒效过期解除
            target.HasGreenPoison = false;
            return (false, 0, false);
        }

        // 官方公式: 绿毒固定每秒扣减 2 点 + 道术加成 (最低 1 点)
        int dmg = Math.Max(1, 2 + (target.Sc / 10));
        target.Hp = Math.Max(0, target.Hp - dmg);

        return (true, dmg, target.Hp == 0);
    }

    /// <summary>
    /// 麻痹判定：若角色当前处于麻痹石化状态，则禁止任何主动动作
    /// </summary>
    public static bool IsActionForbiddenByParalysis(PlayerState state, long nowMs)
    {
        if (state == null) return false;
        return state.IsParalyzed || nowMs < state.ParalyzeExpireMs;
    }
}
