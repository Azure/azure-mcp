// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Commands.Subscription;
using AzureMcp.Options.Kusto;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Kusto;

public sealed class ClusterListCommand(ILogger<ClusterListCommand> logger) : SubscriptionCommand<ClusterListOptions>()
{
    private const string CommandTitle = "List Kusto Clusters";
    private readonly ILogger<ClusterListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        List all Kusto clusters in a subscription. This command retrieves all clusters
        available in the specified subscription. Requires `cluster-name` and `subscription`.
        Result is a list of cluster names as a JSON array.
        """;

    public override string Title => CommandTitle;

    [McpServerTool(Destructive = true, ReadOnly = false, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            var kusto = context.GetService<IKustoService>();
            var clusterNames = await kusto.ListClusters(
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = CreateListResult(
                clusterNames,
                clusters => new ClusterListCommandResult(clusters),
                KustoJsonContext.Default.ClusterListCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing Kusto clusters. Subscription: {Subscription}.", options.Subscription);
            HandleException(context.Response, ex);
        }
        return context.Response;
    }

    internal record ClusterListCommandResult(List<string> Clusters);
}
