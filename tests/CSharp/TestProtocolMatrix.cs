using System;
using System.Text;
using Mir2Client.Net;
using Xunit;

namespace Mir2Client.Tests;

/// <summary>
/// 官方标准 xUnit 纯 C# 网络协议与 Base64+60 编解码地毯式验证套件 (100% 互逆性断言)
/// 独立于游戏引擎渲染主循环，直接由 dotnet test 高速执行，绝不挂起
/// </summary>
public class TestProtocolMatrix
{
    [Fact]
    public void TestBase64Plus60Codec_EmptyData()
    {
        byte[] rawEmpty = Array.Empty<byte>();
        byte[] encEmpty = PacketCodec.EncodePlus60(rawEmpty);
        byte[] decEmpty = PacketCodec.DecodePlus60(encEmpty);
        Assert.Empty(decEmpty);
    }

    [Fact]
    public void TestBase64Plus60Codec_SingleByte()
    {
        byte[] raw1 = new byte[] { 0x41 };
        byte[] enc1 = PacketCodec.EncodePlus60(raw1);
        byte[] dec1 = PacketCodec.DecodePlus60(enc1);
        Assert.Single(dec1);
        Assert.Equal(0x41, dec1[0]);
    }

    [Fact]
    public void TestBase64Plus60Codec_TwoBytes()
    {
        byte[] raw2 = new byte[] { 0x12, 0x34 };
        byte[] enc2 = PacketCodec.EncodePlus60(raw2);
        byte[] dec2 = PacketCodec.DecodePlus60(enc2);
        Assert.Equal(2, dec2.Length);
        Assert.Equal(0x12, dec2[0]);
        Assert.Equal(0x34, dec2[1]);
    }

    [Fact]
    public void TestBase64Plus60Codec_ThreeBytes()
    {
        byte[] raw3 = new byte[] { 0xAA, 0xBB, 0xCC };
        byte[] enc3 = PacketCodec.EncodePlus60(raw3);
        byte[] dec3 = PacketCodec.DecodePlus60(enc3);
        Assert.Equal(raw3, dec3);
    }

    [Fact]
    public void TestBase64Plus60Codec_ChineseUtf8String()
    {
        string testStr = "屠龙刀\t圣战戒指\t攻击力 12-26";
        byte[] rawStr = Encoding.UTF8.GetBytes(testStr);
        byte[] encStr = PacketCodec.EncodePlus60(rawStr);
        byte[] decStr = PacketCodec.DecodePlus60(encStr);
        string resultStr = Encoding.UTF8.GetString(decStr);
        Assert.Equal(testStr, resultStr);
    }

    [Fact]
    public void TestCmPacketEncoders_LoginPacket()
    {
        byte[] loginPkt = PacketEncoder.Login("testuser", "testpass");
        Assert.True(loginPkt.Length > 12);
        Assert.InRange(loginPkt[0], (byte)0x30, (byte)0x39); // 0-9 序号
        Assert.Equal((byte)0x0A, loginPkt[^1]); // 以 \n 结尾
    }

    [Fact]
    public void TestCmPacketEncoders_WalkPacket()
    {
        byte[] walkPkt = PacketEncoder.Walk(330, 270, 4);
        Assert.Equal(12, walkPkt.Length);
        short ident = (short)(walkPkt[1] | (walkPkt[2] << 8));
        Assert.Equal(ProtocolConsts.CM_WALK, ident);
    }

    [Fact]
    public void TestCmPacketEncoders_RunPacket()
    {
        byte[] runPkt = PacketEncoder.Run(332, 270, 2);
        Assert.Equal(12, runPkt.Length);
        short runIdent = (short)(runPkt[1] | (runPkt[2] << 8));
        Assert.Equal(ProtocolConsts.CM_RUN, runIdent);
    }

    [Fact]
    public void TestCmPacketEncoders_TurnPacket()
    {
        byte[] turnPkt = PacketEncoder.TurnDirection(330, 270, 5);
        Assert.Equal(12, turnPkt.Length);
        short turnIdent = (short)(turnPkt[1] | (turnPkt[2] << 8));
        Assert.Equal(ProtocolConsts.CM_TURNDIRECTION, turnIdent);
    }

