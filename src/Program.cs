// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Builder;
using AzureMcp.Areas;
using AzureMcp.Commands;
using AzureMcp.Services.Azure.ResourceGroup;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;
using AzureMcp.Services.Caching;
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
                builder.ConfigureOpenTelemetryLogger();
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
            // Register core areas
            new AzureMcp.Areas.AzureBestPractices.AzureBestPracticesSetup(),
            new AzureMcp.Areas.Extension.ExtensionSetup(),
            new AzureMcp.Areas.Group.GroupSetup(),
            new AzureMcp.Areas.Server.ServerSetup(),
            new AzureMcp.Areas.Subscription.SubscriptionSetup(),
            new AzureMcp.Areas.Tools.ToolsSetup(),

            // Register Azure service areas
            new AzureMcp.Areas.Aks.AksSetup(),
            new AzureMcp.Areas.AppConfig.AppConfigSetup(),
            new AzureMcp.Areas.Authorization.AuthorizationSetup(),
            new AzureMcp.Areas.AzureIsv.AzureIsvSetup(),
            new AzureMcp.Areas.Cosmos.CosmosSetup(),
            new AzureMcp.Areas.Foundry.FoundrySetup(),
            new AzureMcp.Areas.Grafana.GrafanaSetup(),
            new AzureMcp.Areas.KeyVault.KeyVaultSetup(),
            new AzureMcp.Areas.Kusto.KustoSetup(),
            new AzureMcp.Areas.Monitor.MonitorSetup(),
            new AzureMcp.Areas.Postgres.PostgresSetup(),
            new AzureMcp.Areas.Redis.RedisSetup(),
            new AzureMcp.Areas.Search.SearchSetup(),
            new AzureMcp.Areas.ServiceBus.ServiceBusSetup(),
            new AzureMcp.Areas.Sql.SqlSetup(),
            new AzureMcp.Areas.Storage.StorageSetup(),
            new AzureMcp.Areas.BicepSchema.BicepSchemaSetup(),
            new AzureMcp.Areas.AzureTerraformBestPractices.AzureTerraformBestPracticesSetup(),
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
        services.AddSingleton<ITenantService, TenantService>();
        services.AddSingleton<IResourceGroupService, ResourceGroupService>();
        services.AddSingleton<ISubscriptionService, SubscriptionService>();
        services.AddSingleton<CommandFactory>();

        foreach (var area in Areas)
        {
            services.AddSingleton(area);
            area.ConfigureServices(services);
        }
    }
}
