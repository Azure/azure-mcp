// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.MySql.Options.Server;

public class ServerSetParamOptions : BaseMySqlOptions
{
    [JsonPropertyName(MySqlOptionDefinitions.ParamName)]
    public string? Param { get; set; }

    [JsonPropertyName(MySqlOptionDefinitions.ValueName)]
    public string? Value { get; set; }
}
