// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Core.Models;

[JsonSerializable(typeof(List<CommandInfo>))]
[JsonSerializable(typeof(CommandResponse))]
[JsonSerializable(typeof(ETag), TypeInfoPropertyName = "McpETag")]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class ModelsJsonContext : JsonSerializerContext
{
    // This class is intentionally left empty. It is used for source generation of JSON serialization.
}
