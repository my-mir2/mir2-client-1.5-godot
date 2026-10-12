using System;
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
    public async Task TestE2E_PickUp_GroundItem_AddsToBag_And_Broadcasts_DelGroundItem()
    {
        int testPort = GetRandomTestPort();
        var mapManager = new MapManager();
        var dispatcher = new PacketDispatcher(mapManager);
        var gateway = new TcpGateway(testPort);
        gateway.PacketReceivedHandler = (session, pkt) => dispatcher.Dispatch(session, pkt);
        gateway.BinaryPacketReceivedHandler = (session, header, payload) => dispatcher.DispatchBinary(session, header, payload);
        gateway.Start();

        var tickEngine = new GameWorldTick(gateway);
        tickEngine.Start();

        try
        {
            // 客户端 A: 拾取者 (200, 200)
            using var clientA = new System.Net.Sockets.TcpClient();
            await clientA.ConnectAsync("127.0.0.1", testPort);
            var streamA = clientA.GetStream();
            await SendPackAsync(streamA, 103, "拾取者A/200/200");

            // 客户端 B: 同屏旁观者 (201, 200)
            using var clientB = new System.Net.Sockets.TcpClient();
            await clientB.ConnectAsync("127.0.0.1", testPort);
            var streamB = clientB.GetStream();
            await SendPackAsync(streamB, 103, "旁观者B/201/200");

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
            await SendPackAsync(streamA, 1001);

            // 1. 验证客户端 A 收到添加物品通知 SM_ADDITEM (200) 与地表物品消失广播 SM_DELGROUNDITEM (315)
            string respA = await ReadUntilContainsPackAsync(streamA, $"#{ProtocolConsts.SM_DELGROUNDITEM}/", 3000);

            Assert.Contains($"#{ProtocolConsts.SM_ADDITEM}/", respA);
            Assert.Contains("井中月", respA);
            Assert.Contains($"#{ProtocolConsts.SM_DELGROUNDITEM}/", respA);

            // 2. 验证会话 A 的背包中已成功加入井中月
            Assert.Single(sessionA.State!.BagItems);
            Assert.Equal("井中月", sessionA.State.BagItems[0].Name);
            Assert.Equal(40, sessionA.State.BagWeight);

            // 3. 验证同屏旁观者 B 也必须收到地表物品消失广播 SM_DELGROUNDITEM (315)
            string respB = await ReadUntilContainsPackAsync(streamB, $"#{ProtocolConsts.SM_DELGROUNDITEM}/", 3000);
            Assert.Contains($"#{ProtocolConsts.SM_DELGROUNDITEM}/", respB);
            Assert.Contains("井中月", respB);
        }
        finally
        {
            tickEngine.Stop();
            gateway.Stop();
        }
    }

    [Fact]
    public void TestOfficialM2_PickUp_MultiItemsStacked_PicksUnprotectedItem()
    {
        var map = new GameMap(0, "盟重省", 400, 400);
        long now = 2_000_000;

        // 物品1: 受到他人 (102) 保护的裁决之杖
        map.AddGroundItem(new GroundItemEntity
        {
            Uid = 1001,
            Name = "裁决之杖",
            X = 200,
            Y = 200,
            OwnerSessionId = 102,
            DropTickMs = now,
            Weight = 50
        });

        // 物品2: 无保护的金币 (或者归属己方 101 的物品)
        map.AddGroundItem(new GroundItemEntity
        {
            Uid = 1002,
            Name = "金币",
            X = 200,
            Y = 200,
            OwnerSessionId = 0,
            DropTickMs = now,
            Count = 10000,
            IsGold = true
        });

        // 玩家 101 尝试拾取 (200, 200)
        // 应该自动跳过受保护的物品1，成功拾取物品2金币
        var (ok, item, err) = map.TryPickGroundItem(200, 200, sessionId: 101, nowMs: now + 10_000);
        Assert.True(ok);
        Assert.NotNull(item);
        Assert.Equal(1002, item.Uid);
        Assert.Equal("金币", item.Name);

        // 再次拾取，此时只剩下受他人保护的裁决之杖，应当被拦截
        var (ok2, item2, err2) = map.TryPickGroundItem(200, 200, sessionId: 101, nowMs: now + 10_000);
        Assert.False(ok2);
        Assert.Null(item2);
        Assert.Contains("保护", err2);
    }
}
