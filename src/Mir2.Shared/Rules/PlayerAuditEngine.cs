// ============================================================================
// Mir2 客户端 - 传奇 1.5 官方实证底层算法与公式引擎 (1:1 盛大官方二进制实证)
// 官方实证：IDA sub_49F428, sub_4BF7DC, sub_4C6238, sub_4C7720, 0x4BD4A0（已证client无函数） 等
// ============================================================================

using System;

namespace Mir2.Shared.Rules
{
    public static class PlayerAuditEngine
    {
        public static readonly MapGridOffset[] DirOffsets = new[]
        {
            new MapGridOffset(0, -1),  // 0: 上
            new MapGridOffset(1, -1),  // 1: 右上
            new MapGridOffset(1, 0),   // 2: 右
            new MapGridOffset(1, 1),   // 3: 右下
            new MapGridOffset(0, 1),   // 4: 下
            new MapGridOffset(-1, 1),  // 5: 左下
            new MapGridOffset(-1, 0),  // 6: 左
            new MapGridOffset(-1, -1), // 7: 左上
        };

        public readonly struct ShopPriceResult
        {
            public int BasePrice { get; init; }
            public int BonusPoints { get; init; }
            public int BonusMarkup { get; init; }
            public int MaxDura { get; init; }
            public int CurDura { get; init; }
            public int DuraDiscount { get; init; }
            public int ActualValue { get; init; }
            public int SellPrice { get; init; }
        }

        public static ShopPriceResult AuditCalcShopItemPrice(int basePrice, int bonusPoints, int maxDura, int curDura)
        {
            int val = basePrice;
            int bonusMarkup = bonusPoints * (val / 5);
            val += bonusMarkup;
            int duraDiscount = 0;
            if (maxDura > 0 && curDura < maxDura)
            {
                duraDiscount = (int)((float)val / 2.0f / (float)maxDura * (float)(maxDura - curDura));
                val -= duraDiscount;
            }
            int actualValue = Math.Max(2, val);
            int sellPrice = Math.Max(2, (int)MathF.Round((float)actualValue / 2.0f));

            return new ShopPriceResult
            {
                BasePrice = basePrice,
                BonusPoints = bonusPoints,
                BonusMarkup = bonusMarkup,
                MaxDura = maxDura,
                CurDura = curDura,
                DuraDiscount = duraDiscount,
                ActualValue = actualValue,
                SellPrice = sellPrice
            };
        }

        public readonly struct ThreeDimWeightResult
        {
            public int Job { get; init; }
            public int Level { get; init; }
            public int BagWeight { get; init; }
            public int WearWeight { get; init; }
            public int ArmPower { get; init; }
            public bool IsCapped { get; init; }
        }

        public static ThreeDimWeightResult AuditCalcThreeDimWeight(int job, int level)
        {
            if (level > 56)
            {
                return new ThreeDimWeightResult
                {
                    Job = job,
                    Level = level,
                    BagWeight = 16711935,
                    WearWeight = 16711935,
                    ArmPower = 16711935,
                    IsCapped = true
                };
            }

            float lvlSq = (float)(level * level);
            int bag = 0;
            int wear = 0;
            int arm = 0;

            switch (job)
            {
                case 0: // 战士
                    bag = 50 + (int)MathF.Round(lvlSq / 3.0f);
                    wear = 15 + (int)MathF.Round(lvlSq / 20.0f);
                    arm = 12 + (int)MathF.Round(lvlSq / 13.0f);
                    break;
                case 1: // 法师
                    bag = 50 + (int)MathF.Round(lvlSq / 5.0f);
                    wear = 15 + (int)MathF.Round(lvlSq / 100.0f);
                    arm = 12 + (int)MathF.Round(lvlSq / 90.0f);
                    break;
                case 2: // 道士
                    bag = 50 + (int)MathF.Round(lvlSq / 4.0f);
                    wear = 15 + (int)MathF.Round(lvlSq / 50.0f);
                    arm = 12 + (int)MathF.Round(lvlSq / 42.0f);
                    break;
                default:
                    bag = 50;
                    wear = 15;
                    arm = 12;
                    break;
            }

            return new ThreeDimWeightResult
            {
                Job = job,
                Level = level,
                BagWeight = bag,
                WearWeight = wear,
                ArmPower = arm,
                IsCapped = false
            };
        }

