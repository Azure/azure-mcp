// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Kusto;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Kusto;

public sealed class ClusterGetCommand : BaseClusterCommand<ClusterGetArguments>
{
    private const string _commandTitle = "Get Kusto Cluster Details";
    private readonly ILogger<ClusterGetCommand> _logger;

    public ClusterGetCommand(ILogger<ClusterGetCommand> logger)
    {
        _logger = logger;
    }

    public override string Name => "get";

    public override string Description =>
        """
        Get details for a specific Kusto cluster. Requires `subscription` and `cluster-name`.
        The response includes the `clusterUri` property for use in subsequent commands.
        """;

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true)]
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
            var cluster = await kusto.GetCluster(
                args.Subscription!,
                args.ClusterName!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = cluster is null ?
            null : ResponseResult.Create(new ClusterGetCommandResult(cluster), KustoJsonContext.Default.ClusterGetCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred getting Kusto cluster details. Cluster: {Cluster}.", args.ClusterName);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record ClusterGetCommandResult(KustoClusterResourceProxy Cluster);
}
