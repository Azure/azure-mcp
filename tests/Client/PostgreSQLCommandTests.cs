// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client.Helpers;
using Microsoft.Azure.Amqp.Framing;
using Xunit;

namespace AzureMcp.Tests.Client
{
    public class PostgreSQLCommandTests(McpClientFixture mcpClient, LiveTestSettingsFixture liveTestSettings, ITestOutputHelper output)
    : CommandTestsBase(mcpClient, liveTestSettings, output),
    IClassFixture<McpClientFixture>, IClassFixture<LiveTestSettingsFixture>
    {
        private const string UserName = "azure-sdk-internal-devops-connections";
        private const string DatabaseName = "postgres";
        private const string TableName = "pg_extension";

        [Fact]
        [Trait("Category", "Live")]
        public async Task Should_list_postgres_servers_by_subscription_id()
        {
            var result = await CallToolAsync(
                "azmcp-postgres-server-list",
                new()
                {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "user-name", UserName }
                });

            var actual = result.AssertProperty("Servers");
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
            Assert.NotEmpty(actual.EnumerateArray());
        }

        [Fact]
        [Trait("Category", "Live")]
        public async Task Should_list_postgres_servers_by_subscription_id_with_tenant_id()
        {
            var result = await CallToolAsync(
                "azmcp-postgres-server-list",
                new()
                {
                { "subscription", Settings.SubscriptionId },
                { "tenant", Settings.TenantId },
                { "resource-group", Settings.ResourceGroupName },
                { "user-name", UserName }
                });
            var actual = result.AssertProperty("Servers");
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
            Assert.NotEmpty(actual.EnumerateArray());
        }

        [Fact]
        [Trait("Category", "Live")]
        public async Task Should_get_postgres_server_config()
        {
            var result = await CallToolAsync(
                "azmcp-postgres-server-config",
                new()
                {
                { "subscription", Settings.SubscriptionId },
                { "server", Settings.ResourceGroupName },
                { "user-name", UserName },
                { "resource-group", Settings.ResourceGroupName }
                });

            var actual = result.AssertProperty("Configuration");
            Assert.Equal(JsonValueKind.String, actual.ValueKind);
            Assert.False(string.IsNullOrEmpty(actual.GetString()));
        }

        [Fact]
        [Trait("Category", "Live")]
        public async Task Should_get_postgres_server_param()
        {
            var result = await CallToolAsync(
                "azmcp-postgres-server-param",
                new()
                {
                { "subscription", Settings.SubscriptionId },
                { "server", Settings.ResourceGroupName },
                { "user-name", UserName },
                { "resource-group", Settings.ResourceGroupName },
                { "param", "azure.extensions" }
                });

            var actual = result.AssertProperty("ParameterValue");
            Assert.Equal(JsonValueKind.String, actual.ValueKind);
            Assert.False(string.IsNullOrEmpty(actual.GetString()));
        }

        [Fact]
        [Trait("Category", "Live")]
        public async Task Should_list_postgres_databases()
        {
            var result = await CallToolAsync(
                "azmcp-postgres-database-list",
                new()
                {
                { "subscription", Settings.SubscriptionId },
                { "server", Settings.ResourceGroupName },
                { "user-name", UserName },
                { "resource-group", Settings.ResourceGroupName }
                });

            var actual = result.AssertProperty("Databases");
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
            Assert.NotEmpty(actual.EnumerateArray());
        }

        [Fact]
        [Trait("Category", "Live")]
        public async Task Should_run_postgres_database_query()
        {
            var result = await CallToolAsync(
                "azmcp-postgres-database-query",
                new()
                {
                { "subscription", Settings.SubscriptionId },
                { "server", Settings.ResourceGroupName },
                { "user-name", UserName },
                { "resource-group", Settings.ResourceGroupName },
                { "database", DatabaseName },
                { "query", "SELECT * FROM pg_extension;" }
                });

            var actual = result.AssertProperty("QueryResult");
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
            Assert.NotEmpty(actual.EnumerateArray());
        }

        [Fact]
        [Trait("Category", "Live")]
        public async Task Should_get_postgres_table_list()
        {
            var result = await CallToolAsync(
                "azmcp-postgres-table-list",
                new()
                {
                { "subscription", Settings.SubscriptionId },
                { "server", Settings.ResourceGroupName },
                { "user-name", UserName },
                { "resource-group", Settings.ResourceGroupName },
                { "database", DatabaseName }
                });

            var actual = result.AssertProperty("Tables");
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
            Assert.NotEmpty(actual.EnumerateArray());
        }

        [Fact]
        [Trait("Category", "Live")]
        public async Task Should_get_postgres_table_schema()
        {
            var result = await CallToolAsync(
                "azmcp-postgres-table-schema",
                new()
                {
                { "subscription", Settings.SubscriptionId },
                { "server", Settings.ResourceGroupName },
                { "user-name", UserName },
                { "resource-group", Settings.ResourceGroupName },
                { "database", DatabaseName },
                { "table", TableName }
                });

            var actual = result.AssertProperty("Schema");
            Assert.Equal(JsonValueKind.Array, actual.ValueKind);
            Assert.NotEmpty(actual.EnumerateArray());
        }
    }
}
