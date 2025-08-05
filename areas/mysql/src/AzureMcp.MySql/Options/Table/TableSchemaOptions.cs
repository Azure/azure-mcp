// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.MySql.Options.Table;

public class TableSchemaOptions : BaseMySqlOptions
{
    [JsonPropertyName(MySqlOptionDefinitions.TableName)]
    public string? Table { get; set; }
}
