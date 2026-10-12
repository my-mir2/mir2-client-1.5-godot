using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 原版 7 大特戒与 4 大套装隐藏属性状态与结算引擎 (M2Server sub_4C03B0 / sub_4BE3FC / sub_4C7720 / sub_4C1EF4 / ProcessSpiritSuite 铁证)
/// 严格 1:1 比特级还原:
///   1. 护身戒指: 1:1.5 MP 抵扣伤害，MP 耗尽后剩余伤害如实转扣 HP (0x4BE3FC（已证client无函数）, flt_4BE508=1.5)
///   2. 复活戒指: HP 归零瞬间回满，消耗 1 点底层持久(1000点)，30 秒冷却时钟 (0x4C7720（已证client无函数） @ 0x7530ms)
///   3. 麻痹戒指: 物理攻击命中时 1/(target.poison_recover + 5) 概率石化 5 秒 (0x4C1EF4（已证client无函数） @ 0x4c2268)
///   4. 超负载戒指: 背包、穿戴、腕力三大负重严格翻倍 (0x4C03B0（已证client无函数） @ 0x4c0dd0)
///   5. 隐身戒指: 佩戴常态隐身，普通怪物忽略索敌，移动不破隐，攻击破隐后立刻恢复 (0x4C03B0（已证client无函数） @ Shape 111)
///   6. 传送戒指: 佩戴支持 @move x y / @飞 x y 坐标瞬移指令，需合法坐标与阻挡校验 (0x4C03B0（已证client无函数） @ Shape 112)
///   7. 技能戒指: 战士/道士佩戴火焰戒指赋予火球术(MagicId=1)，佩戴防御戒指赋予治愈术(MagicId=2) (0x4BFFD0（已证client无函数） / sub_4C01B8)
///   8. 记忆套装: 记忆头盔(125)+项链(123)+手镯(124)+戒指(122)；队长佩戴并输入 @天地合一 召唤同图队友 (180s CD)
///   9. 祈祷套装: 祈祷之刃+头盔+项链+手镯+戒指 5件全齐，全服天谴广播，宠物暴走叛变，随机碎裂1件
///  10. 魔血套装: 单件 25 点蓝转血，三件套集齐额外 +50 点蓝转血，保底 1 点 MP (0x4C03B0（已证client无函数） @ 0x4c0891)
///  11. 虹魔套装: 单件 10% 吸血权重，累积满 2.0 回复生命，三件套集齐准确 +2 (0x4C03B0（已证client无函数） @ 0x4c08a9, sub_4C1EF4 @ 0x4c22aa)
/// </summary>
public static class SpecialRings
{
    /// <summary>
    /// 1. 护身戒指伤害抵扣计算 (0x4BE3FC（已证client无函数）, flt_4BE508=1.5)
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
    /// 2. 复活戒指触发判定 (30 秒冷却，持久扣 1000 点，0x4C7720（已证client无函数） @ 0x7530ms)
    /// </summary>
    public static (bool canRevive, int newHp, int newDura, bool isBroken, string reason)
        TriggerReviveRing(bool hasRevive, int curHp, int maxHp, int curDura, long lastReviveTick, long nowTick)
    {
        if (!hasRevive)
            return (false, curHp, curDura, false, "NO_REVIVE_RING");

        if (curDura <= 0)
            return (false, curHp, curDura, false, "BROKEN");

        if (lastReviveTick > 0 && nowTick - lastReviveTick < 30000)
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
    /// 3. 麻痹戒指石化判定 (0x4C1EF4（已证client无函数） @ 0x4c2268)
    /// 概率: Random(目标毒物躲避 AntiPoison + 5) == 0（对齐 Rust check_paralysis_hit；形参旧名 targetPoisonRecover 已正名为 AntiPoison）
    /// </summary>
    public static (bool isParalyzed, int durationSec, int denom)
        TriggerParalysisRing(bool hasParalysis, int targetAntiPoison, bool targetHasAntiParalysis = false, int rngRoll = -1)
    {
        if (!hasParalysis || targetHasAntiParalysis)
            return (false, 0, 0);

        int denom = targetAntiPoison + 5;
        if (denom <= 0) return (false, 0, 0);

        int roll = rngRoll >= 0 ? (rngRoll % denom) : (Random.Shared.Next(denom));

        if (roll == 0)
        {
            return (true, 5, denom); // 5 秒石化定身
        }

        return (false, 0, denom);
    }

    /// <summary>
    /// 4. 超负载戒指三大负重翻倍 (0x4C03B0（已证client无函数） @ 0x4c0dd0)
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
    /// 5. 隐身戒指索敌判定 (0x4C03B0（已证client无函数） @ Shape 111)
    /// 佩戴常态隐身，普通怪物忽略索敌；破隐视破怪（如楔蛾、月魔蜘蛛、教主）可破隐视破
    /// </summary>
    public static bool CheckMonsterCanTarget(bool playerHasInvisibleRing, bool monsterCanDetectInvis)
    {
        if (!playerHasInvisibleRing)
            return true; // 未佩戴隐身戒指，怪物正常索敌

        // 佩戴隐身戒指时，只有具备真视破隐能力的怪物才能索敌
        return monsterCanDetectInvis;
    }

    /// <summary>
    /// 6. 传送戒指 @move x y 瞬移指令解析与合法校验 (0x4C03B0（已证client无函数） @ Shape 112)
    /// </summary>
    public static (bool success, int targetX, int targetY, string message)
        ExecuteTeleportRing(bool hasRing, string command, int currentX, int currentY,
            Func<int, int, bool>? isTileBlocked = null, int mapWidth = 1000, int mapHeight = 1000,
            long lastTeleportTick = 0, long nowTick = 0)
    {
        if (string.IsNullOrWhiteSpace(command))
            return (false, currentX, currentY, "指令为空");

        string trimmed = command.Trim();
        bool isMoveCmd = trimmed.StartsWith("@move", StringComparison.OrdinalIgnoreCase) ||
                         trimmed.StartsWith("@飞", StringComparison.OrdinalIgnoreCase) ||
                         trimmed.StartsWith("@传送", StringComparison.OrdinalIgnoreCase) ||
                         trimmed.StartsWith("@瞬移", StringComparison.OrdinalIgnoreCase);

        if (!isMoveCmd)
            return (false, currentX, currentY, "非传送指令");

        if (!hasRing)
            return (false, currentX, currentY, "你未佩戴传送戒指，无法使用空间瞬移！");

        // 检查传送冷却 (盛大官方 1.5 严格 10 秒冷却)
        if (lastTeleportTick > 0 && nowTick - lastTeleportTick < 10000)
        {
            int remainSec = Math.Max(1, (int)Math.Ceiling((10000 - (nowTick - lastTeleportTick)) / 1000.0));
            return (false, currentX, currentY, $"传送戒指魔力正在凝聚中，请等待 {remainSec} 秒后再试！");
        }

        // 解析参数
        string[] parts = trimmed.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length < 3 || !int.TryParse(parts[1], out int targetX) || !int.TryParse(parts[2], out int targetY))
        {
            return (false, currentX, currentY, "指令格式错误，正确格式: @move X Y");
        }

        if (targetX < 0 || targetX >= mapWidth || targetY < 0 || targetY >= mapHeight)
        {
            return (false, currentX, currentY, $"传送失败：目标坐标 ({targetX}, {targetY}) 超出地图边界");
        }

        if (isTileBlocked != null && isTileBlocked(targetX, targetY))
        {
            return (false, currentX, currentY, $"传送失败：目标坐标 ({targetX}, {targetY}) 处于阻挡区域，无法降落");
        }

        return (true, targetX, targetY, $"传送成功至 ({targetX}, {targetY})");
    }

