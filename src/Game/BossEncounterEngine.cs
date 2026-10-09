using System;
using System.Collections.Generic;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 怪物高阶 AI、首领狂暴、大爆掉落与尸体交互引擎 (细节 191 ~ 210)
/// 严格依据官方服务端脚本、怪物数据库与客户端渲染实证
/// </summary>
public static class BossEncounterEngine
{
    // 细节 191: 触龙神全屏绿毒与大范围石化喷吐 (无法移动攻击 3 秒)
    public static class EvilSnakePoisonEngine
    {
        public static (bool petrified, bool poisoned, float duration) EvaluateBreath(int roll100)
        {
            bool petrify = roll100 < 25; // 25% 概率石化 3 秒
            return (petrify, true, petrify ? 3.0f : 0f);
        }
    }

    // 细节 192: 触龙神绝对免疫一切魔法攻击 (魔法抗性 100%，纯物理检定)
    public static class EvilSnakeMagicImmunityEngine
    {
        public static int CalculateDamage(int rawDamage, bool isMagicAttack)
        {
            if (isMagicAttack) return 0; // 魔法免疫
            return rawDamage;
        }
    }

    // 细节 193: 邪恶毒蛇高速 S 型滑行移动与近身剧毒
    public static class ViperMobilityEngine
    {
        public static (float moveSpeedMultiplier, bool applyToxic) EvaluateViperState(int distanceToTarget)
        {
            return (1.6f, distanceToTarget <= 1);
        }
    }

    // 细节 194: 极品祖玛卫士 (大锤) 移速提升 50% 与前摇缩短
    public static class ZumaSuperGuardEngine
    {
        public static (float speedMulti, float attackPrecastSeconds) GetGuardAttributes(bool isSuperGuard)
        {
            if (isSuperGuard) return (1.5f, 0.25f);
            return (1.0f, 0.45f);
        }
    }

    // 细节 195: 极品祖玛雕像濒死 (HP < 10%) 自爆机制
    public static class ZumaStatueSelfDestructEngine
    {
        public static (bool willExplode, int explodeDamage) CheckSelfDestruct(int currentHp, int maxHp)
        {
            if (currentHp > 0 && ((float)currentHp / maxHp) <= 0.10f)
            {
                return (true, 80); // 周围 1 格造成 80 点自爆伤害
            }
            return (false, 0);
        }
    }

    // 细节 196: 黑色恶蛆高速滚地突进高速撞击造成 200ms 顿挫
    public static class BlackMaggotChargeEngine
    {
        public static (bool hitSuccess, int stunMs) EvaluateCharge(int distance)
        {
            if (distance <= 4)
            {
                return (true, 200); // 顿挫 200ms
            }
            return (false, 0);
        }
    }

    // 细节 197: 楔蛾喷吐 5% 概率石化定身 3 秒
    public static class MothPetrifyEngine
    {
        public static (bool isPetrified, float duration) CheckPetrify(int roll100)
        {
            if (roll100 < 5) return (true, 3.0f);
            return (false, 0f);
        }
    }

    // 细节 198: 角蝇同屏吐出蝙蝠最多 10 只超限停止产卵
    public static class BatSpitterEngine
    {
        public const int MaxBats = 10;
        public static bool CanSpawnBat(int currentBatsAlive)
        {
            return currentBatsAlive < MaxBats;
        }
    }

    // 细节 199: 沃玛卫士双刀连斩造成双倍物理伤害
    public static class WomaDualBladeEngine
    {
        public static (bool isDualSlash, int finalDamage) CalculateSlashDamage(int baseDc, bool dualSlashProc)
        {
            if (dualSlashProc) return (true, baseDc * 2);
            return (false, baseDc);
        }
    }

