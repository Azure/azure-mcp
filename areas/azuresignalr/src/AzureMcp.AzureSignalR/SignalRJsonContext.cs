// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.AzureSignalR.Commands.Certificate;
using AzureMcp.AzureSignalR.Commands.SignalR;
using AzureMcp.AzureSignalR.Models;
using System.Text.Json.Serialization;

namespace AzureMcp.AzureSignalR;

/// <summary>
/// JSON serialization context for Azure SignalR Service commands.
/// </summary>
[JsonSerializable(typeof(SignalRServiceListCommand.SignalRServiceListCommandResult))]
[JsonSerializable(typeof(CertificateShowCommand.CertificateShowCommandResult))]
[JsonSerializable(typeof(SignalRServiceModel))]
[JsonSerializable(typeof(SignalRCertificateModel))]
[JsonSerializable(typeof(IEnumerable<SignalRServiceModel>))]
public partial class SignalRJsonContext : JsonSerializerContext
{
}
