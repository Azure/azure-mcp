// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;
using AzureMcp.SignalR.Models;

namespace AzureMcp.SignalR.Services;

/// <summary>
/// Service interface for Azure SignalR operations.
/// </summary>
public interface ISignalRService
{
    Task<IEnumerable<SignalRRuntimeModel>> ListRuntimesAsync(
        string subscription,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<SignalRRuntimeModel?> GetRuntimeAsync(
        string subscription,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);

    /// <summary>
    /// Lists keys for a SignalR service.
    /// </summary>
    Task<SignalRKeyModel> ListKeysAsync(
        string subscription,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);

    /// <summary>
    /// Gets identity configuration for a SignalR service.
    /// </summary>
    Task<SignalRIdentityModel?> GetSignalRIdentityAsync(
        string subscription,
        string resourceGroupName,
        string signalRName,
        string? tenant = null,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);

    /// <summary>
    /// Lists network ACL rules for a SignalR service.
    /// </summary>
    Task<SignalRNetworkAclModel?> GetNetworkRulesAsync(
        string subscription,
        string resourceGroup,
        string signalRName,
        AuthMethod? authMethod = null,
        RetryPolicyOptions? retryPolicy = null);
}
