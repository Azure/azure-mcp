// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.AppConfig.Options.KeyValue;

public class KeyValueSetOptions : BaseKeyValueOptions
{
    [JsonPropertyName(AppConfigOptionDefinitions.ValueName)]
    public string? Value { get; set; }
}
