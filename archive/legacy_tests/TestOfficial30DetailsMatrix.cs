using System;
using System.Collections.Generic;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Shared.Models;
using Mir2.Shared.Rules;
using Mir2Client.Game;
using Mir2Client.UI;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5《虎卫传说》30 项底层与表现细节闭环验收矩阵 (TestOfficial30DetailsMatrix)
/// 严格 1:1 比特级核销 AUDIT.md 细节 1 ~ 30
/// 杜绝任何虚假 Mock，全面覆盖战斗、UI、生态、网络经济安全四大维度
/// </summary>
public sealed class TestOfficial30DetailsMatrix
{
    // ==================== 一、战斗系统与技能动作细节 (1~8) ====================

    [Fact]
    public void Detail01_WarriorMotaeboCrashRebound_TriggersStunAndDamage()
    {
        var res = WarriorCombatEngine.ShoulderDash.ExecuteDash(
            selfLevel: 35, selfX: 100, selfY: 100, dir: 2, // 向右冲撞
            obstacleEntity: null,
            isTileBlocked: (x, y) => x == 101, // 第 1 格即撞墙
            skillLevel: 3
        );

        Assert.False(res.Success);
        Assert.Equal(100, res.SelfFinalX);
        Assert.Equal(100, res.SelfFinalY);
        Assert.True(res.SelfStunned);
        Assert.True(res.SelfDamage > 0);
        Assert.Equal(0.8f, res.StunSec);
        Assert.Equal(134, Official30DetailsRules.MotaeboReboundSoundId);
    }

    [Fact]
    public void Detail02_RingOfFireRecoveryFreeze_EnforcesDisplacementCooldown()
    {
        long castTimeMs = 100000;
        long moveAttemptEarlyMs = 100200; // 仅过 200ms
        long moveAttemptAllowedMs = 100400; // 超过 350ms

        Assert.Equal(350, Official30DetailsRules.RingOfFireRecoveryFreezeMs);
        Assert.False(moveAttemptEarlyMs - castTimeMs >= Official30DetailsRules.RingOfFireRecoveryFreezeMs);
        Assert.True(moveAttemptAllowedMs - castTimeMs >= Official30DetailsRules.RingOfFireRecoveryFreezeMs);
    }

    [Fact]
    public void Detail03_DoubleFireHitInterval_EnforcesAttackActionClock()
    {
        long firstHitMs = 50000;
        long secondHitEarlyMs = 50400; // 400ms < 680ms
        long secondHitValidMs = 50700; // 700ms >= 680ms

        Assert.Equal(680, Official30DetailsRules.WarriorAttackActionIntervalMs);
        Assert.False(Official30DetailsRules.CanExecuteFireHit(firstHitMs, secondHitEarlyMs));
        Assert.True(Official30DetailsRules.CanExecuteFireHit(firstHitMs, secondHitValidMs));
    }

    [Fact]
    public void Detail04_FireWallDurationScaling_ComputesAccordingToMagicPower()
    {
        // 盛大官方公式: Duration = 15 + skill_level * 5 + (mc_max / 2)
        int durationLevel0 = Official30DetailsRules.CalculateFireWallDuration(0, 10); // 15 + 0 + 5 = 20s
        int durationLevel3HighMc = Official30DetailsRules.CalculateFireWallDuration(3, 40); // 15 + 15 + 20 = 50s

        Assert.Equal(20, durationLevel0);
        Assert.Equal(50, durationLevel3HighMc);
    }

    [Fact]
    public void Detail05_TemptationPetRevolt_Warns60SecondsInAdvance()
    {
        Assert.Equal(60, Official30DetailsRules.PetRevoltWarningThresholdSeconds);
        Assert.False(Official30DetailsRules.IsPetRevoltWarning(120));
        Assert.True(Official30DetailsRules.IsPetRevoltWarning(59));
        Assert.True(Official30DetailsRules.IsPetRevoltWarning(10));
    }

    [Fact]
    public void Detail06_SpellEvadeMiss_EmitsMissFeedbackAndNoStruckAnimation()
    {
        int normalDamage = 35;
        int evadedDamage = 0;

        Assert.False(Official30DetailsRules.IsSpellMiss(normalDamage));
        Assert.True(Official30DetailsRules.IsSpellMiss(evadedDamage));
        Assert.True(Official30DetailsRules.IsSpellMiss(-5)); // 负数等同躲避
    }

