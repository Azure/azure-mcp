// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Postgres.Options;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.Postgres.Options.Table;

public class GetSchemaOptions : BasePostgresOptions
{
    [JsonPropertyName(PostgresOptionDefinitions.TableName)]
    public string? Table { get; set; }
}
