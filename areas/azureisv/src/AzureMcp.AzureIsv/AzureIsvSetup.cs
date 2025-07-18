// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.AzureIsv.Commands.Datadog;
using AzureMcp.AzureIsv.Services;
using AzureMcp.AzureIsv.Services.Datadog;
using AzureMcp.Core.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AzureMcp.Core.Areas;

namespace AzureMcp.AzureIsv;

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
