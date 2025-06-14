// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;
using AzureMcp.Options.Sql.Server;

namespace AzureMcp.Options.Sql.Database;

public class BaseDatabaseOptions : BaseServerOptions
{
    [JsonPropertyName(OptionDefinitions.Sql.DatabaseName)]
    public string? Database { get; set; }
}
