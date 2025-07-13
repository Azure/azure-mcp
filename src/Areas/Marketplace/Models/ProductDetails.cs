// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Marketplace.Models;

public class ProductDetails
{
    /// <summary>
    /// Unique product identifier
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// BigCat product identifier
    /// </summary>
    public string? BigId { get; set; }

    /// <summary>
    /// Legacy product identifier
    /// </summary>
    public string? LegacyId { get; set; }

    /// <summary>
    /// Product language
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// Whether product has standard contract amendments
    /// </summary>
    public bool HasStandardContractAmendments { get; set; }

    /// <summary>
    /// Marketplace offer identifier
    /// </summary>
    public string? OfferId { get; set; }

    /// <summary>
    /// Offer version number
    /// </summary>
    public string? OfferVersion { get; set; }

    /// <summary>
    /// Publisher identifier
    /// </summary>
    public string? PublisherId { get; set; }

    /// <summary>
    /// Publisher display name
    /// </summary>
    public string? PublisherDisplayName { get; set; }

    /// <summary>
    /// Type of marketplace offer
    /// </summary>
    public string? OfferType { get; set; }

    /// <summary>
    /// Product description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Extended product summary
    /// </summary>
    public string? LongSummary { get; set; }

    /// <summary>
    /// Brief product summary
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Product display name
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Product version
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    /// Search keywords
    /// </summary>
    public List<string>? Keywords { get; set; }

    /// <summary>
    /// Product category identifiers
    /// </summary>
    public List<string>? CategoryIds { get; set; }

    /// <summary>
    /// Target industry identifiers
    /// </summary>
    public List<string>? IndustryIds { get; set; }

    /// <summary>
    /// Available markets
    /// </summary>
    public List<string>? Markets { get; set; }

    /// <summary>
    /// Product screenshot URLs
    /// </summary>
    public List<string>? ScreenshotUris { get; set; }

    /// <summary>
    /// Product icon URLs by size
    /// </summary>
    public Dictionary<string, string>? IconFileUris { get; set; }

    /// <summary>
    /// Available pricing plans
    /// </summary>
    public List<PlanDetails>? Plans { get; set; }

    /// <summary>
    /// Product artifacts and downloads
    /// </summary>
    public List<ArtifactDetails>? Artifacts { get; set; }

    /// <summary>
    /// Related links
    /// </summary>
    public List<LinkDetails>? Links { get; set; }

    /// <summary>
    /// Documentation links
    /// </summary>
    public List<LinkDetails>? DocumentLinks { get; set; }

    /// <summary>
    /// Product demo videos
    /// </summary>
    public List<VideoDetails>? Videos { get; set; }

    /// <summary>
    /// Product images grouped by context
    /// </summary>
    public List<ImageGroupDetails>? Images { get; set; }

    /// <summary>
    /// Search and display filters
    /// </summary>
    public List<FilterDetails>? Filters { get; set; }

    /// <summary>
    /// Privacy policy URL
    /// </summary>
    public string? PrivacyPolicyUri { get; set; }

    /// <summary>
    /// Legal terms URL
    /// </summary>
    public string? LegalTermsUri { get; set; }

    /// <summary>
    /// Support contact URL
    /// </summary>
    public string? SupportUri { get; set; }

    /// <summary>
    /// Help documentation URL
    /// </summary>
    public string? HelpLink { get; set; }

    /// <summary>
    /// Pricing information URL
    /// </summary>
    public string? PricingDetailsUri { get; set; }

    /// <summary>
    /// Whether product is in preview
    /// </summary>
    public bool IsPreview { get; set; }

    /// <summary>
    /// Whether product is private
    /// </summary>
    public bool IsPrivate { get; set; }

    /// <summary>
    /// Whether product is published by Microsoft
    /// </summary>
    public bool IsMicrosoftProduct { get; set; }

    /// <summary>
    /// Whether product is from third-party publisher
    /// </summary>
    public bool IsThirdParty { get; set; }

    /// <summary>
    /// Whether product offers free plans
    /// </summary>
    public bool HasFreePlans { get; set; }

    /// <summary>
    /// Whether product offers free trials
    /// </summary>
    public bool HasFreeTrials { get; set; }

    /// <summary>
    /// Whether product offers pay-as-you-go pricing
    /// </summary>
    public bool HasPaygPlans { get; set; }

    /// <summary>
    /// Last modification timestamp
    /// </summary>
    public DateTime? LastModifiedTimeStamp { get; set; }

    /// <summary>
    /// Product creation date
    /// </summary>
    public DateTime? CreationDate { get; set; }

    /// <summary>
    /// Product popularity score
    /// </summary>
    public double? Popularity { get; set; }

    /// <summary>
    /// Additional dynamic properties
    /// </summary>
    public Dictionary<string, object>? AdditionalProperties { get; set; }

    /// <summary>
    /// Custom string properties
    /// </summary>
    public Dictionary<string, string>? Properties { get; set; }
}

/// <summary>
/// Plan details POJO
/// </summary>
public class PlanDetails
{
    /// <summary>
    /// Plan identifier
    /// </summary>
    public string? PlanId { get; set; }

    /// <summary>
    /// Plan display name
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Plan summary
    /// </summary>
    public string? Summary { get; set; }

    /// <summary>
    /// Plan description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Whether plan is private
    /// </summary>
    public bool IsPrivate { get; set; }

    /// <summary>
    /// Whether plan is free
    /// </summary>
    public bool IsFree { get; set; }

    /// <summary>
    /// Additional plan properties
    /// </summary>
    public Dictionary<string, object>? AdditionalProperties { get; set; }
}

/// <summary>
/// Artifact details POJO
/// </summary>
public class ArtifactDetails
{
    /// <summary>
    /// Artifact name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Artifact download URL
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    /// Type of artifact
    /// </summary>
    public string? ArtifactType { get; set; }
}

/// <summary>
/// Link details POJO
/// </summary>
public class LinkDetails
{
    /// <summary>
    /// Link identifier
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Link display name
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Link URL
    /// </summary>
    public string? Uri { get; set; }
}

/// <summary>
/// Video details POJO
/// </summary>
public class VideoDetails
{
    /// <summary>
    /// Video caption
    /// </summary>
    public string? Caption { get; set; }

    /// <summary>
    /// Video URL
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    /// Video format type
    /// </summary>
    public string? VideoType { get; set; }

    /// <summary>
    /// Video thumbnail URL
    /// </summary>
    public string? ThumbnailUri { get; set; }
}

/// <summary>
/// Image group details POJO
/// </summary>
public class ImageGroupDetails
{
    /// <summary>
    /// Image group context
    /// </summary>
    public string? Context { get; set; }

    /// <summary>
    /// Images in this group
    /// </summary>
    public List<ImageDetails>? Items { get; set; }
}

/// <summary>
/// Image details POJO
/// </summary>
public class ImageDetails
{
    /// <summary>
    /// Image identifier
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Image URL
    /// </summary>
    public string? Uri { get; set; }

    /// <summary>
    /// Image format type
    /// </summary>
    public string? ImageType { get; set; }
}

/// <summary>
/// Filter details POJO
/// </summary>
public class FilterDetails
{
    /// <summary>
    /// Filter type
    /// </summary>
    public string? FilterType { get; set; }

    /// <summary>
    /// Filter value
    /// </summary>
    public string? Value { get; set; }
}
