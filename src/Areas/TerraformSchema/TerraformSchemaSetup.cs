// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.TerraformSchema.Commands;
using AzureMcp.Areas.TerraformSchema.Services;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.TerraformSchema;

public class TerraformSchemaSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ITerraformSchemaService, TerraformSchemaService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {

        // Create Terraform Schema command group
        var terraform = new CommandGroup("terraformschema", "Terraform schema operations - Commands for working with Terraform IaC generation.");
        rootGroup.AddSubGroup(terraform);

        // Register Terraform Schema command
        terraform.AddCommand("get", new TerraformSchemaGetCommand(loggerFactory.CreateLogger<TerraformSchemaGetCommand>()));

    }
}

