// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.SignalR.Options;

/// <summary>
/// Option definitions for Azure SignalR commands.
/// </summary>
public static class SignalROptionDefinitions
{
    /// <summary>
    /// SignalR service name option.
    /// </summary>
    public static readonly Option<string> SignalRName = new(
        aliases: ["--signalr-name", "-n"],
        description: "The name of the SignalR service")
    {
        IsRequired = true
    };

    /// <summary>
    /// The name of the custom certificate.
    /// </summary>
    public static readonly Option<string> CertificateName = new(
        aliases: ["--name"],
        description: "The name of the custom certificate.")
    { IsRequired = true };

    public static readonly Option<string> CustomDomainName = new(
        aliases: ["--name"],
        description: "The name of the custom domain to be added to the SignalR service.")
    { IsRequired = true };
}
