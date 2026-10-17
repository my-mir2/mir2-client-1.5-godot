// ============================================================================
// Mir2 客户端 - 键鼠输入处理与事件路由插件 (1:1 盛大 1.5 官方实证)
// 官方实证：IDA 原版 Delphi 键盘/鼠标事件处理
// ============================================================================

using System;
using Godot;
using Mir2Client.Render;
using Mir2Client.UI;

namespace Mir2Client.Game
{
    public partial class InputPlugin : Node
    {
        [Signal] public delegate void WalkRequestedEventHandler(int dir);
        [Signal] public delegate void RunRequestedEventHandler(int dir);
        [Signal] public delegate void TurnRequestedEventHandler(int dir);
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
        [Signal] public delegate void EntityInteractedEventHandler(int targetUid, int entityType, int tileX, int tileY, bool isShift);
        [Signal] public delegate void ButcherRequestedEventHandler(int tileX, int tileY, int dir);
        [Signal] public delegate void PetCommandRequestedEventHandler();
        [Signal] public delegate void LogoutRequestedEventHandler();
        [Signal] public delegate void QuitRequestedEventHandler();
        [Signal] public delegate void GroupToggleRequestedEventHandler();
        [Signal] public delegate void GroupAllowToggleRequestedEventHandler();
        [Signal] public delegate void NavigateRequestedEventHandler(Vector2I targetTile);
        [Signal] public delegate void HalfMoonToggleRequestedEventHandler();
        [Signal] public delegate void ThrustingToggleRequestedEventHandler();
        [Signal] public delegate void ScreenCaptureRequestedEventHandler();

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
        private EntityManager? _entityManager;
        private HudController? _hudController;
        private ulong _lastContinuousAttackTick = 0;

        private ulong _lastRightRunMs = 0;
        // 盛大官方 1.5 奔跑动作步频对齐 (单次奔跑 6 帧为 0.54s，采样周期设为 540ms 确保步步踏实绝不抢步抽搐)
        public const ulong RightRunIntervalMs = 600;
        // IDA sub_4CB7F8 @ 0x4CB833：攻速基准 900 - 60 * HitSpeed (HitSpeed=0 时 900ms)，低于此值服务端丢弃并计超速
        public const ulong AttackIntervalMs = 900;

        public void SetMapRenderer(MapRenderer mr) => _mapRenderer = mr;
        public void SetEntityManager(EntityManager em) => _entityManager = em;
        public void SetPlayer(Node2D p) => _playerNode = p;
        public void SetGame(Node g) => _gameNode = g;
        public void SetHudController(HudController hud) => _hudController = hud;

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
            if (_playerNode is PlayerNode pn)
            {
                if (_mapRenderer != null)
                {
                    return _mapRenderer.WorldToScreen(pn.GetCurrentRenderPos()) + new Vector2(24, 16);
                }
                return pn.Position + new Vector2(24, 16);
            }
            if (_playerNode != null)
            {
                return _playerNode.Position + new Vector2(24, 16);
            }
            return new Vector2(400, 225);
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
            if (_hudController != null)
            {
                return _hudController.IsMouseOverUi(mousePos);
            }
            if (_gameNode is GameView gv && gv.HudInstance != null)
            {
                _hudController = gv.HudInstance;
                return _hudController.IsMouseOverUi(mousePos);
            }
            // 物理防线：盛大官方 1.5 原版 DFM DBottom 真实位置 Top=416，未绑定 HUD 时保守防穿透，放行大世界地面
            return mousePos.Y >= 416;
        }

