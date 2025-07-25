// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Grafana.Commands.Workspace;

namespace AzureMcp.Grafana.Commands;

[JsonSerializable(typeof(WorkspaceListCommand.WorkspaceListCommandResult))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault)]
internal sealed partial class GrafanaJsonContext : JsonSerializerContext;
