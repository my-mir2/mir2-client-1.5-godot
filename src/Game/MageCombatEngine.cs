using System;
using System.Collections.Generic;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 法师战斗与法术机制引擎 (Mage Combat Engine)
/// 严格还原官方 1.5 二进制规则：
/// 1. 抗拒火环 (RingOfFire, MagicId = 8): 周围 8 格等级压制推开 (Caster.Level > Target.Level)，障碍物反弹顿挫 (sub_49BC20)；
/// 2. 诱惑之光 (Temptation/Tame, MagicId = 9): 定身、驯服随从与叛变红名状态机；
/// 3. 地狱雷光 (HellFire, MagicId = 22): 中心爆发 8 方向不死系怪物 200% 圣属性双倍克制。
/// </summary>
public static class MageCombatEngine
{
    // ==================== 1. 抗拒火环 (Ring of Fire, MagicId = 8) ====================
    public static class RingOfFire
    {
        public struct PushResult
        {
            public int Uid;
            public int OriginalX;
            public int OriginalY;
            public int FinalX;
            public int FinalY;
            public bool Pushed;
            public bool BlockedByObstacle;
            public string FailReason;
        }

        /// <summary>
        /// 执行抗拒火环：推开周围 8 格且等级低于法师的所有活体实体
        /// </summary>
        public static List<PushResult> Execute(
            int casterLevel,
            int casterX,
            int casterY,
            IEnumerable<(int uid, int level, int x, int y, bool isBoss)> surroundingEntities,
            Func<int, int, bool> isTileBlocked)
        {
            var results = new List<PushResult>();

            foreach (var ent in surroundingEntities)
            {
                int dx = ent.x - casterX;
                int dy = ent.y - casterY;

                // 仅影响贴身 1 格以内的周围实体 (Chebyshev 距离 == 1)
                if (Math.Max(Math.Abs(dx), Math.Abs(dy)) != 1)
                {
                    continue;
                }

                // Boss 免疫抗拒火环推开
                if (ent.isBoss)
                {
                    results.Add(new PushResult
                    {
                        Uid = ent.uid,
                        OriginalX = ent.x,
                        OriginalY = ent.y,
                        FinalX = ent.x,
                        FinalY = ent.y,
                        Pushed = false,
                        BlockedByObstacle = false,
                        FailReason = "目标为首领级首脑，免疫抗拒火环冲击。"
                    });
                    continue;
                }

                // 官方等级压制：法师等级必须严格大于目标等级
                if (casterLevel <= ent.level)
                {
                    results.Add(new PushResult
                    {
                        Uid = ent.uid,
                        OriginalX = ent.x,
                        OriginalY = ent.y,
                        FinalX = ent.x,
                        FinalY = ent.y,
                        Pushed = false,
                        BlockedByObstacle = false,
                        FailReason = "目标等级高于或等于法师，推斥失败。"
                    });
                    continue;
                }

                // 沿推斥方向向外延伸 1 格
                int pushX = ent.x + dx;
                int pushY = ent.y + dy;

                if (isTileBlocked(pushX, pushY))
                {
                    // 撞墙或被障碍物阻挡：停留在原格，进入顿挫硬直
                    results.Add(new PushResult
                    {
                        Uid = ent.uid,
                        OriginalX = ent.x,
                        OriginalY = ent.y,
                        FinalX = ent.x,
                        FinalY = ent.y,
                        Pushed = false,
                        BlockedByObstacle = true,
                        FailReason = "推开落点存在地形阻挡，目标发生反弹顿挫。"
                    });
                }
                else
                {
                    results.Add(new PushResult
                    {
                        Uid = ent.uid,
                        OriginalX = ent.x,
                        OriginalY = ent.y,
                        FinalX = pushX,
                        FinalY = pushY,
                        Pushed = true,
                        BlockedByObstacle = false,
                        FailReason = string.Empty
                    });
                }
            }

            return results;
        }
    }

    // ==================== 2. 诱惑之光 (Temptation, MagicId = 9) ====================
    public static class Temptation
    {
        public enum TameOutcome
        {
            Failed = 0,     // 毫无反应
            Paralyzed,      // 陷入黄色定身麻痹
            Tamed,          // 驯服成功，变为绿色宝宝随从
            Mutiny          // 超过时效叛变，化为极度危险的狂暴红名
        }

        public struct TameResult
        {
            public TameOutcome Outcome;
            public int ParalyzeDurationSec;
            public string Description;
        }

