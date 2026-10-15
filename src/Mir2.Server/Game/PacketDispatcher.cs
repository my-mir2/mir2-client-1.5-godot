using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Server.Persistence;
using Mir2.Server.Logging;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2.Shared.Protocols.Modern;
using Mir2.Shared.Rules;

namespace Mir2.Server.Game;

/// <summary>
/// 盛大官方 1.5 原版网络封包路由与分发中心 (M2 PacketDispatcher)
/// 严格 1:1 处理官方 CM_ 上行协议，并驱动服务端物理世界与九宫格 AOI 广播
/// </summary>
public sealed partial class PacketDispatcher
{
    private readonly MapManager _mapManager;
    private readonly System.Threading.Lock _ninePatchLock = new();
    private readonly List<ClientSession> _ninePatchBuffer = new(64);
    public MonsterRegenEngine? RegenEngine { get; set; }
    public NpcMerchantEngine NpcEngine { get; set; } = new();
    public TradeEngine TradeEngine { get; set; } = new();
    public GroupEngine GroupEngine { get; set; } = new();
    public GuildEngine GuildEngine { get; set; } = new();
    public SabukSiegeEngine SabukEngine { get; set; } = new();
    public EconomyAntiDupeEngine EcoEngine { get; set; } = new();
    public NpcScriptEngine ScriptEngine { get; set; } = new();
    public SafeZoneAndGuardEngine SafeZoneEngine { get; set; } = new();
    public EnvironmentLightingEngine LightingEngine { get; set; } = new();
    public StorageSecurityEngine StorageSecEngine { get; set; } = new();
    public ModernNpcPluginEngine ModernNpcEngine { get; set; } = new();
    public SabukTreasuryEngine TreasuryEngine { get; set; } = new();
    public GuardPatrolAndReturnEngine GuardPatrolEngine { get; set; } = new();
    public DataPersistenceEngine PersistenceEngine { get; set; } = new();
    public PostgreSqlPersistenceEngine? PgPersistenceEngine { get; set; }
    public SabukCastleGateEngine CastleGateEngine { get; set; } = new();
    public DynamicAoiLodEngine AoiLodEngine { get; set; } = new();
    public GuildWarEngine GuildWarEngine { get; set; } = new();
    public Mir2.Server.Security.SpeedHackWatchdogEngine SpeedWatchdog { get; set; } = new();
    public System.Collections.Concurrent.ConcurrentDictionary<int, ClientSession> ActiveSessions { get; } = new();
    private int _nextGroundItemUid = 10000;

    public static bool TryParseInt(ReadOnlySpan<char> span, out int value)
    {
        span = span.TrimEnd('/');
        int slash = span.IndexOf('/');
        if (slash >= 0) span = span[..slash];
        return int.TryParse(span, out value);
    }

    public PacketDispatcher(MapManager mapManager, MonsterRegenEngine? regenEngine = null, NpcMerchantEngine? npcEngine = null, TradeEngine? tradeEngine = null, GroupEngine? groupEngine = null, GuildEngine? guildEngine = null, SabukSiegeEngine? sabukEngine = null, EconomyAntiDupeEngine? ecoEngine = null)
    {
        _mapManager = mapManager;
        RegenEngine = regenEngine;
        if (RegenEngine != null)
        {
            RegenEngine.MonsterSpawned += mon =>
            {
                var map = _mapManager.GetMap(mon.MapId) ?? _mapManager.GetMap(0);
                if (map != null)
                {
                    BroadcastToCoords(map, mon.X, mon.Y, 220, mon.Uid, mon.X, mon.Y, mon.Direction, $"{mon.Name}\t{mon.X}\t{mon.Y}\t{mon.Direction}\t{mon.Appr}");
                }
            };
        }
        if (npcEngine != null) NpcEngine = npcEngine;
        if (tradeEngine != null) TradeEngine = tradeEngine;
        if (groupEngine != null) GroupEngine = groupEngine;
        if (guildEngine != null) GuildEngine = guildEngine;
        if (sabukEngine != null) SabukEngine = sabukEngine;
        if (ecoEngine != null) EcoEngine = ecoEngine;
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
        if (session == null) return;
        ActiveSessions[session.SessionId] = session;
        if (string.IsNullOrEmpty(rawPacket) || rawPacket.Length < 3) return;

        // 去除首尾 '#' 与 '!' (兼容遗留测试)
        ReadOnlySpan<char> span = rawPacket.AsSpan();
        if (span.Length > 0 && span[0] == '#') span = span[1..];
        if (span.Length > 0 && span[^1] == '!') span = span[..^1];

        // 尝试按斜杠分词
        short ident;
        ReadOnlySpan<char> remaining;
        int slashIdx = span.IndexOf('/');
        if (slashIdx >= 0)
        {
            if (!short.TryParse(span[..slashIdx], out ident)) return;
            remaining = span[(slashIdx + 1)..];
        }
        else
        {
            if (!short.TryParse(span, out ident)) return;
            remaining = ReadOnlySpan<char>.Empty;
        }

        DispatchIdent(session, ident, remaining);
    }

    /// <summary>
    /// 基于协议 Ident 权威派发 (供 HPSocket TCP-Pack 二进制包与上层直通调用)
    /// </summary>
    public void DispatchIdent(ClientSession session, short ident, ReadOnlySpan<char> remaining)
    {
        switch (ident)
        {
            case 80: // CM_QUERYUSERNAME
                if (session.State != null && TryParseInt(remaining, out int queryUid))
                {
                    var targetSession = ActiveSessions.TryGetValue(queryUid, out var ts) ? ts : null;
                    if (targetSession != null)
                    {
                        session.EnqueuePacket(ProtocolConsts.SM_USERNAME, targetSession.SessionId, 0, 0, 0, targetSession.CharacterName);
                    }
                }
                break;

            case 81: // CM_HEARTBEAT 心跳保活
                session.LastActiveTick = Environment.TickCount64;
                break;

            case 82: // CM_QUERYUSERNAME_EXT
                int slashExt = remaining.IndexOf('/');
                var uidSpan = slashExt >= 0 ? remaining[..slashExt] : remaining;
                if (session.State != null && int.TryParse(uidSpan, out int queryUidExt))
                {
                    var targetSession = ActiveSessions.TryGetValue(queryUidExt, out var ts) ? ts : null;
                    if (targetSession != null)
                    {
                        var guild = GuildEngine.GetGuildByPlayer(targetSession.CharacterName);
                        string guildStr = guild != null ? $" 所属行会：{guild.Name}" : "";
                        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"目标角色：{targetSession.CharacterName}{guildStr}");

                        if (targetSession.State != null)
                        {
                            var shapes = new int[9];
                            for (int i = 0; i < 9; i++)
                            {
                                shapes[i] = targetSession.State.Equipment[i]?.Shape ?? 0;
                            }
                            string shapeList = string.Join(",", shapes);
                            string inspectExtra = $"{targetSession.CharacterName}/{targetSession.State.Level}/{targetSession.State.Job}/{targetSession.State.Sex}|{shapeList}";
                            session.EnqueuePacket(751, queryUidExt, 0, 0, 0, inspectExtra);
                        }
                    }
                }
                break;

            case ProtocolConsts.CM_QUERYVERSION: // 2001 客户端查询版本 / 握手
                HandleQueryVersion(session);
                break;

            case ProtocolConsts.CM_LOGIN: // 2 账号密码登录
                HandleLogin(session, remaining);
                break;

            case ProtocolConsts.CM_NEWACCOUNT: // 2002 账号注册
                HandleNewAccount(session, remaining);
                break;

            case ProtocolConsts.CM_UPDATEACCOUNT: // 2003 修改密码
                HandleChangePassword(session, remaining);
                break;

            case ProtocolConsts.CM_QUERYCHRINFO: // 119 查询角色列表
                HandleQueryCharInfo(session);
                break;

            case ProtocolConsts.CM_SELECTSERVER: // 104 选择服务器
                session.EnqueuePacket(ProtocolConsts.SM_SELECTSERVER_OK, 0, 0, 0, 0);
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
                HandlePickUp(session, remaining);
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

            case ProtocolConsts.CM_USERREPAIRITEM: // 1023 / 47 铁匠铺普通修理
            case 47:
                HandleRepairItem(session, remaining, isSpecial: false);
                break;

            case ProtocolConsts.CM_REPAIRSPECIAL: // 48 铁匠铺特修
                HandleRepairItem(session, remaining, isSpecial: true);
                break;

            case ProtocolConsts.CM_QUERYREPAIRCOST: // 1024 查询修理金币消耗
                if (session.State != null && TryParseInt(remaining, out int repairUid))
                {
                    var (normalCost, specialCost, itemName) = NpcEngine.CalculateRepairCost(session, repairUid);
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, normalCost, specialCost, 0, 0, $"{itemName} 普通修理需要 {normalCost} 金币，特殊修理需要 {specialCost} 金币");
                }
                break;

            case ProtocolConsts.CM_QUERYSELLPRICE: // 1012 查询商店回收金币估价
                int slashSell = remaining.IndexOf('/');
                var sellSpan = slashSell >= 0 ? remaining[..slashSell] : remaining;
                if (session.State != null && int.TryParse(sellSpan, out int sellUid))
                {
                    var (sellPrice, itemName) = NpcEngine.CalculateSellPrice(session, sellUid);
                    session.EnqueuePacket(ProtocolConsts.SM_QUERYPRICE_REC, sellPrice, 0, 0, 0, itemName);
                }
                break;

            case ProtocolConsts.CM_USERGETDETAILITEM: // 1034 查询物品详细属性
                if (session.State != null && TryParseInt(remaining, out int detailUid))
                {
                    var equip = session.State.BagItems.Find(i => i.Uid == detailUid)
                                ?? (detailUid >= 0 && detailUid < session.State.Equipment.Length ? session.State.Equipment[detailUid] : null);
                    if (equip != null)
                    {
                        session.EnqueuePacket(ProtocolConsts.SM_SENDDETAILGOODSLIST, equip.Uid, equip.Dura, equip.MaxDura, equip.Weight, equip.Name);
                    }
                }
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

            case ProtocolConsts.CM_WANTMINIMAP: // 1033 官方按TAB请求小地图
                HandleWantMinimap(session);
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

