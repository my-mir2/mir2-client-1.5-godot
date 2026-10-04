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
            }
        }
    }

    public override void _Ready()
    {
        base._Ready();
        // 关键：采用独立的居中 TextureRect 渲染槽位物品图标，绝对禁用 Button.Icon！
        // 从而彻底杜绝 Godot Button 在设置 Icon 时由于贴图大于按钮尺寸而强行把按钮物理 Bounds 和调试框拉大的严重缺陷！
        Icon = null;
        ClipContents = true; // 官方 1:1 实证：槽位按钮硬裁剪，严禁贴图溢出槽位边框

        _iconRect = new TextureRect
        {
            Name = "__SlotIcon__",
            Texture = _itemTexture,
            Visible = _itemTexture != null,
            MouseFilter = MouseFilterEnum.Ignore,
            // 官方 1:1 实证：装备槽位图标严格不超出槽位矩形。
            // KeepAspectCentered = 保持宽高比居中缩放；ClipContents = 超出部分硬裁剪。
            // 官方 WIL 贴图（如死神手套 36x35）可能大于槽位框（34x31），必须裁剪。
            StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered,
            ClipContents = true,
            ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize
        };
        _iconRect.SetAnchorsPreset(LayoutPreset.FullRect);
        AddChild(_iconRect);
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
        return dict.ContainsKey("type") && dict["type"].AsString() == "item_drag";
    }

    public override void _DropData(Vector2 atPosition, Variant data)
    {
        if (!_CanDropData(atPosition, data)) return;
        var dict = data.AsGodotDictionary();
        int fromType = dict["from_type"].AsInt32();
        int fromSlot = dict["from_slot"].AsInt32();

        GD.Print($"[SlotButton] Drop item from ({fromType}:{fromSlot}) to ({(int)CurrentSlotType}:{SlotIndex})");
    }
}
