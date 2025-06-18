// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.Kusto.Options;

public class BaseTableOptions : BaseDatabaseOptions
{
    [JsonPropertyName(KustoOptionDefinitions.TableName)]
    public string? Table { get; set; }
}
