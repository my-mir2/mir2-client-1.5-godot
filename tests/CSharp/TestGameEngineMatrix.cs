using System;
using Mir2Client.Game;
using Mir2Client.Render;
using Xunit;

namespace Mir2Client.Tests
{
    public class TestGameEngineMatrix
    {
        [Fact]
        public void TestSpriteEngineActionOffsetsAndMonsters()
        {
            Assert.Equal(0, SpriteEngine.ActionOffsets[ActionType.Stand]);
            Assert.Equal(64, SpriteEngine.ActionOffsets[ActionType.Walk]);
            Assert.Equal(128, SpriteEngine.ActionOffsets[ActionType.Run]);
            Assert.Equal(200, SpriteEngine.ActionOffsets[ActionType.Hit]);
            Assert.Equal(392, SpriteEngine.ActionOffsets[ActionType.Cast]);
            Assert.Equal(472, SpriteEngine.ActionOffsets[ActionType.Struck]);
            Assert.Equal(536, SpriteEngine.ActionOffsets[ActionType.Die]);

            // 触龙神 (appr 41)
            int chulong = SpriteEngine.CalcMonsterBaseOffset(41);
            Assert.Equal(600, chulong);

            // 森林雪人 (raceImg 10)
            int yetiStand = SpriteEngine.CalcMonsterFrame(ActionType.Stand, 2, 0, appr: 20, raceImg: 10);
            Assert.True(yetiStand >= 0);
        }

        [Fact]
        public void TestPlayerAuditEngineFormulas()
        {
            // 商店极品与折价计算
            var shopRes = PlayerAuditEngine.AuditCalcShopItemPrice(1000, 2, 100, 80);
            Assert.True(shopRes.BonusMarkup > 0);
            Assert.True(shopRes.SellPrice >= 2);

            // 三维负重 56 级封顶
            var wCapped = PlayerAuditEngine.AuditCalcThreeDimWeight(0, 57);
            Assert.True(wCapped.IsCapped);
            Assert.Equal(16711935, wCapped.BagWeight);

            // 太阳水与蓄水池
            var instant = PlayerAuditEngine.AuditProcessPotionDrink(1, 50, 50, 10, 100, 10, 100, 0, 0);
            Assert.Equal("instant", instant.Mode);
            Assert.Equal(60, instant.NewHp);

            var slow = PlayerAuditEngine.AuditProcessPotionDrink(0, 50, 50, 10, 100, 10, 100, 0, 0);
            Assert.Equal("reservoir", slow.Mode);
            Assert.Equal(50, slow.ReservoirHp);
        }

        [Fact]
        public void TestTradeManagerStateMachine()
        {
            var tm = new TradeManager();
            Assert.Equal(TradeState.Idle, tm.State);

            tm.StartTrade("伙伴测试");
            Assert.Equal(TradeState.Trading, tm.State);
            Assert.Equal("伙伴测试", tm.PeerName);

            var item = new EquipItem { Name = "木剑", Weight = 7 };
            Assert.True(tm.PutItem(0, item));
            Assert.NotNull(tm.LocalItems[0]);

            Assert.True(tm.LockTrade());
            Assert.True(tm.LocalLocked);

            // 双方锁定
            tm.SetRemoteLocked(true);
            Assert.Equal(TradeState.Locked, tm.State);

            // 对方变动物品触发官方防掉包重置机制
            tm.RemoteAddItem(0, new EquipItem { Name = "金创药" });
            Assert.False(tm.LocalLocked);
            Assert.False(tm.RemoteLocked);
            Assert.Equal(TradeState.Trading, tm.State);
        }

        [Fact]
        public void TestStorageManagerCapacityAndGold()
        {
            var sm = new StorageManager();
            Assert.Empty(sm.StorageItems);

            for (int i = 0; i < StorageManager.MaxStorageCapacity; i++)
            {
                bool ok = sm.DepositItem(new EquipItem { Name = $"道具_{i}" }, out string err);
                Assert.True(ok);
            }

            // 第 51 件拦截
            bool overflow = sm.DepositItem(new EquipItem { Name = "溢出道具" }, out string errOver);
            Assert.False(overflow);
            Assert.Contains("上限50格", errOver);

            // 金币存储上限
            bool goldOk = sm.DepositGold(5_000_000, 5_000_000, out int newPlGold, out _);
            Assert.True(goldOk);
            Assert.Equal(0, newPlGold);
            Assert.Equal(5_000_000, sm.StorageGold);
        }

