using System.Text.Json.Serialization;
using AzureMcp.Commands.Monitor.TableType;
using AzureMcp.Commands.Monitor.Workspace;

namespace AzureMcp.Commands.Monitor;

[JsonSerializable(typeof(WorkspaceListCommand.WorkspaceListCommandResult))]
[JsonSerializable(typeof(Table.TableListCommand.TableListCommandResult))]
[JsonSerializable(typeof(TableTypeListCommand.TableTypeListCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class MonitorJsonContext : JsonSerializerContext
{
}
