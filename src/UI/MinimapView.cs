using System;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 小地图雷达与全景大地图系统 (纯 C# 实现)
/// TAB 键三态轮转 (0:关闭, 1:常规 120x120, 2:大地图) 严格对齐 IDA sub_46FF0C
/// </summary>
public partial class MinimapView : Control, IManagedWindow
{
    public enum MinimapMode
    {
        Closed = 0,
        Mini = 1,
        Big = 2
    }

    public enum RadarEntityType
    {
        Player,
        Npc,
        Monster,
        GroupMember
    }

    [Signal]
    public delegate void ModeChangedEventHandler(int newMode);

    private MinimapMode _currentMode = MinimapMode.Closed;
    private TextureRect? _mapTexRect;
    private Panel? _bgPanel;
    private Label? _lblMapCoord;
    private ColorRect? _playerDot;
    private Control? _entityDotsContainer;
    private readonly System.Collections.Generic.List<ColorRect> _dotPool = new();
    private float _blinkTimer = 0.0f;
    private bool _blinkState = true;
    private string _mapName = "比奇省";
    private int _playerX = 330;
    private int _playerY = 270;

    public string WindowName => "MinimapView";
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
        Size = new Vector2(120, 120);
        Position = new Vector2(680, 0);

        _bgPanel = new Panel
        {
            Name = "MinimapBg",
            Size = new Vector2(120, 120),
            MouseFilter = MouseFilterEnum.Ignore
        };
        var sb = new StyleBoxFlat
        {
            BgColor = new Color(0.08f, 0.06f, 0.04f, 0.85f),
            BorderColor = new Color(0.6f, 0.5f, 0.3f, 0.95f),
            BorderWidthLeft = 2,
            BorderWidthTop = 2,
            BorderWidthRight = 2,
            BorderWidthBottom = 2
        };
        _bgPanel.AddThemeStyleboxOverride("panel", sb);
        AddChild(_bgPanel);

        // 官方 mmap 真实地图全景贴图 (比奇省 mmap_0000.png 600x400, IDA sub_46FF0C @ 0x46ff6a)
        var texMap = ResPath.LoadImageTexture("resources/wil/png/mmap/mmap_0000.png");
        _mapTexRect = new TextureRect
        {
            Name = "MapTexture",
            Texture = texMap,
            ExpandMode = TextureRect.ExpandModeEnum.IgnoreSize,
            StretchMode = TextureRect.StretchModeEnum.Scale,
            Size = new Vector2(116, 116),
            Position = new Vector2(2, 2),
            MouseFilter = MouseFilterEnum.Ignore
        };
        AddChild(_mapTexRect);

        _lblMapCoord = new Label
        {
            Name = "LblMapCoord",
            Position = new Vector2(4, 4),
            Size = new Vector2(112, 16),
            Text = $"{_mapName} ({_playerX},{_playerY})",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };
        _lblMapCoord.AddThemeFontSizeOverride("font_size", 9);
        _lblMapCoord.AddThemeColorOverride("font_color", new Color(1.0f, 0.95f, 0.6f));
        AddChild(_lblMapCoord);

        _playerDot = new ColorRect
        {
            Name = "PlayerDot",
            Position = new Vector2(58, 58),
            Size = new Vector2(4, 4),
            Color = new Color(0.2f, 1.0f, 0.3f, 1.0f) // 官方原生自身绿色闪烁点
        };
        AddChild(_playerDot);

        _entityDotsContainer = new Control
        {
            Name = "EntityDotsContainer",
            MouseFilter = MouseFilterEnum.Ignore
        };
        AddChild(_entityDotsContainer);

        GD.Print("[MinimapView] 小地图场景节点绑定就绪");
    }

    public void UpdatePlayerPosition(string mapName, int x, int y)
    {
        _mapName = mapName;
        _playerX = x;
        _playerY = y;
        if (_lblMapCoord != null)
        {
            _lblMapCoord.Text = $"{_mapName} ({_playerX},{_playerY})";
        }
    }

    public void UpdateRadarEntities(System.Collections.Generic.IEnumerable<(int tileX, int tileY, RadarEntityType type)> entities)
    {
        if (_currentMode == MinimapMode.Closed || _entityDotsContainer == null) return;

        // 小地图中心坐标：Mini 模式 (58, 58)，Big 模式 (178, 118)
        Vector2 center = _currentMode == MinimapMode.Mini ? new Vector2(58, 58) : new Vector2(178, 118);
        Vector2 bounds = _currentMode == MinimapMode.Mini ? new Vector2(116, 116) : new Vector2(356, 236);
        float scale = _currentMode == MinimapMode.Mini ? 1.5f : 1.0f; // 视野缩放

        int dotIdx = 0;
        foreach (var (tx, ty, type) in entities)
        {
            float dx = (tx - _playerX) * scale;
            float dy = (ty - _playerY) * scale;
            Vector2 dotPos = center + new Vector2(dx, dy);

            // 裁剪越界点
            if (dotPos.X < 2 || dotPos.X > bounds.X || dotPos.Y < 2 || dotPos.Y > bounds.Y)
                continue;

            Color dotColor = type switch
            {
                RadarEntityType.Npc => new Color(1.0f, 0.95f, 0.1f),      // NPC 亮黄点
                RadarEntityType.Monster => new Color(1.0f, 0.2f, 0.2f),  // 怪物红点
                RadarEntityType.GroupMember => new Color(0.2f, 0.6f, 1.0f), // 队友蓝点
                _ => new Color(1.0f, 1.0f, 1.0f)                          // 其他玩家白点
            };

            while (_dotPool.Count <= dotIdx)
            {
                var newDot = new ColorRect
                {
                    Size = new Vector2(3, 3),
                    MouseFilter = MouseFilterEnum.Ignore
                };
                _entityDotsContainer.AddChild(newDot);
                _dotPool.Add(newDot);
            }

            var dot = _dotPool[dotIdx];
            dot.Position = dotPos;
            dot.Color = dotColor;
            dot.Visible = true;
            dotIdx++;
        }

        // 隐藏未用到的点
        for (int i = dotIdx; i < _dotPool.Count; i++)
        {
            _dotPool[i].Visible = false;
        }
    }

    public override void _Process(double delta)
    {
        if (_currentMode == MinimapMode.Closed) return;

        _blinkTimer += (float)delta;
        if (_blinkTimer >= 0.3f) // 官方 300ms 闪烁 (IDA sub_46FF0C @ 0x46ff31)
        {
            _blinkTimer = 0.0f;
            _blinkState = !_blinkState;
            if (_playerDot != null)
            {
                _playerDot.Visible = _blinkState;
            }
        }
    }

    public void SetMode(MinimapMode mode)
    {
        _currentMode = mode;
        ApplyMode();
    }

    public void CycleMode()
    {
        _currentMode = (MinimapMode)(((int)_currentMode + 1) % 3);
        ApplyMode();
    }

    private void ApplyMode()
    {
        switch (_currentMode)
        {
            case MinimapMode.Closed:
                Visible = false;
                break;
            case MinimapMode.Mini:
                Visible = true;
                Size = new Vector2(120, 120);
                Position = new Vector2(680, 0);
                if (_bgPanel != null) _bgPanel.Size = new Vector2(120, 120);
                if (_mapTexRect != null)
                {
                    _mapTexRect.Position = new Vector2(2, 2);
                    _mapTexRect.Size = new Vector2(116, 116);
                }
                if (_lblMapCoord != null)
                {
                    _lblMapCoord.Position = new Vector2(4, 4);
                    _lblMapCoord.Size = new Vector2(112, 16);
                }
                if (_playerDot != null)
                {
                    _playerDot.Position = new Vector2(58, 58);
                    _playerDot.Visible = true;
                }
                break;
            case MinimapMode.Big:
                Visible = true;
                Size = new Vector2(360, 240);
                Position = new Vector2(220, 60);
                if (_bgPanel != null) _bgPanel.Size = new Vector2(360, 240);
                if (_mapTexRect != null)
                {
                    _mapTexRect.Position = new Vector2(2, 2);
                    _mapTexRect.Size = new Vector2(356, 236);
                }
                if (_lblMapCoord != null)
                {
                    _lblMapCoord.Position = new Vector2(10, 8);
                    _lblMapCoord.Size = new Vector2(340, 20);
                }
                if (_playerDot != null)
                {
                    _playerDot.Position = new Vector2(178, 118);
                    _playerDot.Visible = true;
                }
                break;
        }
        EmitSignal(SignalName.ModeChanged, (int)_currentMode);
    }
}
