using System;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 铁匠铺单件特修窗口 (DRepairDlg, 纯 C# 实现)
/// 严格 1:1 对齐官方 Prguse_0392 (140x181, 悬挂式铁砧)
/// </summary>
public partial class RepairPanelView : Control, IManagedWindow
{
    [Signal]
    public delegate void RepairConfirmedEventHandler(int itemUid, bool isSpecial);

    [Signal]
    public delegate void PanelClosedEventHandler();

    public const float PanelWidth = 140.0f;
    public const float PanelHeight = 181.0f;

    private Panel? _panel;

    public string WindowName => "RepairPanelView";
    public new bool IsVisible
    {
        get => Visible;
        set => Visible = value;
    }
    public (int x, int y, int width, int height) Bounds =>
        ((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    public override void _Ready()
    {
        Visible = false;
        Size = new Vector2(PanelWidth, PanelHeight);
        Position = new Vector2(138, 163);

        _panel = new Panel
        {
            Size = new Vector2(PanelWidth, PanelHeight),
            MouseFilter = MouseFilterEnum.Stop
        };

        var texBg = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0392.png");
        if (texBg != null)
        {
            _panel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texBg });
        }
        AddChild(_panel);

        // 官方原生关闭按钮 (Prguse_0064 @ 115, 0, IDA sub_481dd4 [0x48519f..0x4851cf] 底板已烘焙凸起红叉)
        var texClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0064.png");
        var btnClose = new TextureButton
        {
            Name = "BtnRepairClose",
            Position = new Vector2(115, 0),
            Size = new Vector2(16, 23),
            TextureNormal = null,
            TexturePressed = texClose
        };
        btnClose.Pressed += OnClose;
        _panel.AddChild(btnClose);

        // 官方 1.5 铁匠铺顶牌标题凹槽 (Left=6, Top=3, Width=105, Height=15)
        var lblTitle = new Label
        {
            Name = "LblRepairTitle",
            Text = "特殊修理",
            Position = new Vector2(6, 3),
            Size = new Vector2(105, 15),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        lblTitle.AddThemeFontSizeOverride("font_size", 10);
        lblTitle.AddThemeColorOverride("font_color", new Color(0.95f, 0.85f, 0.45f));
        _panel.AddChild(lblTitle);

        // 官方原生待修装备放置槽位 (Left=27, Top=67, Width=61, Height=52, IDA sub_481dd4 [0x4851d4..0x485215])
        var slotRepair = new Panel
        {
            Name = "RepairSlotContainer",
            Position = new Vector2(27, 67),
            Size = new Vector2(61, 52),
            MouseFilter = MouseFilterEnum.Stop
        };
        var emptyStyle = new StyleBoxEmpty();
        slotRepair.AddThemeStyleboxOverride("panel", emptyStyle);
        _panel.AddChild(slotRepair);

        // 槽位内部物品图标占位 (默认展示一把磨损待修的凝霜剑，消除黑洞死区)
        var repairSlotBtn = new SlotButton
        {
            Name = "RepairSlotBtn",
            CurrentSlotType = SlotButton.SlotType.Equip,
            SlotIndex = 0,
            Position = new Vector2(12, 8),
            CustomMinimumSize = new Vector2(36, 36),
            Size = new Vector2(36, 36),
            Flat = true,
            Icon = ResPath.LoadImageTexture("resources/wil/png/Items/Items_0001.png")
        };
        slotRepair.AddChild(repairSlotBtn);

        // 官方原生单件特修按钮 (Prguse_0393 @ 85, 150, 24x13, IDA sub_481dd4 [0x485159..0x48518c])
        var texRepair = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0393.png");
        var btnOk = new TextureButton
        {
            Name = "BtnRepairOk",
            Position = new Vector2(85, 150),
            Size = new Vector2(24, 13),
            TextureNormal = texRepair
        };
        btnOk.Pressed += OnRepair;
        _panel.AddChild(btnOk);
    }

    public void TogglePanel()
    {
        Visible = !Visible;
    }

    private void OnRepair()
    {
        Visible = false;
        EmitSignal(SignalName.RepairConfirmed, 0, true);
    }

    private void OnClose()
    {
        Visible = false;
        EmitSignal(SignalName.PanelClosed);
    }
}
