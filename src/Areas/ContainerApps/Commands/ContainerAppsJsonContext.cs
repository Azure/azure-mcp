// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.ContainerApps.Models;

namespace AzureMcp.Areas.ContainerApps.Commands;

[JsonSerializable(typeof(Models.ContainerRegistry))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class ContainerAppsJsonContext : JsonSerializerContext
{
}