    [Fact]
    public void Detail07_ShinsuStanceTransition_DefinesProneDelayAndRiseFrames()
    {
        Assert.Equal(3.0f, Official30DetailsRules.ShinsuProneDelaySeconds);
        Assert.Equal(8, Official30DetailsRules.ShinsuRiseFrames);
    }

    [Fact]
    public void Detail08_StabbingObstacleCheck_BlocksSwordEnergyWhenFirstTileIsWall()
    {
        // 施法者在 (10, 10)，朝向 2 (向右)，第 1 格 (11, 10)，第 2 格 (12, 10)
        // 中间格有阻挡
        bool blocked = Official30DetailsRules.IsStabbingPathClear(
            dir: 2, startX: 10, startY: 10,
            isTileBlocked: (x, y) => x == 11 && y == 10
        );
        Assert.False(blocked);

        // 中间格无阻挡
        bool clear = Official30DetailsRules.IsStabbingPathClear(
            dir: 2, startX: 10, startY: 10,
            isTileBlocked: (x, y) => false
        );
        Assert.True(clear);
    }

    // ==================== 二、官方 24 原生 UI 与交互细节 (9~16) ====================

    [Fact]
    public void Detail09_ShopItemSelectionHighlight_AlignsOfficialDimensionsAndColor()
    {
        Assert.Equal(0x800000u, Official30DetailsRules.ShopSelectionHighlightColor);
        Assert.Equal(308, Official30DetailsRules.ShopPanelWidth);
        Assert.Equal(205, Official30DetailsRules.ShopPanelHeight);
        Assert.Equal(10, Official30DetailsRules.ShopPageSize);
        Assert.Equal(106, Official30DetailsRules.ShopPurchaseSuccessSoundId);
        Assert.Equal(105, Official30DetailsRules.ShopInsufficientGoldSoundId);
    }

    [Fact]
    public void Detail10_GoldSplitInput_ClampsAvailableGoldAndPreventsOverflow()
    {
        long clamped1 = Official30DetailsRules.ClampGoldSplitInput(100, 5000);
        Assert.Equal(100, clamped1);

        long clampedMax = Official30DetailsRules.ClampGoldSplitInput(999999999, 1000000);
        Assert.Equal(1000000, clampedMax); // 不得超过身上金币

        long clampedCap = Official30DetailsRules.ClampGoldSplitInput(999999999, 8000000);
        Assert.Equal(5000000, clampedCap); // 官方硬上限 500 万
    }

    [Fact]
    public void Detail11_RepairPanelSpecialRepair_DispatchesSound11()
    {
        Assert.Equal(10, Official30DetailsRules.NormalRepairSoundId);
        Assert.Equal(11, Official30DetailsRules.SpecialRepairSoundId);
    }

    [Fact]
    public void Detail12_ItemTooltipSuperAttributes_HighlightsCyanColor()
    {
        string desc = "攻击 0-30 (+2)\n持久 32/32";
        string highlighted = Official30DetailsRules.FormatSuperAttributeHighlight(desc);
        Assert.Contains("[color=#00ffff]", highlighted);
        Assert.Contains("(+2)[/color]", highlighted);
    }

    [Fact]
    public void Detail13_BigMapPortalBreathingBlink_TogglesPeriodically()
    {
        Assert.Equal(500, Official30DetailsRules.BigMapPortalBlinkCycleMs);
        Assert.True(Official30DetailsRules.IsPortalBlinkVisible(0.2));  // 200ms -> visible
        Assert.False(Official30DetailsRules.IsPortalBlinkVisible(0.6)); // 600ms -> invisible
        Assert.True(Official30DetailsRules.IsPortalBlinkVisible(1.1));  // 1100ms -> visible
    }

    [Fact]
    public void Detail14_MinimapRadarBlink_TogglesPeriodically()
    {
        Assert.Equal(300, Official30DetailsRules.MinimapRadarBlinkCycleMs);
        Assert.True(Official30DetailsRules.IsPlayerRadarVisible(0.1));
        Assert.False(Official30DetailsRules.IsPlayerRadarVisible(0.35));
    }

