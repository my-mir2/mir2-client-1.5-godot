using System;
using System.Collections.Concurrent;
using Mir2.Server.Network;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 仓库二级密码与防暴力破解密保锁权威引擎 (StorageSecurityEngine)
/// 严格 1:1 对齐官方 M2Server 仓库密保规范:
/// 1. 独立二级密码设置与状态流转 (SetPassword, VerifyPassword, ChangePassword)
/// 2. 登录上线后首次存取仓库强制验密，解锁前严禁支取金币与物品
/// 3. 防暴力破解锁: 连续输错 3 次密码冻结仓库 10 分钟 (600,000 ms)
/// </summary>
public sealed class StorageSecurityEngine
{
    public sealed class StorageSecurityProfile
    {
        public string AccountName { get; init; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool HasPassword => !string.IsNullOrEmpty(PasswordHash);
        public bool IsSessionUnlocked { get; set; }
        public int FailedAttempts { get; set; }
        public long LockedUntilTickMs { get; set; }
    }

    private readonly ConcurrentDictionary<string, StorageSecurityProfile> _profiles = new(StringComparer.OrdinalIgnoreCase);

    public StorageSecurityProfile GetOrCreateProfile(string accountName) =>
        _profiles.GetOrAdd(accountName, acc => new StorageSecurityProfile { AccountName = acc });

    /// <summary>
    /// 设置初始二级密码
    /// </summary>
    public (bool success, string message) SetInitialPassword(string accountName, string rawPassword)
    {
        if (string.IsNullOrWhiteSpace(rawPassword) || rawPassword.Length < 4)
        {
            return (false, "二级密码长度必须至少为 4 位。");
        }

        var profile = GetOrCreateProfile(accountName);
        if (profile.HasPassword)
        {
            return (false, "当前已设置二级密码，无法直接重置！请走修改密码流程。");
        }

        profile.PasswordHash = rawPassword; // 实际可带哈希
        profile.IsSessionUnlocked = true;
        profile.FailedAttempts = 0;
        return (true, "仓库二级密码设置成功并已自动解锁！");
    }

    /// <summary>
    /// 验证二级密码并解锁仓库 (防暴力破解)
    /// </summary>
    public (bool success, string message) VerifyAndUnlock(string accountName, string rawPassword, long nowMs)
    {
        var profile = GetOrCreateProfile(accountName);

        if (!profile.HasPassword)
        {
            profile.IsSessionUnlocked = true;
            return (true, "该账号未设置二级密码，默认开启仓库。");
        }

        if (nowMs < profile.LockedUntilTickMs)
        {
            long remainSec = Math.Max(1, (profile.LockedUntilTickMs - nowMs) / 1000);
            return (false, $"密码输错过多，仓库已被安全锁定！请等待 {remainSec / 60} 分 {remainSec % 60} 秒后重试。");
        }

        if (profile.PasswordHash.Equals(rawPassword, StringComparison.Ordinal))
        {
            profile.IsSessionUnlocked = true;
            profile.FailedAttempts = 0;
            profile.LockedUntilTickMs = 0;
            return (true, "密码验证成功！仓库已安全解锁。");
        }
        else
        {
            profile.FailedAttempts++;
            if (profile.FailedAttempts >= 3)
            {
                profile.LockedUntilTickMs = nowMs + 600_000; // 锁定 10 分钟
                return (false, "密码连续输错 3 次！为保护账号财产安全，仓库已被锁定 10 分钟！");
            }

            return (false, $"密码错误！剩余重试次数: {3 - profile.FailedAttempts} 次。");
        }
    }

    /// <summary>
    /// 修改二级密码
    /// </summary>
    public (bool success, string message) ChangePassword(string accountName, string oldPassword, string newPassword, long nowMs)
    {
        var profile = GetOrCreateProfile(accountName);
        if (!profile.HasPassword)
        {
            return SetInitialPassword(accountName, newPassword);
        }

        var (ok, verifyMsg) = VerifyAndUnlock(accountName, oldPassword, nowMs);
        if (!ok) return (false, $"原密码验证失败: {verifyMsg}");

        if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 4)
        {
            return (false, "新密码长度必须至少为 4 位。");
        }

        profile.PasswordHash = newPassword;
        profile.IsSessionUnlocked = true;
        profile.FailedAttempts = 0;
        return (true, "仓库二级密码修改成功！");
    }

    /// <summary>
    /// 检查是否允许支取仓库
    /// </summary>
    public bool CanAccessStorage(string accountName)
    {
        var profile = GetOrCreateProfile(accountName);
        return !profile.HasPassword || profile.IsSessionUnlocked;
    }
}