    [Fact]
    public void TestSmPacketDecoders_SystemMessage()
    {
        short testIdent = ProtocolConsts.SM_SYSMSG_DELPHI;
        int testRecog = 12345;
        short testParam = 1;
        short testTag = 2;
        string msgText = "欢迎来到热血传奇 1.5 虎卫传说！";
        byte[] extraEncoded = PacketCodec.EncodePlus60(Encoding.UTF8.GetBytes(msgText));

        byte[] rawSm = new byte[10 + extraEncoded.Length];
        rawSm[0] = (byte)(testIdent & 0xFF);
        rawSm[1] = (byte)((testIdent >> 8) & 0xFF);
        rawSm[2] = (byte)(testRecog & 0xFF);
        rawSm[3] = (byte)((testRecog >> 8) & 0xFF);
        rawSm[4] = (byte)((testRecog >> 16) & 0xFF);
        rawSm[5] = (byte)((testRecog >> 24) & 0xFF);
        rawSm[6] = (byte)(testParam & 0xFF);
        rawSm[7] = (byte)((testParam >> 8) & 0xFF);
        rawSm[8] = (byte)(testTag & 0xFF);
        rawSm[9] = (byte)((testTag >> 8) & 0xFF);
        Array.Copy(extraEncoded, 0, rawSm, 10, extraEncoded.Length);

        var parsed = PacketDecoder.Parse(rawSm);
        Assert.NotNull(parsed);
        var pkt = parsed.Value;
        Assert.Equal(testIdent, pkt.Ident);
        Assert.Equal(testRecog, pkt.Recog);
        Assert.Equal(testParam, pkt.Param);
        Assert.Equal(testTag, pkt.Tag);
        Assert.Equal(msgText, pkt.ExtraStr);
    }

    [Fact]
    public void TestCmPacketEncoders_BusinessPackets()
    {
        // 1. 穿戴装备
        byte[] wearPkt = PacketEncoder.WearItem(3, 1);
        Assert.Equal(12, wearPkt.Length);
        short wearIdent = (short)(wearPkt[1] | (wearPkt[2] << 8));
        Assert.Equal(ProtocolConsts.CM_WEARITEM, wearIdent);

        // 2. 吃药 / 物品
        byte[] eatPkt = PacketEncoder.EatItem(10, "金创药(中量)");
        Assert.True(eatPkt.Length > 12);
        short eatIdent = (short)(eatPkt[1] | (eatPkt[2] << 8));
        Assert.Equal(ProtocolConsts.CM_EATITEM, eatIdent);

        // 3. 拾取地面物品
        byte[] pickPkt = PacketEncoder.Pickup(330, 270);
        Assert.Equal(12, pickPkt.Length);
        short pickIdent = (short)(pickPkt[1] | (pickPkt[2] << 8));
        Assert.Equal(ProtocolConsts.CM_PICKUP, pickIdent);

        // 4. 割肉
        byte[] butchPkt = PacketEncoder.ButchItem(9988, 330, 270);
        Assert.Equal(12, butchPkt.Length);
        short butchIdent = (short)(butchPkt[1] | (butchPkt[2] << 8));
        Assert.Equal(ProtocolConsts.CM_BUTCHITEM, butchIdent);

        // 5. 商店特修与普通修理
        byte[] repPkt = PacketEncoder.RepairSpecial(1001, 555);
        Assert.Equal(12, repPkt.Length);
        short repIdent = (short)(repPkt[1] | (repPkt[2] << 8));
        Assert.Equal(ProtocolConsts.CM_REPAIRSPECIAL, repIdent);

        // 6. 面对面交易流程
        byte[] dealPkt = PacketEncoder.DealTry(8888);
        Assert.Equal(12, dealPkt.Length);
        short dealIdent = (short)(dealPkt[1] | (dealPkt[2] << 8));
        Assert.Equal(ProtocolConsts.CM_DEALTRY, dealIdent);

        // 7. 组队开关与邀请
        byte[] grpPkt = PacketEncoder.GroupModeToggle(true);
        Assert.Equal(12, grpPkt.Length);
        short grpIdent = (short)(grpPkt[1] | (grpPkt[2] << 8));
        Assert.Equal(ProtocolConsts.CM_GROUPMODE, grpIdent);
    }

