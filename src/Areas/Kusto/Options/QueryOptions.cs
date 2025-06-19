// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Kusto.Options;

public class QueryOptions : BaseDatabaseOptions
{
    [JsonPropertyName(KustoOptionDefinitions.QueryText)]
    public string? Query { get; set; }
}
