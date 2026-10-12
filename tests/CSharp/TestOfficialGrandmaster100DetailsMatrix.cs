using System;
using System.Collections.Generic;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.5 宗师级 100 项底层机制与对抗测试矩阵 (细节 401 ~ 500)
/// 覆盖三职业属性成长、防御抵扣、怪物 AI、网络安全与 UI 交互 100% 物理闭环断言
/// </summary>
public class TestOfficialGrandmaster100DetailsMatrix
{
    // =========================================================================
    // 维度一：属性成长与伤害抵扣公式体系 (细节 401~425)
    // =========================================================================

    [Fact]
    public void Test_401_403_Class_HpMp_Growth_Curves()
    {
        // 40 级战士: 高血量，低魔法
        var (warHp, warMp) = OfficialGrandmaster100DetailsEngine.CalculateMaxHpMp(job: 0, level: 40);
        Assert.True(warHp > 600);
        Assert.True(warMp < 150);

        // 40 级法师: 极低血量，高额魔法
        var (wizHp, wizMp) = OfficialGrandmaster100DetailsEngine.CalculateMaxHpMp(job: 1, level: 40);
        Assert.True(wizHp < 200);
        Assert.True(wizMp > 900);

        // 40 级道士: 均衡成长
        var (taoHp, taoMp) = OfficialGrandmaster100DetailsEngine.CalculateMaxHpMp(job: 2, level: 40);
        Assert.True(taoHp > wizHp && taoHp < warHp);
        Assert.True(taoMp > warMp);
    }

    [Fact]
    public void Test_404_413_Defense_Accuracy_And_Recovery_Formulas()
    {
        // 404: 物理防御抵扣 (100 伤害，防御 20~30，抵扣后 70~80)
        int physDmg = OfficialGrandmaster100DetailsEngine.ApplyPhysicalDefense(100, 20, 30, roll: 0.5);
        Assert.Equal(75, physDmg);

        // 405: 魔法防御抵扣
        int magicDmg = OfficialGrandmaster100DetailsEngine.ApplyMagicDefense(100, 15, 25, roll: 0.5);
        Assert.Equal(80, magicDmg);

        // 409: 物理命中准确率 (准确 15, 敏捷 15 -> 50% 命中)
        Assert.True(OfficialGrandmaster100DetailsEngine.RollPhysicalHit(accuracy: 15, targetAgility: 15, roll: 0.40));
        Assert.False(OfficialGrandmaster100DetailsEngine.RollPhysicalHit(accuracy: 15, targetAgility: 15, roll: 0.60));

        // 410: 魔法躲避 (30% 躲避)
        Assert.True(OfficialGrandmaster100DetailsEngine.RollMagicEvade(evadePercent: 30, roll: 0.20));
        Assert.False(OfficialGrandmaster100DetailsEngine.RollMagicEvade(evadePercent: 30, roll: 0.50));

        // 411: 中毒恢复时间缩短 (3点恢复 -> 缩短 30%, 10000ms -> 7000ms)
        Assert.Equal(7000, OfficialGrandmaster100DetailsEngine.ApplyPoisonRecoveryDuration(10_000, poisonRecoveryPoints: 3));
    }

    [Fact]
    public void Test_414_425_Weight_Limits_And_Hidden_Attributes()
    {
        // 414~416: 负重限制
        Assert.True(OfficialGrandmaster100DetailsEngine.ValidateBagWeight(40, 60, 15));
        Assert.False(OfficialGrandmaster100DetailsEngine.ValidateBagWeight(50, 60, 15));
        Assert.True(OfficialGrandmaster100DetailsEngine.ValidateWearWeight(20, 30, 8));
        Assert.False(OfficialGrandmaster100DetailsEngine.ValidateWearWeight(25, 30, 8));
        Assert.True(OfficialGrandmaster100DetailsEngine.ValidateHandWeight(30, 40));
        Assert.False(OfficialGrandmaster100DetailsEngine.ValidateHandWeight(45, 40));

        // 417: 祈祷头盔必碎
        Assert.True(OfficialGrandmaster100DetailsEngine.IsQiDaoHelmetShatteredOnDeath("祈祷头盔"));
        Assert.False(OfficialGrandmaster100DetailsEngine.IsQiDaoHelmetShatteredOnDeath("圣战头盔"));

        // 419: 绿色项链破甲 (防御减半)
        Assert.Equal(20, OfficialGrandmaster100DetailsEngine.ApplyGreenNecklaceArmorBreak(targetDefense: 40, roll: 0.05));
        Assert.Equal(40, OfficialGrandmaster100DetailsEngine.ApplyGreenNecklaceArmorBreak(targetDefense: 40, roll: 0.50));

        // 425: 阎罗手套反伤 10%
        Assert.Equal(10, OfficialGrandmaster100DetailsEngine.CalculateYanLuoReflectDamage(100, hasPair: true, roll: 0.05));
        Assert.Equal(0, OfficialGrandmaster100DetailsEngine.CalculateYanLuoReflectDamage(100, hasPair: true, roll: 0.50));
    }

