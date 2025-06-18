// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Redis.CacheForRedis;

public class BaseCacheOptions : SubscriptionOptions
{
    [JsonPropertyName(OptionDefinitions.Redis.CacheName)]
    public string? Cache { get; set; }
}
