// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.Monitor.Options;

public class ResourceLogQueryOptions : ResourceOptions
{
    public string? Query { get; set; }
    public int? Hours { get; set; }
    public int? Limit { get; set; }
    [JsonPropertyName(MonitorOptionDefinitions.TableNameName)]
    public string? TableName { get; set; }
}
