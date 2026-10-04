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
    public const int PageSize = 10; // 官方固定每页 10 行 (IDA sub_48F020 / sub_48FC14)

    private int _npcUid = 0;
    private Panel? _panel;
    private VBoxContainer? _itemListContainer;
    private int _selectedIndex = -1;
    private int _pageIndex = 0;
    private readonly List<Panel> _itemRowPanels = new();

    public string WindowName => "ShopPanelView";
    public new bool IsVisible
    {
        get => Visible;
        set => Visible = value;
    }
    public (int x, int y, int width, int height) Bounds =>
        ((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

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

    private readonly List<ShopItemEntry> _items = new();

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

        // 官方表头 sub_48F020 0x48f10b..0x48f181: 物品列表(19,8)/价格(156,8)/持久(245,8)
        foreach (var (txt, hx) in new[] { ("物品列表", 19), ("价格", 156), ("持久", 245) })
        {
            var hl = new Label { Text = txt, Position = new Vector2(hx, 8) };
            hl.AddThemeFontSizeOverride("font_size", 12);
            hl.AddThemeColorOverride("font_color", Colors.White);
            _panel.AddChild(hl);
        }

        // 官方原生商品列表展示区域 (Left: 0, Top: 32, Width: 307, Height: 130，10 行 x 13px)
        _itemListContainer = new VBoxContainer
        {
            Name = "ShopItemListContainer",
            Position = new Vector2(0, 32),
            Size = new Vector2(307, 130),
            MouseFilter = MouseFilterEnum.Pass
        };
        _itemListContainer.AddThemeConstantOverride("separation", 0);
        _panel.AddChild(_itemListContainer);

        // 监听滚轮翻页支持 (IDA sub_48FC14/sub_48FCB4)
        _panel.GuiInput += OnPanelGuiInput;

        // 官方原生上一页按钮 (Prguse_0388 @ 43, 175, 物理尺寸 24x24, IDA sub_481dd4 [0x484fde..0x485011] & sub_48FC14)
        var texPrev = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0388.png");
        var btnPrev = new TextureButton
        {
            Name = "BtnShopPrev",
            Position = new Vector2(43, 175),
            Size = new Vector2(24, 24),
            TextureNormal = texPrev
        };
        btnPrev.Pressed += OnPrevPage;
        _panel.AddChild(btnPrev);

        // 官方原生下一页按钮 (Prguse_0387 @ 90, 175, 物理尺寸 24x24, IDA sub_481dd4 [0x485024..0x485057] & sub_48FCB4)
        var texNext = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0387.png");
        var btnNext = new TextureButton
        {
            Name = "BtnShopNext",
            Position = new Vector2(90, 175),
            Size = new Vector2(24, 24),
            TextureNormal = texNext
        };
        btnNext.Pressed += OnNextPage;
        _panel.AddChild(btnNext);

        // 官方原生确定购买按钮 (Prguse_0386 OK 标识 @ 215, 171, 物理尺寸 44x16, IDA sub_481dd4 [0x48506a..0x48509d] & sub_48FA80)
        var texBuy = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0386.png");
        var btnBuy = new TextureButton
        {
            Name = "BtnShopBuy",
            Position = new Vector2(215, 171),
            Size = new Vector2(44, 16),
            TextureNormal = texBuy
        };
        btnBuy.Pressed += OnBuyCurrentSelection;
        _panel.AddChild(btnBuy);
    }

    private void OnPanelGuiInput(InputEvent @event)
    {
        if (@event is InputEventMouseButton mb && mb.Pressed)
        {
            if (mb.ButtonIndex == MouseButton.WheelUp)
            {
                OnPrevPage();
            }
            else if (mb.ButtonIndex == MouseButton.WheelDown)
            {
                OnNextPage();
            }
        }
    }

    private void OnPrevPage()
    {
        if (_pageIndex > 0)
        {
            _pageIndex--;
            RefreshPageDisplay();
        }
    }

    private void OnNextPage()
    {
        if ((_pageIndex + 1) * PageSize < _items.Count)
        {
            _pageIndex++;
            RefreshPageDisplay();
        }
    }

    private void OnBuyCurrentSelection()
    {
        if (_selectedIndex >= 0 && _selectedIndex < _items.Count)
        {
            EmitSignal(SignalName.BuyRequested, _npcUid, _selectedIndex, 1);
        }
    }

    /// <summary>
    /// 设置商品列表(来源:服务端商店消息解析结果,或 e2e 的 mock 数据)。
    /// </summary>
    public void SetItems(IEnumerable<ShopItemEntry> items)
    {
        _items.Clear();
        _items.AddRange(items);
        _selectedIndex = -1;
        _pageIndex = 0;
        RefreshPageDisplay();
    }

    private void RefreshPageDisplay()
    {
        if (_itemListContainer == null) return;
        foreach (var c in _itemListContainer.GetChildren()) c.QueueFree();
        _itemRowPanels.Clear();

        int startIdx = _pageIndex * PageSize;
        int endIdx = Math.Min(startIdx + PageSize, _items.Count);

        for (int i = startIdx; i < endIdx; i++)
        {
            int globalIdx = i;
            var item = _items[globalIdx];

            var row = new Panel
            {
                CustomMinimumSize = new Vector2(307, 13),
                Size = new Vector2(307, 13),
                MouseFilter = MouseFilterEnum.Stop
            };

            bool isSelected = (globalIdx == _selectedIndex);
            row.AddThemeStyleboxOverride("panel", isSelected 
                ? new StyleBoxFlat { BgColor = new Color(0.18f, 0.25f, 0.45f, 0.85f) } 
                : new StyleBoxEmpty());

            // 1. 商品名称 (Left=19, Width=130, 官方 13px 行高)
            var lblName = new Label
            {
                Position = new Vector2(19, -2),
                Size = new Vector2(130, 16),
                Text = item.Name,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblName.AddThemeFontSizeOverride("font_size", 11);
            lblName.AddThemeColorOverride("font_color", isSelected ? new Color(1.0f, 1.0f, 0.4f) : new Color(0.9f, 0.9f, 0.9f));
            row.AddChild(lblName);

            // 2. 价格 (Left=156, Width=80)
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

            // 3. 重量/持久 (Left=245, Width=40)
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

            // 鼠标悬停高亮与点击选中
            row.MouseEntered += () =>
            {
                if (globalIdx != _selectedIndex)
                {
                    row.AddThemeStyleboxOverride("panel", new StyleBoxFlat { BgColor = new Color(0.25f, 0.35f, 0.55f, 0.35f) });
                }
            };
            row.MouseExited += () =>
            {
                if (globalIdx != _selectedIndex)
                {
                    row.AddThemeStyleboxOverride("panel", new StyleBoxEmpty());
                }
            };

            row.GuiInput += (ev) =>
            {
                if (ev is InputEventMouseButton mb && mb.Pressed)
                {
                    if (mb.ButtonIndex == MouseButton.Left)
                    {
                        if (mb.DoubleClick)
                        {
                            SelectRow(globalIdx);
                            OnBuyCurrentSelection();
                        }
                        else
                        {
                            SelectRow(globalIdx);
                        }
                    }
                }
            };

            _itemRowPanels.Add(row);
            _itemListContainer.AddChild(row);
        }
    }

    private void SelectRow(int globalIdx)
    {
        _selectedIndex = globalIdx;
        int startIdx = _pageIndex * PageSize;

        for (int p = 0; p < _itemRowPanels.Count; p++)
        {
            int curGlobalIdx = startIdx + p;
            var panel = _itemRowPanels[p];
            bool selected = (curGlobalIdx == _selectedIndex);

            panel.AddThemeStyleboxOverride("panel", selected 
                ? new StyleBoxFlat { BgColor = new Color(0.18f, 0.25f, 0.45f, 0.85f) } 
                : new StyleBoxEmpty());

            var nameLbl = panel.GetChildOrNull<Label>(0);
            if (nameLbl != null)
            {
                nameLbl.AddThemeColorOverride("font_color", selected ? new Color(1.0f, 1.0f, 0.4f) : new Color(0.9f, 0.9f, 0.9f));
            }
        }
    }

    public void OpenForNpc(int npcUid)
    {
        _npcUid = npcUid;
        _selectedIndex = -1;
        _pageIndex = 0;
        RefreshPageDisplay();
        Visible = true;
    }

    private void OnClose()
    {
        Visible = false;
        EmitSignal(SignalName.PanelClosed);
    }
}
