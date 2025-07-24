// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.AppService.Commands.Database;
using AzureMcp.Areas.AppService.Services;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.AppService;

public class AppServiceSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IAppServiceService, AppServiceService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Create AppService command group
        var appService = new CommandGroup("appservice", "App Service operations - Commands for managing Azure App Service resources including web apps, databases, and configurations.");
        rootGroup.AddSubGroup(appService);

        // Create Database subgroup
        var database = new CommandGroup("database", "App Service database operations - Commands for managing databases connected to Azure App Service applications.");
        appService.AddSubGroup(database);

        // Register Database commands
        database.AddCommand("add", new DatabaseAddCommand(
            loggerFactory.CreateLogger<DatabaseAddCommand>()));
    }
}
