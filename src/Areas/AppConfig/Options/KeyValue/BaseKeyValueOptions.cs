// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.AppConfig.Options;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.AppConfig.Options.KeyValue;

public class BaseKeyValueOptions : BaseAppConfigOptions
{
    [JsonPropertyName(OptionDefinitions.AppConfig.KeyName)]
    public string? Key { get; set; }

    [JsonPropertyName(OptionDefinitions.AppConfig.LabelName)]
    public string? Label { get; set; }
}
