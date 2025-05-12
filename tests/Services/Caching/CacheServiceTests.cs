// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Linq;
using System.Threading.Tasks;
using AzureMcp.Services.Caching;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Xunit;

namespace AzureMcp.Tests.Services.Caching;

public class CacheServiceTests
{
    private readonly ICacheService _cacheService;
    private readonly IMemoryCache _memoryCache;

    public CacheServiceTests()
    {
        _memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        _cacheService = new CacheService(_memoryCache);
    }

    [Fact]
    public async Task SetAndGet_WithoutGroup_ShouldWorkAsExpected()
    {
        // Arrange
        string group = "test-group";
        string key = "test-key";
        string value = "test-value";

        // Act
        await _cacheService.SetAsync(group, key, value);
        var result = await _cacheService.GetAsync<string>(group, key);

        // Assert
        Assert.Equal(value, result);
    }

    [Fact]
    public async Task SetAndGet_WithGroup_ShouldWorkAsExpected()
    {
        // Arrange
        string group = "test-group";
        string key = "test-key";
        string value = "test-value";

        // Act
        await _cacheService.SetAsync(group, key, value);
        var result = await _cacheService.GetAsync<string>(group, key);

        // Assert
        Assert.Equal(value, result);
    }

    [Fact]
    public async Task GetGroupKeysAsync_ShouldReturnKeysInGroup()
    {
        // Arrange
        string group = "test-group";
        string key1 = "test-key1";
        string key2 = "test-key2";
        string value1 = "test-value1";
        string value2 = "test-value2";

        // Act
        await _cacheService.SetAsync(group, key1, value1);
        await _cacheService.SetAsync(group, key2, value2);
        var groupKeys = await _cacheService.GetGroupKeysAsync(group);

        // Assert
        Assert.Equal(2, groupKeys.Count());
        Assert.Contains(key1, groupKeys);
        Assert.Contains(key2, groupKeys);
    }

    [Fact]
    public async Task DeleteAsync_WithGroup_ShouldRemoveKeyFromGroup()
    {
        // Arrange
        string group = "test-group";
        string key1 = "test-key1";
        string key2 = "test-key2";
        string value1 = "test-value1";
        string value2 = "test-value2";

        // Act
        await _cacheService.SetAsync(group, key1, value1);
        await _cacheService.SetAsync(group, key2, value2);
        await _cacheService.DeleteAsync(group, key1);

        var groupKeys = await _cacheService.GetGroupKeysAsync(group);
        var result1 = await _cacheService.GetAsync<string>(group, key1);
        var result2 = await _cacheService.GetAsync<string>(group, key2);

        // Assert
        Assert.Single(groupKeys);
        Assert.Contains(key2, groupKeys);
        Assert.Null(result1);
        Assert.Equal(value2, result2);
    }
}
