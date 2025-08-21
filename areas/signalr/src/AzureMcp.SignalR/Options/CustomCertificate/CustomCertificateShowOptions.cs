// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.SignalR.Options.CustomCertificate;

/// <summary>
/// Options for the SignalR certificate show command.
/// </summary>
public class CustomCertificateShowOptions : BaseSignalROptions
{
    /// <summary>
    /// The name of the custom certificate.
    /// </summary>
    public string? CertificateName { get; set; }
}
