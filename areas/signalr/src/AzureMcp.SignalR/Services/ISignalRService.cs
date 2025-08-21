// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;
using AzureMcp.SignalR.Models;

namespace AzureMcp.SignalR.Services;

public interface ISignalRService
{
    Task<IEnumerable<SignalRServiceModel>> ListSignalRServicesAsync(
        string subscription,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<SignalRCertificateModel?> GetCertificateAsync(
        string subscription,
        string resourceGroupName,
        string signalRName,
        string certificateName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<IEnumerable<SignalRCustomDomainModel>> ListCustomDomainsAsync(
        string subscription,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<IEnumerable<SignalRCertificateModel>> ListCertificatesAsync(
        string subscription,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<SignalRCustomDomainModel?> GetCustomDomainAsync(
        string subscription,
        string resourceGroupName,
        string signalRName,
        string customDomainName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<SignalRKeyModel> ListKeysAsync(
        string subscription,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<SignalRServiceModel?> GetSignalRServiceAsync(
        string subscription,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);
}
