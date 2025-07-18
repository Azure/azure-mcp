// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Storage.Commands.Account;
using AzureMcp.Areas.Storage.Commands.Blob;
using AzureMcp.Areas.Storage.Commands.Blob.Container;
using AzureMcp.Areas.Storage.Commands.DataLake.FileSystem;
using AzureMcp.Areas.Storage.Commands.Table;

namespace AzureMcp.Commands.Storage;

[JsonSerializable(typeof(BlobListCommand.BlobListCommandResult))]
[JsonSerializable(typeof(AccountListCommand.Result), TypeInfoPropertyName = "AccountListCommandResult")]
[JsonSerializable(typeof(TableListCommand.TableListCommandResult))]
[JsonSerializable(typeof(ContainerListCommand.ContainerListCommandResult))]
[JsonSerializable(typeof(ContainerDetailsCommand.ContainerDetailsCommandResult))]
[JsonSerializable(typeof(FileSystemListPathsCommand.FileSystemListPathsCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class StorageJsonContext : JsonSerializerContext
{
}
