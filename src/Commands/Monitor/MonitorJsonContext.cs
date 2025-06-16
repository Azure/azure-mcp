// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Commands.Monitor.ApplicationInsights;
using AzureMcp.Commands.Monitor.TableType;
using AzureMcp.Commands.Monitor.Workspace;

namespace AzureMcp.Commands.Monitor;

[JsonSerializable(typeof(WorkspaceListCommand.WorkspaceListCommandResult))]
[JsonSerializable(typeof(Table.TableListCommand.TableListCommandResult))]
[JsonSerializable(typeof(TableTypeListCommand.TableTypeListCommandResult))]
[JsonSerializable(typeof(AppDiagnoseCommand.AppDiagnoseCommandResult))]
[JsonSerializable(typeof(AppTraceCommand.AppTraceCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class MonitorJsonContext : JsonSerializerContext
{
}
