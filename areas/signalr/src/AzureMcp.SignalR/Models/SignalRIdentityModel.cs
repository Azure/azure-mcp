// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.SignalR.Models;

/// <summary>
/// Model representing an Azure SignalR Service identity.
/// </summary>
public sealed class SignalRIdentityModel
{
    /// <summary>
    /// The type of identity used for the resource.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// The principal ID of the system assigned identity.
    /// </summary>
    public string? PrincipalId { get; set; }

    /// <summary>
    /// The tenant ID of the system assigned identity.
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
public sealed class UserAssignedIdentity
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
