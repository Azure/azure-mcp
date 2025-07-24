// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Aks.Options;

namespace AzureMcp.Areas.Aks.Options.Cluster;

public class ClusterGetOptions : BaseAksOptions
{
    [JsonPropertyName(AksOptionDefinitions.ClusterName)]
    public string? ClusterName { get; set; }
}
