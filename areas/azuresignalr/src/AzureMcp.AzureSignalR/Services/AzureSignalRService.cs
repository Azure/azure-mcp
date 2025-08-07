// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.SignalR;
using AzureMcp.Core.Models.Identity;
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
}
