using System;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2.Tests;

/// <summary>
/// 盛大官方 1.5 终极深水区 100 项底层机制验证矩阵 (细节 201 ~ 300)
/// 严格 1:1 对齐官方 MIR_final_v3_p.exe IDA MCP 物理真值
/// </summary>
public class TestOfficialUltimate100DetailsMatrix
{
    [Fact]
    public void Test_201_TCP缓冲区溢出64KB熔断()
    {
        Assert.True(OfficialUltimate100DetailsEngine.CheckPacketOverflow(65536));
        Assert.False(OfficialUltimate100DetailsEngine.CheckPacketOverflow(65535));
    }

    [Fact]
    public void Test_202_心跳保活8000ms探测()
    {
        Assert.True(OfficialUltimate100DetailsEngine.CheckHeartbeatTimeout(lastHeartbeatMs: 1000, currentTickMs: 9500));
        Assert.False(OfficialUltimate100DetailsEngine.CheckHeartbeatTimeout(lastHeartbeatMs: 1000, currentTickMs: 8000));
    }

    [Fact]
    public void Test_205_走跑步频()
    {
        var (runInterval, runStep) = OfficialUltimate100DetailsEngine.GetMovementTiming(isRun: true);
        Assert.Equal(380, runInterval);
        Assert.Equal(2, runStep);

        var (walkInterval, walkStep) = OfficialUltimate100DetailsEngine.GetMovementTiming(isRun: false);
        Assert.Equal(550, walkInterval);
        Assert.Equal(1, walkStep);
    }

    [Fact]
    public void Test_206_跑步受重击定身300ms()
    {
        var (interrupted, stunMs) = OfficialUltimate100DetailsEngine.CheckRunInterruptionOnDamage(isRunning: true, isHeavyHit: true);
        Assert.True(interrupted);
        Assert.Equal(300, stunMs);

        var (notInterrupted, _) = OfficialUltimate100DetailsEngine.CheckRunInterruptionOnDamage(isRunning: false, isHeavyHit: true);
        Assert.False(notInterrupted);
    }

    [Fact]
    public void Test_208_209_地表金币外观与5000万上限()
    {
        Assert.Equal("单枚铜币", OfficialUltimate100DetailsEngine.GetGroundGoldAppearance(50).lookName);
        Assert.Equal("小堆金币", OfficialUltimate100DetailsEngine.GetGroundGoldAppearance(500).lookName);
        Assert.Equal("大金币堆", OfficialUltimate100DetailsEngine.GetGroundGoldAppearance(5000).lookName);

        var (succ, newGold, overflow) = OfficialUltimate100DetailsEngine.AddGoldSafely(49_990_000, 20_000);
        Assert.True(succ);
        Assert.Equal(50_000_000, newGold);
        Assert.Equal(10_000, overflow);
    }

    [Fact]
    public void Test_211_212_特修三倍花费与普修折损()
    {
        // 满耐久 20，当前耐久 10，基础价 6000
        int cost = OfficialUltimate100DetailsEngine.CalculateSpecialRepairCost(curDura: 10, maxDura: 20, basePrice: 6000);
        Assert.Equal(3000, cost);

        var (dura, maxDura) = OfficialUltimate100DetailsEngine.PerformNormalRepairWear(curDura: 5, maxDura: 32);
        Assert.Equal(30, maxDura); // 32 - 32/16 = 30
        Assert.Equal(30, dura);
    }

    [Fact]
    public void Test_214_祝福油概率阶梯与诅咒()
    {
        // 0 -> 1 (roll 10 < 50 成功)
        var (s1, _, luck1, _) = OfficialUltimate100DetailsEngine.DrinkBlessOil(0, 0, 10);
        Assert.True(s1);
        Assert.Equal(1, luck1);

        // 达到 +7 封顶不加
        var (s7, _, luck7, _) = OfficialUltimate100DetailsEngine.DrinkBlessOil(7, 0, 0);
        Assert.False(s7);
        Assert.Equal(7, luck7);

        // roll 98 触发诅咒
        var (_, cursed, _, curseVal) = OfficialUltimate100DetailsEngine.DrinkBlessOil(3, 0, 98);
        Assert.True(cursed);
        Assert.Equal(1, curseVal);
    }

    [Fact]
    public void Test_216_218_白黄红名()
    {
        Assert.Equal("红名", OfficialUltimate100DetailsEngine.EvaluateNameColor(200, false).colorLabel);
        Assert.Equal("黄名", OfficialUltimate100DetailsEngine.EvaluateNameColor(50, true).colorLabel);
        Assert.Equal("白名", OfficialUltimate100DetailsEngine.EvaluateNameColor(0, false).colorLabel);
    }

    [Fact]
    public void Test_226_祖玛教主残血狂暴召唤()
    {
        var (canSummon, count) = OfficialUltimate100DetailsEngine.CheckZumaLordRageSummon(800, false);
        Assert.True(canSummon);
        Assert.Equal(3, count);

        var (cantSummon, _) = OfficialUltimate100DetailsEngine.CheckZumaLordRageSummon(800, true);
        Assert.False(cantSummon);
    }

