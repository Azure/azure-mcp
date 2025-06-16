// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Commands.Sql.Db;
using AzureMcp.Commands.Sql.Server;

namespace AzureMcp.Commands.Sql;

[JsonSerializable(typeof(DbAdviseCommand.DbAdviseCommandResult))]
[JsonSerializable(typeof(ServerListCommand.ServerListCommandResult))]
[JsonSerializable(typeof(DbListCommand.DbListCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class SqlJsonContext : JsonSerializerContext;
