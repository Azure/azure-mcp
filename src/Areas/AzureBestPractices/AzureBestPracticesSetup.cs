// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.AzureBestPractices.Commands;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.AzureBestPractices;

internal class AzureBestPracticesSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Register Azure Best Practices command at the root level
        var bestPractices = new CommandGroup(
            "bestpractices",
            "Returns secure, production-grade Azure SDK best practices. Call this before generating Azure SDK code."
        );
        rootGroup.AddSubGroup(bestPractices);

        bestPractices.AddCommand("get", new AzureBestPracticesGetCommand(loggerFactory.CreateLogger<AzureBestPracticesGetCommand>()));
    }
}
