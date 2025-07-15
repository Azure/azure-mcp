// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.ContainerApps.Commands.ContainerRegistry;
using AzureMcp.Areas.ContainerApps.Services;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.ContainerApps;

public class ContainerAppsSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IContainerAppsService, ContainerAppsService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Create ContainerApps command group
        var containerApps = new CommandGroup("containerapps", "Container Apps operations - Commands for managing Azure Container Apps and related services like Container Registry");
        rootGroup.AddSubGroup(containerApps);

        // Create Container Registry subgroup
        var containerRegistry = new CommandGroup("containerregistry", "Container Registry operations - Commands for creating and managing Azure Container Registry instances");
        containerApps.AddSubGroup(containerRegistry);

        // Register Container Registry commands
        containerRegistry.AddCommand("create", new ContainerRegistryCreateCommand(
            loggerFactory.CreateLogger<ContainerRegistryCreateCommand>()));
    }
}
