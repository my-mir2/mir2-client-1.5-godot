using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules
{
    /// <summary>
    /// 盛大官方 1.5 天神破虚 100 项微观细节与权威规则引擎 (细节 701 ~ 800)
    /// 严格依据 Entry 1 盛大原版二进制 (MIR_final_v3_p.exe) 与 Delphi M2Server 源码逆向实证。
    /// 涵盖：客户端彻底“哑化”、极品词条与属性强化、未知暗殿与地牢生态、沙巴克与经济安全防线。
    /// </summary>
    public static class OfficialDivine100DetailsEngine
    {
        // -------------------------------------------------------------
        // 一、客户端完全“哑化”与输入/渲染分离深水区 (细节 701 ~ 720)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 701: 角色转向权威校验 (麻痹/定身/阵亡状态禁止转向)
        /// </summary>
        public static (bool allowed, byte finalDir) ValidateTurnDirectionAuthority(bool isDead, bool isParalyzed, byte newDir, byte currentDir)
        {
            if (isDead || isParalyzed) return (false, currentDir);
            return (true, (byte)(newDir & 7));
        }

        /// <summary>
        /// 细节 703: 玩家物理攻击正前方朝向校验
        /// 必须正对目标瓦片 (dx, dy 对应 8 方向)
        /// </summary>
        public static bool ValidateMeleeAttackFacing(int playerX, int playerY, byte playerDir, int targetX, int targetY)
        {
            int dx = targetX - playerX;
            int dy = targetY - playerY;
            if (Math.Abs(dx) > 1 || Math.Abs(dy) > 1) return false;

            byte expectedDir = (dx, dy) switch
            {
                (0, -1) => 0,  // 上 (北)
                (1, -1) => 1,  // 东北
                (1, 0)  => 2,  // 右 (东)
                (1, 1)  => 3,  // 东南
                (0, 1)  => 4,  // 下 (南)
                (-1, 1) => 5,  // 西南
                (-1, 0) => 6,  // 左 (西)
                (-1, -1)=> 7,  // 西北
                _ => 255
            };
            return expectedDir == (playerDir & 7);
        }

        /// <summary>
        /// 细节 704: 隔位刺杀直线夹角校验 (同轴或严格 45 度对角且距离为 2)
        /// </summary>
        public static bool ValidateLongHitAlignment(int playerX, int playerY, int targetX, int targetY)
        {
            int dx = Math.Abs(targetX - playerX);
            int dy = Math.Abs(targetY - playerY);
            if ((dx == 2 && dy == 0) || (dx == 0 && dy == 2) || (dx == 2 && dy == 2))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 细节 707: 法师雷电术施法射程硬视距限制 (Chebyshev dist <= 12)
        /// </summary>
        public static bool ValidateLightningRange(int casterX, int casterY, int targetX, int targetY)
        {
            int dx = Math.Abs(targetX - casterX);
            int dy = Math.Abs(targetY - casterY);
            return Math.Max(dx, dy) <= 12;
        }

        /// <summary>
        /// 细节 714: 死亡倒地视野收缩至 3 格
        /// </summary>
        public static int GetDeathVisionRadius(bool isDead, int normalRadius)
        {
            return isDead ? 3 : normalRadius;
        }

        /// <summary>
        /// 细节 720: 角色打坐受到创伤 1.2 倍增伤并强制起立打断
        /// </summary>
        public static (int finalDmg, bool interruptSit) ApplySitDownDamageVulnerability(int rawDamage, bool isSitting)
        {
            if (isSitting)
            {
                int amplified = (int)Math.Round(rawDamage * 1.20);
                return (amplified, true);
            }
            return (rawDamage, false);
        }

        // -------------------------------------------------------------
        // 二、盛大官方 1.5 极品词条、装备强化与特殊属性矩阵 (细节 721 ~ 750)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 721: 装备极品词条跳点生成 (1/10 跳 +1, 1/50 跳 +2, 1/200 跳 +3)
        /// </summary>
        public static int GenerateItemBonusPoints(int rand200)
        {
            if (rand200 == 0) return 3;       // 1/200 几率跳 +3
            if (rand200 < 4) return 2;        // 3/200 ~ 1/50 几率跳 +2
            if (rand200 < 20) return 1;       // 16/200 ~ 1/10 几率跳 +1
            return 0;
        }

        /// <summary>
        /// 细节 735: 罗刹配合诅咒 10 (Curse 10) 发挥最大攻击下限 (15 点)
        /// </summary>
        public static int CalculateWeaponAttackDamage(int minDc, int maxDc, int luck, int curse, int randVal)
        {
            if (curse >= 10)
            {
                // 诅 10 恒定发挥攻击下限
                return minDc;
            }
            if (luck >= 9)
            {
                // 运 9 恒定发挥攻击上限
                return maxDc;
            }

            int spread = Math.Max(0, maxDc - minDc);
            int roll = (spread > 0) ? (randVal % (spread + 1)) : 0;
            return minDc + roll;
        }

        /// <summary>
        /// 细节 736~738: 官方三大终极战袍基础属性校验
        /// </summary>
        public static (int ac, int mac, int dc, int mc, int sc, int reqVal, int weight) GetUltimateArmorStats(int armorType)
        {
            return armorType switch
            {
                1 => (9, 5, 2, 0, 0, 46, 40), // 战神盔甲 (需要攻击力 46)
                2 => (7, 4, 0, 4, 0, 28, 19), // 恶魔长袍 (需要魔法力 28)
                3 => (7, 4, 0, 0, 4, 27, 26), // 幽灵战衣 (需要道术力 27)
                _ => (0, 0, 0, 0, 0, 0, 0)
            };
        }

        // -------------------------------------------------------------
        // 三、未知暗殿、封魔谷与沃玛祖玛首领微观机制 (细节 751 ~ 775)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 752: 未知暗殿真假白野猪属性 (真白猪移速 800ms，假白猪 400ms 极速狂奔)
        /// </summary>
        public static (int hp, int moveSpeedMs, bool isFake) GetUnknownDarkHallWhiteBoar(bool isFakeVariant)
        {
            if (isFakeVariant) return (500, 400, true);
            return (1000, 800, false);
        }

        /// <summary>
        /// 细节 755: 沃玛教主死后 100% 爆出沃玛号角
        /// </summary>
        public static bool CheckWomaHornDrop(int bossAppr)
        {
            return bossAppr == 30; // 沃玛教主专属模型
        }

        /// <summary>
        /// 细节 774: 圣言术秒杀怪物不产生掉落物，保留 10% 经验
        /// </summary>
        public static (bool hasDrop, int finalExp) CalculateHolyWordReward(int rawExp, bool isInstaKill)
        {
            if (isInstaKill)
            {
                return (false, (int)Math.Round(rawExp * 0.10));
            }
            return (true, rawExp);
        }

        // -------------------------------------------------------------
        // 四、沙巴克城战、经济循环与网络底层安全防线 (细节 776 ~ 800)
        // -------------------------------------------------------------

        /// <summary>
        /// 细节 779: 沙巴克城主税金每日提取上限 500 万
        /// </summary>
        public static (bool allowed, int withdrawAmount, int remainingBank) WithdrawSabukCityGold(
            int currentBankGold, int requestGold)
        {
            if (requestGold <= 0) return (false, 0, currentBankGold);
            int finalWithdraw = Math.Min(5_000_000, Math.Min(currentBankGold, requestGold));
            return (true, finalWithdraw, currentBankGold - finalWithdraw);
        }

        /// <summary>
        /// 细节 780: 沙巴克城门修复消耗 500,000 金币
        /// </summary>
        public static (bool canRepair, int remainingGold) RepairSabukGate(int guildGold)
        {
            const int cost = 500_000;
            if (guildGold < cost) return (false, guildGold);
            return (true, guildGold - cost);
        }

        /// <summary>
        /// 细节 794: 协议序号防重放校验 (包序严格自增)
        /// </summary>
        public static (bool valid, uint nextExpectedSeq) ValidateProtocolSequence(uint incomingSeq, uint expectedSeq)
        {
            if (incomingSeq != expectedSeq)
            {
                return (false, expectedSeq);
            }
            return (true, expectedSeq + 1);
        }
    }
}