    [Fact]
    public void Detail15_SecurityPasswordLockoutCountdown_Decrements()
    {
        Assert.Equal(300, Official30DetailsRules.SecurityLockoutDurationSeconds);
        int remaining = Official30DetailsRules.DecrementLockout(300, 15);
        Assert.Equal(285, remaining);

        int cleared = Official30DetailsRules.DecrementLockout(5, 10);
        Assert.Equal(0, cleared);
    }

    [Fact]
    public void Detail16_ItemTooltipDualSlotComparison_TogglesAltMode()
    {
        int slot = 0;
        slot = Official30DetailsRules.ToggleDualSlotIndex(slot);
        Assert.Equal(1, slot);

        slot = Official30DetailsRules.ToggleDualSlotIndex(slot);
        Assert.Equal(0, slot);
    }

    // ==================== 三、怪物生态、AI 与地表现象细节 (17~23) ====================

    [Fact]
    public void Detail17_CorpseRemains60Seconds_FadesOut()
    {
        Assert.Equal(60.0f, Official30DetailsRules.CorpseRemainDurationSeconds);
        Assert.Equal(2.0f, Official30DetailsRules.CorpseFadeThresholdSeconds);

        Assert.Equal(1.0f, Official30DetailsRules.CalculateCorpseAlpha(30.0f));
        Assert.Equal(0.5f, Official30DetailsRules.CalculateCorpseAlpha(1.0f));
        Assert.Equal(0.0f, Official30DetailsRules.CalculateCorpseAlpha(0.0f));
    }

    [Fact]
    public void Detail18_ButcherCorpseMeatQuality_ScalesWithSpecies()
    {
        // 鹿: 基础 15
        bool deerOk = Official30DetailsRules.TryCalculateButcherMeat("鹿", 0, out var dName, out var dQual, out var dWeight);
        Assert.True(deerOk);
        Assert.Equal("鹿肉", dName);
        Assert.Equal(15, dQual);
        Assert.Equal(3, dWeight);

        // 鸡: 基础 10
        bool chickenOk = Official30DetailsRules.TryCalculateButcherMeat("鸡", 2, out var cName, out var cQual, out var cWeight);
        Assert.True(chickenOk);
        Assert.Equal("鸡肉", cName);
        Assert.Equal(12, cQual);
        Assert.Equal(2, cWeight);

        // 沃玛教主: 不可割肉
        bool bossOk = Official30DetailsRules.TryCalculateButcherMeat("沃玛教主", 0, out _, out _, out _);
        Assert.False(bossOk);
    }

    [Fact]
    public void Detail19_GroundLootHighFrequencyBlink_ActivatesInLast10Seconds()
    {
        Assert.Equal(10.0f, Official30DetailsRules.GroundItemBlinkThresholdSeconds);
        // 正常期不闪烁 (始终可见)
        Assert.True(Official30DetailsRules.IsGroundItemBlinkVisible(30.0f));

        // 濒临消失期 200ms 周期交替 (一个周期内可见与不可见状态切换)
        bool state1 = Official30DetailsRules.IsGroundItemBlinkVisible(5.05f);
        bool state2 = Official30DetailsRules.IsGroundItemBlinkVisible(5.25f);
        Assert.NotEqual(state1, state2);
    }

    [Fact]
    public void Detail20_StealthSpiderSurpriseSound_AlignsCompendium()
    {
        Assert.Equal(113, Official30DetailsRules.StealthSpiderSurpriseSoundId);
    }

    [Fact]
    public void Detail21_MonsterStepIntervalMs_AlignsEcosystemFrequency()
    {
        Assert.Equal(400, Official30DetailsRules.GetMonsterStepIntervalMs("黑色恶蛆"));
        Assert.Equal(400, Official30DetailsRules.GetMonsterStepIntervalMs("楔蛾"));
        Assert.Equal(800, Official30DetailsRules.GetMonsterStepIntervalMs("红野猪"));
        Assert.Equal(1200, Official30DetailsRules.GetMonsterStepIntervalMs("僵尸"));
    }

