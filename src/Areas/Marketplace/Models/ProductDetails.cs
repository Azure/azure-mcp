// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Marketplace.Models;
public class ProductDetails : ProductSummary
{
    public string? Language { get; set; }
    public bool? HasStandardContractAmendments { get; set; }
    public string? OfferId { get; set; }
    public Guid? StandardContractAmendmentsRevisionId { get; set; }
    public string? UniversalAmendmentUrl { get; set; }
    public bool? IsPrivate { get; set; }
    public bool? IsStopSell { get; set; }
    public StopSellInfo? StopSellInfo { get; set; }
    public MarketingMaterial? MarketingMaterial { get; set; }
    public string? LegalTermsUri { get; set; }
    public LegalTermsType? LegalTermsType { get; set; }
    public new string? PrivacyPolicyUri { get; set; }
    public string? SupportUri { get; set; }
    public string? UiDefinitionUri { get; set; }
    public IList<string>? ScreenshotUris { get; set; }
    public IList<LinkProperties>? Links { get; set; }
    public string? LargeIconUri { get; set; }
    public string? WideIconUri { get; set; }
    public IList<ImageGroup>? Images { get; set; }
    public IList<Artifact>? Artifacts { get; set; }
    public IList<ProductVideo>? Videos { get; set; }
    public new IList<PlanDetails>? Plans { get; set; }
    public IList<PlanDetails>? Skus
    {
        get => Plans;
        set => Plans = value;
    }
    public IDictionary<string, string>? AdditionalProperties { get; set; }
    public string? PricingDetailsUri { get; set; }
    public bool? IsReseller { get; set; }
    public bool? DisableSendEmailOnPurchase { get; set; }
    public string? ServiceId { get; set; }
    public string? ProductCustomData { get; set; }
}

public class ProductSummary
{
    public string? DisplayName { get; set; }
    public double? Popularity { get; set; }
    public IReadOnlyList<string>? CategoryIds { get; set; }
    public IReadOnlyList<string>? IndustryIds { get; set; }
    public string? PublisherId { get; set; }
    public AzureBenefit? AzureBenefit { get; set; }
    public IReadOnlyList<Badge>? Badges { get; set; }
    public IReadOnlyList<string>? ProductBadges { get; set; }
    public IReadOnlyList<string>? ProductLabels { get; set; }
    public PublisherType? PublisherType { get; set; }
    public PublishingStage? PublishingStage { get; set; }
    public string? UniqueProductId { get; set; }
    public ProductType? ProductType { get; set; }
    public string? ProductSubType { get; set; }
    public string? ProductFamily { get; set; }
    public IReadOnlyList<string>? OperatingSystems { get; set; }
    public IReadOnlyList<PricingType>? PricingTypes { get; set; }
    public string? PublisherDisplayName { get; set; }
    public string? LongSummary { get; set; }
    public string? Summary { get; set; }
    public IReadOnlyList<string>? LinkedAddIns { get; set; }
    public IDictionary<string, string>? LinkedAddInsTypes { get; set; }
    public string? SmallIconUri { get; set; }
    public string? MediumIconUri { get; set; }
    public string? Description { get; set; }
    public string? PrivacyPolicyUri { get; set; }
    public IReadOnlyList<RatingBucket>? RatingBuckets { get; set; }
    public double? RatingAverage { get; set; }
    public int? RatingCount { get; set; }
    public MarketStartPrice? StartingPrice { get; set; }
    public IReadOnlyList<PlanSummary>? Plans { get; set; }
    public IReadOnlyList<string>? SupportedProducts { get; set; }
    public IReadOnlyList<string>? ApplicableProducts { get; set; }
    public DateTimeOffset? LastModifiedDateTime { get; set; }
    public IReadOnlyList<string>? Locations { get; set; }
    public string? ServiceFamily { get; set; }
    public string? Service { get; set; }
    public string? ProductId { get; set; }
    public bool? HasRIPlans { get; set; }
    public bool? HasMarketplaceFootprint { get; set; }
    public List<ProductAttribute>? Attributes { get; set; }
    public bool? IsCoreVm { get; set; }
    public string? ProductOwnershipSellingMotion { get; set; }
    public string? ReservationResourceType { get; set; }
    public bool? IsTestProduct { get; set; }
    public int? MaxQuantityOnProposal { get; set; }
    public SkuAggregatedData? SkuAggregatedData { get; set; }
}

