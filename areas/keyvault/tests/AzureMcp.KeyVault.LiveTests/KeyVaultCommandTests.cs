// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.Security.KeyVault.Keys;
using AzureMcp.Core.UnitTests.Client;
using AzureMcp.Core.UnitTests.Client.Helpers;
using AzureMcp.Tests;
using Xunit;

namespace AzureMcp.KeyVault.LiveTests;

public class KeyVaultCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output),
    IClassFixture<LiveTestFixture>
{
    [Fact]
    public async Task Should_list_keys()
    {
        var result = await CallToolAsync(
            "azmcp_keyvault_key_list",
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
    public async Task Should_get_key()
    {
        // Created in keyvault.bicep.
        var knownKeyName = "foo-bar";
        var result = await CallToolAsync(
            "azmcp_keyvault_key_get",
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
    public async Task Should_create_key()
    {
        var keyName = Settings.ResourceBaseName + Random.Shared.NextInt64();
        var result = await CallToolAsync(
            "azmcp_keyvault_key_create",
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
    public async Task Should_get_secret()
    {
        // Created in keyvault.bicep.
        var secretName = "foo-bar-secret";
        var result = await CallToolAsync(
            "azmcp_keyvault_secret_get",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "vault", Settings.ResourceBaseName },
                { "secret", secretName }
            });

        var name = result.AssertProperty("name");
        Assert.Equal(JsonValueKind.String, name.ValueKind);
        Assert.Equal(secretName, name.GetString());

        var value = result.AssertProperty("value");
        Assert.Equal(JsonValueKind.String, value.ValueKind);
        Assert.NotNull(value.GetString());
    }
}
