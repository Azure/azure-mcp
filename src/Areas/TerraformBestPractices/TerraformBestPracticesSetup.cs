// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.TerraformBestPractices.Commands;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.TerraformBestPractices;

internal class TerraformBestPracticesSetup : IAreaSetup
{

    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Register Terraform Best Practices command at the root level
        var terraformBestPractices = new CommandGroup(
            "terraformbestpractices",
            "Returns Terraform best practices. Call this before generating Terraform code."
        );
        rootGroup.AddSubGroup(terraformBestPractices);
        terraformBestPractices.AddCommand(
            "get",
            new TerraformBestPracticesGetCommand(loggerFactory.CreateLogger<TerraformBestPracticesGetCommand>())
        );
    }
}
