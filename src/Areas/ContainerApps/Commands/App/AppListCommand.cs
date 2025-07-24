// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.ContainerApps.Commands;
using AzureMcp.Areas.ContainerApps.Models;
using AzureMcp.Areas.ContainerApps.Options;
using AzureMcp.Areas.ContainerApps.Options.App;
using AzureMcp.Areas.ContainerApps.Services;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.ContainerApps.Commands.App;

public sealed class AppListCommand(ILogger<AppListCommand> logger) : BaseContainerAppsCommand<AppListOptions>
{
    private const string CommandTitle = "List Container Apps";
    private readonly ILogger<AppListCommand> _logger = logger;
    private new readonly Option<string> _resourceGroupOption = ContainerAppsOptionDefinitions.OptionalResourceGroup;

    public override string Name => "list";

    public override string Description =>
        "Retrieves Azure Container Apps within a specified subscription or resource group.";

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceGroupOption);
    }

    protected override AppListOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        return options;
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

            var containerAppsService = context.GetService<IContainerAppsService>() ??
                throw new InvalidOperationException("Container Apps service is not available.");

            var apps = await containerAppsService.ListApps(
                options.Subscription!,
                options.ResourceGroup,
                options.Environment,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = apps?.Count > 0 ?
                ResponseResult.Create(
                    new AppListCommandResult(apps),
                    ContainerAppsSerializationContext.Default.AppListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error listing container apps. ResourceGroup: {ResourceGroup}, Environment: {Environment}, Options: {@Options}",
                options.ResourceGroup, options.Environment, options);
            HandleException(context, ex);
        }

        return context.Response;
    }

    public record AppListCommandResult(List<ContainerApp> ContainerApps);
}
