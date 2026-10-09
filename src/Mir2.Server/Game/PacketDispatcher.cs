using System;
using System.Collections.Generic;
using System.Text;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2.Shared.Protocols.Modern;
using Mir2.Shared.Rules;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.5 原版网络封包路由与分发中心 (M2 PacketDispatcher)
/// 严格 1:1 处理官方 CM_ 上行协议，并驱动服务端物理世界与九宫格 AOI 广播
/// </summary>
public sealed class PacketDispatcher
{
    private readonly MapManager _mapManager;
    private readonly List<ClientSession> _ninePatchBuffer = new(64);
    public MonsterRegenEngine? RegenEngine { get; set; }
    public NpcMerchantEngine? NpcEngine { get; set; }
    public TradeEngine? TradeEngine { get; set; }
    public GroupEngine GroupEngine { get; set; } = new();
    public GuildEngine GuildEngine { get; set; } = new();
    private int _nextGroundItemUid = 10000;

    public PacketDispatcher(MapManager mapManager, MonsterRegenEngine? regenEngine = null, NpcMerchantEngine? npcEngine = null, TradeEngine? tradeEngine = null, GroupEngine? groupEngine = null, GuildEngine? guildEngine = null)
    {
        _mapManager = mapManager;
        RegenEngine = regenEngine;
        NpcEngine = npcEngine;
        TradeEngine = tradeEngine;
        if (groupEngine != null) GroupEngine = groupEngine;
        if (guildEngine != null) GuildEngine = guildEngine;
    }

    public PacketDispatcher(NinePatchAoi<ClientSession> mapAoi)
    {
        _mapManager = new MapManager();
        _mapManager.RegisterMap(new GameMap(0, "比奇省", mapAoi.Width, mapAoi.Height));
    }

    /// <summary>
    /// 处理客户端发来的单条原始封包 (格式: #IDENT/RECOG/PARAM/TAG/SERIES/EXTRA!)
    /// 兼容传统 ASCII 协议与二进制 6-Bit 混合帧
    /// </summary>
    public void Dispatch(ClientSession session, string rawPacket)
    {
        if (string.IsNullOrEmpty(rawPacket) || rawPacket.Length < 3) return;

        // 去除首尾 '#' 与 '!'
        ReadOnlySpan<char> span = rawPacket.AsSpan();
        if (span[0] == '#') span = span[1..];
        if (span.Length > 0 && span[^1] == '!') span = span[..^1];

        // 尝试按斜杠分词
        int slashIdx = span.IndexOf('/');
        if (slashIdx < 0) return;

        if (!short.TryParse(span[..slashIdx], out short ident)) return;

        ReadOnlySpan<char> remaining = span[(slashIdx + 1)..];

        switch (ident)
        {
            case ProtocolConsts.CM_QUERYVERSION: // 2001 客户端查询版本 / 握手
                HandleQueryVersion(session);
                break;

            case ProtocolConsts.CM_LOGIN: // 2 账号密码登录
                HandleLogin(session, remaining);
                break;

            case ProtocolConsts.CM_QUERYCHRINFO: // 119 查询角色列表
                HandleQueryCharInfo(session);
                break;

            case ProtocolConsts.CM_SELECTCHAR: // 103 选择角色进入游戏
                HandleSelectChar(session, remaining);
                break;

            case ProtocolConsts.CM_TURNDIRECTION: // 3010 转向
                HandleTurn(session, remaining);
                break;

            case ProtocolConsts.CM_WALK: // 3011 行走 (1格)
                HandleMove(session, remaining, isRun: false);
                break;

            case ProtocolConsts.CM_RUN: // 3013 跑步 (2格)
                HandleMove(session, remaining, isRun: true);
                break;

            case ProtocolConsts.CM_HIT: // 3014 物理攻击
                HandleHit(session, remaining);
                break;

            case ProtocolConsts.CM_SPELL: // 3017 技能施法
                HandleSpell(session, remaining);
                break;

            case ProtocolConsts.CM_SAY: // 3030 聊天通道
                HandleSay(session, remaining);
                break;

            case ProtocolConsts.CM_PICKUP: // 1001 拾取地表物品 (IDA 0x4C5CB0)
                HandlePickUp(session);
                break;

            case ProtocolConsts.CM_MERCHANTHOMEPAGE: // 1010 点击NPC打开对话框主页
                HandleNpcHomePage(session, remaining);
                break;

            case ProtocolConsts.CM_NPCSELECT: // 1011 点击NPC对话选项标签
                HandleNpcSelect(session, remaining);
                break;

            case ProtocolConsts.CM_USERBUYITEM: // 45 / 1014 购买商品
            case ProtocolConsts.CM_USERBUYITEM_ALT:
                HandleBuyItem(session, remaining);
                break;

            case ProtocolConsts.CM_USERSELLITEM: // 46 / 1013 出售物品
            case ProtocolConsts.CM_USERSELLITEM_ALT:
                HandleSellItem(session, remaining);
                break;

            case ProtocolConsts.CM_REPAIRSPECIAL: // 48 铁匠铺特修
                HandleSpecialRepair(session, remaining);
                break;

            case ProtocolConsts.CM_DEALTRY: // 1025 发起面对面交易邀请
                HandleDealTry(session, remaining);
                break;

            case ProtocolConsts.CM_DEALADDITEM: // 1026 放入交易物品
                HandleDealAddItem(session, remaining);
                break;

            case ProtocolConsts.CM_DEALDELITEM: // 1027 撤下交易物品
                HandleDealDelItem(session, remaining);
                break;

            case ProtocolConsts.CM_DEALCHGGOLD: // 1029 修改交易金币
                HandleDealChgGold(session, remaining);
                break;

            case ProtocolConsts.CM_DEALEND: // 1030 确认锁定/执行交易
                HandleDealEnd(session);
                break;

            case 1028: // 取消交易
                HandleDealCancel(session);
                break;

            case ProtocolConsts.CM_USERSTORAGEDEPOSIT: // 1031 存入仓库物品
            case 1015: // CM_STORAGE 官方 1.4/1.5
                HandleStorageDeposit(session, remaining);
                break;

            case ProtocolConsts.CM_USERSTORAGEWITHDRAW: // 1032 取回仓库物品
                HandleStorageWithdraw(session, remaining);
                break;

            case 1017: // CM_SAVEGOLD 官方存入金币
                HandleStorageSaveGold(session, remaining);
                break;

            case ProtocolConsts.CM_REVIVE: // 1018 官方死亡复活/重新开始 (IDA 0x481efc, bridge.rs:63)
                if (session.State != null && session.State.Hp <= 0)
                {
                    HandleRevive(session, remaining);
                }
                else
                {
                    HandleStorageTakeGold(session, remaining);
                }
                break;

            case ProtocolConsts.CM_CHECKPASSWORD: // 1046 官方二级密码校验
                HandleStoragePassword(session, remaining);
                break;

            case ProtocolConsts.CM_EATITEM: // 1006 使用物品/吃药
                HandleEatItem(session, remaining);
                break;

            case ProtocolConsts.CM_OPENDOOR: // 1002 官方打开石门
                HandleOpenDoorPacket(session, remaining);
                break;

            case ProtocolConsts.CM_WEARITEM: // 1003 官方穿戴装备 (负重与腕力权威校验)
                HandleWearItemPacket(session, remaining);
                break;

            case ProtocolConsts.CM_TAKEOFFITEM: // 1004 / 1005 官方脱下装备 (背包容量与负重权威校验)
            case 1005:
                HandleTakeOffItemPacket(session, remaining);
                break;

            case ProtocolConsts.CM_DROPITEM: // 1000 官方丢弃物品到地面
                HandleDropItemPacket(session, remaining);
                break;

            case ProtocolConsts.CM_DROPGOLD: // 1016 官方丢弃金币到地面
                HandleDropGoldPacket(session, remaining);
                break;

            case ProtocolConsts.CM_ADJUST_BONUS: // 1043 官方属性加点
                HandleAdjustBonusPacket(session, remaining);
                break;

            case ProtocolConsts.CM_BUTCHITEM: // 1007 官方生物尸体割肉 (Alt+点击)
                HandleButchItemPacket(session, remaining);
                break;

            case ProtocolConsts.CM_GROUPMODE: // 1019 组队模式切换
                HandleGroupMode(session, remaining);
                break;

            case ProtocolConsts.CM_CREATEGROUP: // 1020 创建队伍
                HandleCreateGroup(session, remaining);
                break;

            case ProtocolConsts.CM_ADDGROUPMEMBER: // 1021 邀请队员
                HandleAddGroupMember(session, remaining);
                break;

            case ProtocolConsts.CM_DELGROUPMEMBER: // 1022 踢出/退出队伍
                HandleDelGroupMember(session, remaining);
                break;

            case ProtocolConsts.CM_CREATEGUILD: // 1042 沃玛号角创建行会
                HandleCreateGuild(session, remaining);
                break;

            case ProtocolConsts.CM_GUILDOPEN: // 1035 打开行会面板
            case ProtocolConsts.CM_GUILD_MEMBER_LIST: // 1037
                HandleOpenGuild(session);
                break;

            case ProtocolConsts.CM_GUILDADDMEMBER: // 1038 招收行会成员
                HandleGuildAddMember(session, remaining);
                break;

            case ProtocolConsts.CM_GUILD_KICK_MEMBER: // 1039 开除行会成员
                HandleGuildKickMember(session, remaining);
                break;

            case ProtocolConsts.CM_GUILD_NOTICE_UPDATE: // 1040 发布行会公告
                HandleGuildUpdateNotice(session, remaining);
                break;

            case ProtocolConsts.CM_BIGHIT: // 3015 挥镐挖矿 (手持矿镐)
                HandleMineHit(session, remaining);
                break;

            case ProtocolConsts.CM_HEAVYHIT: // 3016 刺杀剑术隔位破防刺杀
            case ProtocolConsts.CM_LONGHIT: // 3019 刺杀剑气穿透
                HandleHeavyHit(session, remaining);
                break;

            case ProtocolConsts.CM_NEWCHAR: // 104 创建角色
                HandleNewChar(session, remaining);
                break;

            case ProtocolConsts.CM_DELCHAR: // 105 删除角色
                HandleDelChar(session, remaining);
                break;

            case ProtocolConsts.CM_SOFTCLOSE: // 1009 / 106 正常登出
            case ProtocolConsts.CM_LOGOUT: // 3001
                HandleLogout(session);
                break;

            case ProtocolConsts.CM_SITDOWN: // 3012 休息打坐
                HandleSitDown(session);
                break;

            default:
                // 未知或扩展协议默认回显记录
                break;
        }
    }

    private void HandleQueryVersion(ClientSession session)
    {
        // 官方 1.5 握手认证通过: SM_PASSOK (500)
        session.EnqueuePacket(ProtocolConsts.SM_PASSOK, session.SessionId, 0, 0, 0, "盛大官方1.5原版引擎就绪");
    }

    private void HandleLogin(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: 账号/密码
        int slash = payload.IndexOf('/');
        string account = slash > 0 ? payload[..slash].ToString() : payload.ToString();
        session.AccountName = account;

        // 回复登录成功: SM_LOGINSUCCESS (530)
        session.EnqueuePacket(ProtocolConsts.SM_LOGINSUCCESS, session.SessionId, 0, 0, 0, account);
    }

    private void HandleQueryCharInfo(ClientSession session)
    {
        // 默认模拟返回 1 个 1.5 原版角色: 战士, 等级 35, 男
        string charInfo = $"{session.AccountName}_战神/0/35/0/0/0";
        session.EnqueuePacket(ProtocolConsts.SM_QUERYCHAR_OK, 1, 0, 0, 0, charInfo);
    }

    private void HandleSelectChar(ClientSession session, ReadOnlySpan<char> payload)
    {
        string name = payload.ToString();
        int spawnX = 330;
        int spawnY = 270;

        int slash1 = payload.IndexOf('/');
        if (slash1 > 0)
        {
            name = payload[..slash1].ToString();
            ReadOnlySpan<char> rest = payload[(slash1 + 1)..];
            int slash2 = rest.IndexOf('/');
            if (slash2 > 0 && int.TryParse(rest[..slash2], out int px) && int.TryParse(rest[(slash2 + 1)..], out int py))
            {
                spawnX = px;
                spawnY = py;
            }
        }

        session.CharacterName = name;
        session.CharacterName = name;
        session.CurrentMapId = 0; // 比奇省
        session.X = spawnX;
        session.Y = spawnY;
        session.State ??= new PlayerState();

        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;

        // 1. 注册到当前地图 AOI 空间九宫格
        map.Aoi.Enter(session, session.X, session.Y);

        // 2. 下发官方进入游戏核心响应: SM_LOGON_DELPHI (50)
        session.EnqueuePacket(ProtocolConsts.SM_LOGON_DELPHI, session.SessionId, session.X, session.Y, 0, session.CharacterName);

        // 3. 下发地图切换: SM_NEWMAP_DELPHI (521) / 比奇省 0, 坐标 (330, 270)
        session.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, map.MapId, session.X, session.Y, 0, map.Name);

