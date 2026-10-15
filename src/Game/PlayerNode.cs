// ============================================================================
// Mir2 客户端 - 玩家实体场景表现节点 (1:1 盛大 1.5 官方实证)
// 官方实证：IDA sub_495EBC & sub_496120 & Hum/Mon 物理帧与纸娃娃层叠
// ============================================================================

using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Audio;
using Mir2Client.Core;
using Mir2Client.Render;
using Mir2Client.Utils;

namespace Mir2Client.Game
{
    public partial class PlayerNode : Node2D
    {
        [Signal] public delegate void HealthChangedEventHandler(int hp, int maxHp, int mp, int maxMp);
        [Signal] public delegate void ExpChangedEventHandler(int exp, int maxExp, int level);
        [Signal] public delegate void GoldChangedEventHandler(int gold);
        [Signal] public delegate void PlayerDiedEventHandler();
        [Signal] public delegate void RevivedEventHandler();
        [Signal] public delegate void TileSteppedEventHandler(Vector2I newTile);
        [Signal] public delegate void StepStartedEventHandler(int targetX, int targetY, int dir, bool isRun);
        [Signal] public delegate void TurnedEventHandler(int dir);
        [Signal] public delegate void StepCompletedEventHandler();

        public int Uid { get; set; } = 0;
        public string CharacterName { get; set; } = "玩家";
        public int Level { get; set; } = 1;
        public int Hp { get; set; } = 19;
        public int MaxHp { get; set; } = 19;
        public int Mp { get; set; } = 11;
        public int MaxMp { get; set; } = 11;
        public int Exp { get; set; } = 0;
        public int MaxExp { get; set; } = 1000;
        public int Gold { get; set; } = 0;

        public int TileX { get; set; } = 0;
        public int TileY { get; set; } = 0;
        public int Direction { get; set; } = 4;
        public bool IsDead { get; set; } = false;

        public int BodyDressType { get; set; } = 0;
        public int WeaponType { get; set; } = 0;
        public int HairStyle { get; set; } = 1;
        public int Gender { get; set; } = 0; // 0: 男, 1: 女

        public string GuildName { get; set; } = string.Empty;
        public string GuildRank { get; set; } = string.Empty;

        private Sprite2D? _bodySprite;
        private Sprite2D? _weaponSprite;
        private Sprite2D? _hairSprite;
        private Label? _nameLabel;
        private ColorRect? _hpBarBg;
        private ColorRect? _hpBarFill;
        private float _hpBarHideTimer = 0f;

        public int PkPoints { get; set; } = 0; // 0~99 白名, 100~199 黄名, >=200 红名 (IDA 0x4D68D0（已证client无函数）)

        private WilLoader? _wilHum;
        private WilLoader? _wilWeapon;
        private WilLoader? _wilHair;
        private MapRenderer? _mapRenderer;

        public AnimState Anim { get; } = new();

        private bool _isMoving = false;
        private ulong _moveCooldownUntilMs = 0;
        private Vector2 _moveStartWorld = Vector2.Zero;
        private Vector2 _moveTargetWorld = Vector2.Zero;
        private float _moveDuration = 0.5f;
        private float _moveTimer = 0f;
        private int _targetTileX = 0;
        private int _targetTileY = 0;

        private readonly List<Vector2I> _pathQueue = new();
        private bool _isPathRun = true;

        public bool IsMoving => _isMoving;

        public override void _Ready()
        {
            SetupVisuals();
            Anim.SetAction(ActionType.Stand, Direction);
        }

