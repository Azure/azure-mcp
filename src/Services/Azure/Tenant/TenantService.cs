// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Services.Azure.Tenant;

public class TenantService(ICacheService cacheService)
    : BaseAzureService, ITenantService
{
    private readonly ICacheService _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
    private const string CACHE_GROUP = "tenant";
    private const string CACHE_KEY = "tenants";
    private static readonly TimeSpan CACHE_DURATION = TimeSpan.FromHours(12);

    public async Task<List<TenantResource>> GetTenants()
    {
        // Try to get from cache first
        var cachedResults = await _cacheService.GetAsync<List<TenantResource>>(CACHE_GROUP, CACHE_KEY, CACHE_DURATION);
        if (cachedResults != null)
        {
            return cachedResults;
        }

        // If not in cache, fetch from Azure
        var results = new List<TenantResource>();

        var options = AddDefaultPolicies(new ArmClientOptions());
        var client = new ArmClient(await GetCredential(), default, options);

        await foreach (var tenant in client.GetTenants())
        {
            results.Add(tenant);
        }

        // Cache the results
        await _cacheService.SetAsync(CACHE_GROUP, CACHE_KEY, results, CACHE_DURATION);
        return results;
    }

    public bool IsTenantId(string tenant)
    {
        return Guid.TryParse(tenant, out _);
    }

    public async Task<string?> GetTenantId(string tenant)
    {
        if (IsTenantId(tenant))
        {
            return tenant;
        }

        return await GetTenantIdByName(tenant);
    }

    public async Task<string?> GetTenantIdByName(string tenantName)
    {
        var tenants = await GetTenants();
        var tenant = tenants.FirstOrDefault(t => t.Data.DisplayName?.Equals(tenantName, StringComparison.OrdinalIgnoreCase) == true) ??
            throw new Exception($"Could not find tenant with name {tenantName}");

        if (tenant.Data.TenantId == null)
            throw new InvalidOperationException($"Tenant {tenantName} has a null TenantId");

        return tenant.Data.TenantId.ToString();
    }

    public async Task<string?> GetTenantNameById(string tenantId)
    {
        var tenants = await GetTenants();
        var tenant = tenants.FirstOrDefault(t => t.Data.TenantId?.ToString().Equals(tenantId, StringComparison.OrdinalIgnoreCase) == true) ??
            throw new Exception($"Could not find tenant with ID {tenantId}");

        if (tenant.Data.DisplayName == null)
            throw new InvalidOperationException($"Tenant with ID {tenantId} has a null DisplayName");

        return tenant.Data.DisplayName;
    }
}
