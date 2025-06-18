// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Builder;
using AzureMcp.Areas;
using AzureMcp.Areas.AppConfig;
using AzureMcp.Areas.AppConfig.Services;
using AzureMcp.Areas.Authorization;
using AzureMcp.Areas.Authorization.Services;
using AzureMcp.Areas.AzureIsv;
using AzureMcp.Areas.AzureIsv.Services.Datadog;
using AzureMcp.Areas.Cosmos;
using AzureMcp.Areas.Cosmos.Services;
using AzureMcp.Areas.KeyVault.Services;
using AzureMcp.Areas.Kusto.Services;
using AzureMcp.Areas.Monitor;
using AzureMcp.Areas.Monitor.Services;
using AzureMcp.Areas.Postgres;
using AzureMcp.Areas.Postgres.Services;
using AzureMcp.Areas.Redis;
using AzureMcp.Areas.Redis.Services;
using AzureMcp.Areas.Search;
using AzureMcp.Areas.Search.Services;
using AzureMcp.Areas.ServiceBus;
using AzureMcp.Areas.ServiceBus.Services;
using AzureMcp.Areas.Subscription.Services;
using AzureMcp.Commands;
using AzureMcp.Services.Azure.ResourceGroup;
using AzureMcp.Services.Azure.Tenant;
using AzureMcp.Services.Caching;
using AzureMcp.Services.Interfaces;
using AzureMcp.Services.ProcessExecution;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

internal class Program
{
    private static IAreaSetup[] Areas = RegisterAreas();

    private static async Task<int> Main(string[] args)
    {
        try
        {
            ServiceCollection services = new();

            ConfigureServices(services);

            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Information);
            });

            var serviceProvider = services.BuildServiceProvider();

            var parser = BuildCommandLineParser(serviceProvider);
            return await parser.InvokeAsync(args);
        }
        catch (Exception ex)
        {
            WriteResponse(new CommandResponse
            {
                Status = 500,
                Message = ex.Message,
                Duration = 0
            });
            return 1;
        }
    }

    private static IAreaSetup[] RegisterAreas()
    {
        return [
            new AzureMcp.Areas.Storage.StorageSetup(),
        ];
    }

    private static Parser BuildCommandLineParser(IServiceProvider serviceProvider)
    {
        var commandFactory = serviceProvider.GetRequiredService<CommandFactory>();
        var rootCommand = commandFactory.RootCommand;
        var builder = new CommandLineBuilder(rootCommand);

        builder.AddMiddleware(async (context, next) =>
        {
            var commandContext = new CommandContext(serviceProvider);
            var command = context.ParseResult.CommandResult.Command;
            if (command.Handler is IBaseCommand baseCommand)
            {
                var validationResult = baseCommand.Validate(context.ParseResult.CommandResult, commandContext.Response);
                if (!validationResult.IsValid)
                {
                    WriteResponse(commandContext.Response);
                    context.ExitCode = commandContext.Response.Status;
                    return;
                }
            }
            await next(context);
        });

        builder.UseDefaults();
        return builder.Build();
    }

    private static void WriteResponse(CommandResponse response)
    {
        Console.WriteLine(JsonSerializer.Serialize(response, ModelsJsonContext.Default.CommandResponse));
    }

    internal static void ConfigureServices(IServiceCollection services)
    {
        services.ConfigureOpenTelemetry();
        services.AddMemoryCache();
        services.AddSingleton<ICacheService, CacheService>();
        services.AddSingleton<IExternalProcessService, ExternalProcessService>();
        services.AddSingleton<ISubscriptionService, SubscriptionService>();
        services.AddSingleton<ITenantService, TenantService>();
        services.AddSingleton<ICosmosService, CosmosService>();
        services.AddSingleton<IKustoService, KustoService>();
        services.AddSingleton<IDatadogService, DatadogService>();
        services.AddSingleton<IMonitorService, MonitorService>();
        services.AddSingleton<IMonitorHealthModelService, MonitorHealthModelService>();
        services.AddSingleton<IResourceGroupService, ResourceGroupService>();
        services.AddSingleton<IAppConfigService, AppConfigService>();
        services.AddSingleton<ISearchService, SearchService>();
        services.AddSingleton<IPostgresService, PostgresService>();
        services.AddSingleton<IKeyVaultService, KeyVaultService>();
        services.AddSingleton<IServiceBusService, ServiceBusService>();
        services.AddSingleton<IRedisService, RedisService>();
        services.AddSingleton<IAuthorizationService, AuthorizationService>();
        services.AddSingleton<CommandFactory>();

        foreach (var area in Areas)
        {
            services.AddSingleton(area);
            area.ConfigureServices(services);
        }
    }
}
