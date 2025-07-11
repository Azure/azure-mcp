// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.ContainerApps.Commands.ContainerApp;
using AzureMcp.Areas.ContainerApps.Services;

namespace AzureMcp.Areas.ContainerApps.Commands;

[JsonSerializable(typeof(ContainerAppListCommand.ContainerAppListCommandResult))]
[JsonSerializable(typeof(ContainerAppInfo))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class ContainerAppsJsonContext : JsonSerializerContext;