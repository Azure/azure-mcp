// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Arguments.AppConfig.KeyValue;

public class KeyValueSetArguments : BaseKeyValueArguments
{
    [JsonPropertyName(ArgumentDefinitions.AppConfig.ValueName)]
    public string? Value { get; set; }
}