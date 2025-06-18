// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;
using AzureMcp.Options;

namespace AzureMcp.Areas.Redis.Options.ManagedRedis;

public class BaseClusterOptions : SubscriptionOptions
{
    [JsonPropertyName(OptionDefinitions.Redis.ClusterName)]
    public string? Cluster { get; set; }
}
