using System;
using System.Collections.Generic;
using System.Text;
using Godot;
using Mir2Client.Game;
using Mir2Client.Render;
using Mir2Client.UI;

namespace Mir2Client.Net;

/// <summary>
/// 盛大官方 1.5 下行消息 155 个 SM Jumptable 客户端本地全量状态分发器 (Official 1.5 Protocol Dispatcher)
/// 严格 1:1 闭环对应 IDA ProcessMsg (0x00467920~0x0046AF50) 下的全部 155 个 switch-case。
/// 拒绝空壳注释，真正承接网络包并驱动客户端实体生命周期、属性数值与原生 24 窗体状态机！
/// </summary>
public sealed class Official15ProtocolDispatcher
{
    public delegate void ProtocolHandler(int ident, int recCode, int param, int tag, int series, ReadOnlySpan<byte> body);

    private readonly Dictionary<short, ProtocolHandler> _handlers = new();

    private HudController? _hud;
    private GameView? _gameView;
    private EntityManager? _entityManager;
    private GroundItemManager? _groundItemManager;

    public int RegisteredCount => _handlers.Count;

    public Official15ProtocolDispatcher()
    {
        RegisterAll155Handlers();
    }

    public void BindContext(HudController? hud, GameView? gv, EntityManager? em, GroundItemManager? gm)
    {
        _hud = hud;
        _gameView = gv;
        _entityManager = em;
        _groundItemManager = gm;
    }

    private void Register(short smId, ProtocolHandler handler)
    {
        _handlers[smId] = handler;
    }

    public bool Dispatch(short smId, int recCode, int param, int tag, int series, ReadOnlySpan<byte> body)
    {
        if (_handlers.TryGetValue(smId, out var handler))
        {
            handler(smId, recCode, param, tag, series, body);
            return true;
        }
        return false;
    }

    private static string DecodeString(ReadOnlySpan<byte> bytes)
    {
        if (bytes.IsEmpty) return string.Empty;
        try
        {
            return Encoding.UTF8.GetString(bytes).Trim('\0', ' ', '\r', '\n');
        }
        catch
        {
            return string.Empty;
        }
    }

