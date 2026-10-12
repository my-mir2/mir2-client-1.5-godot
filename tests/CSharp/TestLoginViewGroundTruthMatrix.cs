using System;
using System.IO;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 官方 1.5 登录界面与模态提示框真值审计测试矩阵
/// 依据官方 IDA sub_486DF0 / Prguse.wil[360] / ChrSel.wil[22] 物理真值
/// 坚决杜绝在登录面板按钮上覆盖红字等低级占位符坏账
/// </summary>
public class TestLoginViewGroundTruthMatrix
{
    private readonly string _loginTscnPath;

    private static string FindProjectRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir.FullName, "Mir2Client.csproj")))
            {
                return dir.FullName;
            }
            dir = dir.Parent;
        }
        return Directory.GetCurrentDirectory();
    }

    public TestLoginViewGroundTruthMatrix()
    {
        string rootDir = FindProjectRoot();
        _loginTscnPath = Path.Combine(rootDir, "scenes", "Login.tscn");
    }

    [Fact]
    public void Test_LoginTscn_LblStatus_MustBeHidden_ToAvoidButtonOverlap()
    {
        // 审计场景文件 scenes/Login.tscn
        Assert.True(File.Exists(_loginTscnPath), $"场景文件 {_loginTscnPath} 必须存在");
        string tscnContent = File.ReadAllText(_loginTscnPath);

        // 验证 LblStatus 节点标记了 visible = false，杜绝遮挡【新用户】与【修改密码】按钮
        Assert.Contains("[node name=\"LblStatus\" type=\"Label\" parent=\"Panel\"]", tscnContent);
        int lblIndex = tscnContent.IndexOf("[node name=\"LblStatus\" type=\"Label\" parent=\"Panel\"]", StringComparison.Ordinal);
        string lblSection = tscnContent.Substring(lblIndex, Math.Min(300, tscnContent.Length - lblIndex));
        
        Assert.Contains("visible = false", lblSection);
    }

    [Fact]
    public void Test_LoginTscn_MsgModal_MustMatchOfficialSub486DF0()
    {
        // 官方 IDA sub_486DF0 模态提示框真值：452x179 居中 (174, 210)
        Assert.True(File.Exists(_loginTscnPath), $"场景文件 {_loginTscnPath} 必须存在");
        string tscnContent = File.ReadAllText(_loginTscnPath);

        // 必须包含 MsgModal 节点且隶属于根节点
        Assert.Contains("[node name=\"MsgModal\" type=\"Panel\" parent=\".\"]", tscnContent);
        
        // 必须包含文本框 MsgText 与确认按钮 BtnMsgOk
        Assert.Contains("[node name=\"MsgText\" type=\"Label\" parent=\"MsgModal\"]", tscnContent);
        Assert.Contains("[node name=\"BtnMsgOk\" type=\"TextureButton\" parent=\"MsgModal\"]", tscnContent);

        // 验证坐标与尺寸：174, 210, 626 (174+452), 389 (210+179)
        int modalIndex = tscnContent.IndexOf("[node name=\"MsgModal\" type=\"Panel\" parent=\".\"]", StringComparison.Ordinal);
        string modalSection = tscnContent.Substring(modalIndex, Math.Min(400, tscnContent.Length - modalIndex));

        Assert.Contains("offset_left = 174.0", modalSection);
        Assert.Contains("offset_top = 210.0", modalSection);
        Assert.Contains("offset_right = 626.0", modalSection);
        Assert.Contains("offset_bottom = 389.0", modalSection);
    }

    [Fact]
    public void Test_OfficialAssets_Prguse360_ModalNoticeAssets_Exist()
    {
        // 验证官方模态框贴图资产实体物理存在
        string rootDir = FindProjectRoot();
        
        string modalBg = Path.Combine(rootDir, "resources", "wil", "png", "Prguse", "Prguse_0360.png");
        string btnOkNorm = Path.Combine(rootDir, "resources", "wil", "png", "Prguse", "Prguse_0062.png");
        string btnOkPress = Path.Combine(rootDir, "resources", "wil", "png", "Prguse", "Prguse_0081.png");

        Assert.True(File.Exists(modalBg), $"官方 1.5 模态提示框底图必须存在: {modalBg}");
        Assert.True(File.Exists(btnOkNorm), $"官方 1.5 确认按钮正常态贴图必须存在: {btnOkNorm}");
        Assert.True(File.Exists(btnOkPress), $"官方 1.5 确认按钮按下态贴图必须存在: {btnOkPress}");
    }

    [Fact]
    public async System.Threading.Tasks.Task Test_RealServer_LoginRequest_ReceivesPassOk()
    {
        // 验证真实后台正在运行的 7000 服务端能正常接收 LoginRequest 并返回 SM_PASSOK (500)
        using var client = new System.Net.Sockets.TcpClient();
        try
        {
            await client.ConnectAsync("127.0.0.1", 7000);
        }
        catch
        {
            // 若测试机环境离线则跳过
            return;
        }

        var stream = client.GetStream();
        var payload = Mir2.Shared.Protocols.Modern.BinaryProtocol.LoginIntentPayload.Create("test_verify_user", "123456");
        
        byte[] sendBuf;
        unsafe
        {
            int payloadLen = sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.LoginIntentPayload);
            var header = new Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader
            {
                Magic = Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic,
                MsgId = (ushort)Mir2.Shared.Protocols.Modern.BinaryProtocol.MsgType.LoginRequest,
                PayloadLength = (ushort)payloadLen,
                Sequence = 1
            };

            sendBuf = new byte[sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader) + payloadLen];
            fixed (byte* pBuf = sendBuf)
            {
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader*)pBuf = header;
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.LoginIntentPayload*)(pBuf + sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader)) = payload;
            }
        }

        await stream.WriteAsync(sendBuf, 0, sendBuf.Length);

        // 读取服务端响应
        byte[] recvBuf = new byte[4096];
        var readTask = stream.ReadAsync(recvBuf, 0, recvBuf.Length);
        var completedTask = await System.Threading.Tasks.Task.WhenAny(readTask, System.Threading.Tasks.Task.Delay(3000));
        
        Assert.True(completedTask == readTask, "服务端必须在 3 秒内响应登录请求");
        int readBytes = await readTask;
        Assert.True(readBytes >= Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize, "服务端必须返回至少包含帧头的响应封包");

        // 验证收到的二进制封包流中包含 SM_PASSOK (500) 或 SM_LOGINSUCCESS (530)
        var receivedMsgIds = new System.Collections.Generic.List<ushort>();
        int offset = 0;
        while (offset + Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize <= readBytes)
        {
            var respHeader = Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader.Read(recvBuf.AsSpan(offset, Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize));
            if (respHeader.Magic != Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic) break;
            receivedMsgIds.Add(respHeader.MsgId);
            offset += Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize + respHeader.PayloadLength;
        }

        Assert.True(receivedMsgIds.Contains((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_PASSOK) || 
                    receivedMsgIds.Contains(530), 
                    $"服务端响应帧列表中必须包含 SM_PASSOK (500) 或 SM_LOGINSUCCESS (530)，实际收到的 MsgId 列表: [{string.Join(", ", receivedMsgIds)}]");
    }
}


