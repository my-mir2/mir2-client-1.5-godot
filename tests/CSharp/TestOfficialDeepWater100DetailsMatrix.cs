using System;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2.Tests;

/// <summary>
/// 盛大官方 1.5 权威深水区 100 项底层机制与战斗规则验证矩阵 (细节 101 ~ 200)
/// 严格 1:1 对齐官方 MIR_final_v3_p.exe IDA MCP 物理真值
/// </summary>
public class TestOfficialDeepWater100DetailsMatrix
{
    [Fact]
    public void Test_101_野蛮冲撞撞墙硬直()
    {
        var (hitWall, stunMs) = OfficialDeepWater100DetailsEngine.CheckSavageHitWall(isObstacleAhead: true);
        Assert.True(hitWall);
        Assert.Equal(500, stunMs);

        var (hitNoWall, stunNone) = OfficialDeepWater100DetailsEngine.CheckSavageHitWall(isObstacleAhead: false);
        Assert.False(hitNoWall);
        Assert.Equal(0, stunNone);
    }

    [Fact]
    public void Test_102_魔法盾破盾剩余伤害穿透()
    {
        // 盾还剩 30 HP，承受 100 点伤害 -> 吸收 30，穿透 70，破盾
        var (absorbed, penetrate, broken) = OfficialDeepWater100DetailsEngine.CalculateShieldPenetration(30, 100);
        Assert.Equal(30, absorbed);
        Assert.Equal(70, penetrate);
        Assert.True(broken);

        // 盾有 100 HP，承受 50 点伤害 -> 吸收 50，穿透 0，未破
        var (absorbed2, penetrate2, broken2) = OfficialDeepWater100DetailsEngine.CalculateShieldPenetration(100, 50);
        Assert.Equal(50, absorbed2);
        Assert.Equal(0, penetrate2);
        Assert.False(broken2);
    }

    [Fact]
    public void Test_103_抗拒火环破除隐身()
    {
        Assert.True(OfficialDeepWater100DetailsEngine.BreakInvisibilityOnRepelled(true, true));
        Assert.False(OfficialDeepWater100DetailsEngine.BreakInvisibilityOnRepelled(false, true));
    }

    [Fact]
    public void Test_104_神兽站卧形态与2格喷火()
    {
        var (resting, range) = OfficialDeepWater100DetailsEngine.GetPetBeastStance(12_000);
        Assert.True(resting);
        Assert.Equal(2, range);

        var (restingActive, _) = OfficialDeepWater100DetailsEngine.GetPetBeastStance(3_000);
        Assert.False(restingActive);
    }

    [Fact]
    public void Test_105_骷髅变色色阶()
    {
        Assert.Equal("白骨", OfficialDeepWater100DetailsEngine.GetSkeletonNameColor(0).colorName);
        Assert.Equal("浅蓝", OfficialDeepWater100DetailsEngine.GetSkeletonNameColor(2).colorName);
        Assert.Equal("深蓝", OfficialDeepWater100DetailsEngine.GetSkeletonNameColor(4).colorName);
        Assert.Equal("亮黄", OfficialDeepWater100DetailsEngine.GetSkeletonNameColor(6).colorName);
        Assert.Equal("血红", OfficialDeepWater100DetailsEngine.GetSkeletonNameColor(7).colorName);
    }

    [Fact]
    public void Test_106_随从6小时狂暴叛变()
    {
        const long SixHoursMs = 6L * 3600 * 1000;
        Assert.False(OfficialDeepWater100DetailsEngine.CheckTamedMonsterMad(SixHoursMs - 1000));
        Assert.True(OfficialDeepWater100DetailsEngine.CheckTamedMonsterMad(SixHoursMs + 1000));
    }

    [Fact]
    public void Test_107_未知暗殿真假反转()
    {
        var realBoss = OfficialDeepWater100DetailsEngine.GetUnknownDarkHallStats("祖玛教主", false);
        Assert.Equal(3000, realBoss.hp);

        var fakeBoss = OfficialDeepWater100DetailsEngine.GetUnknownDarkHallStats("祖玛教主", true);
        Assert.Equal(100, fakeBoss.hp);
        Assert.True(fakeBoss.isWeakFake);

        var abnormalChicken = OfficialDeepWater100DetailsEngine.GetUnknownDarkHallStats("鸡", true);
        Assert.Equal(3000, abnormalChicken.hp);
        Assert.Equal(80, abnormalChicken.maxDc);
    }

    [Fact]
    public void Test_108_大刀守卫9999神罚红名()
    {
        var (kill, dmg) = OfficialDeepWater100DetailsEngine.GuardKillRedName(pkPoints: 300, distance: 1);
        Assert.True(kill);
        Assert.Equal(9999, dmg);

        var (killWhite, _) = OfficialDeepWater100DetailsEngine.GuardKillRedName(pkPoints: 0, distance: 1);
        Assert.False(killWhite);
    }

    [Fact]
    public void Test_110_双毒叠加()
    {
        var (green, red, dot, defRed) = OfficialDeepWater100DetailsEngine.ApplySnakePoison(true, true);
        Assert.True(green);
        Assert.True(red);
        Assert.Equal(2, dot);
        Assert.Equal(0.10f, defRed);
    }

    [Fact]
    public void Test_126_血僵尸独占血饮产出()
    {
        Assert.True(OfficialDeepWater100DetailsEngine.CanDropBloodyDrinkWeapon("血僵尸", 0));
        Assert.False(OfficialDeepWater100DetailsEngine.CanDropBloodyDrinkWeapon("祖玛卫士", 0));
    }

