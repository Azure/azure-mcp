// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.Monitor.Options;

public class TableListOptions : WorkspaceOptions
{
    [JsonPropertyName(MonitorOptionDefinitions.TableTypeName)]
    public string? TableType { get; set; }
}
