// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.Authorization;
using AzureMcp.Areas.Authorization.Models;
using AzureMcp.Options;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Tenant;

namespace AzureMcp.Areas.Authorization.Services;

public class AuthorizationService(ITenantService tenantService)
    : BaseAzureService(tenantService), IAuthorizationService
{
    public async Task<List<RoleAssignment>> ListRoleAssignments(
        string? scope,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(scope);

        try
        {
            ArmClient armClient = await CreateArmClientAsync(tenantId, retryPolicy);
            ResourceIdentifier scopeResourceId = new(scope!);
            RoleAssignmentCollection resources = armClient.GetRoleAssignments(scopeResourceId);
            List<RoleAssignment> roleAssignments = [];
            await foreach (RoleAssignmentResource resource in resources.GetAllAsync())
            {
                var roleAssignment = new RoleAssignment
                {
                    Id = resource.Id.ToString(),
                    Name = resource.Data.Name,
                    PrincipalId = resource.Data.PrincipalId,
                    PrincipalType = resource.Data.PrincipalType?.ToString(),
                    RoleDefinitionId = resource.Data.RoleDefinitionId?.ToString(),
                    Scope = resource.Data.Scope,
                    Description = resource.Data.Description,
                    DelegatedManagedIdentityResourceId = resource.Data.DelegatedManagedIdentityResourceId?.ToString() ?? string.Empty,
                    Condition = resource.Data.Condition
                };
                roleAssignments.Add(roleAssignment);
            }

            return roleAssignments;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error listing authorization role assignments: {ex.Message}", ex);
        }
    }
}
