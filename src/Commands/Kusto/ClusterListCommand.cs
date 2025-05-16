// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Arguments.Kusto;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Kusto;

public sealed class ClusterListCommand : SubscriptionCommand<ClusterListArguments>
{
    private const string _commandTitle = "List Kusto Clusters";
    private readonly ILogger<ClusterListCommand> _logger;

    public ClusterListCommand(ILogger<ClusterListCommand> logger) : base()
    {
        _logger = logger;
    }

    public override string Name => "list";

    public override string Description =>
        """
        List all Kusto clusters in a subscription. This command retrieves all clusters
        available in the specified subscription. Requires `cluster-name` and `subscription`.
        Result is a list of cluster names as a JSON array.
        """;

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = true, ReadOnly = false, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);
        try
        {
            if (!await ProcessArguments(context, args))
                return context.Response;

            var kusto = context.GetService<IKustoService>();
            var clusterNames = await kusto.ListClusters(
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = clusterNames?.Count > 0 ?
                ResponseResult.Create(new ClusterListCommandResult(clusterNames), KustoJsonContext.Default.ClusterListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing Kusto clusters. Subscription: {Subscription}.", args.Subscription);
            HandleException(context.Response, ex);
        }
        return context.Response;
    }

    internal record ClusterListCommandResult(List<string> Clusters);
}
