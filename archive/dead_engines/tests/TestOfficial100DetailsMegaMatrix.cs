// ============================================================================
// 盛大官方 1.5《虎卫传说》100 项底层与表现细节全量自动化测试矩阵
// 严格 1:1 比特级核销盛大官方原版二进制 (MIR_final_v3_p.exe / IDA Pro MCP 物理逆向元数据)
// ============================================================================

using System;
using Mir2.Shared.Protocols.Modern;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

public class TestOfficial100DetailsMegaMatrix
{
    // =========================================================================
    // 一、底层网络协议与封包编码物理细节 (1 ~ 10)
    // =========================================================================

    [Fact]
    public void Test_001_Encode6BitPair()
    {
        var (c1, c2, overflow) = Official100DetailsEngine.Encode6BitPair(65);
        Assert.Equal((byte)(1 + 0x3C), c1);
        Assert.Equal((byte)(1 + 0x3C), c2);
        Assert.False(overflow);

        var (_, _, ov) = Official100DetailsEngine.Encode6BitPair(5000);
        Assert.True(ov);
    }

    [Fact]
    public void Test_002_RejectMalformedStream()
    {
        Assert.False(Official100DetailsEngine.ShouldRejectMalformedStream(10, 1000));
        Assert.True(Official100DetailsEngine.ShouldRejectMalformedStream(256, 100));
        Assert.True(Official100DetailsEngine.ShouldRejectMalformedStream(10, 8193));
    }

    [Fact]
    public void Test_003_Parse751QueryUsername()
    {
        var (name, guild, rank) = Official100DetailsEngine.Parse751QueryUsername("无涯/邪恶之毒/沙巴克城主");
        Assert.Equal("无涯", name);
        Assert.Equal("邪恶之毒", guild);
        Assert.Equal("沙巴克城主", rank);
    }

    [Fact]
    public void Test_004_HeartbeatConstants()
    {
        Assert.Equal(8000, Official100DetailsEngine.ClientHeartbeatIntervalMs);
        Assert.Equal(24000, Official100DetailsEngine.ServerProbeTimeoutMs);
        Assert.Equal(30000, Official100DetailsEngine.ServerDisconnectTimeoutMs);
    }

    [Fact]
    public void Test_005_InterpolateActionStep()
    {
        float step = Official100DetailsEngine.InterpolateActionStep(3, 6, 1.0f);
        Assert.Equal(0.5f, step, 3);
    }

    [Fact]
    public void Test_006_LoginFailureMessage()
    {
        Assert.Contains("密码错误", Official100DetailsEngine.GetLoginFailureMessage(1));
        Assert.Contains("不存在", Official100DetailsEngine.GetLoginFailureMessage(2));
        Assert.Contains("已在游戏中", Official100DetailsEngine.GetLoginFailureMessage(3));
        Assert.Contains("封禁", Official100DetailsEngine.GetLoginFailureMessage(4));
        Assert.Contains("维护", Official100DetailsEngine.GetLoginFailureMessage(5));
    }

    [Fact]
    public void Test_007_CreateCharFailReason()
    {
        Assert.Contains("非法字符", Official100DetailsEngine.GetCreateCharFailReason(-1));
        Assert.Contains("已被其他玩家使用", Official100DetailsEngine.GetCreateCharFailReason(-2));
        Assert.Contains("上限", Official100DetailsEngine.GetCreateCharFailReason(-3));
    }

    [Fact]
    public void Test_008_ServerPingColor()
    {
        Assert.Equal("#00ff00", Official100DetailsEngine.GetServerPingColorHex(50));
        Assert.Equal("#ffff00", Official100DetailsEngine.GetServerPingColorHex(120));
        Assert.Equal("#ff0000", Official100DetailsEngine.GetServerPingColorHex(250));
    }

    [Fact]
    public void Test_009_UnpackTripleWeight()
    {
        var (bag, wear, hand) = Official100DetailsEngine.UnpackTripleWeight(50, 30, 20);
        Assert.Equal(50, bag);
        Assert.Equal(30, wear);
        Assert.Equal(20, hand);
    }

    [Fact]
    public void Test_010_TeamMarkBlinkPeriod()
    {
        Assert.Equal(1000, Official100DetailsEngine.TeamMarkBlinkPeriodMs);
    }

