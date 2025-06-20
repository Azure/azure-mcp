// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Tests.Commands.Server;

internal class CommandFactoryHelpers
{
    public static CommandFactory CreateCommandFactory(IServiceProvider? serviceProvider = default)
    {
        IServiceProvider services = serviceProvider ?? new ServiceCollection().AddLogging().BuildServiceProvider();

        var logger = services.GetRequiredService<ILogger<CommandFactory>>();

        var areaSetups = typeof(IAreaSetup).Assembly.GetTypes()
           .Where(t => typeof(IAreaSetup).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
           .Select(t => ActivatorUtilities.CreateInstance(services, t) as IAreaSetup)
           .OfType<IAreaSetup>()
           .ToArray();

        var commandFactory = new CommandFactory(services, areaSetups, logger);

        return commandFactory;
    }
}
