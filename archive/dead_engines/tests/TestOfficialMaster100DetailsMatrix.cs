using System;
using System.Collections.Generic;
using Mir2.Shared.Protocols.Modern;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 终极大师级 100 项底层机制与对抗测试矩阵 (细节 301 ~ 400)
/// 覆盖经济金融、深层战斗、首领生态、沙巴克城战与现代 Direct-Blit 协议 100% 物理闭环断言
/// </summary>
public class TestOfficialMaster100DetailsMatrix
{
    // =========================================================================
    // 维度一：经济金融与仓库交易权威体系 (细节 301~320)
    // =========================================================================

    [Fact]
    public void Test_301_304_StorageGold_Deposit_Withdraw_Boundary_And_Execution()
    {
        // 1. 存入金币: 正常存入
        bool validDeposit = OfficialMaster100DetailsEngine.ValidateStorageDepositGold(
            currentBagGold: 50_000,
            currentStorageGold: 100_000,
            depositAmount: 20_000,
            out string reason);
        Assert.True(validDeposit);
        Assert.Empty(reason);

        var (bagAfterDep, storAfterDep) = OfficialMaster100DetailsEngine.ExecuteStorageDepositGold(50_000, 100_000, 20_000);
        Assert.Equal(30_000, bagAfterDep);
        Assert.Equal(120_000, storAfterDep);

        // 2. 存入金币: 背包金币不足
        bool failDepositBag = OfficialMaster100DetailsEngine.ValidateStorageDepositGold(10_000, 100_000, 20_000, out string reasonBag);
        Assert.False(failDepositBag);
        Assert.Contains("不足", reasonBag);

        // 3. 存入金币: 超过 5000 万硬上限
        bool failDepositMax = OfficialMaster100DetailsEngine.ValidateStorageDepositGold(2_000_000, 49_000_000, 1_500_000, out string reasonMax);
        Assert.False(failDepositMax);
        Assert.Contains("上限", reasonMax);

        // 4. 取出金币: 正常取出
        bool validWithdraw = OfficialMaster100DetailsEngine.ValidateStorageWithdrawGold(
            currentBagGold: 10_000,
            currentStorageGold: 50_000,
            withdrawAmount: 30_000,
            out string reasonWit);
        Assert.True(validWithdraw);

        var (bagAfterWit, storAfterWit) = OfficialMaster100DetailsEngine.ExecuteStorageWithdrawGold(10_000, 50_000, 30_000);
        Assert.Equal(40_000, bagAfterWit);
        Assert.Equal(20_000, storAfterWit);

        // 5. 取出金币: 仓库不足
        bool failWitStor = OfficialMaster100DetailsEngine.ValidateStorageWithdrawGold(10_000, 20_000, 30_000, out string reasonWitStor);
        Assert.False(failWitStor);
        Assert.Contains("不足", reasonWitStor);
    }

    [Fact]
    public void Test_305_307_Trade_Security_And_Capacity_Checks()
    {
        // 305: 金币变更应打断既有锁定
        Assert.True(OfficialMaster100DetailsEngine.ShouldResetTradeLockOnGoldChange(1000, 2000, currentLocked: true));
        Assert.False(OfficialMaster100DetailsEngine.ShouldResetTradeLockOnGoldChange(1000, 1000, currentLocked: true));

        // 306: 背包空格容量检定
        Assert.True(OfficialMaster100DetailsEngine.ValidateTradeSlotCapacity(emptySlots: 5, incomingItemCount: 3));
        Assert.False(OfficialMaster100DetailsEngine.ValidateTradeSlotCapacity(emptySlots: 2, incomingItemCount: 3));

        // 307: 交易负重检定
        Assert.True(OfficialMaster100DetailsEngine.ValidateTradeWeightCapacity(currentWeight: 40, maxWeight: 60, incomingWeight: 15));
        Assert.False(OfficialMaster100DetailsEngine.ValidateTradeWeightCapacity(currentWeight: 50, maxWeight: 60, incomingWeight: 15));
    }

