// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Kusto.Options;

public class BaseDatabaseOptions : BaseClusterOptions
{
    [JsonPropertyName(KustoOptionDefinitions.DatabaseName)]
    public string? Database { get; set; }
}
