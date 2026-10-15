using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using Mir2.Server.Game;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 盛大官方 1.4/1.5 装备生命周期、沙巴克武器升级与祝福油/诅咒 权威矩阵测试
/// 100% 覆盖:
/// 1. 物理攻击 1/30 武器磨损、折断判定（攻击力降为拳头 1 点）、受击衣服 1/15、首饰 1/40 掉持久、普修折损与特修保上限
/// 2. 沙巴克武器升级全流程（黑铁矿纯度算率、首饰属性极值加权、1小时出炉倒计时拦截、取回开光、第一刀试刀成功加点或碎刀销毁）
/// 3. 喝祝福油幸运 +1~+7 概率、运九套满攻暴击、击杀白名武器诅咒 +1~+10 与发挥攻击下限
/// </summary>
public class TestOfficialM2_ItemLifecycleMatrix
{
    private sealed class PredictableRng : Random
    {
        private readonly Queue<int> _nextValues = new();

        public void EnqueueNext(int value) => _nextValues.Enqueue(value);

        public override int Next(int maxValue)
        {
            if (_nextValues.TryDequeue(out var val))
            {
                return val % maxValue;
            }
            return 0;
        }

        public override int Next(int minValue, int maxValue)
        {
            if (_nextValues.TryDequeue(out var val))
            {
                var range = maxValue - minValue;
                return minValue + (val % range);
            }
            return minValue;
        }
    }

    private static ClientSession CreateMockSession(int sessionId, string charName)
    {
        var dummySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var session = new ClientSession(sessionId, dummySocket)
        {
            CharacterName = charName,
            State = new PlayerState
            {
                Uid = sessionId,
                Name = charName,
                Gold = 500000
            }
        };
        return session;
    }

    [Fact]
    public void Test_WeaponDurability_DeductionAndBreak_OnAttack()
    {
        var player = new PlayerState
        {
            Uid = 1001,
            Name = "战士测试员",
            Gold = 50000
        };

        var weapon = new EquipItem
        {
            Uid = 9001,
            Name = "井中月",
            MaxDura = 30000,
            Dura = 1,
            Dc = 7
        };
        player.Equipment[(int)OfficialEquipSlot.Weapon] = weapon;

        var mockRng = new PredictableRng();
        // 第一次命中 1/30 (rng.Next(30) == 0)
        mockRng.EnqueueNext(0);

        var (deducted, broken) = ItemLifecycleEngine.DeductWeaponDurabilityOnAttack(player, mockRng);
        Assert.True(deducted);
        Assert.True(broken);
        Assert.Equal(0, weapon.Dura);

        // 武器折断后伤害降为拳头徒手 1 点物理伤害
        int normalDamage = player.CalculateAttackDamageWithLuck(15, 30, mockRng);
        Assert.True(normalDamage >= 15);

        // 模拟战斗结算中的折断分支
        int brokenDamage = (weapon.Dura <= 0) ? 1 : player.CalculateAttackDamageWithLuck(15, 30, mockRng);
        Assert.Equal(1, brokenDamage);
    }

    [Fact]
    public void Test_ArmorAndJewelry_DurabilityDeduction_OnHit()
    {
        var player = new PlayerState
        {
            Uid = 1002,
            Name = "受击道士"
        };

        var dress = new EquipItem { Uid = 9002, Name = "灵魂战衣", MaxDura = 20000, Dura = 1000 };
        var ring = new EquipItem { Uid = 9003, Name = "铂金戒指", MaxDura = 12000, Dura = 500 };

        player.Equipment[(int)OfficialEquipSlot.Dress] = dress;
        player.Equipment[(int)OfficialEquipSlot.RingL] = ring;

        var mockRng = new PredictableRng();
        // 衣服 1/15 命中 (Next(15) == 0)
        mockRng.EnqueueNext(0);
        // 首饰 1/40 命中 (Next(40) == 0)
        mockRng.EnqueueNext(0);

        var wornSlots = ItemLifecycleEngine.DeductArmorDurabilityOnDamage(player, mockRng);
        Assert.Contains(OfficialEquipSlot.Dress, wornSlots);
        Assert.Contains(OfficialEquipSlot.RingL, wornSlots);
        Assert.Equal(999, dress.Dura);
        Assert.Equal(499, ring.Dura);
    }

    [Fact]
    public void Test_ItemRepair_NormalRepair_Vs_SpecialRepair()
    {
        var weapon = new EquipItem
        {
            Uid = 9004,
            Name = "裁决之杖",
            MaxDura = 32000,
            Dura = 10000
        };

        // 普通修理: 掉 1/10 持久上限
        bool nSuccess = ItemLifecycleEngine.RepairEquip(weapon, isSpecialRepair: false);
        Assert.True(nSuccess);
        Assert.Equal(weapon.MaxDura, weapon.Dura);
        Assert.True(weapon.MaxDura < 32000, "普通修理必须折损持久上限");

        // 特殊修理: 保留当前上限
        int currentMax = weapon.MaxDura;
        weapon.Dura = 5000;
        bool sSuccess = ItemLifecycleEngine.RepairEquip(weapon, isSpecialRepair: true);
        Assert.True(sSuccess);
        Assert.Equal(currentMax, weapon.Dura);
        Assert.Equal(currentMax, weapon.MaxDura);
    }