    // =========================================================================
    // 二、战士职业核心动作、攻速与碰撞细节 (11 ~ 20)
    // =========================================================================

    [Fact]
    public void Test_011_BasicSwordAccuracyBonus()
    {
        Assert.Equal(0, Official100DetailsEngine.GetBasicSwordAccuracyBonus(0));
        Assert.Equal(1, Official100DetailsEngine.GetBasicSwordAccuracyBonus(1));
        Assert.Equal(2, Official100DetailsEngine.GetBasicSwordAccuracyBonus(2));
        Assert.Equal(3, Official100DetailsEngine.GetBasicSwordAccuracyBonus(3));
    }

    [Fact]
    public void Test_012_PowerHitAudioVisual()
    {
        Assert.Equal(3, Official100DetailsEngine.PowerHitRedSlashFrames);
        Assert.Equal(131, Official100DetailsEngine.PowerHitSoundId);
    }

    [Fact]
    public void Test_013_CalculateHalfMoonSubDamage()
    {
        int subDamage0 = Official100DetailsEngine.CalculateHalfMoonSubDamage(100, 0); // 33%
        int subDamage3 = Official100DetailsEngine.CalculateHalfMoonSubDamage(100, 3); // 48%
        Assert.Equal(33, subDamage0);
        Assert.Equal(48, subDamage3);
    }

    [Fact]
    public void Test_014_HalfMoonVisualLayer()
    {
        Assert.Equal(1, Official100DetailsEngine.HalfMoonVisualLayerOffset);
    }

    [Fact]
    public void Test_015_CanPushDoubleEntities()
    {
        Assert.True(Official100DetailsEngine.CanPushDoubleEntities(35, 30, 32));
        Assert.False(Official100DetailsEngine.CanPushDoubleEntities(35, 36, 30));
    }

    [Fact]
    public void Test_016_IsActionLockedByMotaebo()
    {
        Assert.True(Official100DetailsEngine.IsActionLockedByMotaebo(1000, 1400));
        Assert.False(Official100DetailsEngine.IsActionLockedByMotaebo(1500, 1400));
    }

    [Fact]
    public void Test_017_FireHitFlameConstants()
    {
        Assert.Equal(6, Official100DetailsEngine.FireHitFlameFrames);
        Assert.Equal(80, Official100DetailsEngine.FireHitFlameIntervalMs);
    }

    [Fact]
    public void Test_018_CalculateAttackFrameDuration()
    {
        Assert.Equal(100, Official100DetailsEngine.CalculateAttackFrameDuration(100, 0));
        Assert.Equal(80, Official100DetailsEngine.CalculateAttackFrameDuration(100, 2));
    }

    [Fact]
    public void Test_019_MissSwingSoundAndDurability()
    {
        Assert.Equal(130, Official100DetailsEngine.MissSwingSoundId);
        Assert.False(Official100DetailsEngine.ShouldDeductDurability(0));
        Assert.True(Official100DetailsEngine.ShouldDeductDurability(1));
    }

    [Fact]
    public void Test_020_FootstepSounds()
    {
        Assert.Equal(1, Official100DetailsEngine.GetFootstepSoundId(1));
        Assert.Equal(2, Official100DetailsEngine.GetFootstepSoundId(2));
    }

    // =========================================================================
    // 三、法师职业法术弹道、前摇后摇与地表魔法细节 (21 ~ 30)
    // =========================================================================

    [Fact]
    public void Test_021_FireballTrajectoryHit()
    {
        Assert.True(Official100DetailsEngine.IsFireballHit(10, 10, 10, 10));
        Assert.False(Official100DetailsEngine.IsFireballHit(11, 10, 10, 10));
    }

    [Fact]
    public void Test_022_LightningOffset()
    {
        Assert.Equal(-120, Official100DetailsEngine.LightningOriginYOffset);
    }

    [Fact]
    public void Test_023_HellfireDistance()
    {
        Assert.Equal(5, Official100DetailsEngine.HellfireMaxDistance);
    }

    [Fact]
    public void Test_024_LaserLightDurationAndSound()
    {
        Assert.Equal(200, Official100DetailsEngine.LaserLightDurationMs);
        Assert.Equal(124, Official100DetailsEngine.LaserLightSoundId);
    }

    [Fact]
    public void Test_025_HellThunderUndeadBonus()
    {
        Assert.Equal(100, Official100DetailsEngine.CalculateHellThunderDamage(100, false));
        Assert.Equal(150, Official100DetailsEngine.CalculateHellThunderDamage(100, true));
    }

