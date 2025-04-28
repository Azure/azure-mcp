
using AzureMcp.Commands;
using AzureMcp.Commands.AppConfig.Account;
using AzureMcp.Commands.AppConfig.KeyValue;
using AzureMcp.Commands.Cosmos;
using AzureMcp.Commands.Group;
using AzureMcp.Commands.Monitor.Workspace;
using AzureMcp.Commands.Storage.Account;
using AzureMcp.Commands.Storage.Blob;
using AzureMcp.Commands.Storage.Blob.Container;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Command;
using AzureMcp.Services.ProcessExecution;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace AzureMcp;

[JsonSerializable(typeof(ExternalProcessService.ParseError))]
[JsonSerializable(typeof(ExternalProcessService.ParseOutput))]
[JsonSerializable(typeof(GroupListCommand.Result))]
[JsonSerializable(typeof(BaseCommand.ExceptionResult))]
[JsonSerializable(typeof(KeyValueUnlockCommand.KeyValueUnlockResult))]
[JsonSerializable(typeof(List<CommandInfo>))]
[JsonSerializable(typeof(KeyValueShowCommand.KeyValueShowResult))]
[JsonSerializable(typeof(Commands.AppConfig.Account.AccountListCommand.AccountListCommandResult), TypeInfoPropertyName = "AccountListCommandResult")]
[JsonSerializable(typeof(Commands.Cosmos.AccountListCommand.AccountListCommandResult), TypeInfoPropertyName = "CosmosAccountListCommandResult")]
[JsonSerializable(typeof(Commands.Storage.Account.AccountListCommand.Result), TypeInfoPropertyName = "StorageAccountListCommandResult")]
[JsonSerializable(typeof(SubscriptionListCommand.SubscriptionListCommandResult))]
[JsonSerializable(typeof(KeyValueSetCommand.KeyValueSetCommandResult))]
[JsonSerializable(typeof(Commands.Storage.Table.TableListCommand.TableListCommandResult), TypeInfoPropertyName = "StorageTableListCommandResult")]
[JsonSerializable(typeof(Commands.Monitor.Table.TableListCommand.TableListCommandResult), TypeInfoPropertyName = "MonitorTableListCommandResult")]
[JsonSerializable(typeof(KeyValueLockCommand.KeyValueLockCommandResult))]
[JsonSerializable(typeof(KeyValueListCommand.KeyValueListCommandResult))]
[JsonSerializable(typeof(BlobListCommand.BlobListCommandResult))]
[JsonSerializable(typeof(WorkspaceListCommand.WorkspaceListCommandResult))]
[JsonSerializable(typeof(DatabaseListCommand.DatabaseListCommandResult))]
[JsonSerializable(typeof(Commands.Storage.Blob.Container.ContainerListCommand.ContainerListCommandResult), TypeInfoPropertyName = "StorageContainerListCommandResult")]
[JsonSerializable(typeof(Commands.Cosmos.ContainerListCommand.ContainerListCommandResult), TypeInfoPropertyName = "CosmosContainerListCommandResult")]
[JsonSerializable(typeof(KeyValueDeleteCommand.KeyValueDeleteCommandResult))]
[JsonSerializable(typeof(ContainerDetailsCommand.ContainerDetailsCommandResult))]
[JsonSerializable(typeof(ItemQueryCommand.ItemQueryCommandResult))]
[JsonSerializable(typeof(JsonElement))]
[JsonSerializable(typeof(List<string>))]
[JsonSerializable(typeof(List<JsonNode>))]
[JsonSerializable(typeof(CommandResponse))]
[JsonSerializable(typeof(AzureMcp.Models.ETag), TypeInfoPropertyName = "McpETag")]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class JsonSourceGenerationContext : JsonSerializerContext
{

}