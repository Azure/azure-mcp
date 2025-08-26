// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.SignalR.Models;

/// <summary>
/// Represents a SignalR network rule.
/// </summary>
public class NetworkRule
{
    /// <summary>
    /// Gets or sets the default action to take when no specific rule matches.
    /// Valid values are "Allow" or "Deny".
    /// </summary>
    public string? DefaultAction { get; set; }

    /// <summary>
    /// Gets or sets the collection of private endpoint configurations with their specific access rules.
    /// Each private endpoint can have its own allow/deny rules independent of the public network rules.
    /// </summary>
    public IEnumerable<PrivateEndpointNetworkAcl>? PrivateEndpoints { get; set; }

    /// <summary>
    /// Gets or sets the network access rules for public network connections.
    /// If null, the default action will be applied to public network requests.
    /// </summary>
    public NetworkAcl? PublicNetwork { get; set; }
}

/// <summary>
/// Represents a private endpoint configuration for SignalR network access control,
/// defining access rules for a specific private endpoint connection.
/// </summary>
public class PrivateEndpointNetworkAcl: NetworkAcl
{
    /// <summary>
    /// Gets or sets the name of the private endpoint.
    /// </summary>
    public string? Name { get; set; }
}

/// <summary>
/// Represents the public network access rule configuration for a SignalR service,
/// defining which request types are allowed or denied from public networks.
/// </summary>
public class NetworkAcl
{
    /// <summary>
    /// Gets or sets the collection of allowed request types for public network access.
    /// Common values include "ServerConnection", "ClientConnection", "RESTAPI", "Trace".
    /// </summary>
    public IEnumerable<string>? Allow { get; set; }

    /// <summary>
    /// Gets or sets the collection of denied request types for public network access.
    /// Common values include "ServerConnection", "ClientConnection", "RESTAPI", "Trace".
    /// </summary>
    public IEnumerable<string>? Deny { get; set; }
}
