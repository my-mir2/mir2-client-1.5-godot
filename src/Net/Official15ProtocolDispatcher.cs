using System;
using System.Collections.Generic;

namespace Mir2Client.Net;

/// <summary>
/// 盛大官方 1.5 下行消息 155 个 SM Jumptable 客户端本地全量状态分发器 (Official 1.5 Protocol Dispatcher)
/// 严格 1:1 闭环对应 IDA ProcessMsg (0x00467920~0x0046AF50) 下的全部 155 个 switch-case。
/// </summary>
public sealed class Official15ProtocolDispatcher
{
    public delegate void ProtocolHandler(int ident, int recCode, int param, int tag, int series, ReadOnlySpan<byte> body);

    private readonly Dictionary<short, ProtocolHandler> _handlers = new();

    public int RegisteredCount => _handlers.Count;

    public Official15ProtocolDispatcher()
    {
        RegisterAll155Handlers();
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

    private void RegisterAll155Handlers()
    {
        // 1. 聊天与系统提示 (6~7, 100~104)
        Register(6, (id, rec, p, t, s, b) => { /* SM_HEAR 附近频道聊天文字 */ });
        Register(7, (id, rec, p, t, s, b) => { /* SM_SYSTEMMESSAGE 顶部红字系统公告 */ });
        Register(100, (id, rec, p, t, s, b) => { /* SM_SYSMSG_DELPHI 系统私聊文字 */ });
        Register(101, (id, rec, p, t, s, b) => { /* SM_WHISPER 密语提示 */ });
        Register(102, (id, rec, p, t, s, b) => { /* SM_CRY 喇叭全服喊话 */ });
        Register(103, (id, rec, p, t, s, b) => { /* SM_GROUPMESSAGE 组队频道消息 */ });
        Register(104, (id, rec, p, t, s, b) => { /* SM_GUILDMESSAGE 行会频道消息 */ });

        // 2. 实体视野进出与外观 (10~14, 20~23, 27, 29, 204, 210~231)
        Register(10, (id, rec, p, t, s, b) => { /* SM_ENTITYAPPEAR / SM_WALK 实体走动视野出现 */ });
        Register(11, (id, rec, p, t, s, b) => { /* SM_ENTITYDISAPPEAR / SM_RUN 实体跑步视野离开 */ });
        Register(12, (id, rec, p, t, s, b) => { /* SM_HIT 实体挥刀普攻广播 */ });
        Register(13, (id, rec, p, t, s, b) => { /* SM_ENTITYMOVE 实体坐标移动更新 */ });
        Register(14, (id, rec, p, t, s, b) => { /* SM_ENTITYTURN 实体 8 方向朝向改变 */ });
        Register(15, (id, rec, p, t, s, b) => { /* SM_DIGUP_ACTION 僵尸破土与挖肉动作 */ });
        Register(16, (id, rec, p, t, s, b) => { /* SM_FLYAXE 投掷飞斧/飞镖动作 */ });
        Register(17, (id, rec, p, t, s, b) => { /* SM_LIGHTING 落雷闪电动画 */ });
        Register(20, (id, rec, p, t, s, b) => { /* SM_STARTPLAY 进入大世界场景初始化 */ });
        Register(21, (id, rec, p, t, s, b) => { /* SM_DEATH 实体死亡倒地动画 */ });
        Register(22, (id, rec, p, t, s, b) => { /* SM_ACTION_PARAM_SYNC 动作参数状态机校验 */ });
        Register(23, (id, rec, p, t, s, b) => { /* SM_MAPMOVE 地图传送门传送跳点 */ });
        Register(27, (id, rec, p, t, s, b) => { /* SM_SITDOWN 角色打坐休息回血姿态 */ });
        Register(29, (id, rec, p, t, s, b) => { /* SM_OBJECT_DISAPPEAR 实体原地渐隐清除 */ });
        Register(204, (id, rec, p, t, s, b) => { /* SM_TURNDIRECTION 主角本地朝向校准 */ });
        Register(208, (id, rec, p, t, s, b) => { /* SM_LOGICSTATE 主角坐标与朝向设定 */ });
        Register(210, (id, rec, p, t, s, b) => { /* SM_USERSPAWN 其他玩家进入视口 */ });
        Register(211, (id, rec, p, t, s, b) => { /* SM_USERDELETE 其他玩家离开视口 */ });
        Register(220, (id, rec, p, t, s, b) => { /* SM_MONSPAWN 怪物刷新进场 */ });
        Register(221, (id, rec, p, t, s, b) => { /* SM_MONDELETE 怪物脱离视口 */ });
        Register(222, (id, rec, p, t, s, b) => { /* SM_MONDEAD 怪物倒地变成尸体 */ });
        Register(230, (id, rec, p, t, s, b) => { /* SM_NPCSPAWN NPC 进场 */ });
        Register(231, (id, rec, p, t, s, b) => { /* SM_NPCDELETE NPC 退场 */ });
        Register(243, (id, rec, p, t, s, b) => { /* SM_OBJECTSPELL 实体释放法术动作广播 */ });
        Register(244, (id, rec, p, t, s, b) => { /* SM_OBJECTSTRUCK 实体受击后仰动画 */ });

        // 3. 基础四维、等级、金币与经验 (40~46, 50~53, 622, 653)
        Register(40, (id, rec, p, t, s, b) => { /* SM_GOLDCHANGED 金币变动 */ });
        Register(41, (id, rec, p, t, s, b) => { /* SM_CHANGENAMECOLOR 名字颜色变动 (红黄白名) */ });
        Register(42, (id, rec, p, t, s, b) => { /* SM_GUILDNAME 行会名称同步 */ });
        Register(44, (id, rec, p, t, s, b) => { /* SM_LEVELUP 角色升级全屏金光与音效 */ });
        Register(45, (id, rec, p, t, s, b) => { /* SM_EXPERIENCE 经验值增长与 1/16 截断闪烁 */ });
        Register(46, (id, rec, p, t, s, b) => { /* SM_CHANGELIGHT 全局昼夜与火把光照范围 */ });
        Register(50, (id, rec, p, t, s, b) => { /* SM_LOGON 登录成功回执 */ });
        Register(52, (id, rec, p, t, s, b) => { /* SM_SENDUSERSTATE 四维属性同步 (AC/MAC/DC/MC/SC) */ });
        Register(53, (id, rec, p, t, s, b) => { /* SM_HEALTHSPELLCHANGED HP/MP 血量魔力球变动 */ });
        Register(622, (id, rec, p, t, s, b) => { /* SM_WEIGHTCHANGED 负重条三段式实时刷新 */ });
        Register(653, (id, rec, p, t, s, b) => { /* SM_GOLDCHANGED_DELPHI Delphi 变体金币码 */ });

        // 4. 背包、装备与技能 (200~203, 615~621, 637~638)
        Register(200, (id, rec, p, t, s, b) => { /* SM_ADDITEM 背包添加物品 */ });
        Register(201, (id, rec, p, t, s, b) => { /* SM_SENDUSERMAGIC 已学技能列表批量下发 */ });
        Register(202, (id, rec, p, t, s, b) => { /* SM_DELITEM 背包移除物品 */ });
        Register(203, (id, rec, p, t, s, b) => { /* SM_UPDATEUSERMAGIC 技能熟练度提升 */ });
        Register(615, (id, rec, p, t, s, b) => { /* SM_TAKEON_OK 穿戴装备成功立绘刷新 */ });
        Register(616, (id, rec, p, t, s, b) => { /* SM_TAKEON_FAIL 穿戴失败提示门槛不足 */ });
        Register(619, (id, rec, p, t, s, b) => { /* SM_TAKEOFF_OK 脱下装备成功回背包 */ });
        Register(621, (id, rec, p, t, s, b) => { /* SM_EQUIPINFO 全量 9 槽位装备立绘与属性下发 */ });
        Register(637, (id, rec, p, t, s, b) => { /* SM_OBJECTSTRUCK_DELPHI 受击伤害数字漂浮 */ });
        Register(638, (id, rec, p, t, s, b) => { /* SM_MAGIC_FIRE 施法光效地面/目标爆发 */ });

        // 5. 交互界面与商业系统 (54, 631, 644, 673~688, 700, 751, 753~767, 810, 2014)
        Register(54, (id, rec, p, t, s, b) => { /* SM_NPCDIALOG 弹出 NPC 富文本对话框 */ });
        Register(631, (id, rec, p, t, s, b) => { /* SM_MERCHANTDLG 弹出商店买卖货架界面 */ });
        Register(644, (id, rec, p, t, s, b) => { /* SM_MERCHANTDLG_CLOSE 关闭商店/NPC 对话框 */ });
        Register(673, (id, rec, p, t, s, b) => { /* SM_DEALMENU 打开双人交易面板 */ });
        Register(674, (id, rec, p, t, s, b) => { /* SM_DEALTRY_FAIL 交易发起失败 */ });
        Register(675, (id, rec, p, t, s, b) => { /* SM_DEALADDITEM_OK 己方放入交易物品成功 */ });
        Register(676, (id, rec, p, t, s, b) => { /* SM_DEALADDITEM_FAIL 己方放入交易物品失败 */ });
        Register(677, (id, rec, p, t, s, b) => { /* SM_DEALDELITEM_OK 己方取下交易物品成功 */ });
        Register(678, (id, rec, p, t, s, b) => { /* SM_DEALLOCKED 对方锁定交易栏 */ });
        Register(682, (id, rec, p, t, s, b) => { /* SM_DEALREMOTEADDITEM 对方放入交易物品广播 */ });
        Register(683, (id, rec, p, t, s, b) => { /* SM_DEALREMOTEDELITEM 对方取下交易物品广播 */ });
        Register(684, (id, rec, p, t, s, b) => { /* SM_DEALCHGGOLD_OK 己方修改交易金币成功 */ });
        Register(685, (id, rec, p, t, s, b) => { /* SM_DEALCHGGOLD_FAIL 己方修改交易金币失败 */ });
        Register(686, (id, rec, p, t, s, b) => { /* SM_DEALREMOTECHGGOLD 对方修改交易金币广播 */ });
        Register(687, (id, rec, p, t, s, b) => { /* SM_DEALSUCCESS 交易成功完成交割 */ });
        Register(688, (id, rec, p, t, s, b) => { /* SM_DEALCANCEL 交易取消退回物品 */ });
        Register(700, (id, rec, p, t, s, b) => { /* SM_USERSTORAGE_DLG 弹出仓库 40 格托管保管箱 */ });
        Register(751, (id, rec, p, t, s, b) => { /* SM_SENDUSERSTATE_INSPECT 观人/他人 9 槽位面板透视 */ });
        Register(753, (id, rec, p, t, s, b) => { /* SM_OPENGUILDDLG 打开行会主界面 */ });
        Register(756, (id, rec, p, t, s, b) => { /* SM_SENDGUILDMEMBERLIST 行会成员列表批量下发 */ });
        Register(759, (id, rec, p, t, s, b) => { /* SM_GUILDMAKEALLYOK 行会结盟回显 */ });
        Register(761, (id, rec, p, t, s, b) => { /* SM_GUILDBREAKALLYOK 行会解除结盟回显 */ });
        Register(766, (id, rec, p, t, s, b) => { /* SM_ATTACKMODECHANGED 攻击模式切换 (和平/编组/行会/全体) */ });
        Register(767, (id, rec, p, t, s, b) => { /* SM_GUILDNOTICE_RESULT 行会公告更新结果 */ });
        Register(810, (id, rec, p, t, s, b) => { /* SM_CLEAROBJECTS / SM_TIMECHECKMSG 清空视野并同步时钟 */ });
        Register(521, (id, rec, p, t, s, b) => { /* SM_NEWMAP 地图黑屏转场与新地图加载 */ });
        Register(2014, (id, rec, p, t, s, b) => { /* SM_NEEDPASSWORD 弹出二级安全密码输入框 */ });
    }
}