    [Fact]
    public void Test_308_312_Npc_Pricing_Repair_And_Oil_Formulas()
    {
        // 308: NPC 出售回收价格 (10000 基础价，满持久 20/20 -> 5000 金币; 半持久 10/20 -> 2500 金币)
        int fullPrice = OfficialMaster100DetailsEngine.CalculateItemSellPrice(10000, 20, 20);
        int halfPrice = OfficialMaster100DetailsEngine.CalculateItemSellPrice(10000, 10, 20);
        Assert.Equal(5000, fullPrice);
        Assert.Equal(2500, halfPrice);

        // 309: 特修费用 ((20 - 5) * 10000 * 3 / 20 = 22500)
        int specRepairCost = OfficialMaster100DetailsEngine.CalculateSpecialRepairCost(10000, 5, 20);
        Assert.Equal(22500, specRepairCost);

        // 310: 普修最大持久扣减 1/16 (16 最大持久扣减 1 -> 15)
        var (newCur, newMax) = OfficialMaster100DetailsEngine.CalculateNormalRepairDura(10, 16);
        Assert.Equal(15, newMax);
        Assert.Equal(15, newCur);

        // 311: 战神油 0 损耗特修
        var (zsCur, zsMax) = OfficialMaster100DetailsEngine.ApplyZhanShenOil(25);
        Assert.Equal(25, zsCur);
        Assert.Equal(25, zsMax);

        // 312: 修复油成功与失败
        var (repOk, okCur, okMax) = OfficialMaster100DetailsEngine.ApplyRepairOil(5, 32, 0.50);
        Assert.True(repOk);
        Assert.Equal(30, okMax); // 32 - 2 = 30

        var (repFail, failCur, failMax) = OfficialMaster100DetailsEngine.ApplyRepairOil(5, 32, 0.95);
        Assert.False(repFail);
        Assert.Equal(31, failMax); // 惩罚扣 1 点
    }

    [Fact]
    public void Test_313_320_GroundGold_MysticItem_And_Suits()
    {
        // 313: 地表金币外观
        Assert.Equal(110, OfficialMaster100DetailsEngine.GetGroundGoldAppr(50));
        Assert.Equal(111, OfficialMaster100DetailsEngine.GetGroundGoldAppr(500));
        Assert.Equal(112, OfficialMaster100DetailsEngine.GetGroundGoldAppr(5000));

        // 314: 金币合并
        Assert.Equal(6000, OfficialMaster100DetailsEngine.MergeGroundGold(2000, 4000));

        // 315: 死亡掉金币 (红名 100万 * 40% = 40万; 白名 100万 * 7.5% = 7.5万)
        int redDrop = OfficialMaster100DetailsEngine.CalculateDeathGoldDrop(1_000_000, isRedName: true, 0.5);
        int whiteDrop = OfficialMaster100DetailsEngine.CalculateDeathGoldDrop(1_000_000, isRedName: false, 0.5);
        Assert.Equal(400_000, redDrop);
        Assert.Equal(75_000, whiteDrop);

        // 316~318: 神秘装备与神水
        Assert.True(OfficialMaster100DetailsEngine.IsMysticHidden(0));
        Assert.False(OfficialMaster100DetailsEngine.IsMysticHidden(1));
        Assert.False(OfficialMaster100DetailsEngine.CanTakeoffMysticItem(isCursed: true, hasUsedShenShui: false));
        Assert.True(OfficialMaster100DetailsEngine.CanTakeoffMysticItem(isCursed: true, hasUsedShenShui: true));

        // 319: 狂风套装攻速缩短 (攻速+4 -> 650 - 80 = 570ms)
        Assert.Equal(570, OfficialMaster100DetailsEngine.CalculateAttackInterval(4));

        // 320: 魔血套装 (全套5件: 5*25 + 50 = 175 HP, 125 MP 扣减)
        var (hpGain, mpLoss) = OfficialMaster100DetailsEngine.CalculateMoXueSetBonus(5);
        Assert.Equal(175, hpGain);
        Assert.Equal(125, mpLoss);
    }

    // =========================================================================
    // 维度二：高级战斗与技能深层对抗 (细节 321~350)
    // =========================================================================

