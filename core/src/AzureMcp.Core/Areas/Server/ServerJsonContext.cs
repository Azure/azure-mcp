// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Server.Models;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Server;

[JsonSerializable(typeof(RegistryRoot))]
[JsonSerializable(typeof(Dictionary<string, RegistryServerInfo>))]
[JsonSerializable(typeof(RegistryServerInfo))]
[JsonSerializable(typeof(ListToolsResult))]
[JsonSerializable(typeof(IList<McpClientTool>))]
[JsonSerializable(typeof(Dictionary<string, object?>))]
[JsonSerializable(typeof(JsonElement))]
[JsonSerializable(typeof(Tool))]
[JsonSerializable(typeof(List<Tool>))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
    WriteIndented = true
)]
internal sealed partial class ServerJsonContext : JsonSerializerContext
{
}