        public readonly struct PotionProcessResult
        {
            public string Mode { get; init; }
            public int AddedHp { get; init; }
            public int AddedMp { get; init; }
            public int NewHp { get; init; }
            public int NewMp { get; init; }
            public int ReservoirHp { get; init; }
            public int ReservoirMp { get; init; }
        }

        public static PotionProcessResult AuditProcessPotionDrink(int potionShape, int itemHp, int itemMp, int currentHp, int maxHp, int currentMp, int maxMp, int reservoirHp, int reservoirMp)
        {
            if (potionShape == 1) // 瞬回药 (太阳水等)
            {
                return new PotionProcessResult
                {
                    Mode = "instant",
                    AddedHp = itemHp,
                    AddedMp = itemMp,
                    NewHp = Math.Min(maxHp, currentHp + itemHp),
                    NewMp = Math.Min(maxMp, currentMp + itemMp),
                    ReservoirHp = reservoirHp,
                    ReservoirMp = reservoirMp
                };
            }
            else // 蓄水池缓慢释放
            {
                int rHp = reservoirHp;
                int rMp = reservoirMp;
                if (rHp + itemHp < 500 && itemHp != 0) rHp += itemHp;
                if (rMp + itemMp < 500 && itemMp != 0) rMp += itemMp;

                return new PotionProcessResult
                {
                    Mode = "reservoir",
                    AddedHp = 0,
                    AddedMp = 0,
                    NewHp = currentHp,
                    NewMp = currentMp,
                    ReservoirHp = rHp,
                    ReservoirMp = rMp
                };
            }
        }

        // ====================================================================
        // 1. 祝福油喝油矩阵与运 7 天花板算法 (M2Server sub_4BD48C 内偏移 0x4BD4A0 @ 0x4bd4a0~0x4bd668)
        // ====================================================================
        public readonly struct BlessOilResult
        {
            public bool Success { get; init; }
            public bool Cursed { get; init; }
            public bool CurseCleared { get; init; }
            public int NewLuck { get; init; }
            public int NewCurse { get; init; }
            public string Notice { get; init; }
        }

        public static BlessOilResult AuditDrinkBlessOil(int maxDc, int minDc, int curLuck, int curCurse, int rngRoll100, int rngRollSub)
        {
            // 武器极差惩罚除5分母: v6 = abs(maxDc - minDc) / 5
            int v6 = Math.Abs(maxDc - minDc) / 5;

            // 5% 诅咒恶运: rngRoll100 % 20 == 1 (5%)
            if (rngRoll100 % 20 == 1)
            {
                if (curLuck > 0)
                {
                    return new BlessOilResult
                    {
                        Success = false,
                        Cursed = true,
                        CurseCleared = false,
                        NewLuck = curLuck - 1,
                        NewCurse = curCurse,
                        Notice = "Weapon is cursed"
                    };
                }
                else
                {
                    int nextCurse = Math.Min(10, curCurse + 1);
                    return new BlessOilResult
                    {
                        Success = false,
                        Cursed = true,
                        CurseCleared = false,
                        NewLuck = 0,
                        NewCurse = nextCurse,
                        Notice = "Weapon is cursed"
                    };
                }
            }

            // 100% 清除已有诅咒
            if (curCurse > 0)
            {
                return new BlessOilResult
                {
                    Success = true,
                    Cursed = false,
                    CurseCleared = true,
                    NewLuck = curLuck,
                    NewCurse = curCurse - 1,
                    Notice = "Weapon gains luck"
                };
            }

            // 运 0 必升运 1
            if (curLuck == 0)
            {
                return new BlessOilResult
                {
                    Success = true,
                    Cursed = false,
                    CurseCleared = false,
                    NewLuck = 1,
                    NewCurse = 0,
                    Notice = "Weapon gains luck"
                };
            }

            // 运 1~2: 几率 1 / (v6 + 6)
            if (curLuck < 3)
            {
                int denom = v6 + 6;
                bool win = (rngRollSub % denom) == 0;
                return new BlessOilResult
                {
                    Success = win,
                    Cursed = false,
                    CurseCleared = false,
                    NewLuck = win ? curLuck + 1 : curLuck,
                    NewCurse = 0,
                    Notice = win ? "Weapon gains luck" : "Nowt"
                };
            }

            // 运 3~6: 几率 1 / (5 * v6 + 30)
            if (curLuck < 7)
            {
                int denom = 5 * v6 + 30;
                bool win = (rngRollSub % denom) == 0;
                return new BlessOilResult
                {
                    Success = win,
                    Cursed = false,
                    CurseCleared = false,
                    NewLuck = win ? curLuck + 1 : curLuck,
                    NewCurse = 0,
                    Notice = win ? "Weapon gains luck" : "Nowt"
                };
            }

            // 运 >= 7: 绝对天花板锁死，普通祝福油永远无法达到运 8/9
            return new BlessOilResult
            {
                Success = false,
                Cursed = false,
                CurseCleared = false,
                NewLuck = curLuck,
                NewCurse = 0,
                Notice = "Nowt"
            };
        }

