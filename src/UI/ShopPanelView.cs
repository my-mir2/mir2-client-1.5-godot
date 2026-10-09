using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Net;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 商店买卖与特修面板 (纯 C# 实现)
/// 严格 1:1 对齐官方 Prguse_0385 (308x205)
/// </summary>
public partial class ShopPanelView : Control, IManagedWindow
{
    public enum ShopMode
    {
        Buy = 0,
        Sell = 1,
        Storage = 2
    }

    [Signal]
    public delegate void BuyRequestedEventHandler(int npcUid, int itemIdx, int count);

    [Signal]
    public delegate void TakeBackRequestedEventHandler(int storageSlot, string itemName);

    [Signal]
    public delegate void SellRequestedEventHandler(int npcUid, int itemUid);

    [Signal]
    public delegate void RepairRequestedEventHandler(int npcUid, int itemUid);

    [Signal]
    public delegate void RepairSpecialRequestedEventHandler(int npcUid, int itemUid);

    [Signal]
    public delegate void ItemSelectedForDetailEventHandler(string itemName);

    [Signal]
    public delegate void PanelClosedEventHandler();

    public const float PanelWidth = 308.0f;
    public const float PanelHeight = 205.0f;
    public const int PageSize = 10; // 官方固定每页 10 行 (IDA sub_48F020 / sub_48FC14)

    private int _npcUid = 0;
    private ShopMode _currentMode = ShopMode.Buy;
    private Panel? _panel;
    private Label? _lblHeaderList;
    private Label? _lblHeaderPrice;
    private Label? _lblHeaderDura;
    private VBoxContainer? _itemListContainer;
    private int _selectedIndex = -1;
    private int _pageIndex = 0;
    private readonly List<Panel> _itemRowPanels = new();
    private TextureButton? _btnPrev;
    private TextureButton? _btnNext;

    public ShopMode CurrentMode => _currentMode;

    public string WindowName => "ShopPanelView";
    public new bool IsVisible
    {
        get => Visible;
        set => Visible = value;
    }
    public (int x, int y, int width, int height) Bounds =>
        ((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);



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

        // 官方表头 sub_48F020 0x48f10b..0x48f181 & 0x48f37a: 物品列表(19,11)/价格(156,11)/持久(245,11) (mov eax, 0Bh=11)
        _lblHeaderList = new Label { Text = "物品列表", Position = new Vector2(19, 11) };
        _lblHeaderList.AddThemeFontSizeOverride("font_size", 12);
        _lblHeaderList.AddThemeColorOverride("font_color", Colors.White);
        _panel.AddChild(_lblHeaderList);

        _lblHeaderPrice = new Label { Text = "价格", Position = new Vector2(156, 11) };
        _lblHeaderPrice.AddThemeFontSizeOverride("font_size", 12);
        _lblHeaderPrice.AddThemeColorOverride("font_color", Colors.White);
        _panel.AddChild(_lblHeaderPrice);

        _lblHeaderDura = new Label { Text = "持久", Position = new Vector2(245, 11) };
        _lblHeaderDura.AddThemeFontSizeOverride("font_size", 12);
        _lblHeaderDura.AddThemeColorOverride("font_color", Colors.White);
        _panel.AddChild(_lblHeaderDura);

        // 官方原生商品列表展示区域 (IDA sub_48F020: 内胆 X: 10..295, Top: 32, 10 行 x 13px，左右留出内衬)
        _itemListContainer = new VBoxContainer
        {
            Name = "ShopItemListContainer",
            Position = new Vector2(10, 32),
            Size = new Vector2(285, 130),
            MouseFilter = MouseFilterEnum.Pass
        };
        _itemListContainer.AddThemeConstantOverride("separation", 0);
        _panel.AddChild(_itemListContainer);

        // 监听滚轮翻页支持 (IDA sub_48FC14/sub_48FCB4)
        _panel.GuiInput += OnPanelGuiInput;

        // IDA 铁证: 商店窗体按钮的绘制回调(0x490AD2..0x490B4F)为
        //   if (Sender.Downed(+274h)) { 画 (Sender+254h 图库, +258h 图号) 于按钮位置 }
        // 即 388/387/386 仅是"按下态叠加图"，常态完全不画，显露底板 Prguse_0385 自带的按钮；
        // 官方无悬停态、无禁用遮罩 → TextureNormal/TextureHover 均为 null，禁止任何自创遮罩。
        // 上一页 (Prguse_0388 @ 43,175 24x24, IDA sub_481dd4 [0x484fde..0x485011])
        var texPrev = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0388.png");
        _btnPrev = new TextureButton
        {
            Name = "BtnShopPrev",
            Position = new Vector2(43, 175),
            Size = new Vector2(24, 24),
            TextureNormal = null,
            TexturePressed = texPrev,
            TextureHover = null
        };
        _btnPrev.Pressed += OnPrevPage;
        _panel.AddChild(_btnPrev);

        // 下一页 (Prguse_0387 @ 90,175 24x24, IDA sub_481dd4 [0x485024..0x485057])
        var texNext = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0387.png");
        _btnNext = new TextureButton
        {
            Name = "BtnShopNext",
            Position = new Vector2(90, 175),
            Size = new Vector2(24, 24),
            TextureNormal = null,
            TexturePressed = texNext,
            TextureHover = null
        };
        _btnNext.Pressed += OnNextPage;
        _panel.AddChild(_btnNext);

        // 确定 (Prguse_0386 @ 215,171 44x16, IDA sub_481dd4 [0x48506a..0x48509d])
        var texBuy = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0386.png");
        var btnBuy = new TextureButton
        {
            Name = "BtnShopBuy",
            Position = new Vector2(215, 171),
            Size = new Vector2(44, 16),
            TextureNormal = null,
            TexturePressed = texBuy,
            TextureHover = null
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

    /// <summary>官方 1.5 货架下翻页 (SM_MERCHANT_PAGE 647, Recog > 0)</summary>
    public void PageNext() => OnNextPage();

    /// <summary>官方 1.5 货架上翻页 (SM_MERCHANT_PAGE 647, Recog < 0)</summary>
    public void PagePrev() => OnPrevPage();

    /// <summary>官方 1.5 货架直接跳页</summary>
    public void SetPage(int pageIndex)
    {
        if (pageIndex >= 0 && pageIndex * PageSize < _items.Count)
        {
            _pageIndex = pageIndex;
            RefreshPageDisplay();
        }
    }

    private void OnBuyCurrentSelection()
    {
        RequestBuyWithCount(1);
    }

    /// <summary>
    /// 盛大官方 1.5 商店批量购买 (支持按住 Ctrl 点击批量购买 1~99 份药水与耗材)
    /// </summary>
    public void RequestBuyWithCount(int count)
    {
        if (_selectedIndex >= 0 && _selectedIndex < _items.Count)
        {
            if (_currentMode == ShopMode.Storage)
            {
                EmitSignal(SignalName.TakeBackRequested, _selectedIndex, _items[_selectedIndex].Name);
            }
            else
            {
                int buyCount = Math.Clamp(count, 1, 99);
                EmitSignal(SignalName.BuyRequested, _npcUid, _selectedIndex, buyCount);
            }
        }
    }

    private MessageBoxDialog? GetOrFindMessageBox()
    {
        var hud = GetParent() as HudController;
        if (hud?.MessageBoxDialog != null) return hud.MessageBoxDialog;
        return GetTree().Root.FindChild("MessageBoxDialog", true, false) as MessageBoxDialog;
    }

    public void SetMode(ShopMode mode)
    {
        _currentMode = mode;
        // IDA sub_48F020: +775h(托管/列表标志)=0 → 表头 "物品列表"@19 / "价格"@156 / "持久"@245；
        //                 +775h≠0 → 表头 "托管物品列表"@19 / "持久"@156 / ""@245
        bool stored = mode == ShopMode.Storage;
        if (_lblHeaderList != null)
        {
            _lblHeaderList.Text = stored ? "托管物品列表" : "物品列表";
        }
        if (_lblHeaderPrice != null)
        {
            _lblHeaderPrice.Text = stored ? "持久" : "价格";
        }
        if (_lblHeaderDura != null)
        {
            _lblHeaderDura.Text = stored ? "" : "持久";
        }
    }

    public void OpenForStorage(IEnumerable<ShopItemEntry> items, int npcUid = 0)
    {
        _npcUid = npcUid;
        SetMode(ShopMode.Storage);
        SetItems(items);
        Visible = true;
    }

    /// <summary>
    /// 官方 1.5 仓库物品列表数据驱动开启 (SM_USERSTORAGE_ITEMS 704)
    /// </summary>
    public void OpenForStorage(IEnumerable<StorageItemInfo> items, int npcUid = 0)
    {
        var entries = new List<ShopItemEntry>();
        if (items != null)
        {
            foreach (var it in items)
            {
                entries.Add(ShopItemEntry.Stored(it.Name, it.Dura, it.MaxDura));
            }
        }
        OpenForStorage(entries, npcUid);
    }

    /// <summary>
    /// 清空仓库/货架列表 (SM_STORAGE_CLEARITEMS_DELPHI 709)
    /// </summary>
    public void ClearItems()
    {
        _items.Clear();
        _selectedIndex = -1;
        _pageIndex = 0;
        RefreshPageDisplay();
    }

    /// <summary>
    /// 取回成功后从列表中移除指定索引条目 (SM_TAKEBACKITEM_OK 705)
    /// </summary>
    public void RemoveItemAt(int index)
    {
        if (index >= 0 && index < _items.Count)
        {
            _items.RemoveAt(index);
            if (_selectedIndex >= _items.Count)
            {
                _selectedIndex = _items.Count - 1;
            }
            RefreshPageDisplay();
        }
    }

    /// <summary>
    /// 设置商品列表(来源:服务端商店消息解析结果,或 e2e 的 mock 数据)。
    /// </summary>
    public void SetItems(IEnumerable<ShopItemEntry> items)
    {
        _items.Clear();
        _items.AddRange(items);
        _selectedIndex = _items.Count > 0 ? 0 : -1;
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
                CustomMinimumSize = new Vector2(285, 13),
                Size = new Vector2(285, 13),
                MouseFilter = MouseFilterEnum.Stop
            };

            bool isSelected = (globalIdx == _selectedIndex);
            // 官方 IDA sub_48F020 @ 0x48f1ef..0x48f240 铁证：列表绝无横向背景拉伸色块！
            // 选中行文字 SetTextColor(hdc, 0xFF) 为鲜红色(RGB 255, 0, 0)，未选中为白色，背景完全透明
            row.AddThemeStyleboxOverride("panel", new StyleBoxEmpty());

            var rowColor = isSelected ? new Color(1.0f, 0.0f, 0.0f) : Colors.White;

            // 0. 官方选中指示符 (IDA sub_48F020 @ 0x48f201/0x48f467: 仅选中行在 X=12 绘制 #7 字符标记, 相对行内 X=2)
            var lblBullet = new Label
            {
                Position = new Vector2(2, 0),
                Size = new Vector2(7, 13),
                Text = isSelected ? "•" : "",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblBullet.AddThemeFontSizeOverride("font_size", 10);
            lblBullet.AddThemeColorOverride("font_color", new Color(1.0f, 0.0f, 0.0f));
            row.AddChild(lblBullet);

            // 1. 商品名称 (相对容器 X=10，文本相对行 X=9 即绝对 X=19 对齐表头, 官方 13px 行高)
            var lblName = new Label
            {
                Position = new Vector2(9, 0),
                Size = new Vector2(125, 13),
                Text = item.Name,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblName.AddThemeFontSizeOverride("font_size", 11);
            lblName.AddThemeColorOverride("font_color", rowColor);
            row.AddChild(lblName);

            bool storedMode = _currentMode == ShopMode.Storage;

            // 2. x=156 列 (行容器左偏移 10 → 行内相对 X=146，整行同色，左对齐)
            //    普通模式 IDA 0x48f2c1..0x48f303: TextOut(156, 32+13*行, 价格+"金币")
            //    托管模式 IDA 0x48f527..0x48f583: TextOut(156, 32+13*行, 当前持久+"/"+最大持久)
            var lblPrice = new Label
            {
                Position = new Vector2(146, 0),
                Size = new Vector2(88, 13),
                Text = storedMode ? $"{item.Dura}/{item.MaxDura}" : item.Price.ToString() + "金币",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            lblPrice.AddThemeFontSizeOverride("font_size", 11);
            lblPrice.AddThemeColorOverride("font_color", rowColor);
            row.AddChild(lblPrice);

            // 3. x=245 列 仅普通模式存在 (IDA 0x48f317..0x48f35e: TextOut(245, ...), 左对齐)，行内相对 X=235
            //    托管分支(0x48f43a..0x48f58e)没有该列
            if (!storedMode)
            {
                var lblWeight = new Label
                {
                    Position = new Vector2(235, 0),
                    Size = new Vector2(50, 13),
                    Text = item.Weight.ToString(),
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center
                };
                lblWeight.AddThemeFontSizeOverride("font_size", 11);
                lblWeight.AddThemeColorOverride("font_color", rowColor);
                row.AddChild(lblWeight);
            }

            row.GuiInput += (ev) =>
            {
                if (ev is InputEventMouseButton mb && mb.Pressed)
                {
                    if (mb.ButtonIndex == MouseButton.Left)
                    {
                        SelectRow(globalIdx);
                        if (mb.CtrlPressed && _currentMode == ShopMode.Buy)
                        {
                            var msgBox = GetOrFindMessageBox();
                            if (msgBox != null)
                            {
                                msgBox.ShowInput($"请输入购买 [{item.Name}] 的数量 (1 ~ 99)：", "1", (input) =>
                                {
                                    if (int.TryParse(input.Trim(), out var cnt) && cnt > 0)
                                    {
                                        RequestBuyWithCount(cnt);
                                    }
                                });
                            }
                            else
                            {
                                OnBuyCurrentSelection();
                            }
                        }
                        else if (mb.DoubleClick)
                        {
                            OnBuyCurrentSelection();
                        }
                    }
                }
            };

            _itemRowPanels.Add(row);
            _itemListContainer.AddChild(row);
        }

        // 官方 sub_48FC14/sub_48FCB4 仅调整 +5FCh 起始行并重绘，按钮无禁用/遮罩态，此处不做任何自创处理
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

            panel.AddThemeStyleboxOverride("panel", new StyleBoxEmpty());

            var nameLbl = panel.GetChildOrNull<Label>(0);
            if (nameLbl != null)
            {
                nameLbl.AddThemeColorOverride("font_color", selected ? new Color(1.0f, 0.25f, 0.25f) : Colors.White);
            }
            var priceLbl = panel.GetChildOrNull<Label>(1);
            if (priceLbl != null)
            {
                priceLbl.AddThemeColorOverride("font_color", selected ? new Color(1.0f, 0.25f, 0.25f) : Colors.White);
            }
            var weightLbl = panel.GetChildOrNull<Label>(2);
            if (weightLbl != null)
            {
                weightLbl.AddThemeColorOverride("font_color", selected ? new Color(1.0f, 0.25f, 0.25f) : Colors.White);
            }
        }

        // 官方 1.5 购买模式下单品详情查询 (CM_USERGETDETAILITEM 1013)
        if (globalIdx >= 0 && globalIdx < _items.Count && _currentMode == ShopMode.Buy)
        {
            EmitSignal(SignalName.ItemSelectedForDetail, _items[globalIdx].Name);
        }
    }

    public void OpenForNpc(int npcUid)
    {
        _npcUid = npcUid;
        _selectedIndex = _items.Count > 0 ? 0 : -1;
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
