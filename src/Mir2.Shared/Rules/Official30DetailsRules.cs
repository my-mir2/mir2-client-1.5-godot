using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5《虎卫传说》30 项底层与表现细节权威规约与计算引擎 (Official30DetailsRules)
/// 严格 1:1 对齐官方原版二进制 (MIR_final_v3_p.exe / IDA Pro MCP) 物理真值
/// 跨端共享，供客户端 UI/渲染节点与服务端战斗/经济/网络逻辑共用
/// </summary>
public static class Official30DetailsRules
{
    // ==================== 一、战斗系统与技能动作细节 (1~8) ====================

    /// <summary>细节 1: 野蛮冲撞撞墙受挫顿步硬直 800ms，反噬自伤受创音效 SoundId=134</summary>
    public const int MotaeboCrashStunMs = 800;
    public const int MotaeboReboundSoundId = 134;
    public static int CalculateMotaeboSelfDamage(int skillLevel) => 5 * (skillLevel + 1);

    /// <summary>细节 2: 抗拒火环气流收拢施法后摇位移冻结 350ms，音效 SoundId=120</summary>
    public const int RingOfFireRecoveryFreezeMs = 350;
    public const int RingOfFireSoundId = 120;

    /// <summary>细节 3: 战士物理出刀基础动作间隙严格锁死 680ms (防瞬发双烈火外挂)</summary>
    public const int WarriorAttackActionIntervalMs = 680;
    public static bool CanExecuteFireHit(long lastAttackMs, long nowMs) =>
        nowMs - lastAttackMs >= WarriorAttackActionIntervalMs;

    /// <summary>细节 4: 火墙持续时间受魔法力上限衰减公式 (IDA sub_4A32C0: 15 + skill_level * 5 + mc_max / 2)</summary>
    public static int CalculateFireWallDuration(int skillLevel, int mcMax) =>
        15 + skillLevel * 5 + (mcMax / 2);

    /// <summary>细节 5: 诱惑之光随从叛变前 60 秒名字黄色/暗红变色预警</summary>
    public const int PetRevoltWarningThresholdSeconds = 60;
    public static bool IsPetRevoltWarning(int remainingSeconds) =>
        remainingSeconds <= PetRevoltWarningThresholdSeconds;

    /// <summary>细节 6: 目标躲避法术时飘出 MISS 虚字且不触发受击后仰动画 (damage <= 0)</summary>
    public static bool IsSpellMiss(int damage) => damage <= 0;

    /// <summary>细节 7: 道士神兽脱战 3 秒趴下防御，发现目标起立 8 帧前摇喷火</summary>
    public const float ShinsuProneDelaySeconds = 3.0f;
    public const int ShinsuRiseFrames = 8;

    /// <summary>细节 8: 战士隔位刺杀必须确保第 1 格无厚墙/石碑阻隔，否则剑气撞碎</summary>
    public static bool IsStabbingPathClear(int dir, int startX, int startY, Func<int, int, bool> isTileBlocked)
    {
        var (dx, dy) = GetDirectionDelta(dir);
        int midX = startX + dx;
        int midY = startY + dy;
        return !isTileBlocked(midX, midY);
    }

    // ==================== 二、官方 24 原生 UI 与交互细节 (9~16) ====================

    /// <summary>细节 9: 商店货架选中行深红矩形高亮 0x800000，底板 308x205，每页 10 行</summary>
    public const uint ShopSelectionHighlightColor = 0x800000;
    public const int ShopPanelWidth = 308;
    public const int ShopPanelHeight = 205;
    public const int ShopPageSize = 10;
    public const int ShopPurchaseSuccessSoundId = 106;
    public const int ShopInsufficientGoldSoundId = 105;

    /// <summary>细节 10: 背包拆分/存金币输入截断最大值保护 (1 ~ 5,000,000)</summary>
    public static long ClampGoldSplitInput(long input, long availableGold) =>
        Math.Clamp(input, 1, Math.Min(availableGold, 5000000));

    /// <summary>细节 11: 铁匠铺普通修理 SoundId=10，特殊修理叮当打铁音 SoundId=11 (IDA sub_4901C8)</summary>
    public const int NormalRepairSoundId = 10;
    public const int SpecialRepairSoundId = 11;

