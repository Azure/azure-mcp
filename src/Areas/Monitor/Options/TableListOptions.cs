// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Monitor;

public class TableListOptions : WorkspaceOptions
{
    [JsonPropertyName(OptionDefinitions.Monitor.TableTypeName)]
    public string? TableType { get; set; }
}
