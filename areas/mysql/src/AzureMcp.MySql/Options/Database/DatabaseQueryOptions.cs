// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.MySql.Options.Database;

public class DatabaseQueryOptions : BaseMySqlOptions
{
    [JsonPropertyName(MySqlOptionDefinitions.QueryText)]
    public string? Query { get; set; }
}
