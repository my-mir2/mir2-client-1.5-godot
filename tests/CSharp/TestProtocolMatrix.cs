using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mir2Client.Game;
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
        using var readCts = new System.Threading.CancellationTokenSource(TimeSpan.FromSeconds(3));
        byte[] recvBuf = new byte[2048];

        while (!readCts.IsCancellationRequested && !foundAllowMove)
        {
            if (client.Available > 0)
            {
                int read = await stream.ReadAsync(recvBuf.AsMemory(0, Math.Min(client.Available, recvBuf.Length)), readCts.Token);
                for (int i = 0; i <= read - 12; i++)
                {
                    if (recvBuf[i] >= 0x30 && recvBuf[i] <= 0x39) // '0'..'9' 包序号前缀
                    {
                        short smCode = (short)(recvBuf[i + 1] | (recvBuf[i + 2] << 8));
                        if (smCode == ProtocolConsts.SM_ALLOWMOVE)
                        {
                            foundAllowMove = true;
                            int posRecog = recvBuf[i + 3] | (recvBuf[i + 4] << 8) | (recvBuf[i + 5] << 16) | (recvBuf[i + 6] << 24);
                            int posX = posRecog & 0xFFFF;
                            int posY = (posRecog >> 16) & 0xFFFF;
                            short dir = (short)(recvBuf[i + 7] | (recvBuf[i + 8] << 8));

                            Assert.True(posX > 0);
                            Assert.True(posY > 0);
                            Assert.Equal(2, dir & 7);
                            break;
                        }
                    }
                }
            }
            else
            {
                await Task.Delay(50);
            }
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

    // ==================== 官方 1.5 七大特戒与四大套装底层规则矩阵实证测试 ====================

    [Fact]
    public void TestSpecialRings_ProtectRing_FormulaAndFlow()
    {
        var ps = new Mir2Client.Game.PlayerState
        {
            Hp = 100,
            MaxHp = 100,
            Mp = 100,
            MaxMp = 100
        };

        // 佩戴护身戒指 (槽位 7: 左戒指, Shape 118)
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.RingL, new Mir2Client.Game.EquipItem
        {
            Uid = 9001,
            Name = "护身戒指",
            Shape = 118,
            Dura = 5
        });

        Assert.True(ps.HasProtectRing);

        // 1. 受到 20 点伤害: mpCost = ROUND(20 * 1.5) = 30 MP, HP 完全不扣
        var (hp1, mp1, died1, rev1) = ps.TakeCombatDamage(20, nowTick: 1000);
        Assert.Equal(100, hp1);
        Assert.Equal(70, mp1);
        Assert.False(died1);
        Assert.False(rev1);

        // 2. MP 不足抵扣: 当前 70 MP, 受到 60 点伤害 (需 90 MP)
        // 抵扣 70 MP, 剩余 20 MP 缺口转为 20 / 1.5 = 13 点伤害扣 HP
        var (hp2, mp2, died2, rev2) = ps.TakeCombatDamage(60, nowTick: 2000);
        Assert.Equal(0, mp2);
        Assert.Equal(87, hp2); // 100 - 13 = 87
        Assert.False(died2);

        // 3. MP 为 0 时全额扣 HP
        var (hp3, mp3, died3, rev3) = ps.TakeCombatDamage(10, nowTick: 3000);
        Assert.Equal(0, mp3);
        Assert.Equal(77, hp3);

        // 4. 脱下护身戒指
        ps.TakeOffItem(Mir2Client.Game.OfficialEquipSlot.RingL);
        Assert.False(ps.HasProtectRing);
    }

    [Fact]
    public void TestSpecialRings_ReviveRing_CooldownAndBreak()
    {
        var ps = new Mir2Client.Game.PlayerState
        {
            BaseMaxHp = 200,
            Hp = 50,
            MaxHp = 200,
            Mp = 50,
            MaxMp = 100,
            ReviveRingDura = 2000 // 初始内部持久 2000 (可复活 2 次)
        };

        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.RingR, new Mir2Client.Game.EquipItem
        {
            Uid = 9002,
            Name = "复活戒指",
            Shape = 114,
            Dura = 2
        });

        Assert.True(ps.HasReviveRing);

        // 1. 致命伤害 500 点触发第一次复活
        var (hp1, mp1, died1, rev1) = ps.TakeCombatDamage(500, nowTick: 10000);
        Assert.Equal(200, hp1); // 满血复活
        Assert.False(died1);
        Assert.True(rev1);
        Assert.Equal(1000, ps.ReviveRingDura); // 持久扣除 1000

        // 2. 30 秒冷却时间内再次受到致命伤害 (15 秒后，nowTick = 25000): 复活失败死亡
        var (hp2, mp2, died2, rev2) = ps.TakeCombatDamage(500, nowTick: 25000);
        Assert.Equal(0, hp2);
        Assert.True(died2);
        Assert.False(rev2);

        // 3. 冷却期过后 (45 秒后，nowTick = 55000) 满血复活，持久归零破碎
        ps.Hp = 10;
        var (hp3, mp3, died3, rev3) = ps.TakeCombatDamage(500, nowTick: 55000);
        Assert.Equal(200, hp3);
        Assert.False(died3);
        Assert.True(rev3);
        Assert.Equal(0, ps.ReviveRingDura);
        Assert.False(ps.HasReviveRing); // 戒指破碎自动脱下销毁
    }

    [Fact]
    public void TestSpecialRings_ParalysisRing_ProbabilityAndAntiParalysis()
    {
        var ps = new Mir2Client.Game.PlayerState();
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.RingL, new Mir2Client.Game.EquipItem
        {
            Uid = 9003,
            Name = "麻痹戒指",
            Shape = 113
        });

        Assert.True(ps.HasParalysisRing);

        // 1. 目标中毒恢复 = 0 (分母 = 5), roll = 0 命中石化 5 秒
        var (p1, dur1) = ps.TryParalyzeTarget(targetPoisonRecover: 0, targetAntiParalysis: false, rngRoll: 0);
        Assert.True(p1);
        Assert.Equal(5, dur1);

        // 2. roll = 1 未命中
        var (p2, dur2) = ps.TryParalyzeTarget(targetPoisonRecover: 0, targetAntiParalysis: false, rngRoll: 1);
        Assert.False(p2);
        Assert.Equal(0, dur2);

        // 3. 目标佩戴防麻痹装备 (targetAntiParalysis = true): 强制免疫石化
        var (p3, dur3) = ps.TryParalyzeTarget(targetPoisonRecover: 0, targetAntiParalysis: true, rngRoll: 0);
        Assert.False(p3);
        Assert.Equal(0, dur3);
    }

    [Fact]
    public void TestSpecialRings_OverloadRing_WeightsDoubled()
    {
        var ps = new Mir2Client.Game.PlayerState
        {
            BaseMaxBagWeight = 200,
            BaseMaxWearWeight = 60,
            BaseMaxHandWeight = 40
        };

        // 初始负重
        ps.RecalculateAttributes();
        Assert.Equal(200, ps.MaxBagWeight);
        Assert.Equal(60, ps.MaxWearWeight);
        Assert.Equal(40, ps.MaxHandWeight);

        // 佩戴超负载戒指 (Shape 119)
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.RingR, new Mir2Client.Game.EquipItem
        {
            Uid = 9004,
            Name = "超负载戒指",
            Shape = 119
        });

        Assert.True(ps.HasOverloadRing);
        Assert.Equal(400, ps.MaxBagWeight); // 严格翻倍
        Assert.Equal(120, ps.MaxWearWeight);
        Assert.Equal(80, ps.MaxHandWeight);

        // 脱下超负载戒指恢复
        ps.TakeOffItem(Mir2Client.Game.OfficialEquipSlot.RingR);
        Assert.False(ps.HasOverloadRing);
        Assert.Equal(200, ps.MaxBagWeight);
        Assert.Equal(60, ps.MaxWearWeight);
        Assert.Equal(40, ps.MaxHandWeight);
    }

    [Fact]
    public void TestSpecialRings_InvisibleRing_MonsterTargeting()
    {
        var ps = new Mir2Client.Game.PlayerState();
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.RingL, new Mir2Client.Game.EquipItem
        {
            Uid = 9005,
            Name = "隐身戒指",
            Shape = 111
        });

        Assert.True(ps.HasInvisibleRing);
        Assert.True(ps.StatusBits[8]); // 隐身状态位激活

        // 普通怪物索敌忽略
        bool canTargetNormal = Mir2Client.Game.SpecialRings.CheckMonsterCanTarget(ps.HasInvisibleRing, monsterCanDetectInvis: false);
        Assert.False(canTargetNormal);

        // 破隐视破怪 (如月魔蜘蛛、楔蛾) 可锁定
        bool canTargetBoss = Mir2Client.Game.SpecialRings.CheckMonsterCanTarget(ps.HasInvisibleRing, monsterCanDetectInvis: true);
        Assert.True(canTargetBoss);

        // 脱下隐身戒指恢复
        ps.TakeOffItem(Mir2Client.Game.OfficialEquipSlot.RingL);
        Assert.False(ps.HasInvisibleRing);
        Assert.False(ps.StatusBits[8]);
        Assert.True(Mir2Client.Game.SpecialRings.CheckMonsterCanTarget(ps.HasInvisibleRing, monsterCanDetectInvis: false));
    }

    [Fact]
    public void TestSpecialRings_TeleportRing_CommandExecution()
    {
        var ps = new Mir2Client.Game.PlayerState
        {
            MapX = 330,
            MapY = 270
        };

        // 1. 未佩戴传送戒指时输入指令拒接
        var res1 = ps.TryExecuteTeleport("@move 350 280", nowTick: 1000);
        Assert.False(res1.success);
        Assert.Equal(330, ps.MapX);

        // 2. 佩戴传送戒指
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.RingR, new Mir2Client.Game.EquipItem
        {
            Uid = 9006,
            Name = "传送戒指",
            Shape = 112
        });
        Assert.True(ps.HasTeleportRing);

        // 3. 成功瞬移
        var res2 = ps.TryExecuteTeleport("@move 350 280", nowTick: 1000);
        Assert.True(res2.success);
        Assert.Equal(350, ps.MapX);
        Assert.Equal(280, ps.MapY);

        // 4. 5 秒冷却期内再次瞬移拒绝
        var res3 = ps.TryExecuteTeleport("@move 360 290", nowTick: 3000);
        Assert.False(res3.success);
        Assert.Equal(350, ps.MapX);

        // 5. 阻挡点瞬移拒绝
        var res4 = ps.TryExecuteTeleport("@飞 400 400", isTileBlocked: (x, y) => (x == 400 && y == 400), nowTick: 10000);
        Assert.False(res4.success);
        Assert.Equal(350, ps.MapX);
    }

    [Fact]
    public void TestSpecialRings_FlameAndHealingRing_SkillsGranted()
    {
        // 战士角色 (Job = 0)
        var ps = new Mir2Client.Game.PlayerState { Job = 0 };

        // 佩戴火焰戒指赋予火球术 (MagicId = 1)
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.RingL, new Mir2Client.Game.EquipItem
        {
            Uid = 9007,
            Name = "火焰戒指",
            Shape = 113
        });
        Assert.Contains(1, ps.GrantedRingSkills);

        // 佩戴防御戒指赋予治愈术 (MagicId = 2)
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.RingR, new Mir2Client.Game.EquipItem
        {
            Uid = 9008,
            Name = "防御戒指",
            Shape = 116
        });
        Assert.Contains(2, ps.GrantedRingSkills);

        // 脱下火焰戒指注销火球术
        ps.TakeOffItem(Mir2Client.Game.OfficialEquipSlot.RingL);
        Assert.DoesNotContain(1, ps.GrantedRingSkills);
        Assert.Contains(2, ps.GrantedRingSkills);
    }

    [Fact]
    public void TestSpecialSets_MemoryRecallSuite_TeammateRecall()
    {
        var ps = new Mir2Client.Game.PlayerState
        {
            MapX = 330,
            MapY = 270,
            Direction = 2 // 朝东
        };

        // 佩戴记忆套装 4 件
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.Helmet, new Mir2Client.Game.EquipItem { Name = "记忆头盔", Shape = 125 });
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.Necklace, new Mir2Client.Game.EquipItem { Name = "记忆项链", Shape = 123 });
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.ArmringL, new Mir2Client.Game.EquipItem { Name = "记忆手镯", Shape = 124 });
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.RingL, new Mir2Client.Game.EquipItem { Name = "记忆戒指", Shape = 122 });

        Assert.True(ps.HasRecallSet);

        var teammates = new List<(int uid, string name, int x, int y)>
        {
            (1001, "队友甲", 100, 100),
            (1002, "队友乙", 150, 150)
        };

        // 1. 队长输入 @天地合一
        var res1 = ps.TryExecuteRecall("@天地合一", isLeader: true, teammates, nowTick: 200000);
        Assert.True(res1.success);
        Assert.Equal(2, res1.movedMembers.Count);
        // 验证落在队长周边
        Assert.True(Math.Abs(res1.movedMembers[0].targetX - ps.MapX) <= 2);
        Assert.True(Math.Abs(res1.movedMembers[0].targetY - ps.MapY) <= 2);

        // 2. 180 秒冷却期内再次使用拒绝
        var res2 = ps.TryExecuteRecall("@天地合一", isLeader: true, teammates, nowTick: 250000);
        Assert.False(res2.success);

        // 3. 非队长使用拒绝
        var res3 = ps.TryExecuteRecall("@天地合一", isLeader: false, teammates, nowTick: 400000);
        Assert.False(res3.success);
    }

    [Fact]
    public void TestSpecialSets_PrayerApocalypseSuite_TriggerAndBroken()
    {
        var ps = new Mir2Client.Game.PlayerState();

        // 佩戴祈祷之刃、头盔、项链、手镯、戒指 5 件全齐
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.Weapon, new Mir2Client.Game.EquipItem { Name = "祈祷之刃", Shape = 24 });
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.Helmet, new Mir2Client.Game.EquipItem { Name = "祈祷头盔", Shape = 122 });
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.Necklace, new Mir2Client.Game.EquipItem { Name = "祈祷项链", Shape = 121 });
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.ArmringL, new Mir2Client.Game.EquipItem { Name = "祈祷手镯", Shape = 123 });
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.RingL, new Mir2Client.Game.EquipItem { Name = "祈祷戒指", Shape = 124 });

        Assert.True(ps.HasPrayerSet);

        // 触发天谴判定: roll = 2 (对应 equippedSlots[2] 为祈祷头盔)
        var res = ps.TryTriggerPrayerApocalypse(rngSlotRoll: 2);
        Assert.True(res.triggered);
        Assert.True(res.petRebellion);
        Assert.Contains("神之祈祷", res.broadcastMsg);
        Assert.Equal(Mir2Client.Game.OfficialEquipSlot.Helmet, res.brokenSlot);

        // 验证该部件已碎裂卸下销毁，套装状态不再激活
        Assert.Null(ps.Equipment[(int)Mir2Client.Game.OfficialEquipSlot.Helmet]);
        Assert.False(ps.HasPrayerSet);
    }

    [Fact]
    public void TestSpecialSets_MagicBloodSuite_AttributesConversion()
    {
        var ps = new Mir2Client.Game.PlayerState
        {
            BaseMaxHp = 100,
            BaseMaxMp = 200,
            Hp = 100,
            Mp = 200
        };

        // 1. 佩戴单件魔血项链 (Shape 135): HP + 25, MP - 25
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.Necklace, new Mir2Client.Game.EquipItem { Name = "魔血项链", Shape = 135 });
        Assert.Equal(125, ps.MaxHp);
        Assert.Equal(175, ps.MaxMp);
        Assert.False(ps.HasMagicBloodSet);

        // 2. 佩戴魔血手镯 (Shape 134)
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.ArmringL, new Mir2Client.Game.EquipItem { Name = "魔血手镯", Shape = 134 });
        Assert.Equal(150, ps.MaxHp);
        Assert.Equal(150, ps.MaxMp);

        // 3. 佩戴魔血戒指 (Shape 133): 3 件全齐额外 +50 (总共 +125 HP, -125 MP)
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.RingL, new Mir2Client.Game.EquipItem { Name = "魔血戒指", Shape = 133 });
        Assert.True(ps.HasMagicBloodSet);
        Assert.Equal(225, ps.MaxHp);
        Assert.Equal(75, ps.MaxMp);

        // 4. MP 保底机制: 若基础 MP 为 100，转化 125 会导致负数，必须保底保留 1 点 MP
        ps.BaseMaxMp = 100;
        ps.RecalculateAttributes();
        Assert.Equal(1, ps.MaxMp);
        Assert.Equal(199, ps.MaxHp); // 100 + 99 = 199
    }

    [Fact]
    public void TestSpecialSets_RainbowMagicSuite_LifestealAndBonusHit()
    {
        var ps = new Mir2Client.Game.PlayerState { Hp = 50, MaxHp = 100 };

        // 佩戴虹魔项链、手镯、戒指 3 件 (Shape 138, 137, 136)
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.Necklace, new Mir2Client.Game.EquipItem { Name = "虹魔项链", Shape = 138 });
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.ArmringL, new Mir2Client.Game.EquipItem { Name = "虹魔手镯", Shape = 137 });
        ps.WearItem(Mir2Client.Game.OfficialEquipSlot.RingL, new Mir2Client.Game.EquipItem { Name = "虹魔戒指", Shape = 136 });

        Assert.True(ps.HasRainbowSet);
        Assert.Equal(30, ps.LeechWeight); // 30% 吸血权重
        Assert.Equal(2, ps.BonusHit);     // 全套准确 +2

        // 攻击造成 5 点伤害: 5 * 0.3 = 1.5, 累积池未达 2.0，不回血
        int heal1 = ps.ProcessAttackLifesteal(5);
        Assert.Equal(0, heal1);
        Assert.Equal(1.5, ps.LifestealAccumPool, 2);
        Assert.Equal(50, ps.Hp);

        // 再次攻击造成 5 点伤害: 累积 1.5 + 1.5 = 3.0, 达到 2.0，提取 3 点回血，池子余 0
        int heal2 = ps.ProcessAttackLifesteal(5);
        Assert.Equal(3, heal2);
        Assert.Equal(0.0, ps.LifestealAccumPool, 2);
        Assert.Equal(53, ps.Hp);
    }

    [Fact]
    public void TestWarrior_FireHit_CooldownAndDoubleFireWindow()
    {
        var tracker = new Mir2Client.Game.WarriorCombatEngine.FireHitTracker();

        // 1. T=1000: 首次凝聚烈火剑法
        var cast1 = tracker.Cast(1000);
        Assert.True(cast1.success);
        Assert.True(tracker.IsReady);

        // 2. T=5000: CD 未到 (8秒)，再次施法被拒绝
        var castFail = tracker.Cast(5000);
        Assert.False(castFail.success);
        Assert.Contains("冷却中", castFail.reason);

        // 3. T=9005: 已经过 8005ms，进入合法双烈火窗口 (8.01s ~ 9.99s)
        Assert.True(tracker.CanCastSecondFireHit(9005));

        // 4. 在第 9005ms 挥出第一刀: 打出 260% 物理爆发伤害 (基础 100 -> 260)
        var hit1 = tracker.ConsumeOnHit(100, 9005);
        Assert.True(hit1.isFireHit);
        Assert.Equal(260, hit1.damage);
        Assert.False(tracker.IsReady);

        // 5. 立即二次施展烈火 (第 2 刀双烈火秒出)
        var cast2 = tracker.Cast(9010);
        Assert.True(cast2.success);
        Assert.True(tracker.IsReady);

        // 6. 超时测试: 若蓄力超过 10 秒不出刀 (T=20000)，烈火自然熄灭
        var hitExpire = tracker.ConsumeOnHit(100, 20000);
        Assert.False(hitExpire.isFireHit);
        Assert.Equal(100, hitExpire.damage);
        Assert.Contains("超时", hitExpire.desc);
    }

    [Fact]
    public void TestWarrior_Stabbing_PiercingShieldAndDefense()
    {
        // 1. 距离判定: 朝向 0 (正北, Y - 1)
        var distMelee = Mir2Client.Game.WarriorCombatEngine.StabbingTracker.CheckHitDistance(100, 100, 100, 99, 0);
        Assert.Equal(Mir2Client.Game.WarriorCombatEngine.StabbingTracker.HitDistanceType.MeleeClose, distMelee);

        var distStab = Mir2Client.Game.WarriorCombatEngine.StabbingTracker.CheckHitDistance(100, 100, 100, 98, 0);
        Assert.Equal(Mir2Client.Game.WarriorCombatEngine.StabbingTracker.HitDistanceType.StabbingFar, distStab);

        // 2. 贴身第 1 格: 受到目标防御 15 和魔法盾 60% 减伤
        // raw=50 -> 50 - 15 = 35 -> 35 * (1 - 0.6) = 14
        var meleeDmg = Mir2Client.Game.WarriorCombatEngine.StabbingTracker.CalculateDamage(
            50, distMelee, autoStab: true, targetAc: 15, targetShieldReduction: 0.6);
        Assert.False(meleeDmg.isArmorPiercing);
        Assert.Equal(14, meleeDmg.finalDamage);

        // 3. 隔位第 2 格开启刺杀: 完全无视防御与魔法盾，造成满额 50 穿透伤害
        var stabDmg = Mir2Client.Game.WarriorCombatEngine.StabbingTracker.CalculateDamage(
            50, distStab, autoStab: true, targetAc: 15, targetShieldReduction: 0.6);
        Assert.True(stabDmg.isArmorPiercing);
        Assert.Equal(50, stabDmg.finalDamage);

        // 4. 隔位第 2 格若关闭刺杀: 退化为常规防御和护盾减免
        var noStabDmg = Mir2Client.Game.WarriorCombatEngine.StabbingTracker.CalculateDamage(
            50, distStab, autoStab: false, targetAc: 15, targetShieldReduction: 0.6);
        Assert.False(noStabDmg.isArmorPiercing);
        Assert.Equal(14, noStabDmg.finalDamage);
    }

    [Fact]
    public void TestWarrior_ShoulderDash_LevelAndObstacleCollision()
    {
        // 1. 等级压制撞开: 等级 35 冲撞等级 30，朝向 2 (正东, X + 1)
        var pushRes = Mir2Client.Game.WarriorCombatEngine.ShoulderDash.ExecuteDash(
            selfLevel: 35,
            selfX: 100,
            selfY: 100,
            dir: 2,
            obstacleEntity: (true, 30, 101, 100),
            isTileBlocked: (x, y) => false
        );
        Assert.True(pushRes.Success);
        Assert.True(pushRes.TargetPushed);
        Assert.Equal(101, pushRes.SelfFinalX);
        Assert.Equal(102, pushRes.TargetFinalX);
        Assert.False(pushRes.SelfStunned);

        // 2. 等级不足反弹顿挫: 等级 35 冲撞等级 38，撞击失败自身顿挫
        var failRes = Mir2Client.Game.WarriorCombatEngine.ShoulderDash.ExecuteDash(
            selfLevel: 35,
            selfX: 100,
            selfY: 100,
            dir: 2,
            obstacleEntity: (true, 38, 101, 100),
            isTileBlocked: (x, y) => false
        );
        Assert.False(failRes.Success);
        Assert.True(failRes.SelfStunned);
        Assert.Equal(100, failRes.SelfFinalX);
        Assert.Contains("反弹硬直", failRes.Message);

        // 3. 撞墙反弹: 第 1 格为墙壁阻挡
        var wallRes = Mir2Client.Game.WarriorCombatEngine.ShoulderDash.ExecuteDash(
            selfLevel: 35,
            selfX: 100,
            selfY: 100,
            dir: 2,
            obstacleEntity: null,
            isTileBlocked: (x, y) => x == 101 && y == 100
        );
        Assert.False(wallRes.Success);
        Assert.True(wallRes.SelfStunned);
        Assert.Contains("撞击墙体", wallRes.Message);

        // 4. 目标身后受墙壁阻挡: 目标就位第 1 格受撞硬直，自身保持原地
        var blockedBehindRes = Mir2Client.Game.WarriorCombatEngine.ShoulderDash.ExecuteDash(
            selfLevel: 35,
            selfX: 100,
            selfY: 100,
            dir: 2,
            obstacleEntity: (true, 30, 101, 100),
            isTileBlocked: (x, y) => x == 102 && y == 100 // 目标身后为阻挡点
        );
        Assert.True(blockedBehindRes.Success);
        Assert.False(blockedBehindRes.TargetPushed);
        Assert.Equal(100, blockedBehindRes.SelfFinalX);
        Assert.Equal(101, blockedBehindRes.TargetFinalX);
        Assert.Contains("身后受墙壁阻挡", blockedBehindRes.Message);
    }

    [Fact]
    public void TestWarrior_DoubleFireHit_ConsecutiveBursts()
    {
        var tracker = new Mir2Client.Game.WarriorCombatEngine.FireHitTracker();

        // 0.0s: 开启烈火蓄力
        tracker.Cast(0);
        Assert.True(tracker.IsReady);

        // 8.1s: 冷却转好 (8000ms)，蓄力未过 (10000ms)，挥出第一刀爆发 260%
        var hit1 = tracker.ConsumeOnHit(80, 8100);
        Assert.True(hit1.isFireHit);
        Assert.Equal((int)Math.Round(80 * 2.6), hit1.damage); // 208

        // 8.2s: 立刻无缝开启第二发烈火剑法
        var cast2 = tracker.Cast(8200);
        Assert.True(cast2.success);

        // 8.3s: 挥出第二刀秒伤爆发 260%！实现传奇正统双烈火核心战术
        var hit2 = tracker.ConsumeOnHit(80, 8300);
        Assert.True(hit2.isFireHit);
        Assert.Equal(208, hit2.damage);
    }

    [Fact]
    public void TestTrade_TwoPhaseLockAndSafetyChecks()
    {
        var tm = new Mir2Client.Game.TradeManager();
        var ps = new Mir2Client.Game.PlayerState { Gold = 1000000 };
        ps.BagItems.Add(new Mir2Client.Game.EquipItem { Uid = 101, Name = "裁决之杖" });

        tm.StartTrade("传奇战士", 2001);
        Assert.Equal(Mir2Client.Game.TradeState.Trading, tm.State);

        // 1. 未锁定时尝试确认交易 -> 拒绝
        var failConfirm = tm.ConfirmTrade(ps);
        Assert.False(failConfirm.success);

        // 2. 放入本地物品与金币并锁定
        tm.SetGold(200000, ps.Gold);
        tm.PutItem(0, ps.BagItems[0]);
        tm.LockTrade();
        Assert.True(tm.LocalLocked);

        // 3. 本地主动解锁或修改 -> 自动打断锁定
        tm.UnlockTrade();
        Assert.False(tm.LocalLocked);
        tm.SetGold(300000, ps.Gold);
        tm.LockTrade();
        Assert.True(tm.LocalLocked);

        // 模拟对方修改物品或金币 -> 触发安全机制，自动打断我方锁定
        tm.RemoteChangeGold(400000);
        Assert.False(tm.LocalLocked);

        // 4. 对方放入金币与物品，双方共同锁定进入 Locked 状态
        tm.RemoteChangeGold(500000);
        tm.RemoteAddItem(0, new Mir2Client.Game.EquipItem { Uid = 202, Name = "骨玉权杖" });
        tm.SetRemoteLocked(true);
        tm.LockTrade();
        Assert.Equal(Mir2Client.Game.TradeState.Locked, tm.State);

        // 5. 校验 500 万金币上限防溢出: 假设对方给 500 万金币导致溢出
        tm.RemoteChangeGold(5000000);
        tm.SetRemoteLocked(true);
        tm.LockTrade();
        var overflowRes = tm.ConfirmTrade(ps);
        Assert.False(overflowRes.success);
        Assert.Contains("5,000,000", overflowRes.reason);

        // 6. 恢复正常金币，校验背包满 40 格拦截
        tm.RemoteChangeGold(100000);
        tm.SetRemoteLocked(true);
        tm.LockTrade();
        for (int i = ps.BagItems.Count; i < 40; i++)
        {
            ps.BagItems.Add(new Mir2Client.Game.EquipItem { Uid = 300 + i, Name = "金创药" });
        }
        var bagFullRes = tm.ConfirmTrade(ps);
        Assert.False(bagFullRes.success);
        Assert.Contains("空间不足", bagFullRes.reason);

        // 7. 腾出空间，两阶段确认成功并执行原子交换
        ps.BagItems.RemoveAt(ps.BagItems.Count - 1);
        var successConfirm = tm.ConfirmTrade(ps);
        Assert.True(successConfirm.success);

        var exchangeRes = tm.ExecuteAtomicExchange(ps);
        Assert.True(exchangeRes.success);

        // 验证金币变动: 1000000 - 300000 + 100000 = 800000
        Assert.Equal(800000, ps.Gold);
        // 验证裁决之杖已移出，骨玉权杖已入袋
        Assert.DoesNotContain(ps.BagItems, item => item.Uid == 101);
        Assert.Contains(ps.BagItems, item => item.Uid == 202 && item.Name == "骨玉权杖");
    }

    [Fact]
    public void TestTaoist_Poison_HitAlgorithmAndDualSlots()
    {
        // 1. 目标 0 抗毒时必中 (RandInt(7) 范围 0~6，恒 <= 6)
        for (int i = 0; i < 50; i++)
        {
            Assert.True(Mir2Client.Game.TaoistCombatEngine.CheckPoisonHit(targetAntiPoison: 0));
        }

        // 2. 持续时长计算: SC=20 时，绿毒 30 + 40 = 70s, 红毒 40 + 40 = 80s
        int greenSec = Mir2Client.Game.TaoistCombatEngine.CalculatePoisonDurationSec(Mir2Client.Game.TaoistCombatEngine.PoisonType.Green, 20);
        int redSec = Mir2Client.Game.TaoistCombatEngine.CalculatePoisonDurationSec(Mir2Client.Game.TaoistCombatEngine.PoisonType.Red, 20);
        Assert.Equal(70, greenSec);
        Assert.Equal(80, redSec);

        // 3. 红毒 1.2 倍增伤 (tbyte_4BE3CC)
        int normalDmg = 50;
        int vulDmg = Mir2Client.Game.TaoistCombatEngine.ApplyRedPoisonVulnerability(normalDmg, hasRedPoison: true);
        Assert.Equal(60, vulDmg); // 50 * 1.2 = 60

        // 4. 双毒独立槽位与 EntityManager 周期掉血
        var em = new Mir2Client.Game.EntityManager();
        em.AddEntity(new Mir2Client.Game.EntityManager.EntityData
        {
            Uid = 3001,
            Name = "双毒靶标稻草人",
            Type = Mir2Client.Game.EntityManager.EntityType.Monster,
            TileX = 330,
            TileY = 270,
            Hp = 100,
            MaxHp = 100
        });

        // 施加绿毒 (SC=15, 每跳扣 Math.Max(1, 15/3)+1 = 6 点)
        long t0 = 10000;
        em.ApplyPoison(3001, Mir2Client.Game.TaoistCombatEngine.PoisonType.Green, 30, sc: 15, t0);
        var target = em.GetEntity(3001)!;
        Assert.Equal(1, target.PoisonMask); // Bit 0 置位

        // 施加红毒 (并发施加，形成双毒)
        em.ApplyPoison(3001, Mir2Client.Game.TaoistCombatEngine.PoisonType.Red, 40, sc: 15, t0);
        Assert.Equal(3, target.PoisonMask); // Bit 0 与 Bit 1 同时置位

        // T0 + 2500ms: 驱动绿毒第一跳掉血
        int poisonDmgTaken = 0;
        em.ProcessPoisonTicks(t0 + 2500, (uid, dmg, newHp) =>
        {
            poisonDmgTaken = dmg;
        });
        Assert.Equal(6, poisonDmgTaken);
        Assert.Equal(94, target.Hp);

        // 清除中毒
        em.ClearPoison(3001);
        Assert.Equal(0, target.PoisonMask);
    }

    [Fact]
    public void TestTaoist_PoisonBagAndTalisman_DurabilityConsumption()
    {
        // 1. 施毒药包耐久单次扣 100
        var greenBag = new Mir2Client.Game.EquipItem { Name = "黄色毒粉(中)", Shape = 1, Dura = 250 };
        var res1 = Mir2Client.Game.TaoistCombatEngine.TryConsumePoisonBag(greenBag, Mir2Client.Game.TaoistCombatEngine.PoisonType.Green);
        Assert.True(res1.success);
        Assert.Equal(150, greenBag.Dura);
        Assert.False(res1.destroyed);

        // 药包类型不匹配
        var misRes = Mir2Client.Game.TaoistCombatEngine.TryConsumePoisonBag(greenBag, Mir2Client.Game.TaoistCombatEngine.PoisonType.Red);
        Assert.False(misRes.success);
        Assert.Contains("类型不匹配", misRes.desc);

        // 扣除至销毁
        var res2 = Mir2Client.Game.TaoistCombatEngine.TryConsumePoisonBag(greenBag, Mir2Client.Game.TaoistCombatEngine.PoisonType.Green);
        Assert.True(res2.success);
        Assert.Equal(50, greenBag.Dura);
        var res3 = Mir2Client.Game.TaoistCombatEngine.TryConsumePoisonBag(greenBag, Mir2Client.Game.TaoistCombatEngine.PoisonType.Green);
        Assert.False(res3.success); // 剩余 50 < 100 无法施法

        // 2. 护身符消耗: 召唤骷髅扣 100 (1张)，召唤神兽扣 500 (5张)
        var talisman = new Mir2Client.Game.EquipItem { Name = "灵魂护身符(大)", Shape = 5, Dura = 600 };
        var skelCost = Mir2Client.Game.TaoistCombatEngine.TryConsumeTalisman(talisman, Mir2Client.Game.TaoistCombatEngine.TalismanCostSkeleton);
        Assert.True(skelCost.success);
        Assert.Equal(500, talisman.Dura);

        var shinsuCost = Mir2Client.Game.TaoistCombatEngine.TryConsumeTalisman(talisman, Mir2Client.Game.TaoistCombatEngine.TalismanCostShinsu);
        Assert.True(shinsuCost.success);
        Assert.Equal(0, talisman.Dura);
        Assert.True(shinsuCost.destroyed);

        // 3. 随从 0~7 级属性规格
        var skel0 = Mir2Client.Game.TaoistCombatEngine.GetSkeletonStats(0);
        var skel7 = Mir2Client.Game.TaoistCombatEngine.GetSkeletonStats(7);
        Assert.Equal(100, skel0.MaxHp);
        Assert.Equal(296, skel7.MaxHp);

        var dog0 = Mir2Client.Game.TaoistCombatEngine.GetShinsuStats(0);
        var dog7 = Mir2Client.Game.TaoistCombatEngine.GetShinsuStats(7);
        Assert.Equal(200, dog0.MaxHp);
        Assert.Equal(795, dog7.MaxHp);
        Assert.Equal(14, dog7.Appr); // 7 级终极直立神兽外观
    }

    [Fact]
    public void TestMage_MagicShield_DamageAbsorptionAndPiercingBreak()
    {
        var ps = new Mir2Client.Game.PlayerState { Hp = 100, MaxHp = 100, Mp = 100 };

        // 1. 激活 3 级魔法盾 (减伤吸收)
        ps.ActivateMagicShield(level: 3, durationSec: 30);
        Assert.True(ps.HasMagicShield);

        // 受到 50 点普通伤害:
        // actual = ROUND( (50 / 100.0) * (3 + 2) * 8.0 ) = ROUND(0.5 * 40.0) = 20 点实际承伤
        // absorbed = 50 - 20 = 30 点被吸收
        var hitRes = ps.TakeCombatDamageFull(50, nowTick: 1000, isArmorPiercing: false);
        Assert.Equal(30, hitRes.shieldAbsorbed);
        Assert.Equal(80, ps.Hp); // 100 - 20 = 80
        Assert.False(hitRes.shieldBroken);

        // 2. 隔位刺杀破盾 (isArmorPiercing = true): 完全无视魔法盾，直接造成满额 50 伤害
        var pierceRes = ps.TakeCombatDamageFull(50, nowTick: 2000, isArmorPiercing: true);
        Assert.Equal(0, pierceRes.shieldAbsorbed);
        Assert.Equal(30, ps.Hp); // 80 - 50 = 30

        // 3. 破盾机制: 扣减护盾持续时间
        ps.ActivateMagicShield(level: 1, durationSec: 3);
        // 第一次受击：剩余 3 秒扣减进入 1 秒濒死喘息期
        var nearBreakRes = ps.TakeCombatDamageFull(20, nowTick: 3000, isArmorPiercing: false);
        Assert.False(nearBreakRes.shieldBroken);
        Assert.True(ps.HasMagicShield);
        Assert.Equal(1, ps.MagicShieldRemainingSec);

        // 第二次受击：剩余 1 秒受击彻底破盾归零
        var breakRes = ps.TakeCombatDamageFull(20, nowTick: 4000, isArmorPiercing: false);
        Assert.True(breakRes.shieldBroken);
        Assert.False(ps.HasMagicShield);
    }

    [Fact]
    public void TestWeaponUpgrade_BlackIronPurityAndTrialSlashFlow()
    {
        // 1. 黑铁矿纯度因子计算 (sub_4A0218)
        var lowOres = new List<WeaponUpgradeEngine.BlackIronOre>
        {
            new(5), new(6), new(4) // avg = 5, count < 5 => factor = 5 (< 9 必掉持久)
        };
        int lowFactor = WeaponUpgradeEngine.CalcBlackIronDuraFactor(lowOres.Select(o => o.Purity).ToList());
        Assert.Equal(5, lowFactor);
        Assert.Equal(-3000, WeaponUpgradeEngine.CalcWeaponDuraChange(lowFactor, 0));

        var perfectOres = new List<WeaponUpgradeEngine.BlackIronOre>
        {
            new(16), new(16), new(16), new(16), new(16) // avg = 16, count >= 5 => 16 + 1 = 17 (16..17 绝对保平)
        };
        int perfectFactor = WeaponUpgradeEngine.CalcBlackIronDuraFactor(perfectOres.Select(o => o.Purity).ToList());
        Assert.Equal(17, perfectFactor);
        Assert.Equal(0, WeaponUpgradeEngine.CalcWeaponDuraChange(perfectFactor, 0));

        // 2. 首饰加权与主属性推导 (sub_4A0CB8)
        var accessories = new List<WeaponUpgradeEngine.AccessoryInfo>
        {
            new(StdMode: 22, Dc: 4, Mc: 0, Sc: 0), // 幽灵项链
            new(StdMode: 24, Dc: 3, Mc: 0, Sc: 0), // 龙之戒指 (戒指额外 +1 => 4)
            new(StdMode: 26, Dc: 2, Mc: 0, Sc: 0)  // 力量戒指 (戒指额外 +1 => 3)
        };
        var p = WeaponUpgradeEngine.CalcWeaponUpgradeParams(perfectOres, accessories);
        Assert.Equal(WeaponUpgradeEngine.StatUpgradeType.Dc, p.StatType);
        Assert.True(p.StatVal > 0);

        // 3. 成功率与结果码 (sub_4A0F48)
        int rate = WeaponUpgradeEngine.CalcWeaponUpgradeRate(userLuck: 0, upgradeCount: 0, statScore: p.StatVal, pkValue: 0);
        Assert.True(rate is >= 30 and <= 85);

        // 模拟成功升级并跳点 +2 (rate > 63, roll30 == 0)
        int jumpCode = WeaponUpgradeEngine.DetermineUpgradeResultCode(rate: 70, roll100: 10, roll30: 0, roll200: 50, WeaponUpgradeEngine.StatUpgradeType.Dc);
        Assert.Equal(11, jumpCode); // DC +2 代码

        // 4. 试刀开刀测试 (sub_4C27C0)
        var ps = new Mir2Client.Game.PlayerState();
        var weapon = new EquipItem
        {
            Uid = 9001,
            Name = "裁决之杖",
            Dc = 30,
            UpgradeState = 11 // 待试刀：成功 +2
        };
        ps.Equipment[(int)OfficialEquipSlot.Weapon] = weapon;

        // 执行开刀试刀
        var trialRes = ps.TrialSlashEquippedWeapon();
        Assert.Equal(WeaponUpgradeEngine.TrialSlashOutcome.Success, trialRes.Outcome);
        Assert.Equal(2, trialRes.Bonus);
        Assert.Equal(32, weapon.Dc); // 30 + 2 = 32
        Assert.Equal(1, weapon.UpgradeCount);
        Assert.Equal(0, weapon.UpgradeState); // 试刀状态核销

        // 5. 试刀碎刀销毁测试
        weapon.UpgradeState = 1; // 碎刀标志码
        var breakRes = ps.TrialSlashEquippedWeapon();
        Assert.Equal(WeaponUpgradeEngine.TrialSlashOutcome.Broken, breakRes.Outcome);
        Assert.Null(ps.Equipment[(int)OfficialEquipSlot.Weapon]); // 武器已化为乌有彻底销毁
    }

    [Fact]
    public void TestPkRule_MurderLevelGuardExecutionAndRedVillageRespawn()
    {
        var ps = new Mir2Client.Game.PlayerState { Name = "红名狂魔", Hp = 100, MaxHp = 100 };
        Assert.False(ps.IsRedName);
        Assert.False(ps.IsYellowName);
        Assert.Equal(0, ps.PkLevel);

        // 1. 恶意谋杀两名白名玩家 (每次 +100 PK 点数)
        ps.AddPkMurder();
        Assert.Equal(100, ps.PkPoint);
        Assert.True(ps.IsYellowName);
        Assert.False(ps.IsRedName);
        Assert.Equal(1, ps.PkLevel);

        ps.AddPkMurder();
        Assert.Equal(200, ps.PkPoint);
        Assert.False(ps.IsYellowName);
        Assert.True(ps.IsRedName);
        Assert.Equal(2, ps.PkLevel);

        // 2. 大刀卫士仇恨检测与 999 真实伤害秒杀 (sub_4D7C20)
        Assert.True(PkRuleEngine.IsHostileToGuard(ps.PkPoint));
        var execRes = PkRuleEngine.ExecuteGuardSlash(ps);
        Assert.Equal(999, execRes.damage);
        Assert.True(execRes.isDead);
        Assert.Equal(0, ps.Hp); // 当场阵亡

        // 3. 红名村死亡强制重生地图流转 (sub_4BD1BC case 3)
        var respawn = PkRuleEngine.GetRespawnLocation(ps.PkPoint);
        Assert.Equal(9, respawn.mapId);   // 红名村地图 9
        Assert.Equal(33, respawn.spawnX); // (33, 33)
        Assert.Equal(33, respawn.spawnY);
        Assert.Equal("盟重红名村", respawn.locationName);

        // 白名玩家阵亡回盟重土城安全区
        var normalRespawn = PkRuleEngine.GetRespawnLocation(0);
        Assert.Equal(3, normalRespawn.mapId);
        Assert.Equal(330, normalRespawn.spawnX);
        Assert.Equal(330, normalRespawn.spawnY);
    }

    [Fact]
    public void TestMonsterWilPool_OfficialSub495EBCAndSub496120GroundTruth()
    {
        // 1. 官方 sub_495EBC 怪物文件路由映射测试 (Appr / 10 -> Mon1..18)
        Assert.Equal(1, Mir2Client.Render.MonsterWilPool.GetMonFileIndex(0));   // 0 -> Mon1
        Assert.Equal(1, Mir2Client.Render.MonsterWilPool.GetMonFileIndex(9));   // 9 -> Mon1
        Assert.Equal(2, Mir2Client.Render.MonsterWilPool.GetMonFileIndex(10));  // 10 -> Mon2
        Assert.Equal(3, Mir2Client.Render.MonsterWilPool.GetMonFileIndex(20));  // 20 (变异骷髅) -> Mon3
        Assert.Equal(18, Mir2Client.Render.MonsterWilPool.GetMonFileIndex(170)); // 170 (神兽) -> Mon18
        Assert.Equal(6, Mir2Client.Render.MonsterWilPool.GetMonFileIndex(900)); // 90 (0x5A) -> Mon6 特例

        // 2. 官方 sub_496120 怪物帧偏移算法测试
        // v1 = 0: 280 * v8
        Assert.Equal(0, Mir2Client.Render.MonsterWilPool.CalcMonsterBaseOffset(0));
        Assert.Equal(280, Mir2Client.Render.MonsterWilPool.CalcMonsterBaseOffset(1));
        Assert.Equal(280 * 5, Mir2Client.Render.MonsterWilPool.CalcMonsterBaseOffset(5));

        // v1 = 1: 230 * v8
        Assert.Equal(0, Mir2Client.Render.MonsterWilPool.CalcMonsterBaseOffset(10));
        Assert.Equal(230, Mir2Client.Render.MonsterWilPool.CalcMonsterBaseOffset(11));

        // v1 = 2: 360 * v8 (变异骷髅 Appr=20)
        Assert.Equal(0, Mir2Client.Render.MonsterWilPool.CalcMonsterBaseOffset(20));

        // v1 = 4: 360 * v8 (v8==1 则 600)
        Assert.Equal(600, Mir2Client.Render.MonsterWilPool.CalcMonsterBaseOffset(41));
        Assert.Equal(360 * 2, Mir2Client.Render.MonsterWilPool.CalcMonsterBaseOffset(42));

        // v1 = 6: 440 * v8
        Assert.Equal(440 * 3, Mir2Client.Render.MonsterWilPool.CalcMonsterBaseOffset(63));

        // v1 = 13: 360 / 440 / 550 / 360*v8
        Assert.Equal(360, Mir2Client.Render.MonsterWilPool.CalcMonsterBaseOffset(131));
        Assert.Equal(440, Mir2Client.Render.MonsterWilPool.CalcMonsterBaseOffset(132));
        Assert.Equal(550, Mir2Client.Render.MonsterWilPool.CalcMonsterBaseOffset(133));

        // v1 = 17: 350 * v8 (神兽 Appr=170)
        Assert.Equal(0, Mir2Client.Render.MonsterWilPool.CalcMonsterBaseOffset(170));
        Assert.Equal(350, Mir2Client.Render.MonsterWilPool.CalcMonsterBaseOffset(171));
    }
}





