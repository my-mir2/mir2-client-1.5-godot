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
        Skill = 6,
        Quick = 7
    }

    [Export]
    public SlotType CurrentSlotType { get; set; } = SlotType.Bag;

    [Export]
    public int SlotIndex { get; set; } = 0;

    private TextureRect? _iconRect;
    private Texture2D? _itemTexture;
    private ColorRect? _lowDuraBorder;
    private bool _lowDuraWarning = false;
    private double _flashTimer = 0.0;

    public bool LowDurabilityWarning
    {
        get => _lowDuraWarning;
        set
        {
            _lowDuraWarning = value;
            if (_lowDuraBorder != null)
            {
                _lowDuraBorder.Visible = value;
            }
        }
    }

    public bool HasItem => _itemTexture != null;

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
            }
        }
    }

    public override void _Ready()
    {
        base._Ready();
        // 关键：采用独立的居中 TextureRect 渲染槽位物品图标，绝对禁用 Button.Icon！
        // 官方 1.5 铁证 (IDA sub_48E458 / DItemGridGridPaint):
        // 官方原生使用 DirectDraw 透明拷贝 DirectPaint 绘制物品, 绝不做裁剪 (ClipRect) 或缩放拉伸!
        // 36x33 的标准格子里，长剑、屠龙裁决、重盔甲等 42x42 或 48x48 贴图会居中并自然溢出格子边框，
        // 呈现出原汁原味"物品比格子略大"的经典视觉表现。故 ClipContents 必须为 false。
        ClipContents = false;

        _iconRect = new TextureRect
        {
            Name = "__SlotIcon__",
            Texture = _itemTexture,
            Visible = _itemTexture != null,
            MouseFilter = MouseFilterEnum.Ignore,
            // 官方 1.5 铁证 (IDA sub_48E458): 原生 1:1 像素居中拷贝，大物品自然溢出，绝不拉伸与剪裁！
            StretchMode = TextureRect.StretchModeEnum.KeepCentered,
            ClipContents = false,
            ExpandMode = TextureRect.ExpandModeEnum.KeepSize
        };
        AddChild(_iconRect);
        _iconRect.SetAnchorsPreset(LayoutPreset.FullRect);
        _iconRect.OffsetLeft = 0;
        _iconRect.OffsetTop = 0;
        _iconRect.OffsetRight = 0;
        _iconRect.OffsetBottom = 0;

        // 官方低持久危急红色闪烁边框
        _lowDuraBorder = new ColorRect
        {
            Name = "__LowDuraBorder__",
            Color = new Color(1.0f, 0.15f, 0.15f, 0.4f),
            MouseFilter = MouseFilterEnum.Ignore,
            Visible = _lowDuraWarning
        };
        AddChild(_lowDuraBorder);
        _lowDuraBorder.SetAnchorsPreset(LayoutPreset.FullRect);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (_lowDuraWarning && _lowDuraBorder != null)
        {
            _flashTimer += delta;
            float alpha = (Mathf.Sin((float)_flashTimer * 8.0f) + 1.0f) * 0.25f + 0.15f;
            _lowDuraBorder.Color = new Color(1.0f, 0.15f, 0.15f, alpha);
        }
    }

    [Signal]
    public delegate void ItemDroppedEventHandler(int fromType, int fromSlot, int toType, int toSlot);

    public static event Action<SlotType, int, SlotType, int>? ItemDroppedGlobal;

    [Signal]
    public delegate void SlotDoubleClickedEventHandler(int slotType, int slotIndex);

    public static event Action<SlotType, int>? SlotDoubleClickedGlobal;

    public override void _GuiInput(InputEvent @event)
    {
        base._GuiInput(@event);
        if (@event is InputEventMouseButton mb && mb.ButtonIndex == MouseButton.Left && mb.DoubleClick && mb.Pressed)
        {
            TriggerDoubleClick();
            AcceptEvent();
        }
    }

    /// <summary>
    /// 触发双击逻辑（供运行时输入与单元测试双轨调用）
    /// </summary>
    public void TriggerDoubleClick()
    {
        EmitSignal(SignalName.SlotDoubleClicked, (int)CurrentSlotType, SlotIndex);
        RaiseSlotDoubleClicked(CurrentSlotType, SlotIndex);
        GD.Print($"[SlotButton] Double clicked slot ({CurrentSlotType}:{SlotIndex})");
    }

    public static void RaiseSlotDoubleClicked(SlotType slotType, int slotIndex)
    {
        SlotDoubleClickedGlobal?.Invoke(slotType, slotIndex);
    }

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
