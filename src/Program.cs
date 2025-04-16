// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Commands;
using AzureMcp.Extensions;
using AzureMcp.Models.Command;
using AzureMcp.Services.Azure.AppConfig;
using AzureMcp.Services.Azure.Cosmos;
using AzureMcp.Services.Azure.Monitor;
using AzureMcp.Services.Azure.ResourceGroup;
using AzureMcp.Services.Azure.Storage;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;
using AzureMcp.Services.Caching;
using AzureMcp.Services.Interfaces;
using AzureMcp.Services.ProcessExecution;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.Text.Json;

try
{
    ServiceCollection services = new();
    ConfigureServices(services);
    var serviceProvider = services.BuildServiceProvider();

    var commandFactory = serviceProvider.GetRequiredService<CommandFactory>();
    var rootCommand = commandFactory.RootCommand;

    return await rootCommand.InvokeAsync(args);
}
catch (Exception ex)
{
    CommandResponse response = new()
    {
        Status = 500,
        Message = ex.Message,
        Arguments = [],
        Duration = 0
    };

    Console.WriteLine(JsonSerializer.Serialize(response));
    return 1;
}

static void ConfigureServices(IServiceCollection services)
{
    services.ConfigureOpenTelemetry();
    services.AddMemoryCache();
    services.AddLogging(builder =>
    {
        builder.AddConsole();
        builder.SetMinimumLevel(LogLevel.Information);
    });
    services.AddSingleton<ICacheService, CacheService>();
    services.AddSingleton<IExternalProcessService, ExternalProcessService>();
    services.AddSingleton<ISubscriptionService, SubscriptionService>();
    services.AddSingleton<ITenantService, TenantService>();
    services.AddSingleton<ICosmosService, CosmosService>();
    services.AddSingleton<IStorageService, StorageService>();
    services.AddSingleton<IMonitorService, MonitorService>();
    services.AddSingleton<IResourceGroupService, ResourceGroupService>();
    services.AddSingleton<IAppConfigService, AppConfigService>();
    services.AddSingleton<CommandFactory>();
}