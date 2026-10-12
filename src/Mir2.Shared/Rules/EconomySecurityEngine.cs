using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 经济交易、行会社交系统、客户端底层防护与反作弊引擎 (细节 231 ~ 250)
/// 严格依据官方服务端安全逻辑与客户端底层规范
/// </summary>
public static class EconomySecurityEngine
{
    // 盛大官方 1.5 权威细节 231: 面对面两阶段交易原子锁定状态机 (TFrmDlg_Deal，官方 1.5 绝无个人摆摊)
    public static class DealAtomicLockEngine
    {
        public static (bool bothLocked, bool canProceedToExchange) CheckDealLock(bool myLocked, bool remoteLocked)
        {
            bool both = myLocked && remoteLocked;
            return (both, both);
        }
    }

    // 盛大官方 1.5 权威细节 232: 装备普通修理与特殊修理持久递减公式 (IDA sub_4A1F30)
    public static class EquipmentRepairWearEngine
    {
        public static (int newMaxDura, int newCurrentDura, int costGold) ExecuteRepair(int currentDura, int maxDura, bool isSpecialRepair, int basePrice)
        {
            if (isSpecialRepair)
            {
                // 特殊修理: 0 损耗，但修理费用为普通修理的 3 倍
                return (maxDura, maxDura, Math.Max(100, basePrice * 3));
            }
            // 普通修理: 扣减 1 点最大持久上限 (1000 内部单位表示 1 点持久)
            int decayedMax = Math.Max(1000, maxDura - 1000);
            return (decayedMax, decayedMax, Math.Max(50, basePrice));
        }
    }

    // 盛大官方 1.5 权威细节 233: 祝福油幸运与诅咒几率判定公式 (IDA sub_4C6238)
    public static class BlessOilCurseLuckEngine
    {
        public static (bool success, int newLuck, int newCurse, string message) ApplyBlessOil(int currentLuck, int currentCurse, int roll100)
        {
            if (currentCurse > 0)
            {
                // 诅咒清洗
                return (true, currentLuck, currentCurse - 1, "武器上的诅咒力量消退了。");
            }
            if (currentLuck >= 7)
            {
                return (false, currentLuck, currentCurse, "武器的幸运值已达到玛法极限，无法再通过祝福油提升！");
            }
            if (roll100 < 35) // 35% 几率加幸运
            {
                return (true, currentLuck + 1, 0, "你的武器被注入了神圣的祝福力量！");
            }
            if (roll100 >= 85) // 15% 几率加诅咒
            {
                return (false, Math.Max(0, currentLuck - 1), currentCurse + 1, "武器被邪恶力量污染，受到了诅咒！");
            }
            return (false, currentLuck, currentCurse, "祝福油化作一缕青烟消散了。");
        }
    }


    // 细节 234: 交易双向锁定后 3 秒防换物倒计时冷却
    public static class TradeConfirmDelayEngine
    {
        public const float LockDelaySeconds = 3.0f;
        public static (bool canConfirm, float remainingSeconds) CheckConfirmDelay(float elapsedSinceBothLocked)
        {
            if (elapsedSinceBothLocked < LockDelaySeconds)
            {
                return (false, LockDelaySeconds - elapsedSinceBothLocked);
            }
            return (true, 0f);
        }
    }

    // 细节 235: 行会宣战与结盟状态显示 (绿色同盟、红色敌对)
    public static class GuildDiplomacyColorEngine
    {
        public enum DiplomacyStatus { Neutral, Ally, Hostile }
        public static string GetTitleColor(DiplomacyStatus status)
        {
            return status switch
            {
                DiplomacyStatus.Ally => "#00FF00",    // 绿色同盟
                DiplomacyStatus.Hostile => "#FF0000", // 红色敌对
                _ => "#FFFFFF"
            };
        }
    }

    // 细节 236: 行会封号自定义 (IDA sub_48A7B0 限制 14 字符)
    public static class GuildTitleLengthEngine
    {
        public const int MaxTitleBytes = 14;
        public static (bool valid, string formattedTitle) SanitizeTitle(string rawTitle)
        {
            if (string.IsNullOrEmpty(rawTitle)) return (true, "");
            int byteLen = 0;
            foreach (char c in rawTitle)
            {
                byteLen += c > 127 ? 2 : 1;
            }
            if (byteLen > MaxTitleBytes) return (false, "封号长度超出限制 (最多 14 字节/7 个汉字)！");
            return (true, rawTitle);
        }
    }

    // 盛大官方 1.5 权威细节 237: 白日门虎卫堂虎卫招募机制 (诱惑之光 Lv.3 检定)
    public static class HuWeiTameEngine
    {
        public static (bool canTame, float baseSuccessRate) CheckHuWeiTame(int playerJob, int spellLevel, int playerLevel)
        {
            // 官方 1.5 规则: 仅法师 (Job=1) 且诱惑之光达到 3 级、角色等级 >= 13 方可招募虎卫
            if (playerJob != 1 || spellLevel < 3 || playerLevel < 13)
            {
                return (false, 0f);
            }
            return (true, 0.15f); // 基础 15% 招募几率
        }
    }

    // 盛大官方 1.5 权威细节 238: 丛林迷宫鹰卫荒野索敌与超远距离物理箭矢射击机制
    public static class YingWeiCombatEngine
    {
        public const int MaxAttackRange = 8; // 官方 1.5 鹰卫射程 8 格
        public static bool IsInAttackRange(int distance) => distance >= 1 && distance <= MaxAttackRange;
    }


    // 细节 239: 天下第一雕像每周属性与外观同步更新
    public static class StatueWeeklySyncEngine
    {
        public record ChampionInfo(string Name, int Job, int Level);
        public static bool NeedsSync(int currentWeek, int recordedWeek) => currentWeek != recordedWeek;
    }

