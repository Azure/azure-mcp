// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.AzureSignalR.Options.CustomDomain;

/// <summary>
/// Options for showing SignalR custom domain details.
/// </summary>
public class CustomDomainShowOptions : BaseAzureSignalROptions
{
    /// <summary>
    /// The name of the custom domain.
    /// </summary>
    public string? CustomDomainName { get; set; }
}