    [Fact]
    public void Test_233_火墙持续时间()
    {
        Assert.Equal(15, OfficialUltimate100DetailsEngine.CalculateFireWallDurationSeconds(0));
        Assert.Equal(20, OfficialUltimate100DetailsEngine.CalculateFireWallDurationSeconds(25)); // 15 + 25/5 = 20
    }

    [Fact]
    public void Test_235_雷电术不死系30破甲()
    {
        int undeadDmg = OfficialUltimate100DetailsEngine.CalculateThunderDamage(baseDmg: 50, isUndead: true, targetMac: 20);
        // targetMac 变成 14 -> 50 - 14 = 36
        Assert.Equal(36, undeadDmg);

        int normalDmg = OfficialUltimate100DetailsEngine.CalculateThunderDamage(baseDmg: 50, isUndead: false, targetMac: 20);
        Assert.Equal(30, normalDmg);
    }

    [Fact]
    public void Test_237_魔法盾吸收比率()
    {
        Assert.Equal(0.50f, OfficialUltimate100DetailsEngine.GetShieldAbsorbRate(0));
        Assert.Equal(0.60f, OfficialUltimate100DetailsEngine.GetShieldAbsorbRate(1));
        Assert.Equal(0.70f, OfficialUltimate100DetailsEngine.GetShieldAbsorbRate(2));
        Assert.Equal(0.80f, OfficialUltimate100DetailsEngine.GetShieldAbsorbRate(3));
    }

    [Fact]
    public void Test_238_圣言术秒杀公式()
    {
        // 等级高 5 级，技能 3 级 -> rate = 5*2 + 3*5 = 25%
        Assert.True(OfficialUltimate100DetailsEngine.CheckHolySpellKill(playerLevel: 40, monsterLevel: 35, skillLevel: 3, isUndead: true, roll100: 20));
        Assert.False(OfficialUltimate100DetailsEngine.CheckHolySpellKill(playerLevel: 40, monsterLevel: 35, skillLevel: 3, isUndead: true, roll100: 30));
        // 生灵非不死系永不秒杀
        Assert.False(OfficialUltimate100DetailsEngine.CheckHolySpellKill(playerLevel: 40, monsterLevel: 35, skillLevel: 3, isUndead: false, roll100: 5));
    }

    [Fact]
    public void Test_251_攻杀剑术刀刃泛红()
    {
        var (power, bonus) = OfficialUltimate100DetailsEngine.CheckPowerHit(maxDc: 40, roll4: 0);
        Assert.True(power);
        Assert.Equal(12, bonus); // 40 * 0.3 = 12

        var (normal, _) = OfficialUltimate100DetailsEngine.CheckPowerHit(maxDc: 40, roll4: 2);
        Assert.False(normal);
    }

    [Fact]
    public void Test_252_刺杀剑术隔位破盾破防()
    {
        int pierceDmg = OfficialUltimate100DetailsEngine.CalculateAssassinateDamage(rawDc: 45, targetAc: 30, isDistance2: true);
        Assert.Equal(45, pierceDmg); // 100% 破防

        int normalDmg = OfficialUltimate100DetailsEngine.CalculateAssassinateDamage(rawDc: 45, targetAc: 30, isDistance2: false);
        Assert.Equal(15, normalDmg); // 45 - 30 = 15
    }

    [Fact]
    public void Test_253_半月弯刀副目标衰减40()
    {
        Assert.Equal(50, OfficialUltimate100DetailsEngine.CalculateHalfMoonDamage(50, isMainTarget: true));
        Assert.Equal(20, OfficialUltimate100DetailsEngine.CalculateHalfMoonDamage(50, isMainTarget: false)); // 50 * 0.4 = 20
    }

    [Fact]
    public void Test_258_狂风攻速时间缩短()
    {
        Assert.Equal(680, OfficialUltimate100DetailsEngine.CalculateAttackIntervalMs(680, 0));
        Assert.Equal(660, OfficialUltimate100DetailsEngine.CalculateAttackIntervalMs(680, 2)); // 减 20ms
    }

    [Fact]
    public void Test_280_281_持久归零攻击与护甲衰减()
    {
        var (dc, ac) = OfficialUltimate100DetailsEngine.EvaluateBrokenEquipStats(rawDc: 35, rawAc: 20, weaponDura: 0, dressDura: 0);
        Assert.Equal(1, dc);
        Assert.Equal(0, ac);
    }

    [Fact]
    public void Test_291_292_组队11人上限与经验加成均摊()
    {
        var (canJoin, exp) = OfficialUltimate100DetailsEngine.CalculatePartyExpShare(currentPartySize: 3, monsterBaseExp: 1000);
        Assert.True(canJoin);
        // totalExp = 1000 * 1.2 = 1200, 4人均摊 -> 300
        Assert.Equal(300, exp);

        var (cantJoin, _) = OfficialUltimate100DetailsEngine.CalculatePartyExpShare(currentPartySize: 11, monsterBaseExp: 1000);
        Assert.False(cantJoin);
    }

    [Fact]
    public void Test_293_行会封号14字符限制()
    {
        Assert.Equal("沙巴克荣誉战神", OfficialUltimate100DetailsEngine.NormalizeGuildRankName("沙巴克荣誉战神"));
        Assert.Equal("12345678901234", OfficialUltimate100DetailsEngine.NormalizeGuildRankName("12345678901234567890"));
    }
}