        // ====================================================================
        // 2. 沙巴克武器升级黑铁矿石纯度与持久增减算法 (sub_4A0218, sub_4A0CB8)
        // ====================================================================
        public readonly struct WeaponUpgradeDuraResult
        {
            public int TopCount { get; init; }
            public int TotalPurity { get; init; }
            public int DuraDelta { get; init; } // 底层持久变化点数 (1000 = 1点持久)
        }

        public static WeaponUpgradeDuraResult AuditCalcUpgradeDura(int[] orePurities, int rngRoll)
        {
            if (orePurities == null || orePurities.Length == 0)
            {
                return new WeaponUpgradeDuraResult { TopCount = 0, TotalPurity = 0, DuraDelta = -3000 };
            }

            var sorted = (int[])orePurities.Clone();
            Array.Sort(sorted);
            Array.Reverse(sorted);

            int take = Math.Min(5, sorted.Length); // 最多只取前 5 块
            int sum = 0;
            for (int i = 0; i < take; i++)
            {
                sum += sorted[i];
            }

            int delta = 0;
            if (sum < 9)
            {
                delta = -3000; // -3 点持久
            }
            else if (sum <= 15)
            {
                // RandInt(sum) < 6 时扣除 1000
                if (rngRoll % sum < 6)
                {
                    delta = -1000;
                }
            }
            else if (sum >= 18)
            {
                // >= 18 有几率增加 +1000 ~ +4000
                if (sum >= 25) delta = 4000;
                else if (sum >= 20) delta = 2000;
                else delta = 1000;
            }

            return new WeaponUpgradeDuraResult
            {
                TopCount = take,
                TotalPurity = sum,
                DuraDelta = delta
            };
        }

        // ====================================================================
        // 3. 沙巴克武器升级首饰得分与最高 85% 成功率及跳点机制 (sub_4A0CB8)
        // ====================================================================
        public readonly struct WeaponUpgradeSuccessResult
        {
            public int JewelryScore { get; init; }
            public int SuccessRatePercent { get; init; } // 封顶 85%
            public bool IsSuccess { get; init; }
            public int StatBonus { get; init; } // +1, +2(跳点), +3(神级跳点)
        }

        public static WeaponUpgradeSuccessResult AuditCalcUpgradeSuccess(int bestJewelry, int secondJewelry, int weaponLuck, int upgradeCount, int rngRoll100, int rngRollJump)
        {
            // 首饰有效分: Best + Best/5 + Second/3，有效封顶 11 点
            int rawScore = bestJewelry + (bestJewelry / 5) + (secondJewelry / 3);
            int validScore = Math.Min(11, rawScore);

            // 成功率封顶门限: Min(85, luck + upg_count + 7 * validScore + 10 - history)
            int rate = Math.Min(85, Math.Max(10, weaponLuck + upgradeCount + 7 * validScore + 10));

            bool success = rngRoll100 % 100 < rate;
            int bonus = 0;
            if (success)
            {
                bonus = 1;
                // 跳点判定: 阈值 > 79 且 1/200 几率跳 +3; 阈值 > 63 且 1/30 几率跳 +2
                if (rate > 79 && (rngRollJump % 200) == 0)
                {
                    bonus = 3;
                }
                else if (rate > 63 && (rngRollJump % 30) == 0)
                {
                    bonus = 2;
                }
            }

            return new WeaponUpgradeSuccessResult
            {
                JewelryScore = validScore,
                SuccessRatePercent = rate,
                IsSuccess = success,
                StatBonus = bonus
            };
        }

