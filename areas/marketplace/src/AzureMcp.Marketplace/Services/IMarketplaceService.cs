// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Marketplace.Models;
using AzureMcp.Core.Options;

namespace AzureMcp.Marketplace.Services;

public interface IMarketplaceService
{
    Task<ProductDetails> GetProduct(
        string productId,
        string subscription,
        bool? includeStopSoldPlans = null,
        string? language = null,
        string? market = null,
        bool? lookupOfferInTenantLevel = null,
        string? planId = null,
        string? skuId = null,
        bool? includeServiceInstructionTemplates = null,
        string? partnerTenantId = null,
        string? pricingAudience = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
}
