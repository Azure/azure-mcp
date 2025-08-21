// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.SignalR.Models;

/// <summary>
/// Represents a SignalR service custom domain.
/// </summary>
public class SignalRCustomDomainModel
{
    /// <summary>
    /// The name of the custom domain.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The resource ID of the custom domain.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The type of the custom domain resource.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// The provisioning state of the custom domain.
    /// </summary>
    public string ProvisioningState { get; set; } = string.Empty;

    /// <summary>
    /// The domain name.
    /// </summary>
    public string DomainName { get; set; } = string.Empty;

    /// <summary>
    /// The custom certificate resource.
    /// </summary>
    public string CustomCertificate { get; set; } = string.Empty;
}
