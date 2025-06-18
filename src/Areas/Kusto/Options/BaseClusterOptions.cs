// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Kusto;

public class BaseClusterOptions : SubscriptionOptions
{
    [JsonPropertyName(OptionDefinitions.Kusto.ClusterName)]
    public string? ClusterName { get; set; }

    [JsonPropertyName(OptionDefinitions.Kusto.ClusterUriName)]
    public string? ClusterUri { get; set; }
}
