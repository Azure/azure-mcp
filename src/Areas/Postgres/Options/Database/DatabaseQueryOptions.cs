// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Postgres.Database;

public class DatabaseQueryOptions : BasePostgresOptions
{
    [JsonPropertyName(OptionDefinitions.Postgres.QueryText)]
    public string? Query { get; set; }

}
