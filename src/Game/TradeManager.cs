// ============================================================================
// Mir2 客户端 - 面对面安全交易与锁定理 (1:1 盛大 1.5 官方实证)
// 包含两阶段锁定校验、防刷币防溢出、背包容量原子检查与物品金币互换
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

        public void UnlockTrade()
        {
            BreakTradeLockOnChange();
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

        /// <summary>
        /// 双方锁定后，进行第二阶段成交确认 (防刷币、容量与上限原子校验)
        /// </summary>
        public (bool success, string reason) ConfirmTrade(PlayerState player)
        {
            if (State != TradeState.Locked || !LocalLocked || !RemoteLocked)
            {
                return (false, "双方必须先全部锁定交易内容后，方可进行最终确认！");
            }

            // 1. 校验金币余额充足
            if (player.Gold < LocalGold)
            {
                BreakTradeLockOnChange();
                return (false, "自身携带金币不足以支付交易金额！");
            }

            // 2. 校验金币 1000 万上限防溢出（M2 sub_4DDC8C 0x989680 / Rust MAX_GOLD_CAP，对齐服务端权威值）
            if ((long)player.Gold - LocalGold + RemoteGold > PlayerState.MaxGoldLimit)
            {
                return (false, "交易完成后金币将超出单人 10,000,000 上限，无法完成交易！");
            }

            // 3. 校验背包剩余空格
            int remoteItemCount = 0;
            for (int i = 0; i < RemoteItems.Length; i++)
            {
                if (RemoteItems[i] != null) remoteItemCount++;
            }

            int availableBagSlots = 40 - player.BagItems.Count;
            if (availableBagSlots < remoteItemCount)
            {
                return (false, $"背包剩余空间不足 (仅剩 {availableBagSlots} 格，对方放入 {remoteItemCount} 件物品)！");
            }

            LocalConfirmed = true;
            State = TradeState.Confirmed;
            return (true, "本地已确认交易！等待服务端下发最终结算！");
        }

        /// <summary>
        /// 服务端确认交易成功，执行本地原子交换
        /// </summary>
        public (bool success, string reason) ExecuteAtomicExchange(PlayerState player)
        {
            if (!LocalConfirmed)
            {
                return (false, "未通过前置确认流程");
            }

            // 扣除本地放入的物品和金币
            player.Gold -= LocalGold;
            for (int i = 0; i < LocalItems.Length; i++)
            {
                var item = LocalItems[i];
                if (item != null)
                {
                    player.BagItems.RemoveAll(x => x.Uid == item.Uid);
                }
            }

            // 存入对方放入的物品和金币
            player.Gold += RemoteGold;
            for (int i = 0; i < RemoteItems.Length; i++)
            {
                var item = RemoteItems[i];
                if (item != null)
                {
                    player.BagItems.Add(item);
                }
            }

            CompleteTrade();
            return (true, "交易成功完成！物品与金币已入袋！");
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
