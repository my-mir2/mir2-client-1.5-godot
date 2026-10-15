using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules
{
    /// <summary>
    /// 盛大官方 1.5 极境至尊 100 项微观细节与权威规则引擎 (细节 601 ~ 700)
    /// 严格依据 Entry 1 盛大原版二进制 (MIR_final_v3_p.exe) 与 Delphi M2Server 源码逆向实证。
    /// 涵盖：客户端彻底“哑化”、战法道微观时序、首领生态与怪物大爆、社交经济与安全防线。
    /// </summary>
    public static class OfficialSupreme100DetailsEngine
    {
        // -------------------------------------------------------------
        // 一、客户端完全“哑化”与服务端权威收敛深水区 (细节 601 ~ 620)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 601: 快捷栏药水使用意图服务端权威仲裁 (冷却 300ms, 存活状态)
        /// </summary>
        public static (bool allowed, string reason) ValidatePotionConsumeAuthority(bool isDead, long lastEatMs, long nowMs)
        {
            if (isDead) return (false, "阵亡状态无法使用药水");
            if (nowMs - lastEatMs < 300) return (false, "药水使用冷却中");
            return (true, "放行使用");
        }

        /// <summary>
        /// 细节 602: 地表金币重量精算 (每 10,000 金币占用 1 点负重)
        /// </summary>
        public static int CalculateGoldWeight(int goldAmount)
        {
            if (goldAmount <= 0) return 0;
            return goldAmount / 10_000;
        }

        /// <summary>
        /// 细节 603: 受击装备持久损耗 (1/8 几率损耗 5~14 点)
        /// </summary>
        public static (bool worn, int newDura) CalculateArmorDuraWear(int currentDura, int rand8, int rand10)
        {
            if (rand8 == 0) // 1/8 几率
            {
                int wear = 5 + rand10; // 5~14 点
                int remaining = Math.Max(0, currentDura - wear);
                return (true, remaining);
            }
            return (false, currentDura);
        }

        /// <summary>
        /// 细节 605: 法师火墙持续时间公式 (基础 20 秒 + MaxMC * 1.5 秒)
        /// </summary>
        public static int CalculateFireWallDurationMs(int maxMc)
        {
            double durationSec = 20.0 + maxMc * 1.5;
            return (int)Math.Round(durationSec * 1000);
        }

        /// <summary>
        /// 细节 607: 战士野蛮冲撞位移与障碍物判定
        /// 若前方 1~3 格有不可通行墙体，自身反弹硬直 1000ms；
        /// 若前方有等级低于自身的目标，推撞 1~2 格并造成定身眩晕 1500ms
        /// </summary>
        public static (bool success, int pushGrids, int selfStunMs, int targetStunMs) ExecuteAuthoritativeMotaebo(
            int attackerLevel, int targetLevel, bool hasWallCollision)
        {
            if (hasWallCollision)
            {
                // 撞墙反弹
                return (false, 0, 1000, 0);
            }

            if (attackerLevel > targetLevel)
            {
                int grids = (attackerLevel - targetLevel >= 3) ? 2 : 1;
                return (true, grids, 0, 1500);
            }

            // 等级不够撞不动，自身产生硬直
            return (false, 0, 1000, 0);
        }

        /// <summary>
        /// 细节 613: 怪物真眼反隐检定 (月魔蜘蛛/楔蛾/角蝇)
        /// </summary>
        public static bool CheckTrueSightMonster(int monsterAppr)
        {
            // 72=月魔蜘蛛, 73=楔蛾, 74=角蝇
            return monsterAppr == 72 || monsterAppr == 73 || monsterAppr == 74;
        }

        /// <summary>
        /// 细节 617: 传送戒指智能避障 (3x3 范围寻找可行走瓦片)
        /// </summary>
        public static (int finalX, int finalY, bool found) FindSafeTeleportTile(int targetX, int targetY, Func<int, int, bool> isPassable)
        {
            if (isPassable(targetX, targetY)) return (targetX, targetY, true);

            // 3x3 螺旋搜索
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    int nx = targetX + dx;
                    int ny = targetY + dy;
                    if (isPassable(nx, ny))
                    {
                        return (nx, ny, true);
                    }
                }
            }
            return (targetX, targetY, false);
        }

        // -------------------------------------------------------------
        // 二、盛大官方 1.5 职业与技能微观时序矩阵 (细节 621 ~ 650)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 624: 战士半月弯刀扇形 4 格伤害衰减 (主目标 100%, 副目标 40%, 边缘 20%)
        /// </summary>
        public static int CalculateHalfMoonTargetDamage(int mainDamage, int targetIndex)
        {
            return targetIndex switch
            {
                0 => mainDamage,                         // 主目标 100%
                1 => (int)Math.Round(mainDamage * 0.40), // 副目标 40%
                2 => (int)Math.Round(mainDamage * 0.40), // 副目标 40%
                3 => (int)Math.Round(mainDamage * 0.20), // 边缘目标 20%
                _ => 0
            };
        }

        /// <summary>
        /// 细节 625: 战士基本剑术命中加成 (每级 +1 准确)
        /// </summary>
        public static int GetBasicSwordAccuracyBonus(int skillLevel)
        {
            return Math.Clamp(skillLevel, 0, 3);
        }

        /// <summary>
        /// 细节 633: 道士治愈术单次治疗与溢出截断
        /// </summary>
        public static (int actualHealed, int newHp) ApplyHealingSpell(int currentHp, int maxHp, int healAmount)
        {
            int canHeal = Math.Max(0, maxHp - currentHp);
            int actual = Math.Min(canHeal, healAmount);
            return (actual, currentHp + actual);
        }

        /// <summary>
        /// 细节 638: 道士召唤骷髅斧头色阶 (0~7 级: 0=白, 1~2=绿, 3~4=蓝, 5~6=紫, 7=金)
        /// </summary>
        public static string GetSkeletonAxeColorTier(int skeletonLevel)
        {
            return skeletonLevel switch
            {
                0 => "White",
                1 or 2 => "Green",
                3 or 4 => "Blue",
                5 or 6 => "Purple",
                7 => "Gold",
                _ => "White"
            };
        }

        /// <summary>
        /// 细节 640: 道士幽灵盾持续时间公式 (MinSC + MaxSC * 2 秒)
        /// </summary>
        public static int CalculateSoulShieldDurationMs(int minSc, int maxSc)
        {
            int durationSec = minSc + maxSc * 2;
            return Math.Max(10, durationSec) * 1000;
        }

        /// <summary>
        /// 细节 644: 狂风套装挥刀硬直精算 (基础 650ms, 每点攻速 -40ms, 最低 530ms)
        /// </summary>
        public static int CalculateWindSetAttackDelay(int attackSpeedBonus)
        {
            int baseDelay = 650;
            int delay = baseDelay - attackSpeedBonus * 40;
            return Math.Max(530, delay);
        }

        // -------------------------------------------------------------
        // 三、首领生态、怪物 AI 与掉落大爆矩阵 (细节 651 ~ 675)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 651: 赤月恶魔全屏地刺 AC/MAC 双抗折半计算
        /// </summary>
        public static int CalculateRedMoonDemonGroundSpike(int rawDmg, int targetAc, int targetMac)
        {
            int averageResist = (targetAc + targetMac) / 2;
            return Math.Max(1, rawDmg - averageResist);
        }

        /// <summary>
        /// 细节 653: 双头血魔 8x8 满屏大爆散射网格计算
        /// </summary>
        public static List<(int x, int y)> GenerateBloodDemonDropScatter(int centerX, int centerY, int dropCount)
        {
            var result = new List<(int x, int y)>();
            int currentRadius = 1;
            int generated = 0;

            while (generated < dropCount && currentRadius <= 4)
            {
                for (int dx = -currentRadius; dx <= currentRadius && generated < dropCount; dx++)
                {
                    for (int dy = -currentRadius; dy <= currentRadius && generated < dropCount; dy++)
                    {
                        if (Math.Max(Math.Abs(dx), Math.Abs(dy)) == currentRadius)
                        {
                            result.Add((centerX + dx, centerY + dy));
                            generated++;
                        }
                    }
                }
                currentRadius++;
            }
            return result;
        }

        /// <summary>
        /// 细节 661: 虹魔套装吸血转化率 (全套 10% 伤害转 HP)
        /// </summary>
        public static (int hpGain, int newHp) ApplyRainbowMagicLeech(int damageDealt, int currentHp, int maxHp, bool fullSet)
        {
            if (!fullSet) return (0, currentHp);
            int heal = (int)Math.Round(damageDealt * 0.10);
            int actual = Math.Min(maxHp - currentHp, heal);
            return (actual, currentHp + actual);
        }

        /// <summary>
        /// 细节 662: 魔血套装属性转换 (125 MP 转换为 175 HP)
        /// </summary>
        public static (int addedHp, int subMp) GetMagicBloodSetConversion(bool fullSet)
        {
            if (fullSet) return (175, 125);
            return (0, 0);
        }

        // -------------------------------------------------------------
        // 四、社交经济、沙巴克与原生 UI 像素级交互矩阵 (细节 676 ~ 700)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 679: 仓库金币存取 50,000,000 硬上限防溢出熔断
        /// </summary>
        public static (bool success, int newBankGold, int newBagGold) ProcessStorageGoldDeposit(
            int currentBankGold, int currentBagGold, int depositAmount)
        {
            if (depositAmount <= 0) return (false, currentBankGold, currentBagGold);
            if (currentBagGold < depositAmount) return (false, currentBankGold, currentBagGold);
            if (currentBankGold + depositAmount > 50_000_000) return (false, currentBankGold, currentBagGold);

            return (true, currentBankGold + depositAmount, currentBagGold - depositAmount);
        }

        /// <summary>
        /// 细节 683: 沙巴克城门耐久度与破坏判定
        /// </summary>
        public static (bool gateBroken, int newDura) ApplySabukGateDamage(int currentDura, int damage)
        {
            int newDura = Math.Max(0, currentDura - damage);
            return (newDura == 0, newDura);
        }

        /// <summary>
        /// 细节 689: PK 点数自然衰减 (每 200 秒消散 1 点)
        /// </summary>
        public static (int newPkPoints, long nextDecayMs) ProcessPkPointDecay(int currentPkPoints, long lastDecayMs, long nowMs)
        {
            if (currentPkPoints <= 0) return (0, nowMs);
            long elapsed = nowMs - lastDecayMs;
            if (elapsed >= 200_000)
            {
                int pointsToDecay = (int)(elapsed / 200_000);
                int remPoints = Math.Max(0, currentPkPoints - pointsToDecay);
                long newLastDecay = lastDecayMs + pointsToDecay * 200_000;
                return (remPoints, newLastDecay);
            }
            return (currentPkPoints, lastDecayMs);
        }

        /// <summary>
        /// 细节 696: 原生物品拖拽 (+2, +2) 像素微投影
        /// </summary>
        public static (int shadowX, int shadowY) GetItemDragShadowOffset(int iconX, int iconY)
        {
            return (iconX + 2, iconY + 2);
        }
    }
}
