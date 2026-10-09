// ============================================================================
// Mir2 客户端 - 大世界游戏场景主控制器 (1:1 盛大 1.5 官方实证)
// 官方实证：整合地图渲染、实体管理器、玩家纸娃娃、HUD与网络分发
// ============================================================================

using System;
using System.Collections.Generic;
using Godot;
using Mir2.Shared.Protocols.Modern;
using Mir2.Shared.Rules;
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
        private bool _isAllowDeal = true;
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
        private int _selectedTargetUid = 0;
        public int SelectedTargetUid => _selectedTargetUid;

        public Node2D ItemContainer { get; private set; } = null!;
        public GroundItemManager GroundItemManagerInstance { get; } = new();
        private readonly Dictionary<int, GroundItemNode> _itemNodes = new();

        // 盛大官方 1.5 药水双轨回复系统 (瞬回与持续缓回池)
        public PotionSystem PotionRecoverySystem { get; } = new();

        // 盛大官方 1.5 玩家查名频控节流缓存 (CM_QUERYUSERNAME 80 / CM_QUERYUSERNAME_EXT 82 防抖拦截)
        private readonly Dictionary<int, long> _queryUserThrottle = new();
        public const long QueryUserDebounceMs = 600;

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
            if (InputPluginInstance != null)
            {
                if (MapRendererInstance != null) InputPluginInstance.SetMapRenderer(MapRendererInstance);
                InputPluginInstance.SetEntityManager(EntityManagerInstance);
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

                string masterName = data.MasterUid > 0 && EntityManagerInstance.GetEntity(data.MasterUid) is { } master
                    ? master.Name
                    : string.Empty;

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
                    MaxHp = data.MaxHp,
                    PetLevel = data.PetLevel,
                    MasterName = masterName
                };

                WilLoader? loader = data.Type == EntityManager.EntityType.Npc
                    ? _wilNpc
                    : MonsterWilPool.Instance.GetLoaderForAppr(data.Appr);

                node.SetLoader(loader);
                if (MapRendererInstance != null) node.SetMapRenderer(MapRendererInstance);
                if (data.PkPoint != 0) node.UpdatePk(data.PkPoint);
                if (data.PoisonMask != 0) node.UpdatePoison(data.PoisonMask);
                if (data.PetLevel > 0 || !string.IsNullOrEmpty(masterName)) node.SetPetInfo(data.PetLevel, masterName);

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

            EntityManagerInstance.EntityHitTriggered += (uid, dir) =>
            {
                if (_entityNodes.TryGetValue(uid, out var node))
                {
                    node.PlayAction(ActionType.Hit, dir, 0.4f);
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
                    // 盛大官方 1.5 规则: 玩家移动步伐时，面对面交易、仓库、商店与 NPC 对话框自动强制中断关闭
                    HudInstance?.StackManager.CloseWindowsOnMovement();
                    if (HudInstance?.TradePanel != null && HudInstance.TradePanel.Visible)
                    {
                        HudInstance.CloseTrade();
                    }

                    if (_net != null && _net.IsConnectedToServer)
                    {
                        if (_net.UseModernBinaryProtocol)
                        {
                            Span<byte> movePayload = stackalloc byte[6];
                            new BinaryProtocol.MoveIntentPayload
                            {
                                TargetX = (short)tx,
                                TargetY = (short)ty,
                                Direction = (byte)(dir & 7),
                                IsRun = (byte)(isRun ? 1 : 0)
                            }.Write(movePayload);
                            _net.SendBinaryIntent((ushort)BinaryProtocol.MsgType.IntentMove, movePayload);
                        }
                        else
                        {
                            byte[] pkt = isRun
                                ? PacketEncoder.Run((short)tx, (short)ty, (short)dir)
                                : PacketEncoder.Walk((short)tx, (short)ty, (short)dir);
                            _net.SendPacket(pkt);
                        }
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

                        if (_net.UseModernBinaryProtocol)
                        {
                            Span<byte> attackPayload = stackalloc byte[6];
                            new BinaryProtocol.AttackIntentPayload
                            {
                                TargetUid = _selectedTargetUid,
                                Direction = (byte)(dir & 7),
                                SkillType = 0
                            }.Write(attackPayload);
                            _net.SendBinaryIntent((ushort)BinaryProtocol.MsgType.IntentAttack, attackPayload);
                        }
                        else
                        {
                            _net.SendPacket(PacketEncoder.Hit((short)(dir & 7), targetX, targetY));
                        }
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
                    if (PlayerInstance.IsDead) return;
                    PerformButcher(tx, ty, dir);
                };

                InputPluginInstance.PkModeToggleRequested += () =>
                {
                    HudInstance?.CycleNextAttackMode();
                };

                InputPluginInstance.QuickItemKeyPressed += (slot) =>
                {
                    if (PlayerInstance.IsDead) return;
                    HudInstance?.UseQuickSlot(slot);
                };

                InputPluginInstance.SkillKeyPressed += (slot) =>
                {
                    OnSkillKeyPressed(slot);
                };

                InputPluginInstance.TargetSelected += (targetUid) =>
                {
                    _selectedTargetUid = targetUid;
                };

                InputPluginInstance.EntityInteracted += (targetUid, entityType, tileX, tileY, isShift) =>
                {
                    HandleEntityInteraction(targetUid, (EntityManager.EntityType)entityType, tileX, tileY, isShift);
                };

                // 官方 1.5 快捷键闭环连接 (Alt+X 退出, Alt+Q 小退, 各原生 UI 切换)
                InputPluginInstance.QuitRequested += () => HudInstance?.RequestExitGame();
                InputPluginInstance.LogoutRequested += () => HudInstance?.RequestSoftClose();
                InputPluginInstance.BagToggleRequested += () => HudInstance?.ToggleBag();
                InputPluginInstance.EquipToggleRequested += () => HudInstance?.ToggleEquip();
                InputPluginInstance.SkillToggleRequested += () => HudInstance?.ToggleSkill();
                InputPluginInstance.GroupToggleRequested += () => HudInstance?.ToggleGroup();
                InputPluginInstance.GroupAllowToggleRequested += () => HudInstance?.GroupPanel?.ToggleAllowGroup();
                InputPluginInstance.HalfMoonToggleRequested += () => HudInstance?.ToggleHalfMoon();
                InputPluginInstance.ThrustingToggleRequested += () => HudInstance?.ToggleThrusting();
                InputPluginInstance.PetCommandRequested += () => TogglePetCommand();
                InputPluginInstance.ScreenCaptureRequested += () => CaptureScreen();
                InputPluginInstance.MinimapToggleRequested += () => HudInstance?.ToggleMinimap();
                InputPluginInstance.EscapeRequested += () => HudInstance?.CloseTopDialog();
            }

            if (HudInstance != null)
            {
                HudInstance.QuickItemUsed += (slot, item) =>
                {
                    // 盛大官方 1.5 快捷栏使用物品发包 CM_EATITEM (1006)
                    if (_net != null && _net.IsConnectedToServer)
                    {
                        if (_net.UseModernBinaryProtocol)
                        {
                            Span<byte> itemPayload = stackalloc byte[4];
                            System.Buffers.Binary.BinaryPrimitives.WriteInt32LittleEndian(itemPayload, slot);
                            _net.SendBinaryIntent((ushort)BinaryProtocol.MsgType.IntentUseItem, itemPayload);
                        }
                        else
                        {
                            _net.SendPacket(PacketEncoder.EatItem(slot, item));
                        }
                    }
                    else if (_isSandbox)
                    {
                        ApplyPotionRecovery(item);
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

                // 官方 1.5 仓库保管箱存取发包 (CM_USERSTORAGEDEPOSIT 1031 / CM_USERSTORAGEWITHDRAW 1032)
                HudInstance.StorageDepositRequested += (bagSlot, itemName) =>
                {
                    _net?.SendPacket(PacketEncoder.DepositStorage(bagSlot, itemName));
                };
                HudInstance.StorageWithdrawRequested += (storageSlot) =>
                {
                    _net?.SendPacket(PacketEncoder.WithdrawStorage(storageSlot));
                };

                // 官方攻击模式变更发包 (官方原版发送 @AttackMode 聊天指令，避免 1019 破坏 CM_GROUPMODE 组队状态)
                HudInstance.ChangeAttackModeRequested += (mode) =>
                {
                    _net?.SendPacket(PacketEncoder.Say("@AttackMode"));
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
                    string npcName = EntityManagerInstance.GetEntity(npcUid)?.Name ?? string.Empty;
                    _net?.SendPacket(PacketEncoder.NpcSelect(npcUid, npcName, index));
                };

                // 现代二进制意图吃药与官方兼容发包
                HudInstance.UseItemRequested += (slot, item) =>
                {
                    if (PlayerInstance?.IsDead == true) return;
                    if (_net != null && _net.IsConnectedToServer && _net.UseModernBinaryProtocol)
                    {
                        Span<byte> itemPayload = stackalloc byte[4];
                        System.Buffers.Binary.BinaryPrimitives.WriteInt32LittleEndian(itemPayload, slot);
                        _net.SendBinaryIntent((ushort)BinaryProtocol.MsgType.IntentUseItem, itemPayload);
                    }
                    else
                    {
                        if (_net != null && _net.IsConnectedToServer)
                        {
                            _net.SendPacket(PacketEncoder.EatItem(slot, item));
                        }
                        else if (_isSandbox)
                        {
                            ApplyPotionRecovery(item);
                        }
                    }
                };
                HudInstance.WearItemRequested += (bagSlot, equipSlot) =>
                {
                    if (PlayerInstance?.IsDead == true) return;
                    _net?.SendPacket(PacketEncoder.WearItem((short)bagSlot, (short)equipSlot));
                };
                HudInstance.TakeoffItemRequested += (equipSlot) =>
                {
                    if (PlayerInstance?.IsDead == true) return;
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

                // 官方 1.5 角色小退换人模态确认与发包 (对齐 IDA TFrmDlg_DBotLogoutClick 0x491474 / sub_461DEC 0x461338)
                HudInstance.SoftCloseRequested += () =>
                {
                    if (HudInstance.MessageBox != null)
                    {
                        HudInstance.MessageBox.ShowMessage("你想退出传奇吗？", true, () =>
                        {
                            _net?.SendPacket(PacketEncoder.SoftClose());
                            HudInstance.AddChat("系统", "[系统] 正在注销当前角色并返回选角神龛...", new Color(1f, 0.8f, 0.4f));
                            var main = GetTree().Root.GetNodeOrNull<Main>("Main");
                            main?.SwitchPhase(Main.Phase.SelectChar);
                        });
                    }
                    else
                    {
                        _net?.SendPacket(PacketEncoder.SoftClose());
                    }
                };

                // 官方 1.5 退出游戏模态确认与发包 (对齐 IDA TFrmDlg_DBotExitClick 0x491520 / sub_461DEC 0x461244)
                HudInstance.ExitGameRequested += () =>
                {
                    if (HudInstance.MessageBox != null)
                    {
                        HudInstance.MessageBox.ShowMessage("你想退出吗 ?", true, () =>
                        {
                            _net?.SendPacket(PacketEncoder.SoftClose());
                            GetTree().Quit();
                        });
                    }
                    else
                    {
                        _net?.SendPacket(PacketEncoder.SoftClose());
                        GetTree().Quit();
                    }
                };

                // 官方 1.5 组队面板交互发包 (CM_CREATEGROUP 1020 / CM_ADDGROUPMEMBER 1021 / CM_DELGROUPMEMBER 1022 / CM_GROUPMODE 1019)
                if (HudInstance.GroupPanel != null)
                {
                    HudInstance.GroupPanel.CreateGroupRequested += (targetName) =>
                    {
                        _net?.SendPacket(PacketEncoder.GroupCreate());
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
                    HudInstance.GuildPanel.WarRequested += (targetGuild) =>
                    {
                        _net?.SendPacket(PacketEncoder.GuildApplyWar(targetGuild));
                    };
                    HudInstance.GuildPanel.CancelWarRequested += (targetGuild) =>
                    {
                        _net?.SendPacket(PacketEncoder.GuildCancelWar(targetGuild));
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

                if (HudInstance.DeathScreen != null)
                {
                    HudInstance.DeathScreen.ReviveAtStartRequested += () =>
                    {
                        PerformPlayerRevive();
                    };
                    HudInstance.DeathScreen.ExitGameRequested += () =>
                    {
                        HudInstance?.RequestExitGame();
                    };
                }
            }
        }

        /// <summary>
        /// 盛大官方 1.5 药水双轨回复与单机沙盒数值对齐 (战法道全量药水):
        /// 1. 金创药族 / 魔法药族: 持续缓回机制 (注入 PotionSystem 缓冲池，每 250ms tick 定频注入，经典顶药);
        /// 2. 太阳水族 (太阳水/强效太阳水/疗伤药/万年雪霜): 瞬时即刻加血加魔 (Instant Heal)，并调用 HudInstance.ApplyHeal。
        /// </summary>
        private void ApplyPotionRecovery(string item)
        {
            if (PlayerInstance == null || HudInstance == null || string.IsNullOrEmpty(item)) return;

            long nowMs = (long)Time.GetTicksMsec();
            var meta = PotionSystem.ParsePotion(item);
            if (meta.Kind == PotionSystem.PotionKind.None) return;

            var (instantHp, instantMp) = PotionRecoverySystem.Drink(item, nowMs);

            if (meta.IsInstant)
            {
                if (instantHp > 0)
                {
                    PlayerInstance.Hp = Math.Min(PlayerInstance.MaxHp, PlayerInstance.Hp + instantHp);
                    HudInstance.ApplyHeal(instantHp);
                }
                if (instantMp > 0)
                {
                    PlayerInstance.Mp = Math.Min(PlayerInstance.MaxMp, PlayerInstance.Mp + instantMp);
                    HudInstance.UpdateMp(PlayerInstance.Mp, PlayerInstance.MaxMp);
                }

                string gainText = instantHp > 0 && instantMp > 0
                    ? $"生命瞬间恢复 {instantHp} 点，魔法瞬间恢复 {instantMp} 点"
                    : (instantHp > 0 ? $"生命瞬间恢复 {instantHp} 点" : $"魔法瞬间恢复 {instantMp} 点");
                HudInstance.AddChat("提示", $"使用了 {item}，{gainText}。", new Color(0.6f, 1f, 0.6f));
            }
            else
            {
                // 缓回药水入池
                string poolText = meta.TotalHp > 0
                    ? $"开始缓慢恢复生命 (总计 {meta.TotalHp} 点，当前缓回池: {PotionRecoverySystem.PendingHp} 点)"
                    : $"开始缓慢恢复魔法 (总计 {meta.TotalMp} 点，当前缓回池: {PotionRecoverySystem.PendingMp} 点)";
                HudInstance.AddChat("提示", $"使用了 {item}，{poolText}。", new Color(0.6f, 0.95f, 0.6f));
            }
        }

        private bool _isPetResting = false;

        /// <summary>
        /// 盛大官方 1.5 道士快捷键 Ctrl+A 宝宝状态切换 (休息/攻击)
        /// </summary>
        private void TogglePetCommand()
        {
            _isPetResting = !_isPetResting;
            string status = _isPetResting ? "休息" : "攻击";
            HudInstance?.AddChat("系统", $"[系统] 召唤兽进入{status}状态。", new Color(0.4f, 1.0f, 0.4f));
        }

        /// <summary>
        /// 盛大官方 1.5 PrintScreen/Pause 全屏截屏 (自动保存至 Images/ 目录)
        /// </summary>
        private void CaptureScreen()
        {
            try
            {
                var img = GetViewport().GetTexture().GetImage();
                if (img != null)
                {
                    string dir = "Images";
                    if (!System.IO.Directory.Exists(dir))
                    {
                        System.IO.Directory.CreateDirectory(dir);
                    }
                    string filename = System.IO.Path.Combine(dir, $"Mir_{DateTime.Now:yyyyMMdd_HHmmss}.png");
                    img.SavePng(filename);
                    HudInstance?.AddChat("系统", $"[系统] 游戏截图已保存至 {filename}。", new Color(1.0f, 0.95f, 0.3f));
                    AudioManager.Instance?.PlaySoundId(AudioManager.SoundId.NormButtonClick);
                }
            }
            catch (Exception ex)
            {
                GD.PrintErr($"[GameView] 截屏保存失败: {ex.Message}");
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

            // 3. 允许/拒绝组队模式指令 (@允许编组 / @拒绝编组 / @编组 / @groupmode)
            if (trimmed.Equals("@允许编组", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Equals("@允许组队", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Equals("@拒绝编组", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Equals("@拒绝组队", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Equals("@编组", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Equals("@groupmode", StringComparison.OrdinalIgnoreCase))
            {
                HudInstance?.GroupPanel?.ToggleAllowGroup();
                return;
            }

            // 4. 允许/拒绝交易模式指令 (@允许交易 / @拒绝交易 / @交易 / @allowdeal)
            if (trimmed.Equals("@允许交易", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Equals("@拒绝交易", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Equals("@交易", StringComparison.OrdinalIgnoreCase) ||
                trimmed.Equals("@allowdeal", StringComparison.OrdinalIgnoreCase))
            {
                _isAllowDeal = !_isAllowDeal;
                string statusMsg = _isAllowDeal 
                    ? "[系统] 已开启允许面对面交易模式。" 
                    : "[系统] 已关闭允许面对面交易模式，其他人将无法向你发起交易。";
                HudInstance?.AddChat("系统", statusMsg, new Color(1f, 0.95f, 0.3f));
                if (_net != null && _net.IsConnectedToServer)
                {
                    _net.SendPacket(PacketEncoder.Say("@AllowDeal"));
                }
                return;
            }

            // 5. 官方 1.5 探测项链坐标侦测指令 (@探测 玩家名字 / @Searching 玩家名字)
            if (trimmed.StartsWith("@探测", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("@Searching", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("@probe", StringComparison.OrdinalIgnoreCase))
            {
                string[] parts = trimmed.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length >= 2)
                {
                    string targetName = parts[1];
                    HudInstance?.AddChat("提示", $"【探测项链】正在探查目标玩家 [{targetName}] 当前坐标方位...", new Color(0.4f, 1f, 0.5f));
                    if (_net != null && _net.IsConnectedToServer)
                    {
                        _net.SendPacket(PacketEncoder.Say($"@Searching {targetName}"));
                    }
                }
                else
                {
                    HudInstance?.AddChat("系统", "【探测项链】格式错误，正确格式: @探测 玩家名字", new Color(1f, 0.4f, 0.4f));
                }
                return;
            }

            // 6. 官方 1.5 攻击模式切换指令 (@攻击模式 / @AttackMode)
            if (trimmed.StartsWith("@攻击模式", StringComparison.OrdinalIgnoreCase) ||
                trimmed.StartsWith("@AttackMode", StringComparison.OrdinalIgnoreCase))
            {
                HudInstance?.CycleNextAttackMode();
                return;
            }

            // 7. 复古 16-bit 像素抖动滤镜切换指令 (@dither / @16bit)
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

            // 8. 聊天发包处理 (普通说话 CM_SAY 3030 与 私聊 CM_WHISPER 3030 分流)
            if (_net != null && _net.IsConnectedToServer)
            {
                if (trimmed.StartsWith("/"))
                {
                    string content = trimmed[1..].TrimStart();
                    int spaceIdx = content.IndexOf(' ');
                    if (spaceIdx > 0)
                    {
                        string target = content[..spaceIdx].Trim();
                        string whisperMsg = content[(spaceIdx + 1)..].Trim();
                        _net.SendPacket(PacketEncoder.Whisper(target, whisperMsg));
                    }
                    else
                    {
                        _net.SendPacket(PacketEncoder.Say(trimmed));
                    }
                }
                else
                {
                    _net.SendPacket(PacketEncoder.Say(trimmed));
                }
            }
        }

        /// <summary>
        /// 受到物理或魔法攻击扣血结算 (集成法师魔法盾吸收、护身戒指蓝量抵扣与复活戒指)
        /// </summary>
        public void TakeDamage(int rawDamage, bool isArmorPiercing = false)
        {
            // 盛大官方 1.5 规则: 受到攻击受击结算时强制中断面对面交易
            if (HudInstance?.TradePanel != null && HudInstance.TradePanel.Visible)
            {
                HudInstance.CloseTrade();
            }

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
                TriggerPlayerDeath();
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

            void SendCastSkillPacket(short magicId, int targetX, int targetY, int targetUid = 0)
            {
            if (_net == null || !_net.IsConnectedToServer) return;
            if (_net.UseModernBinaryProtocol)
            {
                Span<byte> spellPayload = stackalloc byte[9];
                new BinaryProtocol.SpellIntentPayload
                {
                    TargetUid = targetUid,
                    SpellId = (byte)magicId,
                    TargetX = (short)targetX,
                    TargetY = (short)targetY
                }.Write(spellPayload);
                _net.SendBinaryIntent((ushort)BinaryProtocol.MsgType.IntentSpell, spellPayload);
            }
            else
            {
                _net.SendPacket(PacketEncoder.Spell(magicId, (short)targetX, (short)targetY));
            }
        }

        switch (magicId)
            {
                case 3: // 基本剑术 (被动)
                    HudInstance?.AddChat("技能", "基本剑术为被动技能，攻击时自动提升精准。", new Color(1f, 0.9f, 0.5f));
                    break;

                case 6: // 施毒术 (Poisoning, MagicId = 6)
                    // 盛大官方 1.5 检定：必须持有灰色/黄色毒粉且持久充足
                    var pBagDura = HudInstance?.GetTalismanOrPoisonDura(TaoistCombatEngine.PoisonDuraCost) ?? 1000;
                    var poisonCheck = TaoistCombatEngine.TryConsumePoisonBag(pBagDura, TaoistCombatEngine.PoisonType.Green);
                    if (!poisonCheck.success)
                    {
                        HudInstance?.AddChat("提示", poisonCheck.desc, new Color(1f, 0.4f, 0.4f));
                        return;
                    }

                    PlayerInstance.Anim.SetAction(ActionType.Cast, dir);
                    MagicManagerInstance?.PlayMagic(magicId, playerTile, targetTile);
                    SendCastSkillPacket(6, targetTile.X, targetTile.Y, _selectedTargetUid);
                    if (_isSandbox)
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

                case 8: // 抗拒火环 (Ring of Fire)
                    PlayerInstance.Anim.SetAction(ActionType.Cast, dir);
                    AudioManager.Instance?.PlaySoundId(134);
                    if (_isSandbox)
                    {
                        var surrounding = new List<(int uid, int level, int x, int y, bool isBoss)>();
                        foreach (var ent in EntityManagerInstance.AllEntities)
                        {
                            if (ent.Type == EntityManager.EntityType.Monster && !ent.IsDead)
                            {
                                surrounding.Add((ent.Uid, 25, ent.TileX, ent.TileY, false));
                            }
                        }
                        var pushResults = MageCombatEngine.RingOfFire.Execute(
                            casterLevel: PlayerStateInstance.Level,
                            casterX: PlayerInstance.TileX,
                            casterY: PlayerInstance.TileY,
                            surroundingEntities: surrounding,
                            isTileBlocked: (x, y) => !(MapRendererInstance?.IsPassable(x, y) ?? true)
                        );
                        int pushedCount = 0;
                        foreach (var res in pushResults)
                        {
                            if (res.Pushed)
                            {
                                pushedCount++;
                                var ent = EntityManagerInstance.GetEntity(res.Uid);
                                if (ent != null)
                                {
                                    ent.TileX = res.FinalX;
                                    ent.TileY = res.FinalY;
                                    EntityManagerInstance.TriggerStruck(ent.Uid, 5, Math.Max(0, ent.Hp - 5));
                                }
                            }
                        }
                        string fireMsg = pushedCount > 0
                            ? $"抗拒火环轰鸣！强劲火流震退了周围 {pushedCount} 个目标！"
                            : "抗拒火环呼啸而过，周围没有可以推动的目标。";
                        HudInstance?.AddChat("技能", fireMsg, new Color(1f, 0.5f, 0.2f));
                    }
                    SendCastSkillPacket(8, PlayerInstance.TileX, PlayerInstance.TileY);
                    break;

                case 9: // 诱惑之光 (Temptation)
                    PlayerInstance.Anim.SetAction(ActionType.Cast, dir);
                    AudioManager.Instance?.PlaySoundId(135);
                    if (_isSandbox)
                    {
                        EntityManager.EntityData? targetMonster = null;
                        foreach (var ent in EntityManagerInstance.AllEntities)
                        {
                            if (ent.Type == EntityManager.EntityType.Monster && !ent.IsDead && ent.TileX == targetTile.X && ent.TileY == targetTile.Y)
                            {
                                targetMonster = ent;
                                break;
                            }
                        }
                        if (targetMonster != null)
                        {
                            var tameRes = MageCombatEngine.Temptation.Evaluate(
                                casterLevel: PlayerStateInstance.Level,
                                skillLevel: 3,
                                monsterLevel: 25,
                                currentPetCount: 0,
                                isUndeadOrBoss: false
                            );
                            if (tameRes.Outcome == MageCombatEngine.Temptation.TameOutcome.Tamed)
                            {
                                targetMonster.MasterUid = EntityManagerInstance.LocalPlayerUid;
                                targetMonster.PetLevel = 1;
                                if (_entityNodes.TryGetValue(targetMonster.Uid, out var node))
                                {
                                    node.SetPetInfo(1, Username);
                                }
                                HudInstance?.AddChat("技能", $"【诱惑成功】目标【{targetMonster.Name}】已被驯服为您的随从！", new Color(0.3f, 1f, 0.4f));
                            }
                            else if (tameRes.Outcome == MageCombatEngine.Temptation.TameOutcome.Paralyzed)
                            {
                                targetMonster.IsFrozen = true;
                                HudInstance?.AddChat("技能", $"【心智迷离】目标【{targetMonster.Name}】受到强光定身麻痹 {tameRes.ParalyzeDurationSec} 秒！", new Color(1f, 0.9f, 0.2f));
                            }
                            else
                            {
                                HudInstance?.AddChat("技能", $"诱惑之光闪烁，【{targetMonster.Name}】抵抗了心智侵蚀。", new Color(0.8f, 0.8f, 0.8f));
                            }
                        }
                    }
                    SendCastSkillPacket(9, targetTile.X, targetTile.Y, _selectedTargetUid);
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

                case 13: // 灵魂火符 (Soul Fire Ball)
                    // 盛大官方 1.5 检定：必须佩戴护身符且剩余张数 >= 1 (持久 >= 100)
                    var talismanDura = HudInstance?.GetTalismanOrPoisonDura(TaoistCombatEngine.ReqTalismanDuraFireBall) ?? 1000;
                    var consumeRes = TaoistCombatEngine.TryConsumeTalisman(talismanDura, TaoistCombatEngine.ReqTalismanDuraFireBall);
                    if (!consumeRes.success)
                    {
                        HudInstance?.AddChat("提示", consumeRes.desc, new Color(1f, 0.4f, 0.4f));
                        return;
                    }
                    PlayerInstance.Anim.SetAction(ActionType.Cast, dir);
                    MagicManagerInstance?.PlayMagic(13, playerTile, targetTile);
                    SendCastSkillPacket(13, targetTile.X, targetTile.Y, _selectedTargetUid);
                    if (_isSandbox)
                    {
                        CheckSandboxSpellHit(13, targetTile);
                        HudInstance?.AddChat("法术", $"【灵魂火符】呼啸飞出！{consumeRes.desc}", new Color(1f, 0.8f, 0.2f));
                        AudioManager.Instance?.PlaySoundId(15);
                    }
                    break;

                case 22: // 地狱雷光 (HellFire)
                    PlayerInstance.Anim.SetAction(ActionType.Cast, dir);
                    AudioManager.Instance?.PlaySoundId(132);
                    if (_isSandbox)
                    {
                        int hitCount = 0;
                        int px = PlayerInstance.TileX;
                        int py = PlayerInstance.TileY;
                        foreach (var ent in EntityManagerInstance.AllEntities)
                        {
                            if (ent.Type == EntityManager.EntityType.Monster && !ent.IsDead)
                            {
                                int dist = Math.Max(Math.Abs(ent.TileX - px), Math.Abs(ent.TileY - py));
                                if (dist <= 2) // 周围 2 格范围圣雷震荡
                                {
                                    bool isUndead = ent.Name.Contains("僵尸") || ent.Name.Contains("骷髅") || ent.Name.Contains("尸王");
                                    int dmg = MageCombatEngine.HellFire.CalculateDamage(50, isUndead);
                                    EntityManagerInstance.TriggerStruck(ent.Uid, dmg, Math.Max(0, ent.Hp - dmg));
                                    hitCount++;
                                }
                            }
                        }
                        HudInstance?.AddChat("技能", $"地狱雷光轰然降临！强劲圣雷震荡波波及周围 {hitCount} 个目标！", new Color(0.4f, 0.9f, 1f));
                    }
                    SendCastSkillPacket(22, PlayerInstance.TileX, PlayerInstance.TileY);
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
                        if (_net.UseModernBinaryProtocol)
                        {
                            SendCastSkillPacket(26, PlayerInstance.TileX, PlayerInstance.TileY);
                        }
                        else
                        {
                            _net.SendPacket(PacketEncoder.FireHit(0, (short)dir));
                        }
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
                        if (_net.UseModernBinaryProtocol)
                        {
                            SendCastSkillPacket(25, PlayerInstance.TileX, PlayerInstance.TileY);
                        }
                        else
                        {
                            _net.SendPacket(PacketEncoder.Motaebo((short)dir));
                        }
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

        private ulong _lastPickupTick = 0;

        public void PerformPickup()
        {
            if (PlayerInstance == null || PlayerInstance.IsDead) return;

            ulong now = Time.GetTicksMsec();
            if (now - _lastPickupTick < 300) return; // 盛大官方 300ms 拾取防刷节流冷却 (ClMain.pas / IDA sub_46594C)
            _lastPickupTick = now;

            int px = PlayerInstance.TileX;
            int py = PlayerInstance.TileY;

            if (_net != null && _net.IsConnectedToServer)
            {
                // 盛大官方 1.5 格式对齐 (带 x\ty 坐标 Extra，完全闭环网关解包)
                _net.SendPacket(PacketEncoder.Pickup((short)px, (short)py));
            }
            else if (_isSandbox)
            {
                var items = GroundItemManagerInstance.GetItemsAt(px, py);
                if (items.Count > 0)
                {
                    var picked = items[0];
                    if (picked.Name.Contains("金币"))
                    {
                        GroundItemManagerInstance.RemoveItem(picked.ItemId);
                        PlayerInstance.Gold += Math.Max(1, picked.Amount);
                        HudInstance?.UpdateGold(PlayerInstance.Gold);
                        HudInstance?.AddChat("拾取", $"捡起金币：{picked.Amount} 金币。", new Color(1f, 0.9f, 0.3f));
                        AudioManager.Instance?.PlaySoundId(AudioManager.SoundId.Money); // 官方 s_money = 106
                    }
                    else
                    {
                        if (HudInstance != null && HudInstance.AddBagItem(picked.Name))
                        {
                            GroundItemManagerInstance.RemoveItem(picked.ItemId);
                            HudInstance.AddChat("拾取", $"捡起物品：【{picked.Name}】。", new Color(0.4f, 1f, 0.4f));
                            AudioManager.Instance?.PlaySoundId(AudioManager.SoundId.ItemClick); // 官方 s_itmclick = 118
                        }
                        else
                        {
                            HudInstance?.AddChat("提示", "你身上携带的物品太多，无法再拿了。", new Color(1f, 0.4f, 0.4f));
                        }
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

            // 2. 检索目标地块上的死者尸体 UID (对齐 bridge.rs:886 pkt.recog)
            int targetUid = 0;
            var corpses = EntityManagerInstance.GetEntitiesAt(tx, ty);
            for (int i = 0; i < corpses.Count; i++)
            {
                if (corpses[i].IsDead)
                {
                    targetUid = corpses[i].Uid;
                    break;
                }
            }

            // 3. 真实网络发包
            if (_net != null && _net.IsConnectedToServer)
            {
                _net.SendPacket(PacketEncoder.ButchItem(targetUid, (short)tx, (short)ty));
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

            // 驱动盛大官方 1.5 药水缓回 Tick 灌注 (仅单机沙盒模式模拟；联网模式完全由服务端 SM_HEALTHSPELLCHANGED 权威驱动)
            if (_isSandbox && PlayerInstance != null && !PlayerInstance.IsDead && HudInstance != null)
            {
                if (PotionRecoverySystem.ProcessTick(nowTick, out int hpTick, out int mpTick))
                {
                    if (hpTick > 0 && PlayerInstance.Hp < PlayerInstance.MaxHp)
                    {
                        PlayerInstance.Hp = Math.Min(PlayerInstance.MaxHp, PlayerInstance.Hp + hpTick);
                        HudInstance.UpdateHp(PlayerInstance.Hp, PlayerInstance.MaxHp);
                    }
                    if (mpTick > 0 && PlayerInstance.Mp < PlayerInstance.MaxMp)
                    {
                        PlayerInstance.Mp = Math.Min(PlayerInstance.MaxMp, PlayerInstance.Mp + mpTick);
                        HudInstance.UpdateMp(PlayerInstance.Mp, PlayerInstance.MaxMp);
                    }
                }
            }
        }

        public void SetNet(NetManager net, int sId, string uname, int ulevel = 35)
        {
            _net = net;
            ServerMessageRouterInstance = new ServerMessageRouter(HudInstance, EntityManagerInstance, this, GroundItemManagerInstance);
            _net.PacketReceived += ServerMessageRouterInstance.OnPacket;
            _net.BinaryPacketReceived += OnBinaryPacketReceived;
            SessionId = sId;
            Username = uname;
            if (PlayerInstance != null)
            {
                PlayerInstance.CharacterName = uname;
                PlayerInstance.Level = ulevel;
                PlayerInstance.UpdateNameLabelText();
            }
        }

        /// <summary>
        /// 处理现代紧凑二进制服务端权威状态同步 (反外挂强制拉回、权威血量同步等)
        /// </summary>
        public void OnBinaryPacketReceived(ushort msgId, byte[] payload)
        {
            switch ((BinaryProtocol.MsgType)msgId)
            {
                case BinaryProtocol.MsgType.SyncPosition:
                    if (payload.Length >= 6)
                    {
                        var sync = BinaryProtocol.PositionSyncPayload.Read(payload);
                        if (sync.ForcePullback == 1)
                        {
                            // 权威拉回！主角如果发生越界、加速或穿墙，立即强行修正并停止当前步伐
                            PlayerInstance?.SetTilePos(sync.X, sync.Y);
                            HudInstance?.AddChat("系统", $"【安全防御】坐标异常已被服务端权威拉回 -> ({sync.X}, {sync.Y})", new Color(1f, 0.2f, 0.2f));
                        }
                        else
                        {
                            PlayerInstance?.SetTilePos(sync.X, sync.Y);
                        }
                    }
                    break;

                case BinaryProtocol.MsgType.BroadcastHealthSync:
                    if (payload.Length >= 8)
                    {
                        var hpSync = BinaryProtocol.HealthSyncPayload.Read(payload);
                        if (PlayerInstance != null)
                        {
                            PlayerInstance.Hp = hpSync.Hp;
                            PlayerInstance.Mp = hpSync.Mp;
                            PlayerInstance.MaxHp = hpSync.MaxHp;
                            PlayerInstance.MaxMp = hpSync.MaxMp;
                            HudInstance?.UpdateHp(hpSync.Hp, hpSync.MaxHp);
                            HudInstance?.UpdateMp(hpSync.Mp, hpSync.MaxMp);
                        }
                    }
                    break;
            }
        }

        private void HandleEntityInteraction(int targetUid, EntityManager.EntityType type, int tileX, int tileY, bool isShift)
        {
            if (PlayerInstance == null || PlayerInstance.IsDead) return;
            var ent = EntityManagerInstance.GetEntity(targetUid);
            if (ent == null) return;

            // 1. NPC 交互：向服务端发送点击 NPC (CM_CLICKNPC) 并呼出 NPC 对话
            if (type == EntityManager.EntityType.Npc)
            {
                if (_net != null && _net.IsConnectedToServer)
                {
                    _net.SendPacket(PacketEncoder.ClickNpc(ent.Name));
                }
                else if (_isSandbox && HudInstance?.NpcDialog != null)
                {
                    HudInstance.NpcDialog.CurrentNpcUid = ent.Uid;
                    HudInstance.ShowNpcDialog($"@{ent.Name}\n欢迎来到热血传奇世界！请问有什么可以帮助您的？\n<购买物品/@buy>\n<出售物品/@sell>\n<修理装备/@repair>\n<离开/@exit>");
                }
                GD.Print($"[GameView] 交互 NPC: {ent.Name} (Uid: {targetUid})");
            }
            // 2. 玩家交互：查询玩家名与他人装备 (官方实证 0x463649: Shift+双击 CM 82; 普通点击 CM 80)
            else if (type == EntityManager.EntityType.Player)
            {
                long now = (long)Time.GetTicksMsec();
                if (_queryUserThrottle.TryGetValue(targetUid, out long lastTick) && now - lastTick < QueryUserDebounceMs)
                {
                    GD.Print($"[GameView] 查名节流防抖拦截: Uid {targetUid} (距上次发包 {now - lastTick}ms < {QueryUserDebounceMs}ms)");
                    return;
                }
                _queryUserThrottle[targetUid] = now;

                if (_net != null && _net.IsConnectedToServer)
                {
                    byte[] pkt = isShift
                        ? PacketEncoder.QueryUsernameExt(targetUid)
                        : PacketEncoder.QueryUsername(targetUid);
                    _net.SendPacket(pkt);
                }
                else if (_isSandbox && HudInstance != null)
                {
                    HudInstance.OpenInspect(ent.Name);
                }
                GD.Print($"[GameView] 交互玩家: {ent.Name} (Uid: {targetUid}, isShift: {isShift})");
            }
            // 3. 怪物交互：锁定目标
            else if (type == EntityManager.EntityType.Monster)
            {
                GD.Print($"[GameView] 锁定怪物目标: {ent.Name} (Uid: {targetUid})");
            }
        }

        /// <summary>
        /// 盛大官方 1.5 主角阵亡全流程闭环 (IDA sub_481DD4 / sub_486DF0 & DeathScreenView 灰屏与确认框)
        /// </summary>
        public void TriggerPlayerDeath()
        {
            if (PlayerInstance == null || PlayerInstance.IsDead) return;

            PotionRecoverySystem.Reset();
            PlayerInstance.ApplyDie();
            PlayerStateInstance.Hp = 0;
            HudInstance?.UpdateHp(0, PlayerStateInstance.MaxHp);
            HudInstance?.DeathScreen?.ShowDeath();
            HudInstance?.AddChat("提示", "你已经阵亡了，是否想要重新开始？", new Color(1f, 0.2f, 0.2f));
        }

        /// <summary>
        /// 盛大官方 1.5 服务端复活应答落地 (SM_REVIVE 501: 恢复血蓝、重置坐标、解除灰屏)
        /// </summary>
        public void OnServerRevive(int x, int y)
        {
            if (PlayerInstance == null) return;
            if (HudInstance?.DeathScreen != null)
            {
                HudInstance.DeathScreen.Visible = false;
            }

            PlayerStateInstance.Hp = PlayerStateInstance.MaxHp;
            PlayerStateInstance.Mp = PlayerStateInstance.MaxMp;
            HudInstance?.UpdateHp(PlayerStateInstance.Hp, PlayerStateInstance.MaxHp);
            HudInstance?.UpdateMp(PlayerStateInstance.Mp, PlayerStateInstance.MaxMp);
            PlayerInstance.ApplyRevive(PlayerStateInstance.MaxHp, PlayerStateInstance.MaxMp);
            PlayerInstance.Direction = 0;
            PlayerInstance.SetTilePos(x, y);
            UpdateEnvironmentLighting();
            HudInstance?.AddChat("复活", $"[复活] 你已在安全区 ({x}, {y}) 重获新生！", new Color(0.3f, 1f, 0.4f));
        }

        /// <summary>
        /// 盛大官方 1.5 主角复活全流程闭环 (单机沙盒恢复满血满蓝传送安全区，联机发送复活发包 CM_REVIVE 1018)
        /// </summary>
        public void PerformPlayerRevive()
        {
            if (PlayerInstance == null) return;
            if (HudInstance?.DeathScreen != null)
            {
                HudInstance.DeathScreen.Visible = false;
            }

            if (_isSandbox)
            {
                OnServerRevive(330, 270);
            }
            else
            {
                // 官方原版点击重新开始必须向服务端请求复活 CM_REVIVE (1018, 对齐 bridge.rs:63)
                _net?.SendPacket(PacketEncoder.Revive(0));
            }
        }
    }
}