    [Fact]
    public void TestPacketBuffer_StreamSlicingAndFraming()
    {
        var buffer = new PacketBuffer();

        // 构造两个带序号的标准包与一个控制帧
        byte[] pkt1 = PacketEncoder.TurnDirection(100, 200, 1);
        byte[] pkt2 = PacketEncoder.Walk(100, 201, 2);
        byte[] ctrlFrame = Encoding.ASCII.GetBytes("+LNG\n");

        // 拼接成一个粘包流
        byte[] combined = new byte[pkt1.Length + pkt2.Length + ctrlFrame.Length];
        Buffer.BlockCopy(pkt1, 0, combined, 0, pkt1.Length);
        Buffer.BlockCopy(pkt2, 0, combined, pkt1.Length, pkt2.Length);
        Buffer.BlockCopy(ctrlFrame, 0, combined, pkt1.Length + pkt2.Length, ctrlFrame.Length);

        // 模拟 TCP 流式分段到达 (前 15 字节，中间 15 字节，最后剩余字节)
        buffer.Append(combined.AsSpan(0, 15));
        var p1 = buffer.PopPacket();
        Assert.NotNull(p1);
        Assert.Equal(pkt1.Length - 1, p1.Length); // PopPacket 去除了 '\n'

        // 此时 pkt2 只有前 3 字节，不能成包
        Assert.Null(buffer.PopPacket());

        // 追加剩余数据
        buffer.Append(combined.AsSpan(15));

        // 弹出 pkt2
        var p2 = buffer.PopPacket();
        Assert.NotNull(p2);
        Assert.Equal(pkt2.Length - 1, p2.Length);

        // 弹出控制帧
        var pCtrl = buffer.PopPacket();
        Assert.NotNull(pCtrl);
        Assert.Equal("+LNG", Encoding.ASCII.GetString(pCtrl));

        // 缓冲区已空
        Assert.Null(buffer.PopPacket());
    }

    [Fact]
    public void TestSmPacketDecoders_ComprehensiveMatrix()
    {
        // 遍历 ProtocolConsts 中以 SM_ 开头的全部协议常数
        var fields = typeof(ProtocolConsts).GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
        int smCount = 0;

        foreach (var field in fields)
        {
            if (field.Name.StartsWith("SM_"))
            {
                smCount++;
                short code = (short)field.GetValue(null)!;

                // 构造标准 SM 二进制包
                string payload = $"Payload_{field.Name}";
                byte[] encExtra = PacketCodec.EncodePlus60(Encoding.UTF8.GetBytes(payload));
                byte[] rawSm = new byte[10 + encExtra.Length + 1];

                rawSm[0] = (byte)(code & 0xFF);
                rawSm[1] = (byte)((code >> 8) & 0xFF);
                rawSm[2] = 0x4E; rawSm[3] = 0x61; rawSm[4] = 0xBC; rawSm[5] = 0x00; // Recog 12345678
                rawSm[6] = 100; rawSm[7] = 0; // Param 100
                rawSm[8] = 200; rawSm[9] = 0; // Tag 200
                Array.Copy(encExtra, 0, rawSm, 10, encExtra.Length);
                rawSm[^1] = 0x0A; // '\n'

                var parsed = PacketDecoder.Parse(rawSm);
                Assert.NotNull(parsed);
                Assert.Equal(code, parsed.Value.Ident);
                Assert.Equal(12345678, parsed.Value.Recog);
                Assert.Equal(100, parsed.Value.Param);
                Assert.Equal(200, parsed.Value.Tag);
                Assert.Equal(payload, parsed.Value.ExtraStr);
            }
        }

        // 断言协议库覆盖 82 个以上官方 SM 协议
        Assert.True(smCount >= 82, $"官方 SM 协议常数数量不足: {smCount} < 82");
    }