        private void SetupVisuals()
        {
            _bodySprite = new Sprite2D { Centered = false };
            AddChild(_bodySprite);

            _weaponSprite = new Sprite2D { Centered = false };
            AddChild(_weaponSprite);

            _hairSprite = new Sprite2D { Centered = false };
            AddChild(_hairSprite);

            _nameLabel = new Label
            {
                Name = "PlayerNameLabel",
                HorizontalAlignment = HorizontalAlignment.Center,
                Visible = !IsDead,
                ZAsRelative = false,
                ZIndex = 3600,
                MouseFilter = Control.MouseFilterEnum.Ignore
            };
            FontManager.ApplyOfficialStyle(_nameLabel, Colors.White, FontManager.OfficialFontSize, true);
            AddChild(_nameLabel);

            // 官方 1.5 角色头顶微型血条 (PlayScn.pas 0x471310 SayY=-47 & DrawScrn.pas 0x47A186 SayY-10=-57, Prguse2.wil 0/1 帧 30x4)
            _hpBarBg = new ColorRect
            {
                Name = "HpBarBg",
                Position = new Vector2(9, -57),
                Size = new Vector2(30, 4),
                Color = new Color(0.05f, 0.05f, 0.05f, 0.95f),
                Visible = !IsDead,
                ZAsRelative = false,
                ZIndex = 3601,
                MouseFilter = Control.MouseFilterEnum.Ignore
            };
            AddChild(_hpBarBg);

            _hpBarFill = new ColorRect
            {
                Name = "HpBarFill",
                Size = new Vector2(28, 2),
                Position = new Vector2(1, 1),
                Color = new Color(0.95f, 0.15f, 0.15f),
                Visible = !IsDead,
                MouseFilter = Control.MouseFilterEnum.Ignore
            };
            _hpBarBg.AddChild(_hpBarFill);

            UpdateNameLabelText();
        }

        public void SetPkPoints(int points)
        {
            PkPoints = points;
            UpdateNameLabelText();
        }

        public void TriggerHpDamage(int newHp, int newMaxHp)
        {
            Hp = newHp;
            MaxHp = newMaxHp;
            _hpBarHideTimer = 3.0f;
            if (_hpBarBg != null && _hpBarFill != null)
            {
                _hpBarBg.Visible = !IsDead;
                _hpBarFill.Visible = !IsDead;
                float ratio = MaxHp > 0 ? Mathf.Clamp((float)Hp / MaxHp, 0f, 1f) : 1f;
                _hpBarFill.Size = new Vector2(28 * ratio, 2);
                _hpBarFill.Color = new Color(0.95f, 0.15f, 0.15f);
            }
        }

        public void SetWilLoaders(WilLoader? hum, WilLoader? weapon, WilLoader? hair)
        {
            _wilHum = hum;
            _wilWeapon = weapon;
            _wilHair = hair;
            UpdateRenderFrames();
        }

        public void SetMapRenderer(MapRenderer? mr) => _mapRenderer = mr;

        public void UpdateNameLabelText()
        {
            if (_nameLabel == null) return;

            // 依据 PK 点数设置名字颜色 (IDA 0x4D68D0: 白名/黄名/红名)
            Color nameColor = PkPoints switch
            {
                >= 200 => new Color(1.0f, 0.15f, 0.15f), // 鲜红名 (大恶魔)
                >= 100 => new Color(1.0f, 0.95f, 0.1f),  // 亮黄名 (警告)
                _ => new Color(1.0f, 1.0f, 1.0f)        // 白名 (良民)
            };
            // 盛大官方 1.5 权威名牌与血条基准锁死 (严格 1:1 对齐 Delphi PlayScn.pas 0x471310 SayY=-47 & DrawScrn.pas 0x47A186):
            // 人类发髻最高点在 Y = -49 (Hair.wil 3000 py=-49)，血条置于 Y=-57 (SayY-10)，名字置于 Y=-71，彻底杜绝名字横穿发髻！
            FontManager.ApplyOfficialWorldNameplateStyle(_nameLabel, nameColor);

            if (!string.IsNullOrEmpty(GuildName))
            {
                string rankStr = !string.IsNullOrEmpty(GuildRank) ? " " + GuildRank : "";
                _nameLabel.Text = $"【{GuildName}{rankStr}】\n{CharacterName}";
                _nameLabel.Position = new Vector2(-40, -85);
                _nameLabel.CustomMinimumSize = new Vector2(128, 32);
                if (_hpBarBg != null) _hpBarBg.Position = new Vector2(9, -57);
            }
            else
            {
                _nameLabel.Text = CharacterName;
                _nameLabel.Position = new Vector2(-40, IsDead ? -14 : -71);
                _nameLabel.CustomMinimumSize = new Vector2(128, 20);
                if (_hpBarBg != null) _hpBarBg.Position = new Vector2(9, -57);
            }
            _nameLabel.Visible = !IsDead;
            if (_hpBarBg != null) _hpBarBg.Visible = !IsDead;
            if (_hpBarFill != null) _hpBarFill.Visible = !IsDead;
        }

