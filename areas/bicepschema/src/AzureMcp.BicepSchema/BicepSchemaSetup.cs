// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.BicepSchema.Commands;
using AzureMcp.BicepSchema.Services;
using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.BicepSchema;

public class BicepSchemaSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IBicepSchemaService, BicepSchemaService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {

        // Create Bicep Schema command group
        var bicep = new CommandGroup("bicepschema", "Bicep schema operations - Commands for working with Azure Bicep Infrastructure as Code (IaC) generation and schema management. Includes operations for retrieving Bicep schemas, templates, and resource definitions to support infrastructure deployment automation.");
        rootGroup.AddSubGroup(bicep);

        // Register Bicep Schema command
        bicep.AddCommand("get", new BicepSchemaGetCommand(loggerFactory.CreateLogger<BicepSchemaGetCommand>()));

    }
}
