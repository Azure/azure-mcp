// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.AppConfig.KeyValue;

public class KeyValueSetArguments : BaseKeyValueArguments
{
    [JsonPropertyName(ArgumentDefinitions.AppConfig.ValueName)]
    public string? Value { get; set; }
}
