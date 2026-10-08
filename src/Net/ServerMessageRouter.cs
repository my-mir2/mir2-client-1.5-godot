using System;
using System.Collections.Generic;
using System.Text;
using Godot;
using Mir2Client.Audio;
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
    public const short SM_GOLDCHANGED_DELPHI     = 653;  // Delphi 官方金币变动码 (653)
    public const short SM_LEVELUP                = 44;   // 角色升级
    public const short SM_EXPERIENCE             = 45;   // 经验增长
    public const short SM_WINEXP                 = 619;  // Delphi 杀怪/任务获得经验 (619)
    public const short SM_SENDUSERSTATE          = 52;   // 角色基础四维属性 (攻魔道防)
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
    public const short SM_CLEAROBJECTS           = 810;  // 清空视野实体 (810)
    public const short SM_ADDITEM                = 200;  // 背包添加物品
    public const short SM_SENDUSERMAGIC          = 201;  // 角色已学技能列表批量下发
    public const short SM_DELITEM                = 202;  // 背包移除物品
    public const short SM_UPDATEUSERMAGIC        = 203;  // 技能经验熟练度更新
    public const short SM_ALLOWMOVE              = 203;  // 盛大官方/网关位移确认与拉回校准 (bridge.rs)
    public const short SM_SKILLLEVELUP           = 644;  // 盛大官方/网关技能熟练度升级 (bridge.rs)
    public const short SM_TAKEON_OK              = 615;  // 穿戴装备成功
    public const short SM_EXCHGTAKEONOK          = 615;  // 互换穿戴装备成功 (IDA 0x467920)
    public const short SM_TAKEON_FAIL            = 616;  // 穿戴装备失败 (IDA 0x467920)
    public const short SM_EXCHGTAKEONFAIL        = 616;  // 互换穿戴装备失败 (IDA 0x467920)
    public const short SM_TAKEOFF_OK             = 619;  // 脱下装备成功
    public const short SM_EQUIPINFO              = 621;  // 全量 9 槽位装备下发
    public const short SM_WEIGHTCHANGED          = 622;  // 负重同步
    public const short SM_MERCHANTDLG            = 631;  // 商店货架界面下发
    public const short SM_DEALMENU               = 670;  // 交易界面弹出 (ProtocolConsts)
    public const short SM_DEALMENU_IDA           = 673;  // 交易界面弹出 (IDA 0x46A5AA)
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
    public const short SM_DEALCOMPLETE          = 678;  // 悬置：与 SM_DEALDELITEM_FAIL=678 重号（bridge 内亦复用），路由归取下失败，待联调
    public const short SM_DEALCANCEL            = 688;  // 交易取消退回
    public const short SM_DEALCANCEL_DELPHI     = 681;  // 交易取消 (Delphi 变体 681)
    public const short SM_USERSTORAGE_DLG        = 700;  // 仓库保管箱托管界面弹出
    public const short SM_SENDUSERSTATE_INSPECT  = 751;  // 观人/他人状态下发 (IDA 0x46A857)
    public const short SM_OPENGUILDDLG           = 753;  // 打开行会主页 (bridge SM_OPENGUILDDLG_DELPHI)
    public const short SM_UPDATEGUILDINFO        = 756;  // 行会信息/公告更新 (bridge SM_UPDATEGUILDINFO_DELPHI，非名单！)
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

        switch (smCode)
        {
            case SM_GOLDCHANGED:
            case SM_GOLDCHANGED_DELPHI:
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
            case SM_USERDELETE:
            case SM_MONDELETE:
            case SM_NPCDELETE:
            case SM_OBJECT_DISAPPEAR:
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
            case SM_CLEAROBJECTS:
                _entityManager?.Clear();
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
            case SM_SKILLLEVELUP:
                HandleUpdateUserMagic(packet);
                break;
            case SM_EQUIPINFO:
                HandleEquipInfo(packet);
                break;
            case SM_TAKEON_OK:
                HandleTakeOnOk(packet);
                break;
            case SM_TAKEOFF_OK: // 619: 官方 Delphi 619 号多态处理 (脱装备或经验)
                if (packet.Recog == 0 && packet.Param < 9)
                {
                    HandleTakeOffOk(packet);
                }
                else
                {
                    HandleExperience(packet);
                }
                break;
            case SM_NPCDIALOG:
                HandleNpcDialog(packet);
                break;
            case SM_MERCHANTDLG:
                HandleMerchantDlg(packet);
                break;
            case SM_DEALMENU:
            case SM_DEALMENU_IDA:
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
                break;
            case SM_DEALDELITEM_OK:
                _hud?.OnServerTradeDelItemOk(packet.Param);
                break;
            case SM_DEALDELITEM_FAIL:
                _hud?.AddChat("交易", $"[交易] 取下物品失败: {DecodeExtraText(packet.Extra)}", new Color(1f, 0.4f, 0.4f));
                break;
            case SM_DEALREMOTEADDITEM:
                _hud?.OnServerTradeRemoteAddItem(packet.Param, DecodeExtraText(packet.Extra));
                break;
            case SM_DEALREMOTEDELITEM:
                _hud?.OnServerTradeRemoteDelItem(packet.Param);
                break;
            case SM_DEALCHGGOLD_OK:
                _hud?.OnServerTradeChgGold(packet.Recog, true);
                break;
            case SM_DEALCHGGOLD_FAIL:
                _hud?.AddChat("交易", $"[交易] 修改金币失败: {DecodeExtraText(packet.Extra)}", new Color(1f, 0.4f, 0.4f));
                break;
            case SM_DEALREMOTECHGGOLD:
                _hud?.OnServerTradeChgGold(packet.Recog, false);
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
                _hud?.OnServerAttackModeChanged(packet.Param);
                break;
            case SM_NEEDPASSWORD:
                _hud?.OnNeedPassword();
                break;
            case SM_SENDUSERSTATE:
            case SM_SENDUSERSTATE_INSPECT:
                HandleSendUserState(packet);
                break;
            case SM_GUILDMEMBERLIST_ALT:
                HandleSendGuildMemberList(packet);
                break;
            case SM_UPDATEGUILDINFO:
            case SM_GUILDNOTICE_RESULT:
                HandleGuildMessage(packet);
                break;
            case ProtocolConsts.SM_BUILDGUILDOK:
            case ProtocolConsts.SM_BUILDGUILDFAIL:
                HandleBuildGuildResult(packet);
                break;
            // bridge 真号：757 加人成功 / 754 加人失败 / 759 踢人成功 / 760 踢人失败（旧 753/755/756 号已废）。
            case ProtocolConsts.SM_GUILDADDMEMBEROK:
            case ProtocolConsts.SM_GUILDADDMEMBERFAIL:
            case ProtocolConsts.SM_GUILDDELMEMBEROK:
            case ProtocolConsts.SM_GUILDDELMEMBERFAIL:
                HandleGuildMemberChange(packet);
                break;
            // 结盟/解盟成功无专用号（走 SM_SAY 聊天，bridge 2416/2422），仅失败号 769/771 进路由。
            case ProtocolConsts.SM_GUILDMAKEALLYFAIL:
            case ProtocolConsts.SM_GUILDBREAKALLYFAIL:
                HandleGuildDiplomacy(packet);
                break;
            case ProtocolConsts.SM_GUILDRANKUPDATEFAIL:
                HandleGuildRankResult(packet);
                break;
            case ProtocolConsts.SM_ADJUSTBONUS:
                _hud?.ToggleAdjustAbil(Math.Max(0, packet.Recog));
                break;
            // 边缘动作包（bridge 34/36/37/750/764/765）：飞斧/破土/潜地/改名/捐献，L2。
            case 34: // SM_FLYAXE：recog=投掷者，param/tag=目标。无抛射体 API，日志留痕。
                GD.Print($"[SM] 飞斧投掷 caster={packet.Recog} target=({packet.Param},{packet.Tag})");
                break;
            case 36: // SM_DIGUP：recog=实体，param/tag=出土点。实体缺席则按怪补生。
                HandleDigUpDown(packet, true);
                break;
            case 37: // SM_DIGDOWN：recog=实体潜地。暂按移除（下次 spawn/move 重建）。
                HandleDigUpDown(packet, false);
                break;
            case 750: // SM_CHANGEGUILDNAME：extra=新行会名（加入/退出/更名同号，bridge 2118）。
                HandleChangeGuildName(packet);
                break;
            case ProtocolConsts.SM_DONATEFAIL:
                HandleDonateResult(packet);
                break;
            case ProtocolConsts.SM_GROUPMEMBERS:
                HandleGroupMembers(packet);
                break;
            case ProtocolConsts.SM_GUILDMESSAGE:
                HandleGuildMessage(packet);
                break;
            case ProtocolConsts.SM_BUYITEMSUCCESS:
            case ProtocolConsts.SM_BUYITEM_SUCCESS:
                _hud?.AddChat("商店", $"[商店] 购买成功：{DecodeExtraText(packet.Extra)}", new Color(0.5f, 1f, 0.5f));
                break;
            case ProtocolConsts.SM_BUYITEMFAIL:
            case ProtocolConsts.SM_BUYITEM_FAIL:
                _hud?.AddChat("商店", $"[商店] 购买失败：{DecodeExtraText(packet.Extra)}", new Color(1f, 0.5f, 0.5f));
                break;
            // 注：SM_STORAGE_ITEMS=682 与 SM_DEALREMOTEADDITEM=682 重号，
            // 真值表 682 归交易对方放物（有 OnServerTradeRemoteAddItem 实证），
            // 仓库列表分支停用，仓库走 SM_USERSTORAGE_DLG=700 开板，L2 待 M2 联调裁决真号。
            case ProtocolConsts.SM_EAT_OK:
                _hud?.AddChat("系统", $"[服药] {DecodeExtraText(packet.Extra)}", new Color(1f, 1f, 0.6f));
                break;
            case ProtocolConsts.SM_DELMAGIC:
                _hud?.AddChat("技能", $"[技能] 已删除技能：{DecodeExtraText(packet.Extra)}", new Color(0.8f, 0.8f, 1f));
                break;
            case SM_OBJECTSPELL:
                HandleObjectSpell(packet);
                break;
            case SM_MAGIC_FIRE:
            case SM_LIGHTING_DELPHI:
                HandleMagicFire(packet);
                break;
            case SM_SPELLRESULT:
            case SM_OBJECTSTRUCK:
            case SM_OBJECTSTRUCK_DELPHI:
                HandleSpellResult(packet);
                break;
            // 批量包：15/16 均在真值表内；逐行 "uid/x/y/dir[/run]" 与 "uid/damage/hp"，L2。
            case ProtocolConsts.SM_BATCHMOVE:
                HandleBatchMove(packet);
                break;
            case ProtocolConsts.SM_BATCHSTRUCK:
                HandleBatchStruck(packet);
                break;
        }
    }

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

    private void HandleLevelUp(ServerPacket pkt)
    {
        int level = pkt.Param > 0 ? pkt.Param : pkt.Recog;
        if (level > 0)
        {
            _hud?.UpdateLevel(level);
        }
    }

    private void HandleExperience(ServerPacket pkt)
    {
        long curExp = (uint)pkt.Recog;
        long maxExp = pkt.Param > 0 ? (uint)pkt.Param : 100000;
        _hud?.UpdateExp(curExp, maxExp);
    }

    private void HandleHealthSpellChanged(ServerPacket pkt)
    {
        int hp = pkt.Recog;
        int maxHp = pkt.Param > 0 ? pkt.Param : Math.Max(hp, 1);
        int mp = pkt.Tag;
        int maxMp = Math.Max(mp, 1);

        if (!string.IsNullOrEmpty(pkt.ExtraStr))
        {
            var parts = pkt.ExtraStr.Split(new[] { '/', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 4)
            {
                int.TryParse(parts[0], out hp);
                int.TryParse(parts[1], out maxHp);
                int.TryParse(parts[2], out mp);
                int.TryParse(parts[3], out maxMp);
            }
        }

        _hud?.UpdateHp(hp, maxHp);
        _hud?.UpdateMp(mp, maxMp);
    }

    private void HandleWeightChanged(ServerPacket pkt)
    {
        int curWeight = pkt.Recog;
        int maxWeight = pkt.Param > 0 ? pkt.Param : 50;
        _hud?.UpdateWeight(curWeight, maxWeight);

        // 若携带制表符 6 字段扩展 (weight, max_weight, wear_weight, max_wear_weight, hand_weight, max_hand_weight)
        // 自动驱动状态面板 (StatusPanelView) 能力页第 1 (背包重)、第 2 (负重)、第 3 (腕力)
        if (!string.IsNullOrEmpty(pkt.ExtraStr) && pkt.ExtraStr.Contains('\t'))
        {
            var parts = pkt.ExtraStr.Split('\t');
            if (parts.Length >= 6)
            {
                var sp = _hud?.StatusPanel;
                if (sp != null)
                {
                    sp.UpdateAbilityValue(1, $"{parts[0]}/{parts[1]}", int.TryParse(parts[0], out var w) && int.TryParse(parts[1], out var mw) && w > mw);
                    sp.UpdateAbilityValue(2, $"{parts[2]}/{parts[3]}", int.TryParse(parts[2], out var ww) && int.TryParse(parts[3], out var mww) && ww > mww);
                    sp.UpdateAbilityValue(3, $"{parts[4]}/{parts[5]}", int.TryParse(parts[4], out var hw) && int.TryParse(parts[5], out var mhw) && hw > mhw);
                }
            }
        }
    }

    private void HandleChangeLight(ServerPacket pkt)
    {
        int light = pkt.Param;
        int state = light switch
        {
            <= 0 => 2, // 黑夜
            1 => 1,    // 黄昏
            2 => 3,    // 清晨
            _ => 0     // 正午白天
        };
        _hud?.SetDayNightState(state);
        bool isDark = state == 1 || state == 2;
        _hud?.UpdateVisionFog(isDark, isDark ? 6 : 15, new Godot.Vector2(400, 225));
    }

    private void HandleAddItem(ServerPacket pkt)
    {
        int slot = pkt.Param >= 0 && pkt.Param < 40 ? pkt.Param : (pkt.Recog >= 0 && pkt.Recog < 40 ? pkt.Recog : 0);
        string itemName = "新物品";
        string iconFile = "StateItem_0001.png";
        string desc = "[color=#c0c0c0]重量 1   持久力 10/10[/color]";

        if (!string.IsNullOrEmpty(pkt.ExtraStr))
        {
            var parts = pkt.ExtraStr.Split(new[] { '/', '|' }, StringSplitOptions.None);
            if (parts.Length >= 1 && !string.IsNullOrEmpty(parts[0])) itemName = parts[0];
            if (parts.Length >= 2 && !string.IsNullOrEmpty(parts[1])) iconFile = parts[1];
            if (parts.Length >= 3 && !string.IsNullOrEmpty(parts[2])) desc = parts[2];
        }

        _hud?.SetBagItem(slot, itemName, iconFile, desc);
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
                    inspect.Inspect(name);

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

    // 行会结果系回包：分发器归属待 IDA 确认（不在 ProcessServerMessage 140 case 内），
    // 暂按 M2Server 语义接入本地面板与聊天，L2。
    private void HandleBuildGuildResult(ServerPacket pkt)
    {
        bool ok = pkt.Ident == ProtocolConsts.SM_BUILDGUILDOK;
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = ok ? "行会创建成功！" : "行会创建失败！";
        _hud?.AddChat("行会", $"[行会] {text}", new Color(0.4f, 1f, 0.5f));
        if (ok) _hud?.ToggleGuild();
    }

    private void HandleGuildMemberChange(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        string msg = pkt.Ident switch
        {
            var i when i == ProtocolConsts.SM_GUILDADDMEMBEROK => $"已吸纳成员：{text}",
            var i when i == ProtocolConsts.SM_GUILDADDMEMBERFAIL => $"吸纳成员失败：{text}",
            var i when i == ProtocolConsts.SM_GUILDDELMEMBEROK => $"已开除成员：{text}",
            _ => $"成员变更失败：{text}",
        };
        _hud?.AddChat("行会", $"[行会] {msg}", new Color(1f, 0.9f, 0.5f));
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
    }

    private void HandleGuildRankResult(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        _hud?.AddChat("行会", $"[行会] 调整封号失败：{text}", new Color(1f, 0.5f, 0.5f));
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

    // 沙城捐献失败（765，extra=原因）。
    private void HandleDonateResult(ServerPacket pkt)
    {
        string text = DecodeExtraText(pkt.Extra);
        if (string.IsNullOrEmpty(text)) text = pkt.ExtraStr;
        if (string.IsNullOrEmpty(text)) text = "捐献失败";
        _hud?.AddChat("沙城", $"[捐献] {text}", new Color(1f, 0.5f, 0.5f));
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
        if (_entityManager == null) return;
        int uid = pkt.Recog;
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
    }

    private void HandleDealAddItemOk(ServerPacket pkt)
    {
        string itemName = DecodeExtraText(pkt.Extra);
        // 如果己方已经通过拖拽放置，此处确认即可
        _hud?.AddChat("交易", $"[交易] 确认放入物品 [{itemName}] 到交易栏 #{pkt.Param + 1} 格。", new Color(0.8f, 1f, 0.8f));
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
}
