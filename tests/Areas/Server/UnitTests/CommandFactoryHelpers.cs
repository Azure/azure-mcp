// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using AzureMcp.Areas;
using AzureMcp.Commands;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Tests.Areas.Server.UnitTests;

internal class CommandFactoryHelpers
{
    public static CommandFactory CreateCommandFactory(IServiceProvider? serviceProvider = default)
    {
        IServiceProvider services = serviceProvider ?? new ServiceCollection().AddLogging().BuildServiceProvider();

        var logger = services.GetRequiredService<ILogger<CommandFactory>>();
        var telemetryService = services.GetService<ITelemetryService>() ?? new NoOpTelemetryService();
        var areaSetups = typeof(IAreaSetup).Assembly.GetTypes()
           .Where(t => typeof(IAreaSetup).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
           .Select(t => ActivatorUtilities.CreateInstance(services, t) as IAreaSetup)
           .OfType<IAreaSetup>()
           .ToArray();

        var commandFactory = new CommandFactory(services, areaSetups, telemetryService, logger);

        return commandFactory;
    }

    public class NoOpTelemetryService : ITelemetryService
    {
        public void Dispose()
        {
        }

        public Activity? StartActivity(string activityName)
        {
            return null;
        }

        public Activity? StartActivity(string activityName, Implementation? clientInfo)
        {
            return null;
        }
    }
}