    /// <summary>
    /// 7. 技能戒指检测 (0x4BFFD0（已证client无函数） / sub_4C01B8)
    /// 火焰戒指赋予火球术 (MagicId=1)，防御戒指赋予治愈术 (MagicId=2)
    /// </summary>
    public static List<int> GetSpecialRingGrantedSkills(IEnumerable<EquipItem?> equips, int playerJob)
    {
        var granted = new List<int>();
        bool hasFlame = false;
        bool hasHealing = false;

        foreach (var eq in equips)
        {
            if (eq == null) continue;
            if (eq.Shape == 113 || eq.Shape == 115 || eq.Shape == 151 || eq.Shape == 152 || eq.Shape == 153 || eq.Shape == 157 ||
                eq.Name.Contains("火焰戒指"))
            {
                hasFlame = true;
            }
            if (eq.Shape == 116 || eq.Shape == 152 || eq.Shape == 155 || eq.Name.Contains("防御戒指") || eq.Name.Contains("治愈戒指"))
            {
                hasHealing = true;
            }
        }

        // 战士(0)或道士(2)佩戴火焰戒指，赋予火球术 (ID 1)
        if (hasFlame && playerJob != 1)
        {
            granted.Add(1);
        }

        // 战士(0)或法师(1)佩戴防御戒指，赋予治愈术 (ID 2)
        if (hasHealing && playerJob != 2)
        {
            granted.Add(2);
        }

        return granted;
    }

