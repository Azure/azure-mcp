// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Security.KeyVault.Certificates;
using Azure.Security.KeyVault.Keys;
using Azure.Security.KeyVault.Secrets;
using AzureMcp.Options;
using AzureMcp.Services.Azure;

namespace AzureMcp.Areas.KeyVault.Services;

public sealed class KeyVaultService : BaseAzureService, IKeyVaultService
{
    public async Task<List<string>> ListKeys(
        string vaultName,
        bool includeManagedKeys,
        string subscriptionId,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(vaultName, subscriptionId);

        var credential = await GetCredential(tenantId);
        var client = new KeyClient(new Uri($"https://{vaultName}.vault.azure.net"), credential);
        var keys = new List<string>();

        try
        {
            await foreach (var key in client.GetPropertiesOfKeysAsync().Where(x => x.Managed == includeManagedKeys))
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

    public async Task<KeyVaultKey> GetKey(
        string vaultName,
        string keyName,
        string subscriptionId,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(vaultName, subscriptionId);

        if (string.IsNullOrWhiteSpace(keyName))
        {
            throw new ArgumentException("Key name cannot be null or empty", nameof(keyName));
        }

        var credential = await GetCredential(tenantId);
        var client = new KeyClient(new Uri($"https://{vaultName}.vault.azure.net"), credential);

        try
        {
            return await client.GetKeyAsync(keyName);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving key '{keyName}' from vault {vaultName}: {ex.Message}", ex);
        }
    }

    public async Task<KeyVaultKey> CreateKey(
        string vaultName,
        string keyName,
        string keyType,
        string subscriptionId,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(vaultName, subscriptionId);

        if (string.IsNullOrWhiteSpace(keyName))
        {
            throw new ArgumentException("Key name cannot be null or empty", nameof(keyName));
        }

        if (string.IsNullOrWhiteSpace(keyType))
        {
            throw new ArgumentException("Key type cannot be null or empty", nameof(keyType));
        }

        var type = new KeyType(keyType);
        var credential = await GetCredential(tenantId);
        var client = new KeyClient(new Uri($"https://{vaultName}.vault.azure.net"), credential);

        try
        {
            return await client.CreateKeyAsync(keyName, type);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating key '{keyName}' in vault {vaultName}: {ex.Message}", ex);
        }
    }

    public async Task<List<string>> ListSecrets(
        string vaultName,
        string subscriptionId,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(vaultName, subscriptionId);

        var credential = await GetCredential(tenantId);
        var client = new SecretClient(new Uri($"https://{vaultName}.vault.azure.net"), credential);
        var secrets = new List<string>();

        try
        {
            await foreach (var secret in client.GetPropertiesOfSecretsAsync())
            {
                secrets.Add(secret.Name);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving secrets from vault {vaultName}: {ex.Message}", ex);
        }

        return secrets;
    }

    public async Task<KeyVaultSecret> CreateSecret(
        string vaultName,
        string secretName,
        string secretValue,
        string subscriptionId,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(vaultName, subscriptionId);

        if (string.IsNullOrWhiteSpace(secretName))
        {
            throw new ArgumentException("Secret name cannot be null or empty", nameof(secretName));
        }

        if (string.IsNullOrWhiteSpace(secretValue))
        {
            throw new ArgumentException("Secret value cannot be null or empty", nameof(secretValue));
        }

        var credential = await GetCredential(tenantId);
        var client = new SecretClient(new Uri($"https://{vaultName}.vault.azure.net"), credential);

        try
        {
            return await client.SetSecretAsync(secretName, secretValue);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating secret '{secretName}' in vault {vaultName}: {ex.Message}", ex);
        }
    }

    public async Task<KeyVaultSecret> GetSecret(
        string vaultName,
        string secretName,
        string subscriptionId,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(vaultName, subscriptionId);

        if (string.IsNullOrWhiteSpace(secretName))
        {
            throw new ArgumentException("Secret name cannot be null or empty", nameof(secretName));
        }

        var credential = await GetCredential(tenantId);
        var client = new SecretClient(new Uri($"https://{vaultName}.vault.azure.net"), credential);

        try
        {
            var response = await client.GetSecretAsync(secretName);
            return response.Value;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving secret '{secretName}' from vault {vaultName}: {ex.Message}", ex);
        }
    }

    public async Task<List<string>> ListCertificates(
        string vaultName,
        string subscriptionId,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(vaultName, subscriptionId);

        var credential = await GetCredential(tenantId);
        var client = new CertificateClient(new Uri($"https://{vaultName}.vault.azure.net"), credential);
        var certificates = new List<string>();

        try
        {
            await foreach (var certificate in client.GetPropertiesOfCertificatesAsync())
            {
                certificates.Add(certificate.Name);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving certificates from vault {vaultName}: {ex.Message}", ex);
        }

        return certificates;
    }

    public async Task<KeyVaultCertificateWithPolicy> GetCertificate(
        string vaultName,
        string certificateName,
        string subscriptionId,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(vaultName, subscriptionId);

        if (string.IsNullOrWhiteSpace(certificateName))
        {
            throw new ArgumentException("Certificate name cannot be null or empty", nameof(certificateName));
        }

        var credential = await GetCredential(tenantId);
        var client = new CertificateClient(new Uri($"https://{vaultName}.vault.azure.net"), credential);

        try
        {
            return await client.GetCertificateAsync(certificateName);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving certificate '{certificateName}' from vault {vaultName}: {ex.Message}", ex);
        }
    }

    public async Task<CertificateOperation> CreateCertificate(
        string vaultName,
        string certificateName,
        string subscriptionId,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(vaultName, subscriptionId);

        if (string.IsNullOrWhiteSpace(certificateName))
        {
            throw new ArgumentException("Certificate name cannot be null or empty", nameof(certificateName));
        }

        var credential = await GetCredential(tenantId);
        var client = new CertificateClient(new Uri($"https://{vaultName}.vault.azure.net"), credential);

        try
        {
            return await client.StartCreateCertificateAsync(certificateName, CertificatePolicy.Default);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating certificate '{certificateName}' in vault {vaultName}: {ex.Message}", ex);
        }
    }
}
