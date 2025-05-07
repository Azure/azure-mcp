// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Arguments.Kusto;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Kusto;

public sealed class TableListCommand(ILogger<TableListCommand> logger) : BaseDatabaseCommand<TableListArguments>
{
    private readonly ILogger<TableListCommand> _logger = logger;

    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
        "List all tables in a specific Kusto database. Returns table names as a JSON array.";

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);
        try
        {
            if (!await ProcessArguments(context, args))
                return context.Response;

            var kusto = context.GetService<IKustoService>();
            List<string> tableNames = [];

            if (UseClusterUri(args))
            {
                tableNames = await kusto.ListTables(
                    args.ClusterUri!,
                    args.Database!,
                    args.Tenant,
                    args.AuthMethod,
                    args.RetryPolicy);
            }
            else
            {
                tableNames = await kusto.ListTables(
                    args.Subscription!,
                    args.ClusterName!,
                    args.Database!,
                    args.Tenant,
                    args.AuthMethod,
                    args.RetryPolicy);
            }

            context.Response.Results = tableNames?.Count > 0 ?
                ResponseResult.Create(new TableListCommandResult(tableNames), KustoJsonContext.Default.TableListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing tables. Cluster: {Cluster}, Database: {Database}.", args.ClusterUri ?? args.ClusterName, args.Database);
            HandleException(context.Response, ex);
        }
        return context.Response;
    }

    public record TableListCommandResult(List<string> Tables);
}
