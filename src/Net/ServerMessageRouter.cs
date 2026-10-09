using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Godot;
using Mir2Client.Audio;
using Mir2Client.Game;
using Mir2Client.Render;
using Mir2Client.UI;
using Mir2Client.Utils;

namespace Mir2Client.Net;

/// <summary>
/// 盛大官方 1.5 服务端下行消息 (SM_) 总分发与原生 UI 驱动路由器。
/// 严格 1:1 对齐官方客户端二进制分发器 (IDA 0x467920 jumptable 实证)，
/// 覆盖背包、装备、技能魔法书、属性能力、NPC 交互、商店货架、仓库托管、行会与组队等核心生命周期。
/// </summary>
public sealed class ServerMessageRouter
{
    // ============ 官方 1.5 权威下行 SM 协议号 (对齐 IDA 0x467920) ============
    public const short SM_GOLDCHANGED            = 40;   // 金币变动广播
    public const short SM_GOLDCHANGED_DELPHI     = 653;  // Delphi 官方金币变动码 (IDA case 653 @ 0x469C80)
    public const short SM_LEVELUP                = 44;   // 角色升级 (IDA case ',' @ 0x46A18D)
    public const short SM_EXPERIENCE             = 45;   // 经验增长 (IDA case '-' @ 0x46A1B0)
    public const short SM_SENDUSERSTATE          = 52;   // 角色基础四维属性 (攻魔道防, IDA case '4' @ 0x46A250)
    public const short SM_HEALTHSPELLCHANGED     = 53;   // HP/MP 血量魔法值变动
    public const short SM_CHANGELIGHT            = 46;   // 全局昼夜光照范围与火把状态 (46)
    public const short SM_NPCDIALOG              = 54;   // NPC 对话脚本页面
    public const short SM_LOGICSTATE             = 208;  // 主角坐标与朝向设定 (208)
    public const short SM_USERSPAWN              = 210;  // 玩家进入视野 (210)
    public const short SM_USERDELETE             = 211;  // 玩家离开视野 (211)
    public const short SM_MONSPAWN               = 220;  // 怪物进入视野 (220)
    public const short SM_MONDELETE              = 221;  // 怪物离开视野 (221)
    public const short SM_MONDEAD                = 222;  // 怪物倒地死亡 (222)
    public const short SM_DEATH                  = 21;   // 实体死亡倒地 (21)
    public const short SM_NPCSPAWN               = 230;  // NPC 进入视野 (230)
    public const short SM_NPCDELETE              = 231;  // NPC 离开视野 (231)
    public const short SM_OBJECT_DISAPPEAR       = 29;   // 实体消除 (Delphi 29)
    public const short SM_TURNDIRECTION_DELPHI   = 204;  // 实体转向 (204)
    public const short SM_NEWMAP_DELPHI          = 521;  // 地图切换/初始化黑屏转场 (521)
    public const short SM_CLEAROBJECTS           = 810;  // 清空视野实体 / 官方时间同步 (810, SM_TIMECHECKMSG 同号)
    public const short SM_ADDITEM                = 200;  // 背包添加物品
    public const short SM_SENDUSERMAGIC          = 201;  // 角色已学技能列表批量下发
    public const short SM_DELITEM                = 202;  // 背包移除物品
    public const short SM_UPDATEUSERMAGIC        = 203;  // 技能经验熟练度更新
    public const short SM_ALLOWMOVE              = 203;  // 盛大官方/网关位移确认与拉回校准 (bridge.rs)
    public const short SM_MERCHANTDLG_CLOSE      = 644;  // 官方关闭 NPC 对话框 (IDA 0x467920 case 644 @ 0x46D65C)
    public const short SM_SKILLLEVELUP           = 644;  // 历史测试别名兼容 (644)
    public const short SM_TAKEON_OK              = 615;  // 穿戴装备成功
    public const short SM_EXCHGTAKEONOK          = 615;  // 互换穿戴装备成功 (IDA 0x467920)
    public const short SM_TAKEON_FAIL            = 616;  // 穿戴装备失败 (IDA 0x467920)
    public const short SM_EXCHGTAKEONFAIL        = 616;  // 互换穿戴装备失败 (IDA 0x467920)
    public const short SM_TAKEOFF_OK             = 619;  // 脱下装备成功
    public const short SM_EQUIPINFO              = 621;  // 全量 9 槽位装备下发
    public const short SM_WEIGHTCHANGED          = 622;  // 负重同步
    public const short SM_MERCHANTDLG            = 631;  // 商店货架界面下发
    public const short SM_DEALMENU               = 673;  // 交易界面弹出 (IDA 0x46A5AA case 673)
    public const short SM_DEALTRY_FAIL          = 674;  // 交易发起失败/熔断取消
    public const short SM_DEALADDITEM_OK        = 675;  // 己方放入物品成功
    public const short SM_DEALADDITEM_FAIL      = 676;  // 己方放入物品失败
    public const short SM_DEALDELITEM_OK        = 677;  // 己方取下物品成功
    public const short SM_DEALDELITEM_FAIL      = 678;  // 己方取下物品失败
    public const short SM_DEALREMOTEADDITEM     = 682;  // 对方放入物品广播
    public const short SM_DEALREMOTEDELITEM     = 683;  // 对方取下物品广播
    public const short SM_DEALCHGGOLD_OK        = 684;  // 己方修改交易金币成功
    public const short SM_DEALCHGGOLD_FAIL      = 685;  // 己方修改交易金币失败
    public const short SM_DEALREMOTECHGGOLD     = 686;  // 对方修改交易金币广播
    public const short SM_DEALSUCCESS           = 687;  // 交易完成交割
    public const short SM_DEALLOCKED            = 678;  // 对方锁定交易栏状态同步 (IDA case 678 sub_49F9E4)
    public const short SM_DEALCOMPLETE          = 678;  // 悬置：与 SM_DEALDELITEM_FAIL=678 重号（bridge 内亦复用），路由归取下失败，待联调
    public const short SM_DEALCANCEL            = 688;  // 交易取消退回
    public const short SM_DEALCANCEL_DELPHI     = 681;  // 交易取消 (Delphi 变体 681)
    public const short SM_USERSTORAGE_DLG        = 700;  // 仓库保管箱托管界面弹出
    public const short SM_SENDUSERSTATE_INSPECT  = 751;  // 观人/他人状态下发 (IDA 0x46A857)
    public const short SM_OPENGUILDDLG           = 753;  // 打开行会主页 (bridge SM_OPENGUILDDLG_DELPHI)
    public const short SM_SENDGUILDMEMBERLIST    = 756;  // 行会成员列表批量下发 (IDA 0x467920 case 756 load_guild_members)
    public const short SM_GUILDMEMBERLIST_ALT    = 671;  // 行会成员列表 (bridge SM_GUILDMEMBERLIST，recog=人数)
    public const short SM_GUILDMAKEALLYOK        = 759;  // 行会结盟成功回显广播 (IDA 0x467920)
    public const short SM_GUILDBREAKALLYOK       = 761;  // 行会解除结盟广播 (IDA 0x467920)
    public const short SM_GUILDNOTICE_RESULT     = 767;  // 公告更新结果 (bridge SM_GUILDNOTICE_RESULT_DELPHI)
    public const short SM_ATTACKMODECHANGED      = 766;  // 官方攻击模式变更回显 (IDA sub_467920 case 766 @ 0x468A2B)
    public const short SM_NEEDPASSWORD           = 2014; // 二级安全密码弹窗 (sub_486DF0)
    public const short SM_OBJECTSPELL            = 243;  // 实体施法动作 (bridge.rs 权威实证)
    public const short SM_OBJECTSTRUCK           = 244;  // 实体受击后仰 (bridge.rs 权威实证)
    public const short SM_OBJECTSTRUCK_DELPHI    = 637;  // 实体受击伤害结算 (Delphi 变体)
    public const short SM_MAGIC_FIRE             = 638;  // 施法特效下发广播 (bridge.rs 权威实证)
    public const short SM_LIGHTING_DELPHI        = 17;   // 定向轰击闪电 (bridge.rs / Delphi)
    public const short SM_SPELLRESULT            = 30;   // 施法伤害结算 (ProtocolConsts)

    private readonly HudController? _hud;
    private readonly Mir2Client.Game.EntityManager? _entityManager;
    private readonly GameView? _gameView;
    private readonly Mir2Client.Game.GroundItemManager? _groundItemManager;

    public ServerMessageRouter(HudController? hud, Mir2Client.Game.EntityManager? em = null, GameView? gv = null, Mir2Client.Game.GroundItemManager? gm = null)
    {
        _hud = hud;
        _entityManager = em;
        _gameView = gv;
        _groundItemManager = gm;
    }

    /// <summary>
    /// 处理服务端下发的数据包并驱动对应原生 UI 窗体
    /// </summary>
    public void OnPacket(short smCode, byte[] pkt)
    {
        ServerPacket packet;
        if (pkt != null && pkt.Length >= 10)
        {
            var parsed = PacketDecoder.Parse(pkt);
            packet = parsed ?? new ServerPacket(smCode, 0, 0, 0, pkt);
        }
        else
        {
            packet = new ServerPacket(smCode, 0, 0, 0, pkt ?? Array.Empty<byte>());
        }

        OnPacket(packet);
    }

