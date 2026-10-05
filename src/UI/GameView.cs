// ============================================================================
// Mir2 客户端 - 大世界游戏场景主控制器 (1:1 盛大 1.5 官方实证)
// 官方实证：整合地图渲染、实体管理器、玩家纸娃娃、HUD与网络分发
// ============================================================================

using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Audio;
using Mir2Client.Core;
using Mir2Client.Game;
using Mir2Client.Net;
using Mir2Client.Render;
using Mir2Client.Utils;

namespace Mir2Client.UI
{
    public partial class GameView : Node
    {
        [Signal] public delegate void ItemEatenEventHandler(int slot);

        public const string DataDir = "res://resources/wil/Data/";
        public const string PngDir = "res://resources/wil/png/";
        public const string MapDir = "res://resources/maps/Map/";

        private NetManager? _net;
        private bool _isSandbox = false;
        public int SessionId { get; set; } = 0;
        public string Username { get; set; } = string.Empty;

        // 子系统引用
        public MapRenderer? MapRendererInstance { get; private set; }
        public PlayerNode? PlayerInstance { get; private set; }
        public InputPlugin? InputPluginInstance { get; private set; }
        public HudController? HudInstance { get; private set; }
        public EntityManager EntityManagerInstance { get; } = new();
        public MagicManager? MagicManagerInstance { get; private set; }
        public GroundItemManager? GroundItemManagerInstance { get; private set; }
        public EffectManager? EffectManagerInstance { get; private set; }

        public TeamManager TeamManagerInstance { get; } = new();
        public TradeManager TradeManagerInstance { get; } = new();
        public StorageManager StorageManagerInstance { get; } = new();
        public GuildManager GuildManagerInstance { get; } = new();

        private WilLoader? _wilFloor;
        private WilLoader? _wilSmTiles;
        private WilLoader? _wilObj;
        private WilLoader? _wilHum;
        private WilLoader? _wilWeapon;
        private WilLoader? _wilHair;

        public override void _Ready()
        {
            MapRendererInstance = GetNodeOrNull<MapRenderer>("MapRenderer");
            PlayerInstance = GetNodeOrNull<PlayerNode>("Player");
            if (PlayerInstance != null)
            {
                InputPluginInstance = PlayerInstance.GetNodeOrNull<InputPlugin>("InputPlugin");
            }
            HudInstance = GetNodeOrNull<HudController>("HUD");

            LoadResources();
            ConnectInputAndSignals();

            if (PlayerInstance != null && MapRendererInstance != null)
            {
                PlayerInstance.SetMapRenderer(MapRendererInstance);
            }
            if (InputPluginInstance != null && MapRendererInstance != null)
            {
                InputPluginInstance.SetMapRenderer(MapRendererInstance);
            }

            Callable.From(CheckAutoSandbox).CallDeferred();
        }

        private void LoadResources()
        {
            _wilFloor = new WilLoader();
            _wilSmTiles = new WilLoader();
            _wilObj = new WilLoader();
            _wilHum = new WilLoader();
            _wilWeapon = new WilLoader();
            _wilHair = new WilLoader();

            _wilFloor.Load(ResPath.Resolve("resources/wil/Data/Tiles.wil"), ResPath.Resolve("resources/wil/Data/Tiles.wix"));
            _wilSmTiles.Load(ResPath.Resolve("resources/wil/Data/SmTiles.wil"), ResPath.Resolve("resources/wil/Data/SmTiles.wix"));
            _wilObj.Load(ResPath.Resolve("resources/wil/Data/Objects.wil"), ResPath.Resolve("resources/wil/Data/Objects.wix"));
            _wilHum.Load(ResPath.Resolve("resources/wil/Data/Hum.wil"), ResPath.Resolve("resources/wil/Data/Hum.wix"));
            _wilWeapon.Load(ResPath.Resolve("resources/wil/Data/Weapon.wil"), ResPath.Resolve("resources/wil/Data/Weapon.wix"));
            _wilHair.Load(ResPath.Resolve("resources/wil/Data/Hair.wil"), ResPath.Resolve("resources/wil/Data/Hair.wix"));

            MapRendererInstance?.SetLoaders(_wilFloor, _wilObj, _wilSmTiles);
            PlayerInstance?.SetWilLoaders(_wilHum, _wilWeapon, _wilHair);
        }