        public Vector2 SmoothOffset { get; set; } = Vector2.Zero;
        public const float SmoothDampingDecayRate = 20.0f; // 约 150ms 吸收 95% 误差
        public const float SnapToleranceDistance = 64.0f; // 约 1.3 格，容差内采用平滑阻尼，超出直接瞬移拉回

        public Vector2 GetCurrentWorldPos()
        {
            if (_isMoving)
            {
                float t = _moveDuration > 0f ? Math.Clamp(_moveTimer / _moveDuration, 0.0f, 1.0f) : 1.0f;
                return _moveStartWorld.Lerp(_moveTargetWorld, t);
            }
            return new Vector2(TileX * 48, TileY * 32);
        }

        public Vector2 GetCurrentRenderPos()
        {
            return GetCurrentWorldPos() + SmoothOffset;
        }

        public void SetTilePos(int tx, int ty)
        {
            TileX = tx;
            TileY = ty;
            _targetTileX = tx;
            _targetTileY = ty;
            _isMoving = false;
            _pathQueue.Clear();
            SmoothOffset = Vector2.Zero;

            var staticWorld = new Vector2(tx * 48, ty * 32);
            SyncCameraAndPlayer(staticWorld);
        }

        /// <summary>
        /// 服务端权威位置纠偏与碰撞/阻挡/弱网拉回校准
        /// - 若物理偏移在 1 格容差以内 (<= 64 像素): 阻尼吸收误差，视觉无突兀瞬移，且终止错误目标格防覆写
        /// - 若物理偏移超过容差 (如传送、被冲撞退、撞墙硬阻挡): 强制瞬间瞬移拉回并恢复站立
        /// </summary>
        public void ReconcilePosition(int tx, int ty, int dir)
        {
            Vector2 serverWorld = new Vector2(tx * 48, ty * 32);
            Vector2 currentRender = GetCurrentRenderPos();
            float dist = currentRender.DistanceTo(serverWorld);

            if (Engine.GetMainLoop() != null)
            {
                _moveCooldownUntilMs = Time.GetTicksMsec() + 180;
            }

            if (dist <= SnapToleranceDistance)
            {
                // 容差范围内：将当前视觉坐标相对服务端权威坐标的差值赋给 SmoothOffset
                // 同时必须同步重置 _targetTileX/Y 并停止步进，防止步进结束时将 TileX/Y 覆写回被服务端拒绝的非法格！
                TileX = tx;
                TileY = ty;
                _targetTileX = tx;
                _targetTileY = ty;
                _isMoving = false;
                _pathQueue.Clear();
                Direction = dir & 7;
                Anim?.SetAction(ActionType.Stand, Direction);

                SmoothOffset = currentRender - serverWorld;
            }
            else
            {
                // 超出阈值 (如传送门、防外挂拉回、大距离阻挡)，执行绝对瞬移
                SetTilePos(tx, ty);
                Direction = dir & 7;
                Anim?.SetAction(ActionType.Stand, Direction);
            }
        }

        private float _stunTimer = 0f;
        public bool IsStunned => _stunTimer > 0f;

        private float _attackLockTimer = 0f;
        public bool IsAttacking => _attackLockTimer > 0f && Anim.Action == ActionType.Hit;

        public void TriggerStun(float durationSec, ActionType animAction = ActionType.Struck)
        {
            _stunTimer = Math.Max(_stunTimer, durationSec);
            _attackLockTimer = 0f;
            _isMoving = false;
            _pathQueue.Clear();
            Anim.SetAction(animAction, Direction);
        }

