// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Arguments.Kusto;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Kusto;

public sealed class TableSchemaCommand(ILogger<TableSchemaCommand> logger) : BaseTableCommand<TableSchemaArguments>
{
    private readonly ILogger<TableSchemaCommand> _logger = logger;

    protected override string GetCommandName() => "schema";

    protected override string GetCommandDescription() =>
        "Get the schema of a specific table in an Kusto database.";

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);
        try
        {
            if (!await ProcessArguments(context, args))
                return context.Response;

            var kusto = context.GetService<IKustoService>();
            string tableSchema;

            if (UseClusterUri(args))
            {
                tableSchema = await kusto.GetTableSchema(
                    args.ClusterUri!,
                    args.Database!,
                    args.Table!,
                    args.Tenant,
                    args.AuthMethod,
                    args.RetryPolicy);
            }
            else
            {
                tableSchema = await kusto.GetTableSchema(
                    args.Subscription!,
                    args.ClusterName!,
                    args.Database!,
                    args.Table!,
                    args.Tenant,
                    args.AuthMethod,
                    args.RetryPolicy);
            }

            context.Response.Results = ResponseResult.Create(new TableSchemaCommandResult(tableSchema), KustoJsonContext.Default.TableSchemaCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred getting table schema. Cluster: {Cluster}, Table: {Table}.", args.ClusterUri ?? args.ClusterName, args.Table);
            HandleException(context.Response, ex);
        }
        return context.Response;
    }

    internal record TableSchemaCommandResult(string Schema);
}
