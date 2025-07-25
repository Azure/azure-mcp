// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using Azure.Security.KeyVault.Keys;
using AzureMcp.Tests;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
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
    public async Task Should_list_secrets()
    {
        var result = await CallToolAsync(
            "azmcp_keyvault_secret_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "vault", Settings.ResourceBaseName }
            });

        var secrets = result.AssertProperty("secrets");
        Assert.Equal(JsonValueKind.Array, secrets.ValueKind);
        Assert.NotEmpty(secrets.EnumerateArray());
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

    [Fact]
    public async Task Should_create_secret()
    {
        var secretName = Settings.ResourceBaseName + Random.Shared.NextInt64();
        var secretValue = "test-value-" + Random.Shared.NextInt64();
        var result = await CallToolAsync(
            "azmcp_keyvault_secret_create",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "vault", Settings.ResourceBaseName },
                { "secret", secretName},
                { "value", secretValue }
            });

        var createdSecretName = result.AssertProperty("name");
        Assert.Equal(JsonValueKind.String, createdSecretName.ValueKind);
        Assert.Equal(secretName, createdSecretName.GetString());

        var returnedValue = result.AssertProperty("value");
        Assert.Equal(JsonValueKind.String, returnedValue.ValueKind);
        Assert.Equal(secretValue, returnedValue.GetString());
    }

    [Fact]
    public async Task Should_list_certificates()
    {
        var result = await CallToolAsync(
            "azmcp_keyvault_certificate_list",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "vault", Settings.ResourceBaseName }
            });

        var certificates = result.AssertProperty("certificates");
        Assert.Equal(JsonValueKind.Array, certificates.ValueKind);
        // Certificates might be empty if the test certificate creation has not yet completed, so we won't assert non-empty
    }

    [Fact]
    public async Task Should_get_certificate()
    {
        // Created in keyvault.bicep.
        var certificateName = "foo-bar-certificate";
        var result = await CallToolAsync(
            "azmcp_keyvault_certificate_get",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "vault", Settings.ResourceBaseName },
                { "certificate", certificateName }
            });

        var name = result.AssertProperty("name");
        Assert.Equal(JsonValueKind.String, name.ValueKind);
        Assert.Equal(certificateName, name.GetString());

        // Verify that the certificate has some expected properties
        ValidateCertificate(result);
    }

    [Fact]
    public async Task Should_create_certificate()
    {
        var certificateName = Settings.ResourceBaseName + Random.Shared.NextInt64();
        var result = await CallToolAsync(
            "azmcp_keyvault_certificate_create",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "vault", Settings.ResourceBaseName },
                { "certificate", certificateName}
            });

        var createdCertificateName = result.AssertProperty("name");
        Assert.Equal(JsonValueKind.String, createdCertificateName.ValueKind);
        Assert.Equal(certificateName, createdCertificateName.GetString());

        // Verify that the certificate has some expected properties
        ValidateCertificate(result);
    }

    private void ValidateCertificate(JsonElement? result)
    {
        Assert.NotNull(result);

        var requiredProperties = new[] { "name", "thumbprint", "cer" };

        foreach (var propertyName in requiredProperties)
        {
            var property = result.AssertProperty(propertyName);
            Assert.Equal(JsonValueKind.String, property.ValueKind);
            Assert.NotNull(property.GetString());
        }
    }
}
