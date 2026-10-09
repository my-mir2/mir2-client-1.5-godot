using System;
using System.Collections.Generic;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2.Shared.Protocols.Modern;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 服务端全权威物理战斗与职业特技深度测试矩阵 (1:1 官方 1.5 权威真值)
/// 严格断言服务端物理世界对战士、法师、道士战斗规则与负重约束的权威裁决，杜绝客户端私算与外挂伪造
/// </summary>
public sealed class TestServerAuthoritativeCombatMatrix
{
    private static (PacketDispatcher dispatcher, GameMap map, ClientSession warrior, ClientSession defender) CreateCombatContext()
    {
        var map = new GameMap(0, "比奇省", 100, 100);
        var mapMgr = new MapManager();
        mapMgr.RegisterMap(map);
        var dispatcher = new PacketDispatcher(mapMgr);

        var warrior = new ClientSession(1, null!)
        {
            CharacterName = "权威战士",
            CurrentMapId = 0,
            X = 50,
            Y = 50,
            Direction = 0,
            UseModernProtocol = true,
            State = new PlayerState
            {
                Uid = 1,
                Name = "权威战士",
                Job = 0, // 战士
                Level = 40,
                Hp = 600,
                MaxHp = 600,
                Mp = 200,
                MaxMp = 200,
                Hit = 25,
                Speed = 15
            }
        };
        map.Aoi.Enter(warrior, 50, 50);

        var defender = new ClientSession(2, null!)
        {
            CharacterName = "受测法师",
            CurrentMapId = 0,
            X = 50,
            Y = 49, // 贴身正上方 1 格
            Direction = 4,
            UseModernProtocol = true,
            State = new PlayerState
            {
                Uid = 2,
                Name = "受测法师",
                Job = 1, // 法师
                Level = 35,
                Hp = 200,
                MaxHp = 200,
                Mp = 500,
                MaxMp = 500,
                Hit = 15,
                Speed = 10
            }
        };
        map.Aoi.Enter(defender, 50, 49);

        return (dispatcher, map, warrior, defender);
    }

    [Fact]
    public void Test_Warrior_FireHit_Cooldown_And_Authoritative_Burst()
    {
        var (dispatcher, map, warrior, defender) = CreateCombatContext();
        long now = 100000;

        // 1. 施展烈火剑法凝聚火焰
        var (castOk, reason) = warrior.State!.FireHitTracker.Cast(now);
        Assert.True(castOk, "首次凝聚烈火应成功");

        // 2. CD 内重复施法应被拦截 (CD 10 秒)
        var (castFail, failReason) = warrior.State.FireHitTracker.Cast(now + 5000);
        Assert.False(castFail, "CD 内施法必须被服务端拦截");
        Assert.Contains("冷却中", failReason);

        // 3. 挥刀命中，触发 2.6 倍烈火暴击
        int dist = 1;
        var (damage, hit, isArmorPiercing, isFireHit, died) = dispatcher.ResolveAuthoritativePhysicalCombat(warrior, defender, dist, now + 1000);
        Assert.True(hit, "必须命中");
        Assert.True(isFireHit, "必须触发权威烈火剑法暴击");
        Assert.True(damage >= (int)(15 * 2.6), "烈火伤害必须为基础攻击的 2.6 倍");

        // 4. 第二刀烈火蓄力已消耗，应恢复为普通物理攻击
        var (dmg2, _, _, isFire2, _) = dispatcher.ResolveAuthoritativePhysicalCombat(warrior, defender, dist, now + 2000);
        Assert.False(isFire2, "第二刀已脱离蓄力，不再是烈火");
    }