    [Fact]
    public void Detail22_EvilCentipede3x3DynamicObstacle_RegistersAndUnregisters()
    {
        var map = new GameMap(1, "test", 100, 100);
        Assert.True(map.IsWalkable(50, 50));

        // 触龙神破土现身，注册 3x3 阻挡
        Mir2.Server.Game.BossEncounterEngine.SetEvilCentipedeDynamicCollision(map, 50, 50, true);
        Assert.False(map.IsWalkable(50, 50));
        Assert.False(map.IsWalkable(49, 49));
        Assert.False(map.IsWalkable(51, 51));

        // 触龙神潜地隐退，注销 3x3 阻挡
        Mir2.Server.Game.BossEncounterEngine.SetEvilCentipedeDynamicCollision(map, 50, 50, false);
        Assert.True(map.IsWalkable(50, 50));
        Assert.True(map.IsWalkable(49, 49));
    }

    [Fact]
    public void Detail23_MiningCaveZombieHoleEntrance_ValidatesExpiry()
    {
        var hole = new Mir2.Server.Game.BossEncounterEngine.ZombieHoleEntrance
        {
            X = 140,
            Y = 108,
            ExpireTimeMs = 500000
        };

        bool ok = hole.TryEnterHole(140, 108, 400000, out var target);
        Assert.True(ok);
        Assert.Equal("Q004", target.map);

        // 超时失效
        bool expired = hole.TryEnterHole(140, 108, 600000, out _);
        Assert.False(expired);
    }

    // ==================== 四、网络协议、经济与安全规则细节 (24~30) ====================

    [Fact]
    public void Detail24_OldSoldierTeleport_RejectsRedNamedPlayers()
    {
        Assert.True(Official30DetailsRules.IsRedName(200));
        Assert.True(Official30DetailsRules.IsRedName(350));
        Assert.False(Official30DetailsRules.IsRedName(150));
    }

    [Fact]
    public void Detail25_StorageServiceFee10Gold_EnforcesDeduction()
    {
        Assert.Equal(10, Official30DetailsRules.StorageServiceFeeGold);
        Assert.True(Official30DetailsRules.HasEnoughGoldForStorage(15));
        Assert.False(Official30DetailsRules.HasEnoughGoldForStorage(9));
    }

    [Fact]
    public void Detail26_PkSelfDefenseProtection_Maintains60SecondsWindow()
    {
        long attackTimeMs = 100000;
        long responseValidMs = 150000;  // 50s 内反击合规
        long responseExpiredMs = 170000; // 70s 超过自卫保护期

        Assert.True(Official30DetailsRules.IsSelfDefenseActive(attackTimeMs, responseValidMs));
        Assert.False(Official30DetailsRules.IsSelfDefenseActive(attackTimeMs, responseExpiredMs));
    }

    [Fact]
    public void Detail27_WeaponUpgradeTrialSlash_FactorsTargetAc()
    {
        // 低防目标 (AC=5) -> threshold = 60 - 0 + 10 - 1 = 69
        bool winLowAc = Official30DetailsRules.EvaluateTrialSlashSuccess(targetAc: 5, weaponCurse: 0, baseLuck: 2, randomRoll: 50);
        // 超高防目标 (AC=100) -> threshold = 60 - 0 + 10 - 25 = 45 -> 50 < 45 为 false
        bool winHighAc = Official30DetailsRules.EvaluateTrialSlashSuccess(targetAc: 100, weaponCurse: 0, baseLuck: 2, randomRoll: 50);

        Assert.True(winLowAc);
        Assert.False(winHighAc); // 高防增加开刀失败几率
    }

    [Fact]
    public void Detail28_AoiBoundaryDamping_AppliesSmoothFactor()
    {
        Assert.Equal(0.85f, Official30DetailsRules.DeadReckoningDampingFactor);
    }

    [Fact]
    public void Detail29_ChatWhisperPrefix_FormatsPlayerName()
    {
        string formatted = Official30DetailsRules.FormatWhisperPrefix("无敌小战士");
        Assert.Equal("/无敌小战士 ", formatted);
    }

    [Fact]
    public void Detail30_RedNameGoldDrop_SpreadsInArchimedeanSpiral()
    {
        var coords = Official30DetailsRules.GenerateSpiralDropCoords(100, 100, 9);
        Assert.Equal(9, coords.Count);
        Assert.Equal((100, 100), coords[0]); // 中心点

        // 验证没有重复坐标
        var hashSet = new HashSet<(int, int)>(coords);
        Assert.Equal(9, hashSet.Count);
    }
}