        private void ConnectInputAndSignals()
        {
            if (InputPluginInstance != null && PlayerInstance != null)
            {
                PlayerInstance.StepStarted += (tx, ty, dir, isRun) =>
                {
                    if (_net != null && _net.IsConnectedToServer)
                    {
                        byte[] pkt = isRun
                            ? PacketEncoder.Run((short)tx, (short)ty, (short)dir)
                            : PacketEncoder.Walk((short)tx, (short)ty, (short)dir);
                        _net.SendPacket(pkt);
                    }
                };

                InputPluginInstance.WalkRequested += (dir) =>
                {
                    if (PlayerInstance.IsDead || PlayerInstance.IsMoving) return;
                    var offset = PlayerAuditEngine.DirOffsets[dir & 7];
                    int tx = PlayerInstance.TileX + offset.X;
                    int ty = PlayerInstance.TileY + offset.Y;
                    if (MapRendererInstance != null && !MapRendererInstance.IsPassable(tx, ty)) return;
                    PlayerInstance.StartStepMovement(tx, ty, 1, ActionType.Walk, dir);
                };

                InputPluginInstance.RunRequested += (dir) =>
                {
                    if (PlayerInstance.IsDead || PlayerInstance.IsMoving) return;
                    var offset = PlayerAuditEngine.DirOffsets[dir & 7];
                    int tx = PlayerInstance.TileX + offset.X * 2;
                    int ty = PlayerInstance.TileY + offset.Y * 2;
                    int midX = PlayerInstance.TileX + offset.X;
                    int midY = PlayerInstance.TileY + offset.Y;
                    if (MapRendererInstance != null && (!MapRendererInstance.IsPassable(midX, midY) || !MapRendererInstance.IsPassable(tx, ty)))
                    {
                        if (MapRendererInstance != null && MapRendererInstance.IsPassable(midX, midY))
                        {
                            PlayerInstance.StartStepMovement(midX, midY, 1, ActionType.Walk, dir);
                        }
                        return;
                    }
                    PlayerInstance.StartStepMovement(tx, ty, 2, ActionType.Run, dir);
                };

                InputPluginInstance.NavigateRequested += (targetTile) =>
                {
                    if (PlayerInstance.IsDead) return;
                    var startTile = new Vector2I(PlayerInstance.TileX, PlayerInstance.TileY);
                    var path = FastPathFinder.FindPath(startTile, targetTile, (x, y) => MapRendererInstance?.IsPassable(x, y) ?? true);
                    if (path != null && path.Count > 0)
                    {
                        PlayerInstance.SetPath(path, run: true);
                    }
                };

                InputPluginInstance.HitRequested += (dir) =>
                {
                    if (PlayerInstance.IsDead) return;
                    PlayerInstance.Direction = dir & 7;
                    PlayerInstance.Anim.SetAction(ActionType.Hit, dir);
                    AudioManager.Instance?.PlaySoundId(1);
                    if (_net != null && _net.IsConnectedToServer)
                    {
                        var offset = PlayerAuditEngine.DirOffsets[dir & 7];
                        short targetX = (short)(PlayerInstance.TileX + offset.X);
                        short targetY = (short)(PlayerInstance.TileY + offset.Y);
                        _net.SendPacket(PacketEncoder.Hit((short)(dir & 7), targetX, targetY));
                    }
                };

                InputPluginInstance.PickupRequested += () =>
                {
                    if (PlayerInstance.IsDead) return;
                    if (_net != null && _net.IsConnectedToServer)
                    {
                        int posRecog = (PlayerInstance.TileX << 16) | (PlayerInstance.TileY & 0xFFFF);
                        _net.SendPacket(PacketEncoder.MakePacket(ProtocolConsts.CM_PICKUP, posRecog));
                    }
                };

                InputPluginInstance.PkModeToggleRequested += () =>
                {
                    HudInstance?.CycleNextAttackMode();
                };

                InputPluginInstance.QuickItemKeyPressed += (slot) =>
                {
                    HudInstance?.UseQuickSlot(slot);
                };
            }

            if (HudInstance != null)
            {
                HudInstance.QuickItemUsed += (slot, item) =>
                {
                    if (PlayerInstance == null) return;
                    if (item.Contains("金创") || item.Contains("红"))
                    {
                        PlayerInstance.Hp = Math.Min(PlayerInstance.MaxHp, PlayerInstance.Hp + 30);
                        HudInstance.UpdateHp(PlayerInstance.Hp, PlayerInstance.MaxHp);
                        HudInstance.AddChat("提示", $"使用了 {item}，生命值恢复 30 点。", new Color(1f, 0.4f, 0.4f));
                    }
                    else if (item.Contains("魔法") || item.Contains("蓝"))
                    {
                        PlayerInstance.Mp = Math.Min(PlayerInstance.MaxMp, PlayerInstance.Mp + 30);
                        HudInstance.UpdateMp(PlayerInstance.Mp, PlayerInstance.MaxMp);
                        HudInstance.AddChat("提示", $"使用了 {item}，魔法值恢复 30 点。", new Color(0.4f, 0.6f, 1f));
                    }
                    else if (item.Contains("太阳水"))
                    {
                        PlayerInstance.Hp = Math.Min(PlayerInstance.MaxHp, PlayerInstance.Hp + 50);
                        PlayerInstance.Mp = Math.Min(PlayerInstance.MaxMp, PlayerInstance.Mp + 50);
                        HudInstance.UpdateHp(PlayerInstance.Hp, PlayerInstance.MaxHp);
                        HudInstance.UpdateMp(PlayerInstance.Mp, PlayerInstance.MaxMp);
                        HudInstance.AddChat("提示", $"使用了 {item}，生命与魔法恢复 50 点。", new Color(1f, 0.9f, 0.2f));
                    }
                };
            }
        }