public class PlanDetails : PlanSummary
{
    public string? Id { get; set; }
    public IList<Availability>? Availabilities { get; set; }
    public string? UiDefinitionUri { get; set; }
    public IList<Artifact>? Artifacts { get; set; }
    public string? GalleryItemVersion { get; set; }
    public bool? IsHidden { get; set; }
    public bool? IsStopSell { get; set; }
    public StopSellInfo? StopSellInfo { get; set; }
    public CspState? CSPState { get; set; }
    public int? MinQuantity { get; set; }
    public int? MaxQuantity { get; set; }
    public bool? IsQuantifiable { get; set; }
    public IList<BillingComponent>? BillingComponents { get; set; }
    public IList<PurchaseDurationDiscount>? PurchaseDurationDiscounts { get; set; }
    public bool? IsHiddenPrivateOffer { get; set; }
    public IList<LinkProperties>? Certifications { get; set; }
    public string? CustomerInstruction { get; set; }
    public string? PlanArtifactsVersion { get; set; }

    // Sku properties
    public string? SkuType { get; set; }
    public string? SkuTitle { get; set; }
    public string? Location { get; set; }
    public string? ArmRegionName { get; set; }
    public string? Cloud { get; set; }
    public string? LocationType { get; set; }
    public string? Region { get; set; }
    public string? SkuGroupId { get; set; }
    public string? Zone { get; set; }
    public string? Feature { get; set; }
    public string? ServiceType { get; set; }
    public List<ProductAttribute>? SkuAttributes { get; set; }
    public SkuProperties? SkuProperties { get; set; }
    public List<OfferingProperties>? OfferingProperties { get; set; }
}

public class PlanSummary
{
    public string? PlanId { get; set; }
    public string? UniquePlanId { get; set; }
    public string? DisplayName { get; set; }
    public VmArchitectureType? VmArchitectureType { get; set; }
    public CspState? CspState { get; set; }
    public PlanMetadata? Metadata { get; set; }
    public string? AltStackReference { get; set; }
    public string? StackType { get; set; }
    public string? AltArchitectureReference { get; set; }
    public IReadOnlyList<string>? CategoryIds { get; set; }
    public bool? HasProtectedArtifacts { get; set; }
    public IReadOnlyList<PricingType>? PricingTypes { get; set; }
    public IReadOnlyList<VmSecurityType>? VmSecurityTypes { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public string? SkuId { get; set; }
    public ProductType? Type { get; set; }
    public string? DisplayRank { get; set; }
    public bool? IsPrivate { get; set; }
    public bool? HasRI { get; set; }
    public IReadOnlyList<string>? PlanLabels { get; set; }
}

public class StopSellInfo
{
    public DateTime? StartDate { get; set; }
    public StopSellReason? Reason { get; set; }
    public string? AlternativeOfferId { get; set; }
    public string? AlternativePlanId { get; set; }
}

public class MarketingMaterial
{
    public string? Path { get; set; }
    public string? LearnUri { get; set; }
}

public class LinkProperties
{
    public string? Id { get; set; }
    public string? DisplayName { get; set; }
    public string? Uri { get; set; }
}

public class ImageGroup
{
    public ImageGroup()
    {
        Items = new List<Image>();
    }

