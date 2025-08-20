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
    public string Name => "bicepschema";

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IBicepSchemaService, BicepSchemaService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var bicepschema = new CommandGroup(Name, "Bicep schema utility commands - Commands for interacting with the Bicep schema and related tooling.");
        rootGroup.AddSubGroup(bicepschema);

        // Register Bicep Schema command
        bicepschema.AddCommand("get", new BicepSchemaGetCommand(loggerFactory.CreateLogger<BicepSchemaGetCommand>()));
    }
}
