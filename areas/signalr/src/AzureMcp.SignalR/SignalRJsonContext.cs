// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.SignalR.Commands.CustomCertificate;
using AzureMcp.SignalR.Commands.CustomDomain;
using AzureMcp.SignalR.Commands.Key;
using AzureMcp.SignalR.Commands.SignalR;
using AzureMcp.SignalR.Models;

namespace AzureMcp.SignalR;

/// <summary>
/// JSON serialization context for Azure SignalR Service commands.
/// </summary>
[JsonSerializable(typeof(SignalRServiceListCommand.SignalRServiceListCommandResult))]
[JsonSerializable(typeof(SignalRShowCommand.SignalRShowCommandResult))]
[JsonSerializable(typeof(CustomCertificateShowCommand.CertificateShowCommandResult))]
[JsonSerializable(typeof(CustomCertificateListCommand.CertificateListCommandResult))]
[JsonSerializable(typeof(CustomDomainListCommand.CustomDomainListCommandResult))]
[JsonSerializable(typeof(CustomDomainShowCommand.CustomDomainShowCommandResult))]
[JsonSerializable(typeof(KeyListCommand.KeyListCommandResult))]
[JsonSerializable(typeof(SignalRServiceModel))]
[JsonSerializable(typeof(SignalRCustomCertificateModel))]
[JsonSerializable(typeof(SignalRCustomDomainModel))]
[JsonSerializable(typeof(SignalRKeyModel))]
[JsonSerializable(typeof(IEnumerable<SignalRServiceModel>))]
[JsonSerializable(typeof(List<SignalRCustomDomainModel>))]
[JsonSerializable(typeof(List<SignalRCustomCertificateModel>))]
public partial class SignalRJsonContext : JsonSerializerContext
{
}
