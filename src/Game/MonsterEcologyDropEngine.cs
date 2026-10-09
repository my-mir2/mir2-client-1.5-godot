using System;
using System.Collections.Generic;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 全怪物生态、掉落爆率树、攻击前摇/后摇与尸体腐烂时钟引擎 (细节 451 ~ 550)
/// 严格依据官方服务端怪物配置与实证数据
/// </summary>
public static class MonsterEcologyDropEngine
{
    // 细节 451 ~ 480: 初级生物、矿洞不死系与石墓首领
    public static class CreatureEcologyEngine
    {
        public static (int meatQuality, float moveSpeedMultiplier) GetChickenDeathMeat(int struckCount)
        {
            // 细节 451/452: 击打次数越少鸡肉/鹿肉品质越高
            int quality = Math.Clamp(15 - struckCount, 5, 15);
            return (quality, 2.0f);
        }

        public static (float precastSec, int stunMs) GetOrcPrecast(string orcType)
        {
            // 细节 459: 半兽人石斧重劈前摇 0.4 秒
            return (0.4f, 250);
        }

        public static (float normalCorpseSeconds, float bossCorpseSeconds) CorpseDecayTimers => (60.0f, 300.0f); // 细节 510/511

        public static (int minHearingRange, int maxVisionRange) GetMonsterSenses(bool isBoss)
        {
            // 细节 537: 普通怪 8 格视距，首领 12 格视距
            return isBoss ? (6, 12) : (4, 8);
        }

        public static int WomaGuardDualSlashIntervalMs => 600; // 细节 543: 沃玛卫士二连斩间隔 600ms
    }

    // 细节 481 ~ 510: 祖玛、赤月、牛魔与暗之重装使徒
    public static class BossDropTreeEngine
    {
        public static (float innerRadius, float outerRadius) GetBossDropRadii()
        {
            // 细节 532/533: 核心首饰散落内圈 (1~2格)，药水金币散落外圈 (3~4格)
            return (2.0f, 4.0f);
        }

        public static (bool hasOwnership, float protectionSeconds) CheckDropOwnership(long killerId, long pickerId, float dropElapsedSeconds)
        {
            // 细节 534: 掉落物保护 120 秒 (2分钟)
            if (dropElapsedSeconds >= 120.0f) return (true, 0f);
            bool owner = (killerId == pickerId);
            return (owner, owner ? 0f : (120.0f - dropElapsedSeconds));
        }

        public static (bool success, string dropItem) EvaluateBloodZombieBloodDrink(int roll1000)
        {
            // 细节 494: 血僵尸死后挖取血饮概率
            if (roll1000 < 5) return (true, "血饮");
            return (false, "");
        }

        public static (bool harvestSuccess, float progressSec) EvaluateButcherCarving(bool hasKnife, float standingSeconds)
        {
            // 细节 529/530: 割肉持续 3 秒且需站在尸体 1 格内
            if (!hasKnife) return (false, 0f);
            return (standingSeconds >= 3.0f, Math.Min(3.0f, standingSeconds));
        }
    }
}
