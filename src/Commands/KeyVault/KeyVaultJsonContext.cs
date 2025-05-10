// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Commands.KeyVault.Key;

namespace AzureMcp.Commands.KeyVault;

[JsonSerializable(typeof(KeyListCommand.KeyListCommandResult))]
[JsonSerializable(typeof(KeyGetCommand.KeyGetCommandResult))]
[JsonSerializable(typeof(KeyCreateCommand.KeyCreateCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class KeyVaultJsonContext : JsonSerializerContext
{
}