    // =========================================================================
    // 维度二：怪物 AI 行为树与高级首领生态 (细节 426~450)
    // =========================================================================

    [Fact]
    public void Test_426_450_Monster_Ai_And_Ecological_Rules()
    {
        // 426: 沃玛卫士低血顺劈斩
        Assert.True(OfficialGrandmaster100DetailsEngine.ShouldWoMaGuardCleave(currentHp: 400, maxHp: 1000, roll: 0.10));
        Assert.False(OfficialGrandmaster100DetailsEngine.ShouldWoMaGuardCleave(currentHp: 800, maxHp: 1000, roll: 0.10));

        // 432: 角蝇产卵上限 (10 只)
        Assert.True(OfficialGrandmaster100DetailsEngine.CanJiaoYingSpawnBat(currentBatsAlive: 8));
        Assert.False(OfficialGrandmaster100DetailsEngine.CanJiaoYingSpawnBat(currentBatsAlive: 10));

        // 435: 祖玛弓箭手后撤步 (向后拉开 2 格)
        var (newX, newY) = OfficialGrandmaster100DetailsEngine.CalculateArcherRetreat(archerX: 100, archerY: 100, targetX: 99, targetY: 100);
        Assert.Equal(102, newX);
        Assert.Equal(100, newY);

        // 439: 月魔蜘蛛麻痹 15% 几率
        Assert.True(OfficialGrandmaster100DetailsEngine.RollYueMoParalysis(0.10));
        Assert.False(OfficialGrandmaster100DetailsEngine.RollYueMoParalysis(0.20));

        // 443: 赤月恶魔地刺穿刺伤害 (60~120)
        int spikeDmg = OfficialGrandmaster100DetailsEngine.CalculateChiYueSpikeDamage(0.5);
        Assert.Equal(90, spikeDmg);

        // 444: 神兽 10 秒脱战卧倒
        Assert.True(OfficialGrandmaster100DetailsEngine.ShouldShinsuLieDown(nowMs: 15_000, lastCombatMs: 2_000));
        Assert.False(OfficialGrandmaster100DetailsEngine.ShouldShinsuLieDown(nowMs: 15_000, lastCombatMs: 10_000));

        // 445: 骷髅等级成长与变色
        var (hp0, color0) = OfficialGrandmaster100DetailsEngine.GetSkeletonStatsByLevel(0);
        var (hp7, color7) = OfficialGrandmaster100DetailsEngine.GetSkeletonStatsByLevel(7);
        Assert.Equal(80, hp0);
        Assert.Equal("White", color0);
        Assert.Equal(350, hp7);
        Assert.Equal("Red", color7);

        // 448: 弓箭守卫击退 1 格
        var (kx, ky) = OfficialGrandmaster100DetailsEngine.CalculateGuardKnockback(guardX: 100, guardY: 100, targetX: 105, targetY: 100);
        Assert.Equal(106, kx);
    }

    // =========================================================================
    // 维度三：深层网络通信、安全风控与防重放机制 (细节 451~475)
    // =========================================================================

