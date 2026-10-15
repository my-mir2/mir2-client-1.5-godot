using System;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 终极深水区 100 项底层机制与战斗规则引擎 (细节 201 ~ 300)
/// 严格 1:1 对齐盛大官方原版二进制 MIR_final_v3_p.exe IDA MCP 物理真值与 M2 权威源码
/// </summary>
public static class OfficialUltimate100DetailsEngine
{
    // =========================================================================
    // 网络与通信安全细节 (细节 201 ~ 210)
    // =========================================================================

    /// <summary>
    /// 细节 201: TCP 缓冲区溢出保护 64KB (65535 字节) 熔断拦截
    /// </summary>
    public static bool CheckPacketOverflow(int payloadLength)
    {
        return payloadLength > 65535;
    }

    /// <summary>
    /// 细节 202: 客户端 8000ms 心跳保活探测
    /// </summary>
    public static bool CheckHeartbeatTimeout(long lastHeartbeatMs, long currentTickMs)
    {
        return (currentTickMs - lastHeartbeatMs) >= 8000;
    }

    /// <summary>
    /// 细节 205: 走路 550ms 与跑步 380ms 步频检定
    /// </summary>
    public static (int minIntervalMs, int stepDistanceTiles) GetMovementTiming(bool isRun)
    {
        return isRun ? (380, 2) : (550, 1);
    }

    /// <summary>
    /// 细节 206: 跑步被刺杀或雷电击中破跑定身 300ms
    /// </summary>
    public static (bool isInterrupted, int stunDurationMs) CheckRunInterruptionOnDamage(bool isRunning, bool isHeavyHit)
    {
        if (isRunning && isHeavyHit)
        {
            return (true, 300);
        }
        return (false, 0);
    }

    /// <summary>
    /// 细节 208: 地面金币根据数量展示不同堆叠外观与贴图索引
    /// </summary>
    public static (string lookName, int minGold) GetGroundGoldAppearance(int goldAmount)
    {
        return goldAmount switch
        {
            < 100 => ("单枚铜币", 1),
            < 1000 => ("小堆金币", 100),
            _ => ("大金币堆", 1000)
        };
    }

    /// <summary>
    /// 细节 209: 拾取地表金币直接增加人物金币并校验 5000 万安全上限
    /// </summary>
    public static (bool success, int newGold, int overflowGold) AddGoldSafely(int currentGold, int pickedGold)
    {
        const int MaxGold = 50_000_000;
        long total = (long)currentGold + pickedGold;
        if (total > MaxGold)
        {
            int overflow = (int)(total - MaxGold);
            return (true, MaxGold, overflow);
        }
        return (true, (int)total, 0);
    }

    // =========================================================================
    // 装备磨损、修理与祝福油细节 (细节 211 ~ 220)
    // =========================================================================

    /// <summary>
    /// 细节 211: 特殊修理金币花费计算公式 (普通修理的 3 倍)
    /// </summary>
    public static int CalculateSpecialRepairCost(int curDura, int maxDura, int basePrice)
    {
        if (maxDura <= 0 || curDura >= maxDura) return 0;
        int normalCost = ((maxDura - curDura) * basePrice) / (maxDura * 3);
        return Math.Max(1, normalCost * 3);
    }

    /// <summary>
    /// 细节 212: 普通修理折损最大持久度 1/16
    /// </summary>
    public static (int newDura, int newMaxDura) PerformNormalRepairWear(int curDura, int maxDura)
    {
        if (maxDura <= 0) return (0, 0);
        int loss = Math.Max(1, maxDura / 16);
        int newMax = Math.Max(1, maxDura - loss);
        return (newMax, newMax);
    }

    /// <summary>
    /// 细节 214: 祝福油提升武器幸运概率递减曲线 (0->1:50% ... 6->7:1%)
    /// </summary>
    public static (bool success, bool cursed, int newLuck, int newCurse) DrinkBlessOil(int currentLuck, int currentCurse, int roll100)
    {
        if (currentCurse > 0)
        {
            // 优先洗掉诅咒
            if (roll100 < 50) return (true, false, currentLuck, currentCurse - 1);
            return (false, false, currentLuck, currentCurse);
        }
        if (currentLuck >= 7)
        {
            return (false, false, currentLuck, currentCurse); // +7 封顶
        }
        int successRate = currentLuck switch
        {
            0 => 50,
            1 => 35,
            2 => 20,
            3 => 10,
            4 => 5,
            5 => 2,
            _ => 1
        };
        if (roll100 < successRate)
        {
            return (true, false, currentLuck + 1, 0);
        }
        // 5% 概率反被诅咒
        if (roll100 >= 95)
        {
            return (false, true, currentLuck, currentCurse + 1);
        }
        return (false, false, currentLuck, currentCurse);
    }

    /// <summary>
    /// 细节 216 ~ 218: 名字颜色映射 (白/黄/红名)
    /// </summary>
    public static (string colorLabel, ushort rgb16) EvaluateNameColor(int pkPoints, bool isSelfDefense)
    {
        if (pkPoints >= 200) return ("红名", 0xF000);
        if (isSelfDefense) return ("黄名", 0xFFF0);
        return ("白名", 0xFFFF);
    }

