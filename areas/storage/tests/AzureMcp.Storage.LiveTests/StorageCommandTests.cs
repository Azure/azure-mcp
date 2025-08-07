// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Storage.LiveTests
{
    public class StorageCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output), IClassFixture<LiveTestFixture>
    {
        [Fact]
        public async Task Should_list_storage_accounts_by_subscription_id()
        {
            var result = await CallToolAsync(
                "azmcp_storage_account_list",
                new()
                {
                { "subscription", Settings.SubscriptionId }
                });

            var accounts = result.AssertProperty("accounts");
            Assert.Equal(JsonValueKind.Array, accounts.ValueKind);
            Assert.NotEmpty(accounts.EnumerateArray());
        }

        [Fact]
        public async Task Should_list_storage_accounts_by_subscription_name()
        {
            var result = await CallToolAsync(
                "azmcp_storage_account_list",
                new()
                {
                { "subscription", Settings.SubscriptionName }
                });

            var accounts = result.AssertProperty("accounts");
            Assert.Equal(JsonValueKind.Array, accounts.ValueKind);
            Assert.NotEmpty(accounts.EnumerateArray());
        }

        [Fact]
        public async Task Should_list_storage_accounts_by_subscription_name_with_tenant_id()
        {
            var result = await CallToolAsync(
                "azmcp_storage_account_list",
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
        public async Task Should_list_storage_accounts_by_subscription_name_with_tenant_name()
        {
            Assert.SkipWhen(Settings.IsServicePrincipal, TenantNameReason);

            var result = await CallToolAsync(
                "azmcp_storage_account_list",
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
        public async Task Should_list_blobs_in_container()
        {
            var result = await CallToolAsync(
                "azmcp_storage_blob_list",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "tenant", Settings.TenantId },
                { "account", Settings.ResourceBaseName },
                { "container", "bar" },
                });

            var actual = result.AssertProperty("blobs");
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
            Assert.NotEmpty(actual.EnumerateArray());
        }

        [Fact]
        public async Task Should_list_containers()
        {
            var result = await CallToolAsync(
                "azmcp_storage_blob_container_list",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "tenant", Settings.TenantId },
                { "account", Settings.ResourceBaseName },
                { "retry-max-retries", 0 }
                });

            var actual = result.AssertProperty("containers");
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
            Assert.NotEmpty(actual.EnumerateArray());
        }

        [Fact]
        public async Task Should_list_storage_tables()
        {
            var result = await CallToolAsync(
                "azmcp_storage_table_list",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "tenant", Settings.TenantId },
                { "account", Settings.ResourceBaseName },
                });

            var actual = result.AssertProperty("tables");
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
            Assert.NotEmpty(actual.EnumerateArray());
        }

        [Fact]
        public async Task Should_list_storage_tables_with_tenant_id()
        {
            var result = await CallToolAsync(
                "azmcp_storage_table_list",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "tenant", Settings.TenantId },
                { "account", Settings.ResourceBaseName },
                });

            var actual = result.AssertProperty("tables");
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
            Assert.NotEmpty(actual.EnumerateArray());
        }

        [Fact()]
        public async Task Should_list_storage_tables_with_tenant_name()
        {
            Assert.SkipWhen(Settings.IsServicePrincipal, TenantNameReason);

            var result = await CallToolAsync(
                "azmcp_storage_table_list",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "tenant", Settings.TenantName },
                { "account", Settings.ResourceBaseName },
                });

            var actual = result.AssertProperty("tables");
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
            Assert.NotEmpty(actual.EnumerateArray());
        }

        [Fact]
        public async Task Should_get_container_details()
        {
            var result = await CallToolAsync(
                "azmcp_storage_blob_container_details",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "account", Settings.ResourceBaseName },
                { "container", "bar" }
                });

            var actual = result.AssertProperty("details");
            Assert.Equal(JsonValueKind.Object, actual.ValueKind);
        }

        [Fact]
        public async Task Should_get_container_details_with_tenant_authkey()
        {
            var result = await CallToolAsync(
                "azmcp_storage_blob_container_details",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "account", Settings.ResourceBaseName },
                { "container", "bar" },
                { "auth-method", "key" }
                });

            var actual = result.AssertProperty("details");
            Assert.Equal(JsonValueKind.Object, actual.ValueKind);
        }

        [Fact]
        public async Task Should_list_datalake_filesystem_paths()
        {
            var result = await CallToolAsync(
                "azmcp_storage_datalake_file-system_list-paths",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "account", Settings.ResourceBaseName },
                { "file-system", "testfilesystem" }
                });

            var actual = result.AssertProperty("paths");
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
        }

        [Fact]
        public async Task Should_list_datalake_filesystem_paths_recursively()
        {
            var result = await CallToolAsync(
                "azmcp_storage_datalake_file-system_list-paths",
                new()
                {
                { "subscription", Settings.SubscriptionName },
                { "account", Settings.ResourceBaseName },
                { "file-system", "testfilesystem" },
                { "recursive", true }
                });

            var actual = result.AssertProperty("paths");
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
        }

        [Fact]
        public async Task Should_create_datalake_directory()
        {
            var directoryPath = "testfilesystem/test-directory";

            var result = await CallToolAsync(
                "azmcp_storage_datalake_directory_create",
                new()
                {
                    { "subscription", Settings.SubscriptionName },
                    { "account", Settings.ResourceBaseName },
                    { "directory-path", directoryPath }
                });

            var directory = result.AssertProperty("directory");
            Assert.Equal(JsonValueKind.Object, directory.ValueKind);

            var name = directory.GetProperty("name").GetString();
            var type = directory.GetProperty("type").GetString();

            Assert.Equal(directoryPath, name);
            Assert.Equal("directory", type);
        }

        [Fact]
        public async Task Should_set_blob_tier_batch()
        {
            // This test assumes the test storage account has the "bar" container with some test blobs
            // We'll set tier to Cool for multiple blobs
            var result = await CallToolAsync(
                "azmcp_storage_blob_batch_set-tier",
                new()
                {
                    { "subscription", Settings.SubscriptionName },
                    { "account", Settings.ResourceBaseName },
                    { "container", "bar" },
                    { "tier", "Cool" },
                    { "blob-names", "blob1.txt blob2.txt" }
                });

            var successfulBlobs = result.AssertProperty("successfulBlobs");
            var failedBlobs = result.AssertProperty("failedBlobs");

            Assert.Equal(JsonValueKind.Array, successfulBlobs.ValueKind);
            Assert.Equal(JsonValueKind.Array, failedBlobs.ValueKind);

            // At least one of the blobs should succeed if they exist, or all should be in failed if they don't exist
            var successCount = successfulBlobs.GetArrayLength();
            var failedCount = failedBlobs.GetArrayLength();

            Assert.True(successCount + failedCount > 0, "Should have processed at least one blob");
        }

        [Fact]
        public async Task Should_list_files_in_share_directory()
        {
            var result = await CallToolAsync(
                "azmcp_storage_share_file_list",
                new()
                {
                    { "subscription", Settings.SubscriptionName },
                    { "account", Settings.ResourceBaseName },
                    { "share", "testshare" },
                    { "directory-path", "/" }
                });

            var files = result.AssertProperty("files");
            Assert.Equal(JsonValueKind.Array, files.ValueKind);
            Assert.NotEmpty(files.EnumerateArray());
        }

        [Fact]
        public async Task Should_list_files_in_share_directory_with_prefix()
        {
            var result = await CallToolAsync(
                "azmcp_storage_share_file_list",
                new()
                {
                    { "subscription", Settings.SubscriptionName },
                    { "account", Settings.ResourceBaseName },
                    { "share", "testshare" },
                    { "directory-path", "/" },
                    { "prefix", "NoSuchPrefix" }
                });

            // When using a prefix that does not match any files, we should still return a valid response
            // with no result.
            Assert.Null(result);
        }
    }
}