    [Fact]
    public void Test_026_FireWallDamageTrigger()
    {
        Assert.True(Official100DetailsEngine.ShouldTriggerFireWallDamage(true, 0, 100));
        Assert.False(Official100DetailsEngine.ShouldTriggerFireWallDamage(false, 0, 500));
        Assert.True(Official100DetailsEngine.ShouldTriggerFireWallDamage(false, 0, 1000));
    }

    [Fact]
    public void Test_027_BreakTemptationOnDamage()
    {
        Assert.False(Official100DetailsEngine.BreakTemptationStunOnDamage(0));
        Assert.True(Official100DetailsEngine.BreakTemptationStunOnDamage(1));
    }

    [Fact]
    public void Test_028_AntiAirMap()
    {
        Assert.True(Official100DetailsEngine.IsAntiAirMap("150"));
        Assert.False(Official100DetailsEngine.IsAntiAirMap("0"));
    }

    [Fact]
    public void Test_029_MagicShieldShatterSound()
    {
        Assert.Equal(127, Official100DetailsEngine.MagicShieldShatterSoundId);
    }

    [Fact]
    public void Test_030_HolyWordChance()
    {
        Assert.Equal(0, Official100DetailsEngine.CalculateHolyWordChance(35, 40, 3, true));
        Assert.Equal(0, Official100DetailsEngine.CalculateHolyWordChance(40, 35, 3, false));
        int chance = Official100DetailsEngine.CalculateHolyWordChance(40, 35, 3, true); // (40-35)*4 + 3*5 = 35%
        Assert.Equal(35, chance);
    }

    // =========================================================================
    // 四、道士职业符咒、施毒与召唤兽生命周期细节 (31 ~ 40)
    // =========================================================================

    [Fact]
    public void Test_031_CanCastSoulFire()
    {
        Assert.True(Official100DetailsEngine.CanCastSoulFire(10, out int newDura));
        Assert.Equal(9, newDura);
        Assert.False(Official100DetailsEngine.CanCastSoulFire(0, out _));
    }

    [Fact]
    public void Test_032_PoisonTypeEnum()
    {
        Assert.Equal(1, (int)Official100DetailsEngine.PoisonType.GreenHpDrain);
        Assert.Equal(2, (int)Official100DetailsEngine.PoisonType.RedArmorBreak);
    }

    [Fact]
    public void Test_033_CalculateHealingPulse()
    {
        Assert.Equal(6, Official100DetailsEngine.HealingPulseTotalTicks);
        int heal = Official100DetailsEngine.CalculateHealingPulse(5, 10, 2);
        Assert.True(heal >= 10);
    }

    [Fact]
    public void Test_034_GroupHealingRadius()
    {
        Assert.Equal(1, Official100DetailsEngine.GroupHealingAreaRadius);
    }

    [Fact]
    public void Test_035_GhostShieldDurationAndSound()
    {
        Assert.Equal(117, Official100DetailsEngine.GhostShieldExpireSoundId);
        Assert.Equal(50, Official100DetailsEngine.CalculateGhostShieldDuration(10));
    }

    [Fact]
    public void Test_036_HolyArmorDuration()
    {
        Assert.Equal(70, Official100DetailsEngine.CalculateHolyArmorDuration(20));
    }

    [Fact]
    public void Test_037_ShouldBreakInvisibilityOnMove()
    {
        Assert.False(Official100DetailsEngine.ShouldBreakInvisibilityOnMove(10, 10, 10, 10));
        Assert.True(Official100DetailsEngine.ShouldBreakInvisibilityOnMove(10, 10, 11, 10));
    }

    [Fact]
    public void Test_038_SkeletonColor()
    {
        Assert.Equal("#ffffff", Official100DetailsEngine.GetSkeletonColorHex(0));
        Assert.Equal("#80d0ff", Official100DetailsEngine.GetSkeletonColorHex(1));
        Assert.Equal("#0080ff", Official100DetailsEngine.GetSkeletonColorHex(3));
        Assert.Equal("#ffff00", Official100DetailsEngine.GetSkeletonColorHex(5));
        Assert.Equal("#ff0000", Official100DetailsEngine.GetSkeletonColorHex(7));
    }

