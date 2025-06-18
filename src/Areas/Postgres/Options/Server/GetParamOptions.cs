// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Postgres.Server;

public class GetParamOptions : BasePostgresOptions
{
    [JsonPropertyName(OptionDefinitions.Postgres.ParamName)]
    public string? Param { get; set; }
}
