// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Commands.KeyVault.Key;
using AzureMcp.Commands.KeyVault.Secret;

namespace AzureMcp.Commands.KeyVault;

[JsonSerializable(typeof(KeyListCommand.KeyListCommandResult))]
[JsonSerializable(typeof(KeyGetCommand.KeyGetCommandResult))]
[JsonSerializable(typeof(KeyCreateCommand.KeyCreateCommandResult))]
[JsonSerializable(typeof(SecretGetCommand.SecretGetCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class KeyVaultJsonContext : JsonSerializerContext
{
}
