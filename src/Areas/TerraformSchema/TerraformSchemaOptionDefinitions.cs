// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.TerraformSchema;
public static class TerraformSchemaOptionDefinitions
{
    public const string ResourceType = "resource-type";
    public const string Provider = "provider-name";
    public const string UserDirectory = "user-directory";

    public static readonly Option<string> ResourceTypeName = new(
        $"--{ResourceType}",
        "The name of the Terraform Resource Type (e.g., azurerm_api_management_redis_cache)."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> ProviderName = new(
        $"--{Provider}",
        "The name of the Terraform Provider (e.g., registry.terraform.io/hashicorp/azurerm)."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> UserDirectoryPath = new(
        $"--{UserDirectory}",
        "The path to the user's current working directory (e.g., C:/terraform/)."
    )
    {
        IsRequired = true
    };
}

