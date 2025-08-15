// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.Grafana.Commands.Workspace;
using AzureMcp.Grafana.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Grafana;

public class GrafanaSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IGrafanaService, GrafanaService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var grafana = new CommandGroup("grafana", "Grafana workspace operations - Commands for managing and accessing Azure Managed Grafana instances for creating monitoring dashboards and data visualizations from Azure Monitor, Application Insights, and other data sources. Use this tool when you need to list Grafana workspaces, access workspace configurations, manage dashboard permissions, or retrieve information about available Grafana instances for monitoring and observability. This tool focuses on Azure Managed Grafana service management rather than dashboard content creation. Do not use this tool for Azure Monitor queries, Application Insights analytics, Log Analytics workspace operations, or creating dashboard content - this tool manages the Grafana service instances themselves.");
        rootGroup.AddSubGroup(grafana);

        grafana.AddCommand("list", new WorkspaceListCommand(loggerFactory.CreateLogger<WorkspaceListCommand>()));
    }
}
