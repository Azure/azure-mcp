// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Postgres.Options.Server;

public class SetParamOptions : BasePostgresOptions
{
    [JsonPropertyName(PostgresOptionDefinitions.ParamName)]
    public string? Param { get; set; }

    [JsonPropertyName(PostgresOptionDefinitions.ValueName)]
    public string? Value { get; set; }
}
