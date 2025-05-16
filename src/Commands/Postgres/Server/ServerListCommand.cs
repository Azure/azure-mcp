// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using Azure.ResourceManager.Resources.Models;
using AzureMcp.Arguments.Postgres.Server;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Postgres.Server;

public sealed class ServerListCommand(ILogger<ServerListCommand> logger) : BasePostgresCommand<ServerListArguments>(logger)
{
    private const string _commandTitle = "List PostgreSQL Servers";

    public override string Name => "list";

    public override string Description =>
        "Lists all PostgreSQL servers in the specified subscription.";

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

            IPostgresService pgService = context.GetService<IPostgresService>() ?? throw new InvalidOperationException("PostgreSQL service is not available.");
            List<string> servers = await pgService.ListServersAsync(args.Subscription!, args.ResourceGroup!, args.User!);
            context.Response.Results = servers?.Count > 0 ?
                ResponseResult.Create(
                    new ServerListCommandResult(servers),
                    PostgresJsonContext.Default.ServerListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred while listing servers");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record ServerListCommandResult(List<string> Servers);
}