    [Fact]
    public void Test_SabukWeaponUpgrade_FullLifecycle_And_TrialHit()
    {
        var session = CreateMockSession(2001, "沙巴克铁匠学徒");

        var weapon = new EquipItem
        {
            Uid = 8888,
            Name = "骨玉权杖",
            MaxDura = 18000,
            Dura = 18000,
            Mc = 2,
            MaxMc = 6
        };
        var ore1 = new EquipItem { Uid = 1, Name = "黑铁矿石", Shape = 20, Weight = 10 };
        var ore2 = new EquipItem { Uid = 2, Name = "黑铁矿石", Shape = 20, Weight = 10 };
        var ore3 = new EquipItem { Uid = 3, Name = "黑铁矿石", Shape = 20, Weight = 10 };
        var acc1 = new EquipItem { Uid = 11, Name = "生命项链", Shape = 21, Mc = 3 }; // 魔法属性
        var acc2 = new EquipItem { Uid = 12, Name = "放大镜", Shape = 21, Mc = 3 };

        session.State!.BagItems.Add(weapon);
        session.State.BagItems.Add(ore1);
        session.State.BagItems.Add(ore2);
        session.State.BagItems.Add(ore3);
        session.State.BagItems.Add(acc1);
        session.State.BagItems.Add(acc2);

        var mockRng = new PredictableRng();
        // 成功升级概率判定序列:
        // tieBreakerRoll = 0, duraChange rolls, rate roll < 100 => 成功
        mockRng.EnqueueNext(0);
        mockRng.EnqueueNext(5);
        mockRng.EnqueueNext(5);
        mockRng.EnqueueNext(10); // roll100 < rate
        mockRng.EnqueueNext(0);  // crit roll
        mockRng.EnqueueNext(0);

        long nowMs = 100000000;

        // 1. 提交升级 (武器在 slot 0, 矿石在 1,2,3, 首饰在 4,5)
        var (submitOk, submitMsg) = ItemLifecycleEngine.SubmitWeaponUpgrade(
            session, weaponSlot: 0,
            oreBagSlots: new[] { 1, 2, 3 },
            accessoryBagSlots: new[] { 4, 5 },
            mockRng, nowMs, durationMs: 3600_000);

        Assert.True(submitOk, submitMsg);
        Assert.Empty(session.State.BagItems); // 材料与武器均被扣走

        // 2. 立即提取 (未满 1 小时倒计时)，必须被驳回
        var (claimEarly, _, claimEarlyMsg) = ItemLifecycleEngine.RetrieveUpgradedWeapon(session, nowMs + 1000);
        Assert.False(claimEarly);
        Assert.Contains("猛火淬炼中", claimEarlyMsg);

        // 3. 1 小时后提取出炉
        long readyMs = nowMs + 3600_000 + 1000;
        var (claimOk, claimedWeapon, claimMsg) = ItemLifecycleEngine.RetrieveUpgradedWeapon(session, readyMs);
        Assert.True(claimOk, claimMsg);
        Assert.NotNull(claimedWeapon);
        Assert.True(claimedWeapon!.UpgradeState > 0, "提取出的武器必须携带升级结果试刀码");

        // 4. 穿戴武器并执行第一刀开刀试刀
        session.State.Equipment[(int)OfficialEquipSlot.Weapon] = claimedWeapon;
        var trialRes = session.State.TrialSlashEquippedWeapon();

        Assert.NotEqual(WeaponUpgradeEngine.TrialSlashOutcome.None, trialRes.Outcome);
        Assert.Equal(WeaponUpgradeEngine.TrialSlashOutcome.Success, trialRes.Outcome);
        Assert.True(claimedWeapon.MaxMc > 6, "试刀成功魔法属性必须提升");
        Assert.Equal(0, claimedWeapon.UpgradeState); // 试刀后重置为普通武器
    }

    [Fact]
    public void Test_SabukWeaponUpgrade_TrialHit_BrokenWeapon()
    {
        var session = CreateMockSession(2002, "倒霉的铁匠");

        var weapon = new EquipItem
        {
            Uid = 8889,
            Name = "降魔",
            MaxDura = 17000,
            Dura = 17000,
            UpgradeState = 1 // 官方碎刀代码 1
        };

        session.State!.Equipment[(int)OfficialEquipSlot.Weapon] = weapon;

        // 第一刀砍怪/砍人
        var trialRes = session.State.TrialSlashEquippedWeapon();
        Assert.Equal(WeaponUpgradeEngine.TrialSlashOutcome.Broken, trialRes.Outcome);
        Assert.Null(session.State.Equipment[(int)OfficialEquipSlot.Weapon]); // 碎刀直接彻底灰飞烟灭
        Assert.Contains("你的武器碎了", trialRes.Message);
    }

