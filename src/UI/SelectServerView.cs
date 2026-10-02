using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Audio;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 服务器分区选择石碑界面 (纯 C# 场景驱动控制器)
/// 严格 1:1 对齐官方二进制 MIR_final_v3_p.exe 逆向实证 (IDA sub_481DD4 @ 0x482606 - 0x482861)
/// 
/// 官方核心规范实证：
/// 1. 背景：ChrSel.wil[22] 800x600 神龛全景背景
/// 2. 石碑本体：Prguse2.wil[4] 单列双龙石碑 (404x452)，居中位于 (198, 74)
/// 3. 石碑关闭按钮：Prguse.wil[64] 16x23 @ (348, 31) (或 244, 30)
/// 4. 服务器槽位算法：
///    - 垂直居中基准：Y_base = 235 - (42 * num_servers) / 2
///    - 槽位间距：每项间隔 42 像素，Y = Y_base + 42 * index
///    - 水平居中：X = (404 - 168) / 2 = 118
/// 5. 官方石质切片按钮贴图：
///    - 常态：Prguse2.wil[2] (168x41)
///    - 悬浮/按下：Prguse2.wil[3] (168x41)
/// </summary>
public partial class SelectServerView : Control
{
    public const int OfficialTabletWidth = 404;
    public const int OfficialTabletHeight = 452;
    public const int OfficialSlotWidth = 168;
    public const int OfficialSlotHeight = 41;
    public const int OfficialSlotSpacing = 42;

    [Signal]
    public delegate void ServerSelectedEventHandler(int index);

    private readonly List<string> _servers = ["雷霆一区(电信)", "光芒二区(网通)", "烈焰三区(全通)"];
    private Panel? _panel;
    private Label? _lblInfo;
    private TextureButton? _btnClose;

    public override void _Ready()
    {
        // 1. 设置神龛全景背景贴图 (ChrSel.wil[22] 800x600)
        var bgNode = GetNodeOrNull<Control>("BG");
        var texBg = ResPath.LoadImageTexture("resources/wil/png/ChrSel/ChrSel_0022.png");
        if (texBg != null && bgNode != null)
        {
            var tr = new TextureRect
            {
                Name = "BGTex",
                Texture = texBg,
                Size = new Vector2(800, 600),
                MouseFilter = MouseFilterEnum.Ignore
            };
            AddChild(tr);
            MoveChild(tr, 0);
            bgNode.Visible = false;
        }

        // 2. 绑定场景中的石碑面板并配置官方双龙石碑纹理 (Prguse2.wil[4] 404x452 @ 198, 74)
        _panel = GetNodeOrNull<Panel>("Panel");
        if (_panel != null)
        {
            var texPanel = ResPath.LoadImageTexture("resources/wil/png/Prguse2/Prguse2_0004.png");
            if (texPanel != null)
            {
                _panel.AddThemeStyleboxOverride("panel", new StyleBoxTexture { Texture = texPanel });
                _panel.Position = new Vector2(198, 74);
                _panel.Size = new Vector2(OfficialTabletWidth, OfficialTabletHeight);
            }

            // 绑定关闭按钮 (Prguse.wil[64] 16x23 @ 348, 31)
            var texClose = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0064.png");
            _btnClose = new TextureButton
            {
                Name = "BtnClose",
                Position = new Vector2(348, 31),
                Size = new Vector2(16, 23),
                TextureNormal = texClose
            };
            _btnClose.Pressed += () => GetTree().Quit();
            _panel.AddChild(_btnClose);
        }

        _lblInfo = GetNodeOrNull<Label>("Panel/LblInfo");

        // 3. 按照官方 IDA 公式动态渲染服务器按钮槽位 (sub_481DD4 line 864)
        if (_panel != null)
        {
            int numServers = _servers.Count;
            int startY = CalculateServerStartY(numServers);
            int startX = (OfficialTabletWidth - OfficialSlotWidth) / 2; // 118

            var texNormal = ResPath.LoadImageTexture("resources/wil/png/Prguse2/Prguse2_0002.png");
            var texHover = ResPath.LoadImageTexture("resources/wil/png/Prguse2/Prguse2_0003.png");

            for (int i = 0; i < numServers; i++)
            {
                int idx = i;
                int slotY = startY + i * OfficialSlotSpacing;

                var btn = new TextureButton
                {
                    Name = $"BtnServer_{idx}",
                    Position = new Vector2(startX, slotY),
                    Size = new Vector2(OfficialSlotWidth, OfficialSlotHeight),
                    TextureNormal = texNormal,
                    TextureHover = texHover,
                    TexturePressed = texHover
                };

                var lbl = new Label
                {
                    Name = "Title",
                    Text = _servers[i],
                    Size = new Vector2(OfficialSlotWidth, OfficialSlotHeight),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    MouseFilter = MouseFilterEnum.Ignore
                };
                lbl.AddThemeColorOverride("font_color", new Color(1, 1, 1, 1));
                lbl.AddThemeColorOverride("font_shadow_color", new Color(0, 0, 0, 1));
                lbl.AddThemeConstantOverride("shadow_offset_x", 1);
                lbl.AddThemeConstantOverride("shadow_offset_y", 1);
                lbl.AddThemeFontSizeOverride("font_size", 12);

                btn.AddChild(lbl);
                btn.Pressed += () => OnServerClicked(idx);
                _panel.AddChild(btn);
            }
        }

        GD.Print("[SelectServerView] 官方 1.5 双龙石碑与 Prguse2[2..4] 槽位贴图全量绑定就绪");
    }

    /// <summary>
    /// 官方 1.5 单列石碑垂直居中公式实证 (sub_481DD4 line 864):
    /// Y_base = 235 - ((42 * num_servers) >> 1)
    /// </summary>
    public static int CalculateServerStartY(int count)
    {
        return 235 - ((OfficialSlotSpacing * count) >> 1);
    }

    private void OnServerClicked(int index)
    {
        GD.Print($"[SelectServerView] 选服: index={index}, name={_servers[index]}");
        AudioManager.Instance?.PlaySoundId(101);

        if (_lblInfo != null)
        {
            _lblInfo.Text = $"正在连接服务器: {_servers[index]}...";
        }
        EmitSignal(SignalName.ServerSelected, index);
    }
}