        // 4. 下发自身属性面板: SM_MYSTATUS (52)
        session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);

        // 5. 在九宫格内向其他玩家广播该玩家登场生成: SM_SENDUSERSTATE (250)
        BroadcastToNinePatch(session, ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, session.X, session.Y, 0, session.CharacterName, includeSelf: false);
    }

    private void HandleTurn(ClientSession session, ReadOnlySpan<char> payload)
    {
        // payload 包含朝向 (0..7)
        if (int.TryParse(payload, out int dir))
        {
            // 向九宫格广播转向: SM_SENDUSERSTATE (250)
            BroadcastToNinePatch(session, ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, session.X, session.Y, dir, session.CharacterName, includeSelf: false);
        }
    }

    private void HandleMove(ClientSession session, ReadOnlySpan<char> payload, bool isRun)
    {
        // 格式: 目标X/目标Y/朝向
        int slash1 = payload.IndexOf('/');
        if (slash1 < 0) return;
        ReadOnlySpan<char> xSpan = payload[..slash1];
        ReadOnlySpan<char> rest = payload[(slash1 + 1)..];
        int slash2 = rest.IndexOf('/');
        if (slash2 < 0) return;
        ReadOnlySpan<char> ySpan = rest[..slash2];
        ReadOnlySpan<char> dirSpan = rest[(slash2 + 1)..];
        int slash3 = dirSpan.IndexOf('/');
        if (slash3 >= 0)
        {
            dirSpan = dirSpan[..slash3];
        }

        if (int.TryParse(xSpan, out int newX) &&
            int.TryParse(ySpan, out int newY) &&
            int.TryParse(dirSpan, out int dir))
        {
            int oldX = session.X;
            int oldY = session.Y;

            var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;

            // 1. 地图物理边界与碰撞阻挡判定 (IsWalkable 包含超界越境与水面/城墙障碍检测)
            if (!map.IsWalkable(newX, newY))
            {
                // 撞墙或穿墙外挂，立即下发坐标拉回包
                session.EnqueuePacket(ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, oldX, oldY, dir, session.CharacterName);
                return;
            }

            // 2. 步长与防瞬移物理校验 (行走限 1 格，跑步限 2 格)
            int maxDist = isRun ? 2 : 1;
            int deltaX = Math.Abs(newX - oldX);
            int deltaY = Math.Abs(newY - oldY);
            if (deltaX > maxDist || deltaY > maxDist)
            {
                // 非法瞬移拉回当前坐标: SM_SENDUSERSTATE (250)
                session.EnqueuePacket(ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, oldX, oldY, dir, session.CharacterName);
                return;
            }

            // 3. 移动速度反外挂检测 (最小间隔校验)
            long nowMs = Environment.TickCount64;
            if (session.LastMoveTimeMs > 0)
            {
                var (isSpeedHack, shouldRollback) = EconomySecurityEngine.SpeedHackDetectorEngine.CheckMovePacket(nowMs, session.LastMoveTimeMs);
                if (isSpeedHack && shouldRollback)
                {
                    // 移动过快判定外挂拉回
                    session.EnqueuePacket(ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, oldX, oldY, dir, session.CharacterName);
                    return;
                }
            }
            session.LastMoveTimeMs = nowMs;

            // 更新实体坐标
            session.X = newX;
            session.Y = newY;

            // 更新九宫格 AOI 空间分区
            map.Aoi.UpdatePosition(session, oldX, oldY, newX, newY, out _, out _);

            // 广播移动数据帧: 行走 SM_OBJECTWALK (241) / 跑步 SM_OBJECTRUN (240)
            short smCode = isRun ? ProtocolConsts.SM_OBJECTRUN : ProtocolConsts.SM_OBJECTWALK;
            BroadcastToNinePatch(session, smCode, session.SessionId, newX, newY, dir, session.CharacterName, includeSelf: false);

            // 踏入火墙踩火权威伤害结算 (IDA sub_4C7720 / MageCombatEngine.Firewall)
            var fires = map.GetFireWallsAt(newX, newY);
            if (fires.Count > 0 && session.State != null && session.State.Hp > 0)
            {
                int baseFireDmg = fires[0].Damage;
                int totalFireDmg = MageCombatEngine.Firewall.CalculateTileDamage(baseFireDmg, fires.Count);
                var (finalHp, finalMp, died, _, _, _) = session.State.TakeCombatDamageFull(totalFireDmg, nowMs);
                BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, session.SessionId, newX, newY, totalFireDmg, session.CharacterName, includeSelf: true);
                session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
                if (died)
                {
                    BroadcastToNinePatch(session, ProtocolConsts.SM_DEATH_ACTION_DELPHI, session.SessionId, newX, newY, 0, session.CharacterName, includeSelf: true);
                }
            }

            // 4. 盛大官方跳点传送权威检测 (Transfer Passage)
            var passage = _mapManager.FindPassage(session.CurrentMapId, newX, newY);
            if (passage != null)
            {
                TriggerMapPassage(session, passage);
                return;
            }
        }
    }

    private void HandleHit(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: 目标X/目标Y/朝向 (CM_HIT)
        int slash1 = payload.IndexOf('/');
        if (slash1 < 0) return;
        ReadOnlySpan<char> xSpan = payload[..slash1];
        ReadOnlySpan<char> rest = payload[(slash1 + 1)..];
        int slash2 = rest.IndexOf('/');
        if (slash2 < 0) return;
        ReadOnlySpan<char> ySpan = rest[..slash2];
        ReadOnlySpan<char> dirSpan = rest[(slash2 + 1)..];
        int slash3 = dirSpan.IndexOf('/');
        if (slash3 >= 0) dirSpan = dirSpan[..slash3];

        if (int.TryParse(xSpan, out int targetX) &&
            int.TryParse(ySpan, out int targetY) &&
            int.TryParse(dirSpan, out int dir))
        {
            long nowMs = Environment.TickCount64;
            // 攻击速度校验 (间隔必须 >= 680ms)
            if (session.LastAttackTimeMs > 0 && !EconomySecurityEngine.AttackSpeedValidatorEngine.IsAttackValid(nowMs, session.LastAttackTimeMs))
            {
                return; // 挥刀过快拦截
            }
            session.LastAttackTimeMs = nowMs;

            // 1. 向九宫格广播普通物理挥刀动作: SM_OBJECTHIT (242)
            BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTHIT, session.SessionId, session.X, session.Y, dir, session.CharacterName, includeSelf: false);

            // 2. 武器开刀试刀判定
            session.State?.TrialSlashEquippedWeapon();

            // 3. 在九宫格同屏检索攻击目标 (坐标处于 targetX, targetY)
            ClientSession? defender = null;
            var hitMap = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
            lock (_ninePatchBuffer)
            {
                hitMap.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
                for (int i = 0; i < _ninePatchBuffer.Count; i++)
                {
                    var ent = _ninePatchBuffer[i];
                    if (ent.SessionId != session.SessionId && ent.X == targetX && ent.Y == targetY)
                    {
                        defender = ent;
                        break;
                    }
                }
            }

            if (defender != null && defender.State != null && session.State != null)
            {
                // 1. 官方 PK 模式规则判定 (0x4D1E54 btAttackMode)
                // 模式 1: [Mode: Peaceful] 和平模式，禁止攻击白名玩家
                if (session.State.AttackMode == 1 && !defender.State.IsRedName && !defender.State.IsYellowName)
                {
                    return; // 和平模式受限
                }

                int dist = Math.Max(Math.Abs(defender.X - session.X), Math.Abs(defender.Y - session.Y));
                var (rawDmg, hit, isArmorPiercing, isFireHit, died) = ResolveAuthoritativePhysicalCombat(session, defender, dist, nowMs);

                if (hit && rawDmg > 0)
                {
                    // 受击后仰广播: SM_OBJECTSTRUCK (244)
                    BroadcastToNinePatch(defender, ProtocolConsts.SM_OBJECTSTRUCK, defender.SessionId, defender.X, defender.Y, rawDmg, defender.CharacterName, includeSelf: true);

                    // 目标血量更新
                    defender.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, defender.State.Hp, defender.State.MaxHp, defender.State.Mp, defender.State.MaxMp);

                    // 死亡倒地动画广播: SM_DEATH_ACTION_DELPHI (28)
                    if (died)
                    {
                        session.State?.AddPkMurder();
                        BroadcastToNinePatch(defender, ProtocolConsts.SM_DEATH_ACTION_DELPHI, defender.SessionId, defender.X, defender.Y, 0, defender.CharacterName, includeSelf: true);

                        // 官方死亡爆装与爆背包结算 (IDA 0x4BB44C / 0x4BB6C8)
                        var dropped = MonsterDropEngine.ExecuteDeathDrop(defender.State, OfficialClientRulesEngine.DelphiRandom.Shared);
                        for (int d = 0; d < dropped.Count; d++)
                        {
                            var drop = dropped[d];
                            hitMap.AddGroundItem(new GroundItemEntity
                            {
                                Uid = drop.ItemUid,
                                Name = drop.ItemName,
                                X = defender.X,
                                Y = defender.Y,
                                OwnerSessionId = session.SessionId, // 杀手独享 80 秒拾取保护权 (IDA 0x4C5CB0)
                                DropTickMs = nowMs,
                                Weight = 1
                            });
                            BroadcastToNinePatch(defender, ProtocolConsts.SM_GROUNDITEM, drop.ItemUid, defender.X, defender.Y, 0, drop.ItemName, includeSelf: true);
                        }
                    }
                }
            }
            else if (RegenEngine != null && session.State != null)
            {
                // 4. 检索目标位置的怪物并进行物理攻击与掉落结算
                var monster = RegenEngine.FindMonsterAt(session.CurrentMapId, targetX, targetY);
                if (monster != null && !monster.IsDead)
                {
                    int dist = Math.Max(Math.Abs(monster.X - session.X), Math.Abs(monster.Y - session.Y));
                    var (rawDmg, hit, isFireHit, died) = ResolveAuthoritativeMonsterPhysicalCombat(session, monster, dist, nowMs);
                    if (hit && rawDmg > 0)
                    {
                        // 广播怪物受击后仰: SM_OBJECTSTRUCK (244)
                        BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, monster.Uid, monster.X, monster.Y, rawDmg, monster.Name, includeSelf: true);

                        if (died)
                        {
                            RegenEngine.OnMonsterDied(monster.Uid);

                            // 广播怪物死亡倒地: SM_DEATH_ACTION_DELPHI (28)
                            BroadcastToNinePatch(session, ProtocolConsts.SM_DEATH_ACTION_DELPHI, monster.Uid, monster.X, monster.Y, 0, monster.Name, includeSelf: true);

                            // 怪物死亡爆落战利品
                            var loot = new GroundItemEntity
                            {
                                Uid = 50000 + monster.Uid % 10000,
                                Name = $"{monster.Name}_战利品",
                                X = monster.X,
                                Y = monster.Y,
                                OwnerSessionId = session.SessionId, // 击杀者 80 秒归属权
                                DropTickMs = nowMs,
                                Weight = 1
                            };
                            hitMap.AddGroundItem(loot);
                            BroadcastToNinePatch(session, ProtocolConsts.SM_GROUNDITEM, loot.Uid, monster.X, monster.Y, 0, loot.Name, includeSelf: true);
                            OnMonsterKilledByPlayer(session, monster);
                        }
                    }
                }
            }
        }
    }

    private void HandleSpell(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null) return;

        // 格式: targetUid/magicId/targetX/targetY (CM_SPELL 3017)
        int slash1 = payload.IndexOf('/');
        if (slash1 < 0) return;
        ReadOnlySpan<char> uidSpan = payload[..slash1];
        ReadOnlySpan<char> rest1 = payload[(slash1 + 1)..];

        int slash2 = rest1.IndexOf('/');
        if (slash2 < 0) return;
        ReadOnlySpan<char> magicSpan = rest1[..slash2];
        ReadOnlySpan<char> rest2 = rest1[(slash2 + 1)..];

        int slash3 = rest2.IndexOf('/');
        if (slash3 < 0) return;
        ReadOnlySpan<char> xSpan = rest2[..slash3];
        ReadOnlySpan<char> ySpan = rest2[(slash3 + 1)..];
        int slash4 = ySpan.IndexOf('/');
        if (slash4 >= 0)
        {
            ySpan = ySpan[..slash4];
        }

        if (int.TryParse(uidSpan, out int targetUid) &&
            short.TryParse(magicSpan, out short magicId) &&
            int.TryParse(xSpan, out int targetX) &&
            int.TryParse(ySpan, out int targetY))
        {
            long nowMs = Environment.TickCount64;
            // 施法公共CD校验 (500ms)
            if (session.LastSpellTimeMs > 0 && (nowMs - session.LastSpellTimeMs < 500))
            {
                return;
            }
            session.LastSpellTimeMs = nowMs;

            var skill = SkillFormulas.GetSkill(magicId);
            int mpCost = skill?.MpCost ?? 10;
            if (session.State.Mp < mpCost)
            {
                return; // 蓝量不足
            }

            // 施法耗材权威检验 (道士药粉与护身符)
            if (!CheckAndConsumeSpellReagent(session, magicId))
            {
                return;
            }

            session.State.Mp -= mpCost;
            session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);

            // 向九宫格广播施法动作: SM_OBJECTSPELL (243)
            BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSPELL, session.SessionId, session.X, session.Y, magicId, session.CharacterName, includeSelf: true);

            // 权威法术与特技解析核心 (含烈火凝聚、刺杀开关、抗拒火环、诱惑之光、地狱雷光、施毒术、治愈术)
            ResolveAuthoritativeSpell(session, magicId, targetUid, targetX, targetY, nowMs);
        }
    }

    private void HandleSay(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 兼容带包头参数格式 (recog/param/tag/series/msg) 或纯消息文本
        ReadOnlySpan<char> text = payload;
        int lastSlash = payload.LastIndexOf('/');
        if (lastSlash >= 0 && lastSlash + 1 < payload.Length)
        {
            text = payload[(lastSlash + 1)..];
        }

        // 1. 官方控制指令判断 (IDA 0x4D1E54 TPlayObject.Command)
        if (text.Equals("@AttackMode", StringComparison.OrdinalIgnoreCase))
        {
            if (session.State != null)
            {
                session.State.AttackMode = (byte)(session.State.AttackMode >= 4 ? 0 : session.State.AttackMode + 1);
                string modeStr = session.State.AttackMode switch
                {
                    0 => "[Mode: Attack All]",
                    1 => "[Mode: Peaceful]",
                    2 => "[Mode: Group Attack]",
                    3 => "[Mode: Guild Attack]",
                    4 => "[Mode: Red/White Name]",
                    _ => "[Mode: Attack All]"
                };
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, modeStr);
            }
            return;
        }

        // 2. 盛大官方特戒传送指令 (IDA 0x4D1E54 / SpecialRingEngine.TeleportRing)
        if (text.StartsWith("@move", StringComparison.OrdinalIgnoreCase))
        {
            var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
            var (valid, tx, ty, err) = SpecialRingEngine.TeleportRing.ParseCommand(text.ToString(), map.Width, map.Height, (x, y) => !map.IsWalkable(x, y));
            if (valid)
            {
                int oldX = session.X;
                int oldY = session.Y;
                session.X = tx;
                session.Y = ty;
                map.Aoi.UpdatePosition(session, oldX, oldY, tx, ty, out _, out _);

                // 强制同步玩家坐标与拉回
                SendPositionPullback(session, tx, ty, session.Direction);
                BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTRUN, session.SessionId, tx, ty, session.Direction, session.CharacterName, includeSelf: false);
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"【传送戒指】瞬移至 ({tx}, {ty}) 成功！");
            }
            else
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"传送失败：{err}");
            }
            return;
        }

        // 3. 普通聊天广播到周围九宫格
        string msg = $"{session.CharacterName}: {payload}";
        BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSAY, session.SessionId, session.X, session.Y, 0, msg, includeSelf: true);
    }

    /// <summary>
    /// 盛大官方 1.5 死亡复活权威裁决 (IDA 0x481EFC / bridge.rs:63 CM_REVIVE 1018)
    /// </summary>
    public void HandleRevive(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null || session.State.Hp > 0) return;

        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
        int oldX = session.X;
        int oldY = session.Y;

        // 1. 恢复状态与满血满魔
        session.State.Hp = session.State.MaxHp;
        session.State.Mp = session.State.MaxMp;
        session.State.HasGreenPoison = false;
        session.State.HasRedPoison = false;
        session.State.IncHealth = 0;
        session.State.IncSpell = 0;

        // 2. 传送回安全区 (比奇省银杏山谷 330, 270)
        int respawnX = 330;
        int respawnY = 270;
        session.X = respawnX;
        session.Y = respawnY;

        // 3. 更新九宫格 AOI 空间索引
        map.Aoi.UpdatePosition(session, oldX, oldY, respawnX, respawnY, out _, out _);

        // 4. 下发官方复活应答: SM_REVIVE (501)
        session.EnqueuePacket(ProtocolConsts.SM_REVIVE, session.SessionId, respawnX, respawnY, 0, session.CharacterName);

        // 5. 刷新玩家自身血蓝与状态: SM_MYSTATUS (504)
        session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);

        // 6. 广播新坐标正常站立状态给周围九宫格玩家: SM_SENDUSERSTATE (503)
        BroadcastToNinePatch(session, ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, respawnX, respawnY, session.Direction, session.CharacterName, includeSelf: false);
    }

    /// <summary>
    /// 盛大官方 1.4/1.5 拾取地表物品权威逻辑 (IDA 0x4C5CB0 TCreature.PickUp)
    /// 包含死亡状态校验、80秒归属保护期、背包40格满仓检测、负重上限校验
    /// </summary>
    private void HandlePickUp(ClientSession session)
    {
        if (session.State == null || session.State.Hp <= 0) return; // 死亡不可拾取 (IDA 0x4C5CB0 @ L889)

        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
        long nowMs = Environment.TickCount64;

        // 尝试拾取玩家当前脚下 (session.X, session.Y) 的物品
        var (success, groundItem, errorMsg) = map.TryPickGroundItem(session.X, session.Y, session.SessionId, nowMs);
        if (!success || groundItem == null)
        {
            if (!string.IsNullOrEmpty(errorMsg))
            {
                // 发送官方系统提示消息
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, errorMsg);
            }
            return;
        }

        // 1. 金币拾取处理
        if (groundItem.IsGold)
        {
            session.State.Gold += groundItem.Count;
            // 广播地表物品消失: SM_DELGROUNDITEM (315)
            BroadcastToNinePatch(session, ProtocolConsts.SM_DELGROUNDITEM, groundItem.Uid, groundItem.X, groundItem.Y, 0, groundItem.Name, includeSelf: true);
            // 刷新金币
            session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, 0, 0, 0);
            return;
        }

        // 2. 普通装备/物品拾取处理
        // 2.1 检查背包格子上限 (官方 40 格)
        if (session.State.BagItems.Count >= 40)
        {
            // 背包已满，放回地面
            map.AddGroundItem(groundItem);
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "背包已满，无法拾取！");
            return;
        }

        // 2.2 检查背包负重 (IDA 0x4C5CB0 @ L913)
        if (session.State.BagWeight + groundItem.Weight > session.State.MaxBagWeight)
        {
            // 超过负重，放回地面
            map.AddGroundItem(groundItem);
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "负重不足，无法拾取！");
            return;
        }

        // 2.3 入包结算
        session.State.BagItems.Add(new EquipItem
        {
            Uid = groundItem.Uid,
            Name = groundItem.Name,
            Weight = groundItem.Weight
        });
        session.State.BagWeight += groundItem.Weight;

        // 2.4 九宫格广播地表物品消失: SM_DELGROUNDITEM (315)
        BroadcastToNinePatch(session, ProtocolConsts.SM_DELGROUNDITEM, groundItem.Uid, groundItem.X, groundItem.Y, 0, groundItem.Name, includeSelf: true);

        // 2.5 下发物品入包通知: SM_ADDITEM (200)
        session.EnqueuePacket(ProtocolConsts.SM_ADDITEM, groundItem.Uid, groundItem.Weight, 0, 0, groundItem.Name);
    }

    /// <summary>
    /// 执行局部九宫格高效广播 (将包 Enqueue 到周围玩家的待合包缓冲池中，100ms 末尾统一冲刷)
    /// </summary>
    private void BroadcastToNinePatch(ClientSession source, short ident, int recog, int param, int tag, int series, string extra, bool includeSelf)
    {
        var map = _mapManager.GetMap(source.CurrentMapId) ?? _mapManager.DefaultMap;
        lock (_ninePatchBuffer)
        {
            map.Aoi.GetEntitiesInNinePatch(source.X, source.Y, _ninePatchBuffer);
            for (int i = 0; i < _ninePatchBuffer.Count; i++)
            {
                var target = _ninePatchBuffer[i];
                if (!includeSelf && target.SessionId == source.SessionId) continue;
                target.EnqueuePacket(ident, recog, param, tag, series, extra);
            }
        }
    }

    /// <summary>
    /// 驱动一次怪物 AI 行为并在九宫格内向玩家广播其动作 (移动/近战/巡逻)
    /// </summary>
    public int ProcessMonsterAiCycle(OfficialClientRulesEngine.DelphiRandom rng)
    {
        if (RegenEngine == null) return 0;

        var (actions, _) = RegenEngine.ProcessMonsterAiTick((mapId, x, y) =>
        {
            var map = _mapManager.GetMap(mapId) ?? _mapManager.DefaultMap;
            var list = new List<ClientSession>();
            map.Aoi.GetEntitiesInNinePatch(x, y, list);
            return list;
        }, rng);

        for (int i = 0; i < actions.Count; i++)
        {
            var act = actions[i];
            var map = _mapManager.GetMap(0);
            if (map == null) continue;

            if (act.ActionType == 0 || act.ActionType == 2)
            {
                // 怪物移动广播: SM_OBJECTWALK (241)
                BroadcastToCoords(map, act.X, act.Y, ProtocolConsts.SM_OBJECTWALK, act.MonsterUid, act.X, act.Y, act.Direction, act.MonsterName);
            }
            else if (act.ActionType == 1)
            {
                // 怪物近战攻击动作: SM_OBJECTHIT (242)
                BroadcastToCoords(map, act.X, act.Y, ProtocolConsts.SM_OBJECTHIT, act.MonsterUid, act.X, act.Y, act.Direction, act.MonsterName);

                // 目标玩家受击后仰: SM_OBJECTSTRUCK (244)
                BroadcastToCoords(map, act.X, act.Y, ProtocolConsts.SM_OBJECTSTRUCK, act.TargetSessionId, act.X, act.Y, act.DamageDealt, act.MonsterName);
            }
        }

        return actions.Count;
    }

    private void BroadcastToCoords(GameMap map, int x, int y, short ident, int recog, int param, int tag, int series, string extra)
    {
        lock (_ninePatchBuffer)
        {
            map.Aoi.GetEntitiesInNinePatch(x, y, _ninePatchBuffer);
            for (int i = 0; i < _ninePatchBuffer.Count; i++)
            {
                _ninePatchBuffer[i].EnqueuePacket(ident, recog, param, tag, series, extra);
            }
        }
    }

    private void HandleNpcHomePage(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: npcUid (CM_MERCHANTHOMEPAGE 1010)
        if (NpcEngine == null) return;
        if (int.TryParse(payload, out int npcUid))
        {
            string script = NpcEngine.GetNpcHomePage(npcUid);
            // 下发官方 NPC 脚本对话框: SM_NPCDIALOG (54)
            session.EnqueuePacket(ProtocolConsts.SM_NPCDIALOG, npcUid, 0, 0, 0, script);
        }
    }

    private void HandleNpcSelect(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: npcUid/label (CM_NPCSELECT 1011)
        if (NpcEngine == null) return;
        int slash = payload.IndexOf('/');
        if (slash < 0) return;

        if (int.TryParse(payload[..slash], out int npcUid))
        {
            string label = payload[(slash + 1)..].ToString();
            string script = NpcEngine.SelectScriptOption(npcUid, label);
            session.EnqueuePacket(ProtocolConsts.SM_NPCDIALOG, npcUid, 0, 0, 0, script);
        }
    }

    private void HandleBuyItem(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: npcUid/goodsId (CM_USERBUYITEM 1014)
        if (NpcEngine == null || session.State == null) return;
        int slash = payload.IndexOf('/');
        if (slash < 0) return;

        if (int.TryParse(payload[..slash], out int npcUid) && int.TryParse(payload[(slash + 1)..], out int goodsId))
        {
            var (success, errorMsg, item) = NpcEngine.BuyItem(session, npcUid, goodsId);
            if (success && item != null)
            {
                // 1. 下发官方购买成功: SM_BUY_OK_DELPHI (648)
                session.EnqueuePacket(ProtocolConsts.SM_BUY_OK_DELPHI, item.Uid, 0, 0, 0, item.Name);
                // 2. 刷新金币: SM_GOLDCHANGED_DELPHI (653)
                session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, 0, 0, 0);
                // 3. 入包通知: SM_ADDITEM (200)
                session.EnqueuePacket(ProtocolConsts.SM_ADDITEM, item.Uid, item.Weight, 0, 0, item.Name);
            }
            else
            {
                // 下发官方购买失败: SM_BUY_FAIL_DELPHI (651)
                session.EnqueuePacket(ProtocolConsts.SM_BUY_FAIL_DELPHI, 0, 0, 0, 0, errorMsg);
            }
        }
    }

    private void HandleSellItem(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: itemUid (CM_USERSELLITEM 1013)
        if (NpcEngine == null || session.State == null) return;
        if (int.TryParse(payload, out int itemUid))
        {
            var (success, goldEarned, itemName, errorMsg) = NpcEngine.SellItem(session, itemUid);
            if (success)
            {
                // 1. 下发官方出售成功: SM_SELL_OK_DELPHI (650)
                session.EnqueuePacket(ProtocolConsts.SM_SELL_OK_DELPHI, itemUid, goldEarned, 0, 0, itemName);
                // 2. 刷新金币: SM_GOLDCHANGED_DELPHI (653)
                session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, 0, 0, 0);
            }
            else
            {
                // 下发官方出售失败: SM_SELL_FAIL_DELPHI (649)
                session.EnqueuePacket(ProtocolConsts.SM_SELL_FAIL_DELPHI, itemUid, 0, 0, 0, errorMsg);
            }
        }
    }

    private void HandleSpecialRepair(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: slot (CM_REPAIRSPECIAL 48)
        if (NpcEngine == null || session.State == null) return;
        if (int.TryParse(payload, out int slotVal) && slotVal >= 0 && slotVal < 9)
        {
            var (success, cost, itemName, errorMsg) = NpcEngine.SpecialRepairItem(session, (OfficialEquipSlot)slotVal);
            if (success)
            {
                var equip = session.State.Equipment[slotVal]!;
                // 1. 下发官方修理成功: SM_REPAIROK_DELPHI (669)
                session.EnqueuePacket(ProtocolConsts.SM_REPAIROK_DELPHI, equip.Uid, equip.Dura, equip.MaxDura, 0, itemName);
                // 2. 刷新金币: SM_GOLDCHANGED_DELPHI (653)
                session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, 0, 0, 0);
            }
            else
            {
                // 下发官方修理失败: SM_REPAIRFAIL_DELPHI (670)
                session.EnqueuePacket(ProtocolConsts.SM_REPAIRFAIL_DELPHI, 0, 0, 0, 0, errorMsg);
            }
        }
    }

    private void HandleDealTry(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: targetSessionId (CM_DEALTRY 1025)
        if (TradeEngine == null) return;
        if (int.TryParse(payload, out int targetSessionId))
        {
            var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
            ClientSession? target = null;
            lock (_ninePatchBuffer)
            {
                map.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
                for (int i = 0; i < _ninePatchBuffer.Count; i++)
                {
                    if (_ninePatchBuffer[i].SessionId == targetSessionId)
                    {
                        target = _ninePatchBuffer[i];
                        break;
                    }
                }
            }

            if (target != null)
            {
                var (success, errorMsg) = TradeEngine.TryInitiateTrade(session, target);
                if (!success)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_DEALTRY_FAIL, 0, 0, 0, 0, errorMsg);
                }
            }
            else
            {
                session.EnqueuePacket(ProtocolConsts.SM_DEALTRY_FAIL, 0, 0, 0, 0, "目标未在视野内");
            }
        }
    }

    private void HandleDealAddItem(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: itemUid (CM_DEALADDITEM 1026)
        if (TradeEngine == null) return;
        if (int.TryParse(payload, out int itemUid))
        {
            var (success, errorMsg) = TradeEngine.AddItem(session, itemUid);
            if (!success)
            {
                session.EnqueuePacket(ProtocolConsts.SM_DEALADDITEM_FAIL, itemUid, 0, 0, 0, errorMsg);
            }
        }
    }

    private void HandleDealDelItem(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: itemUid (CM_DEALDELITEM 1027)
        if (TradeEngine == null) return;
        if (int.TryParse(payload, out int itemUid))
        {
            TradeEngine.DelItem(session, itemUid);
        }
    }

    private void HandleDealChgGold(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: gold (CM_DEALCHGGOLD 1029)
        if (TradeEngine == null) return;
        if (int.TryParse(payload, out int gold))
        {
            var (success, errorMsg) = TradeEngine.ChangeGold(session, gold);
            if (!success)
            {
                session.EnqueuePacket(ProtocolConsts.SM_DEALCHGGOLD_FAIL, 0, 0, 0, 0, errorMsg);
            }
        }
    }

    private void HandleDealEnd(ClientSession session)
    {
        // CM_DEALEND (1030)
        TradeEngine?.ConfirmDeal(session);
    }

    private void HandleDealCancel(ClientSession session)
    {
        TradeEngine?.CancelTrade(session);
    }

    private void HandleStorageDeposit(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null) return;
        int slot = -1;
        string itemName = string.Empty;

        int slash = payload.IndexOf('/');
        if (slash > 0)
        {
            int.TryParse(payload[..slash], out slot);
            itemName = payload[(slash + 1)..].ToString();
        }
        else
        {
            if (!int.TryParse(payload, out slot))
            {
                itemName = payload.ToString();
            }
        }

        var (res, item) = StorageEngine.DepositItem(session.State, slot, itemName);
        switch (res)
        {
            case StorageEngine.StorageResult.Success when item != null:
                session.EnqueuePacket(ProtocolConsts.SM_SAVEITEM_OK, item.Id, 0, 0, 0, item.Name);
                session.EnqueuePacket(ProtocolConsts.SM_DELITEM, item.Id, 0, 0, 0, item.Name);
                session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
                break;
            case StorageEngine.StorageResult.StorageFull:
                session.EnqueuePacket(ProtocolConsts.SM_SAVEITEM_FULL, 0, 0, 0, 0, "保管箱已满无法寄存");
                break;
            case StorageEngine.StorageResult.NeedPassword:
                session.EnqueuePacket(ProtocolConsts.SM_NEEDPASSWORD, 0, 0, 0, 0, "请先输入仓库二级密码");
                break;
            default:
                session.EnqueuePacket(ProtocolConsts.SM_SAVEITEM_FAIL, 0, 0, 0, 0, "寄存失败");
                break;
        }
    }

    private void HandleStorageWithdraw(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null) return;
        if (!int.TryParse(payload, out int storageSlot)) return;

        var (res, item) = StorageEngine.WithdrawItem(session.State, storageSlot);
        switch (res)
        {
            case StorageEngine.StorageResult.Success when item != null:
                session.EnqueuePacket(ProtocolConsts.SM_TAKEBACKITEM_OK, item.Id, 0, 0, 0, item.Name);
                session.EnqueuePacket(ProtocolConsts.SM_ADDITEM, item.Id, 0, 0, 0, item.Name);
                session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
                break;
            case StorageEngine.StorageResult.BagFull:
            case StorageEngine.StorageResult.OverWeight:
                session.EnqueuePacket(ProtocolConsts.SM_TAKEBACKITEM_FULL, 0, 0, 0, 0, "个人包袱已满或负重超限");
                break;
            case StorageEngine.StorageResult.NeedPassword:
                session.EnqueuePacket(ProtocolConsts.SM_NEEDPASSWORD, 0, 0, 0, 0, "请先输入仓库二级密码");
                break;
            default:
                session.EnqueuePacket(ProtocolConsts.SM_TAKEBACKITEM_FAIL, 0, 0, 0, 0, "取回失败");
                break;
        }
    }

    private void HandleStorageSaveGold(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null) return;
        if (!int.TryParse(payload, out int amount)) return;

        var res = StorageEngine.DepositGold(session.State, amount);
        if (res == StorageEngine.StorageResult.Success)
        {
            session.EnqueuePacket(ProtocolConsts.SM_STORAGE_SOUND, 0, 0, 0, 0);
            session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, session.State.StorageGold, 0, 0);
        }
        else if (res == StorageEngine.StorageResult.NeedPassword)
        {
            session.EnqueuePacket(ProtocolConsts.SM_NEEDPASSWORD, 0, 0, 0, 0, "请先输入仓库二级密码");
        }
    }

    private void HandleStorageTakeGold(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null) return;
        if (!int.TryParse(payload, out int amount)) return;

        var res = StorageEngine.WithdrawGold(session.State, amount);
        if (res == StorageEngine.StorageResult.Success)
        {
            session.EnqueuePacket(ProtocolConsts.SM_STORAGE_SOUND, 0, 0, 0, 0);
            session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, session.State.StorageGold, 0, 0);
        }
        else if (res == StorageEngine.StorageResult.NeedPassword)
        {
            session.EnqueuePacket(ProtocolConsts.SM_NEEDPASSWORD, 0, 0, 0, 0, "请先输入仓库二级密码");
        }
    }

    private void HandleStoragePassword(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null) return;
        string pass = payload.ToString();
        var res = StorageEngine.VerifyPassword(session.State, pass);
        if (res == StorageEngine.StorageResult.Success)
        {
            session.EnqueuePacket(ProtocolConsts.SM_PASSWORDSTATUS, 1, 0, 0, 0, "密码验证成功");
            // 自动下发仓库物品列表
            session.EnqueuePacket(ProtocolConsts.SM_OPENSTORAGEDLG, 0, 0, 0, 0);
        }
        else
        {
            session.EnqueuePacket(ProtocolConsts.SM_PASSWORDSTATUS, 0, 0, 0, 0, "密码错误");
        }
    }

    private void HandleEatItem(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null) return;
        int slot = -1;
        string itemName = string.Empty;

        int slash = payload.IndexOf('/');
        if (slash > 0)
        {
            int.TryParse(payload[..slash], out slot);
            itemName = payload[(slash + 1)..].ToString();
        }
        else
        {
            if (!int.TryParse(payload, out slot))
            {
                itemName = payload.ToString();
            }
        }

        var (res, effect) = PotionEngine.ConsumePotion(session.State, slot, itemName);
        if (res == PotionEngine.PotionResult.Success && effect != null)
        {
            session.EnqueuePacket(ProtocolConsts.SM_EAT_OK, 0, 0, 0, 0);
            session.EnqueuePacket(ProtocolConsts.SM_HEALTHSPELLCHANGED, session.State.Hp, session.State.Mp, session.State.MaxHp, session.State.MaxMp);
            session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);

            if (effect.ClearPoison)
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "体内的毒素已被化解，恢复了健康。");
                SendAbility(session);
            }
        }
    }

    private void HandleGroupMode(ClientSession session, ReadOnlySpan<char> payload)
    {
        bool enable = true;
        if (payload.Length > 0 && (payload[0] == '0' || payload[0] == 'f' || payload[0] == 'F'))
        {
            enable = false;
        }
        GroupEngine.ToggleGroupMode(session, enable);
    }

    private void HandleCreateGroup(ClientSession session, ReadOnlySpan<char> payload)
    {
        GroupEngine.CreateParty(session);
    }

    private void HandleAddGroupMember(ClientSession session, ReadOnlySpan<char> payload)
    {
        string targetName = payload.ToString();
        var map = _mapManager.GetMap(session.CurrentMapId);
        if (map == null) return;

        ClientSession? target = null;
        lock (_ninePatchBuffer)
        {
            map.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
            target = _ninePatchBuffer.Find(s => s.CharacterName == targetName);
        }

        if (target != null)
        {
            GroupEngine.AddMember(session, target);
        }
    }

    private void HandleDelGroupMember(ClientSession session, ReadOnlySpan<char> payload)
    {
        string targetName = payload.ToString();
        var party = GroupEngine.GetParty(session.SessionId);
        if (party == null) return;

        var target = party.Members.Find(m => m.CharacterName == targetName) ?? session;
        GroupEngine.RemoveMember(session, target);
    }

    private void HandleCreateGuild(ClientSession session, ReadOnlySpan<char> payload)
    {
        string guildName = payload.ToString();
        var (res, guild) = GuildEngine.CreateGuild(session, guildName);
        if (res == GuildEngine.GuildResult.Success && guild != null)
        {
            session.EnqueuePacket(ProtocolConsts.SM_BUILDGUILDOK, 0, 0, 0, 0, guild.Name);
            if (session.State != null)
            {
                session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, 0, 0, 0);
                session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
            }
        }
        else
        {
            session.EnqueuePacket(ProtocolConsts.SM_BUILDGUILDFAIL, 0, 0, 0, 0, "创建行会失败(金币不足或无沃玛号角)");
        }
    }

    private void HandleOpenGuild(ClientSession session)
    {
        GuildEngine.SendGuildMemberList(session);
    }

    private void HandleGuildAddMember(ClientSession session, ReadOnlySpan<char> payload)
    {
        string targetName = payload.ToString();
        var res = GuildEngine.AddMember(session, targetName);
        if (res == GuildEngine.GuildResult.Success)
        {
            session.EnqueuePacket(ProtocolConsts.SM_GUILDADDMEMBEROK, 0, 0, 0, 0, targetName);
        }
        else
        {
            session.EnqueuePacket(ProtocolConsts.SM_GUILDADDMEMBERFAIL, 0, 0, 0, 0, targetName);
        }
    }

    private void HandleGuildKickMember(ClientSession session, ReadOnlySpan<char> payload)
    {
        string targetName = payload.ToString();
        var res = GuildEngine.KickMember(session, targetName);
        if (res == GuildEngine.GuildResult.Success)
        {
            session.EnqueuePacket(ProtocolConsts.SM_GUILDDELMEMBEROK, 0, 0, 0, 0, targetName);
        }
        else
        {
            session.EnqueuePacket(ProtocolConsts.SM_GUILDDELMEMBERFAIL, 0, 0, 0, 0, targetName);
        }
    }

    private void HandleGuildUpdateNotice(ClientSession session, ReadOnlySpan<char> payload)
    {
        string notice = payload.ToString();
        var res = GuildEngine.UpdateNotice(session, notice);
        if (res == GuildEngine.GuildResult.Success)
        {
            session.EnqueuePacket(ProtocolConsts.SM_GUILDMESSAGE, 0, 0, 0, 0, notice);
        }
    }

    private void HandleMineHit(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 挥镐挖矿判定 (CM_BIGHIT 3015)
        if (session.State == null) return;
        // 检查武器是否手持鹤嘴锄/矿镐
        var weapon = session.State.Equipment[(int)OfficialEquipSlot.Weapon];
        bool hasPickaxe = weapon != null && (weapon.Name.Contains("锄") || weapon.Name.Contains("镐"));

        if (!hasPickaxe)
        {
            // 非矿镐挥击仅播动作
            return;
        }

        // 1/3 概率挖出品质 1~20 的矿石
        if (Random.Shared.Next(3) == 0)
        {
            int purity = Random.Shared.Next(1, 21);
            string oreName = purity >= 15 ? $"黑铁矿石(纯度{purity})" : $"金矿(纯度{purity})";
            var ore = new EquipItem
            {
                Uid = Random.Shared.Next(100000, 999999),
                Name = oreName,
                Weight = 5,
                Dura = purity * 1000,
                MaxDura = purity * 1000
            };

            if (session.State.BagItems.Count < 40 && session.State.BagWeight + ore.Weight <= session.State.MaxBagWeight)
            {
                session.State.BagItems.Add(ore);
                session.State.BagWeight += ore.Weight;
                session.EnqueuePacket(ProtocolConsts.SM_MINERESULT, ore.Uid, purity, 0, 0, ore.Name);
                session.EnqueuePacket(ProtocolConsts.SM_ADDITEM, ore.Uid, 0, 0, 0, ore.Name);
                session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
            }
        }
    }

    private void HandleHeavyHit(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 刺杀剑术隔位刺杀判定 (CM_HEAVYHIT 3016 / CM_LONGHIT 3019)
        // 隔位破防刺杀：物理攻击伤害无视目标防御
        HandleHit(session, payload);
    }

    private void HandleNewChar(ClientSession session, ReadOnlySpan<char> payload)
    {
        // CM_NEWCHAR (104) 格式: Name\tJob\tGender
        string chrName = payload.ToString();
        session.EnqueuePacket(ProtocolConsts.SM_NEWID_SUCCESS, session.SessionId, 0, 0, 0, chrName);
    }

    private void HandleDelChar(ClientSession session, ReadOnlySpan<char> payload)
    {
        // CM_DELCHAR (105)
        session.EnqueuePacket(ProtocolConsts.SM_DELCHAR_SUCCESS_DELPHI, session.SessionId, 0, 0, 0);
    }

    private void HandleLogout(ClientSession session)
    {
        // CM_SOFTCLOSE (106) / CM_LOGOUT (3001)
        session.EnqueuePacket(ProtocolConsts.SM_DISCONNECT_DELPHI, session.SessionId, 0, 0, 0);
    }

    private void HandleSitDown(ClientSession session)
    {
        // CM_SITDOWN (3012)
        session.EnqueuePacket(ProtocolConsts.SM_SITDOWN, session.SessionId, 0, 0, 0);
    }

    #region 现代高吞吐全权威服务端状态机体系 (Server-Authoritative Intent Engine)

    /// <summary>
    /// 处理现代高吞吐紧凑二进制封包 (Direct-Blit 零分配，全权威状态机驱动)
    /// </summary>
    public void DispatchBinary(ClientSession session, in BinaryProtocol.PacketHeader header, ReadOnlySpan<byte> payload)
    {
        switch ((BinaryProtocol.MsgType)header.MsgId)
        {
            case BinaryProtocol.MsgType.HandshakeRequest:
                HandleBinaryHandshake(session);
                break;

            case BinaryProtocol.MsgType.IntentMove:
                HandleBinaryMoveIntent(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentAttack:
                HandleBinaryAttackIntent(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentSpell:
                HandleBinarySpellIntent(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentUseItem:
                HandleBinaryUseItemIntent(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentPickup:
                HandleBinaryPickupIntent(session);
                break;

            case BinaryProtocol.MsgType.Heartbeat:
                // 心跳回执
                session.EnqueueBinaryPacket((ushort)BinaryProtocol.MsgType.Heartbeat, ReadOnlySpan<byte>.Empty);
                break;
        }
    }

    private static void HandleBinaryHandshake(ClientSession session)
    {
        session.UseModernProtocol = true;
        // 回复握手成功响应 (0 载荷)
        session.EnqueueBinaryPacket((ushort)BinaryProtocol.MsgType.HandshakeResponse, ReadOnlySpan<byte>.Empty);
    }

    /// <summary>
    /// 权威移动意图校验 (杜绝穿墙、瞬移、变速齿轮加速外挂)
    /// </summary>
    public void HandleBinaryMoveIntent(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < 6) return;
        var move = BinaryProtocol.MoveIntentPayload.Read(payload);

        int oldX = session.X;
        int oldY = session.Y;
        int newX = move.TargetX;
        int newY = move.TargetY;
        bool isRun = move.IsRun != 0;
        byte dir = move.Direction;

        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;

        // 1. 地图物理边界与障碍阻挡判定
        if (!map.IsWalkable(newX, newY))
        {
            SendPositionPullback(session, oldX, oldY, session.Direction);
            return;
        }

        // 2. 步长与防瞬移物理校验 (行走限 1 格，跑步限 2 格)
        int maxDist = isRun ? 2 : 1;
        int deltaX = Math.Abs(newX - oldX);
        int deltaY = Math.Abs(newY - oldY);
        if (deltaX > maxDist || deltaY > maxDist || (deltaX == 0 && deltaY == 0))
        {
            SendPositionPullback(session, oldX, oldY, session.Direction);
            return;
        }

        // 3. 移速反加速外挂校验 (基于真实服务器物理时间戳)
        long nowMs = Environment.TickCount64;
        if (session.LastMoveTimeMs > 0)
        {
            var (isSpeedHack, shouldRollback) = EconomySecurityEngine.SpeedHackDetectorEngine.CheckMovePacket(nowMs, session.LastMoveTimeMs);
            if (isSpeedHack && shouldRollback)
            {
                SendPositionPullback(session, oldX, oldY, session.Direction);
                return;
            }
        }
        session.LastMoveTimeMs = nowMs;

        // 4. 权威更新实体状态
        session.X = newX;
        session.Y = newY;
        session.Direction = dir;

        // 5. 更新九宫格 AOI 空间分区
        map.Aoi.UpdatePosition(session, oldX, oldY, newX, newY, out _, out _);

        // 6. 千人同屏九宫格 LOD 广播实体移动 (仅 6 字节二进制帧)
        Span<byte> syncSpan = stackalloc byte[6];
        new BinaryProtocol.PositionSyncPayload
        {
            X = (short)newX,
            Y = (short)newY,
            Direction = dir,
            ForcePullback = 0
        }.Write(syncSpan);

        BroadcastBinaryToNinePatch(session, (ushort)BinaryProtocol.MsgType.BroadcastEntityMove, syncSpan, includeSelf: false);
    }

    /// <summary>
    /// 权威物理攻击意图校验 (杜绝一秒十刀加速外挂与超远攻击)
    /// </summary>
    public void HandleBinaryAttackIntent(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < 6) return;
        var attack = BinaryProtocol.AttackIntentPayload.Read(payload);

        long nowMs = Environment.TickCount64;
        if (session.LastAttackTimeMs > 0 && !EconomySecurityEngine.AttackSpeedValidatorEngine.IsAttackValid(nowMs, session.LastAttackTimeMs))
        {
            return; // 挥刀过快拦截
        }
        session.LastAttackTimeMs = nowMs;

        // 1. 向同屏九宫格广播动作帧
        Span<byte> actionSpan = stackalloc byte[6];
        actionSpan[0] = attack.Direction;
        actionSpan[1] = attack.SkillType;
        BroadcastBinaryToNinePatch(session, (ushort)BinaryProtocol.MsgType.BroadcastEntityAction, actionSpan, includeSelf: false);

        // 2. 武器试刀判定
        session.State?.TrialSlashEquippedWeapon();

        // 3. 在九宫格检索攻击目标
        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
        ClientSession? defender = null;
        lock (_ninePatchBuffer)
        {
            _ninePatchBuffer.Clear();
            map.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
            for (int i = 0; i < _ninePatchBuffer.Count; i++)
            {
                var p = _ninePatchBuffer[i];
                if (p.SessionId == attack.TargetUid)
                {
                    defender = p;
                    break;
                }
            }
        }

        if (defender != null && defender.State != null)
        {
            int dist = Math.Max(Math.Abs(defender.X - session.X), Math.Abs(defender.Y - session.Y));
            var (rawDmg, hit, isArmorPiercing, isFireHit, died) = ResolveAuthoritativePhysicalCombat(session, defender, dist, nowMs);

            if (hit && rawDmg > 0)
            {
                // 权威血量同步
                Span<byte> hpSpan = stackalloc byte[8];
                new BinaryProtocol.HealthSyncPayload
                {
                    Hp = (short)defender.State.Hp,
                    Mp = (short)defender.State.Mp,
                    MaxHp = (short)defender.State.MaxHp,
                    MaxMp = (short)defender.State.MaxMp
                }.Write(hpSpan);

                defender.EnqueueBinaryPacket((ushort)BinaryProtocol.MsgType.BroadcastHealthSync, hpSpan);
            }
        }
    }

    /// <summary>
    /// 权威法术施法意图校验 (杜绝无CD施法与全屏雷电外挂)
    /// </summary>
    public void HandleBinarySpellIntent(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < 9) return;
        var spell = BinaryProtocol.SpellIntentPayload.Read(payload);

        long nowMs = Environment.TickCount64;
        if (session.LastSpellTimeMs > 0 && (nowMs - session.LastSpellTimeMs < 500))
        {
            return; // 施法过快拦截
        }
        session.LastSpellTimeMs = nowMs;

        if (session.State != null)
        {
            var skill = SkillFormulas.GetSkill(spell.SpellId);
            int mpCost = skill?.MpCost ?? 10;
            if (session.State.Mp < mpCost) return; // 蓝量不足
            session.State.Mp -= mpCost;

            // 广播施法动作
            Span<byte> actionSpan = stackalloc byte[6];
            actionSpan[0] = 0; // Spell Action
            actionSpan[1] = spell.SpellId;
            BroadcastBinaryToNinePatch(session, (ushort)BinaryProtocol.MsgType.BroadcastEntityAction, actionSpan, includeSelf: false);

            // 权威法术与特技解析核心
            ResolveAuthoritativeSpell(session, spell.SpellId, spell.TargetUid, spell.TargetX, spell.TargetY, nowMs);

            // 自身蓝量同步
            Span<byte> hpSpan = stackalloc byte[8];
            new BinaryProtocol.HealthSyncPayload
            {
                Hp = (short)session.State.Hp,
                Mp = (short)session.State.Mp,
                MaxHp = (short)session.State.MaxHp,
                MaxMp = (short)session.State.MaxMp
            }.Write(hpSpan);
            session.EnqueueBinaryPacket((ushort)BinaryProtocol.MsgType.BroadcastHealthSync, hpSpan);
        }
    }

    /// <summary>
    /// 权威消耗品使用意图校验
    /// </summary>
    public void HandleBinaryUseItemIntent(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < 4) return;
        int itemUid = System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(payload[0..4]);

        if (session.State == null) return;
        for (int i = 0; i < session.State.BagItems.Count; i++)
        {
            var item = session.State.BagItems[i];
            if (item.Uid == itemUid)
            {
                session.State.BagItems.RemoveAt(i);
                // 恢复 50 点生命与 30 点魔法
                session.State.Hp = Math.Min(session.State.MaxHp, session.State.Hp + 50);
                session.State.Mp = Math.Min(session.State.MaxMp, session.State.Mp + 30);

                // 下发权威血蓝同步
                Span<byte> hpSpan = stackalloc byte[8];
                new BinaryProtocol.HealthSyncPayload
                {
                    Hp = (short)session.State.Hp,
                    Mp = (short)session.State.Mp,
                    MaxHp = (short)session.State.MaxHp,
                    MaxMp = (short)session.State.MaxMp
                }.Write(hpSpan);
                session.EnqueueBinaryPacket((ushort)BinaryProtocol.MsgType.BroadcastHealthSync, hpSpan);
                break;
            }
        }
    }

    /// <summary>
    /// 权威拾取意图校验 (杜绝全屏吸物外挂)
    /// </summary>
    public void HandleBinaryPickupIntent(ClientSession session)
    {
        // 必须与地表掉落物在同一格 (session.X, session.Y)
        HandlePickUp(session);
    }

    /// <summary>
    /// 服务端下发强制瞬移拉回包 (物理终结外挂穿墙与加速)
    /// </summary>
    private static void SendPositionPullback(ClientSession session, int x, int y, byte dir)
    {
        Span<byte> pullbackSpan = stackalloc byte[6];
        new BinaryProtocol.PositionSyncPayload
        {
            X = (short)x,
            Y = (short)y,
            Direction = dir,
            ForcePullback = 1
        }.Write(pullbackSpan);

        session.EnqueueBinaryPacket((ushort)BinaryProtocol.MsgType.SyncPosition, pullbackSpan);
    }

    /// <summary>
    /// 千人同屏九宫格视距降采样与高密度目标上限熔断广播 (LOD Spatial Partitioning & Density Cap)
    /// </summary>
    public void BroadcastBinaryToNinePatch(ClientSession source, ushort msgId, ReadOnlySpan<byte> payload, bool includeSelf = false, int maxDist = 15, int maxTargets = 80)
    {
        var map = _mapManager.GetMap(source.CurrentMapId) ?? _mapManager.DefaultMap;
        lock (_ninePatchBuffer)
        {
            _ninePatchBuffer.Clear();
            map.Aoi.GetEntitiesInNinePatch(source.X, source.Y, _ninePatchBuffer);

            int sentCount = 0;
            for (int i = 0; i < _ninePatchBuffer.Count; i++)
            {
                var target = _ninePatchBuffer[i];
                if (!includeSelf && target == source) continue;

                // 视距 LOD 剔除: 切比雪夫距离超出 maxDist 格的实体不广播高频微小动作
                int dist = Math.Max(Math.Abs(target.X - source.X), Math.Abs(target.Y - source.Y));
                if (dist > maxDist) continue;

                if (target.UseModernProtocol)
                {
                    target.EnqueueBinaryPacket(msgId, payload);
                    sentCount++;
                    // 千人高密度同屏熔断保护：单屏单个动作广播目标上限锁定在 80 人以内，彻底阻断百万级网络广播风暴
                    if (sentCount >= maxTargets) break;
                }
            }
        }
    }

    /// <summary>
    /// 服务端权威物理战斗精算与结算核心 (官方 1.5 权威真值)
    /// 涵盖：敏捷/准确命中对抗、武器运九套爆发/诅咒下限、双烈火蓄力 2.6 倍暴击、隔位刺杀破盾破防、道士红毒 1.2 倍易伤、护身与复活戒指
    /// </summary>
    public (int damage, bool hit, bool isArmorPiercing, bool isFireHit, bool died) ResolveAuthoritativePhysicalCombat(
        ClientSession attacker, ClientSession defender, int dist, long nowMs, bool forcePierce = false)
    {
        if (attacker.State == null || defender.State == null)
            return (0, false, false, false, false);

        // 1. 距离校验 (贴身 1 格，隔位刺杀 2 格，大于 2 格物理打不着)
        if (dist > 2) return (0, false, false, false, false);

        bool isArmorPiercing = forcePierce || (dist == 2 && attacker.State.StabbingTracker.AutoStab);
        if (dist == 2 && !isArmorPiercing)
        {
            // 距离为 2 但未开刺杀，打不到目标
            return (0, false, false, false, false);
        }

        // 2. 物理命中对抗 (IDA sub_4C1EF4: rollSpeed < Hit)
        int rollSpeed = OfficialClientRulesEngine.DelphiRandom.Shared.Next(defender.State.Speed + 1);
        bool hit = rollSpeed < attacker.State.Hit;
        if (!hit)
        {
            return (0, false, isArmorPiercing, false, false);
        }

        // 3. 基础伤害精算 (结合武器与项链幸运/诅咒与运九套满攻爆发)
        int rawDmg = attacker.State.CalculateAttackDamageWithLuck(15, 30, Random.Shared);

        // 4. 攻杀剑气附加伤害
        if (attacker.State.BoAttackKill)
        {
            attacker.State.BoAttackKill = false;
            rawDmg += attacker.State.HitPlus;
        }

        // 5. 烈火剑法火焰爆发 (2.6 倍物理爆发，双烈火蓄力时钟)
        bool isFireHit = false;
        var (isFire, fireDmg, _) = attacker.State.FireHitTracker.ConsumeOnHit(rawDmg, nowMs);
        if (isFire)
        {
            rawDmg = fireDmg;
            isFireHit = true;
        }
        else if (attacker.State.BoFireHit)
        {
            attacker.State.BoFireHit = false;
            int multiplier = 10 * attacker.State.FireHitPlus;
            rawDmg += (int)Math.Round((double)rawDmg / 100.0 * multiplier);
            isFireHit = true;
        }

        // 6. 道士红毒 1.2 倍易伤 (IDA sub_4C7720 / tbyte_4BE3CC)
        if (defender.State.HasRedPoison)
        {
            rawDmg = TaoistCombatEngine.ApplyRedPoisonVulnerability(rawDmg, hasRedPoison: true);
        }

        // 7. 虹魔吸血判定 (0x4C1EF4 @ L12733)
        attacker.State.ProcessAttackLifesteal(rawDmg);

        // 8. 麻痹戒指判定
        if (attacker.State.HasParalysisRing)
        {
            int rollPara = OfficialClientRulesEngine.DelphiRandom.Shared.Next(defender.State.AntiPoison + 5);
            if (rollPara == 0)
            {
                defender.State.StatusBits[5] = true;
            }
        }

        // 9. 伤害扣减结算 (隔位刺杀 isArmorPiercing 击穿魔法盾与基础护甲)
        var (finalHp, finalMp, died, revived, absorbed, shieldBroken) = defender.State.TakeCombatDamageFull(rawDmg, nowMs, isArmorPiercing: isArmorPiercing);

        return (rawDmg, true, isArmorPiercing, isFireHit, died);
    }

    /// <summary>
    /// 服务端权威怪物受击物理结算
    /// </summary>
    public (int damage, bool hit, bool isFireHit, bool died) ResolveAuthoritativeMonsterPhysicalCombat(
        ClientSession attacker, MonsterInstance monster, int dist, long nowMs)
    {
        if (attacker.State == null || monster.IsDead)
            return (0, false, false, false);

        if (dist > 2) return (0, false, false, false);

        bool isArmorPiercing = dist == 2 && attacker.State.StabbingTracker.AutoStab;
        if (dist == 2 && !isArmorPiercing) return (0, false, false, false);

        int rollSpeed = OfficialClientRulesEngine.DelphiRandom.Shared.Next(16);
        bool hit = rollSpeed < attacker.State.Hit;
        if (!hit) return (0, false, false, false);

        int rawDmg = attacker.State.CalculateAttackDamageWithLuck(15, 30, Random.Shared);
        if (attacker.State.BoAttackKill)
        {
            attacker.State.BoAttackKill = false;
            rawDmg += attacker.State.HitPlus;
        }

        bool isFireHit = false;
        var (isFire, fireDmg, _) = attacker.State.FireHitTracker.ConsumeOnHit(rawDmg, nowMs);
        if (isFire)
        {
            rawDmg = fireDmg;
            isFireHit = true;
        }

        monster.Hp = Math.Max(0, monster.Hp - rawDmg);
        return (rawDmg, true, isFireHit, monster.IsDead);
    }

    /// <summary>
    /// 服务端权威法术与特技解析核心 (官方 1.5 权威实证)
    /// 包含烈火剑法蓄力、刺杀开关、抗拒火环推人、诱惑之光定身/驯服、地狱雷光不死克制、施毒术红绿毒
    /// </summary>
    public void ResolveAuthoritativeSpell(ClientSession session, short magicId, int targetUid, int targetX, int targetY, long nowMs)
    {
        if (session.State == null) return;
        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;

        // 1. 烈火剑法凝聚火焰 (MagicId = 26)
        if (magicId == 26)
        {
            var (castSuccess, reason) = session.State.FireHitTracker.Cast(nowMs);
            session.State.BoFireHit = castSuccess;
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, reason);
            return;
        }

        // 2. 刺杀剑术开关切换 (MagicId = 12)
        if (magicId == 12)
        {
            bool active = session.State.StabbingTracker.ToggleAutoStab();
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, active ? "已开启刺杀剑气 (隔位破盾破防)" : "已关闭刺杀剑气");
            return;
        }

        // 3. 魔法盾 (MagicId = 31)
        if (magicId == 31)
        {
            session.State.ActivateMagicShield(3, 30);
            return;
        }

        // 4. 抗拒火环 (MagicId = 8)
        if (magicId == 8)
        {
            var surrounding = new List<(int uid, int level, int x, int y, bool isBoss)>();
            lock (_ninePatchBuffer)
            {
                map.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
                for (int i = 0; i < _ninePatchBuffer.Count; i++)
                {
                    var ent = _ninePatchBuffer[i];
                    if (ent.SessionId != session.SessionId && Math.Abs(ent.X - session.X) <= 1 && Math.Abs(ent.Y - session.Y) <= 1)
                    {
                        surrounding.Add((ent.SessionId, ent.State?.Level ?? 1, ent.X, ent.Y, isBoss: false));
                    }
                }
            }
            if (RegenEngine != null)
            {
                var monsters = RegenEngine.GetMonstersInArea(session.CurrentMapId, session.X - 1, session.Y - 1, session.X + 1, session.Y + 1);
                for (int i = 0; i < monsters.Count; i++)
                {
                    var m = monsters[i];
                    if (!m.IsDead)
                    {
                        surrounding.Add((m.Uid, 20, m.X, m.Y, isBoss: m.Name.Contains("教主")));
                    }
                }
            }

            var pushResults = MageCombatEngine.RingOfFire.Execute(session.State.Level, session.X, session.Y, surrounding, (x, y) => !map.IsWalkable(x, y));
            for (int i = 0; i < pushResults.Count; i++)
            {
                var pr = pushResults[i];
                if (pr.Pushed)
                {
                    ClientSession? targetPlayer = null;
                    lock (_ninePatchBuffer)
                    {
                        for (int j = 0; j < _ninePatchBuffer.Count; j++)
                        {
                            if (_ninePatchBuffer[j].SessionId == pr.Uid)
                            {
                                targetPlayer = _ninePatchBuffer[j];
                                break;
                            }
                        }
                    }
                    if (targetPlayer != null)
                    {
                        int oldX = targetPlayer.X;
                        int oldY = targetPlayer.Y;
                        targetPlayer.X = pr.FinalX;
                        targetPlayer.Y = pr.FinalY;
                        map.Aoi.UpdatePosition(targetPlayer, oldX, oldY, pr.FinalX, pr.FinalY, out _, out _);
                        SendPositionPullback(targetPlayer, pr.FinalX, pr.FinalY, targetPlayer.Direction);
                        BroadcastToNinePatch(targetPlayer, ProtocolConsts.SM_OBJECTRUN, targetPlayer.SessionId, pr.FinalX, pr.FinalY, targetPlayer.Direction, targetPlayer.CharacterName, includeSelf: true);
                    }
                }
            }
            return;
        }

        // 5. 施毒术 (MagicId = 6)
        if (magicId == 6)
        {
            ClientSession? targetPlayer = null;
            lock (_ninePatchBuffer)
            {
                map.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
                for (int i = 0; i < _ninePatchBuffer.Count; i++)
                {
                    var ent = _ninePatchBuffer[i];
                    if (ent.SessionId != session.SessionId && (ent.SessionId == targetUid || (ent.X == targetX && ent.Y == targetY)))
                    {
                        targetPlayer = ent;
                        break;
                    }
                }
            }
            if (targetPlayer != null && targetPlayer.State != null)
            {
                bool hit = TaoistCombatEngine.CheckPoisonHit(targetPlayer.State.AntiPoison, Random.Shared);
                if (hit)
                {
                    var pType = !targetPlayer.State.HasGreenPoison ? TaoistCombatEngine.PoisonType.Green : TaoistCombatEngine.PoisonType.Red;
                    int duration = TaoistCombatEngine.CalculatePoisonDurationSec(pType, taoistSc: 25);
                    targetPlayer.State.ApplyPoison(pType, duration, 25, nowMs);

                    string pDesc = pType == TaoistCombatEngine.PoisonType.Green ? "中绿毒 (每2.5s持续掉血，清空回血池)" : "中红毒 (1.2倍易伤减防)";
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"施毒成功：目标已{pDesc}");
                    targetPlayer.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"受到道士施毒术感染：{pDesc}");
                }
            }
            return;
        }

        // 6. 地狱雷光 (MagicId = 21)
        if (magicId == 21)
        {
            if (RegenEngine != null)
            {
                var monsters = RegenEngine.GetMonstersInArea(session.CurrentMapId, session.X - 1, session.Y - 1, session.X + 1, session.Y + 1);
                for (int i = 0; i < monsters.Count; i++)
                {
                    var m = monsters[i];
                    if (!m.IsDead)
                    {
                        bool isUndead = m.Name.Contains("尸") || m.Name.Contains("骷髅") || m.Name.Contains("骨");
                        int dmg = MageCombatEngine.HellFire.CalculateDamage(40, isUndead);
                        m.Hp = Math.Max(0, m.Hp - dmg);
                        BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, m.Uid, m.X, m.Y, dmg, m.Name, includeSelf: true);
                        if (m.IsDead)
                        {
                            RegenEngine.OnMonsterDied(m.Uid);
                            BroadcastToNinePatch(session, ProtocolConsts.SM_DEATH_ACTION_DELPHI, m.Uid, m.X, m.Y, 0, m.Name, includeSelf: true);
                            OnMonsterKilledByPlayer(session, m);
                        }
                    }
                }
            }
            return;
        }

        // 7. 治愈术 (MagicId = 2) 与 群体治愈术 (MagicId = 29)
        if (magicId == 2 || magicId == 29)
        {
            int heal = TaoistCombatEngine.MassHeal.CalculateHealAmount(sc: 25, skillLevel: 3);
            if (magicId == 29)
            {
                // 3x3 范围群体治愈
                lock (_ninePatchBuffer)
                {
                    map.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
                    for (int i = 0; i < _ninePatchBuffer.Count; i++)
                    {
                        var p = _ninePatchBuffer[i];
                        if (p.State != null && Math.Abs(p.X - session.X) <= 1 && Math.Abs(p.Y - session.Y) <= 1)
                        {
                            p.State.Hp = Math.Min(p.State.MaxHp, p.State.Hp + heal);
                            p.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, p.State.Hp, p.State.MaxHp, p.State.Mp, p.State.MaxMp);
                            BroadcastToNinePatch(p, ProtocolConsts.SM_SPELLEFFECT_DELPHI, p.SessionId, p.X, p.Y, 29, "群疗", includeSelf: true);
                        }
                    }
                }
            }
            else
            {
                session.State.Hp = Math.Min(session.State.MaxHp, session.State.Hp + heal);
                session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
                BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, session.X, session.Y, 2, "治愈术", includeSelf: true);
            }
            return;
        }

        // 7.1 幽灵盾 (MagicId = 14) 3x3 范围内队友与自身增加 MAC +3 光环
        if (magicId == 14)
        {
            lock (_ninePatchBuffer)
            {
                map.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
                for (int i = 0; i < _ninePatchBuffer.Count; i++)
                {
                    var p = _ninePatchBuffer[i];
                    if (p.State != null && Math.Abs(p.X - session.X) <= 1 && Math.Abs(p.Y - session.Y) <= 1)
                    {
                        p.State.BonusMac += TaoistCombatEngine.AuraBuffs.BonusMac;
                        p.State.RecalculateAttributes();
                        p.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "【幽灵盾】获得神圣魔法防御提升光环！");
                        BroadcastToNinePatch(p, ProtocolConsts.SM_SPELLEFFECT_DELPHI, p.SessionId, p.X, p.Y, 14, "幽灵盾", includeSelf: true);
                    }
                }
            }
            return;
        }

        // 7.2 神圣战甲术 (MagicId = 15) 3x3 范围内队友与自身增加 AC +3 光环
        if (magicId == 15)
        {
            lock (_ninePatchBuffer)
            {
                map.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
                for (int i = 0; i < _ninePatchBuffer.Count; i++)
                {
                    var p = _ninePatchBuffer[i];
                    if (p.State != null && Math.Abs(p.X - session.X) <= 1 && Math.Abs(p.Y - session.Y) <= 1)
                    {
                        p.State.BonusAc += TaoistCombatEngine.AuraBuffs.BonusAc;
                        p.State.RecalculateAttributes();
                        p.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "【神圣战甲术】获得物理神圣装甲提升光环！");
                        BroadcastToNinePatch(p, ProtocolConsts.SM_SPELLEFFECT_DELPHI, p.SessionId, p.X, p.Y, 15, "神圣战甲术", includeSelf: true);
                    }
                }
            }
            return;
        }

        // 7.3 隐身术 (MagicId = 18) 与 集体隐身术 (MagicId = 19)
        if (magicId == 18 || magicId == 19)
        {
            if (RegenEngine != null)
            {
                var monsters = RegenEngine.GetMonstersInArea(session.CurrentMapId, session.X - 5, session.Y - 5, session.X + 5, session.Y + 5);
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (monsters[i].TargetSessionId == session.SessionId)
                    {
                        monsters[i].TargetSessionId = 0; // 脱离怪物仇恨目标
                    }
                }
            }
            BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, session.X, session.Y, magicId, "隐身术", includeSelf: true);
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "【隐身术】已进入隐身匿踪状态！");
            return;
        }

        // 7.4 召唤骷髅 (MagicId = 17) 与 召唤神兽 (MagicId = 30)
        if (magicId == 17 || magicId == 30)
        {
            if (RegenEngine != null)
            {
                string petName = magicId == 30 ? "神兽" : "变异骷髅";
                int baseHp = magicId == 30 ? 300 : 150;
                int petX = session.X + 1;
                int petY = session.Y;
                if (!map.IsWalkable(petX, petY) || RegenEngine.FindMonsterAt(session.CurrentMapId, petX, petY) != null)
                {
                    petX = session.X; petY = session.Y + 1;
                }
                if (!map.IsWalkable(petX, petY) || RegenEngine.FindMonsterAt(session.CurrentMapId, petX, petY) != null)
                {
                    petX = session.X - 1; petY = session.Y;
                }

                RegenEngine.AddRule(new MonsterSpawnRule
                {
                    RuleId = 88000 + session.SessionId % 1000 + (magicId == 30 ? 10000 : 0),
                    MonsterName = petName,
                    MapId = session.CurrentMapId,
                    CenterX = petX,
                    CenterY = petY,
                    Radius = 0,
                    MaxCount = 1,
                    BaseHp = baseHp
                });
                RegenEngine.ProcessRegenTick(OfficialClientRulesEngine.DelphiRandom.Shared);
                var pet = RegenEngine.FindMonsterAt(session.CurrentMapId, petX, petY);
                if (pet != null)
                {
                    pet.TargetSessionId = 0;
                }
                BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, petX, petY, magicId, petName, includeSelf: true);
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"【道法召唤】成功召唤出【{petName}】！");
            }
            return;
        }

        // 7.5 心灵启示 (MagicId = 28)
        if (magicId == 28)
        {
            ClientSession? targetPlayer = null;
            lock (_ninePatchBuffer)
            {
                map.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
                for (int i = 0; i < _ninePatchBuffer.Count; i++)
                {
                    var p = _ninePatchBuffer[i];
                    if (p.SessionId == targetUid || (p.X == targetX && p.Y == targetY))
                    {
                        targetPlayer = p;
                        break;
                    }
                }
            }
            if (targetPlayer?.State != null)
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"【心灵启示】{targetPlayer.CharacterName} 当前生命: {targetPlayer.State.Hp} / {targetPlayer.State.MaxHp}");
                return;
            }
            if (RegenEngine != null)
            {
                var m = RegenEngine.FindMonsterByUid(targetUid) ?? RegenEngine.FindMonsterAt(session.CurrentMapId, targetX, targetY);
                if (m != null)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"【心灵启示】{m.Name} 当前生命: {m.Hp} / {m.MaxHp}");
                    return;
                }
            }
            return;
        }

        // 7.6 困魔咒 (MagicId = 16)
        if (magicId == 16)
        {
            if (RegenEngine != null)
            {
                var monsters = RegenEngine.GetMonstersInArea(session.CurrentMapId, targetX - 1, targetY - 1, targetX + 1, targetY + 1);
                for (int i = 0; i < monsters.Count; i++)
                {
                    var m = monsters[i];
                    if (!m.IsDead && (m.Name.Contains("尸") || m.Name.Contains("骷髅") || m.Name.Contains("骨")))
                    {
                        m.NextActionTimeMs = nowMs + 15_000;
                    }
                }
            }
            BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, targetX, targetY, 16, "困魔咒", includeSelf: true);
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "【困魔咒】金光符文阵禁锢了范围内的亡灵生物！");
            return;
        }

        // 8. 半月弯刀 (MagicId = 25) 扇面 4 目标结算 (IDA sub_4C1EF4)
        if (magicId == 25)
        {
            var affectedTiles = WarriorCombatEngine.HalfMoon.GetAffectedTiles(session.X, session.Y, session.Direction);
            int baseDmg = session.State.CalculateAttackDamageWithLuck(15, 30, Random.Shared);

            foreach (var (tx, ty, isMain) in affectedTiles)
            {
                int dmg = WarriorCombatEngine.HalfMoon.CalculateDamage(baseDmg, isMain, skillLevel: 3);

                // 结算该格子的玩家
                lock (_ninePatchBuffer)
                {
                    map.Aoi.GetEntitiesInNinePatch(tx, ty, _ninePatchBuffer);
                    for (int i = 0; i < _ninePatchBuffer.Count; i++)
                    {
                        var target = _ninePatchBuffer[i];
                        if (target.SessionId != session.SessionId && target.X == tx && target.Y == ty && target.State != null && target.State.Hp > 0)
                        {
                            var (finalHp, finalMp, died, revived, absorbed, shieldBroken) = target.State.TakeCombatDamageFull(dmg, nowMs);
                            BroadcastToNinePatch(target, ProtocolConsts.SM_OBJECTSTRUCK, target.SessionId, target.X, target.Y, dmg, target.CharacterName, includeSelf: true);
                            target.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, target.State.Hp, target.State.MaxHp, target.State.Mp, target.State.MaxMp);
                            if (died)
                            {
                                session.State.AddPkMurder();
                                BroadcastToNinePatch(target, ProtocolConsts.SM_DEATH_ACTION_DELPHI, target.SessionId, target.X, target.Y, 0, target.CharacterName, includeSelf: true);
                            }
                        }
                    }
                }

                // 结算该格子的怪物
                if (RegenEngine != null)
                {
                    var m = RegenEngine.FindMonsterAt(session.CurrentMapId, tx, ty);
                    if (m != null && !m.IsDead)
                    {
                        m.Hp = Math.Max(0, m.Hp - dmg);
                        BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, m.Uid, m.X, m.Y, dmg, m.Name, includeSelf: true);
                        if (m.IsDead)
                        {
                            RegenEngine.OnMonsterDied(m.Uid);
                            BroadcastToNinePatch(session, ProtocolConsts.SM_DEATH_ACTION_DELPHI, m.Uid, m.X, m.Y, 0, m.Name, includeSelf: true);
                            OnMonsterKilledByPlayer(session, m);
                        }
                    }
                }
            }
            return;
        }

        // 9. 火墙术 (MagicId = 22) 生成 2x2 地表火焰实体并广播
        if (magicId == 22)
        {
            int fireDmg = OfficialClientRulesEngine.CombatMath.CalculateMagicDamage(15, 25, 0, 5, OfficialClientRulesEngine.DelphiRandom.Shared);
            long expireMs = nowMs + 20_000; // 存活 20 秒

            for (int fx = targetX; fx <= targetX + 1; fx++)
            {
                for (int fy = targetY; fy <= targetY + 1; fy++)
                {
                    if (map.IsWalkable(fx, fy))
                    {
                        map.AddFireWall(new FireWallEntity
                        {
                            Uid = 90000 + (fx * 1000 + fy) % 10000,
                            X = fx,
                            Y = fy,
                            CasterSessionId = session.SessionId,
                            Damage = fireDmg,
                            ExpireTimeMs = expireMs
                        });
                        BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, fx, fy, 21, "火墙", includeSelf: true);
                    }
                }
            }
            return;
        }

        // 10. 瞬息移动 (MagicId = 11, 兼容 14)
        if (magicId == 11 && targetUid == 0 && targetX == 0 && targetY == 0 || magicId == 14)
        {
            var (tx, ty, ok) = MageCombatEngine.Teleport.ResolveRandomLandingTile(map.Width, map.Height, (x, y) => !map.IsWalkable(x, y), Random.Shared);
            int oldX = session.X;
            int oldY = session.Y;
            session.X = tx;
            session.Y = ty;
            map.Aoi.UpdatePosition(session, oldX, oldY, tx, ty, out _, out _);
            SendPositionPullback(session, tx, ty, session.Direction);
            BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTRUN, session.SessionId, tx, ty, session.Direction, session.CharacterName, includeSelf: true);
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"【瞬息移动】已瞬移至 ({tx}, {ty})");
            return;
        }

        // 11. 诱惑之光 (MagicId = 20, 兼容 11 目标为怪物)
        if (magicId == 20 || (magicId == 11 && (targetUid != 0 || targetX != 0 || targetY != 0)))
        {
            if (RegenEngine != null)
            {
                var monster = RegenEngine.FindMonsterByUid(targetUid) ?? RegenEngine.FindMonsterAt(session.CurrentMapId, targetX, targetY);
                if (monster != null && !monster.IsDead)
                {
                    bool isBossOrUndead = monster.Name.Contains("教主") || monster.Name.Contains("骷髅") || monster.Name.Contains("尸");
                    var result = MageCombatEngine.Temptation.Evaluate(session.State.Level, 3, 20, 0, isBossOrUndead, Random.Shared.Next(100));
                    if (result.Outcome == MageCombatEngine.Temptation.TameOutcome.Paralyzed)
                    {
                        monster.NextActionTimeMs = nowMs + result.ParalyzeDurationSec * 1000;
                        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, result.Description);
                    }
                    else if (result.Outcome == MageCombatEngine.Temptation.TameOutcome.Tamed)
                    {
                        monster.TargetSessionId = 0; // 解除仇恨
                        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, result.Description);
                    }
                    else
                    {
                        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, result.Description);
                    }
                }
            }
            return;
        }

        // 12. 线性穿透法术：地狱火 (MagicId = 9, 5格) 与 疾光电影 (MagicId = 23, 8格)
        if (magicId == 9 || magicId == 23)
        {
            int maxSteps = magicId == 23 ? 8 : 5;
            var (stepDx, stepDy) = MonsterRegenEngine.GetDirOffset(session.Direction);
            if (stepDx == 0 && stepDy == 0) (stepDx, stepDy) = (0, -1);

            for (int step = 1; step <= maxSteps; step++)
            {
                int lineX = session.X + stepDx * step;
                int lineY = session.Y + stepDy * step;
                if (!map.IsWalkable(lineX, lineY)) break; // 遇障碍阻断

                // 穿透结算玩家
                lock (_ninePatchBuffer)
                {
                    map.Aoi.GetEntitiesInNinePatch(lineX, lineY, _ninePatchBuffer);
                    for (int i = 0; i < _ninePatchBuffer.Count; i++)
                    {
                        var target = _ninePatchBuffer[i];
                        if (target.SessionId != session.SessionId && target.X == lineX && target.Y == lineY && target.State != null && target.State.Hp > 0)
                        {
                            int dmg = magicId == 23
                                ? OfficialClientRulesEngine.CombatMath.CalculateMagicDamage(35, 60, 0, 8, OfficialClientRulesEngine.DelphiRandom.Shared)
                                : OfficialClientRulesEngine.CombatMath.CalculateMagicDamage(15, 30, 0, 4, OfficialClientRulesEngine.DelphiRandom.Shared);

                            var (finalHp, finalMp, died, revived, absorbed, shieldBroken) = target.State.TakeCombatDamageFull(dmg, nowMs);
                            BroadcastToNinePatch(target, ProtocolConsts.SM_OBJECTSTRUCK, target.SessionId, target.X, target.Y, dmg, target.CharacterName, includeSelf: true);
                            target.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, target.State.Hp, target.State.MaxHp, target.State.Mp, target.State.MaxMp);
                            if (died)
                            {
                                session.State.AddPkMurder();
                                BroadcastToNinePatch(target, ProtocolConsts.SM_DEATH_ACTION_DELPHI, target.SessionId, target.X, target.Y, 0, target.CharacterName, includeSelf: true);
                            }
                        }
                    }
                }

                // 穿透结算怪物
                if (RegenEngine != null)
                {
                    var m = RegenEngine.FindMonsterAt(session.CurrentMapId, lineX, lineY);
                    if (m != null && !m.IsDead)
                    {
                        int dmg = magicId == 23
                            ? OfficialClientRulesEngine.CombatMath.CalculateMagicDamage(35, 60, 0, 8, OfficialClientRulesEngine.DelphiRandom.Shared)
                            : OfficialClientRulesEngine.CombatMath.CalculateMagicDamage(15, 30, 0, 4, OfficialClientRulesEngine.DelphiRandom.Shared);

                        m.Hp = Math.Max(0, m.Hp - dmg);
                        BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, m.Uid, m.X, m.Y, dmg, m.Name, includeSelf: true);
                        if (m.IsDead)
                        {
                            RegenEngine.OnMonsterDied(m.Uid);
                            BroadcastToNinePatch(session, ProtocolConsts.SM_DEATH_ACTION_DELPHI, m.Uid, m.X, m.Y, 0, m.Name, includeSelf: true);
                            OnMonsterKilledByPlayer(session, m);
                        }
                    }
                }
            }
            return;
        }

        // 13. 范围法术：爆裂火焰 (MagicId = 15) 与 冰咆哮 (MagicId = 32 或 33, 100%无视魔躲)
        if (magicId == 15 || magicId == 32 || magicId == 33)
        {
            for (int ax = targetX - 1; ax <= targetX + 1; ax++)
            {
                for (int ay = targetY - 1; ay <= targetY + 1; ay++)
                {
                    lock (_ninePatchBuffer)
                    {
                        map.Aoi.GetEntitiesInNinePatch(ax, ay, _ninePatchBuffer);
                        for (int i = 0; i < _ninePatchBuffer.Count; i++)
                        {
                            var target = _ninePatchBuffer[i];
                            if (target.SessionId != session.SessionId && target.X == ax && target.Y == ay && target.State != null && target.State.Hp > 0)
                            {
                                int dmg = (magicId == 32 || magicId == 33)
                                    ? MageCombatEngine.Blizzard.CalculateDamage(55, target.State.Mac)
                                    : OfficialClientRulesEngine.CombatMath.CalculateMagicDamage(20, 35, 0, 5, OfficialClientRulesEngine.DelphiRandom.Shared);

                                var (finalHp, finalMp, died, revived, absorbed, shieldBroken) = target.State.TakeCombatDamageFull(dmg, nowMs);
                                BroadcastToNinePatch(target, ProtocolConsts.SM_OBJECTSTRUCK, target.SessionId, target.X, target.Y, dmg, target.CharacterName, includeSelf: true);
                                target.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, target.State.Hp, target.State.MaxHp, target.State.Mp, target.State.MaxMp);
                                if (died)
                                {
                                    session.State.AddPkMurder();
                                    BroadcastToNinePatch(target, ProtocolConsts.SM_DEATH_ACTION_DELPHI, target.SessionId, target.X, target.Y, 0, target.CharacterName, includeSelf: true);
                                }
                            }
                        }
                    }

                    if (RegenEngine != null)
                    {
                        var m = RegenEngine.FindMonsterAt(session.CurrentMapId, ax, ay);
                        if (m != null && !m.IsDead)
                        {
                            int dmg = (magicId == 32 || magicId == 33)
                                ? MageCombatEngine.Blizzard.CalculateDamage(55, 5)
                                : OfficialClientRulesEngine.CombatMath.CalculateMagicDamage(20, 35, 0, 5, OfficialClientRulesEngine.DelphiRandom.Shared);

                            m.Hp = Math.Max(0, m.Hp - dmg);
                            BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, m.Uid, m.X, m.Y, dmg, m.Name, includeSelf: true);
                            if (m.IsDead)
                            {
                                RegenEngine.OnMonsterDied(m.Uid);
                                BroadcastToNinePatch(session, ProtocolConsts.SM_DEATH_ACTION_DELPHI, m.Uid, m.X, m.Y, 0, m.Name, includeSelf: true);
                                OnMonsterKilledByPlayer(session, m);
                            }
                        }
                    }
                }
            }
            return;
        }

        // 14. 圣言术 (MagicId = 24)
        if (magicId == 24)
        {
            if (RegenEngine != null)
            {
                var monster = RegenEngine.FindMonsterByUid(targetUid) ?? RegenEngine.FindMonsterAt(session.CurrentMapId, targetX, targetY);
                if (monster != null && !monster.IsDead)
                {
                    bool isUndead = monster.Name.Contains("尸") || monster.Name.Contains("骷髅") || monster.Name.Contains("骨");
                    if (isUndead && !monster.Name.Contains("教主"))
                    {
                        int levelDiff = session.State.Level - 20;
                        int killChance = Math.Clamp(40 + levelDiff * 5, 20, 90);
                        if (Random.Shared.Next(100) < killChance)
                        {
                            monster.Hp = 0;
                            BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, monster.Uid, monster.X, monster.Y, monster.MaxHp, monster.Name, includeSelf: true);
                            RegenEngine.OnMonsterDied(monster.Uid);
                            BroadcastToNinePatch(session, ProtocolConsts.SM_DEATH_ACTION_DELPHI, monster.Uid, monster.X, monster.Y, 0, monster.Name, includeSelf: true);
                            OnMonsterKilledByPlayer(session, monster);
                            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"【圣言术】神圣光芒净化了 {monster.Name}！");
                            return;
                        }
                    }
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "【圣言术】圣言术未产生秒杀效果。");
                }
            }
            return;
        }

        // 15. 雷电术 (MagicId = 10) 视线射线穿透检测 (防穿墙)
        if (magicId == 10)
        {
            if (!map.CheckLineOfSight(session.X, session.Y, targetX, targetY))
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "障碍物阻挡了施法视线！");
                return;
            }
        }

        // 16. 常规单体法术 (雷电术=10, 火球术=1, 大火球=5, 灵魂火符=13)
        ClientSession? defender = null;
        lock (_ninePatchBuffer)
        {
            map.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
            for (int i = 0; i < _ninePatchBuffer.Count; i++)
            {
                var ent = _ninePatchBuffer[i];
                if (ent.SessionId != session.SessionId && (ent.SessionId == targetUid || (ent.X == targetX && ent.Y == targetY)))
                {
                    defender = ent;
                    break;
                }
            }
        }
        if (defender != null && defender.State != null)
        {
            int magicDmg = OfficialClientRulesEngine.CombatMath.CalculateMagicDamage(20, 35, 0, 5, OfficialClientRulesEngine.DelphiRandom.Shared);
            if (defender.State.HasRedPoison)
            {
                magicDmg = TaoistCombatEngine.ApplyRedPoisonVulnerability(magicDmg, hasRedPoison: true);
            }
            var (finalHp, finalMp, died, revived, absorbed, shieldBroken) = defender.State.TakeCombatDamageFull(magicDmg, nowMs);
            BroadcastToNinePatch(defender, ProtocolConsts.SM_OBJECTSTRUCK, defender.SessionId, defender.X, defender.Y, magicDmg, defender.CharacterName, includeSelf: true);
            defender.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, defender.State.Hp, defender.State.MaxHp, defender.State.Mp, defender.State.MaxMp);
            if (died)
            {
                session.State.AddPkMurder();
                BroadcastToNinePatch(defender, ProtocolConsts.SM_DEATH_ACTION_DELPHI, defender.SessionId, defender.X, defender.Y, 0, defender.CharacterName, includeSelf: true);
            }
        }
        else if (RegenEngine != null)
        {
            var monster = RegenEngine.FindMonsterByUid(targetUid) ?? RegenEngine.FindMonsterAt(session.CurrentMapId, targetX, targetY);
            if (monster != null && !monster.IsDead)
            {
                int magicDmg = OfficialClientRulesEngine.CombatMath.CalculateMagicDamage(20, 35, 0, 5, OfficialClientRulesEngine.DelphiRandom.Shared);
                monster.Hp = Math.Max(0, monster.Hp - magicDmg);
                BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, monster.Uid, monster.X, monster.Y, magicDmg, monster.Name, includeSelf: true);
                if (monster.IsDead)
                {
                    RegenEngine.OnMonsterDied(monster.Uid);
                    BroadcastToNinePatch(session, ProtocolConsts.SM_DEATH_ACTION_DELPHI, monster.Uid, monster.X, monster.Y, 0, monster.Name, includeSelf: true);
                    OnMonsterKilledByPlayer(session, monster);
                }
            }
        }
    }

    /// <summary>
    /// 服务端周期性 Tick 毒素结算 (每 2.5 秒触发一次绿毒扣血并清空回血池)
    /// </summary>
    public void TickWorldPoisons(long nowMs, IEnumerable<ClientSession> allSessions)
    {
        foreach (var session in allSessions)
        {
            if (session.State != null)
            {
                var (tookDmg, dmg, greenExpired, redExpired) = session.State.ProcessPoisonTick(nowMs);
                if (tookDmg)
                {
                    BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, session.SessionId, session.X, session.Y, dmg, session.CharacterName, includeSelf: true);
                    session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
                    if (session.State.Hp <= 0)
                    {
                        BroadcastToNinePatch(session, ProtocolConsts.SM_DEATH_ACTION_DELPHI, session.SessionId, session.X, session.Y, 0, session.CharacterName, includeSelf: true);
                    }
                }
                if (greenExpired)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "绿毒效果已消散。");
                }
                if (redExpired)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "红毒效果已消散。");
                }
            }
        }
    }

    /// <summary>
    /// 服务端权威穿戴装备校验 (负重与腕力硬性拦截)
    /// </summary>
    public bool HandleWearItem(ClientSession session, int bagSlot, OfficialEquipSlot equipSlot)
    {
        if (session.State == null) return false;
        if (bagSlot < 0 || bagSlot >= session.State.BagItems.Count) return false;

        var item = session.State.BagItems[bagSlot];
        if (!session.State.CheckCanEquipAuthoritative(equipSlot, item.Weight, out string reason))
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"穿戴失败：{reason}");
            return false;
        }

        session.State.BagItems.RemoveAt(bagSlot);
        var oldItem = session.State.Equipment[(int)equipSlot];
        session.State.Equipment[(int)equipSlot] = item;
        if (oldItem != null)
        {
            session.State.BagItems.Add(oldItem);
        }
        session.State.RecalculateAuthoritativeWeights(session.State.Gold);
        session.State.RecalculateAttributes();
        return true;
    }

    private void HandleWearItemPacket(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: bagSlot/equipSlot
        int slash = payload.IndexOf('/');
        if (slash < 0) return;
        if (int.TryParse(payload[..slash], out int bagSlot) &&
            byte.TryParse(payload[(slash + 1)..], out byte equipSlot))
        {
            if (equipSlot < 9)
            {
                bool success = HandleWearItem(session, bagSlot, (OfficialEquipSlot)equipSlot);
                if (success)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_TAKEON_OK, session.SessionId, equipSlot, 0, 0);
                    SendAbility(session);
                }
                else
                {
                    session.EnqueuePacket(ProtocolConsts.SM_TAKEON_FAIL, session.SessionId, equipSlot, 0, 0);
                }
            }
        }
    }

    private void HandleTakeOffItemPacket(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null) return;
        int slash = payload.IndexOf('/');
        if (slash >= 0) payload = payload[..slash];
        if (!byte.TryParse(payload, out byte slot) || slot >= 9) return;

        var equip = session.State.Equipment[slot];
        if (equip == null)
        {
            session.EnqueuePacket(ProtocolConsts.SM_TAKEOFF_FAIL, session.SessionId, slot, 0, 0);
            return;
        }

        // 校验背包是否有空间 (40格上限)
        if (session.State.BagItems.Count >= 40)
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "背包已满，无法卸下装备");
            session.EnqueuePacket(ProtocolConsts.SM_TAKEOFF_FAIL, session.SessionId, slot, 0, 0);
            return;
        }

        // 校验背包负重 (卸下后放入背包)
        if (session.State.BagWeight + equip.Weight > session.State.MaxBagWeight)
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "背包负重不足，无法卸下装备");
            session.EnqueuePacket(ProtocolConsts.SM_TAKEOFF_FAIL, session.SessionId, slot, 0, 0);
            return;
        }

        session.State.Equipment[slot] = null;
        session.State.BagItems.Add(equip);
        session.State.RecalculateAuthoritativeWeights(session.State.Gold);
        session.State.RecalculateAttributes();

        session.EnqueuePacket(ProtocolConsts.SM_TAKEOFF_OK, session.SessionId, slot, 0, 0);
        SendAbility(session);
    }

    private void HandleDropItemPacket(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null) return;
        // 格式: bagSlot 或 bagSlot/count
        int slash = payload.IndexOf('/');
        ReadOnlySpan<char> slotSpan = slash >= 0 ? payload[..slash] : payload;
        if (!int.TryParse(slotSpan, out int bagSlot) || bagSlot < 0 || bagSlot >= session.State.BagItems.Count)
        {
            return;
        }

        var item = session.State.BagItems[bagSlot];
        session.State.BagItems.RemoveAt(bagSlot);
        session.State.RecalculateAuthoritativeWeights(session.State.Gold);
        session.State.RecalculateAttributes();

        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
        var groundItem = new GroundItemEntity
        {
            Uid = System.Threading.Interlocked.Increment(ref _nextGroundItemUid),
            Name = item.Name,
            X = session.X,
            Y = session.Y,
            OwnerSessionId = 0, // 玩家主动丢弃，无归属保护期
            DropTickMs = Environment.TickCount64,
            Weight = item.Weight
        };
        map.AddGroundItem(groundItem);

        // 广播地面显示物品: SM_ITEMSHOW (625)
        BroadcastToNinePatch(session, ProtocolConsts.SM_ITEMSHOW, groundItem.Uid, session.X, session.Y, item.Shape, item.Name, includeSelf: true);
        SendAbility(session);
    }

    private void HandleDropGoldPacket(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null) return;
        int slash = payload.IndexOf('/');
        if (slash >= 0) payload = payload[..slash];
        if (!int.TryParse(payload, out int amount) || amount <= 0 || session.State.Gold < amount)
        {
            return;
        }

        session.State.Gold -= amount;
        session.State.RecalculateAuthoritativeWeights(session.State.Gold);

        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
        var goldEntity = new GroundItemEntity
        {
            Uid = System.Threading.Interlocked.Increment(ref _nextGroundItemUid),
            Name = "金币",
            X = session.X,
            Y = session.Y,
            OwnerSessionId = 0,
            DropTickMs = Environment.TickCount64,
            Weight = Math.Max(1, amount / 1000)
        };
        map.AddGroundItem(goldEntity);

        BroadcastToNinePatch(session, ProtocolConsts.SM_ITEMSHOW, goldEntity.Uid, session.X, session.Y, 111, $"金币({amount})", includeSelf: true);
        SendAbility(session);
    }

    private void HandleAdjustBonusPacket(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null || session.State.BonusPoint <= 0) return;
        // 格式: bonusType (0=DC, 1=MC, 2=SC, 3=AC, 4=MAC, 5=HP, 6=MP)
        int slash = payload.IndexOf('/');
        if (slash >= 0) payload = payload[..slash];
        if (int.TryParse(payload, out int bonusType))
        {
            session.State.BonusPoint--;
            switch (bonusType)
            {
                case 0: session.State.BonusDc++; break;
                case 1: session.State.BonusMc++; break;
                case 2: session.State.BonusSc++; break;
                case 3: session.State.BonusAc++; break;
                case 4: session.State.BonusMac++; break;
                case 5: session.State.BonusHp += 10; session.State.Hp += 10; break;
                case 6: session.State.BonusMp += 10; session.State.Mp += 10; break;
            }
            session.State.RecalculateAttributes();
            SendAbility(session);
        }
    }

    public void SendAbility(ClientSession session)
    {
        if (session.State == null) return;
        // SM_ABILITY: level, hp, maxhp, mp, maxmp, exp, maxexp, weight, maxweight, wearweight, maxwearweight, handweight, maxhandweight
        session.EnqueuePacket(ProtocolConsts.SM_ABILITY, session.State.Level, session.State.Hp, session.State.MaxHp, session.State.Mp,
            $"{session.State.MaxMp}/{session.State.Exp}/{session.State.MaxExp}/{session.State.BagWeight}/{session.State.MaxBagWeight}/{session.State.WearWeight}/{session.State.MaxWearWeight}/{session.State.HandWeight}/{session.State.MaxHandWeight}/{session.State.Gold}/{session.State.BonusPoint}");
    }

    private void OnMonsterKilledByPlayer(ClientSession killer, MonsterInstance monster)
    {
        if (killer.State == null) return;
        int exp = Math.Max(10, monster.MaxHp * 2);
        bool leveled = killer.State.GainExp(exp);

        killer.EnqueuePacket(ProtocolConsts.SM_EXPERIENCE, (int)killer.State.Exp, (int)killer.State.MaxExp, 0, 0);
        killer.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"获得经验值: {exp}");

        if (leveled)
        {
            BroadcastToNinePatch(killer, ProtocolConsts.SM_LEVELUP_DELPHI, killer.SessionId, killer.X, killer.Y, killer.State.Level, killer.CharacterName, includeSelf: true);
            killer.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"恭喜！你升到了 {killer.State.Level} 级！");
        }
        SendAbility(killer);
    }

    private void HandleButchItemPacket(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null || session.State.Hp <= 0) return;
        // 格式: targetUid/tx/ty/ 或 targetUid (CM_BUTCHITEM 1007)
        int targetUid = 0;
        int tx = session.X;
        int ty = session.Y;

        int slash1 = payload.IndexOf('/');
        if (slash1 > 0)
        {
            int.TryParse(payload[..slash1], out targetUid);
            var rest = payload[(slash1 + 1)..];
            int slash2 = rest.IndexOf('/');
            if (slash2 > 0)
            {
                int.TryParse(rest[..slash2], out tx);
                var rest2 = rest[(slash2 + 1)..];
                int slash3 = rest2.IndexOf('/');
                if (slash3 > 0) int.TryParse(rest2[..slash3], out ty);
                else int.TryParse(rest2, out ty);
            }
            else
            {
                int.TryParse(rest, out tx);
            }
        }
        else
        {
            int.TryParse(payload, out targetUid);
        }

        // 距离校验: 必须在相邻 2 格内 (官方 IDA 割肉范围)
        if (Math.Abs(session.X - tx) > 2 || Math.Abs(session.Y - ty) > 2)
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "距离太远，无法割肉！");
            return;
        }

        // 查找尸体
        var corpse = RegenEngine?.FindCorpse(targetUid) ?? RegenEngine?.FindCorpseAt(session.CurrentMapId, tx, ty);
        if (corpse == null || !corpse.IsDead)
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "目标不存在或尚未死亡！");
            return;
        }

        var monDef = OfficialMonsterCompendium.GetByName(corpse.Name);
        if (monDef == null || !monDef.CanButcherMeat)
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "这具尸体上没有可以割取的肉。");
            return;
        }

        if (corpse.IsButchered)
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "这具尸体已经被割空了。");
            return;
        }

        // 权威割肉
        corpse.IsButchered = true;
        int quality = corpse.Name.Contains("鹿") ? Random.Shared.Next(10, 21) : Random.Shared.Next(5, 16);
        string meatName = $"{corpse.Name}肉";

        // 背包容量与负重校验
        if (session.State.BagItems.Count < 40 && session.State.BagWeight + 1 <= session.State.MaxBagWeight)
        {
            var meatItem = new EquipItem
            {
                Uid = (int)(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() % 1_000_000_000) + Random.Shared.Next(1, 999),
                Name = meatName,
                Weight = 1,
                Dura = quality * 1000,
                MaxDura = 20000
            };
            session.State.BagItems.Add(meatItem);
            session.State.BagWeight += 1;
            session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
        }
        else
        {
            // 掉落在地表
            var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
            var groundMeat = new GroundItemEntity
            {
                Uid = 80000 + Random.Shared.Next(1, 9999),
                Name = meatName,
                X = session.X,
                Y = session.Y,
                Weight = 1,
                DropTickMs = Environment.TickCount64
            };
            map.AddGroundItem(groundMeat);
            BroadcastToNinePatch(session, ProtocolConsts.SM_ITEMSHOW, groundMeat.Uid, groundMeat.X, groundMeat.Y, 0, groundMeat.Name, includeSelf: true);
        }

        // 回复客户端割肉成功与广播 (SM_BUTCH 665, Recog = corpse.Uid, 负载 = 肉品品质描述)
        session.EnqueuePacket(ProtocolConsts.SM_BUTCH, corpse.Uid, 0, 0, 0, $"品质 {quality} 的{meatName}");
        BroadcastToNinePatch(session, ProtocolConsts.SM_SITDOWN, session.SessionId, session.X, session.Y, session.Direction, session.CharacterName, includeSelf: false);
    }

    private bool CheckAndConsumeSpellReagent(ClientSession session, short magicId)
    {
        if (session.State == null) return false;

        // 1. 施毒术 (MagicId = 6): 必须消耗灰色药粉 (绿毒) 或黄色药粉 (红毒)
        if (magicId == 6)
        {
            var powder = session.State.BagItems.Find(i => i.Name.Contains("药粉") || i.Name.Contains("毒药"));
            if (powder == null)
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "缺少施毒药粉，施法失败！");
                return false;
            }

            if (powder.Dura > 1)
            {
                powder.Dura -= 1;
            }
            else
            {
                session.State.BagItems.Remove(powder);
                session.State.BagWeight = Math.Max(0, session.State.BagWeight - powder.Weight);
                session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"{powder.Name}已用尽！");
            }
            return true;
        }

        // 2. 需消耗护身符的道士法术:
        // 灵魂火符 (13), 召唤骷髅 (17), 召唤神兽 (30), 幽灵盾 (14), 神圣战甲术 (15), 隐身术 (18), 集体隐身术 (19), 困魔咒 (16)
        if (magicId is 13 or 17 or 30 or 14 or 15 or 18 or 19 or 16)
        {
            // 优先检查装备栏照明/护符槽位 (官方 OfficialEquipSlot.Lighting = 2)
            var eqAmulet = session.State.Equipment[(int)OfficialEquipSlot.Lighting];
            if (eqAmulet != null && eqAmulet.Name.Contains("符"))
            {
                if (eqAmulet.Dura > 1)
                {
                    eqAmulet.Dura -= 1;
                }
                else
                {
                    session.State.Equipment[(int)OfficialEquipSlot.Lighting] = null;
                    session.State.WearWeight = Math.Max(0, session.State.WearWeight - eqAmulet.Weight);
                    session.State.RecalculateAttributes();
                    session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "护身符已耗尽！");
                }
                return true;
            }

            // 装备栏未佩戴，检查背包中的护身符
            var bagAmulet = session.State.BagItems.Find(i => i.Name.Contains("护身符") || i.Name.Contains("符"));
            if (bagAmulet != null)
            {
                if (bagAmulet.Dura > 1)
                {
                    bagAmulet.Dura -= 1;
                }
                else
                {
                    session.State.BagItems.Remove(bagAmulet);
                    session.State.BagWeight = Math.Max(0, session.State.BagWeight - bagAmulet.Weight);
                    session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"{bagAmulet.Name}已耗尽！");
                }
                return true;
            }

            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "缺少护身符，施法失败！");
            return false;
        }

        return true;
    }

    public void HandleOpenDoorPacket(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null || session.State.Hp <= 0) return;
        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;

        // 格式: doorX/doorY/ 或缺省取正前方 1 格 (CM_OPENDOOR 1002)
        int doorX = session.X;
        int doorY = session.Y;
        int slash = payload.IndexOf('/');
        if (slash > 0)
        {
            int.TryParse(payload[..slash], out doorX);
            var rest = payload[(slash + 1)..];
            int slash2 = rest.IndexOf('/');
            if (slash2 >= 0) rest = rest[..slash2];
            int.TryParse(rest, out doorY);
        }
        else
        {
            var (fx, fy) = GetFrontCoord(session.X, session.Y, session.Direction);
            doorX = fx;
            doorY = fy;
        }

        // 权威距离检定: 玩家与门必须相邻 (<= 1 格)
        if (Math.Abs(session.X - doorX) > 1 || Math.Abs(session.Y - doorY) > 1) return;

        long nowMs = Environment.TickCount64;
        bool opened = map.OpenDoor(doorX, doorY, nowMs);
        if (opened)
        {
            // 向九宫格广播开门成功动画 (SM_OPENDOOR 612)
            BroadcastToNinePatch(session, ProtocolConsts.SM_OPENDOOR, doorX, doorY, 1, 0, "石门开启", includeSelf: true);
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "石门已被推开。");
        }
    }

    public void TickWorldDoors(long nowMs)
    {
        foreach (var map in _mapManager.AllMaps)
        {
            var closed = map.ProcessDoorTick(nowMs);
            for (int i = 0; i < closed.Count; i++)
            {
                var d = closed[i];
                // 广播关门协议 SM_CLOSEDOOR (614) 并恢复阻挡
                lock (_ninePatchBuffer)
                {
                    map.Aoi.GetEntitiesInNinePatch(d.X, d.Y, _ninePatchBuffer);
                    for (int j = 0; j < _ninePatchBuffer.Count; j++)
                    {
                        _ninePatchBuffer[j].EnqueuePacket(ProtocolConsts.SM_CLOSEDOOR, d.X, d.Y, 0, 0, "石门关闭");
                    }
                }
            }
        }
    }

    public void TriggerMapPassage(ClientSession session, MapPassage passage)
    {
        var oldMap = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
        var newMap = _mapManager.GetMap(passage.ToMapId) ?? _mapManager.DefaultMap;

        int oldX = session.X;
        int oldY = session.Y;

        // 1. 从原地图离开并广播实体消失
        oldMap.Aoi.Leave(session, oldX, oldY);
        BroadcastToNinePatch(session, ProtocolConsts.SM_ENTITYDISAPPEAR, session.SessionId, oldX, oldY, 0, session.CharacterName, includeSelf: false);

        // 2. 变更会话地图与物理坐标
        session.CurrentMapId = newMap.MapId;
        session.X = passage.ToX;
        session.Y = passage.ToY;

        // 3. 下发客户端地图切换与黑屏转场协议 (SM_NEWMAP_DELPHI 521)
        session.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, newMap.MapId, passage.ToX, passage.ToY, 0, newMap.Name);
        session.EnqueuePacket(ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, passage.ToX, passage.ToY, session.Direction, session.CharacterName);
        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"进入【{newMap.Name}】");

        // 4. 进入新地图 AOI 空间分区并广播登场
        newMap.Aoi.Enter(session, passage.ToX, passage.ToY);
        BroadcastToNinePatch(session, ProtocolConsts.SM_ENTITYAPPEAR, session.SessionId, passage.ToX, passage.ToY, session.Direction, session.CharacterName, includeSelf: false);
    }

    public static (int x, int y) GetFrontCoord(int x, int y, int dir)
    {
        return dir switch
        {
            0 => (x, y - 1),     // 北
            1 => (x + 1, y - 1), // 东北
            2 => (x + 1, y),     // 东
            3 => (x + 1, y + 1), // 东南
            4 => (x, y + 1),     // 南
            5 => (x - 1, y + 1), // 西南
            6 => (x - 1, y),     // 西
            7 => (x - 1, y - 1), // 西北
            _ => (x, y)
        };
    }

    #endregion
}
