// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.SignalR;
using AzureMcp.Core.Options;
using AzureMcp.Core.Services.Azure;
using AzureMcp.Core.Services.Azure.Subscription;
using AzureMcp.Core.Services.Azure.Tenant;
using AzureMcp.AzureSignalR.Models;
using AzureMcp.Core.Models;

namespace AzureMcp.AzureSignalR.Services;

public class AzureSignalRService(ISubscriptionService subscriptionService, ITenantService tenantService)
    : BaseAzureService(tenantService), IAzureSignalRService
{
    public async Task<IEnumerable<SignalRServiceModel>> ListSignalRServicesAsync(
        string subscriptionId,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId);

        try
        {
            var subscriptionResource = await subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy)
                ?? throw new Exception($"Subscription '{subscriptionId}' not found");

            var signalRServices = new List<SignalRServiceModel>();

            await foreach (var signalR in subscriptionResource.GetSignalRsAsync())
            {
                signalRServices.Add(new SignalRServiceModel
                {
                    Name = signalR.Data.Name,
                    ResourceGroupName = signalR.Id.ResourceGroupName ?? string.Empty,
                    Location = signalR.Data.Location.Name,
                    SkuName = signalR.Data.Sku?.Name ?? string.Empty,
                    SkuTier = signalR.Data.Sku?.Tier?.ToString() ?? string.Empty,
                    ProvisioningState = signalR.Data.ProvisioningState?.ToString() ?? string.Empty,
                    HostName = signalR.Data.HostName ?? string.Empty,
                    PublicPort = signalR.Data.PublicPort,
                    ServerPort = signalR.Data.ServerPort
                });
            }

            return signalRServices;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to list SignalR services: {ex.Message}", ex);
        }
    }

    public async Task<SignalRCertificateModel?> GetCertificateAsync(
        string subscriptionId,
        string resourceGroupName,
        string signalRName,
        string certificateName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId, resourceGroupName, signalRName, certificateName);

        try
        {
            var subscriptionResource = await subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy)
                ?? throw new Exception($"Subscription '{subscriptionId}' not found");

            var resourceGroupResource = await subscriptionResource
                .GetResourceGroupAsync(resourceGroupName);

            var signalRResource = await resourceGroupResource.Value
                .GetSignalRs()
                .GetAsync(signalRName);

            var certificateResource = await signalRResource.Value
                .GetSignalRCustomCertificates()
                .GetAsync(certificateName);

            var certificate = certificateResource.Value;

            return new SignalRCertificateModel
            {
                Name = certificate.Data.Name,
                Id = certificate.Id.ToString(),
                Type = certificate.Data.ResourceType.ToString(),
                ProvisioningState = certificate.Data.ProvisioningState?.ToString() ?? string.Empty,
                KeyVaultBaseUri = certificate.Data.KeyVaultBaseUri?.ToString() ?? string.Empty,
                KeyVaultSecretName = certificate.Data.KeyVaultSecretName ?? string.Empty,
                KeyVaultSecretVersion = certificate.Data.KeyVaultSecretVersion
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to get SignalR certificate '{certificateName}': {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<SignalRCustomDomainModel>> ListCustomDomainsAsync(
        string subscriptionId,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId, resourceGroupName, signalRName);

        try
        {
            var subscriptionResource = await subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy)
                ?? throw new Exception($"Subscription '{subscriptionId}' not found");

            var resourceGroupResource = await subscriptionResource
                .GetResourceGroupAsync(resourceGroupName);

            var signalRResource = await resourceGroupResource.Value
                .GetSignalRs()
                .GetAsync(signalRName);

            var customDomains = new List<SignalRCustomDomainModel>();

            await foreach (var customDomain in signalRResource.Value.GetSignalRCustomDomains())
            {
                customDomains.Add(new SignalRCustomDomainModel
                {
                    Name = customDomain.Data.Name,
                    Id = customDomain.Id.ToString(),
                    Type = customDomain.Data.ResourceType.ToString(),
                    ProvisioningState = customDomain.Data.ProvisioningState?.ToString() ?? string.Empty,
                    DomainName = customDomain.Data.DomainName ?? string.Empty,
                    CustomCertificate = string.Empty // Note: CustomCertificate property may not be directly accessible
                });
            }

            return customDomains;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to list SignalR custom domains for service '{signalRName}': {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<SignalRCertificateModel>> ListCertificatesAsync(
        string subscriptionId,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId, resourceGroupName, signalRName);

        try
        {
            var subscriptionResource = await subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy)
                ?? throw new Exception($"Subscription '{subscriptionId}' not found");

            var resourceGroupResource = await subscriptionResource
                .GetResourceGroupAsync(resourceGroupName);

            var signalRResource = await resourceGroupResource.Value
                .GetSignalRs()
                .GetAsync(signalRName);

            var certificates = new List<SignalRCertificateModel>();

            await foreach (var certificate in signalRResource.Value.GetSignalRCustomCertificates())
            {
                certificates.Add(new SignalRCertificateModel
                {
                    Name = certificate.Data.Name,
                    Id = certificate.Id.ToString(),
                    Type = certificate.Data.ResourceType.ToString(),
                    ProvisioningState = certificate.Data.ProvisioningState?.ToString() ?? string.Empty,
                    KeyVaultBaseUri = certificate.Data.KeyVaultBaseUri?.ToString() ?? string.Empty,
                    KeyVaultSecretName = certificate.Data.KeyVaultSecretName ?? string.Empty,
                    KeyVaultSecretVersion = certificate.Data.KeyVaultSecretVersion
                });
            }

            return certificates;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to list SignalR certificates for service '{signalRName}': {ex.Message}", ex);
        }
    }

    public async Task<SignalRCustomDomainModel?> GetCustomDomainAsync(
        string subscriptionId,
        string resourceGroupName,
        string signalRName,
        string customDomainName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId, resourceGroupName, signalRName, customDomainName);

        try
        {
            var subscriptionResource = await subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy)
                ?? throw new Exception($"Subscription '{subscriptionId}' not found");

            var resourceGroupResource = await subscriptionResource
                .GetResourceGroupAsync(resourceGroupName);

            var signalRResource = await resourceGroupResource.Value
                .GetSignalRs()
                .GetAsync(signalRName);

            var customDomainResource = await signalRResource.Value
                .GetSignalRCustomDomains()
                .GetAsync(customDomainName);

            var customDomain = customDomainResource.Value;

            return new SignalRCustomDomainModel
            {
                Name = customDomain.Data.Name,
                Id = customDomain.Id.ToString(),
                Type = customDomain.Data.ResourceType.ToString(),
                ProvisioningState = customDomain.Data.ProvisioningState?.ToString() ?? string.Empty,
                DomainName = customDomain.Data.DomainName ?? string.Empty,
                CustomCertificate = string.Empty // Note: CustomCertificate property may not be directly accessible
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to get SignalR custom domain '{customDomainName}': {ex.Message}", ex);
        }
    }

    public async Task<SignalRKeyModel> ListKeysAsync(
        string subscriptionId,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId, resourceGroupName, signalRName);

        try
        {
            var subscriptionResource = await subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy)
                ?? throw new Exception($"Subscription '{subscriptionId}' not found");

            var resourceGroupResource = await subscriptionResource
                .GetResourceGroupAsync(resourceGroupName);

            var signalRResource = await resourceGroupResource.Value
                .GetSignalRs()
                .GetAsync(signalRName);

            var keys = await signalRResource.Value.GetKeysAsync();

            return new SignalRKeyModel
            {
                KeyType = "Both",
                PrimaryKey = keys.Value.PrimaryKey ?? string.Empty,
                SecondaryKey = keys.Value.SecondaryKey ?? string.Empty,
                PrimaryConnectionString = keys.Value.PrimaryConnectionString ?? string.Empty,
                SecondaryConnectionString = keys.Value.SecondaryConnectionString ?? string.Empty,
                ConnectionString = keys.Value.PrimaryConnectionString ?? string.Empty
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to list SignalR keys for service '{signalRName}': {ex.Message}", ex);
        }
    }

    public async Task<SignalRServiceModel?> GetSignalRServiceAsync(
        string subscriptionId,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null)
    {
        ValidateRequiredParameters(subscriptionId, resourceGroupName, signalRName);

        try
        {
            var subscriptionResource = await subscriptionService.GetSubscription(subscriptionId, tenant, retryPolicy)
                ?? throw new Exception($"Subscription '{subscriptionId}' not found");

            var resourceGroupResource = await subscriptionResource
                .GetResourceGroupAsync(resourceGroupName);

            var signalRResource = await resourceGroupResource.Value
                .GetSignalRs()
                .GetAsync(signalRName);

            var signalR = signalRResource.Value;

            return new SignalRServiceModel
            {
                Name = signalR.Data.Name,
                ResourceGroupName = signalR.Id.ResourceGroupName ?? string.Empty,
                Location = signalR.Data.Location.Name,
                SkuName = signalR.Data.Sku?.Name ?? string.Empty,
                SkuTier = signalR.Data.Sku?.Tier?.ToString() ?? string.Empty,
                ProvisioningState = signalR.Data.ProvisioningState?.ToString() ?? string.Empty,
                HostName = signalR.Data.HostName ?? string.Empty,
                PublicPort = signalR.Data.PublicPort,
                ServerPort = signalR.Data.ServerPort
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to get SignalR service '{signalRName}': {ex.Message}", ex);
        }
    }
}
