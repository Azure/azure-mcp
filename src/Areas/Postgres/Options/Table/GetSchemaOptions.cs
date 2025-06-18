// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Postgres.Table;

public class GetSchemaOptions : BasePostgresOptions
{
    [JsonPropertyName(OptionDefinitions.Postgres.TableName)]
    public string? Table { get; set; }
}