    /// <summary>
    /// 7b. 复合戒指 Shape 谓词（对齐 Rust special_items.rs apply_ring_shape 152..160，M2 0x50a2xx 段）：
    /// 152=火焰+二阶盾 / 153=火焰+超负载 / 154=护身+盾 / 155=护身+二阶盾 /
    /// 156=护身+超负载 / 157=传送+火焰 / 158=传送+护身 / 159=传送+探测 / 160=双速。
    /// 纯 Shape 判定，供战斗/移动/探测调用方装配（服务端 Rust 同源，联调口径一致）。
    /// </summary>
    public static bool HasTeleportRingShape(int shape) => shape == 112 || (shape >= 157 && shape <= 159);
    public static bool HasOverloadRingShape(int shape) => shape == 119 || shape == 153 || shape == 156;
    public static bool HasMagicShieldRingShape(int shape) => shape >= 154 && shape <= 158;
    public static bool HasProbeNecklaceShape(int shape) => shape == 159;
    public static bool HasDoubleSpeedShape(int shape) => shape == 160;
    public static bool HasFlameRingShape(int shape) =>
        shape == 113 || shape == 115 || shape == 151 || shape == 152 || shape == 153 || shape == 157;

    /// <summary>
    /// 8. 记忆套装检测 (头盔 125, 项链 123, 手镯 124, 戒指 122)
    /// </summary>
    public static bool CheckRecallSet(EquipItem?[] equips)
    {
        if (equips == null || equips.Length < 9) return false;

        bool hasHelmet = false;
        bool hasNecklace = false;
        bool hasBracelet = false;
        bool hasRing = false;

        // 项链槽位 3
        var neck = equips[(int)OfficialEquipSlot.Necklace];
        if (neck != null && (neck.Shape == 123 || neck.Name == "记忆项链")) hasNecklace = true;

        // 头盔槽位 4
        var helm = equips[(int)OfficialEquipSlot.Helmet];
        if (helm != null && (helm.Shape == 125 || helm.Name == "记忆头盔")) hasHelmet = true;

        // 手镯槽位 5, 6
        for (int i = 5; i <= 6; i++)
        {
            var arm = equips[i];
            if (arm != null && (arm.Shape == 124 || arm.Name == "记忆手镯")) hasBracelet = true;
        }

        // 戒指槽位 7, 8
        for (int i = 7; i <= 8; i++)
        {
            var rng = equips[i];
            if (rng != null && (rng.Shape == 122 || rng.Name == "记忆戒指")) hasRing = true;
        }

        return hasHelmet && hasNecklace && hasBracelet && hasRing;
    }

    /// <summary>
    /// 记忆套装 @天地合一 指令触发 (180秒 CD，召唤全队)
    /// </summary>
    public static (bool success, List<(int uid, int targetX, int targetY)> movedMembers, string message)
        TriggerGroupRecall(bool hasRecallSet, bool isLeader, string command, int leaderX, int leaderY,
            int leaderDir, List<(int uid, string name, int x, int y)> teammates,
            Func<int, int, bool>? isTileBlocked = null, long lastRecallTick = 0, long nowTick = 0)
    {
        if (string.IsNullOrWhiteSpace(command))
            return (false, new(), "指令为空");

        string trimmed = command.Trim();
        bool isRecallCmd = trimmed.Equals("@天地合一", StringComparison.OrdinalIgnoreCase) ||
                           trimmed.Equals("@grouprecall", StringComparison.OrdinalIgnoreCase);

        if (!isRecallCmd)
            return (false, new(), "非全队召集指令");

        if (!hasRecallSet)
            return (false, new(), "你未集齐并穿戴整套记忆套装（头盔、项链、手镯、戒指）");

        if (!isLeader)
            return (false, new(), "只有队伍队长才能发动记忆套装 @天地合一 召唤");

        if (lastRecallTick > 0 && nowTick - lastRecallTick < 180000)
        {
            int remainingSec = (int)Math.Ceiling((180000 - (nowTick - lastRecallTick)) / 1000.0);
            return (false, new(), $"记忆套装冷却中，还需等待 {remainingSec} 秒");
        }

        if (teammates == null || teammates.Count == 0)
            return (false, new(), "当前队伍中没有其他队友");

        // 计算队长周围落点 (3x3 环绕队长)
        var offsets = new (int dx, int dy)[]
        {
            (0, -1), (1, -1), (1, 0), (1, 1),
            (0, 1), (-1, 1), (-1, 0), (-1, -1),
            (0, -2), (0, 2), (-2, 0), (2, 0)
        };

        var moved = new List<(int uid, int targetX, int targetY)>();
        int offsetIdx = 0;

        foreach (var member in teammates)
        {
            int targetX = leaderX;
            int targetY = leaderY;

            while (offsetIdx < offsets.Length)
            {
                int candX = leaderX + offsets[offsetIdx].dx;
                int candY = leaderY + offsets[offsetIdx].dy;
                offsetIdx++;

                if (candX >= 0 && candY >= 0 && (isTileBlocked == null || !isTileBlocked(candX, candY)))
                {
                    targetX = candX;
                    targetY = candY;
                    break;
                }
            }

            moved.Add((member.uid, targetX, targetY));
        }

        return (true, moved, $"天地合一！成功召唤了 {moved.Count} 名队友至身边！");
    }

