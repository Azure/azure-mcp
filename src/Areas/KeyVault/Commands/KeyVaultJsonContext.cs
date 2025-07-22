// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.KeyVault.Commands.Certificate;
using AzureMcp.Areas.KeyVault.Commands.Key;
using AzureMcp.Areas.KeyVault.Commands.Secret;

namespace AzureMcp.Commands.KeyVault;

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
