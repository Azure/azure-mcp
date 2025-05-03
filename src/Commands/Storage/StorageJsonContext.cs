
using System.Text.Json.Serialization;
using AzureMcp.Commands.Storage.Blob;
using AzureMcp.Commands.Storage.Blob.Container;

namespace AzureMcp.Commands.Storage;

[JsonSerializable(typeof(BlobListCommand.BlobListCommandResult))]
[JsonSerializable(typeof(Account.AccountListCommand.Result), TypeInfoPropertyName = "AccountListCommandResult")]
[JsonSerializable(typeof(Table.TableListCommand.TableListCommandResult))]
[JsonSerializable(typeof(ContainerListCommand.ContainerListCommandResult))]
[JsonSerializable(typeof(ContainerDetailsCommand.ContainerDetailsCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class StorageJsonContext : JsonSerializerContext
{
}
