// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Options;

namespace AzureMcp.Areas.Quota.Options;

public sealed class UsageCheckOptions : SubscriptionOptions
{
    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;

    [JsonPropertyName("resourceTypes")]
    public string ResourceTypes { get; set; } = string.Empty;
}
