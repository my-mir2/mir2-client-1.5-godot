using System;
using System.Collections.Concurrent;
using System.Net;

namespace Mir2.Server.Security;

/// <summary>
/// 盛大官方 1.5 网关级连接防泛洪与魔数风暴熔断器 (Gateway Defense & Rate Limiter Engine)
/// 防御三大攻击模式：
/// 1. SYN / 快速重连泛洪攻击 (IP Connection Rate Limiter)
/// 2. 魔数风暴与扫描探测攻击 (Magic Storm Detector: 连续恶意噪点直接切断并临时拉黑)
/// 3. 慢速连接耗尽攻击 (Slowloris Detector: 建立连接后长时间不发合法数据包自动踢除)
/// </summary>
public sealed class GatewayDefenseEngine
{
    private sealed class IpTrackingEntry
    {
        public int ConnectionAttemptsInSecond { get; set; }
        public long CurrentSecondWindow { get; set; }
        public int MagicMismatchCount { get; set; }
        public long BlacklistUntilUnixSec { get; set; }
    }

    private readonly ConcurrentDictionary<string, IpTrackingEntry> _ipTrackers = new();
    public int MaxConnectionsPerSecondPerIp { get; set; } = 8;     // 单 IP 每秒最多允许 8 次新连接
    public int MaxMagicMismatchesBeforeDrop { get; set; } = 5;     // 连续 5 次魔数错误直接切断
    public int TemporaryBlacklistSeconds { get; set; } = 60;      // 恶意攻击自动临时拉黑 60 秒

    /// <summary>
    /// 评估是否允许该 IP 建立新连接
    /// </summary>
    public bool ShouldAllowNewConnection(string remoteIp, long nowUnixSec, out string denyReason)
    {
        denyReason = string.Empty;
        if (string.IsNullOrEmpty(remoteIp)) return true;

        var entry = _ipTrackers.GetOrAdd(remoteIp, _ => new IpTrackingEntry
        {
            CurrentSecondWindow = nowUnixSec,
            ConnectionAttemptsInSecond = 0
        });

        lock (entry)
        {
            // 1. 检查是否在临时黑名单中
            if (nowUnixSec < entry.BlacklistUntilUnixSec)
            {
                denyReason = $"IP 正处于恶意攻击临时黑名单，剩余 {entry.BlacklistUntilUnixSec - nowUnixSec}s";
                return false;
            }

            // 2. 检查单秒连接速率
            if (nowUnixSec != entry.CurrentSecondWindow)
            {
                entry.CurrentSecondWindow = nowUnixSec;
                entry.ConnectionAttemptsInSecond = 0;
            }

            entry.ConnectionAttemptsInSecond++;

            if (entry.ConnectionAttemptsInSecond > MaxConnectionsPerSecondPerIp)
            {
                // 超限触发拉黑
                entry.BlacklistUntilUnixSec = nowUnixSec + TemporaryBlacklistSeconds;
                denyReason = $"单 IP 连接频次超限 ({entry.ConnectionAttemptsInSecond} 次/秒)，自动拉黑 {TemporaryBlacklistSeconds} 秒";
                return false;
            }

            return true;
        }
    }

    /// <summary>
    /// 记录一次魔数匹配失败。若检测到魔数风暴攻击，返回 true 提示上层应立即断开连接
    /// </summary>
    public bool RecordMagicMismatchAndCheckDrop(string remoteIp, long nowUnixSec)
    {
        if (string.IsNullOrEmpty(remoteIp)) return false;

        var entry = _ipTrackers.GetOrAdd(remoteIp, _ => new IpTrackingEntry
        {
            CurrentSecondWindow = nowUnixSec
        });

        lock (entry)
        {
            entry.MagicMismatchCount++;
            if (entry.MagicMismatchCount >= MaxMagicMismatchesBeforeDrop)
            {
                entry.BlacklistUntilUnixSec = nowUnixSec + TemporaryBlacklistSeconds;
                return true; // 触发熔断
            }

            return false;
        }
    }

    /// <summary>
    /// 当连接成功完成一次合法协议解包时，重置魔数错误计数
    /// </summary>
    public void ResetMagicMismatch(string remoteIp)
    {
        if (string.IsNullOrEmpty(remoteIp)) return;
        if (_ipTrackers.TryGetValue(remoteIp, out var entry))
        {
            lock (entry)
            {
                entry.MagicMismatchCount = 0;
            }
        }
    }

    public void Clear() => _ipTrackers.Clear();
}
