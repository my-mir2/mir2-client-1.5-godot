using System;
using System.Text;
using System.Threading.Tasks;
using Mir2.Server.Core;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Models;
using Mir2.Shared.Protocols;
using Mir2.Shared.Rules;
using Xunit;

namespace Mir2Client.Tests;

public class TestOfficialM2_DropEngineMatrix
{
    [Fact]
    public void TestOfficialM2_RedName_BagItems_100Percent_Drop()
    {
        // 1. 红名玩家 (PK >= 200)
        var redPlayer = new PlayerState { PkPoints = 300 };
        Assert.True(redPlayer.IsRedName, "PK 300 必须判定为红名大盗");

        // 背包放入 10 件物品
        for (int i = 0; i < 10; i++)
        {
            redPlayer.BagItems.Add(new EquipItem { Uid = 1000 + i, Name = $"战神药水_{i}" });
        }
        Assert.Equal(10, redPlayer.BagItems.Count);

        // 无论随机数是什么，红名死亡背包必须 100% 全部爆落 (IDA 0x4BB44C)
        var rng = new OfficialClientRulesEngine.DelphiRandom(12345);
        var drops = MonsterDropEngine.ExecuteDeathDrop(redPlayer, rng);

        // 验证背包被彻底爆空
        Assert.Empty(redPlayer.BagItems);
        Assert.Equal(10, drops.Count);
        for (int i = 0; i < 10; i++)
        {
            int expectedUid = 1000 + i;
            Assert.Contains(drops, d => !d.FromEquipment && d.ItemUid == expectedUid);
        }
    }

    [Fact]
    public void TestOfficialM2_WhiteName_Equipment_1_In_30_DropRate()
    {
        // 2. 白名平民 (PK == 0)
        int dropsCount = 0;
        int trials = 3000;

        // 统计 3000 次死亡穿戴掉落分布
        for (int t = 0; t < trials; t++)
        {
            var whitePlayer = new PlayerState { PkPoints = 0 };
            whitePlayer.Equipment[1] = new EquipItem { Uid = 888, Name = "裁决之杖" }; // 武器槽位 1
            var rng = new OfficialClientRulesEngine.DelphiRandom((uint)(t + 1));

            var drops = MonsterDropEngine.ExecuteDeathDrop(whitePlayer, rng);
            if (drops.Exists(d => d.FromEquipment && d.ItemUid == 888))
            {
                dropsCount++;
            }
        }

        // 理论掉落率为 1/30 (约 3.33%)，3000 次试验期望约 100 次
        // 验证真实掉落分布处于统计置信区间内 (70 ~ 130)
        Assert.True(dropsCount >= 60 && dropsCount <= 140, $"白名裁决之杖爆出次数实际为 {dropsCount}，符合 1/30 理论分布");
    }

    [Fact]
    public async Task TestE2E_CombatKill_Spawns_GroundItem_Broadcast()
    {
        int testPort = 17008;
        var mapManager = new MapManager();
        var dispatcher = new PacketDispatcher(mapManager);
        var gateway = new TcpGateway(testPort);
        gateway.PacketReceivedHandler = (session, pkt) => dispatcher.Dispatch(session, pkt);
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            // 杀手 A (200, 200)
            using var clientA = new System.Net.Sockets.TcpClient();
            await clientA.ConnectAsync("127.0.0.1", testPort);
            var streamA = clientA.GetStream();
            await streamA.WriteAsync(Encoding.UTF8.GetBytes("#103/狂战士A/200/200!"));
            await streamA.FlushAsync();

            // 濒死红名怪/玩家 B (201, 200)，仅剩 5 点生命值，背包有一把屠龙刀
            using var clientB = new System.Net.Sockets.TcpClient();
            await clientB.ConnectAsync("127.0.0.1", testPort);
            var streamB = clientB.GetStream();
            await streamB.WriteAsync(Encoding.UTF8.GetBytes("#103/红名老妖B/201/200!"));
            await streamB.FlushAsync();

            await Task.Delay(200);

            // 获取服务器上的会话 A 与 B，设置其为红名、低血量且背包带屠龙刀
            var map = mapManager.GetMap(0);
            var buffer = new System.Collections.Generic.List<ClientSession>();
            map?.Aoi.GetEntitiesInNinePatch(200, 200, buffer);
            var sessionA = buffer.Find(s => s.CharacterName == "狂战士A");
            var sessionB = buffer.Find(s => s.CharacterName == "红名老妖B");
            Assert.NotNull(sessionA);
            Assert.NotNull(sessionB);

            sessionA.State!.Hit = 100; // 绝对命中
            sessionB.State!.Speed = 0;
            sessionB.State.PkPoints = 500; // 红名
            sessionB.State.Hp = 5; // 濒死
            sessionB.State.BagItems.Add(new EquipItem { Uid = 9999, Name = "屠龙" });

            byte[] flushBuf = new byte[4096];
            while (streamA.DataAvailable) _ = await streamA.ReadAsync(flushBuf, 0, flushBuf.Length);
            while (streamB.DataAvailable) _ = await streamB.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 狂战士 A 挥刀攻击 (201, 200)，造成 15~30 伤害必致死
            byte[] killHitPkt = Encoding.UTF8.GetBytes("#3014/201/200/1!");
            await streamA.WriteAsync(killHitPkt);
            await streamA.FlushAsync();

            await Task.Delay(200);

            // 杀手 A 必须收到红名老妖 B 死亡倒地广播 SM_DEATH_ACTION_DELPHI (28)
            // 以及屠龙刀爆落在地面的广播 SM_GROUNDITEM (314)
            Assert.True(streamA.DataAvailable, "击杀目标后杀手同屏必须收到死亡倒地与爆装广播");
            byte[] recvBuf = new byte[4096];
            int recvLen = await streamA.ReadAsync(recvBuf, 0, recvBuf.Length);
            string recvStr = Encoding.UTF8.GetString(recvBuf, 0, recvLen);

            Assert.Contains($"#{ProtocolConsts.SM_DEATH_ACTION_DELPHI}/", recvStr);
            Assert.Contains($"#{ProtocolConsts.SM_GROUNDITEM}/", recvStr);
            Assert.Contains("屠龙", recvStr); // 爆落出的地面物品正是屠龙刀！
        }
        finally
        {
            tickEngine.Stop();
            gateway.Stop();
        }
    }
}