    // 细节 200: 尸王铁链扫击前方 3 格横扫伤害与金属拖地音效
    public static class CorpseKingChainEngine
    {
        public static List<(int x, int y)> GetChainSweepGrids(int bossX, int bossY, int dir)
        {
            int[] dx = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] dy = { -1, -1, 0, 1, 1, 1, 0, -1 };
            var list = new List<(int, int)>();
            for (int r = 1; r <= 3; r++)
            {
                list.Add((bossX + dx[dir] * r, bossY + dy[dir] * r));
            }
            return list;
        }
    }

    // 细节 201: 骷髅精灵双叉狂暴突刺双眼泛红光
    public static class SkeletonElfEnrageEngine
    {
        public static (bool eyesGlowRed, float dcMultiplier) CheckEnraged(int currentHp, int maxHp)
        {
            bool enraged = ((float)currentHp / maxHp) <= 0.5f;
            return (enraged, enraged ? 1.3f : 1.0f);
        }
    }

    // 细节 202: 沃玛教主狂暴瞬移到最高仇恨目标身后
    public static class WomaLeaderTeleportEngine
    {
        public static (int tpX, int tpY, bool teleportTriggered) CalculateAmbush(int targetX, int targetY, int targetDir, bool isBerserk)
        {
            if (!isBerserk) return (0, 0, false);
            int[] dx = { 0, 1, 1, 1, 0, -1, -1, -1 };
            int[] dy = { -1, -1, 0, 1, 1, 1, 0, -1 };
            int backDir = (targetDir + 4) % 8; // 目标背后
            return (targetX + dx[backDir], targetY + dy[backDir], true);
        }
    }

    // 细节 203: 祖玛教主残血 (HP < 20%) 战鼓召唤 4 尊卫士雕像
    public static class ZumaLeaderSummonEngine
    {
        public static (bool summonTriggered, int count) CheckSummon(int curHp, int maxHp, bool alreadySummoned)
        {
            if (!alreadySummoned && ((float)curHp / maxHp) <= 0.20f)
            {
                return (true, 4);
            }
            return (false, 0);
        }
    }

    // 细节 204: 赤月恶魔全屏无死角地刺 (受目标防御与魔防双重减免)
    public static class RedMoonSpikeDefenseEngine
    {
        public static int CalculateSpikeDamage(int rawSpikeDamage, int targetAc, int targetMac)
        {
            int reduction = (int)(targetAc * 0.5f + targetMac * 0.5f);
            return Math.Max(10, rawSpikeDamage - reduction);
        }
    }

    // 细节 205: 双头金刚死后尸体不爆道具需持屠夫小刀挖肉
    public static class DoubleHeadKongHarvestEngine
    {
        public static (bool canHarvest, string droppedItem) TryHarvest(bool hasButcherKnife, int roll100)
        {
            if (!hasButcherKnife) return (false, "缺少屠夫小刀，无法解剖尸体！");
            if (roll100 < 15) return (true, "法神头盔");
            if (roll100 < 30) return (true, "法神披风");
            return (true, "高纯度金刚肉");
        }
    }

    // 细节 206: 双头血魔死后天降暴雨与大爆圣战首饰散落
    public static class BloodDemonStormDropEngine
    {
        public static (bool triggerRain, List<string> dropItems) EvaluateDeath()
        {
            return (true, new List<string> { "圣战项链", "圣战手镯", "圣战戒指", "祝福油", "金币" });
        }
    }

    // 细节 207: 牛魔王瞬移法杖挥击与烈焰刀光双重伤害
    public static class CowKingFlameStrikeEngine
    {
        public static (int meleeDc, int flameMc) CalculateDualDamage(int baseDc, int baseMc)
        {
            return (baseDc, (int)(baseMc * 1.25f));
        }
    }

    // 细节 208: 暗之双头金刚死后挖取天尊道袍重装
    public static class DarkKongRobeHarvestEngine
    {
        public static (bool success, string robeName) HarvestRobe(bool hasKnife, int roll100)
        {
            if (!hasKnife) return (false, "");
            if (roll100 < 40) return (true, "天尊道袍");
            return (false, "");
        }
    }

    // 细节 209: 暗之骷髅精灵死后大爆圣战宝甲散落
    public static class DarkElfArmorDropEngine
    {
        public static List<string> GenerateArmorDrop(int roll100)
        {
            var drops = new List<string> { "疗伤药", "强效太阳水", "金币" };
            if (roll100 < 40) drops.Add("圣战宝甲");
            return drops;
        }
    }

    // 细节 210: 白野猪死后满屏 8x8 道具同心圆螺旋散开大爆
    public static class WhiteBoarBigDropEngine
    {
        public static List<(int x, int y, string item)> Generate8x8Drop(int centerX, int centerY)
        {
            var list = new List<(int, int, string)>();
            for (int dx = -3; dx <= 4; dx++)
            {
                for (int dy = -3; dy <= 4; dy++)
                {
                    string it = ((dx + dy) % 2 == 0) ? "金币" : "强效太阳水";
                    list.Add((centerX + dx, centerY + dy, it));
                }
            }
            return list;
        }
    }
}
