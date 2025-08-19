// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.AzureSignalR.Commands.Certificate;
using AzureMcp.AzureSignalR.Commands.CustomDomain;
using AzureMcp.AzureSignalR.Commands.Key;
using AzureMcp.AzureSignalR.Commands.SignalR;
using AzureMcp.AzureSignalR.Models;
using System.Text.Json.Serialization;

namespace AzureMcp.AzureSignalR;

/// <summary>
/// JSON serialization context for Azure SignalR Service commands.
/// </summary>
[JsonSerializable(typeof(SignalRServiceListCommand.SignalRServiceListCommandResult))]
[JsonSerializable(typeof(SignalRShowCommand.SignalRShowCommandResult))]
[JsonSerializable(typeof(CertificateShowCommand.CertificateShowCommandResult))]
[JsonSerializable(typeof(CertificateListCommand.CertificateListCommandResult))]
[JsonSerializable(typeof(CustomDomainListCommand.CustomDomainListCommandResult))]
[JsonSerializable(typeof(CustomDomainShowCommand.CustomDomainShowCommandResult))]
[JsonSerializable(typeof(KeyListCommand.KeyListCommandResult))]
[JsonSerializable(typeof(SignalRServiceModel))]
[JsonSerializable(typeof(SignalRCertificateModel))]
[JsonSerializable(typeof(SignalRCustomDomainModel))]
[JsonSerializable(typeof(SignalRKeyModel))]
[JsonSerializable(typeof(IEnumerable<SignalRServiceModel>))]
[JsonSerializable(typeof(List<SignalRCustomDomainModel>))]
[JsonSerializable(typeof(List<SignalRCertificateModel>))]
public partial class AzureSignalRJsonContext : JsonSerializerContext
{
}
