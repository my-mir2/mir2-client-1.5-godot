using System;
using Mir2Client.Game;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 官方标准 xUnit 纯 C# 32 门技能数值与 7 特戒 4 套装规则验证套件
/// 独立于游戏引擎渲染主循环，直接由 dotnet test 高速执行，毫秒级退出
/// </summary>
public class TestSkillsMatrix
{
    [Fact]
    public void TestSkillsCount_Exact32OfficialSkills()
    {
        // 封闭分母: 盛大官方 1.5 严格 32 门连续技能 (ID 1 ~ 32)
        Assert.Equal(32, SkillFormulas.TotalSkillsCount);

        for (short id = 1; id <= 32; id++)
        {
            var meta = SkillFormulas.GetSkill(id);
            Assert.True(meta.HasValue, $"缺少官方技能 ID: {id}");
            Assert.Equal(id, meta.Value.Id);
            Assert.False(string.IsNullOrEmpty(meta.Value.Name));
        }

        // 验证战士技能
        Assert.Equal("烈火剑法", SkillFormulas.GetSkill(26)?.Name);
        Assert.Equal("野蛮冲撞", SkillFormulas.GetSkill(27)?.Name);
        Assert.Equal("半月弯刀", SkillFormulas.GetSkill(25)?.Name);

        // 验证法师技能
        Assert.Equal("魔法盾", SkillFormulas.GetSkill(31)?.Name);
        Assert.Equal("冰咆哮", SkillFormulas.GetSkill(32)?.Name);

        // 验证道士技能
        Assert.Equal("召唤神兽", SkillFormulas.GetSkill(30)?.Name);
        Assert.Equal("群体治愈术", SkillFormulas.GetSkill(29)?.Name);
    }

    [Fact]
    public void TestMagicShieldFormulas_DamageReductionAndDuration()
    {
        // 100 伤害，3 级盾 (shield_level=3)
        // actual_damage = ROUND( (100 / 100) * (3 + 2) * 8 ) = 40
        // absorbed_damage = 100 - 40 = 60
        var res = SkillFormulas.CalculateMagicShieldDamage(100, 3, 30);
        Assert.Equal(40, res.actualDamage);
        Assert.Equal(60, res.absorbedDamage);
        Assert.Equal(27, res.newRemainingSec); // 30 - 3 = 27 秒
        Assert.False(res.isBroken);

        // 盾即将破碎 (剩余 3 秒时受击变为 1 秒)
        var lowRes = SkillFormulas.CalculateMagicShieldDamage(50, 3, 2);
        Assert.Equal(1, lowRes.newRemainingSec);
    }

    [Fact]
    public void TestSpecialRings_ProtectRing()
    {
        // 1. MP 充足情况: 100 伤害 -> 耗蓝 150 (1:1.5), HP 无损
        var res1 = SpecialRings.CalculateProtectRingDamage(true, 100, 300, 200);
        Assert.True(res1.hasProtect);
        Assert.Equal(150, res1.mpCost);
        Assert.Equal(150, res1.remainingMp);
        Assert.Equal(0, res1.remainingDamage);
        Assert.Equal(200, res1.newHp);
        Assert.Equal(0, res1.hpLoss);

        // 2. MP 不足情况: 100 伤害 (需要 150 蓝), 仅剩 60 蓝
        // 蓝缺口 = 150 - 60 = 90 -> 转扣 HP = ROUND(90 / 1.5) = 60
        // newHp = 200 - 60 = 140
        var res2 = SpecialRings.CalculateProtectRingDamage(true, 100, 60, 200);
        Assert.True(res2.hasProtect);
        Assert.Equal(60, res2.mpCost);
        Assert.Equal(0, res2.remainingMp);
        Assert.Equal(60, res2.remainingDamage);
        Assert.Equal(140, res2.newHp);
        Assert.Equal(60, res2.hpLoss);
    }

    [Fact]
    public void TestSpecialRings_ReviveRing()
    {
        // 1. 冷却中判定 (已过 10 秒，未达 30 秒)
        long now = 100000;
        long last = 90000;
        var cdRes = SpecialRings.TriggerReviveRing(true, 0, 500, 5000, last, now);
        Assert.False(cdRes.canRevive);
        Assert.Equal("COOLDOWN", cdRes.reason);

        // 2. 冷却完成触发 (已过 35 秒)
        long now2 = 135000;
        var okRes = SpecialRings.TriggerReviveRing(true, 0, 500, 5000, last, now2);
        Assert.True(okRes.canRevive);
        Assert.Equal(500, okRes.newHp);
        Assert.Equal(4000, okRes.newDura); // 扣 1000 点底层持久
        Assert.False(okRes.isBroken);

        // 3. 持久耗尽破碎
        var breakRes = SpecialRings.TriggerReviveRing(true, 0, 500, 800, last, now2);
        Assert.True(breakRes.canRevive);
        Assert.Equal(0, breakRes.newDura);
        Assert.True(breakRes.isBroken);
    }

    [Fact]
    public void TestSpecialSets_MagicBloodSet()
    {
        int[] pieces = [133, 134, 135]; // 戒指、手镯、项链全套
        // 单件 25*3 = 75，全套额外 +50，总计转化 125 点 MP 为 HP
        var res = SpecialRings.CalculateMagicBloodBonus(pieces, 300, 200);
        Assert.True(res.hasFullSet);
        Assert.Equal(125, res.totalTransfer);
        Assert.Equal(425, res.finalHp); // 300 + 125
        Assert.Equal(75, res.finalMp);  // 200 - 125

        // 保底 1 点 MP 保护
        var lowMpRes = SpecialRings.CalculateMagicBloodBonus(pieces, 300, 80);
        Assert.Equal(79, lowMpRes.totalTransfer); // 最多转 79 点，保留 1 点 MP
        Assert.Equal(379, lowMpRes.finalHp);
        Assert.Equal(1, lowMpRes.finalMp);
    }

    [Fact]
    public void TestSpecialSets_RainbowMagicSet()
    {
        int[] pieces = [136, 137, 138]; // 戒指、手镯、项链全套
        // 单件 10% * 3 = 30% 吸血，全套准确 +2
        // 造成 200 点物理伤害 -> 吸血 = ROUND(200 / 100 * 30) = 60
        var res = SpecialRings.CalculateRainbowMagicBonus(pieces, 15, 200);
        Assert.True(res.hasFullSet);
        Assert.Equal(30, res.leechWeight);
        Assert.Equal(2, res.bonusHit);
        Assert.Equal(60, res.leechHp);
    }
}
