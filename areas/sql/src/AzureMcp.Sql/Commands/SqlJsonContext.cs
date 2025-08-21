// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Sql.Commands.Database;
using AzureMcp.Sql.Commands.ElasticPool;
using AzureMcp.Sql.Commands.EntraAdmin;
using AzureMcp.Sql.Commands.FirewallRule;
using AzureMcp.Sql.Models;
using AzureMcp.Sql.Services.Models;

namespace AzureMcp.Sql.Commands;

[JsonSerializable(typeof(DatabaseShowCommand.DatabaseShowResult))]
[JsonSerializable(typeof(DatabaseListCommand.DatabaseListResult))]
[JsonSerializable(typeof(EntraAdminListCommand.EntraAdminListResult))]
[JsonSerializable(typeof(FirewallRuleListCommand.FirewallRuleListResult))]
[JsonSerializable(typeof(ElasticPoolListCommand.ElasticPoolListResult))]
[JsonSerializable(typeof(SqlDatabase))]
[JsonSerializable(typeof(SqlServerEntraAdministrator))]
[JsonSerializable(typeof(SqlServerFirewallRule))]
[JsonSerializable(typeof(SqlElasticPool))]
[JsonSerializable(typeof(DatabaseSku))]
[JsonSerializable(typeof(ElasticPoolSku))]
[JsonSerializable(typeof(ElasticPoolPerDatabaseSettings))]
[JsonSerializable(typeof(SqlDatabaseData))]
[JsonSerializable(typeof(SqlDatabaseProperties))]
[JsonSerializable(typeof(SqlServerAadAdministratorData))]
[JsonSerializable(typeof(SqlElasticPoolData))]
[JsonSerializable(typeof(SqlElasticPoolProperties))]
[JsonSerializable(typeof(SqlElasticPoolPerDatabaseSettings))]
[JsonSerializable(typeof(SqlFirewallRuleData))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    WriteIndented = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
internal partial class SqlJsonContext : JsonSerializerContext;