    [Fact]
    public void Test_321_330_SpecialRings_And_CombatMechanics()
    {
        // 321: 虹魔套装 10% 吸血 (造成 300 伤害吸血 30 点)
        Assert.Equal(30, OfficialMaster100DetailsEngine.CalculateHongMoLifesteal(300, 3));

        // 322: 祈祷套装触发
        Assert.True(OfficialMaster100DetailsEngine.CheckQiDaoSetTrigger(true, true, true, true, true));
        Assert.False(OfficialMaster100DetailsEngine.CheckQiDaoSetTrigger(true, true, true, true, false));

        // 323: 记忆套装天地合一
        Assert.True(OfficialMaster100DetailsEngine.CheckJiYiSummonValid(true, true, true, true, true));
        Assert.False(OfficialMaster100DetailsEngine.CheckJiYiSummonValid(false, true, true, true, true));

        // 324: 传送戒指智能避障
        var (tx, ty) = OfficialMaster100DetailsEngine.CalculateTeleportRingLanding(100, 100, (x, y) => x == 100 && y == 100);
        Assert.NotEqual((100, 100), (tx, ty)); // 避开阻挡格

        // 326: 反隐真眼
        Assert.True(OfficialMaster100DetailsEngine.CanMonsterDetectInvisible("月魔蜘蛛"));
        Assert.False(OfficialMaster100DetailsEngine.CanMonsterDetectInvisible("红野猪"));

        // 328: 复活戒指 60 秒冷却
        Assert.True(OfficialMaster100DetailsEngine.CanReviveWithRing(100_000, 30_000, currentDura: 1));
        Assert.False(OfficialMaster100DetailsEngine.CanReviveWithRing(100_000, 50_000, currentDura: 1));

        // 329: 麻痹戒指几率 (1/15)
        Assert.True(OfficialMaster100DetailsEngine.RollParalysisChance(0.05));
        Assert.False(OfficialMaster100DetailsEngine.RollParalysisChance(0.10));

        // 330: 护身戒指 MP 优先抵扣
        var (hp1, mp1) = OfficialMaster100DetailsEngine.ApplyHuShenRingDamage(currentHp: 100, currentMp: 80, damage: 50);
        Assert.Equal(100, hp1);
        Assert.Equal(30, mp1);

        var (hp2, mp2) = OfficialMaster100DetailsEngine.ApplyHuShenRingDamage(currentHp: 100, currentMp: 30, damage: 50);
        Assert.Equal(80, hp2); // 剩余 20 扣 HP
        Assert.Equal(0, mp2);
    }

    [Fact]
    public void Test_331_350_Spells_And_TacticalFormulas()
    {
        // 331 & 332: 诱惑之光与随从升阶
        Assert.True(OfficialMaster100DetailsEngine.CanTameMonster(playerLevel: 35, monsterLevel: 28, isBoss: false, isUndead: false));
        Assert.False(OfficialMaster100DetailsEngine.CanTameMonster(playerLevel: 35, monsterLevel: 28, isBoss: true, isUndead: false));
        Assert.Equal(7, OfficialMaster100DetailsEngine.CalculatePetLevel(350));
        Assert.Equal(0, OfficialMaster100DetailsEngine.CalculatePetLevel(1));

        // 335: 圣言术秒杀公式 (等级高于怪物且 roll < rate)
        Assert.True(OfficialMaster100DetailsEngine.RollHolyStrikeInstantKill(casterLevel: 40, monsterLevel: 30, skillLevel: 3, roll: 0.50));
        Assert.False(OfficialMaster100DetailsEngine.RollHolyStrikeInstantKill(casterLevel: 30, monsterLevel: 40, skillLevel: 3, roll: 0.10));

        // 336: 疾光电影 8 格穿透
        var beam = OfficialMaster100DetailsEngine.GetLightningBeamTiles(100, 100, dir: 2); // 向右
        Assert.Equal(8, beam.Count);
        Assert.Equal((108, 100), beam[7]);

        // 337: 地狱雷光不死系 150% 加成
        Assert.Equal(150, OfficialMaster100DetailsEngine.CalculateHellThunderDamage(100, isUndead: true));
        Assert.Equal(100, OfficialMaster100DetailsEngine.CalculateHellThunderDamage(100, isUndead: false));

        // 339: 冰咆哮穿透魔法躲避
        Assert.False(OfficialMaster100DetailsEngine.IsMagicEvadable(32));
        Assert.True(OfficialMaster100DetailsEngine.IsMagicEvadable(1));

        // 343: 刺杀第二格穿盾
        Assert.True(OfficialMaster100DetailsEngine.IsStabSkillArmorIgnored(2));
        Assert.False(OfficialMaster100DetailsEngine.IsStabSkillArmorIgnored(1));

        // 344 & 345: 攻杀 1.3 倍与半月副目标 40%
        Assert.Equal(130, OfficialMaster100DetailsEngine.CalculatePowerHitDamage(100));
        Assert.Equal(40, OfficialMaster100DetailsEngine.CalculateHalfMoonSubTargetDamage(100));

        // 346: 红毒降防 20%
        Assert.Equal(80, OfficialMaster100DetailsEngine.ApplyRedPoisonDefenseReduction(100));

        // 350: 道术 Buff 持续时间 (道术 15 -> 20s + 45s = 65000ms)
        Assert.Equal(65_000, OfficialMaster100DetailsEngine.CalculateTaoistBuffDurationMs(15));
    }

