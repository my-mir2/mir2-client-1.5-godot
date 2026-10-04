// ============================================================================
// Mir2 客户端 - 键鼠输入处理与事件路由插件 (1:1 盛大 1.5 官方实证)
// 官方实证：IDA 原版 Delphi 键盘/鼠标事件处理
// ============================================================================

using System;
using Godot;
using Mir2Client.Render;

namespace Mir2Client.Game
{
    public partial class InputPlugin : Node
    {
        [Signal] public delegate void WalkRequestedEventHandler(int dir);
        [Signal] public delegate void RunRequestedEventHandler(int dir);
        [Signal] public delegate void HitRequestedEventHandler(int dir);
        [Signal] public delegate void PowerHitRequestedEventHandler(int dir);
        [Signal] public delegate void PickupRequestedEventHandler();
        [Signal] public delegate void ChatInputRequestedEventHandler();
        [Signal] public delegate void BagToggleRequestedEventHandler();
        [Signal] public delegate void EquipToggleRequestedEventHandler();
        [Signal] public delegate void SkillToggleRequestedEventHandler();
        [Signal] public delegate void MinimapToggleRequestedEventHandler();
        [Signal] public delegate void EscapeRequestedEventHandler();
        [Signal] public delegate void SkillKeyPressedEventHandler(int slot);
        [Signal] public delegate void QuickItemKeyPressedEventHandler(int slot);
        [Signal] public delegate void PkModeToggleRequestedEventHandler();
        [Signal] public delegate void AltShowItemsToggledEventHandler(bool show);
        [Signal] public delegate void TargetSelectedEventHandler(int targetUid);
        [Signal] public delegate void ButcherRequestedEventHandler(int targetUid, string targetName);
        [Signal] public delegate void PetCommandRequestedEventHandler();
        [Signal] public delegate void LogoutRequestedEventHandler();
        [Signal] public delegate void QuitRequestedEventHandler();
        [Signal] public delegate void GroupToggleRequestedEventHandler();
        [Signal] public delegate void GroupAllowToggleRequestedEventHandler();
        [Signal] public delegate void NavigateRequestedEventHandler(Vector2I targetTile);

        public const int DirUp = 0;
        public const int DirUpRight = 1;
        public const int DirRight = 2;
        public const int DirDownRight = 3;
        public const int DirDown = 4;
        public const int DirDownLeft = 5;
        public const int DirLeft = 6;
        public const int DirUpLeft = 7;

        private Node2D? _playerNode;
        private Node? _gameNode;
        private MapRenderer? _mapRenderer;

        private ulong _lastRightRunMs = 0;
        public const ulong RightRunIntervalMs = 360;

        public void SetMapRenderer(MapRenderer mr) => _mapRenderer = mr;
        public void SetPlayer(Node2D p) => _playerNode = p;
        public void SetGame(Node g) => _gameNode = g;

        public override void _Ready()
        {
            if (_playerNode == null && GetParent() is Node2D parent2d)
                _playerNode = parent2d;
            if (_playerNode != null && _gameNode == null && _playerNode.GetParent() != null)
                _gameNode = _playerNode.GetParent();

            SetProcessUnhandledInput(true);
            SetProcess(true);
        }

        private Vector2 GetPlayerScreenPos()
        {
            if (_mapRenderer != null && _playerNode != null)
            {
                var tileX = (int)(_playerNode.Get("tile_x").AsInt32());
                var tileY = (int)(_playerNode.Get("tile_y").AsInt32());
                return _mapRenderer.TileToScreen(tileX, tileY) + new Vector2(24, 16);
            }
            if (_playerNode != null && _playerNode.IsInsideTree())
            {
                var vp = _playerNode.GetViewport();
                if (vp != null) return vp.GetVisibleRect().Size / 2.0f;
            }
            return _playerNode?.Position ?? Vector2.Zero;
        }

        private int CalcTileDirection(Vector2I from, Vector2I to)
        {
            int dx = to.X - from.X;
            int dy = to.Y - from.Y;
            if (dx == 0 && dy == 0)
                return _playerNode != null ? _playerNode.Get("direction").AsInt32() : 0;

            int sx = Math.Clamp(dx, -1, 1);
            int sy = Math.Clamp(dy, -1, 1);

            return (sx, sy) switch
            {
                (0, -1) => DirUp,
                (1, -1) => DirUpRight,
                (1, 0) => DirRight,
                (1, 1) => DirDownRight,
                (0, 1) => DirDown,
                (-1, 1) => DirDownLeft,
                (-1, 0) => DirLeft,
                (-1, -1) => DirUpLeft,
                _ => 0
            };
        }

        private bool IsCursorOverUi(Vector2 mousePos)
        {
            if (_gameNode != null)
            {
                var hud = _gameNode.Get("hud").AsGodotObject();
                if (hud != null && hud.HasMethod("is_mouse_over_ui"))
                {
                    return hud.Call("is_mouse_over_ui", mousePos).AsBool();
                }
            }
            return false;
        }

