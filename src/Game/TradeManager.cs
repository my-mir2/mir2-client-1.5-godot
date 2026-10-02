// ============================================================================
// Mir2 客户端 - 面对面安全交易与锁定理 (1:1 盛大 1.5 官方实证)
// ============================================================================

using System;
using System.Collections.Generic;

namespace Mir2Client.Game
{
    public enum TradeState
    {
        Idle,
        Trading,
        Locked,
        Confirmed
    }

    public class TradeManager
    {
        public TradeState State { get; private set; } = TradeState.Idle;
        public string PeerName { get; private set; } = "";
        public int PeerUid { get; private set; } = 0;

        public EquipItem?[] LocalItems { get; } = new EquipItem?[10];
        public EquipItem?[] RemoteItems { get; } = new EquipItem?[10];

        public int LocalGold { get; private set; } = 0;
        public int RemoteGold { get; private set; } = 0;

        public bool LocalLocked { get; private set; } = false;
        public bool RemoteLocked { get; private set; } = false;
        public bool LocalConfirmed { get; private set; } = false;
        public bool RemoteConfirmed { get; private set; } = false;

        public event Action<string>? TradeOpened;
        public event Action<string>? TradeClosed;
        public event Action<bool, bool>? TradeLockedChanged;
        public event Action? TradeCompleted;

        public void Reset()
        {
            State = TradeState.Idle;
            PeerName = "";
            PeerUid = 0;
            Array.Clear(LocalItems, 0, LocalItems.Length);
            Array.Clear(RemoteItems, 0, RemoteItems.Length);
            LocalGold = 0;
            RemoteGold = 0;
            LocalLocked = false;
            RemoteLocked = false;
            LocalConfirmed = false;
            RemoteConfirmed = false;
        }

        public bool StartTrade(string peer, int pUid = 0)
        {
            Reset();
            PeerName = peer;
            PeerUid = pUid;
            State = TradeState.Trading;
            TradeOpened?.Invoke(peer);
            return true;
        }

        public bool PutItem(int slot, EquipItem item)
        {
            if (State != TradeState.Trading || LocalLocked) return false;
            if (slot < 0 || slot >= LocalItems.Length) return false;
            LocalItems[slot] = item;
            BreakTradeLockOnChange();
            return true;
        }

        public bool TakeItem(int slot)
        {
            if (State != TradeState.Trading || LocalLocked) return false;
            if (slot < 0 || slot >= LocalItems.Length) return false;
            LocalItems[slot] = null;
            BreakTradeLockOnChange();
            return true;
        }

        public bool SetGold(int gold, int playerGold)
        {
            if (State != TradeState.Trading || LocalLocked) return false;
            if (gold < 0 || gold > playerGold) return false;
            LocalGold = gold;
            BreakTradeLockOnChange();
            return true;
        }

        public void RemoteAddItem(int slot, EquipItem item)
        {
            if (slot >= 0 && slot < RemoteItems.Length)
            {
                RemoteItems[slot] = item;
                BreakTradeLockOnChange();
            }
        }

        public void RemoteDelItem(int slot)
        {
            if (slot >= 0 && slot < RemoteItems.Length)
            {
                RemoteItems[slot] = null;
                BreakTradeLockOnChange();
            }
        }

        public void RemoteChangeGold(int gold)
        {
            RemoteGold = gold;
            BreakTradeLockOnChange();
        }

        public bool LockTrade()
        {
            if (State != TradeState.Trading) return false;
            LocalLocked = true;
            if (RemoteLocked) State = TradeState.Locked;
            TradeLockedChanged?.Invoke(true, true);
            return true;
        }

        public void SetRemoteLocked(bool locked)
        {
            RemoteLocked = locked;
            if (LocalLocked && RemoteLocked) State = TradeState.Locked;
            TradeLockedChanged?.Invoke(false, locked);
        }

        public void BreakTradeLockOnChange()
        {
            if (LocalLocked || RemoteLocked || LocalConfirmed || RemoteConfirmed)
            {
                LocalLocked = false;
                RemoteLocked = false;
                LocalConfirmed = false;
                RemoteConfirmed = false;
                State = TradeState.Trading;
                TradeLockedChanged?.Invoke(true, false);
                TradeLockedChanged?.Invoke(false, false);
            }
        }

        public void CancelTrade(string reason = "交易已取消")
        {
            State = TradeState.Idle;
            TradeClosed?.Invoke(reason);
            Reset();
        }

        public void CompleteTrade()
        {
            State = TradeState.Idle;
            TradeCompleted?.Invoke();
            Reset();
        }
    }
}
