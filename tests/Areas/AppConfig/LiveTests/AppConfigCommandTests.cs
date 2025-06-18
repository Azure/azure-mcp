// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Areas.AppConfig.Services;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;
using AzureMcp.Services.Caching;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace AzureMcp.Tests.Areas.AppConfig.LiveTests;

public class AppConfigCommandTests : CommandTestsBase,
    IClassFixture<LiveTestFixture>
{
    private const string AccountsKey = "accounts";
    private const string SettingsKey = "settings";
    private readonly AppConfigService _appConfigService;
    private readonly string _subscriptionId;
    private readonly string _accountName;

    public AppConfigCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output) : base(liveTestFixture, output)
    {
        var memoryCache = new MemoryCache(Microsoft.Extensions.Options.Options.Create(new MemoryCacheOptions()));
        var cacheService = new CacheService(memoryCache);
        var tenantService = new TenantService(cacheService);
        var subscriptionService = new SubscriptionService(cacheService, tenantService);
        _appConfigService = new AppConfigService(subscriptionService, tenantService);
        _subscriptionId = Settings.SubscriptionId;
        _accountName = Settings.ResourceBaseName;
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_appconfig_accounts()
    {
        // act
        var result = await CallToolAsync(
            "azmcp-appconfig-account-list",
            new()
            {
                { "subscription", _subscriptionId }
            });

        // assert
        var accountsArray = result.AssertProperty(AccountsKey);
        Assert.Equal(JsonValueKind.Array, accountsArray.ValueKind);
        Assert.NotEmpty(accountsArray.EnumerateArray());
        Assert.Contains(accountsArray.EnumerateArray(), acc => acc.GetProperty("name").GetString() == _accountName);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_appconfig_kvs()
    {
        // arrange
        const string key0 = "foo";
        const string value0 = "fo-value";
        const string key1 = "bar";
        const string value1 = "bar-value";

        await _appConfigService.SetKeyValue(_accountName, key0, value0, _subscriptionId);
        await _appConfigService.SetKeyValue(_accountName, key1, value1, _subscriptionId);

        // act
        var result = await CallToolAsync(
            "azmcp-appconfig-kv-list",
            new()
            {
                { "subscription", _subscriptionId },
                { "account-name", _accountName }
            });

        // assert
        var kvsArray = result.AssertProperty(SettingsKey);
        Assert.Equal(JsonValueKind.Array, kvsArray.ValueKind);
        Assert.NotEmpty(kvsArray.EnumerateArray());

        var foo = kvsArray.EnumerateArray().FirstOrDefault(kv => kv.GetProperty("key").GetString() == key0);
        var bar = kvsArray.EnumerateArray().FirstOrDefault(kv => kv.GetProperty("key").GetString() == key1);
        Assert.Equal(JsonValueKind.Object, foo.ValueKind);
        Assert.Equal(value0, foo.GetProperty("value").GetString());
        Assert.Equal(JsonValueKind.Object, bar.ValueKind);
        Assert.Equal(value1, bar.GetProperty("value").GetString());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_appconfig_kvs_with_key_and_label()
    {
        // arrange
        const string key = "foo1";
        const string value = "foo-value";
        const string label = "foobar";
        await _appConfigService.SetKeyValue(_accountName, key, value, _subscriptionId, label: label);

        // act
        var result = await CallToolAsync(
            "azmcp-appconfig-kv-list",
            new()
            {
                { "subscription", _subscriptionId },
                { "account-name", _accountName },
                { "key", key },
                { "label", label }
            });

        // assert
        var kvsArray = result.AssertProperty(SettingsKey);
        Assert.Equal(JsonValueKind.Array, kvsArray.ValueKind);
        Assert.NotEmpty(kvsArray.EnumerateArray());

        var found = kvsArray.EnumerateArray().FirstOrDefault(kv => kv.GetProperty("key").GetString() == key && kv.GetProperty("label").GetString() == label);
        Assert.Equal(JsonValueKind.Object, found.ValueKind);
        Assert.Equal(value, found.GetProperty("value").GetString());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_lock_appconfig_kv_with_key_and_label()
    {
        // arrange
        const string key = "foo2";
        const string value = "foo-value";
        const string label = "staging";
        const string newValue = "new-value";
        try
        {
            // if it exists, unlock it
            await _appConfigService.UnlockKeyValue(_accountName, key, _subscriptionId, label: label);
        }
        catch
        {
        }
        // make sure it exists
        await _appConfigService.SetKeyValue(_accountName, key, value, _subscriptionId, label: label);

        // act
        var result = await CallToolAsync(
            "azmcp-appconfig-kv-lock",
            new()
            {
                { "subscription", _subscriptionId },
                { "account-name", _accountName },
                { "key", key },
                { "label", label }
            });

        // assert
        await Assert.ThrowsAnyAsync<Exception>(() => _appConfigService.SetKeyValue(_accountName, key, newValue, _subscriptionId, label: label));
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_lock_appconfig_kv_with_key()
    {
        // arrange
        const string key = "foo3";
        const string value = "foo-value";
        const string newValue = "new-value";
        try
        {
            // if it exists, unlock it
            await _appConfigService.UnlockKeyValue(_accountName, key, _subscriptionId);
        }
        catch
        {
        }
        // make sure it exists
        await _appConfigService.SetKeyValue(_accountName, key, value, _subscriptionId);

        // act
        var result = await CallToolAsync(
            "azmcp-appconfig-kv-lock",
            new()
            {
                { "subscription", _subscriptionId },
                { "account-name", _accountName },
                { "key", key }
            });

        // assert
        await Assert.ThrowsAnyAsync<Exception>(() => _appConfigService.SetKeyValue(_accountName, key, newValue, _subscriptionId));
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_unlock_appconfig_kv_with_key_and_label()
    {
        // arrange
        const string key = "foo4";
        const string value = "foo-value";
        const string label = "staging";
        const string newValue = "new-value";
        try
        {
            // if it exists, unlock it
            await _appConfigService.UnlockKeyValue(_accountName, key, _subscriptionId, label: label);
        }
        catch
        {
        }
        // make sure it exists
        await _appConfigService.SetKeyValue(_accountName, key, value, _subscriptionId, label: label);
        await _appConfigService.LockKeyValue(_accountName, key, _subscriptionId, label: label);

        // act
        _ = await CallToolAsync(
            "azmcp-appconfig-kv-unlock",
            new()
            {
                { "subscription", _subscriptionId },
                { "account-name", _accountName },
                { "key", key },
                { "label", "staging" }
            });

        // assert
        try
        {
            await _appConfigService.SetKeyValue(_accountName, key, newValue, _subscriptionId, label: label);
        }
        catch (Exception ex)
        {
            Assert.Fail($"Failed to set value after unlock: {ex.Message}");
        }
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_unlock_appconfig_kv_with_key()
    {
        // arrange
        const string key = "foo5";
        const string value = "foo-value";
        const string newValue = "new-value";
        try
        {
            // if it exists, unlock it
            await _appConfigService.UnlockKeyValue(_accountName, key, _subscriptionId);
        }
        catch
        {
        }
        // make sure it exists
        await _appConfigService.SetKeyValue(_accountName, key, value, _subscriptionId);
        await _appConfigService.LockKeyValue(_accountName, key, _subscriptionId);

        // act
        _ = await CallToolAsync(
            "azmcp-appconfig-kv-unlock",
            new()
            {
                { "subscription", _subscriptionId },
                { "account-name", _accountName },
                { "key", key }
            });

        // assert
        try
        {
            await _appConfigService.SetKeyValue(_accountName, key, newValue, _subscriptionId);
        }
        catch (Exception ex)
        {
            Assert.Fail($"Failed to set value after unlock: {ex.Message}");
        }
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_show_appconfig_kv()
    {
        // arrange
        const string key = "foo6";
        const string value = "foo-value";
        const string label = "staging";
        await _appConfigService.SetKeyValue(_accountName, key, value, _subscriptionId, label: label);

        // act
        var result = await CallToolAsync(
            "azmcp-appconfig-kv-show",
            new()
            {
                { "subscription", _subscriptionId },
                { "account-name", _accountName },
                { "key", key },
                { "label", label }
            });

        // assert
        var setting = result.AssertProperty("setting");
        Assert.Equal(JsonValueKind.Object, setting.ValueKind);
        var valueRead = setting.AssertProperty("value");
        Assert.Equal(JsonValueKind.String, valueRead.ValueKind);
        Assert.Equal(value, valueRead.GetString());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_set_and_delete_appconfig_kv()
    {
        // arrange
        const string key = "foo7";
        const string value = "funkyfoo";

        // act and assert
        var result = await CallToolAsync(
            "azmcp-appconfig-kv-set",
            new()
            {
                { "subscription", _subscriptionId },
                { "account-name", _accountName },
                { "key", key },
                { "value", value }
            });

        var valueRead = result.AssertProperty("value");
        Assert.Equal(value, valueRead.GetString());

        result = await CallToolAsync(
            "azmcp-appconfig-kv-delete",
            new()
            {
                { "subscription", _subscriptionId },
                { "account-name", _accountName },
                { "key", key }
            });

        var keyProperty = result.AssertProperty("key");
        Assert.Equal(key, keyProperty.GetString());
    }
}
