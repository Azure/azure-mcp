// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Kusto;

public class QueryArguments : BaseDatabaseArguments
{
    [JsonPropertyName(ArgumentDefinitions.Kusto.QueryText)]
    public string? Query { get; set; }
}