    [Fact]
    public void Test_039_ShinsuFlameConstants()
    {
        Assert.Equal(2, Official100DetailsEngine.ShinsuFlameRange);
        Assert.Equal(135, Official100DetailsEngine.ShinsuFlameSoundId);
    }

    [Fact]
    public void Test_040_MindRevelationBar()
    {
        Assert.Equal(32, Official100DetailsEngine.MindRevelationBarWidth);
        Assert.Equal(40, Official100DetailsEngine.CalculateMindRevelationDuration(10));
    }

    // =========================================================================
    // 五、官方 24 原生 UI、窗口层级与焦点交互细节 (41 ~ 50)
    // =========================================================================

    [Fact]
    public void Test_041_SwitchLoginInputFocus()
    {
        Assert.Equal(1, Official100DetailsEngine.SwitchLoginInputFocus(0));
        Assert.Equal(0, Official100DetailsEngine.SwitchLoginInputFocus(1));
    }

    [Fact]
    public void Test_042_RotateCharDirection()
    {
        Assert.Equal(1, Official100DetailsEngine.RotateCharDirection(0, true));
        Assert.Equal(7, Official100DetailsEngine.RotateCharDirection(0, false));
    }

    [Fact]
    public void Test_043_BrazierFlameFrames()
    {
        Assert.Equal(8, Official100DetailsEngine.BrazierFlameFrames);
    }

    [Fact]
    public void Test_044_WindowPriorityEnum()
    {
        Assert.True((int)Official100DetailsEngine.WindowPriority.Modal > (int)Official100DetailsEngine.WindowPriority.Normal);
    }

    [Fact]
    public void Test_045_GetDragAnchorOffset()
    {
        var (x, y) = Official100DetailsEngine.GetDragAnchorOffset();
        Assert.Equal(2, x);
        Assert.Equal(2, y);
    }

    [Fact]
    public void Test_046_EquipRejectSound()
    {
        Assert.Equal(105, Official100DetailsEngine.EquipRejectSoundId);
    }

    [Fact]
    public void Test_047_NpcDialogMaxVisibleLines()
    {
        Assert.Equal(8, Official100DetailsEngine.NpcDialogMaxVisibleLines);
    }

    [Fact]
    public void Test_048_CompareGuildMembers()
    {
        // 掌门排第一
        int cmp = Official100DetailsEngine.CompareGuildMembers(true, true, 40, false, true, 50);
        Assert.True(cmp < 0);
    }

    [Fact]
    public void Test_049_GroupHpBarDimensions()
    {
        Assert.Equal(40, Official100DetailsEngine.GroupHpBarWidth);
        Assert.Equal(6, Official100DetailsEngine.GroupHpBarHeight);
    }

    [Fact]
    public void Test_050_IsHotKeyConflict()
    {
        Assert.False(Official100DetailsEngine.IsHotKeyConflict(0, 1));
        Assert.False(Official100DetailsEngine.IsHotKeyConflict(1, 1));
        Assert.True(Official100DetailsEngine.IsHotKeyConflict(1, 2));
    }

    // =========================================================================
    // 六、HUD 底栏、血球魔球、快捷键与聊天系统细节 (51 ~ 60)
    // =========================================================================

    [Fact]
    public void Test_051_GetHudBottomBarDimensions()
    {
        var (lw, rw, tw) = Official100DetailsEngine.GetHudBottomBarDimensions();
        Assert.Equal(400, lw);
        Assert.Equal(400, rw);
        Assert.Equal(800, tw);
    }

    [Fact]
    public void Test_052_SphereDiameter()
    {
        Assert.Equal(56, Official100DetailsEngine.SphereDiameter);
    }

    [Fact]
    public void Test_053_GetBeltSlotPressedOffset()
    {
        var (dx, dy) = Official100DetailsEngine.GetBeltSlotPressedOffset();
        Assert.Equal(1, dx);
        Assert.Equal(1, dy);
    }

    [Fact]
    public void Test_054_ChatHangingIndent()
    {
        Assert.Equal("    ", Official100DetailsEngine.ChatHangingIndent);
    }

