// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Server.Models;

namespace AzureMcp.Models.Server;

[JsonSerializable(typeof(RegistryRoot))]
[JsonSerializable(typeof(Dictionary<string, RegistryServerInfo>))]
[JsonSerializable(typeof(RegistryServerInfo))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    WriteIndented = false
)]
public partial class RegistryJsonContext : JsonSerializerContext
{
}