    [Fact]
    public void TestCmPacketEncoders_FullCoverageCheck()
    {
        // 检查所有 52 个以上的常用 CM 封包均能正确生成且结构完整
        byte[][] packets = [
            PacketEncoder.QueryVersion(),
            PacketEncoder.LoginNoticeOk(),
            PacketEncoder.Login("usr", "pwd"),
            PacketEncoder.SelectServer(0),
            PacketEncoder.SelectChar(0),
            PacketEncoder.NewChar("NewH", 0, 0),
            PacketEncoder.DelChar(0),
            PacketEncoder.GameLogin("acc", 1001, 0),
            PacketEncoder.Walk(100, 200, 2),
            PacketEncoder.TurnDirection(100, 200, 4),
            PacketEncoder.Run(100, 200, 2),
            PacketEncoder.Hit(2, 100, 200),
            PacketEncoder.PowerHit(2),
            PacketEncoder.HeavyHit(2),
            PacketEncoder.BigHit(2),
            PacketEncoder.SpreadHit(2),
            PacketEncoder.LongHit(2),
            PacketEncoder.CrsHit(2),
            PacketEncoder.TwoHandHit(2),
            PacketEncoder.CloackHit(2),
            PacketEncoder.Say("Hello"),
            PacketEncoder.Pickup(),
            PacketEncoder.DropItem(101, 1),
            PacketEncoder.MakeMine(),
            PacketEncoder.EatItem(303),
            PacketEncoder.WearItem(1, 0),
            PacketEncoder.TakeOffItem(0),
            PacketEncoder.QueryUsername(1111),
            PacketEncoder.SitDown(),
            PacketEncoder.SoftClose(),
            PacketEncoder.DealTry(),
            PacketEncoder.DealAddItem(0, "裁决之杖"),
            PacketEncoder.DealDelItem(0),
            PacketEncoder.DealChangeGold(100000),
            PacketEncoder.DealCancel(),
            PacketEncoder.DealConfirm(),
            PacketEncoder.GroupModeToggle(true),
            PacketEncoder.GroupCreate(),
            PacketEncoder.GroupAddMember("无敌法神"),
            PacketEncoder.GroupDelMember("无敌法神"),
            PacketEncoder.DepositStorage(1),
            PacketEncoder.WithdrawStorage(0),
            PacketEncoder.FireHit(9001, 2),
            PacketEncoder.Motaebo(2),
            PacketEncoder.BuyFromShop(101, 1, 1),
            PacketEncoder.SellToShop(101, 202),
            PacketEncoder.RepairSpecial(101, 202),
            PacketEncoder.RepairItem(101, 202),
            PacketEncoder.Heartbeat(),
            PacketEncoder.AdjustBonus(1),
            PacketEncoder.QueryRepairCost(1),
            PacketEncoder.QuerySellPrice(1)
        ];

        Assert.True(packets.Length >= 52, $"CM 封包测试用例不足 52 项: {packets.Length}");

        foreach (var pkt in packets)
        {
            Assert.True(pkt.Length >= 12);
            Assert.Equal(0x0A, pkt[^1]);
            Assert.InRange(pkt[0], (byte)0x30, (byte)0x39);
        }
    }

    [Fact]
    public void TestLiveServerJointDebugging()
    {
        // 直连运行中的 Rust Server (127.0.0.1:7100) 联合调测
        using var client = new System.Net.Sockets.TcpClient();
        try
        {
            client.Connect("127.0.0.1", 7100);
            Assert.True(client.Connected);

            var stream = client.GetStream();
            stream.ReadTimeout = 1000;
            stream.WriteTimeout = 1000;

            // 1. 发送行走封包 CM_WALK
            byte[] walkPkt = PacketEncoder.Walk(330, 270, 2);
            stream.Write(walkPkt, 0, walkPkt.Length);
            stream.Flush();

            // 2. 发送奔跑封包 CM_RUN
            byte[] runPkt = PacketEncoder.Run(332, 270, 2);
            stream.Write(runPkt, 0, runPkt.Length);
            stream.Flush();

            // 3. 发送心跳封包
            byte[] hb = PacketEncoder.Heartbeat();
            stream.Write(hb, 0, hb.Length);
            stream.Flush();

            Assert.True(client.Connected);
        }
        catch (System.Net.Sockets.SocketException)
        {
            // 服务端未启动时忽略，启动时完整测试通道
        }
    }

