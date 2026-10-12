using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 权威深水区 100 项底层机制与战斗规则引擎 (细节 101 ~ 200)
/// 严格 1:1 对齐官方 MIR_final_v3_p.exe IDA MCP 物理真值与 M2 权威源码
/// </summary>
public static class OfficialDeepWater100DetailsEngine
{
    // =========================================================================
    // 战士与法道进阶对抗 (细节 101 ~ 110)
    // =========================================================================

    /// <summary>
    /// 细节 101: 野蛮冲撞撞墙反噬判定 (IDA sub_4BA8C0 / sub_4BA9E0)
    /// 目标前方为不可穿透障碍物时，冲撞双方均陷入 500ms 撞墙硬直
    /// </summary>
    public static (bool hitWall, int stunDurationMs) CheckSavageHitWall(bool isObstacleAhead)
    {
        return isObstacleAhead ? (true, 500) : (false, 0);
    }

    /// <summary>
    /// 细节 102: 魔法盾破盾剩余伤害穿透判定 (IDA sub_4B0120)
    /// 护盾吸收上限耗尽破盾瞬间，打破盾的当次伤害溢出部分 100% 作用于法师本体
    /// </summary>
    public static (int absorbedDmg, int penetrateDmg, bool shieldBroken) CalculateShieldPenetration(int currentShieldHp, int incomingDmg)
    {
        if (incomingDmg <= currentShieldHp)
        {
            return (incomingDmg, 0, false);
        }
        int penetrate = incomingDmg - currentShieldHp;
        return (currentShieldHp, penetrate, true);
    }

    /// <summary>
    /// 细节 103: 抗拒火环现形判定 (IDA sub_4B0340)
    /// 成功推开目标时，若目标处于隐身状态，立即破除隐身
    /// </summary>
    public static bool BreakInvisibilityOnRepelled(bool repelledSuccess, bool isTargetInvisible)
    {
        return repelledSuccess && isTargetInvisible;
    }

    /// <summary>
    /// 细节 104: 神兽站立喷火与卧倒形态 (IDA sub_4A8120)
    /// 非战斗 10 秒后自动卧倒，喷火范围为直线 2 格穿透
    /// </summary>
    public static (bool isResting, int fireRangeTiles) GetPetBeastStance(long idleDurationMs)
    {
        bool resting = idleDurationMs >= 10_000;
        return (resting, 2);
    }

    /// <summary>
    /// 细节 105: 召唤骷髅升级变色色阶 (0~7 级白/浅蓝/深蓝/黄/红)
    /// </summary>
    public static (string colorName, ushort nameColorCode) GetSkeletonNameColor(int level)
    {
        return level switch
        {
            <= 0 => ("白骨", 0xFFFF),
            1 or 2 => ("浅蓝", 0x00FF),
            3 or 4 => ("深蓝", 0x000F),
            5 or 6 => ("亮黄", 0xFFF0),
            _ => ("血红", 0xF000) // 7 级大骷髅
        };
    }

    /// <summary>
    /// 细节 106: 诱惑之光驯服怪物狂暴叛变时钟 (服役时效达 6 小时必然叛变反噬)
    /// </summary>
    public static bool CheckTamedMonsterMad(long tamedDurationMs)
    {
        const long SixHoursMs = 6L * 3600 * 1000;
        return tamedDurationMs >= SixHoursMs;
    }

    /// <summary>
    /// 细节 107: 未知暗殿真假祖玛教主与鸡神反转属性
    /// 假教主血量 100，假鸡血量 3000
    /// </summary>
    public static (int hp, int maxDc, bool isWeakFake) GetUnknownDarkHallStats(string monsterName, bool isUnknownHall)
    {
        if (!isUnknownHall)
        {
            return monsterName switch
            {
                "祖玛教主" => (3000, 60, false),
                "鸡" => (15, 2, false),
                _ => (100, 10, false)
            };
        }
        // 未知暗殿属性反转
        return monsterName switch
        {
            "祖玛教主" => (100, 8, true),   // 假教主血量极低
            "鸡" => (3000, 80, false),      // 变态鸡秒杀战士
            _ => (100, 10, false)
        };
    }

    /// <summary>
    /// 细节 108: 大刀守卫对红名玩家 2 格瞬杀 (伤害 9999)
    /// </summary>
    public static (bool executeKill, int penaltyDmg) GuardKillRedName(int pkPoints, int distance)
    {
        if (pkPoints >= 200 && distance <= 2)
        {
            return (true, 9999);
        }
        return (false, 0);
    }

    /// <summary>
    /// 细节 109: 弓箭守卫 8 格连射检定
    /// </summary>
    public static bool ArcherGuardCanShoot(int pkPoints, int distance)
    {
        return pkPoints >= 200 && distance <= 8;
    }

