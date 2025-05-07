using System.Text.Json.Serialization;
using AzureMcp.Commands.Postgres.Database;
using AzureMcp.Commands.Postgres.Server;
using AzureMcp.Commands.Postgres.Table;

namespace AzureMcp.Commands.Postgres;

[JsonSerializable(typeof(DatabaseListCommand.DatabaseListCommandResult))]
[JsonSerializable(typeof(DatabaseQueryCommand.DatabaseQueryCommandResult))]
[JsonSerializable(typeof(GetConfigCommand.GetConfigCommandResult))]
[JsonSerializable(typeof(GetParamCommand.GetParamCommandResult))]
[JsonSerializable(typeof(ServerListCommand.ServerListCommandResult))]
[JsonSerializable(typeof(GetSchemaCommand.GetSchemaCommandResult))]
[JsonSerializable(typeof(TableListCommand.TableListCommandResult))]

internal sealed partial class PostgresJsonContext : JsonSerializerContext
{
}
