// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Commands.Subscription;
using AzureMcp.Options.Redis.CacheForRedis;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Redis.CacheForRedis;

/// <summary>
/// Lists Azure Cache for Redis resources (Basic, Standard, and Premium tier caches) in the specified subscription.
/// </summary>
public sealed class CacheListCommand(ILogger<CacheListCommand> logger) : SubscriptionCommand<CacheListOptions>()
{
    private const string _commandTitle = "List Redis Caches";
    private readonly ILogger<CacheListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all Redis Cache resources in a specified subscription. Returns an array of Redis Cache details.
        Use this command to explore which Redis Cache resources are available in your subscription.
        """;

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            var redisService = context.GetService<IRedisService>() ?? throw new InvalidOperationException("Redis service is not available.");
            var caches = await redisService.ListCachesAsync(
                options.Subscription!,
                options.Tenant,
                options.AuthMethod,
                options.RetryPolicy);

            context.Response.Results = caches.Any() ?
                ResponseResult.Create(
                    new CacheListCommandResult(caches),
                    RedisJsonContext.Default.CacheListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list Redis Caches");

            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record CacheListCommandResult(IEnumerable<Models.Redis.CacheForRedis.Cache> Caches);
}
