using System;
using System.Collections.Generic;

namespace Mir2Client.Game;

/// <summary>
/// 盛大官方 1.5 装备槽位乐观事务锁与原子回滚状态机 (纯 C# 实现，脱机零依赖)
/// 针对网络高延迟 (RTT > 500ms) 或频繁连点场景:
/// 1. 槽位操作发起时记录本地快照并加锁节流 (2000ms 防重入窗口)
/// 2. 收到服务端失败回执 (SM_TAKEON_FAIL 616 / SM_TAKEOFF_FAIL 620) 时计算并执行原子回滚
/// 3. 收到服务端成功确认 (SM_TAKEON_OK 615 / SM_TAKEOFF_OK 619) 时提交事务并解锁
/// </summary>
public sealed class EquipTransactionManager
{
    public enum ActionType
    {
        Wear,
        TakeOff
    }

    public struct TransactionSnapshot
    {
        public ActionType Type;
        public int BagSlot;
        public int EquipSlot;
        public string? BagItemName;
        public string? BagItemDesc;
        public string? EquipItemName;
        public string? EquipItemDesc;
        public long Timestamp;
    }

    private readonly Dictionary<int, TransactionSnapshot> _pendingActions = new();

    public int PendingCount => _pendingActions.Count;

    public bool HasPending(int equipSlot) => _pendingActions.ContainsKey(equipSlot);

    public TransactionSnapshot? GetPending(int equipSlot) =>
        _pendingActions.TryGetValue(equipSlot, out var s) ? s : null;

    /// <summary>
    /// 尝试发起穿戴事务，若该槽位已在挂起且未超时 (2000ms)，返回 false (防重入拦截)
    /// </summary>
    public bool TryBeginWear(int bagSlot, int equipSlot, string? bagName, string? bagDesc, string? curEquipName, string? curEquipDesc, long nowTick)
    {
        if (equipSlot < 0 || equipSlot >= 9) return false;
        if (_pendingActions.TryGetValue(equipSlot, out var existing) && nowTick - existing.Timestamp < 2000)
        {
            return false; // 防重入节流
        }

        _pendingActions[equipSlot] = new TransactionSnapshot
        {
            Type = ActionType.Wear,
            BagSlot = bagSlot,
            EquipSlot = equipSlot,
            BagItemName = bagName,
            BagItemDesc = bagDesc,
            EquipItemName = curEquipName,
            EquipItemDesc = curEquipDesc,
            Timestamp = nowTick
        };
        return true;
    }

    /// <summary>
    /// 尝试发起脱下事务，若该槽位已在挂起且未超时 (2000ms)，返回 false (防重入拦截)
    /// </summary>
    public bool TryBeginTakeOff(int equipSlot, int destBagSlot, string? equipName, string? equipDesc, long nowTick)
    {
        if (equipSlot < 0 || equipSlot >= 9) return false;
        if (_pendingActions.TryGetValue(equipSlot, out var existing) && nowTick - existing.Timestamp < 2000)
        {
            return false; // 防重入节流
        }

        _pendingActions[equipSlot] = new TransactionSnapshot
        {
            Type = ActionType.TakeOff,
            BagSlot = destBagSlot,
            EquipSlot = equipSlot,
            BagItemName = null,
            BagItemDesc = null,
            EquipItemName = equipName,
            EquipItemDesc = equipDesc,
            Timestamp = nowTick
        };
        return true;
    }

    /// <summary>
    /// 服务端成功确认 (SM_TAKEON_OK / SM_TAKEOFF_OK): 提交事务并解锁
    /// </summary>
    public void Commit(int equipSlot)
    {
        _pendingActions.Remove(equipSlot);
    }

    /// <summary>
    /// 服务端失败回执 (SM_TAKEON_FAIL 616 / SM_TAKEOFF_FAIL 620): 获取回滚快照并解锁
    /// </summary>
    public TransactionSnapshot? Rollback(int equipSlot = -1)
    {
        int targetSlot = equipSlot >= 0 && equipSlot < 9 ? equipSlot : -1;
        if (targetSlot == -1 && _pendingActions.Count > 0)
        {
            foreach (var key in _pendingActions.Keys)
            {
                targetSlot = key;
                break;
            }
        }

        if (targetSlot >= 0 && _pendingActions.TryGetValue(targetSlot, out var snapshot))
        {
            _pendingActions.Remove(targetSlot);
            return snapshot;
        }

        return null;
    }

    public void Clear()
    {
        _pendingActions.Clear();
    }
}
