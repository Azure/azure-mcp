// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.ContainerApps.Commands;
using AzureMcp.Areas.ContainerApps.Options.ContainerApp;
using AzureMcp.Areas.ContainerApps.Services;
using AzureMcp.Commands;
using AzureMcp.Models.Command;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.ContainerApps.Commands.ContainerApp;

public sealed class ContainerAppListCommand(ILogger<ContainerAppListCommand> logger) 
    : BaseContainerAppsCommand<ListOptions>
{
    private const string CommandTitle = "List Container Apps";
    private readonly ILogger<ContainerAppListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        List all Container Apps in a subscription or resource group. This command retrieves all Container Apps
        available in the specified subscription, optionally filtered by resource group. Results include app names,
        locations, status, and FQDNs returned as a JSON array.
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

            var containerAppsService = context.GetService<IContainerAppsService>();
            var containerApps = await containerAppsService.ListContainerAppsAsync(
                options.Subscription!,
                options.ResourceGroup,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = containerApps?.Count > 0
                ? ResponseResult.Create(
                    new ContainerAppListCommandResult(containerApps),
                    ContainerAppsJsonContext.Default.ContainerAppListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error listing container apps. Subscription: {Subscription}, ResourceGroup: {ResourceGroup}, Options: {@Options}",
                options.Subscription, options.ResourceGroup, options);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx when reqEx.Status == 404 =>
            "Resource not found. Verify the subscription and resource group exist and you have access.",
        Azure.RequestFailedException reqEx when reqEx.Status == 403 =>
            $"Authorization failed accessing Container Apps. Details: {reqEx.Message}",
        Azure.RequestFailedException reqEx => reqEx.Message,
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx => reqEx.Status,
        _ => base.GetStatusCode(ex)
    };

    internal record ContainerAppListCommandResult(List<ContainerAppInfo> ContainerApps);
}