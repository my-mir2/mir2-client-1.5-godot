using Godot;
using Mir2Client.UI;
using Mir2Client.Utils;

namespace Mir2Client.Net;

/// <summary>
/// 服务端消息 → 界面的最小分发骨架。
/// 消息号全部取自官方二进制分发器 (IDA 0x467920 jumptable,已逐一核对 case 地址),
/// 与 ProtocolConsts.cs 中部分旧常量不一致 (如 SM_DEALMENU 旧值 670,官方为 673),此处以 IDA 为准。
/// 目前只接入"打开窗体"类消息;带 extra 的数据解析 (商品/成员/观察结构) 需按官方格式逐个实证后再接。
/// </summary>
public sealed class ServerMessageRouter
{
    // IDA 分发器 case 地址: 673→0x46A5AA(sub_4915EC 开交易), 700→sub_46DAE8(开仓库托管),
    // 751→0x46A857(sub_46EB18 观察), 753→0x46A528(sub_46E0B8 开行会), 756→0x46A542(sub_46E560 成员列表)
    public const short SM_DEALMENU = 673;
    public const short SM_USERSTORAGE_DLG = 700;
    public const short SM_SENDUSERSTATE = 751;
    public const short SM_OPENGUILDDLG = 753;
    public const short SM_SENDGUILDMEMBERLIST = 756;

    private readonly HudController _hud;

    public ServerMessageRouter(HudController hud) { _hud = hud; }

    public void OnPacket(short smCode, byte[] pkt)
    {
        switch (smCode)
        {
            case SM_DEALMENU:
                _hud.ToggleTrade();
                break;
            case SM_USERSTORAGE_DLG:
                _hud.ToggleStorage();
                break;
            case SM_OPENGUILDDLG:
                _hud.ToggleGuild();
                break;
            case SM_SENDUSERSTATE:
            case SM_SENDGUILDMEMBERLIST:
                GD.Print($"[Router] SM_{smCode} 已收到,extra 格式尚未按官方实证解析,暂不驱动界面");
                break;
        }
    }
}
