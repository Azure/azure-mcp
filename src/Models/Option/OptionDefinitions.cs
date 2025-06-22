// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using AzureMcp.Areas.Server.Options;

namespace AzureMcp.Models.Option;

public static partial class OptionDefinitions
{
    public static class Common
    {
        public const string TenantName = "tenant";
        public const string SubscriptionName = "subscription";
        public const string ResourceGroupName = "resource-group";
        public const string AuthMethodName = "auth-method";

        public static readonly Option<string> Tenant = new(
            $"--{TenantName}",
            "The Azure Active Directory tenant ID or name. This can be either the GUID identifier or the display name of your Azure AD tenant."
        )
        {
            IsRequired = false,
            IsHidden = true
        };

        public static readonly Option<string> Subscription = new(
            $"--{SubscriptionName}",
            "The Azure subscription ID or name. This can be either the GUID identifier or the display name of the Azure subscription to use."
        )
        {
            IsRequired = true
        };

        public static readonly Option<AuthMethod> AuthMethod = new(
            $"--{AuthMethodName}",
            () => Models.AuthMethod.Credential,
            "Authentication method to use. Options: 'credential' (Azure CLI/managed identity), 'key' (access key), or 'connectionString'."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> ResourceGroup = new(
            $"--{ResourceGroupName}",
            "The name of the Azure resource group. This is a logical container for Azure resources."
        )
        {
            IsRequired = true
        };
    }

    public static class RetryPolicy
    {
        public const string DelayName = "retry-delay";
        public const string MaxDelayName = "retry-max-delay";
        public const string MaxRetriesName = "retry-max-retries";
        public const string ModeName = "retry-mode";
        public const string NetworkTimeoutName = "retry-network-timeout";

        public static readonly Option<double> Delay = new(
            $"--{DelayName}",
            () => 2.0,
            "Initial delay in seconds between retry attempts. For exponential backoff, this value is used as the base."
        )
        {
            IsRequired = false,
            IsHidden = true
        };

        public static readonly Option<double> MaxDelay = new(
            $"--{MaxDelayName}",
            () => 10.0,
            "Maximum delay in seconds between retries, regardless of the retry strategy."
        )
        {
            IsRequired = false,
            IsHidden = true
        };

        public static readonly Option<int> MaxRetries = new(
            $"--{MaxRetriesName}",
            () => 3,
            "Maximum number of retry attempts for failed operations before giving up."
        )
        {
            IsRequired = false,
            IsHidden = true
        };

        public static readonly Option<RetryMode> Mode = new(
            $"--{ModeName}",
            () => RetryMode.Exponential,
            "Retry strategy to use. 'fixed' uses consistent delays, 'exponential' increases delay between attempts."
        )
        {
            IsRequired = false,
            IsHidden = true
        };

        public static readonly Option<double> NetworkTimeout = new(
            $"--{NetworkTimeoutName}",
            () => 100.0,
            "Network operation timeout in seconds. Operations taking longer than this will be cancelled."
        )
        {
            IsRequired = false,
            IsHidden = true
        };
    }

    public static class Service
    {
        public const string TransportName = "transport";
        public const string PortName = "port";
        public const string ServiceName = "service";
        public const string ReadOnlyName = "read-only";

        public static readonly Option<string> Transport = new(
            $"--{TransportName}",
            () => TransportTypes.StdIo,
            "Transport mechanism to use for Azure MCP Server."
        )
        {
            IsRequired = false
        };

        public static readonly Option<int> Port = new(
            $"--{PortName}",
            () => 5008,
            "Port to use for Azure MCP Server."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string?> ServiceType = new(
            $"--{ServiceName}",
            () => null,
            "The service to expose on the MCP server."
        )
        {
            IsRequired = false,
        };

        public static readonly Option<bool?> ReadOnly = new(
            $"--{ReadOnlyName}",
            () => null,
            "Whether the MCP server should be read-only. If true, no write operations will be allowed.");
    }

    public static class Authorization
    {
        public const string ScopeName = "scope";

        public static readonly Option<string> Scope = new(
            $"--{ScopeName}",
            "Scope at which the role assignment or definition applies to, e.g., /subscriptions/0b1f6471-1bf0-4dda-aec3-111122223333, /subscriptions/0b1f6471-1bf0-4dda-aec3-111122223333/resourceGroups/myGroup, or /subscriptions/0b1f6471-1bf0-4dda-aec3-111122223333/resourceGroups/myGroup/providers/Microsoft.Compute/virtualMachines/myVM."
        )
        {
            IsRequired = true,
        };
    }

    public static class Marketplace
    {
        public const string ProductIdName = "product-id";
        public const string IncludeStopSoldPlansName = "include-stop-sold-plans";
        public const string LanguageName = "language";
        public const string MarketName = "market";
        public const string LookupOfferInTenantLevelName = "lookup-offer-in-tenant-level";
        public const string IncludeHiddenPlansName = "include-hidden-plans";
        public const string PlanIdName = "plan-id";
        public const string SkuIdName = "sku-id";
        public const string IncludeServiceInstructionTemplatesName = "include-service-instruction-templates";
        public const string PartnerTenantIdName = "partner-tenant-id";
        public const string PricingAudienceName = "pricing-audience";
        public const string ObjectIdName = "object-id";
        public const string AltSecIdName = "alt-sec-id";

        public static readonly Option<string> ProductId = new(
            $"--{ProductIdName}",
            "The ID of the marketplace product to retrieve. This is the unique identifier for the product in the Azure Marketplace."
        )
        {
            IsRequired = true
        };

        public static readonly Option<bool> IncludeStopSoldPlans = new(
            $"--{IncludeStopSoldPlansName}",
            () => false,
            "Include stop-sold or hidden plans in the response."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> Language = new(
            $"--{LanguageName}",
            () => "en",
            "Product language code (e.g., 'en' for English, 'fr' for French)."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> Market = new(
            $"--{MarketName}",
            () => "US",
            "Product market code (e.g., 'US' for United States, 'UK' for United Kingdom)."
        )
        {
            IsRequired = false
        };

        public static readonly Option<bool> LookupOfferInTenantLevel = new(
            $"--{LookupOfferInTenantLevelName}",
            () => false,
            "Check against tenant private audience when retrieving the product."
        )
        {
            IsRequired = false
        };

        public static readonly Option<bool> IncludeHiddenPlans = new(
            $"--{IncludeHiddenPlansName}",
            () => false,
            "Include hidden plans in the response."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> PlanId = new(
            $"--{PlanIdName}",
            "Filter results by a specific plan ID."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> SkuId = new(
            $"--{SkuIdName}",
            "Filter results by a specific SKU ID."
        )
        {
            IsRequired = false
        };

        public static readonly Option<bool> IncludeServiceInstructionTemplates = new(
            $"--{IncludeServiceInstructionTemplatesName}",
            () => false,
            "Include service instruction templates in the response."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> PartnerTenantId = new(
            $"--{PartnerTenantIdName}",
            "Partner tenant ID for the request header."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> PricingAudience = new(
            $"--{PricingAudienceName}",
            "Pricing audience for the request header."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> ObjectId = new(
            $"--{ObjectIdName}",
            "AAD user ID for the request header."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> AltSecId = new(
            $"--{AltSecIdName}",
            "Alternate Security ID (for MSA users) for the request header."
        )
        {
            IsRequired = false
        };
    }
}
