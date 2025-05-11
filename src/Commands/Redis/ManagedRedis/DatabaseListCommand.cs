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
/// Lists the databases in the specified Azure Managed Redis or Azure Redis Enterprise cluster.
/// </summary>
public sealed class DatabaseListCommand(ILogger<DatabaseListCommand> logger) : BaseClusterCommand<DatabaseListArguments>()
{
    private readonly ILogger<DatabaseListCommand> _logger = logger;

    protected override string GetCommandName() => "list";    protected override string GetCommandDescription() =>
        $"""
        List the databases in the specified Redis Cluster resource. Returns an array of Redis database details.
        Use this command to explore which databases are available in your Redis Cluster.
        """;

    [McpServerTool(Destructive = false, ReadOnly = true)]
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