    /// <summary>细节 12: Tooltip 极品追加词条 (+X) 渲染为天蓝色 (#00ffff)</summary>
    private static readonly Regex SuperAttrRegex = new Regex(@"(\(\+\d+\))", RegexOptions.Compiled);
    public static string FormatSuperAttributeHighlight(string text)
    {
        if (string.IsNullOrEmpty(text)) return string.Empty;
        return SuperAttrRegex.Replace(text, "[color=#00ffff]$1[/color]");
    }

    /// <summary>细节 13: 大地图传送点光圈 500ms 周期呼吸闪烁</summary>
    public const int BigMapPortalBlinkCycleMs = 500;
    public static bool IsPortalBlinkVisible(double elapsedTimeSeconds) =>
        ((int)(elapsedTimeSeconds * 1000 / BigMapPortalBlinkCycleMs) % 2) == 0;

    /// <summary>细节 14: 小地图自身原点 300ms 闪烁，并带尖角指向 8 方向</summary>
    public const int MinimapRadarBlinkCycleMs = 300;
    public static bool IsPlayerRadarVisible(double elapsedTimeSeconds) =>
        ((int)(elapsedTimeSeconds * 1000 / MinimapRadarBlinkCycleMs) % 2) == 0;

    /// <summary>细节 15: 仓库二级密码连续输错 3 次锁定 300 秒，倒计时动态递减</summary>
    public const int SecurityLockoutDurationSeconds = 300;
    public static int DecrementLockout(int currentRemaining, int elapsedSeconds) =>
        Math.Max(0, currentRemaining - elapsedSeconds);

    /// <summary>细节 16: 双槽位手镯(4/5)、戒指(6/7) 按 Alt 键在 0/1 槽位间对比切换</summary>
    public static int ToggleDualSlotIndex(int currentSlotIndex) => currentSlotIndex == 0 ? 1 : 0;

    // ==================== 三、怪物生态、AI 与地表现象细节 (17~23) ====================

    /// <summary>细节 17: 怪物尸体倒地后保留 60 秒供割肉，最后 2 秒渐隐沉入地底消除</summary>
    public const float CorpseRemainDurationSeconds = 60.0f;
    public const float CorpseFadeThresholdSeconds = 2.0f;
    public static float CalculateCorpseAlpha(float remainingSeconds)
    {
        if (remainingSeconds <= 0f) return 0f;
        if (remainingSeconds < CorpseFadeThresholdSeconds)
            return remainingSeconds / CorpseFadeThresholdSeconds;
        return 1.0f;
    }

    /// <summary>细节 18: 割肉品质与重量受怪物生态折算 (鹿15, 鸡10, 狼12, 金刚18，品质 1~20)</summary>
    public static bool TryCalculateButcherMeat(string monsterName, int randomDelta, out string meatName, out int quality, out int weight)
    {
        meatName = string.Empty;
        quality = 0;
        weight = 1;

        int baseQual = monsterName switch
        {
            "鹿" => 15,
            "鸡" => 10,
            "狼" => 12,
            "双头金刚" => 18,
            _ => -1
        };
        if (baseQual < 0) return false;

        quality = Math.Clamp(baseQual + randomDelta, 1, 20);
        weight = Math.Max(1, quality / 5);
        meatName = $"{monsterName}肉";
        return true;
    }

    /// <summary>细节 19: 地表战利品寿命最后 10 秒开启 200ms 高频闪烁预警消失</summary>
    public const float GroundItemBlinkThresholdSeconds = 10.0f;
    public const float GroundItemBlinkPeriodSeconds = 0.2f;
    public static bool IsGroundItemBlinkVisible(float remainingSeconds)
    {
        if (remainingSeconds > GroundItemBlinkThresholdSeconds) return true;
        if (remainingSeconds <= 0f) return false;
        return ((int)(remainingSeconds / GroundItemBlinkPeriodSeconds) % 2) == 0;
    }

    /// <summary>细节 20: 月魔蜘蛛/楔蛾自石化/地底突袭破隐破土专属声效 SoundId=113</summary>
    public const int StealthSpiderSurpriseSoundId = 113;

