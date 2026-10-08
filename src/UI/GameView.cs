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
        public EffectManager? EffectManagerInstance { get; private set; }
        public ServerMessageRouter? ServerMessageRouterInstance { get; private set; }

        public TeamManager TeamManagerInstance { get; } = new();
        public TradeManager TradeManagerInstance { get; } = new();
        public StorageManager StorageManagerInstance { get; } = new();
        public GuildManager GuildManagerInstance { get; } = new();
        public PlayerState PlayerStateInstance { get; } = new();
        public WarriorCombatEngine.FireHitTracker FireHitTrackerInstance { get; } = new();
        public WarriorCombatEngine.StabbingTracker StabbingTrackerInstance { get; } = new();

        private WilLoader? _wilFloor;
        private WilLoader? _wilSmTiles;
        private WilLoader? _wilObj;
        private WilLoader? _wilHum;
        private WilLoader? _wilWeapon;
        private WilLoader? _wilHair;
        private WilLoader? _wilMagic;
        private WilLoader? _wilMagic2;
        private WilLoader? _wilEffect;
        private WilLoader? _wilMon1;
        private WilLoader? _wilMon2;
        private WilLoader? _wilMon3;
        private WilLoader? _wilNpc;
        private WilLoader? _wilDnItems;

        public Node2D EntityContainer { get; private set; } = null!;
        private readonly Dictionary<int, EntityNode> _entityNodes = new();

        public Node2D ItemContainer { get; private set; } = null!;
        public GroundItemManager GroundItemManagerInstance { get; } = new();
        private readonly Dictionary<int, GroundItemNode> _itemNodes = new();

        private ulong _lastSpellTick = 0;
        public const ulong SpellCooldownMs = 600;

        private ColorRect? _retroDitherOverlay;
        public bool IsRetroDitherEnabled => _retroDitherOverlay?.Visible ?? false;

        public void SetRetroDither(bool enable)
        {
            if (_retroDitherOverlay != null)
            {
                _retroDitherOverlay.Visible = enable;
            }
        }

        public override void _Ready()
        {
            MapRendererInstance = GetNodeOrNull<MapRenderer>("MapRenderer");
            PlayerInstance = GetNodeOrNull<PlayerNode>("Player");
            if (PlayerInstance != null)
            {
                InputPluginInstance = PlayerInstance.GetNodeOrNull<InputPlugin>("InputPlugin");
            }
            HudInstance = GetNodeOrNull<HudController>("HUD");

            MagicManagerInstance = new MagicManager();
            AddChild(MagicManagerInstance);

            ItemContainer = new Node2D { Name = "ItemContainer", YSortEnabled = true };
            AddChild(ItemContainer);
            SetupGroundItemEvents();

            EntityContainer = new Node2D { Name = "EntityContainer", YSortEnabled = true };
            AddChild(EntityContainer);
            SetupEntityManagerEvents();

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

            SetupRetroDitherOverlay();

            Callable.From(CheckAutoSandbox).CallDeferred();
        }

        private void SetupRetroDitherOverlay()
        {
            var shader = GD.Load<Shader>("res://shaders/retro_dither_16bit.gdshader");
            if (shader != null)
            {
                var mat = new ShaderMaterial { Shader = shader };
                _retroDitherOverlay = new ColorRect
                {
                    Name = "RetroDitherOverlay",
                    Size = new Vector2(800, 600),
                    Position = Vector2.Zero,
                    MouseFilter = Control.MouseFilterEnum.Ignore,
                    Material = mat,
                    ZIndex = 150,
                    Visible = false
                };
                AddChild(_retroDitherOverlay);
            }
        }

        private void LoadResources()
        {
            _wilFloor = new WilLoader();
            _wilSmTiles = new WilLoader();
            _wilObj = new WilLoader();
            _wilHum = new WilLoader();
            _wilWeapon = new WilLoader();
            _wilHair = new WilLoader();
            _wilMagic = new WilLoader();
            _wilMagic2 = new WilLoader();
            _wilEffect = new WilLoader();

            _wilFloor.Load(ResPath.Resolve("resources/wil/Data/Tiles.wil"), ResPath.Resolve("resources/wil/Data/Tiles.wix"));
            _wilSmTiles.Load(ResPath.Resolve("resources/wil/Data/SmTiles.wil"), ResPath.Resolve("resources/wil/Data/SmTiles.wix"));
            _wilObj.Load(ResPath.Resolve("resources/wil/Data/Objects.wil"), ResPath.Resolve("resources/wil/Data/Objects.wix"));
            _wilHum.Load(ResPath.Resolve("resources/wil/Data/Hum.wil"), ResPath.Resolve("resources/wil/Data/Hum.wix"));
            _wilWeapon.Load(ResPath.Resolve("resources/wil/Data/Weapon.wil"), ResPath.Resolve("resources/wil/Data/Weapon.wix"));
            _wilHair.Load(ResPath.Resolve("resources/wil/Data/Hair.wil"), ResPath.Resolve("resources/wil/Data/Hair.wix"));
            _wilMagic.Load(ResPath.Resolve("resources/wil/Data/Magic.wil"), ResPath.Resolve("resources/wil/Data/Magic.WIX"));
            _wilMagic2.Load(ResPath.Resolve("resources/wil/Data/Magic2.wil"), ResPath.Resolve("resources/wil/Data/Magic2.WIX"));
            _wilEffect.Load(ResPath.Resolve("resources/wil/Data/Effect.wil"), ResPath.Resolve("resources/wil/Data/Effect.WIX"));

            _wilMon1 = new WilLoader();
            _wilMon2 = new WilLoader();
            _wilMon3 = new WilLoader();
            _wilNpc = new WilLoader();
            _wilMon1.Load(ResPath.Resolve("resources/wil/Data/Mon1.wil"), ResPath.Resolve("resources/wil/Data/Mon1.WIX"));
            _wilMon2.Load(ResPath.Resolve("resources/wil/Data/Mon2.wil"), ResPath.Resolve("resources/wil/Data/Mon2.WIX"));
            _wilMon3.Load(ResPath.Resolve("resources/wil/Data/Mon3.wil"), ResPath.Resolve("resources/wil/Data/Mon3.WIX"));
            _wilNpc.Load(ResPath.Resolve("resources/wil/Data/npc.wil"), ResPath.Resolve("resources/wil/Data/npc.WIX"));

            _wilDnItems = new WilLoader();
            _wilDnItems.Load(ResPath.Resolve("resources/wil/Data/DnItems.wil"), ResPath.Resolve("resources/wil/Data/DnItems.WIX"));

            MapRendererInstance?.SetLoaders(_wilFloor, _wilObj, _wilSmTiles);
            PlayerInstance?.SetWilLoaders(_wilHum, _wilWeapon, _wilHair);
            MagicManagerInstance?.SetWilLoaders(_wilMagic, _wilMagic2, _wilEffect);
            if (MapRendererInstance != null)
            {
                MagicManagerInstance?.SetMapRenderer(MapRendererInstance);
            }
        }

        private void SetupGroundItemEvents()
        {
            GroundItemManagerInstance.ItemAdded += (data) =>
            {
                if (_itemNodes.ContainsKey(data.ItemId)) return;

                var node = new GroundItemNode
                {
                    Name = $"Item_{data.ItemId}",
                    ItemId = data.ItemId,
                    ItemName = data.Name,
                    TileX = data.TileX,
                    TileY = data.TileY,
                    Looks = data.Looks,
                    Amount = data.Amount
                };

                node.SetLoader(_wilDnItems);
                if (MapRendererInstance != null) node.SetMapRenderer(MapRendererInstance);

                ItemContainer.AddChild(node);
                _itemNodes[data.ItemId] = node;
            };

            GroundItemManagerInstance.ItemRemoved += (itemId) =>
            {
                if (_itemNodes.Remove(itemId, out var node))
                {
                    if (IsInstanceValid(node)) node.QueueFree();
                }
            };

            GroundItemManagerInstance.ItemsCleared += () =>
            {
                foreach (var node in _itemNodes.Values)
                {
                    if (IsInstanceValid(node)) node.QueueFree();
                }
                _itemNodes.Clear();
            };
        }

        private void SetupEntityManagerEvents()
        {
            EntityManagerInstance.EntityAdded += (data) =>
            {
                if (_entityNodes.ContainsKey(data.Uid)) return;

                var node = new EntityNode
                {
                    Name = $"Entity_{data.Uid}",
                    Uid = data.Uid,
                    EntityName = data.Name,
                    Type = data.Type,
                    Appr = data.Appr,
                    TileX = data.TileX,
                    TileY = data.TileY,
                    Direction = data.Direction,
                    Hp = data.Hp,
                    MaxHp = data.MaxHp
                };

                WilLoader? loader = data.Type == EntityManager.EntityType.Npc
                    ? _wilNpc
                    : MonsterWilPool.Instance.GetLoaderForAppr(data.Appr);

                node.SetLoader(loader);
                if (MapRendererInstance != null) node.SetMapRenderer(MapRendererInstance);
                if (data.PkPoint != 0) node.UpdatePk(data.PkPoint);
                if (data.PoisonMask != 0) node.UpdatePoison(data.PoisonMask);

                EntityContainer.AddChild(node);
                _entityNodes[data.Uid] = node;
            };

            EntityManagerInstance.EntityRemoved += (uid) =>
            {
                if (_entityNodes.Remove(uid, out var node))
                {
                    if (IsInstanceValid(node)) node.QueueFree();
                }
            };

            EntityManagerInstance.EntityTurned += (uid, dir) =>
            {
                if (_entityNodes.TryGetValue(uid, out var node))
                {
                    node.SetTilePos(node.TileX, node.TileY, dir);
                }
            };

            EntityManagerInstance.EntityMoved += (uid, x, y, dir) =>
            {
                if (_entityNodes.TryGetValue(uid, out var node))
                {
                    // 默认步长平滑位移
                    node.StartStepMovement(x, y, dir, false);
                }
            };

            EntityManagerInstance.EntityStepMoved += (uid, x, y, dir, isRun) =>
            {
                if (_entityNodes.TryGetValue(uid, out var node))
                {
                    node.StartStepMovement(x, y, dir, isRun);
                }
            };

            EntityManagerInstance.EntityReconciled += (uid, x, y, dir) =>
            {
                if (_entityNodes.TryGetValue(uid, out var node))
                {
                    node.ReconcilePosition(x, y, dir);
                }
            };

            EntityManagerInstance.EntityStruck += (uid, damage, curHp) =>
            {
                if (_entityNodes.TryGetValue(uid, out var node))
                {
                    node.TriggerStruck(damage, curHp);
                }
            };

            EntityManagerInstance.EntityDied += (uid) =>
            {
                if (_entityNodes.TryGetValue(uid, out var node))
                {
                    node.TriggerDeath();
                    if (_isSandbox)
                    {
                        SpawnMonsterLoot(node.EntityName, node.TileX, node.TileY);
                    }
                }
            };

            EntityManagerInstance.EntityPoisonChanged += (uid, mask) =>
            {
                if (_entityNodes.TryGetValue(uid, out var node))
                {
                    node.UpdatePoison(mask);
                }
            };

            EntityManagerInstance.EntityPkChanged += (uid, pk) =>
            {
                if (_entityNodes.TryGetValue(uid, out var node))
                {
                    node.UpdatePk(pk);
                }
            };
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
                    else if (_isSandbox)
                    {
                        CheckSandboxMeleeHit(dir, 15);
                    }
                };

                InputPluginInstance.PickupRequested += () =>
                {
                    if (PlayerInstance.IsDead) return;
                    PerformPickup();
                };

                PlayerInstance.TileStepped += (tile) =>
                {
                    if (PlayerInstance.IsDead) return;
                    PerformPickup();
                };

                InputPluginInstance.ButcherRequested += (tx, ty, dir) =>
                {
                    PerformButcher(tx, ty, dir);
                };

                InputPluginInstance.PkModeToggleRequested += () =>
                {
                    HudInstance?.CycleNextAttackMode();
                };

                InputPluginInstance.QuickItemKeyPressed += (slot) =>
                {
                    HudInstance?.UseQuickSlot(slot);
                };

                InputPluginInstance.SkillKeyPressed += (slot) =>
                {
                    OnSkillKeyPressed(slot);
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

                HudInstance.ChatSent += OnChatSent;
                HudInstance.ItemDropToGroundRequested += OnItemDropToGroundRequested;

                // 官方面对面安全交易网络发包打通 (CM_ADDDEALITEM / CM_DELDEALITEM / CM_DEALCHGGOLD / CM_DEALEND / CM_TRADECANCEL)
                HudInstance.TradeAddItemRequested += (slot, name) =>
                {
                    _net?.SendPacket(PacketEncoder.DealAddItem((short)slot, name));
                };
                HudInstance.TradeDelItemRequested += (slot) =>
                {
                    _net?.SendPacket(PacketEncoder.DealDelItem((short)slot));
                };
                HudInstance.TradeChangeGoldRequested += (gold) =>
                {
                    _net?.SendPacket(PacketEncoder.DealChangeGold(gold));
                };
                HudInstance.TradeConfirmRequested += () =>
                {
                    _net?.SendPacket(PacketEncoder.DealConfirm());
                };
                HudInstance.TradeCancelRequested += () =>
                {
                    _net?.SendPacket(PacketEncoder.DealCancel());
                };

                // 官方攻击模式变更发包 (CM_CHANGEATTACKMODE 1019)
                HudInstance.ChangeAttackModeRequested += (mode) =>
                {
                    _net?.SendPacket(PacketEncoder.MakePacket(ProtocolConsts.CM_CHANGEATTACKMODE, (short)mode));
                };

                // 官方 NPC 对话选项点击发包 (CM_NPCSELECT 1011)
                HudInstance.NpcActionSelected += (actionTag) =>
                {
                    int npcUid = HudInstance.NpcDialog?.CurrentNpcUid ?? 0;
                    _net?.SendPacket(PacketEncoder.MakePacket(ProtocolConsts.CM_NPCSELECT, npcUid, 0, 0, System.Text.Encoding.UTF8.GetBytes(actionTag)));
                };
                HudInstance.NpcOptionSelected += (index) =>
                {
                    int npcUid = HudInstance.NpcDialog?.CurrentNpcUid ?? 0;
                    _net?.SendPacket(PacketEncoder.NpcSelect(npcUid, string.Empty, index));
                };

                // 官方 1.5 物品使用/吃药、穿戴、脱下网络发包 (CM_EATITEM 1006 / CM_WEARITEM 1003 / CM_TAKEOFFITEM 1004)
                HudInstance.UseItemRequested += (slot) =>
                {
                    _net?.SendPacket(PacketEncoder.EatItem(slot));
                };
                HudInstance.WearItemRequested += (bagSlot, equipSlot) =>
                {
                    _net?.SendPacket(PacketEncoder.WearItem((short)bagSlot, (short)equipSlot));
                };
                HudInstance.TakeoffItemRequested += (equipSlot) =>
                {
                    _net?.SendPacket(PacketEncoder.TakeOffItem((short)equipSlot));
                };

                // 官方战士核心技能开关网络发包 (CM_THRUSTINGONOFF 3026 / CM_HALFMOONONOFF 3027)
                HudInstance.ToggleThrustingRequested += (enabled) =>
                {
                    _net?.SendPacket(PacketEncoder.ThrustingOnOff(enabled));
                };
                HudInstance.ToggleHalfMoonRequested += (enabled) =>
                {
                    _net?.SendPacket(PacketEncoder.HalfMoonOnOff(enabled));
                };

                // 官方 1.5 背包金币丢弃发包 (CM_DROPGOLD 1016, 对齐 IDA TFrmDlg_DGoldClick 0x48e564)
                HudInstance.DropGoldRequested += (amount) =>
                {
                    _net?.SendPacket(PacketEncoder.DropGold(amount));
                };

                // 官方 1.5 角色小退换人发包 (CM_SOFTCLOSE 1009, 对齐 IDA TFrmDlg_DBotLogoutClick 0x491474)
                HudInstance.SoftCloseRequested += () =>
                {
                    _net?.SendPacket(PacketEncoder.SoftClose());
                };

                // 官方 1.5 退出游戏确认与发包 (对齐 IDA TFrmDlg_DBotExitClick 0x491520)
                HudInstance.ExitGameRequested += () =>
                {
                    _net?.SendPacket(PacketEncoder.SoftClose());
                    if (HudInstance.MessageBox != null)
                    {
                        HudInstance.MessageBox.ShowMessage("确定要退出游戏吗？", true, () =>
                        {
                            GetTree().Quit();
                        });
                    }
                    else
                    {
                        GetTree().Quit();
                    }
                };

                // 官方 1.5 组队面板交互发包 (CM_CREATEGROUP 1020 / CM_ADDGROUPMEMBER 1021 / CM_DELGROUPMEMBER 1022 / CM_GROUPMODE 1019)
                if (HudInstance.GroupPanel != null)
                {
                    HudInstance.GroupPanel.CreateGroupRequested += (targetName) =>
                    {
                        _net?.SendPacket(PacketEncoder.GroupAddMember(targetName));
                    };
                    HudInstance.GroupPanel.AddMemberRequested += (targetName) =>
                    {
                        _net?.SendPacket(PacketEncoder.GroupAddMember(targetName));
                    };
                    HudInstance.GroupPanel.DelMemberRequested += (targetName) =>
                    {
                        _net?.SendPacket(PacketEncoder.GroupDelMember(targetName));
                    };
                    HudInstance.GroupPanel.GroupModeToggled += (enabled) =>
                    {
                        _net?.SendPacket(PacketEncoder.GroupModeToggle(enabled));
                    };
                }

                // 官方 1.5 商店面板交互发包 (CM_USERBUYITEM 45 / CM_USERSTORAGEWITHDRAW 1032 / CM_USERGETDETAILITEM 1013)
                if (HudInstance.ShopPanel != null)
                {
                    HudInstance.ShopPanel.BuyRequested += (npcUid, itemIdx, count) =>
                    {
                        _net?.SendPacket(PacketEncoder.BuyFromShop(npcUid, (short)count, (short)itemIdx));
                    };
                    HudInstance.ShopPanel.TakeBackRequested += (storageSlot, itemName) =>
                    {
                        _net?.SendPacket(PacketEncoder.WithdrawStorage(storageSlot));
                    };
                    HudInstance.ShopPanel.ItemSelectedForDetail += (itemName) =>
                    {
                        _net?.SendPacket(PacketEncoder.UserGetDetailItem(itemName));
                    };
                }

                // 官方 1.5 铁匠铺修理与仓库保管交互发包 (CM_USERREPAIRITEM 1023 / CM_REPAIRSPECIAL 48 / CM_USERSTORAGEDEPOSIT 1031)
                if (HudInstance.RepairPanel != null)
                {
                    HudInstance.RepairPanel.RepairConfirmed += (itemUid, isSpecial) =>
                    {
                        if (isSpecial)
                        {
                            _net?.SendPacket(PacketEncoder.RepairSpecial(0, itemUid));
                        }
                        else
                        {
                            _net?.SendPacket(PacketEncoder.RepairItem(0, itemUid));
                        }
                    };
                    HudInstance.RepairPanel.StorageDepositConfirmed += (bagSlot, itemName) =>
                    {
                        _net?.SendPacket(PacketEncoder.DepositStorage(bagSlot, itemName));
                    };
                }

                if (HudInstance.GuildPanel != null)
                {
                    HudInstance.GuildPanel.HomeRequested += () =>
                    {
                        _net?.SendPacket(PacketEncoder.GuildOpenPage(0));
                    };
                    HudInstance.GuildPanel.MemberListRequested += () =>
                    {
                        _net?.SendPacket(PacketEncoder.GuildMemberList(0));
                    };
                    HudInstance.GuildPanel.AddMemberRequested += (targetName) =>
                    {
                        _net?.SendPacket(PacketEncoder.GuildAddMember(targetName));
                    };
                    HudInstance.GuildPanel.DelMemberRequested += (targetName) =>
                    {
                        _net?.SendPacket(PacketEncoder.GuildKickMember(targetName));
                    };
                    HudInstance.GuildPanel.AllyRequested += (targetGuild) =>
                    {
                        _net?.SendPacket(PacketEncoder.GuildAllyRequest(targetGuild));
                    };
                    HudInstance.GuildPanel.CancelAllyRequested += (targetGuild) =>
                    {
                        _net?.SendPacket(PacketEncoder.GuildBreakAlly(targetGuild));
                    };
                    HudInstance.GuildPanel.NoticeUpdated += (newNotice) =>
                    {
                        _net?.SendPacket(PacketEncoder.GuildNoticeUpdate(newNotice));
                    };
                    HudInstance.GuildPanel.RankUpdated += (memberName, newRank) =>
                    {
                        _net?.SendPacket(PacketEncoder.GuildUpdateRank(memberName, newRank));
                    };
                }

                if (HudInstance.AdjustAbilPanel != null)
                {
                    HudInstance.AdjustAbilPanel.StatSubmitted += (bonusType) =>
                    {
                        _net?.SendPacket(PacketEncoder.AdjustBonus((short)bonusType));
                    };
                }

                if (HudInstance.KeySelectDialog != null)
                {
                    HudInstance.KeySelectDialog.KeyBindingConfirmed += (magicId, keyNum) =>
                    {
                        _net?.SendPacket(PacketEncoder.MagicKeyChange(magicId, (byte)keyNum));
                    };
                }

                HudInstance.MinimapToggled += () =>
                {
                    _net?.SendPacket(PacketEncoder.WantMinimap());
                };

                if (HudInstance.Minimap != null)
                {
                    HudInstance.Minimap.MapTargetSelected += (targetX, targetY) =>
                    {
                        if (PlayerInstance == null || PlayerInstance.IsDead) return;
                        var startTile = new Vector2I(PlayerInstance.TileX, PlayerInstance.TileY);
                        var targetTile = new Vector2I(targetX, targetY);
                        var path = FastPathFinder.FindPath(startTile, targetTile, (x, y) => MapRendererInstance?.IsPassable(x, y) ?? true);
                        if (path != null && path.Count > 0)
                        {
                            PlayerInstance.SetPath(path, run: true);
                            HudInstance?.AddChat("寻路", $"正在自动寻路前往目标点 ({targetX}, {targetY}) ...", new Color(0.3f, 1f, 0.8f));
                        }
                        else
                        {
                            HudInstance?.AddChat("提示", $"无法到达目标点 ({targetX}, {targetY})，该区域可能存在阻挡障碍。", new Color(1f, 0.4f, 0.4f));
                            HudInstance?.Minimap?.ClearTarget();
                        }
                    };
                }
            }
        }

        private void OnItemDropToGroundRequested(int bagSlot, string itemName, Vector2 dropScreenPos)
        {
            if (PlayerInstance == null) return;

            // 1. 计算地表瓦片坐标 (默认在主角当前脚下)
            int dropTileX = PlayerInstance.TileX;
            int dropTileY = PlayerInstance.TileY;

            // 2. 生成地表战利品掉落物
            int look = itemName.Contains("金币") ? 1 : (itemName.Contains("药") ? 2 : 13);
            GroundItemManagerInstance.AddItem(_dropItemIdSeed++, itemName, dropTileX, dropTileY, look, 1);

            // 3. 播放官方经典物品落地声效 (金币为 401，装备道具为 2)
            int sfxId = itemName.Contains("金币") ? 401 : 2;
            AudioManager.Instance?.PlaySoundId(sfxId);

            // 4. 发送网络协议包 CM_DROPITEM (1000)
            _net?.SendPacket(PacketEncoder.DropItem(bagSlot, 1));
        }

        private void OnChatSent(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg)) return;
            string trimmed = msg.Trim();

            // 1. 传送戒指瞬移指令 (@move x y / @飞 x y)
            if (trimmed.StartsWith("@move", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("@飞", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("@瞬移", StringComparison.OrdinalIgnoreCase))
            {
                var res = PlayerStateInstance.TryExecuteTeleport(
                    trimmed,
                    isTileBlocked: (x, y) => !(MapRendererInstance?.IsPassable(x, y) ?? true),
                    nowTick: (long)Time.GetTicksMsec());

                if (res.success)
                {
                    PlayerInstance?.SetTilePos(res.targetX, res.targetY);
                    HudInstance?.AddChat("提示", res.message, new Color(0.2f, 1f, 0.4f));
                    AudioManager.Instance?.PlaySoundId(2);
                }
                else
                {
                    HudInstance?.AddChat("系统", res.message, new Color(1f, 0.3f, 0.3f));
                }
                return;
            }

            // 2. 记忆套装全队召集指令 (@天地合一 / @grouprecall)
            if (trimmed.Equals("@天地合一", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Equals("@grouprecall", StringComparison.OrdinalIgnoreCase))
            {
                var teammates = new List<(int uid, string name, int x, int y)>();
                foreach (var tm in TeamManagerInstance.Members)
                {
                    teammates.Add((tm.Uid, tm.Name, 0, 0));
                }

                var res = PlayerStateInstance.TryExecuteRecall(
                    trimmed,
                    isLeader: true,
                    teammates,
                    isTileBlocked: (x, y) => !(MapRendererInstance?.IsPassable(x, y) ?? true),
                    nowTick: (long)Time.GetTicksMsec());

                if (res.success)
                {
                    HudInstance?.AddChat("行会/组队", res.message, new Color(1f, 0.9f, 0.2f));
                    AudioManager.Instance?.PlaySoundId(2);
                }
                else
                {
                    HudInstance?.AddChat("系统", res.message, new Color(1f, 0.3f, 0.3f));
                }
                return;
            }

            // 3. 复古 16-bit 像素抖动滤镜切换指令 (@dither / @16bit)
            if (trimmed.StartsWith("@dither", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("@16bit", StringComparison.OrdinalIgnoreCase))
            {
                bool enable = !IsRetroDitherEnabled;
                if (trimmed.Contains("on", StringComparison.OrdinalIgnoreCase)) enable = true;
                else if (trimmed.Contains("off", StringComparison.OrdinalIgnoreCase)) enable = false;

                SetRetroDither(enable);
                string statusMsg = enable 
                    ? "【画质设置】已开启盛大官方原版 DirectDraw 16-bit (RGB 565) 像素抖动复古滤镜！" 
                    : "【画质设置】已恢复原生 32-bit 全彩平滑高清画质！";
                HudInstance?.AddChat("系统", statusMsg, new Color(0.2f, 1f, 0.4f));
                return;
            }

            // 4. 普通聊天内容
            if (_net != null && _net.IsConnectedToServer)
            {
                _net.SendPacket(PacketEncoder.Say(trimmed));
            }
        }

        /// <summary>
        /// 受到物理或魔法攻击扣血结算 (集成法师魔法盾吸收、护身戒指蓝量抵扣与复活戒指)
        /// </summary>
        public void TakeDamage(int rawDamage, bool isArmorPiercing = false)
        {
            var res = PlayerStateInstance.TakeCombatDamageFull(rawDamage, nowTick: (long)Time.GetTicksMsec(), isArmorPiercing: isArmorPiercing);
            HudInstance?.UpdateHp(PlayerStateInstance.Hp, PlayerStateInstance.MaxHp);
            HudInstance?.UpdateMp(PlayerStateInstance.Mp, PlayerStateInstance.MaxMp);

            if (res.shieldAbsorbed > 0)
            {
                HudInstance?.AddChat("法术", $"【魔法盾】吸收化解了 {res.shieldAbsorbed} 点攻击伤害！", new Color(0.4f, 0.9f, 1f));
            }
            if (res.shieldBroken)
            {
                HudInstance?.AddChat("法术", "【魔法盾】抵御极限破裂消散！", new Color(1f, 0.4f, 0.4f));
                AudioManager.Instance?.PlaySoundId(138);
            }

            if (res.revived)
            {
                HudInstance?.AddChat("提示", "【复活戒指】神力生效！生命瞬间恢复全满！", new Color(1f, 0.95f, 0.3f));
                AudioManager.Instance?.PlaySoundId(2);
            }
            else if (res.died)
            {
                HudInstance?.AddChat("提示", "你已经阵亡……", new Color(1f, 0.2f, 0.2f));
                PlayerInstance?.Anim.SetAction(ActionType.Die, PlayerInstance.Direction);
            }
            else
            {
                HudInstance?.AddChat("承伤", $"受到攻击，剩余生命 {PlayerStateInstance.Hp}，魔法 {PlayerStateInstance.Mp}", new Color(1f, 0.6f, 0.6f));
            }
        }

        private void OnSkillKeyPressed(int slot)
        {
            if (PlayerInstance == null || PlayerInstance.IsDead) return;

            // 1. 获取按键绑定的技能 ID (slot: 0..7 对应 F1..F8)
            int keyNum = slot + 1;
            int magicId = HudInstance?.SkillPanel?.GetMagicIdForKey(keyNum) ?? 0;
            if (magicId == 0)
            {
                // 若未配置，根据默认槽位智能映射
                magicId = slot switch
                {
                    0 => 3,  // F1: 基本剑术
                    1 => 7,  // F2: 攻杀剑术
                    2 => 12, // F3: 刺杀剑术
                    3 => 25, // F4: 半月弯刀
                    4 => 26, // F5: 烈火剑法
                    5 => 27, // F6: 野蛮冲撞
                    6 => 1,  // F7: 火球术
                    7 => 10, // F8: 雷电术
                    _ => 0
                };
            }
            if (magicId == 0) return;

            // 2. 计算目标世界瓦片坐标与朝向
            var mousePos = GetViewport()?.GetMousePosition() ?? Vector2.Zero;
            var playerTile = new Vector2I(PlayerInstance.TileX, PlayerInstance.TileY);
            var targetTile = MapRendererInstance != null ? MapRendererInstance.ScreenToTile(mousePos) : playerTile;
            if (targetTile.X < 0 || targetTile.Y < 0)
            {
                targetTile = playerTile;
            }

            TriggerSkill(magicId, targetTile);
        }

        /// <summary>
        /// 触发特定技能施法，驱动角色动作、音效、魔法光效与网络协议闭环
        /// </summary>
        public void TriggerSkill(int magicId, Vector2I targetTile)
        {
            if (PlayerInstance == null || PlayerInstance.IsDead) return;

            ulong now = Time.GetTicksMsec();
            if (now - _lastSpellTick < SpellCooldownMs)
            {
                return;
            }
            _lastSpellTick = now;

            var playerTile = new Vector2I(PlayerInstance.TileX, PlayerInstance.TileY);
            bool isSelfBuff = magicId is 2 or 14 or 15 or 18 or 31;
            if (isSelfBuff && (targetTile.X < 0 || Math.Max(Math.Abs(targetTile.X - playerTile.X), Math.Abs(targetTile.Y - playerTile.Y)) > 5))
            {
                targetTile = playerTile;
            }

            int dir = PlayerInstance.Direction;
            var delta = targetTile - playerTile;
            if (delta != Vector2I.Zero)
            {
                dir = InputPlugin.CalcDirection(new Vector2(delta.X, delta.Y));
                PlayerInstance.Direction = dir;
            }

            // 检查并扣减 MP
            var meta = SkillFormulas.GetSkill((short)magicId);
            int mpCost = meta?.MpCost ?? 0;
            if (PlayerInstance.Mp < mpCost)
            {
                HudInstance?.AddChat("提示", "魔法值不足，无法施展技能！", new Color(1f, 0.4f, 0.4f));
                return;
            }

            switch (magicId)
            {
                case 3: // 基本剑术 (被动)
                    HudInstance?.AddChat("技能", "基本剑术为被动技能，攻击时自动提升精准。", new Color(1f, 0.9f, 0.5f));
                    break;

                case 6: // 施毒术 (Poisoning, MagicId = 6)
                    PlayerInstance.Anim.SetAction(ActionType.Cast, dir);
                    MagicManagerInstance?.PlayMagic(magicId, playerTile, targetTile);
                    if (_net != null && _net.IsConnectedToServer)
                    {
                        _net.SendPacket(PacketEncoder.Spell(6, (short)targetTile.X, (short)targetTile.Y));
                    }
                    else if (_isSandbox)
                    {
                        EntityManager.EntityData? poisonTarget = null;
                        foreach (var ent in EntityManagerInstance.AllEntities)
                        {
                            if (ent.Type == EntityManager.EntityType.Monster && !ent.IsDead)
                            {
                                if (Math.Abs(ent.TileX - targetTile.X) <= 1 && Math.Abs(ent.TileY - targetTile.Y) <= 1)
                                {
                                    poisonTarget = ent;
                                    break;
                                }
                            }
                        }

                        if (poisonTarget != null)
                        {
                            bool hit = TaoistCombatEngine.CheckPoisonHit(poisonTarget.AntiPoison);
                            if (hit)
                            {
                                var pType = (poisonTarget.PoisonMask & 1) == 0 ? TaoistCombatEngine.PoisonType.Green : TaoistCombatEngine.PoisonType.Red;
                                int duration = TaoistCombatEngine.CalculatePoisonDurationSec(pType, taoistSc: 25);
                                EntityManagerInstance.ApplyPoison(poisonTarget.Uid, pType, duration, sc: 25, (long)now);
                                string pName = pType == TaoistCombatEngine.PoisonType.Green ? "【绿毒】持续侵蚀" : "【红毒】防御削弱 20% 并承受 1.2 倍增伤";
                                HudInstance?.AddChat("施毒", $"施毒术命中【{poisonTarget.Name}】！附着 {pName}（持续 {duration} 秒）！", new Color(0.2f, 1f, 0.4f));
                                AudioManager.Instance?.PlaySoundId(14);
                            }
                            else
                            {
                                HudInstance?.AddChat("战斗", $"【{poisonTarget.Name}】凭借高抗毒属性成功抵抗了毒粉侵袭！", new Color(0.8f, 0.8f, 0.8f));
                            }
                        }
                    }
                    break;

                case 7: // 攻杀剑术
                    PlayerInstance.Anim.SetAction(ActionType.PowerHit, dir);
                    AudioManager.Instance?.PlaySoundId(1);
                    if (_net != null && _net.IsConnectedToServer)
                    {
                        _net.SendPacket(PacketEncoder.PowerHit((short)dir));
                    }
                    else if (_isSandbox)
                    {
                        CheckSandboxMeleeHit(dir, 30);
                    }
                    break;

                case 12: // 刺杀剑术
                    PlayerInstance.Anim.SetAction(ActionType.LongHit, dir);
                    AudioManager.Instance?.PlaySoundId(1);
                    if (_net != null && _net.IsConnectedToServer)
                    {
                        _net.SendPacket(PacketEncoder.LongHit((short)dir));
                    }
                    else if (_isSandbox)
                    {
                        CheckSandboxMeleeHit(dir, 35, isStabSkill: true);
                    }
                    break;

                case 25: // 半月弯刀
                    PlayerInstance.Anim.SetAction(ActionType.Hit, dir);
                    AudioManager.Instance?.PlaySoundId(133);
                    if (_net != null && _net.IsConnectedToServer)
                    {
                        _net.SendPacket(PacketEncoder.WideHit((short)dir));
                    }
                    else if (_isSandbox)
                    {
                        CheckSandboxMeleeHit(dir, 28);
                    }
                    break;

                case 26: // 烈火剑法
                    PlayerInstance.Anim.SetAction(ActionType.FireHit, dir);
                    long nowTick = (long)now;
                    var castRes = FireHitTrackerInstance.Cast(nowTick);
                    if (castRes.success)
                    {
                        HudInstance?.AddChat("技能", castRes.reason, new Color(1f, 0.6f, 0.1f));
                        AudioManager.Instance?.PlaySoundId(136);
                    }
                    else
                    {
                        HudInstance?.AddChat("提示", castRes.reason, new Color(1f, 0.3f, 0.3f));
                    }
                    if (_net != null && _net.IsConnectedToServer)
                    {
                        _net.SendPacket(PacketEncoder.FireHit(0, (short)dir));
                    }
                    else if (_isSandbox)
                    {
                        // 蓄力成功后如果正前方有目标，立即挥出烈火第一刀
                        CheckSandboxMeleeHit(dir, 30);
                    }
                    break;

                case 27: // 野蛮冲撞
                    PlayerInstance.Anim.SetAction(ActionType.Run, dir);
                    AudioManager.Instance?.PlaySoundId(134);
                    if (_isSandbox)
                    {
                        var offsetDash = PlayerAuditEngine.DirOffsets[dir & 7];
                        int f1x = PlayerInstance.TileX + offsetDash.X;
                        int f1y = PlayerInstance.TileY + offsetDash.Y;
                        EntityManager.EntityData? obstacle = null;
                        foreach (var ent in EntityManagerInstance.AllEntities)
                        {
                            if (ent.Type == EntityManager.EntityType.Monster && !ent.IsDead && ent.TileX == f1x && ent.TileY == f1y)
                            {
                                obstacle = ent;
                                break;
                            }
                        }

                        var obsTuple = obstacle != null ? ((bool, int, int, int)?)(true, 10, obstacle.TileX, obstacle.TileY) : null;
                        var dashRes = WarriorCombatEngine.ShoulderDash.ExecuteDash(
                            selfLevel: PlayerStateInstance.Level,
                            selfX: PlayerInstance.TileX,
                            selfY: PlayerInstance.TileY,
                            dir: dir,
                            obstacleEntity: obsTuple,
                            isTileBlocked: (x, y) => !(MapRendererInstance?.IsPassable(x, y) ?? true)
                        );

                        if (dashRes.Success)
                        {
                            PlayerInstance.SetTilePos(dashRes.SelfFinalX, dashRes.SelfFinalY);
                            if (dashRes.TargetPushed && obstacle != null)
                            {
                                obstacle.TileX = dashRes.TargetFinalX;
                                obstacle.TileY = dashRes.TargetFinalY;
                                EntityManagerInstance.TriggerStruck(obstacle.Uid, 15, Math.Max(0, obstacle.Hp - 15));
                            }
                            HudInstance?.AddChat("技能", dashRes.Message, new Color(0.2f, 1f, 0.4f));
                        }
                        else
                        {
                            if (dashRes.SelfStunned)
                            {
                                PlayerInstance.TriggerStun(dashRes.StunSec);
                                AudioManager.Instance?.PlaySoundId(1);
                            }
                            if (dashRes.SelfDamage > 0)
                            {
                                PlayerStateInstance.Hp = Math.Max(1, PlayerStateInstance.Hp - dashRes.SelfDamage);
                                HudInstance?.UpdateHp(PlayerStateInstance.Hp, PlayerStateInstance.MaxHp);
                            }
                            HudInstance?.AddChat("技能", dashRes.Message, new Color(1f, 0.3f, 0.3f));
                        }
                    }
                    if (_net != null && _net.IsConnectedToServer)
                    {
                        _net.SendPacket(PacketEncoder.Motaebo((short)dir));
                    }
                    break;

                case 17: // 召唤骷髅 (Summon Skeleton)
                    PlayerInstance.Anim.SetAction(ActionType.Cast, dir);
                    AudioManager.Instance?.PlaySoundId(15);
                    if (_net != null && _net.IsConnectedToServer)
                    {
                        _net.SendPacket(PacketEncoder.Spell(17, (short)targetTile.X, (short)targetTile.Y));
                    }
                    else if (_isSandbox)
                    {
                        var stats = TaoistCombatEngine.GetSkeletonStats(3);
                        var offsetSummon = PlayerAuditEngine.DirOffsets[(dir + 2) & 7];
                        int petX = PlayerInstance.TileX + offsetSummon.X;
                        int petY = PlayerInstance.TileY + offsetSummon.Y;
                        EntityManagerInstance.AddEntity(new EntityManager.EntityData
                        {
                            Uid = 7001,
                            Name = stats.Name,
                            Type = EntityManager.EntityType.Monster,
                            Appr = stats.Appr,
                            TileX = petX,
                            TileY = petY,
                            Hp = stats.MaxHp,
                            MaxHp = stats.MaxHp,
                            Direction = (byte)dir,
                            MasterUid = PlayerInstance.Uid,
                            PetLevel = stats.Level
                        });
                        HudInstance?.AddChat("召唤", $"消耗 1 张护身符，从地底召唤出了【{stats.Name}】守护在身旁！", new Color(0.3f, 1f, 0.6f));
                    }
                    break;

                case 30: // 召唤神兽 (Summon Shinsu)
                    PlayerInstance.Anim.SetAction(ActionType.Cast, dir);
                    AudioManager.Instance?.PlaySoundId(16);
                    if (_net != null && _net.IsConnectedToServer)
                    {
                        _net.SendPacket(PacketEncoder.Spell(30, (short)targetTile.X, (short)targetTile.Y));
                    }
                    else if (_isSandbox)
                    {
                        var stats = TaoistCombatEngine.GetShinsuStats(7);
                        var offsetSummon = PlayerAuditEngine.DirOffsets[(dir + 6) & 7];
                        int petX = PlayerInstance.TileX + offsetSummon.X;
                        int petY = PlayerInstance.TileY + offsetSummon.Y;
                        EntityManagerInstance.AddEntity(new EntityManager.EntityData
                        {
                            Uid = 7002,
                            Name = stats.Name,
                            Type = EntityManager.EntityType.Monster,
                            Appr = stats.Appr,
                            TileX = petX,
                            TileY = petY,
                            Hp = stats.MaxHp,
                            MaxHp = stats.MaxHp,
                            Direction = (byte)dir,
                            MasterUid = PlayerInstance.Uid,
                            PetLevel = stats.Level
                        });
                        HudInstance?.AddChat("召唤", $"消耗 5 张护身符，召唤出了威严强悍的【{stats.Name}】降临人间！", new Color(1f, 0.8f, 0.2f));
                    }
                    break;

                case 31: // 魔法盾 (Magic Shield)
                    PlayerInstance.Anim.SetAction(ActionType.Cast, dir);
                    MagicManagerInstance?.PlayMagic(31, playerTile, playerTile);
                    PlayerStateInstance.ActivateMagicShield(level: 3, durationSec: 30);
                    AudioManager.Instance?.PlaySoundId(137);
                    HudInstance?.AddChat("法术", "【魔法盾】神力激活！化解 70% 伤害，坚固屏障笼罩全身！", new Color(0.4f, 0.9f, 1f));
                    if (_net != null && _net.IsConnectedToServer)
                    {
                        _net.SendPacket(PacketEncoder.Spell(31, (short)playerTile.X, (short)playerTile.Y));
                    }
                    break;

                default: // 全量官方远程与法术 (火球术、雷电术、治愈术、火符、冰咆哮等)
                    PlayerInstance.Anim.SetAction(ActionType.Cast, dir);
                    MagicManagerInstance?.PlayMagic(magicId, playerTile, targetTile);
                    if (_net != null && _net.IsConnectedToServer)
                    {
                        _net.SendPacket(PacketEncoder.Spell((short)magicId, (short)targetTile.X, (short)targetTile.Y));
                    }
                    else if (_isSandbox)
                    {
                        CheckSandboxSpellHit(magicId, targetTile);
                    }
                    break;
            }

            if (mpCost > 0)
            {
                PlayerInstance.Mp = Math.Max(0, PlayerInstance.Mp - mpCost);
                HudInstance?.UpdateMp(PlayerInstance.Mp, PlayerInstance.MaxMp);
            }
        }

        public void PerformPickup()
        {
            if (PlayerInstance == null || PlayerInstance.IsDead) return;

            int px = PlayerInstance.TileX;
            int py = PlayerInstance.TileY;

            if (_net != null && _net.IsConnectedToServer)
            {
                int posRecog = (px << 16) | (py & 0xFFFF);
                _net.SendPacket(PacketEncoder.MakePacket(ProtocolConsts.CM_PICKUP, posRecog));
            }
            else if (_isSandbox)
            {
                var items = GroundItemManagerInstance.GetItemsAt(px, py);
                if (items.Count > 0)
                {
                    var picked = items[0];
                    GroundItemManagerInstance.RemoveItem(picked.ItemId);
                    if (picked.Name.Contains("金币"))
                    {
                        PlayerInstance.Gold += Math.Max(1, picked.Amount);
                        HudInstance?.UpdateGold(PlayerInstance.Gold);
                        HudInstance?.AddChat("拾取", $"捡起金币：{picked.Amount} 金币。", new Color(1f, 0.9f, 0.3f));
                        AudioManager.Instance?.PlaySoundId(AudioManager.SoundId.Money); // 官方 s_money = 106
                    }
                    else
                    {
                        HudInstance?.AddBagItem(picked.Name);
                        HudInstance?.AddChat("拾取", $"捡起物品：【{picked.Name}】。", new Color(0.4f, 1f, 0.4f));
                        AudioManager.Instance?.PlaySoundId(AudioManager.SoundId.ItemClick); // 官方 s_itmclick = 118
                    }
                }
            }
        }

        public void PerformButcher(int tx, int ty, int dir)
        {
            if (PlayerInstance == null || PlayerInstance.IsStunned || PlayerInstance.IsDead) return;

            // 1. 角色转向并进入割肉/蹲下姿势 (Delphi ClMain.pas:2890 ssAlt in Shift -> SendButchAnimal)
            PlayerInstance.Direction = dir;
            PlayerInstance.Anim.SetAction(ActionType.SitDown, dir);

            // 2. 真实网络发包
            if (_net != null && _net.IsConnectedToServer)
            {
                _net.SendPacket(PacketEncoder.MakePacket(ProtocolConsts.CM_BUTCHITEM, 0, (short)tx, (short)ty));
            }
            else if (_isSandbox)
            {
                // 3. 沙箱单机模式：割肉成功并给予高品质肉块
                AudioManager.Instance?.PlaySoundId(AudioManager.SoundId.ItemClick);
                HudInstance?.AddBagItem("品质15的鹿肉");
                HudInstance?.AddChat("割肉", "割肉成功，获得了品质 15 的【鹿肉】。", new Color(1f, 0.85f, 0.2f));
            }
        }

        private int _dropItemIdSeed = 9000;

        public void SpawnMonsterLoot(string monName, int x, int y)
        {
            if (monName.Contains("稻草人"))
            {
                // 掉落 88 枚金币与强效金创药
                GroundItemManagerInstance.AddItem(_dropItemIdSeed++, "金币", x, y, 1, 88);
                GroundItemManagerInstance.AddItem(_dropItemIdSeed++, "强效金创药", x + 1, y, 2, 1);
                HudInstance?.AddChat("掉落", "【稻草人】倒地爆出了金币与药水！", new Color(1f, 0.95f, 0.4f));
            }
            else if (monName.Contains("半兽人") || monName.Contains("骷髅"))
            {
                // 掉落 188 枚金币与轻型盔甲
                GroundItemManagerInstance.AddItem(_dropItemIdSeed++, "金币", x, y, 1, 188);
                GroundItemManagerInstance.AddItem(_dropItemIdSeed++, "轻型盔甲", x, y + 1, 13, 1);
                HudInstance?.AddChat("掉落", $"【{monName}】倒地爆出了金币与战利品！", new Color(1f, 0.95f, 0.4f));
            }
            else
            {
                GroundItemManagerInstance.AddItem(_dropItemIdSeed++, "金币", x, y, 1, 50);
            }
        }

        private void CheckSandboxMeleeHit(int dir, int damage, bool isStabSkill = false)
        {
            if (PlayerInstance == null) return;
            long now = (long)Time.GetTicksMsec();

            // 1. 若处于烈火剑法蓄力状态，结算烈火出刀消耗
            if (FireHitTrackerInstance.IsReady)
            {
                var fireRes = FireHitTrackerInstance.ConsumeOnHit(damage, now);
                damage = fireRes.damage;
                if (fireRes.isFireHit)
                {
                    HudInstance?.AddChat("特技", fireRes.desc, new Color(1f, 0.5f, 0.1f));
                    AudioManager.Instance?.PlaySoundId(136);
                }
            }

            var offset = PlayerAuditEngine.DirOffsets[dir & 7];
            int step1X = PlayerInstance.TileX + offset.X;
            int step1Y = PlayerInstance.TileY + offset.Y;
            int step2X = PlayerInstance.TileX + offset.X * 2;
            int step2Y = PlayerInstance.TileY + offset.Y * 2;

            // 2. 优先命中贴身第 1 格目标
            EntityManager.EntityData? targetStep1 = null;
            EntityManager.EntityData? targetStep2 = null;

            foreach (var ent in EntityManagerInstance.AllEntities)
            {
                if (ent.Type == EntityManager.EntityType.Monster && !ent.IsDead)
                {
                    if (ent.TileX == step1X && ent.TileY == step1Y)
                    {
                        targetStep1 = ent;
                        break;
                    }
                    else if (ent.TileX == step2X && ent.TileY == step2Y)
                    {
                        targetStep2 = ent;
                    }
                }
            }

            if (targetStep1 != null)
            {
                bool hasRed = (targetStep1.PoisonMask & 2) != 0;
                int realDmg = TaoistCombatEngine.ApplyRedPoisonVulnerability(damage, hasRed);
                int newHp = Math.Max(0, targetStep1.Hp - realDmg);
                EntityManagerInstance.TriggerStruck(targetStep1.Uid, realDmg, newHp);
                string redDesc = hasRed ? "【红毒破防 1.2 倍增伤】" : "";
                HudInstance?.AddChat("战斗", $"{redDesc}近身命中【{targetStep1.Name}】，造成 {realDmg} 点物理伤害！", new Color(1f, 0.8f, 0.2f));
                return;
            }

            // 3. 若第 1 格无目标，且开启刺杀开关或触发刺杀技能，判定隔位第 2 格剑气
            if (targetStep2 != null && (isStabSkill || StabbingTrackerInstance.AutoStabEnabled))
            {
                var (finalDmg, isPierce, desc) = WarriorCombatEngine.StabbingTracker.CalculateDamage(
                    damage,
                    WarriorCombatEngine.StabbingTracker.HitDistanceType.StabbingFar,
                    autoStab: true,
                    targetAc: 15,
                    targetShieldReduction: 0.6);

                bool hasRed = (targetStep2.PoisonMask & 2) != 0;
                int realDmg = TaoistCombatEngine.ApplyRedPoisonVulnerability(finalDmg, hasRed);
                int newHp = Math.Max(0, targetStep2.Hp - realDmg);
                EntityManagerInstance.TriggerStruck(targetStep2.Uid, realDmg, newHp);
                string redDesc = hasRed ? "【红毒 1.2 倍】" : "";
                HudInstance?.AddChat("刺杀", $"【隔位刺杀】{redDesc}剑气穿透！命中【{targetStep2.Name}】，无视防御与魔法盾造成 {realDmg} 点穿透伤害！", new Color(0.3f, 0.9f, 1f));
                AudioManager.Instance?.PlaySoundId(1);
            }
        }


        private void CheckSandboxSpellHit(int magicId, Vector2I targetTile)
        {
            int damage = magicId switch
            {
                1 => 20,  // 火球术
                10 => 45, // 雷电术
                22 => 30, // 火墙
                23 => 35, // 疾光电影
                33 => 60, // 冰咆哮
                _ => 25
            };

            foreach (var ent in EntityManagerInstance.AllEntities)
            {
                if (ent.Type == EntityManager.EntityType.Monster && !ent.IsDead)
                {
                    if (Math.Abs(ent.TileX - targetTile.X) <= 1 && Math.Abs(ent.TileY - targetTile.Y) <= 1)
                    {
                        bool hasRed = (ent.PoisonMask & 2) != 0;
                        int realDmg = TaoistCombatEngine.ApplyRedPoisonVulnerability(damage, hasRed);
                        int newHp = Math.Max(0, ent.Hp - realDmg);
                        EntityManagerInstance.TriggerStruck(ent.Uid, realDmg, newHp);
                        string redDesc = hasRed ? "【红毒 1.2 倍增伤】" : "";
                        HudInstance?.AddChat("战斗", $"{redDesc}法术命中【{ent.Name}】，造成 {realDmg} 点魔法伤害！", new Color(0.4f, 0.9f, 1f));
                    }
                }
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

            if (ServerMessageRouterInstance == null)
            {
                ServerMessageRouterInstance = new ServerMessageRouter(HudInstance, EntityManagerInstance, this, GroundItemManagerInstance);
            }

            string mapPath = ResPath.Resolve("resources/maps/Map/0.map");
            MapRendererInstance?.LoadMap(mapPath);

            EntityManagerInstance.Clear();
            // 比奇城安全区纯净还原：稻草人与骷髅兵部署在城外演武训练场 (远离安全区 (330, 270))，杜绝界面透出怪物尸体惊悚感
            EntityManagerInstance.AddEntity(new EntityManager.EntityData
            {
                Uid = 1001,
                Name = "稻草人",
                Type = EntityManager.EntityType.Monster,
                Appr = 10,
                TileX = 360,
                TileY = 290,
                Direction = 6,
                Hp = 80,
                MaxHp = 80
            });
            EntityManagerInstance.AddEntity(new EntityManager.EntityData
            {
                Uid = 1002,
                Name = "骷髅战士",
                Type = EntityManager.EntityType.Monster,
                Appr = 21,
                TileX = 362,
                TileY = 292,
                Direction = 2,
                Hp = 120,
                MaxHp = 120
            });
            EntityManagerInstance.AddEntity(new EntityManager.EntityData
            {
                Uid = 2001,
                Name = "比奇老兵",
                Type = EntityManager.EntityType.Npc,
                Appr = 0,
                TileX = 330,
                TileY = 266,
                Direction = 4,
                Hp = 1000,
                MaxHp = 1000
            });

            if (PlayerInstance != null)
            {
                PlayerInstance.CharacterName = "热血战士";
                PlayerInstance.SetTilePos(330, 270);
                PlayerInstance.BodyDressType = 2; // 布衣男
                PlayerInstance.WeaponType = 2;     // 木剑
                PlayerInstance.HairStyle = 4; // 官方经典男战士短发 (Hair.wil base 2400)
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
                HudInstance.UpdateFullAbilities(
                    wearWeight: 15, maxWearWeight: 30,
                    handWeight: 12, maxHandWeight: 25,
                    accuracy: 15, agility: 18,
                    magicDef: "0-0", poisonDef: "0%",
                    poisonRecover: "+10%", hpRecover: "+15%", mpRecover: "+20%");
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

            // 驱动全图实体绿毒掉血与到期清除
            long nowTick = (long)Time.GetTicksMsec();
            EntityManagerInstance.ProcessPoisonTicks(nowTick, (uid, dmg, newHp) =>
            {
                var ent = EntityManagerInstance.GetEntity(uid);
                if (ent != null)
                {
                    HudInstance?.AddChat("施毒", $"【绿毒侵蚀】目标【{ent.Name}】受到 {dmg} 点持续剧毒伤害！", new Color(0.2f, 1f, 0.3f));
                }
            });

            // 驱动官方 15 格切比雪夫距离监控 (超距自动关闭 NPC 交互窗口)
            if (PlayerInstance != null)
            {
                HudInstance?.OnPlayerPositionChanged(PlayerInstance.TileX, PlayerInstance.TileY);
            }
        }

        public void SetNet(NetManager net, int sId, string uname, int ulevel = 35)
        {
            _net = net;
            ServerMessageRouterInstance = new ServerMessageRouter(HudInstance, EntityManagerInstance, this, GroundItemManagerInstance);
            _net.PacketReceived += ServerMessageRouterInstance.OnPacket;
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
