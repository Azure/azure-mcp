// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.SignalR.Models;

/// <summary>
/// Model representing an Azure SignalR Service identity.
/// </summary>
public class Identity
{
    /// <summary>
    /// The type of identity used for the resource.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The principal ID of the identity.
    /// </summary>
    public string? PrincipalId { get; set; }

    /// <summary>
    /// The tenant ID of the identity.
    /// </summary>
    public string? TenantId { get; set; }

    /// <summary>
    /// The list of user assigned identities associated with the resource.
    /// </summary>
    public Dictionary<string, UserAssignedIdentity>? UserAssignedIdentities { get; set; }
}

/// <summary>
/// Model representing a user assigned identity.
/// </summary>
public class UserAssignedIdentity
{
    /// <summary>
    /// The principal ID of the user assigned identity.
    /// </summary>
    public string? PrincipalId { get; set; }

    /// <summary>
    /// The client ID of the user assigned identity.
    /// </summary>
    public string? ClientId { get; set; }
}
