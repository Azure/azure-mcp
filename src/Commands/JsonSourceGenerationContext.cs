// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using AzureMcp.Commands;
using AzureMcp.Commands.Group;
using AzureMcp.Services.Azure.Arc;

namespace AzureMcp;

[JsonSerializable(typeof(GroupListCommand.Result))]
[JsonSerializable(typeof(BaseCommand.ExceptionResult))]
[JsonSerializable(typeof(JsonElement))]
[JsonSerializable(typeof(List<string>))]
[JsonSerializable(typeof(List<JsonNode>))]
[JsonSerializable(typeof(AzureCredentials))]
[JsonSerializable(typeof(DeploymentResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class JsonSourceGenerationContext : JsonSerializerContext
{

}