        /// <summary>
        /// 盛大官方 1.5 物理出刀攻击动作锁 (450ms 严格锁定，保护出刀动画完整播放，防止被走动/转向打断)
        /// </summary>
        public void PlayHit(int dir = -1)
        {
            if (IsStunned || IsDead) return;
            if (dir >= 0) Direction = dir & 7;
            _attackLockTimer = 0.45f;
            _isMoving = false;
            _pathQueue.Clear();
            Anim.SetAction(ActionType.Hit, Direction);
            AudioManager.Instance?.PlayWeaponSwingSound(WeaponType);
        }

        /// <summary>
        /// 盛大官方 1.5 割肉/挖肉动作 (IDA sub_478148 / ButcherCorpse)
        /// 播放弯腰挥刀动作并调度 40 号割肉音效
        /// </summary>
        public void PlayButcher(int dir = -1)
        {
            if (IsStunned || IsDead) return;
            if (dir >= 0) Direction = dir & 7;
            _isMoving = false;
            _pathQueue.Clear();
            Anim.SetAction(ActionType.Hit, Direction);
            AudioManager.Instance?.PlaySoundId(40);
        }

        public void TurnTo(int dir)
        {
            if (_isMoving || IsStunned || IsDead || IsAttacking) return;
            int newDir = dir & 7;
            if (Direction == newDir && Anim.Action == ActionType.Stand) return;
            Direction = newDir;
            Anim.SetAction(ActionType.Stand, Direction);
            EmitSignal(SignalName.Turned, Direction);
        }

        public void StartStepMovement(int tx, int ty, int stepTiles, ActionType action, int dir)
        {
            if (_isMoving || IsStunned || IsDead || IsAttacking) return;
            if (Engine.GetMainLoop() != null && Time.GetTicksMsec() < _moveCooldownUntilMs) return;

            _isMoving = true;
            _moveStartWorld = new Vector2(TileX * 48, TileY * 32);
            _moveTargetWorld = new Vector2(tx * 48, ty * 32);
            // 盛大官方 1.5 Actor.pas SetAction (0x44C800) 铁律：迈步瞬间立即更新逻辑瓦片坐标 m_nCurrX/Y = tx/ty，
            // 通过 m_nShiftX/Y (_moveStartWorld -> _moveTargetWorld) 进行平滑像素插值，防止服务端回包时误判坐标差而拉回打断！
            TileX = tx;
            TileY = ty;
            _targetTileX = tx;
            _targetTileY = ty;
            Direction = dir & 7;
            _moveTimer = 0f;
            // IDA sub_4CAF08 / sub_4CB11C：服务端行走与奔跑均有 600ms 速度门禁，客户端行走设为 0.65s (跑动 0.60s)
            _moveDuration = (action == ActionType.Run) ? 0.60f : 0.65f;
            Anim.SetAction(action, dir);

            // 触发官方 1:1 左右脚交替脚步声 (对齐 Delphi SoundUtil.pas: s_walk_ground / s_run_ground)
            if (action == ActionType.Run)
            {
                AudioManager.Instance?.PlayRunStepSound();
            }
            else
            {
                AudioManager.Instance?.PlayWalkStepSound();
            }

            EmitSignal(SignalName.StepStarted, tx, ty, Direction, action == ActionType.Run);
        }

        public void SetPath(IEnumerable<Vector2I> path, bool run = true)
        {
            _pathQueue.Clear();
            _isPathRun = run;
            foreach (var p in path)
            {
                if (p.X == TileX && p.Y == TileY) continue;
                _pathQueue.Add(p);
            }

            if (!_isMoving && _pathQueue.Count > 0)
            {
                ConsumeNextPathStep();
            }
        }

