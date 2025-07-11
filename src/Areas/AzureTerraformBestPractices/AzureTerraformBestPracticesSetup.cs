// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.AzureTerraformBestPractices.Commands;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.AzureTerraformBestPractices;

internal class AzureTerraformBestPracticesSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Register Azure Terraform Best Practices command at the root level
        var azureTerraformBestPractices = new CommandGroup(
            "azureterraformbestpractices",
            "Returns Terraform best practices for Azure. Call this before generating Terraform code for Azure Providers."
        );
        rootGroup.AddSubGroup(azureTerraformBestPractices);
        azureTerraformBestPractices.AddCommand(
            "get",
            new AzureTerraformBestPracticesGetCommand(loggerFactory.CreateLogger<AzureTerraformBestPracticesGetCommand>())
        );
    }
}
