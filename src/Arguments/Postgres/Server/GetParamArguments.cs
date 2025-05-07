// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Postgres.Server;

public class GetParamArguments : BasePostgresArguments
{
    [JsonPropertyName(ArgumentDefinitions.Postgres.ParamName)]
    public string? Param { get; set; }
}