        private void ConsumeNextPathStep()
        {
            if (_pathQueue.Count == 0) return;

            var nextTile = _pathQueue[0];
            _pathQueue.RemoveAt(0);
            int ndx = nextTile.X - TileX;
            int ndy = nextTile.Y - TileY;
            int ndir = InputPlugin.CalcDirection(new Vector2(ndx, ndy));

            // 判断是否可跑步进 (路径中下个节点也是相同方向且连续走2格)
            if (_isPathRun && _pathQueue.Count > 0 && (_pathQueue[0].X - nextTile.X) == ndx && (_pathQueue[0].Y - nextTile.Y) == ndy)
            {
                var runTile = _pathQueue[0];
                _pathQueue.RemoveAt(0);
                StartStepMovement(runTile.X, runTile.Y, 2, ActionType.Run, ndir);
            }
            else
            {
                StartStepMovement(nextTile.X, nextTile.Y, 1, ActionType.Walk, ndir);
            }
        }

        public void SyncCameraAndPlayer(Vector2 worldPos)
        {
            int curTileY = (int)MathF.Round(worldPos.Y / 32.0f);
            ZIndex = IsDead ? -1600 : Math.Clamp(curTileY * 2, 1, 3500);

            if (_mapRenderer == null)
            {
                Position = new Vector2(400, 225);
                return;
            }

            float camX = worldPos.X - 400.0f;
            float camY = worldPos.Y - 225.0f;
            _mapRenderer.SetCameraPos(camX, camY);
            var actualCam = _mapRenderer.GetCameraPos();
            Position = worldPos - actualCam;
        }

        public override void _Process(double delta)
        {
            float dt = (float)delta;

            if (_nameLabel != null)
            {
                _nameLabel.Visible = !IsDead;
            }
            if (_hpBarBg != null && _hpBarFill != null)
            {
                _hpBarBg.Visible = !IsDead;
                _hpBarFill.Visible = !IsDead;
                float ratio = MaxHp > 0 ? Mathf.Clamp((float)Hp / MaxHp, 0f, 1f) : 1f;
                _hpBarFill.Size = new Vector2(28 * ratio, 2);
            }

            if (_stunTimer > 0f)
            {
                _stunTimer -= dt;
                if (_stunTimer <= 0f)
                {
                    _stunTimer = 0f;
                    Anim.SetAction(ActionType.Stand, Direction);
                }
            }

            if (_attackLockTimer > 0f)
            {
                _attackLockTimer -= dt;
                if (_attackLockTimer <= 0f)
                {
                    _attackLockTimer = 0f;
                    if (Anim.Action == ActionType.Hit)
                    {
                        Anim.SetAction(ActionType.Stand, Direction);
                    }
                }
            }

            Anim.Update(dt * 1000.0f);
            UpdateRenderFrames();

            // 弱网与纠偏误差平滑阻尼吸收 (指数衰减收敛至 0)
            if (SmoothOffset.LengthSquared() > 0.005f)
            {
                float decayFactor = MathF.Exp(-SmoothDampingDecayRate * dt);
                SmoothOffset *= decayFactor;
                if (SmoothOffset.LengthSquared() < 0.01f)
                {
                    SmoothOffset = Vector2.Zero;
                }
            }

            if (_isMoving)
            {
                _moveTimer += dt;
                float t = Math.Clamp(_moveTimer / _moveDuration, 0.0f, 1.0f);
                var curWorld = _moveStartWorld.Lerp(_moveTargetWorld, t);
                SyncCameraAndPlayer(curWorld + SmoothOffset);
                if (t >= 1.0f)
                {
                    _isMoving = false;
                    TileX = _targetTileX;
                    TileY = _targetTileY;
                    _mapRenderer?.SetPlayerTile(TileX, TileY);
                    EmitSignal(SignalName.TileStepped, new Vector2I(TileX, TileY));
                    if (_pathQueue.Count > 0)
                    {
                        ConsumeNextPathStep();
                    }
                    else
                    {
                        EmitSignal(SignalName.StepCompleted);
                        if (!_isMoving && !Anim.IsBusy())
                        {
                            Anim.SetAction(ActionType.Stand, Direction);
                        }
                    }
                }
            }
            else
            {
                var staticWorld = new Vector2(TileX * 48, TileY * 32);
                SyncCameraAndPlayer(staticWorld + SmoothOffset);
            }
        }

