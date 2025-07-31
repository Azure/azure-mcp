// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.AppConfig.Options.KeyValue;

public class KeyValueListOptions : BaseAppConfigOptions
{
    [JsonPropertyName(AppConfigOptionDefinitions.KeyName)]
    public string? Key { get; set; }

    [JsonPropertyName(AppConfigOptionDefinitions.LabelName)]
    public string? Label { get; set; }
}
