using System;
using System.Collections.Generic;
using System.Text;
using Godot;
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
    public const short SM_TAKEOFF_OK             = 619;  // 脱下装备成功
    public const short SM_EQUIPINFO              = 621;  // 全量 9 槽位装备下发
    public const short SM_WEIGHTCHANGED          = 622;  // 负重同步
    public const short SM_MERCHANTDLG            = 631;  // 商店货架界面下发
    public const short SM_DEALMENU               = 670;  // 交易界面弹出 (ProtocolConsts)
    public const short SM_DEALMENU_IDA           = 673;  // 交易界面弹出 (IDA 0x46A5AA)
    public const short SM_USERSTORAGE_DLG        = 700;  // 仓库保管箱托管界面弹出
    public const short SM_SENDUSERSTATE_INSPECT  = 751;  // 观人/他人状态下发 (IDA 0x46A857)
    public const short SM_OPENGUILDDLG           = 753;  // 打开行会主页 (IDA 0x46A528)
    public const short SM_SENDGUILDMEMBERLIST    = 756;  // 行会成员列表 (IDA 0x46A542)
    public const short SM_GUILDMEMBERLIST_ALT    = 671;  // 行会成员列表 (ProtocolConsts)
    public const short SM_NEEDPASSWORD           = 2014; // 二级安全密码弹窗 (sub_486DF0)

    private readonly HudController? _hud;
    private readonly Mir2Client.Game.EntityManager? _entityManager;
    private readonly GameView? _gameView;

    public ServerMessageRouter(HudController? hud, Mir2Client.Game.EntityManager? em = null, GameView? gv = null)
    {
        _hud = hud;
        _entityManager = em;
        _gameView = gv;
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
            case SM_CLEAROBJECTS:
                _entityManager?.Clear();
                break;
            case SM_ADDITEM:
                HandleAddItem(packet);
                break;
            case SM_DELITEM:
                HandleDelItem(packet);
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
                _hud?.ToggleTrade();
                break;
            case SM_USERSTORAGE_DLG:
                _hud?.ToggleStorage();
                break;
            case SM_OPENGUILDDLG:
                HandleOpenGuildDlg(packet);
                break;
            case SM_NEEDPASSWORD:
                _hud?.OnNeedPassword();
                break;
            case SM_SENDUSERSTATE:
            case SM_SENDUSERSTATE_INSPECT:
                HandleSendUserState(packet);
                break;
            case SM_SENDGUILDMEMBERLIST:
            case SM_GUILDMEMBERLIST_ALT:
                HandleSendGuildMemberList(packet);
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

    private void HandleOpenGuildDlg(ServerPacket pkt)
    {
        string text = pkt.ExtraStr;
        if (!string.IsNullOrEmpty(text) && _hud?.GuildPanel != null)
        {
            if (text.Contains("<Notice>") || text.Contains("公告"))
            {
                var notices = new List<string>();
                var killGuilds = new List<string>();
                var allyGuilds = new List<string>();

                int section = 0;
                var lines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    string trimmed = line.Trim();
                    if (trimmed.Equals("<Notice>", StringComparison.OrdinalIgnoreCase)) { section = 1; continue; }
                    if (trimmed.Equals("<KillGuilds>", StringComparison.OrdinalIgnoreCase)) { section = 2; continue; }
                    if (trimmed.Equals("<AllyGuilds>", StringComparison.OrdinalIgnoreCase)) { section = 3; continue; }

                    switch (section)
                    {
                        case 1: notices.Add(trimmed); break;
                        case 2: killGuilds.Add(trimmed); break;
                        case 3: allyGuilds.Add(trimmed); break;
                        default: notices.Add(trimmed); break;
                    }
                }
                _hud?.GuildPanel.SetHomeText(notices, killGuilds, allyGuilds);
            }
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
            if (parts.Length >= 2)
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

        var type = pkt.Ident switch
        {
            SM_MONSPAWN => Mir2Client.Game.EntityManager.EntityType.Monster,
            SM_NPCSPAWN => Mir2Client.Game.EntityManager.EntityType.Npc,
            _ => Mir2Client.Game.EntityManager.EntityType.Player
        };

        _entityManager.AddEntity(uid, name, type, x, y, dir);
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
        var ent = _entityManager.GetEntity(uid);
        if (ent != null)
        {
            ent.Direction = dir;
        }
    }
}
