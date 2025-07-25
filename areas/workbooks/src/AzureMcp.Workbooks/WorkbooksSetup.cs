// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.Workbooks.Commands.Workbooks;
using AzureMcp.Workbooks.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Workbooks;

public class WorkbooksSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IWorkbooksService, WorkbooksService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var workbooks = new CommandGroup("workbooks", "Workbooks operations - Commands for managing Azure Workbooks resources.");
        rootGroup.AddSubGroup(workbooks);

        workbooks.AddCommand("list", new ListWorkbooksCommand(
            loggerFactory.CreateLogger<ListWorkbooksCommand>()));

        workbooks.AddCommand("show", new ShowWorkbooksCommand(
            loggerFactory.CreateLogger<ShowWorkbooksCommand>()));

        workbooks.AddCommand("update", new UpdateWorkbooksCommand(
            loggerFactory.CreateLogger<UpdateWorkbooksCommand>()));

        workbooks.AddCommand("create", new CreateWorkbooksCommand(
            loggerFactory.CreateLogger<CreateWorkbooksCommand>()));

        workbooks.AddCommand("delete", new DeleteWorkbooksCommand(
            loggerFactory.CreateLogger<DeleteWorkbooksCommand>()));
    }
}