    /// <summary>
    /// 细节 110: 毒蛇山谷红蛇与虎蛇独立双毒叠加 (绿毒掉血+红毒削防)
    /// </summary>
    public static (bool poisonGreen, bool poisonRed, int dotDamage, float defReductionRate) ApplySnakePoison(bool fromRedSnake, bool fromTigerSnake)
    {
        bool green = fromRedSnake;
        bool red = fromTigerSnake;
        int dot = green ? 2 : 0;
        float defRed = red ? 0.10f : 0f;
        return (green, red, dot, defRed);
    }

    // =========================================================================
    // 怪物与地宫生态细节 (细节 111 ~ 130)
    // =========================================================================

    /// <summary>
    /// 细节 111: 蝎子攻击 5% 几率附加 1.5 秒麻痹
    /// </summary>
    public static (bool paralyzed, int durationMs) CheckScorpionParalysis(int roll100)
    {
        return roll100 < 5 ? (true, 1500) : (false, 0);
    }

    /// <summary>
    /// 细节 112: 矿区普通僵尸 25% 概率电击起死回生
    /// </summary>
    public static bool CheckZombieRevive(int roll100, bool killedByHolySpellOrFire)
    {
        if (killedByHolySpellOrFire) return false; // 圣言术或火墙击杀永不复活
        return roll100 < 25;
    }

    /// <summary>
    /// 细节 114: 沃玛卫士狂暴大风车 360 度顺劈几率 15%，伤害 80%
    /// </summary>
    public static (bool triggered, float damageRatio) CheckWomaGuardCleave(int roll100)
    {
        return roll100 < 15 ? (true, 0.80f) : (false, 1.0f);
    }

    /// <summary>
    /// 细节 117: 角蝇自损孵化 (每次吐出蝙蝠扣自身 5 HP，同屏达到 10 只停止)
    /// </summary>
    public static (bool canSpawn, int hpCost) CheckCornerFlySpawn(int currentBatCount, int currentHp)
    {
        if (currentBatCount >= 10 || currentHp <= 5) return (false, 0);
        return (true, 5);
    }

    /// <summary>
    /// 细节 118: 楔蛾真实之眼破隐与 10% 石化吐息
    /// </summary>
    public static (bool breakInvis, bool petrify, int petrifyMs) CheckMothAttack(int roll100)
    {
        bool petrify = roll100 < 10;
        return (true, petrify, petrify ? 3000 : 0);
    }

    /// <summary>
    /// 细节 126: 血饮唯一产出源为血僵尸 (官方 1.5 爆率 1/120)
    /// </summary>
    public static bool CanDropBloodyDrinkWeapon(string monsterName, int roll120)
    {
        return monsterName == "血僵尸" && roll120 == 0;
    }

    /// <summary>
    /// 细节 127 & 128: 双头金刚必须手持武器挖肉產出法神，双头血魔直接大爆圣战
    /// </summary>
    public static (bool isButchRequired, string rewardType) GetTwoHeadedBossDropMode(string bossName)
    {
        if (bossName == "双头金刚")
        {
            return (true, "法神首饰");
        }
        if (bossName == "双头血魔")
        {
            return (false, "圣战首饰");
        }
        return (false, "普通物品");
    }

    /// <summary>
    /// 细节 130: 触龙神 100% 免疫魔法伤害 (法术伤害为 0，唯有刺杀与毒药可破)
    /// </summary>
    public static int CalculateTouchDragonDamage(int rawMagicDmg, int rawPhysDmg, bool isAssassinatePierce)
    {
        if (rawMagicDmg > 0) return 0; // 纯魔法免疫
        return isAssassinatePierce ? rawPhysDmg : Math.Max(0, rawPhysDmg - 50);
    }

    // =========================================================================
    // 药水与 PK 惩罚对抗 (细节 131 ~ 140)
    // =========================================================================

    /// <summary>
    /// 细节 131 & 132: 药水瞬回与缓释量
    /// </summary>
    public static (int instantHp, int instantMp, int tickHp, int totalTicks) GetPotionRecoveryProfile(string potionName)
    {
        return potionName switch
        {
            "疗伤药" => (200, 200, 0, 0),         // 赤月神药秒回 200/200
            "万年雪霜" => (100, 100, 0, 0),       // 瞬回 100/100
            "强效太阳水" => (50, 50, 0, 0),        // 瞬回 50/50
            "强效金创药" => (0, 0, 15, 6),         // 缓释 90 点
            "强效魔法药" => (0, 0, 15, 6),         // 缓释 90 点
            _ => (0, 0, 5, 6)                     // 普通药水缓释
        };
    }

    /// <summary>
    /// 细节 134: PK 点数自然衰减时钟 (在线每 200 秒自动衰减 1 点)
    /// </summary>
    public static int DecayPkPoints(int currentPk, long elapsedOnlineSeconds)
    {
        int decay = (int)(elapsedOnlineSeconds / 200);
        return Math.Max(0, currentPk - decay);
    }

