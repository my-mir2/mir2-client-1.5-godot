// ============================================================================
// Mir2 客户端 - 地图瓦片与建筑渲染 (1:1 盛大 1.5 官方实证)
// 官方实证：IDA sub_47009C & sub_44D6CC
// ============================================================================

using System;
using System.Collections.Generic;
using Godot;

namespace Mir2Client.Render
{
    public partial class MapRenderer : Node2D
    {
        public const int HeaderSize = 52;
        public const int CellSize = 12;
        public const int TileW = 48;
        public const int TileH = 32;

        public const int ViewportW = 800;
        public const int ViewportH = 600;

        public const int BufferTilesYUp = 18;
        public const int BufferTilesYDown = 4;
        public const int BufferTilesX = 4;

        private WilLoader? _wilFloor;
        private WilLoader? _wilSmTiles;
        private WilLoader? _wilObj;
        private readonly List<WilLoader> _wilObjs = new();

        private int _mapW = 0;
        private int _mapH = 0;
        private byte[] _cells = Array.Empty<byte>();

        private float _camX = 0f;
        private float _camY = 0f;

        private readonly List<Sprite2D> _floorSprites = new();
        private readonly List<Sprite2D> _smtileSprites = new();
        private readonly List<Sprite2D> _objSprites = new();

        private int _viewTilesW = 0;
        private int _viewTilesH = 0;

        private readonly CanvasItemMaterial _additiveMaterial = new()
        {
            BlendMode = CanvasItemMaterial.BlendModeEnum.Add
        };

        private int _playerTileX = -1;
        private int _playerTileY = -1;

        private readonly HashSet<Vector2I> _dynamicObstacles = new();

        [Signal]
        public delegate void MapLoadedEventHandler(int width, int height);

        public override void _Ready()
        {
            int baseW = (int)Math.Ceiling((float)ViewportW / TileW);
            int baseH = (int)Math.Ceiling((float)ViewportH / TileH);
            _viewTilesW = baseW + BufferTilesX * 2;
            _viewTilesH = baseH + BufferTilesYUp + BufferTilesYDown;
            InitSpritePool();
        }

        public void SetPlayerTile(int tx, int ty)
        {
            if (_playerTileX != tx || _playerTileY != ty)
            {
                _playerTileX = tx;
                _playerTileY = ty;
                DirtyAll();
            }
        }

        public void SetLoaders(WilLoader floorLoader, WilLoader objLoader, WilLoader? smtilesLoader = null)
        {
            _wilFloor = floorLoader;
            _wilObj = objLoader;
            _wilSmTiles = smtilesLoader;
            DirtyAll();
        }

        public void SetObjectLoaders(IEnumerable<WilLoader> loaders)
        {
            _wilObjs.Clear();
            _wilObjs.AddRange(loaders);
            DirtyAll();
        }

        public bool LoadMap(string path)
        {
            if (!Godot.FileAccess.FileExists(path))
            {
                GD.PushError($"[MapRenderer] Cannot find map file: {path}");
                return false;
            }

            using var f = Godot.FileAccess.Open(path, Godot.FileAccess.ModeFlags.Read);
            if (f == null)
            {
                GD.PushError($"[MapRenderer] Cannot open map: {path}");
                return false;
            }

            f.Seek(0);
            _mapW = f.Get16();
            _mapH = f.Get16();

            if (_mapW <= 0 || _mapH <= 0 || _mapW > 4000 || _mapH > 4000)
            {
                GD.PushError($"[MapRenderer] Invalid map dimensions: {_mapW}x{_mapH}");
                return false;
            }

            f.Seek(HeaderSize);
            _cells = f.GetBuffer(_mapW * _mapH * CellSize);

            GD.Print($"[MapRenderer] Loaded {System.IO.Path.GetFileName(path)}: {_mapW}x{_mapH} cells");
            DirtyAll();
            EmitSignal(SignalName.MapLoaded, _mapW, _mapH);
            return true;
        }

        public void MoveCamera(int tileX, int tileY)
        {
            SetCameraPos(tileX * TileW - ViewportW / 2.0f, tileY * TileH - ViewportH / 2.0f);
        }

        public void CenterOn(int tileX, int tileY) => MoveCamera(tileX, tileY);

        public void SetCameraPos(float cx, float cy)
        {
            float maxCx = Math.Max(0.0f, (float)(_mapW * TileW - ViewportW));
            float maxCy = Math.Max(0.0f, (float)(_mapH * TileH - ViewportH));
            _camX = Math.Clamp(cx, 0.0f, maxCx);
            _camY = Math.Clamp(cy, 0.0f, maxCy);
            DirtyAll();
        }

