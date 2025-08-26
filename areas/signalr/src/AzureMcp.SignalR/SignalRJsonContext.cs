// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.SignalR.Commands.Identity;
using AzureMcp.SignalR.Commands.Key;
using AzureMcp.SignalR.Commands.NetworkRule;
using AzureMcp.SignalR.Commands.Runtime;
using AzureMcp.SignalR.Models;

namespace AzureMcp.SignalR;

/// <summary>
/// JSON serialization context for Azure SignalR Service commands.
/// </summary>
[JsonSerializable(typeof(RuntimeListCommand.RuntimeListCommandResult))]
[JsonSerializable(typeof(RuntimeShowCommand.RuntimeShowCommandResult))]
[JsonSerializable(typeof(KeyListCommand.KeyListCommandResult))]
[JsonSerializable(typeof(IdentityShowCommand.IdentityShowCommandResult))]
[JsonSerializable(typeof(NetworkRuleListCommand.NetworkRuleListCommandResult))]
[JsonSerializable(typeof(Runtime))]
[JsonSerializable(typeof(Key))]
[JsonSerializable(typeof(Identity))]
[JsonSerializable(typeof(NetworkRule))]
[JsonSerializable(typeof(NetworkAcl))]
[JsonSerializable(typeof(PrivateEndpointNetworkAcl))]
[JsonSerializable(typeof(UserAssignedIdentity))]
[JsonSerializable(typeof(IEnumerable<Runtime>))]
[JsonSerializable(typeof(IEnumerable<PrivateEndpointNetworkAcl>))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public partial class SignalRJsonContext : JsonSerializerContext
{
}
