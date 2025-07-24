// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.ContainerApps.Commands.App;
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
        // Create ContainerApp command group
        var containerApp = new CommandGroup("containerapp", "Container Apps operations - Commands for managing and listing Azure Container Apps.");
        rootGroup.AddSubGroup(containerApp);

        // Register ContainerApp commands
        containerApp.AddCommand("list", new AppListCommand(loggerFactory.CreateLogger<AppListCommand>()));
    }
}