        private void CheckAutoSandbox()
        {
            if (_net == null && !_isSandbox)
            {
                InitSandboxMode();
            }
        }

        public void InitSandboxMode()
        {
            if (_isSandbox) return;
            _isSandbox = true;
            GD.Print("[GameView] 启动 1.50 官方纯 C# 单机沙盒演练模式...");

            string mapPath = ResPath.Resolve("resources/maps/Map/0.map");
            MapRendererInstance?.LoadMap(mapPath);

            if (PlayerInstance != null)
            {
                PlayerInstance.CharacterName = "传奇体验者";
                PlayerInstance.SetTilePos(330, 270);
                PlayerInstance.BodyDressType = 2; // 布衣男
                PlayerInstance.WeaponType = 2;     // 木剑
                PlayerInstance.HairStyle = 1;
                PlayerInstance.Direction = 2;
                PlayerInstance.Hp = 100;
                PlayerInstance.MaxHp = 100;
                PlayerInstance.Mp = 100;
                PlayerInstance.MaxMp = 100;
                PlayerInstance.Level = 35;
                PlayerInstance.Gold = 100000;
            }

            if (HudInstance != null)
            {
                HudInstance.UpdateHp(100, 100);
                HudInstance.UpdateMp(100, 100);
                HudInstance.UpdateLevel(35);
                HudInstance.UpdateGold(100000);
                HudInstance.UpdateExp(35000, 100000);
                HudInstance.UpdateWeight(25, 60);
                HudInstance.AddChat("系统", "【官方 1.50 虎卫传说】纯 C# 单轨化大世界沙盒引擎已启动！", new Color(1f, 0.8f, 0.2f));
                HudInstance.AddChat("系统", "已为您自动装配布衣与木剑，置身比奇省安全区中心(330, 270)。", new Color(0.4f, 1f, 0.4f));
            }

            CurrentMapName = "0";
            UpdateEnvironmentLighting();
        }

        public string CurrentMapName { get; set; } = "0";

        public void UpdateEnvironmentLighting()
        {
            if (HudInstance == null || PlayerInstance == null) return;

            // 1. 判断是否地牢地图或特定黑暗区域
            bool isDark = HudInstance.FogManager.IsDarkMap(CurrentMapName);

            // 2. 依据照明装备 (火把/蜡烛) 或地表计算视野瓦片半径
            int radiusTiles = isDark ? DarknessFogManager.TorchRadius : DarknessFogManager.DaylightRadius;
            
            // 3. 计算主角在屏幕主视口上的像素中心位置 (默认中心 400, 225)
            Vector2 screenPos = new Vector2(400, 225);
            if (MapRendererInstance != null)
            {
                var pWorld = MapRendererInstance.TileToScreen(PlayerInstance.TileX, PlayerInstance.TileY);
                screenPos = new Vector2(pWorld.X + 24, pWorld.Y + 16);
            }

            HudInstance.UpdateVisionFog(isDark, radiusTiles, screenPos);
        }

        public override void _Process(double delta)
        {
            if (PlayerInstance != null && HudInstance?.Minimap != null && HudInstance.Minimap.Visible)
            {
                HudInstance.Minimap.UpdatePlayerPosition(CurrentMapName == "0" ? "比奇省" : CurrentMapName, PlayerInstance.TileX, PlayerInstance.TileY);
                
                var radarList = new List<(int tx, int ty, MinimapView.RadarEntityType type)>();
                if (EntityManagerInstance.Count > 0)
                {
                    foreach (var ent in EntityManagerInstance.AllEntities)
                    {
                        var rType = ent.Type switch
                        {
                            EntityManager.EntityType.Monster => MinimapView.RadarEntityType.Monster,
                            EntityManager.EntityType.Npc => MinimapView.RadarEntityType.Npc,
                            _ => MinimapView.RadarEntityType.GroupMember
                        };
                        radarList.Add((ent.TileX, ent.TileY, rType));
                    }
                }
                else
                {
                    // 沙盒或初始状态下的默认演示实体点
                    radarList.Add((330, 266, MinimapView.RadarEntityType.Npc));
                    radarList.Add((325, 275, MinimapView.RadarEntityType.Npc));
                    radarList.Add((345, 280, MinimapView.RadarEntityType.Monster));
                    radarList.Add((350, 260, MinimapView.RadarEntityType.Monster));
                    radarList.Add((332, 271, MinimapView.RadarEntityType.GroupMember));
                }
                HudInstance.Minimap.UpdateRadarEntities(radarList.ToArray());
            }
        }

        public void SetNet(NetManager net, int sId, string uname, int ulevel = 35)
        {
            _net = net;
            var router = new ServerMessageRouter(HudInstance, EntityManagerInstance, this);
            _net.PacketReceived += router.OnPacket;
            SessionId = sId;
            Username = uname;
            if (PlayerInstance != null)
            {
                PlayerInstance.CharacterName = uname;
                PlayerInstance.Level = ulevel;
                PlayerInstance.UpdateNameLabelText();
            }
        }
    }
}
