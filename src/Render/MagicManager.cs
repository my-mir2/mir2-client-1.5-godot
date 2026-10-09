// ============================================================================
// Mir2 客户端 - 魔法技能光效与弹道渲染管理器 (1:1 盛大 1.5 官方实证)
// 官方实证：IDA sub_491EA0 (TMagicManager) 与 SM_MAGIC_FIRE / SM_MAGICONMAP
// ============================================================================

using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Audio;
using Mir2Client.Utils;

namespace Mir2Client.Render
{
    public partial class MagicManager : Node2D
    {
        private WilLoader? _wilMagic;
        private WilLoader? _wilMagic2;
        private WilLoader? _wilEffect;
        private MapRenderer? _mapRenderer;

        public class Projectile
        {
            public int MagicId;
            public Sprite2D Sprite = null!;
            public Vector2 StartPos;
            public Vector2 TargetPos;
            public float Progress;
            public float Speed = 300f;
            public int BaseFrame = 1;
            public int FrameCount = 10;
            public float AnimTimer;
            public int HitFrame = 171;
            public int HitCount = 10;
            public bool IsHit;
            public float HitTimer;
            public WilLoader? WilLoaderInstance;
        }

        public class GroundMagic
        {
            public Sprite2D Sprite = null!;
            public Vector2I TilePos;
            public float RemainingLife = 15f;
            public int BaseFrame = 400;
            public int FrameCount = 6;
            public float AnimTimer;
            public WilLoader? WilLoaderInstance;
        }

        public class AttachedEffect
        {
            public int MagicId;
            public Node TargetEntity = null!;
            public int TargetUid;
            public Sprite2D Sprite = null!;
            public int BaseFrame;
            public int FrameCount;
            public float AnimTimer;
            public float Duration;
            public bool IsPersistent;
            public Vector2 Offset = Vector2.Zero;
            public WilLoader? WilLoaderInstance;
        }

        private readonly List<Projectile> _flyingProjectiles = new();
        private readonly List<GroundMagic> _groundMagics = new();
        private readonly List<AttachedEffect> _attachedEffects = new();

        public override void _Ready()
        {
            ZIndex = 100;
        }

        public void SetWilLoaders(WilLoader? m1, WilLoader? m2, WilLoader? eff)
        {
            _wilMagic = m1;
            _wilMagic2 = m2;
            _wilEffect = eff;
        }

        public void SetMapRenderer(MapRenderer mr) => _mapRenderer = mr;

        public override void _Process(double delta)
        {
            float dt = (float)delta;
            UpdateProjectiles(dt);
            UpdateGroundMagics(dt);
            UpdateAttachedEffects(dt);
        }

        public void PlayMagicSfx(int magicId, int phase = 0)
        {
            var am = AudioManager.Instance;
            if (am == null) return;

            switch (magicId)
            {
                case 7:
                    am.PlaySoundId(phase == 0 ? 130 : 131);
                    return;
                case 12:
                    am.PlaySoundId(132);
                    return;
                case 25:
                    am.PlaySoundId(133);
                    return;
                case 26:
                    am.PlaySoundId(phase == 0 ? 136 : 137);
                    return;
                case 27:
                    am.PlaySoundId(phase == 0 ? 134 : 135);
                    return;
                case 17:
                    if (phase == 2) { am.PlaySoundId(570); return; }
                    break;
                case 30:
                    if (phase == 2) { am.PlaySoundId(1900); return; }
                    break;
            }

            int sid = 10000 + magicId * 10 + phase;
            am.PlaySoundId(sid);
        }