    // 细节 240: 沙巴克税金每日凌晨 0 点自动入城主私库
    public static class SabukDailyTaxEngine
    {
        public static int SettleDailyTax(int totalCityTurnover, float taxRate = 0.05f)
        {
            return (int)(totalCityTurnover * taxRate);
        }
    }

    // 盛大官方 1.5 权威细节 241: 沙巴克秘密通道传送凭证检定 (沙巴克密道老人/香石墓穴通行卷认证，严禁混淆 1.76 幻境藏宝阁)
    public static class SabukSecretPassageEngine
    {
        public static bool CanEnter(HashSet<string> bagItemNames)
        {
            return bagItemNames.Contains("密道传送卷") || bagItemNames.Contains("沙巴克凭证");
        }
    }

    // 细节 242: 客户端移动加速作弊检测 (允许合理网络时序抖动，阈值设为 240ms 拦截极端变速齿轮外挂，保障 300ms 官方奔跑流畅通过)
    public static class SpeedHackDetectorEngine
    {
        public const int MinMoveIntervalMs = 240;
        public static (bool isSpeedHack, bool rollbackToLastPos) CheckMovePacket(long currentPacketTimeMs, long lastPacketTimeMs)
        {
            if (currentPacketTimeMs - lastPacketTimeMs < MinMoveIntervalMs)
            {
                return (true, true); // 移动过快，判定外挂拉回
            }
            return (false, false);
        }
    }

    // 细节 243: 客户端挥刀出刀速度检测 (攻击间隔动态对齐攻击速度 AttackSpeed)
    public static class AttackSpeedValidatorEngine
    {
        public const int MinAttackIntervalMs = 680;

        /// <summary>
        /// 校验出刀攻击间隔 (盛大官方 1.5 规则: 狂风套/极品攻速动态缩短攻击硬直，每点攻速缩减 60ms，下限 420ms，上限 1000ms)
        /// </summary>
        public static bool IsAttackValid(long currentAttackMs, long lastAttackMs, int attackSpeed = 0)
        {
            int effectiveInterval = Math.Clamp(MinAttackIntervalMs - attackSpeed * 60, 420, 1000);
            return (currentAttackMs - lastAttackMs) >= effectiveInterval;
        }
    }

    // 细节 244: 客户端法术公共 CD 检测 (施法间隔 < 850ms 拦截)
    public static class SpellGlobalCdEngine
    {
        public const int SpellGcdMs = 850;
        public static (bool allowed, string statusMsg) CheckSpellCast(long nowMs, long lastSpellMs)
        {
            if (nowMs - lastSpellMs < SpellGcdMs)
            {
                return (false, "施法过于频繁，法力发生紊乱！");
            }
            return (true, "");
        }
    }

    // 细节 245: 客户端穿人穿怪外挂检测 (物理重叠强制拉回)
    public static class NoclipDetectorEngine
    {
        public static bool CheckNoclip(int newX, int newY, HashSet<(int x, int y)> solidEntities)
        {
            return solidEntities.Contains((newX, newY)); // 发现与活体实体坐标重叠
        }
    }

    // 细节 246: 本地内存保护与状态锁 (禁止本地伪造血量回写)
    public static class MemoryStateLockEngine
    {
        public static int SynchronizeServerHp(int localHp, int verifiedServerHp)
        {
            // 客户端绝对不可信任本地篡改的 HP，永远以服务端权威为准
            return verifiedServerHp;
        }
    }

    // 细节 247: 音频 WIL 资源异步多线程预加载防卡顿
    public static class AudioPreloadQueueEngine
    {
        private static readonly Queue<int> _preloadQueue = new();
        public static void EnqueueSound(int soundId) => _preloadQueue.Enqueue(soundId);
        public static int? DequeueNext() => _preloadQueue.Count > 0 ? _preloadQueue.Dequeue() : null;
        public static int PendingCount => _preloadQueue.Count;
    }

    // 细节 248: UI 窗体层级置顶与 Esc 逐级关闭堆栈
    public class UiZOrderStackEngine
    {
        private readonly List<string> _stack = new();

        public void Push(string windowName)
        {
            _stack.Remove(windowName);
            _stack.Add(windowName);
        }

        public string? PopTop()
        {
            if (_stack.Count == 0) return null;
            string top = _stack[^1];
            _stack.RemoveAt(_stack.Count - 1);
            return top;
        }

        public int Count => _stack.Count;
    }

    // 细节 249: 全屏与窗口化自由切换等比自适应黑边拉伸 (800x600)
    public static class AspectFitScalerEngine
    {
        public static (int viewportW, int viewportH, int offsetX, int offsetY) CalculateAspectFit(
            int screenW, int screenH, int targetW = 800, int targetH = 600)
        {
            float scaleX = (float)screenW / targetW;
            float scaleY = (float)screenH / targetH;
            float scale = Math.Min(scaleX, scaleY);

            int viewW = (int)(targetW * scale);
            int viewH = (int)(targetH * scale);
            int offX = (screenW - viewW) / 2;
            int offY = (screenH - viewH) / 2;
            return (viewW, viewH, offX, offY);
        }
    }

    // 细节 250: 客户端异常捕获与 Crash Dump 日志归档
    public static class CrashDumpLoggerEngine
    {
        public static string FormatCrashDump(string exceptionMsg, string stackTrace)
        {
            return $"[CRASH_DUMP_{DateTime.UtcNow:yyyyMMdd_HHmmss}]\nError: {exceptionMsg}\nStackTrace:\n{stackTrace}\n";
        }
    }
}
