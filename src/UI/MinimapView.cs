using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Game;
using Mir2Client.Utils;

namespace Mir2Client.UI;

/// <summary>
/// 盛大官方 1.5 小地图雷达与全景大地图系统 (纯 C# 工业级实现)
/// 严格 1:1 对齐官方 Delphi MIR_final_v3_p.exe sub_46FF0C 与 ClMain.pas (540x360 大地图与 120x120 小地图双模)
/// 支持 TAB/V 键三态轮转 (0:关闭, 1:常规 120x120, 2:大地图 540x360)、鼠标悬停坐标嗅探、点击全局 A* 自动寻路与目标光标
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

    [Signal]
    public delegate void MapTargetSelectedEventHandler(int targetX, int targetY);

    private MinimapMode _currentMode = MinimapMode.Closed;
    private Panel? _bgPanel;
    private TextureRect? _mapTexRect;
    private Label? _lblMapCoord;
    private Label? _lblHoverCoord;
    private TextureButton? _btnClose;
    private ColorRect? _playerDot;
    private ColorRect? _targetMarker;
    private Control? _entityDotsContainer;
    private Control? _landmarksContainer;
    private readonly List<ColorRect> _dotPool = new();
    private readonly List<Label> _landmarkLabels = new();
    private readonly List<ColorRect> _landmarkMarkers = new();

    private float _blinkTimer = 0.0f;
    private bool _blinkState = true;
    private string _mapName = "比奇省";
    private string _mapId = "0";
    private int _playerX = 330;
    private int _playerY = 270;
    private int _hoverX = -1;
    private int _hoverY = -1;
    private int _targetX = -1;
    private int _targetY = -1;

    // 官方地图尺寸（瓦片数，默认比奇省 400x400）
    private int _mapWidthTiles = 400;
    private int _mapHeightTiles = 400;

    public string WindowName => "MinimapView";
    public new bool IsVisible
    {
        get => Visible;
        set
        {
            Visible = value;
            if (!value) _currentMode = MinimapMode.Closed;
        }
    }
    public (int x, int y, int width, int height) Bounds =>
        ((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

    public MinimapMode CurrentMode => _currentMode;

    public override void _Ready()
    {
        Visible = false;
        Size = new Vector2(120, 120);
        Position = new Vector2(680, 0);
        MouseFilter = MouseFilterEnum.Stop;

        _bgPanel = new Panel
        {
            Name = "MinimapBg",
            Size = new Vector2(120, 120),
            MouseFilter = MouseFilterEnum.Ignore
        };
        var sb = new StyleBoxFlat
        {
            BgColor = new Color(0.06f, 0.05f, 0.04f, 0.90f),
            BorderColor = new Color(0.65f, 0.55f, 0.35f, 0.95f),
            BorderWidthLeft = 2,
            BorderWidthTop = 2,
            BorderWidthRight = 2,
            BorderWidthBottom = 2,
            CornerRadiusTopLeft = 2,
            CornerRadiusTopRight = 2,
            CornerRadiusBottomLeft = 2,
            CornerRadiusBottomRight = 2
        };
        _bgPanel.AddThemeStyleboxOverride("panel", sb);
        AddChild(_bgPanel);

        // 官方 mmap 真实地图全景贴图 (比奇省 mmap_0000.png 600x400, IDA sub_46FF0C @ 0x46ff6a)
        var texMap = LoadMapTexture(_mapId);
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
            VerticalAlignment = VerticalAlignment.Center,
            MouseFilter = MouseFilterEnum.Ignore
        };
        _lblMapCoord.AddThemeFontSizeOverride("font_size", 9);
        _lblMapCoord.AddThemeColorOverride("font_color", new Color(1.0f, 0.95f, 0.6f));
        AddChild(_lblMapCoord);

        _lblHoverCoord = new Label
        {
            Name = "LblHoverCoord",
            Position = new Vector2(10, 335),
            Size = new Vector2(300, 20),
            Text = "",
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Center,
            Visible = false,
            MouseFilter = MouseFilterEnum.Ignore
        };
        _lblHoverCoord.AddThemeFontSizeOverride("font_size", 10);
        _lblHoverCoord.AddThemeColorOverride("font_color", new Color(0.4f, 1.0f, 0.8f));
        AddChild(_lblHoverCoord);

        // 官方右上角关闭红叉按钮 (Prguse_0064 16x23)
        var closeTex = ResPath.LoadImageTexture("resources/wil/png/Prguse/Prguse_0064.png");
        _btnClose = new TextureButton
        {
            Name = "BtnClose",
            TextureNormal = closeTex,
            Position = new Vector2(518, 4),
            Size = new Vector2(16, 23),
            Visible = false
        };
        _btnClose.Pressed += () => SetMode(MinimapMode.Closed);
        AddChild(_btnClose);

        _playerDot = new ColorRect
        {
            Name = "PlayerDot",
            Position = new Vector2(58, 58),
            Size = new Vector2(4, 4),
            Color = new Color(0.2f, 1.0f, 0.3f, 1.0f), // 官方原生自身绿色闪烁点
            MouseFilter = MouseFilterEnum.Ignore
        };
        AddChild(_playerDot);

        _targetMarker = new ColorRect
        {
            Name = "TargetMarker",
            Position = new Vector2(-10, -10),
            Size = new Vector2(5, 5),
            Color = new Color(1.0f, 0.2f, 0.2f, 0.9f), // 目标寻路红色光点
            Visible = false,
            MouseFilter = MouseFilterEnum.Ignore
        };
        AddChild(_targetMarker);

        _entityDotsContainer = new Control
        {
            Name = "EntityDotsContainer",
            MouseFilter = MouseFilterEnum.Ignore
        };
        AddChild(_entityDotsContainer);

        _landmarksContainer = new Control
        {
            Name = "LandmarksContainer",
            MouseFilter = MouseFilterEnum.Ignore,
            Visible = false
        };
        AddChild(_landmarksContainer);

        GD.Print("[MinimapView] 官方 1.5 双模地图场景与交互引擎就绪");
    }

    private Texture2D? LoadMapTexture(string mapId)
    {
        string formattedIndex = mapId.PadLeft(4, '0');
        string pngPath = $"resources/wil/png/mmap/mmap_{formattedIndex}.png";
        var tex = ResPath.LoadImageTexture(pngPath);
        if (tex == null)
        {
            // 回退到默认比奇省大地图
            tex = ResPath.LoadImageTexture("resources/wil/png/mmap/mmap_0000.png");
        }
        return tex;
    }

    public void SetMap(string mapId, string mapName, int widthTiles = 400, int heightTiles = 400)
    {
        _mapId = mapId;
        _mapName = mapName;
        _mapWidthTiles = widthTiles > 0 ? widthTiles : 400;
        _mapHeightTiles = heightTiles > 0 ? heightTiles : 400;

        if (_mapTexRect != null)
        {
            _mapTexRect.Texture = LoadMapTexture(mapId);
        }
        UpdatePlayerPosition(_mapName, _playerX, _playerY);
    }

    public void UpdatePlayerPosition(string mapName, int x, int y)
    {
        _mapName = mapName;
        _playerX = x;
        _playerY = y;
        if (_lblMapCoord != null)
        {
            string title = _currentMode == MinimapMode.Big ? $"【{_mapName}】全景地图 (自身: {_playerX}, {_playerY})" : $"{_mapName} ({_playerX},{_playerY})";
            _lblMapCoord.Text = title;
        }

        // 检查是否到达寻路目标点
        if (_targetX >= 0 && _targetY >= 0)
        {
            if (Math.Abs(_playerX - _targetX) <= 1 && Math.Abs(_playerY - _targetY) <= 1)
            {
                ClearTarget();
            }
        }

        UpdatePlayerDotPosition();
    }

    private void UpdatePlayerDotPosition()
    {
        if (_playerDot == null) return;

        if (_currentMode == MinimapMode.Mini)
        {
            _playerDot.Position = new Vector2(58, 58);
        }
        else if (_currentMode == MinimapMode.Big)
        {
            // 在 540x360 全景大地图上定位主角实际坐标
            float mapAreaW = 536.0f;
            float mapAreaH = 328.0f;
            float px = 2 + (_playerX / (float)_mapWidthTiles) * mapAreaW;
            float py = 24 + (_playerY / (float)_mapHeightTiles) * mapAreaH;
            _playerDot.Position = new Vector2(Math.Clamp(px - 2, 2, 534), Math.Clamp(py - 2, 24, 350));
        }
    }

    public void SetTargetMarker(int x, int y)
    {
        _targetX = x;
        _targetY = y;
        if (_targetMarker != null)
        {
            _targetMarker.Visible = true;
            UpdateTargetMarkerPosition();
        }
    }

    public void ClearTarget()
    {
        _targetX = -1;
        _targetY = -1;
        if (_targetMarker != null)
        {
            _targetMarker.Visible = false;
        }
    }

    private void UpdateTargetMarkerPosition()
    {
        if (_targetMarker == null || _targetX < 0 || _targetY < 0) return;

        if (_currentMode == MinimapMode.Mini)
        {
            float dx = (_targetX - _playerX) * 1.5f;
            float dy = (_targetY - _playerY) * 1.0f;
            Vector2 pos = new Vector2(58 + dx, 58 + dy);
            if (pos.X >= 2 && pos.X <= 116 && pos.Y >= 2 && pos.Y <= 116)
            {
                _targetMarker.Position = pos;
                _targetMarker.Visible = true;
            }
            else
            {
                _targetMarker.Visible = false;
            }
        }
        else if (_currentMode == MinimapMode.Big)
        {
            float mapAreaW = 536.0f;
            float mapAreaH = 328.0f;
            float px = 2 + (_targetX / (float)_mapWidthTiles) * mapAreaW;
            float py = 24 + (_targetY / (float)_mapHeightTiles) * mapAreaH;
            _targetMarker.Position = new Vector2(Math.Clamp(px - 2, 2, 534), Math.Clamp(py - 2, 24, 350));
            _targetMarker.Visible = true;
        }
    }

    public void UpdateRadarEntities(IEnumerable<(int tileX, int tileY, RadarEntityType type)> entities)
    {
        if (_currentMode == MinimapMode.Closed || _entityDotsContainer == null) return;

        int dotIdx = 0;
        if (_currentMode == MinimapMode.Mini)
        {
            Vector2 center = new Vector2(58, 58);
            foreach (var (tx, ty, type) in entities)
            {
                float dx = (tx - _playerX) * 1.5f;
                float dy = (ty - _playerY) * 1.0f;
                Vector2 dotPos = center + new Vector2(dx, dy);

                if (dotPos.X < 2 || dotPos.X > 116 || dotPos.Y < 2 || dotPos.Y > 116)
                    continue;

                Color dotColor = GetRadarColor(type);
                SetDot(dotIdx++, dotPos, dotColor);
            }
        }
        else if (_currentMode == MinimapMode.Big)
        {
            float mapAreaW = 536.0f;
            float mapAreaH = 328.0f;
            foreach (var (tx, ty, type) in entities)
            {
                float px = 2 + (tx / (float)_mapWidthTiles) * mapAreaW;
                float py = 24 + (ty / (float)_mapHeightTiles) * mapAreaH;
                if (px < 2 || px > 536 || py < 24 || py > 352)
                    continue;

                Color dotColor = GetRadarColor(type);
                SetDot(dotIdx++, new Vector2(px, py), dotColor);
            }
        }

        for (int i = dotIdx; i < _dotPool.Count; i++)
        {
            _dotPool[i].Visible = false;
        }
    }

    private Color GetRadarColor(RadarEntityType type) => type switch
    {
        RadarEntityType.Npc => new Color(1.0f, 0.95f, 0.1f),      // NPC 亮黄点
        RadarEntityType.Monster => new Color(1.0f, 0.2f, 0.2f),  // 怪物红点
        RadarEntityType.GroupMember => new Color(0.2f, 0.6f, 1.0f), // 队友蓝点
        _ => new Color(1.0f, 1.0f, 1.0f)                          // 其他玩家白点
    };

    private void SetDot(int idx, Vector2 pos, Color col)
    {
        while (_dotPool.Count <= idx)
        {
            var newDot = new ColorRect
            {
                Size = new Vector2(3, 3),
                MouseFilter = MouseFilterEnum.Ignore
            };
            _entityDotsContainer?.AddChild(newDot);
            _dotPool.Add(newDot);
        }
        var dot = _dotPool[idx];
        dot.Position = pos;
        dot.Color = col;
        dot.Visible = true;
    }

    public override void _GuiInput(InputEvent @event)
    {
        if (_currentMode == MinimapMode.Closed) return;

        if (@event is InputEventMouseMotion motion)
        {
            HandleMouseMove(motion.Position);
            AcceptEvent();
        }
        else if (@event is InputEventMouseButton btn && btn.Pressed)
        {
            if (btn.ButtonIndex == MouseButton.Left)
            {
                HandleMouseClick(btn.Position);
                AcceptEvent();
            }
            else if (btn.ButtonIndex == MouseButton.Right)
            {
                // 右键快速关闭或取消目标
                if (_targetX >= 0)
                {
                    ClearTarget();
                }
                else
                {
                    CycleMode();
                }
                AcceptEvent();
            }
        }
    }

    private void HandleMouseMove(Vector2 pos)
    {
        if (_currentMode == MinimapMode.Mini)
        {
            float dx = pos.X - 58.0f;
            float dy = pos.Y - 58.0f;
            _hoverX = _playerX + (int)Math.Round(dx / 1.5f);
            _hoverY = _playerY + (int)Math.Round(dy / 1.0f);
        }
        else if (_currentMode == MinimapMode.Big)
        {
            float mapAreaW = 536.0f;
            float mapAreaH = 328.0f;
            float relX = Math.Clamp(pos.X - 2.0f, 0, mapAreaW);
            float relY = Math.Clamp(pos.Y - 24.0f, 0, mapAreaH);
            _hoverX = (int)Math.Round((relX / mapAreaW) * _mapWidthTiles);
            _hoverY = (int)Math.Round((relY / mapAreaH) * _mapHeightTiles);

            if (_lblHoverCoord != null)
            {
                _lblHoverCoord.Text = $"目标落点: ({_hoverX}, {_hoverY}) [左键点击自动寻路]";
                _lblHoverCoord.Visible = true;
            }
        }
    }

    public override void _Notification(int what)
    {
        base._Notification(what);
        if (what == NotificationMouseExit)
        {
            if (_lblHoverCoord != null) _lblHoverCoord.Visible = false;
        }
    }

    private void HandleMouseClick(Vector2 pos)
    {
        HandleMouseMove(pos);
        if (_hoverX >= 0 && _hoverY >= 0)
        {
            int targetX = Math.Clamp(_hoverX, 0, _mapWidthTiles - 1);
            int targetY = Math.Clamp(_hoverY, 0, _mapHeightTiles - 1);
            SetTargetMarker(targetX, targetY);
            GD.Print($"[MinimapView] 玩家在地图上点击坐标: ({targetX}, {targetY})，触发自动寻路");
            EmitSignal(SignalName.MapTargetSelected, targetX, targetY);
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
                if (_btnClose != null) _btnClose.Visible = false;
                if (_lblHoverCoord != null) _lblHoverCoord.Visible = false;
                if (_landmarksContainer != null) _landmarksContainer.Visible = false;
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
                    _lblMapCoord.Text = $"{_mapName} ({_playerX},{_playerY})";
                }
                if (_btnClose != null) _btnClose.Visible = false;
                if (_lblHoverCoord != null) _lblHoverCoord.Visible = false;
                if (_landmarksContainer != null) _landmarksContainer.Visible = false;
                UpdatePlayerDotPosition();
                UpdateTargetMarkerPosition();
                break;

            case MinimapMode.Big:
                // 官方原版大地图尺寸 540x360 居中停靠 (Left: 130, Top: 100)
                Visible = true;
                Size = new Vector2(540, 360);
                Position = new Vector2(130, 100);
                if (_bgPanel != null) _bgPanel.Size = new Vector2(540, 360);
                if (_mapTexRect != null)
                {
                    _mapTexRect.Position = new Vector2(2, 24);
                    _mapTexRect.Size = new Vector2(536, 328);
                }
                if (_lblMapCoord != null)
                {
                    _lblMapCoord.Position = new Vector2(20, 4);
                    _lblMapCoord.Size = new Vector2(490, 20);
                    _lblMapCoord.Text = $"【{_mapName}】全景地图 (自身: {_playerX}, {_playerY})";
                }
                if (_btnClose != null)
                {
                    _btnClose.Position = new Vector2(518, 2);
                    _btnClose.Visible = true;
                }
                if (_lblHoverCoord != null)
                {
                    _lblHoverCoord.Position = new Vector2(10, 335);
                    _lblHoverCoord.Size = new Vector2(300, 20);
                    _lblHoverCoord.Visible = false;
                }
                UpdatePlayerDotPosition();
                UpdateTargetMarkerPosition();
                RenderLandmarks();
                break;
        }
        EmitSignal(SignalName.ModeChanged, (int)_currentMode);
    }

    private void RenderLandmarks()
    {
        if (_landmarksContainer == null) return;
        var landmarks = MapLandmarkEngine.GetLandmarks(_mapId.Length > 0 ? _mapId : _mapName);
        if (landmarks == null || landmarks.Count == 0)
        {
            _landmarksContainer.Visible = false;
            return;
        }

        _landmarksContainer.Visible = true;
        float mapAreaW = 536.0f;
        float mapAreaH = 328.0f;

        for (int i = 0; i < landmarks.Count; i++)
        {
            var lm = landmarks[i];
            float px = 2 + (lm.X / (float)_mapWidthTiles) * mapAreaW;
            float py = 24 + (lm.Y / (float)_mapHeightTiles) * mapAreaH;

            while (_landmarkMarkers.Count <= i)
            {
                var marker = new ColorRect
                {
                    Size = new Vector2(4, 4),
                    MouseFilter = MouseFilterEnum.Ignore
                };
                _landmarksContainer.AddChild(marker);
                _landmarkMarkers.Add(marker);
            }
            var curMarker = _landmarkMarkers[i];
            curMarker.Position = new Vector2(Math.Clamp(px - 2, 2, 534), Math.Clamp(py - 2, 24, 350));
            curMarker.Color = lm.Type == MapLandmarkEngine.LandmarkType.Palace ? new Color(1f, 0.8f, 0.1f)
                           : lm.Type == MapLandmarkEngine.LandmarkType.Portal ? new Color(0.2f, 0.9f, 1f)
                           : new Color(0.3f, 1f, 0.4f);
            curMarker.Visible = true;

            while (_landmarkLabels.Count <= i)
            {
                var lbl = new Label
                {
                    MouseFilter = MouseFilterEnum.Ignore
                };
                lbl.AddThemeFontSizeOverride("font_size", 9);
                _landmarksContainer.AddChild(lbl);
                _landmarkLabels.Add(lbl);
            }
            var curLbl = _landmarkLabels[i];
            curLbl.Text = $"{lm.Name} ({lm.X},{lm.Y})";
            curLbl.Position = new Vector2(Math.Clamp(px + 4, 4, 460), Math.Clamp(py - 6, 24, 340));
            curLbl.AddThemeColorOverride("font_color", curMarker.Color);
            curLbl.Visible = true;
        }

        for (int i = landmarks.Count; i < _landmarkMarkers.Count; i++)
        {
            _landmarkMarkers[i].Visible = false;
            _landmarkLabels[i].Visible = false;
        }
    }
}
