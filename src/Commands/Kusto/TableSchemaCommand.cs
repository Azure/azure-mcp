// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Kusto;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Kusto;

public sealed class TableSchemaCommand(ILogger<TableSchemaCommand> logger) : BaseTableCommand<TableSchemaArguments>
{
    private const string _commandTitle = "Get Kusto Table Schema";
    private readonly ILogger<TableSchemaCommand> _logger = logger;

    public override string Name => "schema";

    public override string Description =>
        "Get the schema of a specific table in an Kusto database.";

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindOptions(parseResult);

        try
        {
            var validationResult = Validate(parseResult.CommandResult);

            if (!validationResult.IsValid)
            {
                context.Response.Status = 400;
                context.Response.Message = validationResult.ErrorMessage!;
                return context.Response;
            }

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
