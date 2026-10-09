using System;
using System.Text;
using System.Threading.Tasks;
using Mir2.Server.Core;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
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

    [Fact]
    public async Task TestE2E_NpcInteraction_BuyItem_And_SellItem_Cycle()
    {
        int testPort = 17011;
        var mapManager = new MapManager();
        var npcEngine = new NpcMerchantEngine();

        var weaponShopNpc = new NpcEntity
        {
            Uid = 20002,
            Name = "比奇铁匠",
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
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            using var client = new System.Net.Sockets.TcpClient();
            await client.ConnectAsync("127.0.0.1", testPort);
            var stream = client.GetStream();

            // 1. 玩家进入游戏
            await stream.WriteAsync(Encoding.UTF8.GetBytes("#103/购物新手/334/275!"));
            await stream.FlushAsync();

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
            byte[] clickPkt = Encoding.UTF8.GetBytes("#1010/20002!");
            await stream.WriteAsync(clickPkt);
            await stream.FlushAsync();

            await Task.Delay(150);

            // 必须收到 NPC 对话界面: SM_NPCDIALOG (54)
            Assert.True(stream.DataAvailable);
            byte[] buf1 = new byte[4096];
            int len1 = await stream.ReadAsync(buf1, 0, buf1.Length);
            string resp1 = Encoding.UTF8.GetString(buf1, 0, len1);
            Assert.Contains($"#{ProtocolConsts.SM_NPCDIALOG}/", resp1);
            Assert.Contains("欢迎来到铁匠铺", resp1);

            while (stream.DataAvailable) _ = await stream.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 3. 购买青铜剑 (GoodsId: 101, 价格 1200): CM_USERBUYITEM (1014)
            byte[] buyPkt = Encoding.UTF8.GetBytes("#1014/20002/101!");
            await stream.WriteAsync(buyPkt);
            await stream.FlushAsync();

            await Task.Delay(150);

            // 必须收到购买成功 SM_BUY_OK_DELPHI (648)、金币扣减 SM_GOLDCHANGED_DELPHI (653)、入包 SM_ADDITEM (200)
            Assert.True(stream.DataAvailable);
            byte[] buf2 = new byte[4096];
            int len2 = await stream.ReadAsync(buf2, 0, buf2.Length);
            string resp2 = Encoding.UTF8.GetString(buf2, 0, len2);
            Assert.Contains($"#{ProtocolConsts.SM_BUY_OK_DELPHI}/", resp2);
            Assert.Contains($"#{ProtocolConsts.SM_GOLDCHANGED_DELPHI}/", resp2);
            Assert.Contains($"#{ProtocolConsts.SM_ADDITEM}/", resp2);
            Assert.Contains("青铜剑", resp2);

            Assert.Equal(5000 - 1200, session.State.Gold);
            Assert.Single(session.State.BagItems);
            var sword = session.State.BagItems[0];
            Assert.Equal("青铜剑", sword.Name);

            while (stream.DataAvailable) _ = await stream.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 4. 将青铜剑出售给铁匠: CM_USERSELLITEM (1013)
            byte[] sellPkt = Encoding.UTF8.GetBytes($"#1013/{sword.Uid}!");
            await stream.WriteAsync(sellPkt);
            await stream.FlushAsync();

            await Task.Delay(150);

            // 必须收到出售成功 SM_SELL_OK_DELPHI (650) 与金币增加
            Assert.True(stream.DataAvailable);
            byte[] buf3 = new byte[4096];
            int len3 = await stream.ReadAsync(buf3, 0, buf3.Length);
            string resp3 = Encoding.UTF8.GetString(buf3, 0, len3);
            Assert.Contains($"#{ProtocolConsts.SM_SELL_OK_DELPHI}/", resp3);
            Assert.Contains($"#{ProtocolConsts.SM_GOLDCHANGED_DELPHI}/", resp3);

            // 背包被清空，金币回血
            Assert.Empty(session.State.BagItems);
            Assert.True(session.State.Gold > 3800);
        }
        finally
        {
            tickEngine.Stop();
            gateway.Stop();
        }
    }
}
