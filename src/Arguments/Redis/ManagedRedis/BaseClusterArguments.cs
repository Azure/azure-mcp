// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Redis.ManagedRedis;

public class BaseClusterArguments : SubscriptionArguments
{
    [JsonPropertyName(ArgumentDefinitions.Redis.ClusterName)]
    public string? Cluster { get; set; }
}