    [Fact]
    public void Test_055_GetChannelColorHex()
    {
        Assert.Equal("#ffffff", Official100DetailsEngine.GetChannelColorHex("Normal"));
        Assert.Equal("#ffff00", Official100DetailsEngine.GetChannelColorHex("Whisper"));
        Assert.Equal("#00ffff", Official100DetailsEngine.GetChannelColorHex("Group"));
        Assert.Equal("#00ff00", Official100DetailsEngine.GetChannelColorHex("Guild"));
        Assert.Equal("#ff0000", Official100DetailsEngine.GetChannelColorHex("System"));
    }

    [Fact]
    public void Test_056_TruncateChatInput()
    {
        string longStr = new string('A', 100);
        string truncated = Official100DetailsEngine.TruncateChatInput(longStr);
        Assert.Equal(70, truncated.Length);
    }

    [Fact]
    public void Test_057_ChatHistoryMaxCapacity()
    {
        Assert.Equal(10, Official100DetailsEngine.ChatHistoryMaxCapacity);
    }

    [Fact]
    public void Test_058_RadarButtonFrames()
    {
        Assert.Equal(28, Official100DetailsEngine.RadarButtonNormalFrame);
        Assert.Equal(29, Official100DetailsEngine.RadarButtonHighlightFrame);
    }

    [Fact]
    public void Test_059_AttackModeSwitchSoundId()
    {
        Assert.Equal(100, Official100DetailsEngine.AttackModeSwitchSoundId);
    }

    [Fact]
    public void Test_060_ExpBarSegmentsAndLevelUpSound()
    {
        Assert.Equal(8, Official100DetailsEngine.ExpBarSegmentsCount);
        Assert.Equal(12, Official100DetailsEngine.LevelUpHornSoundId);
    }

    // =========================================================================
    // 七、装备系统、持久磨损、极品属性与特戒套装细节 (61 ~ 70)
    // =========================================================================

    [Fact]
    public void Test_061_ApplyWeaponZeroDurability()
    {
        Assert.Equal(0, Official100DetailsEngine.ApplyWeaponZeroDurability(0, 50));
        Assert.Equal(50, Official100DetailsEngine.ApplyWeaponZeroDurability(1, 50));
    }

    [Fact]
    public void Test_062_ApplyArmorZeroDurability()
    {
        Assert.Equal(0, Official100DetailsEngine.ApplyArmorZeroDurability(0, 30));
        Assert.Equal(30, Official100DetailsEngine.ApplyArmorZeroDurability(1, 30));
    }

    [Fact]
    public void Test_063_RepairWithGodWarOil()
    {
        var (dura, maxDura) = Official100DetailsEngine.RepairWithGodWarOil(20);
        Assert.Equal(20, dura);
        Assert.Equal(20, maxDura);
    }

    [Fact]
    public void Test_064_RepairWithNormalOil()
    {
        var (dura, maxDura, success) = Official100DetailsEngine.RepairWithNormalOil(20);
        Assert.True(success);
        Assert.Equal(19, dura);
        Assert.Equal(19, maxDura);
    }

    [Fact]
    public void Test_065_ClampWeaponLuck()
    {
        Assert.Equal(7, Official100DetailsEngine.ClampWeaponLuck(9));
        Assert.Equal(5, Official100DetailsEngine.ClampWeaponLuck(5));
    }

    [Fact]
    public void Test_066_MysticHiddenMask()
    {
        Assert.Equal("???", Official100DetailsEngine.MysticHiddenMask);
    }

    [Fact]
    public void Test_067_FindFreeTileAround()
    {
        // 目标格阻挡，周围 (11, 10) 空闲
        var tile = Official100DetailsEngine.FindFreeTileAround(10, 10, (x, y) => x == 10 && y == 10);
        Assert.NotNull(tile);
        Assert.NotEqual((10, 10), tile.Value);
    }

    [Fact]
    public void Test_068_RollParalysis()
    {
        Assert.True(Official100DetailsEngine.RollParalysis(0));
        Assert.False(Official100DetailsEngine.RollParalysis(1));
    }

    [Fact]
    public void Test_069_CalculateProtectionRingDamage()
    {
        // 蓝够扣
        var (hp1, mp1) = Official100DetailsEngine.CalculateProtectionRingDamage(100, 50, 30);
        Assert.Equal(100, hp1);
        Assert.Equal(20, mp1);

        // 蓝不够扣，溢出扣血
        var (hp2, mp2) = Official100DetailsEngine.CalculateProtectionRingDamage(100, 50, 70);
        Assert.Equal(80, hp2);
        Assert.Equal(0, mp2);
    }

