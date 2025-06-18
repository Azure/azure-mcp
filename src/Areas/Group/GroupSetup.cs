// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Group.Commands;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Group;

internal class GroupSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Create Group command group
        var group = new CommandGroup("group", "Resource group operations - Commands for listing and managing Azure resource groups in your subscriptions.");
        rootGroup.AddSubGroup(group);

        // Register Group commands
        group.AddCommand("list", new GroupListCommand(loggerFactory.CreateLogger<GroupListCommand>()));
    }
}
