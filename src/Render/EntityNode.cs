// ============================================================================
// Mir2 客户端 - 大世界通用实体渲染节点 (怪物 / NPC / 他人) (1:1 盛大 1.5 官方实证)
// 官方实证：IDA sub_495EBC & sub_496120 怪物外形贴图库与动作帧位移算法
// ============================================================================

using System;
using Godot;
using Mir2Client.Audio;
using Mir2Client.Game;
using Mir2Client.Utils;

namespace Mir2Client.Render
{
    public partial class EntityNode : Node2D
    {
        public int Uid { get; set; }
        public string EntityName { get; set; } = string.Empty;
        public EntityManager.EntityType Type { get; set; } = EntityManager.EntityType.Monster;
        public int Appr { get; set; } = 0;
        public int TileX { get; set; }
        public int TileY { get; set; }
        public int Direction { get; set; } = 2;
        public int Hp { get; set; } = 100;
        public int MaxHp { get; set; } = 100;
        public bool IsDead { get; set; } = false;
        public bool IsCorpse { get; set; } = false;
        public int PetLevel { get; set; } = 0;
        public string MasterName { get; set; } = string.Empty;

        private Sprite2D? _sprite;
        private Label? _nameLabel;
        private ColorRect? _hpBarBg;
        private ColorRect? _hpBarFill;
        private Label? _damageLabel;
        private float _hpBarHideTimer = 0f;
        private float _damageLabelTimer = 0f;

        private WilLoader? _loader;
        private MapRenderer? _mapRenderer;

        public DeadReckoningEngine.EntityState MovementState { get; } = new();

        public ActionType CurrentAction { get; private set; } = ActionType.Stand;
        private float _animTimer = 0f;
        private int _currentFrame = 0;
        private bool _isActionLocked = false;
        private float _actionDuration = 0f;

        public override void _Ready()
        {
            DeadReckoningEngine.InitializeEntity(MovementState, TileX, TileY, (byte)Direction);
            SetupVisuals();
            UpdatePosition();
            UpdateFrameGraphic();
        }

        private void SetupVisuals()
        {
            _sprite = new Sprite2D { Centered = false };
            AddChild(_sprite);

            // 头顶名字 (怪物为浅黄，NPC 为水绿，带 2px 黑色轮廓描边)
            _nameLabel = new Label
            {
                Name = "EntityNameLabel",
                HorizontalAlignment = HorizontalAlignment.Center,
                Position = new Vector2(-40, -45),
                Size = new Vector2(128, 16)
            };
            _nameLabel.AddThemeFontSizeOverride("font_size", 10);
            _nameLabel.AddThemeColorOverride("font_outline_color", new Color(0, 0, 0, 0.9f));
            _nameLabel.AddThemeConstantOverride("outline_size", 2);
            _nameLabel.Text = EntityName;
            UpdateNameColor();
            AddChild(_nameLabel);
        }

        private int _poisonMask = 0;
        private int _pkPoint = 0;

        public void UpdatePoison(int poisonMask)
        {
            _poisonMask = poisonMask;
            UpdateNameColor();
        }

        public void UpdatePk(int pkPoint)
        {
            _pkPoint = pkPoint;
            UpdateNameColor();
        }

        public void SetPetInfo(int level, string masterName)
        {
            PetLevel = Math.Clamp(level, 0, 7);
            MasterName = masterName ?? string.Empty;
            UpdateNameColor();
        }

