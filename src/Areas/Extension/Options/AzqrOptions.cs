// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Options;

namespace AzureMcp.Areas.Extension.Options;

public class AzqrOptions : GlobalOptions
{
    [JsonPropertyName(ExtensionOptionDefinitions.Azqr.SubscriptionIdName)]
    public string SubscriptionId { get; set; } = string.Empty;

    [JsonPropertyName(ExtensionOptionDefinitions.Azqr.ResourceGroupIdName)]
    public string? ResourceGroupName { get; set; }
}
