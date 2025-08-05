// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.MySql.Commands.Database;
using AzureMcp.MySql.Commands.Server;
using AzureMcp.MySql.Commands.Table;
using AzureMcp.MySql.Services;

namespace AzureMcp.MySql.Json;

[JsonSerializable(typeof(DatabaseListCommand.DatabaseListCommandResult))]
[JsonSerializable(typeof(DatabaseQueryCommand.DatabaseQueryCommandResult))]
[JsonSerializable(typeof(ServerConfigCommand.ServerConfigCommandResult))]
[JsonSerializable(typeof(ServerParamCommand.ServerParamCommandResult))]
[JsonSerializable(typeof(ServerSetParamCommand.ServerSetParamCommandResult))]
[JsonSerializable(typeof(ServerListCommand.ServerListCommandResult))]
[JsonSerializable(typeof(TableSchemaCommand.TableSchemaCommandResult))]
[JsonSerializable(typeof(TableListCommand.TableListCommandResult))]
[JsonSerializable(typeof(MySqlService.ServerConfigResult))]

internal sealed partial class MySqlJsonContext : JsonSerializerContext
{
}
