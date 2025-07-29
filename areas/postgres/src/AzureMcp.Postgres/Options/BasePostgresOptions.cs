// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.Postgres.Options;

public class BasePostgresOptions : SubscriptionOptions
{
    [JsonPropertyName(PostgresOptionDefinitions.UserName)]
    public string? User { get; set; }

    [JsonPropertyName(PostgresOptionDefinitions.ServerName)]
    public string? Server { get; set; }

    [JsonPropertyName(PostgresOptionDefinitions.DatabaseName)]
    public string? Database { get; set; }
}
