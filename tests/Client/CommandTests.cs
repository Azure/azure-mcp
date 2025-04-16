// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Tests.Client.Helpers;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace AzureMcp.Tests.Client;

public class CommandTests(McpClientFixture mcpClient, LiveTestSettingsFixture liveTestSettings, ITestOutputHelper output)
    : CommandTestsBase(mcpClient, liveTestSettings, output),
    IClassFixture<McpClientFixture>, IClassFixture<LiveTestSettingsFixture>
{
    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_storage_accounts_by_subscription_id()
    {
        var result = await CallToolAsync(
            "azmcp-storage-account-list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        Assert.True(result.TryGetProperty("accounts", out var accounts));
        Assert.Equal(JsonValueKind.Array, accounts.ValueKind);
        Assert.NotEmpty(accounts.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_storage_accounts_by_subscription_name()
    {
        var result = await CallToolAsync(
            "azmcp-storage-account-list",
            new()
            {
                { "subscription", Settings.SubscriptionName }
            });

        Assert.True(result.TryGetProperty("accounts", out var accounts));
        Assert.Equal(JsonValueKind.Array, accounts.ValueKind);
        Assert.NotEmpty(accounts.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_storage_accounts_by_subscription_name_with_tenant()
    {
        var result = await CallToolAsync(
            "azmcp-storage-account-list",
            new()
            {
                { "subscription", Settings.SubscriptionName },
                { "tenant", Settings.TenantId }
            });

        Assert.True(result.TryGetProperty("accounts", out var accounts));
        Assert.Equal(JsonValueKind.Array, accounts.ValueKind);
        Assert.NotEmpty(accounts.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_storage_accounts_by_subscription_name_with_tenant_name()
    {
        var result = await CallToolAsync(
            "azmcp-storage-account-list",
            new()
            {
                { "subscription", Settings.SubscriptionName },
                { "tenant", Settings.TenantName }
            });

        Assert.True(result.TryGetProperty("accounts", out var accounts));
        Assert.Equal(JsonValueKind.Array, accounts.ValueKind);
        Assert.NotEmpty(accounts.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_groups_by_subscription()
    {
        var result = await CallToolAsync(
            "azmcp-group-list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        Assert.True(result.TryGetProperty("groups", out var groupsArray));
        Assert.Equal(JsonValueKind.Array, groupsArray.ValueKind);
        Assert.NotEmpty(groupsArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_subscriptions()
    {
        var result = await CallToolAsync(
            "azmcp-subscription-list",
            new Dictionary<string, object?>());

        Assert.True(result.TryGetProperty("subscriptions", out var subscriptionsArray));
        Assert.Equal(JsonValueKind.Array, subscriptionsArray.ValueKind);
        Assert.NotEmpty(subscriptionsArray.EnumerateArray());
    }
}