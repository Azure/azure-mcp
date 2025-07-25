// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Search.Commands.Index;
using AzureMcp.Search.Commands.Service;

namespace AzureMcp.Search.Commands;

[JsonSerializable(typeof(ServiceListCommand.ServiceListCommandResult))]
[JsonSerializable(typeof(IndexListCommand.IndexListCommandResult))]
[JsonSerializable(typeof(IndexDescribeCommand.IndexDescribeCommandResult))]
[JsonSerializable(typeof(List<JsonElement>))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class SearchJsonContext : JsonSerializerContext
{
    // This class is generated at runtime by the source generator.
}
