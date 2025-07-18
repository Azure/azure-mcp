// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Authorization.Commands;
using AzureMcp.Areas.Authorization.Services;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Authorization;

internal sealed class AuthorizationSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IAuthorizationService, AuthorizationService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Create Authorization RBAC role command group
        var authorization = new CommandGroup("role",
            "Authorization operations - Commands for managing Azure RBAC resources.");
        rootGroup.AddSubGroup(authorization);

        // Create Role Assignment subgroup
        var roleAssignment = new CommandGroup("assignment",
            "Role assignment operations - Commands for listing and managing Azure RBAC role assignments for a given scope.");
        authorization.AddSubGroup(roleAssignment);

        // Register role assignment commands
        roleAssignment.AddCommand("list", new RoleAssignmentListCommand(loggerFactory.CreateLogger<RoleAssignmentListCommand>()));
    }
}
