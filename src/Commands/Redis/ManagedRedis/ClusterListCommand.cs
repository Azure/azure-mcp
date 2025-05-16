// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Arguments.Redis.ManagedRedis;
using AzureMcp.Models.Command;
using AzureMcp.Models.Redis.ManagedRedis;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Redis.ManagedRedis;

/// <summary>
/// Lists Azure Managed Redis cluster resources (`Balanced`, `MemoryOptimized`, `ComputeOptimized`, and `FlashOptimized` tiers) and Azure Redis Enterprise cluster resources (`Enterprise` and `EnterpriseFlash` tiers) in the specified subscription.
/// </summary>
public sealed class ClusterListCommand(ILogger<ClusterListCommand> logger) : SubscriptionCommand<ClusterListArguments>()
{
    private const string _commandTitle = "List Redis Clusters";
    private readonly ILogger<ClusterListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all Redis Cluster resources in a specified subscription. Returns an array of Redis Cluster details.
        Use this command to explore which Redis Cluster resources are available in your subscription.
        """;
    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            var args = BindArguments(parseResult);

            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var redisService = context.GetService<IRedisService>() ?? throw new InvalidOperationException("Redis service is not available.");
            var clusters = await redisService.ListClustersAsync(
                args.Subscription!,
                args.Tenant,
                args.AuthMethod,
                args.RetryPolicy);

            context.Response.Results = clusters.Any() ?
                ResponseResult.Create(
                    new ClusterListCommandResult(clusters),
                    RedisJsonContext.Default.ClusterListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list Redis Clusters");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record ClusterListCommandResult(IEnumerable<Cluster> Clusters);
}
