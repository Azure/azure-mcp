// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Monitor;

public class LogQueryOptions : WorkspaceOptions
{
    public string? Query { get; set; }
    public int? Hours { get; set; }
    public int? Limit { get; set; }
    [JsonPropertyName(OptionDefinitions.Monitor.TableNameName)]
    public string? TableName { get; set; }
}