    [Fact]
    public void Test_Warrior_Stabbing_Authoritative_Pierce_Shield_And_Defense()
    {
        var (dispatcher, map, warrior, defender) = CreateCombatContext();
        long now = 100000;

        // 法师开启 3 级魔法盾 (吸收 60% 伤害)
        defender.State!.ActivateMagicShield(3, 30);
        int initialHp = defender.State.Hp;

        // 情况 A: 贴身 1 格攻击，受魔法盾吸收保护
        int distClose = 1;
        var (dmgClose, hitClose, isPierceClose, _, _) = dispatcher.ResolveAuthoritativePhysicalCombat(warrior, defender, distClose, now);
        Assert.True(hitClose);
        Assert.False(isPierceClose, "贴身 1 格非隔位刺杀");
        Assert.True(defender.State.HasMagicShield, "贴身攻击未击碎魔法盾");

        // 重置法师血量
        defender.State.Hp = initialHp;

        // 情况 B: 隔位 2 格刺杀 (开启刺杀剑气)
        warrior.State!.StabbingTracker.AutoStab = true;
        defender.Y = 48; // 隔位第 2 格 (50, 48) 与 (50, 50) 距离为 2
        map.Aoi.UpdatePosition(defender, 50, 49, 50, 48, out _, out _);

        int distFar = 2;
        var (dmgFar, hitFar, isPierceFar, _, _) = dispatcher.ResolveAuthoritativePhysicalCombat(warrior, defender, distFar, now);
        Assert.True(hitFar);
        Assert.True(isPierceFar, "隔位第 2 格开启刺杀剑气必须判定为破防刺杀");
        // 破防破盾直接造成全额伤害
        Assert.True(dmgFar > 0, "隔位刺杀造成权威穿透伤害");
    }

    [Fact]
    public void Test_Mage_RingOfFire_LevelSuppression_And_Displacement()
    {
        var (dispatcher, map, warrior, defender) = CreateCombatContext();

        // 设立法师为施法者 (Level 35)
        // 目标 A: 低等级战士 (Level 30) - 位于法师正上方 (50, 48)
        var lowLevelWarrior = new ClientSession(3, null!)
        {
            X = 50,
            Y = 48,
            State = new PlayerState { Uid = 3, Level = 30, Hp = 200 }
        };
        map.Aoi.Enter(lowLevelWarrior, 50, 48);

        // 目标 B: 高等级战士 (Level 42) - 位于法师正下方 (50, 50)
        warrior.State!.Level = 42;

        var surrounding = new List<(int uid, int level, int x, int y, bool isBoss)>
        {
            (lowLevelWarrior.SessionId, lowLevelWarrior.State.Level, lowLevelWarrior.X, lowLevelWarrior.Y, false),
            (warrior.SessionId, warrior.State.Level, warrior.X, warrior.Y, false)
        };

        var pushResults = MageCombatEngine.RingOfFire.Execute(
            casterLevel: defender.State!.Level,
            casterX: defender.X,
            casterY: defender.Y,
            surroundingEntities: surrounding,
            isTileBlocked: (x, y) => !map.IsWalkable(x, y)
        );

        var pushA = pushResults.Find(r => r.Uid == 3);
        var pushB = pushResults.Find(r => r.Uid == 1);

        Assert.True(pushA.Pushed, "低等级目标必须被抗拒火环成功推开");
        Assert.Equal(47, pushA.FinalY); // 原为 48，远离 (50, 49) 被推至 47

        Assert.False(pushB.Pushed, "高等级目标必须抵抗抗拒火环推开");
        Assert.Contains("高于", pushB.FailReason);
    }

