// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.BicepSchema.Commands;
using AzureMcp.Areas.BicepSchema.Services;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.BicepSchema;

public class BicepSchemaSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IBicepSchemaService, BicepSchemaService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {

        // Create Bicep Schema command group
        var bicep = new CommandGroup("bicepschema", "Bicep schema operations - Commands for working with Bicep IaC generation.");
        rootGroup.AddSubGroup(bicep);

        // Register Bicep Schema command
        bicep.AddCommand("get", new BicepSchemaGetCommand(loggerFactory.CreateLogger<BicepSchemaGetCommand>()));

    }
}