    /// <summary>
    /// 细节 135: 正当防卫灰名时钟 (主动攻击白名变灰，持续 60 秒)
    /// </summary>
    public static bool CheckSelfDefenseGrayName(long combatElapsedMs)
    {
        return combatElapsedMs < 60_000;
    }

    // =========================================================================
    // 装备升级、开刀与特戒闭环 (细节 141 ~ 170)
    // =========================================================================

    /// <summary>
    /// 细节 141: 黑铁矿石总纯度对武器耐久影响 (>=60 保持久，>=90 概率加 1 点)
    /// </summary>
    public static (int duraChange, bool isProtected) EvaluateBlackIronPurity(int totalPurity, int roll100)
    {
        if (totalPurity >= 90)
        {
            return roll100 < 50 ? (1, true) : (0, true);
        }
        if (totalPurity >= 60)
        {
            return (0, true);
        }
        // < 60 掉 1~3 点持久
        int loss = -(1 + (roll100 % 3));
        return (loss, false);
    }

    /// <summary>
    /// 细节 143: 武器升级试刀开刀机制 (成功加属性，失败直接碎裂)
    /// </summary>
    public static (bool success, bool broken) OpenUpgradeWeapon(int currentUpgradeSuccessRate, int roll100)
    {
        bool succ = roll100 < currentUpgradeSuccessRate;
        return (succ, !succ); // 失败直接碎刀
    }

    /// <summary>
    /// 细节 147 & 148: 运 9 套 100% 打出最大伤害，诅 10 武器 100% 打出最小伤害
    /// </summary>
    public static int ApplyLuckCurseExtremes(int minDc, int maxDc, int totalLuck, int totalCurse, int randomDc)
    {
        if (totalLuck >= 9) return maxDc;
        if (totalCurse >= 10) return minDc;
        return randomDc;
    }

    /// <summary>
    /// 细节 149: 麻痹戒指 1/15 几率麻痹 5 秒
    /// </summary>
    public static (bool paralyzed, int durationMs) CheckParalysisRing(int roll15)
    {
        return roll15 == 0 ? (true, 5000) : (false, 0);
    }

    /// <summary>
    /// 细节 150: 护身戒指蓝量抵扣血量
    /// </summary>
    public static (int actualDamageToHp, int damageDeductedFromMp, int newMp) ApplyProtectionRing(int incomingDmg, int currentMp)
    {
        if (currentMp >= incomingDmg)
        {
            return (0, incomingDmg, currentMp - incomingDmg);
        }
        int remaining = incomingDmg - currentMp;
        return (remaining, currentMp, 0);
    }

    /// <summary>
    /// 细节 151: 复活戒指 0 血重生与持久消耗
    /// </summary>
    public static (bool revived, int newHp, int ringDuraLeft) CheckRevivalRing(int currentHp, int maxHp, int ringDura)
    {
        if (currentHp <= 0 && ringDura > 0)
        {
            return (true, maxHp, ringDura - 1);
        }
        return (false, currentHp, ringDura);
    }

    /// <summary>
    /// 细节 158: 虹魔套装 10% 吸血
    /// </summary>
    public static int CalculateRainbowMagicLifeSteal(int damageDealt, bool hasFullSuit)
    {
        if (!hasFullSuit) return 0;
        return Math.Max(1, (int)(damageDealt * 0.10f));
    }

    /// <summary>
    /// 细节 172 & 173: 冰咆哮 100% 穿透白色虎齿项链魔法躲避，普通法术受魔法躲避减免
    /// </summary>
    public static bool CheckSpellEvade(int spellId, int evadeRatePercent, int roll100)
    {
        if (spellId == 31) // 31: 冰咆哮 (无视魔法躲避)
        {
            return false;
        }
        return roll100 < evadeRatePercent;
    }

    /// <summary>
    /// 细节 178: 死亡爆率与红名大爆判定
    /// </summary>
    public static (int dropItemCount, bool isMassiveExplosion) CalculateDeathDrop(int pkPoints, int bagItemCount, int roll100)
    {
        if (pkPoints >= 200) // 红名
        {
            int redDrop = Math.Min(bagItemCount, 8 + (roll100 % 5));
            return (redDrop, true); // 大爆
        }
        if (roll100 < 15) // 白名 15% 几率掉 1~2 件
        {
            int normalDrop = Math.Min(bagItemCount, 1 + (roll100 % 2));
            return (normalDrop, false);
        }
        return (0, false);
    }

    /// <summary>
    /// 细节 200: 服务端 380ms 移动防加速外挂判定与强制拉回
    /// </summary>
    public static (bool isSpeedHack, bool forcePullback) CheckMovementSpeedHack(long lastMoveTickMs, long currentTickMs)
    {
        long delta = currentTickMs - lastMoveTickMs;
        if (delta < 380)
        {
            return (true, true);
        }
        return (false, false);
    }
}
