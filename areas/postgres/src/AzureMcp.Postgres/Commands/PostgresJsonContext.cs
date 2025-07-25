// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Postgres.Commands.Database;
using AzureMcp.Postgres.Commands.Server;
using AzureMcp.Postgres.Commands.Table;

namespace AzureMcp.Postgres.Commands;

[JsonSerializable(typeof(DatabaseListCommand.DatabaseListCommandResult))]
[JsonSerializable(typeof(DatabaseQueryCommand.DatabaseQueryCommandResult))]
[JsonSerializable(typeof(GetConfigCommand.GetConfigCommandResult))]
[JsonSerializable(typeof(GetParamCommand.GetParamCommandResult))]
[JsonSerializable(typeof(SetParamCommand.SetParamCommandResult))]
[JsonSerializable(typeof(ServerListCommand.ServerListCommandResult))]
[JsonSerializable(typeof(GetSchemaCommand.GetSchemaCommandResult))]
[JsonSerializable(typeof(TableListCommand.TableListCommandResult))]

internal sealed partial class PostgresJsonContext : JsonSerializerContext
{
}
