// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.KeyVault.Commands.Certificate;
using AzureMcp.KeyVault.Commands.Key;
using AzureMcp.KeyVault.Commands.Secret;

namespace AzureMcp.KeyVault.Commands;

[JsonSerializable(typeof(CertificateCreateCommand.CertificateCreateCommandResult))]
[JsonSerializable(typeof(CertificateGetCommand.CertificateGetCommandResult))]
[JsonSerializable(typeof(CertificateListCommand.CertificateListCommandResult))]
[JsonSerializable(typeof(KeyCreateCommand.KeyCreateCommandResult))]
[JsonSerializable(typeof(KeyGetCommand.KeyGetCommandResult))]
[JsonSerializable(typeof(KeyListCommand.KeyListCommandResult))]
[JsonSerializable(typeof(SecretCreateCommand.SecretCreateCommandResult))]
[JsonSerializable(typeof(SecretGetCommand.SecretGetCommandResult))]
[JsonSerializable(typeof(SecretListCommand.SecretListCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class KeyVaultJsonContext : JsonSerializerContext
{
}
