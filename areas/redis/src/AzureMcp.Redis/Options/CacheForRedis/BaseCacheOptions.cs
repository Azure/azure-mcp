// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.Redis.Options.CacheForRedis;

public class BaseCacheOptions : SubscriptionOptions
{
    [JsonPropertyName(RedisOptionDefinitions.CacheName)]
    public string? Cache { get; set; }
}