            case ProtocolConsts.CM_MAGICKEYCHANGE: // 1008 技能快捷键设置同步 / 兼容老旧丢弃物品
            {
                int slash = remaining.IndexOf('/');
                if (slash > 0 && TryParseInt(remaining[..slash], out int magicId) && TryParseInt(remaining[(slash + 1)..], out int keyNum))
                {
                    var magic = session.State?.Magics.Find(m => m.MagicId == magicId);
                    if (magic != null)
                    {
                        magic.Key = (byte)keyNum;
                        session.EnqueuePacket(ProtocolConsts.SM_UPDATEUSERMAGIC, magic.MagicId, magic.Level, magic.Key, magic.CurTrain);
                        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"技能【{magic.Name}】快捷键已设置为 F{keyNum + 1}");
                    }
                }
                else
                {
                    HandleDropItemPacket(session, remaining);
                }
                break;
            }

            case ProtocolConsts.CM_DROPGOLD: // 1016 官方丢弃金币到地面
                HandleDropGoldPacket(session, remaining);
                break;

            case ProtocolConsts.CM_ADJUST_BONUS: // 1043 官方属性加点
                HandleAdjustBonusPacket(session, remaining);
                break;

            case ProtocolConsts.CM_BUTCHITEM: // 1007 官方生物尸体割肉 (Alt+点击)
                HandleButchItemPacket(session, remaining);
                break;

            case ProtocolConsts.CM_WIDEHIT: // 3024 战士半月弯刀横扫出刀
                HandleHit(session, remaining, isWideHit: true);
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

            case 1041: // CM_GUILD_UPDATE_RANK
                HandleGuildUpdateRank(session, remaining);
                break;

            case ProtocolConsts.CM_GUILD_ALLY_REQUEST: // 1044 发起行会结盟
                HandleGuildAllyRequest(session, remaining);
                break;

            case ProtocolConsts.CM_GUILD_BREAK_ALLY: // 1045 解除行会结盟
                HandleGuildBreakAlly(session, remaining);
                break;

            case ProtocolConsts.CM_THRUSTINGONOFF: // 3026 刺杀开关
                if (session.State != null)
                {
                    int slash = remaining.IndexOf('/');
                    var valSpan = slash >= 0 ? remaining[..slash] : remaining;
                    session.State.BoThrusting = valSpan.Length > 0 && valSpan[0] == '1';
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, session.State.BoThrusting ? "打开刺杀剑术" : "关闭刺杀剑术");
                }
                break;

            case ProtocolConsts.CM_HALFMOONONOFF: // 3027 半月开关
                if (session.State != null)
                {
                    int slash = remaining.IndexOf('/');
                    var valSpan = slash >= 0 ? remaining[..slash] : remaining;
                    session.State.BoHalfMoon = valSpan.Length > 0 && valSpan[0] == '1';
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, session.State.BoHalfMoon ? "打开半月弯刀" : "关闭半月弯刀");
                }
                break;

            case ProtocolConsts.CM_BIGHIT: // 3015 挥镐挖矿 (手持矿镐)
                HandleMineHit(session, remaining);
                break;

            case ProtocolConsts.CM_HEAVYHIT: // 3016 刺杀剑术隔位破防刺杀
            case ProtocolConsts.CM_LONGHIT: // 3019 刺杀剑气穿透
                HandleHeavyHit(session, remaining);
                break;

            case ProtocolConsts.CM_POWERHIT: // 3018 攻杀剑术物理重击
                HandlePowerHit(session, remaining);
                break;

            case ProtocolConsts.CM_NEWCHAR: // 104 创建角色 / 选择服务器
                if (remaining.IsEmpty)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_SELECTSERVER_OK, 0, 0, 0, 0);
                }
                else
                {
                    HandleNewChar(session, remaining);
                }
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

    // 登录暴力破解防护字典: 账号 -> (失败次数, 锁定截止时间戳)
    private readonly System.Collections.Concurrent.ConcurrentDictionary<string, (int FailCount, long LockUntilMs)> _loginAttempts = new(StringComparer.OrdinalIgnoreCase);

    // 内存账号备用存储 (支持离线单测): 账号 -> 密码
    private readonly System.Collections.Concurrent.ConcurrentDictionary<string, string> _memoryAccounts = new(StringComparer.OrdinalIgnoreCase);

    // 内存角色备用存储 (支持离线单测): 账号 -> 角色元组列表
    private readonly System.Collections.Concurrent.ConcurrentDictionary<string, List<(string CharName, int Job, int Sex, int Level)>> _memoryCharacters = new(StringComparer.OrdinalIgnoreCase);

    private void HandleNewAccount(ClientSession session, ReadOnlySpan<char> payload)
    {
        int slash = payload.IndexOf('/');
        string account = slash > 0 ? payload[..slash].ToString().Trim() : payload.ToString().Trim();
        string password = slash > 0 ? payload[(slash + 1)..].ToString().Trim() : "123456";
        HandleNewAccountInternal(session, account, password);
    }

    private unsafe void HandleBinaryCreateAccount(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < sizeof(BinaryProtocol.CreateAccountPayload)) return;
        fixed (byte* p = payload)
        {
            var intent = *(BinaryProtocol.CreateAccountPayload*)p;
            var (account, password) = intent.Read();
            HandleNewAccountInternal(session, account, password);
        }
    }

    private void HandleNewAccountInternal(ClientSession session, string account, string password)
    {
        if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(password))
        {
            session.EnqueuePacket(ProtocolConsts.SM_NEWID_FAIL, -1, 0, 0, 0, "账号或密码不能为空！");
            return;
        }

        if (account.Length > 12)
        {
            session.EnqueuePacket(ProtocolConsts.SM_NEWID_FAIL, -1, 0, 0, 0, "账号长度不能超过12位！");
            return;
        }

        bool success = false;
        if (PgPersistenceEngine != null)
        {
            success = PgPersistenceEngine.RegisterAccount(account, password);
        }
        else
        {
            success = _memoryAccounts.TryAdd(account, password);
        }

        if (success)
        {
            ServerLogger.Audit("REGISTER_SUCCESS", account, "", $"新玩家账号 {account} 注册成功");
            session.EnqueuePacket(ProtocolConsts.SM_NEWID_SUCCESS, 1, 0, 0, 0, account);
        }
        else
        {
            ServerLogger.Audit("REGISTER_FAILED", account, "", $"账号 {account} 已被占用，注册失败");
            session.EnqueuePacket(ProtocolConsts.SM_NEWID_FAIL, -2, 0, 0, 0, "该账号已存在，请更换账号名称！");
        }
    }

    private void HandleChangePassword(ClientSession session, ReadOnlySpan<char> payload)
    {
        var parts = payload.ToString().Split('/', StringSplitOptions.RemoveEmptyEntries);
        string account = parts.Length > 0 ? parts[0].Trim() : string.Empty;
        string oldPass = parts.Length > 1 ? parts[1].Trim() : string.Empty;
        string newPass = parts.Length > 2 ? parts[2].Trim() : string.Empty;
        HandleChangePasswordInternal(session, account, oldPass, newPass);
    }

    private unsafe void HandleBinaryChangePassword(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < sizeof(BinaryProtocol.ChangePasswordPayload)) return;
        fixed (byte* p = payload)
        {
            var intent = *(BinaryProtocol.ChangePasswordPayload*)p;
            var (account, oldPass, newPass) = intent.Read();
            HandleChangePasswordInternal(session, account, oldPass, newPass);
        }
    }

    private void HandleChangePasswordInternal(ClientSession session, string account, string oldPassword, string newPassword)
    {
        if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword))
        {
            session.EnqueuePacket(ProtocolConsts.SM_CHANGEPASS_FAIL, -1, 0, 0, 0, "账号和密码不能为空！");
            return;
        }

        bool success = false;
        if (PgPersistenceEngine != null)
        {
            success = PgPersistenceEngine.ChangePassword(account, oldPassword, newPassword);
        }
        else
        {
            if (_memoryAccounts.TryGetValue(account, out var currentPwd) && currentPwd == oldPassword)
            {
                _memoryAccounts[account] = newPassword;
                success = true;
            }
        }

        if (success)
        {
            ServerLogger.Audit("CHGPWD_SUCCESS", account, "", $"账号 {account} 修改密码成功");
            session.EnqueuePacket(ProtocolConsts.SM_CHANGEPASS_OK, 1, 0, 0, 0, "密码修改成功！");
        }
        else
        {
            ServerLogger.Audit("CHGPWD_FAILED", account, "", $"账号 {account} 原密码错误或不存在");
            session.EnqueuePacket(ProtocolConsts.SM_CHANGEPASS_FAIL, -2, 0, 0, 0, "原密码错误或账号不存在！");
        }
    }

    private void HandleLogin(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: 账号/密码
        int slash = payload.IndexOf('/');
        string account = slash > 0 ? payload[..slash].ToString().Trim() : payload.ToString().Trim();
        string password = slash > 0 ? payload[(slash + 1)..].ToString().Trim() : "123456";
        HandleLoginInternal(session, account, password);
    }

    private unsafe void HandleBinaryLogin(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < sizeof(BinaryProtocol.LoginIntentPayload)) return;
        fixed (byte* p = payload)
        {
            var intent = *(BinaryProtocol.LoginIntentPayload*)p;
            var (account, password) = intent.Read();
            HandleLoginInternal(session, account, password);
        }
    }

    private void HandleLoginInternal(ClientSession session, string account, string password)
    {
        if (string.IsNullOrWhiteSpace(account))
        {
            account = $"Player_{session.SessionId}";
        }

        long nowMs = Environment.TickCount64;
        if (_loginAttempts.TryGetValue(account, out var attemptInfo))
        {
            if (nowMs < attemptInfo.LockUntilMs)
            {
                long remainSec = Math.Max(1, (attemptInfo.LockUntilMs - nowMs) / 1000);
                ServerLogger.Audit("LOGIN_BLOCKED", account, "", $"账号处于密码错误安全锁定中，拦截登录尝试 (剩余 {remainSec} 秒)");
                session.EnqueuePacket(ProtocolConsts.SM_OUTOFCONNECTION, 0, 0, 0, 0, $"账号连续输错密码已临时锁定，请等待 {remainSec} 秒后重试！");
                session.Disconnect();
                return;
            }
        }

        if (PgPersistenceEngine != null && !string.IsNullOrEmpty(password))
        {
            if (PgPersistenceEngine.AccountExists(account))
            {
                bool verified = PgPersistenceEngine.VerifyAccount(account, password);
                if (!verified)
                {
                    int fails = 1;
                    if (_loginAttempts.TryGetValue(account, out var oldAtt))
                    {
                        fails = oldAtt.FailCount + 1;
                    }
                    long lockUntil = fails >= 5 ? nowMs + 15 * 60 * 1000 : 0;
                    _loginAttempts[account] = (fails, lockUntil);

                    if (fails >= 5)
                    {
                        ServerLogger.Audit("ACCOUNT_LOCKED", account, "", "密码连续输错达到 5 次上限，系统强制冻结 15 分钟");
                        session.EnqueuePacket(ProtocolConsts.SM_OUTOFCONNECTION, 0, 0, 0, 0, "密码连续错误达5次，账号已安全锁定15分钟！");
                    }
                    else
                    {
                        ServerLogger.Audit("LOGIN_FAILED", account, "", $"密码校验失败，当前失败次数: {fails}");
                        session.EnqueuePacket(ProtocolConsts.SM_OUTOFCONNECTION, 0, 0, 0, 0, $"密码错误！剩余尝试次数: {5 - fails}");
                    }
                    session.Disconnect();
                    return;
                }
            }
            else
            {
                // 新账号自动注册
                PgPersistenceEngine.RegisterOrUpdateAccount(account, password);
                ServerLogger.Audit("REGISTER", account, "", "新玩家注册账号成功");
            }
        }

        // 登录成功，清理失败尝试
        _loginAttempts.TryRemove(account, out _);
        session.AccountName = account;
        ServerLogger.Audit("LOGIN_SUCCESS", account, "", $"账号 {account} 登录成功，通知客户端");

        // 盛大官方 1.5 权威登录成功回执: SM_LOGINSUCCESS (530)
        session.EnqueuePacket(ProtocolConsts.SM_LOGINSUCCESS, session.SessionId, 0, 0, 0, account);
    }

    public void HandleQueryCharInfo(ClientSession session)
    {
        string account = session.AccountName;
        if (string.IsNullOrWhiteSpace(account))
        {
            session.EnqueuePacket(ProtocolConsts.SM_QUERYCHAR_FAIL, 0, 0, 0, 0, "未认证账号");
            return;
        }

        List<(string CharName, int Job, int Sex, int Level)> charList;
        if (PgPersistenceEngine != null)
        {
            charList = PgPersistenceEngine.GetCharactersByAccount(account);
        }
        else
        {
            // 官方 1.5：新账号查角返回 0 个角色，严禁凭空赠送任何角色
            if (!_memoryCharacters.TryGetValue(account, out charList!))
            {
                charList = new List<(string CharName, int Job, int Sex, int Level)>();
                _memoryCharacters[account] = charList;
            }
        }

        var sb = new StringBuilder();
        foreach (var c in charList)
        {
            // 盛大官方 1.5 格式: Name\tJob\tLevel\tGender\t0\t0
            sb.Append($"{c.CharName}\t{c.Job}\t{c.Level}\t{c.Sex}\t0\t0\r\n");
        }
        string payload = sb.ToString();

        // 盛大官方 1.5 权威查角回执: SM_QUERYCHAR_OK (523)
        session.EnqueuePacket(ProtocolConsts.SM_QUERYCHAR_OK, charList.Count, 0, 0, 0, payload);
    }

    private void HandleSelectChar(ClientSession session, ReadOnlySpan<char> payload)
    {
        string name = payload.ToString();
        int spawnX = 288;
        int spawnY = 615;

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

        // 官方 1.5 顶号踢人与安全互斥：防止同一个角色被多个客户端同时登录造成数据撕裂与刷物
        foreach (var pair in ActiveSessions)
        {
            var oldSession = pair.Value;
            if (oldSession != session && oldSession.IsConnected &&
                string.Equals(oldSession.CharacterName, name, StringComparison.OrdinalIgnoreCase))
            {
                if (oldSession.State != null)
                {
                    PgPersistenceEngine?.SavePlayer(oldSession.AccountName, oldSession.CharacterName, oldSession.State, oldSession.CurrentMapId, oldSession.X, oldSession.Y);
                    PersistenceEngine.SavePlayer(oldSession.CharacterName, oldSession.State, oldSession.CurrentMapId, oldSession.X, oldSession.Y);
                }
                oldSession.EnqueuePacket(ProtocolConsts.SM_OUTOFCONNECTION, 0, 0, 0, 0, "您的账号已在其他位置登录");
                oldSession.Disconnect();
            }
        }

        session.CharacterName = name;
        session.CurrentMapId = 0; // 比奇省
        session.X = spawnX;
        session.Y = spawnY;

        if (PgPersistenceEngine != null)
        {
            var loaded = PgPersistenceEngine.LoadPlayer(name, out int mId, out int mx, out int my);
            if (loaded != null)
            {
                session.State = loaded;
                session.CurrentMapId = mId;
                session.X = mx;
                session.Y = my;
            }
        }

        session.State ??= new PlayerState();

        // 官方 1.5 新角色初始生命魔法与属性规范
        if (session.State.MaxHp <= 0)
        {
            session.State.MaxHp = session.State.Job switch { 0 => 19, 1 => 16, _ => 17 };
            session.State.Hp = session.State.MaxHp;
        }
        if (session.State.MaxMp <= 0)
        {
            session.State.MaxMp = session.State.Job switch { 0 => 11, 1 => 17, _ => 13 };
            session.State.Mp = session.State.MaxMp;
        }

        // 官方 1.5 默认新手初始装备：布衣(男/女)、木剑
        if (session.State.Equipment[0] == null)
        {
            bool isFemale = session.State.Sex != 0;
            session.State.Equipment[0] = new EquipItem
            {
                Uid = 1,
                Name = isFemale ? "布衣(女)" : "布衣(男)",
                Shape = 1,
                Weight = 5,
                Dura = 5,
                MaxDura = 5,
                Ac = 0,
                MaxAc = 1,
                Mac = 0,
                MaxMac = 1
            };
        }
        if (session.State.Equipment[1] == null)
        {
            session.State.Equipment[1] = new EquipItem
            {
                Uid = 2,
                Name = "木剑",
                Shape = 1,
                Weight = 7,
                Dura = 4,
                MaxDura = 4,
                Dc = 1,
                MaxDc = 2
            };
        }

        // 官方 1.5 默认新手背包消耗品：金创药(小量)*2, 魔法药(小量)*2, 蜡烛*1
        if (session.State.BagItems.Count == 0)
        {
            session.State.BagItems.Add(new EquipItem { Uid = 10, Name = "金创药(小量)", Weight = 1, Dura = 1 });
            session.State.BagItems.Add(new EquipItem { Uid = 11, Name = "金创药(小量)", Weight = 1, Dura = 1 });
            session.State.BagItems.Add(new EquipItem { Uid = 12, Name = "魔法药(小量)", Weight = 1, Dura = 1 });
            session.State.BagItems.Add(new EquipItem { Uid = 13, Name = "蜡烛", Weight = 1, Dura = 1 });
            session.State.BagWeight = 4;
        }

        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;

        // 1. 注册到当前地图 AOI 空间九宫格
        map.Aoi.Enter(session, session.X, session.Y);

        // 2. 下发官方进入游戏核心确认: SM_STARTPLAY (20) 触发客户端转场
        session.EnqueuePacket(ProtocolConsts.SM_STARTPLAY, session.SessionId, session.X, session.Y, 0, session.CharacterName);

        // 3. 下发大世界全量登场握手封包集
        SendWorldEntryPackets(session);

        // 4. 在九宫格内向其他玩家广播该玩家登场生成: SM_SENDUSERSTATE (250)
        BroadcastToNinePatch(session, ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, session.X, session.Y, 0, session.CharacterName, includeSelf: false);
    }

    /// <summary>
    /// 盛大官方 1.5 原版大世界登场握手封包全集下发
    /// 在角色选定进入以及客户端大世界视图挂载就绪 (IntentLoginNoticeOk) 时双向下发，
    /// 确保地图、坐标、血球、经验条、负重、装备、包裹、昼夜光照 100% 物理对齐点亮。
    /// </summary>
    public void SendWorldEntryPackets(ClientSession session)
    {
        if (session.State == null) return;
        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;

        // 1. 坐标与地图初始化
        session.EnqueuePacket(ProtocolConsts.SM_LOGON_DELPHI, session.SessionId, session.X, session.Y, 0, session.CharacterName);
        session.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, map.MapId, session.X, session.Y, 0, NewMapExtra(map));

        // 2. 生命魔法/体态/经验/负重
        session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
        session.EnqueuePacket(ProtocolConsts.SM_EXPERIENCE, (int)session.State.Exp, (int)session.State.MaxExp, 0, 0, "");
        session.EnqueuePacket(ProtocolConsts.SM_HEALTHSPELLCHANGED, session.State.Hp, session.State.Mp, session.State.MaxHp, session.State.MaxMp);

        // 3. 四维属性、等级、职业、金币 (SM_SENDUSERSTATE 52)
        session.EnqueuePacket(ProtocolConsts.SM_SENDUSERSTATE, session.State.Gold, 0, 0, 0, $"{session.CharacterName}/{session.State.Level}/{session.State.Job}/{session.State.Sex}|{session.State.Ac}-{session.State.MaxAc}/{session.State.Mac}-{session.State.MaxMac}/{session.State.Dc}-{session.State.MaxDc}/{session.State.Mc}-{session.State.MaxMc}/{session.State.Sc}-{session.State.MaxSc}/{session.State.Hit}/{session.State.Speed}");

        // 4. 下发 9 槽位装备: SM_EQUIPINFO (621)
        var eqParts = new List<string>();
        for (int i = 0; i < 9; i++)
        {
            var eq = session.State.Equipment[i];
            if (eq != null)
                eqParts.Add($"{eq.Uid}/{eq.Name}|[color=#ffff00]{eq.Name}[/color]");
            else
                eqParts.Add("");
        }
        session.EnqueuePacket(ProtocolConsts.SM_EQUIPINFO, 0, 0, 0, 0, string.Join(",", eqParts));

        // 5. 下发背包物品 (SM_BAGITEMS 重置 + SM_ADDITEM 逐项添加)
        session.EnqueuePacket(ProtocolConsts.SM_BAGITEMS, 0, 0, 0, 0);
        for (int i = 0; i < session.State.BagItems.Count && i < 40; i++)
        {
            var it = session.State.BagItems[i];
            if (it != null)
            {
                int looks = it.Looks > 0 ? it.Looks : Mir2.Shared.Data.OfficialStdItemsRegistry.ResolveLooks(it.Name, 1);
                session.EnqueuePacket(ProtocolConsts.SM_ADDITEM, it.Uid, i, it.Weight, 0, $"{it.Name}\t{it.Dura}\t{it.MaxDura}\t{looks}");
            }
        }

        // 6. 昼夜自然光照 (SM_CHANGELIGHT 46)
        session.EnqueuePacket(ProtocolConsts.SM_CHANGELIGHT, 0, 0, 10, 0);

        // 7. 官方原版欢迎系统公告 (SM_SYSMSG_DELPHI 驱动聊天窗口显示)
        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "欢迎来到热血传奇！");

        // 8~10. 同步视野内的 NPC / 怪物 / 其他玩家
        SendNearbyEntities(session, map);

        // 11. 立即冲刷发送
        session.Flush();
    }

    /// <summary>SM_NEWMAP 的字符串参数：数据库世界为 "官方地图号|地图名"，客户端据此定位 .map 文件；夹具地图仍为地图名。</summary>
    private static string NewMapExtra(GameMap m) => string.IsNullOrEmpty(m.OfficialId) ? m.Name : $"{m.OfficialId}|{m.Name}";

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
        // 官方麻痹石化状态检测: 麻痹期间封锁移动并拉回
        if (session.State != null && session.State.IsParalyzed)
        {
            session.EnqueuePacket(ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, session.X, session.Y, session.Direction, session.CharacterName);
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你处于麻痹状态，无法移动。");
            return;
        }

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

            // 负重过大禁止奔跑校验
            if (isRun && session.State != null && session.State.BagWeight > session.State.MaxBagWeight)
            {
                session.EnqueuePacket(ProtocolConsts.SM_ENTITY_POS_DELPHI, session.SessionId, oldX, oldY, dir);
                session.EnqueuePacket(ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, oldX, oldY, dir, session.CharacterName);
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "背包过重，包裹或穿戴负重过重，无法快速奔跑！");
                return;
            }

            // 3. 移动速度反外挂与变速齿轮看门狗深度校验
            long nowMs = Environment.TickCount64;
            if (session.IsActionFrozen(nowMs))
            {
                session.EnqueuePacket(ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, oldX, oldY, dir, session.CharacterName);
                return;
            }

            if (SpeedWatchdog.VerifyActionAndDetectSpeedHack(session.SessionId, nowMs, nowMs, out var hackReason))
            {
                session.PenaltyFrozenUntilMs = nowMs + 1200;
                session.EnqueuePacket(ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, oldX, oldY, dir, session.CharacterName);
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你的动作太快了。");
                return;
            }

            if (session.LastMoveTimeMs > 0)
            {
                int fastMoves = session.ConsecutiveFastMoves;
                var (isSpeedHack, shouldRollback) = EconomySecurityEngine.SpeedHackDetectorEngine.CheckMovePacket(nowMs, session.LastMoveTimeMs, ref fastMoves);
                session.ConsecutiveFastMoves = fastMoves;
                if (isSpeedHack && shouldRollback)
                {
                    // 连续多次超速判定外挂，实施制动拉回
                    session.EnqueuePacket(ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, oldX, oldY, dir, session.CharacterName);
                    return;
                }
            }

            session.LastMoveTimeMs = nowMs;

            // 更新实体坐标
            session.X = newX;
            session.Y = newY;
            TradeEngine?.CheckAndCancelIfMoved(session);

            if (session.State != null && session.State.BreakSpellInvisibility())
            {
                BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, session.X, session.Y, 18, "破除隐身", includeSelf: true);
                uint mask = session.State.GetCurrentStatusMask(nowMs);
                session.EnqueuePacket(ProtocolConsts.SM_CHANGENAMECOLOR, session.SessionId, (int)mask, 0, 0);
                BroadcastToNinePatch(session, ProtocolConsts.SM_CHANGENAMECOLOR, session.SessionId, (int)mask, 0, 0, session.CharacterName, includeSelf: false);
            }

            // 更新九宫格 AOI 空间分区并在跨 Zone 时执行双向差分广播 (消除幽灵残影)
            UpdateAoiPositionWithTransition(map, session, oldX, oldY, newX, newY);

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

    private void HandleHit(ClientSession session, ReadOnlySpan<char> payload, bool isHeavyHit = false, bool isPowerHit = false, bool isWideHit = false)
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

        if (session.State != null && session.State.IsParalyzed)
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你处于麻痹状态，无法攻击。");
            return;
        }

        if (int.TryParse(xSpan, out int targetX) &&
            int.TryParse(ySpan, out int targetY) &&
            int.TryParse(dirSpan, out int dir))
        {
            long nowMs = Environment.TickCount64;
            if (session.IsActionFrozen(nowMs))
            {
                return; // 处于加速制动冷冻期，拦截出刀
            }

            if (SpeedWatchdog.VerifyActionAndDetectSpeedHack(session.SessionId, nowMs, nowMs, out _))
            {
                session.PenaltyFrozenUntilMs = nowMs + 1200;
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "出刀太快了。");
                return;
            }

            // 攻击速度校验 (间隔必须 >= 680ms)
            if (session.LastAttackTimeMs > 0 && !EconomySecurityEngine.AttackSpeedValidatorEngine.IsAttackValid(nowMs, session.LastAttackTimeMs, session.State?.AttackSpeed ?? 0))
            {
                return; // 挥刀过快拦截
            }
            session.LastAttackTimeMs = nowMs;
            session.Direction = (byte)dir;

            int cDist = Math.Max(Math.Abs(targetX - session.X), Math.Abs(targetY - session.Y));
            if ((!isWideHit && cDist == 0) || cDist > 2)
            {
                return; // 原地自残或超远攻击直接拦截，不消耗试刀与触发破隐
            }

            if (session.State != null && session.State.BreakSpellInvisibility())
            {
                BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, session.X, session.Y, 18, "破除隐身", includeSelf: true);
                uint mask = session.State.GetCurrentStatusMask(nowMs);
                session.EnqueuePacket(ProtocolConsts.SM_CHANGENAMECOLOR, session.SessionId, (int)mask, 0, 0);
                BroadcastToNinePatch(session, ProtocolConsts.SM_CHANGENAMECOLOR, session.SessionId, (int)mask, 0, 0, session.CharacterName, includeSelf: false);
            }

            // 1. 向九宫格广播普通物理挥刀动作: SM_OBJECTHIT (242)
            BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTHIT, session.SessionId, session.X, session.Y, dir, session.CharacterName, includeSelf: false);

            // 2. 武器开刀试刀判定 (在有效挥刀范围内触发)
            if (session.State != null)
            {
                var trialRes = session.State.TrialSlashEquippedWeapon();
                if (trialRes.Outcome != WeaponUpgradeEngine.TrialSlashOutcome.None)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, trialRes.Message);
                    if (trialRes.Outcome == WeaponUpgradeEngine.TrialSlashOutcome.Broken)
                    {
                        BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, session.X, session.Y, 999, "碎刀", includeSelf: true);
                        session.EnqueuePacket(ProtocolConsts.SM_TAKEOFF_OK, 0, 0, 0, 0);
                        session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.MaxBagWeight, session.State.WearWeight, session.State.MaxWearWeight);
                    }
                    else if (trialRes.Outcome == WeaponUpgradeEngine.TrialSlashOutcome.Success)
                    {
                        BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, session.X, session.Y, 26, "神兵升级成功", includeSelf: true);
                        var weapon = session.State.Equipment[(int)OfficialEquipSlot.Weapon];
                        if (weapon != null)
                        {
                            session.EnqueuePacket(ProtocolConsts.SM_TAKEON_OK, session.SessionId, (int)OfficialEquipSlot.Weapon, 0, 0, weapon.Name);
                        }
                    }
                    SendAbility(session);
                }

                // 官方 1/30 武器物理磨损衰减 (折断则伤害降为徒手 1 点)
                var (deducted, broken) = ItemLifecycleEngine.DeductWeaponDurabilityOnAttack(session.State, Random.Shared);
                if (broken)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你的武器损坏了。");
                }
            }

            // 特殊技能出刀光效广播与状态注入
            bool isPierceStab = isHeavyHit || (cDist == 2 && session.State != null && session.State.StabbingTracker.AutoStab);
            if (isPierceStab)
            {
                BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, targetX, targetY, 12, "刺杀剑气", includeSelf: true);
            }
            else if (isPowerHit)
            {
                BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, targetX, targetY, 7, "攻杀剑术", includeSelf: true);
                if (session.State != null)
                {
                    session.State.BoAttackKill = true;
                    var (tPower, _, mPower, _) = SkillTrainEngine.TrainSkill(session.State, 7, 1);
                    if (tPower && mPower != null)
                    {
                        session.EnqueuePacket(ProtocolConsts.SM_UPDATEUSERMAGIC, mPower.MagicId, mPower.Level, mPower.Key, mPower.CurTrain);
                    }
                }
            }
            else if (session.State != null && session.State.BoHalfMoon)
            {
                if (session.State.Mp >= 7)
                {
                    session.State.Mp -= 7;
                    session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
                    session.EnqueuePacket(ProtocolConsts.SM_HEALTHSPELLCHANGED, session.State.Hp, session.State.Mp, session.State.MaxHp, session.State.MaxMp);
                    BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, session.X, session.Y, 25, "半月弯刀", includeSelf: true);

                    var affectedTiles = WarriorCombatEngine.HalfMoon.GetAffectedTiles(session.X, session.Y, dir);
                    int halfMoonBaseDmg = session.State.CalculateAttackDamageWithLuck(Math.Max(0, session.State.Dc), Math.Max(session.State.Dc, session.State.MaxDc), Random.Shared);
                    foreach (var (tx, ty, isMain) in affectedTiles)
                    {
                        if (!isMain && RegenEngine != null)
                        {
                            var flankM = RegenEngine.FindMonsterAt(session.CurrentMapId, tx, ty);
                            if (flankM != null && !flankM.IsDead)
                            {
                                int halfMoonLvl = session.State.GetSkillLevel(25); // 官方 1.5 半月弯刀 MagicId = 25
                                int flankDmg = WarriorCombatEngine.HalfMoon.CalculateDamage(halfMoonBaseDmg, isMainTarget: false, skillLevel: halfMoonLvl);
                                flankM.Hp = Math.Max(0, flankM.Hp - flankDmg);
                                BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, flankM.Uid, flankM.X, flankM.Y, flankDmg, flankM.Name, includeSelf: true);
                                if (flankM.IsDead)
                                {
                                    RegenEngine.OnMonsterDied(flankM.Uid);
                                    BroadcastToNinePatch(session, ProtocolConsts.SM_DEATH_ACTION_DELPHI, flankM.Uid, flankM.X, flankM.Y, 0, flankM.Name, includeSelf: true);
                                    OnMonsterKilledByPlayer(session, flankM);
                                }
                            }
                        }
                    }
                }
            }

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

            // 沙巴克城门攻击判定 (盟重省 MapId=3, 坐标 672, 331)
            if (session.CurrentMapId == 3 && targetX == 672 && targetY == 331)
            {
                CastleGateEngine.DamageGate(50);
            }

            // 安全区禁武规则: 攻击者或目标处于安全区内一律禁止动武
            if (SafeZoneEngine.IsAttackForbiddenBySafeZone(session.CurrentMapId, session.X, session.Y, targetX, targetY))
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "安全区内禁止动武！");
                return;
            }

            if (defender != null && defender.State != null && session.State != null)
            {
                // 1. 官方 PK 模式规则判定 (0x4D1E54 btAttackMode)
                // 模式 1: [Mode: Peaceful] 和平模式，禁止攻击白名玩家
                if (session.State.AttackMode == 1 && !defender.State.IsRedName && !defender.State.IsYellowName)
                {
                    return; // 和平模式受限
                }
                session.State.AddPkFlashing(nowMs);

                int dist = Math.Max(Math.Abs(defender.X - session.X), Math.Abs(defender.Y - session.Y));
                var (rawDmg, hit, isArmorPiercing, isFireHit, died, _) = ResolveAuthoritativePhysicalCombat(session, defender, dist, nowMs, forcePierce: isHeavyHit);

                if (hit && rawDmg > 0)
                {
                    // 官方受击防具持久磨损衰减 (衣服 1/15, 首饰防具 1/40)
                    ItemLifecycleEngine.DeductArmorDurabilityOnDamage(defender.State, Random.Shared);

                    // 受击后仰广播: SM_OBJECTSTRUCK (244)
                    BroadcastToNinePatch(defender, ProtocolConsts.SM_OBJECTSTRUCK, defender.SessionId, defender.X, defender.Y, rawDmg, defender.CharacterName, includeSelf: true);

                    // 目标血量更新
                    defender.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, defender.State.Hp, defender.State.MaxHp, defender.State.Mp, defender.State.MaxMp);

                    // 死亡倒地动画广播: SM_DEATH_ACTION_DELPHI (28)
                    if (died)
                    {
                        AuthoritativeExecutePlayerDeath(defender, session, hitMap, nowMs);
                    }
                }
            }
            else if (RegenEngine != null && session.State != null)
            {
                if (!hitMap.IsWalkable(targetX, targetY))
                {
                    return; // 目标处于不可通行的物理墙壁阻挡内，普通挥刀被实心墙壁吸收阻隔
                }

                // 4. 检索目标位置的怪物并进行物理攻击与掉落结算
                var monster = RegenEngine.FindMonsterAt(session.CurrentMapId, targetX, targetY);
                if (monster != null && !monster.IsDead)
                {
                    int dist = Math.Max(Math.Abs(monster.X - session.X), Math.Abs(monster.Y - session.Y));
                    var (rawDmg, hit, isFireHit, died) = ResolveAuthoritativeMonsterPhysicalCombat(session, monster, dist, nowMs);
                    if (hit && rawDmg > 0)
                    {
                        // 累加怪物仇恨大账本与破石苏醒 (IDA 0x4B966C)
                        monster.ThreatTable.AddHate(session.SessionId, rawDmg, isTaunt: false);
                        if (monster.IsStoneForm) monster.IsStoneForm = false;
                        if (monster.NextActionTimeMs > nowMs + 1000)
                        {
                            monster.NextActionTimeMs = nowMs + 500;
                            monster.TargetSessionId = session.SessionId;
                        }
                        if (monster.IsTrappedByHolyCurse)
                        {
                            monster.IsTrappedByHolyCurse = false;
                            monster.TrappedExpireMs = 0;
                            monster.NextActionTimeMs = nowMs;
                            monster.TargetSessionId = session.SessionId;
                        }

                        if (isFireHit)
                        {
                            var (tFire, _, mFire, _) = SkillTrainEngine.TrainSkill(session.State, 26, 1);
                            if (tFire && mFire != null)
                            {
                                session.EnqueuePacket(ProtocolConsts.SM_UPDATEUSERMAGIC, mFire.MagicId, mFire.Level, mFire.Key, mFire.CurTrain);
                            }
                        }
                        else if (isHeavyHit || dist == 2)
                        {
                            var (tStab, _, mStab, _) = SkillTrainEngine.TrainSkill(session.State, 12, 1);
                            if (tStab && mStab != null)
                            {
                                session.EnqueuePacket(ProtocolConsts.SM_UPDATEUSERMAGIC, mStab.MagicId, mStab.Level, mStab.Key, mStab.CurTrain);
                            }
                        }

                        // 广播怪物受击后仰: SM_OBJECTSTRUCK (244)
                        BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, monster.Uid, monster.X, monster.Y, rawDmg, monster.Name, includeSelf: true);

                        if (died)
                        {
                            RegenEngine.OnMonsterDied(monster.Uid);

                            // 广播怪物死亡倒地: SM_DEATH_ACTION_DELPHI (28)
                            BroadcastToNinePatch(session, ProtocolConsts.SM_DEATH_ACTION_DELPHI, monster.Uid, monster.X, monster.Y, 0, monster.Name, includeSelf: true);
                            OnMonsterKilledByPlayer(session, monster);
                        }
                    }
                }
            }
        }
    }

    private void HandleSpell(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;
        if (session.State.IsParalyzed)
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你处于麻痹状态，无法施法。");
            return;
        }

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

            // 跨职业特戒技能权限拦截
            if (magicId == 1 && session.State.Job != 1 && !session.State.HasFlameRing)
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你未佩戴火焰戒指，无法运转真气施展火球术");
                return;
            }
            if (magicId == 2 && session.State.Job != 2 && !session.State.HasHealingRing)
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你无法使用这个魔法。");
                return;
            }

            // 抗拒火环 (MagicId = 8) 安全区拦截
            if (magicId == 8 && SafeZoneEngine.IsInSafeZone(session.CurrentMapId, session.X, session.Y))
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "在安全区内不能推开他人。");
                return;
            }

            // 圣言术 (MagicId = 24) BOSS 免疫前置校验
            if (magicId == 24 && RegenEngine != null)
            {
                var monster = RegenEngine.FindMonsterByUid(targetUid) ?? RegenEngine.FindMonsterAt(session.CurrentMapId, targetX, targetY);
                if (monster != null && (monster.Name.Contains("教主") || monster.Name.Contains("尸王") || monster.MaxHp >= 2000))
                {
                    BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, monster.Uid, monster.X, monster.Y, 24, "圣言术", includeSelf: true);
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "BOSS 阶级怪物拥有神圣庇护，圣言术无法度化！");
                    return;
                }
            }

            // 远程单体法术视线射线穿透检测 (防穿墙)
            var spellMap = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
            if ((magicId == 1 || magicId == 5 || magicId == 9 || magicId == 10 || magicId == 13 || magicId == 32) && (targetX != 0 || targetY != 0))
            {
                if (!spellMap.CheckLineOfSight(session.X, session.Y, targetX, targetY))
                {
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "障碍物阻挡了施法视线！");
                    return;
                }
            }

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

            if (magicId != 12 && magicId != 26)
            {
                var (trained, leveledUp, magic, msg) = SkillTrainEngine.TrainSkill(session.State, magicId, 1);
                if (trained && magic != null)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_UPDATEUSERMAGIC, magic.MagicId, magic.Level, magic.Key, magic.CurTrain);
                }
            }
        }
    }

    public void HandleSay(ClientSession session, string message) => HandleSay(session, message.AsSpan());

    public void HandleSay(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 兼容带包头参数格式 (recog/param/tag/series/msg) 或纯消息文本
        ReadOnlySpan<char> text = payload;
        int slash1 = payload.IndexOf('/');
        if (slash1 > 0 && int.TryParse(payload[..slash1], out _))
        {
            int slashes = 0;
            int idx = -1;
            for (int i = 0; i < payload.Length; i++)
            {
                if (payload[i] == '/')
                {
                    slashes++;
                    if (slashes == 4)
                    {
                        idx = i;
                        break;
                    }
                }
            }
            if (idx >= 0 && idx + 1 <= payload.Length)
            {
                text = payload[(idx + 1)..];
            }
        }

        string msgStr = text.ToString();
        if (GmEnabled && msgStr.StartsWith("@gm ", StringComparison.OrdinalIgnoreCase)) { HandleGmCommand(session, msgStr[4..].Trim()); return; }
        if (msgStr.StartsWith("@ReloadPlugin", StringComparison.OrdinalIgnoreCase))
        {
            ModernNpcEngine.HotReloadPlugins();
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "NPC 插件已重新热加载完成！");
            return;
        }


        // 行会聊天 (以 '!~' 或 '/!~' 开头)
        if (msgStr.StartsWith("!~") || msgStr.StartsWith("/!~"))
        {
            string guildMsg = msgStr.StartsWith("/!~") ? msgStr[3..] : msgStr[2..];
            string guildName = GuildEngine.GetPlayerGuild(session.CharacterName) ?? "";
            var guild = GuildEngine.GetGuild(guildName);
            if (guild == null)
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你尚未加入任何行会！");
                return;
            }
            string formatted = $"[行会] {session.CharacterName}: {guildMsg}";
            foreach (var active in ActiveSessions.Values)
            {
                if (active.SessionId != session.SessionId && guild.Members.Contains(active.CharacterName))
                {
                    active.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, formatted);
                }
            }
            return;
        }

        // 组队聊天 (以 '!!' 或 '/!!' 开头)
        if (msgStr.StartsWith("!!") || msgStr.StartsWith("/!!"))
        {
            string groupMsg = msgStr.StartsWith("/!!") ? msgStr[3..] : msgStr[2..];
            var party = GroupEngine.GetParty(session.SessionId);
            if (party == null || party.Members.Count <= 1)
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你当前不在队伍中或队伍无其他成员！");
                return;
            }
            string formatted = $"[组队] {session.CharacterName}: {groupMsg}";
            foreach (var member in party.Members)
            {
                if (member.SessionId != session.SessionId)
                {
                    member.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, formatted);
                }
            }
            return;
        }

        // 全服喊话 / 千里传音 (以 '!' 或 '/!' 开头)
        if (msgStr.StartsWith("!") || msgStr.StartsWith("/!"))
        {
            string shout = msgStr.TrimStart('/', '!');
            if (session.State != null)
            {
                var horn = session.State.BagItems.Find(i => i.Name.Contains("千里传音"));
                if (horn == null)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "背包中没有【千里传音】，无法进行全服喊话！");
                    return;
                }
                session.State.BagItems.Remove(horn);
                session.State.BagWeight = Math.Max(0, session.State.BagWeight - horn.Weight);
                session.EnqueuePacket(ProtocolConsts.SM_DELITEM, horn.Uid, 0, 0, 0, horn.Name);
                session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);

                string formatted = $"[千里传音] {session.CharacterName}: {shout}";
                foreach (var s in GetAllGlobalSessions())
                {
                    s.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, formatted);
                }
            }
            return;
        }

        // 私聊 (以 '/' 开头且包含空格)
        if (msgStr.StartsWith("//") || msgStr.StartsWith("/"))
        {
            string whisperBody = msgStr.TrimStart('/');
            int spaceIdx = whisperBody.IndexOf(' ');
            if (spaceIdx > 0)
            {
                string targetName = whisperBody[..spaceIdx];
                string content = whisperBody[(spaceIdx + 1)..];
                ClientSession? target = null;
                foreach (var s in ActiveSessions.Values)
                {
                    if (s.CharacterName == targetName)
                    {
                        target = s;
                        break;
                    }
                }
                if (target != null)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_WHISPER, 0, 0, 0, 0, $"你对 {targetName} 说: {content}");
                    target.EnqueuePacket(ProtocolConsts.SM_WHISPER, 0, 0, 0, 0, $"{session.CharacterName} 对你说: {content}");
                }
                return;
            }
        }

        if (msgStr.StartsWith("@洗红名", StringComparison.OrdinalIgnoreCase))
        {
            if (session.State != null)
            {
                int cost = session.State.PkPoints * 1000;
                if (session.State.Gold >= cost)
                {
                    session.State.Gold -= cost;
                    session.State.PkPoints = 0;
                    session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, 0, 0, 0);
                    session.EnqueuePacket(ProtocolConsts.SM_CHANGENAMECOLOR, session.SessionId, 0, 0, 0, session.CharacterName);
                    BroadcastToNinePatch(session, ProtocolConsts.SM_CHANGENAMECOLOR, session.SessionId, 0, 0, 0, session.CharacterName, includeSelf: false);
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "红名已洗清，罪孽得以消除。");
                }
                else
                {
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"金币不足，清洗当前罪孽需要 {cost} 金币！");
                }
            }
            return;
        }

        if (msgStr.StartsWith("@沙城税收", StringComparison.OrdinalIgnoreCase))
        {
            var parts = msgStr.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2 && int.TryParse(parts[1], out int amount))
            {
                string guildName = GuildEngine.GetPlayerGuild(session.CharacterName) ?? "";
                var guild = GuildEngine.GetGuild(guildName);
                bool isLeader = guild != null && guild.MasterName.Equals(session.CharacterName, StringComparison.OrdinalIgnoreCase);
                var (ok, withdrawn, taxMsg) = SabukEngine.WithdrawSabukGold(guildName, isLeader, amount);
                if (ok && session.State != null)
                {
                    session.State.Gold += withdrawn;
                    session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, 0, 0, 0);
                }
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, taxMsg);
            }
            return;
        }

        if (msgStr.StartsWith("@标记", StringComparison.OrdinalIgnoreCase))
        {
            var parts = msgStr.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 3 && int.TryParse(parts[1], out int mx) && int.TryParse(parts[2], out int my))
            {
                var party = GroupEngine.GetParty(session.SessionId);
                if (party != null)
                {
                    foreach (var member in party.Members)
                    {
                        member.EnqueuePacket(ProtocolConsts.SM_MAPMARK_DELPHI, mx, my, 0, 0, session.CharacterName);
                        member.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"队长 {session.CharacterName} 标记了坐标 ({mx}, {my})");
                    }
                }
                else
                {
                    session.EnqueuePacket(ProtocolConsts.SM_MAPMARK_DELPHI, mx, my, 0, 0, session.CharacterName);
                }
            }
            return;
        }

        if (msgStr.StartsWith("@清除标记", StringComparison.OrdinalIgnoreCase))
        {
            var party = GroupEngine.GetParty(session.SessionId);
            if (party != null)
            {
                foreach (var member in party.Members)
                {
                    member.EnqueuePacket(ProtocolConsts.SM_MAPMARK_DELPHI, 0, 0, 0, 0, session.CharacterName);
                    member.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"队长 {session.CharacterName} 清除了标记");
                }
            }
            else
            {
                session.EnqueuePacket(ProtocolConsts.SM_MAPMARK_DELPHI, 0, 0, 0, 0, session.CharacterName);
            }
            return;
        }

        if (msgStr.StartsWith("@宣战", StringComparison.OrdinalIgnoreCase))
        {
            var parts = msgStr.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2 && session.State != null && session.State.Gold >= 30000)
            {
                string targetGuild = parts[1];
                string myGuild = GuildEngine.GetPlayerGuild(session.CharacterName) ?? "战神殿";
                session.State.Gold -= 30000;
                GuildWarEngine.DeclareWar(myGuild, targetGuild, Environment.TickCount64);
                session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, 0, 0, 0);
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"{myGuild} 向 {targetGuild} 宣战。");
            }
            return;
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
                session.EnqueuePacket(ProtocolConsts.SM_ATTACKMODECHANGED, session.SessionId, session.State.AttackMode, 0, 0);
            }
            return;
        }

        // 2. 盛大官方特戒传送指令 (IDA 0x4D1E54 / SpecialRingEngine.TeleportRing)
        if (text.StartsWith("@move", StringComparison.OrdinalIgnoreCase) || text.StartsWith("@飞", StringComparison.OrdinalIgnoreCase) || text.StartsWith("@传送", StringComparison.OrdinalIgnoreCase))
        {
            if (session.State == null) return;
            if (session.CurrentMapId == SabukSiegeEngine.SabukPalaceMapId)
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "此地图无法使用传送戒指。");
                return;
            }
            var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
            long nowTick = Environment.TickCount64;
            var res = session.State.TryExecuteTeleport(text.ToString(), (x, y) => !map.IsWalkable(x, y), map.Width, map.Height, nowTick);
            if (res.success)
            {
                if (TradeEngine != null && TradeEngine.GetTrade(session.SessionId) != null)
                {
                    TradeEngine.CancelTrade(session, "传送位移导致交易取消。");
                }
                int oldX = session.X;
                int oldY = session.Y;
                session.X = res.targetX;
                session.Y = res.targetY;
                UpdateAoiPositionWithTransition(map, session, oldX, oldY, session.X, session.Y);
                SendPositionPullback(session, session.X, session.Y, session.Direction);
                BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTRUN, session.SessionId, session.X, session.Y, session.Direction, session.CharacterName, includeSelf: false);
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"瞬移至 ({session.X}, {session.Y}) 成功！");
            }
            else
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, res.message);
            }
            return;
        }

        // 2.1 记忆套装全队召集指令 (@天地合一 / @grouprecall)
        if (text.StartsWith("@天地合一", StringComparison.OrdinalIgnoreCase) || text.StartsWith("@grouprecall", StringComparison.OrdinalIgnoreCase) || text.StartsWith("@recall", StringComparison.OrdinalIgnoreCase))
        {
            if (session.State == null) return;
            var party = GroupEngine?.GetParty(session.SessionId);
            if (party == null || party.LeaderSessionId != session.SessionId)
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "只有队长才能使用组队召唤。");
                return;
            }

            var (success, count, message) = SuitWorldEventEngine.ExecuteMemorySummonAll(session, party.Members, _mapManager, TradeEngine, Environment.TickCount64, RegenEngine);
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, message);
            return;
        }

        // 2.1.1 拒绝/允许天地合一召唤指令
        if (text.StartsWith("@拒绝天地合一", StringComparison.OrdinalIgnoreCase))
        {
            if (session.State != null)
            {
                session.State.AllowGroupRecall = false;
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你已拒绝组队召唤。");
            }
            return;
        }

        if (text.StartsWith("@允许天地合一", StringComparison.OrdinalIgnoreCase))
        {
            if (session.State != null)
            {
                session.State.AllowGroupRecall = true;
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你已允许组队召唤。");
            }
            return;
        }

        // 2.2 随从指令: @休息 / @跟随 (官方 1.5 原生 Ctrl+A 快捷键 Toggle 切换模式)
        if (text.StartsWith("@休息", StringComparison.OrdinalIgnoreCase) || text.StartsWith("@Rest", StringComparison.OrdinalIgnoreCase))
        {
            bool anyResting = session.Pets.Any(p => p.IsResting);
            bool targetState = !anyResting;
            foreach (var pet in session.Pets)
            {
                pet.IsResting = targetState;
                if (RegenEngine != null)
                {
                    var inst = RegenEngine.GetMonster(pet.Uid);
                    if (inst != null) inst.IsResting = targetState;
                }
            }
            if (targetState)
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "宝宝已进入休息状态，原地戒备待命。");
            }
            else
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "宝宝解除休息，跟随主人前进！");
            }
            return;
        }

        if (text.StartsWith("@跟随", StringComparison.OrdinalIgnoreCase) || text.StartsWith("@Follow", StringComparison.OrdinalIgnoreCase))
        {
            foreach (var pet in session.Pets)
            {
                pet.IsResting = false;
                if (RegenEngine != null)
                {
                    var inst = RegenEngine.GetMonster(pet.Uid);
                    if (inst != null) inst.IsResting = false;
                }
            }
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "宝宝解除休息，跟随主人前进！");
            return;
        }

        // 2.3 探测项链指令: @Searching / @探测 / @probe
        if (text.StartsWith("@Searching", StringComparison.OrdinalIgnoreCase) ||
            text.StartsWith("@探测", StringComparison.OrdinalIgnoreCase) ||
            text.StartsWith("@probe", StringComparison.OrdinalIgnoreCase))
        {
            var necklace = session.State?.Equipment[(int)OfficialEquipSlot.Necklace];
            if (necklace == null || (!necklace.Name.Contains("探测") && necklace.Shape != 159))
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你未佩戴探测项链，无法探知他人行踪！");
                return;
            }
            var parts = text.ToString().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
            {
                string targetName = parts[1];
                ClientSession? target = GetAllGlobalSessions().FirstOrDefault(s => s.CharacterName.Equals(targetName, StringComparison.OrdinalIgnoreCase));
                if (target != null)
                {
                    var map = _mapManager.GetMap(target.CurrentMapId);
                    string mapName = map?.Name ?? $"地图{target.CurrentMapId}";
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"{target.CharacterName}当前位于{mapName}({target.X}:{target.Y})");
                }
                else
                {
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"{targetName}探查失败：目标不在线或不存在。");
                }
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

        // 如果主人拥有随从，复活时随从暴毙消散
        if (session.Pets.Count > 0)
        {
            lock (_ninePatchBuffer)
            {
                map.Aoi.GetEntitiesInNinePatch(oldX, oldY, _ninePatchBuffer);
                for (int p = 0; p < session.Pets.Count; p++)
                {
                    var pet = session.Pets[p];
                    for (int i = 0; i < _ninePatchBuffer.Count; i++)
                    {
                        var other = _ninePatchBuffer[i];
                        if (other != session)
                        {
                            other.EnqueuePacket(ProtocolConsts.SM_ENTITYDISAPPEAR, pet.Uid, pet.X, pet.Y, 0, pet.Name);
                        }
                    }
                }
            }
            if (RegenEngine != null)
            {
                foreach (var pet in session.Pets)
                {
                    RegenEngine.RemoveMonster(pet.Uid);
                }
            }
            session.Pets.Clear();
        }

        // 1. 恢复状态与官方半血半魔 (MaxHp / 2, MaxMp / 2)
        session.State.Hp = Math.Max(1, session.State.MaxHp / 2);
        session.State.Mp = Math.Max(1, session.State.MaxMp / 2);
        session.State.HasGreenPoison = false;
        session.State.HasRedPoison = false;
        session.State.IncHealth = 0;
        session.State.IncSpell = 0;
        session.State.InvincibleUntilMs = Environment.TickCount64 + 3000;

        // 2. 传送回复活安全区 (普通玩家比奇 0: 330, 270; 红名玩家盟重红名村 3: 850, 660)
        int targetMapId = 0;
        int respawnX = 330;
        int respawnY = 270;
        if (session.State.PkPoints >= 200)
        {
            targetMapId = 3;
            respawnX = 850;
            respawnY = 660;
        }

        bool mapChanged = session.CurrentMapId != targetMapId;
        if (mapChanged)
        {
            map.Aoi.Leave(session, oldX, oldY);
            BroadcastToNinePatch(session, ProtocolConsts.SM_ENTITYDISAPPEAR, session.SessionId, oldX, oldY, 0, session.CharacterName, includeSelf: false);

            session.CurrentMapId = targetMapId;
            session.X = respawnX;
            session.Y = respawnY;

            var newMap = _mapManager.GetMap(targetMapId) ?? _mapManager.DefaultMap;
            newMap.Aoi.Enter(session, respawnX, respawnY);

            session.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, targetMapId, respawnX, respawnY, 0, string.IsNullOrEmpty(newMap.OfficialId) ? (newMap.Name ?? (targetMapId == 3 ? "盟重省" : "比奇省")) : NewMapExtra(newMap));
            RegenEngine?.PopulateMapNow(newMap.MapId); SendNearbyEntities(session, newMap); // 换图后下发新视野
            session.EnqueuePacket(ProtocolConsts.SM_REVIVE, session.SessionId, respawnX, respawnY, 0, session.CharacterName);
            session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
            session.EnqueuePacket(ProtocolConsts.SM_HEALTHSPELLCHANGED, session.State.Hp, session.State.Mp, session.State.MaxHp, session.State.MaxMp);
            BroadcastToNinePatch(session, ProtocolConsts.SM_ENTITYAPPEAR, session.SessionId, respawnX, respawnY, session.Direction, session.CharacterName, includeSelf: false);
        }
        else
        {
            session.X = respawnX;
            session.Y = respawnY;
            UpdateAoiPositionWithTransition(map, session, oldX, oldY, respawnX, respawnY);
            session.EnqueuePacket(ProtocolConsts.SM_REVIVE, session.SessionId, respawnX, respawnY, 0, session.CharacterName);
            session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
            session.EnqueuePacket(ProtocolConsts.SM_HEALTHSPELLCHANGED, session.State.Hp, session.State.Mp, session.State.MaxHp, session.State.MaxMp);
            BroadcastToNinePatch(session, ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, respawnX, respawnY, session.Direction, session.CharacterName, includeSelf: false);
        }

        uint mask = session.State.GetCurrentStatusMask(Environment.TickCount64);
        session.EnqueuePacket(ProtocolConsts.SM_CHANGENAMECOLOR, session.SessionId, (int)mask, 0, 0);
        BroadcastToNinePatch(session, ProtocolConsts.SM_CHANGENAMECOLOR, session.SessionId, (int)mask, 0, 0, session.CharacterName, includeSelf: false);
    }

    /// <summary>
    /// 盛大官方 1.4/1.5 拾取地表物品权威逻辑 (IDA 0x4C5CB0 TCreature.PickUp)
    /// 包含死亡状态校验、80秒归属保护期、背包40格满仓检测、负重上限校验、相邻格拾取与金币上限截断
    /// </summary>
    private void HandlePickUp(ClientSession session, ReadOnlySpan<char> payload = default)
    {
        if (session.State == null || session.State.Hp <= 0) return; // 死亡不可拾取 (IDA 0x4C5CB0 @ L889)

        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
        long nowMs = Environment.TickCount64;

        int pickX = session.X;
        int pickY = session.Y;

        if (!payload.IsEmpty)
        {
            int lastSlash = payload.LastIndexOf('/');
            var targetCoordSpan = (lastSlash >= 0 ? payload[(lastSlash + 1)..] : payload).TrimEnd('/');
            string coordStr = targetCoordSpan.ToString();
            string[] coordParts = coordStr.Split(new[] { '\t', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (coordParts.Length >= 2 && int.TryParse(coordParts[0], out int cx) && int.TryParse(coordParts[1], out int cy))
            {
                if (Math.Max(Math.Abs(cx - session.X), Math.Abs(cy - session.Y)) <= 1)
                {
                    pickX = cx;
                    pickY = cy;
                }
            }
        }

        // 尝试拾取目标坐标的物品
        var (success, groundItem, errorMsg) = map.TryPickGroundItem(pickX, pickY, session.SessionId, nowMs);
        if (!success || groundItem == null)
        {
            if (pickX != session.X || pickY != session.Y)
            {
                // 如果相邻格未拾取到，再尝试脚下
                (success, groundItem, errorMsg) = map.TryPickGroundItem(session.X, session.Y, session.SessionId, nowMs);
            }
            if (!success || groundItem == null)
            {
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, errorMsg);
                }
                return;
            }
        }

        // 1. 金币拾取处理
        if (groundItem.IsGold)
        {
            if (session.State.Gold >= PlayerState.MaxGoldLimit)
            {
                map.AddGroundItem(groundItem);
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "金币已达上限，无法拾取！");
                return;
            }

            if (session.State.Gold + groundItem.Count > PlayerState.MaxGoldLimit)
            {
                int canTake = PlayerState.MaxGoldLimit - session.State.Gold;
                groundItem.Count -= canTake;
                session.State.Gold = PlayerState.MaxGoldLimit;
                map.AddGroundItem(groundItem);
                session.State.RecalculateAuthoritativeWeights(session.State.Gold);
                session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, 0, 0, 0);
                return;
            }

            var party = GroupEngine.GetParty(session.SessionId);
            if (party != null && party.Members.Count > 1)
            {
                var shares = GroupEngine.DistributeGroupGold(party, session, groundItem.Count);
                foreach (var (member, gain) in shares)
                {
                    if (member.State != null)
                    {
                        member.State.Gold += gain;
                        member.State.RecalculateAuthoritativeWeights(member.State.Gold);
                        member.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, member.State.Gold, 0, 0, 0);
                        member.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"组队分配获得金币 {gain}");
                    }
                }
            }
            else
            {
                session.State.Gold += groundItem.Count;
                session.State.RecalculateAuthoritativeWeights(session.State.Gold);
                session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, 0, 0, 0);
            }

            // 广播地表物品消失: SM_DELGROUNDITEM (315)
            BroadcastToNinePatch(session, ProtocolConsts.SM_DELGROUNDITEM, groundItem.Uid, groundItem.X, groundItem.Y, 0, groundItem.Name, includeSelf: true);
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
        session.State.RecalculateAuthoritativeWeights(session.State.Gold);

        // 2.4 九宫格广播地表物品消失: SM_DELGROUNDITEM (315)
        BroadcastToNinePatch(session, ProtocolConsts.SM_DELGROUNDITEM, groundItem.Uid, groundItem.X, groundItem.Y, 0, groundItem.Name, includeSelf: true);

        // 2.5 下发物品入包通知: SM_ADDITEM (200) 与 SM_WEIGHTCHANGED (204)
        session.EnqueuePacket(ProtocolConsts.SM_ADDITEM, groundItem.Uid, groundItem.Weight, 0, 0, groundItem.Name);
        session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
    }

    /// <summary>
    /// 执行局部九宫格高效广播 (零中间缓冲分配，直接投递到周围玩家待合包缓冲池中)
    /// </summary>
    private void BroadcastToNinePatch(ClientSession source, short ident, int recog, int param, int tag, int series, string extra, bool includeSelf)
    {
        var map = _mapManager.GetMap(source.CurrentMapId) ?? _mapManager.DefaultMap;
        map.Aoi.ForEachInNinePatch(source.X, source.Y, target =>
        {
            if (!includeSelf && target.SessionId == source.SessionId) return;
            target.EnqueuePacket(ident, recog, param, tag, series, extra);
        });
    }

    /// <summary>
    /// 当玩家在同一地图移动并跨越 AOI Zone 时，执行权威视野差分双向同步 (Appear / Disappear)
    /// 彻底消除跨边界幽灵残影与黑天鹅漏包
    /// </summary>
    private void SynchronizeAoiZoneTransition(GameMap map, ClientSession session, List<int>? enteredZones, List<int>? leftZones)
    {
        if (map == null || session == null) return;

        // 1. 离开视野的 Zone：双向发送 SM_ENTITYDISAPPEAR
        if (leftZones != null && leftZones.Count > 0)
        {
            foreach (int zoneIdx in leftZones)
            {
                map.Aoi.ForEachInZone(zoneIdx, other =>
                {
                    if (other != session && other.IsConnected)
                    {
                        other.EnqueuePacket(ProtocolConsts.SM_ENTITYDISAPPEAR, session.SessionId, session.X, session.Y, 0, session.CharacterName);
                        session.EnqueuePacket(ProtocolConsts.SM_ENTITYDISAPPEAR, other.SessionId, other.X, other.Y, 0, other.CharacterName);

                        if (session.Pets != null && session.Pets.Count > 0)
                        {
                            foreach (var pet in session.Pets)
                            {
                                other.EnqueuePacket(ProtocolConsts.SM_ENTITYDISAPPEAR, pet.Uid, pet.X, pet.Y, 0, pet.Name);
                            }
                        }
                        if (other.Pets != null && other.Pets.Count > 0)
                        {
                            foreach (var pet in other.Pets)
                            {
                                session.EnqueuePacket(ProtocolConsts.SM_ENTITYDISAPPEAR, pet.Uid, pet.X, pet.Y, 0, pet.Name);
                            }
                        }
                    }
                });
            }
        }

        // 2. 新进入视野的 Zone：双向发送 SM_ENTITYAPPEAR
        if (enteredZones != null && enteredZones.Count > 0)
        {
            foreach (int zoneIdx in enteredZones)
            {
                map.Aoi.ForEachInZone(zoneIdx, other =>
                {
                    if (other != session && other.IsConnected)
                    {
                        other.EnqueuePacket(ProtocolConsts.SM_ENTITYAPPEAR, session.SessionId, session.X, session.Y, session.Direction, session.CharacterName);
                        session.EnqueuePacket(ProtocolConsts.SM_ENTITYAPPEAR, other.SessionId, other.X, other.Y, other.Direction, other.CharacterName);

                        if (session.Pets != null && session.Pets.Count > 0)
                        {
                            foreach (var pet in session.Pets)
                            {
                                other.EnqueuePacket(ProtocolConsts.SM_ENTITYAPPEAR, pet.Uid, pet.X, pet.Y, 0, pet.Name);
                            }
                        }
                        if (other.Pets != null && other.Pets.Count > 0)
                        {
                            foreach (var pet in other.Pets)
                            {
                                session.EnqueuePacket(ProtocolConsts.SM_ENTITYAPPEAR, pet.Uid, pet.X, pet.Y, 0, pet.Name);
                            }
                        }
                    }
                });
            }

            if (RegenEngine != null)
            {
                foreach (var mon in RegenEngine.AllActiveMonsters)
                {
                    if (mon.MapId == map.MapId && mon.Hp > 0 && Math.Abs(mon.X - session.X) <= 20 && Math.Abs(mon.Y - session.Y) <= 20)
                    {
                        session.EnqueuePacket(220, mon.Uid, mon.X, mon.Y, mon.Direction, $"{mon.Name}\t{mon.X}\t{mon.Y}\t{mon.Direction}\t{mon.Appr}");
                    }
                }
            }
        }
    }

    /// <summary>
    /// 更新 AOI 坐标并在跨 Zone 时自动触发视野差分补偿
    /// </summary>
    private void UpdateAoiPositionWithTransition(GameMap map, ClientSession session, int oldX, int oldY, int newX, int newY)
    {
        if (map.Aoi.UpdatePosition(session, oldX, oldY, newX, newY, out var enteredZones, out var leftZones))
        {
            SynchronizeAoiZoneTransition(map, session, enteredZones, leftZones);
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
            var map = _mapManager.GetMap(act.MapId) ?? _mapManager.GetMap(0);
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

                // 目标玩家血球更新: SM_MYSTATUS
                if (ActiveSessions.TryGetValue(act.TargetSessionId, out var targetSession) && targetSession.State != null)
                {
                    targetSession.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, targetSession.State.Hp, targetSession.State.MaxHp, targetSession.State.Mp, targetSession.State.MaxMp);
                }
                else
                {
                    lock (_ninePatchBuffer)
                    {
                        map.Aoi.GetEntitiesInNinePatch(act.X, act.Y, _ninePatchBuffer);
                        var target = _ninePatchBuffer.Find(s => s.SessionId == act.TargetSessionId);
                        if (target?.State != null)
                        {
                            target.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, target.State.Hp, target.State.MaxHp, target.State.Mp, target.State.MaxMp);
                        }
                    }
                }

                // 被怪物打死：执行死亡流程 (广播倒地、随从解散、爆装等)，此前 act.Died 无人消费
                if (act.Died)
                {
                    ClientSession? victim = ActiveSessions.TryGetValue(act.TargetSessionId, out var vs) ? vs : GetAllGlobalSessions().FirstOrDefault(s => s.SessionId == act.TargetSessionId);
                    if (victim?.State != null)
                    {
                        var victimMap = _mapManager.GetMap(victim.CurrentMapId) ?? map;
                        AuthoritativeExecutePlayerDeath(victim, null, victimMap, Environment.TickCount64);
                        victim.Flush();
                    }
                }
            }
            else if (act.ActionType == 7)
            {
                // 随从驯化满 6 小时发狂叛变
                ClientSession? master = null;
                if (ActiveSessions.TryGetValue(act.TargetSessionId, out var ms))
                {
                    master = ms;
                }
                else
                {
                    master = GetAllGlobalSessions().FirstOrDefault(s => s.SessionId == act.TargetSessionId);
                }

                if (master != null)
                {
                    master.Pets.RemoveAll(p => p.Uid == act.MonsterUid);
                    master.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"你的随从 {act.MonsterName} 叛变了。");
                }
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
        if (TryParseInt(payload, out int npcUid))
        {
            if (NpcEngine != null)
            {
                var npc = NpcEngine.GetNpc(npcUid);
                if (npc != null)
                {
                    int dist = Math.Max(Math.Abs(npc.X - session.X), Math.Abs(npc.Y - session.Y));
                    // 盛大官方 1.5 铁律: 书店、药店、肉店老板等 NPC 位于柜台内，允许最大 4 格切比雪夫距离隔柜台对话
                    if (session.CurrentMapId != npc.MapId || dist > 4)
                    {
                        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "距离太远，无法与该目标对话！");
                        return;
                    }
                }
            }
            session.ActiveNpcUid = npcUid;

            if (ModernNpcEngine.HasPlugin(npcUid))
            {
                var (handled, script, closed) = ModernNpcEngine.DispatchDialog(session, npcUid, "@main");
                if (handled)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_NPCDIALOG, npcUid, 0, 0, 0, script);
                    return;
                }
            }

            if (NpcEngine != null)
            {
                string script = NpcEngine.GetNpcHomePage(npcUid);
                session.EnqueuePacket(ProtocolConsts.SM_NPCDIALOG, npcUid, 0, 0, 0, script);
            }
        }
    }

    private void HandleNpcSelect(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: npcUid/label 或 npcUid/0/0/0/label (CM_NPCSELECT 1011)
        var trimmed = payload.TrimEnd('!').TrimEnd('/');
        int slash = trimmed.IndexOf('/');
        if (slash < 0) return;

        if (int.TryParse(trimmed[..slash], out int npcUid))
        {
            session.ActiveNpcUid = npcUid;
            int lastSlash = trimmed.LastIndexOf('/');
            string label = (lastSlash >= 0 ? trimmed[(lastSlash + 1)..] : trimmed).ToString();

            if (ModernNpcEngine.HasPlugin(npcUid))
            {
                var (handled, script, closed, ctx) = ModernNpcEngine.DispatchDialogEx(session, npcUid, label);
                if (handled)
                {
                    if (ctx?.HasTeleportIntent == true)
                    {
                        int targetMapId = ctx.TargetMapId;
                        int targetX = ctx.TargetX;
                        int targetY = ctx.TargetY;
                        var oldMap = _mapManager.GetMap(session.CurrentMapId);
                        oldMap?.Aoi.Leave(session, session.X, session.Y);

                        session.CurrentMapId = targetMapId;
                        session.X = targetX;
                        session.Y = targetY;

                        var newMap = _mapManager.GetMap(targetMapId) ?? _mapManager.DefaultMap;
                        newMap.Aoi.Enter(session, targetX, targetY);

                        for (int p = 0; p < session.Pets.Count; p++)
                        {
                            var pet = session.Pets[p];
                            pet.CurrentMapId = targetMapId;
                            pet.X = targetX + p + 1;
                            pet.Y = targetY;
                        }

                        string mapName = string.IsNullOrEmpty(newMap.OfficialId) ? (newMap.Name ?? (targetMapId == 3 ? "盟重省" : (targetMapId == 0 ? "比奇省" : "未知"))) : NewMapExtra(newMap);
                        session.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, targetMapId, targetX, targetY, 0, mapName);
            RegenEngine?.PopulateMapNow(newMap.MapId); SendNearbyEntities(session, newMap); // 换图后下发新视野
                        if (!string.IsNullOrEmpty(ctx.TeleportMessage))
                        {
                            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, ctx.TeleportMessage);
                        }
                    }
                    if (!closed)
                    {
                        session.EnqueuePacket(ProtocolConsts.SM_NPCDIALOG, npcUid, 0, 0, 0, script);
                    }

                    // 盛大官方 1.5 铁律: 若选定 @buy 或包含 buy/买，下发商人在售货品清单 SM_MERCHANT_ITEM (645)
                    string lowLabel = label.ToLowerInvariant();
                    if (NpcEngine != null && (lowLabel == "@buy" || lowLabel.Contains("buy") || lowLabel.Contains("买")))
                    {
                        var npcObj = NpcEngine.GetNpc(npcUid);
                        if (npcObj != null && npcObj.Goods.Count > 0)
                        {
                            var sbGoods = new StringBuilder();
                            foreach (var g in npcObj.Goods)
                            {
                                sbGoods.Append($"{g.Name}\t{g.Price}\t{g.Weight}\t{g.MaxDura}\r\n");
                            }
                            session.EnqueuePacket(ProtocolConsts.SM_MERCHANT_ITEM, npcUid, 0, 0, 0, sbGoods.ToString());
                        }
                    }
                    else if (NpcEngine != null && (lowLabel == "@sell" || lowLabel.Contains("sell") || lowLabel.Contains("卖")))
                    {
                        session.EnqueuePacket(ProtocolConsts.SM_MERCHANT_SELL_LIST, npcUid, 0, 0, 0, string.Empty);
                    }
                    return;
                }
            }

            if (NpcEngine != null)
            {
                string script = NpcEngine.SelectScriptOption(npcUid, label);
                session.EnqueuePacket(ProtocolConsts.SM_NPCDIALOG, npcUid, 0, 0, 0, script);

                string lowLabel = label.ToLowerInvariant();
                if (lowLabel == "@buy" || lowLabel.Contains("buy") || lowLabel.Contains("买"))
                {
                    var npcObj = NpcEngine.GetNpc(npcUid);
                    if (npcObj != null && npcObj.Goods.Count > 0)
                    {
                        var sbGoods = new StringBuilder();
                        foreach (var g in npcObj.Goods)
                        {
                            sbGoods.Append($"{g.Name}\t{g.Price}\t{g.Weight}\t{g.MaxDura}\r\n");
                        }
                        session.EnqueuePacket(ProtocolConsts.SM_MERCHANT_ITEM, npcUid, 0, 0, 0, sbGoods.ToString());
                    }
                }
                else if (lowLabel == "@sell" || lowLabel.Contains("sell") || lowLabel.Contains("卖"))
                {
                    session.EnqueuePacket(ProtocolConsts.SM_MERCHANT_SELL_LIST, npcUid, 0, 0, 0, string.Empty);
                }
            }
        }
    }

    private void HandleBuyItem(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: npcUid/goodsId 或 goodsId/count/0/0/name 或 npcUid/goodsName 或 goodsName (CM_USERBUYITEM 1014)
        if (NpcEngine == null || session.State == null) return;
        var trimmed = payload.TrimEnd('!').TrimEnd('/');
        int npcUid = session.ActiveNpcUid;
        int goodsId = -1;
        string? reqName = null;

        int slash = trimmed.IndexOf('/');
        if (slash >= 0)
        {
            var firstPart = trimmed[..slash];
            var secondPart = trimmed[(slash + 1)..];

            if (int.TryParse(firstPart, out int firstVal))
            {
                if (firstVal >= 100000) // 第一个参数是 npcUid
                {
                    npcUid = firstVal;
                    session.ActiveNpcUid = npcUid;
                    if (int.TryParse(secondPart, out int secondVal))
                        goodsId = secondVal;
                    else
                        reqName = secondPart.ToString();
                }
                else
                {
                    goodsId = firstVal;
                    int lastSlash = trimmed.LastIndexOf('/');
                    if (lastSlash > slash)
                        reqName = trimmed[(lastSlash + 1)..].ToString();
                }
            }
            else
            {
                reqName = firstPart.ToString();
            }
        }
        else
        {
            if (int.TryParse(trimmed, out int val))
                goodsId = val;
            else
                reqName = trimmed.ToString();
        }

        if (npcUid > 0 && (goodsId > 0 || !string.IsNullOrWhiteSpace(reqName)))
        {
            var (success, errorMsg, item) = NpcEngine.BuyItem(session, npcUid, goodsId, reqName);
            if (success && item != null)
            {
                // 1. 下发官方购买成功: SM_BUY_OK_DELPHI (648)
                session.EnqueuePacket(ProtocolConsts.SM_BUY_OK_DELPHI, item.Uid, 0, 0, 0, item.Name);
                // 2. 刷新金币: SM_GOLDCHANGED_DELPHI (653)
                session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, 0, 0, 0);
                // 3. 入包通知: SM_ADDITEM (200)
                session.EnqueuePacket(ProtocolConsts.SM_ADDITEM, item.Uid, item.Weight, 0, 0, item.Name);
                session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);

                ServerLogger.Audit("BUY_ITEM", session.AccountName, session.CharacterName, $"从 NPC #{npcUid} 购买物品: {item.Name} (剩余金币: {session.State.Gold})", "ItemUid", item.Uid);
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
        if (NpcEngine == null || session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;
        int slash = payload.IndexOf('/');
        var uidSpan = slash >= 0 ? payload[..slash] : payload;
        if (int.TryParse(uidSpan, out int itemUid))
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

    public void HandleRepairItem(ClientSession session, ReadOnlySpan<char> payload, bool isSpecial)
    {
        if (session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;

        int firstNum = -1;
        int secondNum = -1;
        int slash = payload.IndexOf('/');
        if (slash >= 0)
        {
            int.TryParse(payload[..slash], out firstNum);
            var rest = payload[(slash + 1)..];
            int nextSlash = rest.IndexOf('/');
            var secondSpan = nextSlash >= 0 ? rest[..nextSlash] : rest;
            int.TryParse(secondSpan, out secondNum);
        }
        else
        {
            TryParseInt(payload, out firstNum);
        }

        int targetSlotOrUid = -1;
        EquipItem? equip = null;
        if (firstNum >= 0)
        {
            equip = NpcMerchantEngine.FindEquipOrBagItem(session, firstNum);
            if (equip != null) targetSlotOrUid = firstNum;
        }
        if (equip == null && secondNum >= 0)
        {
            equip = NpcMerchantEngine.FindEquipOrBagItem(session, secondNum);
            if (equip != null) targetSlotOrUid = secondNum;
        }

        if (equip == null)
        {
            session.EnqueuePacket(ProtocolConsts.SM_REPAIRFAIL_DELPHI, 0, 0, 0, 0, "未找到待修理的装备");
            return;
        }

        var res = isSpecial ? NpcEngine.SpecialRepairItem(session, targetSlotOrUid) : NpcEngine.NormalRepairItem(session, targetSlotOrUid);
        if (res.success)
        {
            session.EnqueuePacket(ProtocolConsts.SM_REPAIROK_DELPHI, session.State.Gold, equip.Dura, equip.MaxDura, 0, equip.Name);
            session.State.RecalculateAttributes();
            SendAbility(session);
        }
        else
        {
            session.EnqueuePacket(ProtocolConsts.SM_REPAIRFAIL_DELPHI, 0, 0, 0, 0, res.errorMsg);
        }
    }

    private void HandleDealTry(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: targetSessionId (CM_DEALTRY 1025)
        if (TradeEngine == null || session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;
        if (TryParseInt(payload, out int targetSessionId))
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
        if (TradeEngine == null || session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;
        if (TryParseInt(payload, out int itemUid))
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
        if (TradeEngine == null || session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;
        if (TryParseInt(payload, out int itemUid))
        {
            TradeEngine.DelItem(session, itemUid);
        }
    }

    private void HandleDealChgGold(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 格式: gold (CM_DEALCHGGOLD 1029)
        if (TradeEngine == null || session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;
        if (TryParseInt(payload, out int gold))
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

    private bool CheckStorageAccessible(ClientSession session)
    {
        if (!session.IsStorageOpen)
        {
            session.EnqueuePacket(ProtocolConsts.SM_CLOSESTORAGEDLG, 0, 0, 0, 0);
            return false;
        }

        if (session.StorageNpcUid > 0 && NpcEngine != null)
        {
            var npc = NpcEngine.GetNpc(session.StorageNpcUid);
            if (npc != null)
            {
                int dist = Math.Max(Math.Abs(session.X - npc.X), Math.Abs(session.Y - npc.Y));
                if (dist > 2)
                {
                    session.IsStorageOpen = false;
                    session.EnqueuePacket(ProtocolConsts.SM_CLOSESTORAGEDLG, 0, 0, 0, 0);
                    return false;
                }
            }
        }
        return true;
    }

    private void HandleStorageDeposit(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null) return;
        if (!CheckStorageAccessible(session)) return;

        string acc = !string.IsNullOrEmpty(session.AccountName) ? session.AccountName : session.CharacterName;
        if (!StorageSecEngine.CanAccessStorage(acc))
        {
            session.EnqueuePacket(ProtocolConsts.SM_NEEDPASSWORD, 0, 0, 0, 0, "请先输入仓库二级密码");
            return;
        }

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
                session.EnqueuePacket(ProtocolConsts.SM_SAVEITEM_OK, item.Id, slot >= 0 ? slot : 0, 0, 0, item.Name);
                session.EnqueuePacket(ProtocolConsts.SM_DELITEM, item.Id, slot >= 0 ? slot : 0, 0, 0, item.Name);
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
        if (!CheckStorageAccessible(session)) return;

        string acc = !string.IsNullOrEmpty(session.AccountName) ? session.AccountName : session.CharacterName;
        if (!StorageSecEngine.CanAccessStorage(acc))
        {
            session.EnqueuePacket(ProtocolConsts.SM_NEEDPASSWORD, 0, 0, 0, 0, "请先输入仓库二级密码");
            return;
        }

        if (!int.TryParse(payload, out int storageSlot)) return;

        var (res, item) = StorageEngine.WithdrawItem(session.State, storageSlot);
        switch (res)
        {
            case StorageEngine.StorageResult.Success when item != null:
                session.EnqueuePacket(ProtocolConsts.SM_TAKEBACKITEM_OK, item.Id, storageSlot, 0, 0, item.Name);
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
        if (!CheckStorageAccessible(session)) return;

        string acc = !string.IsNullOrEmpty(session.AccountName) ? session.AccountName : session.CharacterName;
        if (!StorageSecEngine.CanAccessStorage(acc))
        {
            session.EnqueuePacket(ProtocolConsts.SM_NEEDPASSWORD, 0, 0, 0, 0, "请先输入仓库二级密码");
            return;
        }

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
        if (!CheckStorageAccessible(session)) return;

        string acc = !string.IsNullOrEmpty(session.AccountName) ? session.AccountName : session.CharacterName;
        if (!StorageSecEngine.CanAccessStorage(acc))
        {
            session.EnqueuePacket(ProtocolConsts.SM_NEEDPASSWORD, 0, 0, 0, 0, "请先输入仓库二级密码");
            return;
        }

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
        long now = Environment.TickCount64;
        if (session.StoragePasswordLockUntilMs > now)
        {
            session.IsStorageOpen = false;
            session.EnqueuePacket(ProtocolConsts.SM_CLOSESTORAGEDLG, 0, 0, 0, 0);
            return;
        }

        string pass = payload.ToString().TrimEnd('!');
        int lastSlash = pass.LastIndexOf('/');
        if (lastSlash >= 0)
        {
            pass = pass[(lastSlash + 1)..];
        }

        string acc = !string.IsNullOrEmpty(session.AccountName) ? session.AccountName : session.CharacterName;
        var (secOk, secMsg) = StorageSecEngine.VerifyAndUnlock(acc, pass, now);
        bool statePassMatch = string.IsNullOrEmpty(session.State.StoragePassword) || session.State.StoragePassword == pass;

        if (secOk && statePassMatch)
        {
            session.StoragePasswordFailedAttempts = 0;
            session.State.IsStorageUnlocked = true;
            session.EnqueuePacket(ProtocolConsts.SM_PASSWORDSTATUS, 1, 0, 0, 0, "密码验证成功");
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "密码验证成功");
            session.EnqueuePacket(ProtocolConsts.SM_OPENSTORAGEDLG, 0, 0, 0, 0);
        }
        else
        {
            session.StoragePasswordFailedAttempts++;
            if (session.StoragePasswordFailedAttempts >= 3)
            {
                session.StoragePasswordLockUntilMs = now + 5 * 60 * 1000;
                session.IsStorageOpen = false;
                session.EnqueuePacket(ProtocolConsts.SM_CLOSESTORAGEDLG, 0, 0, 0, 0);
            }
            session.EnqueuePacket(ProtocolConsts.SM_PASSWORDSTATUS, 0, 0, 0, 0, "密码错误");
        }
    }

    private void HandleEatItem(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;

        long now = Environment.TickCount64;
        if (session.LastEatItemTimeMs > 0 && now - session.LastEatItemTimeMs < 300)
        {
            return;
        }
        session.LastEatItemTimeMs = now;
        int slot = -1;
        string itemName = string.Empty;

        string payloadStr = payload.ToString().TrimEnd('!', '/');
        int firstSlash = payloadStr.IndexOf('/');
        if (firstSlash >= 0)
        {
            TryParseInt(payloadStr.AsSpan(0, firstSlash), out slot);
            itemName = payloadStr[(firstSlash + 1)..].TrimEnd('/');
        }
        else
        {
            if (!TryParseInt(payloadStr, out slot))
            {
                itemName = payloadStr;
            }
        }

        // 查找目标使用物品
        EquipItem? targetItem = null;
        if (slot >= 0 && slot < session.State.BagItems.Count)
        {
            targetItem = session.State.BagItems[slot];
        }
        else if (!string.IsNullOrEmpty(itemName))
        {
            targetItem = session.State.BagItems.Find(i => i.Name.Contains(itemName));
        }

        // 药包解包判定 (如: 强效太阳水包, 超级金创药包, 超级魔法药包)
        if (targetItem != null && targetItem.Name.EndsWith("包"))
        {
            // 官方真值: 药包解开为 6 瓶单品，原占 1 格，净占 5 格。背包超过 35 格则无法容纳 6 瓶单品
            if (session.State.BagItems.Count + 5 > 40)
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "背包没有足够的空间。");
                return;
            }

            var bundle = targetItem;
            string singleName = bundle.Name[..^1];
            int singleWeight = bundle.Name.Contains("强效太阳水") ? 2 : Math.Max(1, bundle.Weight / 5);
            session.State.BagItems.Remove(bundle);
            session.EnqueuePacket(ProtocolConsts.SM_DELITEM, bundle.Uid, 0, 0, 0, bundle.Name);

            for (int i = 0; i < 6; i++)
            {
                var single = new EquipItem
                {
                    Uid = System.Threading.Interlocked.Increment(ref _nextGroundItemUid),
                    Name = singleName,
                    Weight = singleWeight,
                    Dura = 1,
                    MaxDura = 1
                };
                session.State.BagItems.Add(single);
                session.EnqueuePacket(ProtocolConsts.SM_ADDITEM, single.Uid, single.Weight, 0, 0, single.Name);
            }
            session.State.RecalculateAuthoritativeWeights(session.State.Gold);
            session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
            return;
        }

        // 地牢逃脱卷
        if (targetItem != null && targetItem.Name.Contains("地牢"))
        {
            var scroll = targetItem;
            session.State.BagItems.Remove(scroll);
            session.State.BagWeight = Math.Max(0, session.State.BagWeight - scroll.Weight);
            session.EnqueuePacket(ProtocolConsts.SM_DELITEM, scroll.Uid, 0, 0, 0, scroll.Name);
            session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);

            TradeEngine.CancelTrade(session, "使用传送卷轴");
            if (session.IsStorageOpen)
            {
                session.IsStorageOpen = false;
                session.EnqueuePacket(ProtocolConsts.SM_CLOSESTORAGEDLG, 0, 0, 0, 0);
            }

            var oldMap = _mapManager.GetMap(session.CurrentMapId);
            oldMap?.Aoi.Leave(session, session.X, session.Y);
            if (oldMap != null)
            {
                BroadcastToNinePatch(session, ProtocolConsts.SM_ENTITYDISAPPEAR, session.SessionId, session.X, session.Y, 0, session.CharacterName, includeSelf: false);
            }

            session.CurrentMapId = 0;
            session.X = 330;
            session.Y = 270;

            var targetMap = _mapManager.GetMap(0) ?? _mapManager.DefaultMap;
            targetMap.Aoi.Enter(session, 330, 270);
            BroadcastToNinePatch(session, ProtocolConsts.SM_ENTITYAPPEAR, session.SessionId, 330, 270, session.Direction, session.CharacterName, includeSelf: false);

            session.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, 0, 330, 270, 0, string.IsNullOrEmpty(targetMap.OfficialId) ? (targetMap.Name ?? "比奇省") : NewMapExtra(targetMap));
            RegenEngine?.PopulateMapNow(targetMap.MapId); SendNearbyEntities(session, targetMap); // 换图后下发新视野
            session.EnqueuePacket(ProtocolConsts.SM_EAT_OK, 0, 0, 0, 0);
            return;
        }

        // 随机传送卷
        if (targetItem != null && targetItem.Name.Contains("随机"))
        {
            var scroll = targetItem;
            session.State.BagItems.Remove(scroll);
            session.State.BagWeight = Math.Max(0, session.State.BagWeight - scroll.Weight);
            session.EnqueuePacket(ProtocolConsts.SM_DELITEM, scroll.Uid, 0, 0, 0, scroll.Name);
            session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);

            var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
            var (rOk, rX, rY, _) = TeleportScrollEngine.UseRandomTeleport(session, map, Random.Shared);
            if (rOk)
            {
                if (session.Pets.Count > 0)
                {
                    foreach (var pet in session.Pets)
                    {
                        BroadcastToCoords(map, pet.X, pet.Y, ProtocolConsts.SM_ENTITYDISAPPEAR, pet.Uid, pet.X, pet.Y, 0, pet.Name);
                        pet.X = session.X + 1;
                        pet.Y = session.Y;
                        BroadcastToCoords(map, pet.X, pet.Y, ProtocolConsts.SM_ENTITYAPPEAR, pet.Uid, pet.X, pet.Y, 0, pet.Name);
                    }
                }
            }
            session.EnqueuePacket(ProtocolConsts.SM_EAT_OK, 0, 0, 0, 0);
            return;
        }

        // 战神油与修复油
        if (itemName.Contains("战神油") || (slot >= 0 && slot < session.State.BagItems.Count && session.State.BagItems[slot].Name.Contains("战神油")))
        {
            var oilItem = slot >= 0 && slot < session.State.BagItems.Count && session.State.BagItems[slot].Name.Contains("战神油")
                ? session.State.BagItems[slot]
                : session.State.BagItems.Find(i => i.Name.Contains("战神油"));
            var weapon = session.State.Equipment[(int)OfficialEquipSlot.Weapon];
            if (weapon != null)
            {
                weapon.Dura = weapon.MaxDura;
                if (oilItem != null)
                {
                    session.State.BagItems.Remove(oilItem);
                    session.State.BagWeight = Math.Max(0, session.State.BagWeight - oilItem.Weight);
                    session.EnqueuePacket(ProtocolConsts.SM_DELITEM, oilItem.Uid, 0, 0, 0, oilItem.Name);
                }
                session.EnqueuePacket(ProtocolConsts.SM_EAT_OK, 0, 0, 0, 0);
                session.EnqueuePacket(ProtocolConsts.SM_TAKEON_OK, weapon.Uid, (int)OfficialEquipSlot.Weapon, 0, 0, weapon.Name);
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "已被完全特修完好");
                session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
                return;
            }
        }

        if (itemName.Contains("修复油") || (slot >= 0 && slot < session.State.BagItems.Count && session.State.BagItems[slot].Name.Contains("修复油")))
        {
            var oilItem = slot >= 0 && slot < session.State.BagItems.Count && session.State.BagItems[slot].Name.Contains("修复油")
                ? session.State.BagItems[slot]
                : session.State.BagItems.Find(i => i.Name.Contains("修复油"));
            var weapon = session.State.Equipment[(int)OfficialEquipSlot.Weapon];
            if (weapon != null)
            {
                weapon.MaxDura = Math.Max(1, weapon.MaxDura - 1);
                weapon.Dura = weapon.MaxDura;
                if (oilItem != null)
                {
                    session.State.BagItems.Remove(oilItem);
                    session.State.BagWeight = Math.Max(0, session.State.BagWeight - oilItem.Weight);
                    session.EnqueuePacket(ProtocolConsts.SM_DELITEM, oilItem.Uid, 0, 0, 0, oilItem.Name);
                }
                session.EnqueuePacket(ProtocolConsts.SM_EAT_OK, 0, 0, 0, 0);
                session.EnqueuePacket(ProtocolConsts.SM_TAKEON_OK, weapon.Uid, (int)OfficialEquipSlot.Weapon, 0, 0, weapon.Name);
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "最大持久下降了 1 点");
                session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
                return;
            }
        }

        // 祝福油权威判定 (ItemLifecycleEngine.ApplyBlessOilToEquippedWeaponEx)
        if (itemName.Contains("祝福油") || (slot >= 0 && slot < session.State.BagItems.Count && session.State.BagItems[slot].Name.Contains("祝福油")))
        {
            var (blessOk, blessMsg, oilItem, _) = ItemLifecycleEngine.ApplyBlessOilToEquippedWeaponEx(session, Random.Shared);
            if (oilItem != null)
            {
                session.EnqueuePacket(ProtocolConsts.SM_DELITEM, oilItem.Uid, 0, 0, 0, oilItem.Name);
            }
            session.EnqueuePacket(ProtocolConsts.SM_EAT_OK, 0, 0, 0, 0);
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, blessMsg);
            session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
            var weapon = session.State.Equipment[(int)OfficialEquipSlot.Weapon];
            if (weapon != null)
            {
                session.EnqueuePacket(ProtocolConsts.SM_TAKEON_OK, weapon.Uid, (int)OfficialEquipSlot.Weapon, 0, 0, weapon.Name);
            }
            SendAbility(session);
            return;
        }

        var (res, effect, consumedItem, consumedSlot) = PotionEngine.ConsumePotionEx(session.State, slot, itemName);
        if (res == PotionEngine.PotionResult.Success && effect != null)
        {
            if (consumedItem != null)
            {
                session.EnqueuePacket(ProtocolConsts.SM_DELITEM, consumedItem.Uid, consumedSlot >= 0 ? consumedSlot : 0, 0, 0, consumedItem.Name);
            }
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

    private static string ExtractTrailingName(ReadOnlySpan<char> payload)
    {
        string text = payload.ToString().TrimEnd('!').TrimEnd('/');
        int slash = text.LastIndexOf('/');
        return slash >= 0 ? text[(slash + 1)..] : text;
    }

    private void HandleAddGroupMember(ClientSession session, ReadOnlySpan<char> payload)
    {
        string targetName = ExtractTrailingName(payload);
        var map = _mapManager.GetMap(session.CurrentMapId);
        if (map == null) return;

        ClientSession? target = null;
        lock (_ninePatchBuffer)
        {
            map.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
            target = _ninePatchBuffer.Find(s => s.CharacterName == targetName);
        }
        if (target == null)
        {
            target = ActiveSessions.Values.FirstOrDefault(s => s.CharacterName == targetName);
        }

        if (target != null)
        {
            var res = GroupEngine.AddMember(session, target);
            if (res == GroupEngine.GroupResult.Success)
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"{target.CharacterName} 加入队伍。");
                target.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"你加入了 {session.CharacterName} 的队伍。");
            }
            else if (res == GroupEngine.GroupResult.TargetGroupModeDisabled)
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"{target.CharacterName} 拒绝组队。");
            }
        }
    }

    private void HandleDelGroupMember(ClientSession session, ReadOnlySpan<char> payload)
    {
        string targetName = ExtractTrailingName(payload);
        var party = GroupEngine.GetParty(session.SessionId);
        if (party == null) return;

        var target = party.Members.Find(m => m.CharacterName == targetName) ?? session;
        GroupEngine.RemoveMember(session, target);
    }

    private void HandleCreateGuild(ClientSession session, ReadOnlySpan<char> payload)
    {
        string guildName = ExtractTrailingName(payload);
        var (res, guild, _) = GuildEngine.CreateGuild(session, guildName);
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
        string targetName = ExtractTrailingName(payload);
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
        string targetName = ExtractTrailingName(payload);
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
        string notice = payload.ToString().TrimEnd('!');
        var res = GuildEngine.UpdateNotice(session, notice);
        if (res == GuildEngine.GuildResult.Success)
        {
            var guild = GuildEngine.GetGuildByPlayer(session.CharacterName);
            if (guild != null)
            {
                foreach (var active in ActiveSessions.Values)
                {
                    if (guild.Members.Contains(active.CharacterName))
                    {
                        active.EnqueuePacket(ProtocolConsts.SM_GUILDMESSAGE, 0, 0, 0, 0, notice);
                    }
                }
            }
            else
            {
                session.EnqueuePacket(ProtocolConsts.SM_GUILDMESSAGE, 0, 0, 0, 0, notice);
            }
        }
        else if (res == GuildEngine.GuildResult.NotMaster)
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "只有行会正掌门人方可修改发布行会公告");
        }
    }

    private void HandleGuildUpdateRank(ClientSession session, ReadOnlySpan<char> payload)
    {
        string payloadStr = payload.ToString().TrimEnd('!');
        string targetName = string.Empty;
        string newRank = string.Empty;

        var parts = payloadStr.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length >= 2)
        {
            newRank = parts[^1];
            targetName = parts[^2];
        }
        else
        {
            return;
        }

        var res = GuildEngine.UpdateMemberRank(session, targetName, newRank);
        if (res == GuildEngine.GuildResult.Success)
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"成功将【{targetName}】的职位修改为【{newRank}】");
            var targetSession = ActiveSessions.Values.FirstOrDefault(s => s.CharacterName.Equals(targetName, StringComparison.OrdinalIgnoreCase));
            if (targetSession != null)
            {
                string guildName = GuildEngine.GetPlayerGuild(targetName) ?? "无";
                targetSession.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"掌门人已将你的行会封号调整为【{newRank}】");
                targetSession.EnqueuePacket(ProtocolConsts.SM_GUILDNAME_DELPHI, targetSession.SessionId, 0, 0, 0, $"{guildName}/{newRank}");
                BroadcastToNinePatch(targetSession, ProtocolConsts.SM_GUILDNAME_DELPHI, targetSession.SessionId, targetSession.X, targetSession.Y, 0, $"{guildName}/{newRank}", includeSelf: false);
            }
        }
        else
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "修改封号失败。");
        }
    }

    private void HandleMineHit(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 挥镐挖矿判定 (CM_BIGHIT 3015，官方 sub_4CB7F8)
        if (session.State == null || session.State.IsDead || session.State.IsParalyzed) return;

        // 1. 检查当前地图是否为矿区
        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
        if (!MiningEngine.IsMiningMap(session.CurrentMapId) && !MiningEngine.IsMiningMap(map.Name))
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "这里不是矿区，无法开采矿石！");
            return;
        }

        // 2. 检查武器是否手持鹤嘴锄
        var weapon = session.State.Equipment[(int)OfficialEquipSlot.Weapon];
        if (!MiningEngine.IsPickaxe(weapon))
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你手中未装备鹤嘴锄，无法挖矿！");
            return;
        }

        // 3. 向周围九宫格旁观者广播挥镐动作 SM_OBJECTHIT
        BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTHIT, session.SessionId, session.X, session.Y, session.Direction, session.CharacterName, includeSelf: false);

        // 4. 调用盛大官方权威挖矿引擎执行结算
        var digResult = MiningEngine.DigOnce(session.State, session.CurrentMapId, Random.Shared);

        if (digResult.PickaxeBroken)
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "鹤嘴锄损坏了。");
            session.EnqueuePacket(ProtocolConsts.SM_DURACHANGE_REC, (int)OfficialEquipSlot.Weapon, digResult.PickaxeDura, digResult.PickaxeMaxDura, 0);
            return;
        }

        if (digResult.PickaxeWorn)
        {
            session.EnqueuePacket(ProtocolConsts.SM_DURACHANGE_REC, (int)OfficialEquipSlot.Weapon, digResult.PickaxeDura, digResult.PickaxeMaxDura, 0);
        }

        if (digResult.Success && digResult.MinedOre != null)
        {
            var ore = digResult.MinedOre;
            session.EnqueuePacket(ProtocolConsts.SM_MINERESULT, ore.Uid, ore.Shape, 0, 0, ore.Name);
            session.EnqueuePacket(ProtocolConsts.SM_ADDITEM, ore.Uid, 0, 0, 0, ore.Name);
            session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, digResult.Message);
        }
        else if (!string.IsNullOrEmpty(digResult.Message))
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, digResult.Message);
        }
    }

    private void HandleHeavyHit(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 刺杀剑术隔位刺杀判定 (CM_HEAVYHIT 3016 / CM_LONGHIT 3019)
        // 隔位破防刺杀：物理攻击伤害无视目标防御
        HandleHit(session, payload, isHeavyHit: true);
    }

    private void HandlePowerHit(ClientSession session, ReadOnlySpan<char> payload)
    {
        // 攻杀剑术重击判定 (CM_POWERHIT 3018)
        HandleHit(session, payload, isPowerHit: true);
    }

    private void HandleNewChar(ClientSession session, ReadOnlySpan<char> payload)
    {
        // CM_NEWCHAR 格式: Name\tJob\tGender
        var parts = payload.ToString().Split(new[] { '\t', '/' }, StringSplitOptions.RemoveEmptyEntries);
        string chrName = parts.Length > 0 ? parts[0].Trim() : string.Empty;
        int job = parts.Length > 1 && int.TryParse(parts[1], out int j) ? j : 0;
        int sex = parts.Length > 2 && int.TryParse(parts[2], out int s) ? s : 0;
        HandleNewCharInternal(session, chrName, job, sex);
    }

    private void HandleNewCharInternal(ClientSession session, string charName, int job, int sex, int hair = 1)
    {
        string account = session.AccountName;
        if (string.IsNullOrWhiteSpace(account))
        {
            session.EnqueuePacket(ProtocolConsts.SM_NEWCHAR_FAIL_DELPHI, 4, 0, 0, 0);
            return;
        }

        if (string.IsNullOrWhiteSpace(charName) || charName.Length > 14)
        {
            session.EnqueuePacket(ProtocolConsts.SM_NEWCHAR_FAIL_DELPHI, 4, 0, 0, 0);
            return;
        }

        bool success = false;
        int errCode = 0;
        if (PgPersistenceEngine != null)
        {
            (success, errCode) = PgPersistenceEngine.CreateCharacter(account, charName, job, sex, hair);
        }
        else
        {
            if (!_memoryCharacters.TryGetValue(account, out var list))
            {
                list = new List<(string, int, int, int)>();
                _memoryCharacters[account] = list;
            }
            if (list.Count >= 2)
            {
                errCode = 3; // 满2人
            }
            else if (list.Any(c => string.Equals(c.CharName, charName, StringComparison.OrdinalIgnoreCase)))
            {
                errCode = 2; // 重名
            }
            else
            {
                list.Add((charName, job, sex, 1));
                success = true;
            }
        }

        if (success)
        {
            ServerLogger.Audit("CREATE_CHAR_SUCCESS", account, charName, $"成功创建新角色: {charName}, 职业={job}, 性别={sex}");
            session.EnqueuePacket(ProtocolConsts.SM_NEWCHR_SUCCESS, session.SessionId, 0, 0, 0, charName);
        }
        else
        {
            ServerLogger.Audit("CREATE_CHAR_FAILED", account, charName, $"创角失败: 错误码={errCode}");
            session.EnqueuePacket(ProtocolConsts.SM_NEWCHAR_FAIL_DELPHI, errCode, 0, 0, 0);
        }
    }

    private void HandleDelChar(ClientSession session, ReadOnlySpan<char> payload)
    {
        // CM_DELCHAR 格式: 角色名 或 索引
        string target = payload.ToString().Trim();
        HandleDelCharInternal(session, target);
    }

    private void HandleDelCharInternal(ClientSession session, string target)
    {
        string account = session.AccountName;
        if (string.IsNullOrWhiteSpace(account))
        {
            session.EnqueuePacket(ProtocolConsts.SM_DELCHAR_FAIL_DELPHI, 0, 0, 0, 0);
            return;
        }

        bool success = false;
        if (PgPersistenceEngine != null)
        {
            if (int.TryParse(target, out int idx))
            {
                var chars = PgPersistenceEngine.GetCharactersByAccount(account);
                if (idx >= 0 && idx < chars.Count)
                {
                    target = chars[idx].CharName;
                }
            }
            success = PgPersistenceEngine.DeleteCharacter(account, target);
        }
        else
        {
            if (_memoryCharacters.TryGetValue(account, out var list))
            {
                if (int.TryParse(target, out int idx) && idx >= 0 && idx < list.Count)
                {
                    list.RemoveAt(idx);
                    success = true;
                }
                else
                {
                    int removed = list.RemoveAll(c => string.Equals(c.CharName, target, StringComparison.OrdinalIgnoreCase));
                    success = removed > 0;
                }
            }
        }

        if (success)
        {
            ServerLogger.Audit("DEL_CHAR_SUCCESS", account, target, $"成功删除角色: {target}");
            session.EnqueuePacket(ProtocolConsts.SM_DELCHR_SUCCESS, session.SessionId, 0, 0, 0);
        }
        else
        {
            ServerLogger.Audit("DEL_CHAR_FAILED", account, target, $"删角失败: {target}");
            session.EnqueuePacket(ProtocolConsts.SM_DELCHAR_FAIL_DELPHI, 0, 0, 0, 0);
        }
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
    public unsafe void DispatchBinary(ClientSession session, in BinaryProtocol.PacketHeader header, ReadOnlySpan<byte> payload)
    {
        switch ((BinaryProtocol.MsgType)header.MsgId)
        {
            case BinaryProtocol.MsgType.HandshakeRequest:
                HandleBinaryHandshake(session);
                break;

            case BinaryProtocol.MsgType.IntentTurn:
                HandleBinaryTurnIntent(session, payload);
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

            case BinaryProtocol.MsgType.IntentSplitItem:
                HandleBinarySplitItemIntent(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentShopSell:
                HandleBinaryShopSellIntent(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentClickNpc:
                HandleBinaryClickNpc(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentNpcSelect:
                HandleBinaryNpcSelect(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentShopBuy:
                HandleBinaryShopBuyIntent(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentRepair:
            case BinaryProtocol.MsgType.IntentSpecialRepair:
                HandleBinaryRepairIntent(session, (BinaryProtocol.MsgType)header.MsgId, payload);
                break;

            case BinaryProtocol.MsgType.IntentTrade:
                HandleBinaryTradeIntent(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentStorageDeposit:
                if (payload.Length >= 8)
                {
                    var dep = BinaryProtocol.StorageDepositIntentPayload.Read(payload);
                    HandleStorageDeposit(session, $"{dep.BagSlot}");
                }
                break;

            case BinaryProtocol.MsgType.IntentStorageWithdraw:
                if (payload.Length >= 4)
                {
                    var wit = BinaryProtocol.StorageWithdrawIntentPayload.Read(payload);
                    HandleStorageWithdraw(session, $"{wit.StorageSlot}");
                }
                break;

            case BinaryProtocol.MsgType.IntentStorageDepositGold:
                if (payload.Length >= 4)
                {
                    var goldDep = BinaryProtocol.StorageDepositGoldIntentPayload.Read(payload);
                    HandleStorageSaveGold(session, $"{goldDep.GoldAmount}");
                }
                break;

            case BinaryProtocol.MsgType.IntentStorageWithdrawGold:
                if (payload.Length >= 4)
                {
                    var goldWit = BinaryProtocol.StorageWithdrawGoldIntentPayload.Read(payload);
                    HandleStorageTakeGold(session, $"{goldWit.GoldAmount}");
                }
                break;

            case BinaryProtocol.MsgType.IntentStoragePassword:
                if (payload.Length >= sizeof(BinaryProtocol.StoragePasswordIntentPayload))
                {
                    fixed (byte* p = payload)
                    {
                        var pwd = *(BinaryProtocol.StoragePasswordIntentPayload*)p;
                        HandleStoragePassword(session, pwd.ReadPassword());
                    }
                }
                break;

            case BinaryProtocol.MsgType.IntentGroupAction:
                HandleBinaryGroupAction(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentGuildAction:
                HandleBinaryGuildAction(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentSay:
                HandleSay(session, Encoding.UTF8.GetString(payload));
                break;

            case BinaryProtocol.MsgType.IntentWhisper:
                if (payload.Length >= sizeof(BinaryProtocol.WhisperIntentPayload))
                {
                    fixed (byte* p = payload)
                    {
                        var whisper = *(BinaryProtocol.WhisperIntentPayload*)p;
                        string target = whisper.ReadTargetName();
                        string msg = whisper.ReadMessage();
                        HandleSay(session, $"/{target} {msg}");
                    }
                }
                break;

            case BinaryProtocol.MsgType.IntentAdjustBonus:
                if (payload.Length >= 1)
                {
                    var bonus = BinaryProtocol.AdjustBonusIntentPayload.Read(payload);
                    HandleAdjustBonusPacket(session, $"{bonus.BonusType}");
                }
                break;

            case BinaryProtocol.MsgType.IntentMagicKey:
                if (payload.Length >= 3 && session.State != null)
                {
                    var mk = BinaryProtocol.MagicKeyIntentPayload.Read(payload);
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"快捷键设置已更新");
                }
                break;

            case BinaryProtocol.MsgType.IntentWantMinimap:
                HandleWantMinimap(session);
                break;

            case BinaryProtocol.MsgType.IntentAttackMode:
                if (payload.Length >= 1 && session.State != null)
                {
                    var mode = BinaryProtocol.AttackModeIntentPayload.Read(payload);
                    session.State.AttackMode = mode.Mode;
                    string modeName = mode.Mode switch {
                        0 => "全保模式",
                        1 => "和平模式",
                        2 => "组队模式",
                        3 => "行会模式",
                        4 => "善恶模式",
                        _ => "全体攻击"
                    };
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"攻击模式已切换为：{modeName}");
                }
                break;

            case BinaryProtocol.MsgType.IntentToggleSkill:
                if (payload.Length >= 2 && session.State != null)
                {
                    var toggle = BinaryProtocol.ToggleSkillIntentPayload.Read(payload);
                    if (toggle.SkillType == 0) session.State.BoThrusting = toggle.Enabled == 1;
                    else if (toggle.SkillType == 1) session.State.BoHalfMoon = toggle.Enabled == 1;
                }
                break;

            case BinaryProtocol.MsgType.IntentRevive:
                HandleRevive(session, ReadOnlySpan<char>.Empty);
                break;

            case BinaryProtocol.MsgType.IntentButch:
                if (payload.Length >= 8)
                {
                    var butch = BinaryProtocol.ButchIntentPayload.Read(payload);
                    HandleButchItemPacket(session, $"{butch.TargetUid}");
                }
                break;

            case BinaryProtocol.MsgType.IntentQueryUser:
                if (payload.Length >= 4)
                {
                    var query = BinaryProtocol.QueryUserIntentPayload.Read(payload);
                    DispatchIdent(session, 82, $"{query.TargetUid}");
                }
                break;

            case BinaryProtocol.MsgType.IntentSoftClose:
                HandleLogout(session);
                break;

            case BinaryProtocol.MsgType.IntentEquipItem:
                HandleBinaryEquipItemIntent(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentTakeoffItem:
                HandleBinaryTakeoffItemIntent(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentDropGold:
                HandleBinaryDropGoldIntent(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentDropItem:
                HandleBinaryDropItemIntent(session, payload);
                break;

            case BinaryProtocol.MsgType.CreateAccountRequest:
                HandleBinaryCreateAccount(session, payload);
                break;

            case BinaryProtocol.MsgType.ChangePasswordRequest:
                HandleBinaryChangePassword(session, payload);
                break;

            case BinaryProtocol.MsgType.LoginRequest:
                HandleBinaryLogin(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentSelectServer:
                HandleBinarySelectServer(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentQueryChar:
                HandleQueryCharInfo(session);
                break;

            case BinaryProtocol.MsgType.SelectCharRequest:
                HandleBinarySelectChar(session, payload);
                break;

            case BinaryProtocol.MsgType.CreateCharRequest:
                HandleBinaryCreateChar(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentDeleteChar:
                HandleBinaryDeleteChar(session, payload);
                break;

            case BinaryProtocol.MsgType.IntentLoginNoticeOk:
                // 客户端大世界视口已挂载就绪 ACK，无需重复下发全量登场包避免聊天与状态双重刷屏
                break;

            case BinaryProtocol.MsgType.Heartbeat:
                // 心跳回执
                session.EnqueueBinaryPacket((ushort)BinaryProtocol.MsgType.Heartbeat, ReadOnlySpan<byte>.Empty);
                break;

            default:
                // HPSocket TCP-Pack 封包直通业务 Ident 派发 (彻底消除任何 '#' 字符定界符解析)
                string remaining = payload.IsEmpty ? string.Empty : Encoding.UTF8.GetString(payload);
                DispatchIdent(session, (short)header.MsgId, remaining);
                break;
        }
    }

    private void HandleBinarySelectServer(ClientSession session, ReadOnlySpan<byte> payload)
    {
        // 回复选服成功 SM_SELECTSERVER_OK 并下发服务器列表 SM_SERVERINDEX (114)
        session.EnqueuePacket(ProtocolConsts.SM_SELECTSERVER_OK, 0, 0, 0, 0);
        session.EnqueuePacket(114, 0, 0, 0, 0, "雷霆一区\t光芒二区\t烈焰三区");
    }

    private unsafe void HandleBinarySelectChar(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < sizeof(BinaryProtocol.SelectCharIntentPayload)) return;
        fixed (byte* p = payload)
        {
            var intent = *(BinaryProtocol.SelectCharIntentPayload*)p;
            string charName = intent.ReadCharName();
            if (string.IsNullOrEmpty(charName))
            {
                charName = $"{session.AccountName}_战神";
            }
            HandleSelectChar(session, charName.AsSpan());
        }
    }

    private unsafe void HandleBinaryCreateChar(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < sizeof(BinaryProtocol.CreateCharIntentPayload)) return;
        fixed (byte* p = payload)
        {
            var intent = *(BinaryProtocol.CreateCharIntentPayload*)p;
            string charName = intent.ReadCharName();
            if (string.IsNullOrEmpty(charName))
            {
                charName = $"{session.AccountName}_新角色";
            }
            HandleNewCharInternal(session, charName, intent.Job, intent.Gender, intent.Hair);
        }
    }

    private unsafe void HandleBinaryDeleteChar(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < sizeof(BinaryProtocol.DeleteCharIntentPayload)) return;
        fixed (byte* p = payload)
        {
            var intent = *(BinaryProtocol.DeleteCharIntentPayload*)p;
            HandleDelCharInternal(session, intent.CharIndex.ToString());
        }
    }

    public void HandleBinaryEquipItemIntent(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < 5 || session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;
        var equip = BinaryProtocol.EquipItemIntentPayload.Read(payload);
        int bagSlot = session.State.BagItems.FindIndex(i => i.Uid == equip.ItemUid);
        if (bagSlot < 0 && equip.ItemUid >= 0 && equip.ItemUid < session.State.BagItems.Count)
        {
            bagSlot = equip.ItemUid;
        }
        if (bagSlot >= 0 && equip.TargetSlot < 9)
        {
            byte equipSlot = equip.TargetSlot;
            bool success = HandleWearItem(session, bagSlot, (OfficialEquipSlot)equipSlot);
            if (success)
            {
                var wornEquip = session.State.Equipment[equipSlot];
                session.EnqueuePacket(ProtocolConsts.SM_TAKEON_OK, session.SessionId, equipSlot, 0, 0, wornEquip?.Name ?? string.Empty);
                session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
                SendAbility(session);
            }
            else
            {
                session.EnqueuePacket(ProtocolConsts.SM_TAKEON_FAIL, session.SessionId, equipSlot, 0, 0);
            }
        }
    }

    public void HandleBinaryTakeoffItemIntent(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < 1 || session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;
        var takeoff = BinaryProtocol.TakeoffItemIntentPayload.Read(payload);
        byte slot = takeoff.TargetSlot;
        if (slot >= 9) return;

        var equip = session.State.Equipment[slot];
        if (equip == null)
        {
            session.EnqueuePacket(ProtocolConsts.SM_TAKEOFF_FAIL, session.SessionId, slot, 0, 0);
            return;
        }

        if (equip.Curse > 0 && equip.Name.Contains("神秘"))
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "装备被神秘诅咒力量缠绕，无法脱下！");
            session.EnqueuePacket(ProtocolConsts.SM_TAKEOFF_FAIL, session.SessionId, slot, 0, 0);
            return;
        }

        if (session.State.BagItems.Count >= 40)
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "背包已满，无法卸下装备");
            session.EnqueuePacket(ProtocolConsts.SM_TAKEOFF_FAIL, session.SessionId, slot, 0, 0);
            return;
        }

        session.State.Equipment[slot] = null;
        session.State.BagItems.Add(equip);
        session.State.RecalculateAuthoritativeWeights(session.State.Gold);
        session.State.RecalculateAttributes();

        session.EnqueuePacket(ProtocolConsts.SM_TAKEOFF_OK, session.SessionId, slot, 0, 0);
        int bagIdx = session.State.BagItems.Count - 1;
        int looks = equip.Looks > 0 ? equip.Looks : Mir2.Shared.Data.OfficialStdItemsRegistry.ResolveLooks(equip.Name, 1);
        session.EnqueuePacket(ProtocolConsts.SM_ADDITEM, equip.Uid, bagIdx, equip.Weight, 0, $"{equip.Name}\t{equip.Dura}\t{equip.MaxDura}\t{looks}");
        session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
        SendAbility(session);
    }

    public void HandleBinaryDropGoldIntent(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < 4 || session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;
        var drop = BinaryProtocol.DropGoldIntentPayload.Read(payload);
        int amount = drop.Gold;
        if (amount <= 0 || session.State.Gold < amount) return;

        session.State.Gold -= amount;
        session.State.RecalculateAuthoritativeWeights(session.State.Gold);

        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
        var goldEntity = new GroundItemEntity
        {
            Uid = System.Threading.Interlocked.Increment(ref _nextGroundItemUid),
            Name = "金币",
            Count = amount,
            IsGold = true,
            X = session.X,
            Y = session.Y,
            DropTickMs = Environment.TickCount64
        };
        map.AddGroundItem(goldEntity);

        session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, 0, 0, 0);
        session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
        BroadcastToNinePatch(session, ProtocolConsts.SM_ITEMSHOW, goldEntity.Uid, goldEntity.X, goldEntity.Y, 111, goldEntity.Name, includeSelf: true);
    }

    public void HandleBinaryDropItemIntent(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < 8 || session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;
        var drop = BinaryProtocol.DropItemIntentPayload.Read(payload);
        int slot = drop.BagSlot;
        if (slot >= 0 && slot < session.State.BagItems.Count)
        {
            var item = session.State.BagItems[slot];
            if (drop.ItemUid == 0 || item.Uid == drop.ItemUid)
            {
                var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
                if (map.GetGroundItemsAt(session.X, session.Y).Count >= 8)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "地面上物品堆叠已满，无法丢弃！");
                    return;
                }

                session.State.BagItems.RemoveAt(slot);
                session.State.BagWeight = Math.Max(0, session.State.BagWeight - item.Weight);
                session.State.RecalculateAuthoritativeWeights(session.State.Gold);
                session.State.RecalculateAttributes();

                var groundItem = new GroundItemEntity
                {
                    Uid = System.Threading.Interlocked.Increment(ref _nextGroundItemUid),
                    Name = item.Name,
                    Count = 1,
                    IsGold = false,
                    X = session.X,
                    Y = session.Y,
                    OwnerSessionId = 0,
                    DropTickMs = Environment.TickCount64,
                    Weight = item.Weight
                };
                map.AddGroundItem(groundItem);

                // 1. 广播地表显示物品: SM_ITEMSHOW (625)
                BroadcastToNinePatch(session, ProtocolConsts.SM_ITEMSHOW, groundItem.Uid, session.X, session.Y, item.Shape, groundItem.Name, includeSelf: true);
                // 2. 下发客户端背包物理移除确认: SM_DELITEM (202)
                session.EnqueuePacket(ProtocolConsts.SM_DELITEM, item.Uid, slot, 0, 0, item.Name);
                session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
                SendAbility(session);
            }
        }
    }

    public void HandleBinarySplitItemIntent(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < 8 || session.State == null) return;
        var split = BinaryProtocol.SplitItemIntentPayload.Read(payload);
        int slot = split.BagSlot;
        int count = split.SplitCount;
        if (slot < 0 || slot >= session.State.BagItems.Count || count <= 0) return;
        var item = session.State.BagItems[slot];
        if (item == null || item.Dura <= count) return;
        if (session.State.BagItems.Count >= 40) return;

        item.Dura -= count;
        var newItem = new EquipItem
        {
            Uid = (int)(Environment.TickCount64 % 1000000) + Random.Shared.Next(1, 1000),
            Name = item.Name,
            Shape = item.Shape,
            Weight = item.Weight,
            Dura = count,
            MaxDura = item.MaxDura,
            Ac = item.Ac,
            MaxAc = item.MaxAc,
            Mac = item.Mac,
            MaxMac = item.MaxMac,
            Dc = item.Dc,
            MaxDc = item.MaxDc,
            Mc = item.Mc,
            MaxMc = item.MaxMc,
            Sc = item.Sc,
            MaxSc = item.MaxSc,
            BonusDc = item.BonusDc,
            BonusMc = item.BonusMc,
            BonusSc = item.BonusSc,
            BonusAc = item.BonusAc,
            BonusMac = item.BonusMac,
            Luck = item.Luck,
            Curse = item.Curse
        };
        session.State.BagItems.Add(newItem);
    }

    public void HandleBinaryShopSellIntent(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < 8 || session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;
        var sell = BinaryProtocol.ShopSellIntentPayload.Read(payload);
        int itemUid = sell.ItemUid;

        if (NpcEngine != null)
        {
            var (success, goldEarned, itemName, errorMsg) = NpcEngine.SellItem(session, itemUid);
            if (success)
            {
                session.EnqueuePacket(ProtocolConsts.SM_SELL_OK_DELPHI, itemUid, goldEarned, 0, 0, itemName);
                session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, 0, 0, 0);
                session.EnqueuePacket(ProtocolConsts.SM_DELITEM, itemUid, 0, 0, 0, itemName);
                session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
            }
            else
            {
                session.EnqueuePacket(ProtocolConsts.SM_SELL_FAIL_DELPHI, itemUid, 0, 0, 0, errorMsg);
            }
        }
        else
        {
            var item = session.State.BagItems.Find(i => i.Uid == itemUid);
            if (item != null)
            {
                session.State.BagItems.Remove(item);
                session.State.BagWeight = Math.Max(0, session.State.BagWeight - item.Weight);
                int sellPrice = Math.Max(100, item.Dura * 50 + item.Weight * 20);
                session.State.Gold += sellPrice;
                session.EnqueuePacket(ProtocolConsts.SM_SELL_OK_DELPHI, itemUid, sellPrice, 0, 0, item.Name);
                session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, 0, 0, 0);
                session.EnqueuePacket(ProtocolConsts.SM_DELITEM, item.Uid, 0, 0, 0, item.Name);
                session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
            }
        }
    }

    public void HandleBinaryShopBuyIntent(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < 12 || session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;
        var buy = BinaryProtocol.ShopBuyIntentPayload.Read(payload);
        int npcUid = buy.NpcUid > 0 ? buy.NpcUid : session.ActiveNpcUid;
        if (npcUid > 0)
        {
            HandleBuyItem(session, $"{npcUid}/{buy.GoodsIndex}");
        }
    }

    private void HandleBinaryClickNpc(ClientSession session, ReadOnlySpan<byte> payload)
    {
        string npcTarget = Encoding.UTF8.GetString(payload).Trim();
        int npcUid = -1;
        if (int.TryParse(npcTarget, out int parsedUid))
        {
            npcUid = parsedUid;
        }
        else if (NpcEngine != null)
        {
            foreach (var n in NpcEngine.AllNpcs)
            {
                if (string.Equals(n.Name, npcTarget, StringComparison.OrdinalIgnoreCase))
                {
                    npcUid = n.Uid;
                    break;
                }
            }
        }
        if (npcUid > 0)
        {
            session.ActiveNpcUid = npcUid;
            HandleNpcHomePage(session, $"{npcUid}");
        }
    }

    private void HandleBinaryNpcSelect(ClientSession session, ReadOnlySpan<byte> payload)
    {
        var (npcUid, label) = BinaryProtocol.NpcSelectIntentPayload.Read(payload);
        if (npcUid <= 0 && session.ActiveNpcUid > 0) npcUid = session.ActiveNpcUid;
        if (npcUid > 0)
        {
            HandleNpcSelect(session, $"{npcUid}/{label}");
        }
    }

    private void HandleBinaryRepairIntent(ClientSession session, BinaryProtocol.MsgType type, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < 9) return;
        var repair = BinaryProtocol.RepairIntentPayload.Read(payload);
        bool isSpecial = type == BinaryProtocol.MsgType.IntentSpecialRepair || repair.IsSpecial == 1;
        HandleRepairItem(session, $"{repair.ItemUid}", isSpecial);
    }

    private void HandleBinaryTradeIntent(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < 13) return;
        var trade = BinaryProtocol.TradeIntentPayload.Read(payload);
        switch (trade.Action)
        {
            case 0: HandleDealTry(session, $"{trade.TargetSessionId}"); break;
            case 1: HandleDealAddItem(session, $"{trade.ItemUid}"); break;
            case 2: HandleDealDelItem(session, $"{trade.ItemUid}"); break;
            case 3: HandleDealChgGold(session, $"{trade.Gold}"); break;
            case 4: HandleDealEnd(session); break;
            case 5: HandleDealCancel(session); break;
        }
    }

    private void HandleBinaryGroupAction(ClientSession session, ReadOnlySpan<byte> payload)
    {
        var (grpAct, target) = BinaryProtocol.GroupActionIntentPayload.Read(payload);
        switch (grpAct)
        {
            case 0: HandleCreateGroup(session, target); break;
            case 1: HandleAddGroupMember(session, target); break;
            case 2: HandleDelGroupMember(session, target); break;
            case 3: HandleGroupMode(session, target); break;
        }
    }

    private void HandleBinaryGuildAction(ClientSession session, ReadOnlySpan<byte> payload)
    {
        var (guildAct, p1, p2) = BinaryProtocol.GuildActionIntentPayload.Read(payload);
        switch (guildAct)
        {
            case 0:
            case 1:
                HandleOpenGuild(session);
                break;
            case 2: HandleGuildAddMember(session, p1); break;
            case 3: HandleGuildKickMember(session, p1); break;
            case 4: HandleGuildAllyRequest(session, p1); break;
            case 5: HandleGuildBreakAlly(session, p1); break;
            case 6: HandleGuildUpdateNotice(session, p1); break;
            case 7: HandleGuildUpdateRank(session, $"{p1}/{p2}"); break;
            case 8:
                if (GuildWarEngine != null && !string.IsNullOrEmpty(p1))
                {
                    string myGuild = GuildEngine.GetPlayerGuild(session.CharacterName) ?? "战神殿";
                    GuildWarEngine.DeclareWar(myGuild, p1, Environment.TickCount64);
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"{myGuild} 向 {p1} 宣战。");
                }
                break;
            case 9:
                if (GuildWarEngine != null && !string.IsNullOrEmpty(p1))
                {
                    string myGuild = GuildEngine.GetPlayerGuild(session.CharacterName) ?? "战神殿";
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"{myGuild} 与 {p1} 停止行会战。");
                }
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
    /// 权威转向意图处理并向九宫格同屏广播
    /// </summary>
    public void HandleBinaryTurnIntent(ClientSession session, ReadOnlySpan<byte> payload)
    {
        if (payload.Length < 1) return;
        byte dir = (byte)(payload[0] & 7);
        session.Direction = dir;
        BroadcastToNinePatch(session, ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, session.X, session.Y, dir, session.CharacterName, includeSelf: false);
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

        // 3. 移速反加速外挂校验 (基于真实服务器物理时间戳与连续违规容差)
        long nowMs = Environment.TickCount64;
        if (session.LastMoveTimeMs > 0)
        {
            int fastMoves = session.ConsecutiveFastMoves;
            var (isSpeedHack, shouldRollback) = EconomySecurityEngine.SpeedHackDetectorEngine.CheckMovePacket(nowMs, session.LastMoveTimeMs, ref fastMoves);
            session.ConsecutiveFastMoves = fastMoves;
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

        // 4.1 盛大官方跳点传送权威检测 (Passage Trigger)
        var passage = _mapManager.FindPassage(session.CurrentMapId, newX, newY);
        if (passage != null)
        {
            TriggerMapPassage(session, passage);
            return;
        }

        // 5. 更新九宫格 AOI 空间分区并在跨 Zone 时执行双向差分广播
        UpdateAoiPositionWithTransition(map, session, oldX, oldY, newX, newY);

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
        if (session.LastAttackTimeMs > 0 && !EconomySecurityEngine.AttackSpeedValidatorEngine.IsAttackValid(nowMs, session.LastAttackTimeMs, session.State?.AttackSpeed ?? 0))
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
            var (rawDmg, hit, isArmorPiercing, isFireHit, died, _) = ResolveAuthoritativePhysicalCombat(session, defender, dist, nowMs);

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

                if (died)
                {
                    AuthoritativeExecutePlayerDeath(defender, session, map, nowMs);
                }
            }
        }
        else if (RegenEngine != null && session.State != null)
        {
            var (dx, dy) = MonsterRegenEngine.GetDirOffset(attack.Direction);
            int targetX = session.X + dx;
            int targetY = session.Y + dy;

            var monster = (attack.TargetUid > 0 ? RegenEngine.FindMonsterByUid(attack.TargetUid) : null)
                          ?? RegenEngine.FindMonsterAt(session.CurrentMapId, targetX, targetY);
            if (monster != null && !monster.IsDead)
            {
                int dist = Math.Max(Math.Abs(monster.X - session.X), Math.Abs(monster.Y - session.Y));
                var (rawDmg, hit, isFireHit, died) = ResolveAuthoritativeMonsterPhysicalCombat(session, monster, dist, nowMs);
                if (hit && rawDmg > 0)
                {
                    monster.ThreatTable.AddHate(session.SessionId, rawDmg, isTaunt: false);
                    if (monster.IsStoneForm) monster.IsStoneForm = false;
                    if (monster.NextActionTimeMs > nowMs + 1000)
                    {
                        monster.NextActionTimeMs = nowMs + 500;
                        monster.TargetSessionId = session.SessionId;
                    }
                    if (monster.IsTrappedByHolyCurse)
                    {
                        monster.IsTrappedByHolyCurse = false;
                        monster.TrappedExpireMs = 0;
                        monster.NextActionTimeMs = nowMs;
                        monster.TargetSessionId = session.SessionId;
                        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "困魔咒破除了。");
                    }

                    if (isFireHit)
                    {
                        var (tFire, _, mFire, _) = SkillTrainEngine.TrainSkill(session.State, 26, 1);
                        if (tFire && mFire != null)
                        {
                            session.EnqueuePacket(ProtocolConsts.SM_UPDATEUSERMAGIC, mFire.MagicId, mFire.Level, mFire.Key, mFire.CurTrain);
                        }
                    }
                    else if (attack.SkillType == 1 || dist == 2)
                    {
                        var (tStab, _, mStab, _) = SkillTrainEngine.TrainSkill(session.State, 12, 1);
                        if (tStab && mStab != null)
                        {
                            session.EnqueuePacket(ProtocolConsts.SM_UPDATEUSERMAGIC, mStab.MagicId, mStab.Level, mStab.Key, mStab.CurTrain);
                        }
                    }

                    // 广播怪物后仰动画: SM_OBJECTSTRUCK (244)
                    BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, monster.Uid, monster.X, monster.Y, rawDmg, monster.Name, includeSelf: true);

                    // 怪物死亡判定与经验分配
                    if (died)
                    {
                        RegenEngine.OnMonsterDied(monster.Uid);
                        BroadcastToNinePatch(session, ProtocolConsts.SM_DEATH_ACTION_DELPHI, monster.Uid, monster.X, monster.Y, 0, monster.Name, includeSelf: true);
                        OnMonsterKilledByPlayer(session, monster);
                    }
                }
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
        int bagSlot = payload.Length >= 8 ? System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(payload[4..8]) : -1;

        if (session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;

        int targetIdx = -1;
        if (itemUid > 0)
        {
            targetIdx = session.State.BagItems.FindIndex(it => it.Uid == itemUid);
        }
        if (targetIdx < 0 && bagSlot >= 0 && bagSlot < session.State.BagItems.Count)
        {
            targetIdx = bagSlot;
        }

        if (targetIdx >= 0 && targetIdx < session.State.BagItems.Count)
        {
            var item = session.State.BagItems[targetIdx];
            HandleEatItem(session, $"{targetIdx}/{item.Name}");
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
    public (int damage, bool hit, bool isArmorPiercing, bool isFireHit, bool died, int absorbed) ResolveAuthoritativePhysicalCombat(
        ClientSession attacker, ClientSession defender, int dist, long nowMs, bool forcePierce = false)
    {
        if (attacker.State == null || defender.State == null)
            return (0, false, false, false, false, 0);

        // 1. 距离校验 (贴身 1 格，隔位刺杀 2 格，大于 2 格物理打不着)
        if (dist > 2) return (0, false, false, false, false, 0);

        bool isArmorPiercing = forcePierce || (dist == 2 && attacker.State.StabbingTracker.AutoStab);
        if (dist == 2 && !isArmorPiercing)
        {
            // 距离为 2 但未开刺杀，打不到目标
            return (0, false, false, false, false, 0);
        }

        // 2. 物理命中对抗 (IDA sub_4C1EF4: rollSpeed < Hit)
        int rollSpeed = OfficialClientRulesEngine.DelphiRandom.Shared.Next(defender.State.Speed + 1);
        bool hit = rollSpeed < attacker.State.Hit;
        if (!hit)
        {
            return (0, false, isArmorPiercing, false, false, 0);
        }

        // 3. 基础伤害精算 (结合武器与项链幸运/诅咒与运九套满攻爆发)
        var weapon = attacker.State.Equipment[(int)OfficialEquipSlot.Weapon];
        int minDc = Math.Max(0, attacker.State.Dc);
        int maxDc = Math.Max(minDc, attacker.State.MaxDc);
        int rawDmg = (weapon != null && weapon.Dura <= 0) ? 1 : attacker.State.CalculateAttackDamageWithLuck(minDc, maxDc, Random.Shared);

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
            attacker.State.BoFireHit = false;
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
                defender.State.ParalyzeExpireMs = nowMs + 5000;
            }
        }

        // 9. 伤害扣减结算 (隔位刺杀 isArmorPiercing 击穿魔法盾与基础护甲)
        var (finalHp, finalMp, died, revived, absorbed, shieldBroken) = defender.State.TakeCombatDamageFull(rawDmg, nowMs, isArmorPiercing: isArmorPiercing);

        return (rawDmg, true, isArmorPiercing, isFireHit, died, absorbed);
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

        bool isArmorPiercing = dist == 2 && (attacker.State.BoThrusting || attacker.State.StabbingTracker.AutoStab);
        if (dist == 2)
        {
            if (!isArmorPiercing) return (0, false, false, false);
            var map = _mapManager.GetMap(attacker.CurrentMapId) ?? _mapManager.DefaultMap;
            int midX = (attacker.X + monster.X) / 2;
            int midY = (attacker.Y + monster.Y) / 2;
            if (!map.IsWalkable(midX, midY))
            {
                return (0, false, false, false); // 隔位厚墙吸收剑气
            }
        }

        // IDA sub_4C1EF4 @ 0x4C2180: RandInt(目标敏捷) >= 攻击者准确 判定 Miss；无官方属性的怪物沿用旧常数 16
        int monsterAgility = monster.HasOfficialStats && monster.Agility > 0 ? monster.Agility : 16;
        int rollSpeed = OfficialClientRulesEngine.DelphiRandom.Shared.Next(monsterAgility);
        bool hit = rollSpeed < attacker.State.Hit;
        if (!hit) return (0, false, false, false);

        var weap = attacker.State.Equipment[(int)OfficialEquipSlot.Weapon];
        int minDcM = Math.Max(0, attacker.State.Dc);
        int maxDcM = Math.Max(minDcM, attacker.State.MaxDc);
        int rawDmg = (weap != null && weap.Dura <= 0) ? 1 : attacker.State.CalculateAttackDamageWithLuck(minDcM, maxDcM, Random.Shared);
        if (attacker.State.BoAttackKill)
        {
            attacker.State.BoAttackKill = false;
            rawDmg += attacker.State.HitPlus;
        }

        bool isFireHit = false;
        var (isFire, fireDmg, _) = attacker.State.FireHitTracker.ConsumeOnHit(rawDmg, nowMs);
        if (isFire)
        {
            attacker.State.BoFireHit = false;
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

        if (attacker.State.HasParalysisRing)
        {
            bool isBoss = monster.Name.Contains("教主") || monster.Name.Contains("赤月") || monster.Name.Contains("尸王") || monster.MaxHp >= 2000;
            if (!isBoss)
            {
                int rollPara = OfficialClientRulesEngine.DelphiRandom.Shared.Next(5);
                if (rollPara == 0)
                {
                    monster.NextActionTimeMs = nowMs + 5000;
                }
            }
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

        if (magicId != 18 && magicId != 19)
        {
            if (session.State.BreakSpellInvisibility())
            {
                BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, session.X, session.Y, 18, "破除隐身", includeSelf: true);
                uint mask = session.State.GetCurrentStatusMask(nowMs);
                session.EnqueuePacket(ProtocolConsts.SM_CHANGENAMECOLOR, session.SessionId, (int)mask, 0, 0);
                BroadcastToNinePatch(session, ProtocolConsts.SM_CHANGENAMECOLOR, session.SessionId, (int)mask, 0, 0, session.CharacterName, includeSelf: false);
            }
        }

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
            session.State.BoThrusting = active;
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, active ? "已开启刺杀剑气 (隔位破盾破防)" : "已关闭刺杀剑气");
            return;
        }

        // 3. 魔法盾 (MagicId = 31)
        if (magicId == 31)
        {
            session.State.Mp = Math.Max(0, session.State.Mp - 30);
            session.State.ActivateMagicShield(3, 30);
            session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
            return;
        }

        // 4. 抗拒火环 (MagicId = 8)
        if (magicId == 8)
        {
            if (SafeZoneEngine != null && SafeZoneEngine.IsInSafeZone(session.CurrentMapId, session.X, session.Y))
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "在安全区内不能推开他人。");
                return;
            }

            BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, session.X, session.Y, 8, "抗拒火环", includeSelf: true);

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
                        surrounding.Add((m.Uid, m.Level, m.X, m.Y, isBoss: m.Name.Contains("教主")));
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
                    if (targetPlayer != null && targetPlayer.State != null)
                    {
                        int oldX = targetPlayer.X;
                        int oldY = targetPlayer.Y;
                        targetPlayer.X = pr.FinalX;
                        targetPlayer.Y = pr.FinalY;
                        targetPlayer.State.ParalyzeExpireMs = nowMs + 1000;
                        targetPlayer.State.StatusBits[5] = true;
                        UpdateAoiPositionWithTransition(map, targetPlayer, oldX, oldY, pr.FinalX, pr.FinalY);
                        SendPositionPullback(targetPlayer, pr.FinalX, pr.FinalY, targetPlayer.Direction);
                        BroadcastToNinePatch(targetPlayer, ProtocolConsts.SM_OBJECTRUN, targetPlayer.SessionId, pr.FinalX, pr.FinalY, targetPlayer.Direction, targetPlayer.CharacterName, includeSelf: true);
                    }
                    else if (RegenEngine != null)
                    {
                        var m = RegenEngine.FindMonsterByUid(pr.Uid);
                        if (m != null && !m.IsDead)
                        {
                            m.X = pr.FinalX;
                            m.Y = pr.FinalY;
                            m.NextActionTimeMs = nowMs + 1000;
                            BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTRUN, m.Uid, pr.FinalX, pr.FinalY, 0, m.Name, includeSelf: true);
                        }
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
            MonsterInstance? targetMonster = null;
            if (targetPlayer == null && RegenEngine != null)
            {
                targetMonster = RegenEngine.FindMonsterByUid(targetUid) ?? RegenEngine.FindMonsterAt(session.CurrentMapId, targetX, targetY);
            }

            // 查找道士持有的灰粉与黄粉 (优先装备栏 Slot 2 Lighting，其次背包)
            EquipItem? grayPowder = null;
            bool grayInEquip = false;
            EquipItem? yellowPowder = null;
            bool yellowInEquip = false;

            var eqLight = session.State.Equipment[(int)OfficialEquipSlot.Lighting];
            if (eqLight != null)
            {
                if (eqLight.Name.Contains("灰")) { grayPowder = eqLight; grayInEquip = true; }
                else if (eqLight.Name.Contains("黄")) { yellowPowder = eqLight; yellowInEquip = true; }
                else if (eqLight.Name.Contains("毒") || eqLight.Name.Contains("药粉"))
                {
                    grayPowder = eqLight; grayInEquip = true;
                }
            }
            if (grayPowder == null)
            {
                var bagGray = session.State.BagItems.Find(i => i.Name.Contains("灰"));
                if (bagGray != null) grayPowder = bagGray;
            }
            if (yellowPowder == null)
            {
                var bagYellow = session.State.BagItems.Find(i => i.Name.Contains("黄"));
                if (bagYellow != null) yellowPowder = bagYellow;
            }
            if (grayPowder == null && yellowPowder == null)
            {
                var fallback = session.State.BagItems.Find(i => i.Name.Contains("药粉") || i.Name.Contains("毒药"));
                if (fallback != null)
                {
                    if (fallback.Name.Contains("黄")) yellowPowder = fallback;
                    else grayPowder = fallback;
                }
            }

            if (grayPowder == null && yellowPowder == null)
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你没有带毒药。");
                return;
            }

            void ConsumePowder(EquipItem powder, bool inEquip)
            {
                if (powder.Dura > 1)
                {
                    powder.Dura -= 1;
                }
                else
                {
                    powder.Dura = 0;
                    if (inEquip)
                    {
                        session.State.Equipment[(int)OfficialEquipSlot.Lighting] = null;
                        session.State.WearWeight = Math.Max(0, session.State.WearWeight - powder.Weight);
                        session.State.RecalculateAttributes();
                        session.EnqueuePacket(ProtocolConsts.SM_TAKEOFF_OK, session.SessionId, (int)OfficialEquipSlot.Lighting, 0, 0);
                        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"{powder.Name}已用尽！");
                    }
                    else
                    {
                        session.State.BagItems.Remove(powder);
                        session.State.BagWeight = Math.Max(0, session.State.BagWeight - powder.Weight);
                        session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
                        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"{powder.Name}已用尽！");
                    }
                }
            }

            int taoistSc = Math.Max(15, session.State.Sc);

            if (targetPlayer != null && targetPlayer.State != null)
            {
                if (session.State.AttackMode == 4 && !targetPlayer.State.IsRedName && !targetPlayer.State.IsYellowName)
                {
                    return;
                }

                bool hit = TaoistCombatEngine.CheckPoisonHit(targetPlayer.State.AntiPoison, Random.Shared);
                if (hit)
                {
                    TaoistCombatEngine.PoisonType pType;
                    if (grayPowder != null && yellowPowder != null)
                    {
                        pType = !targetPlayer.State.HasGreenPoison ? TaoistCombatEngine.PoisonType.Green : TaoistCombatEngine.PoisonType.Red;
                    }
                    else if (grayPowder != null)
                    {
                        pType = TaoistCombatEngine.PoisonType.Green;
                    }
                    else
                    {
                        pType = TaoistCombatEngine.PoisonType.Red;
                    }

                    if (pType == TaoistCombatEngine.PoisonType.Green)
                    {
                        ConsumePowder(grayPowder!, grayInEquip);
                    }
                    else
                    {
                        ConsumePowder(yellowPowder!, yellowInEquip);
                    }

                    int duration = TaoistCombatEngine.CalculatePoisonDurationSec(pType, taoistSc);
                    targetPlayer.State.ApplyPoison(pType, duration, taoistSc, nowMs);

                    uint mask = targetPlayer.State.GetCurrentStatusMask(nowMs);
                    targetPlayer.EnqueuePacket(ProtocolConsts.SM_CHANGENAMECOLOR, targetPlayer.SessionId, (int)mask, 0, 0, targetPlayer.CharacterName);
                    BroadcastToNinePatch(targetPlayer, ProtocolConsts.SM_CHANGENAMECOLOR, targetPlayer.SessionId, (int)mask, 0, 0, targetPlayer.CharacterName, includeSelf: true);
                    BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, targetPlayer.X, targetPlayer.Y, 6, "施毒术", includeSelf: true);

                    string pDesc = pType == TaoistCombatEngine.PoisonType.Green ? "中绿毒 (每2.5s持续掉血，清空回血池)" : "中红毒 (1.2倍易伤减防)";
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"施毒成功：目标已{pDesc}");
                    targetPlayer.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"受到道士施毒术感染：{pDesc}");
                }
            }
            else if (targetMonster != null && !targetMonster.IsDead)
            {
                TaoistCombatEngine.PoisonType pType;
                if (grayPowder != null && yellowPowder != null)
                {
                    pType = !targetMonster.HasGreenPoison ? TaoistCombatEngine.PoisonType.Green : TaoistCombatEngine.PoisonType.Red;
                }
                else if (grayPowder != null)
                {
                    pType = TaoistCombatEngine.PoisonType.Green;
                }
                else
                {
                    pType = TaoistCombatEngine.PoisonType.Red;
                }

                if (pType == TaoistCombatEngine.PoisonType.Green)
                {
                    ConsumePowder(grayPowder!, grayInEquip);
                    targetMonster.HasGreenPoison = true;
                    targetMonster.GreenPoisonExpireMs = nowMs + TaoistCombatEngine.CalculatePoisonDurationSec(pType, taoistSc) * 1000;
                    targetMonster.PoisonSc = taoistSc;
                    targetMonster.PoisonerSessionId = session.SessionId;
                }
                else
                {
                    ConsumePowder(yellowPowder!, yellowInEquip);
                    targetMonster.HasRedPoison = true;
                    targetMonster.RedPoisonExpireMs = nowMs + TaoistCombatEngine.CalculatePoisonDurationSec(pType, taoistSc) * 1000;
                }

                if (targetMonster.IsStoneForm) targetMonster.IsStoneForm = false;
                targetMonster.ThreatTable.AddHate(session.SessionId, 50, isTaunt: false);
                BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, targetMonster.X, targetMonster.Y, 6, "施毒术", includeSelf: true);
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "施毒成功！");
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

        // 7. 治愈术 (MagicId = 2 或 7) 与 群体治愈术 (MagicId = 29)
        if (magicId == 2 || magicId == 7 || magicId == 29)
        {
            int healSkillLvl = session.State.GetSkillLevel(magicId);
            int heal = TaoistCombatEngine.MassHeal.CalculateHealAmount(sc: Math.Max(0, session.State.Sc), skillLevel: healSkillLvl);
            if (magicId == 29)
            {
                // 3x3 范围群体治愈
                var nearbyPlayers = new List<ClientSession>();
                map.Aoi.GetEntitiesInNinePatch(targetX, targetY, nearbyPlayers);
                for (int i = 0; i < nearbyPlayers.Count; i++)
                {
                    var p = nearbyPlayers[i];
                    if (p.State != null && Math.Abs(p.X - targetX) <= 1 && Math.Abs(p.Y - targetY) <= 1)
                    {
                        p.State.Hp = Math.Min(p.State.MaxHp, p.State.Hp + heal);
                        p.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, p.State.Hp, p.State.MaxHp, p.State.Mp, p.State.MaxMp);
                        BroadcastToNinePatch(p, ProtocolConsts.SM_SPELLEFFECT_DELPHI, p.SessionId, p.X, p.Y, 29, "群疗", includeSelf: true);
                    }
                }

                if (RegenEngine != null && session.Pets.Count > 0)
                {
                    foreach (var pet in session.Pets)
                    {
                        var mon = RegenEngine.FindMonsterByUid(pet.Uid);
                        if (mon != null && !mon.IsDead)
                        {
                            pet.X = mon.X;
                            pet.Y = mon.Y;
                            if (Math.Abs(mon.X - targetX) <= 1 && Math.Abs(mon.Y - targetY) <= 1)
                            {
                                mon.Hp = Math.Min(mon.MaxHp, mon.Hp + heal);
                                pet.Hp = mon.Hp;
                                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"你为随从【{mon.Name}】恢复了生命值！");
                            }
                        }
                    }
                }
            }
            else
            {
                // 单体治愈术：支持随从宝宝
                var myPet = session.Pets.FirstOrDefault(p => p.Uid == targetUid);
                if (myPet != null && RegenEngine != null)
                {
                    var mon = RegenEngine.FindMonsterByUid(myPet.Uid);
                    if (mon != null && !mon.IsDead)
                    {
                        mon.Hp = Math.Min(mon.MaxHp, mon.Hp + heal);
                        myPet.Hp = mon.Hp;
                        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"你为随从【{mon.Name}】恢复了生命值！");
                        BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, mon.X, mon.Y, magicId, "治愈术", includeSelf: true);
                        return;
                    }
                }

                ClientSession targetSession = session;
                if (targetUid > 0 && targetUid != session.SessionId)
                {
                    var found = ActiveSessions.TryGetValue(targetUid, out var ts) ? ts : null;
                    if (found != null && found.State != null && found.CurrentMapId == session.CurrentMapId && Math.Max(Math.Abs(found.X - session.X), Math.Abs(found.Y - session.Y)) <= 9)
                    {
                        targetSession = found;
                    }
                }
                targetSession.State!.Hp = Math.Min(targetSession.State.MaxHp, targetSession.State.Hp + heal);
                targetSession.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, targetSession.State.Hp, targetSession.State.MaxHp, targetSession.State.Mp, targetSession.State.MaxMp);
                BroadcastToNinePatch(targetSession, ProtocolConsts.SM_SPELLEFFECT_DELPHI, targetSession.SessionId, targetSession.X, targetSession.Y, magicId, "治愈术", includeSelf: true);
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
                        p.State.GhostShieldExpireMs = nowMs + 60_000;
                        p.State.RecalculateAttributes();
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
                        p.State.HolyArmorExpireMs = nowMs + 60_000;
                        p.State.RecalculateAttributes();
                        BroadcastToNinePatch(p, ProtocolConsts.SM_SPELLEFFECT_DELPHI, p.SessionId, p.X, p.Y, 15, "神圣战甲术", includeSelf: true);
                    }
                }
            }
            return;
        }

        // 7.25 困魔咒 (MagicId = 16) 禁锢怪物
        if (magicId == 16)
        {
            if (RegenEngine != null)
            {
                var monsters = RegenEngine.GetMonstersInArea(session.CurrentMapId, targetX - 1, targetY - 1, targetX + 1, targetY + 1);
                foreach (var monster in monsters)
                {
                    if (!monster.IsDead && !monster.Name.Contains("教主") && !monster.Name.Contains("赤月") && monster.MaxHp < 2000)
                    {
                        monster.IsTrappedByHolyCurse = true;
                        monster.TrappedExpireMs = nowMs + 15_000;
                        monster.NextActionTimeMs = nowMs + 15_000;
                        monster.TargetSessionId = 0;
                        monster.ThreatTable.RemoveTarget(session.SessionId);
                    }
                }
            }
            BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, targetX, targetY, 16, "困魔咒", includeSelf: true);
            return;
        }

        // 7.3 隐身术 (MagicId = 18) 与 集体隐身术 (MagicId = 19)
        if (magicId == 18 || magicId == 19)
        {
            if (magicId == 19)
            {
                lock (_ninePatchBuffer)
                {
                    map.Aoi.GetEntitiesInNinePatch(targetX, targetY, _ninePatchBuffer);
                    for (int i = 0; i < _ninePatchBuffer.Count; i++)
                    {
                        var p = _ninePatchBuffer[i];
                        if (p.State != null && Math.Abs(p.X - targetX) <= 1 && Math.Abs(p.Y - targetY) <= 1)
                        {
                            p.State.IsStealthed = true;
                            p.State.StealthExpireMs = nowMs + 30_000;
                            p.State.StatusBits[8] = true;
                            uint m = p.State.GetCurrentStatusMask(nowMs);
                            p.EnqueuePacket(ProtocolConsts.SM_CHANGENAMECOLOR, p.SessionId, (int)m, 0, 0);
                            BroadcastToNinePatch(p, ProtocolConsts.SM_CHANGENAMECOLOR, p.SessionId, (int)m, 0, 0, p.CharacterName, includeSelf: false);
                        }
                    }
                }
            }
            else
            {
                session.State.IsStealthed = true;
                session.State.StealthExpireMs = nowMs + 30_000;
                session.State.StatusBits[8] = true;
                uint mask = session.State.GetCurrentStatusMask(nowMs);
                session.EnqueuePacket(ProtocolConsts.SM_CHANGENAMECOLOR, session.SessionId, (int)mask, 0, 0);
                BroadcastToNinePatch(session, ProtocolConsts.SM_CHANGENAMECOLOR, session.SessionId, (int)mask, 0, 0, session.CharacterName, includeSelf: false);
            }

            if (RegenEngine != null)
            {
                var monsters = RegenEngine.GetMonstersInArea(session.CurrentMapId, session.X - 5, session.Y - 5, session.X + 5, session.Y + 5);
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (monsters[i].TargetSessionId == session.SessionId && !monsters[i].SeeInvisibility)
                    {
                        monsters[i].TargetSessionId = 0; // 脱离怪物仇恨目标
                        monsters[i].ThreatTable.RemoveTarget(session.SessionId);
                    }
                }
            }
            BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, session.X, session.Y, magicId, "隐身术", includeSelf: true);
            return;
        }

        // 7.4 召唤骷髅 (MagicId = 17) 与 召唤神兽 (MagicId = 30)
        if (magicId == 17 || magicId == 30)
        {
            if (RegenEngine != null)
            {
                // 先清除已有的同类型召唤物
                foreach (var oldM in RegenEngine.ActiveMonsters.ToList())
                {
                    if (oldM.MasterSessionId == session.SessionId && (oldM.Name.Contains("骷髅") || oldM.Name.Contains("神兽")))
                    {
                        oldM.Hp = 0;
                        RegenEngine.OnMonsterDied(oldM.Uid);
                        BroadcastToNinePatch(session, ProtocolConsts.SM_DEATH_ACTION_DELPHI, oldM.Uid, oldM.X, oldM.Y, 0, oldM.Name, includeSelf: true);
                        BroadcastToNinePatch(session, ProtocolConsts.SM_MONDELETE, oldM.Uid, 0, 0, 0, oldM.Name, includeSelf: true);
                    }
                }

                var userMagic = session.State.Magics.Find(m => m.MagicId == magicId);
                int skillLv = userMagic?.Level ?? 0;
                var stats = magicId == 30 
                    ? TaoistCombatEngine.GetShinsuStats(skillLv) 
                    : TaoistCombatEngine.GetSkeletonStats(skillLv);

                string petName = stats.Name;
                int baseHp = stats.MaxHp;
                int petX = magicId == 30 ? session.X : session.X + 1;
                int petY = magicId == 30 ? session.Y + 1 : session.Y;
                if (!map.IsWalkable(petX, petY) || RegenEngine.ActiveMonsters.Any(m => !m.IsDead && m.MapId == session.CurrentMapId && m.X == petX && m.Y == petY))
                {
                    petX = session.X - 1; petY = session.Y;
                }

                var pet = RegenEngine.SpawnMonsterDirect(session.CurrentMapId, petX, petY, petName, baseHp);
                if (pet != null)
                {
                    pet.MasterSessionId = session.SessionId;
                    pet.TargetSessionId = 0;
                    pet.Level = stats.Level;
                    pet.AttackDamageMin = stats.MinDc;
                    pet.AttackDamageMax = stats.MaxDc;
                    pet.Defense = stats.Ac;

                    session.Pets.RemoveAll(p => p.Name.Contains("骷髅") || p.Name.Contains("神兽"));
                    session.Pets.Add(new HuWeiPetEngine.SlavePet
                    {
                        Uid = pet.Uid,
                        Name = petName,
                        MasterSessionId = session.SessionId,
                        Level = stats.Level,
                        Hp = pet.Hp,
                        MaxHp = pet.MaxHp,
                        MinDc = stats.MinDc,
                        MaxDc = stats.MaxDc,
                        Ac = stats.Ac,
                        Mac = stats.Mac,
                        X = pet.X,
                        Y = pet.Y,
                        CurrentMapId = session.CurrentMapId
                    });

                    // 盛大官方 1.5 权威召唤兽进场广播: SM_MONSPAWN (220)
                    BroadcastToNinePatch(session, ProtocolConsts.SM_MONSPAWN, pet.Uid, petX, petY, pet.Direction, $"{petName}/{petX}/{petY}/{pet.Direction}/{(magicId == 30 ? 11 : 10)}", includeSelf: true);
                }
                BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, petX, petY, magicId, petName, includeSelf: true);
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
            if (targetPlayer == null && targetUid != 0)
            {
                ActiveSessions.TryGetValue(targetUid, out targetPlayer);
            }
            if (targetPlayer?.State != null)
            {
                targetPlayer.State.IsRevealed = true;
                targetPlayer.State.RevealedBySessionId = session.SessionId;
                targetPlayer.State.RevealedExpireMs = nowMs + 60_000;
                session.EnqueuePacket(ProtocolConsts.SM_HEALTHBAR_DELPHI, targetPlayer.SessionId, 1, 0, 0, "");
                session.EnqueuePacket(ProtocolConsts.SM_HEALTHBAR_COLOR_DELPHI, targetPlayer.SessionId, targetPlayer.State.Hp, targetPlayer.State.MaxHp, 0, "");
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"心灵启示: {targetPlayer.CharacterName} {targetPlayer.State.Hp}/{targetPlayer.State.MaxHp}");
                return;
            }
            if (RegenEngine != null)
            {
                var m = RegenEngine.FindMonsterByUid(targetUid) ?? RegenEngine.FindMonsterAt(session.CurrentMapId, targetX, targetY);
                if (m != null)
                {
                    m.IsRevealed = true;
                    m.RevealedBySessionId = session.SessionId;
                    m.RevealedExpireMs = nowMs + 60_000;
                    session.EnqueuePacket(ProtocolConsts.SM_HEALTHBAR_DELPHI, m.Uid, 1, 0, 0, "");
                    session.EnqueuePacket(ProtocolConsts.SM_HEALTHBAR_COLOR_DELPHI, m.Uid, m.Hp, m.MaxHp, 0, "");
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"心灵启示: {m.Name} {m.Hp}/{m.MaxHp}");
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
                    if (!m.IsDead && (m.Name.Contains("教主") || m.Name.Contains("尸王") || m.MaxHp >= 2000))
                    {
                        continue; // BOSS 免疫
                    }
                    if (!m.IsDead)
                    {
                        m.IsTrappedByHolyCurse = true;
                        m.TrappedExpireMs = nowMs + 15_000;
                        m.NextActionTimeMs = nowMs + 15_000;
                    }
                }
            }
            BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, targetX, targetY, 16, "困魔咒", includeSelf: true);
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "困魔咒生效了。");
            return;
        }

        // 8. 半月弯刀 (MagicId = 25) 扇面 4 目标结算 (IDA sub_4C1EF4)
        if (magicId == 25)
        {
            int dir = session.Direction;
            if (targetX != 0 || targetY != 0)
            {
                int deltaX = Math.Sign(targetX - session.X);
                int deltaY = Math.Sign(targetY - session.Y);
                if (deltaX != 0 || deltaY != 0)
                {
                    dir = MonsterRegenEngine.GetDirection(deltaX, deltaY);
                    session.Direction = (byte)dir;
                }
            }
            var affectedTiles = WarriorCombatEngine.HalfMoon.GetAffectedTiles(session.X, session.Y, dir);
            int baseDmg = session.State.CalculateAttackDamageWithLuck(
                Math.Max(0, session.State.Dc), 
                Math.Max(session.State.Dc, session.State.MaxDc), 
                Random.Shared);

            int halfMoonLvl = session.State.GetSkillLevel(25); // 官方 1.5 半月弯刀 MagicId = 25
            foreach (var (tx, ty, isMain) in affectedTiles)
            {
                int dmg = WarriorCombatEngine.HalfMoon.CalculateDamage(baseDmg, isMain, skillLevel: halfMoonLvl);

                // 结算该格子的玩家
                var targetSessions = new List<ClientSession>();
                map.Aoi.GetEntitiesInNinePatch(tx, ty, targetSessions);
                for (int i = 0; i < targetSessions.Count; i++)
                {
                    var target = targetSessions[i];
                    if (target.SessionId != session.SessionId && target.X == tx && target.Y == ty && target.State != null && target.State.Hp > 0)
                    {
                        var (finalHp, finalMp, died, revived, absorbed, shieldBroken) = target.State.TakeCombatDamageFull(dmg, nowMs);
                        BroadcastToNinePatch(target, ProtocolConsts.SM_OBJECTSTRUCK, target.SessionId, target.X, target.Y, dmg, target.CharacterName, includeSelf: true);
                        target.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, target.State.Hp, target.State.MaxHp, target.State.Mp, target.State.MaxMp);
                        if (died)
                        {
                            AuthoritativeExecutePlayerDeath(target, session, map, nowMs);
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

        // 8.5 野蛮冲撞 (MagicId = 27) 3000ms CD、等级碰撞、撞墙反噬与硬直麻痹
        if (magicId == 27)
        {
            if (session.State == null) return;
            if (session.State.LastWildSlamTickMs > 0 && (nowMs - session.State.LastWildSlamTickMs < 3000))
            {
                return;
            }
            session.State.LastWildSlamTickMs = nowMs;

            if (SafeZoneEngine != null && SafeZoneEngine.IsInSafeZone(session.CurrentMapId, session.X, session.Y))
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "在安全区内不能推开他人。");
                return;
            }

            int dir = session.Direction;
            if (targetX != session.X || targetY != session.Y)
            {
                int deltaX = Math.Sign(targetX - session.X);
                int deltaY = Math.Sign(targetY - session.Y);
                dir = MonsterRegenEngine.GetDirection(deltaX, deltaY);
                session.Direction = (byte)dir;
            }

            var (fx, fy) = GetFrontCoord(session.X, session.Y, dir);
            int dx = fx - session.X;
            int dy = fy - session.Y;

            if (!map.IsWalkable(fx, fy))
            {
                session.State.Hp = Math.Max(1, session.State.Hp - 15);
                session.State.StatusBits[5] = true;
                session.State.ParalyzeExpireMs = nowMs + 1000;
                session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
                BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, session.SessionId, session.X, session.Y, 15, session.CharacterName, includeSelf: true);
                return;
            }

            // PVP 玩家碰撞检测
            ClientSession? frontPlayer = null;
            lock (_ninePatchBuffer)
            {
                map.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
                for (int i = 0; i < _ninePatchBuffer.Count; i++)
                {
                    var other = _ninePatchBuffer[i];
                    if (other.SessionId != session.SessionId && other.CurrentMapId == session.CurrentMapId && other.X == fx && other.Y == fy && other.State != null && other.State.Hp > 0)
                    {
                        frontPlayer = other;
                        break;
                    }
                }
            }

            if (frontPlayer != null && frontPlayer.State != null)
            {
                bool canPush = session.State.Level > frontPlayer.State.Level;
                if (!canPush)
                {
                    session.State.Hp = Math.Max(1, session.State.Hp - 15);
                    session.State.StatusBits[5] = true;
                    session.State.ParalyzeExpireMs = nowMs + 1000;
                    session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
                    BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, session.SessionId, session.X, session.Y, 15, session.CharacterName, includeSelf: true);
                    return;
                }

                int pushDist = (session.State.Level - frontPlayer.State.Level >= 3) ? 2 : 1;
                int targetNewX = frontPlayer.X + dx * pushDist;
                int targetNewY = frontPlayer.Y + dy * pushDist;
                if (!map.IsWalkable(targetNewX, targetNewY))
                {
                    targetNewX = frontPlayer.X + dx;
                    targetNewY = frontPlayer.Y + dy;
                }

                if (map.IsWalkable(targetNewX, targetNewY))
                {
                    int tOldX = frontPlayer.X, tOldY = frontPlayer.Y;
                    frontPlayer.X = targetNewX;
                    frontPlayer.Y = targetNewY;
                    UpdateAoiPositionWithTransition(map, frontPlayer, tOldX, tOldY, targetNewX, targetNewY);
                    SendPositionPullback(frontPlayer, targetNewX, targetNewY, frontPlayer.Direction);
                    BroadcastToNinePatch(frontPlayer, ProtocolConsts.SM_OBJECTRUN, frontPlayer.SessionId, targetNewX, targetNewY, frontPlayer.Direction, frontPlayer.CharacterName, includeSelf: true);
                }
                else
                {
                    frontPlayer.State.Hp = Math.Max(1, frontPlayer.State.Hp - 20);
                    frontPlayer.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, frontPlayer.State.Hp, frontPlayer.State.MaxHp, frontPlayer.State.Mp, frontPlayer.State.MaxMp);
                }

                frontPlayer.State.StatusBits[5] = true;
                frontPlayer.State.ParalyzeExpireMs = nowMs + 1500;

                int oldX = session.X;
                int oldY = session.Y;
                int newX = fx;
                int newY = fy;
                session.X = newX;
                session.Y = newY;
                UpdateAoiPositionWithTransition(map, session, oldX, oldY, newX, newY);
                SendPositionPullback(session, newX, newY, session.Direction);
                BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTRUN, session.SessionId, newX, newY, session.Direction, session.CharacterName, includeSelf: true);
                return;
            }

            MonsterInstance? frontMob = null;
            if (RegenEngine != null)
            {
                var mobs = RegenEngine.GetMonstersInArea(session.CurrentMapId, fx, fy, fx, fy);
                frontMob = mobs.FirstOrDefault(m => !m.IsDead && m.X == fx && m.Y == fy);
            }

            if (frontMob != null)
            {
                bool canPush = frontMob.CanMove && session.State.Level > frontMob.Level;
                if (!canPush)
                {
                    session.State.Hp = Math.Max(1, session.State.Hp - 15);
                    session.State.StatusBits[5] = true;
                    session.State.ParalyzeExpireMs = nowMs + 1000;
                    session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
                    BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, session.SessionId, session.X, session.Y, 15, session.CharacterName, includeSelf: true);
                    return;
                }

                frontMob.Hp = Math.Max(0, frontMob.Hp - 30);
                if (frontMob.Hp <= 0)
                {
                    RegenEngine?.OnMonsterDied(frontMob.Uid);
                    BroadcastToNinePatch(session, ProtocolConsts.SM_DEATH_ACTION_DELPHI, frontMob.Uid, frontMob.X, frontMob.Y, 0, frontMob.Name, includeSelf: true);
                    OnMonsterKilledByPlayer(session, frontMob);
                }
                else
                {
                    frontMob.X += dx * 2;
                    frontMob.Y += dy * 2;
                    frontMob.NextActionTimeMs = nowMs + 1000;
                }

                int oldX = session.X;
                int oldY = session.Y;
                int newX = session.X + dx * 3;
                int newY = session.Y + dy * 3;
                session.X = newX;
                session.Y = newY;
                UpdateAoiPositionWithTransition(map, session, oldX, oldY, newX, newY);
                SendPositionPullback(session, newX, newY, session.Direction);
                BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTRUN, session.SessionId, newX, newY, session.Direction, session.CharacterName, includeSelf: true);
                return;
            }

            // 平地顺畅位移 2 格
            int pOldX = session.X;
            int pOldY = session.Y;
            int pNewX = session.X + dx * 2;
            int pNewY = session.Y + dy * 2;
            session.X = pNewX;
            session.Y = pNewY;
            UpdateAoiPositionWithTransition(map, session, pOldX, pOldY, pNewX, pNewY);
            SendPositionPullback(session, pNewX, pNewY, session.Direction);
            BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTRUN, session.SessionId, pNewX, pNewY, session.Direction, session.CharacterName, includeSelf: true);
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

        // 9.1 爆裂火焰 (MagicId = 33) 3x3 范围魔法伤害
        if (magicId == 33)
        {
            int spellDmg = OfficialClientRulesEngine.CombatMath.CalculateMagicDamage(15, 25, 0, 5, OfficialClientRulesEngine.DelphiRandom.Shared);
            BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, targetX, targetY, 33, "爆裂火焰", includeSelf: true);

            if (RegenEngine != null)
            {
                var monsters = RegenEngine.GetMonstersInArea(session.CurrentMapId, targetX - 1, targetY - 1, targetX + 1, targetY + 1);
                for (int i = 0; i < monsters.Count; i++)
                {
                    var m = monsters[i];
                    if (!m.IsDead)
                    {
                        m.Hp = Math.Max(0, m.Hp - spellDmg);
                        BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, m.Uid, m.X, m.Y, spellDmg, m.Name, includeSelf: true);
                        if (m.IsDead)
                        {
                            RegenEngine.OnMonsterDied(m.Uid);
                            BroadcastToNinePatch(session, ProtocolConsts.SM_DEATH_ACTION_DELPHI, m.Uid, m.X, m.Y, 0, m.Name, includeSelf: true);
                            OnMonsterKilledByPlayer(session, m);
                        }
                    }
                }
            }

            lock (_ninePatchBuffer)
            {
                map.Aoi.GetEntitiesInNinePatch(targetX, targetY, _ninePatchBuffer);
                for (int i = 0; i < _ninePatchBuffer.Count; i++)
                {
                    var target = _ninePatchBuffer[i];
                    if (target.SessionId != session.SessionId && Math.Abs(target.X - targetX) <= 1 && Math.Abs(target.Y - targetY) <= 1 && target.State != null && target.State.Hp > 0)
                    {
                        var (finalHp, finalMp, died, revived, absorbed, shieldBroken) = target.State.TakeCombatDamageFull(spellDmg, nowMs);
                        BroadcastToNinePatch(target, ProtocolConsts.SM_OBJECTSTRUCK, target.SessionId, target.X, target.Y, spellDmg, target.CharacterName, includeSelf: true);
                        target.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, target.State.Hp, target.State.MaxHp, target.State.Mp, target.State.MaxMp);
                        if (died)
                        {
                            AuthoritativeExecutePlayerDeath(target, session, map, nowMs);
                        }
                    }
                }
            }
            return;
        }

        // 10. 瞬息移动 (MagicId = 11)
        if (magicId == 11 && targetUid == 0 && targetX == 0 && targetY == 0)
        {
            var (tx, ty, ok) = MageCombatEngine.Teleport.ResolveRandomLandingTile(map.Width, map.Height, (x, y) => !map.IsWalkable(x, y), Random.Shared);
            int oldX = session.X;
            int oldY = session.Y;
            session.X = tx;
            session.Y = ty;
            UpdateAoiPositionWithTransition(map, session, oldX, oldY, tx, ty);
            SendPositionPullback(session, tx, ty, session.Direction);

            for (int p = 0; p < session.Pets.Count; p++)
            {
                var pet = session.Pets[p];
                pet.X = tx + (p % 2 == 0 ? 1 : -1);
                pet.Y = ty + (p / 2);
            }

            BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, tx, ty, 11, "瞬移降临", includeSelf: true);
            BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTRUN, session.SessionId, tx, ty, session.Direction, session.CharacterName, includeSelf: true);
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
                    int monLevel = monster.Level > 0 ? monster.Level : 20;
                    var userMagic = session.State?.Magics.Find(m => m.MagicId == 20);
                    int skillLvl = userMagic?.Level ?? 3;
                    int petCount = session.Pets?.Count ?? 0;
                    var result = MageCombatEngine.Temptation.Evaluate(session.State?.Level ?? 1, skillLvl, monLevel, petCount, isBossOrUndead, Random.Shared.Next(100));
                    if (result.Outcome == MageCombatEngine.Temptation.TameOutcome.Paralyzed)
                    {
                        monster.NextActionTimeMs = nowMs + result.ParalyzeDurationSec * 1000;
                        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, result.Description);
                    }
                    else if (result.Outcome == MageCombatEngine.Temptation.TameOutcome.Tamed)
                    {
                        monster.MasterSessionId = session.SessionId;
                        monster.TamedTickMs = nowMs;
                        monster.TargetSessionId = 0; // 解除仇恨
                        session.Pets?.Add(new HuWeiPetEngine.SlavePet
                        {
                            Uid = monster.Uid,
                            Name = monster.Name,
                            MasterSessionId = session.SessionId,
                            Hp = monster.Hp,
                            MaxHp = monster.MaxHp
                        });
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
                                AuthoritativeExecutePlayerDeath(target, session, map, nowMs);
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

                        if (m.HasRedPoison)
                        {
                            dmg = TaoistCombatEngine.ApplyRedPoisonVulnerability(dmg, hasRedPoison: true);
                        }
                        if (m.IsTrappedByHolyCurse)
                        {
                            m.IsTrappedByHolyCurse = false;
                            m.TrappedExpireMs = 0;
                            m.NextActionTimeMs = nowMs;
                            m.TargetSessionId = session.SessionId;
                        }
                        if (m.IsStoneForm) m.IsStoneForm = false;
                        m.ThreatTable.AddHate(session.SessionId, dmg, isTaunt: false);

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
                                    AuthoritativeExecutePlayerDeath(target, session, map, nowMs);
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
                    // 盛大官方 1.5 铁律: 无论何种判定结果均广播圣言术神圣光效
                    BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, monster.Uid, monster.X, monster.Y, 24, "圣言术", includeSelf: true);

                    // 1. BOSS 阶级怪物绝对免疫
                    if (monster.Name.Contains("教主") || monster.Name.Contains("尸王") || monster.MaxHp >= 2000)
                    {
                        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "BOSS 阶级怪物拥有神圣庇护，圣言术无法度化！");
                        return;
                    }

                    // 2. 修为等级不足拦截 (高阶怪血量 > 500 时等级约为 45)
                    int monLvl = monster.MaxHp > 500 ? 45 : 15;
                    if (session.State != null && session.State.Level < monLvl)
                    {
                        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你的修为等级不足以度化该强大的不死生物！");
                        return;
                    }

                    // 3. 成功度化秒杀
                    monster.Hp = 0;
                    BroadcastToNinePatch(session, ProtocolConsts.SM_OBJECTSTRUCK, monster.Uid, monster.X, monster.Y, monster.MaxHp, monster.Name, includeSelf: true);
                    RegenEngine.OnMonsterDied(monster.Uid);
                    BroadcastToNinePatch(session, ProtocolConsts.SM_DEATH_ACTION_DELPHI, monster.Uid, monster.X, monster.Y, 0, monster.Name, includeSelf: true);
                    OnMonsterKilledByPlayer(session, monster);
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"圣言术成功度化了{monster.Name}。");
                    return;
                }
            }
            return;
        }

        // 15. 远程单体法术 (火球术=1, 大火球=5, 雷电术=9/10, 灵魂火符=13, 冰咆哮=32) 视线射线穿透检测 (防穿墙)
        if (magicId == 1 || magicId == 5 || magicId == 9 || magicId == 10 || magicId == 13 || magicId == 32)
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
            // 和平模式 (1): 攻击白名玩家 0 伤害直接 return
            if (session.State.AttackMode == 1 && !defender.State.IsRedName && !defender.State.IsYellowName)
            {
                return;
            }
            // 组队模式 (2): 同队队友免伤
            if (session.State.AttackMode == 2 && GroupEngine?.GetParty(session.SessionId)?.Members.Any(m => m.SessionId == defender.SessionId) == true)
            {
                return;
            }
            // 善恶模式 (4): 对白名免伤
            if (session.State.AttackMode == 4 && !defender.State.IsRedName && !defender.State.IsYellowName)
            {
                return;
            }

            // 魔法躲避检定 (白色虎齿项链等)
            if (defender.State.MagicEvade > 0)
            {
                int rollEvade = OfficialClientRulesEngine.DelphiRandom.Shared.Next(100);
                if (rollEvade < defender.State.MagicEvade)
                {
                    // 官方 1.5 铁律：魔法躲避触发仅免伤与播放闪避动作，绝不发送非官方调试文字
                    return;
                }
            }

            session.State.AddPkFlashing(nowMs);
            int skillLevel = session.State.GetSkillLevel(magicId);
            int baseSpellDmg = magicId switch
            {
                10 => 25 + skillLevel * 3, // 雷电术
                1 => 4 + skillLevel * 2,   // 火球术
                5 => 10 + skillLevel * 2,  // 大火球
                13 => 15 + skillLevel * 3, // 灵魂火符
                32 => 20 + skillLevel * 3, // 冰咆哮
                _ => 20
            };
            int attrDmg = magicId == 13
                ? session.State.CalculateAttackDamageWithLuck(session.State.Sc, session.State.MaxSc, Random.Shared)
                : session.State.CalculateAttackDamageWithLuck(session.State.Mc, session.State.MaxMc, Random.Shared);

            int totalDmg = baseSpellDmg + attrDmg;
            int mac = defender.State.Mac + OfficialClientRulesEngine.DelphiRandom.Shared.Next(Math.Max(1, defender.State.MaxMac - defender.State.Mac + 1));
            int magicDmg = Math.Max(1, totalDmg - mac);

            if (defender.State.HasRedPoison)
            {
                magicDmg = TaoistCombatEngine.ApplyRedPoisonVulnerability(magicDmg, hasRedPoison: true);
            }
            var (finalHp, finalMp, died, revived, absorbed, shieldBroken) = defender.State.TakeCombatDamageFull(magicDmg, nowMs);
            BroadcastToNinePatch(defender, ProtocolConsts.SM_OBJECTSTRUCK, defender.SessionId, defender.X, defender.Y, magicDmg, defender.CharacterName, includeSelf: true);
            defender.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, defender.State.Hp, defender.State.MaxHp, defender.State.Mp, defender.State.MaxMp);
            if (died)
            {
                AuthoritativeExecutePlayerDeath(defender, session, map, nowMs);
            }
        }
        else if (RegenEngine != null)
        {
            var monster = RegenEngine.FindMonsterByUid(targetUid) ?? RegenEngine.FindMonsterAt(session.CurrentMapId, targetX, targetY);
            if (monster != null && !monster.IsDead)
            {
                int skillLevel = session.State.GetSkillLevel(magicId);
                int baseSpellDmg = magicId switch
                {
                    10 => 25 + skillLevel * 3, // 雷电术
                    1 => 4 + skillLevel * 2,
                    5 => 10 + skillLevel * 2,
                    13 => 15 + skillLevel * 3,
                    32 => 20 + skillLevel * 3,
                    _ => 20
                };
                int attrDmg = magicId == 13
                    ? session.State.CalculateAttackDamageWithLuck(session.State.Sc, session.State.MaxSc, Random.Shared)
                    : session.State.CalculateAttackDamageWithLuck(session.State.Mc, session.State.MaxMc, Random.Shared);

                int totalDmg = baseSpellDmg + attrDmg;
                // 不死系受雷电术 1.5 倍加成
                if (magicId == 10 && (monster.Name.Contains("尸") || monster.Name.Contains("骷髅") || monster.Name.Contains("骨")))
                {
                    totalDmg = (int)(totalDmg * 1.5f);
                }
                int magicDmg = Math.Max(1, totalDmg - monster.Mac);
                if (monster.HasRedPoison)
                {
                    magicDmg = TaoistCombatEngine.ApplyRedPoisonVulnerability(magicDmg, hasRedPoison: true);
                }
                monster.Hp = Math.Max(0, monster.Hp - magicDmg);
                if (monster.IsStoneForm) monster.IsStoneForm = false;
                monster.ThreatTable.AddHate(session.SessionId, magicDmg, isTaunt: false);
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
                        var (revived, revHp, revMp) = session.State.CheckAndTriggerRevivalRing(nowMs);
                        if (revived)
                        {
                            BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, session.X, session.Y, 2, "神圣复活", includeSelf: true);
                            session.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, session.State.Hp, session.State.MaxHp, session.State.Mp, session.State.MaxMp);
                        }
                        else
                        {
                            var poisonMap = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
                            AuthoritativeExecutePlayerDeath(session, null, poisonMap, nowMs);
                        }
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
        if (session.State == null || session.State.Hp <= 0 || session.State.IsDead) return false;
        if (bagSlot < 0 || bagSlot >= session.State.BagItems.Count) return false;

        var item = session.State.BagItems[bagSlot];
        if (!session.State.CheckCanEquipAuthoritative(equipSlot, item.Weight, out string reason))
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"穿戴失败：{reason}");
            return false;
        }

        var oldItem = session.State.Equipment[(int)equipSlot];
        if (oldItem != null && oldItem.Curse > 0 && oldItem.Name.Contains("神秘"))
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "装备被神秘诅咒力量缠绕，无法替换！");
            return false;
        }

        if (oldItem != null)
        {
            // 校验旧装备放入背包是否会导致背包负重超限
            if (session.State.BagWeight - item.Weight + oldItem.Weight > session.State.MaxBagWeight)
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "包裹负重不足，替换下的装备无法放入包裹！");
                return false;
            }
        }

        session.State.BagItems.RemoveAt(bagSlot);
        session.State.Equipment[(int)equipSlot] = item;
        if (oldItem != null)
        {
            session.State.BagItems.Add(oldItem);
        }
        session.State.RecalculateAuthoritativeWeights(session.State.Gold);
        session.State.RecalculateAttributes();

        if (SuitWorldEventEngine.HasCompleteGodPrayerSuit(session.State))
        {
            var (triggered, msg, brokenName, brokenSlot, brokenUid) =
                SuitWorldEventEngine.TriggerGodPrayerRebellion(session.State, RegenEngine?.ActiveMonsters ?? Array.Empty<MonsterInstance>(), Random.Shared);
            if (triggered)
            {
                foreach (var s in GetAllGlobalSessions())
                {
                    s.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, msg);
                }
                if (brokenSlot >= 0)
                {
                    session.EnqueuePacket(ProtocolConsts.SM_DELITEM, brokenUid, 0, 0, 0, brokenName);
                    session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
                    session.EnqueuePacket(ProtocolConsts.SM_TAKEOFF_OK, session.SessionId, brokenSlot, 0, 0);
                }
            }
        }

        return true;
    }

    private void HandleWearItemPacket(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;
        // 格式: bagSlot/equipSlot
        int slash = payload.IndexOf('/');
        if (slash < 0) return;
        if (TryParseInt(payload[..slash], out int bagSlot) &&
            byte.TryParse(payload[(slash + 1)..].TrimEnd('/'), out byte equipSlot))
        {
            if (equipSlot < 9)
            {
                bool success = HandleWearItem(session, bagSlot, (OfficialEquipSlot)equipSlot);
                if (success)
                {
                    var wornEquip = session.State.Equipment[equipSlot];
                    session.EnqueuePacket(ProtocolConsts.SM_TAKEON_OK, session.SessionId, equipSlot, 0, 0, wornEquip?.Name ?? string.Empty);
                    session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
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
        if (session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;
        int slash = payload.IndexOf('/');
        if (slash >= 0) payload = payload[..slash];
        if (!byte.TryParse(payload, out byte slot) || slot >= 9) return;

        var equip = session.State.Equipment[slot];
        if (equip == null)
        {
            session.EnqueuePacket(ProtocolConsts.SM_TAKEOFF_FAIL, session.SessionId, slot, 0, 0);
            return;
        }

        if (equip.Curse > 0 && equip.Name.Contains("神秘"))
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "装备被神秘诅咒力量缠绕，无法脱下！");
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

        bool wasInvis = session.State.HasInvisibleRing;
        session.State.Equipment[slot] = null;
        session.State.BagItems.Add(equip);
        session.State.RecalculateAuthoritativeWeights(session.State.Gold);
        session.State.RecalculateAttributes();

        if (wasInvis && !session.State.HasInvisibleRing)
        {
            uint mask = session.State.GetCurrentStatusMask(Environment.TickCount64);
            session.EnqueuePacket(ProtocolConsts.SM_CHANGENAMECOLOR, session.SessionId, (int)mask, 0, 0);
            BroadcastToNinePatch(session, ProtocolConsts.SM_CHANGENAMECOLOR, session.SessionId, (int)mask, 0, 0, session.CharacterName, includeSelf: false);
            BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, session.X, session.Y, 18, "显形", includeSelf: true);
        }

        session.EnqueuePacket(ProtocolConsts.SM_TAKEOFF_OK, session.SessionId, slot, 0, 0);
        int bIdx = session.State.BagItems.Count - 1;
        int lks = equip.Looks > 0 ? equip.Looks : Mir2.Shared.Data.OfficialStdItemsRegistry.ResolveLooks(equip.Name, 1);
        session.EnqueuePacket(ProtocolConsts.SM_ADDITEM, equip.Uid, bIdx, equip.Weight, 0, $"{equip.Name}\t{equip.Dura}\t{equip.MaxDura}\t{lks}");
        session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
        SendAbility(session);
    }

    private void HandleDropItemPacket(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;
        // 格式: bagSlot 或 bagSlot/count
        int slash = payload.IndexOf('/');
        ReadOnlySpan<char> slotSpan = slash >= 0 ? payload[..slash] : payload;
        if (!TryParseInt(slotSpan, out int bagSlot) || bagSlot < 0 || bagSlot >= session.State.BagItems.Count)
        {
            return;
        }

        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
        if (map.GetGroundItemsAt(session.X, session.Y).Count >= 8)
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "地面上物品堆叠已满，无法丢弃！");
            return;
        }

        var item = session.State.BagItems[bagSlot];
        session.State.BagItems.RemoveAt(bagSlot);
        session.State.RecalculateAuthoritativeWeights(session.State.Gold);
        session.State.RecalculateAttributes();

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
        session.EnqueuePacket(ProtocolConsts.SM_DELITEM, item.Uid, 0, 0, 0, item.Name);
        session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
        SendAbility(session);
    }

    private void HandleDropGoldPacket(ClientSession session, ReadOnlySpan<char> payload)
    {
        if (session.State == null || session.State.Hp <= 0 || session.State.IsDead) return;
        int slash = payload.IndexOf('/');
        if (slash >= 0) payload = payload[..slash];
        if (!TryParseInt(payload, out int amount) || amount <= 0 || session.State.Gold < amount)
        {
            return;
        }

        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
        if (map.GetGroundItemsAt(session.X, session.Y).Count >= 8)
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "地面上物品堆叠已满，无法丢弃！");
            return;
        }

        session.State.Gold -= amount;
        session.State.RecalculateAuthoritativeWeights(session.State.Gold);

        var goldEntity = new GroundItemEntity
        {
            Uid = System.Threading.Interlocked.Increment(ref _nextGroundItemUid),
            Name = "金币",
            X = session.X,
            Y = session.Y,
            OwnerSessionId = 0,
            DropTickMs = Environment.TickCount64,
            Weight = Math.Max(1, amount / 1000),
            IsGold = true,
            Count = amount
        };
        map.AddGroundItem(goldEntity);

        BroadcastToNinePatch(session, ProtocolConsts.SM_ITEMSHOW, goldEntity.Uid, session.X, session.Y, 111, $"金币({amount})", includeSelf: true);
        session.EnqueuePacket(ProtocolConsts.SM_GOLDCHANGED_DELPHI, session.State.Gold, 0, 0, 0, "");
        session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
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
        session.EnqueuePacket(ProtocolConsts.SM_SENDUSERSTATE, session.State.Gold, 0, 0, 0,
            $"{session.CharacterName}/{session.State.Level}/{session.State.Job}/{session.State.Sex}|{session.State.Ac}-{session.State.MaxAc}/{session.State.Mac}-{session.State.MaxMac}/{session.State.Dc}-{session.State.MaxDc}/{session.State.Mc}-{session.State.MaxMc}/{session.State.Sc}-{session.State.MaxSc}/{session.State.Hit}/{session.State.Speed}");
    }

    /// <summary>击杀经验倍率 (官方 1.0)。由宿主按 ServerConfig.ExpRate 设置，仅用于压测/机器人。</summary>
    public double ExpRate { get; set; } = 1.0;

    public void OnMonsterKilledByPlayer(ClientSession killer, MonsterInstance monster)
    {
        if (killer.State == null) return;
        var map = _mapManager.GetMap(killer.CurrentMapId) ?? _mapManager.DefaultMap;
        long nowMs = Environment.TickCount64;

        // 盛大官方怪物掉落与周边空地散射 (80秒归属权保护)
        var dropRules = MonsterDropEngine.GetDropRulesFor(monster.OfficialKey, monster.Name);
        var drops = MonsterDropEngine.RollMonsterDrops(monster.X, monster.Y, killer.SessionId, nowMs, dropRules, OfficialClientRulesEngine.DelphiRandom.Shared, (tx, ty) => map.IsWalkable(tx, ty));
        // 注意: 这里不再注入任何“<怪物名>_战利品”之类的凭空物品 —— 官方没有该物品，掉落只来自 MonItems 官方掉落表 (数据库 official_mon_drop)。
        foreach (var loot in drops)
        {
            map.AddGroundItem(loot);
            BroadcastToNinePatch(killer, ProtocolConsts.SM_GROUNDITEM, loot.Uid, loot.X, loot.Y, 0, loot.Name, includeSelf: true);
        }

        int exp = monster.Exp > 0 ? monster.Exp : Math.Max(10, monster.MaxHp * 2);
        if (ExpRate != 1.0) exp = (int)Math.Min(int.MaxValue / 2, Math.Max(1, Math.Round(exp * ExpRate)));

        var party = GroupEngine.GetParty(killer.SessionId);
        if (party != null && party.Members.Count > 1)
        {
            var shares = GroupEngine.CalculateGroupExpDistribution(party, killer, exp);
            foreach (var (member, memberExp) in shares)
            {
                DistributeKillExpToPlayerAndPets(member, memberExp);
            }
        }
        else
        {
            DistributeKillExpToPlayerAndPets(killer, exp);
        }
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

        // 查找尸体
        var corpse = RegenEngine?.FindCorpse(targetUid) ?? RegenEngine?.FindCorpseAt(session.CurrentMapId, tx, ty);
        if (corpse == null || !corpse.IsDead)
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "目标不存在或尚未死亡！");
            return;
        }

        // 权威物理距离校验: 必须在相邻 2 格内 (官方 IDA 割肉范围，以尸体实际坐标为准)
        if (Math.Abs(session.X - corpse.X) > 2 || Math.Abs(session.Y - corpse.Y) > 2)
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "距离太远，无法割肉！");
            return;
        }

        // 视线阻挡校验: 玩家与尸体之间必须可通视
        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
        if (!map.CheckLineOfSight(session.X, session.Y, corpse.X, corpse.Y))
        {
            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "视线受阻，无法割肉！");
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
            int bagIdx = session.State.BagItems.Count - 1;
            int looks = Mir2.Shared.Data.OfficialStdItemsRegistry.ResolveLooks(meatItem.Name, 1);
            session.EnqueuePacket(ProtocolConsts.SM_ADDITEM, meatItem.Uid, bagIdx, meatItem.Weight, 0, $"{meatItem.Name}\t{meatItem.Dura}\t{meatItem.MaxDura}\t{looks}");
            session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
        }
        else
        {
            // 掉落在地表
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

        // 1. 施毒术 (MagicId = 6): 检查是否有药粉（装备栏或背包），具体精准扣除由 ResolveAuthoritativeSpell 执行
        if (magicId == 6)
        {
            var eqLight = session.State.Equipment[(int)OfficialEquipSlot.Lighting];
            bool hasEqPowder = eqLight != null && (eqLight.Name.Contains("药粉") || eqLight.Name.Contains("毒药"));
            bool hasBagPowder = session.State.BagItems.Exists(i => i.Name.Contains("药粉") || i.Name.Contains("毒药"));
            if (!hasEqPowder && !hasBagPowder)
            {
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你没有带毒药。");
                return false;
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
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "护身符的使用次数不足。");
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
                    session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "护身符的使用次数不足。");
                }
                return true;
            }

            session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你没有带护身符。");
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

    /// <summary>向会话下发当前位置视野内的 NPC(230)、存活怪物(220)与其他玩家(210)；进图与换图（传送门/回城/复活）后都必须调用。</summary>
    private void SendNearbyEntities(ClientSession session, GameMap map)
    {
        if (NpcEngine != null)
        {
            foreach (var npc in NpcEngine.AllNpcs)
            {
                if (npc.MapId == session.CurrentMapId && Math.Abs(npc.X - session.X) <= 30 && Math.Abs(npc.Y - session.Y) <= 30)
                {
                    session.EnqueuePacket(230, npc.Uid, npc.X, npc.Y, 0, $"{npc.Name}\t{npc.X}\t{npc.Y}\t0\t{npc.Appr}");
                }
            }
        }

        // 9. 同步视野内的存活怪物 (SM_MONSPAWN 220)
        if (RegenEngine != null)
        {
            foreach (var mon in RegenEngine.AllActiveMonsters)
            {
                if (mon.MapId == session.CurrentMapId && mon.Hp > 0 && Math.Abs(mon.X - session.X) <= 30 && Math.Abs(mon.Y - session.Y) <= 30)
                {
                    session.EnqueuePacket(220, mon.Uid, mon.X, mon.Y, mon.Direction, $"{mon.Name}\t{mon.X}\t{mon.Y}\t{mon.Direction}\t{mon.Appr}");
                }
            }
        }

        // 10. 同步视野内的其他在线玩家 (SM_USERSPAWN 210)
        lock (_ninePatchBuffer)
        {
            map.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
            for (int i = 0; i < _ninePatchBuffer.Count; i++)
            {
                var other = _ninePatchBuffer[i];
                if (other.SessionId != session.SessionId && other.State != null)
                {
                    int dressAppr = other.State.Equipment[0]?.Shape ?? other.State.Sex;
                    session.EnqueuePacket(210, other.SessionId, other.X, other.Y, other.Direction, $"{other.CharacterName}\t{other.X}\t{other.Y}\t{other.Direction}\t{dressAppr}");
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

        // 随从跨图同步
        if (session.Pets.Count > 0)
        {
            foreach (var pet in session.Pets)
            {
                BroadcastToCoords(oldMap, pet.X, pet.Y, ProtocolConsts.SM_ENTITYDISAPPEAR, pet.Uid, pet.X, pet.Y, 0, pet.Name);
                pet.CurrentMapId = newMap.MapId;
                pet.X = passage.ToX + 1;
                pet.Y = passage.ToY;
                BroadcastToCoords(newMap, pet.X, pet.Y, ProtocolConsts.SM_ENTITYAPPEAR, pet.Uid, pet.X, pet.Y, 0, pet.Name);
            }
        }

        // 2. 变更会话地图与物理坐标
        session.CurrentMapId = newMap.MapId;
        session.X = passage.ToX;
        session.Y = passage.ToY;

        // 3. 下发客户端地图切换与黑屏转场协议 (SM_NEWMAP_DELPHI 521)
        session.EnqueuePacket(ProtocolConsts.SM_NEWMAP_DELPHI, newMap.MapId, passage.ToX, passage.ToY, 0, NewMapExtra(newMap));
        session.EnqueuePacket(ProtocolConsts.SM_SENDUSERSTATE, session.SessionId, passage.ToX, passage.ToY, session.Direction, session.CharacterName);

        // 4. 进入新地图 AOI 空间分区并广播登场
        newMap.Aoi.Enter(session, passage.ToX, passage.ToY);
        BroadcastToNinePatch(session, ProtocolConsts.SM_ENTITYAPPEAR, session.SessionId, passage.ToX, passage.ToY, session.Direction, session.CharacterName, includeSelf: false);

        // 5. 新地图视野内的 NPC/怪物/玩家（此前缺失，导致换图后视野为空）；先同步激活该图刷怪
        RegenEngine?.PopulateMapNow(newMap.MapId);
        SendNearbyEntities(session, newMap);
        session.Flush();
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

    public void UpdateWorldTick(long tick, long nowMs)
    {
        ProcessMultiMapParallelTick(tick, nowMs);

        if (RegenEngine != null)
        {
            var active = RegenEngine.ActiveMonsters.ToList();
            for (int i = active.Count - 1; i >= 0; i--)
            {
                var monster = active[i];
                if (monster.IsDead) continue;

                // 1. 绿毒扣血与致死判定
                if (monster.HasGreenPoison)
                {
                    if (nowMs >= monster.GreenPoisonExpireMs)
                    {
                        monster.HasGreenPoison = false;
                    }
                    else
                    {
                        int dmg = TaoistCombatEngine.CalculateGreenPoisonTickDamage(monster.PoisonSc);
                        monster.Hp = Math.Max(0, monster.Hp - Math.Max(1, dmg));
                        if (monster.Hp <= 0)
                        {
                            RegenEngine.OnMonsterDied(monster.Uid);
                            int killerSessionId = monster.PoisonerSessionId; // 掉落统一由 OnMonsterKilledByPlayer 按官方掉落表结算
                            if (killerSessionId != 0 && ActiveSessions.TryGetValue(killerSessionId, out var poisoner))
                            {
                                OnMonsterKilledByPlayer(poisoner, monster);
                            }
                        }
                    }
                }

                // 2. 心灵启示超时自然解除显血 (SM_HEALTHBAR_DELPHI param=0)
                if (monster.IsRevealed && nowMs >= monster.RevealedExpireMs)
                {
                    monster.IsRevealed = false;
                    if (monster.RevealedBySessionId != 0 && ActiveSessions.TryGetValue(monster.RevealedBySessionId, out var revealer))
                    {
                        revealer.EnqueuePacket(ProtocolConsts.SM_HEALTHBAR_DELPHI, monster.Uid, 0, 0, 0, "");
                    }
                }

                // 3. 随从服役超过 6 小时野性复苏叛变
                if (monster.MasterSessionId > 0 && monster.TamedTickMs > 0 && (nowMs - monster.TamedTickMs >= 6 * 3600 * 1000))
                {
                    int masterId = monster.MasterSessionId;
                    monster.MasterSessionId = 0;
                    monster.TargetSessionId = masterId;
                    var masterSession = ActiveSessions.GetValueOrDefault(masterId) ?? GetAllGlobalSessions().FirstOrDefault(s => s.SessionId == masterId);
                    if (masterSession != null)
                    {
                        masterSession.Pets.RemoveAll(p => p.Uid == monster.Uid);
                        masterSession.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"你的随从 {monster.Name} 叛变了。");
                    }
                }
            }
        }

        // 3. 玩家药水缓回驱动与状态超时物理复位 (麻痹/绿毒/红毒/隐身/烈火/PK衰减)
        foreach (var s in GetAllGlobalSessions())
        {
            if (s.State != null && s.State.Hp > 0)
            {
                var (hpGain, mpGain) = PotionEngine.TickIncHealthSpell(s.State);
                if (hpGain > 0 || mpGain > 0)
                {
                    s.EnqueuePacket(ProtocolConsts.SM_HEALTHSPELLCHANGED, s.State.Hp, s.State.Mp, s.State.MaxHp, s.State.MaxMp);
                }

                // 盛大官方 1.5 权威心跳自然生命与魔法回复 (IDA sub_4C7720 @ 0x4C7851 ~ 0x4C78EA)
                // 周期为 3 秒 (3000ms)，未中绿毒且未满血按 MaxHp/75+1 回血，未满蓝按 MaxMp/18+1 回蓝
                if (s.State.LastNaturalRegenMs == 0)
                {
                    s.State.LastNaturalRegenMs = nowMs;
                }
                else if (nowMs - s.State.LastNaturalRegenMs >= 3000)
                {
                    s.State.LastNaturalRegenMs = nowMs;
                    bool naturalChanged = false;
                    if (!s.State.HasGreenPoison && s.State.Hp < s.State.MaxHp)
                    {
                        int stepHp = Math.Max(1, (s.State.MaxHp / 75) + 1);
                        s.State.Hp = Math.Min(s.State.MaxHp, s.State.Hp + stepHp);
                        naturalChanged = true;
                    }
                    if (s.State.Mp < s.State.MaxMp)
                    {
                        int stepMp = Math.Max(1, (s.State.MaxMp / 18) + 1);
                        s.State.Mp = Math.Min(s.State.MaxMp, s.State.Mp + stepMp);
                        naturalChanged = true;
                    }
                    if (naturalChanged)
                    {
                        s.EnqueuePacket(ProtocolConsts.SM_HEALTHSPELLCHANGED, s.State.Hp, s.State.Mp, s.State.MaxHp, s.State.MaxMp);
                    }
                }

                if (s.State.FireHitTracker.IsReady && nowMs - s.State.FireHitTracker.ReadyStartTime > WarriorCombatEngine.FireHitReadyDurationMs)
                {
                    s.State.FireHitTracker.ConsumeOnHit(0, nowMs);
                    s.State.BoFireHit = false;
                    s.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "烈火蓄力已超时自然熄灭");
                }

                if (s.State.PkPoints > 0)
                {
                    if (s.State.LastPkDecayMs == 0)
                    {
                        s.State.LastPkDecayMs = nowMs;
                    }
                    else if (nowMs - s.State.LastPkDecayMs >= 60_000)
                    {
                        s.State.PkPoints--;
                        s.State.LastPkDecayMs = nowMs;
                        s.EnqueuePacket(ProtocolConsts.SM_CHANGENAMECOLOR, s.SessionId, s.State.PkLevel, 0, 0, s.CharacterName);
                        BroadcastToNinePatch(s, ProtocolConsts.SM_CHANGENAMECOLOR, s.SessionId, s.State.PkLevel, 0, 0, s.CharacterName, includeSelf: false);
                    }
                }

                bool statusChanged = false;
                if (s.State.StatusBits[5] && nowMs >= s.State.ParalyzeExpireMs)
                {
                    s.State.StatusBits[5] = false;
                    s.State.ParalyzeExpireMs = 0;
                    statusChanged = true;
                }
                if (s.State.HasGreenPoison && nowMs >= s.State.GreenPoisonExpireMs)
                {
                    s.State.HasGreenPoison = false;
                    s.State.StatusBits[0] = false;
                    statusChanged = true;
                }
                if (s.State.HasRedPoison && nowMs >= s.State.RedPoisonExpireMs)
                {
                    s.State.HasRedPoison = false;
                    s.State.StatusBits[1] = false;
                    statusChanged = true;
                }
                if (s.State.IsStealthed && nowMs >= s.State.StealthExpireMs)
                {
                    if (!s.State.HasInvisibleRing)
                    {
                        s.State.IsStealthed = false;
                        s.State.StealthExpireMs = 0;
                        s.State.StatusBits[8] = false;
                        statusChanged = true;
                    }
                }

                if (s.State.HolyArmorExpireMs > 0 && nowMs >= s.State.HolyArmorExpireMs)
                {
                    s.State.HolyArmorExpireMs = 0;
                    s.State.RecalculateAttributes(nowMs);
                    statusChanged = true;
                }
                if (s.State.GhostShieldExpireMs > 0 && nowMs >= s.State.GhostShieldExpireMs)
                {
                    s.State.GhostShieldExpireMs = 0;
                    s.State.RecalculateAttributes(nowMs);
                    statusChanged = true;
                }

                if (statusChanged)
                {
                    uint mask = s.State.GetCurrentStatusMask(nowMs);
                    s.EnqueuePacket(ProtocolConsts.SM_CHANGENAMECOLOR, s.SessionId, (int)mask, 0, 0);
                    BroadcastToNinePatch(s, ProtocolConsts.SM_CHANGENAMECOLOR, s.SessionId, (int)mask, 0, 0, s.CharacterName, includeSelf: false);
                }
            }
        }

        // 4. 大刀卫士/弓箭手守卫巡逻扫描并秒杀处决红名
        if (SafeZoneEngine != null)
        {
            var redExecutions = SafeZoneEngine.ScanAndExecuteRedNames(ActiveSessions.Values, nowMs);
            for (int i = 0; i < redExecutions.Count; i++)
            {
                var (guard, target, dmg) = redExecutions[i];
                if (target.State != null && target.State.Hp <= 0)
                {
                    var hitMap = _mapManager.GetMap(target.CurrentMapId) ?? _mapManager.DefaultMap;
                    AuthoritativeExecutePlayerDeath(target, null, hitMap, nowMs);
                }
            }
        }
    }

    public void OnSessionDisconnected(ClientSession session)
    {
        if (session.State != null && !string.IsNullOrEmpty(session.CharacterName))
        {
            if (PgPersistenceEngine != null)
            {
                PgPersistenceEngine.EnqueueSavePlayer(session.AccountName, session.CharacterName, session.State, session.CurrentMapId, session.X, session.Y);
            }
            PersistenceEngine.SavePlayer(session.CharacterName, session.State, session.CurrentMapId, session.X, session.Y);
        }

        TradeEngine.CancelTrade(session);
        GroupEngine.RemoveMember(session, session);

        var map = _mapManager.GetMap(session.CurrentMapId);
        if (map != null)
        {
            if (session.Pets.Count > 0)
            {
                foreach (var pet in session.Pets)
                {
                    lock (_ninePatchBuffer)
                    {
                        map.Aoi.GetEntitiesInNinePatch(pet.X, pet.Y, _ninePatchBuffer);
                        for (int i = 0; i < _ninePatchBuffer.Count; i++)
                        {
                            var other = _ninePatchBuffer[i];
                            other.EnqueuePacket(ProtocolConsts.SM_ENTITYDISAPPEAR, pet.Uid, pet.X, pet.Y, 0, pet.Name);
                        }
                    }
                }
            }

            lock (_ninePatchBuffer)
            {
                map.Aoi.GetEntitiesInNinePatch(session.X, session.Y, _ninePatchBuffer);
                for (int i = 0; i < _ninePatchBuffer.Count; i++)
                {
                    var other = _ninePatchBuffer[i];
                    if (other != session)
                    {
                        other.EnqueuePacket(ProtocolConsts.SM_ENTITYDISAPPEAR, session.SessionId, session.X, session.Y, 0, session.CharacterName);
                    }
                }
            }
            map.Aoi.Leave(session, session.X, session.Y);
        }
        if (RegenEngine != null && session.Pets.Count > 0)
        {
            foreach (var pet in session.Pets)
            {
                RegenEngine.RemoveMonster(pet.Uid);
            }
        }
        session.Pets.Clear();
        ActiveSessions.TryRemove(session.SessionId, out _);
    }

    public int SaveAllActiveSessions(IEnumerable<ClientSession>? sessions = null)
    {
        if (sessions == null) return 0;
        int count = 0;
        foreach (var s in sessions)
        {
            if (s.IsConnected && s.State != null && !string.IsNullOrEmpty(s.CharacterName))
            {
                if (PgPersistenceEngine != null)
                {
                    PgPersistenceEngine.EnqueueSavePlayer(s.AccountName, s.CharacterName, s.State, s.CurrentMapId, s.X, s.Y);
                }
                PersistenceEngine.SavePlayer(s.CharacterName, s.State, s.CurrentMapId, s.X, s.Y);
                count++;
            }
        }
        return count;
    }

    public void ProcessMultiMapParallelTick(long tick, long nowMs)
    {
        foreach (var map in _mapManager.AllMaps)
        {
            var expired = map.CleanExpiredGroundItemsWithEntities(nowMs);
            if (expired.Count > 0)
            {
                foreach (var item in expired)
                {
                    lock (_ninePatchBuffer)
                    {
                        map.Aoi.GetEntitiesInNinePatch(item.X, item.Y, _ninePatchBuffer);
                        for (int i = 0; i < _ninePatchBuffer.Count; i++)
                        {
                            _ninePatchBuffer[i].EnqueuePacket(ProtocolConsts.SM_DELGROUNDITEM, item.Uid, item.X, item.Y, 0, item.Name);
                        }
                    }
                }
            }
            map.CleanExpiredFireWalls(nowMs);

            var activeFires = map.GetAllActiveFireWalls();
            if (activeFires.Count > 0 && RegenEngine != null)
            {
                foreach (var (fx, fy, fires) in activeFires)
                {
                    if (fires.Count == 0) continue;
                    int totalDmg = 0;
                    int casterId = fires[0].CasterSessionId;
                    foreach (var f in fires) totalDmg += f.Damage;
                    var monster = RegenEngine.FindMonsterAt(map.MapId, fx, fy);
                    if (monster != null && !monster.IsDead)
                    {
                        monster.Hp = Math.Max(0, monster.Hp - totalDmg);
                        lock (_ninePatchBuffer)
                        {
                            map.Aoi.GetEntitiesInNinePatch(fx, fy, _ninePatchBuffer);
                            for (int i = 0; i < _ninePatchBuffer.Count; i++)
                            {
                                _ninePatchBuffer[i].EnqueuePacket(ProtocolConsts.SM_OBJECTSTRUCK, monster.Uid, fx, fy, totalDmg, monster.Name);
                            }
                        }
                        if (monster.Hp <= 0)
                        {
                            RegenEngine.OnMonsterDied(monster.Uid);
                            lock (_ninePatchBuffer)
                            {
                                map.Aoi.GetEntitiesInNinePatch(fx, fy, _ninePatchBuffer);
                                for (int i = 0; i < _ninePatchBuffer.Count; i++)
                                {
                                    _ninePatchBuffer[i].EnqueuePacket(ProtocolConsts.SM_DEATH_ACTION_DELPHI, monster.Uid, fx, fy, 0, monster.Name);
                                }
                            }
                            if (ActiveSessions.TryGetValue(casterId, out var caster))
                            {
                                OnMonsterKilledByPlayer(caster, monster);
                            }
                        }
                    }
                }
            }
        }

        if (SabukEngine != null && SabukEngine.Phase == SabukSiegeEngine.WarPhase.InWar)
        {
            // 1. 战役时间到期结算判定
            if (nowMs >= SabukEngine.WarEndTimeMs)
            {
                var (winnerGuild, changed) = SabukEngine.EndSiege();
                string endMsg = changed
                    ? $"[沙巴克攻城战] 攻城战结束，{winnerGuild} 占领了沙巴克。"
                    : $"[沙巴克攻城战] 攻城战结束，{winnerGuild} 守住了沙巴克。";
                foreach (var s in GetAllGlobalSessions())
                {
                    s.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, endMsg);
                }
            }
            else
            {
                // 2. 皇宫占领判定
                var palaceMap = _mapManager.GetMap(SabukSiegeEngine.SabukPalaceMapId);
                var palacePlayers = new List<ClientSession>();
                if (palaceMap != null)
                {
                    palaceMap.Aoi.GetAllEntities(palacePlayers);
                }
                var set = new HashSet<int>(palacePlayers.Select(p => p.SessionId));
                foreach (var s in GetAllGlobalSessions())
                {
                    if (s.CurrentMapId == SabukSiegeEngine.SabukPalaceMapId && set.Add(s.SessionId))
                    {
                        palacePlayers.Add(s);
                    }
                }
                var playerInfos = new List<(string characterName, string guildName, bool isAlive)>();
                foreach (var p in palacePlayers)
                {
                    string gName = GuildEngine.GetPlayerGuild(p.CharacterName) ?? "";
                    bool isAlive = p.State != null && p.State.Hp > 0;
                    playerInfos.Add((p.CharacterName, gName, isAlive));
                }
                var (occupied, newOccupier) = SabukEngine.CheckPalaceOccupation(playerInfos);
                if (occupied)
                {
                    string palaceMsg = $"[沙巴克攻城战] 皇宫已被 {newOccupier} 占领。";
                    foreach (var s in GetAllGlobalSessions())
                    {
                        s.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, palaceMsg);
                    }
                }
            }
        }
    }

    public void RegisterSession(ClientSession session)
    {
        ActiveSessions[session.SessionId] = session;
    }

    public IEnumerable<ClientSession> GetAllGlobalSessions()
    {
        var set = new HashSet<int>();
        foreach (var s in ActiveSessions.Values)
        {
            if (set.Add(s.SessionId)) yield return s;
        }
        var list = new List<ClientSession>();
        foreach (var map in _mapManager.AllMaps)
        {
            map.Aoi.GetAllEntities(list);
            foreach (var s in list)
            {
                if (set.Add(s.SessionId)) yield return s;
            }
        }
    }

    public void HandleWantMinimap(ClientSession session)
    {
        var map = _mapManager.GetMap(session.CurrentMapId) ?? _mapManager.DefaultMap;
        session.EnqueuePacket(ProtocolConsts.SM_READMINIMAP_OK, map.MapId, map.Width, map.Height, 0, map.Name);
    }

    public void ProcessPlayerDeathAndMurderSettlement(ClientSession killer, ClientSession victim, GameMap map, long nowMs)
    {
        if (killer.State == null || victim.State == null) return;

        // 官方 PK 规则：击杀白名玩家增加 100 点 PK 值，若超过 200 点则变红名 (PkStatus.Red = 2)
        if (victim.State.PkPoints == 0)
        {
            killer.State.PkPoints += 100;
            var status = PkRuleEngine.GetPkStatus(killer.State.PkPoints);
            BroadcastToNinePatch(killer, ProtocolConsts.SM_CHANGENAMECOLOR, killer.SessionId, (int)status, 0, 0, killer.CharacterName, includeSelf: true);
        }
    }

    public void TriggerBreakInvisibility(ClientSession session, string reason, long nowMs)
    {
        if (session.State == null) return;
        session.State.LastAttackTickMs = nowMs;
        session.State.BreakSpellInvisibility();
        if (reason.Contains("攻击"))
        {
            session.EnqueuePacket(ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, session.X, session.Y, 18, "显形破隐");
            BroadcastToNinePatch(session, ProtocolConsts.SM_SPELLEFFECT_DELPHI, session.SessionId, session.X, session.Y, 18, "显形破隐", includeSelf: false);
        }
    }

    public void ExecuteGodPrayerRebellion(ClientSession player)
    {
        player.Pets.Clear();
        if (RegenEngine != null)
        {
            foreach (var m in RegenEngine.ActiveMonsters)
            {
                if (m.MasterSessionId == player.SessionId)
                {
                    m.MasterSessionId = 0;
                    m.TargetSessionId = player.SessionId;
                    m.ThreatTable.AddHate(player.SessionId, 100);
                }
            }
        }
        if (player.State != null)
        {
            OfficialEquipSlot[] prayerSlots = [
                OfficialEquipSlot.Helmet,
                OfficialEquipSlot.Necklace,
                OfficialEquipSlot.ArmringL,
                OfficialEquipSlot.RingL,
                OfficialEquipSlot.Weapon
            ];
            foreach (var slot in prayerSlots)
            {
                var eq = player.State.Equipment[(int)slot];
                if (eq != null && eq.Name.Contains("祈祷"))
                {
                    player.State.Equipment[(int)slot] = null;
                    player.EnqueuePacket(ProtocolConsts.SM_TAKEOFF_OK, (int)slot, 0, 0, 0);
                    player.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "祈祷装备破碎了。");
                    break;
                }
            }
        }
    }

    public void ApplyDamageToMonster(ClientSession attacker, MonsterInstance monster, int baseDamage, long nowMs)
    {
        if (monster.Hp <= 0) return;

        int finalDamage = baseDamage;
        if (monster.HasRedPoison)
        {
            if (nowMs >= monster.RedPoisonExpireMs)
            {
                monster.HasRedPoison = false;
            }
            else
            {
                finalDamage = (int)(baseDamage * 1.2f);
            }
        }

        monster.Hp = Math.Max(0, monster.Hp - finalDamage);

        if (monster.IsTrappedByHolyCurse)
        {
            monster.IsTrappedByHolyCurse = false;
            monster.TrappedExpireMs = 0;
            monster.NextActionTimeMs = nowMs;
            monster.TargetSessionId = attacker.SessionId;
            attacker.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "困魔咒破除了。");
        }

        if (monster.IsRevealed && monster.RevealedBySessionId != 0)
        {
            if (ActiveSessions.TryGetValue(monster.RevealedBySessionId, out var revealer))
            {
                revealer.EnqueuePacket(ProtocolConsts.SM_HEALTHBAR_COLOR_DELPHI, monster.Uid, monster.Hp, monster.MaxHp, 0, "");
            }
        }

        // 盛大官方 1.5 祖玛教主残血狂暴 (HP <= 20% 触发暴怒咆哮并召唤卫士)
        if (monster.Name.Contains("祖玛教主") && !monster.IsMad && monster.Hp > 0 && monster.Hp <= monster.MaxHp * 0.2f)
        {
            monster.IsMad = true;
            attacker.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "祖玛教主发怒了！");
            if (RegenEngine != null)
            {
                RegenEngine.SpawnMonsterDirect(monster.MapId, monster.X + 1, monster.Y + 1, "祖玛卫士", 800, 30, 60);
            }
            BroadcastToNinePatch(attacker, ProtocolConsts.SM_SPELLEFFECT_DELPHI, monster.Uid, monster.X, monster.Y, 203, "ZumaLordRoar", includeSelf: true);
        }
    }

    public void SendUserMagics(ClientSession session)
    {
        if (session.State == null) return;
        session.EnqueuePacket(ProtocolConsts.SM_SENDUSERMAGIC, 0, 0, 0, 0, "");
    }

    public void TickMonsters(OfficialClientRulesEngine.DelphiRandom rng) => ProcessMonsterAiCycle(rng);

    public void OnPlayerDied(ClientSession session)
    {
        if (session.State != null)
        {
            var helmet = session.State.Equipment[(int)OfficialEquipSlot.Helmet];
            if (helmet != null && helmet.Name.Contains("祈祷头盔"))
            {
                session.State.Equipment[(int)OfficialEquipSlot.Helmet] = null;
                session.EnqueuePacket(ProtocolConsts.SM_TAKEOFF_OK, session.SessionId, (int)OfficialEquipSlot.Helmet, 0, 0);
                session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "祈祷头盔破碎了。");
            }
        }

        if (RegenEngine != null && session.Pets.Count > 0)
        {
            foreach (var pet in session.Pets)
            {
                RegenEngine.RemoveMonster(pet.Uid);
            }
        }
        session.Pets.Clear();
    }

    public void SyncRevealedPlayerHealthIfApplicable(ClientSession victim, long nowMs)
    {
        if (victim.State == null || !victim.State.IsRevealed) return;
        if (victim.State.RevealedBySessionId != 0 && ActiveSessions.TryGetValue(victim.State.RevealedBySessionId, out var revealer))
        {
            revealer.EnqueuePacket(ProtocolConsts.SM_HEALTHBAR_COLOR_DELPHI, victim.SessionId, victim.State.Hp, victim.State.MaxHp, 0, "");
        }
    }

    public bool CanFireWallDamagePlayer(int casterSessionId, ClientSession target, long nowMs)
    {
        if (target.SessionId == casterSessionId) return false;
        var party = GroupEngine.GetParty(casterSessionId);
        if (party != null && party.Members.Exists(m => m.SessionId == target.SessionId))
        {
            return false;
        }
        return true;
    }

    private void HandleGuildAllyRequest(ClientSession session, ReadOnlySpan<char> payload)
    {
        string targetGuild = payload.ToString().Trim();
        string myGuild = GuildEngine.GetPlayerGuild(session.CharacterName) ?? "战神殿";
        GuildWarEngine.AddAlliance(myGuild, targetGuild);
        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"{myGuild} 与 {targetGuild} 结盟。");
    }

    private void HandleGuildBreakAlly(ClientSession session, ReadOnlySpan<char> payload)
    {
        string targetGuild = payload.ToString().Trim();
        string myGuild = GuildEngine.GetPlayerGuild(session.CharacterName) ?? "战神殿";
        GuildWarEngine.RemoveAlliance(myGuild, targetGuild);
        session.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"{myGuild} 与 {targetGuild} 解除结盟。");
    }

    public void HandleMonsterDeathAndLoot(ClientSession killer, MonsterInstance monster, GameMap map, long nowMs)
    {
        OnMonsterKilledByPlayer(killer, monster);
    }

    private void DistributeKillExpToPlayerAndPets(ClientSession player, int totalExp)
    {
        if (player.State == null) return;

        int playerExp = totalExp;
        if (player.Pets.Count > 0)
        {
            int petShare = totalExp / 2;
            playerExp = totalExp - petShare;
            int eachPetExp = petShare / player.Pets.Count;
            foreach (var pet in player.Pets)
            {
                if (HuWeiPetEngine.AddKillExpAndTryLevelUp(pet, eachPetExp))
                {
                    player.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, $"你的随从 {pet.Name} 已晋升至 {pet.Level} 级（{pet.NameColor}）");
                }
            }
        }

        bool leveledUp = player.State.GainExp(playerExp);
        player.EnqueuePacket(ProtocolConsts.SM_EXPERIENCE, (int)player.State.Exp, 0, 0, 0);

        if (leveledUp)
        {
            player.EnqueuePacket(ProtocolConsts.SM_LEVELUP_DELPHI, player.State.Level, 0, 0, 0);
            BroadcastToNinePatch(player, ProtocolConsts.SM_LEVELUP_DELPHI, player.SessionId, player.State.Level, 0, 0, player.CharacterName, includeSelf: false);
            player.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, player.State.Hp, player.State.MaxHp, player.State.Mp, player.State.MaxMp);
            SendAbility(player);
        }
    }

    /// <summary>
    /// 盛大官方 1.5 玩家死亡统一权威结算 (涵盖：行会战/攻城免惩罚判定、PK值累计、武器诅咒加深、随从解散、祈祷装备碎裂、爆装与爆背包、25格同心圆散射、SM_TAKEOFF_OK/SM_DELITEM/SM_WEIGHTCHANGED 客户端双向同步)
    /// 严格对齐官方 M2Server 原版物理反编译: TUserEngine.UserDie @ 0x4A0218, TCreature.ScatterBagItems @ 0x4BB44C, TCreature.DropUseItems @ 0x4BB6C8
    /// </summary>
    public void AuthoritativeExecutePlayerDeath(ClientSession defender, ClientSession? killer, GameMap map, long nowMs)
    {
        if (defender.State == null) return;

        // 1. 确保血量归零并触发官方随从解散与祈祷头盔碎裂 (IDA 0x4BB710)
        defender.State.Hp = 0;
        OnPlayerDied(defender);

        // 2. 杀手 PK 惩罚与武器诅咒结算 (官方 M2 0x4C1EF4 / 0x4B012C)
        if (killer != null && killer != defender && killer.State != null)
        {
            bool isFreePk = false;
            if (SabukEngine != null && GuildEngine != null && !string.IsNullOrEmpty(killer.CharacterName) && !string.IsNullOrEmpty(defender.CharacterName))
            {
                isFreePk = SabukEngine.IsFreePkBattle(
                    defender.CurrentMapId,
                    GuildEngine.GetPlayerGuild(killer.CharacterName),
                    GuildEngine.GetPlayerGuild(defender.CharacterName));
            }

            if (!isFreePk)
            {
                bool wasWhiteName = defender.State.PkPoints == 0 && !defender.State.IsFlashingName;
                if (wasWhiteName)
                {
                    killer.State.AddPkMurder();
                    uint mask = killer.State.GetCurrentStatusMask(nowMs);
                    killer.EnqueuePacket(ProtocolConsts.SM_CHANGENAMECOLOR, killer.SessionId, (int)mask, 0, 0);
                    BroadcastToNinePatch(killer, ProtocolConsts.SM_CHANGENAMECOLOR, killer.SessionId, (int)mask, 0, 0, killer.CharacterName, includeSelf: false);

                    // 恶意击杀白名加深武器诅咒 (IDA 0x4C1EF4)
                    bool cursed = ItemLifecycleEngine.CheckWeaponCurseOnMurder(killer.State, Random.Shared);
                    if (cursed)
                    {
                        killer.EnqueuePacket(ProtocolConsts.SM_SYSMSG_DELPHI, 0, 0, 0, 0, "你的武器被诅咒了。");
                    }
                }
            }
        }

        // 3. 广播死亡倒地动作: SM_DEATH_ACTION_DELPHI (28)
        BroadcastToNinePatch(defender, ProtocolConsts.SM_DEATH_ACTION_DELPHI, defender.SessionId, defender.X, defender.Y, 0, defender.CharacterName, includeSelf: true);

        // 4. 官方死亡爆装与爆背包结算 (IDA 0x4BB44C / 0x4BB6C8)
        var dropped = MonsterDropEngine.ExecuteDeathDrop(defender.State, OfficialClientRulesEngine.DelphiRandom.Shared);

        ReadOnlySpan<(int dx, int dy)> scatterOffsets = stackalloc (int, int)[]
        {
            (0, 0),
            (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, 1), (-1, 0), (-1, -1),
            (0, -2), (1, -2), (2, -2), (2, -1), (2, 0), (2, 1), (2, 2), (1, 2),
            (0, 2), (-1, 2), (-2, 2), (-2, 1), (-2, 0), (-2, -1), (-2, -2), (-1, -2)
        };
        int scatterIdx = 0;

        for (int d = 0; d < dropped.Count; d++)
        {
            var drop = dropped[d];
            int dropX = defender.X;
            int dropY = defender.Y;

            while (scatterIdx < scatterOffsets.Length)
            {
                var (ox, oy) = scatterOffsets[scatterIdx++];
                int testX = defender.X + ox;
                int testY = defender.Y + oy;
                if (map.IsWalkable(testX, testY))
                {
                    dropX = testX;
                    dropY = testY;
                    break;
                }
            }

            // 同步死者客户端移除
            if (drop.FromEquipment)
            {
                // 身上装备爆掉: 下发 SM_TAKEOFF_OK (619)
                defender.EnqueuePacket(ProtocolConsts.SM_TAKEOFF_OK, defender.SessionId, drop.OriginSlot, 0, 0);
            }
            else
            {
                // 背包物品爆掉: 下发 SM_DELITEM (202)
                defender.EnqueuePacket(ProtocolConsts.SM_DELITEM, drop.ItemUid, drop.OriginSlot, 0, 0, drop.ItemName);
            }

            // 生成地表实体，杀手享有 80 秒拾取保护权
            int groundUid = System.Threading.Interlocked.Increment(ref _nextGroundItemUid);
            map.AddGroundItem(new GroundItemEntity
            {
                Uid = groundUid,
                Name = drop.ItemName,
                X = dropX,
                Y = dropY,
                OwnerSessionId = killer?.SessionId ?? 0,
                DropTickMs = nowMs,
                Weight = 1
            });
            BroadcastToNinePatch(defender, ProtocolConsts.SM_GROUNDITEM, groundUid, dropX, dropY, 0, drop.ItemName, includeSelf: true);
        }

        // 5. 同步死者负重与自身状态
        defender.State.RecalculateAuthoritativeWeights(defender.State.Gold);
        defender.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, defender.State.BagWeight, defender.State.WearWeight, defender.State.HandWeight, 0);
        defender.EnqueuePacket(ProtocolConsts.SM_MYSTATUS, defender.State.Hp, defender.State.MaxHp, defender.State.Mp, defender.State.MaxMp);
    }

    public void ProcessPlayerDeath(ClientSession defender, ClientSession killer, GameMap map, long nowMs)
    {
        AuthoritativeExecutePlayerDeath(defender, killer, map, nowMs);
    }

    public void SyncRevivalRingStateToClient(ClientSession session)
    {
        if (session.State == null) return;
        session.EnqueuePacket(ProtocolConsts.SM_TAKEOFF_OK, (int)OfficialEquipSlot.RingL, 0, 0, 0);
        session.EnqueuePacket(ProtocolConsts.SM_WEIGHTCHANGED, session.State.BagWeight, session.State.WearWeight, session.State.HandWeight, 0);
    }

    #endregion
}