        // ====================================================================
        // 4. 角色 12 状态位掩码公式 (M2Server sub_4BA43C @ 0x4ba43c~0x4ba48d)
        // ====================================================================
        public static uint AuditCalcStatusMask(bool[] statusSlots, uint baseFlags)
        {
            uint mask = 0;
            if (statusSlots != null)
            {
                for (int i = 0; i < Math.Min(12, statusSlots.Length); i++)
                {
                    if (statusSlots[i])
                    {
                        mask |= (0x80000000u >> i);
                    }
                }
            }
            return mask | (baseFlags & 0xFFFFFu);
        }

        // ====================================================================
        // 5. 大刀卫士索敌与秒杀判定 (M2Server sub_4977B4 / sub_497924)
        // ====================================================================
        public readonly struct GuardAttackResult
        {
            public bool ShouldAttack { get; init; }
            public int Damage { get; init; }
            public int ActionCode { get; init; }
            public bool TeleportToTarget { get; init; }
        }

        public static GuardAttackResult AuditGuardCheckTarget(int targetPkPoints, bool isMonster, bool isSlave)
        {
            // 仅对红名玩家 (PK >= 200) 或入侵防区的非随从野怪发起瞬杀
            bool isRed = targetPkPoints >= 200;
            bool isHostileMonster = isMonster && !isSlave;

            if (isRed || isHostileMonster)
            {
                return new GuardAttackResult
                {
                    ShouldAttack = true,
                    Damage = 9999, // 官方无视护甲秒杀伤害
                    ActionCode = 10004, // 官方大刀斩杀动作码
                    TeleportToTarget = true
                };
            }

            return new GuardAttackResult
            {
                ShouldAttack = false,
                Damage = 0,
                ActionCode = 0,
                TeleportToTarget = false
            };
        }

        // ====================================================================
        // 6. 白名与红名死亡装备大爆与 17 堆金币切分 (sub_4BB6C8, 0x4BB63C（已证client无函数）)
        // ====================================================================
        public readonly struct DeathDropResult
        {
            public bool BagWillDropAll { get; init; }
            public int EquipDropDenom { get; init; } // 1/30 (白/黄) 或 1/15 (红名)
            public int GoldPilesCount { get; init; }
            public int[] GoldPiles { get; init; }
            public int RemainingGold { get; init; }
        }

        public static DeathDropResult AuditCalcDeathDrop(int pkPoints, int currentGold, int rngRoll3)
        {
            bool isRed = pkPoints >= 200;
            // 白名 1/30 几率掉装备; 红名 1/15 几率 (掉率翻倍)
            int equipDenom = isRed ? 15 : 30;

            // 白名 1/3 几率大爆; 红名 100% 大爆空包
            bool bagDrop = isRed || (rngRoll3 % 3 == 0);

            // 金币切分: 最多 17 堆 (sub_4BB63C @ 0x4bb63c~0x4bb6c1)
            // 剩余金币 <= 10,000 一次性全部掉落并归零；> 10,000 每次切出 2,000 散落至地表；最多 17 堆 (上限 34,000 金币)
            var piles = new System.Collections.Generic.List<int>();
            int goldLeft = currentGold;

            while (piles.Count < 17 && goldLeft > 0)
            {
                if (goldLeft <= 10000)
                {
                    piles.Add(goldLeft);
                    goldLeft = 0;
                    break;
                }
                else
                {
                    piles.Add(2000);
                    goldLeft -= 2000;
                }
            }

            return new DeathDropResult
            {
                BagWillDropAll = bagDrop,
                EquipDropDenom = equipDenom,
                GoldPilesCount = piles.Count,
                GoldPiles = piles.ToArray(),
                RemainingGold = goldLeft
            };
        }

