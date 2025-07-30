// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.AppService.Commands;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(DatabaseAddCommandResult))]
[JsonSerializable(typeof(DatabaseConnectionInfo))]
public partial class AppServiceJsonContext : JsonSerializerContext;
