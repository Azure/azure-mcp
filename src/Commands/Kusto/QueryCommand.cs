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

public sealed class QueryCommand : BaseQueryCommand<QueryArguments>
{
    private readonly ILogger<QueryCommand> _logger;

    public QueryCommand(ILogger<QueryCommand> logger) : base()
    {
        _logger = logger;
    }

    protected override string GetCommandName() => "query";

    protected override string GetCommandDescription() =>
        """
        Execute a KQL against items in a Kusto cluster. Requires cluster-uri, database, and query.
        Requires `cluster-uri` (or `cluster-name`), `database-name`, and `query-text`. 
        Results are returned as a JSON array of documents, for example: `[{'Column1': val1, 'Column2': val2}, ...]`.
        """;

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);
        try
        {
            if (!await ProcessArguments(context, args))
                return context.Response;

            List<JsonElement> results = [];
            var kusto = context.GetService<IKustoService>();

            if (UseClusterUri(args))
            {
                results = await kusto.QueryItems(
                    args.ClusterUri!,
                    args.Database!,
                    args.Query!,
                    args.Tenant,
                    args.AuthMethod,
                    args.RetryPolicy);
            }
            else
            {
                results = await kusto.QueryItems(
                    args.Subscription!,
                    args.ClusterName!,
                    args.Database!,
                    args.Query!,
                    args.Tenant,
                    args.AuthMethod,
                    args.RetryPolicy);
            }

            context.Response.Results = results?.Count > 0 ?
                ResponseResult.Create(new QueryCommandResult(results), KustoJsonContext.Default.QueryCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred querying Kusto. Cluster: {Cluster}, Database: {Database},"
            + " Query: {Query}", args.ClusterUri ?? args.ClusterName, args.Database, args.Query);
            HandleException(context.Response, ex);
        }
        return context.Response;
    }

    internal record QueryCommandResult(List<JsonElement> Results);
}
