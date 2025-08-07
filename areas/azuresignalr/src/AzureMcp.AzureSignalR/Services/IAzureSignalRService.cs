// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Models.Identity;
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
}