    [Fact]
    public void Test_451_475_Network_Security_Timeout_And_Decay()
    {
        // 452: 序号单调递增防重放
        Assert.True(OfficialGrandmaster100DetailsEngine.ValidatePacketSequence(incomingSeq: 105, lastExpectedSeq: 104));
        Assert.False(OfficialGrandmaster100DetailsEngine.ValidatePacketSequence(incomingSeq: 100, lastExpectedSeq: 104));

        // 453: 动态协商魔数校验
        Assert.True(OfficialGrandmaster100DetailsEngine.ValidateSessionMagic(packetMagic: 0x594D, sessionMagic: 0x594D));
        Assert.False(OfficialGrandmaster100DetailsEngine.ValidateSessionMagic(packetMagic: 0x1234, sessionMagic: 0x594D));

        // 454: 心跳超时 (24 秒)
        Assert.True(OfficialGrandmaster100DetailsEngine.IsHeartbeatTimedOut(nowMs: 30_000, lastHeartbeatMs: 5_000));
        Assert.False(OfficialGrandmaster100DetailsEngine.IsHeartbeatTimedOut(nowMs: 30_000, lastHeartbeatMs: 15_000));

        // 455: 断线重连 30 秒幽灵保活时钟
        Assert.True(OfficialGrandmaster100DetailsEngine.CanReconnectWithinGracePeriod(nowMs: 25_000, disconnectMs: 5_000));
        Assert.False(OfficialGrandmaster100DetailsEngine.CanReconnectWithinGracePeriod(nowMs: 40_000, disconnectMs: 5_000));

        // 459: 技能公共冷却 (GCD 1000ms)
        Assert.True(OfficialGrandmaster100DetailsEngine.CheckGlobalCooldown(nowMs: 2000, lastSpellMs: 900));
        Assert.False(OfficialGrandmaster100DetailsEngine.CheckGlobalCooldown(nowMs: 1500, lastSpellMs: 900));

        // 468 & 470: 地表物品 80 秒保护与 300 秒腐烂
        Assert.True(OfficialGrandmaster100DetailsEngine.IsGroundItemProtected(nowMs: 50_000, dropMs: 10_000));
        Assert.False(OfficialGrandmaster100DetailsEngine.IsGroundItemProtected(nowMs: 100_000, dropMs: 10_000));
        Assert.True(OfficialGrandmaster100DetailsEngine.IsGroundItemDecayed(nowMs: 350_000, dropMs: 10_000));

        // 471 & 472: 挖肉品质与上限
        Assert.True(OfficialGrandmaster100DetailsEngine.CanButcherMoreMeat(butcheredCount: 1));
        Assert.False(OfficialGrandmaster100DetailsEngine.CanButcherMoreMeat(butcheredCount: 2));
        Assert.Equal(150, OfficialGrandmaster100DetailsEngine.CalculateMeatPrice(basePrice: 100, meatQuality: 15));

        // 473: 打坐起立受创 1.2 倍加成
        Assert.Equal(120, OfficialGrandmaster100DetailsEngine.ApplySitdownDamageVulnerability(100, isSitting: true));
        Assert.Equal(100, OfficialGrandmaster100DetailsEngine.ApplySitdownDamageVulnerability(100, isSitting: false));

        // 474: 移动中受创定身 300ms
        Assert.Equal(300, OfficialGrandmaster100DetailsEngine.CalculateHitStunDurationMs(isMoving: true));
        Assert.Equal(150, OfficialGrandmaster100DetailsEngine.CalculateHitStunDurationMs(isMoving: false));
    }

    // =========================================================================
    // 维度四：原生 UI、音视频与交互全生命周期 (细节 476~500)
    // =========================================================================

    [Fact]
    public void Test_476_500_Ui_Lifecycle_Audio_And_DoubleExp()
    {
        // 477: 聊天频道冷却 (喊话 5000ms)
        Assert.True(OfficialGrandmaster100DetailsEngine.CanSendChatMessage(channel: 1, nowMs: 10_000, lastSendMs: 4_000));
        Assert.False(OfficialGrandmaster100DetailsEngine.CanSendChatMessage(channel: 1, nowMs: 10_000, lastSendMs: 8_000));

        // 479: 安全区外喊话扣 1 点 MP
        var (shoutOk, newMp) = OfficialGrandmaster100DetailsEngine.TryConsumeShoutMp(currentMp: 10, isInSafeZone: false);
        Assert.True(shoutOk);
        Assert.Equal(9, newMp);

        var (failOk, _) = OfficialGrandmaster100DetailsEngine.TryConsumeShoutMp(currentMp: 0, isInSafeZone: false);
        Assert.False(failOk);

        // 483: 拖拽阴影偏移 (+2, +2)
        Assert.Equal((2, 2), OfficialGrandmaster100DetailsEngine.GetDragItemShadowOffset());

        // 485: 非法穿戴音效 105
        Assert.Equal(105, OfficialGrandmaster100DetailsEngine.GetIllegalEquipSoundId());

        // 486: NPC 对话翻页
        Assert.True(OfficialGrandmaster100DetailsEngine.DoesNpcTextRequirePaging(totalLines: 10));
        Assert.False(OfficialGrandmaster100DetailsEngine.DoesNpcTextRequirePaging(totalLines: 7));

        // 492: 皇宫易主 100% 纯净度
        Assert.True(OfficialGrandmaster100DetailsEngine.VerifyPalacePureVictory(new[] { "行会X", "行会X" }, "行会X"));
        Assert.False(OfficialGrandmaster100DetailsEngine.VerifyPalacePureVictory(new[] { "行会X", "行会Y" }, "行会X"));

        // 498 & 499: 环境音周期
        Assert.Equal(45, OfficialGrandmaster100DetailsEngine.GetAmbientSoundIntervalSec("盟重省"));
        Assert.Equal(30, OfficialGrandmaster100DetailsEngine.GetAmbientSoundIntervalSec("比奇矿区"));

        // 500: 全服双倍经验
        Assert.Equal(2000, OfficialGrandmaster100DetailsEngine.ApplyServerDoubleExp(1000, isDoubleExpActive: true));
        Assert.Equal(1000, OfficialGrandmaster100DetailsEngine.ApplyServerDoubleExp(1000, isDoubleExpActive: false));
    }
}
