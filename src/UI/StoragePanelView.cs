using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 仓库保管箱界面 (DStorageDlg, 纯 C# 实现)
/// 严格 1:1 对齐官方标准大底板 Prguse_0402 (416x347 @ 192, 126, IDA sub_481dd4)
/// 40 格独立存储 (8列5行) 与金币存取、原生金属关闭红叉 (398, 4, 16x20)
/// </summary>
public partial class StoragePanelView : Control, IManagedWindow
{
    [Signal]
    public delegate void DepositRequestedEventHandler(int bagSlot);

    [Signal]
    public delegate void WithdrawRequestedEventHandler(int storageSlot);

    [Signal]
    public delegate void DepositGoldRequestedEventHandler(int amount);

    [Signal]
    public delegate void WithdrawGoldRequestedEventHandler(int amount);

    [Signal]
    public delegate void PanelClosedEventHandler();

    public const float PanelWidth = 416.0f;
    public const float PanelHeight = 347.0f;

    private Panel? _panel;
    private GridContainer? _gridContainer;
    private Label? _lblGold;

    public string WindowName => "StoragePanelView";
    public Official24DialogCompendium.DialogId DialogId => Official24DialogCompendium.DialogId.StoragePanel;
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
        // 盛大官方 1.5 IDA sub_481dd4 实证停靠位置: (192, 126)
        Position = new Vector2(192, 126);

        _panel = new Panel
        {
            Size = new Vector2(PanelWidth, PanelHeight),
            MouseFilter = MouseFilterEnum.Stop
        };

        var texBg = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0402.png");
        if (texBg != null)
        {
            _panel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texBg });
        }
        AddChild(_panel);

        // 官方原生右上角关闭按钮 (底板 Prguse_0402 在 (398, 4) 处已自带烘焙凸起红叉，TextureNormal = null，按下显示凹陷 Prguse_0064)
        var texClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0064.png");
        var btnClose = new TextureButton
        {
            Name = "BtnStorageClose",
            Position = new Vector2(398, 4),
            Size = new Vector2(16, 20),
            TextureNormal = null,
            TexturePressed = texClose
        };
        btnClose.Pressed += OnClose;
        _panel.AddChild(btnClose);

        // 官方 40 格独立存储物品网格 (8列5行，单格 36x33，Left=64, Top=52, 288x165)
        _gridContainer = new GridContainer
        {
            Name = "StorageGridContainer",
            Position = new Vector2(64, 52),
            Size = new Vector2(288, 165),
            Columns = 8
        };
        _gridContainer.AddThemeConstantOverride("h_separation", 0);
        _gridContainer.AddThemeConstantOverride("v_separation", 0);
        _panel.AddChild(_gridContainer);

        for (int i = 0; i < 40; i++)
        {
            int slotIdx = i;
            var slot = new SlotButton
            {
                Name = $"StorageSlot_{slotIdx}",
                CurrentSlotType = SlotButton.SlotType.Bag,
                SlotIndex = slotIdx,
                CustomMinimumSize = new Vector2(36, 33),
                Flat = true
            };
            slot.Pressed += () => EmitSignal(SignalName.WithdrawRequested, slotIdx);
            _gridContainer.AddChild(slot);
        }

        // 金币图标 (Prguse_0028 36x21 @ 50, 248)
        var texGold = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0028.png");
        var goldIcon = new TextureRect
        {
            Name = "StorageGoldIcon",
            Position = new Vector2(50, 248),
            Size = new Vector2(36, 21),
            Texture = texGold
        };
        _panel.AddChild(goldIcon);

        // 金币数量显示内胆 (Prguse_0402 物理凹槽 Left=96, Top=248..268, 140x20)
        _lblGold = new Label
        {
            Name = "LblStorageGold",
            Position = new Vector2(96, 248),
            Size = new Vector2(140, 20),
            Text = "0",
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center
        };
        _lblGold.AddThemeFontSizeOverride("font_size", 11);
        _lblGold.AddThemeColorOverride("font_color", new Color(1.0f, 0.9f, 0.3f));
        _panel.AddChild(_lblGold);

        // 存入金币按钮 (Prguse_0062 76x33 @ 250, 242)
        var texConfirm = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0062.png");
        var btnDepositGold = new TextureButton
        {
            Name = "BtnDepositGold",
            Position = new Vector2(250, 242),
            Size = new Vector2(76, 33),
            TextureNormal = texConfirm
        };
        btnDepositGold.Pressed += OnDepositGoldClicked;
        _panel.AddChild(btnDepositGold);

        // 取出金币按钮 (Prguse_0060 76x33 @ 330, 242)
        var texWithdraw = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0060.png") ?? texConfirm;
        var btnWithdrawGold = new TextureButton
        {
            Name = "BtnWithdrawGold",
            Position = new Vector2(330, 242),
            Size = new Vector2(76, 33),
            TextureNormal = texWithdraw
        };
        btnWithdrawGold.Pressed += OnWithdrawGoldClicked;
        _panel.AddChild(btnWithdrawGold);
    }

    public const int MaxStorageGold = 50_000_000; // 盛大官方 5000 万金币安全上限

    private int _currentPage = 0;
    private int _currentStorageGold = 0;
    public int CurrentPage => _currentPage;
    public int CurrentStorageGold => _currentStorageGold;

    public void UpdateStorageGold(int gold)
    {
        _currentStorageGold = Math.Clamp(gold, 0, MaxStorageGold);
        if (_lblGold != null)
        {
            _lblGold.Text = _currentStorageGold.ToString("N0");
        }
    }

    public bool CheckDepositGoldValid(int depositAmount, out string reason)
    {
        if (depositAmount <= 0)
        {
            reason = "存入金币金额必须大于 0！";
            return false;
        }
        if ((long)_currentStorageGold + depositAmount > MaxStorageGold)
        {
            reason = $"仓库金币已达上限 ({MaxStorageGold:N0})，无法继续存放！";
            return false;
        }
        reason = string.Empty;
        return true;
    }

    public void SetPage(int page)
    {
        _currentPage = Math.Clamp(page, 0, 1);
        // 刷新当前页 40 格物品显示偏移
        GD.Print($"[StoragePanelView] 切换仓库页码至: 第 {_currentPage + 1} 页 (格 {_currentPage * 40}..{_currentPage * 40 + 39})");
    }

    private MessageBoxDialog? GetOrFindMessageBox()
    {
        var hud = GetParent() as HudController;
        if (hud?.MessageBoxDialog != null) return hud.MessageBoxDialog;
        return GetTree()?.Root.FindChild("MessageBoxDialog", true, false) as MessageBoxDialog;
    }

    private void OnDepositGoldClicked()
    {
        var msgBox = GetOrFindMessageBox();
        if (msgBox != null)
        {
            msgBox.ShowNumericInput("请输入你要存入保管箱的金币金额：", MaxStorageGold, 10000, amount =>
            {
                if (CheckDepositGoldValid(amount, out string reason))
                {
                    EmitSignal(SignalName.DepositGoldRequested, amount);
                }
                else
                {
                    msgBox.ShowMessage(reason, false);
                }
            });
        }
        else
        {
            if (CheckDepositGoldValid(10000, out string _))
            {
                EmitSignal(SignalName.DepositGoldRequested, 10000);
            }
        }
    }

    public bool CheckWithdrawGoldValid(int withdrawAmount, out string reason)
    {
        if (withdrawAmount <= 0)
        {
            reason = "取出金币金额必须大于 0！";
            return false;
        }
        if (_currentStorageGold < withdrawAmount)
        {
            reason = "保管箱现有金币不足！";
            return false;
        }
        reason = string.Empty;
        return true;
    }

    private void OnWithdrawGoldClicked()
    {
        var msgBox = GetOrFindMessageBox();
        if (msgBox != null)
        {
            msgBox.ShowNumericInput("请输入你要从保管箱取出的金币金额：", _currentStorageGold, Math.Min(10000, _currentStorageGold), amount =>
            {
                if (CheckWithdrawGoldValid(amount, out string reason))
                {
                    EmitSignal(SignalName.WithdrawGoldRequested, amount);
                }
                else
                {
                    msgBox.ShowMessage(reason, false);
                }
            });
        }
        else
        {
            if (CheckWithdrawGoldValid(10000, out string _))
            {
                EmitSignal(SignalName.WithdrawGoldRequested, 10000);
            }
        }
    }

    public void Open()
    {
        Visible = true;
    }

    private void OnClose()
    {
        Visible = false;
        EmitSignal(SignalName.PanelClosed);
    }
}