        public void PlayMagic(int magicId, Vector2I fromTile, Vector2I toTile, int targetUid = 0)
        {
            var fromPos = TileToWorld(fromTile);
            var toPos = TileToWorld(toTile);

            PlayMagicSfx(magicId, 0);

            var loader = _wilMagic ?? _wilMagic2;

            switch (magicId)
            {
                case 1: CreateProjectile(magicId, fromPos, toPos, 0, 10, 170, 10, loader); break;
                case 2: CreateImpact(magicId, toPos, 200, 10, loader); break;
                case 5: CreateProjectile(magicId, fromPos, toPos, 400, 12, 170, 10, loader); break;
                case 6: CreateImpact(magicId, toPos, 600, 8, loader); break;
                case 8: CreateImpact(magicId, fromPos, 900, 10, loader); break;
                case 9: CreateProjectile(magicId, fromPos, toPos, 400, 12, 170, 10, loader); break;
                case 10: CreateImpact(magicId, toPos, 120, 10, loader); break;
                case 11: CreateImpact(magicId, fromPos, 1680, 10, loader); break;
                case 12: CreateImpact(magicId, toPos, 70, 8, loader); break;
                case 13: CreateProjectile(magicId, fromPos, toPos, 900, 8, 920, 6, loader); break;
                case 14: CreateImpact(magicId, toPos, 1560, 10, loader); break;
                case 15: CreateImpact(magicId, toPos, 1590, 10, loader); break;
                case 16: CreateImpact(magicId, toPos, 3990, 10, loader); break;
                case 17: CreateImpact(magicId, toPos, 1500, 10, loader); break;
                case 18: CreateImpact(magicId, fromPos, 1520, 8, loader); break;
                case 19: CreateImpact(magicId, toPos, 1520, 8, loader); break;
                case 20: CreateImpact(magicId, toPos, 1620, 12, loader); break;
                case 21: CreateImpact(magicId, fromPos, 940, 10, loader); break;
                case 22: SpawnGroundMagic(toTile, 15.0f, 0); break;
                case 23: CreateProjectile(magicId, fromPos, toPos, 400, 10, 410, 6, loader); break;
                case 24: CreateImpact(magicId, toPos, 3920, 10, loader); break;
                case 25: CreateImpact(magicId, fromPos, 3960, 10, loader); break;
                case 26: CreateImpact(magicId, toPos, 170, 10, loader); break;
                case 28: CreateImpact(magicId, toPos, 1620, 12, loader); break;
                case 29: CreateImpact(magicId, toPos, 1790, 12, loader); break;
                case 30: CreateImpact(magicId, toPos, 1650, 12, loader); break;
                case 31: CreateImpact(magicId, fromPos, 3880, 8, loader); break;
                case 32: CreateImpact(magicId, toPos, 1540, 12, loader); break;
                default: CreateProjectile(magicId, fromPos, toPos, 0, 10, 170, 10, loader); break;
            }
        }

        public void SpawnGroundMagic(Vector2I tilePos, float duration = 15f, int effectType = 0)
        {
            var gm = new GroundMagic
            {
                TilePos = tilePos,
                RemainingLife = duration,
                WilLoaderInstance = _wilMagic ?? _wilMagic2,
                BaseFrame = 400 + effectType * 10,
                FrameCount = 6
            };

            var spr = new Sprite2D
            {
                Centered = false,
                Position = TileToWorld(tilePos),
                Material = new CanvasItemMaterial { BlendMode = CanvasItemMaterial.BlendModeEnum.Add }
            };
            AddChild(spr);
            gm.Sprite = spr;
            _groundMagics.Add(gm);
        }

        private void CreateProjectile(int magicId, Vector2 fromP, Vector2 toP, int baseF, int fCnt, int hitF, int hitCnt, WilLoader? loader)
        {
            var proj = new Projectile
            {
                MagicId = magicId,
                StartPos = fromP,
                TargetPos = toP,
                BaseFrame = baseF,
                FrameCount = fCnt,
                HitFrame = hitF,
                HitCount = hitCnt,
                WilLoaderInstance = loader
            };

            PlayMagicSfx(magicId, 1);

            var spr = new Sprite2D
            {
                Centered = true,
                Position = fromP,
                Material = new CanvasItemMaterial { BlendMode = CanvasItemMaterial.BlendModeEnum.Add }
            };
            AddChild(spr);
            proj.Sprite = spr;
            _flyingProjectiles.Add(proj);
        }

        private void CreateImpact(int magicId, Vector2 pos, int baseF, int fCnt, WilLoader? loader)
        {
            var proj = new Projectile
            {
                MagicId = magicId,
                StartPos = pos,
                TargetPos = pos,
                Progress = 1.0f,
                IsHit = true,
                HitFrame = baseF,
                HitCount = fCnt,
                FrameCount = fCnt,
                WilLoaderInstance = loader
            };

            PlayMagicSfx(magicId, 2);

            var spr = new Sprite2D
            {
                Centered = true,
                Position = pos,
                Material = new CanvasItemMaterial { BlendMode = CanvasItemMaterial.BlendModeEnum.Add }
            };
            AddChild(spr);
            proj.Sprite = spr;
            _flyingProjectiles.Add(proj);
        }

