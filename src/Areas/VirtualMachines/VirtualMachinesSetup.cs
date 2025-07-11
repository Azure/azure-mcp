// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.VirtualMachines.Commands.Vm;
using AzureMcp.Areas.VirtualMachines.Services;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.VirtualMachines;

public class VirtualMachinesSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IVirtualMachinesService, VirtualMachinesService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Create VirtualMachines command group
        var virtualmachines = new CommandGroup(
            "virtualmachines",
            "Virtual Machine operations - Commands for managing and accessing Azure Virtual Machines. Includes operations for listing VMs and managing their lifecycle.");
        rootGroup.AddSubGroup(virtualmachines);

        // Create VM subgroup
        var vm = new CommandGroup(
            "vm",
            "Virtual Machine operations - Commands for working with individual virtual machines, including listing and status information.");
        virtualmachines.AddSubGroup(vm);

        // Register VM commands
        vm.AddCommand("list", new VmListCommand(
            loggerFactory.CreateLogger<VmListCommand>()));
    }
}