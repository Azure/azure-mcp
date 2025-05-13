// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Arguments.Postgres.Server;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Postgres.Server;

public sealed class GetConfigCommand(ILogger<GetConfigCommand> logger) : BaseServerCommand<GetConfigArguments>(logger)
{
    protected override string GetCommandName() => "config";
    protected override string GetCommandDescription() =>
        "Retrieve the configuration of a PostgreSQL server.";

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

            IPostgresService pgService = context.GetService<IPostgresService>() ?? throw new InvalidOperationException("PostgreSQL service is not available.");
            var config = await pgService.GetServerConfigAsync(args.Subscription!, args.ResourceGroup!, args.User!, args.Server!);
            context.Response.Results = config?.Length > 0 ?
                ResponseResult.Create(
                    new GetConfigCommandResult(config),
                    PostgresJsonContext.Default.GetConfigCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred retrieving server configuration.");
            HandleException(context.Response, ex);
        }
        return context.Response;
    }
    internal record GetConfigCommandResult(string Configuration);
}
