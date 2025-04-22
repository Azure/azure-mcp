// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Tests.Client.Helpers;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace AzureMcp.Tests.Client
{
    public class StorageCommandTests(McpClientFixture mcpClient, LiveTestSettingsFixture liveTestSettings, ITestOutputHelper output)
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
        public async Task Should_list_storage_accounts_by_subscription_name_with_tenant_id()
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
        public async Task Should_list_blobs_in_container()
        {
            var result = await CallToolAsync(
                "azmcp-storage-blob-list",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "tenant", Settings.TenantName },
                { "account-name", Settings.StorageAccountName },
                { "container-name", Settings.StorageContainerName },
                });

            Assert.True(result.TryGetProperty("blobs", out var actual));
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
            Assert.NotEmpty(actual.EnumerateArray());
        }

        [Fact]
        [Trait("Category", "Live")]
        public async Task Should_list_containers()
        {
            var result = await CallToolAsync(
                "azmcp-storage-blob-container-list",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "tenant", Settings.TenantName },
                { "account-name", Settings.StorageAccountName },
                { "retry-max-retries", 0 }
                });

            Assert.True(result.TryGetProperty("containers", out var actual));
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
            Assert.NotEmpty(actual.EnumerateArray());
        }

        [Fact]
        [Trait("Category", "Live")]
        public async Task Should_list_storage_tables()
        {
            var result = await CallToolAsync(
                "azmcp-storage-table-list",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "tenant", Settings.TenantName },
                { "account-name", Settings.StorageAccountName },
                });

            Assert.True(result.TryGetProperty("tables", out var actual));
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
            Assert.NotEmpty(actual.EnumerateArray());
        }

        [Fact]
        [Trait("Category", "Live")]
        public async Task Should_list_storage_tables_with_tenant()
        {
            var result = await CallToolAsync(
                "azmcp-storage-table-list",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "tenant", Settings.TenantName },
                { "account-name", Settings.StorageAccountName },
                });

            Assert.True(result.TryGetProperty("tables", out var actual));
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
            Assert.NotEmpty(actual.EnumerateArray());
        }

        [Fact]
        [Trait("Category", "Live")]
        public async Task Should_get_container_details()
        {
            var result = await CallToolAsync(
                "azmcp-storage-blob-container-details",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "account-name", Settings.StorageAccountName },
                { "container-name", Settings.StorageContainerName }
                });

            Assert.True(result.TryGetProperty("details", out var actual));
            Assert.Equal(JsonValueKind.Object, actual.ValueKind);
        }

        [Fact]
        [Trait("Category", "Live")]
        public async Task Should_get_container_details_with_tenant_authkey()
        {
            var result = await CallToolAsync(
                "azmcp-storage-blob-container-details",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "account-name", Settings.StorageAccountName },
                { "container-name", Settings.StorageContainerName },
                { "auth-method", "key" }
                });

            Assert.True(result.TryGetProperty("details", out var actual));
            Assert.Equal(JsonValueKind.Object, actual.ValueKind);
        }
    }
}