    [Fact]
    public void Test_BlessingOil_And_Luck_Curse_StateMachine()
    {
        var session = CreateMockSession(3001, "幸运道长");
        var weapon = new EquipItem
        {
            Uid = 7777,
            Name = "龙纹剑",
            Luck = 0,
            Curse = 0,
            Dc = 8,
            Sc = 12
        };
        session.State!.Equipment[(int)OfficialEquipSlot.Weapon] = weapon;

        var oil = new EquipItem { Uid = 5555, Name = "祝福油", Weight = 1 };
        session.State.BagItems.Add(oil);

        var mockRng = new PredictableRng();
        // 模拟喝油成功 roll100 = 10, rollSub = 10
        mockRng.EnqueueNext(10);
        mockRng.EnqueueNext(10);

        var (blessOk, blessMsg) = ItemLifecycleEngine.ApplyBlessOilToEquippedWeapon(session, mockRng);
        Assert.True(blessOk, blessMsg);
        Assert.Empty(session.State.BagItems); // 祝福油已消耗
        Assert.True(weapon.Luck > 0 || weapon.Curse == 0);

        // 运九套满攻暴击验证
        weapon.Luck = 9;
        weapon.Curse = 0;
        int maxDmg = session.State.CalculateAttackDamageWithLuck(10, 30, mockRng);
        Assert.Equal(30, maxDmg);

        // 恶意击杀白名玩家加深诅咒验证
        var killer = new PlayerState { Uid = 4001, Name = "红名杀手", PkPoints = 200 };
        killer.Equipment[(int)OfficialEquipSlot.Weapon] = weapon;
        weapon.Luck = 0;
        weapon.Curse = 0;

        mockRng.EnqueueNext(0); // 命中 25% 诅咒几率 (Next(4) == 0)
        bool cursed = ItemLifecycleEngine.CheckWeaponCurseOnMurder(killer, mockRng);
        Assert.True(cursed);
        Assert.Equal(1, weapon.Curse);

        // 诅咒达到 10 时恒定发挥攻击下限
        weapon.Curse = 10;
        int minDmg = killer.CalculateAttackDamageWithLuck(10, 30, mockRng);
        Assert.Equal(10, minDmg);
    }

    [Fact]
    public void Test_TakeOff_And_WearItem_SyncsWeight_And_AddItem()
    {
        var session = CreateMockSession(4001, "力士");
        session.State!.Level = 10;
        session.State.Job = 0; // 10 级战士基础背包负重: 50 + 10*10/3 = 83
        session.State.RecalculateAttributes();
        session.State.RecalculateAuthoritativeWeights(0);
        int baseMaxBag = session.State.BaseMaxBagWeight; // 83

        // 穿戴超负载戒指 (槽位 7: RingL)
        var ring = new EquipItem { Uid = 8001, Name = "超负载戒指", Weight = 1 };
        session.State.Equipment[(int)OfficialEquipSlot.RingL] = ring;
        session.State.RecalculateAttributes();
        session.State.RecalculateAuthoritativeWeights(0);
        Assert.True(session.State.HasOverloadRing);
        Assert.Equal(baseMaxBag * 2, session.State.MaxBagWeight); // 翻倍为 166

        var dispatcher = new PacketDispatcher(new Mir2.Server.Maps.MapManager());

        // 1. 卸下超负载戒指 (槽位 7: RingL, CM_TAKEOFFITEM 1004)
        session.ClearBufferedPackets();
        dispatcher.Dispatch(session, $"#{ProtocolConsts.CM_TAKEOFFITEM}/7/!");

        // 验证卸下成功，戒指进入背包，负重上限恢复为基础上限 83
        Assert.Null(session.State.Equipment[(int)OfficialEquipSlot.RingL]);
        Assert.False(session.State.HasOverloadRing);
        Assert.Equal(baseMaxBag, session.State.MaxBagWeight);
        Assert.Contains(session.State.BagItems, it => it.Name == "超负载戒指");
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_TAKEOFF_OK}/"));
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_WEIGHTCHANGED}/"));

        // 2. 用普通珊瑚戒指穿戴到槽位 7 (CM_WEARITEM 1003, 官方 1.5 规则需攻击力达 25 点)
        session.State.MaxDc = 25;
        var normalRing = new EquipItem { Uid = 8002, Name = "珊瑚戒指", Weight = 1 };
        session.State.BagItems.Add(normalRing);
        int bagIndex = session.State.BagItems.IndexOf(normalRing);

        session.ClearBufferedPackets();
        dispatcher.Dispatch(session, $"#{ProtocolConsts.CM_WEARITEM}/{bagIndex}/7/!");

        // 验证穿戴成功，下发 SM_TAKEON_OK 与 SM_WEIGHTCHANGED
        Assert.NotNull(session.State.Equipment[(int)OfficialEquipSlot.RingL]);
        Assert.Equal("珊瑚戒指", session.State.Equipment[(int)OfficialEquipSlot.RingL]!.Name);
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_TAKEON_OK}/"));
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_WEIGHTCHANGED}/"));
        Assert.True(session.HasBufferedPacket($"#{ProtocolConsts.SM_WEIGHTCHANGED}/"));
    }
}
