// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments;

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
        string subscriptionId,
        string? tenantId = null,
        RetryPolicyArguments? retryPolicy = null);
}