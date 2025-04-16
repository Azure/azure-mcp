// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Arguments.Monitor;

public class LogQueryArguments : BaseMonitorArguments, IWorkspaceArguments
{
    [JsonPropertyName(ArgumentDefinitions.Monitor.WorkspaceIdOrName)]
    public string? Workspace { get; set; }
    public string? Query { get; set; }
    public int? Hours { get; set; }
    public int? Limit { get; set; }
    [JsonPropertyName(ArgumentDefinitions.Monitor.TableNameName)]
    public string? TableName { get; set; }
}