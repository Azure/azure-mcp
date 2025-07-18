// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Group.Commands;

[JsonSerializable(typeof(GroupListCommand.Result))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class GroupJsonContext : JsonSerializerContext
{

}