    public void OnPacket(ServerPacket packet)
    {
        short smCode = packet.Ident;
        switch (smCode)
        {
            case SM_GOLDCHANGED:
            case SM_GOLDCHANGED_DELPHI: // 653: 盛大官方 1.5 原版金币变动协议 (IDA sub_467920 case 653 @ 0x469C80)
                HandleGoldChanged(packet);
                break;
            case SM_LEVELUP:
                HandleLevelUp(packet);
                break;
            case SM_EXPERIENCE:
                HandleExperience(packet);
                break;
            case SM_HEALTHSPELLCHANGED:
                HandleHealthSpellChanged(packet);
                break;
            case SM_WEIGHTCHANGED:
                HandleWeightChanged(packet);
                break;
            case SM_CHANGELIGHT:
                HandleChangeLight(packet);
                break;
            case ProtocolConsts.SM_CHANGENAMECOLOR: // 41: 官方 1.5 实体名字颜色变动 (IDA case ')' @ 0x46A157)
                HandleChangeNameColor(packet);
                break;
            case ProtocolConsts.SM_GUILDNAME_DELPHI: // 42: 官方 1.5 实体所属行会与封号变动 (IDA case '*' @ 0x46A16F)
                HandleChangeEntityGuildName(packet);
                break;
            case ProtocolConsts.SM_SYSMSG_DELPHI: // 100~104: 盛大官方 1.5 系统提示与带音效广播 (IDA sub_467920 100~104 @ 0x46AF10)
            case 101:
            case 102:
            case 103:
            case ProtocolConsts.SM_CHAR_QUERY_EXTRA: // 104
                HandleSysMsgDelphi(packet);
                break;
            case SM_LOGICSTATE:
                HandleLogicState(packet);
                break;
            case SM_NEWMAP_DELPHI:
                HandleNewMap(packet);
                break;
            case SM_USERSPAWN:
            case SM_MONSPAWN:
            case SM_NPCSPAWN:
                HandleEntitySpawn(packet);
                break;
            case SM_USERDELETE: // 211: 兼容玩家离开视野与官方 1.5 角色列表下发 (SM_CHARLIST_DELPHI)
                if (packet.Extra != null && packet.Extra.Length > 0)
                {
                    SafeLog($"[SM] 收到官方 1.5 角色列表更新通知 (SM_CHARLIST_DELPHI=211)");
                }
                else
                {
                    HandleEntityDelete(packet);
                }
                break;
            case SM_MONDELETE:
            case SM_NPCDELETE:
                HandleEntityDelete(packet);
                break;
            case SM_TURNDIRECTION_DELPHI:
                HandleTurnDirection(packet);
                break;
            // Delphi 实体别名：10/11 在真值表内，与 WALK/RUN_DELPHI 同值，按实体语义归属；
            // 路由内无 WALK/RUN 分支，故无冲突，L2。
            case ProtocolConsts.SM_ENTITYAPPEAR:
                HandleEntitySpawn(packet);
                break;
            case ProtocolConsts.SM_ENTITYDISAPPEAR:
                HandleEntityDelete(packet);
                break;
            case ProtocolConsts.SM_ENTITYMOVE:
                HandleEntityMove(packet);
                break;
            case ProtocolConsts.SM_ENTITYTURN:
                HandleTurnDirection(packet);
                break;
            case ProtocolConsts.SM_ITEMSHOW:
                HandleItemShow(packet);
                break;
            case SM_MONDEAD:
            case SM_DEATH:
                HandleEntityDead(packet);
                break;
            case SM_CLEAROBJECTS: // 810: SM_CLEAROBJECTS 与官方 SM_TIMECHECKMSG 同号
                _entityManager?.Clear();
                SafeLog($"[SM] 官方时间同步与视野重置 tick={packet.Recog}");
                break;
            case ProtocolConsts.SM_BAGITEMS: // 600: 背包物品全量刷新 / 官方 1.5 handle_sm_refresh (IDA case 0x258u)
                _hud?.ClearAllBagItems();
                _hud?.AddChat("包裹", "[包裹] 背包数据已重置同步。", new Color(0.7f, 0.9f, 0.7f));
                break;
            case ProtocolConsts.SM_MINERESULT: // 601: 采矿结果 / 官方 1.5 handle_sm_refresh2 (IDA case 0x259u)
                HandleMineResult(packet);
                break;
            case SM_ADDITEM:
                HandleAddItem(packet);
                break;
            case SM_DELITEM:
                HandleDelItem(packet);
                break;
            case ProtocolConsts.SM_GROUNDITEM:
                HandleGroundItem(packet);
                break;
            case ProtocolConsts.SM_DELGROUNDITEM:
                HandleDelGroundItem(packet);
                break;
            case SM_SENDUSERMAGIC:
                HandleSendUserMagic(packet);
                break;
            case SM_ALLOWMOVE:
                HandleAllowMove(packet);
                break;
            case SM_MERCHANTDLG_CLOSE: // 644: 官方 NPC 对话框关闭 (IDA case 644 @ 0x46D65C) / 技能学习与熟练度下发 (bridge SM_SKILLLEVELUP)
                if (packet.Recog > 0)
                {
                    HandleSkillLevelUp(packet);
                }
                else
                {
                    _hud?.CloseNpcDialog();
                }
                break;
            case ProtocolConsts.SM_OUTOFCONNECTION:
                _hud?.ShowModalMessage("连接提示", "服务器连接人数已满，请稍后再试。");
                break;
            case ProtocolConsts.SM_DISCONNECT_DELPHI:
                _hud?.ShowModalMessage("系统提示", "与服务器的连接已被切断。");
                break;
            case ProtocolConsts.SM_BILLING_NOTICE:
                HandleBillingNotice(packet);
                break;
            case SM_EQUIPINFO:
                HandleEquipInfo(packet);
                break;
            case SM_TAKEON_OK:
                HandleTakeOnOk(packet);
                break;
            case SM_TAKEOFF_OK: // 619: 盛大官方 1.5 脱下装备成功 (IDA sub_467920 case 619 @ 0x4685A3)
                HandleTakeOffOk(packet);
                break;
            case SM_NPCDIALOG:
                HandleNpcDialog(packet);
                break;
            case SM_MERCHANTDLG:
                HandleMerchantDlg(packet);
                break;
            case SM_DEALMENU:
                HandleDealMenu(packet);
                break;
            case SM_DEALTRY_FAIL:
            case SM_DEALCANCEL:
            case SM_DEALCANCEL_DELPHI:
                HandleDealCancel(packet);
                break;
            case SM_DEALADDITEM_OK:
                HandleDealAddItemOk(packet);
                break;
            case SM_DEALADDITEM_FAIL:
                _hud?.AddChat("交易", $"[交易] 放入物品失败: {DecodeExtraText(packet.Extra)}", new Color(1f, 0.4f, 0.4f));
                _hud?.OnServerTradeAddItemFail(packet.Param);
                break;
            case SM_DEALLOCKED: // 678: 对方锁定交易栏状态同步 (IDA case 678 sub_49F9E4)
                _hud?.OnServerTradeRemoteLocked(true);
                break;
            case SM_DEALDELITEM_OK:
                _hud?.OnServerTradeDelItemOk(packet.Param);
                break;
            // 注: 678 官方原版 IDA ProcessServerMessage 权威为 SM_DEALLOCKED (锁定同步)
            // 若为错误回显则由 ExtraText 驱动，统一消除重号冲突
            case SM_DEALREMOTEADDITEM: // 682: 对方放入物品广播 (IDA case 682 sub_495794(125))
                _hud?.OnServerTradeRemoteAddItem(packet.Param, DecodeExtraText(packet.Extra));
                AudioManager.Instance?.PlaySoundId(125);
                break;
            case SM_DEALREMOTEDELITEM: // 683: 对方取下物品广播 (IDA case 683 sub_495794(126))
                _hud?.OnServerTradeRemoteDelItem(packet.Param);
                AudioManager.Instance?.PlaySoundId(126);
                break;
            case SM_DEALCHGGOLD_OK: // 684: 己方修改交易金币成功 (IDA case 684/685 sub_495794(106))
                _hud?.OnServerTradeChgGold(packet.Recog, true);
                if (packet.Tag > 0) _hud?.UpdateGold(packet.Tag);
                AudioManager.Instance?.PlaySoundId(106);
                break;
            case SM_DEALCHGGOLD_FAIL:
                _hud?.AddChat("交易", $"[交易] 修改金币失败: {DecodeExtraText(packet.Extra)}", new Color(1f, 0.4f, 0.4f));
                break;
            case SM_DEALREMOTECHGGOLD: // 686: 对方修改交易金币广播 (IDA case 686 sub_495794(106))
                _hud?.OnServerTradeChgGold(packet.Recog, false);
                AudioManager.Instance?.PlaySoundId(106);
                break;
            case SM_DEALSUCCESS:
                _hud?.OnServerTradeSuccess();
                break;
            case SM_USERSTORAGE_DLG:
                _hud?.ToggleStorage();
                break;
            case SM_OPENGUILDDLG:
                HandleOpenGuildDlg(packet);
                break;
            case SM_ATTACKMODECHANGED:
                // 盛大官方 1.5 原版二进制 IDA 0x467920 case 766 @ 0x468A2B: dword_4F938C = HIWORD(v67) (即 packet.Param)
                int attackMode = packet.Param != 0 ? packet.Param : (packet.Tag != 0 ? packet.Tag : 1);
                _hud?.OnServerAttackModeChanged(attackMode);
                break;
            case SM_NEEDPASSWORD:
                _hud?.OnNeedPassword();
                break;
            case SM_SENDUSERSTATE:
            case SM_SENDUSERSTATE_INSPECT:
                HandleSendUserState(packet);
                break;
            case SM_GUILDMEMBERLIST_ALT: // 671: 盛大官方 1.5 商店出售物品反馈 (IDA case 671 handle_sm_sell) / 行会成员列表兼容
                if (!string.IsNullOrEmpty(packet.ExtraStr) && (packet.ExtraStr.Contains('\t') || packet.ExtraStr.Contains('/')))
                {
                    HandleSendGuildMemberList(packet);
                }
                else
                {
                    // 盛大官方 1.5 原版二进制 case 671:
                    // if (v66 >= 0) handle_sm_sell(); else show_dialog_message(v39, dword_46BADC);
                    if (packet.Recog >= 0)
                    {
                        _hud?.UpdateGold(packet.Recog);
                        _hud?.AddChat("商店", $"[商店] 出售物品成功，当前金币：{packet.Recog:N0}", new Color(0.5f, 1f, 0.5f));
                    }
                    else
                    {
                        _hud?.ShowModalMessage("出售提示", "这个物品不能卖出。"); // 官方 IDA 0x46BADC
                        _hud?.AddChat("商店", "[商店] 出售失败：这个物品不能卖出。", new Color(1f, 0.4f, 0.4f));
                    }
                }
                break;
            case ProtocolConsts.SM_SENDGUILDMEMBERLIST: // 756: 盛大官方 1.5 行会成员列表批量下发 (IDA case 756 load_guild_members)
                HandleSendGuildMemberList(packet);
                break;
            case SM_GUILDNOTICE_RESULT: // 767: 行会公告更新结果
                HandleGuildMessage(packet);
                break;
            case ProtocolConsts.SM_BUILDGUILDOK:
            case ProtocolConsts.SM_BUILDGUILDFAIL:
                HandleBuildGuildResult(packet);
                break;
            // bridge/Delphi 权威号：757 加人成功 / 754 加人失败 / 759 踢人成功 / 760 踢人失败。
            case ProtocolConsts.SM_GUILDADDMEMBEROK:
            case ProtocolConsts.SM_GUILDADDMEMBERFAIL:
            case ProtocolConsts.SM_GUILDDELMEMBEROK:
                HandleGuildMemberChange(packet);
                break;
            case ProtocolConsts.SM_GUILDDELMEMBERFAIL: // 760: 踢出行会失败 (IDA sub_467920 0x46A941)
                HandleGuildKickFail(packet);
                break;
            case ProtocolConsts.SM_GUILDMAKEALLYFAIL: // 769: 结盟申请失败 (IDA sub_467920 0x46AAA7)
                HandleGuildAllyFail(packet);
                break;
            case ProtocolConsts.SM_GUILDBREAKALLYFAIL: // 771: 解除结盟失败 (IDA sub_467920 0x46AB30)
                HandleGuildBreakAllyFail(packet);
                break;
            case ProtocolConsts.SM_GUILDRANKUPDATEFAIL: // 761: 更改封号失败 (IDA sub_467920 0x46A9D4)
                HandleGuildRankFail(packet);
                break;
            case ProtocolConsts.SM_ADJUSTBONUS:
                _hud?.ToggleAdjustAbil(Math.Max(0, packet.Recog));
                break;
            // 边缘动作包（bridge 36/37/750/764/765）：破土/潜地/改名/捐献，L2。
            case 36: // SM_DIGUP：recog=实体，param/tag=出土点。实体缺席则按怪补生。
                HandleDigUpDown(packet, true);
                break;
            case 37: // SM_DIGDOWN：recog=实体潜地。暂按移除（下次 spawn/move 重建）。
                HandleDigUpDown(packet, false);
                break;
            case 750: // SM_CHANGEGUILDNAME：extra=新行会名（加入/退出/更名同号，bridge 2118）。
                HandleChangeGuildName(packet);
                break;
            case ProtocolConsts.SM_GROUPMEMBERS: // 667 (SM_GROUPMEMBERLIST 同号)
                HandleGroupMembers(packet);
                break;
            case ProtocolConsts.SM_CRY: // 765: 全服求助与叫喊 (SM_DONATEFAIL 同号)
                HandleCry(packet);
                break;
            case ProtocolConsts.SM_INSTANCEHEALGAUGE: // 659: 即时治愈
                HandleInstanceHealGauge(packet);
                break;
            case ProtocolConsts.SM_SPACEMOVEHIDE: // 800: 空间传送消失
            case ProtocolConsts.SM_SPACEMOVEHIDE2: // 806: 空间传送消失别名 (IDA sub_467920 case 806)
                HandleSpaceMove(packet, false);
                break;
            case ProtocolConsts.SM_SPACEMOVESHOW: // 801: 空间传送出现
            case ProtocolConsts.SM_SPACEMOVESHOW2: // 807: 空间传送出现别名 (IDA sub_467920 case 807)
                HandleSpaceMove(packet, true);
                break;
            case ProtocolConsts.SM_TELEPORT: // 804: 空间坐标瞬移突变 (IDA sub_467920 case 804)
            case ProtocolConsts.SM_MAPMOVE: // 23: 官方 1.5 实体同地图瞬移坐标突变 (IDA sub_467920 case 23)
                HandleTeleport(packet);
                break;
            case ProtocolConsts.SM_ACTION_PARAM_SYNC: // 22: 官方 1.5 动作参数三元组同步 (IDA sub_467920 case 22)
                _entityManager?.UpdateActorParam(packet.Recog, packet.Param);
                break;
            case ProtocolConsts.SM_DEATH_ACTION_DELPHI: // 28: 官方 1.5 实体死亡倒地动画 (IDA sub_467920 case 28)
                HandleEntityDead(packet);
                break;
            case ProtocolConsts.SM_TAKEON_FAIL: // 616: 穿戴装备失败 (SM_EXCHGTAKEONFAIL 同号)
                HandleTakeOnFail(packet);
                break;
            case ProtocolConsts.SM_MAGICLVEXP: // 640: 技能熟练度经验更新 / 官方 1.5 物品估价回显 (SM_QUERYPRICE_REC)
                if (packet.Param > 0 || packet.Tag > 0)
                {
                    HandleMagicLevelExp(packet);
                }
                else
                {
                    _hud?.UpdateItemPrice(packet.Recog);
                }
                break;
            case ProtocolConsts.SM_GROUP_INVITE_CONFIRM: // 658: 组队邀请模态确认 (IDA case 658)
                HandleGroupInviteConfirm(packet);
                break;
            case ProtocolConsts.SM_GROUP_MODE_CHANGED: // 660: 盛大官方 1.5 组队模式强制开启与建队成功 (IDA case 660: dword_4F94E8 = GetTickCount(); byte_4F94F8 = 1)
                _hud?.GroupPanel?.SetAllowGroup(true);
                _hud?.AddChat("组队", "[组队] 已开启允许组队模式。", new Color(0.4f, 1f, 0.5f));
                break;
            case ProtocolConsts.SM_MAPDESCRIPTION: // 661: 地图描述公告 / 组队邀请反馈 (IDA 0x46A3A2)
                if (packet.Recog < 0)
                {
                    HandleGroupInviteFail(packet);
                }
                else
                {
                    HandleMapDescription(packet);
                }
                break;
            case ProtocolConsts.SM_CREATEGROUP_FAIL: // 664: 创建编组失败模态弹窗 (IDA 0x46A42E)
                HandleCreateGroupFail(packet);
                break;
            case ProtocolConsts.SM_LEAVEGROUP_FAIL: // 665: 踢出/退出编组失败模态弹窗 (IDA 0x46A4CE)
                HandleLeaveGroupFail(packet);
                break;
            case ProtocolConsts.SM_DISMISS_GROUP: // 666: 盛大官方 1.5 组队成员重绘回调与解散处理 (IDA case 666: (*(void (**)(void))(*(_DWORD *)dword_4F94FC + 64))())
                _hud?.GroupPanel?.RefreshMembers();
                if (packet.Recog == 0 && packet.Param == 0)
                {
                    _hud?.GroupPanel?.ClearMembers();
                    _hud?.AddChat("组队", "[组队] 编组已经解散。", new Color(1f, 0.8f, 0.4f));
                }
                break;
            case ProtocolConsts.SM_SENDNOTICE: // 662: 全服系统公告跑马灯
                HandleSendNotice(packet);
                break;
            case ProtocolConsts.SM_MAGICFIRE: // 802: 魔法释放回显
                HandleMagicFireAck(packet);
                break;
            case ProtocolConsts.SM_MAGICFIREFAIL: // 803: 魔法释放失败
                HandleMagicFireFail(packet);
                break;
            case ProtocolConsts.SM_GUILDMESSAGE: // 764: 行会消息与捐献成功 (SM_DONATEOK 同号)
                if (packet.Recog > 0)
                {
                    HandleDonateResult(packet);
                }
                else
                {
                    HandleGuildMessage(packet);
                }
                break;
            case ProtocolConsts.SM_HEAR: // 6: 视野内说话广播
                HandleHear(packet);
                break;
            case ProtocolConsts.SM_SYSTEMMESSAGE: // 7: 屏幕滚动系统公告
                HandleSystemMessage(packet);
                break;
            case ProtocolConsts.SM_HIT_DELPHI: // 12: 实体挥砍物理攻击广播
                HandleHit(packet);
                break;
            case ProtocolConsts.SM_SITDOWN: // 27: 蹲下/割肉姿态切换
                HandleSitDown(packet);
                break;
            case ProtocolConsts.SM_MERCHANTSAY: // 633: NPC 商人对话广播 (SM_BUYITEMFAIL 同号)
            case ProtocolConsts.SM_DLGMSG: // 636: NPC 对话框脚本交互下发
                HandleNpcDialogMsg(packet);
                break;
            case ProtocolConsts.SM_BUY_OK_DELPHI: // 648: 购买成功更新金币 (recog = gold_left)
                _hud?.UpdateGold(packet.Recog);
                _hud?.AddChat("商店", $"[商店] 购买成功，当前剩余金币：{packet.Recog:N0}", new Color(0.5f, 1f, 0.5f));
                break;
            case ProtocolConsts.SM_SELL_FAIL_DELPHI: // 649: 出售物品失败 (IDA 0x469F04)
                _hud?.ShowModalMessage("商店提示", "你不能卖这个物品"); // 0x46B820
                _hud?.AddChat("商店", "[商店] 出售失败：你不能卖这个物品", new Color(1f, 0.4f, 0.4f));
                break;
            case ProtocolConsts.SM_SELL_OK_DELPHI: // 650: 出售成功更新金币 (recog = gold_gained)
                _hud?.UpdateGold(packet.Recog);
                _hud?.AddChat("商店", $"[商店] 出售成功，获得金币或剩余金币：{packet.Recog:N0}", new Color(0.5f, 1f, 0.5f));
                break;
            case ProtocolConsts.SM_BUY_FAIL_DELPHI: // 651: 购买物品失败 (IDA 0x46A157)
                {
                    string bMsg = packet.Recog switch
                    {
                        1 => "物品被卖出。",             // 0x46B8E4
                        2 => "没有更多物品可以携带了",   // 0x46B8FC (LABEL_364 共享)
                        3 => "你没有足够的钱来购买物品.", // 0x46B91C
                        _ => !string.IsNullOrEmpty(packet.ExtraStr) ? packet.ExtraStr : DecodeExtraText(packet.Extra)
                    };
                    if (string.IsNullOrEmpty(bMsg)) bMsg = "购买失败。";
                    _hud?.ShowModalMessage("商店提示", bMsg);
                    _hud?.AddChat("商店", $"[商店] 购买失败：{bMsg}", new Color(1f, 0.4f, 0.4f));
                }
                break;
            case ProtocolConsts.SM_OPENREPAIRDLG: // 668: 铁匠铺打开修理面板 (IDA open_shop_repair 0x46dae8)
                _hud?.OpenRepairPanel();
                break;
            case ProtocolConsts.SM_REPAIROK_DELPHI: // 669: 铁匠铺修理成功更新金币与持久 (IDA sub_467920 case 669 @ 0x469F9B: close_shop_repair)
                _hud?.UpdateGold(packet.Recog);
                _hud?.CloseRepairPanel();
                if (packet.Param > 0 && packet.Tag > 0)
                {
                    _hud?.AddChat("修理", $"[铁匠铺] 装备修理完毕，耐久恢复为 {packet.Param}/{packet.Tag}，当前剩余金币：{packet.Recog:N0}", new Color(0.5f, 1f, 0.5f));
                }
                else
                {
                    _hud?.AddChat("修理", $"[铁匠铺] 装备修理成功，当前剩余金币：{packet.Recog:N0}", new Color(0.5f, 1f, 0.5f));
                }
                break;
            case ProtocolConsts.SM_REPAIRFAIL_DELPHI: // 670: 铁匠铺修理失败模态弹窗 (IDA 0x469FE9 0x46B83C: close_shop_repair)
                _hud?.CloseRepairPanel();
                _hud?.ShowModalMessage("修理提示", "你不能修理这个物品 .");
                _hud?.AddChat("修理", "[铁匠铺] 修理失败：你不能修理这个物品 .", new Color(1f, 0.4f, 0.4f));
                break;
            case ProtocolConsts.SM_OPENGUILDDLGFAIL: // 752: 行会面板打开失败 (未加入行会)
                _hud?.ShowModalMessage("行会提示", "您尚未加入任何行会，无法查看行会管理面板。");
                break;
            // 盛大官方 1.5 原版二进制购买成功为 SM_BUY_OK_DELPHI = 648 (IDA 0x469EDA); 629/632 为非标私服兼容别名
            case ProtocolConsts.SM_BUYITEMSUCCESS:
            case ProtocolConsts.SM_BUYITEM_SUCCESS:
                _hud?.AddChat("商店", $"[商店] 购买成功：{DecodeExtraText(packet.Extra)}", new Color(0.5f, 1f, 0.5f));
                break;
            // 注：SM_STORAGE_ITEMS=682 与 SM_DEALREMOTEADDITEM=682 重号，
            // 真值表 682 归交易对方放物（有 OnServerTradeRemoteAddItem 实证），
            // 仓库列表分支停用，仓库走 SM_USERSTORAGE_DLG=700 开板，L2 待 M2 联调裁决真号。
            case ProtocolConsts.SM_EAT_OK:
                _hud?.AddChat("系统", $"[服药] {DecodeExtraText(packet.Extra)}", new Color(1f, 1f, 0.6f));
                break;
            case ProtocolConsts.SM_SAVEITEM_OK: // 701: 寄存物品成功
                _hud?.OnServerStorageDepositOk(packet.Recog);
                break;
            case ProtocolConsts.SM_SAVEITEM_FULL: // 702: 个人包袱已满 (IDA 0x46B85C)
                _hud?.ShowModalMessage("保管箱提示", "你的个人包袱已经满了，你不能再寄存任何东西了。");
                break;
            case ProtocolConsts.SM_SAVEITEM_FAIL: // 703: 不能寄存 (IDA 0x46B894)
                _hud?.ShowModalMessage("保管箱提示", "你不能寄存。");
                break;
            case ProtocolConsts.SM_TAKEBACKITEM_OK: // 705: 取回物品成功
                _hud?.OnServerStorageWithdrawOk(packet.Recog);
                break;
            case ProtocolConsts.SM_TAKEBACKITEM_FAIL: // 706: 不能取回 (IDA 0x46B8CC)
                _hud?.ShowModalMessage("保管箱提示", "你不能取回。");
                break;
            case ProtocolConsts.SM_TAKEBACKITEM_FULL: // 707: 不能再携带更多东西 (IDA 0x46B8AC)
                _hud?.ShowModalMessage("保管箱提示", "你不能再携带更多东西了.");
                break;
            case ProtocolConsts.SM_STORAGE_SOUND: // 711: 打造/仓储神圣音效 (IDA sub_467920 case 711 play_sound(255))
                AudioManager.Instance?.PlaySoundId(AudioManager.SoundId.LevelUp); // 官方原版 255 音效
                break;
            case ProtocolConsts.SM_MAKEOBJECT_OK: // 713: 打造物品成功并更新金币 (IDA sub_467920 case 713 *(_DWORD *)(dword_4F7EF8 + 88) = v66)
                if (packet.Recog >= 0)
                {
                    _hud?.UpdateGold(packet.Recog);
                }
                _hud?.ShowModalMessage("打造提示", "物品已经被正确打造。"); // 官方原版 0x46B940
                break;
            case ProtocolConsts.SM_MAKEOBJECT_FAIL: // 714: 打造物品失败 (IDA 0x46B960~0x46B988)
                HandleMakeObjectFail(packet);
                break;
            case ProtocolConsts.SM_GUILD_MODALMSG: // 772: 行会通用弹窗提示 (IDA sub_467920 0x46AC5C)
                {
                    string gMsg = !string.IsNullOrEmpty(packet.ExtraStr) ? packet.ExtraStr : DecodeExtraText(packet.Extra);
                    if (!string.IsNullOrEmpty(gMsg))
                    {
                        _hud?.ShowModalMessage("行会通知", gMsg);
                    }
                }
                break;
            case ProtocolConsts.SM_CHARSTATUSCHANGED: // 657: 实体状态位图与动画特效同步 (SM_ENTITY_EFFECT_DELPHI 同号)
                _hud?.AddChat("状态", "[状态] 实体增益/减益状态与攻速已同步。", new Color(0.6f, 1f, 0.8f));
                _entityManager?.PlayEntityEffect(packet.Recog, packet.Tag, packet.Param);
                break;
            case ProtocolConsts.SM_MYSTATUS: // 708: 角色体态/饥饿度变动 (IDA sub_467920 0x4689FE)
                _hud?.UpdateHunger(packet.Recog);
                break;
            case ProtocolConsts.SM_READMINIMAP_OK: // 710: 小地图读取与雷达刷新 (IDA sub_467920 0x46A7F8 sub_46EA50)
                _hud?.AddChat("地图", "[小地图] 地图雷达已校准完成。", new Color(0.6f, 0.9f, 1f));
                break;
            case ProtocolConsts.SM_ITEMHIDE: // 805: 地面物品消失/拾取 (IDA sub_467920 0x4699FE)
                // 盛大官方 1.5 IDA 行 890 case 805 亦为 handle_sm_teleport2() 空间瞬移突变显现
                if (packet.Param > 0 || packet.Tag > 0)
                {
                    HandleTeleport(packet);
                }
                _groundItemManager?.RemoveItem(packet.Recog);
                _hud?.AddChat("物品", $"[地表] 地面物品已清除或被拾取 (ID:{packet.Recog})。", new Color(0.8f, 0.8f, 0.8f));
                break;
            case ProtocolConsts.SM_GUILDJOINFAIL: // 758: 加入行会失败 (IDA sub_467920 0x46A880)
                HandleGuildJoinFail(packet);
                break;
            case ProtocolConsts.SM_GUILDMAKEALLYFAIL2: // 768: 结盟申请失败别名 (IDA sub_467920 0x46AAA7)
                HandleGuildAllyFail(packet);
                break;
            case ProtocolConsts.SM_GUILDBREAKALLYFAIL2: // 770: 解除结盟失败别名 (IDA sub_467920 0x46AB30)
                HandleGuildBreakAllyFail(packet);
                break;
            case ProtocolConsts.SM_DELMAGIC: // 212: 兼容官方 1.5 角色删除确认 (SM_DELCHAR_DELPHI) 与技能移除
                _hud?.AddChat("系统", $"[协议] 协议同步回执 (Ident:{packet.Ident})：{DecodeExtraText(packet.Extra)}", new Color(0.8f, 0.8f, 1f));
                break;
            case SM_OBJECTSPELL:
            case ProtocolConsts.SM_SPELLEFFECT: // 32: 官方 1.5 施法光效前摇与动作 (IDA sub_467920 case ' ')
            case ProtocolConsts.SM_SPELLACTION_DELPHI: // 34: 官方 1.5 实体施法动作 (IDA sub_467920 case '"')
                HandleObjectSpell(packet);
                break;
            case SM_MAGIC_FIRE:
            case SM_LIGHTING_DELPHI:
            case ProtocolConsts.SM_FLYAXE_DELPHI: // 33: 官方 1.5 投掷物飞行弹道 (IDA sub_467920 case '!')
                HandleMagicFire(packet);
                break;
            case ProtocolConsts.SM_SPELLRESULT2: // 29: 官方 1.5 实体受击与施法动作反馈 (IDA ProcessServerMessage case 29 @ 0x4682E0，与 case 30 共享 LABEL_284)
            case SM_SPELLRESULT:
            case SM_OBJECTSTRUCK:
            case SM_OBJECTSTRUCK_DELPHI:
                HandleSpellResult(packet);
                break;
            // 盛大官方 1.5 实体动作族：15 为钻出破土完成 (IDA case 15: *(_BYTE *)(v71 + 220) = 1)，16 为投掷飞斧动作 (IDA case 16)
            // 兼顾多行格式批量包 (ExtraStr 包含换行)
            case ProtocolConsts.SM_FLYAXE_ACTION_DELPHI: // 16
                if (!string.IsNullOrEmpty(packet.ExtraStr) && (packet.ExtraStr.Contains('\n') || packet.ExtraStr.Contains('\r')))
                {
                    HandleBatchMove(packet);
                }
                else
                {
                    _entityManager?.UpdateActorParam(packet.Recog, packet.Param);
                }
                break;
            case ProtocolConsts.SM_DIGUP_ACTION_DELPHI: // 15
                if (!string.IsNullOrEmpty(packet.ExtraStr) && (packet.ExtraStr.Contains('\n') || packet.ExtraStr.Contains('\r')))
                {
                    HandleBatchStruck(packet);
                }
                else
                {
                    _entityManager?.SetEntityDigUpFinished(packet.Recog, true);
                    _entityManager?.UpdateActorParam(packet.Recog, packet.Param);
                }
                break;
            case ProtocolConsts.SM_SERVERLIST: // 520: 选服服务器列表
                SafeLog($"[SM] 接收选服服务器列表 (SM_SERVERLIST=520)");
                break;
            case ProtocolConsts.SM_QUERYCHAR_OK: // 523: 查询角色列表成功进入角色槽位
                SafeLog($"[SM] 查询角色列表成功 (SM_QUERYCHAR_OK=523)");
                break;
            case ProtocolConsts.SM_DELCHAR_FAIL_DELPHI: // 524: 删除角色失败模态弹窗 (IDA 0x46B624)
                _hud?.ShowModalMessage("角色提示", "[Failure] 删除角色失败, Email: support@legendofmir.com.cn");
                break;
            case ProtocolConsts.SM_NEWCHAR_FAIL_DELPHI: // 522: 盛大官方 1.5 创角失败模态弹窗 (IDA case 522 @ 0x46B4C8~0x46B55C)
                {
                    string cMsg = packet.Recog switch
                    {
                        2 => "[failure] 这个名字已经存在。", // 0x46B4C8
                        3 => "[Failure] 你只能为一个帐户设两个角色.\n请和游戏管理员联系。", // 0x46B4F0
                        4 => "[failure] 角色建立失败. Error=4", // 0x46B534
                        _ => "[Failure] 未知的错误. 请写信给: support@legendofmir.net" // 0x46B55C
                    };
                    _hud?.ShowModalMessage("角色提示", cMsg);
                }
                break;
            case ProtocolConsts.SM_SELECTSERVER_OK: // 525: 盛大官方 1.5 选服确认进入选角 (IDA case 525)
                SafeLog($"[SM] 选服确认进入选角 (SM_SELECTSERVER_OK=525)");
                break;
            case ProtocolConsts.SM_REFRESH_DELPHI: // 51: 盛大官方 1.5 实体动画与特效播放 (IDA case '3' @ 0x46A2B0 show_entity_animation)
                _entityManager?.PlayEntityEffect(packet.Recog, packet.Tag, packet.Param);
                SafeLog($"[SM] 实体动画与特效播放 (SM_REFRESH_DELPHI=51, uid={packet.Recog}, effect={packet.Tag}, param={packet.Param})");
                break;

            case ProtocolConsts.SM_QUERYCHAR_FAIL: // 528: 查询角色失败/连接超时模态框 (IDA case 528 @ 0x46B758)
                _hud?.ShowModalMessage("连接提示", "与服务器连接超时，连接已被切断。");
                SafeLog($"[SM] 查询角色失败或连接超时 (SM_QUERYCHAR_FAIL=528)");
                break;
            case ProtocolConsts.SM_VERSION_FAIL: // 501: 补丁版本不符模态弹窗 (IDA 0x46B68C) 或 官方角色复活应答 (SM_REVIVE = 501)
                if (_gameView?.PlayerInstance != null && (_gameView.PlayerInstance.Hp <= 0 || _gameView.HudInstance?.DeathScreen?.Visible == true))
                {
                    int rx = packet.Param > 0 ? packet.Param : 330;
                    int ry = packet.Tag > 0 ? packet.Tag : 270;
                    string extraText = !string.IsNullOrEmpty(packet.ExtraStr) ? packet.ExtraStr : DecodeExtraText(packet.Extra);
                    if (!string.IsNullOrEmpty(extraText))
                    {
                        var parts = extraText.Split(new[] { '/', '\t', ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length >= 2 && int.TryParse(parts[0], out int px) && int.TryParse(parts[1], out int py))
                        {
                            rx = px;
                            ry = py;
                        }
                    }
                    _gameView.OnServerRevive(rx, ry);
                    SafeLog($"[SM] 角色复活安全区传送完成 (SM_REVIVE=501, x={rx}, y={ry})");
                }
                else
                {
                    _hud?.ShowModalMessage("版本提示", "补丁版本不符，请更新客户端。");
                    SafeLog($"[SM] 补丁版本不符 (SM_VERSION_FAIL=501)");
                }
                break;
            case ProtocolConsts.SM_LOGINSUCCESS: // 530: 登录成功 (IDA case 530)
                SafeLog($"[SM] 账号登录成功进入选服 (SM_LOGINSUCCESS=530)");
                break;
            case ProtocolConsts.SM_OBJECTSAY: // 301: 官方实体/玩家视野说话广播 (bridge.rs:120)
                {
                    string rawText = DecodeExtraText(packet.Extra);
                    if (string.IsNullOrEmpty(rawText)) rawText = packet.ExtraStr;
                    if (!string.IsNullOrEmpty(rawText))
                    {
                        var parts = rawText.Split('\t', 2);
                        if (parts.Length == 2)
                        {
                            _hud?.AddChat("附近", $"{parts[0]}: {parts[1]}", Colors.White);
                        }
                        else
                        {
                            _hud?.AddChat("附近", rawText, Colors.White);
                        }
                    }
                    SafeLog($"[SM] 实体说话 (SM_OBJECTSAY=301, uid={packet.Recog})");
                }
                break;
            case ProtocolConsts.SM_WHISPER: // 303: 官方私聊/密语下行通知 (bridge.rs:132)
                {
                    string rawText = DecodeExtraText(packet.Extra);
                    if (string.IsNullOrEmpty(rawText)) rawText = packet.ExtraStr;
                    if (!string.IsNullOrEmpty(rawText))
                    {
                        var parts = rawText.Split('\t', 2);
                        if (parts.Length == 2)
                        {
                            _hud?.AddChat("私聊", $"[{parts[0]}] 悄悄对你说: {parts[1]}", new Color(0.4f, 1.0f, 1.0f));
                        }
                        else
                        {
                            _hud?.AddChat("私聊", rawText, new Color(0.4f, 1.0f, 1.0f));
                        }
                    }
                    SafeLog($"[SM] 私聊下行 (SM_WHISPER=303)");
                }
                break;
            case ProtocolConsts.SM_MONSTERSAY: // 1501: 官方怪物战吼/说话广播 (bridge.rs:349)
                {
                    string rawText = DecodeExtraText(packet.Extra);
                    if (string.IsNullOrEmpty(rawText)) rawText = packet.ExtraStr;
                    if (!string.IsNullOrEmpty(rawText))
                    {
                        var parts = rawText.Split('\t', 2);
                        if (parts.Length == 2)
                        {
                            _hud?.AddChat("怪物", $"【{parts[0]}】: {parts[1]}", new Color(1f, 0.6f, 0.2f));
                        }
                        else
                        {
                            _hud?.AddChat("怪物", rawText, new Color(1f, 0.6f, 0.2f));
                        }
                    }
                    SafeLog($"[SM] 怪物战吼 (SM_MONSTERSAY=1501)");
                }
                break;
            case ProtocolConsts.SM_LOGON_DELPHI: // 50: 主角色进入世界并绑定 (IDA case 50)
                if (_entityManager != null && packet.Recog > 0)
                {
                    _entityManager.LocalPlayerUid = packet.Recog;
                    if (packet.Param > 0 && packet.Tag > 0)
                    {
                        _entityManager.UpdatePosition(packet.Recog, packet.Param, packet.Tag);
                    }
                }
                // 盛大官方 1.5 原版二进制 case 50: byte_4F94F8 = (_BYTE)v64 == 1; (初始组队开关模式同步，v64 为 Extra[8])
                if (packet.Extra != null && packet.Extra.Length >= 12)
                {
                    bool allowGroup = packet.Extra[8] == 1;
                    _hud?.GroupPanel?.SetAllowGroup(allowGroup);
                }
                SafeLog($"[SM] 主角色进入世界绑定 (SM_LOGON_DELPHI=50, uid={packet.Recog})");
                break;
            case ProtocolConsts.SM_STARTPLAY: // 20: 角色确认进入世界 / 实体登场 (IDA case 20 @ 0x4683C0)
                if (_entityManager != null && packet.Recog > 0)
                {
                    _entityManager.LocalPlayerUid = packet.Recog;
                    var hero = _entityManager.GetEntity(packet.Recog);
                    if (hero == null)
                    {
                        // 盛大官方 1.5 IDA 物理指令: if (!v71) v71 = create_entity(v63, v62, SBYTE2(v68), v68);
                        int x = packet.Param > 0 ? (packet.Param & 0xFFFF) : 100;
                        int y = packet.Tag > 0 ? (packet.Tag & 0xFFFF) : 100;
                        byte dir = (byte)((packet.Param >> 16) & 7);
                        _entityManager.AddEntity(packet.Recog, "本地玩家", EntityManager.EntityType.Player, x, y, dir, 0);
                    }
                    else if (packet.Param > 0 && packet.Tag > 0)
                    {
                        _entityManager.UpdatePosition(packet.Recog, packet.Param, packet.Tag);
                    }
                    // 盛大官方 1.5 IDA 物理指令: *(_DWORD *)(v71 + 216) = v64; entity_action(...)
                    _entityManager.UpdateActorParam(packet.Recog, packet.Param);
                }
                SafeLog($"[SM] 进入游戏世界确认 (SM_STARTPLAY=20, uid={packet.Recog})");
                break;
            case 31: // 31: 实体位移平滑移动 (IDA case 31)
                HandleEntityMove(packet);
                break;
            case 8:  // 8: 实体受创硬直动作 (IDA case 8)
            case 18: // 18: 实体特定动作/定向轰击闪电 (IDA case 18)
            case 19: // 19: 实体特定动作/火球 (IDA case 19)
            case 24: // 24: 实体特定动作/施法攻击 (IDA case 24)
                _entityManager?.UpdateActorParam(packet.Recog, packet.Param);
                break;
            case 1:  // 1: 心跳时钟同步 (IDA case 1)
                break;
            case ProtocolConsts.SM_SELECTCHAR_OK_DELPHI: // 531: 盛大官方 1.5 选定角色成功 (IDA case 531 @ 0x468867)
                SafeLog($"[SM] 选定角色成功进入世界 (SM_SELECTCHAR_OK_DELPHI=531)");
                break;
            case ProtocolConsts.SM_ENTITY_ACTION_STRING: // 9: 实体动作动画带字符串广播 (IDA case 9)
                _entityManager?.UpdateActorParam(packet.Recog, packet.Param);
                break;
            case ProtocolConsts.SM_TAKEOFF_FAIL: // 620: 脱下装备失败回滚 (IDA 0x4687C8)
                _hud?.OnTakeOffFail(packet.Param);
                break;
            case ProtocolConsts.SM_SPELLEFFECT_DELPHI: // 614: 官方 1.5 施法光效与受击结算 (IDA sub_467920 case 614 @ 0x469B56: handle_sm_spell_effect)
            case ProtocolConsts.SM_MAGICEFFECT: // 639: 官方 1.5 魔法光效播放 (IDA case 639 handle_sm_magic_effect)
                _entityManager?.PlayEntityEffect(packet.Recog, packet.Tag, packet.Param);
                SafeLog($"[SM] 施法光效/命中特效触发 (ident={packet.Ident}, target={packet.Recog}, effect={packet.Tag}, param={packet.Param})");
                break;
            case ProtocolConsts.SM_DURACHANGE_REC: // 642: 物品持久度变动回显
                _hud?.UpdateItemDurability(packet.Recog);
                break;
            case ProtocolConsts.SM_OPENSHOP_REC: // 643: 打开商店货架
            case ProtocolConsts.SM_MERCHANT_NORMAL: // 646: 打开常规买卖面板
                _hud?.ToggleShop(packet.Recog);
                break;
            case ProtocolConsts.SM_MERCHANT_ITEM: // 645: 商店商品明细条目 (IDA case 645 load_shop_detail)
                {
                    string extraStr = DecodeExtraText(packet.Extra);
                    if (string.IsNullOrEmpty(extraStr)) extraStr = packet.ExtraStr;
                    var items = PacketDecoder.DecodeShopItems(extraStr);
                    if (items.Count > 0)
                    {
                        _hud?.ShopPanel?.SetMode(ShopPanelView.ShopMode.Buy);
                        _hud?.ShopPanel?.SetItems(items);
                        if (_hud?.ShopPanel != null && !_hud.ShopPanel.Visible)
                        {
                            _hud.ToggleShop(packet.Recog);
                        }
                    }
                    _hud?.AddChat("商店", $"[商店] 货架商品更新: {extraStr}", new Color(0.9f, 0.9f, 0.5f));
                }
                break;
            case ProtocolConsts.SM_MERCHANT_SELL_LIST: // 652: 打开回购出售列表 (IDA case 652 load_shop_sell_list)
                {
                    string extraStr = DecodeExtraText(packet.Extra);
                    if (string.IsNullOrEmpty(extraStr)) extraStr = packet.ExtraStr;
                    var items = PacketDecoder.DecodeShopItems(extraStr);
                    _hud?.ShopPanel?.SetMode(ShopPanelView.ShopMode.Sell);
                    if (items.Count > 0)
                    {
                        _hud?.ShopPanel?.SetItems(items);
                    }
                    if (_hud?.ShopPanel != null && !_hud.ShopPanel.Visible)
                    {
                        _hud.ToggleShop(packet.Recog);
                    }
                    _hud?.AddChat("商店", "[商店] 回售列表已加载。", new Color(0.8f, 0.9f, 0.6f));
                }
                break;
            case ProtocolConsts.SM_ACTOR_PARAM: // 654: 实体属性/体态更新
                _hud?.OnActorParam(packet.Recog, packet.Param);
                _entityManager?.UpdateActorParam(packet.Recog, packet.Param);
                break;
            case ProtocolConsts.SM_LAMPCHANGEDURA: // 655: 蜡烛油灯持久消耗
                _hud?.UpdateLampDura(packet.Recog);
                break;
            case ProtocolConsts.SM_KICKMEMBER_OK: // 663: 踢出编组成员成功
                _hud?.OnKickMemberOk();
                break;
            case ProtocolConsts.SM_USERSTORAGE_ITEMS: // 704: 仓库物品列表批量刷新 (IDA case 704 handle_sm_shop_items)
                var rawStorageText = DecodeExtraText(packet.Extra);
                var storageItems = PacketDecoder.DecodeStorageItems(rawStorageText);
                _hud?.OpenStorageList(storageItems);
                _hud?.AddChat("仓库", $"[保管箱] 仓储物品列表已同步刷新 (共 {storageItems.Count} 件)。", new Color(0.6f, 0.8f, 1f));
                break;
            case ProtocolConsts.SM_STORAGE_CLEARITEMS_DELPHI: // 709: 仓库物品清理/清空 (IDA case 709 handle_sm_delete_item)
                _hud?.ClearStorageList();
                _hud?.AddChat("仓库", "[保管箱] 仓储物品列表已清空注销。", new Color(0.9f, 0.5f, 0.5f));
                break;
            case ProtocolConsts.SM_SHOP_DETAIL2: // 712: 商品特价明细下发
                _hud?.AddChat("商店", $"[商店] 物品特价详情: {DecodeExtraText(packet.Extra)}", new Color(0.9f, 0.9f, 0.4f));
                break;
            case ProtocolConsts.SM_TAKEON_NOOP1: // 617
            case ProtocolConsts.SM_TAKEON_NOOP2: // 618
                // 官方 1.5 空占位分支 (goto LABEL_486)
                break;
            case ProtocolConsts.SM_MERCHANT_PAGE: // 647: 盛大官方 1.5 商店货架分页切换 (IDA case 647: v66 > 0 ? NextPage : PrevPage)
                if (packet.Recog > 0)
                {
                    _hud?.ShopPanel?.PageNext();
                }
                else if (packet.Recog < 0)
                {
                    _hud?.ShopPanel?.PagePrev();
                }
                else
                {
                    _hud?.ShopPanel?.SetPage(packet.Param);
                }
                SafeLog($"[SM] 商店货架分页切换 (SM_MERCHANT_PAGE=647, recog={packet.Recog}, param={packet.Param})");
                break;
            case ProtocolConsts.SM_LISTADD_DELPHI: // 610: 名单列表条目添加 (IDA case 0x262)
                _hud?.AddChat("系统", $"[名单] 添加条目: {DecodeExtraText(packet.Extra)}", new Color(0.7f, 0.9f, 0.6f));
                break;
            case ProtocolConsts.SM_LISTDEL_DELPHI: // 611: 名单列表条目删除 (IDA case 0x263)
                _hud?.AddChat("系统", $"[名单] 移除条目 (ID:{packet.Recog})", new Color(0.8f, 0.7f, 0.6f));
                break;
            case ProtocolConsts.SM_MAPMARK_DELPHI: // 612: 盛大官方 1.5 小地图雷达标记 (IDA case 0x264)
                _hud?.SetMapMarker(packet.Tag, packet.Param);
                break;
            case ProtocolConsts.SM_NOTICESOUND_DELPHI: // 613: 官方提示音效 (IDA case 0x265)
                AudioManager.Instance?.PlaySoundId(10);
                break;

            case ProtocolConsts.SM_COMBAT_BROADCAST: // 634: 战斗广播/战斗消息 (IDA case 634)
                _hud?.OnCombatBroadcast($"[战斗] {DecodeExtraText(packet.Extra)}");
                break;
            case ProtocolConsts.SM_COMBAT_END: // 635: 盛大官方 1.5 战斗姿态解除与防抖复位 (IDA case 635: byte_4F9410 = 0)
                _hud?.OnCombatEnd();
                SafeLog("[SM] 战斗姿态已解除 (SM_COMBAT_END=635)");
                break;
            case ProtocolConsts.SM_ACTOR_TIMESTAMP: // 656: 实体动作校验时间戳同步 (IDA case 656)
                _entityManager?.UpdateActorTimestamp(packet.Recog, packet.Param);
                break;
            case ProtocolConsts.SM_ATTR2_DELPHI: // 716: 盛大官方 1.5 扩展属性同步2 (IDA 0x46A382: handle_sm_attr2(Tag))
                _hud?.StatusPanel?.UpdateAbilityValue(11, packet.Tag.ToString());
                SafeLog($"[SM] 扩展属性同步2 (SM_ATTR2_DELPHI=716, val={packet.Tag})");
                break;
            case ProtocolConsts.SM_CHAR_FREEZE_START: // 1100: 实体麻痹/定身开始 (IDA case 0x44C)
                _entityManager?.SetActorFrozen(packet.Recog, true);
                if (packet.Param > 0 && packet.Tag > 0 && _entityManager != null && packet.Recog != _entityManager.LocalPlayerUid)
                {
                    _entityManager.UpdatePosition(packet.Recog, packet.Param, packet.Tag);
                }
                if (_entityManager != null && packet.Recog == _entityManager.LocalPlayerUid)
                {
                    _hud?.SetFrozen(true);
                    _hud?.AddChat("状态", "[状态] 你被麻痹定身了，无法移动或使用物品！", new Color(0.3f, 0.8f, 1f));
                }
                SafeLog($"[SM] 实体 UID={packet.Recog} 进入麻痹/定身状态 (SM_CHAR_FREEZE_START=1100)");
                break;
            case ProtocolConsts.SM_CHAR_FREEZE_END: // 1101: 实体麻痹/定身解除 (IDA case 0x44D)
                _entityManager?.SetActorFrozen(packet.Recog, false);
                if (_entityManager != null && packet.Recog == _entityManager.LocalPlayerUid)
                {
                    _hud?.SetFrozen(false);
                    _hud?.AddChat("状态", "[状态] 麻痹定身状态已解除。", new Color(0.4f, 1f, 0.5f));
                }
                SafeLog($"[SM] 实体 UID={packet.Recog} 解除麻痹/定身状态 (SM_CHAR_FREEZE_END=1101)");
                break;
            case ProtocolConsts.SM_BREAK_WEAPON: // 1102: 武器试刀破碎损毁 (IDA case 0x44E)
                _hud?.OnBreakWeapon();
                break;
            case ProtocolConsts.SM_CHAR_STUN: // 1103: 受到重击击晕硬直 (IDA case 0x44F)
                _entityManager?.SetActorStunned(packet.Recog, 2000);
                if (packet.Param > 0 && packet.Tag > 0)
                {
                    _entityManager?.UpdatePosition(packet.Recog, packet.Param, packet.Tag);
                }
                if (_entityManager != null && packet.Recog == _entityManager.LocalPlayerUid)
                {
                    _hud?.SetStunned(true);
                    _hud?.AddChat("状态", "[状态] 你受到重击陷入击晕硬直！", new Color(1f, 0.4f, 0.2f));
                }
                SafeLog($"[SM] 实体 UID={packet.Recog} 受到击晕硬直 2000ms (SM_CHAR_STUN=1103)");
                break;
            case ProtocolConsts.SM_HEALTHBAR_COLOR_DELPHI: // 1104: 实体血条数值与变色 (IDA case 0x450u @ 0x469B70)
                HandleHealthBarColor(packet);
                break;
            case ProtocolConsts.SM_HEALTHBAR_DELPHI: // 1105: 浮动血条显隐切换 (IDA case 0x451)
                _entityManager?.ToggleFloatingHealthBar(packet.Recog, packet.Param != 0);
                break;
            case ProtocolConsts.SM_ENTITY_POS_DELPHI: // 1106: 官方 1.5 实体坐标校准缓冲 (IDA case 0x452u)
                _entityManager?.UpdatePosition(packet.Recog, packet.Param, packet.Tag);
                break;
            case ProtocolConsts.SM_GUILD_WAR_STATUS_DELPHI: // 1200: 官方 1.5 行会战/攻城战战况广播 (IDA case 0x4B0u)
                HandleGuildWarStatus(packet);
                break;
            case ProtocolConsts.SM_CHANGEPASS_FAIL: // 503: 官方改密/登录失败模态框 (IDA case 503 @ 0x467D59)
            {
                string msg = packet.Recog switch
                {
                    -1 => "密码错误.",
                    -2 => "连续三次密码错误。\n你将在一段时间内无法再次连接。",
                    -3 => "这个帐号正在使用，或者是被异常的终止锁定了，\n请稍后再试。",
                    -4 => "这个帐户不能正确访问。\n请改变帐户，\n或者申请付费注册。",
                    -5 => "这个帐户被禁止了.",
                    _ => "ID不存在或未知错误."
                };
                _hud?.ShowModalMessage("改密提示", msg);
                break;
            }
            case ProtocolConsts.SM_NEWID_SUCCESS: // 504: 官方账号建立成功 (IDA case 504 @ 0x46AFE8)
                _hud?.ShowModalMessage("账号注册", "你的帐号已经建立了.\n请妥善保管你的帐户和密码，\n并且不要因任何原因把它们告诉任何其他人。");
                break;
            case ProtocolConsts.SM_NEWID_FAIL: // 505: 官方账号注册失败 (IDA case 505 @ 0x46B0A8~0x46B12C)
            {
                string msg = packet.Recog switch
                {
                    -2 => "在本服务器下, 这个新名字禁止使用。\n请联系我们。",
                    0 => "这个帐号正在被其他的玩家使用了。",
                    _ => "建立ID失败，请确认它没有包含空格,\n特殊字符，或难以辨认的字符。"
                };
                _hud?.ShowModalMessage("注册失败", msg);
                break;
            }
            case ProtocolConsts.SM_SELECTSERVER_FAIL: // 507: 选服连接失败 (IDA case 507 @ 0x46B5DC)
            {
                string msg = packet.Recog == -2 ? "帐户被锁定 .请稍后再试。" : "您选择的服务器用户满员。";
                _hud?.ShowModalMessage("选服提示", msg);
                break;
            }
            case ProtocolConsts.SM_CHGPASSWD_SUCCESS: // 506: 盛大官方 1.5 修改密码成功 (IDA sub_467920 0x46B59C)
            case ProtocolConsts.SM_CHANGEPASS_OK: // 532: 密码变更成功 (IDA 0x46B59C)
                _hud?.ShowModalMessage("改密提示", "密码变更成功.");
                break;


        }
    }

    /// <summary>
    /// 处理打造/特修失败错误码 (SM_MAKEOBJECT_FAIL 714)
    /// 严格对齐官方 Delphi sub_467920 (0x46A20D)
    /// </summary>
    private void HandleMakeObjectFail(ServerPacket packet)
    {
        string errMsg = packet.Recog switch
        {
            1 => "发生了错误",                     // 0x46B960
            2 => "没有更多物品可以携带了",           // 0x46B8FC (LABEL_364 共享)
            3 => "金币不足.",                      // 0x46B974
            4 => "你缺乏所必需的物品。",             // 0x46B988
            _ => "打造失败。"
        };
        _hud?.ShowModalMessage("打造提示", errMsg);
    }

    /// <summary>

    private void HandleGuildJoinFail(ServerPacket pkt)
    {
        string msg = pkt.Recog switch
        {
            1 => "你没有权利使用这个命令。.",             // 官方 IDA 0x46BB28
            2 => "想加入进来的成员应该来面对掌门人。",     // 官方 IDA 0x46BB4C
            3 => "他已经加入我们的行会。",                 // 官方 IDA 0x46BB78 (nullsub_13)
            4 => "已经加入其他行会。",                     // 官方 IDA 0x46BB98
            5 => "对方不允许加入行会。",                   // 官方 IDA 0x46BBB4
            _ => "加入行会申请失败。"
        };
        _hud?.ShowModalMessage("行会提示", msg);
        _hud?.AddChat("行会", $"[行会] 加入失败：{msg}", new Color(1f, 0.4f, 0.4f));
    }

    private void HandleGuildKickFail(ServerPacket pkt)
    {
        string msg = pkt.Recog switch
        {
            1 => "你没有权利使用这个命令。.",             // 官方 IDA 0x46BB28
            2 => " 不是我们的行会成员。",                 // 官方 IDA 0x46BBD4
            3 => "行会掌门人不能撤消他自己。",             // 官方 IDA 0x46BBF4
            4 => "不能使用命令。",                         // 官方 IDA 0x46BC18
            _ => "踢出行会失败。"
        };
        _hud?.ShowModalMessage("行会提示", msg);
        _hud?.AddChat("行会", $"[行会] 开除成员失败：{msg}", new Color(1f, 0.4f, 0.4f));
    }

    private void HandleGuildRankFail(ServerPacket pkt)
    {
        string msg = pkt.Recog switch
        {
            -2 => "[任命错误]  掌门人位置是空的。",         // 官方 IDA 0x46BC30
            -3 => "[任命错误] 掌门人的职位是空的。",       // 官方 IDA 0x46BC58
            -4 => "[任命错误]一个行会只能有两个掌门人",     // 官方 IDA 0x46BC80
            -5 => "[任命错误] 新的行会掌门人已经被传位。", // 官方 IDA 0x46BCAC
            -6 => "[任命错误] 不能添加成员/删除成员",       // 官方 IDA 0x46BCDC
            -7 => "[任命错误] 职位重复或者出错。",         // 官方 IDA 0x46BD08
            _ => "更改封号失败。"
        };
        _hud?.ShowModalMessage("行会提示", msg);
        _hud?.AddChat("行会", $"[行会] 封号变动失败：{msg}", new Color(1f, 0.4f, 0.4f));
    }

    private void HandleGuildAllyFail(ServerPacket pkt)
    {
        string msg = pkt.Recog switch
        {
            -1 => "你没有权利。",                          // 官方 IDA 0x46BD30
            -2 => "联盟失败。",                            // 官方 IDA 0x46BD48
            -3 => "你应该面对你想要结盟的行会掌门人。",     // 官方 IDA 0x46BD5C
            -4 => "对方行会掌门人不允许结盟。",             // 官方 IDA 0x46BD88
            _ => "行会结盟申请失败。"
        };
        _hud?.ShowModalMessage("行会提示", msg);
        _hud?.AddChat("行会", $"[行会] 结盟失败：{msg}", new Color(1f, 0.4f, 0.4f));
    }

    private void HandleGuildBreakAllyFail(ServerPacket pkt)
    {
        string msg = pkt.Recog switch
        {
            -1 => "你没有权利。",                          // 官方 IDA 0x46BD30
            -2 => "你不在行会联盟中。",                     // 官方 IDA 0x46BDAC
            -3 => "这不是一个现有的行会。",                 // 官方 IDA 0x46BDC8
            _ => "解除结盟失败。"
        };
        _hud?.ShowModalMessage("行会提示", msg);
        _hud?.AddChat("行会", $"[行会] 解盟失败：{msg}", new Color(1f, 0.4f, 0.4f));
    }

    // 盛大官方 1.5 金币变动协议 (IDA ProcessServerMessage case 40 @ 0x46A140 与 case 653 @ 0x469C80)
    // 物理指令: *(_DWORD *)(dword_4F7EF8 + 88) = v66; play_sound(106);
    private void HandleGoldChanged(ServerPacket pkt)
    {
        int gold = pkt.Recog;
        if (gold == 0 && !string.IsNullOrEmpty(pkt.ExtraStr) && int.TryParse(pkt.ExtraStr.Trim(), out var g))
        {
            gold = g;
        }
        _hud?.UpdateGold(gold);
        AudioManager.Instance?.PlaySoundId(AudioManager.SoundId.Money); // 对齐 Delphi ClMain.pas:5532 PlaySound(s_money)
    }

    // 盛大官方 1.5 角色升级协议 (IDA ProcessServerMessage case 44 @ 0x46A18D)
    // 物理指令: *(_DWORD *)(dword_4F7EF8 + 72) = v66; (Recog 为等级 Level); play_sound(255);
    private void HandleLevelUp(ServerPacket pkt)
    {
        int level = pkt.Recog > 0 ? pkt.Recog : pkt.Param;
        if (level > 0)
        {
            _hud?.UpdateLevel(level);
            _hud?.AddChat("系统", $"[升级] 恭喜你升到了 {level} 级！", new Color(1f, 0.9f, 0.2f));
            AudioManager.Instance?.PlaySoundId(AudioManager.SoundId.LevelUp);
        }
    }

    private void HandleExperience(ServerPacket pkt)
    {
        long curExp = (uint)pkt.Recog;
        long maxExp = pkt.Param > 0 ? (uint)pkt.Param : 100000;
        _hud?.UpdateExp(curExp, maxExp);
        AudioManager.Instance?.PlaySoundId(AudioManager.SoundId.Notice); // 盛大官方 1.5 IDA case '-' @ 0x46A1B0: play_notice_sound()
    }

    // 盛大官方 1.5 生命与魔法值变动协议 (IDA ProcessServerMessage case 53 '5' @ 0x46A275)
    // 物理指令: v71 = get_actor_by_id(); *(_WORD *)(v71 + 60) = HIWORD(v67); *(_DWORD *)(v71 + 62) = v68;
    private void HandleHealthSpellChanged(ServerPacket pkt)
    {
        // 1. 若带有 ExtraStr（格式: "hp/maxhp/mp/maxmp" 或 "hp/maxhp"），为主角全量状态同步
        if (!string.IsNullOrEmpty(pkt.ExtraStr))
        {
            var parts = pkt.ExtraStr.Split(new[] { '/', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2 && int.TryParse(parts[0], out var eHp) && int.TryParse(parts[1], out var eMaxHp))
            {
                int eMp = parts.Length >= 4 && int.TryParse(parts[2], out var mpVal) ? mpVal : 0;
                int eMaxMp = parts.Length >= 4 && int.TryParse(parts[3], out var maxMpVal) ? maxMpVal : Math.Max(eMp, 1);
                _hud?.UpdateHp(eHp, eMaxHp);
                if (parts.Length >= 4) _hud?.UpdateMp(eMp, eMaxMp);
                var hero = _entityManager?.Hero;
                if (hero != null) _entityManager?.UpdateHp(hero.Uid, eHp, eMaxHp);
                return;
            }
        }

        // 2. 盛大官方 1.5 原版二进制 (IDA case '5' 53 @ 0x46A275):
        // v71 = get_actor_by_id(); (Recog 为实体 UID, Param 为当前 HP, Tag 为最大 MaxHP)
        int uid = pkt.Recog;
        var targetEntity = _entityManager?.GetEntity(uid);

        if (targetEntity != null)
        {
            int hp = pkt.Param;
            int maxHp = pkt.Tag > 0 ? pkt.Tag : Math.Max(hp, 1);
            _entityManager?.UpdateHp(uid, hp, maxHp);

            // 若更新的目标实体即为主角色，同步驱动 HUD 血球
            if (_entityManager != null && uid == _entityManager.LocalPlayerUid)
            {
                _hud?.UpdateHp(hp, maxHp);
            }
        }
        else if (uid == 0 || (_entityManager != null && uid == _entityManager.LocalPlayerUid))
        {
            // 主角 UID 为 0 或未明确指定时的缺省主角更新
            int hp = pkt.Param;
            int maxHp = pkt.Tag > 0 ? pkt.Tag : Math.Max(hp, 1);
            _hud?.UpdateHp(hp, maxHp);
            var hero = _entityManager?.Hero;
            if (hero != null) _entityManager?.UpdateHp(hero.Uid, hp, maxHp);
        }
        else
        {
            // 兼容非标老旧协议格式 (Recog=HP, Param=MaxHP, Tag=MP)
            int hp = pkt.Recog;
            int maxHp = pkt.Param > 0 ? pkt.Param : Math.Max(hp, 1);
            int mp = pkt.Tag;
            int maxMp = Math.Max(mp, 1);
            _hud?.UpdateHp(hp, maxHp);
            _hud?.UpdateMp(mp, maxMp);
            var hero = _entityManager?.Hero;
            if (hero != null) _entityManager?.UpdateHp(hero.Uid, hp, maxHp);
        }
    }

    // 盛大官方 1.5 负重变动协议 (IDA sub_467920 case 622 @ 0x46A7A0)
    // 物理指令: *(_WORD *)(+80)=v66 (Weight); *(_BYTE *)(+84)=BYTE2(v67) (WearWeight); *(_BYTE *)(+86)=v68 (MaxWeight);
    private void HandleWeightChanged(ServerPacket pkt)
    {
        int curWeight = pkt.Recog;
        int wearWeight = pkt.Param;
        int maxWeight = pkt.Tag > 0 ? pkt.Tag : (pkt.Param > 0 ? pkt.Param : 50);
        _hud?.UpdateWeight(curWeight, maxWeight);

        var sp = _hud?.StatusPanel;
        if (sp != null)
        {
            sp.UpdateAbilityValue(1, $"{curWeight}/{maxWeight}", curWeight > maxWeight);
            if (wearWeight > 0)
            {
                sp.UpdateAbilityValue(2, $"{wearWeight}");
            }
        }

        // 若携带制表符 6 字段扩展 (weight, max_weight, wear_weight, max_wear_weight, hand_weight, max_hand_weight)
        // 自动驱动状态面板 (StatusPanelView) 能力页第 1 (背包重)、第 2 (负重)、第 3 (腕力)
        if (!string.IsNullOrEmpty(pkt.ExtraStr) && pkt.ExtraStr.Contains('\t'))
        {
            var parts = pkt.ExtraStr.Split('\t');
            if (parts.Length >= 6 && sp != null)
            {
                sp.UpdateAbilityValue(1, $"{parts[0]}/{parts[1]}", int.TryParse(parts[0], out var w) && int.TryParse(parts[1], out var mw) && w > mw);
                sp.UpdateAbilityValue(2, $"{parts[2]}/{parts[3]}", int.TryParse(parts[2], out var ww) && int.TryParse(parts[3], out var mww) && ww > mww);
                sp.UpdateAbilityValue(3, $"{parts[4]}/{parts[5]}", int.TryParse(parts[4], out var hw) && int.TryParse(parts[5], out var mhw) && hw > mhw);
            }
        }
    }

    // 盛大官方 1.5 全局昼夜与火把光照协议 (IDA ProcessServerMessage case '.' 46 @ 0x46A1B8)
    // 物理指令: dword_4F9384 = HIWORD(v67); (Param: 昼夜状态); *off_4A4144 = v68; (Tag: 光照半径); byte_4F9382 = *off_4A4144 != 0;
    private void HandleChangeLight(ServerPacket pkt)
    {
        int dayNight = pkt.Param;
        int state = dayNight switch
        {
            <= 0 => 2, // 黑夜
            1 => 1,    // 黄昏
            2 => 3,    // 清晨
            _ => 0     // 正午白天
        };
        _hud?.SetDayNightState(state);
        bool isDark = state == 1 || state == 2;
        int radius = pkt.Tag > 0 ? pkt.Tag : (isDark ? 6 : 15);
        _hud?.UpdateVisionFog(isDark, radius, new Godot.Vector2(400, 225));
    }

    private void HandleAddItem(ServerPacket pkt)
    {
        int slot = pkt.Param >= 0 && pkt.Param < 40 ? pkt.Param : (pkt.Recog >= 0 && pkt.Recog < 40 ? pkt.Recog : 0);
        string itemName = "新物品";
        string iconFile = "StateItem_0001.png";
        string desc = "[color=#c0c0c0]重量 1   持久力 10/10[/color]";

        string rawText = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(rawText)) rawText = pkt.ExtraStr;

        if (!string.IsNullOrEmpty(rawText))
        {
            if (rawText.Contains('\t'))
            {
                // 服务端网关 bridge.rs 标准格式: name\tdura\tdura_max\titem_index
                var parts = rawText.Split('\t');
                if (parts.Length >= 1 && !string.IsNullOrEmpty(parts[0])) itemName = parts[0];
                int dura = parts.Length >= 2 && int.TryParse(parts[1], out var d) ? d : 10;
                int duraMax = parts.Length >= 3 && int.TryParse(parts[2], out var dm) ? dm : 10;
                int itemIndex = parts.Length >= 4 && int.TryParse(parts[3], out var idx) ? idx : 1;
                iconFile = $"StateItem_{itemIndex:D4}.png";
                desc = $"[color=#c0c0c0]重量 1   持久力 {dura}/{duraMax}[/color]";
            }
            else
            {
                // 兼容斜杠或竖线分割: name/icon/desc
                var parts = rawText.Split(new[] { '/', '|' }, StringSplitOptions.None);
                if (parts.Length >= 1 && !string.IsNullOrEmpty(parts[0])) itemName = parts[0];
                if (parts.Length >= 2 && !string.IsNullOrEmpty(parts[1])) iconFile = parts[1];
                if (parts.Length >= 3 && !string.IsNullOrEmpty(parts[2])) desc = parts[2];
            }
        }

        _hud?.SetBagItem(slot, itemName, iconFile, desc);
    }

    private void HandleMineResult(ServerPacket pkt)
    {
        string rawText = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(rawText)) rawText = pkt.ExtraStr;
        string oreName = !string.IsNullOrEmpty(rawText) ? rawText : "矿石";
        int purity = pkt.Recog > 0 ? pkt.Recog : (pkt.Param > 0 ? pkt.Param : 10);
        _hud?.AddChat("采矿", $"[采矿] 挖矿获得：{oreName} (纯度 {purity})", new Color(0.85f, 0.75f, 0.5f));
    }