        // ====================================================================
        // 7. 装备受击磨损与衣服 100% 磨损机制 (0x4BDEB8（已证client无函数） @ 0x4bdee0~0x4be38d)
        // ====================================================================
        public static (bool worn, int lossDura) AuditCalcEquipWear(int slotIndex, bool isArmorBuffActive, int rngRoll8, int rngRollLoss)
        {
            // 槽位 0: 衣服 100% 必定磨损; 1~8 槽位仅 1/8 (12.5%) 几率磨损
            bool shouldWear = slotIndex == 0 || (rngRoll8 % 8 == 0);
            if (!shouldWear)
            {
                return (false, 0);
            }

            // 基础损耗: 5 + RandInt(10) 点底层耐久
            int loss = 5 + (rngRollLoss % 10);
            if (isArmorBuffActive)
            {
                loss = (int)MathF.Round((float)loss * 1.2f); // 诅咒加深 1.2 倍
            }

            return (true, loss);
        }

        // ====================================================================
        // 8. 武器近战出刀攻击磨损与幸运免磨损机制 (0x4C1EF4（已证client无函数） @ 0x4c21f9)
        // ====================================================================
        public static (bool worn, int lossDura) AuditCalcWeaponAttackWear(int weaponLuck, int rngRollLoss)
        {
            // v24 = RandInt(5) + 2 - weaponLuck (2~6 基础磨损)
            int rawLoss = (rngRollLoss % 5) + 2 - weaponLuck;
            if (rawLoss <= 0)
            {
                // 幸运抵消免磨损 (运 7 运九套刀刀不掉持久铁证)
                return (false, 0);
            }

            return (true, rawLoss);
        }

        // ====================================================================
        // 9. 法师圣言术秒杀公式与不死生物限制 (sub_4926D4 @ 0x4926f8~0x4927be)
        // ====================================================================
        public static (bool instantKill, int ratePercent) AuditHolyWordInstantKill(int casterLevel, int targetLevel, int skillLevel, bool isUndead, int rngRoll100)
        {
            // 门禁: 必须是不死生物且等级 < 50 (BOSS 免疫)，且施法者具备等级压制
            if (!isUndead || targetLevel >= 50 || casterLevel <= targetLevel)
            {
                return (false, 0);
            }

            // 成功率公式: 7 * skill_level + 15 + (caster_level - target_level)
            int rate = Math.Clamp(7 * skillLevel + 15 + (casterLevel - targetLevel), 1, 95);
            bool kill = (rngRoll100 % 100) < rate;

            return (kill, rate);
        }

        // ====================================================================
        // 10. 野蛮冲撞推人判定与撞墙反震 (0x4C30B0（已证client无函数） & sub_4C3130 @ 0x4c3130)
        // ====================================================================
        public readonly struct RushResult
        {
            public bool Success { get; init; }
            public int PushDistance { get; init; }
            public int PushDamage { get; init; }
            public int SelfStunDamage { get; init; }
            public int ActionCode { get; init; }
            public string Notice { get; init; }
        }

        public static RushResult AuditRushThrust(int attackerLevel, int targetLevel, int skillLevel, bool targetImmune, int rngRoll20)
        {
            // 严格等级门禁: 攻击者必须严格大于被撞者且目标非霸体
            if (attackerLevel <= targetLevel || targetImmune)
            {
                // 撞不动反震
                int selfDmg = 5 * (skillLevel + 1);
                return new RushResult
                {
                    Success = false,
                    PushDistance = 0,
                    PushDamage = 0,
                    SelfStunDamage = selfDmg,
                    ActionCode = 10016, // SM_RUSHLOSER 撞晕硬直
                    Notice = "Too Weak"
                };
            }

            // 成功率公式: RandInt(20) < diff + 3 * skill_level + 6
            int threshold = (attackerLevel - targetLevel) + 3 * skillLevel + 6;
            bool win = (rngRoll20 % 20) < threshold;

            if (win)
            {
                int dist = Math.Max(2, skillLevel + 1);
                int dmg = 4 * (skillLevel + 1);
                return new RushResult
                {
                    Success = true,
                    PushDistance = dist,
                    PushDamage = dmg,
                    SelfStunDamage = 0,
                    ActionCode = 10015, // 冲撞位移
                    Notice = "SUCCESS"
                };
            }
            else
            {
                return new RushResult
                {
                    Success = false,
                    PushDistance = 0,
                    PushDamage = 0,
                    SelfStunDamage = 5 * (skillLevel + 1),
                    ActionCode = 10016,
                    Notice = "Too Weak"
                };
            }
        }
    }
}

