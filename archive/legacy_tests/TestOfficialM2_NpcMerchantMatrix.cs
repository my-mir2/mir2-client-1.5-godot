using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mir2.Server.Core;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2.Shared.Network.HPSocket;
using Mir2.Shared.Protocols.Modern;
using Xunit;

namespace Mir2Client.Tests;

public class TestOfficialM2_NpcMerchantMatrix
{
    [Fact]
    public void TestOfficialM2_NpcScript_Pages_And_Selection()
    {
        var engine = new NpcMerchantEngine();
        var npc = new NpcEntity
        {
            Uid = 20001,
            Name = "比奇药剂师",
            MapId = 0,
            X = 330,
            Y = 270
        };
        npc.ScriptPages["@main"] = "欢迎光临比奇药房！\\ \\<购买药品/@buy>\\ \\<出售物品/@sell>\\ \\<离开/@exit>";
        npc.ScriptPages["@buy"] = "请选择需要购买的药水：\\ \\<金创药(小)/@buy_hp_s>\\ \\<返回/@main>";
        npc.ScriptPages["@sell"] = "我可以高价回收各种草药。\\ \\<返回/@main>";

        engine.RegisterNpc(npc);

        // 1. 请求主页
        string home = engine.GetNpcHomePage(20001);
        Assert.Contains("欢迎光临比奇药房", home);
        Assert.Contains("@buy", home);

        // 2. 选择 @buy 选项
        string buyPage = engine.SelectScriptOption(20001, "@buy");
        Assert.Contains("请选择需要购买的药水", buyPage);
        Assert.Contains("@buy_hp_s", buyPage);

        // 3. 错误选项回退
        string notFound = engine.SelectScriptOption(20001, "@unknown");
        Assert.Contains("不存在", notFound);
    }

    [Fact]
    public void TestOfficialM2_Merchant_SpecialRepair_Zero_MaxDura_Loss()
    {
        var engine = new NpcMerchantEngine();
        var player = new PlayerState { Gold = 50000 };

        // 槽位 1: 武器裁决之杖，当前持久仅剩 500，最大持久 3000
        var weapon = new EquipItem
        {
            Uid = 9001,
            Name = "裁决之杖",
            Dura = 500,
            MaxDura = 3000,
            Weight = 50
        };
        player.Equipment[(int)OfficialEquipSlot.Weapon] = weapon;

        var session = new ClientSession(1, null!) { State = player };

        // 磨损量 = 2500，特修 3 倍费用 = 7500 金币
        var (success, cost, name, errorMsg) = engine.SpecialRepairItem(session, OfficialEquipSlot.Weapon);

        Assert.True(success, errorMsg);
        Assert.Equal(7500, cost);
        Assert.Equal("裁决之杖", name);
        Assert.Equal(50000 - 7500, player.Gold);

        // 验证特修核心真值: 当前持久彻底回满且最大持久上限绝不掉点 (0 损持久上限)
        Assert.Equal(3000, weapon.Dura);
        Assert.Equal(3000, weapon.MaxDura);
    }

    private static int GetRandomTestPort() => TestNetworkPortHelper.GetNextPort();

    private static byte[] BuildPack(ushort msgId, string payloadStr = "", ushort seq = 0)
    {
        byte[] payloadBytes = string.IsNullOrEmpty(payloadStr) ? Array.Empty<byte>() : Encoding.UTF8.GetBytes(payloadStr);
        byte[] packet = new byte[BinaryProtocol.HeaderSize + payloadBytes.Length];
        var span = packet.AsSpan();
        System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(span[0..2], BinaryProtocol.DefaultMagic);
        System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(span[2..4], msgId);
        System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(span[4..6], (ushort)payloadBytes.Length);
        System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(span[6..8], seq);
        if (payloadBytes.Length > 0)
        {
            payloadBytes.CopyTo(packet, BinaryProtocol.HeaderSize);
        }
        return packet;
    }

    private static async Task SendPackAsync(System.Net.Sockets.NetworkStream stream, ushort msgId, string payloadStr = "")
    {
        byte[] packet = BuildPack(msgId, payloadStr);
        await stream.WriteAsync(packet);
        await stream.FlushAsync();
    }

    private static async Task<string> ReadUntilContainsPackAsync(System.Net.Sockets.NetworkStream stream, string target, int timeoutMs = 2500)
    {
        var decoder = new HpPackStreamDecoder();
        var sb = new StringBuilder();
        byte[] buf = new byte[4096];
        long deadline = Environment.TickCount64 + timeoutMs;
        while (Environment.TickCount64 < deadline)
        {
            if (stream.DataAvailable)
            {
                int n = await stream.ReadAsync(buf);
                if (n > 0)
                {
                    decoder.FeedData(buf.AsSpan(0, n), (in BinaryProtocol.PacketHeader header, ReadOnlySpan<byte> payload) =>
                    {
                        int recog = 0, param = 0, tag = 0, series = 0;
                        string extra = string.Empty;
                        if (payload.Length >= 16)
                        {
                            recog = System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(payload[0..4]);
                            param = System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(payload[4..8]);
                            tag = System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(payload[8..12]);
                            series = System.Buffers.Binary.BinaryPrimitives.ReadInt32LittleEndian(payload[12..16]);
                            if (payload.Length > 16)
                            {
                                extra = Encoding.UTF8.GetString(payload[16..]);
                            }
                        }
                        else if (payload.Length > 0)
                        {
                            extra = Encoding.UTF8.GetString(payload);
                        }
                        string packSummary = $"#{header.MsgId}/{recog}/{param}/{tag}/{series}/{extra}!";
                        sb.Append(packSummary);
                        if (!string.IsNullOrEmpty(extra)) sb.Append(extra);
                    });

                    if (!string.IsNullOrEmpty(target) && sb.ToString().Contains(target))
                        return sb.ToString();
                }
            }
            await Task.Delay(10);
        }
        return sb.ToString();
    }

