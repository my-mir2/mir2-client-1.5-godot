// ============================================================================
// Mir2 客户端 - 大世界地表掉落物管理器 (GroundItemManager) (1:1 盛大 1.5 官方实证)
// 官方实证：IDA sub_46AEF0 & SM_GROUNDITEM (314) / SM_DELGROUNDITEM (315)
// ============================================================================

using System;
using System.Collections.Generic;

namespace Mir2Client.Game
{
    public class GroundItemManager
    {
        public sealed class GroundItemData
        {
            public int ItemId { get; set; }
            public string Name { get; set; } = string.Empty;
            public int TileX { get; set; }
            public int TileY { get; set; }
            public int Looks { get; set; }
            public int Amount { get; set; } = 1;
            public float RemainingLife { get; set; } = 120.0f; // 官方 120 秒自然消散老化
            public string OwnerName { get; set; } = string.Empty;
            public float ProtectionTime { get; set; } = 60.0f; // 官方 60 秒拾取归属保护期
        }

        public bool CanPickup(int itemId, string pickerName)
        {
            if (!_items.TryGetValue(itemId, out var item)) return false;
            if (item.ProtectionTime <= 0.0f) return true;
            return string.IsNullOrEmpty(item.OwnerName) || item.OwnerName == pickerName;
        }

        public event Action<GroundItemData>? ItemAdded;
        public event Action<int>? ItemRemoved;
        public event Action? ItemsCleared;

        private readonly Dictionary<int, GroundItemData> _items = new();

        public int Count => _items.Count;
        public IEnumerable<GroundItemData> AllItems => _items.Values;

        public void AddItem(int itemId, string name, int x, int y, int looks, int amount = 1)
        {
            var data = new GroundItemData
            {
                ItemId = itemId,
                Name = name,
                TileX = x,
                TileY = y,
                Looks = looks,
                Amount = amount
            };
            _items[itemId] = data;
            ItemAdded?.Invoke(data);
        }

        public bool RemoveItem(int itemId)
        {
            if (_items.Remove(itemId))
            {
                ItemRemoved?.Invoke(itemId);
                return true;
            }
            return false;
        }

        public List<GroundItemData> GetItemsAt(int tileX, int tileY)
        {
            var result = new List<GroundItemData>();
            foreach (var item in _items.Values)
            {
                if (item.TileX == tileX && item.TileY == tileY)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public GroundItemData? GetItem(int itemId)
        {
            return _items.TryGetValue(itemId, out var data) ? data : null;
        }

        public void Clear()
        {
            var ids = new List<int>(_items.Keys);
            _items.Clear();
            foreach (var id in ids)
            {
                ItemRemoved?.Invoke(id);
            }
            ItemsCleared?.Invoke();
        }
    }
}