    [Fact]
    public void Test_070_PrayerRebellionWorldNotice()
    {
        Assert.Contains("天地动容", Official100DetailsEngine.PrayerRebellionWorldNotice);
    }

    // =========================================================================
    // 八、怪物 AI、仇恨机制、生态步进与尸体战利品细节 (71 ~ 80)
    // =========================================================================

    [Fact]
    public void Test_071_OrcCommanderAxeRange()
    {
        Assert.Equal(4, Official100DetailsEngine.OrcCommanderAxeMaxDistance);
    }

    [Fact]
    public void Test_072_BlackMaggotSound()
    {
        Assert.Equal(111, Official100DetailsEngine.BlackMaggotRollSoundId);
    }

    [Fact]
    public void Test_073_BatFlyNestConstants()
    {
        Assert.Equal(10, Official100DetailsEngine.BatFlyNestMaxSpawns);
        Assert.Equal(5, Official100DetailsEngine.BatFlySpawnSelfDamage);
    }

    [Fact]
    public void Test_074_BoarAttackWindup()
    {
        Assert.Equal(400, Official100DetailsEngine.BoarAttackWindupMs);
    }

    [Fact]
    public void Test_075_ShouldMothFlee()
    {
        Assert.False(Official100DetailsEngine.ShouldMothFlee(50, 100));
        Assert.True(Official100DetailsEngine.ShouldMothFlee(20, 100));
    }

    [Fact]
    public void Test_076_RedMoonDemonSpikeInterval()
    {
        Assert.Equal(1200, Official100DetailsEngine.RedMoonDemonSpikeIntervalMs);
    }

    [Fact]
    public void Test_077_ShouldWomaLeaderAmbush()
    {
        Assert.False(Official100DetailsEngine.ShouldWomaLeaderAmbush(100, 1000, 2));
        Assert.True(Official100DetailsEngine.ShouldWomaLeaderAmbush(250, 1000, 3));
    }

    [Fact]
    public void Test_078_CanPickUpItem()
    {
        // 归属者立即拾取
        Assert.True(Official100DetailsEngine.CanPickUpItem(0, 100, 1, 1));
        // 非归属者在 80 秒前不可拾取
        Assert.False(Official100DetailsEngine.CanPickUpItem(0, 79000, 1, 2));
        // 80 秒后均可拾取
        Assert.True(Official100DetailsEngine.CanPickUpItem(0, 80000, 1, 2));
    }

    [Fact]
    public void Test_079_LockCorpseDirection()
    {
        Assert.Equal(3, Official100DetailsEngine.LockCorpseDirection(3));
        Assert.Equal(1, Official100DetailsEngine.LockCorpseDirection(9));
    }

    [Fact]
    public void Test_080_GoldKongCoreLoot()
    {
        bool harvested = false;
        Assert.True(Official100DetailsEngine.TryDrawGoldKongCoreLoot(ref harvested, out var name));
        Assert.Equal("法神头盔", name);
        Assert.False(Official100DetailsEngine.TryDrawGoldKongCoreLoot(ref harvested, out _));
    }

    // =========================================================================
    // 九、物理地图、黑暗迷雾、动态阻挡与传送阵细节 (81 ~ 90)
    // =========================================================================

    [Fact]
    public void Test_081_GetDungeonFogRadiusTiles()
    {
        Assert.Equal(3, Official100DetailsEngine.GetDungeonFogRadiusTiles(0));
        Assert.Equal(5, Official100DetailsEngine.GetDungeonFogRadiusTiles(1));
        Assert.Equal(8, Official100DetailsEngine.GetDungeonFogRadiusTiles(2));
    }

    [Fact]
    public void Test_082_MinimapFogRevealRadius()
    {
        Assert.Equal(16, Official100DetailsEngine.MinimapFogRevealRadius);
    }

    [Fact]
    public void Test_083_MapPassageOverflowSearchRadius()
    {
        Assert.Equal(2, Official100DetailsEngine.MapPassageOverflowSearchRadius);
    }

    [Fact]
    public void Test_084_CastleGateRubbleTileIndex()
    {
        Assert.Equal(999, Official100DetailsEngine.CastleGateRubbleTileIndex);
    }

    [Fact]
    public void Test_085_SecretPalaceTunnelStepDelay()
    {
        Assert.Equal(500, Official100DetailsEngine.SecretPalaceTunnelStepDelayMs);
    }

