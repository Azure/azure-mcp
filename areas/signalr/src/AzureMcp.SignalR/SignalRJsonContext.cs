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
[JsonSerializable(typeof(SignalRRuntimeModel))]
[JsonSerializable(typeof(SignalRKeyModel))]
[JsonSerializable(typeof(SignalRIdentityModel))]
[JsonSerializable(typeof(SignalRNetworkAclModel))]
[JsonSerializable(typeof(SignalRNetworkRuleModel))]
[JsonSerializable(typeof(SignalRPrivateEndpointModel))]
[JsonSerializable(typeof(UserAssignedIdentity))]
[JsonSerializable(typeof(IEnumerable<SignalRRuntimeModel>))]
[JsonSerializable(typeof(IEnumerable<SignalRPrivateEndpointModel>))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public partial class SignalRJsonContext : JsonSerializerContext
{
}
