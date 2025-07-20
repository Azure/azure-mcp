// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Aks.Models;
using AzureMcp.Areas.Aks.Options.Cluster;
using AzureMcp.Areas.Aks.Services;
using AzureMcp.Commands.Aks;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;
using AzureMcp.Services.Caching;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Aks.Commands.Cluster;

public sealed class ClusterListCommand(ILogger<ClusterListCommand> logger) : BaseAksCommand<ClusterListOptions>()
{
    private const string CommandTitle = "List AKS Clusters";
    private readonly ILogger<ClusterListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        List all Azure Kubernetes Service (AKS) clusters in a subscription.
        Returns detailed cluster information including configuration, network settings, and status.
        """;

    public override string Title => CommandTitle;

    private IAksService GetAksService(CommandContext context)
    {
        try
        {
            var aksService = context.GetService<IAksService>();

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AksService hasn't been retrieved from context.");
        }
        var subscriptionService = context.GetService<ISubscriptionService>();
        var tenantService = context.GetService<ITenantService>();
        var cacheService = context.GetService<ICacheService>();
        return new AksService(subscriptionService, tenantService, cacheService);
    }

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

            var aksService = GetAksService(context);
            var clusters = await aksService.ListClusters(
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = clusters?.Count > 0 ?
                ResponseResult.Create(
                    new ClusterListCommandResult(clusters),
                    AksJsonContext.Default.ClusterListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error listing AKS clusters. Subscription: {Subscription}, Options: {@Options}",
                options.Subscription, options);
            HandleException(context, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx when reqEx.Status == 404 =>
            "Subscription not found. Verify the subscription ID and you have access.",
        Azure.RequestFailedException reqEx when reqEx.Status == 403 =>
            $"Authorization failed accessing AKS clusters. Details: {reqEx.Message}",
        Azure.RequestFailedException reqEx => reqEx.Message,
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx => reqEx.Status,
        _ => base.GetStatusCode(ex)
    };

    internal record ClusterListCommandResult(List<Models.Cluster> Clusters);
}
