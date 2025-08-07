// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.AzureSignalR.Options.Certificate;

/// <summary>
/// Options for the SignalR certificate show command.
/// </summary>
public class CertificateShowOptions : BaseAzureSignalROptions
{
    /// <summary>
    /// The name of the custom certificate.
    /// </summary>
    public string? CertificateName { get; set; }
}
