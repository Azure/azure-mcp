// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Quota.Options;

public static class QuotaOptionDefinitions
{
    public static class QuotaCheck
    {
        public const string RegionName = "region";
        public const string ResourceTypesName = "resource-types";

        public static readonly Option<string> Region = new(
            $"--{RegionName}",
            "The valid Azure region where the resources will be deployed. E.g. 'eastus', 'westus', etc."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> ResourceTypes = new(
            $"--{ResourceTypesName}",
            "The valid Azure resource types that are going to be deployed(comma-separated). E.g. 'Microsoft.App/containerApps,Microsoft.Web/sites,Microsoft.CognitiveServices/accounts', etc."
        )
        {
            IsRequired = true,
            AllowMultipleArgumentsPerToken = true
        };
    }
}
