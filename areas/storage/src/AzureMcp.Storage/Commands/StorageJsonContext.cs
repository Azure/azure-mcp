// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Storage.Commands.Account;
using AzureMcp.Storage.Commands.Blob;
using AzureMcp.Storage.Commands.Blob.Batch;
using AzureMcp.Storage.Commands.Blob.Container;
using AzureMcp.Storage.Commands.DataLake.Directory;
using AzureMcp.Storage.Commands.DataLake.FileSystem;
using AzureMcp.Storage.Commands.Queue.Message;
using AzureMcp.Storage.Commands.Share.File;
using AzureMcp.Storage.Commands.Table;
using AzureMcp.Storage.Models;

namespace AzureMcp.Storage.Commands;

[JsonSerializable(typeof(AccountCreateCommand.AccountCreateCommandResult), TypeInfoPropertyName = "AccountCreateCommandResult")]
[JsonSerializable(typeof(AccountDetailsCommand.AccountDetailsCommandResult), TypeInfoPropertyName = "AccountDetailsCommandResult")]
[JsonSerializable(typeof(AccountListCommand.AccountListCommandResult), TypeInfoPropertyName = "AccountListCommandResult")]
[JsonSerializable(typeof(BatchSetTierCommand.BatchSetTierCommandResult))]
[JsonSerializable(typeof(BlobDetailsCommand.BlobDetailsCommandResult))]
[JsonSerializable(typeof(BlobListCommand.BlobListCommandResult))]
[JsonSerializable(typeof(BlobUploadResult))]
[JsonSerializable(typeof(ContainerCreateCommand.ContainerCreateCommandResult))]
[JsonSerializable(typeof(ContainerDetailsCommand.ContainerDetailsCommandResult))]
[JsonSerializable(typeof(ContainerListCommand.ContainerListCommandResult))]
[JsonSerializable(typeof(DataLakePathInfo))]
[JsonSerializable(typeof(DirectoryCreateCommand.DirectoryCreateCommandResult))]
[JsonSerializable(typeof(FileListCommand.FileListCommandResult))]
[JsonSerializable(typeof(FileShareItemInfo))]
[JsonSerializable(typeof(FileSystemListPathsCommand.FileSystemListPathsCommandResult))]
[JsonSerializable(typeof(QueueMessageSendCommand.QueueMessageSendCommandResult))]
[JsonSerializable(typeof(QueueMessageSendResult))]
[JsonSerializable(typeof(StorageAccountInfo))]
[JsonSerializable(typeof(TableListCommand.TableListCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class StorageJsonContext : JsonSerializerContext
{
}
