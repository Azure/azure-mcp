// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.ResourceHealth.Commands.AvailabilityStatus;
using AzureMcp.ResourceHealth.Commands.ResourceEvents;
using AzureMcp.ResourceHealth.Commands.ServiceHealthEvents;
using AzureMcp.ResourceHealth.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.ResourceHealth;

public class ResourceHealthSetup : IAreaSetup
{
    public string Name => "resourcehealth";

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IResourceHealthService, ResourceHealthService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var resourceHealth = new CommandGroup(Name,
            """
            Resource Health operations - Commands for monitoring and diagnosing Azure resource health status.
            Use this tool to check the current availability status of Azure resources and identify potential issues.
            This tool provides access to Azure Resource Health data including availability state, detailed status,
            and historical health information for troubleshooting and monitoring purposes.
            """);
        rootGroup.AddSubGroup(resourceHealth);

        // Create availability-status subgroup
        var availabilityStatus = new CommandGroup("availability-status",
            "Resource availability status operations - Commands for retrieving current and historical availability status of Azure resources.");
        resourceHealth.AddSubGroup(availabilityStatus);

        // Create service-health-events subgroup
        var serviceHealthEvents = new CommandGroup("service-health-events",
            "Service health events operations - Commands for retrieving service health events affecting Azure services and subscriptions.");
        resourceHealth.AddSubGroup(serviceHealthEvents);

        // Create resource-events subgroup
        var resourceEvents = new CommandGroup("resource-events",
            "Resource health events operations - Commands for retrieving historical availability events for specific Azure resources.");
        resourceHealth.AddSubGroup(resourceEvents);

        // Register availability-status commands
        availabilityStatus.AddCommand("get", new AvailabilityStatusGetCommand(loggerFactory.CreateLogger<AvailabilityStatusGetCommand>()));
        availabilityStatus.AddCommand("list", new AvailabilityStatusListCommand(loggerFactory.CreateLogger<AvailabilityStatusListCommand>()));

        // Register service-health-events commands
        serviceHealthEvents.AddCommand("list", new ServiceHealthEventsListCommand(loggerFactory.CreateLogger<ServiceHealthEventsListCommand>()));

        // Register resource-events commands
        resourceEvents.AddCommand("get", new ResourceEventsGetCommand(loggerFactory.CreateLogger<ResourceEventsGetCommand>()));
    }
}