    [Fact]
    public void Test_Taoist_Poison_Hit_TickDamage_And_Vulnerability()
    {
        var (dispatcher, map, warrior, defender) = CreateCombatContext();
        long now = 100000;

        // 1. 目标 0 抗毒，施毒术必中判定
        bool hit = TaoistCombatEngine.CheckPoisonHit(targetAntiPoison: 0, Random.Shared);
        Assert.True(hit, "0 抗毒必须 100% 命中");

        // 2. 挂载绿毒与红毒
        defender.State!.ApplyPoison(TaoistCombatEngine.PoisonType.Green, durationSec: 30, sc: 25, nowMs: now);
        defender.State.ApplyPoison(TaoistCombatEngine.PoisonType.Red, durationSec: 40, sc: 25, nowMs: now);

        Assert.True(defender.State.HasGreenPoison);
        Assert.True(defender.State.HasRedPoison);
        Assert.Equal(0, defender.State.IncHealth); // 绿毒立即清空生命回复池

        // 3. 绿毒周期 Tick 掉血 (经过 2500ms)
        int hpBefore = defender.State.Hp;
        var (tookDmg, dmg, greenExpired, redExpired) = defender.State.ProcessPoisonTick(now + 2500);
        Assert.True(tookDmg, "2.5 秒后必须触发绿毒扣血");
        Assert.Equal(7, dmg); // 3 级施毒术 + 25 道术扣减 7 点 HP
        Assert.Equal(hpBefore - dmg, defender.State.Hp);

        // 4. 红毒 1.2 倍物理伤害易伤
        int rawPhysical = 100;
        int vulnDamage = TaoistCombatEngine.ApplyRedPoisonVulnerability(rawPhysical, defender.State.HasRedPoison);
        Assert.Equal(120, vulnDamage); // 1.2 倍
    }

    [Fact]
    public void Test_WeightConstraint_Authoritative_Enforcement()
    {
        var (dispatcher, map, warrior, _) = CreateCombatContext();

        // 战士 40 级，腕力上限计算
        warrior.State!.RecalculateAuthoritativeWeights();
        int maxHand = warrior.State.MaxHandWeight;
        Assert.True(maxHand > 0, "战士腕力上限必须大于0");

        // 尝试佩戴超重武器 (例如需要腕力 80 的假想神兵，而当前战士只有约 40~50 腕力)
        warrior.State.BagItems.Add(new EquipItem
        {
            Uid = 999,
            Name = "超重重锤",
            Weight = maxHand + 20
        });

        bool equipOk = dispatcher.HandleWearItem(warrior, bagSlot: 0, OfficialEquipSlot.Weapon);
        Assert.False(equipOk, "腕力超限必须被服务端权威拦截");
        Assert.Null(warrior.State.Equipment[(int)OfficialEquipSlot.Weapon]);

        // 尝试佩戴合格武器 (重量 <= maxHand)
        warrior.State.BagItems.Add(new EquipItem
        {
            Uid = 888,
            Name = "裁决之杖",
            Weight = 30 // 合格
        });

        bool equipCaiJue = dispatcher.HandleWearItem(warrior, bagSlot: 1, OfficialEquipSlot.Weapon);
        Assert.True(equipCaiJue, "合规武器必须允许穿戴");
        Assert.NotNull(warrior.State.Equipment[(int)OfficialEquipSlot.Weapon]);
        Assert.Equal(30, warrior.State.HandWeight);
    }

    [Fact]
    public void Test_Weapon_Luck9_Max_DC_Burst()
    {
        var (dispatcher, map, warrior, _) = CreateCombatContext();

        // 未加幸运时，攻击力在 minDc ~ maxDc 波动
        var rng = new Random(42);

        // 装备运 7 武器 + 运 2 项链 = 运 9 套
        warrior.State!.Equipment[(int)OfficialEquipSlot.Weapon] = new EquipItem
        {
            Uid = 101,
            Name = "裁决之杖",
            Luck = 7
        };
        warrior.State.Equipment[(int)OfficialEquipSlot.Necklace] = new EquipItem
        {
            Uid = 102,
            Name = "记忆项链",
            Luck = 2
        };

        // 连续 20 次攻击，必须刀刀发挥最大 DC (35)
        for (int i = 0; i < 20; i++)
        {
            int dc = warrior.State.CalculateAttackDamageWithLuck(minDc: 10, maxDc: 35, rng);
            Assert.Equal(35, dc); // 运 9 套刀刀上限攻击
        }
    }
}
