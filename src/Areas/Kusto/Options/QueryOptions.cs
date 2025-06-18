// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Kusto;

public class QueryOptions : BaseDatabaseOptions
{
    [JsonPropertyName(OptionDefinitions.Kusto.QueryText)]
    public string? Query { get; set; }
}
