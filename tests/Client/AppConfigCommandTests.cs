// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Services.Azure.AppConfig;
using AzureMcp.Services.Azure.Subscription;
using AzureMcp.Services.Azure.Tenant;
using AzureMcp.Services.Caching;
using AzureMcp.Tests.Client.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Xunit;

namespace AzureMcp.Tests.Client;

public class AppConfigCommandTests : CommandTestsBase,
    IClassFixture<McpClientFixture>,
    IClassFixture<LiveTestSettingsFixture>
{
    private readonly AppConfigService _appConfigService;

    public AppConfigCommandTests(McpClientFixture mcpClient, LiveTestSettingsFixture liveTestSettings, ITestOutputHelper output) : base(mcpClient, liveTestSettings, output)
    {
        var memoryCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        var cacheService = new CacheService(memoryCache);
        var tenantService = new TenantService(cacheService);
        var subscriptionService = new SubscriptionService(cacheService, tenantService);
        _appConfigService = new AppConfigService(subscriptionService, tenantService);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_appconfig_accounts()
    {
        var result = await CallToolAsync(
            "azmcp-appconfig-account-list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        var accountsArray = result.AssertProperty("accounts");
        Assert.Equal(JsonValueKind.Array, accountsArray.ValueKind);
        Assert.NotEmpty(accountsArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_appconfig_kvs()
    {
        // arrange
        await _appConfigService.SetKeyValue(Settings.ResourceBaseName, "foo", "fo-value", Settings.SubscriptionId);
        await _appConfigService.SetKeyValue(Settings.ResourceBaseName, "bar", "bar-value", Settings.SubscriptionId);

        // act
        var result = await CallToolAsync(
            "azmcp-appconfig-kv-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.ResourceBaseName }
            });

        // assert
        var kvsArray = result.AssertProperty("settings");
        Assert.Equal(JsonValueKind.Array, kvsArray.ValueKind);
        Assert.NotEmpty(kvsArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_appconfig_kvs_with_key_and_label()
    {
        const string key = "foo1";
        await _appConfigService.SetKeyValue(Settings.ResourceBaseName, key, "foo-value", Settings.SubscriptionId, label: "foobar");

        var result = await CallToolAsync(
            "azmcp-appconfig-kv-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.ResourceBaseName },
                { "key", key },
                { "label", "foobar" }
            });

        var kvsArray = result.AssertProperty("settings");
        Assert.Equal(JsonValueKind.Array, kvsArray.ValueKind);
        Assert.NotEmpty(kvsArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_lock_appconfig_kv_with_key_and_label()
    {
        // arrange
        const string key = "foo2";
        // if it exists, unlock it
        try
        {
            await _appConfigService.UnlockKeyValue(Settings.ResourceBaseName, key, Settings.SubscriptionId, label: "staging");
        }
        catch
        {
        }

        // make sure it exists
        await _appConfigService.SetKeyValue(Settings.ResourceBaseName, key, "foo-value", Settings.SubscriptionId, label: "staging");

        var result = await CallToolAsync(
            "azmcp-appconfig-kv-lock",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.ResourceBaseName },
                { "key", key },
                { "label", "staging" }
            });

        await Assert.ThrowsAnyAsync<Exception>(() => _appConfigService.SetKeyValue(Settings.ResourceBaseName, key, "new-value", Settings.SubscriptionId, label: "staging"));
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_lock_appconfig_kv_with_key()
    {
        // arrange
        const string key = "foo3";

        // if it exists, unlock it
        try
        {
            await _appConfigService.UnlockKeyValue(Settings.ResourceBaseName, key, Settings.SubscriptionId);
        }
        catch
        {
        }

        // make sure it exists
        await _appConfigService.SetKeyValue(Settings.ResourceBaseName, key, "foo-value", Settings.SubscriptionId);

        // act
        var result = await CallToolAsync(
            "azmcp-appconfig-kv-lock",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.ResourceBaseName },
                { "key", key }
            });

        // assert
        await Assert.ThrowsAnyAsync<Exception>(() => _appConfigService.SetKeyValue(Settings.ResourceBaseName, key, "new-value", Settings.SubscriptionId));
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_unlock_appconfig_kv_with_key_and_label()
    {
        // arrange
        const string key = "foo4";

        // if it exists, unlock it
        try
        {
            await _appConfigService.UnlockKeyValue(Settings.ResourceBaseName, key, Settings.SubscriptionId, label: "staging");
        }
        catch
        {
        }

        // make sure it exists
        await _appConfigService.SetKeyValue(Settings.ResourceBaseName, key, "foo-value", Settings.SubscriptionId, label: "staging");
        await _appConfigService.LockKeyValue(Settings.ResourceBaseName, key, Settings.SubscriptionId, label: "staging");

        var result = await CallToolAsync(
            "azmcp-appconfig-kv-unlock",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.ResourceBaseName },
                { "key", key },
                { "label", "staging" }
            });

        try
        {
            await _appConfigService.SetKeyValue(Settings.ResourceBaseName, key, "new-value", Settings.SubscriptionId, label: "staging");
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
        const string key = "foo5";
        try
        {
            await _appConfigService.UnlockKeyValue(Settings.ResourceBaseName, key, Settings.SubscriptionId);
        }
        catch
        {
        }

        await _appConfigService.SetKeyValue(Settings.ResourceBaseName, key, "foo-value", Settings.SubscriptionId);
        await _appConfigService.LockKeyValue(Settings.ResourceBaseName, key, Settings.SubscriptionId);

        var result = await CallToolAsync(
            "azmcp-appconfig-kv-unlock",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.ResourceBaseName },
                { "key", key }
            });

        try
        {
            await _appConfigService.SetKeyValue(Settings.ResourceBaseName, key, "new-value", Settings.SubscriptionId);
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
        const string key = "foo6";
        await _appConfigService.SetKeyValue(Settings.ResourceBaseName, key, "foo-value", Settings.SubscriptionId, label: "staging");

        var result = await CallToolAsync(
            "azmcp-appconfig-kv-show",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.ResourceBaseName },
                { "key", key },
                { "label", "staging" }
            });

        var setting = result.AssertProperty("setting");
        Assert.Equal(JsonValueKind.Object, setting.ValueKind);

        var value = setting.AssertProperty("value");
        Assert.Equal(JsonValueKind.String, value.ValueKind);
        Assert.Equal("foo-value", value.GetString());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_set_and_delete_appconfig_kv()
    {
        const string key = "foo7";
        var result = await CallToolAsync(
            "azmcp-appconfig-kv-set",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.ResourceBaseName },
                { "key", key },
                { "value", "funkyfoo" }
            });

        var value = result.AssertProperty("value");
        Assert.Equal("funkyfoo", value.GetString());

        result = await CallToolAsync(
            "azmcp-appconfig-kv-delete",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", Settings.ResourceBaseName },
                { "key", key }
            });

        var keyProperty = result.AssertProperty("key");
        Assert.Equal(key, keyProperty.GetString());
    }
}
