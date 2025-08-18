// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.AzureManagedLustre.Commands.FileSystem;
using AzureMcp.AzureManagedLustre.Services;
using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.AzureManagedLustre;

public class AzureManagedLustreSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IAzureManagedLustreService, AzureManagedLustreService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var azureManagedLustre = new CommandGroup("azuremanagedlustre",
            "Azure Managed Lustre operations - Commands for listing and inspecting Azure Managed Lustre file systems (AMLFS) used for high-performance computing workloads.");
        rootGroup.AddSubGroup(azureManagedLustre);

        var fileSystem = new CommandGroup("filesystem", "Azure Managed Lustre file system operations - Commands for listing managed Lustre file systems.");
        azureManagedLustre.AddSubGroup(fileSystem);

        fileSystem.AddCommand("list", new FileSystemListCommand(loggerFactory.CreateLogger<FileSystemListCommand>()));
        fileSystem.AddCommand("required-subnet-size", new FileSystemSubnetSizeCommand(loggerFactory.CreateLogger<FileSystemSubnetSizeCommand>()));
    }
}
