using System;
using System.Collections.Generic;

namespace Mir2.Shared.Rules;

/// <summary>
/// 盛大官方 1.5 经济摆摊、社交系统、客户端底层防护与反作弊引擎 (细节 231 ~ 250)
/// 严格依据官方服务端安全逻辑与客户端底层规范
/// </summary>
public static class EconomySecurityEngine
{
    // 细节 231: 玩家安全区摆摊模式 (招牌文字、盘坐姿态)
    public static class PlayerStallModeEngine
    {
        public static (bool isStalling, string poseAction, string cleanTitle) SetupStall(bool inSafeZone, string rawTitle)
        {
            if (!inSafeZone) return (false, "Stand", "");
            string clean = rawTitle.Trim();
            if (clean.Length > 12) clean = clean.Substring(0, 12);
            return (true, "SitDown", clean);
        }
    }

    // 细节 232: 摆摊物品标价金币合法性安全检查 (防 0 元购、负数与溢出)
    public static class StallPriceValidationEngine
    {
        public const int MaxPrice = 2000000000; // 20 亿金币上限
        public static bool ValidatePrice(int price)
        {
            return price > 0 && price <= MaxPrice;
        }
    }

    // 细节 233: 购买摆摊物品背包空间与金币原子扣除
    public static class StallPurchaseAtomicEngine
    {
        public static (bool success, int remainingBuyerGold, int sellerEarnGold, string failReason) ExecutePurchase(
            int buyerGold, int bagFreeSlots, int price)
        {
            if (bagFreeSlots <= 0) return (false, buyerGold, 0, "背包已满，无法容纳该物品！");
            if (buyerGold < price) return (false, buyerGold, 0, "金币不足，无法购买！");
            return (true, buyerGold - price, price, "");
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
            var bytes = System.Text.Encoding.GetEncoding("GBK", System.Text.EncoderFallback.ReplacementFallback, System.Text.DecoderFallback.ReplacementFallback).GetBytes(rawTitle);
            if (bytes.Length > MaxTitleBytes) return (false, "封号长度超出限制 (最多 14 字节/7 个汉字)！");
            return (true, rawTitle);
        }
    }

    // 细节 237: 师徒系统出师声望奖励 (师傅 +35，徒弟祝福卷轴)
    public static class MasterApprenticeEngine
    {
        public static (int masterPrestigeReward, string apprenticeRewardItem) GraduateApprentice()
        {
            return (35, "出师祝福卷轴");
        }
    }

    // 细节 238: 婚姻系统求婚协议消耗结婚戒指
    public static class MarriageCeremonyEngine
    {
        public static (bool success, string notice) ProposeMarriage(bool hasWeddingRing, string targetPlayer)
        {
            if (!hasWeddingRing) return (false, "求婚需要一枚真诚的求婚戒指！");
            return (true, $"你已向 {targetPlayer} 发出求婚请求，愿结连理！");
        }
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

    // 细节 241: 沙巴克藏宝阁密道凭证检定
    public static class TreasurePavilionPassEngine
    {
        public static bool CanEnter(HashSet<string> bagItemNames)
        {
            return bagItemNames.Contains("藏宝阁通行证");
        }
    }

    // 细节 242: 客户端移动加速作弊检测 (间隔 < 380ms 强制拉回)
    public static class SpeedHackDetectorEngine
    {
        public const int MinMoveIntervalMs = 380;
        public static (bool isSpeedHack, bool rollbackToLastPos) CheckMovePacket(long currentPacketTimeMs, long lastPacketTimeMs)
        {
            if (currentPacketTimeMs - lastPacketTimeMs < MinMoveIntervalMs)
            {
                return (true, true); // 移动过快，判定外挂拉回
            }
            return (false, false);
        }
    }

    // 细节 243: 客户端挥刀出刀速度检测 (攻击间隔 < 680ms 忽略无效攻击)
    public static class AttackSpeedValidatorEngine
    {
        public const int MinAttackIntervalMs = 680;
        public static bool IsAttackValid(long currentAttackMs, long lastAttackMs)
        {
            return (currentAttackMs - lastAttackMs) >= MinAttackIntervalMs;
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