    private void HandleDelItem(ServerPacket pkt)
    {
        int slot = pkt.Param >= 0 && pkt.Param < 40 ? pkt.Param : pkt.Recog;
        if (slot >= 0 && slot < 40)
        {
            _hud?.ClearBagItem(slot);
        }
    }

    private void HandleSendUserMagic(ServerPacket pkt)
    {
        if (string.IsNullOrEmpty(pkt.ExtraStr)) return;

        var skills = new List<SkillPanelView.SkillItemData>();
        var lines = pkt.ExtraStr.Split(new[] { '\r', '\n', ';' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var parts = line.Split(new[] { '/', '|' }, StringSplitOptions.None);
            if (parts.Length >= 2)
            {
                int magicId = int.TryParse(parts[0], out var mid) ? mid : 1;
                string name = parts[1];
                int iconIdx = parts.Length > 2 && int.TryParse(parts[2], out var icon) ? icon : 0;
                string key = parts.Length > 3 ? parts[3] : "";
                int level = parts.Length > 4 && int.TryParse(parts[4], out var lvl) ? lvl : 0;
                int curTrain = parts.Length > 5 && int.TryParse(parts[5], out var cur) ? cur : 0;
                int maxTrain = parts.Length > 6 && int.TryParse(parts[6], out var max) ? max : 1000;
                string desc = parts.Length > 7 ? parts[7] : $"{name}，修炼等级 {level}";
                skills.Add(new SkillPanelView.SkillItemData(magicId, name, iconIdx, key, level, curTrain, maxTrain, desc));
            }
        }

        if (skills.Count > 0 && _hud?.SkillPanel != null)
        {
            _hud?.SkillPanel.SetSkills(skills);
        }
    }

    /// <summary>
    /// 处理服务端移动允许/位移校准 (SM_ALLOWMOVE 203)
    /// Recog 编码 (x & 0xFFFF) | ((y & 0xFFFF) << 16)，Param 为朝向 (0~7)
    /// </summary>
    private void HandleAllowMove(ServerPacket pkt)
    {
        int x = pkt.Recog & 0xFFFF;
        int y = (pkt.Recog >> 16) & 0xFFFF;
        int dir = pkt.Param & 7;

        if (_gameView?.PlayerInstance != null && x > 0 && y > 0)
        {
            int curX = _gameView.PlayerInstance.TileX;
            int curY = _gameView.PlayerInstance.TileY;
            int dist = Math.Max(Math.Abs(curX - x), Math.Abs(curY - y));
            if (dist > 2)
            {
                // 客户端与服务端判定偏差过大（反外挂拉回或地图坐标突变），强制校准坐标
                _gameView.PlayerInstance.SetTilePos(x, y);
                _gameView.PlayerInstance.Direction = dir;
                _gameView.UpdateEnvironmentLighting();
            }
            else
            {
                _gameView.PlayerInstance.Direction = dir;
            }
        }
        else if (_gameView?.PlayerInstance != null && pkt.Param >= 0 && pkt.Param <= 7)
        {
            _gameView.PlayerInstance.Direction = dir;
        }

        // 多态兼顾：若包含 Extra 文本或者 Tag > 0 且非坐标，作为技能经验更新处理
        if (!string.IsNullOrEmpty(pkt.ExtraStr) || (pkt.Tag > 0 && x == 0 && y == 0))
        {
            HandleUpdateUserMagic(pkt);
        }
    }

    private void HandleUpdateUserMagic(ServerPacket pkt)
    {
        int magicId = pkt.Param;
        int level = pkt.Tag;
        int curTrain = pkt.Recog;
        int maxTrain = 1000;
        if (!string.IsNullOrEmpty(pkt.ExtraStr) && int.TryParse(pkt.ExtraStr.Trim(), out var max))
        {
            maxTrain = max;
        }
        _hud?.SkillPanel?.UpdateSkillExp(magicId, level, curTrain, maxTrain);
    }

    private void HandleEquipInfo(ServerPacket pkt)
    {
        if (string.IsNullOrEmpty(pkt.ExtraStr)) return;
        var parts = pkt.ExtraStr.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < parts.Length && i < 9; i++)
        {
            var itemParts = parts[i].Split(new[] { '/', '|' }, StringSplitOptions.None);
            if (itemParts.Length >= 1 && int.TryParse(itemParts[0], out var itemId) && itemId > 0)
            {
                string name = itemParts.Length > 1 ? itemParts[1] : $"装备 #{itemId}";
                string icon = $"StateItem_{itemId:D4}.png";
                string desc = itemParts.Length > 2 ? itemParts[2] : "[color=#ffff00]官方实证装备[/color]";
                _hud?.SetEquipSlot(i, name, icon, desc);
            }
            else
            {
                _hud?.SetEquipSlot(i, null, null, null);
            }
        }
    }

