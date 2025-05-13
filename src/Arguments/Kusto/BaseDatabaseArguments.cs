// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Kusto;

public class BaseDatabaseArguments : BaseClusterArguments
{
    [JsonPropertyName(ArgumentDefinitions.Kusto.DatabaseName)]
    public string? Database { get; set; }
}
