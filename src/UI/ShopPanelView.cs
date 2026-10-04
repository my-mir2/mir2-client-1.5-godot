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

        // 官方原生商品列表展示区域 (Left: 10, Top: 27, Width: 265, Height: 137，对应货架高光框内胆)
        _itemListContainer = new VBoxContainer
        {
            Name = "ShopItemListContainer",
            Position = new Vector2(0, 32),
            Size = new Vector2(307, 130),
            MouseFilter = MouseFilterEnum.Pass
        };
        _itemListContainer.AddThemeConstantOverride("separation", 0);
        // 官方表头 sub_48F020 0x48f10b..0x48f181: 物品列表(19,11)/价格(156,11)/持久(245,11),行 y=32+13*i
        foreach (var (txt, hx) in new[] { ("物品列表", 19), ("价格", 156), ("持久", 245) })
        {
            var hl = new Label { Text = txt, Position = new Vector2(hx, 11 - 3) };
            hl.AddThemeFontSizeOverride("font_size", 12);
            hl.AddThemeColorOverride("font_color", Colors.White);
            _panel.AddChild(hl);
        }
        _panel.AddChild(_itemListContainer);

        // 初始化官方标准药店/杂货铺商品列表
        PopulateDefaultShopItems();

        // 官方原生购买按钮 (Prguse_0388 @ 43, 175, 物理尺寸 24x24, IDA sub_481dd4 [0x484fde..0x485011])
        var texBuy = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0388.png");
        var btnBuy = new TextureButton
        {
            Name = "BtnShopBuy",
            Position = new Vector2(43, 175),
            Size = new Vector2(24, 24),
            TextureNormal = texBuy
        };
        btnBuy.Pressed += () => EmitSignal(SignalName.BuyRequested, _npcUid, _selectedIndex, 1);
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

    }

    private VBoxContainer? _itemListContainer;
    private int _selectedIndex = -1;
    private readonly List<Panel> _itemRowPanels = new();

    public struct ShopItemEntry
    {
        public string Name;
        public int Weight;
        public int Price;
        public ShopItemEntry(string name, int weight, int price)
        {
            Name = name;
            Weight = weight;
            Price = price;
        }
    }

    // 官方商品数据来自服务端,客户端无内置商品(原写死 7 件为非官方数据,已移除)
    private readonly List<ShopItemEntry> _items = new();

    private void PopulateDefaultShopItems()
    {
        if (_itemListContainer == null) return;
        foreach (var c in _itemListContainer.GetChildren()) c.QueueFree();
        _itemRowPanels.Clear();

        for (int i = 0; i < _items.Count; i++)
        {
            int idx = i;
            var item = _items[i];

            var row = new Panel
            {
                CustomMinimumSize = new Vector2(307, 13),
                MouseFilter = MouseFilterEnum.Stop
            };
            row.AddThemeStyleboxOverride("panel", idx == _selectedIndex 
                ? new StyleBoxFlat { BgColor = new Color(0.18f, 0.25f, 0.45f, 0.85f) } 
                : new StyleBoxEmpty());

            // 1. 商品名称 (Left=6, Width=120)
            var lblName = new Label
            {
                Position = new Vector2(19, -2),
                Size = new Vector2(130, 16),
                Text = item.Name,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblName.AddThemeFontSizeOverride("font_size", 11);
            lblName.AddThemeColorOverride("font_color", idx == _selectedIndex ? new Color(1.0f, 1.0f, 0.4f) : new Color(0.9f, 0.9f, 0.9f));
            row.AddChild(lblName);

            // 2. 重量 (Left=130, Width=40)
            var lblWeight = new Label
            {
                Position = new Vector2(245, -2),
                Size = new Vector2(40, 16),
                Text = item.Weight.ToString(),
                VerticalAlignment = VerticalAlignment.Center
            };
            lblWeight.AddThemeFontSizeOverride("font_size", 11);
            lblWeight.AddThemeColorOverride("font_color", new Color(0.75f, 0.75f, 0.75f));
            row.AddChild(lblWeight);

            // 3. 价格 (Left=175, Width=80)
            var lblPrice = new Label
            {
                Position = new Vector2(156, -2),
                Size = new Vector2(80, 16),
                Text = item.Price.ToString(),
                VerticalAlignment = VerticalAlignment.Center
            };
            lblPrice.AddThemeFontSizeOverride("font_size", 11);
            lblPrice.AddThemeColorOverride("font_color", new Color(1.0f, 0.85f, 0.3f));
            row.AddChild(lblPrice);

            row.GuiInput += (ev) =>
            {
                if (ev is InputEventMouseButton mb && mb.Pressed && mb.ButtonIndex == MouseButton.Left)
                {
                    SelectRow(idx);
                }
            };

            _itemRowPanels.Add(row);
            _itemListContainer.AddChild(row);
        }
    }

    private void SelectRow(int idx)
    {
        _selectedIndex = idx;
        for (int i = 0; i < _itemRowPanels.Count; i++)
        {
            var p = _itemRowPanels[i];
            bool selected = (i == _selectedIndex);
            p.AddThemeStyleboxOverride("panel", selected 
                ? new StyleBoxFlat { BgColor = new Color(0.18f, 0.25f, 0.45f, 0.85f) } 
                : new StyleBoxEmpty());
            var nameLbl = p.GetChildOrNull<Label>(0);
            if (nameLbl != null)
            {
                nameLbl.AddThemeColorOverride("font_color", selected ? new Color(1.0f, 1.0f, 0.4f) : new Color(0.9f, 0.9f, 0.9f));
            }
        }
    }

    public void OpenForNpc(int npcUid)
    {
        _npcUid = npcUid;
        SelectRow(-1);
        Visible = true;
    }

    private void OnClose()
    {
        Visible = false;
        EmitSignal(SignalName.PanelClosed);
    }
}
