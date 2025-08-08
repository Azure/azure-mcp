// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.MySql.Options;

public class BaseMySqlOptions : SubscriptionOptions
{
    [JsonPropertyName(MySqlOptionDefinitions.UserName)]
    public string? User { get; set; }

    [JsonPropertyName(MySqlOptionDefinitions.ServerName)]
    public string? Server { get; set; }

    [JsonPropertyName(MySqlOptionDefinitions.DatabaseName)]
    public string? Database { get; set; }
}