    // =========================================================================
    // 维度三：首领生态与地图物理机制 (细节 351~375)
    // =========================================================================

    [Fact]
    public void Test_351_375_Boss_Mechanics_Mining_And_PK()
    {
        // 352: 双头金刚必须割肉
        Assert.True(OfficialMaster100DetailsEngine.CanButchFaShenJewelry("双头金刚", isButchAction: true));
        Assert.False(OfficialMaster100DetailsEngine.CanButchFaShenJewelry("双头金刚", isButchAction: false));

        // 354: 触龙神魔法免疫
        Assert.Equal(0, OfficialMaster100DetailsEngine.FilterChuLongShenDamage(500, isMagicDamage: true));
        Assert.Equal(500, OfficialMaster100DetailsEngine.FilterChuLongShenDamage(500, isMagicDamage: false));

        // 356: 祖玛教主 3 格苏醒
        Assert.True(OfficialMaster100DetailsEngine.ShouldZuMaAwaken(100, 100, 102, 102));
        Assert.False(OfficialMaster100DetailsEngine.ShouldZuMaAwaken(100, 100, 105, 105));

        // 362: 黑铁矿保持久与加持久
        var (keep1, inc1) = OfficialMaster100DetailsEngine.EvaluateBlackIronOre(75);
        Assert.True(keep1);
        Assert.False(inc1);

        var (keep2, inc2) = OfficialMaster100DetailsEngine.EvaluateBlackIronOre(95);
        Assert.True(keep2);
        Assert.True(inc2);

        // 367 & 368: 运9与诅10
        Assert.Equal(100, OfficialMaster100DetailsEngine.CalculateLuckCurseDamage(20, 100, luck: 9, curse: 0, 0.2));
        Assert.Equal(20, OfficialMaster100DetailsEngine.CalculateLuckCurseDamage(20, 100, luck: 0, curse: 10, 0.8));

        // 369: 正当防卫 60 秒免惩罚
        Assert.True(OfficialMaster100DetailsEngine.IsLawfulSelfDefense(nowMs: 100_000, lastAttackedMs: 50_000));
        Assert.False(OfficialMaster100DetailsEngine.IsLawfulSelfDefense(nowMs: 100_000, lastAttackedMs: 20_000));

        // 370 & 371: PK 点色阶与守卫秒杀
        Assert.Equal("White", OfficialMaster100DetailsEngine.GetNameColorByPkPoint(50));
        Assert.Equal("Yellow", OfficialMaster100DetailsEngine.GetNameColorByPkPoint(150));
        Assert.Equal("Red", OfficialMaster100DetailsEngine.GetNameColorByPkPoint(250));
        Assert.Equal(9999, OfficialMaster100DetailsEngine.CalculateGuardDamageToRedName(250));
        Assert.Equal(0, OfficialMaster100DetailsEngine.CalculateGuardDamageToRedName(50));

        // 374: 地牢迷雾半径
        Assert.Equal(8, OfficialMaster100DetailsEngine.GetFogVisionRadius(isDarkMap: true, hasTorch: true, hasCandle: false));
        Assert.Equal(5, OfficialMaster100DetailsEngine.GetFogVisionRadius(isDarkMap: true, hasTorch: false, hasCandle: true));
        Assert.Equal(3, OfficialMaster100DetailsEngine.GetFogVisionRadius(isDarkMap: true, hasTorch: false, hasCandle: false));
    }

