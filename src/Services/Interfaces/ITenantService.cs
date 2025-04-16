// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;

namespace AzureMcp.Services.Interfaces;

public interface ITenantService
{
    Task<List<ArgumentOption>> GetTenants();
    Task<string> GetTenantId(string tenant);
    Task<string> GetTenantIdByName(string tenantName);
    Task<string> GetTenantNameById(string tenantId);
    bool IsTenantId(string tenant);
}