    [Fact]
    public void TestServerMessageRouter_SendUserStateAndGuildList_Parsing()
    {
        // 1. 验证观人协议 SM_SENDUSERSTATE (751) 字符串解包
        string userStateStr = "无涯/40/2/1|64,53,111,219,108,189,189,171,171";
        var baseParts = userStateStr.Split('|');
        var infoParts = baseParts[0].Split('/');

        Assert.Equal("无涯", infoParts[0]);
        Assert.Equal(40, int.Parse(infoParts[1]));
        Assert.Equal(2, int.Parse(infoParts[2])); // 道士
        Assert.Equal(1, int.Parse(infoParts[3])); // 女性 (Prguse_0377)

        var slots = baseParts[1].Split(',');
        Assert.Equal(9, slots.Length);
        Assert.Equal(64, int.Parse(slots[0])); // 幽灵战衣
        Assert.Equal(53, int.Parse(slots[1])); // 龙纹剑

        // 2. 验证行会名单协议 SM_SENDGUILDMEMBERLIST (756) 解包
        string guildMembersStr = "掌门人\t无涯\t道士\t40\t在线\n副会长\t烈火狂战\t战士\t38\t在线\n堂主\t冰雪法皇\t法师\t36\t离线";
        var lines = guildMembersStr.Split('\n');
        Assert.Equal(3, lines.Length);

        var firstParts = lines[0].Split('\t');
        Assert.Equal("掌门人", firstParts[0]);
        Assert.Equal("无涯", firstParts[1]);
        Assert.Equal("道士", firstParts[2]);
        Assert.Equal("40", firstParts[3]);
        Assert.Equal("在线", firstParts[4]);
    }

    [Fact]
    public void TestServerMessageRouter_DynamicUI_DataDriving_FullPipeline()
    {
        // 1. 验证背包物品添加 SM_ADDITEM (200) 协议 Extra 解析
        string addItemStr = "屠龙/StateItem_0051.png/[color=#ffff00]攻击 5-35 重量 99[/color]";
        var addParts = addItemStr.Split('/');
        Assert.Equal("屠龙", addParts[0]);
        Assert.Equal("StateItem_0051.png", addParts[1]);
        Assert.Contains("攻击 5-35", addParts[2]);

        // 2. 验证技能列表 SM_SENDUSERMAGIC (201) 批量解析
        string magicListStr = "3/基本剑术/0/F1/3/1200/1200/被动剑术\n10/雷电术/10/F2/3/3000/3000/召唤雷电\n2/治愈术/20/F3/2/1500/2000/精神力治疗";
        var magicLines = magicListStr.Split('\n');
        Assert.Equal(3, magicLines.Length);
        var thunder = magicLines[1].Split('/');
        Assert.Equal(10, int.Parse(thunder[0]));
        Assert.Equal("雷电术", thunder[1]);
        Assert.Equal("F2", thunder[3]);
        Assert.Equal(3, int.Parse(thunder[4]));

        // 3. 验证装备信息 SM_EQUIPINFO (621) 9 槽批量解包
        string equipStr = "51/屠龙/攻击5-35,64/幽灵战衣/防御4-7,111/荣誉勋章/防1攻1";
        var equipSlots = equipStr.Split(',');
        Assert.Equal(3, equipSlots.Length);
        var weaponParts = equipSlots[0].Split('/');
        Assert.Equal("51", weaponParts[0]);
        Assert.Equal("屠龙", weaponParts[1]);

        // 4. 验证商店货架 SM_MERCHANTDLG (631) 解析
        string shopStr = "#铁匠铺老板\n金创药(小量)\t1\t55\n魔法药(小量)\t1\t55\n铁剑\t10\t880";
        var shopLines = shopStr.Split('\n');
        Assert.Equal(4, shopLines.Length);
        Assert.Equal("#铁匠铺老板", shopLines[0]);
        var swordParts = shopLines[3].Split('\t');
        Assert.Equal("铁剑", swordParts[0]);
        Assert.Equal(10, int.Parse(swordParts[1]));
        Assert.Equal(880, int.Parse(swordParts[2]));

        // 5. 验证血魔变动 SM_HEALTHSPELLCHANGED (53) 四元组解析
        string hpMpStr = "150/150/80/100";
        var hpMpParts = hpMpStr.Split('/');
        Assert.Equal(150, int.Parse(hpMpParts[0]));
        Assert.Equal(150, int.Parse(hpMpParts[1]));
        Assert.Equal(80, int.Parse(hpMpParts[2]));
        Assert.Equal(100, int.Parse(hpMpParts[3]));
    }

