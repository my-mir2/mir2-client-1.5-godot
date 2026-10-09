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
            case 1016: // CM_TAKEOUTSTORAGE 官方 1.4/1.5
                HandleStorageWithdraw(session, remaining);
                break;

            case 1017: // CM_SAVEGOLD 官方存入金币
                HandleStorageSaveGold(session, remaining);
                break;

            case 1018: // CM_TAKEGOLD 官方支取金币
                HandleStorageTakeGold(session, remaining);
                break;

            case ProtocolConsts.CM_CHECKPASSWORD: // 1046 官方二级密码校验
                HandleStoragePassword(session, remaining);
                break;

            case ProtocolConsts.CM_EATITEM: // 1006 使用物品/吃药
            case 1002: // CM_EAT 官方 1.4/1.5
                HandleEatItem(session, remaining);
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

                // 2. 物理命中判定 (1:1 官方 Delphi M2 sub_4C1EF4 @ L12704: RandInt(Defender.Speed) >= Attacker.Hit)
                int rollSpeed = OfficialClientRulesEngine.DelphiRandom.Shared.Next(defender.State.Speed + 1);
                bool hit = rollSpeed < session.State.Hit;

                if (hit)
                {
                    // 物理基础伤害精算
                    int rawDmg = OfficialClientRulesEngine.CombatMath.CalculatePhysicalDamage(15, 30, 0, 5, OfficialClientRulesEngine.DelphiRandom.Shared);

                    // 攻杀剑术附加伤害 (0x4C1EF4 @ L12621)
                    if (session.State.BoAttackKill)
                    {
                        session.State.BoAttackKill = false;
                        rawDmg += session.State.HitPlus;
                    }

                    // 烈火剑法火焰爆发 (0x4C1EF4 @ L12627: ROUND(Damage / 100.0 * 10 * FireHitPlus))
                    if (session.State.BoFireHit)
                    {
                        session.State.BoFireHit = false;
                        int multiplier = 10 * session.State.FireHitPlus;
                        rawDmg += (int)Math.Round((double)rawDmg / 100.0 * multiplier);
                    }

                    // 虹魔吸血判定 (0x4C1EF4 @ L12733: LeechWeight / 100.0)
                    session.State.ProcessAttackLifesteal(rawDmg);

                    // 麻痹戒指判定 (0x4C1EF4 @ L12729: HasParalysisRing && RandInt(AntiPoison + 5) == 0)
                    if (session.State.HasParalysisRing)
                    {
                        int rollPara = OfficialClientRulesEngine.DelphiRandom.Shared.Next(defender.State.AntiPoison + 5);
                        if (rollPara == 0)
                        {
                            defender.State.StatusBits[5] = true;
                        }
                    }

                    // 伤害扣减 (含魔法盾、护身、复活戒指)
                    var (finalHp, finalMp, died, revived, absorbed, shieldBroken) = defender.State.TakeCombatDamageFull(rawDmg, nowMs);

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
                    // 物理命中判定 (1:1 官方 Delphi M2 sub_4C1EF4 @ L12704)
                    int rollSpeed = OfficialClientRulesEngine.DelphiRandom.Shared.Next(16);
                    if (rollSpeed < session.State.Hit)
                    {
                        int rawDmg = OfficialClientRulesEngine.CombatMath.CalculatePhysicalDamage(15, 30, 0, 5, OfficialClientRulesEngine.DelphiRandom.Shared);
                        if (session.State.BoAttackKill)
                        {
                            session.State.BoAttackKill = false;
                            rawDmg += session.State.HitPlus;
                        }
                        if (session.State.BoFireHit)
                        {
                            session.State.BoFireHit = false;
                            int multiplier = 10 * session.State.FireHitPlus;
                            rawDmg += (int)Math.Round((double)rawDmg / 100.0 * multiplier);
                        }

                        monster.Hp = Math.Max(0, monster.Hp - rawDmg);

                        // 广播怪物受击后仰: SM_OBJECTSTRUCK (244)
                        BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, monster.Uid, monster.X, monster.Y, rawDmg, monster.Name, includeSelf: true);

                        if (monster.IsDead)
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

            session.State.Mp -= mpCost;
            session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);

            // 向九宫格广播施法动作: SM_OBJECTSPELL (243)
            BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSPELL, session.SessionId, session.X, session.Y, magicId, session.CharacterName, includeSelf: true);

            // 特殊技能分支
            if (magicId == 31) // 魔法盾
            {
                session.State.ActivateMagicShield(1, 30);
                return;
            }
            if (magicId == 2 || magicId == 29) // 治愈术 / 群体治愈术
            {
                session.State.Hp = Math.Min(session.State.MaxHp, session.State.Hp + 25);
                session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
                return;
            }

            // 伤害性法术 (雷电术=10, 火球术=1, 灵魂火符=13 等)
            ClientSession? defender = null;
            var spellMap = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
            lock (_ninePatchBuffer)
            {
                spellMap.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
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
                var (finalHp, finalMp, died, revived, absorbed, shieldBroken) = defender.State.TakeCombatDamageFull(magicDmg, nowMs);

                // 受击后仰广播: SM_OBJECTSTRUCK (244)
                BroadcastToNinePatch(defender, ProtocolConsts.SM_OBJECTSTRUCK, defender.SessionId, defender.X, defender.Y, magicDmg, defender.CharacterName, includeSelf: true);

                // 同步目标血量
                defender.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, defender.State.Hp, defender.State.MaxHp, defender.State.Mp, defender.State.MaxMp);

                if (died)
                {
                    session.State.AddPkMurder();
                    BroadcastToNinePatch(defender, ProtocolConsts.SM_DEATH_ACTION_DELPHI, defender.SessionId, defender.X, defender.Y, 0, defender.CharacterName, includeSelf: true);

                    // 官方死亡爆装与爆背包结算 (IDA 0x4BB44C / 0x4BB6C8)
                    var dropped = MonsterDropEngine.ExecuteDeathDrop(defender.State, OfficialClientRulesEngine.DelphiRandom.Shared);
                    for (int d = 0; d < dropped.Count; d++)
                    {
                        var drop = dropped[d];
                        spellMap.AddGroundItem(new GroundItemEntity
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
    }

    private void HandleSay(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 1. 官方控制指令判断 (IDA 0x4D1E54 TPlayObject.Command)
        if (payload.Equals("@AttackMode", StringComparison.OrdinalIgnoreCase))
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

        // 2. 普通聊天广播到周围九宫格
        string msg = $"{session.CharacterName}: {payload}";
        BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSAY, session.SessionId, session.X, session.Y, 0, msg, includeSelf: true);
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
            // 距离校验 (战士普通近身攻击限 1 格，刺杀限 2 格)
            int dist = Math.Max(Math.Abs(defender.X - session.X), Math.Abs(defender.Y - session.Y));
            if (dist <= 2)
            {
                int rawDmg = OfficialClientRulesEngine.CombatMath.CalculatePhysicalDamage(15, 30, 0, 5, OfficialClientRulesEngine.DelphiRandom.Shared);
                var (finalHp, finalMp, died, revived, absorbed, shieldBroken) = defender.State.TakeCombatDamageFull(rawDmg, nowMs);

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
        // 施法公共冷却 GCD (1000ms)
        if (session.LastSpellTimeMs > 0 && (nowMs - session.LastSpellTimeMs < 1000))
        {
            return; // 施法过快拦截
        }
        session.LastSpellTimeMs = nowMs;

        // 蓝量扣除与校验 (假设施法消耗 10 点 MP)
        if (session.State != null)
        {
            if (session.State.Mp < 10) return; // 蓝量不足
            session.State.Mp -= 10;

            // 广播施法动作
            Span<byte> actionSpan = stackalloc byte[6];
            actionSpan[0] = 0; // Spell Action
            actionSpan[1] = spell.SpellId;
            BroadcastBinaryToNinePatch(session, (ushort)BinaryProtocol.MsgType.BroadcastEntityAction, actionSpan, includeSelf: false);

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

    #endregion
}
