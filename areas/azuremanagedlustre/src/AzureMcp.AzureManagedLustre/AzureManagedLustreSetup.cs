// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.AzureManagedLustre.Commands.FileSystem;
using AzureMcp.AzureManagedLustre.Services;
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
        var amlfs = new CommandGroup("azuremanagedlustre",
            "Azure Managed Lustre operations - Commands for listing and inspecting Azure Managed Lustre file systems (AMLFS) used for high-performance computing workloads.");
        rootGroup.AddSubGroup(amlfs);

        var fileSystem = new CommandGroup("filesystem", "Azure Managed Lustre file system operations - Commands for listing managed Lustre file systems.");
        amlfs.AddSubGroup(fileSystem);

        fileSystem.AddCommand("list", new FileSystemListCommand(loggerFactory.CreateLogger<FileSystemListCommand>()));
        fileSystem.AddCommand("requiredsubnetsize", new FileSystemSubnetSizeCommand(loggerFactory.CreateLogger<FileSystemSubnetSizeCommand>()));
    }
}
