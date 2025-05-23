// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Kusto;
using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Kusto;

public sealed class QueryCommand : BaseDatabaseCommand<QueryArguments>
{
    private const string _commandTitle = "Query Kusto Database";
    private readonly ILogger<QueryCommand> _logger;
    private readonly Option<string> _queryOption = ArgumentDefinitions.Kusto.Query;

    public QueryCommand(ILogger<QueryCommand> logger) : base()
    {
        _logger = logger;
    }

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_queryOption);
    }

    protected override QueryArguments BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Query = parseResult.GetValueForOption(_queryOption);
        return args;
    }

    public override string Name => "query";

    public override string Description =>
        """
        Execute a KQL against items in a Kusto cluster.
        Requires `cluster-uri` (or `cluster-name` and `subscription`), `database-name`, and `query`. 
        Results are returned as a JSON array of documents, for example: `[{'Column1': val1, 'Column2': val2}, ...]`.
        """;

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

    internal record QueryCommandResult(List<JsonElement> Items);
}