        private void UpdateProjectiles(float delta)
        {
            for (int i = _flyingProjectiles.Count - 1; i >= 0; i--)
            {
                var p = _flyingProjectiles[i];
                if (!p.IsHit)
                {
                    float totalDist = p.StartPos.DistanceTo(p.TargetPos);
                    if (totalDist <= 1.0f)
                    {
                        p.IsHit = true;
                        PlayMagicSfx(p.MagicId, 2);
                    }
                    else
                    {
                        float moveStep = p.Speed * delta;
                        p.Progress += moveStep / totalDist;
                        if (p.Progress >= 1.0f)
                        {
                            p.Progress = 1.0f;
                            p.IsHit = true;
                            p.Sprite.Position = p.TargetPos;
                            PlayMagicSfx(p.MagicId, 2);
                        }
                        else
                        {
                            p.Sprite.Position = p.StartPos.Lerp(p.TargetPos, p.Progress);
                        }
                    }

                    p.AnimTimer += delta;
                    int fIdx = (int)(p.AnimTimer * 12.0f) % p.FrameCount;
                    ApplyFrame(p.Sprite, p.WilLoaderInstance, p.BaseFrame + fIdx);
                }
                else
                {
                    p.HitTimer += delta;
                    int hitAnimIdx = (int)(p.HitTimer * 15.0f);
                    if (hitAnimIdx >= p.HitCount)
                    {
                        p.Sprite.QueueFree();
                        _flyingProjectiles.RemoveAt(i);
                    }
                    else
                    {
                        ApplyFrame(p.Sprite, p.WilLoaderInstance, p.HitFrame + hitAnimIdx);
                    }
                }
            }
        }

        private void UpdateGroundMagics(float delta)
        {
            for (int i = _groundMagics.Count - 1; i >= 0; i--)
            {
                var gm = _groundMagics[i];
                gm.RemainingLife -= delta;
                if (gm.RemainingLife <= 0.0f)
                {
                    gm.Sprite.QueueFree();
                    _groundMagics.RemoveAt(i);
                }
                else
                {
                    gm.AnimTimer += delta;
                    int fIdx = (int)(gm.AnimTimer * 8.0f) % gm.FrameCount;
                    ApplyFrame(gm.Sprite, gm.WilLoaderInstance, gm.BaseFrame + fIdx);
                }
            }
        }

        public void AttachEffectToEntity(Node target, int magicId, float duration = 30f, bool isPersistent = false)
        {
            if (target == null || !IsInstanceValid(target)) return;

            foreach (var existing in _attachedEffects)
            {
                if (existing.TargetEntity == target && existing.MagicId == magicId)
                {
                    existing.Duration = duration;
                    return;
                }
            }

            var loader = _wilMagic ?? _wilMagic2;
            var eff = new AttachedEffect
            {
                MagicId = magicId,
                TargetEntity = target,
                Duration = duration,
                IsPersistent = isPersistent,
                WilLoaderInstance = loader
            };

            switch (magicId)
            {
                case 31: // 魔法盾
                    eff.BaseFrame = 3880;
                    eff.FrameCount = 8;
                    eff.Offset = new Vector2(0, -32);
                    break;
                case 15: // 神圣战甲术
                    eff.BaseFrame = 1590;
                    eff.FrameCount = 10;
                    eff.Offset = new Vector2(-4, -78);
                    break;
                case 14: // 幽灵盾
                    eff.BaseFrame = 1560;
                    eff.FrameCount = 10;
                    eff.Offset = new Vector2(-4, -78);
                    break;
                case 2: // 治愈术
                    eff.BaseFrame = 200;
                    eff.FrameCount = 10;
                    eff.Offset = Vector2.Zero;
                    eff.Duration = 2.0f;
                    break;
                default:
                    eff.BaseFrame = 1590;
                    eff.FrameCount = 10;
                    eff.Offset = new Vector2(-4, -78);
                    break;
            }

            var spr = new Sprite2D
            {
                Centered = true,
                Material = new CanvasItemMaterial { BlendMode = CanvasItemMaterial.BlendModeEnum.Add }
            };
            AddChild(spr);
            eff.Sprite = spr;
            _attachedEffects.Add(eff);
        }

        public void RemoveAttachedEffect(Node target, int magicId = -1)
        {
            for (int i = _attachedEffects.Count - 1; i >= 0; i--)
            {
                var eff = _attachedEffects[i];
                if (eff.TargetEntity == target && (magicId == -1 || eff.MagicId == magicId))
                {
                    if (IsInstanceValid(eff.Sprite)) eff.Sprite.QueueFree();
                    _attachedEffects.RemoveAt(i);
                }
            }
        }

        private void UpdateAttachedEffects(float delta)
        {
            for (int i = _attachedEffects.Count - 1; i >= 0; i--)
            {
                var eff = _attachedEffects[i];
                if (!IsInstanceValid(eff.TargetEntity) || !eff.TargetEntity.IsInsideTree())
                {
                    if (IsInstanceValid(eff.Sprite)) eff.Sprite.QueueFree();
                    _attachedEffects.RemoveAt(i);
                    continue;
                }

                if (!eff.IsPersistent)
                {
                    eff.Duration -= delta;
                    if (eff.Duration <= 0.0f)
                    {
                        if (IsInstanceValid(eff.Sprite)) eff.Sprite.QueueFree();
                        _attachedEffects.RemoveAt(i);
                        continue;
                    }
                }

                var target2d = eff.TargetEntity as Node2D;
                var targetPos = target2d?.GlobalPosition ?? Vector2.Zero;
                eff.Sprite.GlobalPosition = targetPos + eff.Offset;

                eff.AnimTimer += delta;
                int fIdx = (int)(eff.AnimTimer * 12.0f) % eff.FrameCount;
                ApplyFrame(eff.Sprite, eff.WilLoaderInstance, eff.BaseFrame + fIdx);
            }
        }

