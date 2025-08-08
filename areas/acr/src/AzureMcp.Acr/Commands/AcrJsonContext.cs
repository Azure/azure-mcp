// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Acr.Commands.Registry;

namespace AzureMcp.Acr.Commands;

[JsonSerializable(typeof(RegistryListCommand.RegistryListCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class AcrJsonContext : JsonSerializerContext
{
}