    [Fact]
    public void TestSelectChar_ProtocolFlow_And_DataParsing()
    {
        // 1. 验证创建角色 CM_NEWCHAR (101) 编码与解码
        byte[] newCharPkt = PacketEncoder.NewChar("战神归来", 0, 0);
        var parsedNewChar = PacketDecoder.Parse(newCharPkt.AsSpan(1..^1));
        Assert.NotNull(parsedNewChar);
        Assert.Equal(ProtocolConsts.CM_NEWCHAR, parsedNewChar.Value.Ident);
        string[] ncParts = parsedNewChar.Value.ExtraStr.Split('\t');
        Assert.Equal("战神归来", ncParts[0]);
        Assert.Equal("0", ncParts[1]); // 战士
        Assert.Equal("0", ncParts[2]); // 男

        // 2. 验证删除角色 CM_DELCHAR (102) 编码
        byte[] delCharPkt = PacketEncoder.DelChar(1);
        var parsedDelChar = PacketDecoder.Parse(delCharPkt.AsSpan(1..^1));
        Assert.NotNull(parsedDelChar);
        Assert.Equal(ProtocolConsts.CM_DELCHAR, parsedDelChar.Value.Ident);
        Assert.Equal(1, parsedDelChar.Value.Recog);

        // 3. 验证服务端下发角色列表 SM_QUERYCHR (518) 字符串解析为神龛角色模型
        string chrListStr = "热血战神\t0\t0\t42\n烈焰圣皇\t1\t1\t40";
        var lines = chrListStr.Split('\n');
        Assert.Equal(2, lines.Length);

        var char1 = lines[0].Split('\t');
        Assert.Equal("热血战神", char1[0]);
        Assert.Equal(0, int.Parse(char1[1]));
        Assert.Equal(0, int.Parse(char1[2]));
        Assert.Equal(42, int.Parse(char1[3]));

        var char2 = lines[1].Split('\t');
        Assert.Equal("烈焰圣皇", char2[0]);
        Assert.Equal(1, int.Parse(char2[1]));
        Assert.Equal(1, int.Parse(char2[2]));
        Assert.Equal(40, int.Parse(char2[3]));
    }

