// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;
using AzureMcp.Options;

namespace AzureMcp.Areas.Kusto.Options;

public class BaseClusterOptions : SubscriptionOptions
{
    [JsonPropertyName(OptionDefinitions.Kusto.ClusterName)]
    public string? ClusterName { get; set; }

    [JsonPropertyName(OptionDefinitions.Kusto.ClusterUriName)]
    public string? ClusterUri { get; set; }
}
