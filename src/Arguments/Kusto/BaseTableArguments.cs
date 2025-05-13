// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Kusto;

public class BaseTableArguments : BaseDatabaseArguments
{
    [JsonPropertyName(ArgumentDefinitions.Kusto.TableName)]
    public string? Table { get; set; }
}