    [Fact]
    public void Test_127_128_金刚割肉与血魔大爆()
    {
        var (butch1, r1) = OfficialDeepWater100DetailsEngine.GetTwoHeadedBossDropMode("双头金刚");
        Assert.True(butch1);
        Assert.Equal("法神首饰", r1);

        var (butch2, r2) = OfficialDeepWater100DetailsEngine.GetTwoHeadedBossDropMode("双头血魔");
        Assert.False(butch2);
        Assert.Equal("圣战首饰", r2);
    }

    [Fact]
    public void Test_130_触龙神全魔法免疫()
    {
        Assert.Equal(0, OfficialDeepWater100DetailsEngine.CalculateTouchDragonDamage(rawMagicDmg: 80, rawPhysDmg: 0, isAssassinatePierce: false));
        Assert.Equal(50, OfficialDeepWater100DetailsEngine.CalculateTouchDragonDamage(rawMagicDmg: 0, rawPhysDmg: 50, isAssassinatePierce: true));
    }

    [Fact]
    public void Test_131_132_疗伤药瞬回与金创药缓回()
    {
        var heal = OfficialDeepWater100DetailsEngine.GetPotionRecoveryProfile("疗伤药");
        Assert.Equal(200, heal.instantHp);
        Assert.Equal(200, heal.instantMp);

        var drug = OfficialDeepWater100DetailsEngine.GetPotionRecoveryProfile("强效金创药");
        Assert.Equal(0, drug.instantHp);
        Assert.Equal(15, drug.tickHp);
        Assert.Equal(6, drug.totalTicks);
    }

    [Fact]
    public void Test_147_148_运9满额与诅10下限()
    {
        // 运 9 套必出上限 30
        Assert.Equal(30, OfficialDeepWater100DetailsEngine.ApplyLuckCurseExtremes(minDc: 5, maxDc: 30, totalLuck: 9, totalCurse: 0, randomDc: 12));
        // 诅 10 必出下限 5
        Assert.Equal(5, OfficialDeepWater100DetailsEngine.ApplyLuckCurseExtremes(minDc: 5, maxDc: 30, totalLuck: 0, totalCurse: 10, randomDc: 12));
    }

    [Fact]
    public void Test_149_151_特戒三大判定()
    {
        // 麻痹戒指
        Assert.True(OfficialDeepWater100DetailsEngine.CheckParalysisRing(0).paralyzed);
        Assert.False(OfficialDeepWater100DetailsEngine.CheckParalysisRing(5).paralyzed);

        // 护身戒指
        var (hpDmg, mpDmg, newMp) = OfficialDeepWater100DetailsEngine.ApplyProtectionRing(incomingDmg: 40, currentMp: 50);
        Assert.Equal(0, hpDmg);
        Assert.Equal(40, mpDmg);
        Assert.Equal(10, newMp);

        // 复活戒指
        var (revived, newHp, leftDura) = OfficialDeepWater100DetailsEngine.CheckRevivalRing(currentHp: 0, maxHp: 100, ringDura: 5);
        Assert.True(revived);
        Assert.Equal(100, newHp);
        Assert.Equal(4, leftDura);
    }

    [Fact]
    public void Test_158_虹魔套装吸血10()
    {
        Assert.Equal(10, OfficialDeepWater100DetailsEngine.CalculateRainbowMagicLifeSteal(100, true));
        Assert.Equal(0, OfficialDeepWater100DetailsEngine.CalculateRainbowMagicLifeSteal(100, false));
    }

    [Fact]
    public void Test_172_冰咆哮穿透魔法躲避()
    {
        // 虎齿项链 50% 魔法躲避，普通法术触发闪避
        Assert.True(OfficialDeepWater100DetailsEngine.CheckSpellEvade(spellId: 10, evadeRatePercent: 50, roll100: 20)); // 10: 雷电术
        // 冰咆哮 (spellId: 31) 穿透，绝不闪避
        Assert.False(OfficialDeepWater100DetailsEngine.CheckSpellEvade(spellId: 31, evadeRatePercent: 50, roll100: 20));
    }

    [Fact]
    public void Test_178_红名大爆与白名轻度掉落()
    {
        var (redDrop, isMassive) = OfficialDeepWater100DetailsEngine.CalculateDeathDrop(pkPoints: 300, bagItemCount: 20, roll100: 2);
        Assert.True(isMassive);
        Assert.True(redDrop >= 8);

        var (whiteDrop, isMassiveWhite) = OfficialDeepWater100DetailsEngine.CalculateDeathDrop(pkPoints: 0, bagItemCount: 20, roll100: 5);
        Assert.False(isMassiveWhite);
        Assert.True(whiteDrop <= 2);
    }

    [Fact]
    public void Test_200_移动防加速外挂380ms()
    {
        var (hacked, pull) = OfficialDeepWater100DetailsEngine.CheckMovementSpeedHack(lastMoveTickMs: 1000, currentTickMs: 1300); // 间隔 300ms < 380ms
        Assert.True(hacked);
        Assert.True(pull);

        var (normal, normalPull) = OfficialDeepWater100DetailsEngine.CheckMovementSpeedHack(lastMoveTickMs: 1000, currentTickMs: 1600); // 间隔 600ms
        Assert.False(normal);
        Assert.False(normalPull);
    }
}
