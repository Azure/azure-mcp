// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Areas.Storage.LiveTests
{
    public class StorageCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output), IClassFixture<LiveTestFixture>
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

            var accounts = result.AssertProperty("accounts");
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

            var accounts = result.AssertProperty("accounts");
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

            var accounts = result.AssertProperty("accounts");
            Assert.Equal(JsonValueKind.Array, accounts.ValueKind);
            Assert.NotEmpty(accounts.EnumerateArray());
        }

        [Fact()]
        [Trait("Category", "Live")]
        public async Task Should_list_storage_accounts_by_subscription_name_with_tenant_name()
        {
            Assert.SkipWhen(Settings.IsServicePrincipal, TenantNameReason);

            var result = await CallToolAsync(
                "azmcp-storage-account-list",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "tenant", Settings.TenantName }
                });

            var accounts = result.AssertProperty("accounts");
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
                { "tenant", Settings.TenantId },
                { "account-name", Settings.ResourceBaseName },
                { "container-name", "bar" },
                });

            var actual = result.AssertProperty("blobs");
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
                { "tenant", Settings.TenantId },
                { "account-name", Settings.ResourceBaseName },
                { "retry-max-retries", 0 }
                });

            var actual = result.AssertProperty("containers");
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
                { "tenant", Settings.TenantId },
                { "account-name", Settings.ResourceBaseName },
                });

            var actual = result.AssertProperty("tables");
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
            Assert.NotEmpty(actual.EnumerateArray());
        }

        [Fact]
        [Trait("Category", "Live")]
        public async Task Should_list_storage_tables_with_tenant_id()
        {
            var result = await CallToolAsync(
                "azmcp-storage-table-list",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "tenant", Settings.TenantId },
                { "account-name", Settings.ResourceBaseName },
                });

            var actual = result.AssertProperty("tables");
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
            Assert.NotEmpty(actual.EnumerateArray());
        }

        [Fact()]
        [Trait("Category", "Live")]
        public async Task Should_list_storage_tables_with_tenant_name()
        {
            Assert.SkipWhen(Settings.IsServicePrincipal, TenantNameReason);

            var result = await CallToolAsync(
                "azmcp-storage-table-list",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "tenant", Settings.TenantName },
                { "account-name", Settings.ResourceBaseName },
                });

            var actual = result.AssertProperty("tables");
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
                { "account-name", Settings.ResourceBaseName },
                { "container-name", "bar" }
                });

            var actual = result.AssertProperty("details");
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
                { "account-name", Settings.ResourceBaseName },
                { "container-name", "bar" },
                { "auth-method", "key" }
                });

            var actual = result.AssertProperty("details");
            Assert.Equal(JsonValueKind.Object, actual.ValueKind);
        }
    }
}
