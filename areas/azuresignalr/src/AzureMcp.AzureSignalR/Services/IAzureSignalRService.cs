// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;
using AzureMcp.AzureSignalR.Models;
using AzureMcp.Core.Models;

namespace AzureMcp.AzureSignalR.Services;

public interface IAzureSignalRService
{
    Task<IEnumerable<SignalRServiceModel>> ListSignalRServicesAsync(
        string subscriptionId,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<SignalRCertificateModel?> GetCertificateAsync(
        string subscriptionId,
        string resourceGroupName,
        string signalRName,
        string certificateName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<IEnumerable<SignalRCustomDomainModel>> ListCustomDomainsAsync(
        string subscriptionId,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<IEnumerable<SignalRCertificateModel>> ListCertificatesAsync(
        string subscriptionId,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<SignalRCustomDomainModel?> GetCustomDomainAsync(
        string subscriptionId,
        string resourceGroupName,
        string signalRName,
        string customDomainName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<SignalRKeyModel> ListKeysAsync(
        string subscriptionId,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<SignalRServiceModel?> GetSignalRServiceAsync(
        string subscriptionId,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);
}