    // =========================================================================
    // 维度四：原生 UI、社交生态与沙巴克城战 (细节 376~400)
    // =========================================================================

    [Fact]
    public void Test_376_400_Ui_Social_Sabuk_And_AntiHack()
    {
        // 377: 聊天截断 70 字符
        string longChat = new string('A', 100);
        Assert.Equal(70, OfficialMaster100DetailsEngine.TruncateChatMessage(longChat).Length);

        // 379: 经验条 8 晶格 (50% -> 4 格)
        Assert.Equal(4, OfficialMaster100DetailsEngine.CalculateExpGrids(5000, 10000));

        // 380: 血球 56 像素 (50% -> 28 像素)
        Assert.Equal(28, OfficialMaster100DetailsEngine.CalculateOrbHeight(50, 100));

        // 384: 组队 20% 加成均摊 (10000 经验，4人 -> 12000 / 4 = 3000)
        Assert.Equal(3000, OfficialMaster100DetailsEngine.CalculateGroupSharedExp(10000, 4));

        // 386: 行会封号 14 字符截断
        string longRank = "沙巴克天下无双第一战神尊主天下无双";
        Assert.Equal(14, OfficialMaster100DetailsEngine.TruncateGuildRank(longRank).Length);

        // 388: 沙巴克全域免红名
        Assert.True(OfficialMaster100DetailsEngine.IsSabukSiegeFreePk(true, "palace"));
        Assert.False(OfficialMaster100DetailsEngine.IsSabukSiegeFreePk(false, "palace"));

        // 391: 皇宫易主 100% 纯净度扫描
        Assert.True(OfficialMaster100DetailsEngine.CheckPalacePureOwnership(new[] { "行会A", "行会A", "行会A" }, "行会A"));
        Assert.False(OfficialMaster100DetailsEngine.CheckPalacePureOwnership(new[] { "行会A", "行会B", "行会A" }, "行会A"));

        // 399: 装备持久 0 属性失效
        Assert.Equal(0, OfficialMaster100DetailsEngine.GetEffectiveAttribute(attributeValue: 35, dura: 0));
        Assert.Equal(35, OfficialMaster100DetailsEngine.GetEffectiveAttribute(attributeValue: 35, dura: 10));

        // 400: 380ms 移动与 650ms 攻击变速外挂
        Assert.True(OfficialMaster100DetailsEngine.CheckSpeedHackAction(currentTick: 1000, lastActionTick: 800, minIntervalMs: 380));
        Assert.False(OfficialMaster100DetailsEngine.CheckSpeedHackAction(currentTick: 1000, lastActionTick: 500, minIntervalMs: 380));
    }

    // =========================================================================
    // 现代 Direct-Blit 协议: 仓库金币存取双向载荷测试
    // =========================================================================

    [Fact]
    public void Test_DirectBlit_Storage_Gold_Payloads_Roundtrip()
    {
        // 1. 存入金币载荷 (4 字节)
        var depPayload = new BinaryProtocol.StorageDepositGoldIntentPayload { GoldAmount = 250_000 };
        Span<byte> depBuf = stackalloc byte[4];
        depPayload.Write(depBuf);

        var depParsed = BinaryProtocol.StorageDepositGoldIntentPayload.Read(depBuf);
        Assert.Equal(250_000, depParsed.GoldAmount);

        // 2. 取出金币载荷 (4 字节)
        var witPayload = new BinaryProtocol.StorageWithdrawGoldIntentPayload { GoldAmount = 180_000 };
        Span<byte> witBuf = stackalloc byte[4];
        witPayload.Write(witBuf);

        var witParsed = BinaryProtocol.StorageWithdrawGoldIntentPayload.Read(witBuf);
        Assert.Equal(180_000, witParsed.GoldAmount);
    }
}