        private void UpdateNameColor()
        {
            if (_nameLabel == null) return;

            string displayName = EntityName;
            if (!string.IsNullOrEmpty(MasterName))
            {
                displayName = $"{MasterName} 的 {EntityName}";
            }
            if (PetLevel > 0)
            {
                displayName += $" ({PetLevel}级)";
            }
            _nameLabel.Text = displayName;

            if (IsDead)
            {
                _nameLabel.AddThemeColorOverride("font_color", new Color(0.5f, 0.5f, 0.5f));
                return;
            }

            if (_poisonMask == 3) // 双毒兼具
            {
                _nameLabel.AddThemeColorOverride("font_color", new Color(1.0f, 0.3f, 0.9f)); // 紫红色
            }
            else if ((_poisonMask & 1) != 0) // 绿毒
            {
                _nameLabel.AddThemeColorOverride("font_color", new Color(0.15f, 1.0f, 0.2f)); // 荧光绿
            }
            else if ((_poisonMask & 2) != 0) // 红毒
            {
                _nameLabel.AddThemeColorOverride("font_color", new Color(1.0f, 0.25f, 0.25f)); // 暗红
            }
            else if (_pkPoint >= 200) // 官方红名恶徒
            {
                _nameLabel.AddThemeColorOverride("font_color", new Color(1.0f, 0.15f, 0.15f)); // 鲜红色
            }
            else if (_pkPoint >= 100) // 官方黄名警告
            {
                _nameLabel.AddThemeColorOverride("font_color", new Color(1.0f, 0.95f, 0.2f)); // 亮黄色
            }
            else if (PetLevel > 0 || !string.IsNullOrEmpty(MasterName)) // 盛大官方 1.5 随从名牌变色阶梯 (0~7级)
            {
                Color petColor = PetLevel switch
                {
                    0 => new Color(1.0f, 1.0f, 1.0f),       // 0级: 纯白
                    1 or 2 => new Color(0.85f, 1.0f, 0.4f),  // 1~2级: 黄绿
                    3 or 4 => new Color(0.2f, 1.0f, 0.4f),   // 3~4级: 翠绿
                    5 or 6 => new Color(0.3f, 0.8f, 1.0f),   // 5~6级: 天蓝
                    7 => new Color(0.5f, 0.4f, 1.0f),        // 7级: 官方招牌深蓝紫
                    _ => new Color(1.0f, 1.0f, 1.0f)
                };
                _nameLabel.AddThemeColorOverride("font_color", petColor);
            }
            else
            {
                Color nameColor = Type switch
                {
                    EntityManager.EntityType.Npc => new Color(0.45f, 1.0f, 0.45f),
                    EntityManager.EntityType.Monster => new Color(1.0f, 0.95f, 0.5f),
                    _ => new Color(1.0f, 1.0f, 1.0f)
                };
                _nameLabel.AddThemeColorOverride("font_color", nameColor);
            }

            // 官方 1.5 怪物头顶微型血条 (36x4 像素)
            _hpBarBg = new ColorRect
            {
                Name = "EntityHpBg",
                Size = new Vector2(36, 4),
                Position = new Vector2(6, -26),
                Color = new Color(0.1f, 0.1f, 0.1f, 0.85f),
                Visible = false
            };
            AddChild(_hpBarBg);

            _hpBarFill = new ColorRect
            {
                Name = "EntityHpFill",
                Size = new Vector2(34, 2),
                Position = new Vector2(1, 1),
                Color = new Color(0.2f, 0.95f, 0.2f),
                Visible = false
            };
            _hpBarBg.AddChild(_hpBarFill);

            // 受击浮动飘字 (-Damage)
            _damageLabel = new Label
            {
                Name = "DamageLabel",
                HorizontalAlignment = HorizontalAlignment.Center,
                Position = new Vector2(-16, -60),
                Size = new Vector2(80, 20),
                Visible = false
            };
            _damageLabel.AddThemeFontSizeOverride("font_size", 12);
            _damageLabel.AddThemeColorOverride("font_color", new Color(1.0f, 0.25f, 0.25f));
            _damageLabel.AddThemeColorOverride("font_outline_color", new Color(0, 0, 0, 1.0f));
            _damageLabel.AddThemeConstantOverride("outline_size", 2);
            AddChild(_damageLabel);
        }

        public void SetLoader(WilLoader? loader)
        {
            _loader = loader;
            UpdateFrameGraphic();
        }

        public void SetMapRenderer(MapRenderer? mr)
        {
            _mapRenderer = mr;
            UpdatePosition();
        }

        public void SetTilePos(int tx, int ty, int dir = -1)
        {
            TileX = tx;
            TileY = ty;
            if (dir >= 0) Direction = dir & 7;
            DeadReckoningEngine.InitializeEntity(MovementState, TileX, TileY, (byte)Direction);
            UpdatePosition();
        }

        public void StartStepMovement(int targetX, int targetY, int dir, bool isRun)
        {
            if (IsCorpse || IsDead) return;
            Direction = dir & 7;
            TileX = targetX;
            TileY = targetY;
            DeadReckoningEngine.StartMovement(MovementState, targetX, targetY, (byte)Direction, isRun);
            PlayAction(isRun ? ActionType.Run : ActionType.Walk, Direction, MovementState.MoveDuration);
            UpdatePosition();
        }