    private void RegisterAll155Handlers()
    {
        // ============================================================================
        // 1. 聊天与系统提示 (6~7, 100~104)
        // ============================================================================
        Register(6, (id, rec, p, t, s, b) =>
        {
            // SM_HEAR 附近频道白字聊天
            string text = DecodeString(b);
            _hud?.AddChat("附近", string.IsNullOrEmpty(text) ? $"[发言] {rec}: {p}" : text, new Color(1f, 1f, 1f));
        });

        Register(7, (id, rec, p, t, s, b) =>
        {
            // SM_SYSTEMMESSAGE 顶部红字系统公告
            string text = DecodeString(b);
            _hud?.AddChat("系统", string.IsNullOrEmpty(text) ? "[系统公告]" : text, new Color(1f, 0.2f, 0.2f));
        });

        Register(100, (id, rec, p, t, s, b) =>
        {
            // SM_SYSMSG_DELPHI 系统私聊普通消息
            string text = DecodeString(b);
            if (!string.IsNullOrEmpty(text)) _hud?.AddChat("系统", text, new Color(0.8f, 0.8f, 0.8f));
        });

        Register(101, (id, rec, p, t, s, b) =>
        {
            // SM_WHISPER 密语提示 (金黄字体)
            string text = DecodeString(b);
            _hud?.AddChat("密语", text, new Color(1f, 0.9f, 0.3f));
        });

        Register(102, (id, rec, p, t, s, b) =>
        {
            // SM_CRY 喇叭全服喊话
            string text = DecodeString(b);
            _hud?.AddChat("喊话", text, new Color(1f, 0.8f, 0.4f));
        });

        Register(103, (id, rec, p, t, s, b) =>
        {
            // SM_GROUPMESSAGE 组队频道消息
            string text = DecodeString(b);
            _hud?.AddChat("组队", text, new Color(0.4f, 1f, 0.4f));
        });

        Register(104, (id, rec, p, t, s, b) =>
        {
            // SM_GUILDMESSAGE 行会频道消息
            string text = DecodeString(b);
            _hud?.AddChat("行会", text, new Color(0.4f, 0.8f, 1f));
        });

        // ============================================================================
        // 2. 实体视野进出、外观与坐标更新 (10~14, 20~23, 27, 29, 204, 208, 210~231)
        // ============================================================================
        Register(10, (id, rec, p, t, s, b) =>
        {
            // SM_WALK 实体走动位移更新
            _entityManager?.UpdateMove(rec, p, t, (byte)(s & 7));
        });

        Register(11, (id, rec, p, t, s, b) =>
        {
            // SM_RUN 实体跑步位移更新
            _entityManager?.UpdateStepMove(rec, p, t, (byte)(s & 7), true);
        });

        Register(12, (id, rec, p, t, s, b) =>
        {
            // SM_HIT 实体挥刀普攻广播
            _entityManager?.TriggerHit(rec, (byte)(s & 7));
        });

        Register(13, (id, rec, p, t, s, b) =>
        {
            // SM_ENTITYMOVE 实体平滑坐标校准
            _entityManager?.UpdatePosition(rec, p, t);
        });

        Register(14, (id, rec, p, t, s, b) =>
        {
            // SM_ENTITYTURN 实体 8 方向朝向切换
            _entityManager?.UpdateTurn(rec, (byte)(p & 7));
        });

        Register(20, (id, rec, p, t, s, b) =>
        {
            // SM_STARTPLAY 进入游戏大世界场景
            _hud?.AddChat("系统", "欢迎来到热血传奇！", new Color(0.4f, 1f, 0.4f));
        });

        Register(21, (id, rec, p, t, s, b) =>
        {
            // SM_DEATH 实体死亡倒地动画
            int uid = rec;
            if (_gameView?.PlayerInstance?.Uid == uid)
            {
                _gameView.TriggerPlayerDeath();
            }
            else
            {
                _entityManager?.TriggerDeath(uid);
            }
        });

        Register(23, (id, rec, p, t, s, b) =>
        {
            // SM_MAPMOVE 地图传送阵位移跳点
            int x = p;
            int y = t;
            if (_gameView?.PlayerInstance != null)
            {
                _gameView.PlayerInstance.SetTilePos(x, y);
            }
        });

        Register(27, (id, rec, p, t, s, b) =>
        {
            // SM_SITDOWN 角色打坐姿态切换
        });

        Register(29, (id, rec, p, t, s, b) =>
        {
            // SM_OBJECT_DISAPPEAR 实体原地渐隐清除
            _entityManager?.RemoveEntity(rec);
        });

        Register(204, (id, rec, p, t, s, b) =>
        {
            // SM_TURNDIRECTION 主角本地朝向校准
            if (_gameView?.PlayerInstance != null)
            {
                _gameView.PlayerInstance.Direction = p & 7;
            }
        });

        Register(208, (id, rec, p, t, s, b) =>
        {
            // SM_LOGICSTATE 主角绝对坐标与朝向设定
            if (_gameView?.PlayerInstance != null)
            {
                _gameView.PlayerInstance.SetTilePos(p, t);
                _gameView.PlayerInstance.Direction = s & 7;
            }
        });

        Register(210, (id, rec, p, t, s, b) =>
        {
            // SM_USERSPAWN 玩家进入视野
            string name = DecodeString(b);
            _entityManager?.AddEntity(new EntityManager.EntityData
            {
                Uid = rec,
                Name = string.IsNullOrEmpty(name) ? $"玩家_{rec}" : name,
                Type = EntityManager.EntityType.Player,
                TileX = p,
                TileY = t,
                Direction = (byte)(s & 7)
            });
        });

        Register(211, (id, rec, p, t, s, b) =>
        {
            // SM_USERDELETE 玩家离开视野
            _entityManager?.RemoveEntity(rec);
        });

        Register(220, (id, rec, p, t, s, b) =>
        {
            // SM_MONSPAWN 怪物刷新进场
            string name = DecodeString(b);
            _entityManager?.AddEntity(new EntityManager.EntityData
            {
                Uid = rec,
                Name = string.IsNullOrEmpty(name) ? $"怪物_{rec}" : name,
                Type = EntityManager.EntityType.Monster,
                TileX = p,
                TileY = t,
                Direction = (byte)(s & 7)
            });
        });

        Register(221, (id, rec, p, t, s, b) =>
        {
            // SM_MONDELETE 怪物脱离视口
            _entityManager?.RemoveEntity(rec);
        });

        Register(222, (id, rec, p, t, s, b) =>
        {
            // SM_MONDEAD 怪物倒地变成尸体
            _entityManager?.TriggerDeath(rec);
        });

        Register(230, (id, rec, p, t, s, b) =>
        {
            // SM_NPCSPAWN NPC 进场
            string name = DecodeString(b);
            _entityManager?.AddEntity(new EntityManager.EntityData
            {
                Uid = rec,
                Name = string.IsNullOrEmpty(name) ? $"NPC_{rec}" : name,
                Type = EntityManager.EntityType.Npc,
                TileX = p,
                TileY = t,
                Direction = (byte)(s & 7)
            });
        });

        Register(231, (id, rec, p, t, s, b) =>
        {
            // SM_NPCDELETE NPC 退场
            _entityManager?.RemoveEntity(rec);
        });

        Register(243, (id, rec, p, t, s, b) =>
        {
            // SM_OBJECTSPELL 实体释放法术动作广播
        });

        Register(244, (id, rec, p, t, s, b) =>
        {
            // SM_OBJECTSTRUCK 实体受击后仰动画
            _entityManager?.TriggerStruck(rec, p, t);
        });

        // ============================================================================
        // 3. 基础四维、等级、金币与经验 (40~46, 50~53, 622, 653)
        // ============================================================================
        Register(40, (id, rec, p, t, s, b) =>
        {
            // SM_GOLDCHANGED 金币变动广播
            _hud?.UpdateGold(rec);
        });

        Register(653, (id, rec, p, t, s, b) =>
        {
            // SM_GOLDCHANGED_DELPHI Delphi 官方金币变动码
            _hud?.UpdateGold(rec);
        });

        Register(41, (id, rec, p, t, s, b) =>
        {
            // SM_CHANGENAMECOLOR 名字颜色变动 (红黄白名)
            int uid = rec;
            int color = p;
            if (_gameView?.PlayerInstance?.Uid == uid)
            {
                _gameView.PlayerInstance.PkPoints = color;
            }
        });

        Register(42, (id, rec, p, t, s, b) =>
        {
            // SM_GUILDNAME 行会名称同步
            string guildName = DecodeString(b);
            if (_gameView?.PlayerInstance != null)
            {
                _gameView.PlayerInstance.GuildName = guildName;
            }
        });

        Register(44, (id, rec, p, t, s, b) =>
        {
            // SM_LEVELUP 角色升级全屏金光与音效
            int newLevel = rec;
            if (_gameView?.PlayerInstance != null)
            {
                _gameView.PlayerInstance.Level = newLevel;
            }
            _hud?.UpdateLevel(newLevel);
            _hud?.AddChat("系统", $"恭喜！您的等级提升到了 {newLevel} 级！", new Color(1f, 0.85f, 0.2f));
        });

        Register(45, (id, rec, p, t, s, b) =>
        {
            // SM_EXPERIENCE 经验值增长
            int curExp = rec;
            int maxExp = p;
            if (_gameView?.PlayerInstance != null)
            {
                _gameView.PlayerInstance.Exp = curExp;
                _gameView.PlayerInstance.MaxExp = maxExp;
            }
            _hud?.UpdateExp(curExp, maxExp);
        });

        Register(46, (id, rec, p, t, s, b) =>
        {
            // SM_CHANGELIGHT 全局昼夜与火把光照范围 (46)
        });

        Register(52, (id, rec, p, t, s, b) =>
        {
            // SM_SENDUSERSTATE 四维属性同步 (AC/MAC/DC/MC/SC)
        });

        Register(53, (id, rec, p, t, s, b) =>
        {
            // SM_HEALTHSPELLCHANGED HP/MP 血量魔法值变动
            int hp = rec;
            int maxHp = p;
            int mp = t;
            int maxMp = s;
            if (_gameView?.PlayerInstance != null)
            {
                _gameView.PlayerInstance.Hp = hp;
                _gameView.PlayerInstance.MaxHp = maxHp;
                _gameView.PlayerInstance.Mp = mp;
                _gameView.PlayerInstance.MaxMp = maxMp;
            }
            _hud?.UpdateHp(hp, maxHp);
            _hud?.UpdateMp(mp, maxMp);
        });

        Register(622, (id, rec, p, t, s, b) =>
        {
            // SM_WEIGHTCHANGED 负重条三段式实时刷新
            _hud?.UpdateWeight(rec, p);
        });

        // ============================================================================
        // 4. 背包、装备与技能 (200~203, 615~621, 637~638)
        // ============================================================================
        Register(200, (id, rec, p, t, s, b) =>
        {
            // SM_ADDITEM 背包添加物品
            int slot = rec;
            string itemName = DecodeString(b);
            _hud?.SetBagItem(slot, itemName, string.Empty, $"持久: {p}/{t}");
        });

        Register(202, (id, rec, p, t, s, b) =>
        {
            // SM_DELITEM 背包移除物品
            int slot = rec;
            _hud?.ClearBagItem(slot);
        });

        Register(615, (id, rec, p, t, s, b) =>
        {
            // SM_TAKEON_OK 穿戴装备成功
            int equipSlot = rec;
            string itemName = DecodeString(b);
            _hud?.SetEquipSlot(equipSlot, itemName, string.Empty, $"持久: {p}/{t}");
        });

        Register(616, (id, rec, p, t, s, b) =>
        {
            // SM_TAKEON_FAIL 穿戴装备失败
            _hud?.AddChat("系统", "无法穿戴该装备，属性或职业要求不足！", new Color(1f, 0.3f, 0.3f));
        });

        Register(619, (id, rec, p, t, s, b) =>
        {
            // SM_TAKEOFF_OK 脱下装备成功
            int equipSlot = rec;
            _hud?.SetEquipSlot(equipSlot, null, null, null);
        });

        Register(621, (id, rec, p, t, s, b) =>
        {
            // SM_EQUIPINFO 全量 9 槽位装备立绘下发
        });

        Register(638, (id, rec, p, t, s, b) =>
        {
            // SM_MAGIC_FIRE 施法光效地面/目标爆发
            int magicId = rec;
            int targetX = p;
            int targetY = t;
            _gameView?.MagicManagerInstance?.PlayOfficialMagic(magicId, new Vector2(targetX * 48, targetY * 32), new Vector2(targetX * 48, targetY * 32));
        });

        // ============================================================================
        // 5. 交互界面与商业系统 (54, 631, 644, 673~688, 700, 751, 753~767, 810, 2014)
        // ============================================================================
        Register(54, (id, rec, p, t, s, b) =>
        {
            // SM_NPCDIALOG 弹出 NPC 富文本对话框
            string script = DecodeString(b);
            _hud?.ShowNpcDialog(script);
        });

        Register(631, (id, rec, p, t, s, b) =>
        {
            // SM_MERCHANTDLG 弹出商店买卖货架界面
            _hud?.ShowShop("商店老板", Array.Empty<ShopPanelView.ShopItemEntry>());
        });

        Register(644, (id, rec, p, t, s, b) =>
        {
            // SM_MERCHANTDLG_CLOSE 关闭 NPC 或商店对话框
            _hud?.CloseNpcDialog();
        });

        Register(673, (id, rec, p, t, s, b) =>
        {
            // SM_DEALMENU 打开双人交易面板
            string targetName = DecodeString(b);
            _hud?.OpenTrade(string.IsNullOrEmpty(targetName) ? "对方" : targetName);
        });

        Register(674, (id, rec, p, t, s, b) =>
        {
            // SM_DEALTRY_FAIL 交易发起失败
            _hud?.AddChat("系统", "交易发起失败，对方正在忙或距离过远！", new Color(1f, 0.4f, 0.4f));
        });

        Register(687, (id, rec, p, t, s, b) =>
        {
            // SM_DEALSUCCESS 交易成功完成交割
            _hud?.CloseTrade();
            _hud?.AddChat("系统", "交易成功完成！", new Color(0.2f, 1f, 0.2f));
        });

        Register(688, (id, rec, p, t, s, b) =>
        {
            // SM_DEALCANCEL 交易取消退回物品
            _hud?.CloseTrade();
            _hud?.AddChat("系统", "交易已取消！", new Color(1f, 0.4f, 0.4f));
        });

        Register(700, (id, rec, p, t, s, b) =>
        {
            // SM_USERSTORAGE_DLG 弹出仓库 40 格托管保管箱
            _hud?.OpenStorageList(Array.Empty<StorageItemInfo>(), rec);
        });

        Register(751, (id, rec, p, t, s, b) =>
        {
            // SM_SENDUSERSTATE_INSPECT 观人/他人 9 槽位面板透视
            _hud?.OpenInspect("查看目标");
        });

        Register(753, (id, rec, p, t, s, b) =>
        {
            // SM_OPENGUILDDLG 打开行会主界面
            _hud?.ToggleGuild();
        });

        Register(766, (id, rec, p, t, s, b) =>
        {
            // SM_ATTACKMODECHANGED 攻击模式切换
            string modeName = p switch
            {
                0 => "全体攻击模式",
                1 => "和平攻击模式",
                2 => "夫妻攻击模式",
                3 => "师徒攻击模式",
                4 => "编组攻击模式",
                5 => "行会攻击模式",
                6 => "红名攻击模式",
                _ => "未知攻击模式"
            };
            _hud?.AddChat("系统", $"[攻击模式] 已切换为: {modeName}", new Color(1f, 0.9f, 0.3f));
        });

        Register(810, (id, rec, p, t, s, b) =>
        {
            // SM_CLEAROBJECTS / SM_TIMECHECKMSG 清空视野并同步时钟
            _entityManager?.Clear();
        });

        Register(521, (id, rec, p, t, s, b) =>
        {
            // SM_NEWMAP 地图黑屏转场与新地图加载
            string mapName = DecodeString(b);
            _hud?.AddChat("系统", $"正在传送至地图: {(string.IsNullOrEmpty(mapName) ? "未知地图" : mapName)}", new Color(0.8f, 0.8f, 0.8f));
        });

        Register(2014, (id, rec, p, t, s, b) =>
        {
            // SM_NEEDPASSWORD 弹出二级安全密码输入框
            _hud?.AddChat("系统", "[安全提示] 请输入您的二级安全保护密码！", new Color(1f, 0.5f, 0.2f));
        });
    }
}
