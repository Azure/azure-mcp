// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.ContainerApps.Commands.ContainerApp;
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
        // Create Container Apps command group
        var containerApps = new CommandGroup("containerapps", "Container Apps operations - Commands for managing and monitoring Azure Container Apps resources.");
        rootGroup.AddSubGroup(containerApps);

        // Register Container Apps commands
        containerApps.AddCommand("list", new ContainerAppListCommand(
            loggerFactory.CreateLogger<ContainerAppListCommand>()));
    }
}