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
[JsonSerializable(typeof(ServerConfigGetCommand.ServerConfigGetCommandResult))]
[JsonSerializable(typeof(ServerParamGetCommand.ServerParamGetCommandResult))]
[JsonSerializable(typeof(ServerParamSetCommand.ServerParamSetCommandResult))]
[JsonSerializable(typeof(ServerListCommand.ServerListCommandResult))]
[JsonSerializable(typeof(TableSchemaGetCommand.TableSchemaGetCommandResult))]
[JsonSerializable(typeof(TableListCommand.TableListCommandResult))]
[JsonSerializable(typeof(MySqlService.ServerConfigGetResult))]

internal sealed partial class MySqlJsonContext : JsonSerializerContext
{
}