        public void ReconcilePosition(int serverX, int serverY, byte dir)
        {
            TileX = serverX;
            TileY = serverY;
            Direction = dir & 7;
            DeadReckoningEngine.ReconcileServerPosition(MovementState, serverX, serverY, (byte)Direction);
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            var worldPos = DeadReckoningEngine.GetRenderPosition(MovementState);
            if (_mapRenderer != null)
            {
                Position = _mapRenderer.WorldToScreen(worldPos);
            }
            else
            {
                Position = worldPos;
            }
            // 依据视觉 Y 坐标动态计算 ZIndex 确保透视深度排序
            int currentTileY = (int)MathF.Round(worldPos.Y / 32.0f);
            ZIndex = Math.Clamp(currentTileY, 1, 99);
        }

        public void PlayAction(ActionType action, int dir = -1, float duration = 0.4f)
        {
            if (IsCorpse) return;
            if (dir >= 0) Direction = dir & 7;

            CurrentAction = action;
            _animTimer = 0f;
            _currentFrame = 0;
            _actionDuration = duration;
            _isActionLocked = (action == ActionType.Hit || action == ActionType.Struck || action == ActionType.Die);

            // 盛大官方 1.5 怪物 80 种生态帧率与音效统一调度
            var monDef = OfficialMonsterCompendium.GetByName(EntityName) ?? OfficialMonsterCompendium.GetByAppr(Appr);
            if (monDef != null)
            {
                if (action == ActionType.Hit && monDef.SoundAttack > 0)
                {
                    AudioManager.Instance?.PlaySoundId(monDef.SoundAttack);
                }
                else if (action == ActionType.Die && monDef.SoundDie > 0)
                {
                    AudioManager.Instance?.PlaySoundId(monDef.SoundDie);
                }
            }

            UpdateFrameGraphic();
        }

        private float _corpseRemainTimer = 60.0f; // 盛大官方 1.5 规则: 尸体存留 60 秒后老化腐烂沉入地底
        public bool IsShinsuProne { get; private set; } = false; // 神兽趴下防御姿态
        private float _shinsuIdleTimer = 0f;

        public void TriggerMiss()
        {
            if (_damageLabel != null)
            {
                _damageLabel.Text = "MISS";
                _damageLabel.Position = new Vector2(-40, -95);
                _damageLabel.Visible = true;
                _damageLabel.Modulate = new Color(0.2f, 1.0f, 0.4f, 1.0f); // 官方原版亮绿 MISS
                _damageLabelTimer = 0.8f;
            }
        }

        public void TriggerStruck(int damage, int curHp)
        {
            Hp = curHp;
            if (damage <= 0)
            {
                TriggerMiss();
                return;
            }

            PlayAction(ActionType.Struck, Direction, 0.3f);
            AudioManager.Instance?.PlaySoundId(2);

            // 显示微型血条并维持 3 秒
            _hpBarHideTimer = 3.0f;
            if (_hpBarBg != null && _hpBarFill != null)
            {
                _hpBarBg.Visible = true;
                _hpBarFill.Visible = true;
                float ratio = MaxHp > 0 ? Mathf.Clamp((float)Hp / MaxHp, 0f, 1f) : 0f;
                _hpBarFill.Size = new Vector2(34 * ratio, 2);
                _hpBarFill.Color = ratio > 0.5f ? new Color(0.2f, 0.95f, 0.2f) : (ratio > 0.2f ? new Color(1.0f, 0.8f, 0.1f) : new Color(0.95f, 0.2f, 0.2f));
            }

            // 弹出浮动受创飘字
            if (_damageLabel != null && damage > 0)
            {
                _damageLabel.Text = $"-{damage}";
                _damageLabel.Position = new Vector2(-40, -95);
                _damageLabel.Visible = true;
                _damageLabel.Modulate = new Color(1, 1, 1, 1);
                _damageLabelTimer = 0.8f;
            }

            if (Hp <= 0)
            {
                TriggerDeath();
            }
        }

        public void TriggerDeath()
        {
            IsDead = true;
            PlayAction(ActionType.Die, Direction, 0.8f);
            AudioManager.Instance?.PlaySoundId(4);

            if (_hpBarBg != null) _hpBarBg.Visible = false;
            if (_nameLabel != null)
            {
                _nameLabel.AddThemeColorOverride("font_color", new Color(0.5f, 0.5f, 0.5f));
            }
        }

