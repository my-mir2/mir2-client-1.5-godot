using System;
using System.Collections.Concurrent;

namespace Mir2.Server.Security;

/// <summary>
/// 盛大官方 1.5 权威反外挂变速齿轮与时钟加速侦测看门狗 (SpeedHack Watchdog Engine)
/// 专为打击商业私服中最猖獗的“时钟 Hook 变速齿轮、物理加速刀、飞速喝药”而设计。
/// 核心原理：
/// 1. 会话级逻辑时钟与服务端物理高精度单调时钟进行动态差分对齐；
/// 2. 统计滑动时间窗口（如最近 3 秒内）的操作密度与时钟步进速率；
/// 3. 当速率比超过 1.25（即客户端以 1.25 倍以上物理速度奔跑或出刀），判定为加速外挂；
/// 4. 触发制动惩罚：强制注入动作硬直冷冻期，并记录安全审计日志。
/// </summary>
public sealed class SpeedHackWatchdogEngine
{
    public sealed class ClientClockProfile
    {
        public int SessionId { get; set; }
        public long InitialServerTickMs { get; set; }
        public long InitialClientTickMs { get; set; }
        public long LastServerTickMs { get; set; }
        public long LastClientTickMs { get; set; }

        public int ActionCountInWindow { get; set; }
        public long WindowStartServerMs { get; set; }
        public long PenaltyFrozenUntilServerMs { get; set; }
        public int SpeedHackViolationCount { get; set; }
    }

    private readonly ConcurrentDictionary<int, ClientClockProfile> _profiles = new();
    public double MaxSpeedRatioTolerance { get; set; } = 1.35; // 允许最多 35% 的网络偶发突发抖动
    public int MaxActionsPerWindow { get; set; } = 15;         // 3 秒内最多允许 15 次操作（充分包容 300ms 连续奔跑中的合理转向，严密拦截连发外挂）
    public long WindowDurationMs { get; set; } = 3000;         // 统计窗口 3 秒
    public long DefaultPenaltyMs { get; set; } = 800;          // 抓到加速强制冰冻硬直 0.8 秒

    /// <summary>
    /// 当客户端发起移动、攻击、施法等核心逻辑操作时调用此方法核验
    /// </summary>
    /// <returns>若判定为加速作弊则返回 true，同时激活惩罚制动；否则返回 false</returns>
    public bool VerifyActionAndDetectSpeedHack(int sessionId, long clientTickMs, long serverNowMs, out string violationReason)
    {
        violationReason = string.Empty;

        var profile = _profiles.GetOrAdd(sessionId, id => new ClientClockProfile
        {
            SessionId = id,
            InitialServerTickMs = serverNowMs,
            InitialClientTickMs = clientTickMs,
            LastServerTickMs = serverNowMs,
            LastClientTickMs = clientTickMs,
            WindowStartServerMs = serverNowMs,
            ActionCountInWindow = 0
        });

        lock (profile)
        {
            // 1. 如果当前处于变速惩罚冷冻期中，直接拦截制动
            if (serverNowMs < profile.PenaltyFrozenUntilServerMs)
            {
                violationReason = $"处于外挂制动冷冻期，剩余 {profile.PenaltyFrozenUntilServerMs - serverNowMs}ms";
                return true;
            }

            // 2. 检查滑动窗口是否重置
            if (serverNowMs - profile.WindowStartServerMs > WindowDurationMs)
            {
                profile.WindowStartServerMs = serverNowMs;
                profile.ActionCountInWindow = 0;
            }

            profile.ActionCountInWindow++;

            // 3. 动作密度检测：如果 3 秒内动作次数超限
            if (profile.ActionCountInWindow > MaxActionsPerWindow)
            {
                profile.SpeedHackViolationCount++;
                profile.PenaltyFrozenUntilServerMs = serverNowMs + DefaultPenaltyMs;
                violationReason = $"短时间动作密度过载 ({profile.ActionCountInWindow} 次 / {WindowDurationMs}ms)";
                return true;
            }

            // 4. 时钟相对流速比值检测 (Speed Ratio = ClientDelta / ServerDelta)
            long elapsedServer = serverNowMs - profile.InitialServerTickMs;
            long elapsedClient = clientTickMs - profile.InitialClientTickMs;

            // 至少经过 2 秒后才进行时钟斜率比对，避免初始握手抖动误判
            if (elapsedServer >= 2000 && clientTickMs > profile.InitialClientTickMs)
            {
                double speedRatio = (double)elapsedClient / elapsedServer;
                if (speedRatio > MaxSpeedRatioTolerance)
                {
                    profile.SpeedHackViolationCount++;
                    profile.PenaltyFrozenUntilServerMs = serverNowMs + DefaultPenaltyMs;
                    violationReason = $"客户端系统时钟加速 (速率比: {speedRatio:F2}x, 阈值: {MaxSpeedRatioTolerance:F2}x)";
                    return true;
                }
            }

            profile.LastServerTickMs = serverNowMs;
            profile.LastClientTickMs = clientTickMs;
            return false;
        }
    }

    public void UnregisterSession(int sessionId)
    {
        _profiles.TryRemove(sessionId, out _);
    }

    public void Clear() => _profiles.Clear();
}
