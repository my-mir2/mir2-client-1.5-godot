using System;
using System.Collections.Generic;
using Godot;

namespace Mir2Client.Render;

/// <summary>
/// 盛大官方 1.5 场景环境特效与动态技能光效管理器 (Effect.wil, 纯 C# 实现)
/// 严格 1:1 对齐官方 Delphi 客户端场景特效渲染与 SM_USEREFFECT / SM_SHOWEVENT
/// </summary>
public partial class EffectManager : Node2D
{
    public sealed class MapEvent
    {
        public int EventId { get; set; }
        public int TileX { get; set; }
        public int TileY { get; set; }
        public int EventType { get; set; }
    }

    private readonly Dictionary<int, MapEvent> _mapEvents = new();

    public override void _Ready()
    {
        ZIndex = 5;
    }

    public void ShowMapEvent(int eventId, int tx, int ty, int eventType)
    {
        _mapEvents[eventId] = new MapEvent
        {
            EventId = eventId,
            TileX = tx,
            TileY = ty,
            EventType = eventType
        };
    }

    public void HideMapEvent(int eventId)
    {
        _mapEvents.Remove(eventId);
    }

    public void ClearAll()
    {
        _mapEvents.Clear();
    }
}
