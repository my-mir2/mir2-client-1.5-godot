// ============================================================================
// Mir2 客户端 - 地图瓦片与建筑渲染 (1:1 盛大 1.5 官方实证)
// 官方实证：IDA 0x47009C（已证client无函数） & sub_44D6CC
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

        public const int BufferTilesYUp = 24;
        public const int BufferTilesYDown = 8;
        public const int BufferTilesX = 16;

        private WilLoader? _wilFloor;
        private WilLoader? _wilSmTiles;
        private WilLoader? _wilObj;
        private readonly List<WilLoader> _wilObjs = new();

        private int _mapW = 0;
        private int _mapH = 0;
        public int MapWidth => _mapW;
        public int MapHeight => _mapH;
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

        private float _animTime = 0f;
        private float _lastAnimRefresh = 0f;

        public override void _Ready()
        {
            int baseW = (int)Math.Ceiling((float)ViewportW / TileW);
            int baseH = (int)Math.Ceiling((float)ViewportH / TileH);
            _viewTilesW = baseW + BufferTilesX * 2;
            _viewTilesH = baseH + BufferTilesYUp + BufferTilesYDown;
            InitSpritePool();
        }

        public override void _Process(double delta)
        {
            base._Process(delta);
            _animTime += (float)delta;
            // 盛大官方 1.5 规则：地图动态瓦片与物件 (水流/火盆/旗帜) 采用约 6 FPS (166ms) 步长定频刷新 (IDA sub_44D6CC)
            if (_animTime - _lastAnimRefresh >= 0.166f)
            {
                _lastAnimRefresh = _animTime;
                DirtyAll();
            }
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
            public byte AniFrame;
            public byte AniTick;
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
            byte aniFrame = _cells[offset + 8];
            byte aniTick = _cells[offset + 9];
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
                AniFrame = aniFrame,
                AniTick = aniTick,
                Passable = (floorRaw & 0x8000) == 0 && (objRaw & 0x8000) == 0
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
                    ZIndex = -2000,
                    ZAsRelative = false
                };
                AddChild(sp);
                _floorSprites.Add(sp);

                var sm = new Sprite2D
                {
                    Centered = false,
                    Visible = false,
                    ZIndex = -1900,
                    ZAsRelative = false
                };
                AddChild(sm);
                _smtileSprites.Add(sm);

                var so = new Sprite2D
                {
                    Centered = false,
                    Visible = false,
                    ZIndex = -1800,
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
                    float sx = tx * TileW - _camX;
                    float sy = ty * TileH - _camY;

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

                    // 1. 地面层 (Tiles.wil: 96x64 大地砖，盛大官方 1.5 严格对齐 2x2 格，支持块内有效帧回退)
                    if ((tx % 2 == 0) && (ty % 2 == 0) && _wilFloor != null)
                    {
                        int drawFloor = cell.FloorIdx;
                        if (drawFloor < 0)
                        {
                            int f1 = GetCell(tx + 1, ty).FloorIdx;
                            if (f1 >= 0) drawFloor = f1;
                            else
                            {
                                int f2 = GetCell(tx, ty + 1).FloorIdx;
                                if (f2 >= 0) drawFloor = f2;
                                else
                                {
                                    int f3 = GetCell(tx + 1, ty + 1).FloorIdx;
                                    if (f3 >= 0) drawFloor = f3;
                                }
                            }
                        }

                        if (drawFloor >= 0)
                        {
                            var tex = _wilFloor.GetFrame(drawFloor);
                            if (tex != null)
                            {
                                sp.Texture = tex;
                                sp.Position = new Vector2(sx, sy);
                                sp.Visible = true;
                            }
                            else sp.Visible = false;
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

                    // 3. 物件层 (Objects1~7.wil: 普通物件 y = sy + 32 - h; 混合光晕 AniFrame & 0x80 != 0 使用 sx + px - 2, sy + py - 68)
                    if (cell.ObjIdx >= 0)
                    {
                        int drawObjIdx = cell.ObjIdx;
                        bool isBlend = (cell.AniFrame & 0x80) != 0 || (cell.FileIdx == 0 && cell.ObjIdx is >= 2723 and <= 2729);
                        int aniCount = cell.AniFrame & 0x7F;
                        if (aniCount > 0)
                        {
                            // 官方 Delphi 动画时序 (IDA 0x470D80)：AniTick 定义每帧停留倍率，默认 150ms
                            float tickInterval = cell.AniTick > 0 ? (0.15f * (1 + cell.AniTick)) : 0.15f;
                            int animOffset = ((int)(_animTime / tickInterval)) % aniCount;
                            drawObjIdx += animOffset;
                        }

                        WilLoader? targetWil = null;
                        if (cell.FileIdx >= 0 && cell.FileIdx < _wilObjs.Count)
                        {
                            targetWil = _wilObjs[cell.FileIdx];
                        }
                        else
                        {
                            targetWil = _wilObj;
                        }

                        Texture2D? tex2 = targetWil?.GetFrame(drawObjIdx);
                        if (tex2 != null)
                        {
                            so.Texture = tex2;
                            int objW = tex2.GetWidth();
                            int objH = tex2.GetHeight();
                            Vector2 objPos;

                            // 盛大官方 1.5 逆向实证 (PlayScn.pas 0x470D49..0x470F9F)：
                            // 1. 凡 (AniFrame & 0x80) != 0 的混合发光物件 (如灯柱光晕 Objects_2723..2729)，
                            //    调用 GetObjectImage 读取 WIL 帧头偏移 (px, py)，绘制在 (sx + px - 2, sy + py - 68) (0x470F25: sub eax, 0x44; 0x470F66: sub edx, 2)
                            // 2. 普通物件绘制在 (sx, sy + 32 - objH)，矮于或等于 1 格 (objH <= 32) 在底层 (ZIndex = -1800)，高于 1 格按 ty 排序
                            if (isBlend)
                            {
                                var anchor = targetWil?.GetAnchor(drawObjIdx) ?? Vector2I.Zero;
                                if (anchor == Vector2I.Zero && cell.FileIdx == 0 && drawObjIdx is >= 2723 and <= 2729)
                                {
                                    anchor = new Vector2I(-51, -113);
                                }
                                objPos = new Vector2(sx + anchor.X - 2, sy + anchor.Y - 68);
                                so.Material = _additiveMaterial;
                                so.ZIndex = Math.Clamp(ty * 2 + 1, 1, 3501);
                            }
                            else
                            {
                                objPos = new Vector2(sx, sy + 32 - objH);
                                so.Material = null;
                                so.ZIndex = objH > 32 ? Math.Clamp(ty * 2 - 1, 1, 3500) : -1800;
                            }

                            so.Position = objPos;
                            so.Visible = true;

                            so.Modulate = new Color(1f, 1f, 1f, 1.0f);
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
