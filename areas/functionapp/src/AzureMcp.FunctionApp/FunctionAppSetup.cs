// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.FunctionApp.Commands.FunctionApp;
using AzureMcp.FunctionApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.FunctionApp;

public class FunctionAppSetup : IAreaSetup
{
    public string Name => "functionapp";

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IFunctionAppService, FunctionAppService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var functionApp = new CommandGroup(Name, "Function App operations - Commands for managing and accessing Azure Function App resources.");
        rootGroup.AddSubGroup(functionApp);

        functionApp.AddCommand("list", new FunctionAppListCommand(
            loggerFactory.CreateLogger<FunctionAppListCommand>()));

        functionApp.AddCommand("get", new FunctionAppGetCommand(
            loggerFactory.CreateLogger<FunctionAppGetCommand>()));
    }
}
