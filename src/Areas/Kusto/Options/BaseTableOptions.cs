// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Kusto.Options;

public class BaseTableOptions : BaseDatabaseOptions
{
    [JsonPropertyName(KustoOptionDefinitions.TableName)]
    public string? Table { get; set; }
}
