// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Redis.ManagedRedis;

public class BaseClusterOptions : SubscriptionOptions
{
    [JsonPropertyName(OptionDefinitions.Redis.ClusterName)]
    public string? Cluster { get; set; }
}
