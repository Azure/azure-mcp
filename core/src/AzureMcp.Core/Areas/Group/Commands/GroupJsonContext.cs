// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Core.Areas.Group.Commands;

[JsonSerializable(typeof(GroupListCommand.GroupListCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class GroupJsonContext : JsonSerializerContext
{

}
