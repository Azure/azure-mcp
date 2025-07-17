// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Options;

namespace AzureMcp.Areas.Deploy.Options;

public sealed class RegionCheckOptions : SubscriptionOptions
{
    [JsonPropertyName("resourceTypes")]
    public string ResourceTypes { get; set; } = string.Empty;

    [JsonPropertyName("modelName")]
    public string? CognitiveServiceModelName { get; set; }

    [JsonPropertyName("modelVersion")]
    public string? CognitiveServiceModelVersion { get; set; }

    [JsonPropertyName("deploymentSkuName")]
    public string? CognitiveServiceDeploymentSkuName { get; set; }
}
