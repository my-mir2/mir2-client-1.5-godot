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

        public int Uid { get; set; } = 0;
        public string CharacterName { get; set; } = "玩家";
        public int Level { get; set; } = 1;
        public int Hp { get; set; } = 100;
        public int MaxHp { get; set; } = 100;
        public int Mp { get; set; } = 50;
        public int MaxMp { get; set; } = 50;
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

        public string GuildName { get; set; } = string.Empty;
        public string GuildRank { get; set; } = string.Empty;

        private Sprite2D? _bodySprite;
        private Sprite2D? _weaponSprite;
        private Sprite2D? _hairSprite;
        private Label? _nameLabel;

        private WilLoader? _wilHum;
        private WilLoader? _wilWeapon;
        private WilLoader? _wilHair;
        private MapRenderer? _mapRenderer;

        public AnimState Anim { get; } = new();

        private bool _isMoving = false;
        private Vector2 _moveStartWorld = Vector2.Zero;
        private Vector2 _moveTargetWorld = Vector2.Zero;
        private float _moveDuration = 0.5f;
        private float _moveTimer = 0f;
        private int _targetTileX = 0;
        private int _targetTileY = 0;

        private readonly List<Vector2I> _pathQueue = new();

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
                HorizontalAlignment = HorizontalAlignment.Center
            };
            _nameLabel.AddThemeFontSizeOverride("font_size", 11);
            _nameLabel.AddThemeColorOverride("font_color", new Color(1, 1, 1));
            _nameLabel.AddThemeColorOverride("font_outline_color", new Color(0, 0, 0));
            _nameLabel.AddThemeConstantOverride("outline_size", 2);
            AddChild(_nameLabel);

            UpdateNameLabelText();
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
            if (!string.IsNullOrEmpty(GuildName))
            {
                string rankStr = !string.IsNullOrEmpty(GuildRank) ? " " + GuildRank : "";
                _nameLabel.Text = $"【{GuildName}{rankStr}】\n{CharacterName}";
                _nameLabel.Position = new Vector2(-20, -48);
                _nameLabel.CustomMinimumSize = new Vector2(136, 32);
            }
            else
            {
                _nameLabel.Text = CharacterName;
                _nameLabel.Position = new Vector2(8, -34);
                _nameLabel.CustomMinimumSize = new Vector2(80, 20);
            }
        }

        public void SetTilePos(int tx, int ty)
        {
            TileX = tx;
            TileY = ty;
            _targetTileX = tx;
            _targetTileY = ty;
            _isMoving = false;
            _pathQueue.Clear();

            var staticWorld = new Vector2(tx * 48, ty * 32);
            SyncCameraAndPlayer(staticWorld);
        }

        public void StartStepMovement(int tx, int ty, int stepTiles, ActionType action, int dir)
        {
            _isMoving = true;
            _moveStartWorld = new Vector2(TileX * 48, TileY * 32);
            _moveTargetWorld = new Vector2(tx * 48, ty * 32);
            _targetTileX = tx;
            _targetTileY = ty;
            Direction = dir & 7;
            _moveTimer = 0f;
            _moveDuration = stepTiles == 1 ? 0.78f : 0.66f;
            Anim.SetAction(action, dir);
        }

        public void SyncCameraAndPlayer(Vector2 worldPos)
        {
            if (_mapRenderer == null)
            {
                Position = new Vector2(400, 300);
                return;
            }

            float camX = worldPos.X - 400.0f;
            float camY = worldPos.Y - 300.0f;
            _mapRenderer.SetCameraPos(camX, camY);
            var actualCam = _mapRenderer.GetCameraPos();
            Position = worldPos - actualCam;
        }

        public override void _Process(double delta)
        {
            float dt = (float)delta;
            Anim.Update(dt * 1000.0f);
            UpdateRenderFrames();

            if (_isMoving)
            {
                _moveTimer += dt;
                float t = Math.Clamp(_moveTimer / _moveDuration, 0.0f, 1.0f);
                var curWorld = _moveStartWorld.Lerp(_moveTargetWorld, t);
                SyncCameraAndPlayer(curWorld);
                if (t >= 1.0f)
                {
                    _isMoving = false;
                    TileX = _targetTileX;
                    TileY = _targetTileY;
                    _mapRenderer?.SetPlayerTile(TileX, TileY);
                    EmitSignal(SignalName.TileStepped, new Vector2I(TileX, TileY));
                    if (_pathQueue.Count == 0 && !Anim.IsBusy())
                    {
                        Anim.SetAction(ActionType.Stand, Direction);
                    }
                }
            }
            else
            {
                var staticWorld = new Vector2(TileX * 48, TileY * 32);
                SyncCameraAndPlayer(staticWorld);
            }
        }

        private void UpdateRenderFrames()
        {
            if (_wilHum == null || _bodySprite == null) return;

            var baseAnchor = new Vector2(24, 32);
            int frameIdx = Anim.GetHumFrame(BodyDressType);
            var tex = _wilHum.GetFrame(frameIdx);
            if (tex != null)
            {
                _bodySprite.Texture = tex;
                var anchor = _wilHum.GetAnchor(frameIdx);
                _bodySprite.Position = baseAnchor + new Vector2(anchor.X, anchor.Y);
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
                        _weaponSprite.Position = baseAnchor + new Vector2(wAnchor.X, wAnchor.Y);
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
                    int hIdx = SpriteEngine.CalcHairFrame(Anim.Action, Anim.Direction, Anim.AnimFrame, HairStyle);
                    var hTex = _wilHair.GetFrame(hIdx);
                    if (hTex != null)
                    {
                        _hairSprite.Texture = hTex;
                        var hAnchor = _wilHair.GetAnchor(hIdx);
                        _hairSprite.Position = baseAnchor + new Vector2(hAnchor.X, hAnchor.Y);
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
            AudioManager.Instance?.PlaySoundId(11);
            EmitSignal(SignalName.PlayerDied);
        }

        public void ApplyRevive(int newHp, int newMp)
        {
            IsDead = false;
            Hp = newHp > 0 ? newHp : Math.Max(1, MaxHp);
            Mp = newMp > 0 ? newMp : Math.Max(1, MaxMp);
            Anim.SetAction(ActionType.Stand, Direction);
            EmitSignal(SignalName.Revived);
            EmitSignal(SignalName.HealthChanged, Hp, MaxHp, Mp, MaxMp);
        }
    }
}
