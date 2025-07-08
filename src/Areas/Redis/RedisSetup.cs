// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Redis.Commands.CacheForRedis;
using AzureMcp.Areas.Redis.Commands.ManagedRedis;
using AzureMcp.Areas.Redis.Services;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Redis;

public class RedisSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IRedisService, RedisService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var redis = new CommandGroup("redis", "Redis Cache operations - Commands for managing and accessing Azure Redis Cache resources.");
        rootGroup.AddSubGroup(redis);

        // Azure Cache for Redis
        var cache = new CommandGroup("cache", "Redis Cache resource operations - Commands for listing and managing Redis Cache resources in your Azure subscription.");
        redis.AddSubGroup(cache);

        cache.AddCommand("list", new CacheListCommand(loggerFactory.CreateLogger<CacheListCommand>()));

        var accessPolicy = new CommandGroup("accesspolicy", "Redis Cluster database operations - Commands for listing and managing Redis Cluster databases in your Azure subscription.");
        cache.AddSubGroup(accessPolicy);

        accessPolicy.AddCommand("list", new AccessPolicyListCommand(loggerFactory.CreateLogger<AccessPolicyListCommand>()));

        // Azure Managed Redis
        var cluster = new CommandGroup("cluster", "Redis Cluster resource operations - Commands for listing and managing Redis Cluster resources in your Azure subscription.");
        redis.AddSubGroup(cluster);

        cluster.AddCommand("list", new ClusterListCommand(loggerFactory.CreateLogger<ClusterListCommand>()));

        var database = new CommandGroup("database", "Redis Cluster database operations - Commands for listing and managing Redis Cluster Databases in your Azure subscription.");
        cluster.AddSubGroup(database);

        database.AddCommand("list", new DatabaseListCommand(loggerFactory.CreateLogger<DatabaseListCommand>()));
    }
}
