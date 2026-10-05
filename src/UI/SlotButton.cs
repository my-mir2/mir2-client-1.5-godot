using System;
using Godot;

namespace Mir2Client.UI;

/// <summary>
/// 经典传奇 1.5 原版手感: 支持 Godot 4 标准 Drag & Drop 拖拽交互的槽位按钮 (纯 C# 实现)
/// </summary>
public partial class SlotButton : Button
{
    public enum SlotType
    {
        Bag = 0,
        Equip = 1,
        Trade = 2,
        Repair = 3,
        Storage = 4,
        Shop = 5,
        Skill = 6
    }

    [Export]
    public SlotType CurrentSlotType { get; set; } = SlotType.Bag;

    [Export]
    public int SlotIndex { get; set; } = 0;

    private TextureRect? _iconRect;
    private Texture2D? _itemTexture;

    public Texture2D? ItemTexture
    {
        get => _itemTexture;
        set
        {
            _itemTexture = value;
            if (_iconRect != null)
            {
                _iconRect.Texture = value;
                _iconRect.Visible = value != null;
                if (Size.X > 0 && Size.Y > 0)
                {
                    _iconRect.Size = Size;
                }
            }
        }
    }

    public override void _Ready()
    {
        base._Ready();
        // 关键：采用独立的居中 TextureRect 渲染槽位物品图标，绝对禁用 Button.Icon！
        Icon = null;
        ClipContents = true;

        Vector2 initialSize = Size.X > 0 && Size.Y > 0 ? Size : (CustomMinimumSize.X > 0 ? CustomMinimumSize : new Vector2(32, 32));
        _iconRect = new TextureRect
        {
            Name = "__SlotIcon__",
            Texture = _itemTexture,
            Visible = _itemTexture != null,
            MouseFilter = MouseFilterEnum.Ignore,
            StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
            ClipContents = true,
            ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
            Size = initialSize
        };
        _iconRect.SetAnchorsPreset(LayoutPreset.FullRect);
        AddChild(_iconRect);

        Resized += () =>
        {
            if (_iconRect != null && Size.X > 0 && Size.Y > 0)
            {
                _iconRect.Size = Size;
            }
        };
    }

    [Signal]
    public delegate void ItemDroppedEventHandler(int fromType, int fromSlot, int toType, int toSlot);

    public static event Action<SlotType, int, SlotType, int>? ItemDroppedGlobal;

    public override Variant _GetDragData(Vector2 atPosition)
    {
        var currentTex = _itemTexture ?? Icon;
        if (currentTex == null) return new Variant();

        var preview = new Control();
        var iconRect = new TextureRect
        {
            Texture = currentTex,
            Size = new Vector2(32, 32),
            Position = new Vector2(-16, -16),
            Modulate = new Color(1.0f, 1.0f, 1.0f, 0.85f)
        };
        preview.AddChild(iconRect);
        SetDragPreview(preview);

        var data = new Godot.Collections.Dictionary
        {
            { "type", "item_drag" },
            { "from_type", (int)CurrentSlotType },
            { "from_slot", SlotIndex }
        };

        return data;
    }

    public override bool _CanDropData(Vector2 atPosition, Variant data)
    {
        if (data.VariantType != Variant.Type.Dictionary) return false;
        var dict = data.AsGodotDictionary();
        if (!dict.ContainsKey("type") || dict["type"].AsString() != "item_drag") return false;

        int fromType = dict["from_type"].AsInt32();
        int fromSlot = dict["from_slot"].AsInt32();

        return CanDrop((SlotType)fromType, fromSlot, CurrentSlotType, SlotIndex);
    }

    /// <summary>
    /// 纯逻辑判断是否允许拖拽放置 (供单元测试与运行时双轨复用，避免纯 .NET 单测 native 崩溃)
    /// </summary>
    public static bool CanDrop(SlotType fromType, int fromSlot, SlotType toType, int toSlot)
    {
        // 避免自己拖给自己
        if (fromType == toType && fromSlot == toSlot)
        {
            return false;
        }

        return true;
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        if (!_CanDropData(atPosition, data)) return;
        var dict = data.AsGodotDictionary();
        int fromType = dict["from_type"].AsInt32();
        int fromSlot = dict["from_slot"].AsInt32();

        EmitSignal(SignalName.ItemDropped, fromType, fromSlot, (int)CurrentSlotType, SlotIndex);
        ItemDroppedGlobal?.Invoke((SlotType)fromType, fromSlot, CurrentSlotType, SlotIndex);
        GD.Print($"[SlotButton] Drop item from ({fromType}:{fromSlot}) to ({(int)CurrentSlotType}:{SlotIndex})");
    }
}
