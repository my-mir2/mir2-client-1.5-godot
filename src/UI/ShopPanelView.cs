using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 商店买卖与特修面板 (纯 C# 实现)
/// 严格 1:1 对齐官方 Prguse_0385 (308x205)
/// </summary>
public partial class ShopPanelView : Control, IManagedWindow
{
    [Signal]
    public delegate void BuyRequestedEventHandler(int npcUid, int itemIdx, int count);

    [Signal]
    public delegate void SellRequestedEventHandler(int npcUid, int itemUid);

    [Signal]
    public delegate void RepairRequestedEventHandler(int npcUid, int itemUid);

    [Signal]
    public delegate void RepairSpecialRequestedEventHandler(int npcUid, int itemUid);

    [Signal]
    public delegate void PanelClosedEventHandler();

    public const float PanelWidth = 308.0f;
    public const float PanelHeight = 205.0f;

    private int _npcUid = 0;
    private Panel? _panel;

    public string WindowName => "ShopPanelView";
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
        // 盛大官方 1.5 停靠位置 (IDA sub_481dd4 [0x484f98]: Left=138, Top=163)
        Position = new Vector2(138, 163);

        _panel = new Panel
        {
            Size = new Vector2(PanelWidth, PanelHeight),
            MouseFilter = MouseFilterEnum.Stop
        };

        var texBg = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0385.png");
        if (texBg != null)
        {
            _panel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texBg });
        }
        AddChild(_panel);

        // 官方原生关闭按钮 (Prguse_0064 @ 291, 0, IDA sub_481dd4 [0x4850b0..0x4850e0] 底板已烘焙凸起红叉)
        var texClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0064.png");
        var btnClose = new TextureButton
        {
            Name = "BtnShopClose",
            Position = new Vector2(291, 0),
            Size = new Vector2(16, 23),
            TextureNormal = null,
            TexturePressed = texClose
        };
        btnClose.Pressed += OnClose;
        _panel.AddChild(btnClose);

        // 官方原生商品列表展示区域 (Left: 12, Top: 32, Width: 284, Height: 135)
        var shopItemList = new Panel
        {
            Name = "ShopItemListContainer",
            Position = new Vector2(12, 32),
            Size = new Vector2(284, 135),
            MouseFilter = MouseFilterEnum.Stop
        };
        shopItemList.AddThemeStyleboxOverride("panel", new StyleBoxEmpty());
        _panel.AddChild(shopItemList);

        // 官方原生购买按钮 (Prguse_0388 @ 43, 175, 物理尺寸 24x24, IDA sub_481dd4 [0x484fde..0x485011])
        var texBuy = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0388.png");
        var btnBuy = new TextureButton
        {
            Name = "BtnShopBuy",
            Position = new Vector2(43, 175),
            Size = new Vector2(24, 24),
            TextureNormal = texBuy
        };
        btnBuy.Pressed += () => EmitSignal(SignalName.BuyRequested, _npcUid, 0, 1);
        _panel.AddChild(btnBuy);

        // 官方原生产出/出售按钮 (Prguse_0387 @ 90, 175, 物理尺寸 24x24, IDA sub_481dd4 [0x485024..0x485057])
        var texSell = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0387.png");
        var btnSell = new TextureButton
        {
            Name = "BtnShopSell",
            Position = new Vector2(90, 175),
            Size = new Vector2(24, 24),
            TextureNormal = texSell
        };
        btnSell.Pressed += () => EmitSignal(SignalName.SellRequested, _npcUid, 0);
        _panel.AddChild(btnSell);

        // 官方原生修理/特修按钮 (Prguse_0386 @ 215, 171, 物理尺寸 44x16, IDA sub_481dd4 [0x48506a..0x48509d])
        var texRepair = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0386.png");
        var btnRepair = new TextureButton
        {
            Name = "BtnShopRepair",
            Position = new Vector2(215, 171),
            Size = new Vector2(44, 16),
            TextureNormal = texRepair
        };
        btnRepair.Pressed += () => EmitSignal(SignalName.RepairSpecialRequested, _npcUid, 0);
        _panel.AddChild(btnRepair);

        // 官方原生商品翻页小箭头 (Prguse_0396 / Prguse_0398 @ 286, 42 与 286, 140)
        var texUp = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0396.png");
        var texDown = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0398.png");

        var btnUp = new TextureButton
        {
            Name = "BtnShopUp",
            Position = new Vector2(286, 42),
            Size = new Vector2(12, 21),
            TextureNormal = texUp
        };
        _panel.AddChild(btnUp);

        var btnDown = new TextureButton
        {
            Name = "BtnShopDown",
            Position = new Vector2(286, 140),
            Size = new Vector2(12, 21),
            TextureNormal = texDown
        };
        _panel.AddChild(btnDown);
    }

    public void OpenForNpc(int npcUid)
    {
        _npcUid = npcUid;
        Visible = true;
    }

    private void OnClose()
    {
        Visible = false;
        EmitSignal(SignalName.PanelClosed);
    }
}