    [Fact]
    public async Task TestE2E_NpcInteraction_BuyItem_And_SellItem_Cycle()
    {
        int testPort = GetRandomTestPort();
        var mapManager = new MapManager();
        var npcEngine = new NpcMerchantEngine();

        // 注册比奇城铁匠 NPC (Uid: 20002)
        var weaponShopNpc = new NpcEntity
        {
            Uid = 20002,
            Name = "铁匠",
            MapId = 0,
            X = 335,
            Y = 275
        };
        weaponShopNpc.ScriptPages["@main"] = "欢迎来到铁匠铺！\\ \\<购买武器/@buy>\\ \\<出售装备/@sell>";
        weaponShopNpc.Goods.Add(new MerchantGoods
        {
            GoodsId = 101,
            Name = "青铜剑",
            Price = 1200,
            Weight = 9,
            MaxDura = 1500
        });
        npcEngine.RegisterNpc(weaponShopNpc);

        var dispatcher = new PacketDispatcher(mapManager, regenEngine: null, npcEngine: npcEngine);
        var gateway = new TcpGateway(testPort);
        gateway.PacketReceivedHandler = (session, pkt) => dispatcher.Dispatch(session, pkt);
        gateway.BinaryPacketReceivedHandler = (session, header, payload) => dispatcher.DispatchBinary(session, header, payload);
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            using var client = new System.Net.Sockets.TcpClient();
            await client.ConnectAsync("127.0.0.1", testPort);
            var stream = client.GetStream();

            // 1. 玩家进入游戏
            await SendPackAsync(stream, 103, "购物新手/334/275");

            await Task.Delay(200);

            var map = mapManager.GetMap(0);
            var buffer = new System.Collections.Generic.List<ClientSession>();
            map?.Aoi.GetEntitiesInNinePatch(334, 275, buffer);
            var session = buffer.Find(s => s.CharacterName == "购物新手");
            Assert.NotNull(session);
            session.State!.Gold = 5000; // 初始 5000 金币

            byte[] flushBuf = new byte[4096];
            while (stream.DataAvailable) _ = await stream.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 2. 客户端点击铁匠 NPC: CM_MERCHANTHOMEPAGE (1010)
            await SendPackAsync(stream, 1010, "20002");

            // 必须收到 NPC 对话界面: SM_NPCDIALOG (54)
            string resp1 = await ReadUntilContainsPackAsync(stream, $"#{ProtocolConsts.SM_NPCDIALOG}/", timeoutMs: 3000);
            Assert.Contains($"#{ProtocolConsts.SM_NPCDIALOG}/", resp1);
            Assert.Contains("欢迎来到铁匠铺", resp1);

            while (stream.DataAvailable) _ = await stream.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 3. 购买青铜剑 (GoodsId: 101, 价格 1200): CM_USERBUYITEM (1014)
            await SendPackAsync(stream, 1014, "20002/101");

            // 必须收到购买成功 SM_BUY_OK_DELPHI (648)、金币扣减 SM_GOLDCHANGED_DELPHI (653)、入包 SM_ADDITEM (200)
            string resp2 = await ReadUntilContainsPackAsync(stream, $"#{ProtocolConsts.SM_BUY_OK_DELPHI}/", timeoutMs: 3000);
            Assert.Contains($"#{ProtocolConsts.SM_BUY_OK_DELPHI}/", resp2);
            Assert.Contains($"#{ProtocolConsts.SM_GOLDCHANGED_DELPHI}/", resp2);
            Assert.Contains($"#{ProtocolConsts.SM_ADDITEM}/", resp2);
            Assert.Contains("青铜剑", resp2);

            Assert.Equal(5000 - 1200, session.State.Gold);
            Assert.Contains(session.State.BagItems, item => item.Name == "青铜剑");
            var sword = session.State.BagItems.First(item => item.Name == "青铜剑");

            while (stream.DataAvailable) _ = await stream.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 4. 将青铜剑出售给铁匠: CM_USERSELLITEM (1013)
            await SendPackAsync(stream, 1013, $"{sword.Uid}");

            // 必须收到出售成功 SM_SELL_OK_DELPHI (650) 与金币增加
            string resp3 = await ReadUntilContainsPackAsync(stream, $"#{ProtocolConsts.SM_SELL_OK_DELPHI}/", timeoutMs: 3000);
            Assert.Contains($"#{ProtocolConsts.SM_SELL_OK_DELPHI}/", resp3);
            Assert.Contains($"#{ProtocolConsts.SM_GOLDCHANGED_DELPHI}/", resp3);

            // 背包中青铜剑被移除，金币回血
            Assert.DoesNotContain(session.State.BagItems, item => item.Name == "青铜剑");
            Assert.True(session.State.Gold > 3800);
        }
        finally
        {
            tickEngine.Stop();
            gateway.Stop();
        }
    }
}
