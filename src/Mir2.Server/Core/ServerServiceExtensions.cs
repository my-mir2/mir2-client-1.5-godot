using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mir2.Server.Game;
using Mir2.Server.Maps;
using Mir2.Server.Network;
using Mir2.Shared.Plugins;

namespace Mir2.Server.Core;

/// <summary>
/// 服务端依赖注入扩展方法 (Dependency Injection Extensions)
/// 统一配置并装配 M2 服务端领域服务、网络网关、游戏规则与后台宿主服务
/// </summary>
public static class ServerServiceExtensions
{
    public static IServiceCollection AddMir2ServerEngine(this IServiceCollection services, ServerConfig config)
    {
        // 1. 基础配置
        services.AddSingleton(config);

        // 2. 地图与实体空间分区
        services.AddSingleton<MapManager>();

        // 3. 游戏领域核心引擎 (战法道规则、怪物刷新、商业 NPC、交易与行会)
        services.AddSingleton(sp => new MonsterRegenEngine(sp.GetRequiredService<MapManager>()));
        services.AddSingleton<NpcMerchantEngine>();
        services.AddSingleton<TradeEngine>();
        services.AddSingleton<GroupEngine>();
        services.AddSingleton<GuildEngine>();
        services.AddSingleton<SabukSiegeEngine>();
        services.AddSingleton<EconomyAntiDupeEngine>();
        services.AddSingleton<ModernNpcPluginEngine>();

        // 4. 封包分发中枢
        services.AddSingleton(sp => new PacketDispatcher(
            sp.GetRequiredService<MapManager>(),
            sp.GetRequiredService<MonsterRegenEngine>(),
            sp.GetRequiredService<NpcMerchantEngine>(),
            sp.GetRequiredService<TradeEngine>(),
            sp.GetRequiredService<GroupEngine>(),
            sp.GetRequiredService<GuildEngine>(),
            sp.GetRequiredService<SabukSiegeEngine>(),
            sp.GetRequiredService<EconomyAntiDupeEngine>()
        ));

        // 5. 网络层与物理心跳
        services.AddSingleton(sp =>
        {
            var cfg = sp.GetRequiredService<ServerConfig>();
            return new TcpGateway(cfg.GatewayPort)
            {
                MaxConnections = cfg.MaxConnections,
                MaxPacketsPerSecondPerSession = cfg.MaxPacketsPerSecond,
                MaxIncomingBufferBytes = cfg.MaxBufferSize
            };
        });

        services.AddSingleton(sp => new GameWorldTick(sp.GetRequiredService<TcpGateway>()));

        // 6. 注册标准 IHostedService 宿主生命周期
        services.AddHostedService<Mir2ServerHostedService>();

        return services;
    }
}
