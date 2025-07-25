// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using AzureMcp.Monitor.Commands.Metrics;
using AzureMcp.Monitor.Commands.Table;
using AzureMcp.Monitor.Commands.TableType;
using AzureMcp.Monitor.Commands.Workspace;

namespace AzureMcp.Monitor.Commands;

[JsonSerializable(typeof(List<JsonNode>))]
[JsonSerializable(typeof(WorkspaceListCommand.WorkspaceListCommandResult))]
[JsonSerializable(typeof(TableListCommand.TableListCommandResult))]
[JsonSerializable(typeof(TableTypeListCommand.TableTypeListCommandResult))]
[JsonSerializable(typeof(MetricsQueryCommand.MetricsQueryCommandResult))]
[JsonSerializable(typeof(MetricsDefinitionsCommand.MetricsDefinitionsCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal sealed partial class MonitorJsonContext : JsonSerializerContext
{
}