        private void UpdateRenderFrames()
        {
            if (_wilHum == null || _bodySprite == null) return;

            // 盛大官方 1.5 人物纸娃娃切片原生物理真值 (严格 1:1 对齐 Delphi THumActor.DrawChr & Actor.pas:3663)
            // dx + m_nPx + m_nShiftX, dy + m_nPy + m_nShiftY
            // dx, dy 即瓦片左上角原点，切片相对瓦片绝对坐标为 (anchor.X, anchor.Y)，严禁叠加额外偏移！
            int frameIdx = Anim.GetHumFrame(BodyDressType);
            var tex = _wilHum.GetFrame(frameIdx);
            if (tex != null)
            {
                _bodySprite.Texture = tex;
                var anchor = _wilHum.GetAnchor(frameIdx);
                _bodySprite.Position = new Vector2(anchor.X, anchor.Y);
            }

            if (_weaponSprite != null)
            {
                if (_wilWeapon != null && WeaponType > 0)
                {
                    int wIdx = SpriteEngine.CalcWeaponFrame(Anim.Action, Anim.Direction, Anim.AnimFrame, WeaponType);
                    var wTex = _wilWeapon.GetFrame(wIdx);
                    if (wTex != null)
                    {
                        _weaponSprite.Texture = wTex;
                        var wAnchor = _wilWeapon.GetAnchor(wIdx);
                        _weaponSprite.Position = new Vector2(wAnchor.X, wAnchor.Y);
                        bool inFront = SpriteEngine.IsWeaponInFront(Anim.Action, Anim.Direction, Anim.AnimFrame);
                        _weaponSprite.ZIndex = inFront ? 1 : -1;
                        _weaponSprite.Visible = true;
                    }
                    else _weaponSprite.Visible = false;
                }
                else _weaponSprite.Visible = false;
            }

            if (_hairSprite != null)
            {
                if (_wilHair != null)
                {
                    int hIdx = SpriteEngine.CalcHairFrame(Anim.Action, Anim.Direction, Anim.AnimFrame, HairStyle, Gender);
                    var hTex = _wilHair.GetFrame(hIdx);
                    if (hTex != null)
                    {
                        _hairSprite.Texture = hTex;
                        var hAnchor = _wilHair.GetAnchor(hIdx);
                        _hairSprite.Position = new Vector2(hAnchor.X, hAnchor.Y);
                        _hairSprite.ZIndex = 2;
                        _hairSprite.Visible = true;
                    }
                    else _hairSprite.Visible = false;
                }
                else _hairSprite.Visible = false;
            }
        }

        public void ApplyDie()
        {
            IsDead = true;
            Anim.SetAction(ActionType.Die);
            UpdateNameLabelText();
            // 播放官方死亡惨叫音效 (男 144 / 女 145，对齐 Delphi SoundUtil.pas:s_man_die/s_wom_die) 与死亡音乐 Game over2.wav
            int dieSfx = Gender == 0 ? AudioManager.SoundId.ManDie : AudioManager.SoundId.WomDie;
            AudioManager.Instance?.PlaySoundId(dieSfx);
            AudioManager.Instance?.PlayBgm("Game over2.wav", loop: false);
            EmitSignal(SignalName.PlayerDied);
        }

        public void ApplyRevive(int newHp, int newMp)
        {
            IsDead = false;
            Hp = newHp > 0 ? newHp : Math.Max(1, MaxHp);
            Mp = newMp > 0 ? newMp : Math.Max(1, MaxMp);
            Anim.SetAction(ActionType.Stand, Direction);
            UpdateNameLabelText();
            AudioManager.Instance?.PlayBgm("main_theme.wav", loop: true);
            EmitSignal(SignalName.Revived);
            EmitSignal(SignalName.HealthChanged, Hp, MaxHp, Mp, MaxMp);
        }
    }
}