        public override void _Process(double delta)
        {
            if (_playerNode == null) return;
            if (_playerNode is PlayerNode pnl && (pnl.IsStunned || pnl.IsDead)) return;

            // 盛大官方 1.5 规则：上一动作移动插值未完成时，等待当前步完成再衔接下一步
            bool isMoving = false;
            if (_playerNode is PlayerNode pnr)
            {
                isMoving = pnr.IsMoving;
            }
            else
            {
                isMoving = _playerNode.Get("is_moving").AsBool();
            }

            if (isMoving) return;

            var mousePos = _playerNode.IsInsideTree() ? _playerNode.GetViewport().GetMousePosition() : Vector2.Zero;
            if (IsCursorOverUi(mousePos)) return;

            var playerPos = GetPlayerScreenPos();
            var diff = mousePos - playerPos;
            if (diff.Length() <= 20.0f) return; // 20 像素脚下死区，彻底杜绝原地打转

            int dir = CalcDirection(diff);

            // 1. 盛大官方 1.5 右键长按连续奔跑 (Run)
            if (Input.IsMouseButtonPressed(MouseButton.Right))
            {
                EmitSignal(SignalName.RunRequested, dir);
                return;
            }

            // 2. 盛大官方 1.5 左键长按 (若按住 Shift 强制原地攻击；若指着活体怪物则出刀挥砍/追击，否则连续走动)
            if (Input.IsMouseButtonPressed(MouseButton.Left))
            {
                // 盛大官方 1.5 铁律：Shift 按住时优先强制原地朝光标方向挥刀斩击 (IDA sub_461324)
                if (Input.IsKeyPressed(Key.Shift))
                {
                    ulong nowTick = Time.GetTicksMsec();
                    if (nowTick >= _lastContinuousAttackTick + AttackIntervalMs)
                    {
                        _lastContinuousAttackTick = nowTick;
                        EmitSignal(SignalName.HitRequested, dir);
                    }
                    return;
                }

                // 功能键组合优先 (Ctrl 刺杀, Alt 挖肉)，不触发走动
                if (!Input.IsKeyPressed(Key.Ctrl) && !Input.IsKeyPressed(Key.Alt))
                {
                    var targetTile = _mapRenderer != null ? _mapRenderer.ScreenToTile(mousePos) : new Vector2I(-1, -1);
                    var ent = FindEntityUnderCursor(mousePos, targetTile);
                    if (ent != null && ent.Type == EntityManager.EntityType.Monster && !ent.IsDead)
                    {
                        int pTileX = _playerNode.Get("tile_x").AsInt32();
                        int pTileY = _playerNode.Get("tile_y").AsInt32();
                        int cdist = Math.Max(Math.Abs(ent.TileX - pTileX), Math.Abs(ent.TileY - pTileY));
                        if (cdist <= 1)
                        {
                            ulong nowTick = Time.GetTicksMsec();
                            if (nowTick >= _lastContinuousAttackTick + AttackIntervalMs)
                            {
                                _lastContinuousAttackTick = nowTick;
                                int attackDir = cdist > 0
                                    ? CalcDirection(new Vector2(ent.TileX - pTileX, ent.TileY - pTileY))
                                    : CalcDirection(mousePos - playerPos);
                                EmitSignal(SignalName.TargetSelected, ent.Uid);
                                EmitSignal(SignalName.HitRequested, attackDir);
                            }
                            // 权威铁律：只要光标悬停在相邻存活怪物上，无论是否在挥刀冷却中，绝不降级为走动！直接阻断！
                            return;
                        }
                        else
                        {
                            // 远处怪物：官方 1.5 规则，向怪物方向走动逼近
                            EmitSignal(SignalName.NavigateRequested, new Vector2I(ent.TileX, ent.TileY));
                            return;
                        }
                    }

                    // 若指着 NPC 或其他玩家，直接阻断走动
                    if (ent != null && (ent.Type == EntityManager.EntityType.Npc || ent.Type == EntityManager.EntityType.Player))
                    {
                        return;
                    }

                    // 真正指向空地，才触发连续走动
                    EmitSignal(SignalName.WalkRequested, dir);
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
                            case Key.X: EmitSignal(SignalName.QuitRequested); break;   // 官方 1.5 Alt+X: 退出游戏 (sub_461DEC @ 0x461244)
                            case Key.Q: EmitSignal(SignalName.LogoutRequested); break; // 官方 1.5 Alt+Q: 小退换人 (sub_461DEC @ 0x461338)
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
                            case Key.B: EmitSignal(SignalName.HalfMoonToggleRequested); break;
                            case Key.F: EmitSignal(SignalName.ThrustingToggleRequested); break;
                        }
                    }
                    else
                    {
                        switch (key.Keycode)
                        {
                            case Key.Print: case Key.Pause:
                                EmitSignal(SignalName.ScreenCaptureRequested);
                                break;
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
            if (_playerNode is PlayerNode pnl && (pnl.IsStunned || pnl.IsDead)) return;

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

            // 0. Shift 官方强制原地物理攻击 (对齐 Delphi ClMain.pas:2890 ssShift in Shift -> SendHit)
            if (Input.IsKeyPressed(Key.Shift))
            {
                EmitSignal(SignalName.HitRequested, dir);
                return;
            }

            // 1. Alt + 点击：官方割肉/屠宰 (对齐 Delphi ClMain.pas:2890 ssAlt in Shift -> SendButchAnimal)
            if (Input.IsKeyPressed(Key.Alt))
            {
                EmitSignal(SignalName.ButcherRequested, targetTile.X, targetTile.Y, dir);
                return;
            }

            // 实体点击判定 (NPC 对话 / 玩家选中与查名 / 怪物锁定与普攻)
            if (_entityManager != null)
            {
                var ent = FindEntityUnderCursor(screenPos, targetTile);
                if (ent != null)
                {
                    targetTile = new Vector2I(ent.TileX, ent.TileY);
                    chebyshev = Math.Max(Math.Abs(targetTile.X - playerTile.X), Math.Abs(targetTile.Y - playerTile.Y));
                    if (chebyshev > 0)
                    {
                        dir = CalcDirection(new Vector2(targetTile.X - pTileX, targetTile.Y - pTileY));
                    }
                    else
                    {
                        dir = CalcDirection(screenPos - playerScreen);
                    }

                    bool isShift = Input.IsKeyPressed(Key.Shift);
                    EmitSignal(SignalName.TargetSelected, ent.Uid);
                    EmitSignal(SignalName.EntityInteracted, ent.Uid, (int)ent.Type, targetTile.X, targetTile.Y, isShift);

                    // 若为相邻活体怪物 (chebyshev <= 1, 包含同格重合)，自动发起近战攻击并阻断后续任何地面判定
                    if (ent.Type == EntityManager.EntityType.Monster && !ent.IsDead && chebyshev <= 1)
                    {
                        _lastContinuousAttackTick = Time.GetTicksMsec();
                        EmitSignal(SignalName.HitRequested, dir);
                        return;
                    }
                    // 若为远处活体怪物 (chebyshev > 1)，自动触发走向该怪物
                    if (ent.Type == EntityManager.EntityType.Monster && !ent.IsDead && chebyshev > 1)
                    {
                        EmitSignal(SignalName.NavigateRequested, targetTile);
                        return;
                    }
                    // 若为 NPC 或其他玩家，直接返回阻断向其脚下寻路跑动
                    if (ent.Type == EntityManager.EntityType.Npc || ent.Type == EntityManager.EntityType.Player)
                    {
                        return;
                    }
                }
            }

            // 2. 盛大官方 1.5 空地走动逻辑 (彻底移除 targetTile == playerTile 拾取毒丸)：
            // 官方拾取铁律：走动踩在物品上方或 Space 空格键触发！
            if (targetTile.X >= 0)
            {
                if (chebyshev <= 1)
                {
                    var deltaScreen = screenPos - playerScreen;
                    if (deltaScreen.Length() > 16.0f)
                    {
                        EmitSignal(SignalName.WalkRequested, dir);
                    }
                }
                else
                {
                    EmitSignal(SignalName.NavigateRequested, targetTile);
                }
            }
            else
            {
                var deltaScreen = screenPos - playerScreen;
                if (deltaScreen.Length() > 16.0f)
                {
                    EmitSignal(SignalName.WalkRequested, dir);
                }
            }
        }

        /// <summary>
        /// 盛大官方 1.5 指针聚焦实体拾取 (FocusCret, 严格对齐 IDA sub_4614A0 / GetActorAt)
        /// 1. 排除本地主角自身防误选
        /// 2. 存活怪物具有最高绝对优先权
        /// 3. 包围盒依据官方 2.5D 切片覆盖 (X: ±45px, Y: -115px..+20px)
        /// 4. 2.5D 深度优先：TileY 越大（越靠前）越优先命中
        /// 5. 瓦片回退拾取
        /// </summary>
        public EntityManager.EntityData? FindEntityUnderCursor(Vector2 screenPos, Vector2I targetTile)
        {
            if (_entityManager == null) return null;

            int localUid = _entityManager.LocalPlayerUid;
            if (localUid == 0 && _playerNode is PlayerNode pnl)
            {
                localUid = pnl.Uid;
            }

            EntityManager.EntityData? bestCandidate = null;
            int bestTileY = int.MinValue;
            float bestDistSq = float.MaxValue;

            if (_mapRenderer != null)
            {
                foreach (var candidate in _entityManager.AllEntities)
                {
                    if (candidate.Uid == localUid || (candidate.Type == EntityManager.EntityType.Player && candidate.Uid == localUid))
                        continue;

                    var entCenter = _mapRenderer.WorldToScreen(new Vector2(candidate.TileX * 48 + 24, candidate.TileY * 32 + 16));
                    // 覆盖官方 1.5 怪物完整切片像素范围 (宽 90px, 高 145px，向上覆盖至 115px，向下延伸至地面 30px 贴合小型怪)
                    var bbox = new Rect2(entCenter.X - 45, entCenter.Y - 115, 90, 145);
                    bool isHitBbox = bbox.HasPoint(screenPos);
                    bool isHitTile = (targetTile.X >= 0 && candidate.TileX == targetTile.X && candidate.TileY == targetTile.Y);
                    if (isHitBbox || isHitTile)
                    {
                        bool isLivingMonster = candidate.Type == EntityManager.EntityType.Monster && !candidate.IsDead;
                        bool bestIsLivingMonster = bestCandidate != null && bestCandidate.Type == EntityManager.EntityType.Monster && !bestCandidate.IsDead;

                        if (isLivingMonster && !bestIsLivingMonster)
                        {
                            bestCandidate = candidate;
                            bestTileY = candidate.TileY;
                            bestDistSq = screenPos.DistanceSquaredTo(entCenter);
                        }
                        else if (isLivingMonster == bestIsLivingMonster)
                        {
                            // 深度越靠前 (TileY 越大) 优先，若相同则取距离指针最近者
                            if (candidate.TileY > bestTileY || (candidate.TileY == bestTileY && screenPos.DistanceSquaredTo(entCenter) < bestDistSq))
                            {
                                bestCandidate = candidate;
                                bestTileY = candidate.TileY;
                                bestDistSq = screenPos.DistanceSquaredTo(entCenter);
                            }
                        }
                    }
                }
            }

            // 瓦片回退拾取
            if (bestCandidate == null && targetTile.X >= 0)
            {
                var tileEnt = _entityManager.GetEntityAt(targetTile.X, targetTile.Y);
                if (tileEnt != null && tileEnt.Uid != localUid)
                {
                    bestCandidate = tileEnt;
                }
            }

            return bestCandidate;
        }

        private void OnRightClick(Vector2 screenPos)
        {
            if (_playerNode == null) return;
            if (_playerNode is PlayerNode pnr && (pnr.IsStunned || pnr.IsDead)) return;

            var playerScreen = GetPlayerScreenPos();
            var delta = screenPos - playerScreen;
            if (delta.Length() < 24.0f)
            {
                int turnDir = CalcDirection(delta);
                EmitSignal(SignalName.TurnRequested, turnDir);
                return;
            }

            int dir = CalcDirection(delta);
            EmitSignal(SignalName.RunRequested, dir);
        }

        /// <summary>
        /// 当角色单步刚走完时主动调用，立即衔接下一步长按移动，消除步与步之间的卡顿抽搐
        /// </summary>
        public void PollContinuousMovement()
        {
            if (_playerNode == null) return;
            if (_playerNode is PlayerNode pnl && (pnl.IsStunned || pnl.IsDead)) return;

            var mousePos = _playerNode.IsInsideTree() ? _playerNode.GetViewport().GetMousePosition() : Vector2.Zero;
            if (IsCursorOverUi(mousePos)) return;

            var playerPos = GetPlayerScreenPos();
            var diff = mousePos - playerPos;
            if (diff.Length() <= 20.0f) return;

            int dir = CalcDirection(diff);

            // 1. 右键长按连续奔跑
            if (Input.IsMouseButtonPressed(MouseButton.Right))
            {
                EmitSignal(SignalName.RunRequested, dir);
                return;
            }

            // 2. 左键长按连续走动
            if (Input.IsMouseButtonPressed(MouseButton.Left))
            {
                if (!Input.IsKeyPressed(Key.Shift) && !Input.IsKeyPressed(Key.Ctrl) && !Input.IsKeyPressed(Key.Alt))
                {
                    EmitSignal(SignalName.WalkRequested, dir);
                }
            }
        }

        public static int CalcDirection(float dx, float dy)
        {
            if (dx * dx + dy * dy < 1.0f) return 0;
            float angle = MathF.Atan2(dy, dx);
            if (angle < 0) angle += MathF.PI * 2.0f;

            int sector = (int)((angle + MathF.PI / 8.0f) / (MathF.PI / 4.0f)) % 8;
            ReadOnlySpan<int> sectorToDir = stackalloc int[8]
            {
                DirRight, DirDownRight, DirDown, DirDownLeft,
                DirLeft, DirUpLeft, DirUp, DirUpRight
            };
            return sectorToDir[sector];
        }

        public static int CalcDirection(Vector2 delta) => CalcDirection(delta.X, delta.Y);
    }
}