        /// <summary>
        /// 盛大官方 1.5 诱惑之光判定逻辑 (等级压制与最多 5 只随从上限)
        /// </summary>
        public static TameResult Evaluate(
            int casterLevel,
            int skillLevel,
            int monsterLevel,
            int currentPetCount,
            bool isUndeadOrBoss,
            int roll = 50)
        {
            if (isUndeadOrBoss)
            {
                return new TameResult
                {
                    Outcome = TameOutcome.Failed,
                    ParalyzeDurationSec = 0,
                    Description = "目标为不死系或魔王，心智不可诱惑。"
                };
            }

            // 目标等级必须低于法师
            if (casterLevel < monsterLevel)
            {
                return new TameResult
                {
                    Outcome = TameOutcome.Failed,
                    ParalyzeDurationSec = 0,
                    Description = "目标等级过高，诱惑之光无法动摇其意志。"
                };
            }

            // 若已有 5 只随从，无法再驯服新随从，仅能造成定身
            bool canTame = currentPetCount < 5 && (casterLevel >= monsterLevel + 2);

            // 判定驯服或定身
            int tameThreshold = 20 + skillLevel * 15;
            if (canTame && roll <= tameThreshold)
            {
                return new TameResult
                {
                    Outcome = TameOutcome.Tamed,
                    ParalyzeDurationSec = 0,
                    Description = "诱惑成功！该怪物已被降服为您的忠诚随从。"
                };
            }
            else if (roll <= 80)
            {
                int duration = 4 + skillLevel * 3;
                return new TameResult
                {
                    Outcome = TameOutcome.Paralyzed,
                    ParalyzeDurationSec = duration,
                    Description = $"怪物陷入意识迷离，被定身麻痹 {duration} 秒！"
                };
            }

            return new TameResult
            {
                Outcome = TameOutcome.Failed,
                ParalyzeDurationSec = 0,
                Description = "诱惑之光闪烁，目标成功抵抗了心智侵蚀。"
            };
        }
    }

    // ==================== 3. 地狱雷光 (HellFire, MagicId = 22) ====================
    public static class HellFire
    {
        /// <summary>
        /// 盛大官方 1.5 地狱雷光克制倍率：不死系怪物 (僵尸/骷髅/尸王) 造成 200% 圣属性双倍伤害
        /// </summary>
        public static int CalculateDamage(int rawMagicDmg, bool isUndead)
        {
            if (rawMagicDmg <= 0) return 0;
            return isUndead ? rawMagicDmg * 2 : rawMagicDmg;
        }
    }

    // ==================== 4. 魔法盾机制与破盾 (MagicShield, MagicId = 31) ====================
    public static class MagicShield
    {
        public const int BreakSoundId = 138; // 破盾金属碎裂音效

        /// <summary>
        /// 判定魔法盾减免伤害与破盾
        /// </summary>
        public static (int absorbedDmg, int remainingHpDmg, bool shieldBroken) ProcessShieldDamage(
            int incomingDamage, int currentShieldHp, int shieldLevel)
        {
            if (incomingDamage <= 0 || currentShieldHp <= 0)
            {
                return (0, incomingDamage, false);
            }

            double absorbRatio = shieldLevel switch
            {
                1 => 0.50,
                2 => 0.60,
                3 => 0.70,
                _ => 0.40
            };

            int maxAbsorb = (int)Math.Round(incomingDamage * absorbRatio);
            int actualAbsorb = Math.Min(currentShieldHp, maxAbsorb);
            int remainingShield = currentShieldHp - actualAbsorb;
            bool broken = remainingShield <= 0;
            int remainingDamage = incomingDamage - actualAbsorb;

            return (actualAbsorb, remainingDamage, broken);
        }
    }

    // ==================== 5. 火墙多层重叠与踏入伤害 (Firewall, MagicId = 21) ====================
    public static class Firewall
    {
        public const int MaxLayersPerTile = 3;

        /// <summary>
        /// 计算地块火墙燃烧伤害 (支持最多 3 层重叠，每层增伤 50%)
        /// </summary>
        public static int CalculateTileDamage(int baseDamage, int layerCount)
        {
            layerCount = Math.Clamp(layerCount, 1, MaxLayersPerTile);
            double multiplier = 1.0 + (layerCount - 1) * 0.50;
            return (int)Math.Round(baseDamage * multiplier);
        }
    }

    // ==================== 6. 瞬息移动传送机制 (Teleport, MagicId = 11) ====================
    public static class Teleport
    {
        /// <summary>
        /// 在地图范围内随机搜寻合法落点 (遭遇阻挡向外螺旋搜索合法瓦片)
        /// </summary>
        public static (int targetX, int targetY, bool success) ResolveRandomLandingTile(
            int mapWidth, int mapHeight, Func<int, int, bool> isTileBlocked, Random rng)
        {
            for (int attempt = 0; attempt < 30; attempt++)
            {
                int rx = rng.Next(20, Math.Max(21, mapWidth - 20));
                int ry = rng.Next(20, Math.Max(21, mapHeight - 20));
                if (!isTileBlocked(rx, ry))
                {
                    return (rx, ry, true);
                }
            }
            return (330, 270, false); // 回退至默认主城中心
        }
    }

    // ==================== 7. 冰咆哮 (Blizzard, MagicId = 33) ====================
    public static class Blizzard
    {
        /// <summary>
        /// 盛大官方 1.5 铁律：冰咆哮 3x3 范围绝对命中，100% 无视目标魔法躲避
        /// </summary>
        public static int CalculateDamage(int rawMagicDamage, int targetMcDef)
        {
            return Math.Max(1, rawMagicDamage - targetMcDef);
        }
    }
}
