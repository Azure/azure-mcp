// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.AzureBestPractices.Commands;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.AzureBestPractices;

public class AzureBestPracticesSetup : IAreaSetup
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

        // Create Best Practices subgroups
        var general = new CommandGroup("general", "General Azure SDK best practices - Commands for retrieving general best practices for secure and production-grade Azure SDK usage.");
        bestPractices.AddSubGroup(general);

        var azureFunctions = new CommandGroup("azurefunctions", "Azure Functions code generation and deployment best practices - Commands for retrieving best practices for secure and production-grade Azure Functions code generation and deployment.");
        bestPractices.AddSubGroup(azureFunctions);

        general.AddCommand(
            "get",
            new AzureBestPracticesGetCommand(loggerFactory.CreateLogger<AzureBestPracticesGetCommand>())
        );

        azureFunctions.AddCommand(
            "get-code-generation",
            new AzureFunctionsCodeGenBestPracticesGetCommand(loggerFactory.CreateLogger<AzureFunctionsCodeGenBestPracticesGetCommand>())
        );

        azureFunctions.AddCommand(
            "get-deployment",
            new AzureFunctionsDeploymentBestPracticesGetCommand(loggerFactory.CreateLogger<AzureFunctionsDeploymentBestPracticesGetCommand>())
        );
    }
}
