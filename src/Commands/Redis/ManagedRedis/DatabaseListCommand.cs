// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Redis.ManagedRedis;
using AzureMcp.Options.Redis.ManagedRedis;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Redis.ManagedRedis;

/// <summary>
/// Lists the databases in the specified Azure Managed Redis or Azure Redis Enterprise cluster.
/// </summary>
public sealed class DatabaseListCommand(ILogger<DatabaseListCommand> logger) : BaseClusterCommand<DatabaseListOptions>()
{
    private const string _commandTitle = "List Redis Cluster Databases";
    private readonly ILogger<DatabaseListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List the databases in the specified Redis Cluster resource. Returns an array of Redis database details.
        Use this command to explore which databases are available in your Redis Cluster.
        """;
    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            var redisService = context.GetService<IRedisService>() ?? throw new InvalidOperationException("Redis service is not available.");
            var databases = await redisService.ListDatabasesAsync(
                args.Cluster!,
                args.ResourceGroup!,
                args.Subscription!,
                args.Tenant,
                args.AuthMethod,
                args.RetryPolicy);

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
