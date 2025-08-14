// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using Microsoft.Mcp.Core.Models.Command;
using Microsoft.Mcp.Core.Models.Option;

namespace Microsoft.Mcp.Core.Models;

[JsonSerializable(typeof(CommandInfo))]
[JsonSerializable(typeof(CommandResponse))]
[JsonSerializable(typeof(OptionInfo))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
public partial class ModelsJsonContext : JsonSerializerContext
{
}