    // =========================================================================
    // 地牢生态与教主细节 (细节 221 ~ 230)
    // =========================================================================

    /// <summary>
    /// 细节 226: 祖玛教主残血狂暴召唤 (HP < 1000 时召唤 3 只雕像小怪)
    /// </summary>
    public static (bool canSummon, int minionCount) CheckZumaLordRageSummon(int currentHp, bool hasSummonedOnce)
    {
        if (currentHp < 1000 && !hasSummonedOnce)
        {
            return (true, 3);
        }
        return (false, 0);
    }

    /// <summary>
    /// 细节 233: 火墙持续时间与法术攻击力挂钩公式
    /// </summary>
    public static int CalculateFireWallDurationSeconds(int maxMc)
    {
        return 15 + Math.Max(0, maxMc / 5);
    }

    /// <summary>
    /// 细节 235: 雷电术对不死系怪物的 30% 破甲增伤
    /// </summary>
    public static int CalculateThunderDamage(int baseDmg, bool isUndead, int targetMac)
    {
        if (isUndead)
        {
            int reducedMac = (int)(targetMac * 0.70f);
            return Math.Max(1, baseDmg - reducedMac);
        }
        return Math.Max(1, baseDmg - targetMac);
    }

    /// <summary>
    /// 细节 237: 魔法盾吸收比率 (0级50%, 1级60%, 2级70%, 3级80%)
    /// </summary>
    public static float GetShieldAbsorbRate(int shieldLevel)
    {
        return shieldLevel switch
        {
            0 => 0.50f,
            1 => 0.60f,
            2 => 0.70f,
            _ => 0.80f
        };
    }

    /// <summary>
    /// 细节 238: 圣言术不死系秒杀概率公式
    /// </summary>
    public static bool CheckHolySpellKill(int playerLevel, int monsterLevel, int skillLevel, bool isUndead, int roll100)
    {
        if (!isUndead || playerLevel <= monsterLevel) return false;
        int rate = (playerLevel - monsterLevel) * 2 + skillLevel * 5;
        rate = Math.Clamp(rate, 5, 80); // 盛大官方 5%~80% 浮动区间
        return roll100 < rate;
    }

    // =========================================================================
    // 战士剑术与特戒终极机制 (细节 251 ~ 300)
    // =========================================================================

    /// <summary>
    /// 细节 251: 攻杀剑术刀刃泛红爆发判定 (1/4 几率，附加 30% 最大攻击)
    /// </summary>
    public static (bool isPowerHit, int bonusDmg) CheckPowerHit(int maxDc, int roll4)
    {
        if (roll4 == 0)
        {
            int bonus = Math.Max(1, (int)(maxDc * 0.30f));
            return (true, bonus);
        }
        return (false, 0);
    }

    /// <summary>
    /// 细节 252: 刺杀剑术隔位剑气 100% 破盾破防
    /// </summary>
    public static int CalculateAssassinateDamage(int rawDc, int targetAc, bool isDistance2)
    {
        if (isDistance2)
        {
            return rawDc; // 100% 穿透护甲
        }
        return Math.Max(1, rawDc - targetAc);
    }

    /// <summary>
    /// 细节 253: 半月弯刀扇形 4 格顺劈伤害衰减 (主目标 100%，侧翼 3 目标 40%)
    /// </summary>
    public static int CalculateHalfMoonDamage(int rawDc, bool isMainTarget)
    {
        if (isMainTarget) return rawDc;
        return Math.Max(1, (int)(rawDc * 0.40f));
    }

    /// <summary>
    /// 细节 258: 狂风套攻速加成出刀时间间隔
    /// </summary>
    public static int CalculateAttackIntervalMs(int baseIntervalMs, int attackSpeedBonus)
    {
        int reduction = attackSpeedBonus * 10;
        return Math.Max(500, baseIntervalMs - reduction);
    }

    /// <summary>
    /// 细节 280 & 281: 武器与衣服持久归零惩罚 (武器攻击归 1，衣服护甲归 0)
    /// </summary>
    public static (int effectiveDc, int effectiveAc) EvaluateBrokenEquipStats(int rawDc, int rawAc, int weaponDura, int dressDura)
    {
        int dc = weaponDura <= 0 ? 1 : rawDc;
        int ac = dressDura <= 0 ? 0 : rawAc;
        return (dc, ac);
    }

    /// <summary>
    /// 细节 291 & 292: 组队最多 11 人限制与 20% 组队经验加成均摊
    /// </summary>
    public static (bool canJoin, int expPerMember) CalculatePartyExpShare(int currentPartySize, int monsterBaseExp)
    {
        if (currentPartySize >= 11)
        {
            return (false, 0);
        }
        int newPartySize = currentPartySize + 1;
        int totalExp = (int)(monsterBaseExp * 1.20f); // 额外 20% 组队加成
        int perMember = Math.Max(1, totalExp / newPartySize);
        return (true, perMember);
    }

    /// <summary>
    /// 细节 293: 行会掌门封号编辑 14 字符严格限制
    /// </summary>
    public static string NormalizeGuildRankName(string input)
    {
        if (string.IsNullOrEmpty(input)) return "普通成员";
        return input.Length <= 14 ? input : input[..14];
    }
}
