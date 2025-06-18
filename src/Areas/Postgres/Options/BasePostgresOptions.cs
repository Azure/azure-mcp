// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;
using AzureMcp.Options;

namespace AzureMcp.Areas.Postgres.Options;

public class BasePostgresOptions : SubscriptionOptions
{
    [JsonPropertyName(OptionDefinitions.Postgres.UserName)]
    public string? User { get; set; }

    [JsonPropertyName(OptionDefinitions.Postgres.ServerName)]
    public string? Server { get; set; }

    [JsonPropertyName(OptionDefinitions.Postgres.DatabaseName)]
    public string? Database { get; set; }
}
