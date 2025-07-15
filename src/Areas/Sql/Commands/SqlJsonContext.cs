// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Sql.Commands.Database;
using AzureMcp.Areas.Sql.Commands.ElasticPool;
using AzureMcp.Areas.Sql.Commands.EntraAdmin;
using AzureMcp.Areas.Sql.Models;

namespace AzureMcp.Areas.Sql.Commands;

[JsonSerializable(typeof(DatabaseShowCommand.DatabaseShowResult))]
[JsonSerializable(typeof(EntraAdminListCommand.EntraAdminListResult))]
[JsonSerializable(typeof(ElasticPoolListCommand.ElasticPoolListResult))]
[JsonSerializable(typeof(SqlDatabase))]
[JsonSerializable(typeof(SqlServerEntraAdministrator))]
[JsonSerializable(typeof(SqlElasticPool))]
[JsonSerializable(typeof(DatabaseSku))]
[JsonSerializable(typeof(ElasticPoolSku))]
[JsonSerializable(typeof(ElasticPoolPerDatabaseSettings))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    WriteIndented = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
internal partial class SqlJsonContext : JsonSerializerContext;