        [Fact]
        public void TestBlessOilFormulasAndCap()
        {
            // 运 0 必升运 1 (未触发 5% 诅咒)
            var res0 = PlayerAuditEngine.AuditDrinkBlessOil(30, 0, 0, 0, 0, 0);
            Assert.True(res0.Success);
            Assert.Equal(1, res0.NewLuck);

            // 5% 诅咒恶运 (rngRoll100 % 20 == 1)
            var resCurse = PlayerAuditEngine.AuditDrinkBlessOil(30, 0, 3, 0, 1, 0);
            Assert.True(resCurse.Cursed);
            Assert.Equal(2, resCurse.NewLuck);

            // 清除已有诅咒
            var resClear = PlayerAuditEngine.AuditDrinkBlessOil(30, 0, 0, 2, 0, 0);
            Assert.True(resClear.CurseCleared);
            Assert.Equal(1, resClear.NewCurse);

            // 运 >= 7 绝对封顶锁死
            var resCap = PlayerAuditEngine.AuditDrinkBlessOil(30, 0, 7, 0, 0, 0);
            Assert.False(resCap.Success);
            Assert.Equal(7, resCap.NewLuck);
            Assert.Equal("Nowt", resCap.Notice);
        }

        [Fact]
        public void TestWeaponUpgradeAndDura()
        {
            // 黑铁矿石前 5 块采样与纯度测试
            var puritiesLow = new[] { 1, 2, 3 };
            var duraLow = PlayerAuditEngine.AuditCalcUpgradeDura(puritiesLow, 0);
            Assert.Equal(6, duraLow.TotalPurity);
            Assert.Equal(-3000, duraLow.DuraDelta); // 总纯度 < 9 掉 3 点

            var puritiesHigh = new[] { 15, 10, 8, 5, 2, 1 }; // 6 块，只取前 5 块 (15+10+8+5+2 = 40)
            var duraHigh = PlayerAuditEngine.AuditCalcUpgradeDura(puritiesHigh, 0);
            Assert.Equal(5, duraHigh.TopCount);
            Assert.Equal(40, duraHigh.TotalPurity);
            Assert.True(duraHigh.DuraDelta > 0);

            // 首饰成功率与跳点
            var upgRes = PlayerAuditEngine.AuditCalcUpgradeSuccess(10, 6, 2, 1, 50, 0);
            Assert.True(upgRes.SuccessRatePercent <= 85);
            Assert.True(upgRes.JewelryScore <= 11);
        }

        [Fact]
        public void TestDeathDropAndGuardMechanics()
        {
            // 红名卫士秒杀
            var guardHitRed = PlayerAuditEngine.AuditGuardCheckTarget(250, false, false);
            Assert.True(guardHitRed.ShouldAttack);
            Assert.Equal(9999, guardHitRed.Damage);
            Assert.Equal(10004, guardHitRed.ActionCode);

            // 白名卫士不攻击
            var guardHitWhite = PlayerAuditEngine.AuditGuardCheckTarget(50, false, false);
            Assert.False(guardHitWhite.ShouldAttack);

            // 死亡金币 17 堆切分
            // 35000 金币: 前 12 堆每次 2000 (24000), 剩余 11000 再次切 2000 (第 13 堆, 累积 26000), 剩余 9000 <= 10000 全部作为第 14 堆
            var deathDrop = PlayerAuditEngine.AuditCalcDeathDrop(250, 35000, 0);
            Assert.True(deathDrop.BagWillDropAll); // 红名必大爆
            Assert.Equal(15, deathDrop.EquipDropDenom); // 红名 1/15 掉率翻倍
            Assert.Equal(14, deathDrop.GoldPilesCount);
            Assert.Equal(2000, deathDrop.GoldPiles[0]);
            Assert.Equal(9000, deathDrop.GoldPiles[13]);
            Assert.Equal(0, deathDrop.RemainingGold);

            // 巨额金币 100,000 切满 17 堆上限 (17 * 2000 = 34,000), 剩余 66,000 返还
            var dropLarge = PlayerAuditEngine.AuditCalcDeathDrop(250, 100_000, 0);
            Assert.Equal(17, dropLarge.GoldPilesCount);
            Assert.Equal(66_000, dropLarge.RemainingGold);
        }

