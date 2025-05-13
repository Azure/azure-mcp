// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Postgres.Table;

public class GetSchemaArguments : BasePostgresArguments
{
    [JsonPropertyName(ArgumentDefinitions.Postgres.TableName)]
    public string? Table { get; set; }
}
