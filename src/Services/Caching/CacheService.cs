// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace AzureMcp.Services.Caching;

public class CacheService(IMemoryCache memoryCache) : ICacheService
{
    private readonly IMemoryCache _memoryCache = memoryCache;

    public ValueTask<T?> GetAsync<T>(string key, TimeSpan? expiration = null)
    {
        return _memoryCache.TryGetValue(key, out T? value) ? new ValueTask<T?>(value) : default;
    }

    public ValueTask SetAsync<T>(string key, T data, TimeSpan? expiration = null)
    {
        if (data == null) return default;

        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        _memoryCache.Set(key, data, options);
        return default;
    }

    public ValueTask DeleteAsync(string key)
    {
        _memoryCache.Remove(key);
        return default;
    }
}