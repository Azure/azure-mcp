// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.AppConfig.Options;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.AppConfig.Options.KeyValue;

public class BaseKeyValueOptions : BaseAppConfigOptions
{
    [JsonPropertyName(AppConfigOptionDefinitions.KeyName)]
    public string? Key { get; set; }

    [JsonPropertyName(AppConfigOptionDefinitions.LabelName)]
    public string? Label { get; set; }
}
