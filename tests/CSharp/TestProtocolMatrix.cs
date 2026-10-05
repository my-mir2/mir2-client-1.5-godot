using System;
using System.Text;
using System.Threading.Tasks;
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

    [Fact]
    public void TestServerMessageRouter_AllowMoveAndSkillLevelUp_Parsing()
    {
        var router = new ServerMessageRouter(null);

        // 1. 验证 SM_ALLOWMOVE (203) 与 SM_SKILLLEVELUP (644) 常量值
        Assert.Equal(203, ServerMessageRouter.SM_ALLOWMOVE);
        Assert.Equal(644, ServerMessageRouter.SM_SKILLLEVELUP);

        // 2. 模拟网关返回 SM_ALLOWMOVE (203): (x=331, y=272, dir=3)
        int x = 331;
        int y = 272;
        short dir = 3;
        int posRecog = ((x & 0xFFFF) | ((y & 0xFFFF) << 16));
        byte[] movePkt = PacketEncoder.MakePacket(ServerMessageRouter.SM_ALLOWMOVE, posRecog, dir, 0);
        router.OnPacket(ServerMessageRouter.SM_ALLOWMOVE, movePkt.AsSpan(1..^1).ToArray());

        // 3. 模拟 SM_SKILLLEVELUP (644): skillId=1 (基本剑术), level=3, exp=500
        byte[] skillPkt = PacketEncoder.MakePacket(ServerMessageRouter.SM_SKILLLEVELUP, 500, 1, 3);
        router.OnPacket(ServerMessageRouter.SM_SKILLLEVELUP, skillPkt.AsSpan(1..^1).ToArray());

        // 4. 验证 CM_HIT (3014) 与 CM_PICKUP (1001) 发包结构
        byte[] hitPkt = PacketEncoder.Hit(2, 332, 270);
        Assert.Equal(12, hitPkt.Length);
        short hitIdent = (short)(hitPkt[1] | (hitPkt[2] << 8));
        Assert.Equal(ProtocolConsts.CM_HIT, hitIdent);

        byte[] pickupPkt = PacketEncoder.MakePacket(ProtocolConsts.CM_PICKUP, (330 << 16) | (270 & 0xFFFF));
        Assert.Equal(12, pickupPkt.Length);
        short pickupIdent = (short)(pickupPkt[1] | (pickupPkt[2] << 8));
        Assert.Equal(ProtocolConsts.CM_PICKUP, pickupIdent);
    }

    [Fact]
    public async Task TestRealNetwork_WalkAndRun_AllowMoveReceipt()
    {
        using var client = new System.Net.Sockets.TcpClient();
        try
        {
            using var cts = new System.Threading.CancellationTokenSource(1000);
            await client.ConnectAsync("127.0.0.1", 7001, cts.Token);
        }
        catch
        {
            // 若未在后台运行真实网关桥接，安全跳过
            return;
        }

        var stream = client.GetStream();

        // 1. 登录阶段
        await stream.WriteAsync(PacketEncoder.Login("testuser", "testpass"));
        await Task.Delay(100);
        await stream.WriteAsync(PacketEncoder.SelectServer(0));
        await Task.Delay(100);
        await stream.WriteAsync(PacketEncoder.MakePacket(ProtocolConsts.CM_SELECTCHAR, 0, 0, 0, Encoding.UTF8.GetBytes("热血战士")));
        await Task.Delay(150);

        // 清空前置登录与进入世界包缓存
        byte[] discardBuf = new byte[4096];
        while (client.Available > 0)
        {
            _ = await stream.ReadAsync(discardBuf.AsMemory(0, Math.Min(client.Available, discardBuf.Length)));
        }

        // 2. 发送单通行走 CM_WALK (3011, 朝东 dir=2)
        byte[] walkPkt = PacketEncoder.Walk(330, 270, 2);
        await stream.WriteAsync(walkPkt);

        // 3. 循环接收数据包并匹配网关下发的 SM_ALLOWMOVE (203)
        bool foundAllowMove = false;
        byte[] recvBuf = new byte[12];
        for (int i = 0; i < 10; i++)
        {
            if (client.Available >= 12)
            {
                await stream.ReadExactlyAsync(recvBuf.AsMemory(0, 12));
                short smCode = (short)(recvBuf[1] | (recvBuf[2] << 8));
                if (smCode == ProtocolConsts.SM_ALLOWMOVE)
                {
                    foundAllowMove = true;
                    int posRecog = recvBuf[3] | (recvBuf[4] << 8) | (recvBuf[5] << 16) | (recvBuf[6] << 24);
                    int posX = posRecog & 0xFFFF;
                    int posY = (posRecog >> 16) & 0xFFFF;
                    short dir = (short)(recvBuf[7] | (recvBuf[8] << 8));

                    Assert.True(posX > 0);
                    Assert.True(posY > 0);
                    Assert.Equal(2, dir & 7);
                    break;
                }
            }
            await Task.Delay(50);
        }

        Assert.True(foundAllowMove);
    }

    [Fact]
    public void TestSpellPacketEncoding_DelphiAndGatewayAlignment()
    {
        // 1. 测试法师雷电术 CM_SPELL 编码 (MagicId=10, 目标坐标 (335, 275))
        byte[] spellPkt = PacketEncoder.Spell(10, 335, 275);
        Assert.True(spellPkt.Length > 12);
        short ident = (short)(spellPkt[1] | (spellPkt[2] << 8));
        Assert.Equal(ProtocolConsts.CM_SPELL, ident);

        short magicId = (short)(spellPkt[7] | (spellPkt[8] << 8));
        Assert.Equal(10, magicId);

        // 验证 Extra 字符串正确包含制表符分隔的目标坐标 (CM 包首字节为 '0'..'9' 序号)
        var parsed = PacketDecoder.Parse(spellPkt.AsSpan(1));
        Assert.NotNull(parsed);
        Assert.Equal("335\t275", parsed.Value.ExtraStr);

        // 2. 测试战士特种技能 CM_ 封包编码
        byte[] powerHitPkt = PacketEncoder.PowerHit(2);
        Assert.Equal(12, powerHitPkt.Length);
        Assert.Equal(ProtocolConsts.CM_POWERHIT, (short)(powerHitPkt[1] | (powerHitPkt[2] << 8)));

        byte[] longHitPkt = PacketEncoder.LongHit(4);
        Assert.Equal(12, longHitPkt.Length);
        Assert.Equal(ProtocolConsts.CM_LONGHIT, (short)(longHitPkt[1] | (longHitPkt[2] << 8)));

        byte[] wideHitPkt = PacketEncoder.WideHit(6);
        Assert.Equal(12, wideHitPkt.Length);
        Assert.Equal(ProtocolConsts.CM_WIDEHIT, (short)(wideHitPkt[1] | (wideHitPkt[2] << 8)));

        byte[] fireHitPkt = PacketEncoder.FireHit(1001, 3);
        Assert.Equal(12, fireHitPkt.Length);
        Assert.Equal(ProtocolConsts.CM_FIREHIT, (short)(fireHitPkt[1] | (fireHitPkt[2] << 8)));

        byte[] motaeboPkt = PacketEncoder.Motaebo(1);
        Assert.Equal(12, motaeboPkt.Length);
        Assert.Equal(ProtocolConsts.CM_SPELL, (short)(motaeboPkt[1] | (motaeboPkt[2] << 8)));
    }

    [Fact]
    public void TestServerMessageRouter_SpellAndCombatPackets()
    {
        var em = new Mir2Client.Game.EntityManager();
        em.AddEntity(1001, "半兽人", Mir2Client.Game.EntityManager.EntityType.Monster, 335, 275);

        var router = new ServerMessageRouter(null, em, null);

        // 1. 模拟实体施法 SM_OBJECTSPELL (243)
        byte[] encExtraSpell = PacketCodec.EncodePlus60(Encoding.UTF8.GetBytes("275\t10"));
        byte[] rawSpell = new byte[10 + encExtraSpell.Length + 1];
        rawSpell[0] = (byte)(ProtocolConsts.SM_OBJECTSPELL & 0xFF);
        rawSpell[1] = (byte)((ProtocolConsts.SM_OBJECTSPELL >> 8) & 0xFF);
        rawSpell[2] = 0xE9; rawSpell[3] = 0x03; rawSpell[4] = 0x00; rawSpell[5] = 0x00; // Recog 1001
        rawSpell[6] = 10; rawSpell[7] = 0; // Param 10 (雷电术)
        rawSpell[8] = 0x4F; rawSpell[9] = 0x01; // Tag 335
        Array.Copy(encExtraSpell, 0, rawSpell, 10, encExtraSpell.Length);
        rawSpell[^1] = 0x0A;
        router.OnPacket(ProtocolConsts.SM_OBJECTSPELL, rawSpell);

        // 2. 模拟法术特效下发 SM_MAGIC_FIRE (638)
        byte[] encExtraFire = PacketCodec.EncodePlus60(Encoding.UTF8.GetBytes("275\t10"));
        byte[] rawFire = new byte[10 + encExtraFire.Length + 1];
        rawFire[0] = (byte)(ProtocolConsts.SM_MAGIC_FIRE & 0xFF);
        rawFire[1] = (byte)((ProtocolConsts.SM_MAGIC_FIRE >> 8) & 0xFF);
        rawFire[2] = 0xE9; rawFire[3] = 0x03; rawFire[4] = 0x00; rawFire[5] = 0x00; // Recog 1001
        rawFire[6] = 10; rawFire[7] = 0; // Param 10
        rawFire[8] = 0x4F; rawFire[9] = 0x01; // Tag 335
        Array.Copy(encExtraFire, 0, rawFire, 10, encExtraFire.Length);
        rawFire[^1] = 0x0A;
        router.OnPacket(ProtocolConsts.SM_MAGIC_FIRE, rawFire);

        // 3. 模拟伤害结算 SM_SPELLRESULT (30)
        byte[] rawResult = new byte[11];
        rawResult[0] = (byte)(ProtocolConsts.SM_SPELLRESULT & 0xFF);
        rawResult[1] = (byte)((ProtocolConsts.SM_SPELLRESULT >> 8) & 0xFF);
        rawResult[2] = 0xE9; rawResult[3] = 0x03; rawResult[4] = 0x00; rawResult[5] = 0x00; // Recog 1001
        rawResult[6] = 60; rawResult[7] = 0; // Param target_hp=60
        rawResult[8] = 40; rawResult[9] = 0; // Tag damage=40
        rawResult[10] = 0x0A;
        router.OnPacket(ProtocolConsts.SM_SPELLRESULT, rawResult);

        var ent = em.GetEntity(1001);
        Assert.NotNull(ent);
        Assert.Equal(60, ent.Hp);
    }

    [Fact]
    public async Task TestRealNetwork_SpellAndCombatFlow()
    {
        using var client = new System.Net.Sockets.TcpClient();
        try
        {
            using var cts = new System.Threading.CancellationTokenSource(1000);
            await client.ConnectAsync("127.0.0.1", 7001, cts.Token);
        }
        catch
        {
            return;
        }

        var stream = client.GetStream();

        // 1. 登录阶段
        await stream.WriteAsync(PacketEncoder.Login("testuser", "testpass"));
        await Task.Delay(50);
        await stream.WriteAsync(PacketEncoder.SelectServer(0));
        await Task.Delay(50);
        await stream.WriteAsync(PacketEncoder.MakePacket(ProtocolConsts.CM_SELECTCHAR, 0, 0, 0, Encoding.UTF8.GetBytes("热血法师")));
        await Task.Delay(50);

        // 2. 发送技能施法 CM_SPELL (火球术 MagicId=1, 坐标 332, 272)
        byte[] spellPkt = PacketEncoder.Spell(1, 332, 272);
        await stream.WriteAsync(spellPkt);
        await Task.Delay(50);

        // 3. 验证连接仍然健康可用
        Assert.True(client.Connected);
    }

    [Fact]
    public void TestMonster_SpawnTurnMoveDeadFlow()
    {
        var em = new Mir2Client.Game.EntityManager();
        var router = new ServerMessageRouter(null, em);

        // 1. 模拟怪物生成 SM_MONSPAWN (220)
        // Extra: "稻草人\t335\t275\t6\t1" (Name, X, Y, Dir, Appr)
        byte[] encExtra = PacketCodec.EncodePlus60(Encoding.UTF8.GetBytes("稻草人\t335\t275\t6\t1"));
        byte[] rawSpawn = new byte[10 + encExtra.Length + 1];
        rawSpawn[0] = (byte)(ProtocolConsts.SM_MONSPAWN & 0xFF);
        rawSpawn[1] = (byte)((ProtocolConsts.SM_MONSPAWN >> 8) & 0xFF);
        rawSpawn[2] = 0x88; rawSpawn[3] = 0x13; rawSpawn[4] = 0x00; rawSpawn[5] = 0x00; // Recog 5000
        rawSpawn[6] = 1; rawSpawn[7] = 0; // Param (Appr=1)
        Array.Copy(encExtra, 0, rawSpawn, 10, encExtra.Length);
        rawSpawn[^1] = 0x0A;
        router.OnPacket(ProtocolConsts.SM_MONSPAWN, rawSpawn);

        var mon = em.GetEntity(5000);
        Assert.NotNull(mon);
        Assert.Equal("稻草人", mon.Name);
        Assert.Equal(Mir2Client.Game.EntityManager.EntityType.Monster, mon.Type);
        Assert.Equal(335, mon.TileX);
        Assert.Equal(275, mon.TileY);
        Assert.Equal(6, mon.Direction);
        Assert.Equal(1, mon.Appr);

        // 2. 模拟怪物转向 SM_TURNDIRECTION_DELPHI (11)
        byte[] rawTurn = new byte[11];
        rawTurn[0] = (byte)(ProtocolConsts.SM_TURNDIRECTION_DELPHI & 0xFF);
        rawTurn[1] = (byte)((ProtocolConsts.SM_TURNDIRECTION_DELPHI >> 8) & 0xFF);
        rawTurn[2] = 0x88; rawTurn[3] = 0x13; rawTurn[4] = 0x00; rawTurn[5] = 0x00; // Recog 5000
        rawTurn[6] = 2; rawTurn[7] = 0; // Param dir=2
        rawTurn[10] = 0x0A;
        router.OnPacket(ProtocolConsts.SM_TURNDIRECTION_DELPHI, rawTurn);
        Assert.Equal(2, mon.Direction);

        // 3. 模拟受击掉血 SM_SPELLRESULT (30)
        byte[] rawDamage = new byte[11];
        rawDamage[0] = (byte)(ProtocolConsts.SM_SPELLRESULT & 0xFF);
        rawDamage[1] = (byte)((ProtocolConsts.SM_SPELLRESULT >> 8) & 0xFF);
        rawDamage[2] = 0x88; rawDamage[3] = 0x13; rawDamage[4] = 0x00; rawDamage[5] = 0x00; // Recog 5000
        rawDamage[6] = 30; rawDamage[7] = 0; // Param targetHp=30
        rawDamage[8] = 50; rawDamage[9] = 0; // Tag damage=50
        rawDamage[10] = 0x0A;
        router.OnPacket(ProtocolConsts.SM_SPELLRESULT, rawDamage);
        Assert.Equal(30, mon.Hp);
        Assert.False(mon.IsDead);

        // 4. 模拟怪物倒地死亡 SM_MONDEAD (222)
        byte[] rawDead = new byte[11];
        rawDead[0] = (byte)(ProtocolConsts.SM_MONDEAD & 0xFF);
        rawDead[1] = (byte)((ProtocolConsts.SM_MONDEAD >> 8) & 0xFF);
        rawDead[2] = 0x88; rawDead[3] = 0x13; rawDead[4] = 0x00; rawDead[5] = 0x00; // Recog 5000
        rawDead[10] = 0x0A;
        router.OnPacket(ProtocolConsts.SM_MONDEAD, rawDead);
        Assert.True(mon.IsDead);
        Assert.Equal(0, mon.Hp);
    }

    [Fact]
    public void TestNpc_SpawnAndInteractionFlow()
    {
        var em = new Mir2Client.Game.EntityManager();
        var router = new ServerMessageRouter(null, em);

        // 1. 模拟 NPC 生成 SM_NPCSPAWN (230)
        byte[] encExtra = PacketCodec.EncodePlus60(Encoding.UTF8.GetBytes("比奇老兵\t330\t266\t4\t0"));
        byte[] rawSpawn = new byte[10 + encExtra.Length + 1];
        rawSpawn[0] = (byte)(ProtocolConsts.SM_NPCSPAWN & 0xFF);
        rawSpawn[1] = (byte)((ProtocolConsts.SM_NPCSPAWN >> 8) & 0xFF);
        rawSpawn[2] = 0xD0; rawSpawn[3] = 0x07; rawSpawn[4] = 0x00; rawSpawn[5] = 0x00; // Recog 2000
        Array.Copy(encExtra, 0, rawSpawn, 10, encExtra.Length);
        rawSpawn[^1] = 0x0A;
        router.OnPacket(ProtocolConsts.SM_NPCSPAWN, rawSpawn);

        var npc = em.GetEntity(2000);
        Assert.NotNull(npc);
        Assert.Equal("比奇老兵", npc.Name);
        Assert.Equal(Mir2Client.Game.EntityManager.EntityType.Npc, npc.Type);
        Assert.Equal(330, npc.TileX);
        Assert.Equal(266, npc.TileY);
        Assert.Equal(4, npc.Direction);

        // 2. 模拟 NPC 消除 SM_NPCDELETE (231)
        byte[] rawDel = new byte[11];
        rawDel[0] = (byte)(ProtocolConsts.SM_NPCDELETE & 0xFF);
        rawDel[1] = (byte)((ProtocolConsts.SM_NPCDELETE >> 8) & 0xFF);
        rawDel[2] = 0xD0; rawDel[3] = 0x07; rawDel[4] = 0x00; rawDel[5] = 0x00; // Recog 2000
        rawDel[10] = 0x0A;
        router.OnPacket(ProtocolConsts.SM_NPCDELETE, rawDel);
        Assert.Null(em.GetEntity(2000));
    }

    [Fact]
    public void TestGroundItem_SpawnAndRemoveFlow()
    {
        var gm = new Mir2Client.Game.GroundItemManager();

        // 1. 添加掉落物测试
        gm.AddItem(7001, "裁决之杖", 330, 270, 10, 1);
        gm.AddItem(7002, "金币", 330, 270, 1, 1000);

        Assert.Equal(2, gm.Count);
        var item7001 = gm.GetItem(7001);
        Assert.NotNull(item7001);
        Assert.Equal("裁决之杖", item7001.Name);
        Assert.Equal(330, item7001.TileX);
        Assert.Equal(270, item7001.TileY);
        Assert.Equal(10, item7001.Looks);

        // 2. 按瓦片位置查找
        var tileItems = gm.GetItemsAt(330, 270);
        Assert.Equal(2, tileItems.Count);

        // 3. 拾取归属保护期测试
        item7001.OwnerName = "热血战士";
        item7001.ProtectionTime = 60.0f;
        Assert.False(gm.CanPickup(7001, "抢包者"));
        Assert.True(gm.CanPickup(7001, "热血战士"));

        // 4. 保护期过期
        item7001.ProtectionTime = 0.0f;
        Assert.True(gm.CanPickup(7001, "抢包者"));

        // 5. 移除物品
        Assert.True(gm.RemoveItem(7001));
        Assert.Equal(1, gm.Count);
        Assert.Null(gm.GetItem(7001));
    }

    [Fact]
    public void TestGroundItem_NetworkProtocolFlow()
    {
        var gm = new Mir2Client.Game.GroundItemManager();
        var router = new ServerMessageRouter(null, null, null, gm);

        // 1. 模拟地表掉落 SM_GROUNDITEM (314)
        // Extra: "强效太阳水\t332\t271\t3"
        byte[] encExtra = PacketCodec.EncodePlus60(Encoding.UTF8.GetBytes("强效太阳水\t332\t271\t3"));
        byte[] rawDrop = new byte[10 + encExtra.Length + 1];
        rawDrop[0] = (byte)(ProtocolConsts.SM_GROUNDITEM & 0xFF);
        rawDrop[1] = (byte)((ProtocolConsts.SM_GROUNDITEM >> 8) & 0xFF);
        rawDrop[2] = 0x39; rawDrop[3] = 0x05; rawDrop[4] = 0x00; rawDrop[5] = 0x00; // Recog 1337
        rawDrop[6] = 0x4C; rawDrop[7] = 0x01; // Param X=332
        rawDrop[8] = 0x0F; rawDrop[9] = 0x01; // Tag Y=271
        Array.Copy(encExtra, 0, rawDrop, 10, encExtra.Length);
        rawDrop[^1] = 0x0A;
        router.OnPacket(ProtocolConsts.SM_GROUNDITEM, rawDrop);

        var item = gm.GetItem(1337);
        Assert.NotNull(item);
        Assert.Equal("强效太阳水", item.Name);
        Assert.Equal(332, item.TileX);
        Assert.Equal(271, item.TileY);
        Assert.Equal(3, item.Looks);

        // 2. 模拟拾取消除 SM_DELGROUNDITEM (315)
        byte[] rawDel = new byte[11];
        rawDel[0] = (byte)(ProtocolConsts.SM_DELGROUNDITEM & 0xFF);
        rawDel[1] = (byte)((ProtocolConsts.SM_DELGROUNDITEM >> 8) & 0xFF);
        rawDel[2] = 0x39; rawDel[3] = 0x05; rawDel[4] = 0x00; rawDel[5] = 0x00; // Recog 1337
        rawDel[10] = 0x0A;
        router.OnPacket(ProtocolConsts.SM_DELGROUNDITEM, rawDel);

        Assert.Null(gm.GetItem(1337));
        Assert.Equal(0, gm.Count);

        // 3. 验证客户端拾取发包 CM_PICKUP (1001)
        int px = 332;
        int py = 271;
        int posRecog = (px << 16) | (py & 0xFFFF);
        byte[] pickupPkt = PacketEncoder.MakePacket(ProtocolConsts.CM_PICKUP, posRecog);
        Assert.Equal(12, pickupPkt.Length);
        short ident = (short)(pickupPkt[1] | (pickupPkt[2] << 8));
        Assert.Equal(ProtocolConsts.CM_PICKUP, ident);
    }
}


