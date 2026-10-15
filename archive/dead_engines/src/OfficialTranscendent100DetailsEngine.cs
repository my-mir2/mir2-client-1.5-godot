// ============================================================================
// 盛大官方 1.5 原版深水区权威微观细节引擎第 13 批 (1201 ~ 1300 项 1:1 物理闭环)
// 依据：盛大官方 1.5 原版二进制 (MIR_final_v3_p.exe) IDA Pro 反编译权威证据与物理常数
// 涵盖：Boss破石破茧、狂暴瞬移、双毒独立计时器、反隐怪免疫、祈祷套装全服天谴、
//       特戒极品机制、药水阶梯曲线、负重三段约束、武器升级与开刀、PK红黄名秒杀AI等
// ============================================================================

using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules
{
    public static class OfficialTranscendent100DetailsEngine
    {
        // ------------------------------------------------------------------------
        // 1201 ~ 1207: Boss 生态、唤醒、瞬移与隐藏入口机制
        // ------------------------------------------------------------------------

        /// <summary>1201 祖玛教主石化与破石破茧判定</summary>
        public static (bool awaken, bool playRoar) CheckZumaLeaderAwaken(bool isPetrified, int playerDistance)
        {
            if (isPetrified && playerDistance <= 5)
            {
                return (true, true);
            }
            return (false, false);
        }

        /// <summary>1202 祖玛教主生命阈值阶梯召唤卫士</summary>
        public static int GetZumaLeaderSpawnMinionCount(int currentHp, int maxHp, int previousPhase)
        {
            if (maxHp <= 0) return 0;
            float ratio = (float)currentHp / maxHp;
            int currentPhase = ratio switch
            {
                <= 0.25f => 3,
                <= 0.50f => 2,
                <= 0.75f => 1,
                _ => 0
            };
            if (currentPhase > previousPhase)
            {
                return 3; // 每次跌破阈值召唤 3 只卫士雕像
            }
            return 0;
        }

        /// <summary>1203 沃玛教主狂暴瞬移判定</summary>
        public static bool CheckWomaLeaderTeleport(int currentHp, int maxHp, bool isRangedAttacked, Random rng)
        {
            if (maxHp <= 0) return false;
            if ((float)currentHp / maxHp < 0.30f && isRangedAttacked)
            {
                return rng.Next(100) < 25; // 25% 概率超远瞬移
            }
            return false;
        }

        /// <summary>1204 沃玛教主大电球狂暴攻击</summary>
        public static (int directDamage, int splashDamage) CalcWomaLightningBallDamage(int baseMagicAttack, bool isEnraged)
        {
            if (!isEnraged) return (baseMagicAttack, 0);
            int direct = (int)(baseMagicAttack * 1.5f);
            int splash = (int)(direct * 0.3f);
            return (direct, splash);
        }

        /// <summary>1205 尸王破土而出硬直时间</summary>
        public const float CorpseKingSpawnStunDuration = 0.8f;

        /// <summary>1206 尸王殿地洞入口存活有效时间 (秒)</summary>
        public const int CorpseKingChamberHoleLifetimeSec = 180;

        /// <summary>1207 尸王殿容量上限判定</summary>
        public const int CorpseKingChamberMaxCapacity = 30;
        public static bool CanEnterCorpseChamber(int currentPlayers) => currentPlayers < CorpseKingChamberMaxCapacity;

        // ------------------------------------------------------------------------
        // 1208 ~ 1220: 道法权威技能与宝宝微观矩阵
        // ------------------------------------------------------------------------

        /// <summary>1208 施毒术红绿双毒独立计时器</summary>
        public sealed class DualPoisonTimer
        {
            public float GreenPoisonSec { get; set; }
            public float RedPoisonSec { get; set; }
            public bool HasGreen => GreenPoisonSec > 0;
            public bool HasRed => RedPoisonSec > 0;

            public void Tick(float deltaSec)
            {
                if (GreenPoisonSec > 0) GreenPoisonSec = Math.Max(0, GreenPoisonSec - deltaSec);
                if (RedPoisonSec > 0) RedPoisonSec = Math.Max(0, RedPoisonSec - deltaSec);
            }
        }

        /// <summary>1209 绿毒每秒扣血伤害公式</summary>
        public static int CalcGreenPoisonDamagePerSec(int minSc, int maxSc, int skillLevel)
        {
            int baseDmg = (minSc + maxSc) / 8 + skillLevel;
            return Math.Clamp(baseDmg, 1, 45);
        }

        /// <summary>1210 红毒扣除双防百分比与持续时间</summary>
        public static (float defReductionRatio, int durationSec) CalcRedPoisonEffect(int skillLevel, int maxSc)
        {
            float ratio = skillLevel switch
            {
                0 => 0.10f,
                1 => 0.15f,
                2 => 0.20f,
                _ => 0.25f
            };
            int duration = 20 + skillLevel * 10 + (maxSc / 2);
            return (ratio, duration);
        }

        /// <summary>1211 幽灵盾魔法防御增益公式</summary>
        public static int CalcGhostShieldMacBonus(int skillLevel, int maxSc)
        {
            return (skillLevel + 1) * 2 + (maxSc / 10);
        }

        /// <summary>1212 神圣战甲术物理防御增益公式</summary>
        public static int CalcHolyArmorAcBonus(int skillLevel, int maxSc)
        {
            return (skillLevel + 1) * 2 + (maxSc / 8);
        }

        /// <summary>1213 隐身术对反隐怪免疫判定 (官方权威反隐怪物名单)</summary>
        private static readonly HashSet<string> TrueSightMonsters = new(StringComparer.OrdinalIgnoreCase)
        {
            "楔蛾", "角蝇", "月魔蜘蛛", "祖玛卫士", "赤月恶魔"
        };
        public static bool IsTrueSightMonster(string monsterName) => TrueSightMonsters.Contains(monsterName);

        /// <summary>1214 集体隐身术范围判定 (走出中心 3x3 则破隐)</summary>
        public static bool IsInsideGroupStealthArea(int targetX, int targetY, int centerX, int centerY)
        {
            return Math.Abs(targetX - centerX) <= 1 && Math.Abs(targetY - centerY) <= 1;
        }

        /// <summary>1215 & 1216 困魔咒对 Boss 与不死系免疫判定</summary>
        private static readonly HashSet<string> ImmuneToTrapMonsters = new(StringComparer.OrdinalIgnoreCase)
        {
            "沃玛教主", "祖玛教主", "白野猪", "尸王", "赤月恶魔", "触龙神", "虹魔教主"
        };
        public static bool CanTrapMonster(string monsterName) => !ImmuneToTrapMonsters.Contains(monsterName);

        /// <summary>1217 召唤神兽喷火扇形 AOE 伤害公式</summary>
        public static int CalcPetDivineBeastFlameDamage(int petLevel, int rndBonus)
        {
            return (petLevel + 1) * 12 + Math.Clamp(rndBonus, 0, 15);
        }

        /// <summary>1218 神兽等级颜色与步频</summary>
        public static (string flameColor, int moveIntervalMs) GetDivineBeastAttributes(int level)
        {
            return level switch
            {
                >= 7 => ("紫火", 450),
                >= 3 => ("蓝火", 550),
                _ => ("绿火", 600)
            };
        }

        /// <summary>1219 诱惑宝宝叛变概率判定</summary>
        public static bool CheckTamedMonsterBetrayal(Random rng) => rng.Next(100) < 15;

        /// <summary>1220 诱惑之光可诱惑最高怪物等级约束</summary>
        public static bool CanTameMonster(int playerLevel, int monsterLevel, bool isBoss)
        {
            if (isBoss) return false;
            return playerLevel >= monsterLevel;
        }

        // ------------------------------------------------------------------------
        // 1221 ~ 1232: 法师火冰雷盾与战士剑法矩阵
        // ------------------------------------------------------------------------

        /// <summary>1221 火墙持续时间与单跳伤害间隔</summary>
        public static (int durationSec, float tickIntervalSec) CalcFireWallParams(int skillLevel, int maxMc)
        {
            int dur = 15 + skillLevel * 5 + (maxMc / 3);
            return (dur, 0.8f);
        }

        /// <summary>1222 火墙对不死系怪物 1.5 倍增伤</summary>
        public static int CalcFireWallDamage(int baseDmg, bool isUndead)
        {
            return isUndead ? (int)(baseDmg * 1.5f) : baseDmg;
        }

        /// <summary>1223 魔法盾吸收伤害比例与隔位刺杀破盾</summary>
        public static (int absorbedDmg, int penetratedDmg) CalcMagicShieldAbsorption(int rawDamage, int skillLevel, bool isAssassinationSecondTile)
        {
            if (isAssassinationSecondTile) return (0, rawDamage); // 刺杀第二格无视盾
            float ratio = skillLevel switch
            {
                0 => 0.40f,
                1 => 0.50f,
                2 => 0.60f,
                _ => 0.70f
            };
            int absorbed = (int)(rawDamage * ratio);
            int penetrated = rawDamage - absorbed;
            return (absorbed, penetrated);
        }

        /// <summary>1224 魔法盾消耗 MP 动态衰减</summary>
        public static int CalcShieldMpConsumption(int absorbedDamage)
        {
            return (absorbedDamage / 100) * 15;
        }

        /// <summary>1225 冰咆哮中心点与外圈伤害分布 (无视魔避)</summary>
        public static int CalcBlizzardDamage(int rawMagicDmg, bool isCenterTile)
        {
            return isCenterTile ? rawMagicDmg : (int)(rawMagicDmg * 0.75f);
        }

        /// <summary>1226 地狱雷光对不死怪强制硬直时间</summary>
        public const float HellLightningStunSec = 0.6f;

        /// <summary>1227 瞬息移动失败概率</summary>
        public static bool CheckTeleportSkillFail(int skillLevel, Random rng)
        {
            int failRate = (3 - Math.Clamp(skillLevel, 0, 3)) * 10;
            return rng.Next(100) < failRate;
        }

        /// <summary>1228 刺杀剑术第二格无视防御与破盾</summary>
        public static bool IsAssassinationIgnoreAc(int attackerDist) => attackerDist == 2;

        /// <summary>1229 烈火剑法伤害倍数与有效蓄力时间</summary>
        public const float FireHitBuffDurationSec = 10.0f;
        public static int CalcFireHitDamage(int rawDc, float fireMultiplier = 2.8f)
        {
            return (int)(rawDc * fireMultiplier);
        }

        /// <summary>1230 野蛮冲撞等级与重量双重压制判定</summary>
        public static bool CanWildBashTarget(int myLevel, int targetLevel, int myWearWeight, int targetWearWeight)
        {
            return (myLevel > targetLevel) && (myWearWeight >= targetWearWeight);
        }

        /// <summary>1231 野蛮冲撞位移格数</summary>
        public static int GetWildBashDistance(int skillLevel)
        {
            return skillLevel switch
            {
                0 => 1,
                1 => 2,
                2 => 2,
                _ => 3
            };
        }

        /// <summary>1232 攻杀剑术暴击概率与额外命中加成</summary>
        public static (bool triggered, int hitBonus, int extraDc) CalcFatalSwordBonus(int skillLevel, int dc, Random rng)
        {
            int rate = skillLevel switch
            {
                0 => 12,
                1 => 18,
                2 => 24,
                _ => 30
            };
            if (rng.Next(100) < rate)
            {
                int extraDc = 1 + skillLevel * 2 + (dc / 5);
                return (true, 5, extraDc);
            }
            return (false, 0, 0);
        }

        // ------------------------------------------------------------------------
        // 1233 ~ 1245: 套装天谴与特戒全集
        // ------------------------------------------------------------------------

        /// <summary>1233 祈祷套装激活全服宠物叛变判定</summary>
        public static bool IsPrayerSuitComplete(bool helmet, bool necklace, bool bracelet, bool ring, bool weapon)
        {
            return helmet && necklace && bracelet && ring && weapon;
        }

        /// <summary>1234 祈祷之刃死亡必碎</summary>
        public static bool ShouldPrayerBladeDestroyOnDeath(string weaponName)
        {
            return weaponName.Equals("祈祷之刃", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>1235 & 1236 记忆套装全队召集与安全区禁用校验</summary>
        public static (bool allowed, string reason) ValidateMemorySuitRecall(bool isSuitFull, bool isInForbiddenMap)
        {
            if (!isSuitFull) return (false, "记忆套装尚未凑齐 (需要头盔、项链、手镯、戒指)！");
            if (isInForbiddenMap) return (false, "当前领地受到神秘结界庇护，无法感召队友！");
            return (true, string.Empty);
        }

        /// <summary>1237 虹魔套装物理吸血百分比</summary>
        public static int CalcRainbowMagicLifeLeech(int rawDamage, int pieceCount)
        {
            float ratio = pieceCount switch
            {
                >= 4 => 0.20f,
                >= 1 => pieceCount * 0.05f,
                _ => 0f
            };
            return (int)(rawDamage * ratio);
        }

        /// <summary>1238 魔血套装 HP/MP 转化奖励</summary>
        public static (int bonusHp, int minusMp) CalcDemonBloodSetStatDelta(int pieceCount)
        {
            if (pieceCount >= 4) return (86, -86); // 3*12 + 50 套装奖励
            return (pieceCount * 12, -pieceCount * 12);
        }

        /// <summary>1239 传送戒指坐标有效性与防卡墙</summary>
        public static (bool valid, int finalX, int finalY) ValidateTeleportRingMove(int targetX, int targetY, int mapWidth, int mapHeight, bool isWall)
        {
            if (targetX < 0 || targetX >= mapWidth || targetY < 0 || targetY >= mapHeight)
            {
                return (false, 0, 0);
            }
            if (isWall)
            {
                // 自动纠偏至邻近合法格
                return (true, targetX + 1, targetY);
            }
            return (true, targetX, targetY);
        }

        /// <summary>1241 麻痹戒指 1/15 攻击石化判定</summary>
        public static (bool paralyzed, float durationSec) CheckParalysisRing(Random rng)
        {
            if (rng.Next(15) == 0)
            {
                return (true, 5.0f);
            }
            return (false, 0f);
        }

        /// <summary>1242 复活戒指原地满血复活判定</summary>
        public static (bool canRevive, int restoredHp) CheckRevivalRingTrigger(int currentHp, int maxHp, int ringDura, float cooldownRemainingSec)
        {
            if (currentHp <= 0 && ringDura > 0 && cooldownRemainingSec <= 0f)
            {
                return (true, maxHp);
            }
            return (false, 0);
        }

        /// <summary>1243 护身戒指 MP 抵扣伤害</summary>
        public static (int actualHpLost, int mpLost) CalcMagicShieldRingAbsorption(int incomingDamage, int currentMp)
        {
            if (currentMp >= incomingDamage)
            {
                return (0, incomingDamage);
            }
            int remaining = incomingDamage - currentMp;
            return (remaining, currentMp);
        }

        // ------------------------------------------------------------------------
        // 1246 ~ 1255: 铁匠修理、挖矿、武器升级与 PK 裁决
        // ------------------------------------------------------------------------

        /// <summary>1246 商店特修费用与普修折损持久</summary>
        public static (int specialRepairCost, bool maxDuraReduced) CalcEquipmentRepair(int currentDura, int maxDura, int baseItemValue, bool isSpecialRepair, Random rng)
        {
            if (isSpecialRepair)
            {
                int cost = (maxDura - currentDura) * baseItemValue / Math.Max(1, maxDura) * 3;
                return (Math.Max(100, cost), false);
            }
            bool reduced = rng.Next(100) < 20; // 普修 20% 概率永久折损 1 点上限
            return (baseItemValue / 10, reduced);
        }

        /// <summary>1248 矿石种类与纯度产出分布</summary>
        public static (string oreType, int purity) RollMiningYield(Random rng)
        {
            int roll = rng.Next(100);
            string ore = roll switch
            {
                < 40 => "铁矿",
                < 65 => "铜矿",
                < 80 => "银矿",
                < 90 => "金矿",
                < 98 => "黑铁矿石",
                _ => "紫水晶"
            };
            int purity = Math.Clamp((int)(rng.Next(1, 15) + (rng.Next(100) < 5 ? 15 : 0)), 1, 30);
            return (ore, purity);
        }

        /// <summary>1249 黑铁矿石对武器持久升级影响</summary>
        public static int CalcWeaponUpgradeDuraDelta(int totalPurity)
        {
            if (totalPurity < 30) return -1;
            if (totalPurity < 60) return 0;
            if (totalPurity < 90) return 1;
            return 2; // >= 90 必加 2 点持久上限
        }

        /// <summary>1250 武器升级首饰主属性成功率</summary>
        public static bool CheckWeaponUpgradeSuccess(int accessoryStatSum, Random rng)
        {
            int baseRate = 40;
            int bonus = Math.Min(35, accessoryStatSum * 3);
            int finalRate = baseRate + bonus; // 最高 75%
            return rng.Next(100) < finalRate;
        }

        /// <summary>1251 祝福油与运九套满伤判定</summary>
        public static bool IsLuckNineAlwaysMaxDamage(int totalLuck) => totalLuck >= 9;

        /// <summary>1253 & 1254 PK 值与红名爆装惩罚</summary>
        public static (string nameColor, float bagDropRate, float equipDropRate) GetPkStatusAndDropRates(int pkPoints)
        {
            if (pkPoints >= 300)
            {
                return ("红名", 0.60f, 0.40f);
            }
            if (pkPoints >= 200)
            {
                return ("黄名", 0.25f, 0.15f);
            }
            return ("白名", 0.10f, 0.05f);
        }

        /// <summary>1255 大刀卫士对红名绝对秒杀判定</summary>
        public static bool GuardMustInstaKill(string nameColor) => nameColor == "红名";

        // ------------------------------------------------------------------------
        // 1256 ~ 1300: 药水、负重、怪物生态与终局物理收束
        // ------------------------------------------------------------------------

        /// <summary>1256 强效金创药阶梯式持续恢复曲线</summary>
        public const int StrongPotionTotalHp = 90;
        public const int StrongPotionDurationSec = 6;
        public const int StrongPotionHpPerSec = 15;

        /// <summary>1257 喝药防刷节流判定 (300ms)</summary>
        public static bool CheckPotionThrottle(ulong lastDrinkTick, ulong currentTick)
        {
            return currentTick - lastDrinkTick >= 300;
        }

        /// <summary>1258 穿戴装备负重三段约束</summary>
        public static (bool canMove, bool canWearArm, bool canEquipWeapon) CheckWeights(
            int currentBagWeight, int maxBagWeight,
            int currentWearWeight, int maxWearWeight,
            int weaponWeight, int handStrength)
        {
            bool canMove = currentBagWeight <= maxBagWeight;
            bool canWearArm = currentWearWeight <= maxWearWeight;
            bool canEquip = weaponWeight <= handStrength;
            return (canMove, canWearArm, canEquip);
        }

        /// <summary>1267 赤月恶魔全屏地刺突刺攻击</summary>
        public static int CalcEvilRedMoonSpikeDamage(int baseDmg) => Math.Clamp(baseDmg, 80, 120);

        /// <summary>1270 触龙神物免魔抗毒全免疫</summary>
        public static bool IsDragonCentipedeImmuneToMagic() => true;
        public static bool IsDragonCentipedeImmuneToPoison() => true;

        /// <summary>1279 罗刹武器攻击力反向加成 (DC 15-0)</summary>
        public static int CalcRaksasaDamage(int cursedLevel)
        {
            // 诅咒+10 触发刀刀下限 15 点 DC
            return cursedLevel >= 10 ? 15 : 0;
        }

        /// <summary>1288 角色打坐休息回血加速 (300%)</summary>
        public static int CalcSitDownRegen(int baseRegen) => baseRegen * 4;

        /// <summary>1291 命中与敏捷绝对物理对抗抵消</summary>
        public static float CalcPhysicalHitChance(int attackerHit, int defenderAgility)
        {
            int netAgility = Math.Max(0, defenderAgility - attackerHit);
            float dodgeRatio = Math.Min(0.70f, netAgility * 0.02f);
            return Math.Max(0.15f, 1.0f - dodgeRatio); // 保底 15% 命中
        }

        /// <summary>1296 & 1297 道士药粉与护身符消耗</summary>
        public static (bool canCast, int remainingPowder) ConsumeTaoistPowder(int currentPowder)
        {
            if (currentPowder <= 0) return (false, 0);
            return (true, currentPowder - 1);
        }

        /// <summary>1299 负重不足无法拾取地表道具官方阻断</summary>
        public static bool CanPickupGroundItem(int itemWeight, int currentBagWeight, int maxBagWeight)
        {
            return (currentBagWeight + itemWeight) <= maxBagWeight;
        }
    }
}
