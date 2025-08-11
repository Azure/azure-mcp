// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using AzureMcp.AppConfig;
using AzureMcp.Core.Areas;
using AzureMcp.Core.Areas.Subscription;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Deploy;
using AzureMcp.KeyVault;
using AzureMcp.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Core.UnitTests.Areas.Server;

internal class CommandFactoryHelpers
{
    public static CommandFactory CreateCommandFactory(IServiceProvider? serviceProvider = default)
    {
        IServiceProvider services = serviceProvider ?? new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();

        var logger = services.GetRequiredService<ILogger<CommandFactory>>();
        var telemetryService = services.GetService<ITelemetryService>() ?? new NoOpTelemetryService();

        IAreaSetup[] areaSetups = [
            new SubscriptionSetup(),
            new KeyVaultSetup(),
            new StorageSetup(),
            new DeploySetup(),
            new AppConfigSetup()
        ];

        var commandFactory = new CommandFactory(services, areaSetups, telemetryService, logger);

        return commandFactory;
    }

    public class NoOpTelemetryService : ITelemetryService
    {
        public ValueTask<Activity?> StartActivity(string activityName)
        {
            return ValueTask.FromResult<Activity?>(null);
        }

        public ValueTask<Activity?> StartActivity(string activityName, Implementation? clientInfo)
        {
            return ValueTask.FromResult<Activity?>(null);
        }

        public void Dispose()
        {
        }
    }
}