    private void HandleTakeOnOk(ServerPacket pkt)
    {
        int slot = pkt.Param >= 0 && pkt.Param < 9 ? pkt.Param : pkt.Recog;
        string name = "已装备";
        string icon = $"StateItem_{slot:D4}.png";
        string desc = "[color=#ffff00]穿戴装备成功[/color]";
        if (!string.IsNullOrEmpty(pkt.ExtraStr))
        {
            var parts = pkt.ExtraStr.Split(new[] { '/', '|' }, StringSplitOptions.None);
            if (parts.Length >= 1) name = parts[0];
            if (parts.Length >= 2) icon = parts[1];
            if (parts.Length >= 3) desc = parts[2];
        }
        _hud?.SetEquipSlot(slot, name, icon, desc);
    }

    private void HandleTakeOffOk(ServerPacket pkt)
    {
        int slot = pkt.Param >= 0 && pkt.Param < 9 ? pkt.Param : pkt.Recog;
        _hud?.SetEquipSlot(slot, null, null, null);
    }

    private void HandleNpcDialog(ServerPacket pkt)
    {
        string text = pkt.ExtraStr;
        if (string.IsNullOrEmpty(text) && pkt.Extra.Length > 0)
        {
            text = Encoding.UTF8.GetString(pkt.Extra);
        }
        if (!string.IsNullOrEmpty(text))
        {
            if (_hud?.NpcDialog != null) _hud.NpcDialog.CurrentNpcUid = pkt.Recog;
            _hud?.ShowNpcDialog(text);
        }
    }