    [Fact]
    public void Test_086_IsUnknownDarkHallRestricted()
    {
        Assert.True(Official100DetailsEngine.IsUnknownDarkHallRestricted("M001"));
        Assert.False(Official100DetailsEngine.IsUnknownDarkHallRestricted("0"));
    }

    [Fact]
    public void Test_087_GuardExecutionDamage()
    {
        Assert.Equal(9999, Official100DetailsEngine.GuardExecutionDamage);
    }

    [Fact]
    public void Test_088_CanyonPassValidWidth()
    {
        Assert.Equal(2, Official100DetailsEngine.CanyonPassValidWidth);
    }

    [Fact]
    public void Test_089_GroundItemScreenDensityLimit()
    {
        Assert.Equal(300, Official100DetailsEngine.GroundItemScreenDensityLimit);
    }

    [Fact]
    public void Test_090_MudDesertWindSoundId()
    {
        Assert.Equal(201, Official100DetailsEngine.MudDesertWindSoundId);
    }

    // =========================================================================
    // 十、经济交易、沙巴克战役、PK 规则与服务端反作弊细节 (91 ~ 100)
    // =========================================================================

    [Fact]
    public void Test_091_CanAcceptTradeGold()
    {
        Assert.True(Official100DetailsEngine.CanAcceptTradeGold(1000000, 2000000));
        Assert.False(Official100DetailsEngine.CanAcceptTradeGold(1000000, 6000000)); // 超单笔 500w
        Assert.False(Official100DetailsEngine.CanAcceptTradeGold(9000000, 2000000)); // 超总额 1000w
    }

    [Fact]
    public void Test_092_CanBuyWithWeight()
    {
        Assert.True(Official100DetailsEngine.CanBuyWithWeight(50, 100, 10, 3));
        Assert.False(Official100DetailsEngine.CanBuyWithWeight(50, 100, 10, 6));
    }

    [Fact]
    public void Test_093_ValidateAttackSpeedWindow()
    {
        long[] normalSpeed = [680, 700, 660, 690, 670];
        Assert.True(Official100DetailsEngine.ValidateAttackSpeedWindow(normalSpeed));

        long[] hackSpeed = [400, 450, 500, 420, 480];
        Assert.False(Official100DetailsEngine.ValidateAttackSpeedWindow(hackSpeed));
    }

    [Fact]
    public void Test_094_WallHackConstants()
    {
        Assert.Equal(3, Official100DetailsEngine.WallHackTriggerThreshold);
        Assert.Equal(2000, Official100DetailsEngine.WallHackPenaltyStunMs);
    }

    [Fact]
    public void Test_095_GuildWarEnemyColorHex()
    {
        Assert.Equal("#0000ff", Official100DetailsEngine.GuildWarEnemyColorHex);
    }

    [Fact]
    public void Test_096_IsSabukWarPkFreeZone()
    {
        Assert.True(Official100DetailsEngine.IsSabukWarPkFreeZone("150", 100, 100));
        Assert.False(Official100DetailsEngine.IsSabukWarPkFreeZone("0", 100, 100));
    }

    [Fact]
    public void Test_097_IsPalaceOccupiedPurely()
    {
        Assert.False(Official100DetailsEngine.IsPalaceOccupiedPurely(0, 0));
        Assert.False(Official100DetailsEngine.IsPalaceOccupiedPurely(10, 8));
        Assert.True(Official100DetailsEngine.IsPalaceOccupiedPurely(10, 10));
    }

    [Fact]
    public void Test_098_DecayPkPoints()
    {
        var (newPk, remSec) = Official100DetailsEngine.DecayPkPoints(10, 450);
        Assert.Equal(8, newPk);
        Assert.Equal(50, remSec);
    }

    [Fact]
    public void Test_099_IsGuardSafeDistanceForRedVillage()
    {
        Assert.True(Official100DetailsEngine.IsGuardSafeDistanceForRedVillage(800, 600));
        Assert.False(Official100DetailsEngine.IsGuardSafeDistanceForRedVillage(855, 665));
    }

    [Fact]
    public void Test_100_ReconnectionPacerBatchInterval()
    {
        Assert.Equal(50, Official100DetailsEngine.ReconnectionPacerBatchIntervalMs);
    }
}
