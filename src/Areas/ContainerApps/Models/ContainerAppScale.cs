// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.ContainerApps.Models;

public class ContainerAppScale
{
    [JsonPropertyName("minReplicas")]
    public int? MinReplicas { get; set; }

    [JsonPropertyName("maxReplicas")]
    public int? MaxReplicas { get; set; }
}
