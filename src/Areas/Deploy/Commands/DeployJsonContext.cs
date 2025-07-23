// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Deploy.Models;
using AzureMcp.Areas.Deploy.Options;

namespace AzureMcp.Areas.Deploy.Commands;

[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    PropertyNameCaseInsensitive = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
)]
[JsonSerializable(typeof(AppTopology))]
[JsonSerializable(typeof(MermaidData))]
[JsonSerializable(typeof(MermaidConfig))]
[JsonSerializable(typeof(List<string>))]
internal sealed partial class DeployJsonContext : JsonSerializerContext
{
}
