// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Options;

namespace AzureMcp.Areas.Sql.Options;

public class BaseSqlOptions : SubscriptionOptions
{
    [JsonPropertyName(SqlOptionDefinitions.ServerName)]
    public string? Server { get; set; }
}
