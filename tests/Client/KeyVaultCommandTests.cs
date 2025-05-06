using System.Text.Json;
using Azure.Security.KeyVault.Keys;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Client;

public class KeyVaultCommandTests(McpClientFixture mcpClient, LiveTestSettingsFixture liveTestSettings, ITestOutputHelper output)
    : CommandTestsBase(mcpClient, liveTestSettings, output),
    IClassFixture<McpClientFixture>, IClassFixture<LiveTestSettingsFixture>
{
    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_key_vaults_by_subscription_id()
    {
        var result = await CallToolAsync(
            "azmcp-keyvault-key-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "vault", Settings.ResourceBaseName }
            });

        var results = result.AssertProperty("keys");
        Assert.Equal(JsonValueKind.Array, results.ValueKind);
        Assert.NotEmpty(results.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_get_key()
    {
        // Created in keyvault.bicep.
        var existingKey = "foo-bar";
        var result = await CallToolAsync(
            "azmcp-keyvault-key-get",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "vault", Settings.ResourceBaseName },
                { "key", existingKey}
            });

        var results = result.AssertProperty("name");
        Assert.Equal(JsonValueKind.String, results.ValueKind);
        Assert.Equal(existingKey, results.GetString());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_create_key()
    {
        var keyName = Settings.ResourceBaseName + Random.Shared.NextInt64();
        var result = await CallToolAsync(
            "azmcp-keyvault-key-create",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "vault", Settings.ResourceBaseName },
                { "key", keyName},
                { "key-type", KeyType.Rsa.ToString() }
            });

        var results = result.AssertProperty("name");
        Assert.Equal(JsonValueKind.String, results.ValueKind);
        Assert.Equal(keyName, results.GetString());
    }
}
