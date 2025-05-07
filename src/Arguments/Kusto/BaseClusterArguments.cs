// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Kusto;

public class BaseClusterArguments : SubscriptionArguments
{
    [JsonPropertyName(ArgumentDefinitions.Kusto.ClusterName)]
    public string? ClusterName { get; set; }

    [JsonPropertyName(ArgumentDefinitions.Kusto.ClusterUriName)]
    public string? ClusterUri { get; set; }
}
