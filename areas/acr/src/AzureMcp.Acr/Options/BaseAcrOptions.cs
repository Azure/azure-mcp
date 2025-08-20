// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Acr.Options;

public class BaseAcrOptions : SubscriptionOptions
{
    [JsonPropertyName(AcrOptionDefinitions.RegistryName)]
    public string? Registry { get; set; }
}
