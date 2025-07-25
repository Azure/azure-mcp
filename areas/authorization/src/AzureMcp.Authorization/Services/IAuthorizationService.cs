// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Authorization.Models;
using AzureMcp.Core.Options;

namespace AzureMcp.Authorization.Services;

public interface IAuthorizationService
{
    /// <summary>
    /// Lists all role assignments in the subscription.
    /// </summary>
    /// <param name="scope">The scope that the resource will apply against.</param>
    /// <param name="tenantId">Optional tenant ID for cross-tenant operations.</param>
    /// <param name="retryPolicy">Optional retry policy for the operation.</param>
    /// <returns>List of role assignments in the format "Role Definition ID: Principal ID"</returns>
    Task<List<RoleAssignment>> ListRoleAssignments(
        string? scope,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null);
}
