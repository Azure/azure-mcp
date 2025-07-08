// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Kusto.Options;
using AzureMcp.Areas.Kusto.Services;
using AzureMcp.Commands.Kusto;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Kusto.Commands;

public sealed class TableSchemaCommand(ILogger<TableSchemaCommand> logger) : BaseTableCommand<TableSchemaOptions>
{
    private const string CommandTitle = "Get Kusto Table Schema";
    private readonly ILogger<TableSchemaCommand> _logger = logger;

    public override string Name => "schema";

    public override string Description =>
        """
        Get the schema of a specific table in an Kusto database.
        Requires `cluster-uri` ( or `subscription` and `cluster-name`), `database-name` and `table`.
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

            context.Activity?.WithSubscriptionTag(options);

            var kusto = context.GetService<IKustoService>();
            string tableSchema;

            if (UseClusterUri(options))
            {
                tableSchema = await kusto.GetTableSchema(
                    options.ClusterUri!,
                    options.Database!,
                    options.Table!,
                    options.Tenant,
                    options.AuthMethod,
                    options.RetryPolicy);
            }
            else
            {
                tableSchema = await kusto.GetTableSchema(
                    options.Subscription!,
                    options.ClusterName!,
                    options.Database!,
                    options.Table!,
                    options.Tenant,
                    options.AuthMethod,
                    options.RetryPolicy);
            }

            context.Response.Results = ResponseResult.Create(new TableSchemaCommandResult(tableSchema), KustoJsonContext.Default.TableSchemaCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred getting table schema. Cluster: {Cluster}, Table: {Table}.", options.ClusterUri ?? options.ClusterName, options.Table);
            HandleException(context, ex);
        }
        return context.Response;
    }

    internal record TableSchemaCommandResult(string Schema);
}
