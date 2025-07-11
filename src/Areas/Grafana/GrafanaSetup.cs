// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Grafana.Commands.Workspace;
using AzureMcp.Areas.Grafana.Services;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Grafana;

public class GrafanaSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IGrafanaService, GrafanaService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var grafana = new CommandGroup("grafana", "Grafana workspace operations - Commands for managing and accessing Azure Managed Grafana resources.");
        rootGroup.AddSubGroup(grafana);

        grafana.AddCommand("list", new WorkspaceListCommand(loggerFactory.CreateLogger<WorkspaceListCommand>()));
    }
}
