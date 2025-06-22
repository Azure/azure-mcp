// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options;

namespace AzureMcp.Areas.Marketplace.Options.Product;

public class ProductGetOptions : SubscriptionOptions
{
    public string? ProductId { get; set; }
    public bool? IncludeStopSoldPlans { get; set; }
    public string? Language { get; set; }
    public string? Market { get; set; }
    public bool? LookupOfferInTenantLevel { get; set; }
    public bool? IncludeHiddenPlans { get; set; }
    public string? PlanId { get; set; }
    public string? SkuId { get; set; }
    public bool? IncludeServiceInstructionTemplates { get; set; }
    public string? PartnerTenantId { get; set; }
    public string? PricingAudience { get; set; }
    public string? ObjectId { get; set; }
    public string? AltSecId { get; set; }
}