// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Redis.CacheForRedis;

public class BaseCacheArguments : SubscriptionArguments
{
    [JsonPropertyName(ArgumentDefinitions.Redis.CacheName)]
    public string? Cache { get; set; }
}