        public override void _Process(double delta)
        {
            if (_playerNode != null && Input.IsMouseButtonPressed(MouseButton.Right))
            {
                var mousePos = _playerNode.IsInsideTree() ? _playerNode.GetViewport().GetMousePosition() : Vector2.Zero;
                if (IsCursorOverUi(mousePos)) return;

                ulong now = Time.GetTicksMsec();
                if (now - _lastRightRunMs >= RightRunIntervalMs)
                {
                    _lastRightRunMs = now;
                    var playerPos = GetPlayerScreenPos();
                    var diff = mousePos - playerPos;
                    if (diff.Length() > 24)
                    {
                        int dir = CalcDirection(diff);
                        EmitSignal(SignalName.RunRequested, dir);
                    }
                }
            }
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event is InputEventMouseButton mb)
            {
                if (IsCursorOverUi(mb.Position)) return;
                HandleMouse(mb);
            }
            else if (@event is InputEventKey key)
            {
                if (key.Keycode == Key.Alt)
                {
                    EmitSignal(SignalName.AltShowItemsToggled, key.Pressed);
                    return;
                }

                if (key.Pressed && !key.Echo)
                {
                    if (key.AltPressed)
                    {
                        switch (key.Keycode)
                        {
                            case Key.X: EmitSignal(SignalName.LogoutRequested); break;
                            case Key.Q: EmitSignal(SignalName.QuitRequested); break;
                            case Key.W: EmitSignal(SignalName.GroupToggleRequested); break;
                        }
                    }
                    else if (key.CtrlPressed)
                    {
                        switch (key.Keycode)
                        {
                            case Key.A: EmitSignal(SignalName.PetCommandRequested); break;
                            case Key.H: EmitSignal(SignalName.PkModeToggleRequested); break;
                            case Key.E: EmitSignal(SignalName.GroupAllowToggleRequested); break;
                            case Key.B: EmitSignal(SignalName.BagToggleRequested); break;
                        }
                    }
                    else if (key.ShiftPressed)
                    {
                        switch (key.Keycode)
                        {
                            case Key.X: EmitSignal(SignalName.QuitRequested); break;
                            case Key.Q: EmitSignal(SignalName.LogoutRequested); break;
                        }
                    }
                    else
                    {
                        switch (key.Keycode)
                        {
                            case Key.Key1: case Key.Key2: case Key.Key3:
                            case Key.Key4: case Key.Key5: case Key.Key6:
                                EmitSignal(SignalName.QuickItemKeyPressed, (int)key.Keycode - (int)Key.Key1);
                                break;
                            case Key.F1: case Key.F2: case Key.F3: case Key.F4:
                            case Key.F5: case Key.F6: case Key.F7: case Key.F8:
                                EmitSignal(SignalName.SkillKeyPressed, (int)key.Keycode - (int)Key.F1);
                                break;
                            case Key.P: EmitSignal(SignalName.GroupToggleRequested); break;
                            case Key.I: case Key.F9: EmitSignal(SignalName.BagToggleRequested); break;
                            case Key.C: case Key.F10: EmitSignal(SignalName.EquipToggleRequested); break;
                            case Key.F11: EmitSignal(SignalName.SkillToggleRequested); break;
                            case Key.Tab: EmitSignal(SignalName.MinimapToggleRequested); break;
                            case Key.Escape: EmitSignal(SignalName.EscapeRequested); break;
                            case Key.Space: EmitSignal(SignalName.PickupRequested); break;
                            case Key.Enter: EmitSignal(SignalName.ChatInputRequested); break;
                        }
                    }
                }
            }
        }

        private void HandleMouse(InputEventMouseButton mb)
        {
            if (mb.Pressed)
            {
                if (mb.ButtonIndex == MouseButton.Left)
                    OnLeftClick(mb.Position);
                else if (mb.ButtonIndex == MouseButton.Right)
                    OnRightClick(mb.Position);
            }
        }

        private void OnLeftClick(Vector2 screenPos)
        {
            if (_playerNode == null) return;

            var playerScreen = GetPlayerScreenPos();
            var delta = screenPos - playerScreen;
            int dir = CalcDirection(delta);

            var targetTile = new Vector2I(-1, -1);
            if (_mapRenderer != null)
                targetTile = _mapRenderer.ScreenToTile(screenPos);

            int pTileX = _playerNode.Get("tile_x").AsInt32();
            int pTileY = _playerNode.Get("tile_y").AsInt32();
            var playerTile = new Vector2I(pTileX, pTileY);

            int chebyshev = targetTile.X >= 0
                ? Math.Max(Math.Abs(targetTile.X - playerTile.X), Math.Abs(targetTile.Y - playerTile.Y))
                : 999;

            // 1. Shift 原地攻击
            if (Input.IsKeyPressed(Key.Shift))
            {
                EmitSignal(SignalName.HitRequested, dir);
                return;
            }

            // 2. Ctrl 刺杀/重击
            if (Input.IsKeyPressed(Key.Ctrl))
            {
                EmitSignal(SignalName.PowerHitRequested, dir);
                return;
            }

            // 3. 点击自身脚下拾取
            if (targetTile == playerTile)
            {
                EmitSignal(SignalName.PickupRequested);
                return;
            }

            // 4. 空地走动或奔跑寻路
            if (chebyshev <= 1)
            {
                EmitSignal(SignalName.WalkRequested, dir);
            }
            else
            {
                if (targetTile.X >= 0)
                    EmitSignal(SignalName.NavigateRequested, targetTile);
                else
                    EmitSignal(SignalName.RunRequested, dir);
            }
        }

        private void OnRightClick(Vector2 screenPos)
        {
            if (_playerNode == null) return;

            var playerScreen = GetPlayerScreenPos();
            var delta = screenPos - playerScreen;
            int dir = CalcDirection(delta);

            EmitSignal(SignalName.RunRequested, dir);
        }

        public static int CalcDirection(Vector2 delta)
        {
            if (delta.Length() < 1.0f) return 0;
            float angle = delta.Angle();
            if (angle < 0) angle += Mathf.Tau;

            int sector = (int)((angle + Mathf.Pi / 8.0f) / (Mathf.Pi / 4.0f)) % 8;
            ReadOnlySpan<int> sectorToDir = stackalloc int[8]
            {
                DirRight, DirDownRight, DirDown, DirDownLeft,
                DirLeft, DirUpLeft, DirUp, DirUpRight
            };
            return sectorToDir[sector];
        }
    }
}
