// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.VirtualDesktop.Commands.Hostpool;
using AzureMcp.Areas.VirtualDesktop.Commands.SessionHost;

namespace AzureMcp.Areas.VirtualDesktop.Commands;

[JsonSerializable(typeof(HostpoolListCommand.HostPoolListCommandResult))]
[JsonSerializable(typeof(SessionHostListCommand.SessionHostListCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class VirtualDesktopJsonContext : JsonSerializerContext
{
}
