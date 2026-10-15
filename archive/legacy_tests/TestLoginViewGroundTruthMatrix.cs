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

    [Fact]
    public async System.Threading.Tasks.Task Test_RealServer_RegisterAccount_SuccessAndDuplicate()
    {
        using var client = new System.Net.Sockets.TcpClient();
        try { await client.ConnectAsync("127.0.0.1", 7000); } catch { return; }
        var stream = client.GetStream();

        string newAcc = $"u_{System.Environment.TickCount64 % 1000000}";
        var payload = Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateAccountPayload.Create(newAcc, "pass123");
        byte[] sendBuf;
        unsafe
        {
            int payloadLen = sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateAccountPayload);
            var header = new Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader
            {
                Magic = Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic,
                MsgId = (ushort)Mir2.Shared.Protocols.Modern.BinaryProtocol.MsgType.CreateAccountRequest,
                PayloadLength = (ushort)payloadLen,
                Sequence = 1
            };
            sendBuf = new byte[sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader) + payloadLen];
            fixed (byte* pBuf = sendBuf)
            {
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader*)pBuf = header;
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateAccountPayload*)(pBuf + sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader)) = payload;
            }
        }

        await stream.WriteAsync(sendBuf, 0, sendBuf.Length);
        byte[] recvBuf = new byte[4096];
        int readBytes = await stream.ReadAsync(recvBuf, 0, recvBuf.Length);
        Assert.True(readBytes >= Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize);

        var respHeader = Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader.Read(recvBuf.AsSpan(0, Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize));
        Assert.Equal((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_NEWID_SUCCESS, respHeader.MsgId);

        // 再次注册相同账号，必须收到 SM_NEWID_FAIL (505)
        await stream.WriteAsync(sendBuf, 0, sendBuf.Length);
        readBytes = await stream.ReadAsync(recvBuf, 0, recvBuf.Length);
        Assert.True(readBytes >= Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize);
        respHeader = Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader.Read(recvBuf.AsSpan(0, Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize));
        Assert.Equal((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_NEWID_FAIL, respHeader.MsgId);
    }

    [Fact]
    public async System.Threading.Tasks.Task Test_RealServer_ChangePassword_SuccessAndWrongOldPass()
    {
        using var client = new System.Net.Sockets.TcpClient();
        try { await client.ConnectAsync("127.0.0.1", 7000); } catch { return; }
        var stream = client.GetStream();

        string acc = $"chg_{System.Environment.TickCount64 % 1000000}";
        // 1. 注册账号
        var regPayload = Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateAccountPayload.Create(acc, "oldPass123");
        byte[] regBuf;
        unsafe
        {
            int pLen = sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateAccountPayload);
            var hdr = new Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader
            {
                Magic = Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic,
                MsgId = (ushort)Mir2.Shared.Protocols.Modern.BinaryProtocol.MsgType.CreateAccountRequest,
                PayloadLength = (ushort)pLen,
                Sequence = 1
            };
            regBuf = new byte[sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader) + pLen];
            fixed (byte* pBuf = regBuf)
            {
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader*)pBuf = hdr;
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateAccountPayload*)(pBuf + sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader)) = regPayload;
            }
        }
        await stream.WriteAsync(regBuf, 0, regBuf.Length);
        byte[] recvBuf = new byte[4096];
        await stream.ReadAsync(recvBuf, 0, recvBuf.Length);

        // 2. 正确旧密码修改为新密码
        var chgPayload = Mir2.Shared.Protocols.Modern.BinaryProtocol.ChangePasswordPayload.Create(acc, "oldPass123", "newPass456");
        byte[] chgBuf;
        unsafe
        {
            int pLen = sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.ChangePasswordPayload);
            var hdr = new Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader
            {
                Magic = Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic,
                MsgId = (ushort)Mir2.Shared.Protocols.Modern.BinaryProtocol.MsgType.ChangePasswordRequest,
                PayloadLength = (ushort)pLen,
                Sequence = 2
            };
            chgBuf = new byte[sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader) + pLen];
            fixed (byte* pBuf = chgBuf)
            {
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader*)pBuf = hdr;
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.ChangePasswordPayload*)(pBuf + sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader)) = chgPayload;
            }
        }
        await stream.WriteAsync(chgBuf, 0, chgBuf.Length);
        int readBytes = await stream.ReadAsync(recvBuf, 0, recvBuf.Length);
        var respHeader = Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader.Read(recvBuf.AsSpan(0, Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize));
        Assert.Equal((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_CHANGEPASS_OK, respHeader.MsgId);

        // 3. 使用错误旧密码修改 -> 收到 SM_CHANGEPASS_FAIL (503)
        var wrongPayload = Mir2.Shared.Protocols.Modern.BinaryProtocol.ChangePasswordPayload.Create(acc, "wrongOld", "failPass");
        byte[] wrongBuf;
        unsafe
        {
            int pLen = sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.ChangePasswordPayload);
            var hdr = new Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader
            {
                Magic = Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic,
                MsgId = (ushort)Mir2.Shared.Protocols.Modern.BinaryProtocol.MsgType.ChangePasswordRequest,
                PayloadLength = (ushort)pLen,
                Sequence = 3
            };
            wrongBuf = new byte[sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader) + pLen];
            fixed (byte* pBuf = wrongBuf)
            {
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader*)pBuf = hdr;
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.ChangePasswordPayload*)(pBuf + sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader)) = wrongPayload;
            }
        }
        await stream.WriteAsync(wrongBuf, 0, wrongBuf.Length);
        readBytes = await stream.ReadAsync(recvBuf, 0, recvBuf.Length);
        respHeader = Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader.Read(recvBuf.AsSpan(0, Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize));
        Assert.Equal((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_CHANGEPASS_FAIL, respHeader.MsgId);
    }

    [Fact]
    public async System.Threading.Tasks.Task Test_RealServer_CreateChar_QueryChar_DeleteChar_Lifecycle()
    {
        using var client = new System.Net.Sockets.TcpClient();
        try { await client.ConnectAsync("127.0.0.1", 7000); } catch { return; }
        var stream = client.GetStream();

        string acc = $"chr_{System.Environment.TickCount64 % 1000000}";
        // 1. 注册账号
        var regPayload = Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateAccountPayload.Create(acc, "pwd123");
        byte[] regBuf;
        unsafe
        {
            int pLen = sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateAccountPayload);
            var hdr = new Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader
            {
                Magic = Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic,
                MsgId = (ushort)Mir2.Shared.Protocols.Modern.BinaryProtocol.MsgType.CreateAccountRequest,
                PayloadLength = (ushort)pLen,
                Sequence = 1
            };
            regBuf = new byte[sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader) + pLen];
            fixed (byte* pBuf = regBuf)
            {
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader*)pBuf = hdr;
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateAccountPayload*)(pBuf + sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader)) = regPayload;
            }
        }
        await stream.WriteAsync(regBuf, 0, regBuf.Length);
        byte[] recvBuf = new byte[8192];
        await stream.ReadAsync(recvBuf, 0, recvBuf.Length);

        // 2. 登录账号
        var loginPayload = Mir2.Shared.Protocols.Modern.BinaryProtocol.LoginIntentPayload.Create(acc, "pwd123");
        byte[] loginBuf;
        unsafe
        {
            int pLen = sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.LoginIntentPayload);
            var hdr = new Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader
            {
                Magic = Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic,
                MsgId = (ushort)Mir2.Shared.Protocols.Modern.BinaryProtocol.MsgType.LoginRequest,
                PayloadLength = (ushort)pLen,
                Sequence = 2
            };
            loginBuf = new byte[sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader) + pLen];
            fixed (byte* pBuf = loginBuf)
            {
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader*)pBuf = hdr;
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.LoginIntentPayload*)(pBuf + sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader)) = loginPayload;
            }
        }
        await stream.WriteAsync(loginBuf, 0, loginBuf.Length);
        await stream.ReadAsync(recvBuf, 0, recvBuf.Length);

        // 3. 初始查角（应为 0 个角色）
        byte[] queryBuf = new byte[Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize];
        unsafe
        {
            fixed (byte* p = queryBuf)
            {
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader*)p = new Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader
                {
                    Magic = Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic,
                    MsgId = (ushort)Mir2.Shared.Protocols.Modern.BinaryProtocol.MsgType.IntentQueryChar,
                    PayloadLength = 0,
                    Sequence = 3
                };
            }
        }
        await stream.WriteAsync(queryBuf, 0, queryBuf.Length);
        int readBytes = await stream.ReadAsync(recvBuf, 0, recvBuf.Length);
        var respHeader = Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader.Read(recvBuf.AsSpan(0, Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize));
        Assert.Equal((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_QUERYCHAR_OK, respHeader.MsgId);

        // 4. 创建角色 1: 烈焰法神 (职业 1 法师, 性别 0 男)
        string c1Name = $"法{Guid.NewGuid().ToString("N")[..4]}";
        var c1Payload = Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateCharIntentPayload.Create(c1Name, 1, 0, 1);
        byte[] c1Buf;
        unsafe
        {
            int pLen = sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateCharIntentPayload);
            c1Buf = new byte[Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize + pLen];
            fixed (byte* p = c1Buf)
            {
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader*)p = new Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader
                {
                    Magic = Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic,
                    MsgId = (ushort)Mir2.Shared.Protocols.Modern.BinaryProtocol.MsgType.CreateCharRequest,
                    PayloadLength = (ushort)pLen,
                    Sequence = 4
                };
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateCharIntentPayload*)(p + Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize) = c1Payload;
            }
        }
        await stream.WriteAsync(c1Buf, 0, c1Buf.Length);
        readBytes = await stream.ReadAsync(recvBuf, 0, recvBuf.Length);
        respHeader = Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader.Read(recvBuf.AsSpan(0, Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize));
        Assert.Equal((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_NEWCHR_SUCCESS, respHeader.MsgId);

        // 5. 创建同名角色 -> 必须返回重名失败 SM_NEWCHAR_FAIL_DELPHI (522)
        await stream.WriteAsync(c1Buf, 0, c1Buf.Length);
        readBytes = await stream.ReadAsync(recvBuf, 0, recvBuf.Length);
        respHeader = Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader.Read(recvBuf.AsSpan(0, Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize));
        Assert.Equal((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_NEWCHAR_FAIL_DELPHI, respHeader.MsgId);

        // 6. 创建角色 2: 无极道尊 (职业 2 道士, 性别 1 女)
        string c2Name = $"道{Guid.NewGuid().ToString("N")[..4]}";
        var c2Payload = Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateCharIntentPayload.Create(c2Name, 2, 1, 1);
        byte[] c2Buf;
        unsafe
        {
            int pLen = sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateCharIntentPayload);
            c2Buf = new byte[Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize + pLen];
            fixed (byte* p = c2Buf)
            {
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader*)p = new Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader
                {
                    Magic = Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic,
                    MsgId = (ushort)Mir2.Shared.Protocols.Modern.BinaryProtocol.MsgType.CreateCharRequest,
                    PayloadLength = (ushort)pLen,
                    Sequence = 5
                };
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateCharIntentPayload*)(p + Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize) = c2Payload;
            }
        }
        await stream.WriteAsync(c2Buf, 0, c2Buf.Length);
        readBytes = await stream.ReadAsync(recvBuf, 0, recvBuf.Length);
        respHeader = Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader.Read(recvBuf.AsSpan(0, Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize));
        Assert.Equal((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_NEWCHR_SUCCESS, respHeader.MsgId);

        // 7. 创建角色 3 -> 必须返回角色已满失败 SM_NEWCHAR_FAIL_DELPHI (522)
        string c3Name = $"狂{Guid.NewGuid().ToString("N")[..4]}";
        var c3Payload = Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateCharIntentPayload.Create(c3Name, 0, 0, 1);
        byte[] c3Buf;
        unsafe
        {
            int pLen = sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateCharIntentPayload);
            c3Buf = new byte[Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize + pLen];
            fixed (byte* p = c3Buf)
            {
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader*)p = new Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader
                {
                    Magic = Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic,
                    MsgId = (ushort)Mir2.Shared.Protocols.Modern.BinaryProtocol.MsgType.CreateCharRequest,
                    PayloadLength = (ushort)pLen,
                    Sequence = 6
                };
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateCharIntentPayload*)(p + Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize) = c3Payload;
            }
        }
        await stream.WriteAsync(c3Buf, 0, c3Buf.Length);
        readBytes = await stream.ReadAsync(recvBuf, 0, recvBuf.Length);
        respHeader = Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader.Read(recvBuf.AsSpan(0, Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize));
        Assert.Equal((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_NEWCHAR_FAIL_DELPHI, respHeader.MsgId);

        // 8. 删除角色 1 (c1Name)
        var delPayload = Mir2.Shared.Protocols.Modern.BinaryProtocol.DeleteCharIntentPayload.Create(0, c1Name);
        byte[] delBuf;
        unsafe
        {
            int pLen = sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.DeleteCharIntentPayload);
            delBuf = new byte[Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize + pLen];
            fixed (byte* p = delBuf)
            {
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader*)p = new Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader
                {
                    Magic = Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic,
                    MsgId = (ushort)Mir2.Shared.Protocols.Modern.BinaryProtocol.MsgType.IntentDeleteChar,
                    PayloadLength = (ushort)pLen,
                    Sequence = 7
                };
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.DeleteCharIntentPayload*)(p + Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize) = delPayload;
            }
        }
        await stream.WriteAsync(delBuf, 0, delBuf.Length);
        readBytes = await stream.ReadAsync(recvBuf, 0, recvBuf.Length);
        respHeader = Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader.Read(recvBuf.AsSpan(0, Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize));
        Assert.Equal((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_DELCHR_SUCCESS, respHeader.MsgId);
    }

    [Fact]
    public async System.Threading.Tasks.Task Test_RealServer_SelectChar_WorldEntry_HandshakeLifecycle()
    {
        using var client = new System.Net.Sockets.TcpClient();
        try { await client.ConnectAsync("127.0.0.1", 7000); } catch { return; }
        var stream = client.GetStream();

        string acc = $"wld_{System.Environment.TickCount64 % 1000000}";
        // 1. 注册账号
        var regPayload = Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateAccountPayload.Create(acc, "pwd123");
        byte[] regBuf;
        unsafe
        {
            int pLen = sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateAccountPayload);
            var hdr = new Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader
            {
                Magic = Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic,
                MsgId = (ushort)Mir2.Shared.Protocols.Modern.BinaryProtocol.MsgType.CreateAccountRequest,
                PayloadLength = (ushort)pLen,
                Sequence = 1
            };
            regBuf = new byte[sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader) + pLen];
            fixed (byte* pBuf = regBuf)
            {
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader*)pBuf = hdr;
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateAccountPayload*)(pBuf + sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader)) = regPayload;
            }
        }
        await stream.WriteAsync(regBuf, 0, regBuf.Length);
        byte[] recvBuf = new byte[16384];
        await stream.ReadAsync(recvBuf, 0, recvBuf.Length);

        // 2. 登录账号
        var loginPayload = Mir2.Shared.Protocols.Modern.BinaryProtocol.LoginIntentPayload.Create(acc, "pwd123");
        byte[] loginBuf;
        unsafe
        {
            int pLen = sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.LoginIntentPayload);
            var hdr = new Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader
            {
                Magic = Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic,
                MsgId = (ushort)Mir2.Shared.Protocols.Modern.BinaryProtocol.MsgType.LoginRequest,
                PayloadLength = (ushort)pLen,
                Sequence = 2
            };
            loginBuf = new byte[sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader) + pLen];
            fixed (byte* pBuf = loginBuf)
            {
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader*)pBuf = hdr;
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.LoginIntentPayload*)(pBuf + sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader)) = loginPayload;
            }
        }
        await stream.WriteAsync(loginBuf, 0, loginBuf.Length);
        await stream.ReadAsync(recvBuf, 0, recvBuf.Length);

        // 3. 创建角色
        string cName = $"世界战尊{System.Environment.TickCount64 % 10000}";
        var cPayload = Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateCharIntentPayload.Create(cName, 0, 0, 1);
        byte[] cBuf;
        unsafe
        {
            int pLen = sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateCharIntentPayload);
            cBuf = new byte[Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize + pLen];
            fixed (byte* p = cBuf)
            {
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader*)p = new Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader
                {
                    Magic = Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic,
                    MsgId = (ushort)Mir2.Shared.Protocols.Modern.BinaryProtocol.MsgType.CreateCharRequest,
                    PayloadLength = (ushort)pLen,
                    Sequence = 3
                };
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.CreateCharIntentPayload*)(p + Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize) = cPayload;
            }
        }
        await stream.WriteAsync(cBuf, 0, cBuf.Length);
        await stream.ReadAsync(recvBuf, 0, recvBuf.Length);

        // 4. 选择角色进入游戏 (SelectCharRequest)
        var selPayload = Mir2.Shared.Protocols.Modern.BinaryProtocol.SelectCharIntentPayload.Create(0, cName);
        byte[] selBuf;
        unsafe
        {
            int pLen = sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.SelectCharIntentPayload);
            selBuf = new byte[Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize + pLen];
            fixed (byte* p = selBuf)
            {
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader*)p = new Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader
                {
                    Magic = Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic,
                    MsgId = (ushort)Mir2.Shared.Protocols.Modern.BinaryProtocol.MsgType.SelectCharRequest,
                    PayloadLength = (ushort)pLen,
                    Sequence = 4
                };
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.SelectCharIntentPayload*)(p + Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize) = selPayload;
            }
        }
        await stream.WriteAsync(selBuf, 0, selBuf.Length);
        int readBytes = await stream.ReadAsync(recvBuf, 0, recvBuf.Length);
        var respHeader = Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader.Read(recvBuf.AsSpan(0, Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize));
        Assert.Equal((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_STARTPLAY, respHeader.MsgId);

        // 5. 客户端界面就绪后，发送 IntentLoginNoticeOk 握手协议
        var readyPayload = new Mir2.Shared.Protocols.Modern.BinaryProtocol.LoginNoticeOkPayload { Acknowledged = 1 };
        byte[] readyBuf;
        unsafe
        {
            int pLen = sizeof(Mir2.Shared.Protocols.Modern.BinaryProtocol.LoginNoticeOkPayload);
            readyBuf = new byte[Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize + pLen];
            fixed (byte* p = readyBuf)
            {
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader*)p = new Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader
                {
                    Magic = Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic,
                    MsgId = (ushort)Mir2.Shared.Protocols.Modern.BinaryProtocol.MsgType.IntentLoginNoticeOk,
                    PayloadLength = (ushort)pLen,
                    Sequence = 5
                };
                *(Mir2.Shared.Protocols.Modern.BinaryProtocol.LoginNoticeOkPayload*)(p + Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize) = readyPayload;
            }
        }
        await stream.WriteAsync(readyBuf, 0, readyBuf.Length);

        // 6. 验证服务端下发的大世界全集数据包列表
        var receivedMsgIds = new System.Collections.Generic.List<ushort>();
        readBytes = await stream.ReadAsync(recvBuf, 0, recvBuf.Length);
        int offset = 0;
        while (offset + Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize <= readBytes)
        {
            var hdr = Mir2.Shared.Protocols.Modern.BinaryProtocol.PacketHeader.Read(recvBuf.AsSpan(offset, Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize));
            if (hdr.Magic != Mir2.Shared.Protocols.Modern.BinaryProtocol.DefaultMagic) break;
            receivedMsgIds.Add(hdr.MsgId);
            offset += Mir2.Shared.Protocols.Modern.BinaryProtocol.HeaderSize + hdr.PayloadLength;
        }

        // 必须完整包含官方大世界初始化的所有核心状态协议
        Assert.Contains((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_LOGON_DELPHI, receivedMsgIds);       // 50
        Assert.Contains((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_NEWMAP_DELPHI, receivedMsgIds);      // 521
        Assert.Contains((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_MYSTATUS, receivedMsgIds);           // 708
        Assert.Contains((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_EXPERIENCE, receivedMsgIds);         // 45
        Assert.Contains((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_WEIGHTCHANGED, receivedMsgIds);      // 622
        Assert.Contains((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_HEALTHSPELLCHANGED, receivedMsgIds); // 53
        Assert.Contains((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_SENDUSERSTATE, receivedMsgIds);      // 52
        Assert.Contains((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_EQUIPINFO, receivedMsgIds);          // 621
        Assert.Contains((ushort)Mir2.Shared.Protocols.ProtocolConsts.SM_CHANGELIGHT, receivedMsgIds);        // 46
    }
}


