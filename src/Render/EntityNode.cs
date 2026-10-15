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
        private string _entityName = string.Empty;
        public string EntityName
        {
            get => _entityName;
            set
            {
                if (_entityName != value)
                {
                    _entityName = value;
                    UpdateNameColor();
                    if (Type == EntityManager.EntityType.Monster)
                    {
                        var spec = OfficialMonsterAssetRegistry.Resolve(value, _appr);
                        _loader = MonsterWilPool.Instance.GetLoader(spec.WilIndex);
                        UpdateFrameGraphic();
                    }
                }
            }
        }
        public EntityManager.EntityType Type { get; set; } = EntityManager.EntityType.Monster;
        private int _appr = 0;
        public int Appr
        {
            get => _appr;
            set
            {
                _appr = value;
                if (Type == EntityManager.EntityType.Monster)
                {
                    var spec = OfficialMonsterAssetRegistry.Resolve(EntityName, value);
                    _loader = MonsterWilPool.Instance.GetLoader(spec.WilIndex);
                    UpdateFrameGraphic();
                }
            }
        }
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

            // 头顶名字 (官方 1.5 PlayScn.pas SayY=-47 & DrawScrn.pas: 位于血条上方 -71，活体常显、死尸隐藏)
            _nameLabel = new Label
            {
                Name = "EntityNameLabel",
                HorizontalAlignment = HorizontalAlignment.Center,
                Position = new Vector2(-40, -71),
                Size = new Vector2(128, 16),
                Visible = !IsDead && !IsCorpse,
                ZAsRelative = false,
                ZIndex = 3600,
                MouseFilter = Control.MouseFilterEnum.Ignore
            };
            FontManager.ApplyOfficialWorldNameplateStyle(_nameLabel, new Color(1.0f, 1.0f, 1.0f));
            _nameLabel.Text = EntityName;
            AddChild(_nameLabel);

            // 官方 1.5 怪物头顶微型血条 (PlayScn.pas 0x471310 SayY=-47 & DrawScrn.pas 0x47A186 SayY-10=-57, Prguse2.wil 0/1 帧 30x4)
            bool showHp = Type != EntityManager.EntityType.Npc && !IsDead && !IsCorpse;
            _hpBarBg = new ColorRect
            {
                Name = "EntityHpBg",
                Size = new Vector2(30, 4),
                Position = new Vector2(9, -57),
                Color = new Color(0.05f, 0.05f, 0.05f, 0.95f),
                Visible = showHp,
                ZAsRelative = false,
                ZIndex = 3601,
                MouseFilter = Control.MouseFilterEnum.Ignore
            };
            AddChild(_hpBarBg);

            _hpBarFill = new ColorRect
            {
                Name = "EntityHpFill",
                Size = new Vector2(28, 2),
                Position = new Vector2(1, 1),
                Color = new Color(0.95f, 0.15f, 0.15f),
                Visible = showHp,
                MouseFilter = Control.MouseFilterEnum.Ignore
            };
            _hpBarBg.AddChild(_hpBarFill);

            // 受击浮动飘字 (-Damage)
            _damageLabel = new Label
            {
                Name = "DamageLabel",
                HorizontalAlignment = HorizontalAlignment.Center,
                Position = new Vector2(-16, -60),
                Size = new Vector2(80, 20),
                Visible = false,
                ZAsRelative = false,
                ZIndex = 3602,
                MouseFilter = Control.MouseFilterEnum.Ignore
            };
            FontManager.ApplyOfficialWorldNameplateStyle(_damageLabel, new Color(1.0f, 0.25f, 0.25f));
            AddChild(_damageLabel);

            UpdateNameColor();
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

            Color finalColor;
            if (IsDead)
            {
                finalColor = new Color(0.5f, 0.5f, 0.5f);
            }
            else if (_poisonMask == 3) // 双毒兼具
            {
                finalColor = new Color(1.0f, 0.3f, 0.9f); // 紫红色
            }
            else if ((_poisonMask & 1) != 0) // 绿毒
            {
                finalColor = new Color(0.15f, 1.0f, 0.2f); // 荧光绿
            }
            else if ((_poisonMask & 2) != 0) // 红毒
            {
                finalColor = new Color(1.0f, 0.25f, 0.25f); // 暗红
            }
            else if (_pkPoint >= 200) // 官方红名恶徒
            {
                finalColor = new Color(1.0f, 0.15f, 0.15f); // 鲜红色
            }
            else if (_pkPoint >= 100) // 官方黄名警告
            {
                finalColor = new Color(1.0f, 0.95f, 0.2f); // 亮黄色
            }
            else if (PetLevel > 0 || !string.IsNullOrEmpty(MasterName)) // 盛大官方 1.5 随从名牌变色阶梯 (0~7级)
            {
                finalColor = PetLevel switch
                {
                    0 => new Color(1.0f, 1.0f, 1.0f),       // 0级: 纯白
                    1 or 2 => new Color(0.85f, 1.0f, 0.4f),  // 1~2级: 黄绿
                    3 or 4 => new Color(0.2f, 1.0f, 0.4f),   // 3~4级: 翠绿
                    5 or 6 => new Color(0.3f, 0.8f, 1.0f),   // 5~6级: 天蓝
                    7 => new Color(0.5f, 0.4f, 1.0f),        // 7级: 官方招牌深蓝紫
                    _ => new Color(1.0f, 1.0f, 1.0f)
                };
            }
            else
            {
                finalColor = Type switch
                {
                    EntityManager.EntityType.Npc => new Color(0.45f, 1.0f, 0.45f),
                    EntityManager.EntityType.Monster => new Color(1.0f, 0.95f, 0.5f),
                    _ => new Color(1.0f, 1.0f, 1.0f)
                };
            }
            FontManager.ApplyOfficialWorldNameplateStyle(_nameLabel, finalColor);
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
            if ((IsDead || IsCorpse) && (TileX != 0 || TileY != 0)) return;
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
            if ((IsDead || IsCorpse) && (TileX != 0 || TileY != 0)) return;
            TileX = serverX;
            TileY = serverY;
            Direction = dir & 7;
            DeadReckoningEngine.ReconcileServerPosition(MovementState, serverX, serverY, (byte)Direction);
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            var worldPos = (IsDead || IsCorpse)
                ? new Vector2(TileX * 48, TileY * 32)
                : DeadReckoningEngine.GetRenderPosition(MovementState);
            if (_mapRenderer != null)
            {
                Position = _mapRenderer.WorldToScreen(worldPos);
            }
            else
            {
                Position = worldPos;
            }
            // 盛大官方 1.5 PlayScn.pas 深度排序铁律:
            // - 死亡尸体固定绘制在地表层 (-1600)，绝不遮挡存活角色或怪物
            // - 存活实体按当前视觉瓦片行号 currentTileY * 2 排序 (覆盖全图 0~1000 行，绝不截断在 99)
            if (IsDead || IsCorpse)
            {
                ZIndex = -1600;
            }
            else
            {
                int currentTileY = (int)MathF.Round(worldPos.Y / 32.0f);
                ZIndex = Math.Clamp(currentTileY * 2, 1, 3500);
            }
        }

        public void PlayAction(ActionType action, int dir = -1, float duration = 0.4f)
        {
            if (IsCorpse || (IsDead && action != ActionType.Die)) return;

            // 盛大官方 1.5 受击硬直保护 (Actor.pas)：
            // 若当前正处于受击后仰动画 (Struck) 且尚未播完，普通走跑与待机不得强制覆写，防止抽搐滑步
            if (_isActionLocked && CurrentAction == ActionType.Struck && action is ActionType.Walk or ActionType.Run or ActionType.Stand)
            {
                if (dir >= 0) Direction = dir & 7;
                return;
            }

            if (dir >= 0) Direction = dir & 7;

            CurrentAction = action;
            _animTimer = 0f;
            _currentFrame = 0;
            _actionDuration = duration;
            _isActionLocked = (action == ActionType.Hit || action == ActionType.Struck || action == ActionType.Die);

            // 盛大官方 1.5 怪物原生态音效公式调度 (200 + Appr * 10 + offset)
            if (Type == EntityManager.EntityType.Monster)
            {
                if (action == ActionType.Hit)
                {
                    AudioManager.Instance?.PlayMonsterSound(EntityName, Appr, 2);
                }
                else if (action == ActionType.Die)
                {
                    AudioManager.Instance?.PlayMonsterSound(EntityName, Appr, 5);
                }
            }

            UpdateFrameGraphic();
        }

        private float _corpseRemainTimer = 60.0f; // 盛大官方 1.5 规则: 尸体存留 60 秒后老化腐烂沉入地底
        public bool IsShinsuProne { get; private set; } = false; // 神兽趴下防御姿态
        private float _shinsuIdleTimer = 0f;

        private float GetStandingHeadTopY(int currentFrameIdx = -1)
        {
            if (_loader == null) return -47f;
            if (Type == EntityManager.EntityType.Npc)
            {
                _ = _loader.GetFrame(Appr * 10);
                var npcAnchor = _loader.GetAnchor(Appr * 10);
                return npcAnchor != Vector2I.Zero ? Math.Min(-47f, Math.Clamp((float)npcAnchor.Y, -68f, -47f)) : -47f;
            }
            var spec = OfficialMonsterAssetRegistry.Resolve(EntityName, Appr);
            _ = _loader.GetFrame(spec.BaseOffset);
            var standAnchor = _loader.GetAnchor(spec.BaseOffset);
            float topY = -47f;
            if (standAnchor != Vector2I.Zero)
            {
                topY = Math.Min(topY, Math.Clamp((float)standAnchor.Y, -72f, -47f));
            }
            if (currentFrameIdx >= 0)
            {
                var curAnchor = _loader.GetAnchor(currentFrameIdx);
                if (curAnchor != Vector2I.Zero)
                {
                    topY = Math.Min(topY, Math.Clamp((float)curAnchor.Y, -72f, -47f));
                }
            }
            return topY;
        }

        public void TriggerMiss()
        {
            if (_damageLabel != null)
            {
                float topY = GetStandingHeadTopY();
                _damageLabel.Text = "MISS";
                _damageLabel.Position = new Vector2(-16, topY - 24);
                _damageLabel.Visible = true;
                _damageLabel.Modulate = new Color(0.2f, 1.0f, 0.4f, 1.0f); // 官方原版亮绿 MISS
                _damageLabelTimer = 0.8f;
            }
        }

        public void TriggerStruck(int damage, int curHp)
        {
            if (IsDead || IsCorpse) return;
            Hp = curHp;
            if (damage <= 0)
            {
                TriggerMiss();
                return;
            }

            PlayAction(ActionType.Struck, Direction, 0.3f);
            // 播放官方刀剑砍中躯体音效 (62: s_struck_sword) + 怪物受击惨叫音效 (200 + Appr*10 + 4)
            AudioManager.Instance?.PlaySoundId(62);
            if (Type == EntityManager.EntityType.Monster)
            {
                AudioManager.Instance?.PlayMonsterSound(EntityName, Appr, 4);
            }

            // 更新官方 1.5 微型红血条 (DrawScrn.pas 0x47A186..0x47A231, Prguse2.wil 0/1 帧 30x4)
            _hpBarHideTimer = 3.0f;
            if (_hpBarBg != null && _hpBarFill != null)
            {
                bool showHp = Type != EntityManager.EntityType.Npc && !IsDead && !IsCorpse;
                _hpBarBg.Visible = showHp;
                _hpBarFill.Visible = showHp;
                float ratio = MaxHp > 0 ? Mathf.Clamp((float)Hp / MaxHp, 0f, 1f) : 0f;
                _hpBarFill.Size = new Vector2(28 * ratio, 2);
                _hpBarFill.Color = new Color(0.95f, 0.15f, 0.15f);
            }

            // 弹出浮动受创飘字 (紧随怪物体型顶线上方)
            if (_damageLabel != null && damage > 0)
            {
                float topY = GetStandingHeadTopY();
                _damageLabel.Text = $"-{damage}";
                _damageLabel.Position = new Vector2(-16, topY - 24);
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
            DeadReckoningEngine.InitializeEntity(MovementState, TileX, TileY, (byte)Direction);
            UpdatePosition();
            PlayAction(ActionType.Die, Direction, 0.8f);

            if (_hpBarBg != null) _hpBarBg.Visible = false;
            if (_hpBarFill != null) _hpBarFill.Visible = false;
            if (_nameLabel != null)
            {
                _nameLabel.Visible = false;
                UpdateNameColor();
            }
        }

        private void UpdateCorpseNameVisibility()
        {
            bool alive = !IsDead && !IsCorpse;
            if (_nameLabel != null)
            {
                _nameLabel.Visible = alive;
            }
            if (_hpBarBg != null && _hpBarFill != null)
            {
                bool showHp = alive && Type != EntityManager.EntityType.Npc;
                _hpBarBg.Visible = showHp;
                _hpBarFill.Visible = showHp;
                if (showHp)
                {
                    float ratio = MaxHp > 0 ? Mathf.Clamp((float)Hp / MaxHp, 0f, 1f) : 1f;
                    _hpBarFill.Size = new Vector2(28 * ratio, 2);
                }
            }
        }

        public override void _Process(double delta)
        {
            float dt = (float)delta;

            // 盛大官方 1.5 规则: 尸体存留 60 秒后老化腐烂沉入地底消除
            // 关键修复: 尸体状态下必须每帧调用 UpdatePosition() 跟随镜头偏移转换屏幕坐标，严禁锁死在屏幕固定位置跟玩家跑！
            if (IsCorpse)
            {
                UpdatePosition();
                UpdateCorpseNameVisibility();
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

            if (!IsDead)
            {
                DeadReckoningEngine.Update(MovementState, dt);
            }
            UpdatePosition();
            UpdateCorpseNameVisibility();

            _animTimer += dt;

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
                var spec = OfficialMonsterAssetRegistry.Resolve(EntityName, Appr);
                int maxF = CurrentAction switch
                {
                    ActionType.Stand => Math.Max(1, spec.StandFrames),
                    ActionType.Walk => Math.Max(1, spec.WalkFrames),
                    ActionType.Hit => Math.Max(1, spec.AttackFrames),
                    ActionType.Struck => Math.Max(1, spec.StruckFrames),
                    ActionType.Die => Math.Max(1, spec.DieFrames),
                    _ => Math.Max(1, spec.StandFrames)
                };

                if (_isActionLocked)
                {
                    float frameDuration = _actionDuration / Math.Max(maxF, 1);
                    _currentFrame = (int)(_animTimer / frameDuration);
                    if (_currentFrame >= maxF)
                    {
                        if (CurrentAction == ActionType.Die)
                        {
                            _currentFrame = maxF - 1; // 保持倒地尸体最后一帧 (躺平状态)
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
            bool isEmpty = tex == null || (tex.GetWidth() <= 4 && tex.GetHeight() <= 4);

            if (isEmpty && Type == EntityManager.EntityType.Monster)
            {
                var spec = OfficialMonsterAssetRegistry.Resolve(EntityName, Appr);
                int baseOffset = spec.BaseOffset;
                int standFrame = baseOffset + (Direction & 7) * 10;
                tex = _loader.GetFrame(standFrame);
                if (tex != null && tex.GetWidth() > 4) frameIdx = standFrame;
                else
                {
                    tex = _loader.GetFrame(baseOffset);
                    if (tex != null && tex.GetWidth() > 4) frameIdx = baseOffset;
                }
            }

            if (tex != null && tex.GetWidth() > 4)
            {
                _sprite.Texture = tex;
                var anchor = _loader.GetAnchor(frameIdx);

                Vector2 spritePos;
                if (anchor != Vector2I.Zero)
                {
                    spritePos = new Vector2(anchor.X, anchor.Y);
                }
                else
                {
                    int ox = 24 - tex.GetWidth() / 2;
                    int oy = 32 - tex.GetHeight();
                    spritePos = new Vector2(ox, oy);
                }

                _sprite.Position = spritePos;
                _sprite.Visible = true;

                // 依据官方 PlayScn.pas SayY=-47 与该怪物站立帧的真实头顶高度 (standTopY) 紧贴排布血条与名牌 (对齐 DrawScrn.pas 0x47A186)
                float standTopY = GetStandingHeadTopY(frameIdx);
                float hpY = standTopY - 10.0f;
                float nameY = hpY - 14.0f;
                if (_nameLabel != null)
                {
                    _nameLabel.Position = new Vector2(-40.0f, nameY);
                }
                if (_hpBarBg != null)
                {
                    _hpBarBg.Position = new Vector2(9.0f, hpY);
                }
                UpdateCorpseNameVisibility();
            }
        }

        /// <summary>
        /// 依据官方 1.5 权威切片与动作帧算法计算贴图绝对帧号 (严格对齐 Delphi Actor.pas CalcActorFrame 0x44A008)
        /// - MA10 (Mon1.wil 守卫/带刀护卫/弓箭守卫): 每方向 8 帧步长 (Stand=0, Walk=64, Hit=128, Struck=192+dir*2, Die=208+dir*8)
        /// - MA14 (标准 360 帧怪物): 每方向 10 帧步长 (Stand=0, Walk=80, Hit=160, Struck=240+dir*2, Die=260+dir*10)
        /// </summary>
        private int CalcAbsoluteFrameIndex()
        {
            if (Type == EntityManager.EntityType.Npc)
            {
                return Appr * 10 + (_currentFrame % 6);
            }

            var spec = OfficialMonsterAssetRegistry.Resolve(EntityName, Appr);
            int baseOffset = spec.BaseOffset;
            int dir = Direction & 7;
            int dieFrame = IsCorpse ? Math.Max(0, spec.DieFrames - 1) : _currentFrame;

            // 1. 守卫/大刀卫士/弓箭守卫 (Mon1.wil 280 帧 MA10 8 帧步长特异规则, Delphi Actor.pas:96)
            if (spec.WilIndex == 1)
            {
                int guardOffset = CurrentAction switch
                {
                    ActionType.Stand => dir * 8 + (_currentFrame % 4),
                    ActionType.Walk => 64 + dir * 8 + (_currentFrame % 6),
                    ActionType.Hit => 128 + dir * 8 + (_currentFrame % 6),
                    ActionType.Struck => 192 + dir * 2 + (_currentFrame % 2),
                    ActionType.Die => 208 + dir * 8 + Math.Min(dieFrame, Math.Min(spec.DieFrames, 8) - 1),
                    _ => dir * 8 + (_currentFrame % 4)
                };
                return baseOffset + guardOffset;
            }

            // 2. 普通怪物族 (MA14 10 帧步长权威规则, Delphi Actor.pas:106, ActDie start=260, frame=10, skip=0 -> 260 + dir*10 + frame)
            int frameOffset = CurrentAction switch
            {
                ActionType.Stand => dir * 10 + (_currentFrame % spec.StandFrames),
                ActionType.Walk => 80 + dir * 10 + (_currentFrame % spec.WalkFrames),
                ActionType.Hit => 160 + dir * 10 + (_currentFrame % spec.AttackFrames),
                ActionType.Struck => 240 + dir * 2 + (_currentFrame % spec.StruckFrames),
                ActionType.Die => 260 + dir * 10 + Math.Min(dieFrame, Math.Min(spec.DieFrames, 10) - 1),
                _ => dir * 10 + (_currentFrame % spec.StandFrames)
            };
            return baseOffset + frameOffset;
        }
    }
}
