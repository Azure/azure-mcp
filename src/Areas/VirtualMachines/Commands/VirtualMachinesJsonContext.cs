// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.VirtualMachines.Commands.Vm;
using AzureMcp.Areas.VirtualMachines.Services;

namespace AzureMcp.Areas.VirtualMachines.Commands;

[JsonSerializable(typeof(VmListCommand.VmListCommandResult))]
[JsonSerializable(typeof(VirtualMachineInfo))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    WriteIndented = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
internal partial class VirtualMachinesJsonContext : JsonSerializerContext
{
}