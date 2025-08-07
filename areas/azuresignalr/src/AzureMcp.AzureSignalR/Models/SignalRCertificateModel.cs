// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.AzureSignalR.Models;

/// <summary>
/// Represents a SignalR service custom certificate.
/// </summary>
public class SignalRCertificateModel
{
    /// <summary>
    /// The name of the certificate.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The resource ID of the certificate.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The type of the certificate resource.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// The provisioning state of the certificate.
    /// </summary>
    public string ProvisioningState { get; set; } = string.Empty;

    /// <summary>
    /// The Key Vault base URI.
    /// </summary>
    public string KeyVaultBaseUri { get; set; } = string.Empty;

    /// <summary>
    /// The Key Vault secret name.
    /// </summary>
    public string KeyVaultSecretName { get; set; } = string.Empty;

    /// <summary>
    /// The Key Vault secret version.
    /// </summary>
    public string? KeyVaultSecretVersion { get; set; }
}
