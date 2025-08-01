// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.Quota.Commands;
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
        var quota = new CommandGroup("quota", "Quota commands for getting available region for Azure resources or getting usage for Azure resource per region");
        rootGroup.AddSubGroup(quota);

        quota.AddCommand("usage-get", new UsageCheckCommand(loggerFactory.CreateLogger<UsageCheckCommand>()));
        quota.AddCommand("available-region-list", new RegionCheckCommand(loggerFactory.CreateLogger<RegionCheckCommand>()));
    }
}
