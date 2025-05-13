// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Postgres;

public class BasePostgresArguments : SubscriptionArguments
{
    [JsonPropertyName(ArgumentDefinitions.Postgres.UserName)]
    public string? User { get; set; }

    [JsonPropertyName(ArgumentDefinitions.Postgres.ServerName)]
    public string? Server { get; set; }

    [JsonPropertyName(ArgumentDefinitions.Postgres.DatabaseName)]
    public string? Database { get; set; }
}
