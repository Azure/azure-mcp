// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.Security.KeyVault.Keys;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Client;

public class KeyVaultCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output),
    IClassFixture<LiveTestFixture>
{
    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_keys()
    {
        var result = await CallToolAsync(
            "azmcp-keyvault-key-list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "vault", Settings.ResourceBaseName }
            });

        var keys = result.AssertProperty("keys");
        Assert.Equal(JsonValueKind.Array, keys.ValueKind);
        Assert.NotEmpty(keys.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_get_key()
    {
        // Created in keyvault.bicep.
        var knownKeyName = "foo-bar";
        var result = await CallToolAsync(
            "azmcp-keyvault-key-get",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "vault", Settings.ResourceBaseName },
                { "key", knownKeyName}
            });

        var keyName = result.AssertProperty("name");
        Assert.Equal(JsonValueKind.String, keyName.ValueKind);
        Assert.Equal(knownKeyName, keyName.GetString());

        var keyType = result.AssertProperty("keyType");
        Assert.Equal(JsonValueKind.String, keyType.ValueKind);
        Assert.Equal(KeyType.Rsa.ToString(), keyType.GetString());
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

        var createdKeyName = result.AssertProperty("name");
        Assert.Equal(JsonValueKind.String, createdKeyName.ValueKind);
        Assert.Equal(keyName, createdKeyName.GetString());

        var keyType = result.AssertProperty("keyType");
        Assert.Equal(JsonValueKind.String, keyType.ValueKind);
        Assert.Equal(KeyType.Rsa.ToString(), keyType.GetString());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_get_secret()
    {
        // Created in keyvault.bicep.
        var secretName = "foo-bar-secret";
        var result = await CallToolAsync(
            "azmcp-keyvault-secret-get",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "vault", Settings.ResourceBaseName },
                { "secret", secretName }
            });

        var results = result.AssertProperty("name");
        Assert.Equal(JsonValueKind.String, results.ValueKind);
        Assert.Equal(secretName, results.GetString());

        var value = result.AssertProperty("value");
        Assert.Equal(JsonValueKind.String, value.ValueKind);
        Assert.NotNull(value.GetString());
    }
}
