// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Acr.Commands.Registry;

namespace AzureMcp.Acr.Commands;

[JsonSerializable(typeof(RegistryListCommand.RegistryListCommandResult))]
[JsonSerializable(typeof(RegistryRepositoryListCommand.RegistryRepositoryListCommandResult))]
[JsonSerializable(typeof(Models.AcrRegistryInfo))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class AcrJsonContext : JsonSerializerContext
{
}
