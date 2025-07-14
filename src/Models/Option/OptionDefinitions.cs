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
            "The Microsoft Entra ID tenant ID or name. This can be either the GUID identifier or the display name of your Entra ID tenant."
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
        public const string NamespaceName = "namespace";
        public const string ModeName = "mode";
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

        public static readonly Option<string[]?> Namespace = new(
            $"--{NamespaceName}",
            () => null,
            "Azure service areas to expose (e.g., storage, keyvault, cosmos, monitor). " +
            "Limits MCP tools to only the specified Azure services. " +
            "If not specified, all available Azure services will be exposed."
        )
        {
            IsRequired = false,
            Arity = ArgumentArity.OneOrMore,
            AllowMultipleArgumentsPerToken = true
        };

        public static readonly Option<string?> Mode = new Option<string?>(
            $"--{ModeName}",
            () => null,
            "How to organize Azure MCP tools:\n" +
            "  • 'single': Single unified Azure tool for all operations\n" +
            "  • 'namespace': One tool per Azure service area (storage, keyvault, etc.)\n" +
            "  • Default: Individual tools for each specific operation"
        )
        {
            IsRequired = false
        };

        public static readonly Option<bool?> ReadOnly = new(
            $"--{ReadOnlyName}",
            () => null,
            "Enable read-only mode to restrict MCP server to safe operations. " +
            "When enabled, only read operations (list, get, query) are available. " +
            "Write operations (create, update, delete) are excluded for safety.");
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
}
