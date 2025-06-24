// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.AzureIsv.Commands.Datadog;
using AzureMcp.Areas.AzureIsv.Services;
using AzureMcp.Areas.AzureIsv.Services.Datadog;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.AzureIsv;

public class AzureIsvSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IDatadogService, DatadogService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var datadog = new CommandGroup("datadog", "Datadog operations - Commands for managing and querying Datadog resources.");
        rootGroup.AddSubGroup(datadog);

        var monitoredResources = new CommandGroup("monitoredresources", "Datadog monitored resources operations - Commands for listing monitored resources in a specific Datadog monitor.");
        datadog.AddSubGroup(monitoredResources);

        monitoredResources.AddCommand("list", new MonitoredResourcesListCommand(loggerFactory.CreateLogger<MonitoredResourcesListCommand>()));
    }
}