    public string? Context { get; set; }
    public IList<Image>? Items { get; set; }
}

public class Artifact
{
    public string? Name { get; set; }
    public string? Uri { get; set; }
    public ArtifactType? Type { get; set; }
}

public class ProductVideo
{
    public string? Caption { get; set; }
    public Uri? Uri { get; set; }
    public string? VideoPurpose { get; set; }
    public PreviewImage? PreviewImage { get; set; }
}

public class Availability
{
    public string? Id { get; set; }
    public IList<string>? Actions { get; set; }
    public Meter? Meter { get; set; }
    public PricingAudience? PricingAudience { get; set; }
    public IList<Term>? Terms { get; set; }
    public bool? HasFreeTrials { get; set; }
    public string? ConsumptionUnitType { get; set; }
    public int? DisplayRank { get; set; }
    public InvoicingPolicy? InvoicingPolicy { get; set; }
}

public class BillingComponent
{
    public string? BillingTag { get; set; }
    public Dictionary<string, int[]>? CustomMeterIds { get; set; }
}

public class PurchaseDurationDiscount
{
    public string? Duration { get; set; }
    public decimal? DiscountPercentage { get; set; }
}

public class SkuProperties
{
    public string? Category { get; set; }
    public string? DataDiskType { get; set; }
    public string? DiskType { get; set; }
    public string? NumberOfCores { get; set; }
    public string? Ram { get; set; }
    public string? VCpu { get; set; }
    public string? ArmSkuName { get; set; }
    public string? AccessTier { get; set; }
}

public class OfferingProperties
{
    public string? ProductCode { get; set; }
    public string? TermId { get; set; }
    public string? MeterType { get; set; }
    public string? BillingMeterId { get; set; }
    public string? OfferingId { get; set; }
    public string? Type { get; set; }
}

public class ProductAttribute
{
    public string? Key { get; set; }
    public string? Value { get; set; }
}

public class SkuAggregatedData
{
    public ICollection<string>? Actions { get; set; }
    public ICollection<string>? States { get; set; }
    public bool? HasEndUserEligibleSKUs { get; set; }
    public bool? HasAdminEligibleSKUs { get; set; }
    public ICollection<string>? Features { get; set; }
    public ICollection<string>? Programs { get; set; }
    public bool? HasConsumptionComponents { get; set; }
}

public class MarketStartPrice
{
    public string? Market { get; set; }
    public string? TermUnits { get; set; }
    public string? MeterUnits { get; set; }
    public decimal? MinTermPrice { get; set; }
    public decimal? MinMeterPrice { get; set; }
    public string? Currency { get; set; }
}

public class PlanMetadata
{
    public string? Generation { get; set; }
    public string? AltStackReference { get; set; }
    public IReadOnlyList<PlanSkuRelation>? RelatedSkus { get; set; }
}

public class Meter
{
    public string? Id { get; set; }
    public string? PartNumber { get; set; }
    public string? ConsumptionResourceId { get; set; }
    public Price? Price { get; set; }
    public string? MeterType { get; set; }
    public IList<IncludedQuantityProperty>? IncludedQuantityProperties { get; set; }
}

public class Term
{
    public List<TermDescriptionParameter>? TermDescriptionParameters { get; set; }
    public string? TermId { get; set; }
    public string? TermUnits { get; set; }
    public ProrationPolicy? ProrationPolicy { get; set; }
    public string? TermDescription { get; set; }
    public Price? Price { get; set; }
    public string? RenewTermId { get; set; }
    public string? RenewTermUnits { get; set; }
    public BillingPlan? BillingPlan { get; set; }
    public string? RenewToTermBillingPlan { get; set; }
    public bool? IsAutorenewable { get; set; }
}

public class InvoicingPolicy
{
    public string? InvoicingCadence { get; set; }
    public IList<FilterInstruction>? FilterInstructions { get; set; }
}

public class Image
{
    public string? Id { get; set; }
    public string? Uri { get; set; }
    public string? ImageType { get; set; }
}

public class PreviewImage
{
    public string? Caption { get; set; }
    public string? Uri { get; set; }
    public string? ImagePurpose { get; set; }
}

public class PlanSkuRelation
{
    public RelatedSku? Sku { get; set; }
    public string? RelationType { get; set; }
}

public class Price
{
    public string? CurrencyCode { get; set; }
    public bool? IsPIRequired { get; set; }
    public decimal? ListPrice { get; set; }
    public decimal? MSRP { get; set; }
}

public class IncludedQuantityProperty
{
    public string? TermId { get; set; }
    public string? Quantity { get; set; }
}

public class TermDescriptionParameter
{
    public string? Parameter { get; set; }
    public string? Value { get; set; }
}

public class ProrationPolicy
{
    public string? MinimumProratedUnits { get; set; }
}

public class BillingPlan
{
    public string? BillingPeriod { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public Price? Price { get; set; }
}

public class FilterInstruction
{
    public string? FilterName { get; set; }
    public string? Operator { get; set; }
    public IList<string>? Values { get; set; }
}

public class RelatedSku
{
    public string? Name { get; set; }
    public string? Generation { get; set; }
    public string? Identity { get; set; }
}

public enum LegalTermsType
{
    None = 0,
    EA = 1
}

public enum AzureBenefit
{
    Eligible,
    NotEligible
}

public enum Badge
{
    PreferredSolution,
    PowerBICertified,
    AdditionalPurchaseRequirement
}

public enum PublisherType
{
    Microsoft,
    ThirdParty
}

public enum PublishingStage
{
    Preview,
    Live
}

public enum ProductType
{
    Unknown,
    VirtualMachine,
    SolutionTemplate,
    ManagedApplication,
    Container,
    SaasApp,
    Office365App,
    PowerBIApp,
    ConsultingService,
    DynamicsOps,
    DynamicsCe,
    DynamicsBc,
    PowerAppsApp,
    PowerAutomateApp,
    AzureService,
    CoreVirtualMachine,
    IotEdgeModule,
    VisualStudioExtension,
    DevService,
    Teams,
    AadApp
}

public enum PricingType
{
    Free,
    FreeTrial,
    Byol,
    Payg,
    RI
}

public enum RatingBucket
{
    Above1,
    Above2,
    Above3,
    Above4
}

public enum StopSellReason
{
    EndOfSupport,
    SecurityIssue,
    Other
}

public enum CspState
{
    OptIn,
    OptOut,
    Disabled
}

public enum VmArchitectureType
{
    X64,
    Arm64
}

public enum VmSecurityType
{
    Standard,
    Trusted,
    Confidential
}

public enum PricingAudience
{
    DirectCustomer,
    Partner,
    Internal
}

public enum ArtifactType
{
    Unknown,
    Template,
    Custom,
    Nested,
    Fragment
}
