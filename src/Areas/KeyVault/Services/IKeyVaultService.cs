// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Security.KeyVault.Keys;
using AzureMcp.Options;

namespace AzureMcp.Services.Interfaces;

public interface IKeyVaultService
{
    /// <summary>
    /// List all keys in a Key Vault.
    /// </summary>
    /// <param name="vaultName">Name of the Key Vault.</param>
    /// <param name="subscriptionId">Subscription ID containing the Key Vault.</param>
    /// <param name="tenantId">Optional tenant ID for cross-tenant operations.</param>
    /// <param name="retryPolicy">Optional retry policy for the operation.</param>
    /// <returns>List of key names in the vault.</returns>
    Task<List<string>> ListKeys(
        string vaultName,
        bool includeManagedKeys,
        string subscriptionId,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null);

    /// <summary>
    /// Gets a key from an Azure Key Vault.
    /// </summary>
    /// <param name="vaultName">The name of the Key Vault</param>
    /// <param name="keyName">The name of the key to retrieve</param>
    /// <param name="subscriptionId">The subscription ID or name</param>
    /// <param name="tenantId">Optional tenant ID for cross-tenant operations</param>
    /// <param name="retryPolicy">Optional retry policy for the operation</param>
    /// <returns>The key</returns>
    Task<KeyVaultKey> GetKey(
        string vaultName,
        string keyName,
        string subscriptionId,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null);

    /// <summary>
    /// Creates a new key in an Azure Key Vault.
    /// </summary>
    /// <param name="vaultName">The name of the Key Vault</param>
    /// <param name="keyName">The name of the key to create</param>
    /// <param name="keyType">The type of key to create (e.g., RSA, EC, OCT)</param>
    /// <param name="subscriptionId">The subscription ID or name</param>
    /// <param name="tenantId">Optional tenant ID for cross-tenant operations</param>
    /// <param name="retryPolicy">Optional retry policy for the operation</param>
    /// <returns>The created key</returns>
    Task<KeyVaultKey> CreateKey(
        string vaultName,
        string keyName,
        string keyType,
        string subscriptionId,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null);

    /// <summary>
    /// Gets a secret from a Key Vault.
    /// </summary>
    /// <param name="vaultName">The name of the Key Vault</param>
    /// <param name="secretName">The name of the secret to retrieve</param>
    /// <param name="subscriptionId">The subscription ID or name</param>
    /// <param name="tenantId">Optional tenant ID for cross-tenant operations</param>
    /// <param name="retryPolicy">Optional retry policy for the operation</param>
    /// <returns>The secret value</returns>
    Task<string> GetSecret(
        string vaultName,
        string secretName,
        string subscriptionId,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null);
}
