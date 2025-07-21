// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Areas.ContainerApps.Commands;
using AzureMcp.Areas.ContainerApps.Models;
using AzureMcp.Areas.ContainerApps.Options;
using AzureMcp.Areas.ContainerApps.Options.ContainerRegistry;
using AzureMcp.Areas.ContainerApps.Services;
using AzureMcp.Commands;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.ContainerApps.Commands.ContainerRegistry;

public sealed class ContainerRegistryCreateCommand(ILogger<ContainerRegistryCreateCommand> logger) : BaseContainerAppsCommand<ContainerRegistryCreateOptions>()
{
    private const string CommandTitle = "Create Azure Container Registry";
    private readonly ILogger<ContainerRegistryCreateCommand> _logger = logger;

    public override string Name => "create";

    public override string Description =>
        """
        Create a new Azure Container Registry in the specified resource group and location. 
        Container Registry provides a managed, private Docker registry service based on the open-source Docker Registry 2.0.
        You can use Azure container registries with your existing container development and deployment pipelines.
        
        Example usage:
        azmcp containerapps containerregistry create --subscription "my-subscription" --resource-group "my-rg" --registry-name "myregistry123" --location "eastus" --sku-name "Basic"
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(ContainerAppsOptionDefinitions.ContainerRegistry);
        command.AddOption(ContainerAppsOptionDefinitions.Location);
        command.AddOption(ContainerAppsOptionDefinitions.SkuOption);
    }

    protected override ContainerRegistryCreateOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.RegistryName = parseResult.GetValueForOption(ContainerAppsOptionDefinitions.ContainerRegistry);
        options.Location = parseResult.GetValueForOption(ContainerAppsOptionDefinitions.Location);
        options.SkuName = parseResult.GetValueForOption(ContainerAppsOptionDefinitions.SkuOption);
        return options;
    }

    [McpServerTool(Destructive = false, ReadOnly = false, Title = CommandTitle)]
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
            var registry = await containerAppsService.CreateContainerRegistry(
                options.RegistryName!,
                options.ResourceGroup!,
                options.Subscription!,
                options.Location!,
                options.SkuName,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                registry,
                ContainerAppsJsonContext.Default.ContainerRegistry);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "An exception occurred creating Container Registry '{RegistryName}' in resource group '{ResourceGroup}'.",
                options.RegistryName, options.ResourceGroup);
            HandleException(context, ex);
        }

        return context.Response;
    }
}
