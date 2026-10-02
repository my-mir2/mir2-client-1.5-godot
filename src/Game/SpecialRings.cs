using System;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 原版 7 大特戒与 4 大套装隐藏属性状态与结算引擎 (M2Server sub_4C03B0 / sub_4BE3FC / sub_4C7720 / sub_4C1EF4 铁证)
/// 严格 1:1 比特级还原:
///   1. 护身戒指: 1:1.5 MP 抵扣伤害，MP 耗尽后剩余伤害如实转扣 HP (sub_4BE3FC, flt_4BE508=1.5)
///   2. 复活戒指: HP 归零瞬间回满，消耗 1 点底层持久(1000点)，30 秒冷却时钟 (sub_4C7720 @ 0x7530ms)
///   3. 麻痹戒指: 物理攻击命中时 1/(target.poison_recover + 5) 概率石化 5 秒 (sub_4C1EF4 @ 0x4c2268)
///   4. 超负载戒指: 背包、穿戴、腕力三大负重严格翻倍 (sub_4C03B0 @ 0x4c0dd0)
///   5. 魔血套装: 单件 25 点蓝转血，三件套集齐额外 +50 点蓝转血，保底 1 点 MP (sub_4C03B0 @ 0x4c0891)
///   6. 虹魔套装: 单件 10% 吸血权重，三件套集齐准确 +2 (sub_4C03B0 @ 0x4c08a9, sub_4C1EF4 @ 0x4c22aa)
///   7. 技能戒指: 战士佩戴火焰戒指赋予火球术，脱下收回 (sub_4BFFD0 / sub_4C01B8)
/// </summary>
public static class SpecialRings
{
    /// <summary>
    /// 护身戒指伤害抵扣计算
    /// </summary>
    public static (bool hasProtect, int mpCost, int remainingMp, int remainingDamage, int newHp, int hpLoss)
        CalculateProtectRingDamage(bool hasProtect, int damage, int curMp, int curHp)
    {
        if (!hasProtect || damage <= 0 || curMp <= 0)
        {
            int actualHp = Math.Max(0, curHp - damage);
            return (hasProtect, 0, curMp, damage, actualHp, damage);
        }

        // 扣蓝公式: mpCost = ROUND( damage * 1.5 )
        int mpCost = (int)Math.Round(damage * 1.5);
        int remainingMp;
        int remainingDamage;

        if (curMp >= mpCost)
        {
            remainingMp = curMp - mpCost;
            remainingDamage = 0;
        }
        else
        {
            int mpGap = mpCost - curMp;
            remainingMp = 0;
            remainingDamage = (int)Math.Round(mpGap / 1.5);
        }

        int newHp = Math.Max(0, curHp - remainingDamage);
        return (true, Math.Min(mpCost, curMp), remainingMp, remainingDamage, newHp, curHp - newHp);
    }

    /// <summary>
    /// 复活戒指触发判定 (30 秒冷却，持久扣 1000 点)
    /// </summary>
    public static (bool canRevive, int newHp, int newDura, bool isBroken, string reason)
        TriggerReviveRing(bool hasRevive, int curHp, int maxHp, int curDura, long lastReviveTick, long nowTick)
    {
        if (!hasRevive)
            return (false, curHp, curDura, false, "NO_REVIVE_RING");

        if (nowTick - lastReviveTick < 30000)
            return (false, curHp, curDura, false, "COOLDOWN");

        int newDura = curDura - 1000;
        bool isBroken = false;
        if (newDura <= 0)
        {
            newDura = 0;
            isBroken = true;
        }

        return (true, maxHp, newDura, isBroken, "SUCCESS");
    }

    /// <summary>
    /// 麻痹戒指石化判定
    /// </summary>
    public static (bool isParalyzed, int durationSec, int denom)
        TriggerParalysisRing(bool hasParalysis, int targetPoisonRecover, int rngRoll = -1)
    {
        if (!hasParalysis)
            return (false, 0, 0);

        int denom = targetPoisonRecover + 5;
        int roll = rngRoll >= 0 ? (rngRoll % denom) : (Random.Shared.Next(denom));

        if (roll == 0)
        {
            return (true, 5, denom); // 5 秒石化
        }

        return (false, 0, denom);
    }

    /// <summary>
    /// 超负载戒指三大负重翻倍
    /// </summary>
    public static (int maxWeight, int maxWearWeight, int maxHandWeight)
        ApplyOverloadRing(int maxWeight, int maxWearWeight, int maxHandWeight, bool hasRing)
    {
        if (hasRing)
        {
            return (maxWeight * 2, maxWearWeight * 2, maxHandWeight * 2);
        }
        return (maxWeight, maxWearWeight, maxHandWeight);
    }

    /// <summary>
    /// 魔血套装 (戒指 133, 手镯 134, 项链 135) 单件 25 点，全套额外 +50 点
    /// 限制转化上限不超过 baseMp - 1 (保底 1 点 MP)
    /// </summary>
    public static (bool hasFullSet, int totalTransfer, int finalHp, int finalMp)
        CalculateMagicBloodBonus(ReadOnlySpan<int> pieceShapes, int baseHp, int baseMp)
    {
        bool hasRing = false;
        bool hasBrace = false;
        bool hasNeck = false;
        int totalTransfer = 0;

        for (int i = 0; i < pieceShapes.Length; i++)
        {
            int s = pieceShapes[i];
            if (s == 133) { hasRing = true; totalTransfer += 25; }
            else if (s == 134) { hasBrace = true; totalTransfer += 25; }
            else if (s == 135) { hasNeck = true; totalTransfer += 25; }
        }

        bool hasFullSet = hasRing && hasBrace && hasNeck;
        if (hasFullSet)
        {
            totalTransfer += 50;
        }

        if (baseMp <= totalTransfer)
        {
            totalTransfer = Math.Max(0, baseMp - 1);
        }

        return (hasFullSet, totalTransfer, baseHp + totalTransfer, baseMp - totalTransfer);
    }

    /// <summary>
    /// 虹魔套装 (戒指 136, 手镯 137, 项链 138) 单件 10% 吸血，全套准确 +2
    /// </summary>
    public static (bool hasFullSet, int leechWeight, int bonusHit, int leechHp)
        CalculateRainbowMagicBonus(ReadOnlySpan<int> pieceShapes, int baseHit, int attackDamage)
    {
        bool hasRing = false;
        bool hasBrace = false;
        bool hasNeck = false;
        int leechWeight = 0;

        for (int i = 0; i < pieceShapes.Length; i++)
        {
            int s = pieceShapes[i];
            if (s == 136) { hasRing = true; leechWeight += 10; }
            else if (s == 137) { hasBrace = true; leechWeight += 10; }
            else if (s == 138) { hasNeck = true; leechWeight += 10; }
        }

        bool hasFullSet = hasRing && hasBrace && hasNeck;
        int bonusHit = hasFullSet ? 2 : 0;
        int leechHp = (int)Math.Round((double)attackDamage / 100.0 * leechWeight);

        return (hasFullSet, leechWeight, bonusHit, leechHp);
    }
}
