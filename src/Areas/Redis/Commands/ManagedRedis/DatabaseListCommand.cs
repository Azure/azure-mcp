// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Redis.Models.ManagedRedis;
using AzureMcp.Areas.Redis.Options.ManagedRedis;
using AzureMcp.Areas.Redis.Services;
using AzureMcp.Commands.Redis;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Redis.Commands.ManagedRedis;

/// <summary>
/// Lists the databases in the specified Azure Managed Redis or Azure Redis Enterprise cluster.
/// </summary>
public sealed class DatabaseListCommand(ILogger<DatabaseListCommand> logger) : BaseClusterCommand<DatabaseListOptions>()
{
    private const string CommandTitle = "List Redis Cluster Databases";
    private readonly ILogger<DatabaseListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List the databases in the specified Redis Cluster resource. Returns an array of Redis database details.
        Use this command to explore which databases are available in your Redis Cluster.
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

            var redisService = context.GetService<IRedisService>() ?? throw new InvalidOperationException("Redis service is not available.");
            var databases = await redisService.ListDatabasesAsync(
                options.Cluster!,
                options.ResourceGroup!,
                options.Subscription!,
                options.Tenant,
                options.AuthMethod,
                options.RetryPolicy);

            context.Response.Results = databases.Any() ?
                ResponseResult.Create(
                    new DatabaseListCommandResult(databases),
                    RedisJsonContext.Default.DatabaseListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list Redis Databases");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record DatabaseListCommandResult(IEnumerable<Database> Databases);
}
