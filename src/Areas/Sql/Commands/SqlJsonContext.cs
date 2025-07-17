// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Sql.Commands.Database;
using AzureMcp.Areas.Sql.Commands.EntraAdmin;
using AzureMcp.Areas.Sql.Commands.FirewallRule;
using AzureMcp.Areas.Sql.Models;

namespace AzureMcp.Areas.Sql.Commands;

[JsonSerializable(typeof(DatabaseShowCommand.DatabaseShowResult))]
[JsonSerializable(typeof(EntraAdminListCommand.EntraAdminListResult))]
[JsonSerializable(typeof(FirewallRuleListCommand.FirewallRuleListResult))]
[JsonSerializable(typeof(SqlDatabase))]
[JsonSerializable(typeof(SqlServerEntraAdministrator))]
[JsonSerializable(typeof(SqlServerFirewallRule))]
[JsonSerializable(typeof(DatabaseSku))]
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    WriteIndented = true,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
internal partial class SqlJsonContext : JsonSerializerContext;
