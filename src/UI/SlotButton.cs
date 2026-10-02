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

    public override Variant _GetDragData(Vector2 atPosition)
    {
        if (Icon == null) return new Variant();

        var preview = new Control();
        var iconRect = new TextureRect
        {
            Texture = Icon,
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
