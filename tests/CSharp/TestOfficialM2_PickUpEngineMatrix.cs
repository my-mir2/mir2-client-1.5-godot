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

public class TestOfficialM2_PickUpEngineMatrix
{
    [Fact]
    public void TestOfficialM2_PickUp_Protection_80Seconds_Rule()
    {
        var map = new GameMap(0, "比奇省", 400, 400);
        long now = 1_000_000;

        // 掉落一件裁决之杖，归属会话 101，掉落时间为 now
        map.AddGroundItem(new GroundItemEntity
        {
            Uid = 8888,
            Name = "裁决之杖",
            X = 150,
            Y = 150,
            OwnerSessionId = 101,
            DropTickMs = now,
            Weight = 50
        });

        // 1. 他人 (会话 102) 在 80 秒保护期内 (例如经过 50 秒 = 50,000ms) 尝试拾取
        var (s1, i1, err1) = map.TryPickGroundItem(150, 150, sessionId: 102, nowMs: now + 50_000);
        Assert.False(s1, "他人处于 80 秒保护期内无法拾取");
        Assert.Null(i1);
        Assert.Contains("保护", err1);

        // 2. 本人 (会话 101) 在保护期内可自由拾取
        // 注意由于上面拾取失败，物品依然在地面
        var (s2, i2, err2) = map.TryPickGroundItem(150, 150, sessionId: 101, nowMs: now + 50_000);
        Assert.True(s2, "本人在保护期内可正常拾取");
        Assert.NotNull(i2);
        Assert.Equal(8888, i2.Uid);
        Assert.Equal("裁决之杖", i2.Name);

        // 3. 拾取后地面已空
        var (s3, i3, _) = map.TryPickGroundItem(150, 150, sessionId: 101, nowMs: now + 50_000);
        Assert.False(s3, "拾取后地面物品必须已被移走");
        Assert.Null(i3);
    }

    [Fact]
    public void TestOfficialM2_PickUp_Expired_Protection_AnyoneCanPick()
    {
        var map = new GameMap(0, "比奇省", 400, 400);
        long now = 1_000_000;

        // 掉落一件骨玉权杖，归属会话 101
        map.AddGroundItem(new GroundItemEntity
        {
            Uid = 7777,
            Name = "骨玉权杖",
            X = 150,
            Y = 150,
            OwnerSessionId = 101,
            DropTickMs = now,
            Weight = 20
        });

        // 超过 80,000ms (例如 80,001ms 后)，保护期彻底失效，路人 102 可拾取 (IDA 0x4C5CB0 @ 0x13880)
        var (success, item, _) = map.TryPickGroundItem(150, 150, sessionId: 102, nowMs: now + 80_001);
        Assert.True(success, "保护期超时后任何玩家均可自由拾取");
        Assert.NotNull(item);
        Assert.Equal("骨玉权杖", item.Name);
    }

    [Fact]
    public async Task TestE2E_PickUp_GroundItem_AddsToBag_And_Broadcasts_DelGroundItem()
    {
        int testPort = 17009;
        var mapManager = new MapManager();
        var dispatcher = new PacketDispatcher(mapManager);
        var gateway = new TcpGateway(testPort);
        gateway.PacketReceivedHandler = (session, pkt) => dispatcher.Dispatch(session, pkt);
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            // 客户端 A: 拾取者 (200, 200)
            using var clientA = new System.Net.Sockets.TcpClient();
            await clientA.ConnectAsync("127.0.0.1", testPort);
            var streamA = clientA.GetStream();
            await streamA.WriteAsync(Encoding.UTF8.GetBytes("#103/拾取者A/200/200!"));
            await streamA.FlushAsync();

            // 客户端 B: 同屏旁观者 (201, 200)
            using var clientB = new System.Net.Sockets.TcpClient();
            await clientB.ConnectAsync("127.0.0.1", testPort);
            var streamB = clientB.GetStream();
            await streamB.WriteAsync(Encoding.UTF8.GetBytes("#103/旁观者B/201/200!"));
            await streamB.FlushAsync();

            await Task.Delay(200);

            // 获取服务器上的会话 A
            var map = mapManager.GetMap(0);
            Assert.NotNull(map);
            var buffer = new System.Collections.Generic.List<ClientSession>();
            map.Aoi.GetEntitiesInNinePatch(200, 200, buffer);
            var sessionA = buffer.Find(s => s.CharacterName == "拾取者A");
            Assert.NotNull(sessionA);

            // 在 (200, 200) 地面放置一件井中月 (归属 sessionA.SessionId)
            map.AddGroundItem(new GroundItemEntity
            {
                Uid = 6666,
                Name = "井中月",
                X = 200,
                Y = 200,
                OwnerSessionId = sessionA.SessionId,
                DropTickMs = Environment.TickCount64,
                Weight = 40
            });

            byte[] flushBuf = new byte[4096];
            while (streamA.DataAvailable) _ = await streamA.ReadAsync(flushBuf, 0, flushBuf.Length);
            while (streamB.DataAvailable) _ = await streamB.ReadAsync(flushBuf, 0, flushBuf.Length);

            // 客户端 A 发起拾取指令 CM_PICKUP (1001)
            byte[] pickupPkt = Encoding.UTF8.GetBytes("#1001/!");
            await streamA.WriteAsync(pickupPkt);
            await streamA.FlushAsync();

            await Task.Delay(200);

            // 1. 验证客户端 A 收到添加物品通知 SM_ADDITEM (200) 与地表物品消失广播 SM_DELGROUNDITEM (315)
            Assert.True(streamA.DataAvailable, "拾取者自身必须收到加物品与地表物品清除包");
            byte[] bufA = new byte[4096];
            int lenA = await streamA.ReadAsync(bufA, 0, bufA.Length);
            string respA = Encoding.UTF8.GetString(bufA, 0, lenA);

            Assert.Contains($"#{ProtocolConsts.SM_ADDITEM}/", respA);
            Assert.Contains("井中月", respA);
            Assert.Contains($"#{ProtocolConsts.SM_DELGROUNDITEM}/", respA);

            // 2. 验证会话 A 的背包中已成功加入井中月
            Assert.Single(sessionA.State!.BagItems);
            Assert.Equal("井中月", sessionA.State.BagItems[0].Name);
            Assert.Equal(40, sessionA.State.BagWeight);

            // 3. 验证同屏旁观者 B 也必须收到地表物品消失广播 SM_DELGROUNDITEM (315)
            Assert.True(streamB.DataAvailable, "同屏旁观者必须收到地表物品消失广播");
            byte[] bufB = new byte[4096];
            int lenB = await streamB.ReadAsync(bufB, 0, bufB.Length);
            string respB = Encoding.UTF8.GetString(bufB, 0, lenB);
            Assert.Contains($"#{ProtocolConsts.SM_DELGROUNDITEM}/", respB);
            Assert.Contains("井中月", respB);
        }
        finally
        {
            tickEngine.Stop();
            gateway.Stop();
        }
    }
}