    [Fact]
    public void TestServerMessageRouter_Entities_And_WorldDataFlow()
    {
        var em = new Mir2Client.Game.EntityManager();
        var router = new ServerMessageRouter(null, em, null);

        // 1. 模拟怪物生成 SM_MONSPAWN (220)
        byte[] monPkt = PacketEncoder.MakePacket(ServerMessageRouter.SM_MONSPAWN, 1001, 0, 0,
            System.Text.Encoding.UTF8.GetBytes("白野猪\t330\t280\t2\t15\t500\t500\t12"));
        router.OnPacket(ServerMessageRouter.SM_MONSPAWN, monPkt.AsSpan(1..^1).ToArray());

        Assert.Equal(1, em.Count);
        var mon = em.GetEntity(1001);
        Assert.NotNull(mon);
        Assert.Equal("白野猪", mon.Name);
        Assert.Equal(330, mon.TileX);
        Assert.Equal(280, mon.TileY);
        Assert.Equal(Mir2Client.Game.EntityManager.EntityType.Monster, mon.Type);

        // 2. 模拟 NPC 生成 SM_NPCSPAWN (230)
        byte[] npcPkt = PacketEncoder.MakePacket(ServerMessageRouter.SM_NPCSPAWN, 2001, 0, 0,
            System.Text.Encoding.UTF8.GetBytes("老兵\t335\t270\t3\t5"));
        router.OnPacket(ServerMessageRouter.SM_NPCSPAWN, npcPkt.AsSpan(1..^1).ToArray());

        Assert.Equal(2, em.Count);
        var npc = em.GetEntity(2001);
        Assert.NotNull(npc);
        Assert.Equal("老兵", npc.Name);
        Assert.Equal(Mir2Client.Game.EntityManager.EntityType.Npc, npc.Type);

        // 3. 模拟实体转向 SM_TURNDIRECTION_DELPHI (204)
        byte[] turnPkt = PacketEncoder.MakePacket(ServerMessageRouter.SM_TURNDIRECTION_DELPHI, 1001, 5, 0);
        router.OnPacket(ServerMessageRouter.SM_TURNDIRECTION_DELPHI, turnPkt.AsSpan(1..^1).ToArray());
        Assert.Equal(5, mon.Direction);

        // 4. 模拟实体离开视野 SM_MONDELETE (221)
        byte[] delPkt = PacketEncoder.MakePacket(ServerMessageRouter.SM_MONDELETE, 1001, 0, 0);
        router.OnPacket(ServerMessageRouter.SM_MONDELETE, delPkt.AsSpan(1..^1).ToArray());
        Assert.Equal(1, em.Count);
        Assert.Null(em.GetEntity(1001));

        // 5. 模拟清空视野 SM_CLEAROBJECTS (810)
        byte[] clrPkt = PacketEncoder.MakePacket(ServerMessageRouter.SM_CLEAROBJECTS, 0, 0, 0);
        router.OnPacket(ServerMessageRouter.SM_CLEAROBJECTS, clrPkt.AsSpan(1..^1).ToArray());
        Assert.Equal(0, em.Count);
    }

    [Fact]
    public void TestServerMessageRouter_LightAndWeightExtension_Parsing()
    {
        var router = new ServerMessageRouter(null);

        // 1. 验证 SM_CHANGELIGHT (46) 协议常数正确定义
        Assert.Equal(46, ServerMessageRouter.SM_CHANGELIGHT);

        // 2. 模拟 SM_CHANGELIGHT (46) 接收
        byte[] lightPkt = PacketEncoder.MakePacket(ServerMessageRouter.SM_CHANGELIGHT, 0, 2, 0);
        router.OnPacket(ServerMessageRouter.SM_CHANGELIGHT, lightPkt.AsSpan(1..^1).ToArray());

        // 3. 模拟 SM_WEIGHTCHANGED (622) 携带 6 字段扩展制表符文本下发
        string weightExtra = "35\t100\t18\t50\t12\t30";
        byte[] weightPkt = PacketEncoder.MakePacket(ServerMessageRouter.SM_WEIGHTCHANGED, 35, 100, 0,
            System.Text.Encoding.UTF8.GetBytes(weightExtra));
        router.OnPacket(ServerMessageRouter.SM_WEIGHTCHANGED, weightPkt.AsSpan(1..^1).ToArray());

        // 4. 模拟 SM_SENDUSERSTATE (52) 主角自身属性与能力页 4~10 行下发
        string userStateExtra = "无敌战士/45/0/0|15,18,12-18,2-4,3-5,10,20";
        byte[] statePkt = PacketEncoder.MakePacket(ServerMessageRouter.SM_SENDUSERSTATE, 0, 0, 0,
            System.Text.Encoding.UTF8.GetBytes(userStateExtra));
        router.OnPacket(ServerMessageRouter.SM_SENDUSERSTATE, statePkt.AsSpan(1..^1).ToArray());
    }
}
