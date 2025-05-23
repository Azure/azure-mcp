// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Kusto;
using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Kusto;

public sealed class SampleCommand(ILogger<SampleCommand> logger) : BaseTableCommand<SampleArguments>
{
    private const string _commandTitle = "Sample Kusto Table Data";
    private readonly ILogger<SampleCommand> _logger = logger;

    private readonly Option<int> _limitOption = ArgumentDefinitions.Kusto.Limit;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_limitOption);
    }

    protected override SampleArguments BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Limit = parseResult.GetValueForOption(_limitOption);
        return args;
    }

    public override string Name => "sample";

    public override string Description =>
        """
        Return a sample of rows from the specified table in an Kusto table.
        Requires `cluster-uri` (or `cluster-name`), `database-name`, and `table-name`. 
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
