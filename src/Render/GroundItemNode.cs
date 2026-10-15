// ============================================================================
// Mir2 客户端 - 大世界地表掉落物渲染节点 (GroundItemNode) (1:1 盛大 1.5 官方实证)
// 官方实证：IDA sub_46AEF0 & DnItems.wil 地表掉落物品与金币渲染规约
// ============================================================================

using System;
using Godot;
using Mir2Client.Utils;

namespace Mir2Client.Render
{
    public partial class GroundItemNode : Node2D
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
        public int TileX { get; set; }
        public int TileY { get; set; }
        public int Looks { get; set; }
        public int Amount { get; set; } = 1;

        private Sprite2D? _sprite;
        private Label? _nameLabel;
        private WilLoader? _loader;
        private MapRenderer? _mapRenderer;

        public override void _Ready()
        {
            SetupVisuals();
            UpdatePosition();
            UpdateVisual();
        }

        private void SetupVisuals()
        {
            _sprite = new Sprite2D { Centered = false };
            AddChild(_sprite);

            // 盛大官方 1.5 地表物品名称定位 (严格 1:1 对齐 Delphi PlayScn.pas:1397-1398):
            // ix + HALFX - W/2, iy + HALFY - H*2 = (24 - 40 = -16, -12)
            _nameLabel = new Label
            {
                Name = "GroundItemName",
                HorizontalAlignment = HorizontalAlignment.Center,
                Position = new Vector2(-16, -12),
                Size = new Vector2(80, 14),
                Visible = true,
                ZAsRelative = false,
                ZIndex = 110,
                MouseFilter = Control.MouseFilterEnum.Ignore
            };
            Color nameColor = ItemName.Contains("金币")
                ? new Color(1.0f, 0.95f, 0.4f)
                : (ItemName.Contains("裁决") || ItemName.Contains("屠龙") || ItemName.Contains("骨玉") || ItemName.Contains("龙纹")
                    ? new Color(1.0f, 0.4f, 0.4f)
                    : new Color(0.9f, 0.9f, 0.9f));
            FontManager.ApplyOfficialWorldNameplateStyle(_nameLabel, nameColor);
            _nameLabel.Text = Amount > 1 && ItemName.Contains("金币") ? $"{ItemName} ({Amount})" : ItemName;
            AddChild(_nameLabel);
        }

        public void SetLoader(WilLoader? loader)
        {
            _loader = loader;
            UpdateVisual();
        }

        public void SetMapRenderer(MapRenderer? mr)
        {
            _mapRenderer = mr;
            UpdatePosition();
        }

        public void SetTilePos(int tx, int ty)
        {
            TileX = tx;
            TileY = ty;
            UpdatePosition();
        }

        public void UpdatePosition()
        {
            if (_mapRenderer != null)
            {
                Position = _mapRenderer.TileToScreen(TileX, TileY);
            }
            else
            {
                Position = new Vector2(TileX * 48, TileY * 32);
            }
            // 地表掉落物深度低于站立实体，但高于地表 (确保不遮挡角色/怪物脚部)
            ZIndex = Math.Clamp(TileY, 0, 98);
        }

        public void UpdateVisual()
        {
            if (_sprite == null || _loader == null) return;

            int frameIdx = Looks;
            if (ItemName.Contains("金币") && frameIdx < 112)
            {
                // 盛大官方 1.5 DnItems.wil 权威真值：112~116 为小中大 5 档金币堆（DnItems_0001 为生肉块，严禁混用！）
                frameIdx = Amount >= 1000 ? 116 : (Amount >= 500 ? 115 : (Amount >= 200 ? 114 : (Amount >= 100 ? 113 : 112)));
            }
            else if (frameIdx <= 0)
            {
                if (ItemName.Contains("药") || ItemName.Contains("水") || ItemName.Contains("霜")) frameIdx = 28;
                else if (ItemName.Contains("卷")) frameIdx = 3;
                else if (ItemName.Contains("剑") || ItemName.Contains("刀") || ItemName.Contains("杖") || ItemName.Contains("斧")) frameIdx = 22;
                else if (ItemName.Contains("甲") || ItemName.Contains("衣") || ItemName.Contains("袍")) frameIdx = 62;
                else if (ItemName.Contains("肉")) frameIdx = 1;
                else frameIdx = 112;
            }

            Texture2D? tex = _loader.GetFrame(frameIdx);
            if (tex != null)
            {
                _sprite.Texture = tex;
                // 盛大官方 1.5 地表物品居中定位 (严格 1:1 对齐 Delphi PlayScn.pas:1221):
                // m_ObjSurface.Draw(ix + HALFX - (d.Width div 2), iy + HALFY - (d.Height div 2))
                int ox = 24 - (int)(tex.GetWidth() / 2);
                int oy = 16 - (int)(tex.GetHeight() / 2);
                _sprite.Position = new Vector2(ox, oy);
                _sprite.Visible = true;
            }
        }

        public float RemainingLifeSec { get; set; } = 120.0f; // 盛大官方 1.5 地表物品 120 秒寿命
        private float _blinkTimer = 0f;

        public override void _Process(double delta)
        {
            UpdatePosition();
            if (_nameLabel != null)
            {
                _nameLabel.Visible = Visible;
            }
            float dt = (float)delta;
            RemainingLifeSec -= dt;
            if (RemainingLifeSec <= 10.0f && RemainingLifeSec > 0f)
            {
                // 盛大官方 1.5 规则: 战利品即将刷新消失前 10 秒进入 200ms 高频 Alpha 闪烁预警
                _blinkTimer += dt;
                if (_blinkTimer >= 0.2f)
                {
                    _blinkTimer = 0f;
                    Modulate = Modulate.A > 0.5f ? new Color(1, 1, 1, 0.25f) : new Color(1, 1, 1, 1.0f);
                }
            }
            else if (RemainingLifeSec <= 0f)
            {
                Visible = false;
            }
        }
    }
}