    private void HandleMerchantDlg(ServerPacket pkt)
    {
        if (string.IsNullOrEmpty(pkt.ExtraStr))
        {
            _hud?.ToggleShop();
            return;
        }

        var lines = pkt.ExtraStr.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var items = new List<ShopPanelView.ShopItemEntry>();
        string npcName = "商人";

        foreach (var line in lines)
        {
            var parts = line.Split(new[] { '\t', '/' }, StringSplitOptions.None);
            if (parts.Length >= 3)
            {
                string name = parts[0];
                int weight = int.TryParse(parts[1], out var w) ? w : 1;
                int price = int.TryParse(parts[2], out var p) ? p : 100;
                items.Add(new ShopPanelView.ShopItemEntry(name, weight, price));
            }
            else if (parts.Length == 1 && line.StartsWith("#"))
            {
                npcName = line.TrimStart('#');
            }
        }

        _hud?.ShowShop(npcName, items);
    }

    // 仓库物品列表：复用商店行解析（名/重量/价格，缺省补 1/0），L2。
    private void HandleStorageItems(ServerPacket pkt)
    {
        if (string.IsNullOrEmpty(pkt.ExtraStr))
        {
            _hud?.ToggleStorage();
            return;
        }
        var lines = pkt.ExtraStr.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var items = new List<ShopPanelView.ShopItemEntry>();
        foreach (var line in lines)
        {
            var parts = line.Split(new[] { '\t', '/' }, StringSplitOptions.None);
            if (parts.Length >= 3)
            {
                items.Add(new ShopPanelView.ShopItemEntry(parts[0],
                    int.TryParse(parts[1], out var w) ? w : 1,
                    int.TryParse(parts[2], out var p) ? p : 0));
            }
            else if (parts.Length >= 1 && !string.IsNullOrWhiteSpace(parts[0]))
            {
                items.Add(new ShopPanelView.ShopItemEntry(parts[0].Trim(), 1, 0));
            }
        }
        _hud?.OpenStorageList(items);
    }