        private static void ApplyFrame(Sprite2D sprite, WilLoader? loader, int frameIdx)
        {
            Texture2D? tex = loader?.GetFrame(frameIdx);
            if (tex != null)
            {
                sprite.Texture = tex;
                var anchor = loader?.GetAnchor(frameIdx) ?? Vector2I.Zero;
                sprite.Offset = new Vector2(anchor.X, anchor.Y);
            }
        }

        private Vector2 TileToWorld(Vector2I tile)
        {
            if (_mapRenderer != null)
                return _mapRenderer.TileToScreen(tile.X, tile.Y) + new Vector2(24, 16);
            return new Vector2(tile.X * 48 + 24, tile.Y * 32 + 16);
        }

        public void Clear()
        {
            foreach (var p in _flyingProjectiles)
            {
                if (IsInstanceValid(p.Sprite)) p.Sprite.QueueFree();
            }
            _flyingProjectiles.Clear();

            foreach (var gm in _groundMagics)
            {
                if (IsInstanceValid(gm.Sprite)) gm.Sprite.QueueFree();
            }
            _groundMagics.Clear();

            foreach (var eff in _attachedEffects)
            {
                if (IsInstanceValid(eff.Sprite)) eff.Sprite.QueueFree();
            }
            _attachedEffects.Clear();
        }

        /// <summary>
        /// 盛大官方 1.5 全量 32 门技能视听动画统一调度器 (由 OfficialMagicCompendium 驱动)
        /// </summary>
        public void PlayOfficialMagic(int magicId, Vector2 startWorld, Vector2 targetWorld, Node? targetNode = null)
        {
            var def = Mir2Client.Game.OfficialMagicCompendium.Get(magicId);
            if (def == null) return;

            var loader = def.SpellWil == 2 ? _wilMagic2 : _wilMagic;

            switch (def.CastType)
            {
                case Mir2Client.Game.OfficialMagicCompendium.MagicCastType.TargetProjectile:
                    var proj = new Projectile
                    {
                        MagicId = magicId,
                        StartPos = startWorld,
                        TargetPos = targetWorld,
                        Progress = 0f,
                        Speed = def.ProjectileSpeed > 0 ? def.ProjectileSpeed : 500f,
                        BaseFrame = def.EffectStartFrame,
                        FrameCount = Math.Max(1, def.EffectFrameCount),
                        WilLoaderInstance = loader,
                        Sprite = new Sprite2D { Centered = true, ZIndex = 110 }
                    };
                    AddChild(proj.Sprite);
                    _flyingProjectiles.Add(proj);
                    break;

                case Mir2Client.Game.OfficialMagicCompendium.MagicCastType.GroundAoE:
                    var gm = new GroundMagic
                    {
                        TilePos = new Vector2I((int)(targetWorld.X / 48), (int)(targetWorld.Y / 32)),
                        RemainingLife = def.GroundDurationMs > 0 ? def.GroundDurationMs / 1000f : 20f,
                        BaseFrame = def.EffectStartFrame,
                        FrameCount = Math.Max(1, def.EffectFrameCount),
                        WilLoaderInstance = loader,
                        Sprite = new Sprite2D { Centered = true, GlobalPosition = targetWorld, ZIndex = 5 }
                    };
                    AddChild(gm.Sprite);
                    _groundMagics.Add(gm);
                    break;

                case Mir2Client.Game.OfficialMagicCompendium.MagicCastType.SelfCast:
                case Mir2Client.Game.OfficialMagicCompendium.MagicCastType.TargetDirect:
                case Mir2Client.Game.OfficialMagicCompendium.MagicCastType.TargetArea:
                case Mir2Client.Game.OfficialMagicCompendium.MagicCastType.Summon:
                    var eff = new AttachedEffect
                    {
                        MagicId = magicId,
                        TargetEntity = targetNode ?? this,
                        Duration = def.GroundDurationMs > 0 ? def.GroundDurationMs / 1000f : 1.5f,
                        BaseFrame = def.EffectStartFrame,
                        FrameCount = Math.Max(1, def.EffectFrameCount),
                        IsPersistent = def.MagicId == 31, // 魔法盾常驻
                        WilLoaderInstance = loader,
                        Sprite = new Sprite2D { Centered = true, GlobalPosition = targetWorld, ZIndex = 105 }
                    };
                    AddChild(eff.Sprite);
                    _attachedEffects.Add(eff);
                    break;
            }
        }
    }
}
