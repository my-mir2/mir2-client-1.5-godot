using System;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 原版第 1551 ~ 1650 号微观细节规则权威测试矩阵 (Wave 94)
/// </summary>
public class TestOfficialWave94_100DetailsMythicMatrix
{
    // =========================================================================
    // 细节 1551 ~ 1565：沃玛寺庙与沃玛教主神殿全生态权威规则
    // =========================================================================

    [Fact]
    public void Test1551_CalculateWoomaFogViewDistance()
    {
        Assert.Equal(8, OfficialMythicApex100DetailsEngine.CalculateWoomaFogViewDistance(8, false));
        Assert.Equal(4, OfficialMythicApex100DetailsEngine.CalculateWoomaFogViewDistance(8, true));
    }

    [Fact]
    public void Test1552_ShouldPlayWoomaTempleAmbientSound()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.ShouldPlayWoomaTempleAmbientSound(30, 30, 32, 30)); // distSq = 4 <= 64
        Assert.False(OfficialMythicApex100DetailsEngine.ShouldPlayWoomaTempleAmbientSound(30, 30, 45, 30)); // distSq = 225 > 64
    }

    [Fact]
    public void Test1553_CalculateWoomaFireDamageDecay()
    {
        Assert.Equal(100, OfficialMythicApex100DetailsEngine.CalculateWoomaFireDamageDecay(100, 1));
        Assert.Equal(85, OfficialMythicApex100DetailsEngine.CalculateWoomaFireDamageDecay(100, 2)); // decay 15%
    }

    [Fact]
    public void Test1554_CalculateWoomaGuardBurstRadius()
    {
        Assert.Equal(4, OfficialMythicApex100DetailsEngine.CalculateWoomaGuardBurstRadius(42));
        Assert.Equal(3, OfficialMythicApex100DetailsEngine.CalculateWoomaGuardBurstRadius(35));
    }

    [Fact]
    public void Test1555_CheckWoomaLeaderEmergencyTeleport()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CheckWoomaLeaderEmergencyTeleport(500, 2000, 10)); // HP 25% < 30% & roll 10 < 25
        Assert.False(OfficialMythicApex100DetailsEngine.CheckWoomaLeaderEmergencyTeleport(1000, 2000, 10)); // HP 50%
        Assert.False(OfficialMythicApex100DetailsEngine.CheckWoomaLeaderEmergencyTeleport(500, 2000, 50)); // roll 50 >= 25
    }

    [Fact]
    public void Test1556_GetWoomaLeaderSummonGuardCount()
    {
        Assert.Equal(4, OfficialMythicApex100DetailsEngine.GetWoomaLeaderSummonGuardCount(true));
        Assert.Equal(2, OfficialMythicApex100DetailsEngine.GetWoomaLeaderSummonGuardCount(false));
    }

    [Fact]
    public void Test1557_CalculateWoomaLeaderLightningEffect()
    {
        var (pushed1, breakShield1) = OfficialMythicApex100DetailsEngine.CalculateWoomaLeaderLightningEffect(20);
        Assert.True(pushed1);
        Assert.True(breakShield1);

        var (pushed2, breakShield2) = OfficialMythicApex100DetailsEngine.CalculateWoomaLeaderLightningEffect(80);
        Assert.False(pushed2);
        Assert.False(breakShield2);
    }

    [Fact]
    public void Test1558_CanPickupWoomaHorn()
    {
        // 60秒内非同一行会不可拾取
        Assert.False(OfficialMythicApex100DetailsEngine.CanPickupWoomaHorn(1001, 1002, 30));
        // 同一行会可拾取
        Assert.True(OfficialMythicApex100DetailsEngine.CanPickupWoomaHorn(1001, 1001, 30));
        // 超出60秒任意行会可拾取
        Assert.True(OfficialMythicApex100DetailsEngine.CanPickupWoomaHorn(1001, 1002, 65));
    }

    [Fact]
    public void Test1559_CheckWoomaWarriorParalyzeResist()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CheckWoomaWarriorParalyzeResist(20));
        Assert.False(OfficialMythicApex100DetailsEngine.CheckWoomaWarriorParalyzeResist(50));
    }

    [Fact]
    public void Test1560_CalculateTorchLightRadius()
    {
        Assert.Equal(5, OfficialMythicApex100DetailsEngine.CalculateTorchLightRadius(800, 1000));
        Assert.Equal(3, OfficialMythicApex100DetailsEngine.CalculateTorchLightRadius(400, 1000));
        Assert.Equal(2, OfficialMythicApex100DetailsEngine.CalculateTorchLightRadius(100, 1000));
    }

    [Fact]
    public void Test1561_IsWoomaTemplePillarBlocked()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.IsWoomaTemplePillarBlocked(24, 24));
        Assert.False(OfficialMythicApex100DetailsEngine.IsWoomaTemplePillarBlocked(20, 20));
    }

    [Fact]
    public void Test1562_CalculateDarkWarriorMoveIntervalMs()
    {
        Assert.Equal(360, OfficialMythicApex100DetailsEngine.CalculateDarkWarriorMoveIntervalMs(600));
    }

    [Fact]
    public void Test1563_CanRepelDarkWarrior()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CanRepelDarkWarrior(35, 30));
        Assert.False(OfficialMythicApex100DetailsEngine.CanRepelDarkWarrior(28, 30));
    }

    [Fact]
    public void Test1564_ValidateCreateGuildWithHorn()
    {
        var (ok1, _) = OfficialMythicApex100DetailsEngine.ValidateCreateGuildWithHorn(22, 1_000_000, true);
        Assert.True(ok1);

        var (ok2, _) = OfficialMythicApex100DetailsEngine.ValidateCreateGuildWithHorn(22, 1_000_000, false);
        Assert.False(ok2);

        var (ok3, _) = OfficialMythicApex100DetailsEngine.ValidateCreateGuildWithHorn(20, 1_000_000, true);
        Assert.False(ok3);
    }

    [Fact]
    public void Test1565_AdjustDropTileIfBlocked()
    {
        bool IsWalkable(int x, int y) => !(x == 10 && y == 10);
        var (adjX, adjY) = OfficialMythicApex100DetailsEngine.AdjustDropTileIfBlocked(10, 10, IsWalkable);
        Assert.Equal(9, adjY);
    }

    // =========================================================================
    // 细节 1566 ~ 1580：黄泉教主与封魔谷殿堂战术
    // =========================================================================

    [Fact]
    public void Test1566_CalculateHuangQuanMeteorDamage()
    {
        var (hp1, shield1) = OfficialMythicApex100DetailsEngine.CalculateHuangQuanMeteorDamage(100, false);
        Assert.Equal(100, hp1);
        Assert.Equal(0, shield1);

        var (hp2, shield2) = OfficialMythicApex100DetailsEngine.CalculateHuangQuanMeteorDamage(100, true);
        Assert.Equal(40, hp2);
        Assert.Equal(60, shield2);
    }

    [Fact]
    public void Test1567_CheckHuangQuanSuperWeaponDrop()
    {
        Assert.Equal("龙牙", OfficialMythicApex100DetailsEngine.CheckHuangQuanSuperWeaponDrop(0));
        Assert.Equal("怒斩", OfficialMythicApex100DetailsEngine.CheckHuangQuanSuperWeaponDrop(1));
        Assert.Equal("逍遥扇", OfficialMythicApex100DetailsEngine.CheckHuangQuanSuperWeaponDrop(2));
        Assert.Equal("None", OfficialMythicApex100DetailsEngine.CheckHuangQuanSuperWeaponDrop(3));
    }

    [Fact]
    public void Test1568_ShouldRevealHiddenPlayer()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.ShouldRevealHiddenPlayer(20, 20, 22, 20));
        Assert.False(OfficialMythicApex100DetailsEngine.ShouldRevealHiddenPlayer(20, 20, 30, 20));
    }

    [Fact]
    public void Test1569_ShouldAlertBatAlly()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.ShouldAlertBatAlly(5, 5, 8, 7));
        Assert.False(OfficialMythicApex100DetailsEngine.ShouldAlertBatAlly(5, 5, 12, 12));
    }

    [Fact]
    public void Test1570_CalculateHongMoDrain()
    {
        var (hp, mp) = OfficialMythicApex100DetailsEngine.CalculateHongMoDrain(120);
        Assert.Equal(36, hp); // 120 * 0.30
        Assert.Equal(12, mp); // 120 * 0.10
    }

    [Fact]
    public void Test1571_CheckZombieResurrection()
    {
        var (rev1, exp1) = OfficialMythicApex100DetailsEngine.CheckZombieResurrection(160, 10);
        Assert.True(rev1);
        Assert.Equal(80, exp1);

        var (rev2, exp2) = OfficialMythicApex100DetailsEngine.CheckZombieResurrection(160, 50);
        Assert.False(rev2);
        Assert.Equal(160, exp2);
    }

    [Fact]
    public void Test1572_CalculateMillenniumTreeAttack()
    {
        var (dmg1, poison1) = OfficialMythicApex100DetailsEngine.CalculateMillenniumTreeAttack(4);
        Assert.Equal(45, dmg1);
        Assert.True(poison1);

        var (dmg2, poison2) = OfficialMythicApex100DetailsEngine.CalculateMillenniumTreeAttack(8);
        Assert.Equal(0, dmg2);
        Assert.False(poison2);
    }

    [Fact]
    public void Test1573_CalculateGuardExecutionDamage()
    {
        Assert.Equal(9999, OfficialMythicApex100DetailsEngine.CalculateGuardExecutionDamage(true));
        Assert.Equal(0, OfficialMythicApex100DetailsEngine.CalculateGuardExecutionDamage(false));
    }

    [Fact]
    public void Test1574_IsScrollAllowedInSecretTunnel()
    {
        Assert.False(OfficialMythicApex100DetailsEngine.IsScrollAllowedInSecretTunnel("D2008"));
        Assert.True(OfficialMythicApex100DetailsEngine.IsScrollAllowedInSecretTunnel("0"));
    }

    [Fact]
    public void Test1575_CheckSpeedHackAction()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CheckSpeedHackAction(1000, 1400)); // 400ms < 550ms
        Assert.False(OfficialMythicApex100DetailsEngine.CheckSpeedHackAction(1000, 1600)); // 600ms >= 550ms
    }

    [Fact]
    public void Test1576_CalculateEliteGuardMoveInterval()
    {
        Assert.Equal(480, OfficialMythicApex100DetailsEngine.CalculateEliteGuardMoveInterval(800, 200, 1000));
        Assert.Equal(800, OfficialMythicApex100DetailsEngine.CalculateEliteGuardMoveInterval(800, 500, 1000));
    }

    [Fact]
    public void Test1577_GetEvilSnakePoisonEffect()
    {
        var (sec, dps) = OfficialMythicApex100DetailsEngine.GetEvilSnakePoisonEffect();
        Assert.Equal(12, sec);
        Assert.Equal(8, dps);
    }

    [Fact]
    public void Test1578_GetStorageServiceFee()
    {
        Assert.Equal(0, OfficialMythicApex100DetailsEngine.GetStorageServiceFee(true));
        Assert.Equal(10, OfficialMythicApex100DetailsEngine.GetStorageServiceFee(false));
    }

    [Fact]
    public void Test1579_CheckWedgeMothDispelShield()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CheckWedgeMothDispelShield(5));
        Assert.False(OfficialMythicApex100DetailsEngine.CheckWedgeMothDispelShield(20));
    }

    [Fact]
    public void Test1580_IsBookLootProtected()
    {
        Assert.False(OfficialMythicApex100DetailsEngine.IsBookLootProtected(1000, 5000, 101, 101)); // 击杀者本人
        Assert.True(OfficialMythicApex100DetailsEngine.IsBookLootProtected(1000, 5000, 101, 102));  // 他人且在60秒内
        Assert.False(OfficialMythicApex100DetailsEngine.IsBookLootProtected(1000, 70000, 101, 102)); // 超过60秒保护期
    }

    // =========================================================================
    // 细节 1581 ~ 1595：毒符药剂重量沉降与包囊极值
    // =========================================================================

    [Fact]
    public void Test1581_CanPlayerCarryExtraWeight()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CanPlayerCarryExtraWeight(100, 200, 3, 10)); // 100 + 30 <= 200
        Assert.False(OfficialMythicApex100DetailsEngine.CanPlayerCarryExtraWeight(190, 200, 3, 5)); // 190 + 15 > 200
    }

    [Fact]
    public void Test1582_ConsumeTaoistAmulet()
    {
        var (ok1, rem1) = OfficialMythicApex100DetailsEngine.ConsumeTaoistAmulet(100, 1);
        Assert.True(ok1);
        Assert.Equal(99, rem1);

        var (ok2, rem2) = OfficialMythicApex100DetailsEngine.ConsumeTaoistAmulet(0, 1);
        Assert.False(ok2);
        Assert.Equal(0, rem2);
    }

    [Fact]
    public void Test1583_ShouldDropItemToGroundWhenBagFull()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.ShouldDropItemToGroundWhenBagFull(40));
        Assert.False(OfficialMythicApex100DetailsEngine.ShouldDropItemToGroundWhenBagFull(39));
    }

    [Fact]
    public void Test1584_CanPickupGoldWhenBagSlotsFull()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CanPickupGoldWhenBagSlotsFull(40));
    }

    [Fact]
    public void Test1585_CanCreateGuild()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CanCreateGuild(true, 1_000_000));
        Assert.False(OfficialMythicApex100DetailsEngine.CanCreateGuild(false, 1_000_000));
        Assert.False(OfficialMythicApex100DetailsEngine.CanCreateGuild(true, 900_000));
    }

    [Fact]
    public void Test1586_CanReapplyGuildAlliance()
    {
        const long OneDayMs = 24 * 3600 * 1000L;
        Assert.True(OfficialMythicApex100DetailsEngine.CanReapplyGuildAlliance(1000, 1000 + OneDayMs + 10));
        Assert.False(OfficialMythicApex100DetailsEngine.CanReapplyGuildAlliance(1000, 1000 + OneDayMs - 10));
    }

    [Fact]
    public void Test1587_ShouldIncreasePkPointsDuringGuildWar()
    {
        Assert.False(OfficialMythicApex100DetailsEngine.ShouldIncreasePkPointsDuringGuildWar(true));
        Assert.True(OfficialMythicApex100DetailsEngine.ShouldIncreasePkPointsDuringGuildWar(false));
    }

    [Fact]
    public void Test1588_ApplySabukDiscountPrice()
    {
        Assert.Equal(80, OfficialMythicApex100DetailsEngine.ApplySabukDiscountPrice(100, true));
        Assert.Equal(100, OfficialMythicApex100DetailsEngine.ApplySabukDiscountPrice(100, false));
    }

    [Fact]
    public void Test1589_ApplyRedPoisonReduction()
    {
        var (def, mdef) = OfficialMythicApex100DetailsEngine.ApplyRedPoisonReduction(50, 40);
        Assert.Equal(40, def);
        Assert.Equal(32, mdef);
    }

    [Fact]
    public void Test1590_CalculateGreenPoisonDamagePerTick()
    {
        Assert.Equal(6, OfficialMythicApex100DetailsEngine.CalculateGreenPoisonDamagePerTick(30));
        Assert.Equal(2, OfficialMythicApex100DetailsEngine.CalculateGreenPoisonDamagePerTick(5));
    }

    [Fact]
    public void Test1591_CanPassThroughEntity()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CanPassThroughEntity(false, true));
        Assert.True(OfficialMythicApex100DetailsEngine.CanPassThroughEntity(true, false));
        Assert.False(OfficialMythicApex100DetailsEngine.CanPassThroughEntity(false, false));
    }

    [Fact]
    public void Test1592_CanRepelTarget()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CanRepelTarget(35, 30));
        Assert.False(OfficialMythicApex100DetailsEngine.CanRepelTarget(30, 30));
        Assert.False(OfficialMythicApex100DetailsEngine.CanRepelTarget(28, 30));
    }

    [Fact]
    public void Test1593_CheckLureSuccess()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CheckLureSuccess(30, 25, 3, 50));
        Assert.False(OfficialMythicApex100DetailsEngine.CheckLureSuccess(20, 35, 0, 50));
    }

    [Fact]
    public void Test1594_CheckHolyTurnUndead()
    {
        Assert.False(OfficialMythicApex100DetailsEngine.CheckHolyTurnUndead(false, 40, 20, 10)); // 非不死系必定失败
        Assert.True(OfficialMythicApex100DetailsEngine.CheckHolyTurnUndead(true, 40, 20, 50));   // 20 + 20*3 = 80 > 50
    }

    [Fact]
    public void Test1595_ShouldBreakCloakOnAction()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.ShouldBreakCloakOnAction(true));
        Assert.False(OfficialMythicApex100DetailsEngine.ShouldBreakCloakOnAction(false));
    }

    // =========================================================================
    // 细节 1596 ~ 1610：武器升级碎刀残留与开刀机制实证
    // =========================================================================

    [Fact]
    public void Test1596_CalculateWeaponUpgradeDuraDelta()
    {
        Assert.Equal(1000, OfficialMythicApex100DetailsEngine.CalculateWeaponUpgradeDuraDelta(65));
        Assert.Equal(-1000, OfficialMythicApex100DetailsEngine.CalculateWeaponUpgradeDuraDelta(25));
        Assert.Equal(0, OfficialMythicApex100DetailsEngine.CalculateWeaponUpgradeDuraDelta(45));
    }

    [Fact]
    public void Test1597_CalculateWeaponUpgradeResult()
    {
        var (ok1, bonus1) = OfficialMythicApex100DetailsEngine.CalculateWeaponUpgradeResult(50, 0, 3);
        Assert.True(ok1);
        Assert.Equal(2, bonus1); // 跳点 +2

        var (fail, _) = OfficialMythicApex100DetailsEngine.CalculateWeaponUpgradeResult(10, 5, 95);
        Assert.False(fail);
    }

    [Fact]
    public void Test1598_IsValidSlashTarget()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.IsValidSlashTarget(true));
        Assert.False(OfficialMythicApex100DetailsEngine.IsValidSlashTarget(false));
    }

    [Fact]
    public void Test1599_HandleWeaponShatter()
    {
        Assert.False(OfficialMythicApex100DetailsEngine.HandleWeaponShatter(true));  // 升级成功不碎刀
        Assert.True(OfficialMythicApex100DetailsEngine.HandleWeaponShatter(false)); // 升级失败碎刀
    }

    [Fact]
    public void Test1600_CheckZeroDuraAttack()
    {
        var (atk1, brk1) = OfficialMythicApex100DetailsEngine.CheckZeroDuraAttack(50, 5000, 5);
        Assert.Equal(50, atk1);
        Assert.False(brk1);

        var (atk2, brk2) = OfficialMythicApex100DetailsEngine.CheckZeroDuraAttack(50, 0, 5);
        Assert.Equal(0, atk2);
        Assert.True(brk2); // 0持久且随机<10折断
    }

    [Fact]
    public void Test1601_RepairWithZhanshenOil()
    {
        var (dura, maxDura) = OfficialMythicApex100DetailsEngine.RepairWithZhanshenOil(25000);
        Assert.Equal(25000, dura);
        Assert.Equal(25000, maxDura);
    }

    [Fact]
    public void Test1602_RepairWithBlacksmith()
    {
        Assert.Equal(24000, OfficialMythicApex100DetailsEngine.RepairWithBlacksmith(25000, 1));
        Assert.Equal(25000, OfficialMythicApex100DetailsEngine.RepairWithBlacksmith(25000, 50));
    }

    [Fact]
    public void Test1603_DrinkBlessingOil()
    {
        var (ok1, luck1, _) = OfficialMythicApex100DetailsEngine.DrinkBlessingOil(3, 10);
        Assert.True(ok1);
        Assert.Equal(4, luck1);

        var (ok2, luck2, cursed) = OfficialMythicApex100DetailsEngine.DrinkBlessingOil(3, 95);
        Assert.False(ok2);
        Assert.Equal(2, luck2);
        Assert.True(cursed);

        var (ok3, luck3, _) = OfficialMythicApex100DetailsEngine.DrinkBlessingOil(7, 10);
        Assert.False(ok3);
        Assert.Equal(7, luck3);
    }

    [Fact]
    public void Test1604_CalculateDamageWithLuck()
    {
        Assert.Equal(60, OfficialMythicApex100DetailsEngine.CalculateDamageWithLuck(20, 60, 9, 0)); // 运9必定60
        Assert.Equal(20, OfficialMythicApex100DetailsEngine.CalculateDamageWithLuck(20, 60, -9, 99)); // 诅9必定20
    }

    [Fact]
    public void Test1605_IsCursedMinAttackForced()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.IsCursedMinAttackForced(10));
        Assert.False(OfficialMythicApex100DetailsEngine.IsCursedMinAttackForced(5));
    }

    [Fact]
    public void Test1606_CanRetrieveUpgradedWeapon()
    {
        const long OneHourMs = 60 * 60 * 1000L;
        Assert.True(OfficialMythicApex100DetailsEngine.CanRetrieveUpgradedWeapon(1000, 1000 + OneHourMs + 10));
        Assert.False(OfficialMythicApex100DetailsEngine.CanRetrieveUpgradedWeapon(1000, 1000 + OneHourMs - 10));
    }

    [Fact]
    public void Test1607_CanDepositAnotherWeapon()
    {
        Assert.False(OfficialMythicApex100DetailsEngine.CanDepositAnotherWeapon(true));
        Assert.True(OfficialMythicApex100DetailsEngine.CanDepositAnotherWeapon(false));
    }

    [Fact]
    public void Test1608_CanUpgradeWeaponFurther()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CanUpgradeWeaponFurther(6));
        Assert.False(OfficialMythicApex100DetailsEngine.CanUpgradeWeaponFurther(7));
    }

    [Fact]
    public void Test1609_GetUpgradeGoldCost()
    {
        Assert.Equal(10000, OfficialMythicApex100DetailsEngine.GetUpgradeGoldCost(0));
        Assert.Equal(25000, OfficialMythicApex100DetailsEngine.GetUpgradeGoldCost(3));
    }

    [Fact]
    public void Test1610_CanTriggerUpgradeSlashOnTeammate()
    {
        Assert.False(OfficialMythicApex100DetailsEngine.CanTriggerUpgradeSlashOnTeammate(true));
        Assert.True(OfficialMythicApex100DetailsEngine.CanTriggerUpgradeSlashOnTeammate(false));
    }

    // =========================================================================
    // 细节 1611 ~ 1625：官方 1.5 特戒系统物理真值与极品属性权威矩阵
    // =========================================================================

    [Fact]
    public void Test1611_CheckResurrectionRingTrigger()
    {
        var (rev1, hp1, mp1, dura1) = OfficialMythicApex100DetailsEngine.CheckResurrectionRingTrigger(0, 500, 300, 5000, 0, 150000);
        Assert.True(rev1);
        Assert.Equal(500, hp1);
        Assert.Equal(300, mp1);
        Assert.Equal(4000, dura1);

        var (rev2, _, _, _) = OfficialMythicApex100DetailsEngine.CheckResurrectionRingTrigger(0, 500, 300, 5000, 100000, 150000);
        Assert.False(rev2); // 冷却中
    }

    [Fact]
    public void Test1612_IsResurrectionRingOnCooldown()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.IsResurrectionRingOnCooldown(100000, 150000));
        Assert.False(OfficialMythicApex100DetailsEngine.IsResurrectionRingOnCooldown(0, 150000));
    }

    [Fact]
    public void Test1613_CalculateProtectionRingDamage()
    {
        var (hp1, mp1) = OfficialMythicApex100DetailsEngine.CalculateProtectionRingDamage(500, 200, 150);
        Assert.Equal(500, hp1);
        Assert.Equal(50, mp1);

        var (hp2, mp2) = OfficialMythicApex100DetailsEngine.CalculateProtectionRingDamage(500, 100, 150);
        Assert.Equal(450, hp2);
        Assert.Equal(0, mp2);
    }

    [Fact]
    public void Test1614_CheckParalysisRingEffect()
    {
        var (paralyzed1, dur1) = OfficialMythicApex100DetailsEngine.CheckParalysisRingEffect(0);
        Assert.True(paralyzed1);
        Assert.Equal(5000, dur1);

        var (paralyzed2, dur2) = OfficialMythicApex100DetailsEngine.CheckParalysisRingEffect(5);
        Assert.False(paralyzed2);
        Assert.Equal(0, dur2);
    }

    [Fact]
    public void Test1615_CanMonsterDetectInvisibleRing()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CanMonsterDetectInvisibleRing(32)); // 楔蛾破隐
        Assert.True(OfficialMythicApex100DetailsEngine.CanMonsterDetectInvisibleRing(88)); // 赤月恶魔破隐
        Assert.False(OfficialMythicApex100DetailsEngine.CanMonsterDetectInvisibleRing(10)); // 普通怪
    }

    [Fact]
    public void Test1616_ValidateTeleportRingDestination()
    {
        bool IsWalkable(int x, int y) => !(x == 50 && y == 50);
        Assert.True(OfficialMythicApex100DetailsEngine.ValidateTeleportRingDestination(30, 30, IsWalkable));
        Assert.False(OfficialMythicApex100DetailsEngine.ValidateTeleportRingDestination(50, 50, IsWalkable));
        Assert.False(OfficialMythicApex100DetailsEngine.ValidateTeleportRingDestination(-1, 30, IsWalkable));
    }

    [Fact]
    public void Test1617_HasHealingSpellGrantedByRing()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.HasHealingSpellGrantedByRing(1001, 0));
        Assert.False(OfficialMythicApex100DetailsEngine.HasHealingSpellGrantedByRing(1002, 0));
    }

    [Fact]
    public void Test1618_HasFireballSpellGrantedByRing()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.HasFireballSpellGrantedByRing(1002));
        Assert.False(OfficialMythicApex100DetailsEngine.HasFireballSpellGrantedByRing(1001));
    }

    [Fact]
    public void Test1619_CalculateLoadRingMaxWeights()
    {
        var (b, w, h) = OfficialMythicApex100DetailsEngine.CalculateLoadRingMaxWeights(100, 50, 30);
        Assert.Equal(200, b);
        Assert.Equal(100, w);
        Assert.Equal(60, h);
    }

    [Fact]
    public void Test1620_CalculateSkillAmuletExpGain()
    {
        Assert.Equal(2, OfficialMythicApex100DetailsEngine.CalculateSkillAmuletExpGain(1, true));
        Assert.Equal(1, OfficialMythicApex100DetailsEngine.CalculateSkillAmuletExpGain(1, false));
    }

    [Fact]
    public void Test1621_IsArenaBoundaryBlocked()
    {
        Assert.False(OfficialMythicApex100DetailsEngine.IsArenaBoundaryBlocked(50, 50, 10, 100, 10, 100));
        Assert.True(OfficialMythicApex100DetailsEngine.IsArenaBoundaryBlocked(5, 50, 10, 100, 10, 100));
    }

    [Fact]
    public void Test1622_GetPrayerRebellionDurationSeconds()
    {
        Assert.Equal(180, OfficialMythicApex100DetailsEngine.GetPrayerRebellionDurationSeconds());
    }

    [Fact]
    public void Test1623_GetMemorySetMaxSummonCount()
    {
        Assert.Equal(10, OfficialMythicApex100DetailsEngine.GetMemorySetMaxSummonCount());
    }

    [Fact]
    public void Test1624_IsMysteryItemLocked()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.IsMysteryItemLocked(false, 0));
        Assert.False(OfficialMythicApex100DetailsEngine.IsMysteryItemLocked(true, 0));
        Assert.False(OfficialMythicApex100DetailsEngine.IsMysteryItemLocked(false, 100));
    }

    [Fact]
    public void Test1625_CanEquipZhanShenArmor()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CanEquipZhanShenArmor(46));
        Assert.False(OfficialMythicApex100DetailsEngine.CanEquipZhanShenArmor(45));
    }

    // =========================================================================
    // 细节 1626 ~ 1640：行会战火、皇宫密道与城战指挥
    // =========================================================================

    [Fact]
    public void Test1626_IsDoorwayOccupied()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.IsDoorwayOccupied(true));
        Assert.False(OfficialMythicApex100DetailsEngine.IsDoorwayOccupied(false));
    }

    [Fact]
    public void Test1627_CheckPalaceCaptureTime()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CheckPalaceCaptureTime(30));
        Assert.False(OfficialMythicApex100DetailsEngine.CheckPalaceCaptureTime(29));
    }

    [Fact]
    public void Test1628_GetSecretPassageFee()
    {
        Assert.Equal(0, OfficialMythicApex100DetailsEngine.GetSecretPassageFee(true));
        Assert.Equal(5000, OfficialMythicApex100DetailsEngine.GetSecretPassageFee(false));
    }

    [Fact]
    public void Test1629_DamageSiegeBanner()
    {
        var (dest1, msg1) = OfficialMythicApex100DetailsEngine.DamageSiegeBanner(50000, 1000);
        Assert.False(dest1);
        Assert.Empty(msg1);

        var (dest2, msg2) = OfficialMythicApex100DetailsEngine.DamageSiegeBanner(500, 1000);
        Assert.True(dest2);
        Assert.Contains("沙巴克城门战旗", msg2);
    }

    [Fact]
    public void Test1630_SanitizeGuildNotice()
    {
        string raw = "沙巴克行会\r\n诚招天下英雄\n入会发沃玛套！";
        string cleaned = OfficialMythicApex100DetailsEngine.SanitizeGuildNotice(raw);
        Assert.DoesNotContain("\r", cleaned);
        Assert.DoesNotContain("\n", cleaned);
    }

    [Fact]
    public void Test1631_CanAbdicateGuildLeader()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CanAbdicateGuildLeader(true, true, true));
        Assert.False(OfficialMythicApex100DetailsEngine.CanAbdicateGuildLeader(true, true, false));
    }

    [Fact]
    public void Test1632_CheckGuildChatCooldown()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CheckGuildChatCooldown(1000, 2000));
        Assert.False(OfficialMythicApex100DetailsEngine.CheckGuildChatCooldown(1000, 1500));
    }

    [Fact]
    public void Test1633_GetSiegeRespawnMap()
    {
        Assert.Equal("0150", OfficialMythicApex100DetailsEngine.GetSiegeRespawnMap(true));
        Assert.Equal("3", OfficialMythicApex100DetailsEngine.GetSiegeRespawnMap(false));
    }

    [Fact]
    public void Test1634_GetArcherRespawnCost()
    {
        Assert.Equal(100_000, OfficialMythicApex100DetailsEngine.GetArcherRespawnCost());
    }

    [Fact]
    public void Test1635_CanModifyMemberRank()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.CanModifyMemberRank(1));
        Assert.True(OfficialMythicApex100DetailsEngine.CanModifyMemberRank(2));
        Assert.False(OfficialMythicApex100DetailsEngine.CanModifyMemberRank(3));
    }

    [Fact]
    public void Test1636_IsSiegeApplicationValid()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.IsSiegeApplicationValid(true, 3));
        Assert.False(OfficialMythicApex100DetailsEngine.IsSiegeApplicationValid(false, 3));
        Assert.False(OfficialMythicApex100DetailsEngine.IsSiegeApplicationValid(true, 2));
    }

    [Fact]
    public void Test1637_RepairSabukGate()
    {
        var (ok, gold) = OfficialMythicApex100DetailsEngine.RepairSabukGate(600_000);
        Assert.True(ok);
        Assert.Equal(100_000, gold);

        var (fail, orig) = OfficialMythicApex100DetailsEngine.RepairSabukGate(400_000);
        Assert.False(fail);
        Assert.Equal(400_000, orig);
    }

    [Fact]
    public void Test1638_CalculateDailyTaxRevenue()
    {
        Assert.Equal(200_000, OfficialMythicApex100DetailsEngine.CalculateDailyTaxRevenue());
    }

    [Fact]
    public void Test1639_CanJoinNewGuildAfterKick()
    {
        const long TwelveHoursMs = 12 * 3600 * 1000L;
        Assert.True(OfficialMythicApex100DetailsEngine.CanJoinNewGuildAfterKick(1000, 1000 + TwelveHoursMs + 10));
        Assert.False(OfficialMythicApex100DetailsEngine.CanJoinNewGuildAfterKick(1000, 1000 + TwelveHoursMs - 10));
    }

    [Fact]
    public void Test1640_CalculateSiegeFireWallDamage()
    {
        Assert.Equal(120, OfficialMythicApex100DetailsEngine.CalculateSiegeFireWallDamage(100, true, true));
        Assert.Equal(100, OfficialMythicApex100DetailsEngine.CalculateSiegeFireWallDamage(100, true, false));
        Assert.Equal(100, OfficialMythicApex100DetailsEngine.CalculateSiegeFireWallDamage(100, false, true));
    }

    // =========================================================================
    // 细节 1641 ~ 1650：Direct-Blit 协议与 AOT 零分配压测保障
    // =========================================================================

    [Fact]
    public void Test1641_ValidateDirectBlitMagic()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.ValidateDirectBlitMagic(0xAA55));
        Assert.False(OfficialMythicApex100DetailsEngine.ValidateDirectBlitMagic(0x1234));
    }

    [Fact]
    public void Test1642_ValidateSequenceNumber()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.ValidateSequenceNumber(100, 100));
        Assert.True(OfficialMythicApex100DetailsEngine.ValidateSequenceNumber(100, 150));
        Assert.False(OfficialMythicApex100DetailsEngine.ValidateSequenceNumber(100, 99)); // 旧包丢弃
        Assert.False(OfficialMythicApex100DetailsEngine.ValidateSequenceNumber(100, 170)); // 超出窗口64
    }

    [Fact]
    public void Test1643_IsHeartbeatTimedOut()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.IsHeartbeatTimedOut(1000, 6001));
        Assert.False(OfficialMythicApex100DetailsEngine.IsHeartbeatTimedOut(1000, 5000));
    }

    [Fact]
    public void Test1644_IsPacketLengthValid()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.IsPacketLengthValid(128));
        Assert.True(OfficialMythicApex100DetailsEngine.IsPacketLengthValid(4096));
        Assert.False(OfficialMythicApex100DetailsEngine.IsPacketLengthValid(0));
        Assert.False(OfficialMythicApex100DetailsEngine.IsPacketLengthValid(4097));
    }

    [Fact]
    public void Test1645_GetDirectBlitHeaderSize()
    {
        Assert.Equal(12, OfficialMythicApex100DetailsEngine.GetDirectBlitHeaderSize());
    }

    [Fact]
    public void Test1646_CalculateBackoffInterval()
    {
        Assert.Equal(100, OfficialMythicApex100DetailsEngine.CalculateBackoffInterval(100, 0));
        Assert.Equal(200, OfficialMythicApex100DetailsEngine.CalculateBackoffInterval(100, 1));
        Assert.Equal(1600, OfficialMythicApex100DetailsEngine.CalculateBackoffInterval(100, 4));
        Assert.Equal(3200, OfficialMythicApex100DetailsEngine.CalculateBackoffInterval(200, 5));
    }

    [Fact]
    public void Test1647_HasSufficientBufferSpace()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.HasSufficientBufferSpace(1024, 500, 500));
        Assert.False(OfficialMythicApex100DetailsEngine.HasSufficientBufferSpace(1024, 600, 500));
    }

    [Fact]
    public void Test1648_ComputeCrc16()
    {
        byte[] testData = [0x01, 0x02, 0x03, 0x04];
        ushort crc1 = OfficialMythicApex100DetailsEngine.ComputeCrc16(testData);
        ushort crc2 = OfficialMythicApex100DetailsEngine.ComputeCrc16(testData);
        Assert.Equal(crc1, crc2);
        Assert.NotEqual(0, crc1);
    }

    [Fact]
    public void Test1649_IsInputRateExceeded()
    {
        Assert.True(OfficialMythicApex100DetailsEngine.IsInputRateExceeded(21));
        Assert.False(OfficialMythicApex100DetailsEngine.IsInputRateExceeded(20));
    }

    [Fact]
    public void Test1650_ReconcileClientStateWithServerSnapshot()
    {
        int clientSpeculativeHp = 250;
        int serverAuthoritativeHp = 180;
        int reconciled = OfficialMythicApex100DetailsEngine.ReconcileClientStateWithServerSnapshot(clientSpeculativeHp, serverAuthoritativeHp);
        Assert.Equal(serverAuthoritativeHp, reconciled);
    }
}