    private void HandleOpenGuildDlg(ServerPacket pkt)
    {
        string text = pkt.ExtraStr;
        if (!string.IsNullOrEmpty(text) && _hud?.GuildPanel != null)
        {
            _hud.GuildPanel.UpdateHomeNotice(text);
        }
        _hud?.ToggleGuild();
    }

    private void HandleSendUserState(ServerPacket pkt)
    {
        // 盛大官方 1.5 IDA 物理指令 (ProcessServerMessage case '4' 52 @ 0x46A250):
        // *(_DWORD *)(dword_4F7EF8 + 88) = v66; (Recog 权威实证为 Gold 金币)
        // *(_BYTE *)(dword_4F7EF8 + 18) = BYTE2(v67);
        // GetBuffer(40, dword_4F7EF8 + 48); refresh_char_ui(dword_4A2D10);
        if (pkt.Ident == SM_SENDUSERSTATE && pkt.Recog >= 0)
        {
            _hud?.UpdateGold(pkt.Recog);
            _hud?.StatusPanel?.UpdateStat("Gold", pkt.Recog.ToString());
        }

        string text = pkt.ExtraStr;
        if (string.IsNullOrEmpty(text)) return;

        var baseParts = text.Split('|');
        var infoParts = baseParts[0].Split('/');
        if (infoParts.Length >= 1)
        {
            string name = infoParts[0];
            int level = infoParts.Length > 1 && int.TryParse(infoParts[1], out var l) ? l : 1;
            int job = infoParts.Length > 2 && int.TryParse(infoParts[2], out var j) ? j : 0;
            int gender = infoParts.Length > 3 && int.TryParse(infoParts[3], out var g) ? g : 0;

            // 1. 若为 751 (SM_SENDUSERSTATE_INSPECT 观人下行)，驱动 InspectPanel
            if (pkt.Ident == SM_SENDUSERSTATE_INSPECT)
            {
                var inspect = _hud?.InspectPanel;
                if (inspect != null)
                {
                    inspect.SetGender(gender);
                    _hud?.OpenInspect(name);

                    if (baseParts.Length > 1)
                    {
                        var slotItems = baseParts[1].Split(',');
                        for (int i = 0; i < slotItems.Length && i < 9; i++)
                        {
                            if (int.TryParse(slotItems[i], out var itemIdx) && itemIdx > 0)
                            {
                                inspect.SetSlotItem(i, $"StateItem_{itemIdx:D4}.png", $"[color=#ffff40]装备 #{itemIdx}[/color]");
                            }
                        }
                    }
                }
            }
            // 2. 若为 52 (SM_SENDUSERSTATE 主角属性下行)，驱动主角自己的 StatusPanel 与 HUD
            else
            {
                _hud?.UpdateName(name);
                _hud?.UpdateLevel(level);

                var sp = _hud?.StatusPanel;
                if (sp != null)
                {
                    sp.UpdateStat("Name", name);
                    sp.UpdateStat("Level", level.ToString());
                    sp.UpdateStat("Job", job switch { 0 => "战士", 1 => "法师", 2 => "道士", _ => "战士" });
                    sp.UpdateStat("Gold", pkt.Recog.ToString());

                    if (baseParts.Length > 1)
                    {
                        var tokens = baseParts[1].Split(new[] { '/', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        // 驱动能力页第 4~10 行属性 (准确、敏捷、魔御、中毒防御、中毒恢复、体力恢复、魔法恢复)
                        for (int i = 0; i < tokens.Length && (4 + i) <= 10; i++)
                        {
                            sp.UpdateAbilityValue(4 + i, tokens[i].Trim());
                        }
                    }
                }
            }
        }
    }

    private void HandleSendGuildMemberList(ServerPacket pkt)
    {
        string text = pkt.ExtraStr;
        if (string.IsNullOrEmpty(text)) return;

        var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var members = new List<(string role, string name, string job, int level, bool online)>();

        foreach (var line in lines)
        {
            var parts = line.Split(new[] { '\t', '/' }, StringSplitOptions.None);
            // bridge 行会名单行为 4 段：名\t封号\t等级\t在线(1/0)（bridge 2172），recog=人数；
            // 旧 5 段（身份\t名\t职业\t等级\t在线）保留兼容。
            if (parts.Length == 4)
            {
                string name = parts[0];
                string role = parts[1];
                int level = int.TryParse(parts[2], out var lvl) ? lvl : 1;
                bool online = parts[3] == "1" || parts[3] == "true" || parts[3] == "在线";
                members.Add((role, name, "战士", level, online));
            }
            else if (parts.Length >= 2)
            {
                string role = parts[0];
                string name = parts[1];
                string job = parts.Length > 2 ? parts[2] : "战士";
                int level = parts.Length > 3 && int.TryParse(parts[3], out var lvl) ? lvl : 1;
                bool online = parts.Length <= 4 || parts[4] == "1" || parts[4] == "true" || parts[4] == "在线";
                members.Add((role, name, job, level, online));
            }
        }

        if (_hud?.GuildPanel != null)
        {
            _hud.GuildPanel.SetMemberList(members);
            _hud.GuildPanel.ShowMemberList();
            if (!_hud.GuildPanel.Visible)
            {
                _hud.ToggleGuild();
            }
        }
    }

    // 官方 1.5 行会创建结果回显 (IDA ProcessServerMessage 0x467920 case 762/763 @ 0x46A8DE)
    private void HandleBuildGuildResult(ServerPacket pkt)
    {
        bool ok = pkt.Ident == ProtocolConsts.SM_BUILDGUILDOK;
        string text = DecodeExtraText(pkt.Extra);
        if (ok)
        {
            if (string.IsNullOrEmpty(text)) text = "行会正确建立。"; // 官方 IDA 0x46BDE8 "行会正确建立。" 模态弹窗
            _hud?.AddChat("行会", $"[行会] {text}", new Color(0.4f, 1f, 0.5f));
            _hud?.ShowModalMessage("行会提示", text);
            _hud?.ToggleGuild();
        }
        else
        {
            // 官方 IDA 0x467920 case 763 (@ 0x46A8DE): 根据 Recog 错误码弹出精确模态提示
            string failMsg = pkt.Recog switch
            {
                -1 => "已经加入行会。",                     // 官方 IDA 0x46BE00
                -2 => "缺少创建费用。",                     // 官方 IDA 0x46BE18
                -3 => "你没有准备好需要的全部物品。",         // 官方 IDA 0x46BE30
                _ => !string.IsNullOrEmpty(text) ? text : "行会创建失败！"
            };
            _hud?.AddChat("行会", $"[行会] {failMsg}", new Color(1f, 0.4f, 0.4f));
            _hud?.ShowModalMessage("行会提示", failMsg);
        }
    }

    private void HandleGuildMemberChange(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        bool isFail = pkt.Ident == ProtocolConsts.SM_GUILDADDMEMBERFAIL || pkt.Ident == ProtocolConsts.SM_GUILDDELMEMBERFAIL;
        string msg = pkt.Ident switch
        {
            var i when i == ProtocolConsts.SM_GUILDADDMEMBEROK => $"已吸纳成员：{text}",
            var i when i == ProtocolConsts.SM_GUILDADDMEMBERFAIL => $"吸纳成员失败：{text}",
            var i when i == ProtocolConsts.SM_GUILDDELMEMBEROK => $"已开除成员：{text}",
            _ => $"成员变更失败：{text}",
        };
        _hud?.AddChat("行会", $"[行会] {msg}", new Color(1f, 0.9f, 0.5f));
        if (isFail) _hud?.ShowModalMessage("行会提示", msg);
    }

    private void HandleGuildDiplomacy(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        string msg = pkt.Ident switch
        {
            var i when i == ProtocolConsts.SM_GUILDMAKEALLYFAIL => $"结盟失败：{text}",
            _ => $"解除联盟失败：{text}",
        };
        _hud?.AddChat("行会", $"[行会] {msg}", new Color(0.6f, 0.85f, 1f));
        _hud?.ShowModalMessage("行会提示", msg);
    }

    private void HandleGuildRankResult(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        string msg = $"调整封号失败：{text}";
        _hud?.AddChat("行会", $"[行会] {msg}", new Color(1f, 0.5f, 0.5f));
        _hud?.ShowModalMessage("行会提示", msg);
    }

    // 组队名单：bridge 纯名换行串（首行为队长），对齐 GroupPanel.SetMembers(leader, members)。
    private void HandleGroupMembers(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        if (string.IsNullOrEmpty(text) || _hud?.GroupPanel == null) return;
        var names = new List<string>(text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries));
        for (int i = 0; i < names.Count; i++) names[i] = names[i].Trim();
        names.RemoveAll(string.IsNullOrEmpty);
        if (names.Count == 0) return;
        _hud.GroupPanel.SetMembers(names[0], names.Count > 1 ? names.GetRange(1, names.Count - 1) : null);
        if (!_hud.GroupPanel.Visible) _hud.ToggleGroup();
    }

    // 注：764 双义（行会公告 text / 沙城捐献结果 DONATE_OK），网关同号，内容直显，面板更新保持。
    // 捐献成功 extra 为空、recog=金额：此时不刷行会公告面板，只聊天。
    private void HandleGuildMessage(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        if (string.IsNullOrEmpty(text))
        {
            if (pkt.Recog > 0)
                _hud?.AddChat("沙城", $"[捐献] 行会捐献成功：{pkt.Recog:N0} 金币。", new Color(1f, 0.85f, 0.4f));
            return;
        }
        if (_hud?.GuildPanel != null) _hud.GuildPanel.UpdateHomeNotice(text);
        _hud?.AddChat("行会", $"[行会公告] {text}", new Color(1f, 0.85f, 0.4f));
    }

    // 沙城捐献结果（767/768，extra=原因/提示）。
    private void HandleDonateResult(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        bool ok = pkt.Ident == ProtocolConsts.SM_DONATEOK;
        if (string.IsNullOrEmpty(text)) text = ok ? "捐献金币成功！" : "捐献失败";
        var col = ok ? new Color(0.4f, 1f, 0.4f) : new Color(1f, 0.5f, 0.5f);
        _hud?.AddChat("沙城", $"[捐献] {text}", col);
        if (!ok) _hud?.ShowModalMessage("沙城提示", text);
    }

    // 官方 1.5 求助/全服叫喊广播 (IDA ProcessServerMessage case 765)
    private void HandleCry(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        if (!string.IsNullOrEmpty(text))
        {
            _hud?.AddChat("求助", $"[求助] {text}", new Color(1f, 0.4f, 0.4f));
        }
    }

    // 官方 1.5 组队模式开关变动与即时治疗回显指示 (IDA ProcessServerMessage case 659 @ 0x469F54: byte_4F94F8 = HIWORD(v67) != 0)
    private void HandleInstanceHealGauge(ServerPacket pkt)
    {
        // 盛大官方 1.5 原版二进制中 case 659: byte_4F94F8 = HIWORD(v67) != 0; (即 AllowGroup 组队开关由 Param 驱动)
        bool allowGroup = pkt.Param != 0;
        _hud?.GroupPanel?.SetAllowGroup(allowGroup);
        _hud?.AddChat("组队", allowGroup ? "[组队] 已开启允许组队模式。" : "[组队] 已关闭允许组队模式。", new Color(0.4f, 1f, 0.5f));

        // 兼容血球即时治疗变体 (Param 作为治疗增量)
        int healAmount = pkt.Param;
        if (healAmount > 0)
        {
            _hud?.ApplyHeal(healAmount);
            _hud?.AddChat("提示", $"[治愈] 恢复生命值 +{healAmount} 点。", new Color(0.4f, 1f, 0.4f));
        }
    }

    // 官方 1.5 空间传送隐去与现身联动 (IDA ProcessServerMessage case 800 / 801)
    private void HandleSpaceMove(ServerPacket pkt, bool isShow)
    {
        if (_entityManager == null) return;
        if (isShow)
        {
            var ent = _entityManager.GetEntity(pkt.Recog);
            if (ent == null)
            {
                _entityManager.AddEntity(pkt.Recog, string.Empty,
                    Mir2Client.Game.EntityManager.EntityType.Player, pkt.Param, pkt.Tag, 2, 0);
            }
            else
            {
                _entityManager.UpdateMove(pkt.Recog, pkt.Param, pkt.Tag, ent.Direction);
            }
            SafeLog($"[SM] 空间传送现身 uid={pkt.Recog} pos=({pkt.Param},{pkt.Tag})");
        }
        else
        {
            _entityManager.RemoveEntity(pkt.Recog);
            SafeLog($"[SM] 空间传送隐去 uid={pkt.Recog}");
        }
    }

    /// <summary>
    /// 处理空间坐标瞬移突变 (SM_TELEPORT 804)
    /// 严格对齐官方 Delphi sub_467920 (0x468A94 handle_sm_teleport)
    /// </summary>
    private void HandleTeleport(ServerPacket pkt)
    {
        int x = pkt.Param;
        int y = pkt.Tag;
        int uid = pkt.Recog;

        if (_gameView?.PlayerInstance != null && (uid == 0 || uid == _gameView.PlayerInstance.Uid))
        {
            _gameView.PlayerInstance.SetTilePos(x, y);
            _gameView.UpdateEnvironmentLighting();
            SafeLog($"[SM] 主角瞬移至 ({x}, {y})");
        }
        else if (_entityManager != null && uid > 0)
        {
            var ent = _entityManager.GetEntity(uid);
            if (ent != null)
            {
                _entityManager.UpdateMove(uid, x, y, ent.Direction);
                SafeLog($"[SM] 实体 uid={uid} 瞬移至 ({x}, {y})");
            }
        }
    }

    // 官方 1.5 穿戴装备失败提示与解锁 (IDA ProcessServerMessage case 616 @ 0x469B60 sub_49F874)
    private void HandleTakeOnFail(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        if (string.IsNullOrEmpty(text)) text = "穿戴装备失败，可能由于等级、职业要求或负重不足。";
        _hud?.AddChat("提示", $"[系统] {text}", new Color(1f, 0.4f, 0.4f));
        int slot = pkt.Param >= 0 && pkt.Param < 9 ? pkt.Param : (pkt.Recog >= 0 && pkt.Recog < 9 ? pkt.Recog : -1);
        _hud?.OnTakeOnFail(slot);
    }

    // 官方 1.5 技能经验与熟练度更新 (IDA ProcessServerMessage case 640)
    private void HandleMagicLevelExp(ServerPacket pkt)
    {
        int magicId = pkt.Recog;
        int level = pkt.Param;
        int curTrain = pkt.Tag;
        int maxTrain = 1000;
        if (!string.IsNullOrEmpty(pkt.ExtraStr) && int.TryParse(pkt.ExtraStr.Trim(), out var max) && max > 0)
        {
            maxTrain = max;
        }
        _hud?.SkillPanel?.UpdateSkillExp(magicId, level, curTrain, maxTrain);
        _hud?.AddChat("技能", $"[技能] 技能熟练度提升：Lv.{level} ({curTrain}/{maxTrain})。", new Color(1f, 0.9f, 0.4f));
    }

    /// <summary>
    /// 处理官方 1.5 技能学习/等级熟练度广播 (SM_SKILLLEVELUP 644 / bridge 对齐)
    /// Recog: magic_id, Param: level, Tag: curTrain, Extra: skill_name
    /// </summary>
    private void HandleSkillLevelUp(ServerPacket pkt)
    {
        int magicId = pkt.Recog;
        int level = pkt.Param;
        int curTrain = pkt.Tag;
        int maxTrain = 1000;
        string skillName = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(skillName)) skillName = pkt.ExtraStr;
        if (string.IsNullOrEmpty(skillName)) skillName = $"技能_{magicId}";

        if (_hud?.SkillPanel != null)
        {
            var existing = _hud.SkillPanel.Skills.FirstOrDefault(s => s.MagicId == magicId);
            if (existing != null)
            {
                _hud.SkillPanel.UpdateSkillExp(magicId, level, curTrain, existing.MaxTrain > 0 ? existing.MaxTrain : 1000);
            }
            else
            {
                int iconIdx = (magicId - 1) % 15;
                string desc = $"{skillName} (官方权威 ID:{magicId})";
                _hud.SkillPanel.AddOrUpdateSkill(new SkillPanelView.SkillItemData(magicId, skillName, iconIdx, "", level, curTrain, maxTrain, desc));
            }
        }
        _hud?.AddChat("技能", $"[技能] 技能熟练度同步: 【{skillName}】 (ID:{magicId}, Lv.{level}, 修炼:{curTrain})。", new Color(1f, 0.9f, 0.4f));
    }

