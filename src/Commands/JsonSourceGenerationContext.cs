// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using AzureMcp.Areas.Extension.Commands;
using AzureMcp.Areas.Group.Commands;
using AzureMcp.Commands;

namespace AzureMcp;

[JsonSerializable(typeof(GroupListCommand.Result))]
[JsonSerializable(typeof(BaseCommand.ExceptionResult))]
[JsonSerializable(typeof(JsonElement))]
[JsonSerializable(typeof(List<string>))]
[JsonSerializable(typeof(List<JsonNode>))]
[JsonSerializable(typeof(AzureCredentials))]
[JsonSerializable(typeof(AzqrReportResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class JsonSourceGenerationContext : JsonSerializerContext
{

}
