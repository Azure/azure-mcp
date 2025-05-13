// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Arguments.Postgres.Table;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;


namespace AzureMcp.Commands.Postgres.Table;

public sealed class TableListCommand(ILogger<TableListCommand> logger) : BaseDatabaseCommand<TableListArguments>(logger)
{
    protected override string GetCommandName() => "list";
    protected override string GetCommandDescription() => "Lists all tables in the PostgreSQL database.";

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
            List<string> tables = await pgService.ListTablesAsync(args.Subscription!, args.ResourceGroup!, args.User!, args.Server!, args.Database!);
            context.Response.Results = tables?.Count > 0 ?
                ResponseResult.Create(
                    new TableListCommandResult(tables),
                    PostgresJsonContext.Default.TableListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing tables.");
            HandleException(context.Response, ex);
        }
        return context.Response;
    }

    internal record TableListCommandResult(List<string> Tables);
}
