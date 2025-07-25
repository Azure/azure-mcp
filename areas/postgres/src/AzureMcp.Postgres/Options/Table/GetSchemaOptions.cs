// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Postgres.Options.Table;

public class GetSchemaOptions : BasePostgresOptions
{
    [JsonPropertyName(PostgresOptionDefinitions.TableName)]
    public string? Table { get; set; }
}