        public Vector2 GetCameraPos() => new(_camX, _camY);

        public struct CellData
        {
            public int FloorIdx;
            public int SmTileIdx;
            public int ObjIdx;
            public byte FileIdx;
            public byte Light;
            public bool Passable;
        }

        public CellData GetCell(int tx, int ty)
        {
            if (tx < 0 || ty < 0 || tx >= _mapW || ty >= _mapH)
                return default;

            int offset = (tx * _mapH + ty) * CellSize;
            if (offset + CellSize > _cells.Length)
                return default;

            ushort floorRaw = (ushort)(_cells[offset] | (_cells[offset + 1] << 8));
            ushort midRaw = (ushort)(_cells[offset + 2] | (_cells[offset + 3] << 8));
            ushort objRaw = (ushort)(_cells[offset + 4] | (_cells[offset + 5] << 8));
            byte fileIdx = _cells[offset + 10];
            byte light = _cells[offset + 11];

            int floorVal = (floorRaw & 0x7FFF) > 0 ? (floorRaw & 0x7FFF) - 1 : -1;
            int midVal = (midRaw & 0x7FFF) > 0 ? (midRaw & 0x7FFF) - 1 : -1;
            int objVal = (objRaw & 0x7FFF) > 0 ? (objRaw & 0x7FFF) - 1 : -1;

            return new CellData
            {
                FloorIdx = floorVal,
                SmTileIdx = midVal,
                ObjIdx = objVal,
                FileIdx = fileIdx,
                Light = light,
                Passable = (floorRaw & 0x8000) == 0
            };
        }

        public void AddDynamicObstacle(Vector2I pos) => _dynamicObstacles.Add(pos);
        public void RemoveDynamicObstacle(Vector2I pos) => _dynamicObstacles.Remove(pos);

        public void SetTileBlocked(int tx, int ty, bool blocked)
        {
            var p = new Vector2I(tx, ty);
            if (blocked)
            {
                _dynamicObstacles.Add(p);
            }
            else
            {
                _dynamicObstacles.Remove(p);
            }

            if (_cells != null && tx >= 0 && ty >= 0 && tx < _mapW && ty < _mapH)
            {
                int offset = (tx * _mapH + ty) * CellSize;
                if (offset + CellSize <= _cells.Length)
                {
                    ushort floorRaw = (ushort)(_cells[offset] | (_cells[offset + 1] << 8));
                    if (blocked)
                    {
                        floorRaw = (ushort)(floorRaw | 0x8000);
                    }
                    else
                    {
                        floorRaw = (ushort)(floorRaw & 0x7FFF);
                    }
                    _cells[offset] = (byte)(floorRaw & 0xFF);
                    _cells[offset + 1] = (byte)((floorRaw >> 8) & 0xFF);
                }
            }
        }

        public bool IsPassable(int tx, int ty)
        {
            if (_dynamicObstacles.Contains(new Vector2I(tx, ty)))
                return false;
            if (_mapW == 0 || _mapH == 0)
                return true;
            return GetCell(tx, ty).Passable;
        }

        private void InitSpritePool()
        {
            int total = _viewTilesW * _viewTilesH;
            for (int i = 0; i < total; i++)
            {
                var sp = new Sprite2D
                {
                    Centered = false,
                    Visible = false,
                    ZIndex = -10,
                    ZAsRelative = false
                };
                AddChild(sp);
                _floorSprites.Add(sp);

                var sm = new Sprite2D
                {
                    Centered = false,
                    Visible = false,
                    ZIndex = -9,
                    ZAsRelative = false
                };
                AddChild(sm);
                _smtileSprites.Add(sm);

                var so = new Sprite2D
                {
                    Centered = false,
                    Visible = false,
                    ZIndex = -5,
                    ZAsRelative = false
                };
                AddChild(so);
                _objSprites.Add(so);
            }
        }

