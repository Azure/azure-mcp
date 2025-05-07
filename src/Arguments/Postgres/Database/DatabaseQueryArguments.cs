// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Postgres.Database;

public class DatabaseQueryArguments : BasePostgresArguments
{
    [JsonPropertyName(ArgumentDefinitions.Postgres.QueryText)]
    public string? Query { get; set; }

}
