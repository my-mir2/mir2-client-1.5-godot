using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.4/1.5 千里传音与全服红字喊话引擎 (WorldBroadcastEngine)
/// 严格 1:1 对齐官方 M2Server 全服喇叭规则:
/// 1. 道具消耗:
///    - 消耗背包中 1 个官方 1.5 专属全服广播道具【千里传音】
/// 2. 频控保护:
///    - 单个玩家两次全服传音间隔不得低于 5000ms (5 秒)，杜绝脚本刷屏爆服
/// 3. 全网广播分发:
///    - 向服务器中所有在线会话下发高优先级全服置顶消息 (SM_SYSMSG_DELPHI)
/// </summary>
public sealed class WorldBroadcastEngine
{
    public const string BroadcastItemName = "千里传音";
    public const long CooldownMs = 5000;

    private readonly ConcurrentDictionary<string, long> _playerCooldowns = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// 触发一次全服千里传音
    /// </summary>
    public (bool success, EquipItem? consumedItem, string broadcastText, string errorMessage) BroadcastMessage(
        ClientSession sender,
        string messageContent,
        IEnumerable<ClientSession> allActiveSessions,
        long nowMs)
    {
        if (sender.State == null || string.IsNullOrWhiteSpace(messageContent))
        {
            return (false, null, string.Empty, "传音内容不可为空。");
        }

        // 1. 检查频控冷却
        if (_playerCooldowns.TryGetValue(sender.CharacterName, out long lastTime))
        {
            if (nowMs - lastTime < CooldownMs)
            {
                long remainSec = Math.Max(1, (CooldownMs - (nowMs - lastTime)) / 1000);
                return (false, null, string.Empty, $"千里传音冷却中，请在 {remainSec} 秒后再发！");
            }
        }

        // 2. 检查并扣除官方 1.5 原版全服广播道具【千里传音】
        int itemIdx = sender.State.BagItems.FindIndex(i => i.Name.Contains(BroadcastItemName));
        if (itemIdx < 0)
        {
            return (false, null, string.Empty, "背包中没有【千里传音】，无法向全服传音！");
        }

        var consumedItem = sender.State.BagItems[itemIdx];
        sender.State.BagItems.RemoveAt(itemIdx);
        _playerCooldowns[sender.CharacterName] = nowMs;

        // 3. 构建全服广播文本
        string broadcastText = $"[千里传音] {sender.CharacterName}: {messageContent.Trim()}";

        // 4. 广播至全服所有在线玩家
        int sentCount = 0;
        foreach (var target in allActiveSessions)
        {
            if (target.State != null)
            {
                target.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, broadcastText);
                sentCount++;
            }
        }

        return (true, consumedItem, broadcastText, string.Empty);
    }
}
