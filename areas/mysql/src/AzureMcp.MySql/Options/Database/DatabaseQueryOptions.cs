// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.MySql.Options;

namespace AzureMcp.MySql.Options.Database;

public class DatabaseQueryOptions : MySqlDatabaseOptions
{
    [JsonPropertyName(MySqlOptionDefinitions.QueryText)]
    public string? Query { get; set; }
}
