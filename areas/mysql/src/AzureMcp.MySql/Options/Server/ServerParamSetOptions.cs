// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.MySql.Options;

namespace AzureMcp.MySql.Options.Server;

public class ServerParamSetOptions : MySqlServerOptions
{
    [JsonPropertyName(MySqlOptionDefinitions.ParamName)]
    public string? Param { get; set; }

    [JsonPropertyName(MySqlOptionDefinitions.ValueName)]
    public string? Value { get; set; }
}
