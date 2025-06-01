// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;

namespace AzureMcp.Services.Interfaces;

/// <summary>
/// Interface for Defender for Cloud operations.
/// </summary>
public interface ISecurityService
{
    /// <summary>
    /// Gets a security alert by ID.
    /// </summary>
    /// <param name="subscriptionId">The subscription ID to search in.</param>
    /// <param name="alertId">The ID of the alert to retrieve.</param>
    /// <param name="resourceGroupName">Optional resource group name to limit search scope.</param>
    /// <param name="cancellationToken">Cancellation token for the operation.</param>
    /// <returns>A list containing the security alert data as JsonElement if found.</returns>
    Task<List<JsonElement>> GetAlertAsync(
        string subscriptionId,
        string alertId,
        string? resourceGroupName = null,
        CancellationToken cancellationToken = default);
}
