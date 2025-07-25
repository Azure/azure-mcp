// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Postgres.Options.Database;

public class DatabaseQueryOptions : BasePostgresOptions
{
    [JsonPropertyName(PostgresOptionDefinitions.QueryText)]
    public string? Query { get; set; }

}
