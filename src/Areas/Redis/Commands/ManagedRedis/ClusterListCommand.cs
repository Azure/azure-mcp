// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Redis.Models.ManagedRedis;
using AzureMcp.Areas.Redis.Options.ManagedRedis;
using AzureMcp.Areas.Redis.Services;
using AzureMcp.Commands.Redis;
using AzureMcp.Commands.Subscription;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Redis.Commands.ManagedRedis;

/// <summary>
/// Lists Azure Managed Redis cluster resources (`Balanced`, `MemoryOptimized`, `ComputeOptimized`, and `FlashOptimized` tiers) and Azure Redis Enterprise cluster resources (`Enterprise` and `EnterpriseFlash` tiers) in the specified subscription.
/// </summary>
public sealed class ClusterListCommand(ILogger<ClusterListCommand> logger) : SubscriptionCommand<ClusterListOptions>()
{
    private const string CommandTitle = "List Redis Clusters";
    private readonly ILogger<ClusterListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all Redis Cluster resources in a specified subscription. Returns an array of Redis Cluster details.
        Use this command to explore which Redis Cluster resources are available in your subscription.
        """;
    public override string Title => CommandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }


            context.Activity?.WithSubscriptionTag(options);

            var redisService = context.GetService<IRedisService>() ?? throw new InvalidOperationException("Redis service is not available.");
            var clusters = await redisService.ListClustersAsync(
                options.Subscription!,
                options.Tenant,
                options.AuthMethod,
                options.RetryPolicy);

            context.Response.Results = clusters.Any() ?
                ResponseResult.Create(
                    new ClusterListCommandResult(clusters),
                    RedisJsonContext.Default.ClusterListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list Redis Clusters");
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record ClusterListCommandResult(IEnumerable<Cluster> Clusters);
}