        public override void _Process(double delta)
        {
            float dt = (float)delta;

            // 盛大官方 1.5 规则: 尸体存留 60 秒后老化腐烂沉入地底消除
            if (IsCorpse)
            {
                _corpseRemainTimer -= dt;
                if (_corpseRemainTimer <= 2.0f && _corpseRemainTimer > 0f)
                {
                    Modulate = new Color(1, 1, 1, _corpseRemainTimer / 2.0f);
                }
                else if (_corpseRemainTimer <= 0f)
                {
                    Visible = false;
                }
                return;
            }

            // 盛大官方 1.5 IDA sub_496120: 神兽无目标待机 3 秒转为趴下姿态
            if (EntityName.Contains("神兽"))
            {
                if (CurrentAction == ActionType.Stand)
                {
                    _shinsuIdleTimer += dt;
                    if (_shinsuIdleTimer >= 3.0f)
                    {
                        IsShinsuProne = true;
                    }
                }
                else
                {
                    _shinsuIdleTimer = 0f;
                    IsShinsuProne = false;
                }
            }

            DeadReckoningEngine.Update(MovementState, dt);
            UpdatePosition();

            _animTimer += dt;

            // 血条 3 秒倒计时自动隐退
            if (_hpBarHideTimer > 0f)
            {
                _hpBarHideTimer -= dt;
                if (_hpBarHideTimer <= 0f && _hpBarBg != null)
                {
                    _hpBarBg.Visible = false;
                }
            }

            // 浮动伤害字向上飘动渐隐
            if (_damageLabelTimer > 0f && _damageLabel != null)
            {
                _damageLabelTimer -= dt;
                _damageLabel.Position += new Vector2(0, -35f * dt);
                _damageLabel.Modulate = new Color(1, 1, 1, Mathf.Clamp(_damageLabelTimer / 0.8f, 0f, 1f));
                if (_damageLabelTimer <= 0f)
                {
                    _damageLabel.Visible = false;
                }
            }

            // 动画帧步进
            if (Type == EntityManager.EntityType.Npc)
            {
                _currentFrame = (int)(_animTimer * 4.0f) % 6;
            }
            else
            {
                int maxF = CurrentAction switch
                {
                    ActionType.Stand => 4,
                    ActionType.Walk => 6,
                    ActionType.Hit => 6,
                    ActionType.Struck => 2,
                    ActionType.Die => 6,
                    _ => 4
                };

                if (_isActionLocked)
                {
                    float frameDuration = _actionDuration / Math.Max(maxF, 1);
                    _currentFrame = (int)(_animTimer / frameDuration);
                    if (_currentFrame >= maxF)
                    {
                        if (CurrentAction == ActionType.Die)
                        {
                            _currentFrame = maxF - 1; // 保持倒地尸体最后一帧
                            IsCorpse = true;
                            _isActionLocked = false;
                        }
                        else
                        {
                            _isActionLocked = false;
                            CurrentAction = ActionType.Stand;
                            _animTimer = 0f;
                            _currentFrame = 0;
                        }
                    }
                }
                else
                {
                    float fps = CurrentAction == ActionType.Stand ? 4.0f : 6.0f;
                    _currentFrame = (int)(_animTimer * fps) % maxF;
                }
            }

            UpdateFrameGraphic();
        }

        private void UpdateFrameGraphic()
        {
            if (_sprite == null || _loader == null) return;

            int frameIdx = CalcAbsoluteFrameIndex();
            Texture2D? tex = _loader.GetFrame(frameIdx);
            if (tex != null)
            {
                _sprite.Texture = tex;
                var anchor = _loader.GetAnchor(frameIdx);
                _sprite.Position = new Vector2(24 + anchor.X, 32 + anchor.Y);
                _sprite.Visible = true;
            }
        }

        /// <summary>
        /// 依据官方 IDA sub_495EBC & sub_496120 算法计算贴图绝对帧号
        /// </summary>
        private int CalcAbsoluteFrameIndex()
        {
            if (Type == EntityManager.EntityType.Npc)
            {
                return Appr * 10 + (_currentFrame % 6);
            }

            // 官方传奇 1.5 怪物帧偏移权威算法 (sub_496120)
            int baseOffset = MonsterWilPool.CalcMonsterBaseOffset(Appr);

            int dir = Direction & 7;
            return CurrentAction switch
            {
                ActionType.Stand => baseOffset + dir * 4 + (_currentFrame % 4),
                ActionType.Walk => baseOffset + 32 + dir * 6 + (_currentFrame % 6),
                ActionType.Hit => baseOffset + 80 + dir * 6 + (_currentFrame % 6),
                ActionType.Struck => baseOffset + 128 + dir * 2 + (_currentFrame % 2),
                ActionType.Die => baseOffset + 144 + dir * 6 + Math.Min(_currentFrame, 5),
                _ => baseOffset + dir * 4 + (_currentFrame % 4)
            };
        }
    }
}
