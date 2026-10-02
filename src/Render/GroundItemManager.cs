using System;
using System.Collections.Generic;
using Godot;

namespace Mir2Client.Render;

/// <summary>
/// 盛大官方 1.5 地表掉落物品管理器 (纯 C# 实现)
/// 严格 1:1 对齐官方 120 秒自然消散老化与 60 秒拾取归属保护期
/// </summary>
public partial class GroundItemManager : Node2D
{
    public sealed class GroundItem
    {
        public int Uid { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int Looks { get; set; }
        public int TileX { get; set; }
        public int TileY { get; set; }
        public float RemainingLife { get; set; } = 120.0f; // 120s 老化消失
        public string OwnerName { get; set; } = string.Empty;
        public float ProtectionTime { get; set; } = 60.0f; // 60s 归属保护
    }

    private readonly Dictionary<int, GroundItem> _items = new();

    public override void _Ready()
    {
        ZIndex = 5; // 在地面之上，在人物之下
    }

    public void AddItem(int uid, string itemName, int looks, int x, int y, string owner = "")
    {
        _items[uid] = new GroundItem
        {
            Uid = uid,
            ItemName = itemName,
            Looks = looks,
            TileX = x,
            TileY = y,
            OwnerName = owner
        };
    }

    public bool RemoveItem(int uid)
    {
        return _items.Remove(uid);
    }

    public bool CanPickup(int uid, string pickerName)
    {
        if (!_items.TryGetValue(uid, out var item)) return false;
        if (item.ProtectionTime <= 0.0f) return true; // 保护期已过，自由拾取
        return string.IsNullOrEmpty(item.OwnerName) || item.OwnerName == pickerName;
    }

    public override void _Process(double delta)
    {
        float dt = (float)delta;
        var expired = new List<int>();

        foreach (var (uid, item) in _items)
        {
            item.RemainingLife -= dt;
            if (item.ProtectionTime > 0.0f)
            {
                item.ProtectionTime = Math.Max(0.0f, item.ProtectionTime - dt);
            }
            if (item.RemainingLife <= 0.0f)
            {
                expired.Add(uid);
            }
        }

        foreach (int uid in expired)
        {
            _items.Remove(uid);
        }
    }
}
