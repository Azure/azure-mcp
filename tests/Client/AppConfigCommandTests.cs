// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Tests.Client.Helpers;
using System.Text.Json;
using Xunit;

namespace AzureMcp.Tests.Client;

public class AppConfigCommandTests(McpClientFixture mcpClient, LiveTestSettingsFixture liveTestSettings, ITestOutputHelper output)
    : CommandTestsBase(mcpClient, liveTestSettings, output),
    IClassFixture<McpClientFixture>, IClassFixture<LiveTestSettingsFixture>
{
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

        Assert.True(result.TryGetProperty("accounts", out var accountsArray));
        Assert.Equal(JsonValueKind.Array, accountsArray.ValueKind);
        Assert.NotEmpty(accountsArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_appconfig_kvs()
    {
        var result = await CallToolAsync(
            "azmcp-appconfig-kv-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", $"{Settings.ResourceBaseName}-appconfig" }
            });

        Assert.True(result.TryGetProperty("settings", out var kvsArray));
        Assert.Equal(JsonValueKind.Array, kvsArray.ValueKind);
        Assert.NotEmpty(kvsArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_appconfig_kvs_with_key_and_label()
    {
        var result = await CallToolAsync(
            "azmcp-appconfig-kv-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", $"{Settings.ResourceBaseName}-appconfig" },
                { "key", "foo2" },
                { "label", "foobar" }
            });

        Assert.True(result.TryGetProperty("settings", out var kvsArray));
        Assert.Equal(JsonValueKind.Array, kvsArray.ValueKind);
        Assert.NotEmpty(kvsArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_lock_appconfig_kv_with_key_and_label()
    {
        var result = await CallToolAsync(
            "azmcp-appconfig-kv-lock",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", $"{Settings.ResourceBaseName}-appconfig" },
                { "key", "foo2" },
                { "label", "foobar" }
            });

        Assert.True(result.TryGetProperty("key", out var key));
        Assert.Equal("foo2", key.GetString());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_lock_appconfig_kv_with_key()
    {
        var result = await CallToolAsync(
            "azmcp-appconfig-kv-lock",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", $"{Settings.ResourceBaseName}-appconfig" },
                { "key", "foo" }
            });

        Assert.True(result.TryGetProperty("key", out var key));
        Assert.Equal("foo", key.GetString());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_unlock_appconfig_kv_with_key_and_label()
    {
        var result = await CallToolAsync(
            "azmcp-appconfig-kv-unlock",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", $"{Settings.ResourceBaseName}-appconfig" },
                { "key", "foo2" },
                { "label", "foobar" }
            });

        Assert.True(result.TryGetProperty("key", out var key));
        Assert.Equal("foo2", key.GetString());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_unlock_appconfig_kv_with_key()
    {
        var result = await CallToolAsync(
            "azmcp-appconfig-kv-unlock",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", $"{Settings.ResourceBaseName}-appconfig" },
                { "key", "foo" }
            });

        Assert.True(result.TryGetProperty("key", out var key));
        Assert.Equal("foo", key.GetString());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_show_appconfig_kv()
    {
        var result = await CallToolAsync(
            "azmcp-appconfig-kv-show",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", $"{Settings.ResourceBaseName}-appconfig" },
                { "key", "foo2" },
                { "label", "foobar" }
            });

        Assert.True(result.TryGetProperty("setting", out var setting));
        Assert.Equal(JsonValueKind.Object, setting.ValueKind);

        Assert.True(setting.TryGetProperty("Value", out var value));
        Assert.Equal(JsonValueKind.String, value.ValueKind);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_set_and_delete_appconfig_kv()
    {
        var result = await CallToolAsync(
            "azmcp-appconfig-kv-set",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", $"{Settings.ResourceBaseName}-appconfig" },
                { "key", "foo3" },
                { "value", "funkyfoo" }
            });

        Assert.True(result.TryGetProperty("value", out var value));
        Assert.Equal("funkyfoo", value.GetString());

        result = await CallToolAsync(
            "azmcp-appconfig-kv-delete",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "account-name", $"{Settings.ResourceBaseName}-appconfig" },
                { "key", "foo3" }
            });

        Assert.True(result.TryGetProperty("key", out var key));
        Assert.Equal("foo3", key.GetString());
    }
}