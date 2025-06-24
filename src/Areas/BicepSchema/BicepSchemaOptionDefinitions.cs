// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.BicepSchema;

public static class BicepSchemaOptionDefinitions
{
    public const string ResourceType = "resource-type";

    public static readonly Option<string> ResourceTypeName = new(
        $"--{ResourceType}",
        "The name of the Bicep Resource Type (e.g., Microsoft.Storage/storageAccounts)."
    )
    {
        IsRequired = true
    };
}
