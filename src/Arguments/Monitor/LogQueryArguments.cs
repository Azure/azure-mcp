// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Monitor;

public class LogQueryArguments : WorkspaceArguments
{
    public string? Query { get; set; }
    public int? Hours { get; set; }
    public int? Limit { get; set; }
    [JsonPropertyName(ArgumentDefinitions.Monitor.TableNameName)]
    public string? TableName { get; set; }
}
