// ============================================================================
// Mir2 客户端 - 大世界游戏场景主控制器 (1:1 盛大 1.5 官方实证)
// 官方实证：整合地图渲染、实体管理器、玩家纸娃娃、HUD与网络分发
// ============================================================================

using System;
using System.Collections.Generic;
using Godot;
using Mir2Client.Audio;
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
                InputPluginInstance.WalkRequested += (dir) =>
                {
                    if (PlayerInstance.IsDead) return;
                    var offset = PlayerAuditEngine.DirOffsets[dir & 7];
                    int tx = PlayerInstance.TileX + offset.X;
                    int ty = PlayerInstance.TileY + offset.Y;
                    if (MapRendererInstance != null && !MapRendererInstance.IsPassable(tx, ty)) return;
                    PlayerInstance.StartStepMovement(tx, ty, 1, ActionType.Walk, dir);
                };

                InputPluginInstance.RunRequested += (dir) =>
                {
                    if (PlayerInstance.IsDead) return;
                    var offset = PlayerAuditEngine.DirOffsets[dir & 7];
                    int tx = PlayerInstance.TileX + offset.X * 2;
                    int ty = PlayerInstance.TileY + offset.Y * 2;
                    int midX = PlayerInstance.TileX + offset.X;
                    int midY = PlayerInstance.TileY + offset.Y;
                    if (MapRendererInstance != null && (!MapRendererInstance.IsPassable(midX, midY) || !MapRendererInstance.IsPassable(tx, ty)))
                        return;
                    PlayerInstance.StartStepMovement(tx, ty, 2, ActionType.Run, dir);
                };

                InputPluginInstance.HitRequested += (dir) =>
                {
                    if (PlayerInstance.IsDead) return;
                    PlayerInstance.Direction = dir & 7;
                    PlayerInstance.Anim.SetAction(ActionType.Hit, dir);
                    AudioManager.Instance?.PlaySoundId(1);
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
                HudInstance.AddChat("系统", "【官方 1.50 虎卫传说】纯 C# 单轨化大世界沙盒引擎已启动！", new Color(1f, 0.8f, 0.2f));
                HudInstance.AddChat("系统", "已为您自动装配布衣与木剑，置身比奇省安全区中心(330, 270)。", new Color(0.4f, 1f, 0.4f));
            }
        }

        public void SetNet(NetManager net, int sId, string uname, int ulevel = 35)
        {
            _net = net;
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
