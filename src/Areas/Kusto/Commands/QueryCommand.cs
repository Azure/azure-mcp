// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Option;
using AzureMcp.Options.Kusto;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Kusto;

public sealed class QueryCommand(ILogger<QueryCommand> logger) : BaseDatabaseCommand<QueryOptions>()
{
    private const string CommandTitle = "Query Kusto Database";
    private readonly ILogger<QueryCommand> _logger = logger;
    private readonly Option<string> _queryOption = OptionDefinitions.Kusto.Query;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_queryOption);
    }

    protected override QueryOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Query = parseResult.GetValueForOption(_queryOption);
        return options;
    }

    public override string Name => "query";

    public override string Description =>
        """
        Execute a KQL against items in a Kusto cluster.
        Requires `cluster-uri` (or `cluster-name` and `subscription`), `database-name`, and `query`. 
        Results are returned as a JSON array of documents, for example: `[{'Column1': val1, 'Column2': val2}, ...]`.
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

            List<JsonElement> results = [];
            var kusto = context.GetService<IKustoService>();

            if (UseClusterUri(options))
            {
                results = await kusto.QueryItems(
                    options.ClusterUri!,
                    options.Database!,
                    options.Query!,
                    options.Tenant,
                    options.AuthMethod,
                    options.RetryPolicy);
            }
            else
            {
                results = await kusto.QueryItems(
                    options.Subscription!,
                    options.ClusterName!,
                    options.Database!,
                    options.Query!,
                    options.Tenant,
                    options.AuthMethod,
                    options.RetryPolicy);
            }

            context.Response.Results = results?.Count > 0 ?
                ResponseResult.Create(new QueryCommandResult(results), KustoJsonContext.Default.QueryCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred querying Kusto. Cluster: {Cluster}, Database: {Database},"
            + " Query: {Query}", options.ClusterUri ?? options.ClusterName, options.Database, options.Query);
            HandleException(context.Response, ex);
        }
        return context.Response;
    }

    internal record QueryCommandResult(List<JsonElement> Items);
}
