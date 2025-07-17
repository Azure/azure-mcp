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

    public static class LoadTesting
    {
        public const string TestResourceName = "test-resource-name";
        public const string TestRunId = "testrun-id";
        public const string TestId = "test-id";
        public const string DisplayNameOption = "display-name";
        public const string DescriptionOption = "description";
        public const string OldTestRunIdOption = "old-testrun-id";
        public const string VirtualUsersOption = "virtual-users";
        public const string DurationOption = "duration";
        public const string RampUpTimeOption = "ramp-up-time";
        public const string EndpointOption = "endpoint";
        public static readonly Option<string> TestResource = new(
            $"--{TestResourceName}",
            "The name of the load test resource for which you want to fetch the details."
        )
        {
            IsRequired = false
        };
        public static readonly Option<string> TestRun = new(
            $"--{TestRunId}",
            "The ID of the load test run for which you want to fetch the details."
        )
        {
            IsRequired = false
        };
        public static readonly Option<string> Test = new(
            $"--{TestId}",
            "The ID of the load test for which you want to fetch the details."
        )
        {
            IsRequired = true
        };
        public static readonly Option<string> DisplayName = new(
            $"--{DisplayNameOption}",
            "The display name for the load test run. This is a user-friendly name to identify the test run."
        )
        {
            IsRequired = false
        };
        public static readonly Option<string> Description = new(
            $"--{DescriptionOption}",
            "The description for the load test run. This provides additional context about the test run."
        )
        {
            IsRequired = false
        };
        public static readonly Option<string> OldTestRunId = new(
            $"--{OldTestRunIdOption}",
            "The ID of an existing test run to update. If provided, the command will trigger a rerun of the given test run id."
        )
        {
            IsRequired = false
        };
        public static readonly Option<int> VirtualUsers = new(
            $"--{VirtualUsersOption}",
            () => 50,
            "Virtual users is a measure of load that is simulated to test the HTTP endpoint. (Default - 50)"
        )
        {
            IsRequired = false
        };
        public static readonly Option<int> Duration = new(
            $"--{DurationOption}",
            () => 20,
            "This is the duration for which the load is simulated against the endpoint. Enter decimals for fractional minutes (e.g., 1.5 for 1 minute and 30 seconds). Default is 20 mins"
        )
        {
            IsRequired = false
        };
        public static readonly Option<int> RampUpTime = new(
            $"--{RampUpTimeOption}",
            () => 1,
            "The ramp-up time is the time it takes for the system to ramp-up to the total load specified. Enter decimals for fractional minutes (e.g., 1.5 for 1 minute and 30 seconds). Default is 1 min"
        )
        {
            IsRequired = false
        };
        public static readonly Option<string> Endpoint = new(
            $"--{EndpointOption}",
            "The endpoint URL to be tested. This is the URL of the HTTP endpoint that will be subjected to load testing."
        )
        {
            IsRequired = false
        };
    }
}
