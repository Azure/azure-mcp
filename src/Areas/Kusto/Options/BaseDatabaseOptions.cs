// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.Kusto.Options;

public class BaseDatabaseOptions : BaseClusterOptions
{
    [JsonPropertyName(OptionDefinitions.Kusto.DatabaseName)]
    public string? Database { get; set; }
}
