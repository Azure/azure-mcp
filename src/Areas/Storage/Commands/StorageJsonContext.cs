// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Storage.Commands;

[JsonSerializable(typeof(Blob.BlobListCommand.BlobListCommandResult))]
[JsonSerializable(typeof(Account.AccountListCommand.Result), TypeInfoPropertyName = "AccountListCommandResult")]
[JsonSerializable(typeof(Table.TableListCommand.TableListCommandResult))]
[JsonSerializable(typeof(Blob.Container.ContainerListCommand.ContainerListCommandResult))]
[JsonSerializable(typeof(Blob.Container.ContainerDetailsCommand.ContainerDetailsCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class StorageJsonContext : JsonSerializerContext
{
}
