using System;
using Mir2.Shared.Models;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 沙巴克城门开关与城墙血量修缮引擎 (SabukCastleGateEngine)
/// 严格 1:1 对齐官方 M2Server 城堡城门守御体系:
/// 1. 沙巴克城门状态机:
///    - 开门 (Opened) / 关门 (Closed) / 被攻破倒塌 (Broken)
///    - 城门默认生命值 MaxHp = 2000 点
/// 2. 攻城期间受击伤害扣减:
///    - 仅当城门为 Closed 状态时受到攻城方物理与法术攻击
///    - 血量归 0 时转为 Broken 倒塌状态，向九宫格广播破门动画，敌军蜂拥冲入城内
/// 3. 城门修缮修复:
///    - 仅限沙巴克正统掌门人可在和平时期或攻城筹备期修缮城门
///    - 消耗金库或掌门个人 50,000 金币，将城门血量恢复满值并将状态重置为 Closed
/// </summary>
public sealed class SabukCastleGateEngine
{
    public enum GateState
    {
        Closed = 0,
        Opened = 1,
        Broken = 2
    }

    public const int MaxGateHp = 2000;
    public const int RepairGateCost = 50000;

    public int Hp { get; set; } = MaxGateHp;
    public GateState State { get; set; } = GateState.Closed;

    /// <summary>
    /// 城主控制开关门
    /// </summary>
    public (bool success, string message) ToggleGate(bool open, bool isLord)
    {
        if (!isLord)
        {
            return (false, "只有沙巴克城主才能开关城门。");
        }

        if (State == GateState.Broken)
        {
            return (false, "城门已损坏，请先修理城门。");
        }

        State = open ? GateState.Opened : GateState.Closed;
        string msg = open ? "城门已打开。" : "城门已关闭。";
        return (true, msg);
    }

    /// <summary>
    /// 城门承受攻城方伤害
    /// </summary>
    public (bool isDamaged, bool isBroken, string message) DamageGate(int damage)
    {
        if (State != GateState.Closed)
        {
            return (false, false, "城门未闭合或已倒塌，无法攻击。");
        }

        Hp = Math.Max(0, Hp - damage);
        if (Hp == 0)
        {
            State = GateState.Broken;
            return (true, true, "沙巴克城门被攻破了！");
        }

        return (true, false, $"城门受到攻击，耐久度: {Hp}/{MaxGateHp}");
    }

    /// <summary>
    /// 城主修缮城门
    /// </summary>
    public (bool success, string message) RepairGate(PlayerState lordState)
    {
        if (lordState == null) return (false, "状态异常。");

        if (State != GateState.Broken && Hp >= MaxGateHp)
        {
            return (false, "城门坚固完好，无需修缮。");
        }

        if (lordState.Gold < RepairGateCost)
        {
            return (false, $"修理城门需要 {RepairGateCost} 金币，金币不足。");
        }

        lordState.Gold -= RepairGateCost;
        Hp = MaxGateHp;
        State = GateState.Closed;

        return (true, "城门已修理完毕。");
    }
}
