// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Kusto;

public class BaseTableOptions : BaseDatabaseOptions
{
    [JsonPropertyName(OptionDefinitions.Kusto.TableName)]
    public string? Table { get; set; }
}
