// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.SignalR.Models;

/// <summary>
/// Represents a SignalR network rule.
/// </summary>
public record SignalRNetworkRuleModel
{
    public IEnumerable<string>? Allow { get; init; }
    public IEnumerable<string>? Deny { get; init; }
}

/// <summary>
/// Represents a private endpoint configuration for SignalR.
/// </summary>
public record SignalRPrivateEndpointModel
{
    public string? Name { get; init; }
    public IEnumerable<string>? Allow { get; init; }
    public IEnumerable<string>? Deny { get; init; }
}

/// <summary>
/// Represents the complete network ACL configuration for a SignalR service.
/// </summary>
public record SignalRNetworkAclModel
{
    public string? DefaultAction { get; init; }
    public IEnumerable<SignalRPrivateEndpointModel>? PrivateEndpoints { get; init; }
    public SignalRNetworkRuleModel? PublicNetwork { get; init; }
}