        [Fact]
        public void TestEquipWearAndRushMechanics()
        {
            // 衣服槽位受击 100% 磨损
            var (armorWorn, armorLoss) = PlayerAuditEngine.AuditCalcEquipWear(0, false, 5, 2);
            Assert.True(armorWorn);
            Assert.True(armorLoss >= 5);

            // 首饰槽位受击仅 1/8 几率
            var (jewelWorn1, _) = PlayerAuditEngine.AuditCalcEquipWear(1, false, 7, 2);
            Assert.False(jewelWorn1);
            var (jewelWorn2, _) = PlayerAuditEngine.AuditCalcEquipWear(1, false, 8, 2);
            Assert.True(jewelWorn2);

            // 武器运 7 刀刀免磨损
            var (weapWorn, _) = PlayerAuditEngine.AuditCalcWeaponAttackWear(7, 3);
            Assert.False(weapWorn);

            // 野蛮冲撞推人
            var rushWin = PlayerAuditEngine.AuditRushThrust(40, 35, 3, false, 0);
            Assert.True(rushWin.Success);
            Assert.Equal(4, rushWin.PushDistance);
            Assert.Equal(10015, rushWin.ActionCode);

            // 撞不动反震
            var rushFail = PlayerAuditEngine.AuditRushThrust(35, 40, 3, false, 0);
            Assert.False(rushFail.Success);
            Assert.Equal(10016, rushFail.ActionCode);
            Assert.Equal("Too Weak", rushFail.Notice);
        }

        [Fact]
        public void TestPlayerStateCombatDamageAndRings()
        {
            var ps = new PlayerState
            {
                Hp = 100,
                MaxHp = 100,
                Mp = 100,
                MaxMp = 100
            };

            // 1. 穿戴武器并喝祝福油
            var sword = new EquipItem { Name = "裁决之杖", Dc = 30, Luck = 0, Curse = 0 };
            ps.WearItem(OfficialEquipSlot.Weapon, sword);

            var resLuck = ps.DrinkBlessOil(30, 0, 0, 0);
            Assert.True(resLuck.Success);
            Assert.Equal(1, sword.Luck);

            // 2. 状态掩码计算
            ps.StatusBits[0] = true; // 绿毒
            ps.StatusBits[5] = true; // 麻痹
            uint mask = ps.GetCurrentStatusMask();
            Assert.True((mask & 0x80000000u) != 0); // 绿毒位
            Assert.True((mask & 0x04000000u) != 0); // 麻痹位

            // 3. 战斗承伤: 护身戒指 1:1.5 抵扣 (受到 20 伤害需扣 30 MP)
            int dura = 2000;
            var (hp1, mp1, died1, rev1) = ps.TakeCombatDamage(20, hasProtectRing: true, hasReviveRing: false, ref dura, 0, 100000);
            Assert.Equal(100, hp1);
            Assert.Equal(70, mp1);
            Assert.False(died1);
            Assert.False(rev1);

            // 4. 战斗承伤: 致命伤害与复活戒指触发 (受到 999 伤害，复活戒指满血满蓝)
            var (hp2, mp2, died2, rev2) = ps.TakeCombatDamage(999, hasProtectRing: false, hasReviveRing: true, ref dura, 0, 100000);
            Assert.Equal(100, hp2); // 满血复活
            Assert.Equal(1000, dura); // 扣 1000 点底层持久
            Assert.False(died2);
            Assert.True(rev2);
        }
    }
}
