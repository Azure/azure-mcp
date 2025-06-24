// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Nodes;
using Azure.Core;
using AzureMcp.Options;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Tenant;

namespace AzureMcp.Areas.Marketplace.Services;

public class MarketplaceService(ITenantService tenantService)
    : BaseAzureService(tenantService), IMarketplaceService
{
    private const int TokenExpirationBuffer = 300;
    private const string ManagementApiBaseUrl = "https://management.azure.com";
    private const string ApiVersion = "2023-01-01-preview";
    private static readonly HttpClient s_sharedHttpClient = new HttpClient();

    private string? _cachedAccessToken;
    private DateTimeOffset _tokenExpiryTime;

    /// <summary>
    /// Retrieves a single private product (offer) for a given subscription.
    /// </summary>
    /// <param name="productId">The ID of the product to retrieve.</param>
    /// <param name="subscription">The Azure subscription ID.</param>
    /// <param name="includeStopSoldPlans">Include stop-sold or hidden plans.</param>
    /// <param name="language">Product language (default: en).</param>
    /// <param name="market">Product market (default: US).</param>
    /// <param name="lookupOfferInTenantLevel">Check against tenant private audience.</param>
    /// <param name="includeHiddenPlans">Include hidden plans.</param>
    /// <param name="planId">Filter by plan ID.</param>
    /// <param name="skuId">Filter by SKU ID.</param>
    /// <param name="includeServiceInstructionTemplates">Include service instruction templates.</param>
    /// <param name="partnerTenantId">Partner tenant ID.</param>
    /// <param name="pricingAudience">Pricing audience.</param>
    /// <param name="objectId">AAD user ID.</param>
    /// <param name="altSecId">Alternate Security ID (for MSA users).</param>
    /// <param name="tenant">Optional. The Azure tenant ID for authentication.</param>
    /// <param name="retryPolicy">Optional. Policy parameters for retrying failed requests.</param>
    /// <returns>A JSON node containing the product information.</returns>
    /// <exception cref="ArgumentException">Thrown when required parameters are missing or invalid.</exception>
    /// <exception cref="Exception">Thrown when parsing the product response fails.</exception>
    public async Task<JsonNode?> GetProduct(
        string productId,
        string subscription,
        bool? includeStopSoldPlans = null,
        string? language = null,
        string? market = null,
        bool? lookupOfferInTenantLevel = null,
        bool? includeHiddenPlans = null,
        string? planId = null,
        string? skuId = null,
        bool? includeServiceInstructionTemplates = null,
        string? partnerTenantId = null,
        string? pricingAudience = null,
        string? objectId = null,
        string? altSecId = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(productId, subscription);

        string productUrl = BuildProductUrl(subscription, productId, includeStopSoldPlans, language, market,
            lookupOfferInTenantLevel, includeHiddenPlans, planId, skuId, includeServiceInstructionTemplates);

        string productResponseString = await GetMarketplaceResponseAsync(productUrl, partnerTenantId,
            pricingAudience, objectId, altSecId, tenant);

        return JsonNode.Parse(productResponseString) ?? throw new Exception("Failed to parse product response to JSON.");
    }

    private static string BuildProductUrl(
        string subscription,
        string productId,
        bool? includeStopSoldPlans,
        string? language,
        string? market,
        bool? lookupOfferInTenantLevel,
        bool? includeHiddenPlans,
        string? planId,
        string? skuId,
        bool? includeServiceInstructionTemplates)
    {
        var queryParams = new List<string>
        {
            $"api-version={ApiVersion}"
        };

        if (includeStopSoldPlans.HasValue)
            queryParams.Add($"includeStopSoldPlans={includeStopSoldPlans.Value.ToString().ToLower()}");

        if (!string.IsNullOrEmpty(language))
            queryParams.Add($"language={Uri.EscapeDataString(language)}");

        if (!string.IsNullOrEmpty(market))
            queryParams.Add($"market={Uri.EscapeDataString(market)}");

        if (lookupOfferInTenantLevel.HasValue)
            queryParams.Add($"lookupOfferInTenantLevel={lookupOfferInTenantLevel.Value.ToString().ToLower()}");

        if (includeHiddenPlans.HasValue)
            queryParams.Add($"includeHiddenPlans={includeHiddenPlans.Value.ToString().ToLower()}");

        if (!string.IsNullOrEmpty(planId))
            queryParams.Add($"planId={Uri.EscapeDataString(planId)}");

        if (!string.IsNullOrEmpty(skuId))
            queryParams.Add($"skuId={Uri.EscapeDataString(skuId)}");

        if (includeServiceInstructionTemplates.HasValue)
            queryParams.Add($"includeServiceInstructionTemplates={includeServiceInstructionTemplates.Value.ToString().ToLower()}");

        string queryString = string.Join("&", queryParams);
        return $"{ManagementApiBaseUrl}/subscriptions/{subscription}/providers/Microsoft.Marketplace/products/{productId}?{queryString}";
    }

    private async Task<string> GetMarketplaceResponseAsync(string url, string? partnerTenantId,
        string? pricingAudience, string? objectId, string? altSecId, string? tenant)
    {
        string accessToken = await GetAccessTokenAsync(tenant);
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

        // Add optional headers as specified in the API
        if (!string.IsNullOrEmpty(partnerTenantId))
            request.Headers.Add("PartnerTenantId", partnerTenantId);

        if (!string.IsNullOrEmpty(pricingAudience))
            request.Headers.Add("PricingAudience", pricingAudience);

        if (!string.IsNullOrEmpty(objectId))
            request.Headers.Add("ObjectId", objectId);

        if (!string.IsNullOrEmpty(altSecId))
            request.Headers.Add("AltSecId", altSecId);

        HttpResponseMessage response = await s_sharedHttpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        string responseString = await response.Content.ReadAsStringAsync();
        return responseString;
    }

    private async Task<string> GetAccessTokenAsync(string? tenant = null)
    {
        if (_cachedAccessToken != null && DateTimeOffset.UtcNow < _tokenExpiryTime)
        {
            return _cachedAccessToken;
        }

        AccessToken accessToken = await GetEntraIdAccessTokenAsync(ManagementApiBaseUrl, tenant);
        _cachedAccessToken = accessToken.Token;
        _tokenExpiryTime = accessToken.ExpiresOn.AddSeconds(-TokenExpirationBuffer);

        return _cachedAccessToken;
    }

    private async Task<AccessToken> GetEntraIdAccessTokenAsync(string resource, string? tenant = null)
    {
        var tokenRequestContext = new TokenRequestContext(new[] { $"{resource}/.default" });
        var tokenCredential = await GetCredential(tenant);
        return await tokenCredential
            .GetTokenAsync(tokenRequestContext, CancellationToken.None)
            .ConfigureAwait(false);
    }
}