    /// <summary>细节 21: 怪物行动频率生态差异化 (黑色恶蛆/楔蛾400ms, 野猪800ms, 僵尸1200ms)</summary>
    public static int GetMonsterStepIntervalMs(string monsterName) => monsterName switch
    {
        "黑色恶蛆" or "楔蛾" => 400,
        "红野猪" or "黑野猪" => 800,
        "僵尸" or "尸王" => 1200,
        _ => 1000
    };

    /// <summary>细节 22: 触龙神 3x3 空间地表动态占位阻挡偏移</summary>
    public static readonly (int dx, int dy)[] EvilCentipede3x3Offsets = new[]
    {
        (-1, -1), (0, -1), (1, -1),
        (-1,  0), (0,  0), (1,  0),
        (-1,  1), (0,  1), (1,  1)
    };

    /// <summary>细节 23: 废矿区爬行僵尸破土留下的尸王殿地洞入口 60 秒限时有效</summary>
    public const int ZombieHoleEntranceDurationMs = 60000;
    public static bool IsZombieHoleValid(long spawnMs, long currentMs) =>
        currentMs - spawnMs < ZombieHoleEntranceDurationMs;

    // ==================== 四、网络协议、经济与安全规则细节 (24~30) ====================

    /// <summary>细节 24: 老兵传送 NPC 严正拒绝红名玩家 (PK点数 >= 200)</summary>
    public const int RedNameTeleportRejectedPkThreshold = 200;
    public static bool IsRedName(int pkPoints) => pkPoints >= RedNameTeleportRejectedPkThreshold;

    /// <summary>细节 25: 仓库保管员每次存/取款物扣除 10 金币劳务保管费</summary>
    public const int StorageServiceFeeGold = 10;
    public static bool HasEnoughGoldForStorage(int currentGold) => currentGold >= StorageServiceFeeGold;

    /// <summary>细节 26: 正当自卫反击 60 秒有效保护时钟</summary>
    public const int SelfDefenseProtectionDurationSeconds = 60;
    public static bool IsSelfDefenseActive(long lastAttackedMs, long nowMs) =>
        (nowMs - lastAttackedMs) <= SelfDefenseProtectionDurationSeconds * 1000;

    /// <summary>细节 27: 武器升级试刀时目标防御力 (AC) 纳入开刀成功判定因数</summary>
    public static bool EvaluateTrialSlashSuccess(int targetAc, int weaponCurse, int baseLuck, int randomRoll)
    {
        // 目标防御过高增加开刀折断率，诅咒增加跳点失败率
        int successThreshold = 60 - weaponCurse * 10 + baseLuck * 5 - (targetAc / 4);
        return randomRoll < successThreshold;
    }

    /// <summary>细节 28: AOI 跨九宫格边界位移阻尼吸收因子 0.85f (杜绝瞬移抽搐)</summary>
    public const float DeadReckoningDampingFactor = 0.85f;

    /// <summary>细节 29: 聊天输入框双击玩家名字自动填充私聊前缀 /玩家名 </summary>
    public static string FormatWhisperPrefix(string playerName) => $"/{playerName} ";

    /// <summary>细节 30: 红名大爆金币按阿基米德螺旋线由内向外散落地表</summary>
    public static List<(int x, int y)> GenerateSpiralDropCoords(int centerX, int centerY, int count)
    {
        var list = new List<(int x, int y)> { (centerX, centerY) };
        if (count <= 1) return list;

        int x = centerX;
        int y = centerY;
        int dx = 0;
        int dy = -1;
        int stepLength = 1;
        int stepCount = 0;
        int turnCount = 0;

        while (list.Count < count)
        {
            x += dx;
            y += dy;
            list.Add((x, y));
            stepCount++;

            if (stepCount == stepLength)
            {
                stepCount = 0;
                // 顺时针旋转 90 度: (0,-1) -> (1,0) -> (0,1) -> (-1,0)
                int temp = dx;
                dx = -dy;
                dy = temp;
                turnCount++;
                if (turnCount % 2 == 0)
                {
                    stepLength++;
                }
            }
        }
        return list;
    }

    private static (int dx, int dy) GetDirectionDelta(int dir) => (dir % 8) switch
    {
        0 => (0, -1),
        1 => (1, -1),
        2 => (1, 0),
        3 => (1, 1),
        4 => (0, 1),
        5 => (-1, 1),
        6 => (-1, 0),
        7 => (-1, -1),
        _ => (0, 0)
    };
}
