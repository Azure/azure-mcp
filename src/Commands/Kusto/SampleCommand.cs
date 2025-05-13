// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Arguments.Kusto;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Kusto;

public sealed class SampleCommand(ILogger<SampleCommand> logger) : BaseTableCommand<SampleArguments>
{
    private readonly ILogger<SampleCommand> _logger = logger;

    private readonly Option<int> _limitOption = ArgumentDefinitions.Kusto.Limit.ToOption();

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_limitOption);
    }

    protected override SampleArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Limit = parseResult.GetValueForOption(_limitOption);
        return args;
    }

    protected override string GetCommandName() => "sample";

    protected override string GetCommandDescription() =>
        """
        Return a sample of rows from the specified table in an Kusto table.
        Requires `cluster-uri` (or `cluster-name`), `database-name`, and `table-name`. 
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

            var kusto = context.GetService<IKustoService>();
            List<JsonElement> results;
            var query = $"{args.Table} | sample {args.Limit}";

            if (UseClusterUri(args))
            {
                results = await kusto.QueryItems(
                    args.ClusterUri!,
                    args.Database!,
                    query,
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
                    query,
                    args.Tenant,
                    args.AuthMethod,
                    args.RetryPolicy);
            }

            context.Response.Results = results?.Count > 0 ?
                ResponseResult.Create(new SampleCommandResult(results), KustoJsonContext.Default.SampleCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred sampling table. Cluster: {Cluster}, Database: {Database}, Table: {Table}.", args.ClusterUri ?? args.ClusterName, args.Database, args.Table);
            HandleException(context.Response, ex);
        }
        return context.Response;
    }

    internal record SampleCommandResult(List<JsonElement> Results);
}