public sealed class GuildWarEngine
{
    private readonly HashSet<(string, string)> _wars = new();
    private readonly Dictionary<(string, string), long> _warExpirations = new();
    private readonly HashSet<(string, string)> _allies = new();

    private static (string, string) NormalizePair(string a, string b)
    {
        return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) < 0 ? (a, b) : (b, a);
    }

    public void DeclareWar(string guildA, string guildB, long nowMs, long durationMs = 3 * 3600 * 1000L)
    {
        var pair = NormalizePair(guildA, guildB);
        _wars.Add(pair);
        _warExpirations[pair] = nowMs + durationMs;
    }

    public bool AreGuildsAtWar(string guildA, string guildB, long nowMs)
    {
        var pair = NormalizePair(guildA, guildB);
        if (_warExpirations.TryGetValue(pair, out long expire))
        {
            if (nowMs < expire) return true;
            _wars.Remove(pair);
            _warExpirations.Remove(pair);
        }
        return false;
    }

    public bool IsKillExemptFromPkPenalty(string killerGuild, string victimGuild, long nowMs)
    {
        return AreGuildsAtWar(killerGuild, victimGuild, nowMs);
    }

    public void AddAlliance(string guildA, string guildB)
    {
        _allies.Add(NormalizePair(guildA, guildB));
    }

    public void RemoveAlliance(string guildA, string guildB)
    {
        _allies.Remove(NormalizePair(guildA, guildB));
    }

    public bool AreGuildsAllied(string guildA, string guildB)
    {
        return _allies.Contains(NormalizePair(guildA, guildB));
    }
}
