// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Security.KeyVault.Keys;
using AzureMcp.Arguments;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Services.Azure.KeyVault;

public sealed class KeyVaultService(ISubscriptionService subscriptionService) : BaseAzureService, IKeyVaultService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService;

    public async Task<List<string>> ListKeys(
        string vaultName,
        string subscriptionId,
        string? tenantId = null,
        RetryPolicyArguments? retryPolicy = null)
    {
        ValidateRequiredParameters(vaultName, subscriptionId);

        var credential = await GetCredential(tenantId);
        var client = new KeyClient(new Uri($"https://{vaultName}.vault.azure.net"), credential);
        var keys = new List<string>();

        try
        {
            await foreach (var key in client.GetPropertiesOfKeysAsync())
            {
                keys.Add(key.Name);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving keys from vault {vaultName}: {ex.Message}", ex);
        }

        return keys;
    }
}
