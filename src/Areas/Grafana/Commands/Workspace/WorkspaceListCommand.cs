// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Grafana.Models.Workspace;
using AzureMcp.Areas.Grafana.Options.Workspace;
using AzureMcp.Areas.Grafana.Services;
using AzureMcp.Commands.Grafana;
using AzureMcp.Commands.Subscription;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Grafana.Commands.Workspace;

/// <summary>
/// Lists Azure Managed Grafana workspaces in the specified subscription.
/// </summary>
public sealed class WorkspaceListCommand(ILogger<WorkspaceListCommand> logger) : SubscriptionCommand<WorkspaceListOptions>()
{
    private const string CommandTitle = "List Grafana Workspaces";
    private readonly ILogger<WorkspaceListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all Grafana workspace resources in a specified subscription. Returns an array of Grafana workspace details.
        Use this command to explore which Grafana workspace resources are available in your subscription.
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

            context.Activity?.WithSubscriptionTag(options);

            var grafanaService = context.GetService<IGrafanaService>() ?? throw new InvalidOperationException("Grafana service is not available.");
            var workspaces = await grafanaService.ListWorkspacesAsync(
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = workspaces.Any() ?
                ResponseResult.Create(
                    new WorkspaceListCommandResult(workspaces),
                    GrafanaJsonContext.Default.WorkspaceListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list Grafana workspaces");

            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record WorkspaceListCommandResult(IEnumerable<Models.Workspace.Workspace> Workspaces);
}
