// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Services.Interfaces;

public interface ICacheService
{
    ValueTask<T?> GetAsync<T>(string key, TimeSpan? expiration = null);
    ValueTask SetAsync<T>(string key, T data, TimeSpan? expiration = null);
    ValueTask DeleteAsync(string key);
}