    /// <summary>
    /// 9. 祈祷套装检测 (祈祷之刃+头盔+项链+手镯+戒指 5件全齐)
    /// </summary>
    public static (bool isFullSet, List<OfficialEquipSlot> equippedSlots) CheckPrayerSet(EquipItem?[] equips)
    {
        var equippedSlots = new List<OfficialEquipSlot>();
        if (equips == null || equips.Length < 9) return (false, equippedSlots);

        bool hasSword = false;
        bool hasHelm = false;
        bool hasNeck = false;
        bool hasBrace = false;
        bool hasRing = false;

        // 武器
        var weap = equips[(int)OfficialEquipSlot.Weapon];
        if (weap != null && (weap.Name.Contains("祈祷之刃") || weap.Shape == 24))
        {
            hasSword = true;
            equippedSlots.Add(OfficialEquipSlot.Weapon);
        }

        // 项链
        var neck = equips[(int)OfficialEquipSlot.Necklace];
        if (neck != null && (neck.Name.Contains("祈祷项链") || neck.Shape == 121))
        {
            hasNeck = true;
            equippedSlots.Add(OfficialEquipSlot.Necklace);
        }

        // 头盔
        var helm = equips[(int)OfficialEquipSlot.Helmet];
        if (helm != null && (helm.Name.Contains("祈祷头盔") || helm.Shape == 122))
        {
            hasHelm = true;
            equippedSlots.Add(OfficialEquipSlot.Helmet);
        }

        // 手镯
        for (int i = 5; i <= 6; i++)
        {
            var arm = equips[i];
            if (arm != null && (arm.Name.Contains("祈祷手镯") || arm.Shape == 123))
            {
                hasBrace = true;
                equippedSlots.Add((OfficialEquipSlot)i);
                break;
            }
        }

        // 戒指
        for (int i = 7; i <= 8; i++)
        {
            var rng = equips[i];
            if (rng != null && (rng.Name.Contains("祈祷戒指") || rng.Shape == 124))
            {
                hasRing = true;
                equippedSlots.Add((OfficialEquipSlot)i);
                break;
            }
        }

        bool isFull = hasSword && hasHelm && hasNeck && hasBrace && hasRing;
        return (isFull, equippedSlots);
    }

    /// <summary>
    /// 祈祷套装天谴触发：全服神之祈祷暴走广播、宠物叛变反噬、随机碎裂 1 件部件
    /// </summary>
    public static (bool triggered, string broadcastMsg, bool petRebellion, OfficialEquipSlot brokenSlot)
        TriggerPrayerApocalypse(bool isFullSet, List<OfficialEquipSlot> equippedSlots, int rngSlotRoll = -1)
    {
        if (!isFullSet || equippedSlots == null || equippedSlots.Count == 0)
        {
            return (false, string.Empty, false, OfficialEquipSlot.Dress);
        }

        string msg = "神之祈祷……天空变色，地动山摇！全大陆的召唤生灵已脱离控制陷入狂暴叛变！";
        int roll = rngSlotRoll >= 0 ? (rngSlotRoll % equippedSlots.Count) : Random.Shared.Next(equippedSlots.Count);
        OfficialEquipSlot brokenSlot = equippedSlots[roll];

        return (true, msg, true, brokenSlot);
    }

    /// <summary>
    /// 10. 魔血套装 (戒指 133, 手镯 134, 项链 135) 单件 25 点，全套额外 +50 点
    /// 限制转化上限不超过 baseMp - 1 (保底 1 点 MP) (0x4C03B0（已证client无函数） @ 0x4c0891)
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
    /// 11. 虹魔套装 (戒指 136, 手镯 137, 项链 138) 单件 10% 吸血，全套准确 +2 (0x4C03B0（已证client无函数） @ 0x4c08a9, sub_4C1EF4 @ 0x4c22aa)
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

    /// <summary>
    /// 虹魔套装底层跨次吸血池累积算法 (0x4C1EF4（已证client无函数）: flt_4C2790=100.0, flt_4C2794=2.0)
    /// 每次攻击产生小数累积，当累积池达到 2.0 时一次性提取回复生命
    /// </summary>
    public static int AccumulateLifesteal(ref double currentPool, int realDamage, int leechWeight)
    {
        if (realDamage <= 0 || leechWeight <= 0) return 0;

        double gain = (double)realDamage / 100.0 * leechWeight;
        currentPool += gain;

        if (currentPool >= 2.0)
        {
            int heal = (int)Math.Floor(currentPool);
            currentPool -= heal;
            return heal;
        }

        return 0;
    }
}
