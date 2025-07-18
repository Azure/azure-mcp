// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands.Subscription;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Kusto.Commands;
using AzureMcp.Kusto.Options;
using AzureMcp.Kusto.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Kusto.Commands;

public sealed class ClusterListCommand(ILogger<ClusterListCommand> logger) : SubscriptionCommand<ClusterListOptions>()
{
    private const string CommandTitle = "List Kusto Clusters";
    private readonly ILogger<ClusterListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        List all Kusto clusters in a subscription. This command retrieves all clusters
        available in the specified subscription. Requires `subscription`.
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

            context.Activity?.WithSubscriptionTag(options);

            var kusto = context.GetService<IKustoService>();
            var clusterNames = await kusto.ListClusters(
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = clusterNames?.Count > 0 ?
                ResponseResult.Create(new ClusterListCommandResult(clusterNames), KustoJsonContext.Default.ClusterListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing Kusto clusters. Subscription: {Subscription}.", options.Subscription);
            HandleException(context, ex);
        }
        return context.Response;
    }

    internal record ClusterListCommandResult(List<string> Clusters);
}