        public void DirtyAll()
        {
            if (_mapW == 0 || _mapH == 0)
                return;

            int baseTileX = (int)(_camX / TileW);
            int baseTileY = (int)(_camY / TileH);

            int startTx = (baseTileX - BufferTilesX) & ~1;
            int startTy = (baseTileY - BufferTilesYUp) & ~1;

            int poolIdx = 0;
            for (int row = 0; row < _viewTilesH; row++)
            {
                for (int col = 0; col < _viewTilesW; col++)
                {
                    if (poolIdx >= _floorSprites.Count)
                        break;

                    int tx = startTx + col;
                    int ty = startTy + row;
                    int sx = tx * TileW - (int)_camX;
                    int sy = ty * TileH - (int)_camY;

                    var sp = _floorSprites[poolIdx];
                    var sm = _smtileSprites[poolIdx];
                    var so = _objSprites[poolIdx];

                    if (tx < 0 || ty < 0 || tx >= _mapW || ty >= _mapH)
                    {
                        sp.Visible = false;
                        sm.Visible = false;
                        so.Visible = false;
                        poolIdx++;
                        continue;
                    }

                    var cell = GetCell(tx, ty);

                    // 1. 地面层 (Tiles.wil: 96x64 大地砖)
                    if ((tx % 2 == 0) && (ty % 2 == 0) && _wilFloor != null && cell.FloorIdx >= 0)
                    {
                        var tex = _wilFloor.GetFrame(cell.FloorIdx);
                        if (tex != null)
                        {
                            sp.Texture = tex;
                            sp.Position = new Vector2(sx, sy);
                            sp.Visible = true;
                        }
                        else sp.Visible = false;
                    }
                    else sp.Visible = false;

                    // 2. 小地砖层 (SmTiles.wil: 48x32)
                    if (cell.SmTileIdx >= 0 && _wilSmTiles != null)
                    {
                        var smTex = _wilSmTiles.GetFrame(cell.SmTileIdx);
                        if (smTex != null)
                        {
                            sm.Texture = smTex;
                            sm.Position = new Vector2(sx, sy);
                            sm.Visible = true;
                        }
                        else sm.Visible = false;
                    }
                    else sm.Visible = false;

                    // 3. 物件层 (Objects1~7.wil: y = sy + 32 - h)
                    if (cell.ObjIdx >= 0)
                    {
                        WilLoader? targetWil = null;
                        if (cell.FileIdx == 0)
                        {
                            targetWil = _wilObj;
                        }
                        else if (cell.FileIdx > 0 && (cell.FileIdx - 1) < _wilObjs.Count)
                        {
                            targetWil = _wilObjs[cell.FileIdx - 1];
                        }
                        else
                        {
                            targetWil = _wilObj;
                        }

                        Texture2D? tex2 = targetWil?.GetFrame(cell.ObjIdx);
                        if (tex2 != null)
                        {
                            so.Texture = tex2;
                            int objW = tex2.GetWidth();
                            int objH = tex2.GetHeight();
                            var objPos = new Vector2(sx, sy + 32 - objH);
                            so.Position = objPos;
                            so.Visible = true;

                            // 盛大官方 1.5 逆向实证：对安全区光照 (Light > 0) 或光源光晕贴图 (如 2723/2724) 使用加法混合 (BlendMode.Add)
                            // 消除黑色边缘与黑底倒置感，呈现原汁原味的半透明金色安全区光圈
                            bool isLightHalo = (cell.Light > 0) || (cell.FileIdx == 0 && cell.ObjIdx is >= 2723 and <= 2724);
                            if (isLightHalo)
                            {
                                so.Material = _additiveMaterial;
                                so.ZIndex = -4; // 光照覆盖在地表上方
                            }
                            else
                            {
                                so.Material = null;
                                so.ZIndex = -5;
                            }

                            if (objH > 32 && _playerTileX >= 0)
                            {
                                float playerSx = _playerTileX * TileW - (int)_camX + 24;
                                float playerSy = _playerTileY * TileH - (int)_camY + 16;
                                var objRect = new Rect2(objPos.X, objPos.Y, objW, MathF.Max(1.0f, (float)(objH - 10)));
                                if (objRect.HasPoint(new Vector2(playerSx, playerSy)))
                                {
                                    so.Modulate = new Color(1f, 1f, 1f, 0.5f);
                                }
                                else
                                {
                                    so.Modulate = new Color(1f, 1f, 1f, 1.0f);
                                }
                            }
                            else
                            {
                                so.Modulate = new Color(1f, 1f, 1f, 1.0f);
                            }
                        }
                        else so.Visible = false;
                    }
                    else so.Visible = false;

                    poolIdx++;
                }
            }
        }

        public Vector2 TileToScreen(int tx, int ty) => new(tx * TileW - _camX, ty * TileH - _camY);

        public Vector2 WorldToScreen(Vector2 worldPos) => new(worldPos.X - _camX, worldPos.Y - _camY);

        public Vector2I ScreenToTile(Vector2 screenPos) =>
            new((int)((screenPos.X + _camX) / TileW), (int)((screenPos.Y + _camY) / TileH));
    }
}
