// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;
using AzureMcp.Options;

namespace AzureMcp.Areas.Redis.Options.CacheForRedis;

public class BaseCacheOptions : SubscriptionOptions
{
    [JsonPropertyName(OptionDefinitions.Redis.CacheName)]
    public string? Cache { get; set; }
}
