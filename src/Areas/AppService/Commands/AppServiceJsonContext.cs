// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.AppService.Commands.Database;
using AzureMcp.Areas.AppService.Models;
using static AzureMcp.Areas.AppService.Commands.Database.DatabaseAddCommand;

namespace AzureMcp.Areas.AppService.Commands;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(DatabaseAddCommandResult))]
[JsonSerializable(typeof(DatabaseConnectionInfo))]
public partial class AppServiceJsonContext : JsonSerializerContext
{
}
