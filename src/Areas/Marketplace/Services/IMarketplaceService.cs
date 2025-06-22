// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Nodes;
using AzureMcp.Options;

namespace AzureMcp.Areas.Marketplace.Services;

public interface IMarketplaceService
{
    Task<JsonNode?> GetProduct(
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
        RetryPolicyOptions? retryPolicy = null);
}