// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Monitor.Options;

public class TableListOptions : WorkspaceOptions
{
    [JsonPropertyName(MonitorOptionDefinitions.TableTypeName)]
    public string? TableType { get; set; }
}
