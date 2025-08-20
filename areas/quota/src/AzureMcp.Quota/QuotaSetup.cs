// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Extensions;
using AzureMcp.Core.Services.Http;
using AzureMcp.Quota.Commands.Region;
using AzureMcp.Quota.Commands.Usage;
using AzureMcp.Quota.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Quota;

public sealed class QuotaSetup : IAreaSetup
{
    public string Name => "quota";

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClientServices();

        services.AddTransient<IQuotaService>(serviceProvider =>
            new QuotaService(serviceProvider.GetService<ILoggerFactory>(), serviceProvider.GetRequiredService<IHttpClientService>()));
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var quota = new CommandGroup(Name, "Quota commands for getting the available regions of specific Azure resource types"
                    + " or checking Azure resource quota and usage");
        rootGroup.AddSubGroup(quota);

        var usageGroup = new CommandGroup("usage", "Resource usage and quota operations");
        usageGroup.AddCommand("check", new CheckCommand(loggerFactory.CreateLogger<CheckCommand>()));
        quota.AddSubGroup(usageGroup);

        var regionGroup = new CommandGroup("region", "Region availability operations");
        var availabilityGroup = new CommandGroup("availability", "Region availability information");
        availabilityGroup.AddCommand("list", new AvailabilityListCommand(loggerFactory.CreateLogger<AvailabilityListCommand>()));
        regionGroup.AddSubGroup(availabilityGroup);
        quota.AddSubGroup(regionGroup);
    }
}
