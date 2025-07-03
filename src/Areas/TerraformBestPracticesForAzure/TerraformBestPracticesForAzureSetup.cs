// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.TerraformBestPractices.Commands;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.TerraformBestPractices;

internal class TerraformBestPracticesForAzureSetup : IAreaSetup
{

    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Register Terraform Best Practices for Azure command at the root level
        var terraformBestPracticesForAzure = new CommandGroup(
            "terraformbestpracticesforazure",
            "Returns Terraform best practices for Azure. Call this before generating Terraform code for Azure Providers."
        );
        rootGroup.AddSubGroup(terraformBestPracticesForAzure);
        terraformBestPracticesForAzure.AddCommand(
            "get",
            new TerraformBestPracticesForAzureGetCommand(loggerFactory.CreateLogger<TerraformBestPracticesForAzureGetCommand>())
        );
    }
}
