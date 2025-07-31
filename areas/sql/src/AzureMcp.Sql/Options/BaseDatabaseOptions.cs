// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Sql.Options;

public class BaseDatabaseOptions : BaseSqlOptions
{
    [JsonPropertyName(SqlOptionDefinitions.DatabaseName)]
    public string? Database { get; set; }
}
