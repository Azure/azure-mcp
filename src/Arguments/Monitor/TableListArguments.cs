// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Monitor;

public class TableListArguments : BaseMonitorArguments, IWorkspaceArguments
{
    [JsonPropertyName(ArgumentDefinitions.Monitor.WorkspaceIdOrName)]
    public string? Workspace { get; set; }

    [JsonPropertyName(ArgumentDefinitions.Monitor.TableTypeName)]
    public string? TableType { get; set; }
}
