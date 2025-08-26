// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.MySql.Options;

namespace AzureMcp.MySql.Options.Table;

public class TableSchemaGetOptions : MySqlDatabaseOptions
{
    [JsonPropertyName(MySqlOptionDefinitions.TableName)]
    public string? Table { get; set; }
}