    // 官方 1.5 进入地图描述公告 (IDA ProcessServerMessage case 661)
    private void HandleMapDescription(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        if (!string.IsNullOrEmpty(text))
        {
            _hud?.AddChat("地图", $"[地图] {text}", new Color(0.6f, 1f, 0.8f));
        }
    }

    /// <summary>
    /// 处理组队邀请确认弹窗 (SM_GROUP_INVITE_CONFIRM 658)
    /// 严格对齐官方 Delphi sub_467920 (handle_sm_confirm_dialog)
    /// </summary>
    private void HandleGroupInviteConfirm(ServerPacket pkt)
    {
        string inviter = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(inviter)) inviter = pkt.ExtraStr;
        if (string.IsNullOrEmpty(inviter)) inviter = "有玩家";
        _hud?.ShowModalMessage("组队邀请", $"{inviter} 邀请您加入编组，是否同意？", true, () =>
        {
            _hud?.AddChat("组队", $"[组队] 您同意了 {inviter} 的组队邀请。", new Color(0.5f, 1f, 0.5f));
        });
    }

    /// <summary>
    /// 处理组队邀请反馈失败 (SM_GROUP_INVITE_RESULT 661, Recog < 0)
    /// 严格对齐官方 Delphi sub_467920 (0x46A3A2)
    /// </summary>
    private void HandleGroupInviteFail(ServerPacket pkt)
    {
        string errMsg = pkt.Recog switch
        {
            -1 => "已经加入编组.",                                           // 官方 IDA 0x46B9A8
            -2 => "这个被加进编组的名字是不正确的。",                          // 官方 IDA 0x46B9C0
            -3 => "你想邀请加入编组的这个用户已经是其他组的成员了。",          // 官方 IDA 0x46B9EC
            -4 => "对方不允许编组。",                                        // 官方 IDA 0x46BA28 (sub_46BA28)
            _ => "邀请组队失败。"
        };
        _hud?.ShowModalMessage("组队提示", errMsg);
        _hud?.AddChat("组队", $"[组队] {errMsg}", new Color(1f, 0.4f, 0.4f));
    }

    /// <summary>
    /// 处理创建编组失败 (SM_CREATEGROUP_FAIL 664)
    /// 严格对齐官方 Delphi sub_467920 (0x46A42E)
    /// </summary>
    private void HandleCreateGroupFail(ServerPacket pkt)
    {
        string errMsg = pkt.Recog switch
        {
            -1 => "编组还未成立或者你还不够等级创建。", // 官方 IDA 0x46BA44
            -2 => "这个被加进编组的名字是不正确的。",    // 官方 IDA 0x46B9C0
            -3 => "已经加入编组。",                    // 官方 IDA 0x46BA70 (sub_46BA70)
            -4 => "对方不允许编组。",                  // 官方 IDA 0x46BA28 (sub_46BA28)
            -5 => "成员上限已经达到。",                // 官方 IDA 0x46BA88
            _ => "创建编组失败。"
        };
        _hud?.ShowModalMessage("组队提示", errMsg);
        _hud?.AddChat("组队", $"[组队] {errMsg}", new Color(1f, 0.4f, 0.4f));
    }

    /// <summary>
    /// 处理踢出/退出编组失败 (SM_LEAVEGROUP_FAIL 665)
    /// 严格对齐官方 Delphi sub_467920 (0x46A4CE)
    /// </summary>
    private void HandleLeaveGroupFail(ServerPacket pkt)
    {
        string errMsg = pkt.Recog switch
        {
            -1 => "编组还未成立或者你还不够等级创建。", // 官方 IDA 0x46BA44
            -2 => "这个被加进编组的名字是不正确的。",    // 官方 IDA 0x46B9C0
            -3 => "你仍旧不在编组中。",                 // 官方 IDA 0x46BAA4
            _ => "组队操作失败。"
        };
        _hud?.ShowModalMessage("组队提示", errMsg);
        _hud?.AddChat("组队", $"[组队] {errMsg}", new Color(1f, 0.4f, 0.4f));
    }

    // 官方 1.5 全服系统滚动公告跑马灯 (IDA ProcessServerMessage case 662)
    private void HandleSendNotice(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        if (!string.IsNullOrEmpty(text))
        {
            _hud?.AddChat("公告", $"[全服公告] {text}", new Color(1f, 0.85f, 0.2f));
        }
    }

    // 官方 1.5 施放魔法成功回显 (IDA ProcessServerMessage case 802)
    private void HandleMagicFireAck(ServerPacket pkt)
    {
        int casterUid = pkt.Recog;
        int targetX = pkt.Param;
        int targetY = pkt.Tag;
        SafeLog($"[SM] 魔法释放成功 caster={casterUid} pos=({targetX},{targetY})");
    }

    // 官方 1.5 施放魔法失败提示 (IDA ProcessServerMessage case 803)
    private void HandleMagicFireFail(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        if (string.IsNullOrEmpty(text)) text = "魔法施放失败，可能由于魔法值不足或技能处于冷却中。";
        _hud?.AddChat("技能", $"[法术] {text}", new Color(1f, 0.4f, 0.4f));
    }

    // 官方 1.5 视野说话广播 (IDA ProcessServerMessage case 6)
    private void HandleHear(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        if (!string.IsNullOrEmpty(text))
        {
            _hud?.AddChat("附近", text, Colors.White);
        }
    }

    // 官方 1.5 屏幕滚动系统公告 (IDA ProcessServerMessage case 7)
    private void HandleSystemMessage(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        if (!string.IsNullOrEmpty(text))
        {
            _hud?.AddChat("系统", $"[系统] {text}", new Color(1f, 0.3f, 0.3f));
        }
    }

    // 官方 1.5 实体挥砍物理攻击广播 (IDA ProcessServerMessage case 12)
    private void HandleHit(ServerPacket pkt)
    {
        int attackerId = pkt.Recog;
        int dir = pkt.Param;
        _entityManager?.UpdateTurn(attackerId, (byte)dir);
        if (_gameView?.PlayerInstance != null && (attackerId == 0 || attackerId == _gameView.SessionId))
        {
            _gameView.PlayerInstance.Direction = dir;
            _gameView.PlayerInstance.Anim?.SetAction(ActionType.Hit, dir);
        }
    }

    // 官方 1.5 蹲下/割肉姿态 (IDA ProcessServerMessage case 27)
    private void HandleSitDown(ServerPacket pkt)
    {
        int uid = pkt.Recog;
        if (_gameView?.PlayerInstance != null && (uid == 0 || uid == _gameView.SessionId))
        {
            _gameView.PlayerInstance.Anim?.SetAction(ActionType.SitDown, _gameView.PlayerInstance.Direction);
        }
    }

    // 官方 1.5 NPC 对话框内容下发 (IDA ProcessServerMessage case 633 / 636)
    private void HandleNpcDialogMsg(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        if (!string.IsNullOrEmpty(text))
        {
            _hud?.NpcDialog?.ShowDialog(text);
        }
    }

    // 行会更名/加入/退出（750，extra=新行会名，空=无行会）。
    private void HandleChangeGuildName(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        if (_hud?.GuildPanel != null && !string.IsNullOrEmpty(text))
            _hud.GuildPanel.SetGuildName(text);
        _hud?.AddChat("行会", string.IsNullOrEmpty(text) ? "[行会] 你已没有行会。" : $"[行会] 所在行会：{text}", new Color(0.6f, 0.85f, 1f));
    }

    /// <summary>
    /// 盛大官方 1.5 实体名字颜色变动广播 (SM_CHANGENAMECOLOR 41, IDA case ')' @ 0x46A157)
    /// </summary>
    private void HandleChangeNameColor(ServerPacket pkt)
    {
        int uid = pkt.Recog;
        int color = pkt.Param;
        _entityManager?.UpdateNameColor(uid, color);
        SafeLog($"[SM] 实体 UID={uid} 名字颜色变更为 {color} (SM_CHANGENAMECOLOR 41)");
    }

    /// <summary>
    /// 盛大官方 1.5 实体所属行会与封号变动广播 (SM_GUILDNAME_DELPHI 42, IDA case '*' @ 0x46A16F)
    /// </summary>
    private void HandleChangeEntityGuildName(ServerPacket pkt)
    {
        int uid = pkt.Recog;
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        string guildName = text ?? string.Empty;
        string rank = string.Empty;
        if (!string.IsNullOrEmpty(text))
        {
            // 官方 Delphi 分隔符支持 '/' 与 '\'
            int sep = text.IndexOfAny(new[] { '/', '\\' });
            if (sep >= 0)
            {
                guildName = text.Substring(0, sep).Trim();
                rank = text.Substring(sep + 1).Trim();
            }
        }

        _entityManager?.UpdateGuild(uid, guildName, rank);

        if (!string.IsNullOrEmpty(guildName))
        {
            _hud?.AddChat("行会", $"[行会] 实体(ID:{uid}) 所属行会已更新：{guildName} {rank}".Trim(), new Color(0.6f, 0.9f, 0.7f));
        }
        SafeLog($"[SM] 实体 UID={uid} 行会更新 guild={guildName} rank={rank} (SM_GUILDNAME_DELPHI 42)");
    }

    /// <summary>
    /// 盛大官方 1.5 系统提示与带音效广播 (SM_SYSMSG_DELPHI 100~104, IDA 0x467920 100~104 @ 0x46AF10)
    /// </summary>
    private void HandleSysMsgDelphi(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        if (pkt.Param > 0)
        {
            AudioManager.Instance?.PlaySoundId(pkt.Param);
        }
        if (!string.IsNullOrEmpty(text))
        {
            _hud?.AddChat("系统", $"[系统] {text}", new Color(1f, 0.9f, 0.4f));
        }
        SafeLog($"[SM] 官方系统提示: {text} sound={pkt.Param} (SM_SYSMSG_DELPHI {pkt.Ident})");
    }

    // 破土/潜地（36/37）：recog=实体，param/tag=坐标。
    private void HandleDigUpDown(ServerPacket pkt, bool digUp)
    {
        if (_entityManager == null) return;
        if (digUp)
        {
            var ent = _entityManager.GetEntity(pkt.Recog);
            if (ent == null)
            {
                _entityManager.AddEntity(pkt.Recog, string.Empty,
                    Mir2Client.Game.EntityManager.EntityType.Monster, pkt.Param, pkt.Tag, 2, 0);
            }
            else
            {
                _entityManager.UpdateMove(pkt.Recog, pkt.Param, pkt.Tag, ent.Direction);
            }
        }
        else
        {
            _entityManager.RemoveEntity(pkt.Recog);
        }
    }

    private void HandleLogicState(ServerPacket pkt)
    {
        string text = pkt.ExtraStr;
        if (!string.IsNullOrEmpty(text))
        {
            var parts = text.Split(new[] { '\t', '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
            {
                int x = int.TryParse(parts[0], out var px) ? px : 300;
                int y = int.TryParse(parts[1], out var py) ? py : 300;
                byte dir = parts.Length > 2 && byte.TryParse(parts[2], out var pd) ? pd : (byte)2;

                if (_gameView?.PlayerInstance != null)
                {
                    _gameView.PlayerInstance.SetTilePos(x, y);
                    _gameView.PlayerInstance.Direction = dir;
                    _gameView.UpdateEnvironmentLighting();
                }
            }
        }
    }

    private void HandleNewMap(ServerPacket pkt)
    {
        string mapId = pkt.ExtraStr;
        if (string.IsNullOrEmpty(mapId))
        {
            mapId = pkt.Recog.ToString();
        }
        if (!string.IsNullOrEmpty(mapId) && _gameView != null)
        {
            _gameView.CurrentMapName = mapId;
            string mapPath = ResPath.Resolve($"resources/maps/Map/{mapId}.map");
            _gameView.MapRendererInstance?.LoadMap(mapPath);
            _gameView.UpdateEnvironmentLighting();
        }
    }

    private void HandleEntitySpawn(ServerPacket pkt)
    {
        if (_entityManager == null) return;
        int uid = pkt.Recog;
        string text = pkt.ExtraStr;
        if (string.IsNullOrEmpty(text)) return;

        var parts = text.Split(new[] { '\t', '/' }, StringSplitOptions.None);
        if (parts.Length < 3) return;

        string name = parts[0];
        int x = int.TryParse(parts[1], out var tx) ? tx : 300;
        int y = int.TryParse(parts[2], out var ty) ? ty : 300;
        byte dir = parts.Length > 3 && byte.TryParse(parts[3], out var td) ? td : (byte)2;
        int appr = pkt.Param;
        if (parts.Length > 4 && int.TryParse(parts[4], out var a)) appr = a;

        var type = pkt.Ident switch
        {
            SM_MONSPAWN => Mir2Client.Game.EntityManager.EntityType.Monster,
            SM_NPCSPAWN => Mir2Client.Game.EntityManager.EntityType.Npc,
            _ => Mir2Client.Game.EntityManager.EntityType.Player
        };

        if (appr == 0)
        {
            if (name.Contains("稻草人")) appr = 1;
            else if (name.Contains("钉耙猫")) appr = 2;
            else if (name.Contains("半兽人")) appr = 3;
            else if (name.Contains("森林雪人")) appr = 4;
            else if (name.Contains("食人花")) appr = 5;
            else if (name.Contains("毒蜘蛛")) appr = 6;
            else if (name.Contains("骷髅")) appr = 10;
        }

        _entityManager.AddEntity(uid, name, type, x, y, dir, appr);
    }

    private void HandleEntityDelete(ServerPacket pkt)
    {
        if (_entityManager == null) return;
        int uid = pkt.Recog;
        _entityManager.RemoveEntity(uid);
    }

    private void HandleTurnDirection(ServerPacket pkt)
    {
        if (_entityManager == null) return;
        int uid = pkt.Recog;
        byte dir = (byte)(pkt.Param & 7);
        _entityManager.UpdateTurn(uid, dir);
        // 盛大官方 1.5 IDA 物理指令: case 14 entity_action(...) 动作参数同步
        _entityManager.UpdateActorParam(uid, pkt.Param);
    }

    // 他人移动：Extra "x/y/dir" 优先，缺省按 Recog 低16=x/高16=y，L2。
    private void HandleEntityMove(ServerPacket pkt)
    {
        if (_entityManager == null) return;
        int uid = pkt.Recog;
        int x = pkt.Recog & 0xFFFF, y = (pkt.Recog >> 16) & 0xFFFF;
        byte dir = (byte)(pkt.Param & 7);
        if (!string.IsNullOrEmpty(pkt.ExtraStr))
        {
            var parts = pkt.ExtraStr.Split(new[] { '\t', '/', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
            {
                if (int.TryParse(parts[0], out var px)) x = px;
                if (int.TryParse(parts[1], out var py)) y = py;
                if (parts.Length >= 3 && byte.TryParse(parts[2], out var pd)) dir = (byte)(pd & 7);
            }
        }
        if (x > 0 && y > 0) _entityManager.UpdateMove(uid, x, y, dir);
    }

    // 物品展示（625，与 REPAIROK 同值，路由内无修理分支故归此，L2）：聊天回显。
    private void HandleItemShow(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        if (string.IsNullOrEmpty(text)) return;
        _hud?.AddChat("系统", $"[展示] {text}", new Color(1f, 0.95f, 0.6f));
    }

    private void HandleEntityDead(ServerPacket pkt)
    {
        int uid = pkt.Recog;
        if (_gameView?.PlayerInstance != null && (uid == 0 || uid == _gameView.SessionId || uid == _gameView.PlayerInstance.Uid))
        {
            _gameView.TriggerPlayerDeath();
            return;
        }

        if (_entityManager == null) return;
        _entityManager.TriggerDeath(uid);
    }

    private void HandleObjectSpell(ServerPacket pkt)
    {
        int casterId = pkt.Recog;
        int magicId = pkt.Param;
        int targetX = pkt.Tag;
        int targetY = 0;
        if (!string.IsNullOrEmpty(pkt.ExtraStr))
        {
            var parts = pkt.ExtraStr.Split(new[] { '\t', ' ', '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0 && int.TryParse(parts[0], out var ty)) targetY = ty;
            if (parts.Length > 1 && int.TryParse(parts[1], out var mid) && mid > 0) magicId = mid;
        }

        Vector2I fromTile = Vector2I.Zero;
        if (_gameView?.PlayerInstance != null && (casterId == 0 || casterId == _gameView.SessionId))
        {
            fromTile = new Vector2I(_gameView.PlayerInstance.TileX, _gameView.PlayerInstance.TileY);
            var delta = new Vector2I(targetX, targetY) - fromTile;
            int dir = delta != Vector2I.Zero ? Mir2Client.Game.InputPlugin.CalcDirection(new Vector2(delta.X, delta.Y)) : _gameView.PlayerInstance.Direction;
            _gameView.PlayerInstance.Direction = dir;
            _gameView.PlayerInstance.Anim.SetAction(ActionType.Cast, dir);
        }
        else if (_entityManager?.GetEntity(casterId) is { } ent)
        {
            fromTile = new Vector2I(ent.TileX, ent.TileY);
        }

        _gameView?.MagicManagerInstance?.PlayMagicSfx(magicId, 0);
    }

    private void HandleMagicFire(ServerPacket pkt)
    {
        int casterId = pkt.Recog;
        int magicId = pkt.Param;
        int targetX = pkt.Tag;
        int targetY = 0;

        if (pkt.Ident == SM_LIGHTING_DELPHI)
        {
            magicId = 10; // 雷电术
            targetX = pkt.Param;
            targetY = pkt.Tag;
        }
        else if (!string.IsNullOrEmpty(pkt.ExtraStr))
        {
            var parts = pkt.ExtraStr.Split(new[] { '\t', ' ', '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0 && int.TryParse(parts[0], out var ty)) targetY = ty;
            if (parts.Length > 1 && int.TryParse(parts[1], out var mid) && mid > 0) magicId = mid;
        }

        Vector2I fromTile = new Vector2I(targetX, targetY);
        if (_gameView?.PlayerInstance != null && (casterId == 0 || casterId == _gameView.SessionId))
        {
            fromTile = new Vector2I(_gameView.PlayerInstance.TileX, _gameView.PlayerInstance.TileY);
        }
        else if (_entityManager?.GetEntity(casterId) is { } ent)
        {
            fromTile = new Vector2I(ent.TileX, ent.TileY);
        }

        Vector2I toTile = new Vector2I(targetX, targetY);
        _gameView?.MagicManagerInstance?.PlayMagic(magicId, fromTile, toTile, casterId);
    }

    private void HandleSpellResult(ServerPacket pkt)
    {
        int targetId = pkt.Recog;
        int targetHp = pkt.Param;
        int damage = pkt.Tag;

        bool isPlayer = (_gameView?.PlayerInstance != null && (targetId == 0 || targetId == _gameView.SessionId));
        if (isPlayer && _gameView?.PlayerInstance != null)
        {
            _gameView.PlayerInstance.Hp = Math.Max(0, targetHp);
            _hud?.UpdateHp(_gameView.PlayerInstance.Hp, _gameView.PlayerInstance.MaxHp);
            _gameView.PlayerInstance.Anim.SetAction(ActionType.Struck, _gameView.PlayerInstance.Direction);
            Mir2Client.Audio.AudioManager.Instance?.PlaySoundId(2);
            if (damage > 0)
            {
                _hud?.AddChat("战斗", $"受到伤害 {damage} 点，剩余生命 {targetHp}。", new Color(1f, 0.4f, 0.4f));
            }
            if (_gameView.PlayerInstance.Hp <= 0)
            {
                _gameView.TriggerPlayerDeath();
            }
        }
        else if (_entityManager?.GetEntity(targetId) is { } targetEnt)
        {
            _entityManager.TriggerStruck(targetId, damage, targetHp);
            if (damage > 0)
            {
                _hud?.AddChat("战斗", $"命中目标 {targetEnt.Name}，造成 {damage} 点伤害！", new Color(1f, 0.9f, 0.3f));
            }
        }
    }

    private void HandleBatchMove(ServerPacket pkt)
    {
        if (_entityManager == null || string.IsNullOrEmpty(pkt.ExtraStr)) return;
        var lines = pkt.ExtraStr.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var p = line.Split(new[] { '\t', '/', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length < 4) continue;
            if (!int.TryParse(p[0], out var uid)) continue;
            if (!int.TryParse(p[1], out var x) || !int.TryParse(p[2], out var y)) continue;
            if (!byte.TryParse(p[3], out var dir)) dir = 2;
            bool run = p.Length >= 5 && (p[4] == "1" || p[4].Equals("run", StringComparison.OrdinalIgnoreCase));
            if (x <= 0 || y <= 0) continue;
            if (run) _entityManager.UpdateStepMove(uid, x, y, (byte)(dir & 7), true);
            else _entityManager.UpdateMove(uid, x, y, (byte)(dir & 7));
        }
    }

    private void HandleBatchStruck(ServerPacket pkt)
    {
        if (_entityManager == null || string.IsNullOrEmpty(pkt.ExtraStr)) return;
        var lines = pkt.ExtraStr.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var p = line.Split(new[] { '\t', '/', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (p.Length < 2) continue;
            if (!int.TryParse(p[0], out var uid)) continue;
            int damage = p.Length >= 2 && int.TryParse(p[1], out var d) ? d : 0;
            int hp = p.Length >= 3 && int.TryParse(p[2], out var h) ? h : -1;
            var ent = _entityManager.GetEntity(uid);
            if (ent == null) continue;
            _entityManager.TriggerStruck(uid, damage, hp >= 0 ? hp : ent.Hp);
        }
    }

    private void HandleGroundItem(ServerPacket pkt)
    {
        var gm = _groundItemManager ?? _gameView?.GroundItemManagerInstance;
        if (gm == null) return;

        int itemId = pkt.Recog;
        int x = pkt.Param;
        int y = pkt.Tag;
        int looks = 0;
        string name = "掉落物";

        if (pkt.Extra.Length > 0)
        {
            string extraStr = System.Text.Encoding.UTF8.GetString(pkt.Extra);
            string[] parts = extraStr.Split('\t');
            if (parts.Length > 0 && !string.IsNullOrEmpty(parts[0])) name = parts[0];
            if (parts.Length > 1 && int.TryParse(parts[1], out var tx)) x = tx;
            if (parts.Length > 2 && int.TryParse(parts[2], out var ty)) y = ty;
            if (parts.Length > 3 && int.TryParse(parts[3], out var lk)) looks = lk;
        }

        gm.AddItem(itemId, name, x, y, looks);
    }

    private void HandleDelGroundItem(ServerPacket pkt)
    {
        var gm = _groundItemManager ?? _gameView?.GroundItemManagerInstance;
        if (gm == null) return;

        int itemId = pkt.Recog;
        gm.RemoveItem(itemId);
    }

    private void HandleDealMenu(ServerPacket pkt)
    {
        string partner = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(partner)) partner = "对方";
        _hud?.OpenTrade(partner);
    }

    private void HandleDealCancel(ServerPacket pkt)
    {
        string reason = DecodeExtraText(pkt.Extra);
        _hud?.OnServerTradeCancel(reason);

        // 若为 SM_DEALTRY_FAIL (674)，官方 IDA 0x46A588 弹出模态框 0x46BADC
        if (pkt.Ident == SM_DEALTRY_FAIL)
        {
            string msg = !string.IsNullOrEmpty(reason) ? reason : "交易被取消。\n要正确交易你必须和对方面对面。";
            _hud?.ShowModalMessage("交易提示", msg);
        }
    }

    private void HandleDealAddItemOk(ServerPacket pkt)
    {
        string itemName = DecodeExtraText(pkt.Extra);
        _hud?.OnServerTradeAddItemOk(pkt.Param, itemName);
    }

    private void HandleBillingNotice(ServerPacket pkt)
    {
        string notice = string.IsNullOrEmpty(pkt.ExtraStr) ? "您的游戏时间已到期，请充值后继续游戏。" : pkt.ExtraStr;
        _hud?.ShowModalMessage("计费通知", notice);
        _hud?.AddChat("系统", $"[系统] {notice}", new Color(1f, 0.3f, 0.3f));
    }

    private void HandleHealthBarColor(ServerPacket pkt)
    {
        int uid = pkt.Recog;
        int hp = pkt.Param;
        int maxHp = pkt.Tag > 0 ? pkt.Tag : 100;
        int color = 0;
        string extraStr = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(extraStr)) extraStr = pkt.ExtraStr;
        if (!string.IsNullOrEmpty(extraStr) && int.TryParse(extraStr, out var c))
        {
            color = c;
        }
        _entityManager?.UpdateHealthBarColor(uid, hp, maxHp, color);
    }

    private void HandleGuildWarStatus(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        string msg = !string.IsNullOrEmpty(text) ? text : "沙巴克行会战战况局势已更新！";
        _hud?.AddChat("行会", $"[行会战] {msg}", new Color(1f, 0.3f, 0.3f));
        _hud?.ShowModalMessage("行会战况", msg);
    }

    private static string DecodeExtraText(byte[]? extra)
    {
        if (extra == null || extra.Length == 0) return string.Empty;
        try
        {
            return Encoding.UTF8.GetString(extra).TrimEnd('\0');
        }
        catch
        {
            return string.Empty;
        }
    }

    private static void SafeLog(string msg)
    {
        System.Diagnostics.Debug.WriteLine(msg);
    }
}
