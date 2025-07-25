// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.Kusto.Options;

public class BaseClusterOptions : SubscriptionOptions
{
    [JsonPropertyName(KustoOptionDefinitions.ClusterName)]
    public string? ClusterName { get; set; }

    [JsonPropertyName(KustoOptionDefinitions.ClusterUriName)]
    public string? ClusterUri { get; set; }
}
