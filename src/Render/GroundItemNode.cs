// ============================================================================
// Mir2 客户端 - 大世界地表掉落物渲染节点 (GroundItemNode) (1:1 盛大 1.5 官方实证)
// 官方实证：IDA sub_46AEF0 & DnItems.wil 地表掉落物品与金币渲染规约
// ============================================================================

using System;
using Godot;

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

            _nameLabel = new Label
            {
                Name = "GroundItemName",
                HorizontalAlignment = HorizontalAlignment.Center,
                Position = new Vector2(-40, -18),
                Size = new Vector2(80, 14)
            };
            _nameLabel.AddThemeFontSizeOverride("font_size", 9);
            Color nameColor = ItemName.Contains("金币")
                ? new Color(1.0f, 0.95f, 0.4f)
                : (ItemName.Contains("裁决") || ItemName.Contains("屠龙") || ItemName.Contains("骨玉") || ItemName.Contains("龙纹")
                    ? new Color(1.0f, 0.4f, 0.4f)
                    : new Color(0.9f, 0.9f, 0.9f));
            _nameLabel.AddThemeColorOverride("font_color", nameColor);
            _nameLabel.AddThemeColorOverride("font_outline_color", new Color(0, 0, 0, 0.9f));
            _nameLabel.AddThemeConstantOverride("outline_size", 1);
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
            if (ItemName.Contains("金币"))
            {
                frameIdx = Amount >= 100 ? 1 : 0;
            }

            Texture2D? tex = _loader.GetFrame(frameIdx);
            if (tex != null)
            {
                _sprite.Texture = tex;
                var anchor = _loader.GetAnchor(frameIdx);
                // 盛大官方 1.5 瓦片中心底部地表定位
                _sprite.Position = new Vector2(24 + anchor.X - 10, 32 + anchor.Y + 14);
                _sprite.Visible = true;
            }
        }

        public override void _Process(double delta)
        {
            UpdatePosition();
        }
    }
}
