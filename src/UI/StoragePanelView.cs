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

        // 金币数量显示内胆 (Left=96, Top=250, 140x18)
        var goldBox = new Panel
        {
            Name = "StorageGoldBox",
            Position = new Vector2(96, 250),
            Size = new Vector2(140, 18),
            MouseFilter = MouseFilterEnum.Ignore
        };
        goldBox.AddThemeStyleboxOverride("panel", new StyleBoxEmpty());
        _panel.AddChild(goldBox);

        _lblGold = new Label
        {
            Name = "LblStorageGold",
            Position = new Vector2(4, 0),
            Size = new Vector2(132, 18),
            Text = "0",
            VerticalAlignment = VerticalAlignment.Center
        };
        _lblGold.AddThemeFontSizeOverride("font_size", 11);
        _lblGold.AddThemeColorOverride("font_color", new Color(1.0f, 0.9f, 0.3f));
        goldBox.AddChild(_lblGold);

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
    }

    public void UpdateStorageGold(int gold)
    {
        if (_lblGold != null)
        {
            _lblGold.Text = gold.ToString("N0");
        }
    }

    private void OnDepositGoldClicked()
    {
        EmitSignal(SignalName.DepositGoldRequested, 10000);
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
