// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Options;

namespace AzureMcp.Areas.TerraformSchema.Options;

public class BaseTerraformSchemaOptions : SubscriptionOptions
{
    [JsonPropertyName(TerraformSchemaOptionDefinitions.ResourceType)]
    public string? ResourceType { get; set; }

    [JsonPropertyName(TerraformSchemaOptionDefinitions.Provider)]
    public string? ProviderName { get; set; }

    [JsonPropertyName(TerraformSchemaOptionDefinitions.UserDirectory)]
    public string? UserDirectory { get; set; }
}
