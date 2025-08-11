// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.Quota.Commands.Region;
using AzureMcp.Quota.Commands.Usage;
using AzureMcp.Quota.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Quota;

public sealed class QuotaSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IQuotaService, QuotaService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var quota = new CommandGroup("quota", "Quota commands for Azure resource quota checking and usage analysis");
        rootGroup.AddSubGroup(quota);

        // Resource usage and quota operations
        var usageGroup = new CommandGroup("usage", "Resource usage and quota operations");
        usageGroup.AddCommand("check", new CheckCommand(loggerFactory.CreateLogger<CheckCommand>()));
        quota.AddSubGroup(usageGroup);

        // Region availability operations
        var regionGroup = new CommandGroup("region", "Region availability operations");
        var availabilityGroup = new CommandGroup("availability", "Region availability information");
        availabilityGroup.AddCommand("list", new AvailabilityListCommand(loggerFactory.CreateLogger<AvailabilityListCommand>()));
        regionGroup.AddSubGroup(availabilityGroup);
        quota.AddSubGroup(regionGroup);
    }
}
